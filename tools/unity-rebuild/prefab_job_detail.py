"""One-off: show the warnings/plan composition of a prefab job, to explain why objects created in the
Editor did not make it into the saved prefab file."""
import collections
import io
import json
import os
import sys

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
result = json.load(io.open(os.path.join(BASE, "prefab_build_result.json"), encoding="utf-8"))
plan = json.load(io.open(os.path.join(BASE, "prefab_plan_all.json"), encoding="utf-8"))
plan_by_path = {j["asset_path"]: j for j in plan["jobs"]}

names = sys.argv[1:] or ["SaveLoadUI", "GalleryUI"]
for job in result["jobs"]:
    if not any(n in job["asset_path"] for n in names):
        continue
    pj = plan_by_path.get(job["asset_path"], {})
    print(f"\n=== {job['asset_path']}")
    print(f"  status={job['status']} created={job['objects_created']} planned={job['objects_planned']}")
    print(f"  plan object types: {dict(collections.Counter(o['type'] for o in pj.get('objects', [])))}")
    print(f"  plan refs: {len(pj.get('references', {}))}  unresolved: {pj.get('unresolved_counts')}")
    print(f"  warnings ({len(job.get('warnings') or [])}):")
    for w in (job.get("warnings") or [])[:15]:
        print("    -", w[:200])
    print(f"  messages ({len(job.get('messages') or [])}):")
    for m in (job.get("messages") or [])[:5]:
        print("    -", m[:200])
