"""Plan the import of the materials the extractor could not derive a path for (the "Uncategorized" ones).

    python tools/unity-rebuild/plan_material_import.py            # report to stdout
    python tools/unity-rebuild/plan_material_import.py --write     # also write out/unity/material_import_plan.json

Why this exists
---------------
The completed Naninovel *configuration* import (`NaninovelConfigBuilder.cs`,
`out/unity/naninovel_config_import.json`) closed 163 of the 200 PPtr nodes in the shipped configuration
objects and left **37 unbound**. 34 of those 37 name the same two objects:

    25 x Material "MaskableTransparent"        (shipped resources.assets path_id 2)
     9 x Material "MaskableTransparentToned_1" (shipped resources.assets path_id 1)

They are pointed at by `BackgroundsConfiguration.Metadata.metas[0..2].CustomSpriteMaterial` and
`CharactersConfiguration.Metadata.metas[*].CustomSpriteMaterial`, i.e. by every background and character
record the engine reads when it builds a `TransitionalSpriteRenderer`
(`Runtime/Transition/TransitionalRenderer.cs:82` -> `TransitionalSpriteRenderer.Initialize(..., customSpriteMaterial)`
-> `SpriteMaterial = customSpriteMaterial ? new(customSpriteMaterial) : new(Shader.Find("Hidden/Naninovel/Transparent"))`
at `TransitionalSpriteRenderer.cs:72`). Their absence is therefore a rendering difference, not a cosmetic one:
the actor falls back to Naninovel's own default sprite shader.

The two materials are *not* in `UnityProject/Assets` or `UnityProject/Packages`, but they are in the
extraction, as AssetRipper typetree JSON with no derivable asset path (hence "Uncategorized"):

    res/Uncategorized/Material/MaskableTransparent.json          resources.assets path_id 2
    res/Uncategorized/Material/MaskableTransparentToned_1.json   resources.assets path_id 1

This tool is the *plan* half: it reads those typetree files, resolves what it can against this project
(the shader above all), and writes the plan the Editor-side importer (`Assets/Editor/MaterialImporter.cs`)
consumes. It never invents: an unresolved shader, an unresolved texture reference or an unknown property
kind is recorded as such and never substituted.

What "resolved" means here
--------------------------
Two different reference spaces have to be kept apart, and confusing them is how a wrong asset gets bound:

  * A shipped PPtr is `(m_FileID, m_PathID)` inside the *shipped* serialized files. `m_FileID == 0` means
    "same file as the material". For `res/Uncategorized/Material/*.json` that file is `resources.assets`,
    whose object table *is* in `res/_index.csv`, so a same-file PPtr can be resolved through the index:
    `MaskableTransparent.m_Shader = (0, 22)` -> the index row `Shader / resources.assets / pathid_22.json`,
    whose typetree carries the shader name in `m_ParsedForm.m_Name` ("Naninovel Extender/Maskable Transparent").
    `MaskableTransparentToned_1.m_Shader = (0, 32)` resolves the same way ("... Toned").
  * `m_FileID > 0` indexes the file's *externals* table, which was not dumped, so a foreign-file PPtr cannot
    be resolved at all - not by this tool and not by any tool working from this extraction. Two of the seven
    uncategorized materials are in that position (`Font Material` and the `globalgamemanagers.assets`
    `Sprites-Default`, both `m_Shader.m_FileID == 2`); they are reported as UNRESOLVED_EXTERNAL_FILE, never
    guessed from a same-sounding shader in the project.

An *asset path* is then resolved separately: the recorded shader name is looked up in this project's own
shader assets (every `*.shader` file under `UnityProject/Assets` and `UnityProject/Packages`, read for its
ShaderLab `Shader "<name>"` declaration). A name that matches no project asset is SHADER_NOT_FOUND, and that
is a fact about this project, not a failure of the extractor: many of the game's shaders live in packages the
reconstruction does not have at all (the "Naninovel Extender" materials belong to
`GigaCreation.NaninovelExtender.*`, whose assemblies exist in `dist/` but whose package is not embedded here)
and a shipped shader's *source* is never recoverable anyway - the extractor only dumps compiled GPU programs.

Scope of the "how big is this problem" question
----------------------------------------------
`res/_index.csv` holds 155 `Material` rows. No `.mat` asset exists anywhere under `UnityProject/Assets` or
the embedded `UnityProject/Packages` that carries a name one of those 155 declares, and
`Assets/GameContent` - the folder `a13_import_assets.py` writes imported content to - holds no material at
all (`a13_import_assets.py` classifies Material as DEFERRED_BY_TYPE because "it needs its shader, which is
not recoverable from the shipped build"). All 155 are therefore unimported: 148 of them with a usable
`asset_path` and 7 with none (the seven files this tool reads). Which of the 155 anything we already
*imported* actually references is answered from the same artifacts the reconstructors wrote:

  * the configuration import's `needs_binding` records (`out/unity/naninovel_config_import.json`)
  * the material PPtrs of every prefab job the builder consumed (`out/unity/prefab_plan_all.json`, by reading
    each job's object dumps for `m_Materials`).  Two kinds of hit are kept apart: a pointer whose `m_FileID`
    is 0 names an object of the job's own bundle and resolves through `(bundle, path_id)`, while a pointer
    into another serialized file can only be *identified* (its path_id occurs exactly once in the index) -
    never bound, because which file the index refers to was not dumped.

Output (deterministic ordering)
-------------------------------
  tools/unity-rebuild/out/unity/material_import_plan.json   per-material plan for the Editor importer
  stdout                                                    the same facts, human-readable
"""
from __future__ import annotations

import argparse
import csv
import datetime
import json
import os
import re
import sys
from collections import Counter, OrderedDict, defaultdict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
RES = os.path.join(REPO, "res")
INDEX = os.path.join(RES, "_index.csv")
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
OUT_JSON = os.path.join(OUT_DIR, "material_import_plan.json")
UNITY = os.path.join(REPO, "UnityProject")
UNITY_ASSETS = os.path.join(UNITY, "Assets")
UNITY_PACKAGES = os.path.join(UNITY, "Packages")
CONTENT = os.path.join(UNITY_ASSETS, "GameContent")

CONFIG_IMPORT = os.path.join(OUT_DIR, "naninovel_config_import.json")
PREFAB_PLAN = os.path.join(OUT_DIR, "prefab_plan_all.json")

UNCATEGORIZED_MATERIAL_DIR = os.path.join(RES, "Uncategorized", "Material")
UNCATEGORIZED_SHADER_DIR = os.path.join(RES, "Uncategorized", "Shader")

#: Where the created assets go.  Derived from the importer's own convention rather than invented:
#: `a13_import_assets.py:dest_for` writes every extracted object under
#: `UnityProject/Assets/GameContent/<asset_path minus "Assets/">`, which is why the assets the extractor could
#: not place already live under `Assets/GameContent/Uncategorized/{Font,Sprite,TextAsset,Texture2D}`.  An
#: Uncategorized *material* therefore belongs in `Assets/GameContent/Uncategorized/Material`, which is also the
#: only folder in this project whose contents are, by construction, the same set as `res/Uncategorized/`.
#: The shipped build carried these two materials where the shader's own package put them, not in a Resources
#: folder of the game, and nothing in *this* project looks them up by path: the config records that point at
#: them are object references, not strings (see `limitations`).
TARGET_FOLDER = "Assets/GameContent/Uncategorized/Material"

#: `m_SavedProperties` keys this loader understands.  Anything else is reported as an unknown kind.
SAVED_PROPERTY_KEYS = OrderedDict((
    ("m_TexEnvs", "texenv"),
    ("m_Ints", "int"),
    ("m_Floats", "float"),
    ("m_Colors", "color"),
))

