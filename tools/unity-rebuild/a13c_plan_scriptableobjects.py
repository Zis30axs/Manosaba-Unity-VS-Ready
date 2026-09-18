"""a13c: plan the ScriptableObject conversion (G2 composite half, first family).

Reads the object index, keeps the rows that are ScriptableObjects (stored as MonoBehaviour objects
in the shipped build), joins them with the script binding produced by a13b_script_binding.py, and
writes a job list the Editor-side converter executes:

  tools/unity-rebuild/out/unity/so_convert_plan.json
    {"jobs": [{"dump":..., "target":..., "type":..., "assembly":..., "address":..., ...}]}

`address` is the shipped container path, i.e. the Addressables address the G3 content build must
use; `dump` is the typetree JSON holding the field values.

Usage
  python tools/unity-rebuild/a13c_plan_scriptableobjects.py --family data
  python tools/unity-rebuild/a13c_plan_scriptableobjects.py --family scenarios
"""
from __future__ import annotations

import argparse
import csv
import datetime
import json
import os
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
RES = os.path.join(REPO, "res")
INDEX = os.path.join(RES, "_index.csv")
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
BINDING = os.path.join(OUT_DIR, "bundle_script_map.json")
CONTENT = os.path.join(REPO, "UnityProject", "Assets", "GameContent")

FAMILIES = {
    "data": lambda p: p.startswith("Assets/#WitchTrials/Data/") and p.endswith(".asset"),
    "scenarios": lambda p: p.startswith("Assets/#WitchTrials/Scenarios/") and p.endswith(".nani"),
}


def dest_for(asset_path: str) -> str:
    rel = asset_path.replace("\\", "/").lstrip("/")
    if rel.startswith("Assets/"):
        rel = rel[len("Assets/"):]
    rel = rel.replace("Assets/#WitchTrials/", "#WitchTrials/")
    return os.path.join(CONTENT, rel.replace("/", os.sep))


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--family", default="data", choices=sorted(FAMILIES) + ["all"])
    args = ap.parse_args()

    if not os.path.exists(BINDING):
        raise SystemExit(f"missing {BINDING}: run tools/unity-rebuild/a13b_script_binding.py first")
    with open(BINDING, encoding="utf-8") as fh:
        binding = json.load(fh)

    # "<bundle file name>|<path_id>" -> resolved script record (accumulated by a13b)
    resolved = {}
    for key, rec in (binding.get("entries") or {}).items():
        if rec.get("class"):
            resolved[key] = rec

    families = sorted(FAMILIES) if args.family == "all" else [args.family]
    jobs, skipped = [], []
    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            if (row.get("type") or "").strip() != "MonoBehaviour":
                continue
            asset_path = (row.get("asset_path") or "").strip()
            if not any(FAMILIES[f](asset_path) for f in families):
                continue
            bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
            key = f"{bundle}|{(row.get('path_id') or '').strip()}"
            hit = resolved.get(key)
            dump_rel = (row.get("output_file") or "").strip()
            dump = os.path.join(RES, dump_rel.replace("/", os.sep))
            if not hit or not os.path.exists(dump):
                skipped.append({"asset_path": asset_path, "bundle": bundle,
                                "path_id": key[1], "dump": dump_rel,
                                "reason": "no bound script type" if not hit else "dump file missing"})
                continue
            jobs.append({
                "asset_path": asset_path,
                # Unity only imports a ScriptableObject from a native .asset file; the shipped
                # container path (e.g. ".../Act01_Chapter01_Adv01.nani") is kept as the address for
                # the G3 content build, not used as the file name.
                "target": os.path.splitext(dest_for(asset_path))[0] + ".asset",
                "dump": os.path.relpath(dump, REPO).replace("\\", "/"),
                "type": f"{hit['namespace']}.{hit['class']}" if hit.get("namespace") else hit["class"],
                "assembly": hit["assembly"],
                "our_guid": hit.get("our_guid", ""),
                "bundle": bundle,
                "path_id": key[1],
            })

    plan = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "family": args.family,
        "jobs": jobs,
        "job_count": len(jobs),
        "skipped": skipped,
        "skipped_count": len(skipped),
        "source": {"index": os.path.relpath(INDEX, REPO).replace("\\", "/"),
                   "binding": os.path.relpath(BINDING, REPO).replace("\\", "/")},
    }
    os.makedirs(OUT_DIR, exist_ok=True)
    plan_path = os.path.join(OUT_DIR, f"so_convert_plan_{args.family}.json")
    with open(plan_path, "w", encoding="utf-8") as fh:
        json.dump(plan, fh, ensure_ascii=False, indent=1)

    # Paths list for the Editor-side verifier: the G2 gate collects every *_imported_paths.txt, so a
    # converted ScriptableObject is checked with the same LoadMainAssetAtPath + GUID-stability test
    # as the imported leaf assets.
    paths_path = os.path.join(OUT_DIR, f"so_imported_paths_{args.family}.txt")
    asset_paths = []
    for job in jobs:
        target = job["target"].replace("\\", "/")
        index = target.find("/Assets/")
        asset_paths.append(target[index + 1:] if index >= 0 else target)
    with open(paths_path, "w", encoding="utf-8") as fh:
        fh.write("\n".join(sorted(asset_paths)) + "\n")

    by_assembly = {}
    for j in jobs:
        by_assembly[j["assembly"]] = by_assembly.get(j["assembly"], 0) + 1
    print(f"family      : {args.family}")
    print(f"jobs        : {len(jobs)}   skipped: {len(skipped)}")
    print(f"by assembly : {by_assembly}")
    for s in skipped[:5]:
        print(f"  skipped: {s['asset_path']}  ({s['reason']})")
    print(f"plan        : {plan_path}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
