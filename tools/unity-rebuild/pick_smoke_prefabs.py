"""One-off: pick clean reconstructed prefabs whose components bind to a *game* script, plus their
shipped Addressables address.  Used to choose the prefabs the Player smoke probe instantiates."""
import io
import json
import os

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
GAME_PREFIXES = ("WitchTrials", "GigaCreation", "Elringus")

plan = json.load(io.open(os.path.join(BASE, "prefab_plan_all.json"), encoding="utf-8"))
addr = json.load(io.open(os.path.join(BASE, "addressables_plan_prefabs.json"), encoding="utf-8"))

address_for = {}
for e in addr["entries"]:
    key = e["asset_path"].replace("\\", "/").lower()
    address_for.setdefault(key, e["address"])


def unity_path(target):
    t = target.replace("\\", "/")
    i = t.find("/Assets/")
    return t[i + 1:] if i >= 0 else t


rows = []
for job in plan["jobs"]:
    scripts = [o["script"] for o in job["objects"] if o.get("script")]
    game = [s for s in scripts if s.get("assembly", "").startswith(GAME_PREFIXES)]
    if not game or job.get("unresolved_counts"):
        continue
    path = unity_path(job["target"])
    rows.append({
        "asset_path": path,
        "address": address_for.get(path.lower(), "(no shipped address)"),
        "objects": len(job["objects"]),
        "scripts": sorted({s["assembly"] + "." + s["class"] for s in game}),
        "witchtrials": any(s["assembly"].startswith("WitchTrials") for s in game),
    })

rows.sort(key=lambda r: (not r["witchtrials"], r["objects"]))
print(f"{len(rows)} clean prefabs bind a game script")
for r in rows[:30]:
    print(f"  {r['objects']:5} objs  {r['address']:66} {r['scripts'][:2]}")
