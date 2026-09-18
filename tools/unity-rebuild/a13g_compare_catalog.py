"""a13g: compare the project's own catalog with the shipped one (G3 verification).

The project builds its own Addressables content (tools/unity-rebuild/a13f_plan_addressables.py ->
AddressablesSetup.cs -> AddressablesBuild.cs).  Both catalogs are binary catalogs in the same format,
dumped through the same runtime path (CatalogDump.cs with MANOSABA_CATALOG_PATH), so they can be
compared field by field:

  * address coverage: every address we built must exist as a key in the shipped catalog
  * per-address type: the resource type of our location must equal the shipped one's
  * provider agreement and location counts, plus the keys we have and the shipped catalog does not

Usage
  python tools/unity-rebuild/a13g_compare_catalog.py \
      --ours out/unity/catalog_dump_ours_core.json --shipped out/unity/catalog_dump_shipped.json
"""
from __future__ import annotations

import argparse
import collections
import json
import os
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
RESULT = os.path.join(OUT_DIR, "catalog_compare.json")


def load(path):
    with open(path, encoding="utf-8") as fh:
        return json.load(fh)


def is_guid(key: str) -> bool:
    return len(key) == 32 and all(c in "0123456789abcdef" for c in key)


def split_keys(dump):
    """(addresses, guid_keys): our build emits one address per entry plus an implicit key per GUID."""
    by_id = {loc["internal_id"]: loc for loc in dump.get("locations", [])}
    addresses, guids = {}, {}
    for record in dump.get("keys", []):
        key = record["key"]
        locs = []
        for internal_id in record.get("locations", []):
            loc = by_id.get(internal_id)
            locs.append({
                "internal_id": internal_id,
                "provider_id": loc.get("provider_id") if loc else None,
                "resource_type": loc.get("resource_type") if loc else None,
            })
        (guids if is_guid(key) else addresses)[key] = locs
    return addresses, guids


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--ours", default=os.path.join("out", "unity", "catalog_dump_ours_core.json"))
    ap.add_argument("--shipped", default=os.path.join("out", "unity", "catalog_dump_shipped.json"))
    ap.add_argument("--out", default=RESULT)
    args = ap.parse_args()

    ours_path = args.ours if os.path.isabs(args.ours) else os.path.join(REPO, "tools", "unity-rebuild", args.ours)
    shipped_path = args.shipped if os.path.isabs(args.shipped) else os.path.join(REPO, "tools", "unity-rebuild", args.shipped)
    ours = load(ours_path)
    shipped = load(shipped_path)

    our_addresses, our_guids = split_keys(ours)
    shipped_addresses, shipped_guids = split_keys(shipped)
    shipped_key_set = set(shipped_addresses) | set(shipped_guids)

    missing = [k for k in our_addresses if k not in shipped_key_set]
    present = [k for k in our_addresses if k in shipped_key_set]

    type_match = type_mismatch = provider_match = provider_mismatch = 0
    mismatches = []
    for key in present:
        mine = our_addresses[key]
        theirs = shipped_addresses.get(key) or shipped_guids.get(key) or []
        my_types = {l["resource_type"] for l in mine}
        their_types = {l["resource_type"] for l in theirs}
        if my_types == their_types:
            type_match += 1
        else:
            type_mismatch += 1
            if len(mismatches) < 25:
                mismatches.append({"key": key, "ours": sorted(t or "(null)" for t in my_types),
                                   "shipped": sorted(t or "(null)" for t in their_types)})
        my_prov = {str(l["provider_id"]).split(".")[-1] for l in mine}
        their_prov = {str(l["provider_id"]).split(".")[-1] for l in theirs}
        if my_prov == their_prov:
            provider_match += 1
        else:
            provider_mismatch += 1

    my_provider_hist = collections.Counter(
        str(loc.get("provider_id")).split(".")[-1] for loc in ours.get("locations", []))
    my_type_hist = collections.Counter(loc.get("resource_type") or "(null)" for loc in ours.get("locations", []))

    result = {
        "ours": {"path": os.path.relpath(ours_path, REPO).replace("\\", "/"),
                 "keys": ours.get("key_count"), "addresses": len(our_addresses), "guid_keys": len(our_guids),
                 "locations": ours.get("location_count"), "catalog_bytes": ours.get("catalog_bytes"),
                 "provider_histogram": dict(my_provider_hist),
                 "resource_type_histogram": dict(my_type_hist)},
        "shipped": {"path": os.path.relpath(shipped_path, REPO).replace("\\", "/"),
                    "keys": shipped.get("key_count"), "addresses": len(shipped_addresses),
                    "guid_keys": len(shipped_guids), "locations": shipped.get("location_count"),
                    "catalog_bytes": shipped.get("catalog_bytes")},
        "addresses_checked": len(our_addresses),
        "addresses_present_in_shipped": len(present),
        "addresses_missing_from_shipped": len(missing),
        "address_missing_sample": missing[:20],
        "resource_type_match": type_match,
        "resource_type_mismatch": type_mismatch,
        "resource_type_mismatch_sample": mismatches,
        "provider_kind_match": provider_match,
        "provider_kind_mismatch": provider_mismatch,
        "note": ("Our GUID keys are this project's asset GUIDs, so they cannot match the shipped ones; "
                 "the meaningful comparison is our *addresses* (set from the shipped catalog's "
                 "primary_key) plus per-address resource type and provider kind. Internal ids differ by "
                 "construction (new bundles)."),
    }
    os.makedirs(os.path.dirname(args.out), exist_ok=True)
    with open(args.out, "w", encoding="utf-8") as fh:
        json.dump(result, fh, ensure_ascii=False, indent=1)

    print(f"ours    : keys={result['ours']['keys']} locations={result['ours']['locations']} "
          f"catalog={result['ours']['catalog_bytes']} bytes")
    print(f"shipped : keys={result['shipped']['keys']} locations={result['shipped']['locations']}")
    print(f"addresses checked          : {result['addresses_checked']}")
    print(f"  present in shipped       : {result['addresses_present_in_shipped']}")
    print(f"  missing from shipped     : {result['addresses_missing_from_shipped']}")
    print(f"resource type match        : {type_match}   mismatch: {type_mismatch}")
    print(f"provider kind match        : {provider_match}   mismatch: {provider_mismatch}")
    print(f"our provider histogram     : {dict(my_provider_hist)}")
    print(f"result                     : {args.out}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
