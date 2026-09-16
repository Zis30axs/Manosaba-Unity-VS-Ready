#!/usr/bin/env python3
"""A06 - proof-of-concept mechanical rebuild of GameObject/Transform hierarchies
straight out of the res/ typetree JSON dumps.

Scope:
  * level0 (Assets/#WitchTrials/Scenes/Boot.unity) - the only built scene
  * Assets/#WitchTrials/Prefabs/General/Core/Game.prefab - biggest hand-made prefab
Validation: every child<->father link, every GameObject component list, and
component->GameObject back-links are checked; missing objects are reported.

Outputs: out/hierarchy_boot.json, out/hierarchy_game.json, out/hierarchy_report.json
"""
import os, sys, json, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import RES, OUT, load_index

def load_scope(rows, key_kind, key, diagnostics=None):
    """key_kind: 'source' or 'container'

    ``diagnostics`` (optional dict) collects what was skipped or unreadable, so a missing
    dump file or an unknown pathID becomes a reported fact rather than a silent hole.
    """
    diag = diagnostics if diagnostics is not None else {}
    objs = {}
    for r in rows:
        if key_kind == "source" and r["source_bundle"] != key:
            continue
        if key_kind == "container" and r["asset_path"] != key:
            continue
        if not r.pathid_known:
            diag["unknown_pathid_rows"] = diag.get("unknown_pathid_rows", 0) + 1
            diag.setdefault("unknown_pathid_samples", []).append(r["output_file"])
            continue
        fp = os.path.join(RES, r["output_file"].replace("/", os.sep))
        if not os.path.exists(fp):
            diag["dump_missing"] = diag.get("dump_missing", 0) + 1
            diag.setdefault("dump_missing_samples", []).append(r["output_file"])
            objs[r["pid"]] = (r["type"], {"__error__": f"dump file missing: {r['output_file']}"})
            continue
        try:
            with open(fp, encoding="utf-8") as fh:
                objs[r["pid"]] = (r["type"], json.load(fh))
        except (OSError, ValueError) as e:
            diag["unreadable"] = diag.get("unreadable", 0) + 1
            objs[r["pid"]] = (r["type"], {"__error__": f"{type(e).__name__}: {e}"})
    return objs

def rebuild(objs, label):
    gos = {p: j for p, (t, j) in objs.items() if t == "GameObject"}
    trs = {p: j for p, (t, j) in objs.items() if t in ("Transform", "RectTransform")}
    roots = [p for p, j in trs.items() if not (j.get("m_Father") or {}).get("m_PathID")]
    issues = collections.Counter()
    dangling_children = []
    def comp_types(go):
        out = []
        for c in go.get("m_Component") or []:
            p = (c.get("component") or {}).get("m_PathID")
            t = objs.get(p, ("MISSING", None))[0]
            out.append([p, t])
            if t == "MISSING":
                issues["component_missing_from_dump"] += 1
        return out
    tree = {}
    def walk(tp, go_seen):
        j = trs[tp]
        go_p = (j.get("m_GameObject") or {}).get("m_PathID")
        if go_p not in gos:
            issues["transform_without_gameobject"] += 1
        go = gos.get(go_p, {})
        kids = []
        for ch in j.get("m_Children") or []:
            cp = ch.get("m_PathID")
            if cp in trs:
                cj = trs[cp]
                if (cj.get("m_Father") or {}).get("m_PathID") != tp:
                    issues["father_backlink_mismatch"] += 1
                kids.append(walk(cp, go_seen))
            else:
                issues["child_transform_missing"] += 1
                dangling_children.append(cp)
        return {"path_id": tp, "name": go.get("m_Name"), "active": go.get("m_IsActive"),
                "layer": go.get("m_Layer"), "tag": go.get("m_Tag"),
                "components": comp_types(go), "children": kids}
    for r in roots:
        tree[r] = walk(r, set())
    # reverse check: components pointing back at their GameObject
    for p, (t, j) in objs.items():
        if t in ("Transform", "RectTransform", "CanvasRenderer") or "m_GameObject" in j:
            g = (j.get("m_GameObject") or {}).get("m_PathID")
            if g and g not in gos and t != "MonoBehaviour":
                issues["component_go_missing"] += 1
    def count_nodes(ts):
        n = len(ts)
        for v in ts.values():
            n += count_nodes({i: c for i, c in enumerate(v["children"])})
        return n
    total = sum(count_nodes({i: t for i, t in enumerate(tree.values())}) for _ in [0])
    report = {
        "label": label,
        "gameobjects": len(gos), "transforms": len(trs),
        "roots": len(roots), "nodes_in_tree": total,
        "issues": dict(issues),
        "dangling_children": dangling_children[:20],
        "root_names": [tree[r]["name"] for r in roots],
        "max_depth": None,
        "component_type_histogram": dict(collections.Counter(
            c[1] for p, (t, j) in objs.items() if t == "GameObject"
            for c in (comp_types(j)))),
    }
    def depth(n):
        return 1 + max([depth(c) for c in n["children"]] or [0])
    report["max_depth"] = max([depth(tree[r]) for r in roots] or [0])
    return tree, report

def render(nodes, indent=0, max_lines=80, lines=None):
    lines = lines if lines is not None else []
    if len(lines) > max_lines:
        return lines
    if isinstance(nodes, dict):
        nodes = list(nodes.values())
    for n in nodes:
        comps = " ".join(sorted({c[1] for c in n["components"]}))
        lines.append(f"{'  '*indent}- {n['name']}  [{comps}]")
        render(n["children"], indent + 1, max_lines, lines)
    return lines

def main():
    rows = load_index()
    out = {}
    scopes = [("source", "level0", "Boot.unity (level0)"),
              ("source", "resources.assets", "resources.assets"),
              ("container", "Assets/#WitchTrials/Prefabs/General/Core/Game.prefab", "Game.prefab")]
    for kind, key, label in scopes:
        diagnostics = {}
        objs = load_scope(rows, kind, key, diagnostics)
        tree, report = rebuild(objs, label)
        report["load_diagnostics"] = diagnostics
        out[label] = report
        fn = "hierarchy_" + label.split()[0].replace(".", "_").replace("/", "_") + ".json"
        json.dump({"report": report, "tree": tree}, open(os.path.join(OUT, fn), "w", encoding="utf-8"),
                  ensure_ascii=False, indent=1)
        print("=" * 70)
        print(f"{label}: objects={len(objs)} GO={report['gameobjects']} TR={report['transforms']} "
              f"roots={report['roots']} depth={report['max_depth']} issues={report['issues']}")
        print("  component types:", json.dumps(report["component_type_histogram"], ensure_ascii=False)[:300])
        for ln in render(list(tree.values()), max_lines=22):
            print("   ", ln)
    json.dump(out, open(os.path.join(OUT, "hierarchy_report.json"), "w", encoding="utf-8"),
              ensure_ascii=False, indent=1)

if __name__ == "__main__":
    main()
