"""Decode the serialized field values of the shipped Naninovel configuration assets.

    python tools/unity-rebuild/decode_naninovel_configs.py --write
    python tools/unity-rebuild/decode_naninovel_configs.py --check    # fail when a decoded value changed

Why this exists
---------------
The project's Naninovel *configuration* assets (`ScriptsConfiguration.TitleScript`,
`ResourceProviderConfiguration`, the character/background records, ...) were generated into the Unity project and
therefore baked into the shipped Player.  They are the last missing piece before the reconstructed engine can be
told what content to load.  In a player build a MonoBehaviour has **no type tree** (verified: every MonoBehaviour
in `resources.assets` reports `serialized_type.nodes == []`), so UnityPy cannot read them; the layout has to come
from the C# source and then be *checked* against the bytes.

Layout rules (measured on this build)
-------------------------------------
* 28-byte header: `PPtr<GameObject>` (int32 m_FileID + int64 m_PathID = 12), `m_Enabled` (4), `PPtr<MonoScript>`
  (12), then `m_Name` (length-prefixed string), then the class's serialized fields, base-class fields first.
* Unity writes every member into a 4-byte-aligned slot.  `bool`/`byte`/`sbyte` take 1 byte and are padded to 4,
  `short`/`ushort` take 2 and are padded to 4, strings are `int32 length + UTF-8 + pad-to-4` (the pad is *also*
  applied to the last member of the object), arrays are `int32 count + elements`, `[Serializable]` classes and
  structs are inlined recursively, `UnityEngine.Object` is a PPtr, and an enum is its underlying integer.
  Verified byte-for-byte against `ResourceProviderConfiguration` (148 B), `EngineConfiguration` (108 B),
  `ScriptPlayerConfiguration` (92 B), `ProjectStats` (56 B), `LoadingIcon` (40 B), `EngineVersion` (76 B) and
  `WitchTrialsChoiceHandlersConfiguration` (76 B) - every one closes exactly, including the trailing pad.
* `[SerializeField]` on a private field serializes it; `[SerializeReference]` does too, and writes an **int64**
  (negative = no reference, e.g. `0xFFFFFFFFFFFFFFFE`), not an int32.
* `#if` regions matter in both directions.  `ResourceProviderConfiguration`'s Google Drive fields live inside
  `#if UNITY_GOOGLE_DRIVE_AVAILABLE` (not defined in the shipped player, so excluded), while
  `InputConfiguration.InputActions` lives inside `#if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE`, which *was*
  defined - the 524-byte object only closes when that 12-byte PPtr is counted.  Conditions are therefore
  evaluated, and `DEFAULT_DEFINED` records the symbols the shipped player was built with.
* Four objects (`BackgroundsConfiguration`, `CharactersConfiguration`, `ChoiceHandlersConfiguration`,
  `TextPrintersConfiguration`, 28 bytes each) end with a trailing `ManagedReferencesRegistry` - Unity's
  bookkeeping for objects containing `[SerializeReference]` fields, appended after the last declared field.  It
  is decoded only as far as the evidence goes (`m_Version`, `m_RefCount`) and each entry's trailing 12 bytes are
  kept verbatim as undecoded.  `closes_exactly` excludes it; `closes_with_registry` includes it.
* `UnityEngine.Events.UnityEvent` is serialized inline (not a PPtr) and its layout is measured, see
  `UNITY_EVENT_BASES`.

Honesty rules (inherited from `decode_level0_component_values.py`)
-----------------------------------------------------------------
* A walk must consume the object **exactly**.  A wrong type mapping shows up as a non-closing walk; that is
  reported, never padded away or "fixed".
* Every PPtr must resolve to an object in the same file; the target's class is read from the target's own
  `m_Script` (through `res/_monoscripts.csv`, so the namespace survives) and its name from `m_Name`.
* Anything not understood goes to a per-object `unparsed` list with an offset and a reason.  No value is guessed:
  a field whose C# type cannot be laid out stops the walk at that field and says so, and a partially decoded
  structure says which part is undecoded.
* The Addressables catalog cannot be joined to these objects.  `catalog_dump_shipped.json` carries no path_id /
  fileID / GUID field (verified by a byte scan of all 52,783,790 bytes) and `resources.assets` is not
  Addressables content, so `resources_path` from `ResourceManager.m_Container` is recorded instead and a
  `catalog_internal_id` is only ever written on an exact textual match.  See
  `tools/unity-rebuild/out/unity/catalog_pathid_report.md`.

Output (deterministic ordering)
-------------------------------
  tools/unity-rebuild/out/unity/naninovel_configs.json   - every value plus a per-field byte layout
  tools/unity-rebuild/out/unity/naninovel_configs.md     - short human summary of the same run
"""
from __future__ import annotations

import argparse
import csv
import glob
import io
import json
import os
import re
import struct
import sys
from collections import OrderedDict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
OUT_JSON = os.path.join(OUT_DIR, "naninovel_configs.json")
OUT_MD = os.path.join(OUT_DIR, "naninovel_configs.md")
MONOSCRIPTS = os.path.join(REPO, "res", "_monoscripts.csv")
CATALOG = os.path.join(OUT_DIR, "catalog_dump_shipped.json")

# Measured (see tools/unity-rebuild/out/unity/catalog_pathid_report.md): `catalog_dump_shipped.json` carries no
# path_id / fileID / GUID field at all - its `internal_id`s are Addressables *addresses* (asset paths), and
# `resources.assets` is not Addressables content (0 of its objects appear in the catalog).  So a
# `path_id -> internal_id` join is impossible for these objects; the actionable path for a Resources-loaded
# asset is the `ResourceManager.m_Container` key, which this tool records instead.
CATALOG_JOIN_NOTE = ("catalog_dump_shipped.json has no path_id/fileID/GUID field and resources.assets is not "
                     "Addressables content, so a path_id -> internal_id join is not possible for these objects. "
                     "The ResourceManager m_Container Resources path is recorded instead; a catalog internal_id "
                     "is only ever recorded on an exact textual match, never invented.")

# Source roots scanned for C# declarations, in priority order (first match wins for a duplicated type name).
SOURCE_ROOTS = [
    os.path.join(REPO, "UnityProject", "Packages"),
    os.path.join(REPO, "UnityProject", "Assets", "Scripts"),
]

HEADER_BYTES = 28          # PPtr<GameObject> 12 | m_Enabled 4 | PPtr<MonoScript> 12
SCRIPT_OFFSET = 16

# Preprocessor symbols the shipped player was compiled with.  These are not guesses: `InputConfiguration`
# guards `UnityEngine.InputSystem.InputActionAsset InputActions` behind
# `#if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE`, and its object (path_id 1468, 524 bytes) only closes
# exactly when that 12-byte PPtr is counted - without it the walk lands on `Bindings` 12 bytes early.
# `UNITY_EDITOR` and `UNITY_GOOGLE_DRIVE_AVAILABLE` are deliberately absent: `ResourceProviderConfiguration`
# closes exactly only when its four Google Drive fields are excluded.
DEFAULT_DEFINED = {"ENABLE_INPUT_SYSTEM", "INPUT_SYSTEM_AVAILABLE"}

# The six shipped assets that are configuration-like but are not named `*Configuration`.
EXTRA_CONFIG_CLASSES = {
    "Naninovel.EngineVersion", "Naninovel.ProjectStats", "Naninovel.ProjectResources",
    "Naninovel.CompilerLocalization", "Naninovel.UI.LoadingIcon", "Naninovel.UI.EngineInitializationUI",
}

# ------------------------------------------------------------------------------------------------------------
# Serialized value kinds
# ------------------------------------------------------------------------------------------------------------
# `slot` is the bytes the member occupies *including* Unity's pad-to-4.  For the 1- and 2-byte primitives that
# pad is real: `EngineVersion` stores `preview` in a full 4-byte slot (bytes 0x38..0x3B of a 76-byte object).
SCALARS = {
    "float": ("f32", 4, "float"), "double": ("f64", 8, "float"),
    "int": ("i32", 4, "int"), "uint": ("u32", 4, "int"),
    "short": ("i16", 4, "int"), "ushort": ("u16", 4, "int"),
    "byte": ("u8", 4, "int"), "sbyte": ("i8", 4, "int"),
    "long": ("i64", 8, "int"), "ulong": ("u64", 8, "int"),
    "bool": ("bool", 4, "bool"), "char": ("u16", 4, "int"),
    "Color": ("color", 16, "value"), "Color32": ("color32", 4, "value"),
    "Vector2": ("vec2", 8, "value"), "Vector3": ("vec3", 12, "value"),
    "Vector4": ("vec4", 16, "value"), "Quaternion": ("quat", 16, "value"),
    "Rect": ("rect", 16, "value"), "RectInt": ("rectint", 16, "value"),
    "Vector2Int": ("vec2int", 8, "value"), "Vector3Int": ("vec3int", 12, "value"),
    "LayerMask": ("i32", 4, "int"),
}