#: The AssetRipper typetree writes UnityEngine's default-resource references as m_FileID == 0 with these
#: path_ids.  They are not objects of the file being read; recorded as such, never resolved.
DEFAULT_RESOURCE_PATH_IDS = {0: "null reference (shipped field is empty)"}

SHADER_DECL = re.compile(r'^\s*Shader\s+"([^"]+)"', re.MULTILINE)
MATERIAL_PTR = re.compile(r'"m_Materials"\s*:\s*\[(.*?)\]', re.S)
PTR = re.compile(r'"m_FileID"\s*:\s*(-?\d+)\s*,\s*"m_PathID"\s*:\s*(-?\d+)')


# ------------------------------------------------------------------------------------------------------------
# extraction tables
# ------------------------------------------------------------------------------------------------------------

def load_index():
    """`res/_index.csv` -> (rows, (bundle-basename-lower, path_id) -> row).

    The bundle is reduced to its basename because the index stores it Windows-style
    (`StreamingAssets\\aa\\StandaloneWindows64\\...bundle`) while the planners key on the name.
    """
    rows = []
    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            row["type"] = (row.get("type") or "").strip()
            row["name"] = (row.get("name") or "").strip()
            row["asset_path"] = (row.get("asset_path") or "").strip()
            row["output_file"] = (row.get("output_file") or "").strip()
            row["source_bundle"] = (row.get("source_bundle") or "").strip()
            row["path_id"] = (row.get("path_id") or "").strip()
            rows.append(row)
    by_bundle_pid = {}
    for row in rows:
        bundle = os.path.basename(row["source_bundle"].replace("\\", "/")).lower()
        if bundle and row["path_id"]:
            by_bundle_pid.setdefault((bundle, row["path_id"]), row)
    return rows, by_bundle_pid


def rows_by_type_and_pid(rows, wanted_type):
    """(type, path_id) -> [rows], for the cross-bundle fallback lookup."""
    table = defaultdict(list)
    for row in rows:
        if row["type"] == wanted_type and row["path_id"]:
            table[row["path_id"]].append(row)
    return table



def resolve_same_file(path_id, by_bundle_pid, bundle_basename):
    """A `m_FileID == 0` PPtr of a material file -> the index row it pointed at, or None."""
    return by_bundle_pid.get((bundle_basename, str(path_id)))


def read_json(path):
    with open(path, encoding="utf-8") as fh:
        return json.load(fh)


# ------------------------------------------------------------------------------------------------------------
# project shader catalogue
# ------------------------------------------------------------------------------------------------------------

def project_shader_name(text):
    """The ShaderLab name a `.shader` file declares, or None when the file declares none.

    Only the first `Shader "..."` declaration is read; a `.shader` that is really a ShaderGraph wrapper or a
    malformed file simply yields None and is then a file this tool cannot name - which is reported, because a
    shader whose declared name cannot be read must never silently count as "the shader is here".
    """
    match = SHADER_DECL.search(text)
    return match.group(1) if match else None


def collect_project_shaders():
    """Every `.shader` under Assets/ and Packages/, keyed by declared name.

    Returns (name -> sorted asset paths, files read, files whose declared name could not be read).  A shader
    whose `Shader "..."` declaration cannot be read is reported rather than treated as "not here", because the
    two mean different things.
    """
    by_name = defaultdict(list)
    unreadable = []
    files = 0
    for root in (UNITY_PACKAGES, UNITY_ASSETS):
        for dirpath, _dirnames, filenames in os.walk(root):
            for filename in filenames:
                if not filename.endswith(".shader"):
                    continue
                full = os.path.join(dirpath, filename)
                files += 1
                asset_path = os.path.relpath(full, UNITY).replace("\\", "/")
                try:
                    with open(full, encoding="utf-8", errors="replace") as fh:
                        name = project_shader_name(fh.read())
                except OSError as exc:
                    unreadable.append("%s: %s" % (asset_path, exc))
                    continue
                if name:
                    by_name[name].append(asset_path)
                else:
                    unreadable.append("%s: no `Shader \"...\"` declaration found" % asset_path)
    for name in by_name:
        by_name[name].sort()
    return by_name, files, sorted(unreadable)


def collect_extracted_shaders():
    """`res/Uncategorized/Shader/*.json` -> {path_id: {name, guid_raw, file, ...}}.

    The typetree's own `m_Name` is empty for every one of these objects (the extractor had no asset path to
    name them by); the *shader* name lives in `m_ParsedForm.m_Name`.  `m_AssetGUID` is recorded verbatim
    because its shape here is four `data[i]` integers, which is not enough to state a 32-hex GUID - so no
    GUID string is produced, deliberately.
    """
    shaders = {}
    for filename in sorted(os.listdir(UNCATEGORIZED_SHADER_DIR)):
        if not filename.endswith(".json"):
            continue
        full = os.path.join(UNCATEGORIZED_SHADER_DIR, filename)
        try:
            doc = read_json(full)
        except (OSError, ValueError) as exc:
            shaders[filename] = {"file": filename, "error": str(exc)}
            continue
        parsed = doc.get("m_ParsedForm") or {}
        shaders[filename] = {
            "file": filename,
            "output_file": "Uncategorized/Shader/" + filename,
            "m_Name": doc.get("m_Name") or "",
            "shader_name": parsed.get("m_Name") or "",
            "asset_guid_raw": doc.get("m_AssetGUID"),
            "shader_is_baked": doc.get("m_ShaderIsBaked"),
            "dependencies": doc.get("m_Dependencies") or [],
            "fallback_name": parsed.get("m_FallbackName") or "",
            "custom_editor": parsed.get("m_CustomEditorName") or "",
            "property_names": [p.get("m_Name") for p in (parsed.get("m_PropInfo") or {}).get("m_Props", [])],
            "subshader_count": len(parsed.get("m_SubShaders") or []),
        }
    return shaders


# ------------------------------------------------------------------------------------------------------------
# one material
# ------------------------------------------------------------------------------------------------------------

def describe_shader_reference(shader_pptr, shaders_by_path_id, material_bundle):
    """Turn the material's `m_Shader` PPtr into a reportable record.

    Returns a dict with: file_id, path_id, resolution ("resolved" | "unresolved_external_file" |
    "unresolved_missing_from_index"), the extracted shader's own record when it resolved, and the raw GUID.
    """
    file_id = int(shader_pptr.get("m_FileID", 0))
    path_id = int(shader_pptr.get("m_PathID", 0))
    record = {
        "file_id": file_id,
        "path_id": path_id,
        "file_id_meaning": ("same file as the material (%s)" % material_bundle) if file_id == 0 else
                           "index %d of the material file's externals table, which the extraction did not dump" % file_id,
    }
    if file_id != 0:
        record["resolution"] = "unresolved_external_file"
        record["reason"] = ("the shader lives in another serialized file (m_FileID %d); the externals table of "
                            "%s was not extracted, so no path_id mapping can ever name it. This is the same class "
                            "of gap the configuration import reported for m_FileID > 0 references."
                            % (file_id, material_bundle))
        return record

    row = shaders_by_path_id.get(str(path_id))
    if row is None:
        record["resolution"] = "unresolved_missing_from_index"
        record["reason"] = ("path_id %s of %s is not a row of res/_index.csv, so the extraction does not carry "
                            "the object this material's shader pointer names" % (path_id, material_bundle))
        return record

    shader = None
    if row.get("output_file"):
        candidate = os.path.join(RES, row["output_file"].replace("/", os.sep))
        if os.path.exists(candidate):
            try:
                doc = read_json(candidate)
            except (OSError, ValueError) as exc:
                record["resolution"] = "unresolved_unreadable"
                record["reason"] = "the extracted shader file could not be read: %s" % exc
                return record
            parsed = doc.get("m_ParsedForm") or {}
            shader = {
                "output_file": row["output_file"],
                "m_Name": doc.get("m_Name") or "",
                "shader_name": parsed.get("m_Name") or "",
                "asset_guid_raw": doc.get("m_AssetGUID"),
                "shader_is_baked": doc.get("m_ShaderIsBaked"),
                "fallback_name": parsed.get("m_FallbackName") or "",
                "custom_editor": parsed.get("m_CustomEditorName") or "",
                "dependencies": doc.get("m_Dependencies") or [],
                "property_names": [p.get("m_Name") for p in (parsed.get("m_PropInfo") or {}).get("m_Props", [])],
                "subshader_tags": shader_subshader_tags(parsed),
            }
    if shader is None:
        record["resolution"] = "unresolved_missing_from_index"
        record["reason"] = ("res/_index.csv has path_id %s for %s but its output_file is empty or absent on disk"
                            % (path_id, material_bundle))
        return record

    record["resolution"] = "resolved"
    record["extracted_shader"] = shader
    record["reason"] = ("resources.assets path_id %s is the extracted Shader object %s whose m_ParsedForm.m_Name "
                        "is '%s'" % (path_id, shader["output_file"], shader["shader_name"]))
    return record


