"""Decode the shipped `WitchTrials.Models.MasterData` asset and every object it references, recursively.

    python tools/unity-rebuild/decode_master_data.py --write
    python tools/unity-rebuild/decode_master_data.py            # print the walk, write nothing
    python tools/unity-rebuild/decode_master_data.py --check    # fail when a decoded value changed
    python tools/unity-rebuild/decode_master_data.py --trace 1454

Why this exists
---------------
`GigaCreation.Essentials.MasterData.MasterDataProvider.LoadMasterData` (RVA 0xCD3D40) is

    Data = Resources.Load<MasterDataBase>("MasterData");

and `GigaCreation.Essentials.AddressablesUtils.AddressablesManager` feeds that object to Addressables:

    Addressables.LoadResourceLocationsAsync(masterData.AssetKeyNames, MergeMode.Union, null)
        (RVA 0xCC24D0, `MasterDataProvider.Data as IAddressablesMasterData` at 0x180CC2683; the
         `IEnumerable keys` / `MergeMode` / `Type` overload is RVA 0x1887290 and r8d == 1)

so the *asset key names the reconstructed Naninovel resource provider ends up caching* come from the shipped
master-data asset.  The asset is not Addressables content and the shipped catalog has no `MasterData` address:
it lived only in Unity `Resources` (`manosaba_Data/resources.assets`, path_id 1454, 80 bytes) and was never
imported into this project.  This tool is the evidence half of restoring it: it decodes that object and - as
the boot chain requires - *every object it references, recursively* (a master-data asset may point at further
master-data assets in the same file), with a reference target's class read from `res/_monoscripts.csv`.

Layout rules, honesty rules and the byte walker are inherited from `tools/unity-rebuild/decode_naninovel_configs.py`
(its module docstring documents them): player-build MonoBehaviours have `28-byte header + m_Name + fields`, base
class fields first, `string` = int32 length + UTF-8 + 4-byte alignment, arrays = int32 count + elements,
`[Serializable]` records inline recursively, `UnityEngine.Object` is a 12-byte PPtr, and a walk must consume the
object **exactly** - a non-closing walk is reported, never padded away.  This tool *imports* that module rather
than copying it, so the two decoders cannot disagree about a layout rule.

What is specific to this tool
-----------------------------
* The walk is a *closure*, not a single object: every PPtr that resolves inside `resources.assets` is followed
  (breadth-first, cycle-guarded, depth reported) and decoded too when it is a MonoBehaviour whose class has a C#
  declaration in this project.  A target that cannot be decoded is still reported as a row with the reason, so
  the closure is complete in the report even where it is not decodable.
* The root is established **from the code path, not from a class-name match**: the native literal at 0xCD3D60 is
  `"MasterData"`, and `ResourceManager.m_Container` (read through UnityPy) is the table Unity's `Resources.Load`
  consults, so the root is the object the container maps the key `masterdata` to (the container stores keys
  lowercased - the same convention `decode_naninovel_configs.py` measured for the Naninovel configurations).
  As an independent cross-check the tool also lists every MonoBehaviour in the file whose C# ancestry contains
  `GigaCreation.Essentials.MasterData.MasterDataBase` (i.e. every object that could ever be handed to
  `Addressables.LoadResourceLocationsAsync` as an `IAddressablesMasterData`) and reports whether that set agrees
  with the root.  Agreement is evidence; a disagreement is reported, not resolved silently.
* Every decoded asset key name is joined against the shipped Addressables catalog (`catalog_dump.json`, exact
  textual key match only) so the reader can see what the name resolves to - that join is what answers "does
  `AddressablesManager` end up with a non-empty `AssetKeyNames`, and does the catalog answer it".

Output
------
  tools/unity-rebuild/out/unity/master_data_decode.json   objects, values, per-field byte layout, references,
                                                          the key-name list, the catalog join, limitations

Exit codes: 0 = decoded (and, with --check, unchanged), 1 = a decoded value changed, 2 = the root could not be
established from the shipped build, 3 = UnityPy is missing from the interpreter.
"""
from __future__ import annotations

import argparse
import json
import os
import sys
from collections import OrderedDict

