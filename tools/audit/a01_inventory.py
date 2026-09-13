#!/usr/bin/env python3
"""A01 - full recursive inventory with content sniffing.

Scans: main checkout (res/, src/, project/, work/, dist/, root files), the game
install dir, and this worktree.  Classifies every file by extension AND by
content magic, so ".json" can be separated into typetree-object-dumps,
Addressables runtime data, Naninovel script text, etc.

Outputs: out/inventory_summary.json, out/inventory_files.csv.gz
"""
import os, sys, csv, gzip, collections, json
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import REPO, WT, RES, GAME, OUT, sniff_header, UNITY_YAML_MAGIC

ROOTS = [("repo-main", REPO), ("worktree", WT), ("game-install", GAME)]
EXCLUDE_DIRS = {".git", "__pycache__", ".idea", "node_modules"}

def walk(root, label):
    for dirpath, dirnames, files in os.walk(root):
        dirnames[:] = [d for d in dirnames if d not in EXCLUDE_DIRS]
        for f in files:
            fp = os.path.join(dirpath, f)
            yield label, os.path.relpath(fp, root).replace(os.sep, "/"), fp

def main():
    ext_counter = collections.Counter()
    content_counter = collections.Counter()
    label_counter = collections.Counter()
    rows = []
    total = 0
    for label, rel, fp in (r for R in ROOTS for r in walk(R[1], R[0])):
        ext = os.path.splitext(rel)[1].lower() or "(none)"
        kind, _ = sniff_header(fp)
        try:
            size = os.path.getsize(fp)
        except OSError:
            size = -1
        ext_counter[ext] += 1
        content_counter[kind] += 1
        label_counter[f"{label}:{kind}"] += 1
        rows.append((label, rel, ext, kind, size))
        total += 1
        if total % 20000 == 0:
            print("  ...", total, flush=True)

    with gzip.open(os.path.join(OUT, "inventory_files.csv.gz"), "wt", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["root", "relpath", "ext", "content_kind", "size"])
        w.writerows(rows)

    # second pass: which of the "Unity project" extension families exist at all?
    families = {
        ".unity": 0, ".prefab": 0, ".meta": 0, ".asset": 0, ".mat": 0, ".controller": 0,
        ".anim": 0, ".overridecontroller": 0, ".playable": 0, ".timeline": 0, ".spriteatlas": 0,
        ".shader": 0, ".shadergraph": 0, ".bundle": 0, ".asmdef": 0, ".cs": 0, ".dll": 0,
    }
    for label, rel, ext, kind, size in rows:
        if ext in families:
            families[ext] += 1

    unity_yaml = [(l, r) for l, r, e, k, s in rows if k.startswith("UnityYAML")]
    summary = {
        "totals": {"files": total},
        "by_extension": dict(ext_counter.most_common()),
        "by_content_kind": dict(content_counter.most_common()),
        "by_root_and_kind": dict(sorted(label_counter.items())),
        "unity_project_extensions_present": families,
        "unity_yaml_files_found": unity_yaml[:50],
        "unity_yaml_file_count": len(unity_yaml),
    }
    with open(os.path.join(OUT, "inventory_summary.json"), "w", encoding="utf-8") as fh:
        json.dump(summary, fh, ensure_ascii=False, indent=1)
    print(json.dumps({k: summary[k] for k in ("totals", "by_content_kind")}, ensure_ascii=False, indent=1))
    print("families:", families)
    print("YAML files:", len(unity_yaml))

if __name__ == "__main__":
    main()
