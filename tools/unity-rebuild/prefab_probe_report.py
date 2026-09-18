"""Print the prefab build diagnostics that explain a hierarchy shortfall (per job)."""
import io
import json
import os
import sys

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
PATH = sys.argv[1] if len(sys.argv) > 1 else os.path.join(BASE, "prefab_build_probe.json")
doc = json.load(io.open(PATH, encoding="utf-8"))
print(f"{os.path.basename(PATH)}: {doc['status']} built={doc['built']} partial={doc['built_partial']} "
      f"failed={doc['failed']} incomplete={doc.get('incomplete')} "
      f"created={doc['objects_created']} written={doc.get('objects_written')}")

for job in doc["jobs"]:
    print(f"\n{job['asset_path']}")
    print(f"  status={job['status']} planned={job['objects_planned']} created={job['objects_created']} "
          f"written={job.get('objects_written')} root='{job.get('root_name')}' "
          f"children_of_root={job.get('root_children')} roots_detected={job.get('roots_detected')}")
    print(f"  runtime(before save): transforms={job.get('runtime_transforms')} "
          f"components={job.get('runtime_components')} root_children={job.get('runtime_root_children')} "
          f"scene_roots={job.get('runtime_roots_in_scene')}")
    print(f"  stage counts: after_hierarchy={job.get('transforms_after_hierarchy')} "
          f"after_fields={job.get('transforms_after_fields')} "
          f"runtime_transforms={job.get('runtime_transforms')} written_transforms={job.get('saved_transforms')}")
    print(f"  parented={job.get('parented')} parent_missing={job.get('parent_missing')} "
          f"missing_types={job['missing_types']} warnings={job['warning_count']} "
          f"saved_transforms={job.get('saved_transforms')} saved_components={job.get('saved_components')}")
    for warning in (job.get("warnings") or [])[:6]:
        print("    warn:", warning[:220])