HERE = os.path.dirname(os.path.abspath(__file__))
if HERE not in sys.path:
    sys.path.insert(0, HERE)

import decode_naninovel_configs as D  # noqa: E402  (the sibling decoder: layout rules, walker, loaders)

REPO = D.REPO
OUT_DIR = D.OUT_DIR
OUT_JSON = os.path.join(OUT_DIR, "master_data_decode.json")
CATALOG_KEYS = os.path.join(OUT_DIR, "catalog_dump.json")

#: `WitchTrials.Models.MasterData` is the game's `IMasterData` + `IAddressablesMasterData` implementation
#: (dump.cs TypeDefIndex 12239: `class MasterData : MasterDataBase, IMasterData, IAddressablesMasterData,
#: IMasterDataBase`).  It is the only implementor in the shipped build (dump.cs lists exactly one class whose
#: base list contains `IAddressablesMasterData`), so `AssetKeyNames` can only come from an object of this type.
ENTRY_CLASS = "WitchTrials.Models.MasterData"
ENTRY_BASE = "GigaCreation.Essentials.MasterData.MasterDataBase"

#: The path literal of `MasterDataProvider.LoadMasterData` (RVA 0xCD3D40, ScriptString at 0x180CD3D60).
#: The second form is the ResourceManager container key: measured, the container stores the key lowercased
#: (`decode_naninovel_configs.py` measured `naninovel/configuration/<type>` for the same reason).
RESOURCES_KEY = "MasterData"
RESOURCES_CONTAINER_KEY = "masterdata"

#: `IAddressablesMasterData.AssetKeyNames` / `AdditionalCatalogDataList` field names of `MasterData`
#: (dump.cs: `_assetKeyNames // 0x18`, `_additionalCatalogDataList // 0x20`, `_defaultHp // 0x28`,
#: `_skipTimeScaleDuringDebate // 0x2C`, `_voicePitchOnSkip // 0x30`).
KEY_NAMES_FIELD = "_assetKeyNames"

CATALOG_JOIN_NOTE = ("catalog_dump.json is the shipped catalog's key table.  A key name is joined to it only on "
                     "an exact textual match of the key string; the catalog carries no path_id, so no join to "
                     "resources.assets is attempted or invented.")

MAX_DEPTH = 16


def load_catalog_keys():
    """The shipped catalog's key table as `key -> {location_count, is_label, sample internal_ids}`.

    Read from `catalog_dump.json`'s `keys` array, which the catalog dumper produced from the shipped
    `StreamingAssets/aa/catalog.bin`.  The addresses (a location's `internal_id`) are deliberately not indexed
    here: the join this tool needs is key-name -> key entry.
    """
    if not os.path.exists(CATALOG_KEYS):
        return {"available": False, "by_key": {}, "key_count": 0, "reason": "not found: " + CATALOG_KEYS}
    with open(CATALOG_KEYS, encoding="utf-8") as handle:
        data = json.load(handle)
    by_key = {}
    for entry in data.get("keys") or []:
        key = entry.get("key")
        if not isinstance(key, str):
            continue
        by_key.setdefault(key, {
            "key_type": entry.get("key_type"),
            "is_label": entry.get("is_label"),
            "looks_like_guid": entry.get("looks_like_guid"),
            "location_count": entry.get("location_count"),
            "locations_truncated": entry.get("locations_truncated"),
            "sample_internal_ids": list(entry.get("locations") or [])[:5],
        })
    return {"available": True, "by_key": by_key, "key_count": len(data.get("keys") or []),
            "catalog_source": data.get("catalog_source"), "catalog_sha256": data.get("catalog_sha256"),
            "location_count": data.get("location_count")}


