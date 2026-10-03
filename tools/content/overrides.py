#!/usr/bin/env python3
"""Share reviewed res edits without replacing another developer's asset objects.

Only UTF-8 text and indexed MonoBehaviour m_text strings are supported. This
tool never imports resources, touches Unity metadata, or builds Addressables.
"""
from __future__ import annotations

import argparse
import copy
import csv
import hashlib
import json
import os
from pathlib import Path, PurePosixPath
import re
import sys
import tempfile

REPO = Path(__file__).resolve().parents[2]
MAX_FILE_BYTES = 4 * 1024 * 1024
MAX_MANIFEST_BYTES = 16 * 1024 * 1024
MAX_ENTRIES = 4096
IDENTITY_KEYS = {"type", "source_bundle", "path_id"}


class OverrideError(ValueError):
    pass


def digest(data):
    return hashlib.sha256(data).hexdigest()


def relative_path(value):
    if not isinstance(value, str) or not value or ":" in value or "\x00" in value:
        raise OverrideError("Invalid resource path")
    value = value.replace("\\", "/")
    parts = value.split("/")
    if any(part in ("", ".", "..") for part in parts):
        raise OverrideError(f"Unsafe resource path: {value}")
    if parts[0] not in ("Assets", "_serialized"):
        raise OverrideError(f"Only Assets/ and _serialized/ resource edits are supported: {value}")
    return PurePosixPath(value).as_posix()


def within_root(root, relative):
    root = Path(root).resolve()
    candidate = root
    for part in PurePosixPath(relative).parts:
        candidate = candidate / part
        if candidate.is_symlink() or getattr(candidate, "is_junction", lambda: False)():
            raise OverrideError(f"Resource paths cannot traverse symbolic links or junctions: {relative}")
    target = candidate.resolve()
    if not target.is_relative_to(root) or target == root:
        raise OverrideError(f"Path escapes source root: {relative}")
    return target


def read_bytes(path, limit=MAX_FILE_BYTES):
    if not path.is_file():
        raise OverrideError(f"Resource is missing: {path}")
    if path.stat().st_size > limit:
        raise OverrideError(f"Resource exceeds {limit} byte limit: {path}")
    return path.read_bytes()


def load_json_bytes(data):
    try:
        return json.loads(data.decode("utf-8-sig"))
    except (UnicodeError, ValueError) as exc:
        raise OverrideError(f"Invalid UTF-8 JSON: {exc}") from exc


def replace_top_level_m_text(data, before, after):
    """Change only the serialized m_text token, preserving every other byte."""
    pattern = re.compile(rb'(?m)^(\s*"m_text"\s*:\s*)("(?:\\.|[^"\\])*")')
    matches = list(pattern.finditer(data))
    if len(matches) != 1:
        raise OverrideError("Source object has an ambiguous serialized m_text")
    token = matches[0].group(2)
    try:
        current = json.loads(token.decode("utf-8"))
    except (UnicodeError, ValueError) as exc:
        raise OverrideError("Source object has invalid serialized m_text") from exc
    if current != before:
        raise OverrideError("Source object m_text changed during preflight")
    replacement = json.dumps(after, ensure_ascii=False).encode("utf-8")
    return data[:matches[0].start(2)] + replacement + data[matches[0].end(2):]


def identity(value):
    if not isinstance(value, dict) or set(value) != IDENTITY_KEYS:
        raise OverrideError("Identity requires type, source_bundle, and path_id")
    if not all(isinstance(value[key], str) and value[key] for key in IDENTITY_KEYS):
        raise OverrideError("Object identity values must be nonempty strings")
    bundle = value["source_bundle"].replace("\\", "/")
    if ":" in bundle or bundle.startswith("/") or ".." in bundle.split("/"):
        raise OverrideError("Object identity must use a relative source bundle")
    if not re.fullmatch(r"-?\d+", value["path_id"]):
        raise OverrideError("Invalid object path_id")
    return {"type": value["type"], "source_bundle": bundle, "path_id": str(int(value["path_id"]))}


def identity_key(value):
    value = identity(value)
    return tuple(value[key] for key in ("type", "source_bundle", "path_id"))


def index_records(root):
    index = within_root(root, "_index.csv")
    if not index.is_file():
        return []
    records = []
    with index.open(encoding="utf-8-sig", newline="") as stream:
        for row in csv.DictReader(stream):
            if not row.get("output_file"):
                continue
            records.append({"path": row["output_file"].replace("\\", "/"),
                            "identity": {key: row.get(key, "") for key in IDENTITY_KEYS}})
    return records


def indexed_identity(records, path):
    matching = [row for row in records if row["path"] == path]
    if len(matching) > 1:
        raise OverrideError(f"Ambiguous source index path: {path}")
    return identity(matching[0]["identity"]) if matching else None


