"""Explain the 30,333 vs 28,150 difference from the data (no hand-waving)."""
import collections
import csv
import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
sys.path.insert(0, HERE)

complete = json.load(open(os.path.join(OUT, "catalog_dump_shipped.json"), encoding="utf-8"))
plan = json.load(open(os.path.join(OUT, "addressables_plan_all.json"), encoding="utf-8"))
nani = next(r for r in complete["label_membership"] if r["key"] == "Naninovel")
recs = nani["locations"]
distinct = collections.Counter(recs)
print("Naninovel location records          :", len(recs))
print("distinct container paths (internal_id):", len(distinct))
print("records that duplicate a container path:", sum(n - 1 for n in distinct.values() if n > 1),
      "across", sum(1 for n in distinct.values() if n > 1), "paths")
dups = [(k, n) for k, n in distinct.items() if n > 1]
print("examples of duplicated paths (same internal_id, different ResourceType):")
loc_by_id = collections.defaultdict(list)
for l in complete["locations"]:
    loc_by_id[l["internal_id"]].append(l)
for k, n in dups[:3]:
    print("   %s" % k)
    for l in loc_by_id[k]:
        print("      resource_type=%s provider=%s" % (l["resource_type"], l["provider_id"].split(".")[-1]))

entries = plan["entries"]
labelled = [e for e in entries if "Naninovel" in e["labels"]]
labelled_paths = {e["container_path"] for e in labelled}
print("\nplan entries with the label         :", len(labelled))
print("distinct container paths they cover :", len(labelled_paths))
multi = collections.Counter(e["container_path"] for e in labelled)
print("container paths claimed by >1 entry  :", sum(1 for n in multi.values() if n > 1),
      "-> extra entries:", sum(n - 1 for n in multi.values() if n > 1))
print("example:", [(k, n) for k, n in multi.most_common(3)])

missing = sorted(set(distinct) - labelled_paths)
print("\nshipped Naninovel container paths with no plan entry:", len(missing))
ledger = {}
with open(os.path.join(OUT, "a13_import_ledger.csv"), encoding="utf-8-sig", newline="") as fh:
    for row in csv.DictReader(fh):
        ledger[(row["asset_path"] or "").strip()] = row
for path in missing:
    row = ledger.get(path)
    print("   %-72s ledger=%s" % (path, (row or {}).get("status", "NOT-IN-LEDGER")))
    if row:
        print("       reason/notes: %s" % (row.get("reason") or row.get("note") or "(none)"))
    # does the imported Unity asset exist?
    target = None
    if row:
        target = (row.get("target_path") or "").strip()
    if target:
        print("       target exists: %s  (%s)" % (os.path.exists(target), target))

# and the other direction: entries without the label
print("\nentries without the label:", len(entries) - len(labelled))
outside = [e for e in entries if "Naninovel" not in e["labels"]]
print("all of them have a shipped address :",
      all(e["container_path"] not in distinct for e in outside))
sample = collections.Counter(e["address"].rsplit("/", 1)[0] for e in outside)
print("their shipped address families (top 10):", sample.most_common(10))
