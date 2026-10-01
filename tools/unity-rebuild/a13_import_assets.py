"""a13: turn the extracted object dump into real Unity assets (G2).

The audit's G2 requires every target object to have either a successful import or an explicit
failure record.  This tool therefore never silently drops a row: `res/_index.csv` (58,748 rows) is
walked in full and every row gets a status in

  tools/unity-rebuild/out/unity/a13_import_ledger.csv
  tools/unity-rebuild/out/unity/a13_import_summary.json

Categories
  LEAF      one extracted file == one Unity asset, so the conversion is a copy with a deterministic
            destination path: text (TextAsset), font, texture (Texture2D), audio (AudioClip),
            video (VideoClip).   <- implemented here
  DEFERRED  the object is a sub-asset or part of a composite the runtime owns: sprites (owning
            texture + rect/pivot/border metadata), prefabs/scenes (GameObject/Transform/Canvas…),
            MonoBehaviour components, materials, animation.  These are recorded with the reason and
            the dependency that must exist first.
  UNSUPPORTED  nothing in the shipped build carries the information: shaders (only compiled GPU
            programs exist, no source), and anything else without a converter.
  REFERENCE  MonoScript rows: not importable by themselves, but they are the original
            GUID <-> type map that the composite converters need for two-phase binding.

The shipped tree is never modified: sources are read from res/, outputs are written under
UnityProject/Assets/GameContent/.  That directory is git-ignored (bulk game content is regenerable
from res/ + the bundles).

Usage
  python tools/unity-rebuild/a13_import_assets.py --categories text,font
  python tools/unity-rebuild/a13_import_assets.py --categories texture,audio,video
  python tools/unity-rebuild/a13_import_assets.py --dry-run
"""
from __future__ import annotations

import argparse
import csv
import datetime
import json
import os
import re
import shutil
import sys
from pathlib import Path
from functools import lru_cache
from collections import Counter, defaultdict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
RES = os.path.join(REPO, "res")
INDEX = os.path.join(RES, "_index.csv")
CONTENT = os.path.join(REPO, "UnityProject", "Assets", "GameContent")
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
LEDGER = os.path.join(OUT_DIR, "a13_import_ledger.csv")
IMPORTED_PATHS = os.path.join(OUT_DIR, "a13_imported_paths.txt")
SUMMARY = os.path.join(OUT_DIR, "a13_import_summary.json")

#: object type -> category
CATEGORY_BY_TYPE = {
    "TextAsset": "text",
    "Font": "font",
    "Texture2D": "texture",
    "AudioClip": "audio",
    "VideoClip": "video",
    "MovieTexture": "video",
    "Sprite": "sprite",
}
LEAF_CATEGORIES = {"text", "font", "texture", "audio", "video", "sprite"}

#: object types whose converter exists but needs a dependency imported first
DEFERRED_BY_TYPE = {
    "Sprite": ("needs the owning texture plus the sprite rect/pivot/border metadata", "texture"),
    "Material": ("needs its shader, which is not recoverable from the shipped build", "shader"),
    "MonoBehaviour": ("component data; needs its type GUID mapped to a reconstructed MonoScript", "monoscript-map"),
    "AnimationClip": ("needs the animated object graph (prefab/scene)", "prefab"),
    "Animator": ("needs its AnimatorController asset, which the extractor did not dump", "animator-controller"),
    "PlayableDirector": ("needs the timeline asset it plays", "timeline"),
    "Animation": ("part of a prefab/scene graph", "prefab"),
    "AnimatorController": ("needs the state machine and its clips (prefab graph first)", "prefab"),
    "SpriteAtlas": ("needs its member sprites and the packing settings", "sprite"),
    "Mesh": ("mesh vertex/index data was not extracted by the current pipeline", "mesh-extraction"),
    "AudioMixerController": ("mixer graph; needs an AudioMixer asset writer", "audio-mixer"),
    "AudioMixerGroupController": ("mixer graph; needs an AudioMixer asset writer", "audio-mixer"),
    "AudioMixerSnapshotController": ("mixer graph; needs an AudioMixer asset writer", "audio-mixer"),
}
STRUCTURAL_TYPES = {
    "GameObject", "Transform", "RectTransform", "CanvasRenderer", "CanvasGroup", "Canvas",
    "SpriteRenderer", "MeshRenderer", "MeshFilter", "MeshCollider", "SortingGroup",
    "ParticleSystem", "ParticleSystemRenderer", "Camera", "Light", "AudioSource", "BoxCollider",
    "BoxCollider2D", "CircleCollider2D", "PolygonCollider2D", "Rigidbody2D", "Rigidbody",
    "Animator", "SkinnedMeshRenderer", "TextMeshProUGUI", "Image", "RawImage", "Button",
    "SpriteMask", "RotationConstraint",
}
UNSUPPORTED_TYPES = {
    "Shader": "only compiled GPU programs exist in the shipped build; no shader source to import",
    "ComputeShader": "only compiled GPU programs exist in the shipped build; no shader source to import",
}
#: engine objects that exist per scene / per project rather than as importable assets
NOT_IMPORTABLE_TYPES = {
    "PreloadData", "LightmapSettings", "NavMeshSettings", "RenderSettings",
}
#: rows kept for their mapping value instead of being imported
REFERENCE_TYPES = {
    "MonoScript": "not an importable asset: the original GUID <-> type map used for two-phase binding",
}