# UnityEngine types serialized as a PPtr.  A type that is neither declared in the project's C# sources nor listed
# here is reported as *unknown* rather than assumed to be an object reference - an unknown type is exactly the
# thing that would silently shift every following field.
UNITY_OBJECT_TYPES = {
    "Object", "Component", "Behaviour", "MonoBehaviour", "ScriptableObject", "GameObject", "Transform",
    "RectTransform", "UIBehaviour", "Texture", "Texture2D", "Texture3D", "RenderTexture", "Sprite", "Material",
    "Shader", "Mesh", "AudioClip", "AudioSource", "AudioListener", "AudioMixer", "AudioMixerGroup", "Animator",
    "AnimationClip", "RuntimeAnimatorController", "Font", "TextAsset", "StyleSheet", "VisualTreeAsset",
    "PanelSettings", "Camera", "Canvas", "CanvasGroup", "Light", "Collider", "Collider2D", "Rigidbody",
    "Rigidbody2D", "Renderer", "MeshRenderer", "MeshFilter", "SpriteRenderer", "Graphic", "Image", "RawImage",
    "Button", "Toggle", "Slider", "Dropdown", "Scrollbar", "ScrollRect", "InputField", "Mask", "RectMask2D",
    "LayoutElement", "LayoutGroup", "HorizontalOrVerticalLayoutGroup", "ContentSizeFitter", "AspectRatioFitter",
    "CanvasScaler", "GraphicRaycaster", "EventTrigger", "VideoPlayer", "PlayableDirector", "VolumeProfile",
    "AssetReference", "AssetReferenceT", "ParticleSystem", "LineRenderer", "TrailRenderer", "SpriteMask",
    "Tilemap", "TilemapRenderer", "Grid", "GridLayout", "SpriteAtlas", "Avatar", "Motion", "Flare", "GUIStyle",
    "GUISkin", "Cubemap", "ReflectionProbe", "LightProbeGroup", "Terrain", "TerrainData", "Cloth",
    "WheelCollider", "Joint", "ConfigurableJoint", "HingeJoint", "SpringJoint", "FixedJoint",
    "CharacterController", "NavMeshAgent", "NavMeshObstacle", "TMP_FontAsset", "TMP_Text", "TMP_Settings",
    "ScriptableRendererData", "ScriptableRendererFeature",
    # UnityEngine.EventSystems: required by `InputConfiguration.CustomEventSystem`.
    "EventSystem", "BaseInputModule", "PointerInputModule", "StandaloneInputModule", "TouchInputModule",
    "BaseRaycaster", "BaseInput",
    # Unity's Input System package (not in the project's C# sources; `InputConfiguration.InputActions`).
    "InputActionAsset", "InputActionReference", "InputAction", "InputActionMap", "InputControlScheme",
}
# UnityEngine *enum* types, serialized as int32.
UNITY_ENUM_TYPES = {
    "LogType", "KeyCode", "EventModifiers", "TextAnchor", "FontStyle", "Space", "HideFlags", "FilterMode",
    "TextureWrapMode", "TextureFormat", "SystemLanguage", "RenderTextureFormat", "ColorSpace", "CompareFunction",
    "AudioMixerUpdateMode", "AudioRolloffMode", "AudioVelocityUpdateMode", "WrapMode", "AnimatorUpdateMode",
    "AnimatorCullingMode", "CameraClearFlags", "LightType", "BlendMode", "ScaleMode", "SpriteMeshType",
    "SpritePackingMode", "SpritePackingRotation", "TextAlignment", "HorizontalWrapMode", "VerticalWrapMode",
    "EventTriggerType", "TouchScreenKeyboardType", "CharacterValidation", "ContentType", "LineType",
    "SelectionState", "Transition", "RenderMode", "AdditionalCanvasShaderChannels", "ScreenMatchMode",
    "RecenterAxis", "DepthTextureMode", "OpaqueSortMode", "TransparencySortMode", "AnisotropicFiltering",
    "CubemapFace", "MeshTopology", "IndexFormat", "SkinWeights", "ParticleSystemRenderMode", "AspectMode",
    "FitMode", "NavigationMode", "Direction", "Axis", "MotionVectorGenerationMode", "ShadowCastingMode",
    "LightmapBakeType", "MixedLightingMode", "AmbientMode", "DefaultReflectionMode", "FogMode", "BlendWeights",
    "AudioSpeakerMode", "AudioDataLoadState", "AudioClipLoadType", "AudioCompressionFormat", "VideoSource",
    "VideoAspectRatio", "VideoTimeReference", "VideoTimeUpdateMode", "VideoAudioOutputMode", "VideoRenderMode",
}
# Types Unity's serializer ignores entirely (they consume no bytes even if decorated).
NOT_SERIALIZED_GENERICS = {"Dictionary", "IDictionary", "SortedDictionary", "HashSet", "ISet", "Queue", "Stack",
                           "Nullable", "IReadOnlyDictionary"}
NOT_SERIALIZED_NAMES = {"IList", "IEnumerable", "ICollection", "IReadOnlyList", "IReadOnlyCollection",
                        "IDictionary", "ISet", "System.Object"}

# Serialized bases that live in the *engine*, not in the project's C# sources, so their layout cannot be read
# from source and has to be measured.  UnityEngine.Events.UnityEvent is one of these, and it is not a reference:
# Unity serializes the listener list inline.  The layout below was measured on this build from
# `Naninovel.UI.EngineInitializationUI` (path_id 1703, 184 bytes), whose only field is a `FloatUnityEvent`:
#   @0x4C int32 m_Calls.Count = 1
#   @0x50 PPtr m_Target = (fileID 0, pathID 1793)      @0x5C string m_TargetAssemblyTypeName = ""
#   @0x60 string m_MethodName = "set_value"            @0x70 int32 m_Mode = 0
#   @0x74 ArgumentCache: PPtr m_ObjectArgument, string m_ObjectArgumentAssemblyTypeName
#                        ("UnityEngine.Object, UnityEngine"), int32 m_IntArgument, float m_FloatArgument,
#                        string m_StringArgument, bool m_BoolArgument
#   @0xB4 int32 m_CallState = 2                        -> closes the 184-byte object exactly
UNITY_EVENT_BASES = {"UnityEvent", "UnityEventBase"}

TYPE_DECL_RE = re.compile(
    r"\b(?P<kind>class|struct|enum|interface)\s+(?P<name>[A-Za-z_]\w*)\s*"
    r"(?P<generic><[^<>{}()]*>)?\s*(?::\s*(?P<bases>[^{;]*?))?\s*"
    r"(?:where\s+[^{;]*?)?\s*\{", re.S)

# `[modifiers] Type name1, name2 = init;`  - the declarator list is validated so a method or property cannot match.
MODS = (r"(?:(?:public|private|protected|internal|static|readonly|const|volatile|new|unsafe|extern|event|"
        r"partial|async|override|virtual|abstract|sealed)\s+)*")
TYPE_TEXT = r"[A-Za-z_][A-Za-z0-9_.]*(?:\s*<[^<>]*(?:<[^<>]*>[^<>]*)*>)?(?:\s*\[\s*,?\s*\]\s*)*"
FIELD_RE = re.compile(
    r"^\s*(?P<mods>" + MODS + r")(?P<type>" + TYPE_TEXT + r")\s+"
    r"(?P<decls>[A-Za-z_][A-Za-z0-9_]*(?:\s*=\s*[^,]*)?(?:\s*,\s*[A-Za-z_][A-Za-z0-9_]*(?:\s*=\s*[^,]*)?)*)\s*$")
GENERIC_RE = re.compile(r"^([A-Za-z_][A-Za-z0-9_.]*)\s*<\s*(.*)\s*>$")

try:
    import UnityPy
except ImportError:                                                       # reported from main() with the path
    UnityPy = None


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as handle:
        return json.load(handle)["game_root"]


# ------------------------------------------------------------------------------------------------------------
# C# source scanning
# ------------------------------------------------------------------------------------------------------------
def mask_cs(text):
    """Blank out comment and string/char literal *contents*, preserving length and newlines.

    Length preservation keeps character offsets and line numbers valid, so brace matching stays correct and a
    reported field line is the real one.  Masking literals is what makes brace counting work at all: Naninovel
    tooltips are full of `{`, `[` and `]`.
    """
    out = list(text)
    index, size = 0, len(text)
    while index < size:
        char = text[index]
        if char == "/" and index + 1 < size and text[index + 1] == "/":
            while index < size and text[index] != "\n":
                out[index] = " "
                index += 1
        elif char == "/" and index + 1 < size and text[index + 1] == "*":
            out[index] = out[index + 1] = " "
            index += 2
            while index < size and not (text[index] == "*" and index + 1 < size and text[index + 1] == "/"):
                if text[index] != "\n":
                    out[index] = " "
                index += 1
            if index < size:
                out[index] = out[index + 1] = " "
                index += 2
        elif char in "\"'":
            quote, verbatim = char, index > 0 and text[index - 1] == "@"
            index += 1
            while index < size:
                if verbatim:
                    if text[index] == '"':
                        if index + 1 < size and text[index + 1] == '"':
                            out[index] = out[index + 1] = " "
                            index += 2
                            continue
                        break
                    if text[index] != "\n":
                        out[index] = " "
                    index += 1
                else:
                    if text[index] == "\\":
                        out[index] = " "
                        if index + 1 < size and text[index + 1] != "\n":
                            out[index + 1] = " "
                        index += 2
                        continue
                    if text[index] == quote:
                        break
                    if text[index] != "\n":
                        out[index] = " "
                    index += 1
            index += 1
        else:
            index += 1
    return "".join(out)


def pp_eval_condition(expression, defined):
    """Evaluate a C# preprocessor condition; unknown symbols are False.  Any parse failure is False."""
    expression = re.sub(r"\btrue\b", "True", expression)
    expression = re.sub(r"\bfalse\b", "False", expression)
    expression = re.sub(r"\b[A-Za-z_]\w*\b", lambda m: "True" if m.group(0) in defined else "False", expression)
    expression = expression.replace("&&", " and ").replace("||", " or ")
    expression = re.sub(r"!(?!=)", " not ", expression)
    try:
        return bool(eval(expression, {"__builtins__": {}}, {}))          # noqa: S307 - operands are ours
    except Exception:
        return False


def preprocess(masked, defined):
    """Blank out inactive `#if` regions so their fields are not expected in the object."""
    lines = masked.split("\n")
    result, stack = [], []
    for line in lines:
        match = re.match(r"#\s*(if|elif|else|endif)\b(.*)$", line.strip())
        if match:
            keyword, rest = match.group(1), match.group(2).strip()
            if keyword == "if":
                outer = stack[-1][2] if stack else True
                condition = pp_eval_condition(rest, defined)
                stack.append([outer, condition, outer and condition])
            elif keyword == "elif" and stack:
                frame = stack[-1]
                condition = pp_eval_condition(rest, defined)
                frame[2] = frame[0] and condition and not frame[1]
                frame[1] = frame[1] or condition
            elif keyword == "else" and stack:
                frame = stack[-1]
                frame[2] = frame[0] and not frame[1]
                frame[1] = True
            elif keyword == "endif" and stack:
                stack.pop()
            result.append(" " * len(line))
            continue
        result.append(line if (stack[-1][2] if stack else True) else (" " * len(line)))
    return "\n".join(result)


def brace_match(text, open_index):
    depth = 0
    for index in range(open_index, len(text)):
        if text[index] == "{":
            depth += 1
        elif text[index] == "}":
            depth -= 1
            if depth == 0:
                return index
    return min(open_index, len(text) - 1)


