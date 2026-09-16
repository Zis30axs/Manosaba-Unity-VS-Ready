#!/usr/bin/env python3
"""A05 - MonoBehaviour instance census: which scripts have serialized instances,
whether their fields were decoded from an embedded type tree, and how the
script reference resolves to class/namespace/assembly.

Outputs: out/monobehaviour.json, out/mb_classes.csv
"""
import os, sys, json, csv, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import RES, OUT, load_index, load_monoscript_table

BASE_KEYS = {"m_GameObject", "m_Enabled", "m_Script", "m_Name", "m_ObjectHideFlags", "m_CorrespondingSourceObject", "m_PrefabInstance", "m_PrefabAsset"}

def main():
    rows = load_index()
    mst = load_monoscript_table()
    by_pid = {int(k): v for k, v in mst["by_pid"].items()}
    # object table: (source, path_id) -> row  (used to resolve fileID==0 m_Script links)
    per_source = collections.defaultdict(dict)
    for r in rows:
        per_source[r["source_bundle"]][r["pid"]] = r
    ext = {}
    cab2src = {}
    for s in json.load(open(os.path.join(OUT, "sources.json"), encoding="utf-8")):
        ex = []
        for sf in s.get("serialized_files", []):
            ex.extend(sf.get("externals", []))
            cab2src[sf["name"].lower()] = s["rel"]
        ext[s["rel"]] = ex

    mb_rows = [r for r in rows if r["type"] == "MonoBehaviour"]
    classes = collections.Counter()
    fields_by_class = collections.defaultdict(collections.Counter)
    untyped = []
    typed = 0
    field_decoded = 0
    roots = collections.Counter()
    results = []
    for r in mb_rows:
        fp = os.path.join(RES, r["output_file"].replace("/", os.sep))
        if not os.path.exists(fp):
            continue
        try:
            tt = json.load(open(fp, encoding="utf-8"))
        except Exception:
            continue
        keys = list(tt.keys())
        extra = [k for k in keys if k not in BASE_KEYS]
        if extra:
            field_decoded += 1
        scr = tt.get("m_Script") or {}
        fid, pid = scr.get("m_FileID"), scr.get("m_PathID")
        cls = None
        if fid is not None and pid:
            if fid == 0:
                tgt = per_source.get(r["source_bundle"], {}).get(int(pid))
                if tgt and tgt["type"] == "MonoScript":
                    tf = os.path.join(RES, tgt["output_file"].replace("/", os.sep))
                    try:
                        j = json.load(open(tf, encoding="utf-8"))
                        cls = (j.get("m_AssemblyName"), j.get("m_Namespace"), j.get("m_ClassName"))
                    except Exception:
                        pass
            else:
                ex = ext.get(r["source_bundle"]) or []
                if 1 <= fid <= len(ex):
                    tgtfile = ex[fid - 1]["path"] or ""
                    tsrc = cab2src.get(os.path.basename(tgtfile).lower(), "")
                    if "monoscripts" in tsrc or "monoscripts" in tgtfile:
                        rec = by_pid.get(int(pid))
                        if rec:
                            cls = (rec["assembly"], rec["namespace"], rec["class"])
        if cls:
            typed += 1
            key = f"{cls[0]}|{cls[1]}|{cls[2]}"
            classes[key] += 1
            fields_by_class[key].update(extra)
            root = (r["asset_path"] or "").rsplit(".", 1)[-1] or "(no-container)"
            roots[root] += 1
        else:
            untyped.append({"out": r["output_file"], "src": r["source_bundle"], "script": scr})
        results.append({"out": r["output_file"], "src": r["source_bundle"], "container": r["asset_path"],
                        "class": "|".join(x or "" for x in cls) if cls else None, "fields": extra})

    with open(os.path.join(OUT, "mb_classes.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["assembly", "namespace", "class", "instances", "field_names"])
        for k, n in classes.most_common():
            a, ns, c = k.split("|")
            w.writerow([a, ns, c, n, ";".join(fields_by_class[k].keys())])

    out = {
        "monobehaviour_rows": len(mb_rows),
        "dumped_mb_with_json": len(results),
        "with_decoded_typed_fields": field_decoded,
        "typed_via_script_table": typed,
        "untyped": len(untyped),
        "distinct_classes_with_instances": len(classes),
        "top_classes": [{"class": k, "instances": n, "fields": len(fields_by_class[k])}
                        for k, n in classes.most_common(40)],
        "untyped_samples": untyped[:20],
    }
    json.dump(out, open(os.path.join(OUT, "monobehaviour.json"), "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    print(json.dumps({k: out[k] for k in ("monobehaviour_rows", "dumped_mb_with_json",
                                          "with_decoded_typed_fields", "typed_via_script_table",
                                          "untyped", "distinct_classes_with_instances")}, indent=1))
    print("\ntop classes:")
    for c in out["top_classes"][:20]:
        print(f"  {c['instances']:5d}  {c['class']}  ({c['fields']} fields)")

if __name__ == "__main__":
    main()
