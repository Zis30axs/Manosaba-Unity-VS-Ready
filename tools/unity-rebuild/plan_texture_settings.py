"""Read the shipped import-relevant settings of the textures the material plans bind, for TextureSettingsImporter.

    python tools/unity-rebuild/plan_texture_settings.py [--write]

A texture imported from an extracted PNG gets Unity's defaults (mipmaps on, sRGB, Repeat, max size 2048,
normal compression).  That is harmless for most art but wrong for data textures: the shipped "ToneCurve" that
"Naninovel Extender/Maskable Transparent Toned" samples as a lookup table is 256x4 R8, no mipmaps, Clamp, linear -
compressed, mipmapped and wrapped it returns different values, so the tone curve itself would change.

This reads each shipped Texture2D object with UnityPy (it has no typetree issue: the objects carry their own
settings) and records width, height, format, mip count, colour space and sampler settings.  Output:
texture_settings_plan.json, consumed by Assets/Editor/TextureSettingsImporter.cs.

Scope:
  default       every texture a material plan binds (material_import_plan.json + material_embedded_plan.json,
                `texenv` rows whose texture resolved to a project asset);
  --scope all   additionally every Texture2D the import ledger IMPORTED as a plain texture - backgrounds, stills,
                clue images, diced / packed atlases, ...  Those imported with Unity's defaults, and the default that
                matters most is "Non-Power of 2: To Nearest": the shipped 2560x1440 backgrounds became 2048x1024, so
                Naninovel (which sizes a background from its texture) drew every background at 2:1 - the title still
                stretched 12.5 % sideways and the black "Overlay" fader 1920x960 on a 1080-pixel screen, which is the
                unfaded / unblurred band at the top and bottom of the title fade-in.  Sprite crops (".sprite" in the
                path; SpriteImportPolicy owns them) and font atlases (the font pipeline owns them) are left out.
"""
from __future__ import annotations

import argparse
import datetime
import io
import json
import os

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
PLANS = ("material_import_plan.json", "material_embedded_plan.json")
OUT = os.path.join(OUT_DIR, "texture_settings_plan.json")
BACKSLASH = chr(92)


def game_root():
    from imported_source import game_root as imported_game_root
    return imported_game_root()


def wanted_textures():
    """(shipped file, path_id) -> project asset path, from the texenv rows the material plans resolved."""
    wanted = {}
    for name in PLANS:
        path = os.path.join(OUT_DIR, name)
        if not os.path.exists(path):
            continue
        with io.open(path, encoding="utf-8") as fh:
            plan = json.load(fh)
        for material in plan.get("materials") or []:
            source = (material.get("source_bundle") or (material.get("index_row") or {}).get("source_bundle") or "")
            for prop in material.get("properties") or []:
                tex = prop.get("texture") or {}
                if prop.get("kind") != "texenv" or tex.get("resolution") != "resolved":
                    continue
                if int(tex.get("file_id", 0)) != 0:
                    continue          # another file: its settings are read where that file's own plan binds it
                wanted[(source, str(tex.get("path_id")))] = {"asset_path": tex.get("asset_path"),
                                                             "material": material.get("name"), "property": prop.get("name")}
    return wanted


LEDGER = os.path.join(OUT_DIR, "a13_import_ledger.csv")
SKIP_PARTS = (".sprite", "/Fonts/", "/RuntimeFonts/")


def ledger_textures(wanted):
    """Every plain Texture2D the import ledger IMPORTED, keyed like wanted_textures(); existing entries win."""
    import csv
    known = {target["asset_path"] for target in wanted.values()}
    with io.open(LEDGER, encoding="utf-8-sig") as fh:
        for row in csv.DictReader(fh):
            if row["type"] != "Texture2D" or row["status"] != "IMPORTED":
                continue
            target = (row.get("target_path") or "").replace(BACKSLASH, "/")
            if not target.startswith("UnityProject/") or any(part in target for part in SKIP_PARTS):
                continue
            if not os.path.exists(os.path.join(REPO, target)):
                continue
            asset_path = target[len("UnityProject/"):]
            if asset_path in known:
                continue          # a material plan already reads it
            source = row["bundle"].replace(BACKSLASH, "/")      # relative to manosaba_Data
            wanted[(source, str(row["path_id"]))] = {"asset_path": asset_path, "material": None, "property": None}
    return wanted


def shipped_file(root, source):
    """The file a row's source names: resources.assets / sharedassetsN.assets in manosaba_Data, or a bundle."""
    source = source.replace(BACKSLASH, "/")
    data = os.path.join(root, "manosaba_Data")
    if "/" in source:
        return os.path.join(data, source)
    candidate = os.path.join(data, source)
    if os.path.exists(candidate):
        return candidate
    for dirpath, _dirs, files in os.walk(os.path.join(data, "StreamingAssets", "aa")):
        if source in files:
            return os.path.join(dirpath, source)
    return candidate


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    ap.add_argument("--scope", choices=("materials", "all"), default="materials")
    args = ap.parse_args()
    root = game_root()
    wanted = wanted_textures()
    if args.scope == "all":
        wanted = ledger_textures(wanted)
    by_file = {}
    for (source, pid), target in wanted.items():
        by_file.setdefault(source, {})[pid] = target
    entries = []
    for source, pids in sorted(by_file.items()):
        path = shipped_file(root, source)
        if not os.path.exists(path):
            for pid, target in pids.items():
                entries.append({**target, "source": source, "path_id": pid, "status": "SHIPPED_FILE_MISSING",
                                "reason": path})
            continue
        env = UnityPy.load(path)
        objects = {str(o.path_id): o for o in env.objects}
        for pid, target in sorted(pids.items()):
            obj = objects.get(pid)
            if obj is None or obj.type.name != "Texture2D":
                entries.append({**target, "source": source, "path_id": pid, "status": "NOT_A_TEXTURE"})
                continue
            d = obj.read()
            ts = d.m_TextureSettings
            fmt = d.m_TextureFormat
            entries.append({**target, "source": source, "path_id": pid, "status": "READ",
                            "name": d.m_Name, "width": int(d.m_Width), "height": int(d.m_Height),
                            "format": getattr(fmt, "name", str(fmt)), "format_value": int(fmt),
                            "mip_count": int(d.m_MipCount), "color_space": int(getattr(d, "m_ColorSpace", 1) or 0),
                            "is_readable": bool(getattr(d, "m_IsReadable", False)),
                            "filter_mode": int(ts.m_FilterMode), "aniso": int(ts.m_Aniso),
                            "mip_bias": float(ts.m_MipBias), "wrap_u": int(ts.m_WrapU), "wrap_v": int(ts.m_WrapV)})
    doc = {"generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
           "generated_by": "tools/unity-rebuild/plan_texture_settings.py",
           "scope": ("textures bound by " + ", ".join(PLANS)) + (" + every plain Texture2D of the import ledger"
                                                                 if args.scope == "all" else ""),
           "textures": entries}
    for e in entries:
        print("  %-10s %-52s %s" % (e["status"], e.get("asset_path"),
                                    {k: e.get(k) for k in ("width", "height", "format", "mip_count", "color_space",
                                                           "filter_mode", "wrap_u", "wrap_v")}))
    if args.write:
        os.makedirs(OUT_DIR, exist_ok=True)
        with io.open(OUT, "w", encoding="utf-8", newline="\n") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)


if __name__ == "__main__":
    main()