def declaration_start(masked, index):
    """Walk back over the declaration's own words so `preceding_attributes` can see past `public`/`abstract`.

    Without this the attribute lookup stops at the modifier keywords and `[System.Serializable]` is never seen -
    which made `ResourceLoaderConfiguration` look like a non-serializable type even though it is declared
    `[System.Serializable]`.
    """
    cursor = index
    while cursor > 0 and (masked[cursor - 1].isalnum() or masked[cursor - 1] in "_ \t\r\n"):
        cursor -= 1
    return cursor


def preceding_attributes(masked, index):
    """Attribute blocks immediately preceding `index`, as raw text (`[A] [B]` - nearest first, returned in order)."""
    blocks, cursor = [], index
    while True:
        while cursor > 0 and masked[cursor - 1] in " \t\r\n":
            cursor -= 1
        if cursor == 0 or masked[cursor - 1] != "]":
            break
        depth, scan = 0, cursor - 1
        while scan >= 0:
            if masked[scan] == "]":
                depth += 1
            elif masked[scan] == "[":
                depth -= 1
                if depth == 0:
                    break
            scan -= 1
        if scan < 0:
            break
        blocks.append(masked[scan:cursor])
        cursor = scan
    return " ".join(reversed(blocks))


def split_top_level(text, separator=","):
    parts, depth, current = [], 0, ""
    for char in text:
        if char in "<([{":
            depth += 1
        elif char in ">)]}":
            depth -= 1
        if char == separator and depth == 0:
            parts.append(current)
            current = ""
        else:
            current += char
    parts.append(current)
    return [part.strip() for part in parts if part.strip()]


class TypeDecl:
    __slots__ = ("full_name", "name", "namespace", "outer", "kind", "generics", "bases", "attributes",
                 "file", "line", "body_start", "body_end", "span")

    def __init__(self, **kwargs):
        for slot in self.__slots__:
            setattr(self, slot, kwargs.get(slot))


def scan_source(text, path, defined):
    """All type declarations in one file: namespace, enclosing types, base list, attributes and body span."""
    masked = preprocess(mask_cs(text), defined)
    namespaces = []
    for match in re.finditer(r"\bnamespace\s+([A-Za-z_][\w.]*)\s*\{", masked):
        open_index = masked.index("{", match.end() - 1)
        namespaces.append((match.start(), brace_match(masked, open_index), match.group(1)))
    for match in re.finditer(r"\bnamespace\s+([A-Za-z_][\w.]*)\s*;", masked):
        if not any(start <= match.start() <= close for start, close, _ in namespaces):
            namespaces.append((match.start(), len(masked), match.group(1)))
    namespaces.sort(key=lambda row: row[0] - row[1] / max(1, len(masked)))   # innermost wins on ties

    def namespace_at(index):
        hits = [name for start, close, name in namespaces if start <= index <= close]
        return hits[-1] if hits else ""

    raw = []
    for match in TYPE_DECL_RE.finditer(masked):
        open_index = masked.index("{", match.end() - 1)
        close_index = brace_match(masked, open_index)
        generics = [part.strip() for part in match.group("generic")[1:-1].split(",")] \
            if match.group("generic") else []
        bases = []
        if match.group("bases"):
            for part in split_top_level(match.group("bases")):
                part = re.split(r"\bwhere\b", part)[0].strip()
                if part and not part.startswith("new("):
                    bases.append(part)
        raw.append(TypeDecl(
            name=match.group("name"), kind=match.group("kind"), generics=generics, bases=bases,
            attributes=preceding_attributes(masked, declaration_start(masked, match.start())),
            span=(match.start(), close_index), body_start=open_index + 1, body_end=close_index))

    for decl in raw:
        enclosing = sorted((other for other in raw if other.span[0] < decl.span[0] < other.span[1]),
                           key=lambda d: d.span[0])
        decl.outer = [other.name for other in enclosing]
        namespace = namespace_at(decl.span[0]) or (namespace_at(enclosing[0].span[0]) if enclosing else "")
        decl.namespace = namespace
        decl.full_name = ".".join(([namespace] if namespace else []) + decl.outer + [decl.name])
        decl.file, decl.line = path, text.count("\n", 0, decl.span[0]) + 1
    return masked, raw


class SourceIndex:
    """Full-name -> TypeDecl for every C# type in the project, plus per-file lists and a simple-name index."""

    def __init__(self, defined, verbose=False):
        self.by_full, self.by_simple, self.by_file, self.masked_cache = {}, {}, {}, {}
        # Generic *arity* has to be part of the key.  `ActorMetadataMap` (non-generic, abstract, zero serialized
        # fields) and `ActorMetadataMap<TMeta>` (`string[] ids` + `TMeta[] metas`) are two different declarations
        # in one file; keying only on the name made every `...Metadata.Map` resolve to the field-less one and
        # silently consume 0 bytes.
        self.by_arity, self.by_simple_arity = {}, {}
        files = 0
        for root in SOURCE_ROOTS:
            for path in glob.iglob(os.path.join(root, "**", "*.cs"), recursive=True):
                try:
                    with io.open(path, encoding="utf-8-sig", errors="replace") as handle:
                        text = handle.read()
                    masked, decls = scan_source(text, os.path.normpath(path), defined)
                except Exception as failure:                                  # pragma: no cover - defensive
                    if verbose:
                        print(f"  ! skipped {path}: {type(failure).__name__}: {failure}")
                    continue
                files += 1
                self.masked_cache[os.path.normpath(path)] = masked
                self.by_file[os.path.normpath(path)] = decls
                for decl in decls:
                    arity = len(decl.generics or [])
                    self.by_full.setdefault(decl.full_name, decl)
                    self.by_simple.setdefault(decl.name, decl)
                    self.by_arity.setdefault((decl.full_name, arity), decl)
                    self.by_simple_arity.setdefault((decl.name, arity), decl)
        self.files_scanned = files

    def masked_for(self, path):
        return self.masked_cache.get(os.path.normpath(path)) if path else None

    def nested_spans(self, decl):
        """Body spans of types nested directly inside `decl` (their fields are not `decl`'s fields)."""
        return [other.span for other in self.by_file.get(os.path.normpath(decl.file), [])
                if decl.name in (other.outer or []) and decl.file == other.file]

    def resolve(self, type_text, scope=None):
        """Resolve a type name as written at `scope` into a TypeDecl, or None.

        The generic arity of the written type is honoured first, so `ActorMetadataMap<TMeta>` cannot be answered
        with the non-generic `ActorMetadataMap` that happens to sit earlier in the same file.
        """
        raw = re.sub(r"\s+", "", type_text or "")
        arity = 0
        generic = GENERIC_RE.match(raw)
        if generic:
            arity = len(split_top_level(generic.group(2)))
            name = generic.group(1)
        else:
            name = raw
        if not name:
            return None
        # Lookup order matters: C# resolves a bare name against the *enclosing type's* members first, then
        # outwards through the enclosing types and the namespace.  Leaving the scope's own full name out of the
        # chain made `Option[] Options` inside `CharactersConfigurationExtended` resolve to an unrelated
        # `Naninovel.CompleteGUI.Option` and lay out the wrong fields.
        prefixes = []
        if scope is not None and scope.full_name:
            prefixes.append(scope.full_name)
        scope_parts = []
        if scope is not None and scope.namespace is not None:
            scope_parts = ([scope.namespace] if scope.namespace else []) + list(scope.outer or [])
        for count in range(len(scope_parts), -1, -1):
            prefix = ".".join(scope_parts[:count])
            if prefix and prefix not in prefixes:
                prefixes.append(prefix)
        candidates = []
        for prefix in prefixes:
            candidates.append((prefix + "." + name) if prefix else name)
        if "." in name:
            candidates.append(name.rsplit(".", 1)[-1])
        candidates.append(name)
        for candidate in candidates:
            decl = self.by_arity.get((candidate, arity))
            if decl is not None:
                return decl
        if arity:
            return self.by_simple_arity.get((name.rsplit(".", 1)[-1], arity))
        for candidate in candidates:
            decl = self.by_full.get(candidate)
            if decl is not None:
                return decl
        return self.by_simple.get(name)


def iter_members(masked, decl, nested_spans):
    """Yield (attributes, head_text, terminator) for every member at the class body's top level.

    Statements are delimited the way C# delimits them, so an object initializer
    (`public X Loader = new () { ... };`) stays one statement while a method, property or nested type is a brace
    block that nothing serializable can live inside.
    """
    text, index, end = masked, decl.body_start, decl.body_end
    pending_start, attributes = index, ""
    while index < end:
        char = text[index]
        if char == "[" and not text[pending_start:index].strip():
            depth, cursor = 0, index
            while cursor < end:
                if text[cursor] == "[":
                    depth += 1
                elif text[cursor] == "]":
                    depth -= 1
                    if depth == 0:
                        break
                cursor += 1
            attributes += text[index:cursor + 1] + " "
            index = pending_start = cursor + 1
            continue
        if char in "([":
            closer = {"(": ")", "[": "]"}[char]
            depth, cursor = 0, index
            while cursor < end:
                if text[cursor] == char:
                    depth += 1
                elif text[cursor] == closer:
                    depth -= 1
                    if depth == 0:
                        break
                cursor += 1
            index = cursor + 1
            continue
        if char == ";":
            yield attributes, text[pending_start:index], ";"
            attributes, pending_start, index = "", index + 1, index + 1
            continue
        if char == "{":
            close = brace_match(text, index)
            head = text[pending_start:index]
            if any(start <= index <= stop for start, stop in nested_spans):
                yield attributes, head, "type"
                attributes, pending_start, index = "", close + 1, close + 1
                continue
            if block_member(head):
                attributes, pending_start, index = "", close + 1, close + 1
                continue
            index = close + 1          # field with an object/array initializer: run on to the statement's `;`
            continue
        index += 1


def block_member(head):
    """True when a `{`-terminated member head is a method / property / nested type rather than a field."""
    head = head.strip()
    if not head:
        return True
    if re.search(r"\b(class|struct|enum|interface)\s+[A-Za-z_]\w*\s*(<[^>]*>)?\s*(:[^{]*)?$", head):
        return True
    if "=>" in head or "(" in head.split("=")[0]:
        return True
    return "=" not in head          # `public int Foo { get; set; }` - a property


