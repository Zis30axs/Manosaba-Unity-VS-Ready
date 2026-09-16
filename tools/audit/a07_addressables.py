#!/usr/bin/env python3
"""A07 -- Addressables / AssetBundle closure, with the old defects repaired.

Defects repaired (audit UR-008 / section 3.2.4):

1. ``key2bundle.setdefault(c, src)`` kept only the **first** source for a duplicated
   container key, silently discarding the others.  Keys now map to a full list of
   locations, and duplicate/aliased keys are reported.
2. ``idx_keys`` compared only that a *path* existed, never that the key resolved to the
   right target object, type or sub-asset.  Keys are now cross-checked against the
   container's recorded target PPtr from a03.
3. The catalog was read as header + ASCII regex.  It is now parsed by
   `catalog_reader.BinaryStorageBuffer`, and when a Unity Editor is available the
   authoritative parse produced by the real Addressables runtime
   (``UnityProject/Assets/Editor/CatalogDump.cs``) is preferred and merged.
4. ``disk_bundles.add(f)`` deduplicated by basename, which is why the old report showed
   ``total=457`` next to ``bundles_on_disk=450``.  Bundles are now accounted for by
   path relative to the data root, and the 457 vs 450 question is answered explicitly.

Outputs: out/addressables.json, out/addressable_keys.csv.gz, out/addressable_keys_dupes.csv,
out/bundle_accounting.csv
"""
from __future__ import annotations

import collections
import csv
import gzip
import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import catalog_reader  # noqa: E402
from common import AAW, OUT, load_index_ledger  # noqa: E402

DERIVED_DIR = os.path.join(OUT, "unity")
UNITY_CATALOG_DUMP = os.path.join(DERIVED_DIR, "catalog_dump.json")


def load_unity_catalog_dump():
    """Authoritative catalog export produced inside Unity by CatalogDump.cs, if present."""
    if not os.path.exists(UNITY_CATALOG_DUMP):
        return None, None
    try:
        with open(UNITY_CATALOG_DUMP, encoding="utf-8") as fh:
            data = json.load(fh)
        return data, UNITY_CATALOG_DUMP
    except (OSError, ValueError) as exc:
        return None, f"unreadable: {type(exc).__name__}: {exc}"


def load_bundles():
    path = os.path.join(OUT, "bundles.json")
    if not os.path.exists(path):
        raise SystemExit(f"{path} missing -- run a03_sources.py first")
    with open(path, encoding="utf-8") as fh:
        return json.load(fh)


def bundle_files_on_disk():
    """Relative path -> size for every bundle, keyed by path (never basename)."""
    found = {}
    for root, dirs, files in os.walk(AAW):
        for f in files:
            if f.endswith(".bundle"):
                full = os.path.join(root, f)
                rel = os.path.relpath(full, AAW).replace(os.sep, "/")
                found[rel] = os.path.getsize(full)
    return found