def shader_subshader_tags(parsed):
    """The (LOD, tags) of every subshader, so the material's render queue can be reported even when
    `m_CustomRenderQueue` is -1 (in which case Unity uses the shader's own QUEUE tag)."""
    out = []
    for index, sub in enumerate(parsed.get("m_SubShaders") or []):
        tags = ((sub.get("m_Tags") or {}).get("tags")) or []
        out.append({
            "index": index,
            "lod": sub.get("m_LOD"),
            "tags": [[str(k), str(v)] for k, v in tags],
            "passes": len(sub.get("m_Passes") or []),
        })
    return out


def collect_properties(doc, by_bundle_pid, bundle_basename, texture_dest_by_name):
    """Every property of one material, as a flat list of plan rows.

    One row per property: `name`, `kind` ("texenv" | "float" | "color" | "int"), `value` (kind-dependent),
    and - for a texEnv - the resolution of the texture its PPtr names.  Nothing is dropped: a
    `m_SavedProperties` key this loader does not understand becomes a row with kind "unknown" whose raw JSON
    is kept verbatim.
    """
    saved = doc.get("m_SavedProperties") or {}
    properties = []
    unknown_keys = []
    unresolved_textures = []

    for key in saved:
        if key not in SAVED_PROPERTY_KEYS:
            unknown_keys.append(key)

    for key, kind in SAVED_PROPERTY_KEYS.items():
        entries = saved.get(key)
        if entries is None:
            continue
        if not isinstance(entries, list):
            unknown_keys.append(key)
            continue
        for entry in entries:
            if not (isinstance(entry, list) and len(entry) == 2):
                properties.append({
                    "name": None, "kind": "unknown", "source_key": key,
                    "value": entry,
                    "note": "the extraction wrote a %s entry that is not a [name, value] pair" % key,
                })
                continue
            name, value = entry[0], entry[1]
            row = {"name": name, "kind": kind, "source_key": key}
            if kind == "texenv":
                tex = value or {}
                pptr = tex.get("m_Texture") or {}
                file_id = int(pptr.get("m_FileID", 0))
                path_id = int(pptr.get("m_PathID", 0))
                row["scale"] = [float((tex.get("m_Scale") or {}).get("x", 1.0)),
                                float((tex.get("m_Scale") or {}).get("y", 1.0))]
                row["offset"] = [float((tex.get("m_Offset") or {}).get("x", 0.0)),
                                 float((tex.get("m_Offset") or {}).get("y", 0.0))]
                row["texture"] = describe_texture_reference(file_id, path_id, by_bundle_pid,
                                                            bundle_basename, texture_dest_by_name)
                if row["texture"]["resolution"] != "resolved" and not row["texture"].get("is_null_reference"):
                    unresolved_textures.append("%s.%s: %s" % (doc.get("m_Name"), name,
                                                               row["texture"].get("reason")))
            elif kind == "float":
                row["value"] = value
            elif kind == "int":
                row["value"] = value
            elif kind == "color":
                row["value"] = value
            else:
                row["value"] = value
            properties.append(row)
    return properties, unknown_keys, unresolved_textures


def describe_texture_reference(file_id, path_id, by_bundle_pid, bundle_basename, texture_dest_by_name):
    """Resolve a texEnv texture PPtr to an asset this project already has, or say why not.

    A `.png` the importer copied is only usable when exactly ONE extracted texture claims that file name;
    two assets with the same name would make the choice a guess, so an ambiguous name is reported as
    ambiguous rather than picked.
    """
    record = {"file_id": file_id, "path_id": path_id}
    if file_id == 0 and path_id == 0:
        record["resolution"] = "null_reference"
        record["is_null_reference"] = True
        record["reason"] = ("m_FileID 0 / m_PathID 0 is Unity's null reference: the shipped material has no "
                            "texture bound on this slot, so nothing is assigned (which is what the shipped "
                            "object holds, not a gap)")
        return record
    if file_id != 0:
        record["resolution"] = "unresolved_external_file"
        record["reason"] = ("the texture lives in another serialized file (m_FileID %d); that externals table "
                            "was not extracted" % file_id)
        return record

    row = by_bundle_pid.get((bundle_basename, str(path_id)))
    if row is None:
        record["resolution"] = "unresolved_object_not_in_extraction"
        record["reason"] = ("path_id %s of %s is not a row of res/_index.csv, so the extracted set does not "
                            "carry the object this texture pointer names" % (path_id, bundle_basename))
        return record

    record["target_type"] = row["type"]
    record["target_name"] = row["name"]
    record["output_file"] = row["output_file"]
    if row["type"] != "Texture2D":
        record["resolution"] = "unresolved_not_a_texture"
        record["reason"] = ("path_id %s is a %s named '%s', not a Texture2D, so it cannot be assigned to a "
                            "texture slot" % (path_id, row["type"], row["name"]))
        return record

    # the extraction's output_file is what a13_import_assets.py copied into Assets/GameContent
    expected = row["output_file"].replace("\\", "/").lstrip("/")
    base = os.path.basename(expected)
    dest_rel = dest_for_output_file(expected)
    candidates = texture_dest_by_name.get(base, [])
    if len(candidates) == 1 and dest_rel and os.path.exists(os.path.join(UNITY, candidates[0])):
        record["resolution"] = "resolved"
        record["asset_path"] = candidates[0]
        record["reason"] = ("resources.assets path_id %s is the extracted Texture2D '%s' (%s), which this "
                            "project already has at %s" % (path_id, row["name"], row["output_file"], candidates[0]))
        return record
    if len(candidates) > 1:
        record["resolution"] = "ambiguous_in_project"
        record["reason"] = ("%d assets in this project share the extracted file name '%s' (%s); binding one "
                            "would be a guess" % (len(candidates), base, ", ".join(sorted(candidates))))
        record["candidates"] = sorted(candidates)
        return record
    record["resolution"] = "not_imported"
    record["reason"] = ("resources.assets path_id %s is the extracted Texture2D '%s', but no asset with that "
                        "file name exists in this project (%s)" % (path_id, row["name"], dest_rel or "no derivable destination"))
    return record


def dest_for_output_file(output_file):
    """The Unity asset path `a13_import_assets.py:dest_for` would have written this extracted file to."""
    rel = output_file.replace("\\", "/").lstrip("/")
    for prefix in ("Assets/", "_serialized/"):
        if rel.startswith(prefix):
            rel = rel[len(prefix):]
            break
    rel = rel.replace("Assets/#WitchTrials/", "#WitchTrials/")
    if not rel:
        return None
    return "Assets/GameContent/" + rel