def own_serialized_fields(masked, decl, nested_spans):
    """`decl`'s own serialized fields in declaration order, plus anything that looked like a member but was not
    recognised.  Unity's rules decide membership: public instance fields always serialize, private/protected ones
    only with `[SerializeField]`, `[NonSerialized]` excludes, and `static`/`const`/`readonly` never serialize.
    """
    fields, unrecognised = [], []
    for attributes, head, terminator in iter_members(masked, decl, nested_spans):
        if terminator == "type":
            continue
        # A property or method that reached this point must not become a field.  `public string Version =>
        # engineVersion;` is the case that matters: without this guard its `=> engineVersion` parsed as an
        # initializer and the property was appended to the field list, which added two phantom strings to the
        # end of `EngineVersion`'s walk.
        if "=>" in head or "(" in head.split("=")[0]:
            continue
        # Attributes were already lifted out by iter_members, so only a *leading* block is removed here.  A
        # blanket `re.sub(r"\[[^\]]*\]", ...)` would also delete the `[]` of an array type, which silently
        # turned `Vector2Int[] ResolutionsForPc` into a single 8-byte Vector2Int.
        declaration = head.strip()
        while declaration.startswith("["):
            depth, cursor = 0, 0
            while cursor < len(declaration):
                if declaration[cursor] == "[":
                    depth += 1
                elif declaration[cursor] == "]":
                    depth -= 1
                    if depth == 0:
                        break
                cursor += 1
            declaration = declaration[cursor + 1:].strip()
        declaration = declaration.split("=")[0].strip()
        if not declaration:
            continue
        match = FIELD_RE.match(declaration)
        if not match:
            if terminator == ";" and declaration and "(" not in declaration:
                unrecognised.append(declaration[:160])
            continue
        modifiers, type_text = match.group("mods"), match.group("type").strip()
        if re.search(r"\b(static|const|readonly|event)\b", modifiers):
            continue                                        # never serialized by Unity
        if "NonSerialized" in attributes:
            continue
        # `[SerializeReference]` makes even a private field serialized - it is how `ActorMetadata.customData`
        # reaches every actor record.  Requiring `public` or `[SerializeField]` dropped it and left each
        # metadata record 8 bytes short at the field that followed.
        if "public" not in modifiers and "SerializeField" not in attributes \
                and "SerializeReference" not in attributes:
            continue
        for declarator in split_top_level(match.group("decls")):
            name = re.match(r"[A-Za-z_][A-Za-z0-9_]*", declarator.strip())
            if not name or name.group(0).startswith("<"):
                continue
            fields.append({"name": name.group(0), "type": type_text, "owner": decl.full_name,
                           "attributes": attributes.strip(), "line": decl.line})
    return fields, unrecognised


# ------------------------------------------------------------------------------------------------------------
# Kind resolution
# ------------------------------------------------------------------------------------------------------------
class KindResolver:
    """Maps a C# field type onto a serialized kind, inlining [Serializable] value types recursively."""

    def __init__(self, index):
        self.index = index
        self.cache = {}
        self.unknown = {}
        self.ignored_bases = {}
        self.non_serializable = {}

    def kinds(self, type_text, scope, generics, as_object_reference=True):
        key = (type_text, getattr(scope, "full_name", None), tuple(sorted((generics or {}).items())),
               as_object_reference)
        if key in self.cache:
            return self.cache[key]
        self.cache[key] = {"kind": "unknown", "type": type_text, "reason": "resolution in progress"}
        result = self._kind(type_text, scope, generics or {}, as_object_reference)
        self.cache[key] = result
        return result

    # -- helpers -----------------------------------------------------------------------------------------
    def substitute(self, text, generics):
        for name, value in (generics or {}).items():
            text = re.sub(r"\b" + re.escape(name) + r"\b", value, text)
        return text

    def chain(self, decl, root_generics):
        """Inheritance chain from the top-most base down to `decl`, with the generics map of each step.

        Returns (rows, object_base); `object_base` is the UnityEngine.Object base the chain stopped at (its fields
        are never serialized) or None.  A base list mixes the single class base with any number of interfaces, so
        an unresolvable name is only treated as a Unity base when it is a *known* UnityEngine type - an
        unrecognised base is recorded instead of guessed, because guessing one would shift every later field.
        """
        rows, cursor, generics, guard, object_base, special = [], decl, dict(root_generics or {}), 0, None, None
        while cursor is not None and guard < 32:
            guard += 1
            rows.append((cursor, dict(generics)))
            base_decl, base_text = None, None
            for candidate in cursor.bases or []:
                text = re.sub(r"\s+", "", re.split(r"\bwhere\b", candidate)[0])
                if not text:
                    continue
                simple = re.sub(r"<.*$", "", text).rsplit(".", 1)[-1]
                if simple in UNITY_EVENT_BASES:
                    base_decl, base_text = None, text
                    break
                resolved = self.index.resolve(text, cursor)
                if resolved is not None and resolved.kind == "class":
                    base_decl, base_text = resolved, text
                    break
                if resolved is None and simple in UNITY_OBJECT_TYPES:
                    base_decl, base_text = None, text
                    break
                self.ignored_bases.setdefault(f"{cursor.full_name} : {text}", 0)
                self.ignored_bases[f"{cursor.full_name} : {text}"] += 1
            if base_text is None:
                break
            if base_decl is None:
                if re.sub(r"<.*$", "", base_text).rsplit(".", 1)[-1] in UNITY_EVENT_BASES:
                    special = "unity_event"
                else:
                    object_base = "UnityEngine." + re.sub(r"<.*$", "", base_text).rsplit(".", 1)[-1]
                break
            generic = GENERIC_RE.match(base_text)
            if generic and base_decl.generics:
                arguments = [self.substitute(a, generics) for a in split_top_level(generic.group(2))]
                generics = dict(zip(base_decl.generics, arguments))
            else:
                generics = {}
            cursor = base_decl
        rows.reverse()
        return rows, object_base, special

    def inline(self, decl, root_generics):
        """(fields, unsupported, object_base, special) for `decl`: base classes first, then its own fields."""
        rows, object_base, special = self.chain(decl, root_generics)
        fields, unsupported = [], []
        for step, generics in rows:
            masked = self.index.masked_for(step.file)
            if masked is None:
                unsupported.append({"owner": step.full_name, "reason": "source file could not be parsed"})
                continue
            own, unrecognised = own_serialized_fields(masked, step, self.index.nested_spans(step))
            for text in unrecognised:
                unsupported.append({"owner": step.full_name, "reason": "unrecognised member", "text": text})
            for field in own:
                if "SerializeReference" in (field.get("attributes") or ""):
                    # Unity writes a managed reference id (int32) here; the polymorphic payload, when present,
                    # is a separate object whose type is not knowable from the source, so it is not guessed at.
                    field_kind = {"kind": "managed_ref", "slot": 4, "cs": field["type"]}
                else:
                    field_kind = self.kinds(field["type"], step, generics)
                row = dict(field)
                row["kind"] = field_kind
                if field_kind["kind"] == "unknown":
                    unsupported.append({"owner": step.full_name, "name": field["name"], "type": field["type"],
                                        "reason": field_kind.get("reason")})
                fields.append(row)
        return fields, unsupported, object_base, special

    # -- the mapping -------------------------------------------------------------------------------------
    def _kind(self, type_text, scope, generics, as_object_reference):
        raw = self.substitute(re.sub(r"\s+", "", type_text or ""), generics)
        if not raw:
            return {"kind": "unknown", "type": type_text, "reason": "empty type"}
        if raw in SCALARS:
            code, slot, value_kind = SCALARS[raw]
            return {"kind": code, "slot": slot, "value_kind": value_kind, "cs": raw}
        if raw in ("string", "System.String"):
            return {"kind": "string", "slot": 0, "cs": "string"}
        if raw in ("object", "System.Object"):
            return {"kind": "pptr", "slot": 12, "cs": raw, "why": "System.Object - Unity serializes it as a reference"}
        if raw.endswith("?"):
            return {"kind": "unknown", "type": raw, "reason": "Nullable<T> is not serialized by Unity"}
        if raw in NOT_SERIALIZED_NAMES:
            return {"kind": "unknown", "type": raw, "reason": f"{raw} is not serialized by Unity"}

        element = None
        type_arguments = []
        lookup = raw
        if raw.endswith("[]"):
            element = raw[:-2]
        else:
            generic = GENERIC_RE.match(raw)
            if generic:
                head, inner = generic.group(1), generic.group(2)
                short = head.rsplit(".", 1)[-1]
                if short in NOT_SERIALIZED_GENERICS:
                    return {"kind": "unknown", "type": raw,
                            "reason": f"{short}<> is not serialized by Unity"}
                if short in ("List", "IList", "IReadOnlyList", "Collection"):
                    element = split_top_level(inner)[0]
                else:
                    type_arguments = split_top_level(inner)
                lookup = head
        if element is not None:
            element_kind = self.kinds(element, scope, generics)
            if element_kind["kind"] == "unknown":
                return {"kind": "unknown", "type": raw,
                        "reason": "unsupported collection element: " + str(element_kind.get("reason"))}
            return {"kind": "array", "slot": 0, "element": element_kind, "cs": raw}

        simple = lookup.rsplit(".", 1)[-1]
        if simple in UNITY_EVENT_BASES:
            return {"kind": "unity_event", "slot": 0, "cs": raw, "why": "engine type with a measured layout"}
        decl = self.index.resolve(lookup, scope)
        if decl is None:
            if simple in UNITY_OBJECT_TYPES:
                return {"kind": "pptr", "slot": 12, "cs": raw, "why": f"known UnityEngine.Object type {simple}"}
            if simple in UNITY_ENUM_TYPES:
                return {"kind": "i32", "slot": 4, "value_kind": "int", "cs": raw,
                        "why": f"known UnityEngine enum {simple}"}
            self.unknown[simple] = lookup
            return {"kind": "unknown", "type": raw, "reason": f"no C# declaration found for {lookup!r}"}
        if decl.kind == "enum":
            underlying = re.sub(r"\s+", "", decl.bases[0]) if decl.bases else "int"
            code, slot, _ = SCALARS.get(underlying.rsplit(".", 1)[-1], ("i32", 4, "int"))
            return {"kind": code, "slot": slot, "value_kind": "int", "cs": raw, "enum": decl.full_name}
        if decl.kind == "interface":
            return {"kind": "unknown", "type": raw, "reason": f"{decl.full_name} is an interface"}

        own_generics = dict(zip(decl.generics, type_arguments)) if decl.generics else {}
        fields, unsupported, object_base, special = self.inline(decl, own_generics)
        if special is not None:
            return {"kind": special, "slot": 0, "cs": raw}
        serializable = ("Serializable" in (decl.attributes or "") or decl.kind == "struct"
                        or any("Serializable" in (step.attributes or "")
                               for step, _ in self.chain(decl, own_generics)[0]))
        if object_base is not None:
            if as_object_reference:
                return {"kind": "pptr", "slot": 12, "cs": raw, "why": f"derives from {object_base}"}
            return {"kind": "inline", "slot": 0, "cs": raw, "type": decl.full_name, "fields": fields,
                    "declared_unsupported": unsupported, "object_base": object_base}
        if not serializable:
            self.non_serializable[decl.full_name] = True
            return {"kind": "unknown", "type": raw,
                    "reason": f"{decl.full_name} is not [Serializable] and does not derive from UnityEngine.Object"}
        return {"kind": "inline", "slot": 0, "cs": raw, "type": decl.full_name, "fields": fields,
                "declared_unsupported": unsupported}


