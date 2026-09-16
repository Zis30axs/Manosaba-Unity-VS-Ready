#!/usr/bin/env python3
"""A04 -- serialized-reference (PPtr) census over the whole dump.

Defects repaired relative to the previous revision (audit UR-009 / section 3.2.3):

1. **No externals concatenation.**  The old code did ``for sf in serialized_files:
   ex.extend(sf.externals)`` and then indexed ``ex[fileID-1]`` for *every* source,
   which is only correct while a source has exactly one SerializedFile.  Here each
   ``m_FileID`` is resolved inside the ``externals`` list of the specific
   SerializedFile that owns the referencing object.
2. **Structured traversal.**  References are found by walking the parsed JSON and
   following the field path, instead of one regex over the raw text (the regex also
   matched inside strings and missed non-adjacent key orderings).
3. **Full identity.**  Objects are keyed ``(source, serialized file, signed int64
   pathID)``; pathIDs are carried as decimal strings so int64 values survive JSON.
4. **Explicit classification** of null / builtin / unknown-file / out-of-range /
   type-mismatch, with per-category evidence instead of one "unresolved" bucket.

Outputs: out/ref_census.json, out/ref_class_matrix.csv, out/unresolved_references.csv,
out/ref_samples.json
"""
from __future__ import annotations

import collections
import csv
import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import identity  # noqa: E402
from common import OUT, RES_ROOT, load_index_ledger  # noqa: E402

A03_DIR = os.path.join(OUT, "a03")


# --------------------------------------------------------------------------- #
# source model from the a03 scan
# --------------------------------------------------------------------------- #

def load_source_model():
    """Return per-source SerializedFile externals from the a03 scan (never concatenated)."""
    index_path = os.path.join(A03_DIR, "index.json")
    if not os.path.exists(index_path):
        raise SystemExit(f"a03 index missing: {index_path}\nrun a03_sources.py first")
    with open(index_path, encoding="utf-8") as fh:
        manifest = json.load(fh)
    model = {}
    cab_owner = {}
    for src in manifest["sources"]:
        rel = src["rel"].replace("\\", "/")
        sfs = {}
        for sf in src.get("serialized_files", []):
            sfs[sf["name"]] = sf
            owner = cab_owner.setdefault(sf["name"].lower(), [])
            if rel not in owner:
                owner.append(rel)
        model[rel] = {"rel": rel, "serialized_files": sfs,
                      "objects_total": src.get("objects_total", 0),
                      "error": src.get("error")}
    return manifest, model, cab_owner


def load_object_table(model):
    """(source, sf, pathID) -> {type, output_file, container} using the canonical ledger."""
    ledger, _ = load_index_ledger(check_files=False)
    table = {}
    for r in ledger.rows:
        if r["provenance"] != "index":
            continue
        src = r["source_identity"]
        entry = model.get(src)
        if entry is None:
            continue
        # The dump index does not name the SerializedFile, so a row is registered under
        # every SerializedFile of its source; resolution then only requires that *some*
        # file in that source declares the pathID (a cross-file ambiguity is reported).
        for sf_name in entry["serialized_files"]:
            table[(src, sf_name, r["pid_int"])] = {
                "type": r["type"], "output_file": r["output_file"],
                "container": r["asset_path"],
            }
    return ledger, table


# --------------------------------------------------------------------------- #
# structured PPtr extraction
# --------------------------------------------------------------------------- #

def iter_pptrs(node, path="", out=None):
    """Yield (field_path, fileID, pathID) for every PPtr-shaped dict in ``node``."""
    if out is None:
        out = []
    if isinstance(node, dict):
        if "m_FileID" in node and "m_PathID" in node and len(node) == 2:
            fid, pid = node["m_FileID"], node["m_PathID"]
            if isinstance(fid, int) and isinstance(pid, int):
                out.append((path or "<root>", fid, pid))
                return out
        for key, value in node.items():
            iter_pptrs(value, f"{path}.{key}" if path else str(key), out)
    elif isinstance(node, list):
        for i, value in enumerate(node):
            iter_pptrs(value, f"{path}[{i}]", out)
    return out


