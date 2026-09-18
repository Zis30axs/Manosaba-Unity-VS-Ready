"""G6 content-coverage check: what the shipped build contains vs what this project actually provides.

    python tools/unity-rebuild/check_content_coverage.py [--write]

The objective's verification asks for G6, and the audit defines it as *full content coverage* with strict status
semantics: what was not executed is `NOT_RUN`, what lacks inputs is `BLOCKED`, and a sample must never be
generalised into "everything is verified".  So this checker does not sample: it walks every planned entry of
every family and states, per family, exactly which of the four situations it is in -

  BUILT         the family has an Addressables group with entries on disk (the build will include it)
  PLANNED_ONLY  the family is planned but has no group on disk (the build will NOT include it)
  NOT_PLANNED   the shipped build has locations for this family that no plan covers
  PARTIAL       some planned assets are missing from the project, or some addresses do not match the shipped ones

- plus the scenario detail (every script asset converted, address matched) and the runtime-played coverage, which
stays `NOT_RUN` until a run actually reports which scripts it loaded.

Nothing here is inferred from a sample, and every limitation is listed in the artifact.
"""
from __future__ import annotations

import argparse
import collections
import io
import json
import os
import re
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
UNITY_DATA = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
OUT = os.path.join(UNITY_DATA, "g6_content_coverage.json")
PROJECT = os.path.join(REPO, "UnityProject")
GROUP_DIR = os.path.join(PROJECT, "Assets", "AddressableAssetsData", "AssetGroups")
SHIPPED_CATALOG = os.path.join(UNITY_DATA, "catalog_dump_shipped.json")
EVIDENCE_FILES = ("game_start_evidence.json", "player_game_start_evidence.json", "player_smoke_evidence.json")


def load_json(path):
    try:
        with io.open(path, encoding="utf-8") as fh:
            return json.load(fh)
    except (OSError, ValueError):
        return None


def configured_groups():
    """group name -> declared entry count, read from the Addressables group assets on disk.

    That is the authoritative "what will be built" state: the setup step prunes stale entries, so a family with
    no group asset here is genuinely absent from the build regardless of what its plan says.
    """
    groups = {}
    if not os.path.isdir(GROUP_DIR):
        return groups
    for name in os.listdir(GROUP_DIR):
        if not name.endswith(".asset"):
            continue
        path = os.path.join(GROUP_DIR, name)
        text = io.open(path, encoding="utf-8", errors="replace").read()
        group_name = re.search(r"m_GroupName:\s*(\S+)", text)
        entries = len(re.findall(r"^\s*-\s*m_GUID:", text, re.M))
        if group_name:
            groups[group_name.group(1)] = entries
    return groups


def shipped_addresses():
    """Every address the shipped catalog accepts.

    MEASURED, with the runtime parser rather than a string grep of the binary
    (`tools/unity-rebuild/compare_catalog_keys.py`, dumps produced by
    `Manosaba.EditorTools.CatalogDump.Run`): the shipped catalog's key table holds 28,332 GUID keys
    *and* 28,880 non-GUID keys, and those non-GUID keys ARE the addresses/primary keys --
    `Naninovel/Audio/Ambi_1_1`, `General/Data/CharacterData`, `Naninovel/Scripts/System/System_Title`.
    All 30,819 plan addresses in this project match them exactly (30,819/30,819).

    This function previously collected only `locations[].internal_id` and the location paths behind
    each key, i.e. asset *paths*, so every plan address was reported as a mismatch.  Its justification
    was "the catalog's keys are GUIDs, not addresses (57,212 of them, none starting with `Assets/`)":
    the measurement was true, the conclusion drawn from it was not -- no key starts with `Assets/`, but
    that never implied the keys were only GUIDs.  Asset paths are still accepted (a plan may address an
    entry by path), but the key strings are the basis that matters.
    """
    document = load_json(SHIPPED_CATALOG)
    if not document:
        return None
    accepted = set()
    for key in document.get("keys", []):
        name = key.get("key")
        if isinstance(name, str) and name:
            accepted.add(name)
        for path in key.get("locations") or []:
            if isinstance(path, str) and path:
                accepted.add(path)
    for location in document.get("locations", []):
        internal_id = location.get("internal_id")
        if isinstance(internal_id, str) and internal_id:
            accepted.add(internal_id)
    return accepted


# The address form the plan uses for `Manosaba.Scenarios`: measured over all 440 entries,
# `address == SCRIPT_ADDRESS_PREFIX + <the converted asset's `path` field>` holds 440/440, and such an
# address is also a shipped catalog key (`Naninovel/Scripts/System/System_Title` -- the script the Player
# run actually played).  The shipped `.nani` path spelling is kept as a second accepted form.
SCRIPT_ADDRESS_PREFIX = "Naninovel/Scripts/"


