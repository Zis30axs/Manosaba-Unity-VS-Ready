"""a13b: resolve the shipped-build script binding (MonoScript PPtr -> reconstructed GUID).

Every MonoBehaviour / ScriptableObject in a shipped bundle stores its type as
`m_Script: {m_FileID, m_PathID}`.  m_FileID indexes the referencing SerializedFile's externals
table, and the target is a MonoScript object in *another* CAB whose m_Namespace / m_ClassName /
m_AssemblyName name the type.  The composite converters (ScriptableObject, prefab) need that name,
and then need our own script GUID for it (tools/unity-rebuild/a13_script_index.py).

Resolver design (on demand, not a 6.4 GB sweep):
  * CAB -> bundle index from tools/audit/out/a03/per_source/*.json (object_counts_by_file keys,
    which list every CAB the audit saw, with `rel` giving the file)
  * for the requested bundles: read each MonoBehaviour's m_Script, walk its external CAB, open that
    bundle once (cached) and read its MonoScript objects
  * join the resulting (namespace, class, assembly) with out/unity/reconstructed_script_index.json

Usage
  python tools/unity-rebuild/a13b_script_binding.py --family data
  python tools/unity-rebuild/a13b_script_binding.py --bundles general-data,general-scenarios
  python tools/unity-rebuild/a13b_script_binding.py --list-families
"""
from __future__ import annotations

import argparse
import collections
import datetime
import json
import os
import sys

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
PER_SOURCE = os.path.join(REPO, "tools", "audit", "out", "a03", "per_source")
SCRIPT_INDEX = os.path.join(OUT_DIR, "reconstructed_script_index.json")
#: accumulated (bundle, path_id) -> resolved script; merged across runs so several families can be
#: resolved one at a time without losing earlier results
MAP_OUT = os.path.join(OUT_DIR, "bundle_script_map.json")
REPORT_OUT = os.path.join(OUT_DIR, "script_binding.json")

#: the rename apply_game_sources.py had to make (Unity reserves the name Assembly-CSharp)
ALIASES = {"Assembly-CSharp": "WitchTrials.Game"}


def game_root():
    cfg = os.path.join(REPO, "tools", "audit", "audit.config.json")
    with open(cfg, encoding="utf-8-sig") as fh:
        root = json.load(fh).get("game_root", "")
    if not root or not os.path.isdir(root):
        raise SystemExit(f"game_root not usable: {root!r} (see {cfg})")
    return root


def build_cab_index(game):
    """CAB (lowercase) -> bundle path, plus the list of bundle paths, from the audit's scan."""
    data_root = os.path.join(game, "manosaba_Data")
    cab_to_bundle = {}
    bundles = []
    if not os.path.isdir(PER_SOURCE):
        raise SystemExit(f"missing {PER_SOURCE}: run tools/audit/a03_sources.py first")
    for name in sorted(os.listdir(PER_SOURCE)):
        if not name.endswith(".json"):
            continue
        try:
            with open(os.path.join(PER_SOURCE, name), encoding="utf-8") as fh:
                data = json.load(fh)
        except (OSError, ValueError):
            continue
        rel = (data.get("rel") or "").replace("/", os.sep)
        full = os.path.join(data_root, rel) if rel else ""
        if not full or not os.path.exists(full):
            continue
        bundles.append(full)
        for cab in (data.get("object_counts_by_file") or {}):
            cab_to_bundle.setdefault(cab.lower(), full)
        for cab in (data.get("serialized_files") or []):
            cab_name = cab if isinstance(cab, str) else cab.get("name")
            if cab_name:
                cab_to_bundle.setdefault(cab_name.lower(), full)
    return cab_to_bundle, sorted(set(bundles))