STATUS_HEADER = ["type", "category", "status", "reason", "asset_path", "output_file",
                 "target_path", "bundle", "path_id", "bytes"]


@lru_cache(maxsize=128)
def canonical_root(root):
    return Path(root).resolve()


def checked_path(root, relative, resolve=True):
    """Index paths are data: never allow them to escape the declared content tree."""
    root = Path(root).absolute()
    rel = Path(str(relative).replace("\\", "/"))
    if rel.is_absolute() or rel.drive or ".." in rel.parts:
        raise ValueError(f"unsafe content path: {relative!r}")
    result = (root / rel).resolve() if resolve else root / rel
    if not result.is_relative_to(canonical_root(str(root)) if resolve else root):
        raise ValueError(f"content path escapes {root}: {relative!r}")
    return result


def sprite_pair(value, name, rectangle=False):
    import math
    if not isinstance(value, (list, tuple)) or len(value) != 2 or any(
            isinstance(number, bool) or not isinstance(number, (int, float)) or not math.isfinite(number) for number in value):
        raise ValueError(f"Invalid Sprite {name}: two finite numbers are required")
    if rectangle and any(number <= 0 or int(number) != number for number in value):
        raise ValueError("Invalid Sprite rect_size: two positive integers are required")
    return [int(number) if rectangle else float(number) for number in value]


def sprite_canvas(source, rect_size, texture_rect_offset):
    """The existing crop restoration policy, shared with incremental sync."""
    from PIL import Image
    rect_w, rect_h = sprite_pair(rect_size, "rect_size", rectangle=True)
    texture_rect_offset = sprite_pair(texture_rect_offset, "texture_rect_offset")
    with Image.open(source) as image:
        if image.size == (rect_w, rect_h):
            return None, {"status": "UNCHANGED"}
        original_size = list(image.size)
        left = int(round(texture_rect_offset[0]))
        top = rect_h - int(round(texture_rect_offset[1])) - image.size[1]
        over_x = max(0, -left) + max(0, left + image.size[0] - rect_w)
        over_y = max(0, -top) + max(0, top + image.size[1] - rect_h)
        if over_x > 1 or over_y > 1:
            return None, {"status": "OUT_OF_RECT", "offset": [left, top], "old_size": original_size, "rect": [rect_w, rect_h]}
        old = image.convert("RGBA")
    if over_x or over_y:
        cut_left, cut_top = max(0, -left), max(0, -top)
        left, top = max(0, left), max(0, top)
        old = old.crop((cut_left, cut_top, cut_left + min(old.size[0] - cut_left, rect_w - left),
                        cut_top + min(old.size[1] - cut_top, rect_h - top)))
    full = Image.new("RGBA", (rect_w, rect_h), (0, 0, 0, 0))
    full.paste(old, (left, top))
    return full, {"status": "RESTORED", "offset": [left, top], "old_size": original_size,
                  "new_size": [rect_w, rect_h], "texture_rect_offset": list(texture_rect_offset)}


def leaf_payload(source, sprite_settings=None):
    """Return transformed PNG bytes only when the shipped crop needs padding."""
    if not sprite_settings:
        return None
    rect_size = sprite_pair(sprite_settings.get("rect_size"), "rect_size", rectangle=True)
    offset = sprite_settings.get("texture_rect_offset", sprite_settings.get("trimmed", {}).get("texture_rect_offset"))
    if offset is not None:
        offset = sprite_pair(offset, "texture_rect_offset")
    from PIL import Image
    with Image.open(source) as image:
        if list(image.size) == rect_size:
            return None
    if offset is None:
        raise ValueError("Trimmed Sprite has no imported textureRectOffset; regenerate its sprite settings plan from the resource cache")
    image, report = sprite_canvas(source, rect_size, offset)
    if report["status"] != "RESTORED":
        raise ValueError("Sprite crop exceeds its planned rectangle: " + str(report))
    from io import BytesIO
    output = BytesIO()
    image.save(output, format="PNG")
    return output.getvalue()


