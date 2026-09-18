"""Show, for the prefabs still excluded, whether the offending component could even be created in this
project (the plan records `our_guid` when a MonoScript in *our* project was bound to the shipped script)."""
import io
import json
import os

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
PLAN = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "prefab_plan_all.json")
WANT = ("WitchBookUI", "CreditsUI")

plan = json.load(io.open(PLAN, encoding="utf-8"))
for job in plan["jobs"]:
    if not any(w in job["asset_path"] for w in WANT):
        continue
    print(f"\n{os.path.basename(job['asset_path'])}")
    seen = {}
    for obj in job["objects"]:
        script = obj.get("script")
        if not script:
            continue
        key = (script.get("assembly"), script.get("namespace"), script.get("class"))
        if key not in seen:
            seen[key] = {"our_guid": script.get("our_guid"), "count": 0}
        seen[key]["count"] += 1
    for (assembly, namespace, cls), info in sorted(seen.items(), key=lambda kv: (kv[0][0] or "")):
        flag = "BOUND" if info["our_guid"] else "not-bound"
        if any(k in (cls or "") for k in ("Polygon", "Noise", "TrueShadow", "MPImage")):
            print(f"  {assembly:22} {namespace}.{cls:24} x{info['count']:<3} {flag}  guid={info['our_guid']}")
    bound = sum(1 for v in seen.values() if v["our_guid"])
    print(f"  distinct component types: {len(seen)}, bound to our project: {bound}")