# ------------------------------------------------------------------------------------------------------------
# Byte walker
# ------------------------------------------------------------------------------------------------------------
class Unparsed(Exception):
    def __init__(self, offset, reason):
        super().__init__(reason)
        self.offset, self.reason = offset, reason


def pad4(offset):
    return offset + ((4 - offset % 4) % 4)


INT_FORMATS = {"i8": "<b", "u8": "<B", "i16": "<h", "u16": "<H", "i32": "<i", "u32": "<I", "i64": "<q", "u64": "<Q"}
# kind -> (struct format of one component, component size, component count)
VALUE_LAYOUTS = {
    "color": ("<f", 4, 4), "color32": ("<B", 1, 4),
    "vec2": ("<f", 4, 2), "vec3": ("<f", 4, 3), "vec4": ("<f", 4, 4),
    "quat": ("<f", 4, 4), "rect": ("<f", 4, 4), "rectint": ("<i", 4, 4),
    "vec2int": ("<i", 4, 2), "vec3int": ("<i", 4, 3),
}


def read_value(raw, offset, kind, depth=0, trace=None):
    """Read one value; returns (value, new_offset).  Raises Unparsed when the bytes cannot be explained."""
    if depth > 32:
        raise Unparsed(offset, "value nesting too deep")
    code = kind["kind"]
    if code == "unknown":
        raise Unparsed(offset, f"cannot lay out field type {kind.get('type')!r}: {kind.get('reason')}")
    if code == "array":
        if offset + 4 > len(raw):
            raise Unparsed(offset, "array count past the end of the object")
        (count,) = struct.unpack_from("<i", raw, offset)
        if count < 0 or count > 5_000_000:
            raise Unparsed(offset, f"implausible array length {count}")
        cursor, items = offset + 4, []
        for _ in range(count):
            item, cursor = read_value(raw, cursor, kind["element"], depth + 1, trace)
            items.append(item)
        return {"kind": "array", "type": kind.get("cs"), "count": count, "items": items}, pad4(cursor)
    if code == "inline":
        values, cursor, unparsed = walk_fields(raw, offset, kind["fields"], depth + 1, trace)
        if unparsed:
            raise Unparsed(unparsed[0]["offset"], unparsed[0]["reason"])
        return {"kind": "inline", "type": kind.get("type"), "fields": values}, cursor
    if code == "unity_event":
        return read_unity_event(raw, offset)
    if code == "managed_ref":
        # Measured on this build: a `[SerializeReference]` field takes an int64, and a negative value is Unity's
        # "no reference" sentinel (every metadata record in `ChoiceHandlersConfiguration` carries
        # 0xFFFFFFFFFFFFFFFE there).  A non-negative value is a live reference whose polymorphic payload the
        # sources cannot size, so the walk stops and says so instead of guessing.
        if offset + 8 > len(raw):
            raise Unparsed(offset, "managed reference id past the end of the object")
        (reference_id,) = struct.unpack_from("<q", raw, offset)
        if reference_id >= 0:
            raise Unparsed(offset, f"[SerializeReference] holds a live reference (id {reference_id}); the "
                                   f"polymorphic payload's type is not in the sources, so its length is unknown")
        return {"kind": "managed_ref", "value": None, "raw_id": reference_id}, offset + 8
    if code == "string":
        if offset + 4 > len(raw):
            raise Unparsed(offset, "string length past the end of the object")
        (length,) = struct.unpack_from("<i", raw, offset)
        if length < 0 or length > 4_000_000 or offset + 4 + length > len(raw):
            raise Unparsed(offset, f"implausible string length {length}")
        text = raw[offset + 4:offset + 4 + length].decode("utf-8", "replace")
        return {"kind": "string", "value": text}, pad4(offset + 4 + length)
    if code == "pptr":
        if offset + 12 > len(raw):
            raise Unparsed(offset, "PPtr past the end of the object")
        file_id, path_id = struct.unpack_from("<iq", raw, offset)
        return {"kind": "pptr", "m_FileID": file_id, "m_PathID": path_id}, offset + 12
    if code in INT_FORMATS:
        size = struct.calcsize(INT_FORMATS[code])
        if offset + size > len(raw):
            raise Unparsed(offset, "integer past the end of the object")
        value = struct.unpack_from(INT_FORMATS[code], raw, offset)[0]
        out = {"kind": "enum" if kind.get("enum") else "int", "value": value}
        if kind.get("enum"):
            out["enum_type"] = kind["enum"]
        return out, pad4(offset + size)
    if code == "bool":
        if offset + 1 > len(raw):
            raise Unparsed(offset, "bool past the end of the object")
        return {"kind": "bool", "value": bool(raw[offset])}, pad4(offset + 1)
    if code in ("f32", "f64"):
        fmt = "<f" if code == "f32" else "<d"
        if offset + 4 > len(raw) and code == "f32":
            raise Unparsed(offset, "float past the end of the object")
        if offset + 8 > len(raw) and code == "f64":
            raise Unparsed(offset, "double past the end of the object")
        return {"kind": "float", "value": round(struct.unpack_from(fmt, raw, offset)[0], 7)}, offset + struct.calcsize(fmt)
    if code in VALUE_LAYOUTS:
        fmt, step, count = VALUE_LAYOUTS[code]
        size = step * count
        if offset + size > len(raw):
            raise Unparsed(offset, f"{code} past the end of the object")
        values = [struct.unpack_from(fmt, raw, offset + index * step)[0] for index in range(count)]
        if step == 4 and fmt == "<f":
            values = [round(value, 7) for value in values]
        return {"kind": code, "value": values}, offset + size
    raise Unparsed(offset, f"no reader implemented for kind {code!r}")


def read_unity_event(raw, offset):
    """UnityEngine.Events.UnityEvent, whose layout is measured (see UNITY_EVENT_BASES) rather than sourced."""
    start = offset
    if offset + 4 > len(raw):
        raise Unparsed(offset, "UnityEvent listener count past the end of the object")
    (count,) = struct.unpack_from("<i", raw, offset)
    if count < 0 or count > 100_000:
        raise Unparsed(offset, f"implausible UnityEvent listener count {count}")
    offset += 4
    calls = []
    for _ in range(count):
        if offset + 12 > len(raw):
            raise Unparsed(offset, "UnityEvent listener m_Target past the end of the object")
        file_id, path_id = struct.unpack_from("<iq", raw, offset)
        offset += 12
        target_type, offset = read_value(raw, offset, {"kind": "string", "slot": 0})
        method, offset = read_value(raw, offset, {"kind": "string", "slot": 0})
        mode, offset = read_value(raw, offset, {"kind": "i32", "slot": 4})
        if offset + 12 > len(raw):
            raise Unparsed(offset, "UnityEvent argument m_ObjectArgument past the end of the object")
        argument_file_id, argument_path_id = struct.unpack_from("<iq", raw, offset)
        offset += 12
        argument_type, offset = read_value(raw, offset, {"kind": "string", "slot": 0})
        int_argument, offset = read_value(raw, offset, {"kind": "i32", "slot": 4})
        float_argument, offset = read_value(raw, offset, {"kind": "f32", "slot": 4})
        string_argument, offset = read_value(raw, offset, {"kind": "string", "slot": 0})
        bool_argument, offset = read_value(raw, offset, {"kind": "bool", "slot": 4})
        call_state, offset = read_value(raw, offset, {"kind": "i32", "slot": 4})
        calls.append({
            "m_Target": {"kind": "pptr", "m_FileID": file_id, "m_PathID": path_id},
            "m_TargetAssemblyTypeName": target_type["value"], "m_MethodName": method["value"],
            "m_Mode": mode["value"],
            "m_Arguments": {"m_ObjectArgument": {"kind": "pptr", "m_FileID": argument_file_id,
                                                "m_PathID": argument_path_id},
                            "m_ObjectArgumentAssemblyTypeName": argument_type["value"],
                            "m_IntArgument": int_argument["value"], "m_FloatArgument": float_argument["value"],
                            "m_StringArgument": string_argument["value"],
                            "m_BoolArgument": bool_argument["value"]},
            "m_CallState": call_state["value"]})
    return {"kind": "unity_event", "listener_count": count, "listeners": calls,
            "bytes": offset - start}, offset


