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
# Scalars narrower than 4 bytes are followed by alignment to 4 (Unity writes every bool / byte / short field with
# the align flag).  Missing this was why every component with a bool before its last field walked 3 bytes short
# ("TAIL 3") or read the next field misaligned.
ALIGNED_AFTER = {"u8", "i8", "i16", "u16"}


def _scalar(name):
    return {"kind": SCALARS[name][0], "size": SCALARS[name][1]}


def _struct(*fields):
    return {"kind": "struct", "fields": [{"name": name, "kind": kind} for name, kind in fields]}


PPTR = {"kind": "pptr", "size": 12}
STRING = {"kind": "string", "size": 0}

# UnityEngine.Events.UnityEventBase serializes one PersistentCallGroup; each PersistentCall carries an
# ArgumentCache.  Field names are Unity's own, so the scene builder can apply them through SerializedProperty.
ARGUMENT_CACHE = _struct(("m_ObjectArgument", PPTR), ("m_ObjectArgumentAssemblyTypeName", STRING),
                         ("m_IntArgument", _scalar("int")), ("m_FloatArgument", _scalar("float")),
                         ("m_StringArgument", STRING), ("m_BoolArgument", _scalar("bool")))
PERSISTENT_CALL = _struct(("m_Target", PPTR), ("m_TargetAssemblyTypeName", STRING), ("m_MethodName", STRING),
                          ("m_Mode", _scalar("int")), ("m_Arguments", ARGUMENT_CACHE), ("m_CallState", _scalar("int")))
UNITY_EVENT = _struct(("m_PersistentCalls", _struct(("m_Calls", {"kind": "array", "element": PERSISTENT_CALL}))))

# Built-in (package) components have no reconstructed source to read a layout from, so their layouts are written
# out here from the package sources in Library/PackageCache (com.unity.ugui, com.unity.inputsystem), base-class
# fields first.  They are checked the same way as the script layouts: a walk must consume the object exactly.
GRAPHIC = [("m_Material", PPTR), ("m_Color", _scalar("Color")), ("m_RaycastTarget", _scalar("bool")),
           ("m_RaycastPadding", _scalar("Vector4"))]