def collect_project_textures():
    """file name -> [asset paths] for everything that exists in the project, so a texture reference can be
    resolved to a real asset (and ambiguity detected) without touching Unity."""
    by_name = defaultdict(list)
    for root in (UNITY_ASSETS, UNITY_PACKAGES):
        for dirpath, _dirnames, filenames in os.walk(root):
            for filename in filenames:
                if filename.endswith(".meta"):
                    continue
                asset_path = os.path.relpath(os.path.join(dirpath, filename), UNITY).replace("\\", "/")
                by_name[filename].append(asset_path)
    for name in by_name:
        by_name[name].sort()
    return by_name


# ------------------------------------------------------------------------------------------------------------
# who references a material
# ------------------------------------------------------------------------------------------------------------

def collect_config_references():
    """Material name -> {asset_path, field} rows from the configuration import's `needs_binding` list."""
    references = defaultdict(list)
    detail = {"source": os.path.relpath(CONFIG_IMPORT, REPO).replace("\\", "/")}
    if not os.path.exists(CONFIG_IMPORT):
        detail["error"] = "the configuration import artifact is absent; run NaninovelConfigBuilder first"
        return references, detail
    doc = read_json(CONFIG_IMPORT)
    detail["status"] = doc.get("status")
    detail["references_needing_binding"] = doc.get("references_needing_binding")
    for row in doc.get("rows", []):
        for record in row.get("needs_binding", []):
            if record.get("target_class") != "Material":
                continue
            references[record.get("target_name")].append({
                "owner_asset": row.get("asset_path"),
                "owner_type": row.get("type_name"),
                "field": record.get("field"),
                "target_path_id": record.get("target_path_id"),
                "target_file_id": record.get("target_file_id"),
                "recorded_reason": record.get("reason"),
            })
    for name in references:
        references[name].sort(key=lambda r: (r["owner_asset"] or "", r["field"] or ""))
    return references, detail


def collect_prefab_references(rows, by_bundle_pid, material_by_pid):
    """Material name -> the rebuilt prefabs that point at it, by reading each prefab job's object dumps.

    The prefab plan does not record material pointers (`references` holds only PPtrs the planner could turn
    into an asset path, and materials never resolved), so the dumps have to be read.  A dump's
    `m_FileID == 0` PPtr is looked up in the *job's own bundle*, which is the same-file space the pointer
    belongs to.  When that lookup misses, the path_id is looked up across the whole index **by type only**:
    several of the shipped Addressables bundles reference the very same Material object (the extraction
    proves it - `Naninovel_Default` has the same path_id in all twelve character bundles), so a unique
    cross-bundle Material hit identifies the pointer, and the row records that it did so through this
    fallback rather than through the bundle key.  An ambiguous hit is reported and never used.
    """
    references = defaultdict(set)
    identified = defaultdict(set)
    fallback_hits = defaultdict(set)
    foreign_pointers = defaultdict(set)
    detail = {"source": os.path.relpath(PREFAB_PLAN, REPO).replace("\\", "/")}
    if not os.path.exists(PREFAB_PLAN):
        detail["error"] = "the prefab plan is absent; run tools/unity-rebuild/a13e_plan_prefabs.py first"
        return references, identified, detail, Counter()
    doc = read_json(PREFAB_PLAN)
    jobs = doc.get("jobs", [])
    detail["jobs"] = len(jobs)

    job_bundle = {}
    asset_of_dump = {}
    for job in jobs:
        bundle = os.path.basename((job.get("bundle") or "").replace("\\", "/")).lower()
        asset = job.get("asset_path") or ""
        job_bundle[asset] = bundle
        for obj in job.get("objects", []):
            dump = obj.get("dump")
            if dump:
                asset_of_dump[os.path.normpath(os.path.join(REPO, dump))] = asset

    unresolved = Counter()
    unresolved_samples = []
    dumps_read = 0
    missing_dumps = 0
    for dump, asset in sorted(asset_of_dump.items()):
        if not os.path.exists(dump):
            missing_dumps += 1
            continue
        dumps_read += 1
        try:
            with open(dump, encoding="utf-8", errors="replace") as fh:
                text = fh.read()
        except OSError:
            missing_dumps += 1
            continue
        for match in MATERIAL_PTR.finditer(text):
            for file_id, path_id in PTR.findall(match.group(1)):
                file_id, path_id = int(file_id), int(path_id)
                if file_id != 0:
                    key = "m_FileID=%d (the material lives in another serialized file)" % file_id
                    unresolved[key] += 1
                    foreign_pointers[(file_id, path_id)].add(asset)
                    if len(unresolved_samples) < 40:
                        unresolved_samples.append({
                            "prefab": asset, "dump": os.path.basename(dump),
                            "m_FileID": file_id, "m_PathID": path_id,
                            "reason": "the pointer names another serialized file, whose externals table the "
                                      "extraction did not dump, so the material cannot be identified",
                        })
                    continue
                if path_id == 0:
                    unresolved["null reference (m_PathID 0)"] += 1
                    if len(unresolved_samples) < 40:
                        unresolved_samples.append({
                            "prefab": asset, "dump": os.path.basename(dump),
                            "m_FileID": 0, "m_PathID": 0,
                            "reason": "Unity's null reference: the shipped prefab has no material bound on this "
                                      "slot, so leaving it unbound is faithful, not a gap",
                        })
                    continue
                row = by_bundle_pid.get((job_bundle.get(asset, ""), str(path_id)))
                if row is not None and row["type"] == "Material":
                    references[row["name"]].add(asset)
                    continue
                # cross-bundle fallback: the same Material object can be referenced from several bundles
                candidates = material_by_pid.get(str(path_id), [])
                unique = {c["name"] for c in candidates}
                if len(unique) == 1 and candidates:
                    name = candidates[0]["name"]
                    references[name].add(asset)
                    fallback_hits[name].add(
                        "%s (path_id %s found in %s, not in the prefab's own bundle %s)"
                        % (asset, path_id, candidates[0]["source_bundle"], job_bundle.get(asset, "?")))
                    continue
                if len(unique) > 1:
                    key = "m_PathID %s is ambiguous across bundles: %s" % (path_id, ", ".join(sorted(unique)))
                    unresolved[key] += 1
                    if len(unresolved_samples) < 40:
                        unresolved_samples.append({
                            "prefab": asset, "dump": os.path.basename(dump),
                            "m_FileID": 0, "m_PathID": path_id,
                            "reason": "the path_id names different material names in different bundles, so no "
                                      "single one can be picked",
                        })
                    continue
                if row is None:
                    key = ("m_PathID %s is not a Material row of bundle %s and matches no Material anywhere "
                           "in the index" % (path_id, job_bundle.get(asset, "?")))
                else:
                    key = ("m_PathID %s is a %s, not a Material, in bundle %s"
                           % (path_id, row["type"], job_bundle.get(asset, "?")))
                unresolved[key] += 1
                if len(unresolved_samples) < 40:
                    unresolved_samples.append({
                        "prefab": asset, "dump": os.path.basename(dump),
                        "m_FileID": 0, "m_PathID": path_id,
                        "reason": key,
                    })
                continue
    detail["dump_files_read"] = dumps_read
    detail["dump_files_missing"] = missing_dumps
    detail["material_pointers_unresolved"] = dict(unresolved)
    detail["material_pointers_unresolved_samples"] = unresolved_samples
    detail["cross_bundle_fallback_hits"] = {name: sorted(paths) for name, paths in sorted(fallback_hits.items())}
    foreign = identify_foreign_pointers(rows, foreign_pointers, material_by_pid)
    detail["foreign_file_pointers"] = foreign
    # a foreign pointer whose path_id the index names unambiguously *does* identify a material, so it belongs
    # in the reference list; the record keeps 'how' so a reader can tell the two kinds of evidence apart.
    for record in foreign:
        if record.get("identifies"):
            identified[record["identifies"]].update(record["prefabs"])
    detail["material_names_resolved_in_bundle"] = len(references)
    detail["material_names_identified_across_files"] = len(identified)
    return references, identified, detail, unresolved