def locate(root, records, entry):
    path = relative_path(entry["path"])
    if "identity" in entry:
        wanted = identity_key(entry["identity"])
        matches = [row for row in records if identity_key(row["identity"]) == wanted]
        if len(matches) != 1:
            raise OverrideError(f"Object identity has {len(matches)} matches: {path}; import the matching resource version first")
        path = relative_path(matches[0]["path"])
    if entry.get("kind") == "text" and not path.endswith((".txt", ".nani")):
        raise OverrideError(f"Indexed text identity does not resolve to an editable text file: {path}")
    if entry.get("kind") == "json-fields" and not path.endswith(".asset.json"):
        raise OverrideError(f"Indexed object identity does not resolve to source JSON: {path}")
    return path, within_root(root, path)


def validate_manifest(manifest):
    if not isinstance(manifest, dict) or type(manifest.get("version")) is not int or manifest["version"] != 1:
        raise OverrideError("Unsupported override manifest version")
    if set(manifest) - {"version", "description", "entries"}:
        raise OverrideError("Unknown manifest properties")
    if "description" in manifest and not isinstance(manifest["description"], str):
        raise OverrideError("Description must be a string")
    entries = manifest.get("entries")
    if not isinstance(entries, list) or len(entries) > MAX_ENTRIES:
        raise OverrideError("Manifest entries must be a bounded list")
    seen = set()
    for entry in entries:
        if not isinstance(entry, dict):
            raise OverrideError("Each entry must be an object")
        kind = entry.get("kind")
        allowed = {"kind", "path", "identity"}
        if kind == "json-fields":
            allowed |= {"fields"}
            if not str(entry.get("path", "")).endswith(".asset.json"):
                raise OverrideError("JSON overrides require .asset.json source objects")
            if "identity" not in entry or identity(entry["identity"])["type"] != "MonoBehaviour":
                raise OverrideError("JSON overrides require indexed MonoBehaviour identity")
            fields = entry.get("fields")
            if not isinstance(fields, dict) or set(fields) != {"m_text"}:
                raise OverrideError("Only the m_text field is editable by this tool")
            change = fields["m_text"]
            if not isinstance(change, dict) or set(change) != {"before", "after"}:
                raise OverrideError("m_text requires before and after values")
            if not all(isinstance(change[key], str) for key in ("before", "after")):
                raise OverrideError("m_text before and after must be strings")
        elif kind == "text":
            allowed |= {"before_sha256", "after_sha256", "content"}
            if not str(entry.get("path", "")).endswith((".txt", ".nani")):
                raise OverrideError("Text overrides require .txt or .nani source files")
            if not isinstance(entry.get("content"), str):
                raise OverrideError("Text content must be a UTF-8 string")
            payload = entry["content"].encode("utf-8")
            if len(payload) > MAX_FILE_BYTES:
                raise OverrideError("Text override is too large")
            for key in ("before_sha256", "after_sha256"):
                if not isinstance(entry.get(key), str) or not re.fullmatch(r"[0-9a-f]{64}", entry[key]):
                    raise OverrideError(f"Invalid {key}; a real baseline is required")
            if digest(payload) != entry["after_sha256"]:
                raise OverrideError("Text content does not match after_sha256")
        else:
            raise OverrideError(f"Unsupported override kind: {kind}")
        if set(entry) - allowed:
            raise OverrideError("Unknown override entry properties")
        path = relative_path(entry.get("path"))
        if "identity" in entry:
            key = ("identity", identity_key(entry["identity"]))
        else:
            key = ("path", path.casefold())
        if key in seen:
            raise OverrideError(f"Duplicate override entry: {path}")
        seen.add(key)
    return manifest


def plan_apply(manifest, source_root):
    validate_manifest(manifest)
    records = index_records(source_root)
    plans, conflicts, resolved = [], [], set()
    for entry in manifest["entries"]:
        try:
            relative, target = locate(source_root, records, entry)
            if target in resolved:
                raise OverrideError(f"Multiple entries resolve to one resource: {relative}")
            resolved.add(target)
            before = read_bytes(target)
            if entry["kind"] == "json-fields":
                document = load_json_bytes(before)
                if not isinstance(document, dict) or not isinstance(document.get("m_text"), str):
                    raise OverrideError(f"Source object has no string m_text: {relative}")
                change = entry["fields"]["m_text"]
                if document["m_text"] == change["after"]:
                    after = before
                elif document["m_text"] == change["before"]:
                    after = replace_top_level_m_text(before, change["before"], change["after"])
                else:
                    raise OverrideError(f"m_text differs from both baseline and shared value: {relative}")
            else:
                current = digest(before)
                if current == entry["after_sha256"]:
                    after = before
                elif current == entry["before_sha256"]:
                    after = entry["content"].encode("utf-8")
                else:
                    raise OverrideError(f"Text differs from both baseline and shared value: {relative}")
            plans.append({"path": relative, "target": target, "before": before, "after": after})
        except OverrideError as exc:
            conflicts.append(str(exc))
    if conflicts:
        raise OverrideError("No resources were written; resolve conflicts first:\n" + "\n".join(conflicts))
    return plans