def walk_fields(raw, offset, fields, depth=0, trace=None):
    """Consume `fields` from `offset`; returns (values, offset, unparsed_rows).  Stops at the first bad field so
    the reported offset is the field that could not be laid out, not a downstream symptom."""
    values, unparsed = OrderedDict(), []
    for field in fields:
        start = offset
        try:
            value, offset = read_value(raw, offset, field["kind"], depth, trace)
        except Unparsed as failure:
            if trace is not None:
                trace.append({"field": field["name"], "type": field["type"], "owner": field.get("owner"),
                              "offset": start, "end": None, "reason": failure.reason})
            unparsed.append({"field": field["name"], "field_type": field["type"], "owner": field.get("owner"),
                             "offset": failure.offset, "reason": failure.reason})
            return values, offset, unparsed
        if trace is not None:
            trace.append({"field": field["name"], "type": field["type"], "owner": field.get("owner"),
                          "offset": start, "end": offset, "bytes": offset - start,
                          "kind": field["kind"]["kind"]})
        values[field["name"]] = value
    return values, offset, unparsed


def read_managed_references_registry(raw, offset):
    """Decode Unity's trailing `ManagedReferencesRegistry`, if one sits at `offset`.

    When a class contains `[SerializeReference]` fields, Unity appends a registry node to the *object* (not to
    each field).  UnityPy special-cases exactly this node as a child of the top-level class
    (`UnityPy/helpers/TypeTreeHelper.py`: `if child.m_Type == "ManagedReferencesRegistry"`), which is why it ends
    up as trailing bytes here - the type tree that would place it is stripped from a player build.

    Measured on this build: `version` 2, `ref_count` 1, then one 20-byte entry whose leading int64 is -2 (the same
    "no managed reference" sentinel the inline `[SerializeReference]` fields carry).  Only `version`/`ref_count`
    are claimed as decoded; the remaining 12 bytes of each entry are kept verbatim as undecoded payload, so the
    report never pretends to understand more than it does.  Returns None when nothing plausible fits.
    """
    if offset + 8 > len(raw):
        return None
    version, ref_count = struct.unpack_from("<ii", raw, offset)
    if version not in (1, 2, 3, 4) or ref_count < 0 or ref_count > 10_000:
        return None
    entry_size = 20
    end = offset + 8 + ref_count * entry_size
    if end != len(raw):
        return None
    entries = []
    cursor = offset + 8
    for _ in range(ref_count):
        (rid,) = struct.unpack_from("<q", raw, cursor)
        entries.append({"m_Rid": rid, "undecoded_payload_hex": raw[cursor + 8:cursor + entry_size].hex()})
        cursor += entry_size
    return {"kind": "managed_references_registry", "m_Version": version, "m_RefCount": ref_count,
            "entries": entries, "bytes": end - offset,
            "confidence": "version and ref_count decoded; each entry's trailing 12 bytes are undecoded",
            "note": "trailing Unity structure emitted because the object contains [SerializeReference] fields"}


def walk_object(raw, fields, trace=None, registry_wanted=False):
    """Walk a whole configuration object: 28-byte header, m_Name, then the fields."""
    if len(raw) < HEADER_BYTES:
        return None, 0, [{"field": None, "field_type": None, "owner": None, "offset": 0,
                          "reason": f"object is shorter than the {HEADER_BYTES}-byte MonoBehaviour header"}]
    offset = HEADER_BYTES
    if trace is not None:
        trace.append({"field": "PPtr<GameObject> + m_Enabled + PPtr<MonoScript>", "type": "header", "owner": None,
                      "offset": 0, "end": HEADER_BYTES, "bytes": HEADER_BYTES, "kind": "header"})
    name, offset = read_value(raw, offset, {"kind": "string", "slot": 0})
    if trace is not None:
        trace.append({"field": "m_Name", "type": "string", "owner": None, "offset": HEADER_BYTES, "end": offset,
                      "bytes": offset - HEADER_BYTES, "kind": "string", "value": name["value"]})
    values, offset, unparsed = walk_fields(raw, offset, fields, 0, trace)
    values = OrderedDict([("m_Name", name)] + list(values.items()))
    if registry_wanted and not unparsed and offset < len(raw):
        registry = read_managed_references_registry(raw, offset)
        if registry is not None:
            values["_managedReferences"] = registry
            if trace is not None:
                trace.append({"field": "_managedReferences", "type": "ManagedReferencesRegistry", "owner": None,
                              "offset": offset, "end": offset + registry["bytes"], "bytes": registry["bytes"],
                              "kind": "managed_references_registry"})
            offset += registry["bytes"]
    return values, offset, unparsed


def kind_tree_has_managed_reference(fields):
    """True when any field (recursively) is a `[SerializeReference]`, i.e. the object needs a registry."""
    for field in fields:
        kind = field.get("kind") or {}
        code = kind.get("kind")
        if code == "managed_ref":
            return True
        if code == "array" and kind_tree_has_managed_reference([{"kind": kind["element"]}]):
            return True
        if code == "inline" and kind_tree_has_managed_reference(kind.get("fields") or []):
            return True
    return False


# ------------------------------------------------------------------------------------------------------------
# Shipped-build tables
# ------------------------------------------------------------------------------------------------------------
def load_monoscripts():
    """path_id -> fully-qualified `namespace.class` for every MonoScript baked into the shipped build."""
    table = {}
    with io.open(MONOSCRIPTS, encoding="utf-8-sig", newline="") as handle:
        for row in csv.DictReader(handle):
            try:
                path_id = int(row["path_id"])
            except (KeyError, ValueError):
                continue
            namespace = (row.get("namespace") or "").strip()
            class_name = (row.get("class") or "").strip()
            table[path_id] = (namespace + "." + class_name) if namespace else class_name
    return table


def load_resources_container(assets_path, objects):
    """`path_id -> Resources path` for objects in `resources.assets`, read from `ResourceManager.m_Container`.

    This is the actionable path for anything the engine loads with `Resources.Load`, and it is the only join to
    a stable asset path available for these objects.  The container stores `(m_FileID, m_PathID)` pairs, so the
    file id that denotes `resources.assets` is *derived* by scoring each candidate against the path_ids actually
    present in that file, and the winning id plus its score are reported as evidence rather than assumed.
    """
    data_dir = os.path.dirname(assets_path)
    managers = os.path.join(data_dir, "globalgamemanagers")
    if not os.path.exists(managers):
        return {"file_id": None, "by_path_id": {}, "candidates": {}, "entries": 0, "matched": 0}
    try:
        env = UnityPy.load(managers)
    except Exception:
        return {"file_id": None, "by_path_id": {}, "candidates": {}, "entries": 0, "matched": 0}
    container = []
    for obj in env.objects:
        if obj.type.name != "ResourceManager":
            continue
        try:
            read = obj.read()
        except Exception:
            continue
        for entry in (getattr(read, "m_Container", None) or []):
            try:
                key, pointer = entry
                container.append((str(key), int(pointer.m_FileID), int(pointer.m_PathID)))
            except Exception:
                continue
    candidates = {}
    for _, file_id, path_id in container:
        if path_id in objects:
            candidates[file_id] = candidates.get(file_id, 0) + 1
    best = max(candidates, key=lambda key: candidates[key]) if candidates else None
    by_path_id = {path_id: key for key, file_id, path_id in container if file_id == best}
    return {"file_id": best, "by_path_id": by_path_id, "candidates": candidates, "entries": len(container),
            "matched": candidates.get(best, 0) if best is not None else 0}


def load_catalog():
    """Addressables catalog lookup tables (address <-> internal_id), used to annotate PPtrs."""
    if not os.path.exists(CATALOG):
        return {"available": False, "by_address": {}, "by_internal": {}, "entries": []}
    with io.open(CATALOG, encoding="utf-8") as handle:
        data = json.load(handle)
    locations = data.get("locations") if isinstance(data, dict) else None
    by_address, by_internal = {}, {}
    for entry in locations or []:
        address = entry.get("address") or entry.get("primary_key") or entry.get("primaryKey")
        internal = entry.get("internal_id") or entry.get("internalId")
        if address is None or internal is None:
            continue
        by_address.setdefault(str(address), str(internal))
        by_internal.setdefault(str(internal), str(address))
    return {"available": True, "by_address": by_address, "by_internal": by_internal, "entries": locations or []}