def identify_foreign_pointers(rows, foreign_pointers, material_by_pid):
    """`m_FileID > 0` material pointers -> what they name, when the index names it unambiguously.

    This does NOT turn a foreign pointer into a binding, and it must not be read as one: the externals table
    that would say which file index 2 is was not dumped, so which file a pointer lands in is unknown.  What
    *is* known is that the extraction's path_ids are globally unique per object, so a path_id that occurs
    exactly once in the whole index names exactly one shipped object.  That makes the identification
    evidence, not a guess - and it is recorded as such, with the ambiguity stated when there is any.
    """
    out = []
    for pointer in sorted(foreign_pointers):
        file_id, path_id = pointer
        prefabs = foreign_pointers[pointer]
        candidates = material_by_pid.get(str(path_id), [])
        names = sorted({c["name"] for c in candidates})
        record = {
            "m_FileID": file_id, "m_PathID": path_id,
            "prefabs": sorted(prefabs),
            "hits_in_index": len(candidates),
        }
        if len(names) == 1:
            record["identifies"] = names[0]
            record["how"] = ("path_id %s occurs exactly once in res/_index.csv (%s of %s), so it names that "
                             "one shipped Material; which *file* m_FileID %d is was not dumped, so this is an "
                             "identification, not a binding"
                             % (path_id, candidates[0]["name"], candidates[0]["source_bundle"], file_id))
        elif len(names) > 1:
            record["identifies"] = None
            record["how"] = ("path_id %s occurs %d times in res/_index.csv with different names (%s), so it "
                             "identifies nothing on its own" % (path_id, len(candidates), ", ".join(names)))
        else:
            record["identifies"] = None
            record["how"] = "path_id %s is not a Material row of res/_index.csv at all" % path_id
        out.append(record)
    return out


def material_inventory(rows):
    """The full size of the "materials that never got imported" problem.

    "Not imported" is checked two ways and both are reported: no `.mat` asset exists anywhere under
    `Assets/` or the embedded `Packages/` for any of the 155 materials (so all 155 are unimported), and
    `Assets/GameContent` - the folder `a13_import_assets.py` writes to - holds no material at all (so none
    of them was imported into the content tree either).  A `.mat` in an embedded plugin package is *not* an
    import of a shipped material; the names are compared rather than counted, so the two are never conflated.
    """
    material_rows = [r for r in rows if r["type"] == "Material"]
    with_path = [r for r in material_rows if r["asset_path"]]
    without_path = [r for r in material_rows if not r["asset_path"]]
    mat_files = []
    content_mat_files = []
    for root in (UNITY_ASSETS, UNITY_PACKAGES):
        for dirpath, _dirnames, filenames in os.walk(root):
            for filename in filenames:
                if not filename.endswith(".mat"):
                    continue
                rel = os.path.relpath(os.path.join(dirpath, filename), UNITY).replace("\\", "/")
                mat_files.append(rel)
                if rel.startswith("Assets/GameContent/"):
                    content_mat_files.append(rel)
    declared = {r["name"] for r in material_rows}
    in_project = sorted(p for p in mat_files if os.path.splitext(os.path.basename(p))[0] in declared)
    return {
        "index_material_rows": len(material_rows),
        "index_material_rows_with_asset_path": len(with_path),
        "index_material_rows_without_asset_path": len(without_path),
        "uncategorized_material_files": sorted(os.path.basename(r["output_file"]) for r in without_path),
        "mat_assets_in_project": len(mat_files),
        "mat_asset_paths": sorted(mat_files),
        "mat_assets_under_GameContent": len(content_mat_files),
        "mat_assets_matching_an_extracted_material_name": in_project,
        "duplicate_names": {name: count for name, count in
                            Counter(r["name"] for r in material_rows).items() if count > 1},
    }


def scan_import_artifacts():
    """An inventory of the artifacts this tool reads other plans/reports out of, so the report states what
    evidence existed at generation time instead of implying more than was checked."""
    out = []
    for filename in sorted(os.listdir(OUT_DIR)):
        full = os.path.join(OUT_DIR, filename)
        if not os.path.isfile(full) or not filename.endswith(".json"):
            continue
        out.append({"file": filename, "bytes": os.path.getsize(full)})
    return out


# ------------------------------------------------------------------------------------------------------------
# main
# ------------------------------------------------------------------------------------------------------------

