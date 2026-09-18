"""Before/after numbers for the prefab fidelity fix, straight from the build result."""
import io
import json
import os
import sys

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
doc = json.load(io.open(os.path.join(BASE, "prefab_build_result.json"), encoding="utf-8"))
want = sys.argv[1:] or ["CreditsUI", "WitchBookUI", "SaveLoadUI", "OptionsUI", "TitleUI", "DebugLabel",
                        "SimpleCloseButton", "Pagination"]

for job in doc["jobs"]:
    name = os.path.basename(job["asset_path"]).replace(".prefab", "")
    if name not in want:
        continue
    print(f"{name:18} planned={job['objects_planned']:5} created={job['objects_created']:5} "
          f"written={job.get('objects_written'):5} transforms={job.get('saved_transforms'):4} "
          f"roots={job.get('root_count')} extra_root_objects={job.get('objects_in_extra_roots')} "
          f"unbuildable={job.get('objects_unbuildable')} status={job['status']}")
