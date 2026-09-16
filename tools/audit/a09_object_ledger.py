#!/usr/bin/env python3
"""A09 -- the full object / asset / reference / file ledger.

Section 3.2.7 requires a complete ledger instead of truncated samples, and section 3.2.8
requires every source file and every type -- including types that are not in the old
43-type export index and objects the ripper failed to export -- to be accounted for.

The counts that must be reported separately and never conflated (section 3.2.7):

  * source objects      (one row per SerializedFile object in the shipped data)
  * physical files      (one row per file on disk)
  * container paths     (one row per AssetBundle m_Container key)
  * catalog keys        (from the Unity runtime dump when available)
  * reference edges     (from a04)
  * reachable runtime types

Outputs (tools/unity-rebuild/out/):
  object_ledger.jsonl          every source object with identity + export status
  asset_inventory.csv          every dumped file with size, kind, container, addressable key
  file_ledger.csv              every physical input file with size + sha256 (optional)
  source_inventory.jsonl       every source container with object counts and hash
  unresolved_objects.csv       every source object that has no editor-side counterpart
  type_ledger.csv              every type, with counts, and whether the old index knew it
  ledger_summary.json          the separate counts above
"""
from __future__ import annotations

import collections
import csv
import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
import identity  # noqa: E402
from common import GAME_DATA_ROOT, OUT, RES_ROOT, load_index_ledger  # noqa: E402

A03_DIR = os.path.join(OUT, "a03")
PER_SOURCE = os.path.join(A03_DIR, "per_source")
REBUILD_OUT = os.path.join(os.path.dirname(OUT), "..", "unity-rebuild", "out")
REBUILD_OUT = os.path.normpath(REBUILD_OUT)

#: the 43 types the earlier export index covered (audit section 2)
LEGACY_INDEX_TYPES = {
    "AudioClip", "GameObject", "MonoBehaviour", "Transform", "MonoScript", "RectTransform",
    "Sprite", "SpriteRenderer", "CanvasRenderer", "Texture2D", "TextAsset", "Animator",
    "CanvasGroup", "AnimationClip", "PlayableDirector", "Material", "Shader", "Canvas",
    "Animation", "SortingGroup", "MeshFilter", "MeshRenderer", "ParticleSystem",
    "ParticleSystemRenderer", "MeshCollider", "Camera", "SpriteAtlas",
    "AnimatorController", "AudioMixerGroupController", "Font", "RotationConstraint",
    "SpriteMask", "VideoClip", "AudioSource", "AudioMixerController",
    "AudioMixerSnapshotController", "Light", "Mesh", "PreloadData", "ComputeShader",
    "LightmapSettings", "NavMeshSettings", "RenderSettings",
}


def iter_per_source():
    if not os.path.isdir(PER_SOURCE):
        raise SystemExit(f"{PER_SOURCE} missing -- run a03_sources.py first")
    for name in sorted(os.listdir(PER_SOURCE)):
        if not name.endswith(".json"):
            continue
        path = os.path.join(PER_SOURCE, name)
        try:
            with open(path, encoding="utf-8") as fh:
                yield json.load(fh)
        except (OSError, ValueError) as exc:
            print(f"  WARNING: unreadable scan result {name}: {exc}", file=sys.stderr)