def build_plan():
    result = OrderedDict()
    result["generated"] = datetime.datetime.now(datetime.timezone.utc).strftime("%Y-%m-%dT%H:%M:%S.%fZ")
    result["generated_by"] = "tools/unity-rebuild/plan_material_import.py"
    limitations = []
    warnings = []

    if not os.path.exists(INDEX):
        result["status"] = "BLOCKED"
        result["error"] = "res/_index.csv not found: %s (run the extraction pipeline first)" % INDEX
        result["limitations"] = ["nothing was planned: the extraction index is the only table that turns the "
                                 "materials' same-file path_ids into objects"]
        return result

    rows, by_bundle_pid = load_index()
    inventory = material_inventory(rows)
    result["material_inventory"] = inventory

    if inventory["mat_assets_matching_an_extracted_material_name"]:
        warnings.append("this tool plans materials as if none existed, but %d *.mat asset(s) in the project "
                        "carry a name an extracted material also declares (names collide across packages, so "
                        "this is not necessarily the same material): %s"
                        % (len(inventory["mat_assets_matching_an_extracted_material_name"]),
                           ", ".join(inventory["mat_assets_matching_an_extracted_material_name"])))

    extracted_shaders = collect_extracted_shaders()
    project_shaders, project_shader_files, project_shader_files_unreadable = collect_project_shaders()
    project_textures = collect_project_textures()

    config_refs, config_detail = collect_config_references()
    material_by_pid = rows_by_type_and_pid(rows, "Material")
    prefab_refs, prefab_identified, prefab_detail, prefab_unresolved = collect_prefab_references(
        rows, by_bundle_pid, material_by_pid)

    # --- the materials this tool plans ---------------------------------------------------------------
    source_files = sorted(f for f in os.listdir(UNCATEGORIZED_MATERIAL_DIR) if f.endswith(".json"))
    materials = []
    for filename in source_files:
        full = os.path.join(UNCATEGORIZED_MATERIAL_DIR, filename)
        row = None
        for candidate in rows:
            if candidate["type"] == "Material" and candidate["output_file"].replace("\\", "/") == \
                    "Uncategorized/Material/" + filename:
                row = candidate
                break
        bundle = os.path.basename((row["source_bundle"] if row else "resources.assets").replace("\\", "/"))
        entry = OrderedDict()
        entry["source_file"] = "res/Uncategorized/Material/" + filename
        entry["index_row"] = None if row is None else {
            "type": row["type"], "name": row["name"], "asset_path": row["asset_path"],
            "source_bundle": row["source_bundle"], "path_id": row["path_id"],
        }
        if row is None:
            warnings.append("%s is an Uncategorized material the index does not describe; the material file is "
                            "still planned, but its shipped identity (path_id, bundle) is unknown" % filename)

        try:
            doc = read_json(full)
        except (OSError, ValueError) as exc:
            entry["status"] = "UNREADABLE"
            entry["reason"] = "the extracted material typetree could not be read: %s" % exc
            materials.append(entry)
            continue

        entry["name"] = doc.get("m_Name") or ""
        entry["shipped_path_id"] = None if row is None else row["path_id"]
        entry["source_bundle"] = None if row is None else row["source_bundle"]
        entry["target_asset_path"] = "%s/%s.mat" % (TARGET_FOLDER, sanitize_asset_name(entry["name"]))
        entry["target_folder_reason"] = (
            "a13_import_assets.py writes every extracted object under Assets/GameContent/<asset_path minus "
            "'Assets/'>; an object with no asset_path therefore lands in "
            "Assets/GameContent/Uncategorized/<Type>/, which is where this project's imported Font, Sprite, "
            "TextAsset and Texture2D already are")

        shader_record = describe_shader_reference(doc.get("m_Shader") or {}, extracted_shaders_by_path_id(extracted_shaders),
                                                 bundle)
        entry["shader_reference"] = shader_record
        name = (shader_record.get("extracted_shader") or {}).get("shader_name") or ""
        entry["shipped_shader_name"] = name or None
        entry["shader_resolution"] = resolve_shader_in_project(name, project_shaders, shader_record)

        properties, unknown_keys, unresolved_textures = collect_properties(
            doc, by_bundle_pid, bundle.lower(), project_textures)
        entry["properties"] = properties
        entry["property_counts"] = {
            "total": len(properties),
            "texenv": sum(1 for p in properties if p["kind"] == "texenv"),
            "float": sum(1 for p in properties if p["kind"] == "float"),
            "int": sum(1 for p in properties if p["kind"] == "int"),
            "color": sum(1 for p in properties if p["kind"] == "color"),
            "unknown": sum(1 for p in properties if p["kind"] == "unknown"),
        }
        if unknown_keys:
            warnings.append("%s carries m_SavedProperties key(s) this loader does not understand: %s"
                            % (entry["name"], ", ".join(unknown_keys)))
        if unresolved_textures:
            warnings.append("%s has unresolved texture reference(s): %s"
                            % (entry["name"], "; ".join(unresolved_textures)))

        entry["state"] = {
            "m_ValidKeywords": doc.get("m_ValidKeywords") or [],
            "m_InvalidKeywords": doc.get("m_InvalidKeywords") or [],
            "m_LightmapFlags": doc.get("m_LightmapFlags"),
            "m_EnableInstancingVariants": doc.get("m_EnableInstancingVariants"),
            "m_DoubleSidedGI": doc.get("m_DoubleSidedGI"),
            "m_CustomRenderQueue": doc.get("m_CustomRenderQueue"),
            "m_StringTagMap": doc.get("stringTagMap") or [],
            "disabledShaderPasses": doc.get("disabledShaderPasses") or [],
            "m_BuildTextureStacks": doc.get("m_BuildTextureStacks") or [],
        }
        entry["render_queue"] = describe_render_queue(doc, shader_record)

        entry["referenced_by"] = {
            "configuration_needs_binding": config_refs.get(entry["name"], []),
            "rebuilt_prefabs": sorted(prefab_refs.get(entry["name"], [])),
            "rebuilt_prefabs_identified_across_files": sorted(prefab_identified.get(entry["name"], [])),
            "how": ("the prefab pointer names the material's own serialized file, so bundle + path_id resolves it"
                    if entry["name"] in prefab_refs else
                    "the prefab pointer names another serialized file; the material is identified because its "
                    "path_id occurs exactly once in res/_index.csv (see references_summary.prefab_plan."
                    "foreign_file_pointers)" if entry["name"] in prefab_identified else
                    "nothing already reconstructed points at this material"),
        }
        entry["status"] = ("READY" if entry["shader_resolution"]["found"] else
                           "BLOCKED_SHADER_NOT_FOUND" if entry["shader_resolution"]["state"] == "missing_in_project"
                           else "BLOCKED_SHADER_UNRESOLVED")
        materials.append(entry)

    materials.sort(key=lambda m: (m.get("name") or "", m["source_file"]))
    result["materials"] = materials
    result["materials_total"] = len(materials)

    # --- how many of the 155 are referenced by anything already imported ------------------------------
    referenced_by_name = OrderedDict()
    all_referenced = set(config_refs) | set(prefab_refs) | set(prefab_identified)
    for name in sorted(all_referenced):
        matching = [r for r in rows if r["type"] == "Material" and r["name"] == name]
        referenced_by_name[name] = {
            "in_index": len(matching),
            "uncategorized": sum(1 for r in matching if not r["asset_path"]),
            "config_fields": len(config_refs.get(name, [])),
            "rebuilt_prefabs": sorted(prefab_refs.get(name, [])),
            "rebuilt_prefabs_identified_across_files": sorted(prefab_identified.get(name, [])),
            "mat_asset_in_project": any(p.endswith("/" + name + ".mat") for p in inventory["mat_asset_paths"]),
        }
    result["materials_referenced_by_imported_content"] = referenced_by_name
    result["references_summary"] = {
        "config_import": config_detail,
        "prefab_plan": prefab_detail,
        "distinct_material_names_referenced": len(all_referenced),
        "distinct_material_names_uncategorized": sum(1 for n in all_referenced if referenced_by_name[n]["uncategorized"]),
        "distinct_material_names_referenced_by_config": len(config_refs),
        "distinct_material_names_referenced_by_prefabs_in_bundle": len(prefab_refs),
        "distinct_material_names_identified_across_files": len(prefab_identified),
        "materials_we_do_not_have_that_are_referenced": sorted(
            n for n in all_referenced if not referenced_by_name[n]["mat_asset_in_project"]),
    }

    result["shader_catalogue"] = OrderedDict((
        ("extracted_shader_objects", len([s for s in extracted_shaders.values() if "error" not in s])),
        ("project_shader_files", project_shader_files),
        ("project_shader_names", sorted(project_shaders)),
        ("project_shader_files_unreadable", project_shader_files_unreadable),
    ))

    # --- limitations ---------------------------------------------------------------------------------
    ready = [m for m in materials if m.get("status") == "READY"]
    blocked = [m for m in materials if m.get("status", "").startswith("BLOCKED")]
    limitations.append(
        "SHADER AVAILABILITY: %d of the %d uncategorized materials name a shader this tool could resolve to an "
        "extracted Shader object, but %d of those same shaders exist as an asset in this project "
        "(%s). A Material cannot be created without a shader, so the Editor importer reports "
        "SHADER_NOT_FOUND for them and creates nothing; no project shader is substituted, because the shipped "
        "material's appearance is defined by its shader's passes and property defaults and a look-alike shader "
        "would render differently while looking 'fixed'."
        % (sum(1 for m in materials if m.get("shipped_shader_name")),
           len(materials),
           sum(1 for m in materials if m.get("shader_resolution", {}).get("found")),
           ", ".join(sorted({m["shader_resolution"]["asset_path"] for m in materials
                             if m.get("shader_resolution", {}).get("found")})) or "none"))
    limitations.append(
        "WHY THE SHADERS ARE MISSING: the extraction carries 61 Shader objects, all of them *compiled* (only "
        "the GPU program blobs and the reflection tables survive in a player build - `m_ShaderIsBaked` is true "
        "and `m_ParsedForm` has no source). The game's own shaders come from packages the reconstruction does "
        "not have embedded (the 'Naninovel Extender' materials belong to GigaCreation.NaninovelExtender.*, "
        "whose assemblies are in dist/ but whose package is absent). Recovering them is a separate, "
        "shader-shaped job: the extracted reflection tables give each shader's property names and present the "
        "pass state, but the GPU program blobs are bytecode, so re-authoring the source is reconstruction, not "
        "extraction - and it must not be done silently inside a material importer.")
    limitations.append(
        "EXTERNAL-FILE REFERENCES: %d of the %d materials' shader pointers are `m_FileID > 0` and point into a "
        "serialized file whose externals table the extraction did not dump, so their shader can never be "
        "identified from this extraction (reported as UNRESOLVED_EXTERNAL_FILE, never matched by name)."
        % (sum(1 for m in materials if m.get("shader_reference", {}).get("resolution") == "unresolved_external_file"),
           len(materials)))
    limitations.append(
        "UNRESOLVED TEXTURES: a texEnv whose PPtr names a texture the project does not have is left unassigned "
        "and reported on its own property row; the importer applies scale/offset regardless, and says so, "
        "because the shipped material's scale/offset are known even when the texture is not.")
    limitations.append(
        "PROPERTY COVERAGE: the typetree records only properties the shipped material actually overrides plus "
        "the ones Unity always writes; a property absent here keeps the shader's own default when the material "
        "is created, which is exactly what the shipped build did. `m_Ints` is empty in every one of the seven "
        "materials, so the int applier in the Editor tool is exercised by no row of this plan (it exists because "
        "newer Unity versions write integer properties, and this project is 6000.0.48f1).")
    limitations.append(
        "RENDER QUEUE: `m_CustomRenderQueue == -1` means 'use the shader's Queue tag', which is why the "
        "shipped value cannot be turned into a number for those materials; the plan records the tag the "
        "extracted shader declares ('Transparent' -> 3000) as information and the importer leaves "
        "`renderQueue` alone when the shipped value is -1 rather than writing a guessed number.")
    limitations.append(
        "WHAT THE GAME DOES WITH THESE MATERIALS: `BackgroundsConfiguration` / `CharactersConfiguration` hold "
        "the material as an *object reference* (`ActorMetadata.CustomSpriteMaterial`, "
        "`Runtime/Actor/Common/OrthoActorMetadata.cs:23`), read by "
        "`TransitionalRenderer.CreateFor` -> `TransitionalSpriteRenderer.Initialize` "
        "(`Runtime/Transition/TransitionalRenderer.cs:82`, `TransitionalSpriteRenderer.cs:72`, which builds "
        "`new(Shader.Find(\"Hidden/Naninovel/Transparent\"))` when the reference is null). No path lookup is "
        "involved, so the target folder is a project-organisation choice, not something the game requires; "
        "'%s' was chosen to match how the importer already lays uncategorized content out."
        % TARGET_FOLDER)
    limitations.append(
        "NOT VERIFIED WITHOUT RUNNING UNITY: that a material created from a reconstructed shader renders "
        "identically to the shipped one, that AssetDatabase.CreateAsset writes the intended serialized form, "
        "and that `SerializedObject.FindProperty(\"Metadata.metas[0].CustomSpriteMaterial\")` resolves on the "
        "imported configuration assets (this tool has no Unity; the Editor importer reports what it finds and "
        "counts what it binds).")
    limitations.append(
        "WHAT THIS TOOL DOES NOT DO: it does not import the other %d materials. They are *not* unfixable: they "
        "have an asset_path, so their identity is known, and their shaders are equally unavailable - the same "
        "shader-recovery job unblocks both. %d material name(s) the extraction declares are referenced by "
        "content already reconstructed in this project (%d by the imported configuration assets, %d by the "
        "rebuilt prefabs), and they are listed under `materials_referenced_by_imported_content`; every one of "
        "them is missing from the project."
        % (max(0, inventory["index_material_rows"] - len(materials)),
           result["references_summary"]["distinct_material_names_referenced"],
           result["references_summary"]["distinct_material_names_referenced_by_config"],
           (result["references_summary"]["distinct_material_names_referenced_by_prefabs_in_bundle"] +
            result["references_summary"]["distinct_material_names_identified_across_files"])))

    result["limitations"] = limitations
    result["warnings"] = warnings
    result["evidence_scanned"] = {
        "res/_index.csv rows": len(rows),
        "out/unity artifacts (json)": scan_import_artifacts(),
    }
    result["status"] = "BLOCKED" if not ready and blocked else ("PARTIAL" if blocked else "OK")
    return result


