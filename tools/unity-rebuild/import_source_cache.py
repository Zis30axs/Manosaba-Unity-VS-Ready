#!/usr/bin/env python3
"""One-time resource metadata/bundle snapshot, never called by ordinary builds.

Copies readable resource data only. Existing cache edits are protected by the
previous imported hash, as res edits are. A small reconstruction evidence snapshot
survives deletion of regenerable tools/unity-rebuild/out/.
"""
from __future__ import annotations

import argparse
from datetime import datetime, timezone
import hashlib
import json
from pathlib import Path
import shutil
import sys

from imported_source import REPO, res_root, within

EVIDENCE_PATTERNS = (
    "a13_import_ledger.csv", "a13_imported_paths.txt", "bundle_*script_map.json",
    "reconstructed_script_index.json", "addressables_plan_*.json", "so_convert_plan*.json",
    "prefab_plan_*.json", "diced_atlas_plan.json", "material_*plan.json",
    "material_embedded_pointer_map.json", "sprite_settings_plan.json", "texture_settings_plan.json",
    "catalog_dump_shipped*", "level0_*references.json", "level0_component_values.json",
    "naninovel_configs.json", "master_data_decode.json", "asset_guid_alignment.json",
    "naninovel_guid_remap.json", "so_imported_paths*.txt", "prefab_imported_paths*.txt",
)
FORBIDDEN_SUFFIXES = {".exe", ".dll", ".bat", ".cmd", ".ps1", ".com", ".asi", ".pyd", ".so", ".dylib"}
FORBIDDEN_RESOURCE_NAMES = {"global-metadata.dat"}
DATA_NAMES = {"level0", "globalgamemanagers", "boot.config", "app.info"}


def forbidden_resource(path: Path) -> bool:
    """Original runtime/plugin code is never an imported resource input."""
    return (path.suffix.lower() in FORBIDDEN_SUFFIXES or path.name.lower() in FORBIDDEN_RESOURCE_NAMES
            or any(part.lower() == "bepinex" for part in path.parts))


def digest(path: Path) -> str:
    h = hashlib.sha256()
    with path.open("rb") as stream:
        for block in iter(lambda: stream.read(1024 * 1024), b""):
            h.update(block)
    return h.hexdigest()


def read_json(path: Path) -> dict:
    if not path.exists():
        return {}
    return json.loads(path.read_text(encoding="utf-8-sig"))


