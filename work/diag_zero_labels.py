"""Which inputs make the planner emit ZERO labels while addresses still resolve?

Candidate inputs, all present in out/unity/:
  * the shipped dump            (labels present, umbrella key truncated at 64)
  * a dump of OUR built catalog (no label keys at all -- our build dropped every label)
  * an unreachable dump path    (previous code returned an empty scheme silently)

Each run is redirected into work/neg2/ so no real plan artifact is touched.
"""
import json
import os
import shutil
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
NEG = os.path.join(HERE, "neg2")
sys.path.insert(0, os.path.join(REPO, "tools", "unity-rebuild"))

import a13f_plan_addressables as planner  # noqa: E402

CASES = [
    ("shipped dump (truncated membership)", os.path.join(OUT, "catalog_dump_shipped.json")),
    ("dump of OUR built catalog", os.path.join(OUT, "catalog_dump_ours_built.json")),
    ("unreachable dump path", os.path.join(HERE, "does-not-exist.json")),
]

for name, dump in CASES:
    print("\n================ %s" % name)
    print("dump:", dump, "exists:", os.path.exists(dump))
    shutil.rmtree(NEG, ignore_errors=True)
    os.makedirs(NEG, exist_ok=True)
    planner.SHIPPED_DUMP = dump
    planner.OUT_DIR = NEG
    sys.argv = ["a13f_plan_addressables.py", "--group", "all"]
    code = planner.main()
    doc = json.load(open(os.path.join(NEG, "addressables_plan_all.json"), encoding="utf-8"))
    labelled = sum(1 for e in doc["entries"] if e["labels"])
    print("--> exit=%d entries=%d entries_with_labels=%d labels_used=%d address_source=%s membership_complete=%s"
          % (code, doc["entry_count"], labelled, len(doc["labels_used"]), doc["address_source"],
             doc["membership_complete"]))
    print("    warnings:", len(doc["membership_warnings"]),
          [w["key"] for w in doc["membership_warnings"][:4]])

shutil.rmtree(NEG, ignore_errors=True)
