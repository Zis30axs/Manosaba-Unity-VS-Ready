"""Inspect the built-catalog dump for the one key the game's boot chain queries.

The rebuilt `AddressableResourceProviderExtended` routes every Naninovel resource lookup through
`AddressablesManager.LoadResourceLocationsAsync`, whose recovered body is

    Addressables.LoadResourceLocationsAsync(masterData.AssetKeyNames, MergeMode.Union, null)

with `masterData` = `WitchTrials.Models.MasterData` loaded from `Resources/MasterData`, whose
`_assetKeyNames` this rebuild wrote as exactly one element: the string "Naninovel".

So the boot chain depends on the built catalog exposing a key (or label) named "Naninovel" that
maps to the Naninovel-managed locations.  This script reads the dump produced by the catalog-dump
editor tool and reports, without guessing:

  * whether the literal key "Naninovel" is present, and how many locations it maps to;
  * how many locations carry "Naninovel" in their labels list (the label path);
  * whether the 440 script locations are covered by that key;
  * a sample of the Naninovel/... and Assets/... keys with their location types and internal ids.

The dump's `keys[].locations` entries are either strings (primary keys) or objects, depending on
how far the dumper followed each edge; both shapes are handled.

Usage:
    python tools/unity-rebuild/inspect_built_catalog_key.py [path-to-catalog_dump_ours_built.json]
"""
import json
import os
import sys
from collections import Counter

HERE = os.path.dirname(os.path.abspath(__file__))
DEFAULT_DUMP = os.path.join(HERE, "out", "unity", "catalog_dump_ours_built.json")

KEY = "Naninovel"
NANI_PREFIX = "Naninovel/"
SCRIPT_PREFIX = "Naninovel/Scripts/"


def describe(entry):
    if isinstance(entry, dict):
        return (entry.get("primary_key"), entry.get("resource_type"), entry.get("internal_id"),
                entry.get("labels"))
    return (entry, None, None, None)


def main():
    path = sys.argv[1] if len(sys.argv) > 1 else DEFAULT_DUMP
    print(f"dump: {path}")
    with open(path, "r", encoding="utf-8") as fh:
        data = json.load(fh)

    print(f"status                : {data.get('status')}")
    print(f"catalog_bytes         : {data.get('catalog_bytes')}")
    print(f"location_count        : {data.get('location_count')}")
    print(f"key_count             : {data.get('key_count')}")
    labels = data.get("labels") or []
    print(f"labels                : {len(labels)} (Naninovel present: {KEY in labels})")

    keys = data.get("keys") or []
    print(f"key entries in dump   : {len(keys)}")

    nani_keys = [k for k in keys if str(k.get("key", "")).startswith(NANI_PREFIX)]
    script_keys = [k for k in keys if str(k.get("key", "")).startswith(SCRIPT_PREFIX)]
    asset_keys = [k for k in keys if str(k.get("key", "")).startswith("Assets/")]
    print(f"keys starting '{NANI_PREFIX}'   : {len(nani_keys)}")
    print(f"keys starting '{SCRIPT_PREFIX}' : {len(script_keys)}")
    print(f"keys starting 'Assets/'       : {len(asset_keys)}")

    # --- the literal key the boot chain asks for ------------------------------------------------
    exact = [k for k in keys if k.get("key") == KEY]
    print()
    if exact:
        entry = exact[0]
        locs = entry.get("locations") or []
        print(f"EXACT KEY '{KEY}': FOUND -- location_count={entry.get('location_count')} "
              f"is_label={entry.get('is_label')} key_type={entry.get('key_type')} "
              f"locate_error={entry.get('locate_error')!r} "
              f"locations_complete={entry.get('locations_complete')} truncated={entry.get('locations_truncated')}")
        print(f"  materialised location entries: {len(locs)}")
        types = Counter(describe(l)[1] for l in locs)
        for name, count in types.most_common():
            print(f"    resource_type {name}: {count}")
        for l in locs[:8]:
            pk, rt, iid, lb = describe(l)
            print(f"      pk={pk!r} type={rt} internal_id={iid!r} labels={lb}")
    else:
        print(f"EXACT KEY '{KEY}': ** ABSENT ** from the key table")

    # --- the label path -------------------------------------------------------------------------
    labelled = [l for l in (data.get("locations") or []) if KEY in (l.get("labels") or [])]
    print(f"\nlocations carrying label '{KEY}': {len(labelled)}")
    if labelled:
        for name, count in Counter(l.get("resource_type") for l in labelled).most_common():
            print(f"    {name}: {count}")

    # --- script coverage ------------------------------------------------------------------------
    print()
    for wanted in ("Naninovel/Scripts/System/System_Title",
                   "Naninovel/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01"):
        hit = [k for k in keys if k.get("key") == wanted]
        if hit:
            locs = hit[0].get("locations") or []
            print(f"  {wanted}: {len(locs)} location(s)")
            for l in locs[:2]:
                pk, rt, iid, lb = describe(l)
                print(f"      pk={pk!r} type={rt} internal_id={iid!r} labels={lb}")
        else:
            print(f"  {wanted}: ** ABSENT **")

    # --- what the Naninovel/ keys actually look like --------------------------------------------
    print("\nsample of 'Naninovel/' keys (first 6):")
    for k in nani_keys[:6]:
        locs = k.get("locations") or []
        first = describe(locs[0]) if locs else (None, None, None, None)
        print(f"  key={k.get('key')!r} count={k.get('location_count')} "
              f"is_label={k.get('is_label')} type={first[1]} internal_id={first[2]!r}")

    print("\nsample of 'Assets/' keys (first 4):")
    for k in asset_keys[:4]:
        locs = k.get("locations") or []
        first = describe(locs[0]) if locs else (None, None, None, None)
        print(f"  key={k.get('key')!r} count={k.get('location_count')} "
              f"is_label={k.get('is_label')} type={first[1]} internal_id={first[2]!r}")

    return 0


if __name__ == "__main__":
    raise SystemExit(main())
