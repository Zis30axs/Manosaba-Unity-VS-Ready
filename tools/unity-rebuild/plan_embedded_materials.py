"""Plan the materials that live INSIDE the shipped content bundles, so the rebuilt prefabs can point at them.

    python tools/unity-rebuild/plan_embedded_materials.py [--write]

Why
---
Every layered character bundle (alisa.bundle, anan.bundle, ...) carries its own copies of the layer materials
- `Naninovel_Default`, `Naninovel_Multiply`, `Naninovel_Overlay`, `Naninovel_Softlight` and their stencil
variants (`#Mask_Ref1_CutoffLow`, `#Masked_Ref2`, ...) - as implicit dependencies with no asset path of their
own.  `a13e_plan_prefabs.py` skips `Material` objects (they are not part of a prefab's object graph), and no
project asset stood for them, so every SpriteRenderer.m_Materials[0] of those prefabs was left unbound and the
layers drew with the default sprite material: shading layers painted flat instead of multiplied, masks gone.

What this tool does
-------------------
For every `Material` row of res/_index.csv that comes from a content bundle (not resources.assets /
globalgamemanagers / level / sharedassets - those are `plan_material_import.py`'s), it reads the dump, traces the
shader pointer to the bundle's own Shader object and its declared name, and resolves that name against the
project's `.shader` files (the same rule as plan_material_import.py: exact declared name, never a look-alike).
Materials with the same name, shader and property values are one material (the bundles carry byte-identical
copies); a name whose copies differ gets one asset per variant.  The output:

  * `material_embedded_plan.json` - rows in the format `Assets/Editor/MaterialImporter.cs` consumes
    (run it with MANOSABA_MATERIAL_PLAN=material_embedded_plan.json);
  * `material_embedded_pointer_map.json` - "<bundle>|<path_id>" -> project material path, for every shipped copy,
    which `a13e_plan_prefabs.py` merges into its same-bundle sidecar table.

A material whose shader is not in the project (URP / Shader Graph / TextMeshPro ones) is reported, not planned.
"""
from __future__ import annotations

import argparse
import datetime
import hashlib
import json
import os
from collections import defaultdict

import plan_material_import as pm

REPO = pm.REPO
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
PLAN_OUT = os.path.join(OUT_DIR, "material_embedded_plan.json")
MAP_OUT = os.path.join(OUT_DIR, "material_embedded_pointer_map.json")
#: A reconstruction choice (the shipped build gives these no path): one folder, named for what they are.
TARGET_FOLDER = "Assets/GameContent/EmbeddedMaterials"
NOT_CONTENT_BUNDLES = ("resources.assets", "globalgamemanagers", "globalgamemanagers.assets")
BACKSLASH = chr(92)


def is_content_bundle(bundle):
    return bool(bundle) and bundle not in NOT_CONTENT_BUNDLES and not bundle.startswith("level") \
        and not bundle.startswith("sharedassets")


def dump_path(row):
    rel = row["output_file"].replace(BACKSLASH, "/")
    for candidate in (os.path.join(pm.RES, rel), os.path.join(pm.RES, "_serialized", rel)):
        if os.path.exists(candidate):
            return candidate
    return None


def shader_record_for(material_doc, bundle, by_bundle_pid):
    """The bundle's own Shader object the material points at: (declared name, record, reason)."""
    pptr = material_doc.get("m_Shader") or {}
    file_id, path_id = int(pptr.get("m_FileID", 0)), int(pptr.get("m_PathID", 0))
    if file_id != 0:
        return None, None, ("the shader pointer is m_FileID %d (another serialized file); the extraction keeps no "
                            "externals table for content bundles, so it cannot be traced" % file_id)
    row = by_bundle_pid.get((bundle, str(path_id)))
    if row is None or row["type"] != "Shader":
        return None, None, "path_id %d of %s is not an extracted Shader" % (path_id, bundle)
    path = dump_path(row)
    if path is None:
        return None, None, "the Shader dump %s is missing" % row["output_file"]
    doc = pm.read_json(path)
    parsed = doc.get("m_ParsedForm") or {}
    name = parsed.get("m_Name") or ""
    record = {"extracted_shader": {"output_file": row["output_file"], "shader_name": name,
                                   "subshader_tags": pm.shader_subshader_tags(parsed)}}
    return name, record, "same bundle, path_id %d" % path_id


def identity(doc, shader_name, properties):
    """What makes two shipped copies the same material: name, shader, keywords, queue and every property value
    (a texture by where it resolved, never by its per-bundle path_id)."""
    def prop_key(p):
        if p["kind"] == "texenv":
            tex = p.get("texture") or {}
            return [p["name"], "texenv", p.get("scale"), p.get("offset"),
                    tex.get("asset_path") if tex.get("resolution") == "resolved" else
                    ("null" if tex.get("is_null_reference") else "unresolved")]
        return [p["name"], p["kind"], p.get("value")]
    key = {"name": doc.get("m_Name"), "shader": shader_name,
           "keywords": sorted(doc.get("m_ValidKeywords") or []), "queue": doc.get("m_CustomRenderQueue"),
           "properties": sorted(prop_key(p) for p in properties)}
    return hashlib.sha1(json.dumps(key, sort_keys=True).encode("utf-8")).hexdigest()


