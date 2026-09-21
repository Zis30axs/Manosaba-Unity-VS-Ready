"""a13f: plan the Addressables content build (G3) from the G2 ledger.

The shipped catalog is the authority for addresses: every imported object's Addressables address is
the container path the game used (`Assets/#WitchTrials/...`, `Packages/com.elringus.naninovel/...`),
which the extraction kept in the ledger's `asset_path` column.  This planner turns the ledger into the
entry list the Editor-side setup tool creates, grouped by family.

Groups are a reconstruction decision, not recovered data: the shipped build did not carry the original
Editor group layout, and the audit forbids inventing it, so the layout here is documented and
deterministic (one group per family).  Labels likewise are never fabricated: each one is a key the
shipped catalog itself declares for that location, including the *umbrella* keys that reach
thousands of locations at once (`Naninovel`, which the game loads by name from
MasterData._assetKeyNames).  The membership of those keys comes from the dump's `label_membership`
section, because a per-key list capped at 64 locations cannot express it -- and a dump that still
truncates a key makes this planner exit non-zero instead of silently under-labelling.

Outputs
  tools/unity-rebuild/out/unity/addressables_plan_<group>.json
     {"entries": [{"address":..., "asset_path":..., "group":...}], ...}   (unchanged schema)
  tools/unity-rebuild/out/unity/addressables_plan_summary.json

Usage
  python tools/unity-rebuild/a13f_plan_addressables.py --group core
  python tools/unity-rebuild/a13f_plan_addressables.py --group all
"""
from __future__ import annotations

import argparse
import csv
import collections
import datetime
import json
import os
import re
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
LEDGER = os.path.join(OUT_DIR, "a13_import_ledger.csv")
SO_PLAN_GLOB = "so_convert_plan_"
PREFAB_PLAN = os.path.join(OUT_DIR, "prefab_plan_all.json")

#: group per family: which ledger categories / asset path prefixes land in which group
FAMILY_RULES = [
    ("Manosaba.Scenarios", ("Assets/#WitchTrials/Scenarios/",)),
    ("Manosaba.Data", ("Assets/#WitchTrials/Data/",)),
    ("Manosaba.Text", ("Assets/#WitchTrials/Localization/", "Assets/#WitchTrials/ManagedText/")),
    ("Manosaba.Fonts", ("Assets/#WitchTrials/Fonts/",)),
    ("Manosaba.Video", ("Assets/#WitchTrials/Movies/",)),
    ("Manosaba.Sprites", ("Assets/#WitchTrials/SpriteAtlases/",)),
    ("Manosaba.Prefabs", ("Assets/#WitchTrials/Prefabs/", "Assets/GigaCreation/",
                          "Packages/com.elringus.naninovel/")),
    ("Manosaba.Audio", ("Assets/#WitchTrials/Audio/",)),
    ("Manosaba.Textures", ("Assets/#WitchTrials/Textures/",)),
]

#: groups built by the "core" run: small, dependency-light, fast to build
CORE_GROUPS = {"Manosaba.Scenarios", "Manosaba.Data", "Manosaba.Text", "Manosaba.Fonts"}
#: groups built by the "prefabs" run: the prefab graph plus whatever it depends on
PREFAB_GROUPS = {"Manosaba.Prefabs"}


def group_for(address: str) -> str:
    for group, prefixes in FAMILY_RULES:
        if any(address.startswith(p) for p in prefixes):
            return group
    return "Manosaba.Other"


INDEX = os.path.join(REPO, "res", "_index.csv")

#: Families whose single pack-together bundle is too large to write: one group per SHIPPED bundle instead.
#: Writing a bundle loads every object in it; with the ledger-bound sprites the Prefabs bundle alone carried
#: 2.9 GB of texture data, and Textures (5.8 GB of source images) never finished -- on 2026-09-21 Unity passed
#: 6.6 GB during Write Serialized Files and had to be stopped before the machine ran out of commit.  The shipped
#: build kept these families in 269 / 19 / 36 bundles (general-voice-act02_chapter05_trial 368 MB was its largest),
#: and that boundary is recovered data (res/_index.csv source_bundle), not an invented layout: objects the game
#: shipped together stay together, addresses and labels are unchanged.
SPLIT_FAMILIES = {"Manosaba.Textures", "Manosaba.Prefabs", "Manosaba.Audio"}