MASKABLE_GRAPHIC = GRAPHIC + [("m_Maskable", _scalar("bool")), ("m_OnCullStateChanged", UNITY_EVENT)]
SELECTABLE = [
    ("m_Navigation", _struct(("m_Mode", _scalar("int")), ("m_WrapAround", _scalar("bool")),
                             ("m_SelectOnUp", PPTR), ("m_SelectOnDown", PPTR),
                             ("m_SelectOnLeft", PPTR), ("m_SelectOnRight", PPTR))),
    ("m_Transition", _scalar("int")),
    ("m_Colors", _struct(("m_NormalColor", _scalar("Color")), ("m_HighlightedColor", _scalar("Color")),
                         ("m_PressedColor", _scalar("Color")), ("m_SelectedColor", _scalar("Color")),
                         ("m_DisabledColor", _scalar("Color")), ("m_ColorMultiplier", _scalar("float")),
                         ("m_FadeDuration", _scalar("float")))),
    ("m_SpriteState", _struct(("m_HighlightedSprite", PPTR), ("m_PressedSprite", PPTR),
                              ("m_SelectedSprite", PPTR), ("m_DisabledSprite", PPTR))),
    ("m_AnimationTriggers", _struct(("m_NormalTrigger", STRING), ("m_HighlightedTrigger", STRING),
                                    ("m_PressedTrigger", STRING), ("m_SelectedTrigger", STRING),
                                    ("m_DisabledTrigger", STRING))),
    ("m_Interactable", _scalar("bool")),
    ("m_TargetGraphic", PPTR),
]
BUILTIN_LAYOUTS = {
    "UnityEngine.UI.Graphic": GRAPHIC,
    "UnityEngine.UI.MaskableGraphic": MASKABLE_GRAPHIC,
    "UnityEngine.UI.Image": MASKABLE_GRAPHIC + [
        ("m_Sprite", PPTR), ("m_Type", _scalar("int")), ("m_PreserveAspect", _scalar("bool")),
        ("m_FillCenter", _scalar("bool")), ("m_FillMethod", _scalar("int")), ("m_FillAmount", _scalar("float")),
        ("m_FillClockwise", _scalar("bool")), ("m_FillOrigin", _scalar("int")), ("m_UseSpriteMesh", _scalar("bool")),
        ("m_PixelsPerUnitMultiplier", _scalar("float"))],
    "UnityEngine.UI.RawImage": MASKABLE_GRAPHIC + [("m_Texture", PPTR), ("m_UVRect", _scalar("Rect"))],
    "UnityEngine.UI.Selectable": SELECTABLE,
    "UnityEngine.UI.Button": SELECTABLE + [("m_OnClick", UNITY_EVENT)],
    "UnityEngine.UI.Mask": [("m_ShowMaskGraphic", _scalar("bool"))],
    "UnityEngine.UI.CanvasScaler": [
        ("m_UiScaleMode", _scalar("int")), ("m_ReferencePixelsPerUnit", _scalar("float")),
        ("m_ScaleFactor", _scalar("float")), ("m_ReferenceResolution", _scalar("Vector2")),
        ("m_ScreenMatchMode", _scalar("int")), ("m_MatchWidthOrHeight", _scalar("float")),
        ("m_PhysicalUnit", _scalar("int")), ("m_FallbackScreenDPI", _scalar("float")),
        ("m_DefaultSpriteDPI", _scalar("float")), ("m_DynamicPixelsPerUnit", _scalar("float")),
        ("m_PresetInfoIsWorld", _scalar("bool"))],
    "UnityEngine.UI.GraphicRaycaster": [
        ("m_IgnoreReversedGraphics", _scalar("bool")), ("m_BlockingObjects", _scalar("int")),
        ("m_BlockingMask", _scalar("LayerMask"))],
    "UnityEngine.EventSystems.EventSystem": [
        ("m_FirstSelected", PPTR), ("m_sendNavigationEvents", _scalar("bool")), ("m_DragThreshold", _scalar("int"))],
    "UnityEngine.EventSystems.BaseInput": [],
    "UnityEngine.InputSystem.UI.InputSystemUIInputModule": [
        ("m_SendPointerHoverToParent", _scalar("bool")), ("m_MoveRepeatDelay", _scalar("float")),
        ("m_MoveRepeatRate", _scalar("float")),
        # (m_TrackedDeviceDragThresholdMultiplier sits here in the source but has no [SerializeField])
        ("m_XRTrackingOrigin", PPTR), ("m_ActionsAsset", PPTR),
        ("m_PointAction", PPTR), ("m_MoveAction", PPTR), ("m_SubmitAction", PPTR), ("m_CancelAction", PPTR),
        ("m_LeftClickAction", PPTR), ("m_MiddleClickAction", PPTR), ("m_RightClickAction", PPTR),
        ("m_ScrollWheelAction", PPTR), ("m_TrackedDevicePositionAction", PPTR),
        ("m_TrackedDeviceOrientationAction", PPTR), ("m_DeselectOnBackgroundClick", _scalar("bool")),
        ("m_PointerBehavior", _scalar("int")), ("m_CursorLockBehavior", _scalar("int")),
        ("m_ScrollDeltaPerTick", _scalar("float"))],
}
# Base classes whose own serialized data is only the fixed header (nothing to prepend).
EMPTY_BASES = {"MonoBehaviour", "UnityEngine.MonoBehaviour", "ScriptableObject", "UnityEngine.ScriptableObject",
               "UIBehaviour", "UnityEngine.EventSystems.UIBehaviour", "Behaviour", "Component", "Object"}


def builtin_layout(name):
    """A package layout by full or simple name (a game class may derive from `MaskableGraphic`)."""
    if name in BUILTIN_LAYOUTS:
        return BUILTIN_LAYOUTS[name]
    matches = [full for full in BUILTIN_LAYOUTS if full.rsplit(".", 1)[-1] == name]
    return BUILTIN_LAYOUTS[matches[0]] if len(matches) == 1 else None