def build():
    rows, by_bundle_pid = pm.load_index()
    project_shaders, _files, _unreadable = pm.collect_project_shaders()
    texture_dest_by_name = pm.collect_project_textures()

    groups = defaultdict(list)          # (name, identity) -> [copy]
    skipped = []
    for row in rows:
        if row["type"] != "Material":
            continue
        bundle = os.path.basename(row["source_bundle"].replace(BACKSLASH, "/")).lower()
        if not is_content_bundle(bundle):
            continue
        path = dump_path(row)
        if path is None:
            skipped.append({"name": row["name"], "bundle": bundle, "path_id": row["path_id"], "reason": "no dump"})
            continue
        doc = pm.read_json(path)
        shader_name, shader_record, shader_reason = shader_record_for(doc, bundle, by_bundle_pid)
        properties, unknown_keys, unresolved_textures = pm.collect_properties(doc, by_bundle_pid, bundle,
                                                                             texture_dest_by_name)
        copy = {"name": doc.get("m_Name") or row["name"], "bundle": bundle, "path_id": row["path_id"],
                "source_file": os.path.relpath(path, REPO).replace(BACKSLASH, "/"), "doc": doc,
                "shader_name": shader_name, "shader_record": shader_record, "shader_reason": shader_reason,
                "properties": properties, "unknown_keys": unknown_keys, "unresolved_textures": unresolved_textures}
        groups[(copy["name"], identity(doc, shader_name, properties))].append(copy)

    by_name = defaultdict(list)
    for (name, ident), copies in groups.items():
        by_name[name].append((ident, copies))

    materials, pointer_map, not_planned = [], {}, []
    for name in sorted(by_name):
        variants = sorted(by_name[name], key=lambda v: (-len(v[1]), v[0]))
        for index, (ident, copies) in enumerate(variants):
            first = copies[0]
            resolution = pm.resolve_shader_in_project(first["shader_name"], project_shaders,
                                                      first["shader_record"] or {})
            file_name = pm.sanitize_asset_name(name) + ("" if index == 0 else "~%d" % index)
            target = "%s/%s.mat" % (TARGET_FOLDER, file_name)
            sources = ["%s|%s" % (c["bundle"], c["path_id"]) for c in copies]
            if (first["shader_name"] or "").startswith("TextMeshPro/"):
                # TMP font materials are sub-assets / presets of the font assets, which the font pipeline rebuilds
                # with their atlases under Assets/GameContent/RuntimeFonts (and prefabs reach them through the font
                # bundles, never as a same-bundle pointer).  A copy here would have no atlas and could only mislead.
                not_planned.append({"name": name, "variant": index, "copies": len(copies),
                                    "shipped_shader_name": first["shader_name"],
                                    "shader_resolution": {"state": "owned_by_font_pipeline"}, "sources": sources[:6]})
                continue
            if resolution.get("state") != "found":
                not_planned.append({"name": name, "variant": index, "copies": len(copies),
                                    "shipped_shader_name": first["shader_name"], "shader_reason": first["shader_reason"],
                                    "shader_resolution": resolution, "sources": sources[:6]})
                continue
            doc = first["doc"]
            materials.append({
                "source_file": first["source_file"],
                "name": name,
                "shipped_path_id": first["path_id"],
                "source_bundle": first["bundle"],
                "target_asset_path": target,
                "status": "PLANNED",
                "shipped_shader_name": first["shader_name"],
                "shader_reference": {"resolution": "resolved", "reason": first["shader_reason"],
                                     **(first["shader_record"] or {})},
                "shader_resolution": resolution,
                "properties": first["properties"],
                "unknown_saved_property_keys": first["unknown_keys"],
                "unresolved_textures": first["unresolved_textures"],
                "state": {"m_ValidKeywords": doc.get("m_ValidKeywords") or [],
                          "m_InvalidKeywords": doc.get("m_InvalidKeywords") or [],
                          "m_LightmapFlags": doc.get("m_LightmapFlags"),
                          "m_EnableInstancingVariants": doc.get("m_EnableInstancingVariants"),
                          "m_DoubleSidedGI": doc.get("m_DoubleSidedGI"),
                          "m_CustomRenderQueue": doc.get("m_CustomRenderQueue"),
                          "m_StringTagMap": doc.get("stringTagMap") or [],
                          "disabledShaderPasses": doc.get("disabledShaderPasses") or []},
                "render_queue": pm.describe_render_queue(doc, first["shader_record"] or {}),
                "copies": len(copies),
                "sources": sources,
                "referenced_by": {"configuration_needs_binding": []},
            })
            for key in sources:
                pointer_map[key] = target

    return {
        "generated": datetime.datetime.utcnow().isoformat() + "Z",
        "generated_by": "tools/unity-rebuild/plan_embedded_materials.py",
        "status": "OK",
        "target_folder": TARGET_FOLDER,
        "materials_total": len(materials),
        "shipped_copies_mapped": len(pointer_map),
        "materials": materials,
        "not_planned": not_planned,
        "skipped": skipped,
    }, pointer_map


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()
    plan, pointer_map = build()
    print("planned %d materials covering %d shipped copies; %d name/variant(s) not planned (shader not in project)"
          % (plan["materials_total"], plan["shipped_copies_mapped"], len(plan["not_planned"])))
    for m in plan["materials"]:
        print("  PLAN  %-44s %-40s copies=%-3d -> %s" % (m["name"], m["shipped_shader_name"], m["copies"],
                                                         m["target_asset_path"]))
    for m in plan["not_planned"]:
        print("  SKIP  %-44s %-40s copies=%-3d %s" % (m["name"], m["shipped_shader_name"], m["copies"],
                                                     m["shader_resolution"].get("state")))
    if args.write:
        os.makedirs(OUT_DIR, exist_ok=True)
        with open(PLAN_OUT, "w", encoding="utf-8", newline="\n") as fh:
            json.dump(plan, fh, ensure_ascii=False, indent=1)
        with open(MAP_OUT, "w", encoding="utf-8", newline="\n") as fh:
            json.dump({"generated": plan["generated"], "pointers": pointer_map}, fh, ensure_ascii=False, indent=1)
        print("wrote " + PLAN_OUT)
        print("wrote " + MAP_OUT)


if __name__ == "__main__":
    main()
