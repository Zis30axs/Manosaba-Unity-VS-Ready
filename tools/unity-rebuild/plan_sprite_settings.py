"""Read the shipped Sprite objects and plan the project's sprite crops to match them.

    python tools/unity-rebuild/plan_sprite_settings.py              # report
    python tools/unity-rebuild/plan_sprite_settings.py --write      # + sprite_settings_plan.json
    python tools/unity-rebuild/plan_sprite_settings.py --write --restore-crops   # + rewrite trimmed crops (Unity closed!)

Why
---
The extraction wrote one PNG per shipped Sprite ("crop") and SpriteImportPolicy imported every crop with placeholder
settings: 100 pixels per unit, centre pivot, no border, max size 2048, default compression.  Measured against the 2395
shipped Sprites (UnityPy, the bundles carry their own type trees):
  * 262 have another pixels-per-unit (cut-ins 38.85, map / reenactment art 45.5, 50, 68.3, ...): a SpriteRenderer drew
    them at the wrong world size;
  * 11 have a 9-slice border: a Sliced Image stretched their corners;
  * 232 crops (668 counting the diced characters) are smaller than the sprite rect: the shipped sprite is "tight" (its textureRect is a trimmed sub-rect
    of m_Rect, offset by textureRectOffset) and the extraction kept only the trimmed pixels, so a centre pivot sat at
    the wrong place and an Image stretched the trimmed content over the whole rect;
  * the art wider than 2048 (NormalPrinter_Screen 2560x852, ...) was downscaled.
Every pivot is (0.5, 0.5) and every layered-character layer sprite is 100 PPU with an untrimmed crop, so those are
unaffected; the diced character sprites (Textures/Naninovel/Characters, rebuilt by DicedSpriteAtlasBuilder) are left
out.

What it does
------------
For each IMPORTED Sprite of the import ledger whose crop exists: pixels per unit, pivot, border (L, B, R, T), rect size,
mesh type (m_RD.settingsRaw bit 6: 1 = Full Rect), and the settings of the texture the pixels live in (the sprite's own
texture, or its SpriteAtlas page when packed): format, filter, wrap, mip count, colour space.  Written to
out/unity/sprite_settings_plan.json, keyed by asset path, which Assets/Editor/SpriteImportPolicy.cs applies.

--restore-crops rewrites each trimmed crop to the full m_Rect: the trimmed pixels are placed at textureRectOffset on a
transparent canvas of the rect's size.  The crop is what the shipped sprite draws - UnityPy cuts textureRect and clears
what lies outside a Tight sprite's mesh - so padding it (rather than cutting m_Rect out of the sheet, which would bring
back pixels the shipped mesh never covered) reproduces the shipped placement to the rounding of textureRectOffset
(fractional, < 0.5 px).  Never run it while Unity has the project open.
"""
from __future__ import annotations

import argparse
import collections
import csv
import datetime
import io
import json
import os

import UnityPy
from PIL import Image

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
LEDGER = os.path.join(OUT_DIR, "a13_import_ledger.csv")
OUT = os.path.join(OUT_DIR, "sprite_settings_plan.json")
BACKSLASH = chr(92)
#: Diced character sprites: DicedSpriteAtlasBuilder rebuilds them from the atlas dumps, their crops are not used.
EXCLUDED = ("/Textures/Naninovel/Characters/",)


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def texture_settings(tex):
    ts = tex.m_TextureSettings
    fmt = tex.m_TextureFormat
    return {"name": tex.m_Name, "width": int(tex.m_Width), "height": int(tex.m_Height), "format_value": int(fmt),
            "mip_count": int(tex.m_MipCount), "color_space": int(getattr(tex, "m_ColorSpace", 1) or 0),
            "filter_mode": int(ts.m_FilterMode), "wrap_u": int(ts.m_WrapU), "wrap_v": int(ts.m_WrapV),
            "aniso": int(ts.m_Aniso)}