def as_fields(layout):
    return [{"name": name, "kind": kind} for name, kind in layout]


FIELD_RE = re.compile(
    r"^\s*(?:\[[^\]]*\]\s*)*(?:public|private|protected|internal)\s+(?:readonly\s+)?"
    r"([A-Za-z_][A-Za-z0-9_.<>,\[\]]*)\s+([A-Za-z_][A-Za-z0-9_]*)\s*(?:=[^;]*)?;")


def game_root():
    from imported_source import game_root as imported_game_root
    return imported_game_root()


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
    by_simple = {}
    for entry in index["entries"]:
        namespace = entry.get("namespace") or ""
        for type_row in entry["types"]:
            name = type_row["name"]
            full = (namespace + "." + name) if namespace else name
            mapping[full] = entry["asset_path"]
            by_simple.setdefault(name, []).append(full)
            if type_row.get("kind") == "enum":
                enums.add(name)
                enums.add(full)
    CONTEXT.update({"index": mapping, "by_simple": by_simple, "enums": enums})
    return mapping, enums


# Filled by script_index(): what the type resolution below needs to turn a field's declared type into a layout.
CONTEXT = {"index": {}, "by_simple": {}, "enums": set()}
_DECLARATIONS = {}
_CLASS_FIELDS = {}


def resolve_type(name, owner_namespace=None):
    """A declared type name -> the full name of a reconstructed type, or None (not ours, or ambiguous)."""
    name = name.replace(" ", "")
    if name in CONTEXT["index"]:
        return name
    candidates = CONTEXT["by_simple"].get(name.rsplit(".", 1)[-1], [])
    if len(candidates) == 1:
        return candidates[0]
    same = [full for full in candidates if owner_namespace and full.rsplit(".", 1)[0] == owner_namespace]
    return same[0] if len(same) == 1 else None


def class_declaration(full_name):
    """(kind, base type or None, [Serializable]) of a reconstructed type, read from its declaration line."""
    if full_name in _DECLARATIONS:
        return _DECLARATIONS[full_name]
    result = None
    asset_path = CONTEXT["index"].get(full_name)
    path = source_file(asset_path) if asset_path else None
    if path:
        with io.open(path, encoding="utf-8-sig", errors="replace") as fh:
            lines = fh.read().splitlines()
        simple = full_name.rsplit(".", 1)[-1]
        declaration = re.compile(r"^\s*(?:public|internal|private|protected)?\s*(?:(?:sealed|abstract|static|partial)\s+)*"
                                 r"(class|struct)\s+" + re.escape(simple) + r"\b(?:<[^>]*>)?\s*(?::\s*([^{/]+))?")
        for number, line in enumerate(lines):
            match = declaration.match(line)
            if not match:
                continue
            base = None
            if match.group(2):
                first = match.group(2).split(",")[0].strip()
                # an interface list has no base class: IFoo, IBar (the reconstruction keeps the I-prefix convention)
                if not re.match(r"^I[A-Z]", first):
                    base = first
            attributes, lookback = [], number - 1
            while lookback >= 0 and re.match(r"^\s*\[", lines[lookback]):
                attributes.append(lines[lookback])
                lookback -= 1
            result = (match.group(1), base, "Serializable" in " ".join(attributes))
            break
    _DECLARATIONS[full_name] = result
    return result


REACTIVE_PROPERTIES = {"BoolReactiveProperty": "bool", "IntReactiveProperty": "int", "LongReactiveProperty": "long",
                       "FloatReactiveProperty": "float", "DoubleReactiveProperty": "double",
                       "StringReactiveProperty": "string", "ByteReactiveProperty": "byte",
                       "Vector2ReactiveProperty": "Vector2", "Vector3ReactiveProperty": "Vector3",
                       "Vector4ReactiveProperty": "Vector4", "ColorReactiveProperty": "Color",
                       "RectReactiveProperty": "Rect", "QuaternionReactiveProperty": "Quaternion"}
UNITY_OBJECT_BASES = {"MonoBehaviour", "ScriptableObject", "Component", "Behaviour", "Object", "UIBehaviour",
                      "StateMachineBehaviour"}