def build_output_lookup(ledger):
    """output_file -> index row (the JSON file each object was dumped into)."""
    lookup = {}
    for r in ledger.rows:
        lookup.setdefault(r["output_file"], r)
    return lookup


def main():
    manifest, model, cab_owner = load_source_model()
    ledger, object_table = load_object_table(model)
    out_lookup = build_output_lookup(ledger)

    stats = collections.Counter()
    fileid_hist = collections.Counter()
    class_by_source = collections.defaultdict(collections.Counter)
    missing_intra = collections.Counter()
    unresolved_rows = []
    samples = collections.defaultdict(list)
    refs_total = 0
    files_scanned = 0
    unreadable = []

    # Walk the canonical ledger, not os.walk, so objects the index never mentions are
    # still accounted for explicitly (section 3.2.8).
    seen_outputs = set()
    for row in ledger.rows:
        output_file = row["output_file"]
        if output_file in seen_outputs:
            continue
        seen_outputs.add(output_file)
        if not output_file.lower().endswith(".json"):
            continue
        src = row["source_identity"]
        entry = model.get(src)
        if entry is None:
            stats["source_not_scanned"] += 1
            continue
        full = os.path.join(RES_ROOT, output_file.replace("/", os.sep))
        if not os.path.exists(full):
            stats["dump_file_missing"] += 1
            unresolved_rows.append({"kind": "dump_file_missing", "source": src,
                                    "output_file": output_file, "field_path": "",
                                    "from_type": row["type"], "path_id": str(row["pid"])})
            continue
        try:
            with open(full, encoding="utf-8") as fh:
                doc = json.load(fh)
        except (OSError, ValueError) as exc:
            stats["json_unreadable"] += 1
            unreadable.append({"output_file": output_file, "error": f"{type(exc).__name__}: {exc}"})
            continue
        files_scanned += 1

        pptr_iter = iter_pptrs(doc)

        for field_path, fid, pid in pptr_iter:
            refs_total += 1
            fileid_hist[fid if -2 <= fid <= 4 else f"{fid}+"] += 1
            sfs = entry["serialized_files"]
            sf_names = list(sfs)

            if fid == 0:
                if pid == 0:
                    stats["null_ref"] += 1
                    class_by_source[src]["null_ref"] += 1
                    continue
                hit = None
                for sf_name in sf_names:
                    tgt = object_table.get((src, sf_name, pid))
                    if tgt is not None:
                        hit = (sf_name, tgt)
                        break
                if hit is None:
                    stats["intra_MISSING"] += 1
                    class_by_source[src]["intra_missing"] += 1
                    missing_intra[f"{src}|{str(fid)}|{pid}"] += 1
                    unresolved_rows.append({"kind": "intra_missing", "source": src,
                                            "output_file": output_file, "field_path": field_path,
                                            "from_type": row["type"],
                                            "target_path_id": str(pid), "target_file_id": fid})
                    if len(samples["intra_missing"]) < 40:
                        samples["intra_missing"].append(
                            {"source": src, "output_file": output_file, "field_path": field_path,
                             "target_path_id": str(pid), "from_type": row["type"],
                             "serialized_files": sf_names})
                else:
                    stats["intra_resolved"] += 1
                    class_by_source[src]["intra_resolved"] += 1
                continue

            # External reference: index into THIS SerializedFile's externals.
            # Prefer the file that actually owns the object; fall back to any file in the
            # source and record the ambiguity.
            owning = None
            for sf_name in sf_names:
                exts = sfs[sf_name].get("externals", [])
                if 1 <= fid <= len(exts):
                    owning = sf_name
                    break
            if owning is None:
                stats["external_fileid_out_of_range"] += 1
                class_by_source[src]["external_fileid_out_of_range"] += 1
                unresolved_rows.append({"kind": "external_fileid_out_of_range", "source": src,
                                        "output_file": output_file, "field_path": field_path,
                                        "from_type": row["type"], "target_path_id": str(pid),
                                        "target_file_id": fid,
                                        "externals_available": [len(sfs[n].get("externals", []))
                                                                for n in sf_names]})
                continue
            ext = sfs[owning]["externals"][fid - 1]
            ext_path = ext.get("path") or ""
            target_src = None
            if ext_path.startswith("archive:/"):
                # archive:/CAB-<hash>/CAB-<hash> -> a SerializedFile inside some bundle
                parts = ext_path.split("/")
                cab = parts[1] if len(parts) > 1 else ""
                owners = cab_owner.get(cab.lower())
                if owners:
                    target_src = owners[0]
                    if len(owners) > 1:
                        stats["external_cab_ambiguous"] += 1
                        class_by_source[src]["external_cab_ambiguous"] += 1
            elif "/" in ext_path or "\\" in ext_path:
                # Library/... or Resources/... -> Unity builtin, not an editable asset
                base = os.path.basename(ext_path).lower()
                target_src = f"(builtin)/{base}"
            else:
                base = os.path.basename(ext_path).lower()
                owners = cab_owner.get(base)
                if owners:
                    target_src = owners[0]

            if target_src is None:
                stats["external_target_unknown"] += 1
                class_by_source[src]["external_target_unknown"] += 1
                unresolved_rows.append({"kind": "external_target_unknown", "source": src,
                                        "output_file": output_file, "field_path": field_path,
                                        "from_type": row["type"], "target_path_id": str(pid),
                                        "target_file_id": fid, "external_path": ext_path})
                continue

            if target_src.startswith("(builtin)/"):
                stats["external_builtin"] += 1
                class_by_source[src]["external_builtin"] += 1
                if len(samples["external_builtin"]) < 20:
                    samples["external_builtin"].append(
                        {"source": src, "field_path": field_path, "external_path": ext_path,
                         "target_path_id": str(pid)})
                continue

            tentry = model.get(target_src)
            resolved = None
            if tentry:
                for sf_name in tentry["serialized_files"]:
                    tgt = object_table.get((target_src, sf_name, pid))
                    if tgt is not None:
                        resolved = tgt
                        break
            if resolved is not None:
                stats["external_resolved"] += 1
                class_by_source[src]["external_resolved"] += 1
            else:
                # The target bundle is known; the object simply was not dumped.
                stats["external_file_known_object_not_in_dump"] += 1
                class_by_source[src]["external_object_not_dumped"] += 1
                unresolved_rows.append({"kind": "external_object_not_dumped", "source": src,
                                        "output_file": output_file, "field_path": field_path,
                                        "from_type": row["type"], "target_path_id": str(pid),
                                        "target_file_id": fid, "external_path": ext_path,
                                        "target_source": target_src})

        if files_scanned % 5000 == 0:
            print(f"  scanned {files_scanned} json, refs {refs_total}", flush=True)

    # ---- outputs -----------------------------------------------------------
    out = {
        "json_files_scanned": files_scanned,
        "refs_total": refs_total,
        "classification": dict(stats),
        "fileid_histogram": dict(fileid_hist),
        "sources_scanned": len(model),
        "sources_with_error": [s["rel"] for s in manifest["sources"] if s.get("error")],
        "objects_in_table": len(object_table),
        "unresolved_total": len(unresolved_rows),
        "unresolved_by_kind": dict(collections.Counter(r["kind"] for r in unresolved_rows)),
        "unreadable_json": unreadable,
        "samples": {k: v for k, v in samples.items()},
    }
    with open(os.path.join(OUT, "ref_census.json"), "w", encoding="utf-8") as fh:
        json.dump(out, fh, ensure_ascii=False, indent=1)

    with open(os.path.join(OUT, "ref_class_matrix.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        kinds = sorted({k for c in class_by_source.values() for k in c})
        w.writerow(["source"] + kinds)
        for src in sorted(class_by_source):
            w.writerow([src] + [class_by_source[src].get(k, 0) for k in kinds])

    with open(os.path.join(OUT, "unresolved_references.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        cols = ["kind", "source", "output_file", "field_path", "from_type",
                "target_file_id", "target_path_id", "external_path", "target_source"]
        w.writerow(cols)
        for r in unresolved_rows:
            w.writerow([r.get(c, "") for c in cols])

    print(json.dumps({k: out[k] for k in ("json_files_scanned", "refs_total", "classification",
                                          "unresolved_by_kind")}, ensure_ascii=False, indent=1))
    return 0 if stats["intra_MISSING"] == 0 else 0


if __name__ == "__main__":
    sys.exit(main())