def collect_pptrs(value, path, out):
    """Every PPtr in a decoded value tree, with the dotted path that reaches it (arrays and inlines included)."""
    if not isinstance(value, dict):
        return
    kind = value.get("kind")
    if kind == "pptr":
        out.append({"field": path, "m_FileID": value.get("m_FileID"), "m_PathID": value.get("m_PathID")})
    elif kind == "array":
        for index, item in enumerate(value.get("items") or []):
            collect_pptrs(item, f"{path}[{index}]", out)
    elif kind == "inline":
        for name, item in (value.get("fields") or {}).items():
            collect_pptrs(item, f"{path}.{name}", out)
    elif kind == "unity_event":
        for index, listener in enumerate(value.get("listeners") or []):
            collect_pptrs(listener.get("m_Target"), f"{path}.listeners[{index}].m_Target", out)
            collect_pptrs((listener.get("m_Arguments") or {}).get("m_ObjectArgument"),
                          f"{path}.listeners[{index}].m_Arguments.m_ObjectArgument", out)


def strings_of(value):
    """Every decoded `string` leaf in a value tree, in order, as plain text (wrapper-free)."""
    found = []
    if not isinstance(value, dict):
        return found
    kind = value.get("kind")
    if kind == "string":
        found.append(value.get("value"))
    elif kind == "array":
        for item in value.get("items") or []:
            found.extend(strings_of(item))
    elif kind == "inline":
        for item in (value.get("fields") or {}).values():
            found.extend(strings_of(item))
    return found