def main():
    os.makedirs(REBUILD_OUT, exist_ok=True)
    ledger, orphans = load_index_ledger()

    # index rows by (source, pathID) for export-status matching
    indexed = {}
    for r in ledger.rows:
        indexed.setdefault((r["source_identity"], r["pid_int"]), r)
    # container path -> index rows
    rows_by_container = collections.defaultdict(list)
    for r in ledger.rows:
        if r["asset_path"]:
            rows_by_container[r["asset_path"]].append(r)
    # container key -> addressable locations (from a07, when it has run)
    addr_by_container = {}
    addr_path = os.path.join(OUT, "addressables.json")
    if os.path.exists(addr_path):
        try:
            with open(addr_path, encoding="utf-8") as fh:
                addr = json.load(fh)
        except (OSError, ValueError):
            addr = {}
    keys_gz = os.path.join(OUT, "addressable_keys.csv.gz")
    if os.path.exists(keys_gz):
        import gzip
        with gzip.open(keys_gz, "rt", encoding="utf-8", newline="") as fh:
            for row in csv.DictReader(fh):
                addr_by_container[row["addressable_key"]] = row

    type_counts = collections.Counter()
    type_exported = collections.Counter()
    type_in_index = set()
    ledger_path = os.path.join(REBUILD_OUT, "object_ledger.jsonl")
    unresolved_path = os.path.join(REBUILD_OUT, "unresolved_objects.csv")
    source_rows = []
    total_objects = 0
    exported = 0
    not_exported = collections.Counter()

    with open(ledger_path, "w", encoding="utf-8", newline="\n") as ledger_fh, \
            open(unresolved_path, "w", encoding="utf-8", newline="") as unres_fh:
        unres = csv.writer(unres_fh)
        unres.writerow(["source", "serialized_file", "path_id", "type", "reason",
                        "container", "dump_seq"])
        for src in iter_per_source():
            rel = src["rel"].replace("\\", "/")
            sfs = {sf["name"]: sf for sf in src.get("serialized_files", [])}
            exported_here = 0
            for obj in src.get("objects", []):
                total_objects += 1
                pid_raw = obj.get("path_id")
                try:
                    pid = identity.as_int64(pid_raw) if pid_raw is not None else None
                except ValueError:
                    pid = None
                row = indexed.get((rel, pid)) if pid is not None else None
                status = "exported" if row is not None else "not_exported"
                if row is not None:
                    exported += 1
                    exported_here += 1
                    type_in_index.add(obj["type"])
                else:
                    not_exported[obj["type"]] += 1
                type_counts[obj["type"]] += 1
                if row is not None:
                    type_exported[obj["type"]] += 1
                sf = sfs.get(obj.get("file"))
                ledger_fh.write(json.dumps({
                    "source": rel,
                    "serialized_file": obj.get("file"),
                    "serialized_file_unity_version": sf.get("unity_version") if sf else None,
                    "path_id": str(pid) if pid is not None else None,
                    "type": obj["type"],
                    "class_id": obj.get("class_id"),
                    "byte_size": obj.get("byte_size"),
                    "export_status": status,
                    "container": row["asset_path"] if row else None,
                    "output_file": row["output_file"] if row else None,
                    "dump_seq": row["dump_seq"] if row else None,
                }, ensure_ascii=False) + "\n")
                if row is None:
                    unres.writerow([rel, obj.get("file"), str(pid) if pid is not None else "",
                                    obj["type"], "no editor-side dump row", "", ""])
            source_rows.append({
                "rel": rel,
                "size": src.get("size"),
                "sha256": src.get("sha256"),
                "error": src.get("error"),
                "serialized_files": len(sfs),
                "objects_total": src.get("objects_total", 0),
                "objects_exported": exported_here,
                "objects_not_exported": src.get("objects_total", 0) - exported_here,
                "containers": sum(a.get("container_count", 0) for a in src.get("assetbundles", [])),
                "indexed_rows": src.get("indexed_rows", 0),
                "unity_versions": sorted({sf.get("unity_version") for sf in sfs.values()
                                          if sf.get("unity_version")}),
            })

    # ---- asset inventory (one row per dumped file) ------------------------
    with open(os.path.join(REBUILD_OUT, "asset_inventory.csv"), "w", encoding="utf-8",
              newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["output_file", "exists", "bytes", "kind", "type", "path_id",
                    "container", "source", "addressable_key", "addressable_location_count"])
        seen = set()
        for r in ledger.rows:
            if r["output_file"] in seen:
                continue
            seen.add(r["output_file"])
            full = os.path.join(RES_ROOT, r["output_file"].replace("/", os.sep))
            exists = os.path.exists(full)
            size = os.path.getsize(full) if exists else 0
            kind = os.path.splitext(r["output_file"])[1].lower().lstrip(".") or "(none)"
            addr = addr_by_container.get(r["asset_path"])
            w.writerow([r["output_file"], exists, size, kind, r["type"], str(r["pid_int"]),
                        r["asset_path"], r["source_identity"],
                        r["asset_path"] if addr else "",
                        (addr.get("location_count") or "") if addr else ""])

    # ---- source inventory -------------------------------------------------
    with open(os.path.join(REBUILD_OUT, "source_inventory.jsonl"), "w", encoding="utf-8",
              newline="\n") as fh:
        for row in sorted(source_rows, key=lambda x: x["rel"]):
            fh.write(json.dumps(row, ensure_ascii=False) + "\n")

    # ---- type ledger ------------------------------------------------------
    with open(os.path.join(REBUILD_OUT, "type_ledger.csv"), "w", encoding="utf-8",
              newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["type", "objects_total", "objects_exported", "objects_not_exported",
                    "in_legacy_43_type_index", "in_export_index"])
        for t, n in sorted(type_counts.items(), key=lambda kv: (-kv[1], kv[0])):
            w.writerow([t, n, type_exported.get(t, 0), not_exported.get(t, 0),
                        t in LEGACY_INDEX_TYPES, t in type_in_index])

    # ---- summary ----------------------------------------------------------
    physical_files = 0
    physical_bytes = 0
    if GAME_DATA_ROOT and os.path.isdir(GAME_DATA_ROOT):
        for root, dirs, files in os.walk(GAME_DATA_ROOT):
            for f in files:
                physical_files += 1
                try:
                    physical_bytes += os.path.getsize(os.path.join(root, f))
                except OSError:
                    pass
    container_paths = set()
    bundles_path = os.path.join(OUT, "bundles.json")
    if os.path.exists(bundles_path):
        with open(bundles_path, encoding="utf-8") as fh:
            for b in json.load(fh):
                for c in b.get("containers", []):
                    container_paths.add(c["key"])

    refs = {}
    ref_path = os.path.join(OUT, "ref_census.json")
    if os.path.exists(ref_path):
        with open(ref_path, encoding="utf-8") as fh:
            rc = json.load(fh)
        refs = {"refs_total": rc.get("refs_total"),
                "unresolved_total": rc.get("unresolved_total"),
                "unresolved_by_kind": rc.get("unresolved_by_kind")}

    summary = {
        "generated": __import__("time").strftime("%Y-%m-%dT%H:%M:%S"),
        "counts_are_separate_metrics": True,
        "source_objects_total": total_objects,
        "source_objects_exported": exported,
        "source_objects_not_exported": total_objects - exported,
        "source_objects_not_exported_by_type": dict(not_exported.most_common()),
        "index_rows": len(ledger.rows),
        "index_distinct_keys": ledger.stats["distinct_keys"],
        "physical_files_in_game_data": physical_files,
        "physical_bytes_in_game_data": physical_bytes,
        "assetbundle_container_paths": len(container_paths),
        "dumped_files_distinct": len({r["output_file"] for r in ledger.rows}),
        "distinct_source_containers": len(source_rows),
        "distinct_types": len(type_counts),
        "types_outside_legacy_43": sorted(t for t in type_counts if t not in LEGACY_INDEX_TYPES),
        "legacy_43_types_not_present": sorted(t for t in LEGACY_INDEX_TYPES if t not in type_counts),
        "json_files_not_in_index": len(orphans),
        "reference_census": refs,
        "index_stats": dict(ledger.stats),
        "index_defects_by_kind": dict(collections.Counter(d["kind"] for d in ledger.defects)),
    }
    with open(os.path.join(REBUILD_OUT, "ledger_summary.json"), "w", encoding="utf-8") as fh:
        json.dump(summary, fh, ensure_ascii=False, indent=1)

    print(json.dumps({k: v for k, v in summary.items()
                      if k not in ("types_outside_legacy_43", "legacy_43_types_not_present",
                                   "source_objects_not_exported_by_type", "index_defects_by_kind")},
                     ensure_ascii=False, indent=1))
    print("\ntypes outside the legacy 43-type index:",
          len(summary["types_outside_legacy_43"]), summary["types_outside_legacy_43"][:20])
    print("not exported by type:", dict(not_exported.most_common(15)))
    return 0


if __name__ == "__main__":
    sys.exit(main())
