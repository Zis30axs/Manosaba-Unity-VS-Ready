"""Decode the serialized field values of the shipped boot scene's MonoBehaviours.

    python tools/unity-rebuild/decode_level0_component_values.py [--write]

`level0` has no type tree, so its MonoBehaviours are opaque to UnityPy - but the layout is not a secret: the
reconstructed C# in `UnityProject/Assets/Scripts` declares the very fields, in the very order Unity serializes
them, and the header (PPtr m_GameObject, m_Enabled, PPtr m_Script) is a fixed 28 bytes.  So the values can be
walked out of the bytes and then *checked*:

  * the walk must consume the object exactly (a wrong type mapping shows up as a misaligned, non-closing walk);
  * every PPtr must resolve to an object in the same file;
  * a PPtr's target class is read from the target's own m_Script (via the shipped MonoScript table), so
    "_wholeScreenButton" can be proven to point at a Button rather than assumed to.

Anything that does not pass those checks is reported as unparsed instead of guessed - the point is evidence for
the scene rebuild, not a plausible-looking field dump.

Output: tools/unity-rebuild/out/unity/level0_component_values.json
"""
from __future__ import annotations

import argparse
import csv
import io
import json
import os
import re
import struct
import sys
from collections import OrderedDict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "level0_component_values.json")
INVENTORY = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "level0_scene_inventory.json")
MONOSCRIPTS = os.path.join(REPO, "res", "_monoscripts.csv")
# Measured on this build: PPtr<GameObject> (12) + m_Enabled (4) + PPtr<MonoScript> (12) = 28 bytes, then the
# object's own `m_Name` string, then the script's fields in declaration order.
#
# The m_Name is the part that fooled the first version of this tool: for the *scene* components it is empty, so
# its 4-byte length prefix looked like 4 bytes of padding and a 32-byte header closed every walk exactly.  The
# shipped `Naninovel.EngineVersion` asset (resources.assets, path_id 1486, 76 bytes) shows the real shape:
# `... m_Script | 0d 00 00 00 "EngineVersion" | 04 00 00 00 "1.20" | 00 00 00 00 | 0a 00 00 00 "2025-04-26"`.
# The name is therefore consumed explicitly below, so a component whose name is not empty cannot silently shift
# every following field.
HEADER_BYTES = 28

try:
    import UnityPy
except ImportError:
    print("UnityPy is not available in this interpreter: " + sys.executable)
    raise SystemExit(3)