def extracted_shaders_by_path_id(extracted_shaders):
    """`collect_extracted_shaders` is keyed by file name (`pathid_22.json`); the material PPtr needs the
    path_id, which is the file name's number."""
    out = {}
    for filename, record in extracted_shaders.items():
        if "error" in record:
            continue
        stem = filename[len("pathid_"):-len(".json")]
        out[stem] = record
    return out


def resolve_shader_in_project(shader_name, project_shaders, shader_record):
    """State plainly whether the shipped shader exists in this project, by exact declared name."""
    if not shader_name:
        return {
            "state": "no_shader_name",
            "found": False,
            "reason": "the material's shader pointer could not be traced to a named Shader object "
                      "(see shader_reference), so no name could be looked up in this project",
        }
    matches = project_shaders.get(shader_name, [])
    if len(matches) == 1:
        return {"state": "found", "found": True, "asset_path": matches[0],
                "reason": "a project shader declares exactly this name"}
    if len(matches) > 1:
        return {"state": "ambiguous_in_project", "found": True, "asset_paths": matches,
                "reason": "%d project shaders declare this name; the importer must be given one explicitly "
                          "rather than picking" % len(matches)}
    return {
        "state": "missing_in_project", "found": False,
        "reason": ("no *.shader under UnityProject/Assets or UnityProject/Packages declares the name '%s' "
                   "(%d shader files were read). The shipped build's own compiled Shader object is in the "
                   "extraction, but a compiled shader is not importable, so the material cannot be created "
                   "here." % (shader_name, len(project_shaders))),
        "closest_project_names": closest_names(shader_name, project_shaders),
    }


def closest_names(shader_name, project_shaders, limit=6):
    """Names that *look* like the missing one, offered as information only (never auto-bound).

    This is the same rule the configuration importer used: resemblance is recorded, never turned into a
    binding.
    """
    tail = shader_name.rsplit("/", 1)[-1].lower()
    scored = []
    for name in project_shaders:
        other = name.rsplit("/", 1)[-1].lower()
        score = 0
        if other == tail:
            score = 3
        elif tail in other or other in tail:
            score = 2
        elif tail.split()[0] and tail.split()[0] in other:
            score = 1
        if score:
            scored.append((-score, name))
    scored.sort()
    return [name for _score, name in scored[:limit]]


def describe_render_queue(doc, shader_record):
    """What Unity would use as the material's render queue, and where that number comes from."""
    custom = doc.get("m_CustomRenderQueue")
    record = {"m_CustomRenderQueue": custom}
    tags = ((shader_record.get("extracted_shader") or {}).get("subshader_tags")) or []
    queue_tag = None
    for sub in tags:
        for key, value in sub.get("tags", []):
            if key.upper() == "QUEUE":
                queue_tag = value
                break
        if queue_tag:
            break
    record["shader_queue_tag"] = queue_tag
    if custom is not None and custom != -1:
        record["effective"] = custom
        record["reason"] = "the material overrides the queue explicitly (m_CustomRenderQueue %s)" % custom
    elif queue_tag:
        record["effective"] = {"Background": 1000, "Geometry": 2000, "AlphaTest": 2450, "Transparent": 3000,
                               "Overlay": 4000}.get(queue_tag, queue_tag)
        record["reason"] = ("m_CustomRenderQueue is -1, so Unity uses the shader's Queue tag ('%s'); the number "
                            "is shown for information - the importer leaves renderQueue at the value the created "
                            "material gets from its shader" % queue_tag)
    else:
        record["effective"] = None
        record["reason"] = "m_CustomRenderQueue is -1 and the shader's Queue tag could not be read from the extraction"
    return record


def sanitize_asset_name(name):
    """A material's own name is already a valid asset file name; validated rather than assumed."""
    if not name or name != name.strip() or name in (".", ".."):
        return "Material_" + re.sub(r"[^0-9A-Za-z]+", "_", name or "unnamed")
    return re.sub(r'[<>:"/\\|?*\x00-\x1f]', "_", name)