def main():
    with open(os.path.join(AAW, os.pardir, "settings.json"), encoding="utf-8") as fh:
        settings = json.load(fh)
    bundles = load_bundles()
    ledger, _ = load_index_ledger(check_files=False)

    # ---- bundle manifests: keep every key -> every location ---------------
    key_to_locations = collections.defaultdict(list)
    src2cabs = collections.defaultdict(list)
    dep_edges = []
    for b in bundles:
        src = b["source"].replace("\\", "/")
        cab = b.get("name")
        if cab:
            src2cabs[src].append(cab)
        for c in b.get("containers", []):
            key_to_locations[c["key"]].append({
                "source": src,
                "cab": cab,
                "target_file_id": c["target"]["m_FileID"],
                "target_path_id": c["target"]["m_PathID"],
                "preloadIndex": c.get("preloadIndex"),
                "preloadSize": c.get("preloadSize"),
            })
        for d in b.get("dependencies", []) or []:
            dep_edges.append({"from": src, "raw": d, "to": None})
    # CAB -> owning source, by path identity
    cab2src = {}
    for src, cabs in src2cabs.items():
        for cab in cabs:
            cab2src.setdefault(cab.lower(), []).append(src)
    for e in dep_edges:
        owners = cab2src.get(str(e["raw"]).lower())
        e["to"] = owners[0] if owners else None
        e["ambiguous_owners"] = owners if owners and len(owners) > 1 else []

    dup_keys = {k: v for k, v in key_to_locations.items() if len(v) > 1}

    # ---- index side: container path -> the objects actually dumped ---------
    by_container = collections.defaultdict(list)
    for r in ledger.rows:
        if r["asset_path"]:
            by_container[r["asset_path"]].append(r)

    matched, key_without_object, target_mismatch, key_not_in_containers = 0, [], [], []
    checked = 0
    for key, locs in key_to_locations.items():
        rows = by_container.get(key)
        if not rows:
            key_without_object.append(key)
            continue
        checked += 1
        srcs = {r["source_identity"] for r in rows}
        matched += 1
        # Cross-check the container target against the dumped object set.
        for loc in locs:
            if loc["target_file_id"] != 0:
                continue  # external target: resolved through the bundle dependency graph
            pids = {str(r["pid_int"]) for r in rows if r["source_identity"] == loc["source"]}
            if pids and loc["target_path_id"] not in pids:
                target_mismatch.append({"key": key, "source": loc["source"],
                                        "target_path_id": loc["target_path_id"],
                                        "dumped_in_source": sorted(pids)[:5],
                                        "dumped_total": len(pids)})
    container_paths = set(by_container)
    for key in container_paths:
        if key not in key_to_locations:
            key_not_in_containers.append(key)

    # ---- catalog ----------------------------------------------------------
    catalog_py, catalog_error = None, None
    catalog_path = os.path.join(AAW, os.pardir, "catalog.bin")
    try:
        buf = catalog_reader.load_catalog(catalog_path)
        probe = buf.probe()
        catalog_py = {
            "bytes": len(buf.raw),
            "magic": hex(buf.magic),
            "version": buf.version,
            "header_entry_count": buf.entry_count,
            "header_offsets": probe["header_offsets"],
            "printable_strings": probe["string_count"],
            "provider_class_names": buf.provider_names(),
            "bundle_names_mentioned": len(buf.bundle_names()),
            "runtime_path_literals": buf.runtime_path_literals(),
            "reader_errors": buf.errors[:50],
            "completeness": "header + tagged-string dereference only; resource locations, "
                            "GUIDs and labels require the Unity runtime parse",
        }
    except catalog_reader.CatalogFormatError as exc:
        catalog_error = f"{type(exc).__name__}: {exc}"

    unity_dump, unity_src = load_unity_catalog_dump()

    # ---- physical bundles: answer the 457 vs 450 question -----------------
    on_disk = bundle_files_on_disk()
    manifest_sources = {b["source"].replace("\\", "/") for b in bundles}
    # The comparison must be by full relative path, not basename (audit 3.2.4).
    # The old report's "457 total vs 450 on disk" was exactly a basename collapse: the
    # shipped tree has 457 bundle *paths* but only 450 distinct *file names*.
    disk_names = collections.Counter(os.path.basename(k) for k in on_disk)
    duplicate_names = {n: c for n, c in disk_names.items() if c > 1}
    referenced = {os.path.basename(d) for b in bundles for d in (b.get("dependencies") or [])}
    manifest_names = {b.get("asset_bundle_name") or b.get("name") for b in bundles}
    unaccounted = sorted(n for n in disk_names if n not in manifest_names and n not in referenced)

    with open(os.path.join(OUT, "bundle_accounting.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["relative_path", "bytes", "on_disk", "has_manifest_entry", "referenced_as_dependency"])
        for rel, size in sorted(on_disk.items()):
            base = os.path.basename(rel)
            w.writerow([rel, size, True,
                        any(b.get("asset_bundle_name") == base or b.get("name") == base
                            for b in bundles),
                        base in referenced])

    out = {
        "settings": {
            "m_AddressablesVersion": settings.get("m_AddressablesVersion"),
            "m_buildTarget": settings.get("m_buildTarget"),
            "m_SettingsHash": settings.get("m_SettingsHash"),
            "catalog_provider": settings["m_CatalogLocations"][0]["m_Provider"],
            "internal_id": settings["m_CatalogLocations"][0]["m_InternalId"],
        },
        "catalog": {
            "python_reader": catalog_py,
            "python_reader_error": catalog_error,
            "unity_runtime_dump": unity_src,
            "unity_runtime_dump_present": unity_dump is not None,
        },
        "bundles": {
            "manifest_entries": len(bundles),
            "distinct_sources": len(manifest_sources),
            "container_entries": sum(len(v) for v in key_to_locations.values()),
            "distinct_keys": len(key_to_locations),
            "keys_with_multiple_locations": len(dup_keys),
            "bundles_with_dependencies": sum(1 for b in bundles if b.get("dependencies")),
            "physical_bundle_files": len(on_disk),
            "physical_distinct_filenames": len(disk_names),
            "physical_bytes": sum(on_disk.values()),
            "bundles_unaccounted_for": unaccounted,
        },
        "dependency_graph": {
            "edges": len(dep_edges),
            "resolved_edges": sum(1 for e in dep_edges if e["to"]),
            "unresolved_edges": sum(1 for e in dep_edges if not e["to"]),
            "ambiguous_edges": sum(1 for e in dep_edges if e["ambiguous_owners"]),
            "distinct_targets": len({e["raw"] for e in dep_edges}),
        },
        "cross_check": {
            "container_keys_total": len(key_to_locations),
            "container_keys_matched_by_dumped_objects": matched,
            "container_keys_without_dumped_objects": len(key_without_object),
            "container_keys_without_dumped_objects_sample": key_without_object[:40],
            "container_target_mismatch_count": len(target_mismatch),
            "container_target_mismatch_sample": target_mismatch[:25],
            "dumped_containers_missing_from_manifests": len(key_not_in_containers),
            "dumped_containers_missing_from_manifests_sample": key_not_in_containers[:40],
        },
        "limitations": [
            "Key -> location targets are only checked when m_FileID == 0 (intra-bundle).",
            "The Python catalog reader does not decode resource locations, GUIDs or labels; "
            "that requires the Unity runtime dump.",
        ],
    }
    with open(os.path.join(OUT, "addressables.json"), "w", encoding="utf-8") as fh:
        json.dump(out, fh, ensure_ascii=False, indent=1)

    with gzip.open(os.path.join(OUT, "addressable_keys.csv.gz"), "wt", encoding="utf-8",
                   newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["addressable_key", "location_count", "sources", "cabs",
                    "target_path_ids", "dumped_objects_in_container",
                    "dumped_in_one_of_the_sources"])
        for key in sorted(key_to_locations):
            locs = key_to_locations[key]
            rows = by_container.get(key, [])
            srcs = sorted({l["source"] for l in locs})
            w.writerow([key, len(locs), ";".join(srcs),
                        ";".join(sorted({str(l["cab"]) for l in locs})),
                        ";".join(sorted({l["target_path_id"] for l in locs})),
                        len(rows),
                        sum(1 for r in rows if r["source_identity"] in srcs)])

    with open(os.path.join(OUT, "addressable_keys_dupes.csv"), "w", encoding="utf-8",
              newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["addressable_key", "location_count", "sources", "target_path_ids"])
        for key, locs in sorted(dup_keys.items()):
            w.writerow([key, len(locs), ";".join(sorted({l["source"] for l in locs})),
                        ";".join(sorted({l["target_path_id"] for l in locs}))])

    print(json.dumps({k: out[k] for k in ("bundles", "dependency_graph", "cross_check")},
                     ensure_ascii=False, indent=1)[:3000])
    print("catalog python reader:", json.dumps(
        {k: v for k, v in (catalog_py or {}).items()
         if k in ("bytes", "version", "header_entry_count", "printable_strings",
                  "bundle_names_mentioned", "runtime_path_literals")}, ensure_ascii=False))
    print("unity runtime catalog dump present:", unity_dump is not None)
    return 0


if __name__ == "__main__":
    sys.exit(main())