# ------------------------------------------------------------------------------------------------------------
# Main
# ------------------------------------------------------------------------------------------------------------
def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("--write", action="store_true", help="write the JSON and Markdown outputs")
    parser.add_argument("--check", action="store_true",
                        help="compare with the committed output and fail when a decoded value changed")
    parser.add_argument("--assets", default=None, help="override the resources.assets path")
    parser.add_argument("--define", action="append", default=[],
                        help="extra C# preprocessor symbol to treat as defined (repeatable); the shipped-build "
                             "defaults are " + ", ".join(sorted(DEFAULT_DEFINED)))
    parser.add_argument("--trace", default="", metavar="PATH_IDS",
                        help="comma-separated path_ids; print the field-by-field byte layout of those objects")
    parser.add_argument("--verbose", action="store_true")
    args = parser.parse_args()

    if UnityPy is None:
        print("UnityPy is not available in this interpreter: " + sys.executable)
        return 3

    assets = args.assets or os.path.join(game_root(), "manosaba_Data", "resources.assets")
    defined = set(DEFAULT_DEFINED) | set(args.define)

    index = SourceIndex(defined, verbose=args.verbose)
    resolver = KindResolver(index)
    monoscripts = load_monoscripts()

    env = UnityPy.load(assets)
    objects = {int(obj.path_id): obj for obj in env.objects}
    raws = {path_id: obj.get_raw_data() for path_id, obj in objects.items()}
    types = {path_id: obj.type.name for path_id, obj in objects.items()}

    candidates = []
    for path_id, raw in sorted(raws.items()):
        if types[path_id] != "MonoBehaviour" or len(raw) < HEADER_BYTES:
            continue
        _, script_path_id = struct.unpack_from("<iq", raw, SCRIPT_OFFSET)
        class_name = monoscripts.get(script_path_id)
        if class_name is None:
            continue
        if class_name.endswith("Configuration") or class_name.endswith("ConfigurationExtended") \
                or class_name in EXTRA_CONFIG_CLASSES:
            candidates.append((path_id, script_path_id, class_name))
    candidates.sort(key=lambda row: (row[2], row[0]))

    def class_of(path_id):
        """The class of a PPtr target, read from the target's own m_Script (so the namespace survives)."""
        if path_id not in objects:
            return None
        if types[path_id] != "MonoBehaviour":
            return types[path_id]
        raw = raws[path_id]
        if len(raw) < HEADER_BYTES:
            return None
        _, script_path_id = struct.unpack_from("<iq", raw, SCRIPT_OFFSET)
        return monoscripts.get(script_path_id, "MonoBehaviour")

    def name_of(path_id):
        """The target's m_Name.  For MonoBehaviours it sits at the fixed header offset; for other types Unity's
        NamedObject layout starts with m_Name, but that is a convention rather than a guarantee, so the read is
        sanity-checked and reported unreliable when it does not look like a string."""
        if path_id not in objects:
            return None, False
        raw = raws[path_id]
        offset = HEADER_BYTES if types[path_id] == "MonoBehaviour" else 0
        if offset + 4 > len(raw):
            return None, False
        (length,) = struct.unpack_from("<i", raw, offset)
        if length < 0 or length > 512 or offset + 4 + length > len(raw):
            return None, False
        text = raw[offset + 4:offset + 4 + length]
        if length and any(byte < 9 or (13 < byte < 32) for byte in text):
            return None, False
        try:
            return text.decode("utf-8"), True
        except UnicodeDecodeError:
            return None, False

    catalog = load_catalog()
    container = load_resources_container(assets, objects)
    trace_ids = {int(part) for part in args.trace.split(",") if part.strip().isdigit()} if args.trace else set()

    doc_objects = []
    for path_id, script_path_id, class_name in candidates:
        raw = raws[path_id]
        trace_wanted = path_id in trace_ids
        # The layout trace is always collected: it is the evidence for every decoded value (field, byte range,
        # kind) and it is what makes the numbers in the Markdown summary checkable.  Only printing is optional.
        trace = []
        entry = OrderedDict()
        entry["path_id"] = path_id
        entry["m_Script"] = script_path_id
        entry["class"] = class_name
        entry["bytes_total"] = len(raw)
        # The `ResourceManager.m_Container` key is the exact `Resources.Load` path - for 25 of these it is
        # `naninovel/configuration/<lowercased class name>`, matching ProjectConfigurationProvider's
        # `Resources.Load("Naninovel/Configuration/<TypeName>")`.  That is an independent confirmation that the
        # object decoded here really is the configuration of this class.
        entry["resources_path"] = container["by_path_id"].get(path_id)
        decl = index.resolve(class_name)
        if decl is None:
            entry.update({"status": "NO_SOURCE", "error": "no C# declaration found for this class",
                          "bytes_consumed": None, "closes_exactly": False, "values": {}, "unparsed": [
                              {"field": None, "field_type": None, "owner": None, "offset": None,
                               "reason": "no C# declaration found for " + class_name}]})
            doc_objects.append(entry)
            continue
        entry["source"] = os.path.relpath(decl.file, REPO).replace("\\", "/")
        entry["source_line"] = decl.line
        kind = resolver.kinds(class_name, None, {}, as_object_reference=False)
        if kind["kind"] != "inline":
            entry.update({"status": "NOT_INLINE", "bytes_consumed": None, "closes_exactly": False, "values": {},
                          "unparsed": [{"field": None, "field_type": None, "owner": class_name, "offset": None,
                                        "reason": f"class resolved to kind {kind['kind']!r}, expected an "
                                                  f"inlinable class: {kind.get('reason') or kind.get('why')}"}]})
            doc_objects.append(entry)
            continue
        entry["object_base"] = kind.get("object_base")
        entry["fields"] = [{"name": field["name"], "type": field["type"], "owner": field.get("owner"),
                            "line": field.get("line")} for field in kind["fields"]]
        entry["declared_unsupported"] = kind.get("declared_unsupported") or []
        needs_registry = kind_tree_has_managed_reference(kind["fields"])
        values, offset, unparsed = walk_object(raw, kind["fields"], trace, registry_wanted=needs_registry)
        strict_offset = offset - (values.get("_managedReferences", {}).get("bytes", 0)
                                  if isinstance(values, dict) else 0)
        entry["values"] = values
        entry["unparsed"] = unparsed
        entry["bytes_consumed"] = offset
        entry["bytes_consumed_strict"] = strict_offset
        entry["closes_exactly"] = strict_offset == len(raw)
        entry["closes_with_registry"] = offset == len(raw)
        entry["field_layout"] = trace
        if trace_wanted:
            print(f"  --- layout of {class_name} (path_id {path_id}, {len(raw)} bytes) ---")
            for row in trace:
                where = f"0x{row['offset']:04x}" if row.get("offset") is not None else "     "
                span = f"..0x{row['end']:04x}" if row.get("end") is not None else "..  ?  "
                print(f"    {where}{span} {str(row.get('bytes', '')):>5}B  "
                      f"{row['field']} : {row['type']}" + (f"  <-- {row['reason']}" if row.get("reason") else ""))
            print(f"    declared fields end at {strict_offset} of {len(raw)} bytes; walk ends at {offset} -> "
                  f"{'CLOSES EXACTLY' if entry['closes_exactly'] else ('CLOSES WITH REGISTRY' if entry['closes_with_registry'] else 'DOES NOT CLOSE')}")
        if unparsed:
            entry["status"] = "UNPARSED_FIELDS"
        elif offset > len(raw):
            entry["status"] = "OVERRUN"
        elif strict_offset == len(raw):
            entry["status"] = "OK"
        elif entry["closes_with_registry"]:
            entry["status"] = "OK_TRAILING_REGISTRY"
            entry["trailing_registry_bytes"] = len(raw) - strict_offset
        else:
            entry["status"] = "TAIL"
            entry["unparsed_tail_bytes"] = len(raw) - offset

        references = []

        def annotate(value, path):
            if not isinstance(value, dict):
                return
            if value.get("kind") == "pptr":
                file_id, target = value["m_FileID"], value["m_PathID"]
                resolved = file_id == 0 and target in objects
                value["resolves_in_file"] = resolved
                value["target_class"] = class_of(target) if resolved else None
                if resolved:
                    target_name, reliable = name_of(target)
                    value["target_name"] = target_name
                    value["target_name_reliable"] = reliable
                else:
                    value["target_name"], value["target_name_reliable"] = None, False
                row = {"field": path, "m_FileID": file_id, "m_PathID": target,
                       "target_class": value["target_class"], "target_name": value["target_name"]}
                # the Resources path is the actionable identifier for an asset inside resources.assets
                row["resources_path"] = container["by_path_id"].get(target) if file_id == 0 else None
                if catalog["available"]:
                    # A catalog internal_id is only recorded on an exact textual match; the catalog carries no
                    # path_id field and resources.assets is not Addressables content, so this is normally None
                    # and that is reported rather than papered over.
                    row["catalog_internal_id"] = None
                    for candidate_key in (row["resources_path"], value["target_name"]):
                        if candidate_key and candidate_key in catalog["by_address"]:
                            row["catalog_internal_id"] = catalog["by_address"][candidate_key]
                            break
                references.append(row)
            elif value.get("kind") == "array":
                for position, item in enumerate(value.get("items") or []):
                    annotate(item, f"{path}[{position}]")
            elif value.get("kind") == "inline":
                for key, item in (value.get("fields") or {}).items():
                    annotate(item, f"{path}.{key}")
            elif value.get("kind") == "unity_event":
                for position, listener in enumerate(value.get("listeners") or []):
                    annotate(listener.get("m_Target"), f"{path}.listeners[{position}].m_Target")
                    annotate((listener.get("m_Arguments") or {}).get("m_ObjectArgument"),
                             f"{path}.listeners[{position}].m_Arguments.m_ObjectArgument")

        for field_name, value in values.items():
            annotate(value, field_name)
        entry["references"] = references
        entry["reference_count"] = len(references)
        doc_objects.append(entry)
    statuses = {}
    for entry in doc_objects:
        statuses[entry["status"]] = statuses.get(entry["status"], 0) + 1
    document = OrderedDict()
    document["generated_by"] = "tools/unity-rebuild/decode_naninovel_configs.py"
    document["assets_file"] = assets
    document["assets_bytes"] = os.path.getsize(assets) if os.path.exists(assets) else None
    document["source_files_scanned"] = index.files_scanned
    document["header_bytes"] = HEADER_BYTES
    document["preprocessor_defined"] = sorted(defined)
    document["summary"] = OrderedDict([
        ("objects", len(doc_objects)),
        ("closed_exactly", sum(1 for entry in doc_objects if entry.get("closes_exactly"))),
        ("closed_with_trailing_registry",
         sum(1 for entry in doc_objects if entry.get("closes_with_registry") and not entry.get("closes_exactly"))),
        ("not_closed", sum(1 for entry in doc_objects
                           if not entry.get("closes_with_registry"))),
        ("statuses", dict(sorted(statuses.items()))),
        ("catalog_available", catalog["available"]),
        ("catalog_join_note", CATALOG_JOIN_NOTE),
        ("resources_container", {"file_id": container["file_id"], "entries": container["entries"],
                                 "matched_path_ids": container["matched"], "candidates": container["candidates"]}),
        ("unknown_types", dict(sorted(resolver.unknown.items()))),
        ("ignored_base_names", dict(sorted(resolver.ignored_bases.items()))),
        ("non_serializable_types", sorted(resolver.non_serializable)),
    ])
    document["objects"] = doc_objects

    print(f"resources.assets: {assets}")
    print(f"C# source files scanned: {index.files_scanned}")
    print(f"configuration objects: {len(doc_objects)}  closed exactly: {document['summary']['closed_exactly']}"
          f"  closed with trailing registry: {document['summary']['closed_with_trailing_registry']}"
          f"  not closed: {document['summary']['not_closed']}")
    print(f"ResourceManager.m_Container: file_id={container['file_id']} entries={container['entries']} "
          f"matched_path_ids={container['matched']} (candidates {container['candidates']})")
    for entry in doc_objects:
        marker = "OK" if entry["status"] == "OK" else entry["status"]
        detail = ""
        if entry["status"] != "OK":
            if entry.get("error"):
                detail += "  " + entry["error"]
            if entry.get("unparsed"):
                first = entry["unparsed"][0]
                detail += f"  [@0x{first['offset']:x} {first.get('field')}: {first['reason']}]" \
                    if isinstance(first.get("offset"), int) else f"  [{first['reason']}]"
            if entry.get("unparsed_tail_bytes"):
                detail += f"  [{entry['unparsed_tail_bytes']} trailing bytes]"
        print(f"  {marker:<17} {entry['class']:<70} {entry['bytes_total']:>6} B{detail}")
    if resolver.unknown:
        print("unknown C# types: " + ", ".join(sorted(resolver.unknown)))
    if resolver.ignored_bases:
        print("bases ignored (interfaces/unknown): " + ", ".join(sorted(resolver.ignored_bases)))

    if args.write:
        os.makedirs(OUT_DIR, exist_ok=True)
        with io.open(OUT_JSON, "w", encoding="utf-8") as handle:
            json.dump(document, handle, ensure_ascii=False, indent=1)
            handle.write("\n")
        write_markdown(document)
        print("wrote " + OUT_JSON)
        print("wrote " + OUT_MD)

    if args.check:
        return check_against_previous(document)
    return 0