def atlas_texture(sprite):
    """The SpriteAtlas page a packed sprite's pixels live in."""
    atlas = sprite.m_SpriteAtlas.read()
    key_guid, key_id = sprite.m_RenderDataKey
    want = (key_guid.data_0_, key_guid.data_1_, key_guid.data_2_, key_guid.data_3_, key_id)
    for (guid, second), data in atlas.m_RenderDataMap:
        if (guid.data_0_, guid.data_1_, guid.data_2_, guid.data_3_, second) == want:
            return data.texture.read()
    return None


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    ap.add_argument("--restore-crops", action="store_true")
    args = ap.parse_args()
    data_dir = os.path.join(game_root(), "manosaba_Data")
    rows = []
    with io.open(LEDGER, encoding="utf-8-sig") as fh:
        for row in csv.DictReader(fh):
            if row["type"] == "Sprite" and row["status"] == "IMPORTED":
                rows.append(row)
    by_bundle = collections.defaultdict(list)
    for row in rows:
        by_bundle[row["bundle"].replace(BACKSLASH, "/")].append(row)

    plan, stats, restored, problems = {}, collections.Counter(), [], []
    for bundle, bundle_rows in sorted(by_bundle.items()):
        path = os.path.join(data_dir, bundle)
        if not os.path.exists(path):
            stats["shipped file missing"] += len(bundle_rows)
            continue
        objects = {str(o.path_id): o for o in UnityPy.load(path).objects}
        for row in bundle_rows:
            target = row["target_path"].replace(BACKSLASH, "/")
            asset_path = target[len("UnityProject/"):] if target.startswith("UnityProject/") else target
            crop_file = os.path.join(REPO, target)
            if not os.path.exists(crop_file):
                stats["crop missing"] += 1
                continue
            if any(part in target for part in EXCLUDED):
                stats["excluded (diced characters)"] += 1
                continue
            obj = objects.get(str(row["path_id"]))
            if obj is None or obj.type.name != "Sprite":
                stats["not a sprite"] += 1
                continue
            sprite = obj.read()
            rd = sprite.m_RD
            # A sprite of a SpriteAtlas (v2, "include in build") has no texture of its own in the player: its
            # m_RD.texture is null and the atlas's render data map holds the page.  (settingsRaw's packed bit is 0
            # for every shipped sprite, so it cannot tell.)
            packed = rd.texture.m_PathID == 0 and bool(sprite.m_SpriteAtlas and sprite.m_SpriteAtlas.m_PathID)
            tex = atlas_texture(sprite) if packed else (rd.texture.read() if rd.texture.m_PathID else None)
            rect_w, rect_h = int(round(sprite.m_Rect.width)), int(round(sprite.m_Rect.height))
            with Image.open(crop_file) as im:
                crop_size = im.size
            entry = {
                "shipped_bundle": bundle, "shipped_path_id": str(row["path_id"]), "name": sprite.m_Name,
                "pixels_per_unit": float(sprite.m_PixelsToUnits),
                "pivot": [float(sprite.m_Pivot.x), float(sprite.m_Pivot.y)],
                "border": [float(sprite.m_Border.x), float(sprite.m_Border.y), float(sprite.m_Border.z),
                           float(sprite.m_Border.w)],
                "rect_size": [rect_w, rect_h], "crop_size": list(crop_size),
                "full_rect_mesh": bool((rd.settingsRaw >> 6) & 1), "packed": packed,
                "texture": texture_settings(tex) if tex is not None else None,
            }
            stats["planned"] += 1
            if abs(entry["pixels_per_unit"] - 100.0) > 1e-3:
                stats["ppu != 100"] += 1
            if any(entry["border"]):
                stats["border"] += 1
            if tuple(crop_size) != (rect_w, rect_h):
                stats["trimmed crop"] += 1
                entry["trimmed"] = {"texture_rect": [rd.textureRect.x, rd.textureRect.y, rd.textureRect.width,
                                                     rd.textureRect.height],
                                    "texture_rect_offset": [rd.textureRectOffset.x, rd.textureRectOffset.y]}
                if args.restore_crops:
                    result = restore(sprite, packed, tex, crop_file, rect_w, rect_h)
                    entry["trimmed"]["restore"] = result
                    (restored if result["status"] == "RESTORED" else problems).append((asset_path, result))
            plan[asset_path] = entry

    doc = {"generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
           "generated_by": "tools/unity-rebuild/plan_sprite_settings.py", "stats": dict(stats), "sprites": plan}
    for key, value in sorted(stats.items()):
        print("  %-30s %d" % (key, value))
    if args.restore_crops:
        print("restored %d trimmed crops; %d not restored" % (len(restored), len(problems)))
        for asset_path, result in problems[:20]:
            print("  NOT RESTORED", asset_path, result)
    if args.write:
        with io.open(OUT, "w", encoding="utf-8", newline="\n") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)


def restore(sprite, packed, tex, crop_file, rect_w, rect_h):
    """Rewrite a trimmed crop as the full sprite rect, the trimmed pixels at textureRectOffset."""
    rd = sprite.m_RD
    with Image.open(crop_file) as im:
        old = im.convert("RGBA")
    # the trimmed rect inside the full rect, in PIL's top-left coordinates (Unity's offset is from the bottom-left)
    left = int(round(rd.textureRectOffset.x))
    top = rect_h - int(round(rd.textureRectOffset.y)) - old.size[1]
    # The crop is textureRect rounded up, so it can overhang the rect by a pixel; more than that is not rounding.
    over_x = max(0, -left) + max(0, left + old.size[0] - rect_w)
    over_y = max(0, -top) + max(0, top + old.size[1] - rect_h)
    if over_x > 1 or over_y > 1:
        return {"status": "OUT_OF_RECT", "offset": [left, top], "old_size": list(old.size), "rect": [rect_w, rect_h]}
    if over_x or over_y:
        cut_left, cut_top = max(0, -left), max(0, -top)
        left, top = max(0, left), max(0, top)
        old = old.crop((cut_left, cut_top, cut_left + min(old.size[0] - cut_left, rect_w - left),
                        cut_top + min(old.size[1] - cut_top, rect_h - top)))
    full = Image.new("RGBA", (rect_w, rect_h), (0, 0, 0, 0))
    full.paste(old, (left, top))
    full.save(crop_file)
    return {"status": "RESTORED", "offset": [left, top], "old_size": list(old.size), "new_size": [rect_w, rect_h],
            "texture_rect_offset": [rd.textureRectOffset.x, rd.textureRectOffset.y]}


if __name__ == "__main__":
    main()