def load_container_bundles():
    """container (asset_path) -> the shipped bundle most of its objects came from."""
    counts = collections.defaultdict(collections.Counter)
    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            container = (row.get("asset_path") or "").strip()
            bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
            if container and bundle:
                counts[container][bundle] += 1
    return {c: b.most_common(1)[0][0] for c, b in counts.items()}


def bundle_stem(bundle: str) -> str:
    """general-voice-act01_chapter01_trial_assets_all.bundle -> general-voice-act01_chapter01_trial; ema.bundle -> ema."""
    stem = re.sub(r"_(assets|scenes)_.*$", "", bundle)
    if stem.endswith(".bundle"):
        stem = stem[:-len(".bundle")]
    return re.sub(r"[^A-Za-z0-9_-]", "_", stem) or "unknown"


SHIPPED_DUMP = os.path.join(OUT_DIR, "catalog_dump_shipped.json")
#: hand-maintained, evidence-backed exclusions from the content build (see the file for reasons)
EXCLUSIONS = os.path.join(REPO, "tools", "unity-rebuild", "content_build_exclusions.json")

#: The umbrella keys the engine asks Addressables for *by name*, decoded from the shipped build
#: (WitchTrials.Models.MasterData._assetKeyNames == ["Naninovel"], resources.assets path_id 1454).
#: A dump that does not declare them cannot describe the addressing scheme the game needs; the failure
#: is loud because the symptom is silent: a plan with `labels_used: {}` looks like "no labels planned"
#: when in truth the planner never saw the shipped catalog (a dump of *our own* build has no label
#: keys and uses `Assets/GameContent/#WitchTrials/...` internal ids, so nothing matches).
REQUIRED_UMBRELLA_KEYS = ("Naninovel",)


def load_exclusions():
    """asset_path -> {reason, evidence} for assets that cannot enter a content build yet.

    An exclusion is never a silent skip: the entry stays in the plan with status "excluded" and its
    reason, the summary counts it, and the Editor-side setup tool refuses to add it.  Matching is by
    Unity asset path (unique per plan entry) -- *not* by container path, because the shipped catalog
    reuses a single container path for several imported assets (84 of 156 prefab container paths).
    """
    if not os.path.exists(EXCLUSIONS):
        return {}
    with open(EXCLUSIONS, encoding="utf-8-sig") as fh:
        doc = json.load(fh)
    return {e["asset_path"]: e for e in doc.get("excluded", []) if e.get("asset_path")}


