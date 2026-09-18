"""Write a subset prefab plan so PrefabBuilder can be re-run on a handful of prefabs.

    python tools/unity-rebuild/make_prefab_probe_plan.py SaveLoadUI WitchBookUI CreditsUI DebugLabel

Output: tools/unity-rebuild/out/unity/prefab_plan_probe.json (matching MANOSABA_PREFAB_PLAN).
The authoritative plans (prefab_plan_all.json etc.) are never modified."""
from __future__ import annotations

import io
import json
import os
import sys

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
SOURCE = os.path.join(BASE, "prefab_plan_all.json")
TARGET = os.path.join(BASE, "prefab_plan_probe.json")
DEFAULT = ["CreditsUI", "WitchBookUI", "OptionsUI", "SaveLoadUI", "GalleryUI", "Modules/Pagination",
           "DebugLabel", "SimpleCloseButton"]


def main():
    names = sys.argv[1:] or DEFAULT
    with io.open(SOURCE, encoding="utf-8") as fh:
        plan = json.load(fh)

    jobs = [j for j in plan["jobs"] if any(n in j["asset_path"] for n in names)]
    doc = {
        "generated": plan.get("generated"),
        "note": ("subset plan for a PrefabBuilder probe run: " + ", ".join(names) +
                 " (source prefab_plan_all.json, unmodified)"),
        "job_count": len(jobs),
        "jobs": jobs,
    }
    with io.open(TARGET, "w", encoding="utf-8") as fh:
        json.dump(doc, fh, ensure_ascii=False, indent=1)

    print(f"probe plan: {len(jobs)} job(s) -> {TARGET}")
    for job in jobs:
        print(f"  {len(job['objects']):5} objects  {job['asset_path']}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
