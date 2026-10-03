#!/usr/bin/env python3
"""Compare a local extracted resource index with the repository's identity map.

The map deliberately contains identities and logical paths only.  It does not
contain dumped bytes, Unity metadata, GUIDs, absolute paths, or generated
GameContent.  Every developer still needs a readable dump of the same game
version; the map makes that requirement explicit and makes suffix/path changes
(`~1`, `~import2`, and similar) reviewable instead of silently changing the
object a sync targets.
"""
from __future__ import annotations

import argparse
import csv
import hashlib
import json
from collections import defaultdict
from pathlib import Path
import sys
from typing import Iterable


REPO = Path(__file__).resolve().parents[2]
DEFAULT_INDEX = REPO / "res" / "_index.csv"
DEFAULT_MAP = REPO / "tools" / "content" / "resource-map.csv"
DEFAULT_META = REPO / "tools" / "content" / "resource-map.meta.json"
FIELDS = ("type", "name", "asset_path", "source_bundle", "path_id", "sample_id", "output_file")
MAP_FIELDS = FIELDS + ("identity_hint",)
IDENTITY_FIELDS = ("type", "source_bundle", "path_id", "sample_id")
SCHEMA = 1


class ResourceMapError(ValueError):
    pass


def normal(value: object) -> str:
    return str(value or "").replace("\\", "/")


def row_normal(row: dict[str, object]) -> dict[str, str]:
    value = {field: normal(row.get(field, "")) for field in FIELDS}
    if not value["type"] or not value["source_bundle"] or not value["path_id"]:
        raise ResourceMapError("Every index row needs type, source_bundle, and path_id")
    return value


def add_duplicate_hints(rows: list[dict[str, str]]) -> list[dict[str, str]]:
    """Disambiguate recovered rows whose original identity was lost."""
    groups: dict[tuple[str, ...], list[dict[str, str]]] = defaultdict(list)
    for row in rows:
        groups[tuple(row[field] for field in IDENTITY_FIELDS)].append(row)
    for group in groups.values():
        if len(group) == 1:
            group[0]["identity_hint"] = ""
            continue
        # These rows cannot safely follow a renamed ~N output. Including the
        # current output path makes a later ambiguous remap fail closed.
        for row in group:
            payload = "\x1f".join(row[field] for field in ("type", "name", "asset_path", "output_file"))
            row["identity_hint"] = hashlib.sha256(payload.encode("utf-8")).hexdigest()[:16]
    return rows


def key(row: dict[str, str]) -> tuple[str, ...]:
    return tuple(row[field] for field in IDENTITY_FIELDS) + (row.get("identity_hint", ""),)


def read_index(path: Path) -> list[dict[str, str]]:
    if not path.is_file():
        raise ResourceMapError(f"Resource index is missing: {path}")
    try:
        with path.open(encoding="utf-8-sig", newline="") as stream:
            rows = add_duplicate_hints([row_normal(row) | {"identity_hint": normal(row.get("identity_hint", ""))}
                                        for row in csv.DictReader(stream)])
    except (OSError, UnicodeError, csv.Error) as exc:
        raise ResourceMapError(f"Cannot read resource index {path}: {exc}") from exc
    if not rows:
        raise ResourceMapError(f"Resource index is empty: {path}")
    return rows


def indexed(rows: Iterable[dict[str, str]], label: str) -> dict[tuple[str, ...], dict[str, str]]:
    result: dict[tuple[str, ...], dict[str, str]] = {}
    for row in rows:
        identity = key(row)
        if identity in result:
            raise ResourceMapError(f"Duplicate resource identity in {label}: {identity}")
        result[identity] = row
    return result


def sorted_rows(rows: Iterable[dict[str, str]]) -> list[dict[str, str]]:
    return sorted(rows, key=lambda row: (key(row), row["asset_path"], row["output_file"]))


def semantic_payload(rows: Iterable[dict[str, str]]) -> bytes:
    """Hash identity and logical container/name, ignoring incidental output names."""
    values = [
        {field: row.get(field, "") for field in ("type", "name", "asset_path", "source_bundle", "path_id", "sample_id", "identity_hint")}
        for row in sorted_rows(rows)
    ]
    return (json.dumps(values, ensure_ascii=False, separators=(",", ":"), sort_keys=True) + "\n").encode("utf-8")


def digest(rows: Iterable[dict[str, str]]) -> str:
    return hashlib.sha256(semantic_payload(rows)).hexdigest()


def write_map(path: Path, rows: list[dict[str, str]]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_name(path.name + ".tmp")
    with temporary.open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=MAP_FIELDS, lineterminator="\n")
        writer.writeheader()
        writer.writerows(sorted_rows(rows))
    temporary.replace(path)