class Resolver:
    """Resolves script references inside bundles; caches opened bundles and MonoScript tables."""

    def __init__(self, game):
        self.game = game
        self.cab_to_bundle, self.bundles = build_cab_index(game)
        self._env_cache = {}
        self._script_cache = {}

    def _load(self, path):
        if path not in self._env_cache:
            self._env_cache[path] = UnityPy.load(path)
        return self._env_cache[path]

    def monoscripts(self, bundle_path):
        """(cab, path_id) -> (namespace, class, assembly) for one bundle."""
        if bundle_path in self._script_cache:
            return self._script_cache[bundle_path]
        table = {}
        try:
            env = self._load(bundle_path)
        except Exception as exc:  # noqa: BLE001
            self._script_cache[bundle_path] = table
            print(f"    ! could not load {os.path.basename(bundle_path)}: {type(exc).__name__}")
            return table
        for obj in env.objects:
            if obj.type.name != "MonoScript":
                continue
            try:
                data = obj.read()
            except Exception:  # noqa: BLE001
                continue
            key = (getattr(obj.assets_file, "name", "?").lower(), obj.path_id)
            table[key] = (getattr(data, "m_Namespace", "") or "",
                          getattr(data, "m_ClassName", "") or "",
                          getattr(data, "m_AssemblyName", "") or "")
        self._script_cache[bundle_path] = table
        return table

    def resolve_bundle(self, bundle_path):
        """Return (records, error) for one bundle: script references and what they point at."""
        records = []
        try:
            env = self._load(bundle_path)
        except Exception as exc:  # noqa: BLE001
            return [], f"load failed: {type(exc).__name__}: {exc}"

        for obj in env.objects:
            if obj.type.name != "MonoBehaviour":
                continue
            try:
                tree = obj.read_typetree()
            except Exception as exc:  # noqa: BLE001
                records.append({"name": "?", "path_id": obj.path_id, "status": "typetree_failed",
                                "error": f"{type(exc).__name__}: {exc}"})
                continue
            script = tree.get("m_Script") or {}
            file_id = int(script.get("m_FileID", 0) or 0)
            path_id = int(script.get("m_PathID", 0) or 0)
            name = tree.get("m_Name") or f"pathid_{obj.path_id}"
            cab = getattr(obj.assets_file, "name", "?").lower()

            if path_id == 0:
                records.append({"name": name, "path_id": obj.path_id, "status": "no_script"})
                continue

            if file_id == 0:
                hit = self.monoscripts(bundle_path).get((cab, path_id))
                target_cab = cab
            else:
                externals = [getattr(e, "path", "") for e in getattr(obj.assets_file, "externals", [])]
                target_cab = externals[file_id - 1].split("/")[-1].lower() if 0 < file_id <= len(externals) else ""
                provider = self.cab_to_bundle.get(target_cab)
                hit = self.monoscripts(provider).get((target_cab, path_id)) if provider else None

            record = {"name": name, "path_id": obj.path_id, "file_id": file_id,
                      "cab": target_cab, "status": "resolved" if hit else "unresolved"}
            if hit:
                record["namespace"], record["class"], record["assembly"] = hit
            records.append(record)
        return records, None