def derives_from_unity_object(full_name, depth=0):
    """True when a reconstructed class is a UnityEngine.Object (then a field of that type is a PPtr)."""
    declaration = class_declaration(full_name)
    if declaration is None or depth > 16:
        return False
    _, base, _ = declaration
    if not base:
        return False
    simple = base.split("<")[0].rsplit(".", 1)[-1]
    if simple in UNITY_OBJECT_BASES or builtin_layout(simple) is not None:
        return True
    resolved = resolve_type(base.split("<")[0], full_name.rsplit(".", 1)[0] if "." in full_name else None)
    return derives_from_unity_object(resolved, depth + 1) if resolved else False


def class_fields(full_name):
    """Every serialized field of a reconstructed class, base-class fields first.  Returns (fields, error)."""
    if full_name in _CLASS_FIELDS:
        return _CLASS_FIELDS[full_name]
    _CLASS_FIELDS[full_name] = (None, "recursive layout " + full_name)   # guards a self-referencing type
    declaration = class_declaration(full_name)
    asset_path = CONTEXT["index"].get(full_name)
    path = source_file(asset_path) if asset_path else None
    if declaration is None or path is None:
        result = (None, "no reconstructed declaration for " + full_name)
    else:
        _, base, _ = declaration
        namespace = full_name.rsplit(".", 1)[0] if "." in full_name else None
        base_fields, error = [], None
        if base and base.split("<")[0] not in EMPTY_BASES:
            layout = builtin_layout(base)
            if layout is not None:
                base_fields = as_fields(layout)
            else:
                resolved = resolve_type(base.split("<")[0], namespace)
                if resolved is None:
                    error = "base class '" + base + "' of " + full_name + " has no known layout"
                else:
                    base_fields, error = class_fields(resolved)
        if error:
            result = (None, error)
        else:
            parsed, error = serialized_fields(path, full_name, CONTEXT["enums"])
            if error:
                result = (None, error)
            else:
                own, unsupported = parsed
                if unsupported:
                    result = (None, full_name + " has unsupported fields: " +
                              ", ".join(row["name"] + ":" + row["type"] for row in unsupported))
                else:
                    result = (base_fields + own, None)
    _CLASS_FIELDS[full_name] = result
    return result


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

            kind = serialized_kind(field_type, enum_names, class_name.rsplit(".", 1)[0] if "." in class_name else None)
            if kind is None:
                unsupported.append({"name": field_name, "type": field_type})
            else:
                fields.append({"name": field_name, "type": field_type, "kind": kind})
        number += 1
    return (fields, unsupported), None