def write_meta(path: Path, map_path: Path, rows: list[dict[str, str]]) -> None:
    data = {
        "schema": SCHEMA,
        "map": map_path.name,
        "entries": len(rows),
        "semantic_sha256": digest(rows),
        "identity_fields": list(IDENTITY_FIELDS),
        "notes": "Generated from a local res/_index.csv; output_file may be remapped by a later import.",
    }
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_name(path.name + ".tmp")
    temporary.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    temporary.replace(path)


def load_map(path: Path) -> list[dict[str, str]]:
    rows = read_index(path)
    return rows


def verify(index_path: Path, map_path: Path, meta_path: Path | None, check_files: bool) -> dict:
    local = indexed(read_index(index_path), str(index_path))
    expected = indexed(load_map(map_path), str(map_path))
    missing = sorted(set(expected) - set(local))
    extra = sorted(set(local) - set(expected))
    changed = []
    remapped = []
    for identity in sorted(set(expected) & set(local)):
        wanted, actual = expected[identity], local[identity]
        fields = [field for field in ("type", "name", "asset_path") if wanted[field] != actual[field]]
        if fields:
            changed.append({"identity": identity, "fields": fields,
                            "expected": {field: wanted[field] for field in fields},
                            "actual": {field: actual[field] for field in fields}})
        if wanted["output_file"] != actual["output_file"]:
            remapped.append({"identity": identity, "expected": wanted["output_file"],
                             "actual": actual["output_file"]})
    missing_files = []
    if check_files:
        root = index_path.parent
        for row in local.values():
            candidate = root / row["output_file"]
            if not candidate.is_file():
                missing_files.append(row["output_file"])
    actual_digest = digest(local.values()) if not missing and not extra and not changed else None
    expected_digest = digest(expected.values())
    meta_error = None
    if meta_path and meta_path.is_file():
        try:
            meta = json.loads(meta_path.read_text(encoding="utf-8-sig"))
            if meta.get("schema") != SCHEMA or meta.get("semantic_sha256") != expected_digest:
                meta_error = "resource-map.meta.json does not match resource-map.csv"
        except (OSError, UnicodeError, ValueError) as exc:
            meta_error = f"Cannot read metadata: {exc}"
    report = {
        "status": "PASS" if not (missing or extra or changed or missing_files or meta_error) else "FAIL",
        "index": str(index_path), "map": str(map_path), "entries": len(local),
        "expected_entries": len(expected), "missing": len(missing), "extra": len(extra),
        "changed": len(changed), "output_remapped": len(remapped), "missing_files": len(missing_files),
        "expected_semantic_sha256": expected_digest, "actual_semantic_sha256": actual_digest,
        "meta_error": meta_error, "details": {"missing": missing[:20], "extra": extra[:20],
        "changed": changed[:20], "output_remapped": remapped[:20], "missing_files": missing_files[:20]},
    }
    return report


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    commands = parser.add_subparsers(dest="command", required=True)
    generate = commands.add_parser("generate", help="write the tracked identity map from a local index")
    generate.add_argument("--index", type=Path, default=DEFAULT_INDEX)
    generate.add_argument("--map", type=Path, default=DEFAULT_MAP)
    generate.add_argument("--meta", type=Path, default=DEFAULT_META)
    check = commands.add_parser("verify", help="compare a local index with the tracked map")
    check.add_argument("--index", type=Path, default=DEFAULT_INDEX)
    check.add_argument("--map", type=Path, default=DEFAULT_MAP)
    check.add_argument("--meta", type=Path, default=DEFAULT_META)
    check.add_argument("--check-files", action="store_true", help="also check every output_file exists beside the index")
    check.add_argument("--report", type=Path, help="write the full JSON report")
    args = parser.parse_args(argv)
    try:
        if args.command == "generate":
            rows = read_index(args.index)
            indexed(rows, str(args.index))
            write_map(args.map, rows)
            write_meta(args.meta, args.map, rows)
            print(json.dumps({"status": "GENERATED", "entries": len(rows),
                              "semantic_sha256": digest(rows), "map": str(args.map)}, ensure_ascii=False))
            return 0
        report = verify(args.index, args.map, args.meta, args.check_files)
        if args.report:
            args.report.parent.mkdir(parents=True, exist_ok=True)
            args.report.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
        print(json.dumps({key: value for key, value in report.items() if key != "details"}, ensure_ascii=False))
        if report["status"] != "PASS":
            print(json.dumps(report["details"], ensure_ascii=False, indent=2), file=sys.stderr)
        return 0 if report["status"] == "PASS" else 1
    except (OSError, ValueError, ResourceMapError) as exc:
        print(f"[resource-map] FAIL: {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