def load_our_scripts():
    index = {}
    if os.path.exists(SCRIPT_INDEX):
        with open(SCRIPT_INDEX, encoding="utf-8") as fh:
            doc = json.load(fh)
        for entry in doc.get("entries", []):
            for t in entry.get("types", []):
                index[(entry["assembly"], entry["namespace"], t["name"])] = entry["guid"]
    return index


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--family", default="", help="bundle name substring, e.g. data / scenarios / ui")
    ap.add_argument("--bundles", default="", help="comma separated bundle name substrings")
    ap.add_argument("--from-index", action="store_true",
                    help="take the bundle list from res/_index.csv (every bundle holding a GameObject)")
    ap.add_argument("--list-families", action="store_true")
    ap.add_argument("--max-bundles", type=int, default=40)
    args = ap.parse_args()

    game = game_root()
    resolver = Resolver(game)

    if args.list_families:
        counts = collections.Counter()
        for b in resolver.bundles:
            stem = os.path.basename(b).split("_assets")[0]
            counts[stem] += 1
        for stem, n in counts.most_common(40):
            print(f"  {n:5}  {stem}")
        print(f"total bundles: {len(resolver.bundles)}  (CAB index: {len(resolver.cab_to_bundle)})")
        return 0

    if args.from_index:
        import csv
        index = os.path.join(REPO, "res", "_index.csv")
        wanted = set()
        with open(index, encoding="utf-8-sig", newline="") as fh:
            for row in csv.DictReader(fh):
                if (row.get("type") or "").strip() in ("GameObject", "MonoBehaviour"):
                    bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
                    if bundle:
                        wanted.add(bundle.lower())
        targets = [b for b in resolver.bundles if os.path.basename(b).lower() in wanted]
        print(f"bundles holding GameObjects (from the index): {len(targets)} of {len(resolver.bundles)}")
    else:
        needles = [n.strip() for n in (args.bundles or args.family).split(",") if n.strip()]
        if not needles:
            raise SystemExit("give --family, --bundles or --from-index (or --list-families)")
        targets = [b for b in resolver.bundles if any(n.lower() in os.path.basename(b).lower() for n in needles)]
        needles_used = needles
    if not args.from_index:
        needles_used = needles
    else:
        needles_used = ["<from-index>"]
    targets = targets[:args.max_bundles] if args.max_bundles else targets
    if not targets:
        raise SystemExit("no bundle matched")
    print(f"bundles to scan: {len(targets)}")

    our_scripts = load_our_scripts()
    all_records = []
    per_bundle = {}
    for path in targets:
        records, error = resolver.resolve_bundle(path)
        stem = os.path.basename(path)
        per_bundle[stem] = {"scripts": len(records), "error": error}
        for r in records:
            r["bundle"] = stem
            if r.get("status") == "resolved":
                our_asm = ALIASES.get(r["assembly"], r["assembly"])
                r["our_guid"] = our_scripts.get((our_asm, r["namespace"], r["class"])) or \
                                our_scripts.get((our_asm, "", r["class"]))
                r["status"] = "bound" if r["our_guid"] else "type_known_not_reconstructed"
            all_records.append(r)

    counts = collections.Counter(r["status"] for r in all_records)
    types = {(r.get("namespace"), r.get("class"), r.get("assembly"))
             for r in all_records if r.get("class")}

    # ---- merge into the accumulated map -------------------------------------------------
    merged = {}
    if os.path.exists(MAP_OUT):
        try:
            with open(MAP_OUT, encoding="utf-8") as fh:
                merged = json.load(fh).get("entries", {})
        except (OSError, ValueError):
            merged = {}
    added = 0
    for r in all_records:
        # Keep both outcomes: a type that is known but not part of the reconstruction is usually a
        # Unity/package component (UnityEngine.UI.Image, TMPro.TextMeshProUGUI, ...) that the prefab
        # builder must resolve from the packages.  Dropping those rows left 3,378 components
        # uncreatable.  `bound` says whether our own script GUID exists.
        if r.get("status") not in ("bound", "type_known_not_reconstructed"):
            continue
        key = f"{r['bundle']}|{r['path_id']}"
        if key not in merged:
            added += 1
        merged[key] = {
            "asset_path": None,
            "namespace": r.get("namespace", ""),
            "class": r.get("class", ""),
            "assembly": r.get("assembly", ""),
            "our_guid": r.get("our_guid", ""),
            "bound": r.get("status") == "bound",
        }
    map_doc = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "format": "'<bundle file name>|<path_id>' -> {namespace, class, assembly, our_guid}",
        "entry_count": len(merged),
        "entries": merged,
    }
    os.makedirs(OUT_DIR, exist_ok=True)
    with open(MAP_OUT, "w", encoding="utf-8") as fh:
        json.dump(map_doc, fh, ensure_ascii=False, indent=1)

    doc = {
        "generated": map_doc["generated"],
        "needles": needles_used,
        "bundles_scanned": len(targets),
        "scripts_seen": len(all_records),
        "status_counts": dict(counts),
        "distinct_types": len(types),
        "types": sorted([".".join(filter(None, t)) for t in types]),
        "per_bundle": per_bundle,
        "records": all_records,
        "accumulated_map": {"path": os.path.relpath(MAP_OUT, REPO).replace("\\", "/"),
                            "entries": len(merged), "added_this_run": added},
    }
    with open(REPORT_OUT, "w", encoding="utf-8") as fh:
        json.dump(doc, fh, ensure_ascii=False, indent=1)

    print(f"scripts seen    : {len(all_records)}   status: {dict(counts)}")
    print(f"distinct types  : {len(types)}")
    unresolved = [r for r in all_records if r["status"] == "unresolved"][:5]
    for r in unresolved:
        print(f"  unresolved: {r['bundle']} {r['name']} cab={r.get('cab')} pid={r['path_id']}")
    print(f"accumulated map : {len(merged)} entries (+{added}) -> {MAP_OUT}")
    print(f"run report      : {REPORT_OUT}")
    return 0


if __name__ == "__main__":
    sys.exit(main())

