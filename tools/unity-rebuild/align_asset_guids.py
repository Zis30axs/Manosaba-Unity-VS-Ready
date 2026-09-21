#!/usr/bin/env python3
"""Align this project's asset GUIDs with the shipped ones.

Why this exists
---------------
Every reconstructed prefab/scene/asset copies its fields verbatim from the shipped typetree dumps,
and `AssetReference` fields are stored in the shipped data as the *target asset's GUID*.
The extraction step imported the assets with fresh `.meta` GUIDs, so those `AssetReference`
values point at nothing: `AssetReference.RuntimeKeyIsValid()` is true, the Addressables lookup
misses, and the loader returns null.

Measured consequence (round 45): `WitchTrialsLogUi.Initialize` -> `AuthorTextBuilder.InitializeAsync`
-> `LoadDataAsync` NRE because `_characterDataAsset` (GUID 519e9e2fa7c404c4689c3f073450d8e5) did not
resolve — the project asset carries GUID eda968d981e9b834b8e0cbcc1d786e28.  That fault propagated
through `UniTask.WhenAll` in `UIManager.InstantiateUIs` into `Engine.Initialize`, which
`RuntimeInitializer` never observes (`IsCompleted` is true for a faulted task too), so the engine
silently never finished initializing.

The map is recoverable exactly: `catalog_dump_shipped.json` holds one entry per shipped GUID whose
`locations` name the shipped asset path, and the extraction preserved those paths under
`Assets/GameContent/`.

Usage
-----
  python align_asset_guids.py                 # dry run: report coverage, write the plan json
  python align_asset_guids.py --apply         # rewrite GUIDs in every text asset + .meta
"""
import argparse
import json
import os
import re
import sys

REPO = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
SHIPPED_CATALOG = os.path.join(OUT, "catalog_dump_shipped.json")
PLAN_PATH = os.path.join(OUT, "asset_guid_alignment.json")

ASSETS_ROOT = os.path.join(REPO, "UnityProject", "Assets")
PACKAGES_ROOT = os.path.join(REPO, "UnityProject", "Packages")

GUID_RE = re.compile(r"^[0-9a-f]{32}$")
META_GUID_RE = re.compile(rb"^guid:\s*([0-9a-f]{32})\s*$", re.MULTILINE)

# Asset path prefixes the shipped catalog uses, mapped to where the extraction put them.
PREFIX_CANDIDATES = ("", "GameContent")

# Only text assets are rewritten; bundles/media are rebuilt from these, never patched in place.
TEXT_EXT = {
    ".meta", ".asset", ".prefab", ".unity", ".mat", ".controller", ".anim", ".overridecontroller",
    ".physicmaterial", ".physicmaterial2d", ".shader", ".shadergraph", ".playable", ".mixer",
    ".guiskin", ".fontsettings", ".spriteatlas", ".rendertexture", ".cubemap", ".flare",
    ".terrainlayer", ".lighting", ".inputactions", ".preset", ".signal", ".mask", ".brush",
    ".json", ".txt", ".xml", ".csv", ".yml", ".yaml", ".shadersubgraph",
}
SKIP_EXT = {
    ".dll", ".so", ".dylib", ".bundle", ".png", ".jpg", ".jpeg", ".tga", ".psd", ".exr", ".hdr",
    ".wav", ".ogg", ".mp3", ".mp4", ".webm", ".ttf", ".otf", ".bytes", ".fbx", ".obj", ".zip",
    ".nib", ".a", ".lib", ".pdb", ".mdb", ".gz", ".bin",
}


def log(msg):
    print(msg, flush=True)


def load_meta_guids(root):
    """asset path (repo-relative, forward slashes) -> guid, from every .meta under root."""
    found = {}
    for dirpath, _dirnames, filenames in os.walk(root):
        for name in filenames:
            if not name.endswith(".meta"):
                continue
            meta_path = os.path.join(dirpath, name)
            try:
                with open(meta_path, "rb") as fh:
                    head = fh.read(4096)
            except OSError:
                continue
            match = META_GUID_RE.search(head)
            if not match:
                continue
            asset_path = meta_path[: -len(".meta")]
            rel = os.path.relpath(asset_path, REPO).replace("\\", "/")
            found[rel] = match.group(1).decode("ascii")
    return found