def ancestry(decl, resolver):
    """Full names from the top-most base down to `decl` (both ends included), via the decoder's own chain()."""
    rows, _, _ = resolver.chain(decl, {})
    names = [step.full_name for step, _ in rows]
    if decl.full_name not in names:
        names.append(decl.full_name)
    return names


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("--write", action="store_true", help="write the JSON output")
    parser.add_argument("--check", action="store_true",
                        help="compare with the committed output and fail when a decoded value changed")
    parser.add_argument("--assets", default=None, help="override the resources.assets path")
    parser.add_argument("--root", type=int, default=None,
                        help="decode this path_id instead of resolving the root through the Resources container")
    parser.add_argument("--trace", default="", metavar="PATH_IDS",
                        help="comma-separated path_ids; print the field-by-field byte layout of those objects")
    parser.add_argument("--no-catalog", action="store_true",
                        help="skip the shipped-catalog join of the decoded asset key names")
    parser.add_argument("--verbose", action="store_true")
    args = parser.parse_args()

    if D.UnityPy is None:
        print("UnityPy is not available in this interpreter: " + sys.executable)
        return 3

    assets = args.assets or os.path.join(D.game_root(), "manosaba_Data", "resources.assets")
    index = D.SourceIndex(D.DEFAULT_DEFINED, verbose=args.verbose)
    resolver = D.KindResolver(index)
    monoscripts = D.load_monoscripts()

    env = D.UnityPy.load(assets)
    objects = {int(obj.path_id): obj for obj in env.objects}
    raws = {path_id: obj.get_raw_data() for path_id, obj in objects.items()}
    types = {path_id: obj.type.name for path_id, obj in objects.items()}

    def class_of(path_id):
        """The class of an object, from its own m_Script (so the namespace survives) through _monoscripts.csv."""
        if path_id not in objects or types[path_id] != "MonoBehaviour":
            return None
        raw = raws[path_id]
        if len(raw) < D.HEADER_BYTES:
            return None
        _, script_path_id = struct_unpack_script(raw)
        return monoscripts.get(script_path_id)

    def name_of(path_id):
        if path_id not in objects:
            return None
        raw = raws[path_id]
        offset = D.HEADER_BYTES if types[path_id] == "MonoBehaviour" else 0
        if offset + 4 > len(raw):
            return None
        from struct import unpack_from
        (length,) = unpack_from("<i", raw, offset)
        if length < 0 or length > 512 or offset + 4 + length > len(raw):
            return None
        try:
            return raw[offset + 4:offset + 4 + length].decode("utf-8")
        except UnicodeDecodeError:
            return None

    # -- root selection -------------------------------------------------------------------------------------
    container = D.load_resources_container(assets, objects)
    container_path_id = None
    for path_id, key in container["by_path_id"].items():
        if key == RESOURCES_CONTAINER_KEY:
            container_path_id = path_id
            break

    # independent cross-check: every object in this file that could be an IAddressablesMasterData
    master_data_objects = []
    for path_id in sorted(raws):
        class_name = class_of(path_id)
        if class_name is None:
            continue
        decl = index.resolve(class_name)
        if decl is None:
            continue
        if ENTRY_BASE in ancestry(decl, resolver):
            master_data_objects.append({"path_id": path_id, "class": class_name, "bytes": len(raws[path_id]),
                                        "m_Name": name_of(path_id),
                                        "ancestry": ancestry(decl, resolver)})

    root_path_id = args.root if args.root is not None else container_path_id
    root_source = "command line --root" if args.root is not None else \
        ("ResourceManager.m_Container key '" + RESOURCES_CONTAINER_KEY + "'" if container_path_id is not None
         else "UNRESOLVED")
    if root_path_id is None and len(master_data_objects) == 1:
        root_path_id = master_data_objects[0]["path_id"]
        root_source = "the single object whose ancestry contains " + ENTRY_BASE
    if root_path_id is None:
        print("BLOCKED: no root.  The Resources container key '" + RESOURCES_CONTAINER_KEY + "' is absent and "
              + str(len(master_data_objects)) + " candidate master-data object(s) exist.")
        return 2

    catalog = {"available": False, "by_key": {}, "key_count": 0, "reason": "skipped (--no-catalog)"} \
        if args.no_catalog else load_catalog_keys()
    trace_ids = {int(part) for part in args.trace.split(",") if part.strip().isdigit()} if args.trace else set()

    # -- the reference closure ------------------------------------------------------------------------------
    decoded = OrderedDict()          # path_id -> row (in discovery order)
    queue = [(root_path_id, 0, None, None)]
    seen = set()
    edge_rows = []

    while queue:
        path_id, depth, from_path_id, from_field = queue.pop(0)
        if path_id in seen:
            continue
        seen.add(path_id)
        row = OrderedDict()
        row["path_id"] = path_id
        row["depth"] = depth
        row["discovered_from"] = None if from_path_id is None else {"path_id": from_path_id, "field": from_field}
        row["unity_type"] = types.get(path_id)
        row["bytes_total"] = len(raws.get(path_id, b""))
        raw = raws.get(path_id)
        if raw is None:
            row.update({"status": "MISSING_IN_FILE", "class": None, "values": {}, "unparsed": [
                {"field": None, "field_type": None, "owner": None, "offset": None,
                 "reason": "the PPtr resolved to a path_id that is not in this serialized file"}],
                "references": [], "references_found": 0})
            decoded[path_id] = row
            continue

        class_name = class_of(path_id)
        row["class"] = class_name
        row["m_Script"] = None
        if types.get(path_id) == "MonoBehaviour" and len(raw) >= D.HEADER_BYTES:
            _, script_path_id = struct_unpack_script(raw)
            row["m_Script"] = script_path_id
        row["m_Name"] = name_of(path_id)
        row["resources_path"] = container["by_path_id"].get(path_id)

        if class_name is None:
            row.update({"status": "NOT_DECODED", "values": {}, "unparsed": [], "references": [],
                        "references_found": 0,
                        "reason": ("not a MonoBehaviour with a m_Script entry in res/_monoscripts.csv"
                                   if types.get(path_id) != "MonoBehaviour"
                                   else "the MonoBehaviour's m_Script is not in res/_monoscripts.csv")})
            decoded[path_id] = row
            continue

        decl = index.resolve(class_name)
        if decl is None:
            row.update({"status": "NO_SOURCE", "values": {}, "unparsed": [], "references": [],
                        "references_found": 0,
                        "reason": "no C# declaration found for " + class_name})
            decoded[path_id] = row
            continue

        row["source"] = os.path.relpath(decl.file, REPO).replace("\\", "/")
        row["source_line"] = decl.line
        kind = resolver.kinds(class_name, None, {}, as_object_reference=False)
        if kind["kind"] != "inline":
            row.update({"status": "NOT_INLINE", "values": {}, "unparsed": [], "references": [],
                        "references_found": 0,
                        "reason": f"class resolved to kind {kind['kind']!r}, expected an inlinable class: "
                                  f"{kind.get('reason') or kind.get('why')}"})
            decoded[path_id] = row
            continue

        row["object_base"] = kind.get("object_base")
        row["fields"] = [{"name": field["name"], "type": field["type"], "owner": field.get("owner"),
                          "line": field.get("line")} for field in kind["fields"]]
        row["declared_unsupported"] = kind.get("declared_unsupported") or []
        needs_registry = D.kind_tree_has_managed_reference(kind["fields"])
        trace = []
        values, offset, unparsed = D.walk_object(raw, kind["fields"], trace, registry_wanted=needs_registry)
        strict_offset = offset - (values.get("_managedReferences", {}).get("bytes", 0)
                                  if isinstance(values, dict) else 0)
        row["values"] = values
        row["unparsed"] = unparsed
        row["field_layout"] = trace
        row["bytes_consumed"] = offset
        row["bytes_consumed_strict"] = strict_offset
        row["closes_exactly"] = strict_offset == len(raw)
        row["closes_with_registry"] = offset == len(raw)
        if unparsed:
            row["status"] = "UNPARSED_FIELDS"
        elif offset > len(raw):
            row["status"] = "OVERRUN"
        elif strict_offset == len(raw):
            row["status"] = "OK"
        elif row["closes_with_registry"]:
            row["status"] = "OK_TRAILING_REGISTRY"
            row["trailing_registry_bytes"] = len(raw) - strict_offset
        else:
            row["status"] = "TAIL"
            row["unparsed_tail_bytes"] = len(raw) - offset

        if path_id in trace_ids:
            print(f"  --- layout of {class_name} (path_id {path_id}, {len(raw)} bytes) ---")
            for item in trace:
                where = f"0x{item['offset']:04x}" if item.get("offset") is not None else "     "
                span = f"..0x{item['end']:04x}" if item.get("end") is not None else "..  ?  "
                print(f"    {where}{span} {str(item.get('bytes', '')):>5}B  {item['field']} : {item['type']}"
                      + (f"  <-- {item['reason']}" if item.get("reason") else ""))
            print(f"    walk ends at {strict_offset} of {len(raw)} bytes -> "
                  f"{'CLOSES EXACTLY' if row['closes_exactly'] else row['status']}")

        # references: resolved in-file (and queued), or reported as unbound
        pptrs = []
        for name, value in values.items():
            collect_pptrs(value, name, pptrs)
        for pptr in pptrs:
            file_id, target = pptr["m_FileID"], pptr["m_PathID"]
            resolves = file_id == 0 and target in objects
            edge = OrderedDict(pptr)
            edge["from_path_id"] = path_id
            edge["resolves_in_file"] = resolves
            edge["target_class"] = class_of(target) if resolves else None
            edge["target_name"] = name_of(target) if resolves else None
            edge["target_unity_type"] = types.get(target) if resolves else None
            edge["resources_path"] = container["by_path_id"].get(target) if file_id == 0 else None
            if resolves:
                edge_rows.append(edge)
                if depth + 1 <= MAX_DEPTH:
                    queue.append((target, depth + 1, path_id, pptr["field"]))
                else:
                    edge["note"] = f"not followed: depth limit {MAX_DEPTH} reached"
            else:
                edge["note"] = ("m_FileID " + str(file_id) + " is another serialized file; no mapping into it "
                                "exists in this report") if file_id != 0 else \
                    "m_PathID " + str(target) + " is not an object of this serialized file"
            row.setdefault("references", []).append(edge)
        row["references_found"] = len(row.get("references") or [])
        decoded[path_id] = row

    # -- the asset key names --------------------------------------------------------------------------------
    key_names = None
    key_row = None
    if root_path_id in decoded and isinstance(decoded[root_path_id].get("values"), dict):
        for name, value in decoded[root_path_id]["values"].items():
            if name == KEY_NAMES_FIELD:
                key_row = value
                key_names = strings_of(value)
    catalog_join = None
    if key_names is not None:
        entries = []
        for name in key_names:
            entry = {"key": name}
            if catalog["available"]:
                found = catalog["by_key"].get(name)
                if found is None:
                    entry.update({"in_shipped_catalog": False,
                                  "note": "no key of this name exists in the shipped catalog's key table"})
                else:
                    entry.update({"in_shipped_catalog": True})
                    entry.update(found)
            entries.append(entry)
        catalog_join = {"source": "tools/unity-rebuild/out/unity/catalog_dump.json",
                        "join_rule": CATALOG_JOIN_NOTE,
                        "catalog_available": catalog["available"],
                        "catalog_key_count": catalog.get("key_count"),
                        "catalog_location_count": catalog.get("location_count"),
                        "entries": entries}

    statuses = {}
    for row in decoded.values():
        statuses[row["status"]] = statuses.get(row["status"], 0) + 1
    total_values = 0
    for row in decoded.values():
        if isinstance(row.get("values"), dict):
            total_values += sum(count_nodes(value) for value in row["values"].values())

    document = OrderedDict()
    document["generated_by"] = "tools/unity-rebuild/decode_master_data.py"
    document["assets_file"] = assets
    document["assets_bytes"] = os.path.getsize(assets) if os.path.exists(assets) else None
    document["source_files_scanned"] = index.files_scanned
    document["entry"] = OrderedDict([
        ("class", ENTRY_CLASS),
        ("base", ENTRY_BASE),
        ("native_call", "GigaCreation.Essentials.MasterData.MasterDataProvider.LoadMasterData (RVA 0xCD3D40): "
                        "Data = Resources.Load<MasterDataBase>(\"" + RESOURCES_KEY + "\")"),
        ("resources_key", RESOURCES_KEY),
        ("resources_container_key", RESOURCES_CONTAINER_KEY),
        ("resources_container", {"file_id": container["file_id"], "entries": container["entries"],
                                 "matched_path_ids": container["matched"], "candidates": container["candidates"]}),
        ("root_path_id", root_path_id),
        ("root_resolved_by", root_source),
        ("root_container_path_id", container_path_id),
        ("container_agrees_with_scan", container_path_id is not None and len(master_data_objects) == 1
         and master_data_objects[0]["path_id"] == container_path_id),
        ("master_data_objects_in_file", master_data_objects),
        ("scan_note", "every MonoBehaviour in resources.assets whose C# ancestry contains " + ENTRY_BASE +
                      " is listed above, independently of the Resources container; it answers 'is the asset key "
                      "list perhaps supplied by a *different* object'."),
    ])
    document["summary"] = OrderedDict([
        ("objects", len(decoded)),
        ("objects_closing_exactly", sum(1 for row in decoded.values() if row.get("closes_exactly"))),
        ("statuses", dict(sorted(statuses.items()))),
        ("values_total", total_values),
        ("reference_edges", len(edge_rows)),
        ("key_names_found", key_names is not None),
        ("key_name_count", None if key_names is None else len(key_names)),
        ("catalog_available", catalog["available"]),
    ])
    document["asset_key_names"] = OrderedDict([
        ("field", KEY_NAMES_FIELD),
        ("found", key_names is not None),
        ("count", None if key_names is None else len(key_names)),
        ("names", key_names),
        ("decoded_node", key_row),
        ("catalog_join", catalog_join),
    ])
    document["enter_play_question"] = build_answer(document, root_path_id,
                                                   decoded[root_path_id].get("class"))
    document["objects"] = list(decoded.values())
    document["reference_edges"] = edge_rows
    document["limitations"] = build_limitations(document, container, catalog)

    print(f"resources.assets: {assets}")
    print(f"root: path_id {root_path_id} ({root_source}), class "
          f"{decoded[root_path_id].get('class')}")
    print(f"objects decoded: {len(decoded)}  statuses: {dict(sorted(statuses.items()))}")
    print(f"reference edges: {len(edge_rows)}")
    if key_names is None:
        print("asset key names: NOT FOUND in " + KEY_NAMES_FIELD)
    else:
        print(f"asset key names: {len(key_names)} in {KEY_NAMES_FIELD} -> {key_names!r}")
        if catalog_join:
            for entry in catalog_join["entries"]:
                if entry.get("in_shipped_catalog"):
                    print(f"  catalog key {entry['key']!r}: location_count={entry.get('location_count')} "
                          f"is_label={entry.get('is_label')}")
                else:
                    print(f"  catalog key {entry['key']!r}: ABSENT from the shipped catalog key table")
    for path_id, row in decoded.items():
        detail = ""
        if row.get("unparsed"):
            first = row["unparsed"][0]
            detail = f"  [{first['reason']}]"
        if row.get("reason"):
            detail = "  " + row["reason"]
        print(f"  {row['status']:<12} path_id {path_id:>6} depth {row['depth']}  "
              f"{str(row.get('class')):<45} {row['bytes_total']:>5} B{detail}")

    if args.write:
        os.makedirs(OUT_DIR, exist_ok=True)
        with open(OUT_JSON, "w", encoding="utf-8", newline="\n") as handle:
            json.dump(document, handle, ensure_ascii=False, indent=1)
            handle.write("\n")
        print("wrote " + OUT_JSON)

    if args.check:
        return check_against_previous(document)
    return 0