def write_json(path: Path, value: dict) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_suffix(path.suffix + ".tmp")
    temporary.write_text(json.dumps(value, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    temporary.replace(path)


def snapshot_evidence(res: Path) -> list[dict]:
    destination = res / ".manosaba" / "reconstruction"
    sources: dict[str, Path] = {}
    unity = REPO / "tools/unity-rebuild/out/unity"
    for pattern in EVIDENCE_PATTERNS:
        for path in unity.glob(pattern):
            if path.is_file():
                sources["unity/" + path.name] = path
    for path in (REPO / "tools/audit/out/a03/per_source").glob("*.json"):
        sources["audit/per_source/" + path.name] = path
    records = {item['path']: item for item in read_json(destination / 'manifest.json').get('files', [])}
    for relative, source in sorted(sources.items()):
        target = within(destination, relative)
        target.parent.mkdir(parents=True, exist_ok=True)
        sha = digest(source)
        if not target.is_file() or digest(target) != sha:
            shutil.copy2(source, target)
        records[relative] = {"path": relative, "size": source.stat().st_size, "sha256": sha}
    # Empty out/ must never replace a previously complete snapshot with an empty one.
    if records:
        write_json(destination / "manifest.json", {"schema": 1, "files": list(records.values())})
    return list(records.values())


def restore_evidence(res: Path) -> int:
    source_root = res / ".manosaba/reconstruction"
    restored = 0
    for item in read_json(source_root / "manifest.json").get("files", []):
        source = within(source_root, item["path"])
        if digest(source) != item["sha256"]:
            raise ValueError("Imported reconstruction evidence hash mismatch: " + item["path"])
        relative = item["path"]
        if relative.startswith("unity/"):
            target = within(REPO / "tools/unity-rebuild/out/unity", relative[len("unity/"):])
        elif relative.startswith("audit/per_source/"):
            target = within(REPO / "tools/audit/out/a03/per_source", relative[len("audit/per_source/"):])
        else:
            raise ValueError("Unknown evidence destination: " + relative)
        if not target.exists():
            target.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(source, target)
            restored += 1
    return restored


def import_resources(game: Path, res: Path, overwrite: bool, include_bundles: bool) -> dict:
    data = within(game, "manosaba_Data")
    streaming = within(data, "StreamingAssets")
    if not streaming.is_dir():
        raise ValueError(f"Readable game StreamingAssets directory missing: {streaming}")
    cache = res / ".imported-source"
    manifest_path = cache / "manifest.json"
    previous = {r["path"]: r for r in read_json(manifest_path).get("files", [])}
    candidates = []
    for path in streaming.rglob("*"):
        if path.is_file() and not forbidden_resource(path.relative_to(streaming)):
            if include_bundles or path.suffix.lower() != ".bundle":
                candidates.append(path)
    for path in data.iterdir():
        if path.is_file() and (path.name in DATA_NAMES or path.suffix.lower() in {".assets", ".ress", ".resource"}):
            candidates.append(path)
    records, conflicts = [], []
    copied = unchanged = 0
    for source in sorted(candidates):
        relative = "manosaba_Data/" + source.relative_to(data).as_posix()
        source = within(game, source.relative_to(game).as_posix())
        target = within(cache, relative)
        sha = digest(source)
        current = digest(target) if target.is_file() else None
        old = previous.get(relative, {}).get("original_sha256")
        modified = current is not None and current != sha and (old is None or current != old)
        action = "UNCHANGED" if current == sha else "COPIED"
        if modified and not overwrite:
            action = "USER_MODIFIED"
            conflicts.append(relative)
        elif current != sha:
            target.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(source, target)
            copied += 1
        else:
            unchanged += 1
        target_sha = current if action in ("UNCHANGED", "USER_MODIFIED") else digest(target)
        if action == "COPIED" and target_sha != sha:
            raise ValueError("Resource changed during import or copy verification failed: " + relative)
        records.append({"path": relative, "size": source.stat().st_size,
                        "original_sha256": old if action == "USER_MODIFIED" else sha,
                        "sha256": target_sha, "status": action,
                        "category": "BUNDLE" if source.suffix.lower() == ".bundle" else "RESOURCE_METADATA"})
    # Partial imports retain records for cached resources not selected by this run.
    by_path = dict(previous)
    by_path.update({r["path"]: r for r in records})
    result = {"schema": 1, "generated": datetime.now(timezone.utc).isoformat(),
              "kind": "imported-resource-source", "files": list(by_path.values()),
              "copied": copied, "unchanged": unchanged, "user_modified": conflicts,
              "executable_inputs": False}
    write_json(manifest_path, result)
    return result


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-root", type=Path)
    parser.add_argument("--res-root", type=Path, default=res_root())
    parser.add_argument("--overwrite-user-changes", action="store_true")
    parser.add_argument("--metadata-only", action="store_true", help="Omit bundles for an existing extracted workspace")
    parser.add_argument("--snapshot-only", action="store_true")
    parser.add_argument("--restore-evidence", action="store_true")
    args = parser.parse_args()
    try:
        res = args.res_root.resolve()
        if args.restore_evidence:
            print(json.dumps({"status": "OK", "evidence_restored": restore_evidence(res)}))
            return 0
        result = {}
        if not args.snapshot_only:
            if not args.game_root:
                parser.error("--game-root is required for resource import")
            game = args.game_root.resolve()
            if game == res or game in res.parents or res in game.parents:
                raise ValueError("Game install and editable workspace must be separate directories")
            result = import_resources(game, res, args.overwrite_user_changes, not args.metadata_only)
        evidence = snapshot_evidence(res)
        print(json.dumps({"status": "OK", "copied": result.get("copied", 0),
                          "unchanged": result.get("unchanged", 0),
                          "user_modified": len(result.get("user_modified", [])),
                          "evidence_files": len(evidence)}, ensure_ascii=False))
        return 0
    except (OSError, ValueError, KeyError) as error:
        print(f"FAIL: {error}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