# Serialized size of the value kinds this build actually uses.  PPtr = int32 m_FileID + int64 m_PathID.
SCALARS = {
    "float": ("f32", 4), "double": ("f64", 8), "int": ("i32", 4), "uint": ("u32", 4),
    "short": ("i16", 2), "ushort": ("u16", 2), "byte": ("u8", 1), "sbyte": ("i8", 1),
    "long": ("i64", 8), "ulong": ("u64", 8), "bool": ("u8", 1),
    "Color": ("color", 16), "Color32": ("color32", 4),
    "Vector2": ("vec2", 8), "Vector3": ("vec3", 12), "Vector4": ("vec4", 16),
    "Quaternion": ("quat", 16), "Rect": ("rect", 16), "LayerMask": ("i32", 4),
}
FIELD_RE = re.compile(
    r"^\s*(?:\[[^\]]*\]\s*)*(?:public|private|protected|internal)\s+(?:readonly\s+)?"
    r"([A-Za-z_][A-Za-z0-9_.<>,\[\]]*)\s+([A-Za-z_][A-Za-z0-9_]*)\s*(?:=[^;]*)?;")


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def load_monoscripts():
    """path_id -> fully qualified class name.

    The namespace matters: "SplashScreen" or "Button" alone cannot be resolved back to a type when the scene is
    rebuilt, and a reference that cannot be resolved is a silently missing link.
    """
    table = {}
    with io.open(MONOSCRIPTS, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            try:
                path_id = int(row["path_id"])
            except (KeyError, ValueError):
                continue
            namespace = (row.get("namespace") or "").strip()
            class_name = (row.get("class") or "").strip()
            table[path_id] = (namespace + "." + class_name) if namespace else class_name
    return table


def script_index():
    """(class full name -> reconstructed source path, enum simple names) from the script index."""
    with io.open(os.path.join(REPO, "tools", "unity-rebuild", "out", "unity",
                              "reconstructed_script_index.json"), encoding="utf-8") as fh:
        index = json.load(fh)
    mapping, enums = {}, set()
    for entry in index["entries"]:
        namespace = entry.get("namespace") or ""
        for type_row in entry["types"]:
            name = type_row["name"]
            full = (namespace + "." + name) if namespace else name
            mapping[full] = entry["asset_path"]
            if type_row.get("kind") == "enum":
                enums.add(name)
                enums.add(full)
    return mapping, enums


def source_file(asset_path):
    """The reconstructed script on disk: the index records Unity-project-relative paths."""
    for prefix in ("UnityProject", ""):
        candidate = os.path.join(REPO, prefix, asset_path) if prefix else os.path.join(REPO, asset_path)
        if os.path.exists(candidate):
            return candidate
    return None


def serialized_fields(source_path, class_name, enum_names):
    """The class's own serialized fields, in declaration order, as (name, kind) rows.

    Unity serializes declared *instance* fields in source order, so the reconstructed source gives the order -
    but only if the compiler-generated parts are excluded, which is what the depth tracking is for: nested
    async-state-machine structs, `static`/`const`/`readonly` fields and properties are not serialized, and
    including any of them shifts every following field (the walk then stops closing exactly, which is how this
    was caught).
    """
    with io.open(source_path, encoding="utf-8-sig", errors="replace") as fh:
        lines = fh.read().splitlines()

    simple = class_name.rsplit(".", 1)[-1]
    declaration = re.compile(r"^\s*(public|internal|private|protected)?\s*(sealed\s+|abstract\s+|static\s+)*"
                             r"(class|struct|enum|interface)\s+" + re.escape(simple) + r"\b")
    start = next((number for number, line in enumerate(lines) if declaration.match(line)), None)
    if start is None:
        return None, "class declaration not found in " + source_path

    fields, unsupported = [], []
    depth = lines[start].count("{") - lines[start].count("}")
    number = start + 1
    while number < len(lines):
        line = lines[number]
        stripped = line.strip()

        # A nested type at class-body level (async state machines, display classes): skip its whole body.
        if depth <= 1 and re.match(r"^(public|internal|private|protected)?\s*(sealed\s+|abstract\s+|static\s+)*"
                                   r"(class|struct|enum|interface)\s", stripped):
            nested = 0
            while number < len(lines):
                nested += lines[number].count("{") - lines[number].count("}")
                number += 1
                if nested <= 0:
                    break
            continue

        depth += line.count("{") - line.count("}")
        if depth <= 0:
            break   # the class body closed
        if depth != 1:
            number += 1
            continue

        match = FIELD_RE.match(line)
        if match:
            field_type, field_name = match.group(1), match.group(2)
            declaration_text = re.sub(r"\[[^\]]*\]", "", line)
            if re.search(r"\b(static|const|readonly)\b", declaration_text) or field_name.startswith("<"):
                number += 1
                continue    # not serialized

            # Unity's rules decide what is in the object: a public field always is, a private/protected one only
            # with [SerializeField], and [NonSerialized] excludes it.  Ignoring this was the reason SplashScreen's
            # walk ran past the end: its `_inputManager`/`_overlayFader` are private interface fields that Unity
            # never writes.
            attributes = []
            lookback = number - 1
            while lookback > start and re.match(r"^\s*\[", lines[lookback]):
                attributes.append(lines[lookback])
                lookback -= 1
            attribute_text = " ".join(attributes)
            if "NonSerialized" in attribute_text:
                number += 1
                continue
            if "public" not in declaration_text and "SerializeField" not in attribute_text:
                number += 1
                continue

            kind = serialized_kind(field_type, enum_names)
            if kind is None:
                unsupported.append({"name": field_name, "type": field_type})
            else:
                fields.append({"name": field_name, "type": field_type, "kind": kind})
        number += 1
    return (fields, unsupported), None


def serialized_kind(field_type, enum_names):
    """Map a C# field type onto a serialized kind, or None when it cannot be walked safely."""
    field_type = field_type.replace(" ", "")
    if field_type in SCALARS:
        return {"kind": SCALARS[field_type][0], "size": SCALARS[field_type][1]}
    if field_type in ("string", "System.String"):
        return {"kind": "string", "size": 0}
    if field_type in enum_names or field_type.rsplit(".", 1)[-1] in enum_names:
        return {"kind": "i32", "size": 4}   # enums serialize as their underlying int
    if "AssetReference" in field_type:
        # Addressables' AssetReference is a [Serializable] class serialized inline:
        # string m_AssetGUID, string m_SubObjectName, string m_SubObjectType.
        return {"kind": "asset_reference", "size": 0}
    element = None
    if field_type.endswith("[]"):
        element = field_type[:-2]
    elif field_type.startswith("List<") and field_type.endswith(">"):
        element = field_type[5:-1]
    if element is not None:
        element_kind = serialized_kind(element, enum_names)
        if element_kind is None:
            return None
        return {"kind": "array", "element": element_kind}
    return {"kind": "pptr", "size": 12}


def walk_asset_reference(raw, offset):
    """Addressables' AssetReference: three consecutive strings, in the order the class serializes them."""
    parts, cursor = {}, offset
    for name in ("m_AssetGUID", "m_SubObjectName", "m_SubObjectType"):
        value, cursor, error = walk_value(raw, cursor, {"kind": "string", "size": 0})
        if error:
            return None, cursor, error
        parts[name] = value["value"]
    return {"kind": "asset_reference", **parts}, cursor, None


def walk(raw, fields):
    """Consume the object's bytes field by field.  Returns (values, offset, error)."""
    offset = HEADER_BYTES
    values = OrderedDict()

    # m_Name comes before the script's fields; empty for the scene's components, but it must still be consumed
    # rather than assumed away.
    name, offset, error = walk_value(raw, offset, {"kind": "string", "size": 0})
    if error:
        return values, offset, f"m_Name: {error}"
    if name["value"]:
        values["m_Name"] = name

    for field in fields:
        kind, size = field["kind"]["kind"], field["kind"].get("size", 0)
        if offset + size > len(raw):
            return values, offset, f"ran past the end of the object at '{field['name']}'"
        if kind == "array":
            (count,) = struct.unpack_from("<i", raw, offset)
            element = field["kind"]["element"]
            if count < 0 or count > 100000:
                return values, offset, f"implausible array length {count} at '{field['name']}'"
            items, cursor = [], offset + 4
            for _ in range(count):
                item, cursor, nested_error = walk_value(raw, cursor, element)
                if nested_error:
                    return values, cursor, f"{nested_error} (element of '{field['name']}')"
                items.append(item)
            values[field["name"]] = {"kind": "array", "count": count, "items": items}
            offset = cursor
            continue
        if kind == "asset_reference":
            reference, cursor, error = walk_asset_reference(raw, offset)
            if error:
                return values, cursor, f"{error} at '{field['name']}'"
            values[field["name"]] = reference
            offset = cursor
            continue
        value, offset, error = walk_value(raw, offset, field["kind"])
        if error:
            return values, offset, f"{error} at '{field['name']}'"
        values[field["name"]] = value
    return values, offset, None


def walk_value(raw, offset, kind_row):
    """One value; returns (value, new_offset, error)."""
    kind, size = kind_row["kind"], kind_row.get("size", 0)
    if offset + size > len(raw):
        return None, offset, "ran past the end of the object"
    if kind == "pptr":
        file_id, path_id = struct.unpack_from("<iq", raw, offset)
        return {"kind": "component_ref", "m_FileID": file_id, "m_PathID": path_id}, offset + 12, None
    if kind == "string":
        (length,) = struct.unpack_from("<i", raw, offset)
        if length < 0 or offset + 4 + length > len(raw):
            return None, offset, f"implausible string length {length}"
        text = raw[offset + 4:offset + 4 + length].decode("utf-8", "replace")
        end = offset + 4 + length
        return {"kind": "string", "value": text}, end + ((4 - end % 4) % 4), None
    if kind == "f32":
        return {"kind": "float", "value": round(struct.unpack_from("<f", raw, offset)[0], 6)}, offset + 4, None
    if kind == "f64":
        return {"kind": "double", "value": struct.unpack_from("<d", raw, offset)[0]}, offset + 8, None
    if kind in ("i32", "u32", "i16", "u16", "u8", "i8", "i64", "u64"):
        fmt = {"i32": "<i", "u32": "<I", "i16": "<h", "u16": "<H", "u8": "<B", "i8": "<b",
               "i64": "<q", "u64": "<Q"}[kind]
        return {"kind": "int", "value": struct.unpack_from(fmt, raw, offset)[0]}, offset + size, None
    if kind in ("color", "color32", "vec2", "vec3", "vec4", "quat", "rect"):
        count = {"color": 4, "color32": 4, "vec2": 2, "vec3": 3, "vec4": 4, "quat": 4, "rect": 4}[kind]
        fmt = "<B" if kind == "color32" else "<f"
        step = 1 if kind == "color32" else 4
        return ({"kind": kind, "value": [struct.unpack_from(fmt, raw, offset + index * step)[0]
                                         for index in range(count)]}, offset + size, None)
    return None, offset, f"unsupported kind '{kind}'"


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    scene = os.path.join(game_root(), "manosaba_Data", "level0")
    env = UnityPy.load(scene)
    objects = {int(obj.path_id): obj for obj in env.objects}
    monoscripts = load_monoscripts()
    index, enum_names = script_index()

    with io.open(INVENTORY, encoding="utf-8") as fh:
        inventory = json.load(fh)

    # For every PPtr field, the target's class comes from the target's own m_Script - that is what turns
    # "component_ref 117" into "a SteamManager".
    def class_of_object(path_id):
        target = objects.get(path_id)
        if target is None:
            return None
        if target.type.name != "MonoBehaviour":
            return target.type.name
        raw = target.get_raw_data()
        if len(raw) < HEADER_BYTES:
            return None
        _, script_path_id = struct.unpack_from("<iq", raw, 16)
        return monoscripts.get(script_path_id, "MonoBehaviour")

    doc = {"scene": scene, "classes": [], "unparsed": []}
    for row in inventory["objects"]:
        if row["type"] != "MonoBehaviour" or not row.get("script"):
            continue
        class_name = row["script"]
        source_path = index.get(class_name)
        entry = {"path_id": row["path_id"], "class": class_name, "asset_path": source_path}
        resolved = source_file(source_path) if source_path else None
        if resolved is None:
            entry["status"] = "NO_SOURCE"
            doc["unparsed"].append(entry)
            continue
        source_path = resolved
        parsed, error = serialized_fields(source_path, class_name, enum_names)
        if error:
            entry["status"] = "NO_CLASS"
            entry["error"] = error
            doc["unparsed"].append(entry)
            continue
        fields, unsupported = parsed
        raw = objects[row["path_id"]].get_raw_data()
        values, offset, walk_error = walk(raw, fields)
        entry.update({
            "fields": fields,
            "declared_unsupported": unsupported,
            "values": values,
            "bytes_total": len(raw),
            "bytes_consumed": offset,
            "closes_exactly": offset == len(raw),
        })
        if walk_error:
            entry["status"] = "WALK_ERROR"
            entry["error"] = walk_error
        elif unsupported:
            entry["status"] = "UNSUPPORTED_FIELDS"
        elif offset != len(raw):
            entry["status"] = "TAIL"
            entry["unparsed_tail"] = len(raw) - offset
        else:
            entry["status"] = "OK"

        # resolve references and record what they point at (arrays hold references too, so this recurses:
        # without it the array elements carried no target class and the scene rebuild could not link them)
        def resolve_references(value):
            if value.get("kind") == "component_ref":
                value["target_class"] = class_of_object(value["m_PathID"]) if value["m_FileID"] == 0 else None
                value["resolves_in_file"] = value["m_PathID"] in objects
            elif value.get("kind") == "array":
                for item in value.get("items") or []:
                    resolve_references(item)

        for value in values.values():
            resolve_references(value)
        if entry["status"] != "OK":
            doc["unparsed"].append(entry)
        doc["classes"].append(entry)

    ok = [entry for entry in doc["classes"] if entry["status"] == "OK"]
    print(f"scene {scene}")
    print(f"MonoBehaviours: {len(doc['classes'])}  decoded OK: {len(ok)}  "
          f"not decoded: {len(doc['classes']) - len(ok)}")
    for entry in doc["classes"]:
        if entry["status"] == "OK":
            refs = ", ".join(f"{name}->{value.get('target_class')}"
                             for name, value in entry["values"].items() if value.get("kind") == "component_ref")
            print(f"  OK    {entry['class']:<70} bytes={entry['bytes_total']:<4} {refs}")
    for entry in doc["unparsed"]:
        print(f"  {entry['status']:<6} {entry['class']:<70} {entry.get('error', '')}")

    if args.write:
        os.makedirs(os.path.dirname(OUT), exist_ok=True)
        with io.open(OUT, "w", encoding="utf-8") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)
    return 0


if __name__ == "__main__":
    sys.exit(main())
