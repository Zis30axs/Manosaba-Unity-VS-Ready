"""Compare the converted data assets against the shipped typetree dumps.

Why: `AuthorTextBuilder.LoadDataAsync` (line 495) does
`item.Name.ToDictionary(t => t.Locale, t => t.Text)`, which throws on a duplicate locale and
NREs on a null list.  The shipped dump is clean, so any duplicate/missing entry found here is a
conversion defect.
"""
import collections
import json
import os
import re

REPO = r"E:\Manosaba-VSCodeReady"
SO_DIR = os.path.join(REPO, "UnityProject", "Assets", "GameContent", "#WitchTrials", "Data", "ScriptableObjects")
DUMP_DIR = os.path.join(REPO, "res", "Assets", "#WitchTrials", "Data", "ScriptableObjects")

PAIRS = [("CharacterData", "_items", ("_name", "_familyName")),
         ("AuthorData", "_items", ("_taggedText",))]


def yaml_items(text, list_field):
    parts = re.split(r"\n  - ", text)
    return [p for p in parts if p.split("\n", 1)[0].strip().startswith("_")]


def report(name, list_field, sub_fields):
    asset = os.path.join(SO_DIR, name + ".asset")
    dump = os.path.join(DUMP_DIR, name + ".asset.json")
    print("=" * 70)
    print(name)
    if not os.path.exists(asset) or not os.path.exists(dump):
        print("  missing file: asset=%s dump=%s" % (os.path.exists(asset), os.path.exists(dump)))
        return
    text = open(asset, encoding="utf-8").read()
    items = yaml_items(text, list_field)
    shipped = json.load(open(dump, encoding="utf-8"))
    s_items = shipped.get(list_field) or []
    print("  converted items=%d   shipped items=%d" % (len(items), len(s_items)))
    if len(items) != len(s_items):
        print("  !! item count mismatch")

    for label, it in (("converted", None), ("shipped", None)):
        pass

    for idx, raw in enumerate(items):
        iid = raw.split("\n", 1)[0].strip()
        for field in sub_fields:
            m = re.search(r"\n    %s:\s*\n(.*?)(?=\n    _|\n  - |\Z)" % field, raw, re.S)
            if not m:
                print("  [%2d] %-12s %-14s FIELD MISSING in converted" % (idx, iid, field))
                continue
            locs = re.findall(r"_locale:\s*(-?\d+)", m.group(1))
            texts = len(re.findall(r"_text:", m.group(1)))
            dup = sorted(k for k, v in collections.Counter(locs).items() if v > 1)
            note = ""
            if dup:
                note += " DUP=%s" % dup
            if texts != len(locs):
                note += " text/ locale mismatch %d!=%d" % (texts, len(locs))
            if note:
                print("  [%2d] %-12s %-14s locs=%s%s" % (idx, iid, field, locs, note))

    for idx, it in enumerate(s_items):
        iid = it.get("_id")
        for field in sub_fields:
            entries = it.get(field)
            if entries is None:
                print("  shipped [%2d] %-12s %-14s NULL" % (idx, iid, field))
                continue
            locs = [e.get("_locale") for e in entries]
            dup = sorted(k for k, v in collections.Counter(locs).items() if v > 1)
            if dup:
                print("  shipped [%2d] %-12s %-14s DUP=%s" % (idx, iid, field, dup))
    print("  (no output above a sub-field means all entries were well formed)")


for name, list_field, subs in PAIRS:
    report(name, list_field, subs)