def load_shipped_scheme():
    """container path -> {address, labels, resource_type} from the shipped catalog dump.

    The shipped catalog is the authority for the addressing scheme, and it does **not** use container
    paths as addresses (an earlier version of this planner assumed it did -- wrong):

      * a location's internal_id *is* the asset's container path (the ledger's asset_path column)
      * a location's primary_key is the address the build assigned (Naninovel-style, e.g.
        "General/Data/AuthorData", "Naninovel/Audio/Bgm_1_1")
      * every other non-GUID key reaching the same location is a label (e.g. "Audio-Bgm_001")

    A key may reach *many* locations, and those are the ones that matter: the game's
    MasterData._assetKeyNames is ["Naninovel"] and it loads that one key through
    Addressables.LoadResourceLocationsAsync, so the shipped `Naninovel` key is an umbrella label over
    28,150 of the catalog's 29,097 locations.  The single-location keys listed next to a location are
    therefore not the whole label set: **every** non-GUID key that reaches the location contributes,
    single-location or not, and that membership is read from the dump's `label_membership` section
    (or from a key's `locations` list when that list is complete).  A key whose membership is still
    truncated is never guessed at: it is reported and the planner exits non-zero.

    Returns (scheme, report) where report describes completeness:
      {"shipped_dump", "key_count", "keys_from_complete_lists", "keys_from_label_membership",
       "scheme_entries", "membership_complete",
       "warnings": [{"key", "location_count", "locations_available", "reason"}]}

    Nothing here is silent: a missing/unusable dump, a dump that does not declare the umbrella keys the
    engine asks for, and a key whose membership is still truncated all become warnings, and `main()`
    turns any warning into a non-zero exit.  A dump of *our own* build is the trap this guards against:
    it is a valid catalog with no label keys at all, so it used to yield a plan with `labels_used: {}`
    and no error -- indistinguishable from "this project needs no labels".
    """
    scheme = {}
    report = {"shipped_dump": None, "key_count": 0, "keys_from_complete_lists": 0,
              "keys_from_label_membership": 0, "scheme_entries": 0, "membership_complete": True,
              "warnings": []}
    if not os.path.exists(SHIPPED_DUMP):
        report["membership_complete"] = False
        report["warnings"].append({"key": None, "location_count": 0, "locations_available": 0,
                                   "reason": "shipped catalog dump not found at %s" % SHIPPED_DUMP})
        return scheme, report
    report["shipped_dump"] = SHIPPED_DUMP
    with open(SHIPPED_DUMP, encoding="utf-8") as fh:
        dump = json.load(fh)
    locations = {loc["internal_id"]: loc for loc in dump.get("locations", [])}
    # complete membership of every key that reaches more than one location (CatalogDump.cs)
    membership = {}
    for entry in dump.get("label_membership", []) or []:
        membership[entry["key"]] = entry

    # The dump must be the *shipped* catalog: the umbrella keys the engine queries by name have to be
    # in its key table.  Without this check a dump of our own build (no label keys, different internal
    # ids) silently produces a label-free plan.
    for required in REQUIRED_UMBRELLA_KEYS:
        if required not in membership and required not in {r.get("key") for r in dump.get("keys", [])}:
            report["membership_complete"] = False
            report["warnings"].append({
                "key": required, "location_count": 0, "locations_available": 0,
                "reason": ("the dump declares no '%s' key; this is not the shipped catalog (a dump of "
                           "our own build carries no label keys and uses Assets/GameContent/#WitchTrials/... "
                           "internal ids)" % required)})

    def complete_locations(record):
        """The key's complete location list, or None when the dump does not carry it."""
        declared = record.get("location_count", 0)
        listed = record.get("locations") or []
        if len(listed) >= declared:
            return listed, "list"
        entry = membership.get(record["key"])
        entry_listed = (entry or {}).get("locations") or []
        if entry is not None and not entry.get("locations_truncated") and len(entry_listed) >= declared:
            return entry_listed, "label_membership"
        return None, ("label_membership entry truncated" if entry is not None
                      else "no label_membership entry (dump predates the complete-membership section?)")

    for record in dump.get("keys", []):
        key = record["key"]
        report["key_count"] += 1
        if len(key) == 32 and all(c in "0123456789abcdef" for c in key):
            continue                                  # implicit GUID key
        listed, source = complete_locations(record)
        if listed is None:
            # Never silently under-label: a truncated key is exactly how the missing `Naninovel`
            # umbrella label hid the first time.
            report["membership_complete"] = False
            report["warnings"].append({
                "key": key,
                "location_count": record.get("location_count", 0),
                "locations_available": len(record.get("locations") or []),
                "reason": source,
            })
            listed = record.get("locations") or []
        elif source == "list":
            report["keys_from_complete_lists"] += 1
        else:
            report["keys_from_label_membership"] += 1
        for internal_id in listed:
            loc = locations.get(internal_id)
            if not loc:
                continue
            entry = scheme.setdefault(internal_id, {"address": None, "labels": [],
                                                    "resource_type": loc.get("resource_type")})
            if key == loc.get("primary_key"):
                entry["address"] = key          # the location's own address is never a label
            elif key not in entry["labels"]:
                entry["labels"].append(key)
    report["scheme_entries"] = len(scheme)
    return scheme, report


