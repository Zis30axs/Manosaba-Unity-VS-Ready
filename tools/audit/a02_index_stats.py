#!/usr/bin/env python3
"""A02 - analyse res/_index.csv (the dump manifest) and res/ directory layout.

Answers: what the dump contains, per type / per source / per container path,
which containers are prefabs vs assets vs scenes, pathID collisions, and how
the "Uncategorized"/"_serialized" trees are organised.

Outputs: out/index_summary.json, out/containers_by_kind.csv
"""
import os, sys, json, csv, collections, re
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import RES, OUT, load_index

PREFAB = re.compile(r"\.prefab$", re.I)

def main():
    rows = load_index()
    by_type = collections.Counter(r["type"] for r in rows)
    by_src = collections.Counter(r["source_bundle"] for r in rows)
    by_src_type = collections.Counter((r["source_bundle"], r["type"]) for r in rows)

    # container-path analysis (asset_path column == Addressables container path when present)
    prefabs, assets, other = collections.Counter(), collections.Counter(), collections.Counter()
    per_container = collections.defaultdict(collections.Counter)
    scene_src_types = collections.Counter()
    obj_count = collections.Counter()
    for r in rows:
        c = r["asset_path"]
        if not c:
            continue
        per_container[c][r["type"]] += 1
        obj_count[c] += 1
        if PREFAB.search(c):
            prefabs[c] += 1
        elif c.lower().endswith((".asset", ".txt", ".json", ".ogg", ".mp4", ".otf", ".ttf")):
            assets[c] += 1
        else:
            other[c] += 1

    # how many distinct containers, and container kinds
    kinds = collections.Counter()
    for c in per_container:
        ext = os.path.splitext(c)[1].lower()
        kinds[ext or "(no-ext)"] += 1

    # duplicates of path_id inside one source
    pid_coll = collections.Counter((r["source_bundle"], r["pid"]) for r in rows)
    dup = {f"{k[0]}|{k[1]}": v for k, v in pid_coll.items() if v > 1}

    # output_file uniqueness
    ofc = collections.Counter(r["output_file"] for r in rows)

    # recovered rows (adopt_orphans wrote source "(recovered)" and path_id 0)
    recovered = [r for r in rows if r["source_bundle"] == "(recovered)"]

    # prefab dump dirs: how many containers are .prefab and how many objects each
    prefab_objs = sorted(((obj_count[c], c) for c in prefabs), reverse=True)

    summary = {
        "rows": len(rows),
        "types": dict(by_type.most_common()),
        "sources": dict(by_src.most_common()),
        "source_type_matrix": {f"{s}|{t}": n for (s, t), n in by_src_type.most_common()},
        "containers": {
            "total_distinct": len(per_container),
            "prefab_containers": len(prefabs),
            "asset_like_containers": len(assets),
            "other_containers": len(other),
            "by_extension": dict(kinds.most_common()),
        },
        "rows_without_container": sum(1 for r in rows if not r["asset_path"]),
        "recovered_rows_pathid0": len(recovered),
        "duplicate_pathid_within_source": len(dup),
        "duplicate_output_file_rows": sum(v - 1 for v in ofc.values() if v > 1),
        "largest_prefabs_by_dumped_objects": [{"container": c, "objects": n} for n, c in prefab_objs[:25]],
        "objects_per_container_stats": {},
        "type_count_per_source_top": {},
        "index_csv_bytes": os.path.getsize(os.path.join(RES, "_index.csv")),
    }
    counts = sorted(obj_count.values())
    if counts:
        summary["objects_per_container_stats"] = {
            "min": counts[0], "median": counts[len(counts) // 2], "max": counts[-1],
            "mean": round(sum(counts) / len(counts), 1),
        }
    with open(os.path.join(OUT, "index_summary.json"), "w", encoding="utf-8") as fh:
        json.dump(summary, fh, ensure_ascii=False, indent=1)

    with open(os.path.join(OUT, "containers_by_kind.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["container_path", "objects", "types"])
        for c in sorted(per_container, key=lambda x: -len(per_container[x])):
            w.writerow([c, obj_count[c],
                        ";".join(f"{t}={n}" for t, n in per_container[c].most_common())])

    print(json.dumps({k: summary[k] for k in
                      ("rows", "rows_without_container", "recovered_rows_pathid0",
                       "duplicate_pathid_within_source", "containers",
                       "objects_per_container_stats", "duplicate_output_file_rows")},
                     ensure_ascii=False, indent=1))
    print("\ntop types:", json.dumps(dict(by_type.most_common(12)), ensure_ascii=False))

if __name__ == "__main__":
    main()
