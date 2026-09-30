"""Negative control for the planner: against the *current* shipped dump (whose `locations` lists are
still truncated and which has no `label_membership` section) the planner must fail loudly instead of
silently under-labelling.

The output directory is redirected into work/ so the real plan artifacts are left untouched.
"""
import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, ".."))
sys.path.insert(0, os.path.join(REPO, "tools", "unity-rebuild"))

import a13f_plan_addressables as planner  # noqa: E402

planner.SHIPPED_DUMP = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity",
                                    "catalog_dump_shipped.json")
planner.OUT_DIR = HERE
sys.argv = ["a13f_plan_addressables.py", "--group", "all"]
code = planner.main()
print("\nplanner exit code against the truncated dump:", code)

doc = json.load(open(os.path.join(HERE, "addressables_plan_all.json"), encoding="utf-8"))
print("membership_complete      :", doc["membership_complete"])
print("membership_warnings      :", json.dumps(doc["membership_warnings"], indent=1))
print("shipped_keys_from_complete_lists     :", doc["shipped_keys_from_complete_lists"])
print("shipped_keys_from_label_membership   :", doc["shipped_keys_from_label_membership"])
lbl = {}
for e in doc["entries"]:
    for l in e["labels"]:
        lbl[l] = lbl.get(l, 0) + 1
print("entries carrying the 'Naninovel' label (under-labelled):", lbl.get("Naninovel", 0))
print("distinct labels:", len(lbl))