def load_shipped_guid_locations():
    """shipped guid -> shipped asset path, from the shipped catalog's guid-keyed entries."""
    with open(SHIPPED_CATALOG, "r", encoding="utf-8") as fh:
        catalog = json.load(fh)

    entries = catalog.get("entries") or catalog.get("keys") or []
    out, ambiguous, no_location = {}, 0, 0
    for entry in entries:
        key = entry.get("key")
        if not key or not GUID_RE.match(key):
            continue
        locations = entry.get("locations") or []
        paths = [loc for loc in locations if isinstance(loc, str)
                 and (loc.startswith("Assets/") or loc.startswith("Packages/"))]
        if len(paths) != 1:
            if len(paths) > 1:
                ambiguous += 1
            else:
                no_location += 1
            continue
        out[key] = paths[0]
    return out, ambiguous, no_location


def resolve(location, known):
    """shipped location -> the project asset path carrying it, or None.

    Two layout differences between the shipped bundles and this project have to be absorbed:

      * every extracted asset lives under `Assets/GameContent/` (the shipped path is `Assets/...`)
      * a packed texture whose source was a multi-sprite sheet was extracted into a
        `<Name>.sprites/` folder (`.../Packed/UI_Common/CloseButton_Default.png` lives at
        `.../Packed/UI_Common/CloseButton_Default.sprites/CloseButton_Default.png`)
    """
    if location.startswith("Packages/"):
        candidate = "UnityProject/" + location
        return candidate if candidate in known else None
    if not location.startswith("Assets/"):
        return None

    tail = location[len("Assets/"):]
    directory, filename = tail.rsplit("/", 1) if "/" in tail else ("", tail)
    stem, ext = os.path.splitext(filename)

    tails = [tail]
    if directory and stem:
        tails.append("%s/%s.sprites/%s" % (directory, stem, filename))

    # The TMP font variants are regenerated locally (RuntimeResourceRepair): `Assets/#WitchTrials/Fonts/<Family>/<f>.mat`
    # became `Assets/GameContent/RuntimeFonts/<Family>/<f>.mat`.  Only the materials are aligned: the font `.asset`s keep
    # the tracked regeneration GUIDs that RuntimeResourceRepair enforces (nothing references them by GUID), whereas the
    # materials are referenced by their shipped GUID (FontMaterialData._fontMaterialReference, an AssetReferenceT<Material>).
    fonts_marker = "#WitchTrials/Fonts/"
    if ext == ".mat" and directory.startswith(fonts_marker):
        candidate = "UnityProject/Assets/GameContent/RuntimeFonts/" + tail[len(fonts_marker):]
        if candidate in known:
            return candidate

    for prefix in PREFIX_CANDIDATES:
        head = "UnityProject/Assets/" + (prefix + "/" if prefix else "")
        for candidate_tail in tails:
            candidate = head + candidate_tail
            if candidate in known:
                return candidate
    return None


def iter_text_files(roots):
    for root in roots:
        for dirpath, _dirnames, filenames in os.walk(root):
            for name in filenames:
                path = os.path.join(dirpath, name)
                ext = os.path.splitext(name)[1].lower()
                if ext in SKIP_EXT:
                    continue
                if ext not in TEXT_EXT:
                    continue
                yield path