def serialized_kind(field_type, enum_names, owner_namespace=None):
    """Map a C# field type onto a serialized kind, or None when it cannot be walked safely."""
    field_type = field_type.replace(" ", "")
    if field_type in SCALARS:
        return {"kind": SCALARS[field_type][0], "size": SCALARS[field_type][1]}
    if field_type in ("string", "System.String"):
        return {"kind": "string", "size": 0}
    if field_type in enum_names or field_type.rsplit(".", 1)[-1] in enum_names:
        return {"kind": "i32", "size": 4}   # enums serialize as their underlying int
    # Arrays first: `AssetReferenceT<Material>[]` is an array of references, not one reference (checking the name
    # for "AssetReference" before the brackets read LocalizationPresenter's font materials as a single entry).
    element = None
    if field_type.endswith("[]"):
        element = field_type[:-2]
    elif field_type.startswith("List<") and field_type.endswith(">"):
        element = field_type[5:-1]
    if element is not None:
        element_kind = serialized_kind(element, enum_names, owner_namespace)
        if element_kind is None:
            return None
        return {"kind": "array", "element": element_kind}
    if "AssetReference" in field_type:
        # Addressables' AssetReference is a [Serializable] class serialized inline:
        # string m_AssetGUID, string m_SubObjectName, string m_SubObjectType.
        return {"kind": "asset_reference", "size": 0}
    if re.match(r"^(UnityEngine\.Events\.)?UnityEvent(<.*>)?$", field_type):
        return UNITY_EVENT
    # UniRx's inspectable reactive properties are [Serializable] ReactiveProperty<T>: one `[SerializeField] T value`.
    reactive = REACTIVE_PROPERTIES.get(field_type)
    if reactive is None:
        match = re.match(r"^(?:UniRx\.)?ReactiveProperty<(.+)>$", field_type)
        reactive = match.group(1) if match else None
    if reactive is not None:
        value_kind = serialized_kind(reactive, enum_names, owner_namespace)
        return None if value_kind is None else {"kind": "struct", "fields": [{"name": "value", "kind": value_kind}]}
    # One of the reconstructed types: a [Serializable] class or a struct is written inline (its fields, in order),
    # a class deriving from UnityEvent is an event, anything else (MonoBehaviour, ScriptableObject) is a PPtr.
    # Treating the inline ones as PPtrs is what made PerspectiveStagePresenter's `_stageData` decode as garbage.
    full = resolve_type(field_type, owner_namespace)
    if full is not None:
        declaration = class_declaration(full)
        if declaration is not None:
            kind, base, serializable = declaration
            if base and re.match(r"^(UnityEngine\.Events\.)?UnityEvent(<.*>)?$", base.replace(" ", "")):
                return UNITY_EVENT
            if kind == "struct" or (serializable and not derives_from_unity_object(full)):
                return {"kind": "class", "class": full}
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

    fields_values, offset, error = walk_fields(raw, offset, fields)
    values.update(fields_values)
    return values, offset, error


def walk_fields(raw, offset, fields):
    """Consume a sequence of fields (an object body, a struct, a [Serializable] class)."""
    values = OrderedDict()
    for field in fields:
        value, offset, error = walk_value(raw, offset, field["kind"])
        if error:
            return values, offset, f"{error} at '{field['name']}'"
        values[field["name"]] = value
    return values, offset, None


def align4(offset):
    return offset + ((4 - offset % 4) % 4)


