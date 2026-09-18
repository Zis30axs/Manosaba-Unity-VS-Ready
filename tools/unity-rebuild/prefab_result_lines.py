"""Compact one-line-per-prefab report of a prefab build result (works for full and probe runs)."""
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
    print(f"  {os.path.basename(job['asset_path']):34} {job['status']:18} "
          f"created={job['objects_created']:5} written={str(job.get('objects_written')):5} "
          f"transforms={job.get('saved_transforms')}/{job.get('runtime_transforms')} "
          f"roots={job.get('root_count')} main={job.get('main_asset_name')!r}")
    if job.get("root_splice_note"):
        print(f"      splice: {job['root_splice_note']}")
    for warning in (job.get("warnings") or [])[:3]:
        if "hierarchy incomplete" in warning:
            print(f"      warn: {warning[:220]}")
    if job.get("error"):
        print(f"      error: {job['error'][:200]}")
