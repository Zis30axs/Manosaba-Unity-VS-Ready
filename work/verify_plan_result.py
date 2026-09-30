"""Verification of the re-run plan: label counts, coverage vs the shipped catalogue, and the
invariant that no location's address became a label."""
import collections
import json
import os

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")

plan = json.load(open(os.path.join(OUT, "addressables_plan_all.json"), encoding="utf-8"))
complete = json.load(open(os.path.join(HERE, "..", "tools", "unity-rebuild", "out", "unity", "catalog_dump_shipped.json"), encoding="utf-8"))
entries = plan["entries"]
print("entries                       :", len(entries))
print("address_source                :", plan["address_source"])
print("entries_with_labels           :", plan["entries_with_labels"])
print("labels_used_distinct          :", plan["labels_used_distinct"])
print("membership_complete           :", plan["membership_complete"],
      "warnings:", len(plan["membership_warnings"]))
print("shipped_keys_from_label_membership:", plan["shipped_keys_from_label_membership"])

lbl = collections.Counter(l for e in entries for l in e["labels"])
print("\nNaninovel label -> plan entries:", lbl["Naninovel"])

# ---- coverage: distinct container paths vs shipped locations -------------------------------
nani = next(r for r in complete["label_membership"] if r["key"] == "Naninovel")
nani_set = set(nani["locations"])
labelled = [e for e in entries if "Naninovel" in e["labels"]]
labelled_paths = {e["container_path"] for e in labelled}
print("\nshipped 'Naninovel' locations            :", len(nani_set))
print("distinct container paths now labelled    :", len(labelled_paths))
print("plan entries carrying the label          :", len(labelled))
print("  -> entries per container path (multiplicity):",
      round(len(labelled) / max(1, len(labelled_paths)), 3))
print("shipped Naninovel locations with NO planned entry:", len(nani_set - labelled_paths))

# what are the uncovered ones?
all_paths = {e["container_path"] for e in entries}
uncovered = nani_set - all_paths
by_prefix = collections.Counter()
for iid in uncovered:
    by_prefix[iid.rsplit("/", 1)[0] if "/" in iid else iid] += 1
print("uncovered, grouped by folder (top 12):")
for k, n in by_prefix.most_common(12):
    print("   %-70s %d" % (k, n))
by_ext = collections.Counter(os.path.splitext(i)[1].lower() for i in uncovered)
print("uncovered, by extension:", by_ext.most_common(12))

# entries that did NOT get the label: are their container paths outside the umbrella?
unlabelled = [e for e in entries if "Naninovel" not in e["labels"]]
print("\nplan entries WITHOUT the Naninovel label :", len(unlabelled))
outside = [e for e in unlabelled if e["container_path"] not in nani_set]
print("  of those, container path outside the shipped umbrella:", len(outside))
outs = collections.Counter(e["group"] for e in outside)
print("  by group:", dict(sorted(outs.items())))
poss = collections.Counter(i.rsplit("/", 1)[0] if "/" in i else i for i in
                           {e["container_path"] for e in outside} - {""})
print("  by folder (top 10):")
for k, n in poss.most_common(10):
    print("     %-70s %d" % (k, n))

# ---- invariant: an address never became a label --------------------------------------------
bad_addr = [e for e in entries if e["address"] in e["labels"]]
print("\nentries whose own address is also one of its labels:", len(bad_addr))
pks = {l["primary_key"] for l in complete["locations"]}
labels_that_are_some_address = sorted({l for e in entries for l in e["labels"]} & pks)
print("labels that are some *other* location's primary_key:",
      len(labels_that_are_some_address), labels_that_are_some_address[:5])

# ---- the previously documented labels ------------------------------------------------------
for probe in ("Scripts-System", "Audio-Ambient_001", "Audio-Bgm_001", "Scripts-Common", "Audio-Sfx"):
    rec = next((k for k in complete["keys"] if k["key"] == probe), None)
    print("\nlabel %-18s plan entries=%-6d shipped locations=%s"
          % (probe, lbl.get(probe, 0), rec["location_count"] if rec else "ABSENT"))
    print("   per family:", plan["label_entry_counts_by_group"].get(probe))

print("\nall 91 labels used by the plan (entries per label):")
for k, v in sorted(lbl.items(), key=lambda kv: (-kv[1], kv[0])):
    print("   %-42s %d" % (k, v))
print("\n'labels_used' as written in the plan summary:",
      json.dumps(plan["labels_used"], indent=1)[:600])