def atomic_write(path, data):
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = None
    try:
        with tempfile.NamedTemporaryFile(prefix=path.name + ".", suffix=".tmp", dir=path.parent, delete=False) as stream:
            temporary = Path(stream.name)
            stream.write(data)
        os.replace(temporary, path)
    finally:
        if temporary is not None:
            temporary.unlink(missing_ok=True)


def apply_manifest(manifest, source_root, apply=False):
    plans = plan_apply(manifest, source_root)
    changes = [plan for plan in plans if plan["before"] != plan["after"]]
    if apply:
        # Recheck the complete set before writing, including already-applied entries.
        for plan in plans:
            if within_root(source_root, plan["path"]) != plan["target"] or read_bytes(plan["target"]) != plan["before"]:
                raise OverrideError("Source changed during preflight; no resources were written")
        written = []
        try:
            for plan in changes:
                atomic_write(plan["target"], plan["after"])
                written.append(plan)
        except OSError:
            for plan in reversed(written):
                atomic_write(plan["target"], plan["before"])
            raise
    return {"mode": "apply" if apply else "dry-run", "changed": [plan["path"] for plan in changes],
            "already_applied": [plan["path"] for plan in plans if plan["before"] == plan["after"]]}


def export_manifest(source_root, base_root, files, description=""):
    records, bases = index_records(source_root), index_records(base_root)
    entries = []
    for raw_path in files:
        path = relative_path(raw_path.replace("\\", "/").removeprefix("res/"))
        object_identity = indexed_identity(records, path)
        locator = {"path": path}
        if object_identity:
            locator["identity"] = object_identity
        _, base = locate(base_root, bases, locator)
        before = read_bytes(base)
        after = read_bytes(within_root(source_root, path))
        if before == after:
            continue
        if path.endswith(".asset.json"):
            if not object_identity or object_identity["type"] != "MonoBehaviour":
                raise OverrideError(f"JSON resource needs an indexed MonoBehaviour: {path}")
            old, new = load_json_bytes(before), load_json_bytes(after)
            if not isinstance(old, dict) or not isinstance(new, dict) or not all(isinstance(doc.get("m_text"), str) for doc in (old, new)):
                raise OverrideError(f"JSON resource needs a string m_text: {path}")
            # Refuse to silently omit unsupported edits from an explicitly selected object.
            unchanged_old, unchanged_new = copy.deepcopy(old), copy.deepcopy(new)
            unchanged_old.pop("m_text")
            unchanged_new.pop("m_text")
            if unchanged_old != unchanged_new:
                raise OverrideError(f"JSON has changes outside supported m_text: {path}")
            if old["m_text"] == new["m_text"]:
                continue
            entry = {"kind": "json-fields", **locator,
                     "fields": {"m_text": {"before": old["m_text"], "after": new["m_text"]}}}
        elif path.endswith((".txt", ".nani")):
            try:
                before.decode("utf-8-sig")
                text = after.decode("utf-8")
            except UnicodeError as exc:
                raise OverrideError(f"Text resource is not UTF-8: {path}") from exc
            entry = {"kind": "text", **locator, "before_sha256": digest(before),
                     "after_sha256": digest(after), "content": text}
        else:
            raise OverrideError(f"Unsupported resource type: {path}")
        entries.append(entry)
    return validate_manifest({"version": 1, "description": description, "entries": entries})


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    subparsers = parser.add_subparsers(dest="command", required=True)
    for command in ("export", "apply"):
        child = subparsers.add_parser(command)
        child.add_argument("--source-root", type=Path, default=REPO / "res")
        mode = child.add_mutually_exclusive_group()
        mode.add_argument("--apply", action="store_true", help="Write the reviewed changes; otherwise only preview")
        mode.add_argument("--dry-run", action="store_true", help="Preview only (the default)")
        if command == "export":
            child.add_argument("--base-root", type=Path, required=True, help="Explicit unmodified/previous res baseline")
            child.add_argument("--file", action="append", required=True, help="Selected path relative to res; repeatable")
            child.add_argument("--output", type=Path, required=True)
            child.add_argument("--description", default="")
        else:
            child.add_argument("manifest", type=Path)
    args = parser.parse_args(argv)
    try:
        if args.command == "export":
            manifest = export_manifest(args.source_root, args.base_root, args.file, args.description)
            payload = (json.dumps(manifest, ensure_ascii=False, indent=2) + "\n").encode("utf-8")
            if len(payload) > MAX_MANIFEST_BYTES:
                raise OverrideError("Export manifest is too large")
            if args.apply:
                if args.output.exists():
                    raise OverrideError("Export output already exists; choose a new path and review the difference")
                atomic_write(args.output.resolve(), payload)
            report = {"mode": "export" if args.apply else "dry-run", "output": str(args.output),
                      "entries": len(manifest["entries"]), "paths": [entry["path"] for entry in manifest["entries"]]}
        else:
            manifest = load_json_bytes(read_bytes(args.manifest, MAX_MANIFEST_BYTES))
            report = apply_manifest(manifest, args.source_root, args.apply)
        print(json.dumps(report, ensure_ascii=False, indent=2))
        return 0
    except (OverrideError, OSError) as exc:
        print(f"[overrides] {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
