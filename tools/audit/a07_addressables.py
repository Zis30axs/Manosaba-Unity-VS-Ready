#!/usr/bin/env python3
"""A07 - Addressables / AssetBundle recoverability.

Sources of truth:
  * StreamingAssets/aa/settings.json           (Addressables 2.4.6 runtime settings)
  * StreamingAssets/aa/catalog.bin             (BinaryStorageBuffer v2 catalog)
  * every AssetBundle's AssetBundle object     (m_Container + m_Dependencies), from a03
  * res/_index.csv                             (container -> dumped objects), from the ripper

Rebuilds Addressable key -> bundle -> bundle-dependencies and cross-checks the
three views.  Outputs: out/addressables.json, out/addressable_keys.csv.gz
"""
import os, sys, json, gzip, csv, struct, re, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import AA, AAW, OUT, load_index

def main():
    settings = json.load(open(os.path.join(AA, "settings.json"), encoding="utf-8"))
    bundles = json.load(open(os.path.join(OUT, "bundles.json"), encoding="utf-8"))
    sources = json.load(open(os.path.join(OUT, "sources.json"), encoding="utf-8"))

    cab2src, src2cab = {}, {}
    for s in sources:
        for sf in s.get("serialized_files", []):
            key = sf["name"].lower()
            cab2src[key] = s["rel"]
            src2cab.setdefault(s["rel"], sf["name"])

    key2bundle = {}
    key2entry = {}
    dep_edges = []
    for b in bundles:
        src = b["source"]
        for c in b.get("containers", []):
            key2bundle.setdefault(c, src)
            key2entry[c] = {"bundle": src, "cab": src2cab.get(src)}
        for d in b.get("dependencies", []):
            tgt = cab2src.get(d.lower())
            dep_edges.append({"from": src, "to": tgt, "raw": d,
                              "resolved": bool(tgt)})

    rows = load_index()
    idx_keys = collections.Counter(r["asset_path"] for r in rows if r["asset_path"])
    missing_keys = sorted(set(key2bundle) - set(idx_keys))

    # catalog.bin
    craw = open(os.path.join(AA, "catalog.bin"), "rb").read()
    magic, ver, toff, tsize = struct.unpack_from("<4I", craw, 0)
    strings = re.findall(rb"[ -~]{5,}", craw)
    strset = collections.Counter(s.decode("ascii", "replace") for s in strings)
    provider_names = sorted({s for s in strset if "Provider" in s and "." in s})
    disk_bundles = {f for f in os.listdir(AAW) if f.endswith(".bundle")}
    for root, dirs, files in os.walk(AAW):
        for f in files:
            if f.endswith(".bundle"):
                disk_bundles.add(f)
    cat_bundle_names = {s for s in strset if s.endswith(".bundle")}
    declared = [s for s in key2bundle.values()]
    out = {
        "settings": {"m_AddressablesVersion": settings.get("m_AddressablesVersion"),
                     "m_buildTarget": settings.get("m_buildTarget"),
                     "m_SettingsHash": settings.get("m_SettingsHash"),
                     "catalog_provider": settings["m_CatalogLocations"][0]["m_Provider"],
                     "internal_id": settings["m_CatalogLocations"][0]["m_InternalId"]},
        "catalog_bin": {"bytes": len(craw), "magic": hex(magic), "version": ver,
                        "types_offset": toff, "types_size": tsize,
                        "ascii_strings": len(strings),
                        "provider_class_names": provider_names,
                        "bundle_names_mentioned": len(cat_bundle_names),
                        "prefab_key_literals": craw.count(b".prefab"),
                        "nani_key_literals": craw.count(b".nani"),
                        "runtime_path_literals": craw.count(b"StandaloneWindows64")},
        "bundles": {
            "total": len(bundles),
            "with_manifest_object": sum(1 for b in bundles if b.get("container_count") is not None),
            "container_entries": sum(b.get("container_count", 0) for b in bundles),
            "distinct_keys": len(key2bundle),
            "with_dependencies": sum(1 for b in bundles if b.get("dependencies")),
        },
        "dependency_graph": {
            "edges": len(dep_edges),
            "resolved_edges": sum(1 for e in dep_edges if e["resolved"]),
            "distinct_targets": len({e["raw"] for e in dep_edges}),
            "sample": dep_edges[:10],
        },
        "cross_check": {
            "keys_in_manifests": len(key2bundle),
            "keys_with_dumped_objects": len(set(idx_keys) & set(key2bundle)),
            "keys_in_manifests_without_dumped_objects": len(missing_keys),
            "missing_keys_sample": missing_keys[:20],
            "bundles_on_disk": len(disk_bundles),
            "bundle_files_referenced_by_dependency_graph": len({e["to"] for e in dep_edges if e["to"]}),
        },
    }
    json.dump(out, open(os.path.join(OUT, "addressables.json"), "w", encoding="utf-8"),
              ensure_ascii=False, indent=1)
    with gzip.open(os.path.join(OUT, "addressable_keys.csv.gz"), "wt", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["addressable_key", "bundle", "cab", "dumped_objects", "deps_of_bundle"])
        deps_of = collections.defaultdict(list)
        for e in dep_edges:
            deps_of[e["from"]].append(e["to"] or e["raw"])
        for k in sorted(key2bundle):
            src = key2bundle[k]
            w.writerow([k, src, src2cab.get(src, ""), idx_keys.get(k, 0), ";".join(deps_of.get(src, []))])
    print(json.dumps({k: out[k] for k in ("catalog_bin", "bundles", "dependency_graph", "cross_check")},
                     ensure_ascii=False, indent=1)[:2600])

if __name__ == "__main__":
    main()