def walk_value(raw, offset, kind_row, packed=False):
    """One value; returns (value, new_offset, error).  `packed`: an array element (no per-element alignment)."""
    kind, size = kind_row["kind"], kind_row.get("size", 0)
    if offset + size > len(raw):
        return None, offset, "ran past the end of the object"
    if kind == "array":
        (count,) = struct.unpack_from("<i", raw, offset)
        element = kind_row["element"]
        if count < 0 or count > 100000:
            return None, offset, f"implausible array length {count}"
        items, cursor = [], offset + 4
        for _ in range(count):
            item, cursor, nested_error = walk_value(raw, cursor, element, packed=True)
            if nested_error:
                return None, cursor, f"{nested_error} (array element {len(items)})"
            items.append(item)
        if element["kind"] in ALIGNED_AFTER:
            cursor = align4(cursor)     # a byte / bool array is packed and aligned once, after the last element
        return {"kind": "array", "count": count, "items": items}, cursor, None
    if kind == "asset_reference":
        return walk_asset_reference(raw, offset)
    if kind == "struct":
        values, cursor, error = walk_fields(raw, offset, kind_row["fields"])
        return (None if error else {"kind": "struct", "fields": values}), cursor, error
    if kind == "class":
        fields, error = class_fields(kind_row["class"])
        if error:
            return None, offset, error
        values, cursor, error = walk_fields(raw, offset, fields)
        return (None if error else {"kind": "struct", "class": kind_row["class"], "fields": values}), cursor, error
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
        end = offset + size
        if kind in ALIGNED_AFTER and not packed:
            end = align4(end)
        return {"kind": "int", "value": struct.unpack_from(fmt, raw, offset)[0]}, end, None
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

    # A PPtr with m_FileID > 0 points into another serialized file (sharedassets0.assets for the splash logos and
    # the stage RenderTextures).  Record what it is - type, name, and for a RenderTexture its descriptor - so the
    # scene builder can bind the project's copy by type + name, or author the texture, instead of dropping it.
    externals = [external.path for external in list(env.files.values())[0].externals]
    external_envs = {}

    def external_object(file_id, path_id):
        if file_id - 1 >= len(externals):
            return {"file": None, "error": f"m_FileID {file_id} is not in the externals table"}
        file_name = externals[file_id - 1]
        info = {"file": file_name, "path_id": path_id}
        if path_id == 0:
            return info
        if file_name not in external_envs:
            local = os.path.join(os.path.dirname(scene), os.path.basename(file_name))
            try:
                external_envs[file_name] = ({int(obj.path_id): obj for obj in UnityPy.load(local).objects}
                                            if os.path.exists(local) else None)
            except Exception as ex:   # an unreadable file is reported per reference, never guessed
                external_envs[file_name] = None
                info["error"] = f"{type(ex).__name__}: {ex}"
        table = external_envs[file_name]
        target = table.get(path_id) if table else None
        if target is None:
            info.setdefault("error", "not readable" if table is None else "no such object")
            return info
        info["type"] = target.type.name
        try:
            if target.type.name == "MonoBehaviour":
                # no type tree here either: the fixed header, then m_Name (an InputActionReference is named after
                # its action, e.g. "UI/Point", which is how the builder finds the project's copy)
                raw_target = target.get_raw_data()
                _, script_path_id = struct.unpack_from("<iq", raw_target, 16)
                (length,) = struct.unpack_from("<i", raw_target, HEADER_BYTES)
                info["name"] = raw_target[HEADER_BYTES + 4:HEADER_BYTES + 4 + length].decode("utf-8", "replace")
                info["script_path_id"] = script_path_id
                info["script"] = monoscripts.get(script_path_id)
                return info
            data = target.read()
            info["name"] = getattr(data, "m_Name", None) or ""
            if target.type.name == "RenderTexture":
                settings = data.m_TextureSettings
                info["descriptor"] = {
                    "width": int(data.m_Width), "height": int(data.m_Height),
                    "anti_aliasing": int(data.m_AntiAliasing), "color_format": int(data.m_ColorFormat),
                    "depth_stencil_format": int(data.m_DepthStencilFormat or 0), "dimension": int(data.m_Dimension),
                    "volume_depth": int(data.m_VolumeDepth), "mip_map": bool(data.m_MipMap),
                    "generate_mips": bool(data.m_GenerateMips), "srgb": bool(data.m_SRGB),
                    "filter_mode": int(settings.m_FilterMode), "aniso": int(settings.m_Aniso),
                    "wrap_u": int(settings.m_WrapU), "wrap_v": int(settings.m_WrapV)}
        except Exception as ex:
            info["error"] = f"{type(ex).__name__}: {ex}"
        return info

    doc = {"scene": scene, "externals": externals, "classes": [], "unparsed": []}
    for row in inventory["objects"]:
        if row["type"] != "MonoBehaviour" or not row.get("script"):
            continue
        class_name = row["script"]
        source_path = index.get(class_name)
        entry = {"path_id": row["path_id"], "class": class_name, "asset_path": source_path}
        unsupported = []
        if class_name in BUILTIN_LAYOUTS:
            entry["layout"] = "builtin (package source, Library/PackageCache)"
            fields = as_fields(BUILTIN_LAYOUTS[class_name])
        else:
            resolved = source_file(source_path) if source_path else None
            if resolved is None:
                entry["status"] = "NO_SOURCE"
                doc["unparsed"].append(entry)
                continue
            fields, error = class_fields(class_name)
            if error:
                entry["status"] = "NO_CLASS"
                entry["error"] = error
                doc["unparsed"].append(entry)
                continue
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
            if value is None:
                return
            if value.get("kind") == "component_ref":
                value["target_class"] = class_of_object(value["m_PathID"]) if value["m_FileID"] == 0 else None
                value["resolves_in_file"] = value["m_FileID"] == 0 and value["m_PathID"] in objects
                if value["m_FileID"] > 0:
                    value["external"] = external_object(value["m_FileID"], value["m_PathID"])
            elif value.get("kind") == "array":
                for item in value.get("items") or []:
                    resolve_references(item)
            elif value.get("kind") == "struct":
                for item in value["fields"].values():
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
