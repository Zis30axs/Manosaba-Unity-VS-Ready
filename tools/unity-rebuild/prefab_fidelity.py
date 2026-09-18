"""One-off: per-prefab object counts from the prefab build result, so a prefab that was only
partially rebuilt is visible (plan objects vs objects actually created)."""
import io
import json
import os

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
result = json.load(io.open(os.path.join(BASE, "prefab_build_result.json"), encoding="utf-8"))
plan = json.load(io.open(os.path.join(BASE, "prefab_plan_all.json"), encoding="utf-8"))
plan_objects = {j["asset_path"]: len(j["objects"]) for j in plan["jobs"]}

rows = []
for job in result["jobs"]:
    path = job.get("asset_path") or job.get("plan", {}).get("asset_path", "?")
    created = job.get("objects_created", job.get("objects", 0))
    rows.append((path, plan_objects.get(path), created, job.get("status")))

print("sample job keys:", sorted(result["jobs"][0].keys()))
short = [(p, a, c, s) for p, a, c, s in rows if a and c is not None and a != c]
print(f"prefabs whose created objects != planned objects: {len(short)} of {len(rows)}")
for p, a, c, s in sorted(short, key=lambda r: (r[2] / max(r[1], 1)))[:15]:
    print(f"  {c:5}/{a:<5}  {p}")
