"""Answer one question: does the REBUILT catalog expose the shipped addresses as keys?

G6 reports `addr_mismatch == planned` for every family while listing shipped primary keys
(`General/Data/CharacterData`, `Naninovel/Audio/Ambi_1_1`, ...) as absent from the shipped catalog.
Section 32.2 in the ledger already diagnosed that symptom once as a comparison-basis bug, so the
question has to be settled from the catalog itself, parsed by the runtime parser -- never by grepping
the binary (a string grep of a 5.8 MB binary catalog is exactly the trap that note warns about).

Both dumps are produced by `Manosaba.EditorTools.CatalogDump.Run`, i.e. ContentCatalogData.LoadFromFile
plus CreateCustomLocator, so their key sets are directly comparable:

    shipped        tools/unity-rebuild/out/unity/catalog_dump_shipped.json
    ours (fresh)   tools/unity-rebuild/out/unity/catalog_dump_ours_fresh.json

Usage: python tools/unity-rebuild/compare_catalog_keys.py
"""
import json
import os
import re
import sys
from collections import Counter

REPO = r"E:\Manosaba-VSCodeReady"
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
SHIPPED = os.path.join(OUT, "catalog_dump_shipped.json")
OURS = os.path.join(OUT, "catalog_dump_ours_fresh.json")

GUID_RE = re.compile(r"^[0-9a-f]{32}$")
PROBES = [
    "General/Data/CharacterData",
    "General/Data/AuthorData",
    "Naninovel/Audio/Ambi_1_1",
    "Naninovel/Fonts/Common/Sprites/TrialsClockFont",
    "Naninovel/Scripts/System/System_Title",
    "Naninovel",
]


def load(path):
    with open(path, "r", encoding="utf-8") as fh:
        data = json.load(fh)
    if isinstance(data, dict):
        entries = data.get("keys") or data.get("entries") or []
    else:
        entries = data
    keys = {}
    labels = {}
    for e in entries:
        if not isinstance(e, dict):
            continue
        key = e.get("key")
        if key is None:
            continue
        count = e.get("location_count")
        if count is None:
            locs = e.get("locations") or []
            count = len(locs) if isinstance(locs, list) else 0
        keys[key] = count
        if e.get("is_label"):
            labels[key] = count
    return keys, labels


def profile(name, keys):
    guid = [k for k in keys if GUID_RE.match(k)]
    print("%-10s keys=%-7d guid=%-7d label=%-7d non-guid=%d"
          % (name, len(keys), len(guid), 0, len(keys) - len(guid)))
    prefixes = Counter()
    for k in keys:
        if GUID_RE.match(k):
            prefixes["<guid>"] += 1
        else:
            prefixes[k.split("/")[0] if "/" in k else "<no-slash>"] += 1
    top = ", ".join("%s=%d" % kv for kv in prefixes.most_common(8))
    print("           prefixes: %s" % top)
    return prefixes


def main():
    for path in (SHIPPED, OURS):
        if not os.path.exists(path):
            print("MISSING: %s" % path)
            return 1
        print("  %-28s %d bytes" % (os.path.basename(path), os.path.getsize(path)))
    print()
    shipped_keys, shipped_labels = load(SHIPPED)
    our_keys, our_labels = load(OURS)

    profile("shipped", shipped_keys)
    profile("ours", our_keys)
    print()
    print("labels: shipped=%d ours=%d" % (len(shipped_labels), len(our_labels)))
    print()
    print("=== probe keys (present in the catalog's key table?) ===")
    print("%-50s %-22s %s" % ("key", "shipped(locations)", "ours(locations)"))
    for probe in PROBES:
        s = shipped_keys.get(probe)
        o = our_keys.get(probe)
        print("%-50s %-22s %s"
              % (probe, "MISSING" if s is None else str(s), "MISSING" if o is None else str(o)))

    print()
    print("=== label table (the path Naninovel addressing actually uses) ===")
    for probe in PROBES:
        print("  %-46s shipped_label=%s ours_label=%s"
              % (probe,
                 "yes" if probe in shipped_labels else "no",
                 "yes" if probe in our_labels else "no"))

    # How many of the shipped *addresses* (non-guid keys) exist in ours?
    shipped_addr = {k for k in shipped_keys if not GUID_RE.match(k)}
    our_addr = {k for k in our_keys if not GUID_RE.match(k)}
    shared = shipped_addr & our_addr
    print()
    print("shipped addresses (non-guid keys) : %d" % len(shipped_addr))
    print("our addresses (non-guid keys)     : %d" % len(our_addr))
    print("present in both                   : %d" % len(shared))
    print("ours missing from shipped sample  : %s"
          % sorted(our_addr - shipped_addr)[:6])

    # The actual question G6 asks: do our PLAN addresses appear in the shipped catalog?
    # G6 currently answers it against asset *paths* (locations[].internal_id); the shipped keys are
    # the addresses.  Measure both bases so the fix is a measurement, not an opinion.
    plan_path = os.path.join(OUT, "addressables_plan_all.json")
    if not os.path.exists(plan_path):
        print()
        print("addressables_plan_all.json absent; skipping the plan-address comparison")
        return 0
    plan = json.load(open(plan_path, encoding="utf-8"))
    addresses = [e.get("address") for e in plan.get("entries", []) if isinstance(e, dict)]
    addresses = [a for a in addresses if isinstance(a, str) and a]
    print()
    print("=== do our plan addresses match the shipped catalog? ===")
    print("plan entries with an address      : %d" % len(addresses))
    in_keys = sum(1 for a in addresses if a in shipped_keys)
    in_addr = sum(1 for a in addresses if a in shipped_addr)
    in_our_keys = sum(1 for a in addresses if a in our_keys)
    print("  matched against shipped KEYS      : %d / %d" % (in_keys, len(addresses)))
    print("  matched against shipped ADDRESSES : %d / %d" % (in_addr, len(addresses)))
    print("  matched against OUR rebuilt KEYS  : %d / %d" % (in_our_keys, len(addresses)))
    unmatched = [a for a in addresses if a not in shipped_addr]
    print("  not a shipped address             : %d  %s" % (len(unmatched), unmatched[:5]))
    return 0


if __name__ == "__main__":
    sys.exit(main())