def rewrite(remap, roots):
    """Replace every superseded GUID with its shipped counterpart, in place."""
    rewritten, replaced_total = 0, 0
    for path in iter_text_files(roots):
        try:
            with open(path, "rb") as fh:
                data = fh.read()
        except OSError:
            continue
        if b"\x00" in data[:8192]:
            continue
        count = 0
        for current, guid in remap.items():
            needle = current.encode("ascii")
            if needle in data:
                count += data.count(needle)
                data = data.replace(needle, guid.encode("ascii"))
        if count:
            with open(path, "wb") as fh:
                fh.write(data)
            rewritten += 1
            replaced_total += count
    return rewritten, replaced_total


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--apply", action="store_true", help="rewrite files instead of reporting")
    parser.add_argument("--revert", action="store_true",
                        help="undo a previous --apply using the recorded plan")
    args = parser.parse_args()

    if args.revert:
        with open(PLAN_PATH, "r", encoding="utf-8") as fh:
            remap = json.load(fh)["remap"]
        inverse = {guid: current for current, guid in remap.items()}
        log("reverting %d GUIDs ..." % len(inverse))
        rewritten, replaced_total = rewrite(inverse, [ASSETS_ROOT, PACKAGES_ROOT])
        log("  files rewritten            : %d" % rewritten)
        log("  GUID occurrences replaced  : %d" % replaced_total)
        log("NOTE: run G3 (addressables content build) again so the catalog matches the reverted GUIDs.")
        return 0

    log("project .meta GUIDs ...")
    project = {}
    project.update(load_meta_guids(ASSETS_ROOT))
    project.update(load_meta_guids(PACKAGES_ROOT))
    log("  %d assets with a .meta" % len(project))

    log("shipped catalog GUID keys ...")
    shipped, ambiguous, no_location = load_shipped_guid_locations()
    log("  %d GUID keys with exactly one asset location (%d ambiguous, %d bundle-only)"
        % (len(shipped), ambiguous, no_location))

    remap, missing, same, unresolved_by_guid = {}, [], 0, []
    for guid, location in shipped.items():
        path = resolve(location, project)
        if path is None:
            unresolved_by_guid.append((guid, location))
            continue
        current = project[path]
        if current == guid:
            same += 1
            continue
        remap[current] = guid

    log("")
    log("  already aligned            : %d" % same)
    log("  need remapping             : %d" % len(remap))
    log("  shipped GUID, no project   : %d" % len(unresolved_by_guid))

    by_ext = {}
    for guid, location in unresolved_by_guid:
        ext = os.path.splitext(location)[1].lower() or "(none)"
        by_ext[ext] = by_ext.get(ext, 0) + 1
    log("  unresolved by extension    : %s"
        % ", ".join("%s=%d" % kv for kv in sorted(by_ext.items(), key=lambda kv: -kv[1])[:12]))

    # A generated GUID copied onto two different assets would make the rewrite ambiguous.
    inverse = {}
    collisions = []
    for current, guid in remap.items():
        if guid in inverse:
            collisions.append((guid, inverse[guid], current))
        inverse[guid] = current
    log("  collisions (1 shipped GUID -> 2 assets): %d" % len(collisions))
    for guid, a, b in collisions[:10]:
        log("    %s <- %s | %s" % (guid, a, b))

    targets = set(remap)
    log("")
    log("scanning project text files for the %d superseded GUIDs ..." % len(targets))
    hits, files_scanned = 0, 0
    hit_files = []
    for path in iter_text_files([ASSETS_ROOT, PACKAGES_ROOT]):
        files_scanned += 1
        try:
            with open(path, "rb") as fh:
                data = fh.read()
        except OSError:
            continue
        if b"\x00" in data[:8192]:
            continue
        found = {g for g in targets if g.encode("ascii") in data}
        if found:
            hits += len(found)
            hit_files.append((path, len(found)))

    log("  files scanned              : %d" % files_scanned)
    log("  files containing a GUID    : %d" % len(hit_files))
    log("  GUID occurrences (distinct): %d" % hits)
    for path, count in sorted(hit_files, key=lambda kv: -kv[1])[:15]:
        log("    %4d  %s" % (count, os.path.relpath(path, REPO)))

    with open(PLAN_PATH.replace("alignment", "alignment_unresolved"), "w", encoding="utf-8") as fh:
        json.dump([{"guid": g, "location": loc} for g, loc in unresolved_by_guid], fh, indent=1)

    plan = {
        "repo": REPO,
        "shipped_guid_keys": len(shipped),
        "project_assets": len(project),
        "already_aligned": same,
        "remap_count": len(remap),
        "shipped_without_project_asset": len(unresolved_by_guid),
        "unresolved_by_extension": by_ext,
        "collisions": [{"guid": g, "a": a, "b": b} for g, a, b in collisions],
        "files_with_superseded_guid": len(hit_files),
        "remap": remap,
    }
    with open(PLAN_PATH, "w", encoding="utf-8") as fh:
        json.dump(plan, fh, indent=1, sort_keys=True)
    log("")
    log("plan written: %s" % os.path.relpath(PLAN_PATH, REPO))

    if not args.apply:
        log("dry run; re-run with --apply to rewrite")
        return 0

    if collisions:
        log("refusing to apply: %d ambiguous GUID(s)" % len(collisions))
        return 2

    log("")
    log("applying ...")
    rewritten, replaced_total = rewrite(remap, [ASSETS_ROOT, PACKAGES_ROOT])

    log("  files rewritten            : %d" % rewritten)
    log("  GUID occurrences replaced  : %d" % replaced_total)
    log("NOTE: the Addressables catalog still keys these assets by the old GUIDs -- run G3.")
    return 0


if __name__ == "__main__":
    sys.exit(main())
