#!/usr/bin/env python3
"""A04 - serialized-reference (PPtr) graph census over the whole res/ dump.

For every typetree JSON dump it extracts {"m_FileID":N,"m_PathID":P} pointers and
classifies them:

  * m_FileID == 0            -> intra-file ref, resolved via res/_index.csv (source, pathID)
  * m_FileID >= 1            -> external ref, target = sources.json externals[fileID-1]
                                "archive:/CAB-xxxx" targets are matched against the CAB
                                names of the scanned bundles (same hash namespace)
  * other fileIDs            -> unresolved (report the fileID distribution)

Outputs: out/ref_census.json, out/ref_class_matrix.csv, out/ref_missing_samples.json
"""
import os, sys, json, re, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import RES, OUT, load_index, iter_res_files, load_monoscript_table, AAW

PPTR = re.compile(r'"m_FileID"\s*:\s*(-?\d+)\s*,\s*"m_PathID"\s*:\s*(-?\d+)')

def build_tables():
    rows = load_index()
    per_source = collections.defaultdict(dict)      # source -> {pathID: type}
    out_to_row = {}
    for r in rows:
        src = r["source_bundle"]
        per_source[src][r["pid"]] = r
        out_to_row[r["output_file"].replace("/", os.sep)] = r
    ext = {}
    srcj = json.load(open(os.path.join(OUT, "sources.json"), encoding="utf-8"))
    for s in srcj:
        ex = []
        for sf in s.get("serialized_files", []):
            ex.extend(sf.get("externals", []))
        ext[s["rel"]] = ex
    return rows, per_source, out_to_row, ext, srcj

def main():
    rows, per_source, out_to_row, ext, srcj = build_tables()
    # map CAB name -> source rel (from the inner SerializedFile name)
    cab2src = {}
    for s in srcj:
        for sf in s.get("serialized_files", []):
            cab2src[sf["name"].lower()] = s["rel"]

    stats = collections.Counter()
    fileid_hist = collections.Counter()
    missing_intra = collections.Counter()
    missing_ext = collections.Counter()
    ext_targets = collections.Counter()
    per_type_missing = collections.Counter()
    missing_by_source = collections.Counter()
    mst = load_monoscript_table()
    monoscript_by_pid = {int(k): v for k, v in mst["by_pid"].items()}
    ext_res_by_target = collections.Counter()
    ext_miss_by_target = collections.Counter()
    samples = []
    files_scanned = 0
    refs_total = 0
    for dirpath, dirnames, files in os.walk(RES):
        for f in files:
            if not f.endswith(".json"):
                continue
            fp = os.path.join(dirpath, f)
            rel = os.path.relpath(fp, RES).replace(os.sep, "/")
            row = out_to_row.get(os.path.relpath(fp, RES))
            if row is None:
                stats["json_not_in_index"] += 1
                continue
            files_scanned += 1
            try:
                txt = open(fp, encoding="utf-8", errors="replace").read()
            except OSError:
                stats["unreadable"] += 1
                continue
            src = row["source_bundle"]
            for m in PPTR.finditer(txt):
                fid, pid = int(m.group(1)), int(m.group(2))
                refs_total += 1
                fileid_hist[fid if fid <= 4 else f"{fid}+"] += 1
                if fid == 0 and pid == 0:
                    stats["null_ref"] += 1
                elif fid == 0:
                    tgt = per_source.get(src, {}).get(pid)
                    if tgt is not None and tgt["type"] != "MonoBehaviour":
                        stats["intra_resolved"] += 1
                    elif tgt is not None:
                        stats["intra_resolved_monobehaviour"] += 1
                    else:
                        stats["intra_MISSING"] += 1
                        missing_intra[f"{src}|{pid}"] += 1
                        missing_by_source[src] += 1
                        per_type_missing[f"{row['type']}->?fileid0"] += 1
                        if len(samples) < 200:
                            samples.append({"kind": "intra_missing", "from": rel, "from_type": row["type"],
                                            "src": src, "target_pathid": pid})
                else:
                    ex = ext.get(src) or []
                    if 1 <= fid <= len(ex):
                        tgtpath = ex[fid - 1]["path"] or ""
                        ext_targets[tgtpath] += 1
                        base = os.path.basename(tgtpath).lower()
                        tsrc = cab2src.get(base)
                        if tsrc and per_source.get(tsrc, {}).get(pid) is not None:
                            stats["external_resolved_dumpobject"] += 1
                        elif "monoscripts.bundle" in (tsrc or "") and pid in monoscript_by_pid:
                            stats["external_resolved_monoscript_via_table"] += 1
                            ext_res_by_target[tgtpath] += 1
                        elif tsrc:
                            stats["external_file_known_object_not_in_dump"] += 1
                            ext_miss_by_target[tgtpath] += 1
                        elif tgtpath.startswith("Library/") or tgtpath.startswith("Resources/"):
                            stats["external_builtin_unresolvable"] += 1
                        else:
                            stats["external_target_file_unknown"] += 1
                            missing_ext[tgtpath] += 1
                    else:
                        stats["external_fileid_out_of_range"] += 1
                        missing_ext[f"fileid={fid} of {len(ex)} in {src}"] += 1
            if files_scanned % 5000 == 0:
                print(f"  scanned {files_scanned} json, refs {refs_total}", flush=True)

    out = {
        "json_files_with_index_row": files_scanned,
        "refs_total": refs_total,
        "classification": dict(stats),
        "fileid_histogram": dict(fileid_hist),
        "external_target_paths": dict(ext_targets.most_common(30)),
        "top_missing_intra": [{"src|pathid": k, "n": v} for k, v in missing_intra.most_common(25)],
        "top_missing_external": [{"target": k, "n": v} for k, v in missing_ext.most_common(25)],
        "per_type_missing": dict(per_type_missing.most_common(30)),
        "missing_by_source": dict(missing_by_source.most_common(30)),
        "external_resolved_by_target": dict(ext_res_by_target.most_common(20)),
        "external_missing_by_target": dict(ext_miss_by_target.most_common(20)),
        "samples": samples[:100],
    }
    with open(os.path.join(OUT, "ref_census.json"), "w", encoding="utf-8") as fh:
        json.dump(out, fh, ensure_ascii=False, indent=1)
    print(json.dumps({k: out[k] for k in ("json_files_with_index_row", "refs_total", "classification",
                                          "fileid_histogram")}, ensure_ascii=False, indent=1))
    print("external targets:", dict(ext_targets.most_common(6)))

if __name__ == "__main__":
    main()