def check_against_previous(document):
    """Fail (exit 1) when a value decoded before is no longer equal."""
    if not os.path.exists(OUT_JSON):
        print("--check: no previous output at " + OUT_JSON)
        return 1
    with io.open(OUT_JSON, encoding="utf-8") as handle:
        previous = json.load(handle)
    before = {entry["path_id"]: entry for entry in previous.get("objects", [])}
    after = {entry["path_id"]: entry for entry in document["objects"]}
    problems = []
    for path_id in sorted(set(before) | set(after)):
        old, new = before.get(path_id), after.get(path_id)
        label = (new or old)["class"]
        if old is None:
            problems.append(f"+{path_id} {label}: newly present")
        elif new is None:
            problems.append(f"-{path_id} {label}: disappeared")
        elif old.get("values") != new.get("values"):
            problems.append(f"~{path_id} {label}: decoded values changed")
        elif old.get("bytes_consumed") != new.get("bytes_consumed") or \
                old.get("closes_exactly") != new.get("closes_exactly"):
            problems.append(f"~{path_id} {label}: walk extent changed "
                            f"({old.get('bytes_consumed')} -> {new.get('bytes_consumed')})")
    if problems:
        print("--check FAILED:")
        for problem in problems:
            print("  " + problem)
        return 1
    print("--check OK: all decoded values match " + os.path.relpath(OUT_JSON, REPO))
    return 0


def preview(value, budget=24):
    """Trim very large arrays for the Markdown summary; the JSON keeps everything."""
    if isinstance(value, dict):
        if value.get("kind") == "array" and value.get("count", 0) > budget:
            return {"kind": "array", "type": value.get("type"), "count": value["count"],
                    "items_preview": [preview(item, budget) for item in value["items"][:4]],
                    "note": f"truncated here; all {value['count']} items are in the JSON output"}
        return {key: preview(item, budget) for key, item in value.items()}
    if isinstance(value, list):
        return [preview(item, budget) for item in value]
    return value


def write_markdown(document):
    """A *summary*: status per object, the content-deciding values, and every unparsed remainder.  The full value
    tree (including the per-field byte layout) lives in the JSON next to it."""
    summary = document["summary"]
    lines = ["# Shipped Naninovel configuration assets", "",
             "Decoded from `" + str(document["assets_file"]) + "` by "
             "`tools/unity-rebuild/decode_naninovel_configs.py`.", "",
             f"- configuration objects: **{summary['objects']}**",
             f"- byte walk closed exactly at the last declared field: **{summary['closed_exactly']}**",
             f"- closed only after the trailing `ManagedReferencesRegistry`: "
             f"**{summary['closed_with_trailing_registry']}**",
             f"- did not close: **{summary['not_closed']}**",
             f"- C# source files scanned: {document['source_files_scanned']}",
             f"- preprocessor symbols treated as defined: "
             f"{', '.join(document['preprocessor_defined']) or '(none)'}", "",
             "## Objects", "",
             "| path_id | m_Script | class | bytes | consumed | strict | status |", "|---|---|---|---|---|---|---|"]
    for entry in document["objects"]:
        lines.append(f"| {entry['path_id']} | {entry['m_Script']} | `{entry['class']}` | {entry['bytes_total']} | "
                     f"{entry.get('bytes_consumed')} | {entry.get('bytes_consumed_strict')} | {entry['status']} |")
    lines.append("")
    with_path = [entry for entry in document["objects"] if entry.get("resources_path")]
    lines += [f"`ResourceManager.m_Container` (Resources.Load) keys resolved for {len(with_path)} of "
              f"{len(document['objects'])} objects, e.g.:", ""]
    for entry in document["objects"]:
        if entry.get("resources_path"):
            lines.append(f"- {entry['path_id']} `{entry['class']}` -> `{entry['resources_path']}`")
    lines.append("")

    def value_of(class_name, field, occurrence=0):
        hits = [entry for entry in document["objects"] if entry["class"] == class_name]
        if len(hits) <= occurrence:
            return None
        return (hits[occurrence]["values"] or {}).get(field)

    lines += ["## Values that decide what content the engine loads", "",
              "Every value below is quoted with the byte range it was read from, taken from the object's",
              "`field_layout`.", ""]
    headline = [
        ("Naninovel.ScriptsConfiguration", "TitleScript"), ("Naninovel.ScriptsConfiguration", "StartGameScript"),
        ("Naninovel.ScriptsConfiguration", "InitializationScript"), ("Naninovel.ScriptsConfiguration", "Loader"),
        ("Naninovel.ScriptsConfiguration", "ScriptParser"),
        ("Naninovel.ScriptsConfiguration", "CompilerLocalization"),
        ("Naninovel.ScriptsConfiguration", "EnableCommunityModding"),
        ("Naninovel.ResourceProviderConfiguration", "ResourcePolicy"),
        ("Naninovel.ResourceProviderConfiguration", "EnableBuildProcessing"),
        ("Naninovel.ResourceProviderConfiguration", "UseAddressables"),
        ("Naninovel.ResourceProviderConfiguration", "AutoBuildBundles"),
        ("Naninovel.ResourceProviderConfiguration", "ExtraLabels"),
        ("Naninovel.ResourceProviderConfiguration", "ProjectRootPath"),
        ("Naninovel.ResourceProviderConfiguration", "LocalRootPath"),
        ("Naninovel.EngineConfiguration", "InitializeOnApplicationLoad"),
        ("Naninovel.EngineConfiguration", "ShowInitializationUI"),
        ("Naninovel.EngineConfiguration", "CustomInitializationUI"),
        ("Naninovel.LocalizationConfiguration", "Loader"),
        ("Naninovel.LocalizationConfiguration", "Languages"),
        ("Naninovel.LocalizationConfiguration", "SourceLocale"),
        ("Naninovel.LocalizationConfiguration", "DefaultLocale"),
        ("Naninovel.CharactersConfiguration", "Metadata"),
        ("Naninovel.CharactersConfiguration", "DefaultMetadata"),
        ("Naninovel.CharactersConfiguration", "AvatarLoader"),
        ("Naninovel.CharactersConfiguration", "SharedPoses"),
        ("Naninovel.BackgroundsConfiguration", "Metadata"),
        ("Naninovel.TextPrintersConfiguration", "Metadata"),
        ("Naninovel.ChoiceHandlersConfiguration", "Metadata"),
        ("Naninovel.ProjectStats", "TotalScriptsCount"),
        ("Naninovel.EngineVersion", "engineVersion"),
        ("Naninovel.EngineVersion", "buildDate"),
    ]
    for class_name, field in headline:
        entry = next((row for row in document["objects"] if row["class"] == class_name), None)
        if entry is None:
            continue
        value = (entry["values"] or {}).get(field)
        if value is None:
            continue
        spans = [row for row in entry.get("field_layout", []) if row.get("field") == field]
        where = f"0x{spans[0]['offset']:04x}..0x{spans[-1]['end']:04x}" if spans and spans[0].get("offset") is not None \
            else "n/a"
        rendered = json.dumps(preview(value), ensure_ascii=False)
        if len(rendered) > 700:
            rendered = rendered[:700] + " ...(truncated; full value in the JSON)"
        lines.append(f"- `{class_name}.{field}` @ {where} ({value.get('kind')}): `{rendered}`")
    lines.append("")

    lines += ["## Unparsed remainders", ""]
    any_unparsed = False
    for entry in document["objects"]:
        for row in entry.get("unparsed", []) or []:
            any_unparsed = True
            lines.append(f"- `{entry['class']}` (path_id {entry['path_id']}): field `{row.get('field')}` "
                         f"({row.get('field_type')}) at offset {row.get('offset')} - {row.get('reason')}")
    if not any_unparsed:
        lines.append("- none: no field was left undecoded in any of the "
                     f"{summary['objects']} objects")
    lines.append("")

    registry = [entry for entry in document["objects"] if entry.get("trailing_registry_bytes")]
    lines += ["## Trailing `ManagedReferencesRegistry`", "",
              f"{len(registry)} object(s) carry a trailing structure after their last declared field:", ""]
    for entry in registry:
        read = (entry["values"] or {}).get("_managedReferences") or {}
        lines.append(f"- `{entry['class']}` (path_id {entry['path_id']}): {entry['trailing_registry_bytes']} bytes "
                     f"at offset {entry.get('bytes_consumed_strict')}, m_Version={read.get('m_Version')}, "
                     f"m_RefCount={read.get('m_RefCount')}. {read.get('confidence')}.")
    lines += ["",
              "These are the only objects in the whole file with that suffix, and `Naninovel.ActorMetadata` is the",
              "only `[SerializeReference]`-bearing type present, so the structure is Unity's managed-reference",
              "bookkeeping rather than project data. See `catalog_pathid_report.md` and the tool docstring.", "",
              "## Reference annotation", "",
              document["summary"]["catalog_join_note"], ""]

    lines += ["## Objects that did not close exactly", ""]
    bad = [entry for entry in document["objects"] if not entry.get("closes_with_registry")]
    if not bad:
        lines.append("- none")
    for entry in bad:
        lines.append(f"- `{entry['class']}` (path_id {entry['path_id']}): {entry['status']} - "
                     f"{entry.get('error') or (entry.get('unparsed') or [{}])[0].get('reason', '')}")
    lines.append("")
    with io.open(OUT_MD, "w", encoding="utf-8") as handle:
        handle.write("\n".join(lines))


if __name__ == "__main__":
    sys.exit(main())