def sync_leaf(source, target, dry_run=False, payload=None):
    """Shared copy primitive for a13 and content sync; .meta is never replaced."""
    source, target = Path(source), Path(target)
    if target.suffix.lower() == ".meta":
        raise ValueError("Unity metadata is not editable source content")
    if not dry_run:
        target.parent.mkdir(parents=True, exist_ok=True)
        if payload is None:
            shutil.copy2(source, target)
        else:
            target.write_bytes(payload)


def dest_for(asset_path: str, output_file: str) -> str:
    """Deterministic Unity asset path for one extracted file.

    The container path in the shipped build is the Addressables address; the file the extractor
    wrote mirrors it.  "Assets/..." is dropped so the tree becomes
    Assets/GameContent/#WitchTrials/... -- the address itself is carried in the ledger, not
    encoded in the path (an Addressables address is an arbitrary string, so this keeps the file
    tree readable).
    """
    rel = output_file.replace("\\", "/").lstrip("/")
    for prefix in ("Assets/", "_serialized/"):
        if rel.startswith(prefix):
            rel = rel[len(prefix):]
            break
    rel = rel.replace("Assets/#WitchTrials/", "#WitchTrials/")
    return str(checked_path(CONTENT, rel, resolve=False))


def dest_for_sprite(asset_path: str, output_file: str, sprite_name: str) -> str:
    """Unity asset path for one extracted sprite crop.

    In the shipped build a Sprite is a sub-asset of its Texture2D.  The extraction pipeline dumped
    the sprite *pixels* (one crop per sprite) but not the sprite rect/pivot/border, so the faithful
    sub-asset form cannot be rebuilt yet; each crop is imported as an individual single sprite
    instead, named after the original sprite and grouped in <texture>.sprites/ so the owning
    texture stays identifiable.  The deviation is recorded in the ledger and the summary.
    """
    owner = (asset_path or output_file).replace("\\", "/").lstrip("/")
    for prefix in ("Assets/", "_serialized/"):
        if owner.startswith(prefix):
            owner = owner[len(prefix):]
            break
    owner = owner.replace("Assets/#WitchTrials/", "#WitchTrials/")
    folder = os.path.dirname(owner)
    base = os.path.splitext(os.path.basename(owner))[0] or "sprites"
    safe = re.sub(r"[^A-Za-z0-9._\- ]", "_", sprite_name or base).strip() or base
    return str(checked_path(CONTENT, folder + "/" + base + ".sprites/" + safe + ".png", resolve=False))


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--categories", default=",".join(sorted(LEAF_CATEGORIES)),
                    help="comma separated leaf categories to import (others are still recorded)")
    ap.add_argument("--limit", type=int, default=0, help="max rows per category (0 = all)")
    ap.add_argument("--dry-run", action="store_true")
    ap.add_argument("--hash", action="store_true", help="also record sha256 per imported file (slow)")
    args = ap.parse_args()

    wanted = {c.strip() for c in args.categories.split(",") if c.strip()}
    unknown = wanted - LEAF_CATEGORIES
    if unknown:
        raise SystemExit(f"unknown categories: {sorted(unknown)} (known: {sorted(LEAF_CATEGORIES)})")

    if not os.path.exists(INDEX):
        raise SystemExit(f"index missing: {INDEX} (run the extraction pipeline first)")

    os.makedirs(OUT_DIR, exist_ok=True)
    rows_out = []
    counts = Counter()
    per_category = Counter()
    imported = Counter()
    copied_paths = {}
    total_bytes = 0
    per_category_imported = Counter()
    per_category_bytes = Counter()

    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        reader = csv.DictReader(fh)
        for row in reader:
            obj_type = (row.get("type") or "").strip()
            asset_path = (row.get("asset_path") or "").strip()
            output_file = (row.get("output_file") or "").strip()
            category = CATEGORY_BY_TYPE.get(obj_type, "")
            status, reason, target, size = "UNCLASSIFIED", "", "", 0

            if category in LEAF_CATEGORIES:
                if category not in wanted:
                    status, reason = "SKIPPED_CATEGORY", "category not selected for this run"
                elif not output_file:
                    status, reason = "SOURCE_MISSING", "index row has no output_file"
                else:
                    source = str(checked_path(RES, output_file))
                    target = (dest_for_sprite(asset_path, output_file, (row.get("name") or "").strip())
                              if category == "sprite" else dest_for(asset_path, output_file))
                    target = str(checked_path(CONTENT, Path(target).relative_to(CONTENT)))
                    if not os.path.exists(source):
                        status, reason = "SOURCE_MISSING", f"extracted file not found: {source}"
                    elif target in copied_paths:
                        status, reason = "DUPLICATE_DESTINATION", f"same target as {copied_paths[target]}"
                    elif args.limit and per_category[category] >= args.limit:
                        status, reason = "SKIPPED_LIMIT", f"--limit {args.limit} reached for {category}"
                    else:
                        size = os.path.getsize(source)
                        copied_paths[target] = output_file
                        if not args.dry_run:
                            sync_leaf(source, target)
                        status = "WOULD_IMPORT" if args.dry_run else "IMPORTED"
                        per_category_imported[category] += 1
                        per_category_bytes[category] += size
                        total_bytes += size
                per_category[category] += 1
                imported[category] += 1 if status in ("IMPORTED", "WOULD_IMPORT") else 0
            elif obj_type in DEFERRED_BY_TYPE:
                why, dep = DEFERRED_BY_TYPE[obj_type]
                status, reason = "DEFERRED_COMPOSITE", f"{why}; depends on: {dep}"
            elif obj_type in STRUCTURAL_TYPES:
                status, reason = "DEFERRED_STRUCTURAL", "part of a prefab/scene object graph (a13b)"
            elif obj_type in REFERENCE_TYPES:
                status, reason = "REFERENCE_ONLY", REFERENCE_TYPES[obj_type]
            elif obj_type in NOT_IMPORTABLE_TYPES:
                status, reason = "NOT_IMPORTABLE", "engine scene/project settings object, not an asset"
            elif obj_type in UNSUPPORTED_TYPES:
                status, reason = "UNSUPPORTED", UNSUPPORTED_TYPES[obj_type]

            counts[status] += 1
            rows_out.append({
                "type": obj_type, "category": category, "status": status, "reason": reason,
                "asset_path": asset_path, "output_file": output_file,
                "target_path": os.path.relpath(target, REPO).replace("\\", "/") if target else "",
                "bundle": row.get("source_bundle", ""), "path_id": row.get("path_id", ""),
                "bytes": size,
            })

    with open(LEDGER, "w", encoding="utf-8", newline="") as fh:
        writer = csv.DictWriter(fh, fieldnames=STATUS_HEADER)
        writer.writeheader()
        writer.writerows(rows_out)

    # Plain list for the Unity-side verifier: one Unity asset path per imported row, so the editor
    # does not have to parse CSV (the reason column contains commas).
    imported_paths = []
    for row in rows_out:
        if row["status"] == "IMPORTED" and row["target_path"]:
            asset_path = row["target_path"]
            if asset_path.startswith("UnityProject/"):
                asset_path = asset_path[len("UnityProject/"):]
            imported_paths.append(asset_path)
    with open(IMPORTED_PATHS, "w", encoding="utf-8") as fh:
        fh.write("\n".join(sorted(imported_paths)) + "\n")

    summary = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "index": os.path.relpath(INDEX, REPO).replace("\\", "/"),
        "content_root": os.path.relpath(CONTENT, REPO).replace("\\", "/"),
        "dry_run": args.dry_run,
        "categories_requested": sorted(wanted),
        "limit_per_category": args.limit,
        "rows": len(rows_out),
        "status_counts": dict(sorted(counts.items())),
        "leaf_rows_per_category": dict(sorted(per_category.items())),
        "imported_per_category": dict(sorted(per_category_imported.items())),
        "imported_bytes_per_category": {k: v for k, v in sorted(per_category_bytes.items())},
        "imported_files": sum(per_category_imported.values()),
        "imported_bytes": total_bytes,
        "notes": [
            "LEAF categories are a copy: one extracted file == one Unity asset, so no information is "
            "invented. Destination path is derived from output_file; the shipped container path (the "
            "Addressables address) is kept in the ledger's asset_path column for the G3 content build.",
            "DEFERRED_COMPOSITE/STRUCTURAL/UNSUPPORTED rows are recorded with their reason and the "
            "dependency that must land first; none of them is silently dropped.",
            "Unity import settings (texture compression, audio load type, sprite rects) are not "
            "reconstructed here: the extracted files carry the pixels, not the importer configuration.",
        ],
    }
    with open(SUMMARY, "w", encoding="utf-8") as fh:
        json.dump(summary, fh, ensure_ascii=False, indent=1)

    print(f"rows            : {len(rows_out)}")
    print(f"status counts   : {dict(sorted(counts.items()))}")
    print(f"imported        : {sum(per_category_imported.values())} files, {total_bytes/1e6:.1f} MB "
          f"{dict(sorted(per_category_imported.items()))}")
    print(f"ledger          : {LEDGER}")
    print(f"summary         : {SUMMARY}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