def print_report(result):
    print("status: %s  (generated %s)" % (result.get("status"), result.get("generated")))
    inventory = result.get("material_inventory") or {}
    print()
    print("== material inventory ==")
    print("  res/_index.csv Material rows      : %s" % inventory.get("index_material_rows"))
    print("    with an asset_path              : %s" % inventory.get("index_material_rows_with_asset_path"))
    print("    without an asset_path           : %s  %s"
          % (inventory.get("index_material_rows_without_asset_path"),
             ", ".join(inventory.get("uncategorized_material_files", []))))
    print("  *.mat assets in UnityProject      : %s  (%s under Assets/GameContent, %s named like an extracted "
          "material) <- every one of the %s extracted materials is unimported"
          % (inventory.get("mat_assets_in_project"), inventory.get("mat_assets_under_GameContent"),
             len(inventory.get("mat_assets_matching_an_extracted_material_name") or []),
             inventory.get("index_material_rows")))
    if inventory.get("duplicate_names"):
        print("  duplicate material names          : %s" % inventory["duplicate_names"])

    for entry in result.get("materials", []):
        print()
        print("== %s ==" % entry.get("name"))
        print("  source            : %s  (shipped path_id %s in %s)"
              % (entry.get("source_file"), entry.get("shipped_path_id"), entry.get("source_bundle")))
        print("  target asset      : %s" % entry.get("target_asset_path"))
        ref = entry.get("shader_reference", {})
        print("  shader pointer    : (m_FileID %s, m_PathID %s) - %s"
              % (ref.get("file_id"), ref.get("path_id"), ref.get("resolution")))
        if ref.get("extracted_shader"):
            sh = ref["extracted_shader"]
            print("  shipped shader    : '%s'  [%s]  guid_raw=%s  baked=%s"
                  % (sh.get("shader_name"), sh.get("output_file"), sh.get("asset_guid_raw"),
                     sh.get("shader_is_baked")))
            print("    reflected props : %s" % ", ".join(sh.get("property_names") or []))
        else:
            print("  shipped shader    : UNRESOLVED - %s" % ref.get("reason"))
        res = entry.get("shader_resolution", {})
        print("  shader in project : %s%s" % (res.get("state"),
                                              "" if not res.get("asset_path") else "  -> " + res["asset_path"]))
        if not res.get("found"):
            print("    reason          : %s" % res.get("reason"))
            if res.get("closest_project_names"):
                print("    look-alikes     : %s  (information only, never bound)"
                      % ", ".join(res["closest_project_names"]))
        counts = entry.get("property_counts", {})
        print("  properties        : %s total (%s texEnv / %s float / %s int / %s color / %s unknown)"
              % (counts.get("total"), counts.get("texenv"), counts.get("float"), counts.get("int"),
                 counts.get("color"), counts.get("unknown")))
        for prop in entry.get("properties", []):
            if prop["kind"] == "texenv":
                tex = prop.get("texture", {})
                target = tex.get("asset_path") or ("<%s>" % tex.get("resolution"))
                print("      [texenv] %-18s scale=%s offset=%s  texture: %s"
                      % (prop["name"], prop.get("scale"), prop.get("offset"), target))
                if tex.get("resolution") not in ("resolved", "null_reference"):
                    print("               reason: %s" % tex.get("reason"))
            elif prop["kind"] == "color":
                value = prop["value"] or {}
                print("      [color ] %-18s (r=%s, g=%s, b=%s, a=%s)"
                      % (prop["name"], value.get("r"), value.get("g"), value.get("b"), value.get("a")))
            elif prop["kind"] == "unknown":
                print("      [????  ] %-18s raw=%s" % (prop.get("name"), json.dumps(prop.get("value"))[:120]))
            else:
                print("      [%-6s] %-18s %s" % (prop["kind"], prop["name"], prop["value"]))
        state = entry.get("state", {})
        print("  state             : keywords=%s  invalidKeywords=%s  lightmapFlags=%s  instancing=%s  "
              "doubleSidedGI=%s  stringTagMap=%s  disabledPasses=%s"
              % (state.get("m_ValidKeywords"), state.get("m_InvalidKeywords"), state.get("m_LightmapFlags"),
                 state.get("m_EnableInstancingVariants"), state.get("m_DoubleSidedGI"), state.get("m_StringTagMap"),
                 state.get("disabledShaderPasses")))
        queue = entry.get("render_queue", {})
        print("  render queue      : custom=%s  shaderQueueTag=%s  effective=%s  (%s)"
              % (queue.get("m_CustomRenderQueue"), queue.get("shader_queue_tag"), queue.get("effective"),
                 queue.get("reason")))
        refs = entry.get("referenced_by", {})
        config_refs = refs.get("configuration_needs_binding") or []
        print("  referenced by     : %d configuration field(s), %d rebuilt prefab(s)"
              % (len(config_refs), len(refs.get("rebuilt_prefabs") or [])))
        for record in config_refs[:6]:
            print("      %s : %s" % (record["owner_asset"].rsplit("/", 1)[-1], record["field"]))
        if len(config_refs) > 6:
            print("      ... and %d more configuration field(s)" % (len(config_refs) - 6))
        for asset in (refs.get("rebuilt_prefabs") or [])[:6]:
            print("      %s" % asset)
        print("  status            : %s" % entry.get("status"))

    summary = result.get("references_summary") or {}
    print()
    print("== how big is the unimported-material problem ==")
    print("  materials in the extraction                     : %s" % inventory.get("index_material_rows"))
    print("  of those, assets in this project                : %s" %
          len(inventory.get("mat_assets_matching_an_extracted_material_name") or []))
    print("  referenced by content we already reconstructed  : %s distinct name(s)"
          % summary.get("distinct_material_names_referenced"))
    print("    by configuration assets                       : %s"
          % summary.get("distinct_material_names_referenced_by_config"))
    print("    by rebuilt prefabs (pointer in its own file)   : %s"
          % summary.get("distinct_material_names_referenced_by_prefabs_in_bundle"))
    print("    by rebuilt prefabs (identified across files)   : %s"
          % summary.get("distinct_material_names_identified_across_files"))
    print("    of those, uncategorized (no asset_path)        : %s"
          % summary.get("distinct_material_names_uncategorized"))
    print("    of those, present in this project              : %s"
          % (summary.get("distinct_material_names_referenced") -
             len(summary.get("materials_we_do_not_have_that_are_referenced") or [])))
    for name, record in (result.get("materials_referenced_by_imported_content") or {}).items():
        print("      %-42s index=%d uncategorized=%d configFields=%d prefabs=%d+%d"
              % (name, record["in_index"], record["uncategorized"], record["config_fields"],
                 len(record["rebuilt_prefabs"]), len(record["rebuilt_prefabs_identified_across_files"])))

    print()
    print("== limitations ==")
    for line in result.get("limitations", []):
        print("  - %s" % line)
    if result.get("warnings"):
        print()
        print("== warnings ==")
        for line in result["warnings"]:
            print("  - %s" % line)


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("--write", action="store_true",
                        help="write %s" % os.path.relpath(OUT_JSON, REPO).replace("\\", "/"))
    parser.add_argument("--quiet", action="store_true", help="do not print the human-readable report")
    args = parser.parse_args()

    result = build_plan()
    if not args.quiet:
        print_report(result)
    if args.write:
        os.makedirs(OUT_DIR, exist_ok=True)
        with open(OUT_JSON, "w", encoding="utf-8") as fh:
            json.dump(result, fh, ensure_ascii=False, indent=1)
            fh.write("\n")
        print()
        print("wrote %s" % os.path.relpath(OUT_JSON, REPO).replace("\\", "/"))
    return 0 if result.get("status") in ("OK", "PARTIAL") else 2


if __name__ == "__main__":
    sys.exit(main())
