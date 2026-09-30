"""Final state check: every plan file on disk, its provenance, its labels, and the invariants."""
import collections
import json
import os

REPO = r"E:\Manosaba-VSCodeReady"
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
DUMP = os.path.join(OUT, "catalog_dump_shipped.json")
dump = json.load(open(DUMP, encoding="utf-8"))
nani = next(r for r in dump["label_membership"] if r["key"] == "Naninovel")
nani_set = set(nani["locations"])
pks = {l["primary_key"] for l in dump["locations"]}

print("dump: size=%d label_like=%d edges=%d truncated=%d keys_complete=%d"
      % (os.path.getsize(DUMP), dump["label_membership_count"], dump["label_membership_edges"],
         dump["label_membership_truncated_keys"], dump["keys_with_complete_locations"]))

plans = sorted(n for n in os.listdir(OUT) if n.startswith("addressables_plan_") and n.endswith(".json"))
print("\n%-34s %-8s %-9s %-9s %-9s %-9s %s" % ("plan", "entries", "labelled", "Naninovel", "labels",
                                               "complete", "address_from_shipped"))
for n in plans:
    d = json.load(open(os.path.join(OUT, n), encoding="utf-8"))
    print("%-34s %-8d %-9d %-9s %-9d %-9s %s" % (
        n, d["entry_count"], d["entries_with_labels"], d["labels_used"].get("Naninovel"),
        d["labels_used_distinct"], d["membership_complete"], d["address_source"].get("address_from_shipped")))
    assert d["shipped_dump"].endswith("catalog_dump_shipped.json"), (n, d["shipped_dump"])
    assert d["membership_complete"] and not d["membership_warnings"], n

d = json.load(open(os.path.join(OUT, "addressables_plan_all.json"), encoding="utf-8"))
e = d["entries"]
labelled = [x for x in e if "Naninovel" in x["labels"]]
paths = {x["container_path"] for x in labelled}
labels = {l for x in e for l in x["labels"]}
print("\nplan address_source            :", d["address_source"])
print("entries with the Naninovel label:", len(labelled), "over", len(paths), "distinct container paths")
print("shipped umbrella records/paths :", len(nani["locations"]), "/", len(nani_set))
print("umbrella paths with no entry   :", len(nani_set - paths), sorted(nani_set - paths)[:2], "...")
print("entries whose address is a label:", sum(1 for x in e if x["address"] in x["labels"]))
print("labels that are some address    :", len(labels & pks))
print("labels_used (all %d)             :" % d["labels_used_distinct"],
      {k: d["labels_used"][k] for k in ("Naninovel", "Audio-Sfx", "Scripts-System", "Scripts-Common",
                                        "Audio-Ambient_001", "Audio-Bgm_001")})
print("\nstray plan files outside out/unity:",
      [p for p in (os.path.join(REPO, "work", f) for f in os.listdir(os.path.join(REPO, "work")))
       if p.endswith(".json") and "addressables_plan" in os.path.basename(p)])

# label-removal bound: the entries on disk still carry their labels, and the settings label table
grp = open(os.path.join(REPO, "UnityProject/Assets/AddressableAssetsData/AssetGroups/Manosaba.Scenarios.asset"),
           encoding="utf-8").read()
print("Scenarios group still carries serialized labels:", "m_SerializedLabels" in grp)
settings = open(os.path.join(REPO, "UnityProject/Assets/AddressableAssetsData/AddressableAssetSettings.asset"),
                encoding="utf-8").read()
tbl = settings.split("m_LabelTable:")[1].split("m_SchemaTemplates")[0]
print("settings m_LabelTable now:", [l.strip(" -") for l in tbl.splitlines()[1:] if l.strip().startswith("-")])