def struct_unpack_script(raw):
    """The `PPtr<MonoScript>` of a player-build MonoBehaviour header, at the decoder's measured offset."""
    from struct import unpack_from
    return unpack_from("<iq", raw, D.SCRIPT_OFFSET)


def count_nodes(token):
    """How many decoded value nodes a subtree holds (containers included) - the decoder's own accounting rule."""
    if not isinstance(token, dict):
        return 0
    count = 1
    kind = token.get("kind")
    if kind == "array":
        for item in token.get("items") or []:
            count += count_nodes(item)
    elif kind == "inline":
        for item in (token.get("fields") or {}).values():
            count += count_nodes(item)
    return count


def build_answer(document, root_path_id, root_class):
    """The one-sentence answer the boot chain asks for, plus the numbers behind it (no interpretation added)."""
    key_block = document["asset_key_names"]
    join = key_block.get("catalog_join")
    answer = OrderedDict()
    if not key_block["found"]:
        answer["asset_key_names_non_empty"] = None
        answer["statement"] = ("the decoded root object has no " + KEY_NAMES_FIELD + " value, so this tool cannot "
                               "say whether AddressablesManager receives a non-empty AssetKeyNames")
        return answer
    names = key_block["names"] or []
    answer["asset_key_names_non_empty"] = bool(names)
    answer["supplied_by"] = (f"resources.assets path_id {root_path_id} ({root_class}), "
                             f"reached by Resources.Load(\"{RESOURCES_KEY}\") in "
                             f"MasterDataProvider.LoadMasterData (RVA 0xCD3D40)")
    answer["key_names"] = names
    if join and join.get("catalog_available"):
        located = [(entry["key"], entry.get("location_count")) for entry in join["entries"]
                   if entry.get("in_shipped_catalog")]
        answer["catalog_keys_found"] = [key for key, _ in located]
        answer["catalog_location_counts"] = {key: count for key, count in located}
        answer["statement"] = (f"yes: AssetKeyNames is {names!r}, supplied by the master-data asset at "
                               f"resources.assets path_id {root_path_id} (Resources.Load(\"{RESOURCES_KEY}\")), "
                               f"and the shipped catalog answers " +
                               (", ".join(f"{key!r} with {count} location(s)" for key, count in located)
                                if located else "none of those key names") +
                               ". AddressablesManager therefore hands the resource provider " +
                               (f"{located[0][1]} locations" if located else "an empty location list") + ".")
    else:
        answer["statement"] = (f"yes: AssetKeyNames is {names!r}, supplied by the master-data asset at "
                               f"resources.assets path_id {root_path_id}. The shipped-catalog join was skipped.")
    return answer