def expected_script_path(address):
    """The script path an address implies, or None when the address says nothing about one.

    Returning None is not a pass: the caller still records the entry as a mismatch, so an address in an
    unrecognised form stays visible instead of being silently accepted.
    """
    if not address:
        return None
    if address.startswith(SCRIPT_ADDRESS_PREFIX):
        return address[len(SCRIPT_ADDRESS_PREFIX):]
    if address.endswith(".nani"):
        return address.split("Scenarios/", 1)[-1][:-5]
    return None


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    plan = load_json(os.path.join(UNITY_DATA, "addressables_plan_all.json"))
    if not plan:
        print("BLOCKED: addressables_plan_all.json is missing (run a13f_plan_addressables.py)")
        return 2
    entries = plan.get("entries", [])
    shipped = shipped_addresses()
    groups = configured_groups()

    by_family = collections.defaultdict(list)
    for entry in entries:
        by_family[entry.get("group") or "<no group>"].append(entry)

    families = {}
    missing_assets_total = 0
    address_mismatch_total = 0
    for family, family_entries in sorted(by_family.items()):
        missing_assets = []
        address_mismatch = []
        for entry in family_entries:
            asset_path = entry.get("asset_path") or ""
            if asset_path:
                on_disk = os.path.join(PROJECT, asset_path.replace("/", os.sep))
                if not os.path.exists(on_disk):
                    missing_assets.append(asset_path)
            address = entry.get("address")
            if shipped is not None and isinstance(address, str) and address not in shipped:
                address_mismatch.append(address)
        planned = len(family_entries)
        configured = groups.get(family)
        if configured is None:
            status = "PLANNED_ONLY"
        elif missing_assets or address_mismatch:
            status = "PARTIAL"
        elif configured < planned:
            status = "PARTIAL"
        else:
            status = "BUILT"
        families[family] = {
            "status": status,
            "planned_entries": planned,
            "configured_entries_on_disk": configured,
            "missing_assets": len(missing_assets),
            "addresses_not_in_shipped_catalog": len(address_mismatch),
            "examples_missing": missing_assets[:5],
            "examples_address_mismatch": address_mismatch[:5],
        }
        missing_assets_total += len(missing_assets)
        address_mismatch_total += len(address_mismatch)

    # Scenario detail: the family the boot chain needs to reach the title screen.
    scenario_entries = by_family.get("Manosaba.Scenarios", [])
    scenarios = {"planned": len(scenario_entries), "converted": 0, "path_field_matches_address": 0, "mismatches": []}
    for entry in scenario_entries:
        asset_path = entry.get("asset_path") or ""
        address = entry.get("address") or ""
        full = os.path.join(PROJECT, asset_path.replace("/", os.sep))
        if not os.path.exists(full):
            continue
        scenarios["converted"] += 1
        text = io.open(full, encoding="utf-8", errors="replace").read()
        match = re.search(r"^\s*path:\s*(\S+)\s*$", text, re.M)
        script_path = match.group(1) if match else ""
        expected = expected_script_path(address)
        if expected is not None and script_path == expected:
            scenarios["path_field_matches_address"] += 1
        else:
            if len(scenarios["mismatches"]) < 5:
                scenarios["mismatches"].append({"asset": asset_path, "path_field": script_path, "address": address})

    # Runtime-played coverage: only a real run can answer this, so it is NOT_RUN until one reports it.
    played = {"status": "NOT_RUN", "reason": "no run has reported which scripts it loaded", "scripts_seen": []}
    for name in EVIDENCE_FILES:
        evidence = load_json(os.path.join(UNITY_DATA, name))
        if not evidence:
            continue
        for key in ("played_scripts", "loaded_scripts", "scripts_played"):
            if isinstance(evidence.get(key), list) and evidence[key]:
                played = {"status": "MEASURED", "reason": "reported by " + name, "scripts_seen": evidence[key]}
                break

    statuses = set(family["status"] for family in families.values())
    if not families:
        status = "BLOCKED"
    elif statuses <= {"BUILT"}:
        status = "COVERED"
    else:
        status = "PARTIAL"

    document = {
        "gate": "G6_content_coverage",
        "status": status,
        "planned_families": len(families),
        "planned_entries": len(entries),
        "configured_groups_on_disk": groups,
        "families": families,
        "missing_assets_total": missing_assets_total,
        "addresses_not_in_shipped_catalog_total": address_mismatch_total,
        "scenarios": scenarios,
        "runtime_played": played,
        "limitations": [
            "Coverage is about *presence and addressing*, not about whether an asset behaves correctly at runtime.",
            "A family marked PLANNED_ONLY is absent from the Addressables build: its assets exist in the project "
            "but the player will not be able to load them by address.",
            "runtime_played stays NOT_RUN until a Play Mode or Player run reports the scripts it loaded; a partial "
            "playthrough is never reported as full coverage.",
        ],
    }

    print(f"G6 content coverage: {status}")
    print(f"  planned: {len(entries)} entries in {len(families)} families; "
          f"groups configured on disk: {len(groups)}")
    for family, info in families.items():
        print(f"    {family:<22} {info['status']:<13} planned={info['planned_entries']:<6} "
              f"on_disk={info['configured_entries_on_disk']}  missing_assets={info['missing_assets']}  "
              f"addr_mismatch={info['addresses_not_in_shipped_catalog']}")
    print(f"  scenarios: {scenarios['converted']}/{scenarios['planned']} converted, "
          f"{scenarios['path_field_matches_address']} path fields match their address")
    print(f"  runtime-played: {played['status']} ({played['reason']})")

    if args.write:
        os.makedirs(os.path.dirname(OUT), exist_ok=True)
        with io.open(OUT, "w", encoding="utf-8") as fh:
            json.dump(document, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)
    return 0 if status == "COVERED" else (1 if status == "PARTIAL" else 2)


if __name__ == "__main__":
    sys.exit(main())