def unity_path(repo_relative: str) -> str:
    path = repo_relative.replace("\\", "/").lstrip("/")
    if path.startswith("UnityProject/"):
        path = path[len("UnityProject/"):]
    index = path.find("/Assets/")
    if index >= 0:
        path = path[index + 1:]
    return path


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--group", default="core")
    args = ap.parse_args()

    scheme, membership = load_shipped_scheme()
    exclusions = load_exclusions()
    container_bundles = load_container_bundles()
    entries = []
    seen = set()
    stats = collections.Counter()

    def add(container_path: str, asset_path: str, source: str):
        """One entry: address and labels come from the shipped catalog, keyed by container path."""
        if not container_path or not asset_path or asset_path in seen:
            return
        seen.add(asset_path)
        shipped = scheme.get(container_path)
        if shipped and shipped.get("address"):
            address = shipped["address"]
            labels = shipped.get("labels", [])
            stats["address_from_shipped"] += 1
        else:
            address = container_path          # no shipped counterpart: documented fallback
            labels = []
            stats["address_fallback"] += 1
        if labels:
            stats["with_labels"] += 1
        # the group is chosen from the container path (a stable family signal), never from the address; the
        # large families are further split along the shipped bundle boundary (SPLIT_FAMILIES)
        family = group_for(container_path)
        group = family
        if family in SPLIT_FAMILIES:
            shipped_bundle = container_bundles.get(container_path)
            if shipped_bundle:
                group = family + "." + bundle_stem(shipped_bundle)
            else:
                stats["split_family_without_shipped_bundle"] += 1
        entry = {"address": address, "asset_path": asset_path,
                 "labels": labels, "container_path": container_path,
                 "group": group, "family": family, "source": source, "status": "planned"}
        excluded = exclusions.get(asset_path)
        if excluded:
            entry["status"] = "excluded"
            entry["exclusion_reason"] = excluded.get("reason", "")
            entry["exclusion_evidence"] = excluded.get("evidence", "")
            stats["excluded"] += 1
        entries.append(entry)

    # leaf assets + sprites
    with open(LEDGER, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            if row["status"] != "IMPORTED":
                continue
            add((row["asset_path"] or "").strip(), unity_path((row["target_path"] or "").strip()), "a13")

    # ScriptableObjects
    for name in sorted(os.listdir(OUT_DIR)):
        if name.startswith(SO_PLAN_GLOB) and name.endswith(".json"):
            with open(os.path.join(OUT_DIR, name), encoding="utf-8") as fh:
                plan = json.load(fh)
            for job in plan.get("jobs", []):
                add(job["asset_path"], unity_path(job["target"]), name)

    # prefabs
    if os.path.exists(PREFAB_PLAN):
        with open(PREFAB_PLAN, encoding="utf-8") as fh:
            plan = json.load(fh)
        for job in plan.get("jobs", []):
            add(job["asset_path"], unity_path(job["target"]), "prefab_plan_all")

    if args.group == "core":
        selected = [e for e in entries if e["family"] in CORE_GROUPS]
    elif args.group == "prefabs":
        selected = [e for e in entries if e["family"] in PREFAB_GROUPS]
    else:
        selected = entries
    by_group = collections.Counter(e["group"] for e in selected)
    by_family = collections.Counter(e["family"] for e in selected)
    by_source = collections.Counter(e["source"] for e in selected)
    labels_used = collections.Counter(l for e in selected for l in e["labels"])
    entries_with_labels = sum(1 for e in selected if e["labels"])
    # per-family (per-group) breakdown of every label, so a label's reach is visible family by family
    labels_by_group = collections.defaultdict(collections.Counter)
    for e in selected:
        for l in e["labels"]:
            labels_by_group[l][e["family"]] += 1
    excluded_entries = [e for e in selected if e["status"] == "excluded"]
    buildable = [e for e in selected if e["status"] != "excluded"]

    # A plan whose addresses all fell back to container paths did not see the shipped catalog at all
    # (dump missing, or a dump of our own build whose internal ids are Assets/GameContent/...).  That
    # is the "labels_used: {}" trap: it is indistinguishable from a project that needs no labels, so
    # it is a loud failure, not a note.
    if entries and stats["address_from_shipped"] == 0:
        membership["membership_complete"] = False
        membership["warnings"].append({
            "key": None, "location_count": 0, "locations_available": membership["scheme_entries"],
            "reason": ("no plan entry matched the shipped catalog: every address is a container-path "
                       "fallback and no label can be derived (dump not found, or not the shipped "
                       "catalog)")})

    doc = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "group": args.group,
        "entry_count": len(buildable),
        "entry_count_including_excluded": len(selected),
        "excluded_count": len(excluded_entries),
        "excluded": [{"container_path": e["container_path"], "asset_path": e["asset_path"],
                      "reason": e.get("exclusion_reason", ""), "evidence": e.get("exclusion_evidence", "")}
                     for e in excluded_entries],
        "address_source": dict(sorted(stats.items())),
        "entries_with_labels": entries_with_labels,
        # complete, not capped: the shipped catalog declares only 91 label-like keys, and a cap is
        # exactly what hides the label a reader is looking for (labels_used_distinct states the total)
        "labels_used": dict(sorted(labels_used.items())),
        "labels_used_distinct": len(labels_used),
        "label_entry_counts_by_group": {l: dict(sorted(g.items()))
                                        for l, g in sorted(labels_by_group.items())},
        "shipped_dump": membership["shipped_dump"],
        "shipped_key_count": membership["key_count"],
        "shipped_scheme_entries": membership["scheme_entries"],
        "shipped_keys_from_complete_lists": membership["keys_from_complete_lists"],
        "shipped_keys_from_label_membership": membership["keys_from_label_membership"],
        "membership_complete": membership["membership_complete"],
        "membership_warnings": membership["warnings"],
        "groups": dict(sorted(by_group.items())),
        "families": dict(sorted(by_family.items())),
        "split_families": sorted(SPLIT_FAMILIES),
        "by_source": dict(sorted(by_source.items())),
        "note": ("Addresses and labels come from the shipped catalog: the location's primary_key is the address "
                 "and its other non-GUID keys are the labels. Do not describe the address as a 'container path': "
                 "the shipped catalog has 29,097 locations and none of their internal_ids appears as a key, so "
                 "internal_id (container path) and key are different namespaces and only the key is what the "
                 "engine asks Addressables for (a scenario's key is 'Naninovel/Scripts/<script path>'). A key "
                 "that reaches more than one location is a label over all of them, and its complete membership "
                 "comes from the dump's label_membership section -- that is how the umbrella label 'Naninovel' "
                 "(MasterData._assetKeyNames, 28,150 locations in the shipped catalog) reaches every entry it "
                 "covers instead of only the first 64. 'labels_used' is the complete label -> entry count map and "
                 "'label_entry_counts_by_group' breaks each label down by family. Group "
                 "layout is a documented reconstruction decision (one group per family, and for the large families "
                 "in 'split_families' one group per shipped bundle, so no single bundle write exceeds what the "
                 "shipped build wrote); the shipped build carried no Editor group layout. No label is fabricated. Entries with "
                 "status 'excluded' come from tools/unity-rebuild/content_build_exclusions.json and are "
                 "left in the plan with their reason, but the Editor-side setup tool does not add them. "
                 "'membership_complete' false means the plan must not be used: 'membership_warnings' names "
                 "the reason -- a shipped key whose membership is still truncated (regenerate the dump with "
                 "the current CatalogDump.cs, which writes the label_membership section), a dump that does "
                 "not declare the umbrella keys the engine asks for, or a dump that matched no plan entry "
                 "at all ('address_source' then has no 'address_from_shipped')."),
        "entries": selected,
    }
    os.makedirs(OUT_DIR, exist_ok=True)
    path = os.path.join(OUT_DIR, f"addressables_plan_{args.group}.json")
    with open(path, "w", encoding="utf-8") as fh:
        json.dump(doc, fh, ensure_ascii=False, indent=1)

    print(f"group      : {args.group}")
    print(f"entries    : {len(buildable)} buildable of {len(entries)} known "
          f"({len(excluded_entries)} excluded, {len(selected) - len(buildable)} marked)")
    print(f"by family  : {dict(sorted(by_family.items()))}")
    print(f"groups     : {len(by_group)} ({len(by_group) - len(by_family)} more than families, from split_families)")
    print(f"by source  : {dict(sorted(by_source.items()))}")
    print(f"labels     : {entries_with_labels} entries carry a label; "
          f"{len(labels_used)} distinct labels")
    print(f"shipped    : {membership['shipped_dump']} keys={membership['key_count']} "
          f"scheme_entries={membership['scheme_entries']}")
    for label, count in sorted(labels_used.items(), key=lambda kv: (-kv[1], kv[0]))[:10]:
        print(f"   label   : {label} -> {count} entries {dict(sorted(labels_by_group[label].items()))}")
    for e in excluded_entries:
        print(f"excluded   : {e['container_path']}")
        print(f"             {e.get('exclusion_reason', '')}")
    print(f"plan       : {path}")
    if membership["warnings"]:
        # Loud on purpose: a plan written from an incomplete or wrong catalog looks perfectly normal
        # (`labels_used: {}` reads like "no labels needed"), which is how the missing `Naninovel`
        # umbrella label hid the first time.  The plan is still written so the damage is inspectable,
        # but the exit code is non-zero.
        print("", file=sys.stderr)
        print("SHIPPED CATALOG SCHEME INCOMPLETE -- this plan must NOT be applied or built:", file=sys.stderr)
        for w in membership["warnings"][:20]:
            print("   key=%r declared=%s available=%s: %s"
                  % (w["key"], w["location_count"], w["locations_available"], w["reason"]), file=sys.stderr)
        if len(membership["warnings"]) > 20:
            print("   ... and %d more" % (len(membership["warnings"]) - 20), file=sys.stderr)
        print("Fix the input first: re-run Manosaba/Dump Shipped Addressables Catalog with the current "
              "CatalogDump.cs and check that shipped_dump points at the SHIPPED catalog "
              "(out/unity/catalog_dump_shipped.json), then re-run this planner.", file=sys.stderr)
    return 1 if membership["warnings"] else 0


if __name__ == "__main__":
    sys.exit(main())