def build_limitations(document, container, catalog):
    limitations = []
    limitations.append("LAYOUT SOURCE: every field layout came from this project's own C# sources "
                       "(WitchTrials.Models.MasterData, GigaCreation.Essentials.AddressablesUtils."
                       "AdditionalCatalogData, GigaCreation.Essentials.MasterData.MasterDataBase -> "
                       "UnityEngine.ScriptableObject) through the byte walker of "
                       "tools/unity-rebuild/decode_naninovel_configs.py. The walk is only accepted when it "
                       "consumes the object exactly; that is stated per object by `closes_exactly`, and a "
                       "non-closing walk would be reported as such rather than padded away.")
    limitations.append("CLOSURE: the walk follows every PPtr that resolves inside resources.assets and decodes "
                       "the target when it is a MonoBehaviour with a C# declaration ("
                       + str(document["summary"]["objects"]) + " object(s) decoded, "
                       + str(document["summary"]["reference_edges"]) + " reference edge(s)). A target in another "
                       "serialized file (m_FileID != 0) or one whose m_Script is not in res/_monoscripts.csv is "
                       "reported as a row with that reason; nothing is dropped silently and nothing is guessed.")
    limitations.append("ROOT: the root is the object the ResourceManager container maps '" +
                       RESOURCES_CONTAINER_KEY + "' to, because that is the table "
                       "Resources.Load consults and the native literal at 0xCD3D60 is '" + RESOURCES_KEY + "'. "
                       "The container stores keys lowercased (measured earlier for the Naninovel configurations: "
                       "'naninovel/configuration/<name>'). The independent ancestry scan in `entry` lists every "
                       "object in this file that could ever be passed to Addressables as an "
                       "IAddressablesMasterData.")
    if catalog["available"]:
        limitations.append("CATALOG JOIN: the shipped catalog key table has " + str(catalog.get("key_count")) +
                           " keys; a decoded asset key name is joined to it on an exact textual match only. The "
                           "catalog carries no path_id, so no join between the catalog and resources.assets is "
                           "attempted (tools/unity-rebuild/out/unity/catalog_pathid_report.md). `is_label` in that "
                           "dump is unreliable - it reports labels = ['default'] and no label keys, yet a label "
                           "must itself appear in the key table - so a key that aggregates thousands of locations "
                           "is reported by its location_count rather than by that flag.")
    else:
        limitations.append("CATALOG JOIN: skipped or unavailable (" + str(catalog.get("reason")) +
                           "), so this report does not say what the decoded key names resolve to.")
    limitations.append("NOT VERIFIED WITHOUT UNITY: that Unity's Resources.Load(\"" + RESOURCES_KEY + "\") now "
                       "finds an asset, that AddressablesManager therefore stops logging 'The provided master "
                       "data does not include the data for Addressable Asset System.', and that the reconstructed "
                       "resource provider caches a non-empty location list. Those are runtime facts; this tool "
                       "only establishes what the shipped object contained and what the shipped catalog offers.")
    limitations.append("SCOPE: `AddressablesManager`'s own async state machines are outside this tool. Note for "
                       "the reader, measured rather than assumed: `<LoadResourceLocationsAsync>d__25.MoveNext` "
                       "(RVA 0xCC24D0) passes `masterData.AssetKeyNames` as `IEnumerable keys` with `MergeMode` "
                       "value 1 and a null `Type` (dump.cs renders Addressables.MergeMode as None = 0, "
                       "UseFirst = 0, Union = 1, Intersection = 2, so the constant is Union), and its "
                       "null-master-data branch logs and returns an empty List<IResourceLocation>.")
    return limitations


def check_against_previous(document):
    """Fail (exit 1) when a value decoded before is no longer equal."""
    if not os.path.exists(OUT_JSON):
        print("--check: no previous output at " + OUT_JSON)
        return 1
    with open(OUT_JSON, encoding="utf-8") as handle:
        previous = json.load(handle)
    before = {row["path_id"]: row for row in previous.get("objects", [])}
    after = {row["path_id"]: row for row in document["objects"]}
    problems = []
    if set(before) != set(after):
        problems.append("object set changed: " + str(sorted(set(before) ^ set(after))))
    for path_id in sorted(set(before) & set(after)):
        if before[path_id].get("values") != after[path_id].get("values"):
            problems.append(f"path_id {path_id}: decoded values changed")
    if previous.get("asset_key_names", {}).get("names") != document["asset_key_names"]["names"]:
        problems.append("the decoded asset key names changed")
    if problems:
        for problem in problems:
            print("--check FAILED: " + problem)
        return 1
    print("--check OK: every decoded value is unchanged")
    return 0


if __name__ == "__main__":
    sys.exit(main())
