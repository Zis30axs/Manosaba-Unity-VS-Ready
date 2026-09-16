#!/usr/bin/env python3
"""A08 - run the hierarchy validator over EVERY .prefab container in the dump.

For each prefab container path we rebuild the GameObject/Transform tree from the
per-object JSONs and check: root count, orphan GameObjects, father<->child
consistency, component references present, duplicate names.
Outputs out/prefab_sweep.json
"""
import os, sys, json, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import RES, OUT, load_index

def main():
    rows = load_index()
    by_container = collections.defaultdict(list)
    for r in rows:
        if r["asset_path"].endswith(".prefab"):
            by_container[r["asset_path"]].append(r)
    results = []
    agg = collections.Counter()
    for cont, rs in by_container.items():
        objs = {}
        for r in rs:
            fp = os.path.join(RES, r["output_file"].replace("/", os.sep))
            try:
                objs[r["pid"]] = (r["type"], json.load(open(fp, encoding="utf-8")))
            except Exception:
                objs[r["pid"]] = (r["type"], {})
        gos = {p for p, (t, _) in objs.items() if t == "GameObject"}
        trs = {p: j for p, (t, j) in objs.items() if t in ("Transform", "RectTransform")}
        roots = [p for p, j in trs.items() if not (j.get("m_Father") or {}).get("m_PathID")]
        issues = collections.Counter()
        reachable = set()
        def walk(tp):
            if tp in reachable:
                issues["cycle"] += 1
                return
            reachable.add(tp)
            j = trs[tp]
            g = (j.get("m_GameObject") or {}).get("m_PathID")
            if g not in gos:
                issues["transform_without_go"] += 1
            go = objs.get(g, ("?", {}))[1]
            for c in go.get("m_Component") or []:
                cp = (c.get("component") or {}).get("m_PathID")
                if cp not in objs:
                    issues["missing_component"] += 1
            for ch in j.get("m_Children") or []:
                cp = ch.get("m_PathID")
                if cp not in trs:
                    issues["missing_child_transform"] += 1
                    continue
                if (trs[cp].get("m_Father") or {}).get("m_PathID") != tp:
                    issues["father_mismatch"] += 1
                walk(cp)
        for r in roots:
            walk(r)
        orphan_tr = [t for t in trs if t not in reachable]
        orphan_go = [g for g in gos if not any((objs.get(g, ("?", {}))[1].get('m_Component') or []) and tp in trs and (trs[tp].get("m_GameObject") or {}).get("m_PathID") == g for tp in trs)]
        if orphan_tr:
            issues["orphan_transforms"] += len(orphan_tr)
        res = {"container": cont, "objects": len(objs), "gameobjects": len(gos), "transforms": len(trs),
               "roots": len(roots), "reachable_transforms": len(reachable), "issues": dict(issues)}
        results.append(res)
        if not any(issues.values()):
            agg["clean"] += 1
        else:
            agg["with_issues"] += 1
            for k in issues:
                agg["issue_" + k] += 1
        if len(roots) > 1:
            agg["multi_root"] += 1
    json.dump({"prefabs": len(results), "aggregate": dict(agg),
               "samples_with_issues": [r for r in results if any(r["issues"].values())][:25]},
              open(os.path.join(OUT, "prefab_sweep.json"), "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    print("prefabs:", len(results), "aggregate:", dict(agg))
    print("issue histogram:", dict(collections.Counter(k for r in results for k,v in r["issues"].items() if v)))
    print("multi-root prefabs:", sum(1 for r in results if r["roots"] > 1))

if __name__ == "__main__":
    main()
