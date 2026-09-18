"""Find and fix script-GUID references that broke when the reconstructed Naninovel stubs were replaced by the
real package.

    python tools/unity-rebuild/remap_naninovel_guids.py            # report only (default)
    python tools/unity-rebuild/remap_naninovel_guids.py --write    # apply the remap

Why this exists: `Assets/Scripts/Game/Elringus.Naninovel.Runtime` and `.../Naninovel.Common` were skeletons of
the real assemblies.  Unity gave their hundreds of `.cs` files *new* GUIDs when the project first imported them,
and every asset our converters produced (440 scenario `Naninovel.Script` assets, prefabs carrying Naninovel
components, ...) recorded those GUIDs in its `m_Script` reference.  Installing the real package
(`Packages/com.elringus.naninovel`) brought the *original* Naninovel GUIDs instead, so every one of those
references now points at nothing and Unity will show the assets with missing scripts.

The mapping is recoverable exactly: the deprecated stub folders still carry their `.cs.meta` files (old GUID ->
source file -> type name), and the real package carries `.cs.meta` files too (type name -> new GUID).  So the fix
is a rename of GUIDs, applied to the assets that reference them - and anything that cannot be mapped is reported
rather than guessed.
"""
from __future__ import annotations

import argparse
import collections
import io
import json
import os
import re
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
ASSETS = os.path.join(REPO, "UnityProject", "Assets")
PACKAGES = os.path.join(REPO, "UnityProject", "Packages")
DEPRECATED = os.path.join(REPO, "work", "deprecated-stub-assemblies")
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "naninovel_guid_remap.json")

GUID_RE = re.compile(r"guid:\s*([0-9a-f]{32})")
TYPE_RE = re.compile(r"^\s*(?:public|internal|private|protected)?\s*(?:sealed\s+|abstract\s+|static\s+|partial\s+)*"
                     r"(?:class|struct|interface|enum)\s+(\w+)", re.M)
NAMESPACE_RE = re.compile(r"^\s*namespace\s+([\w\.]+)", re.M)
# Assets whose m_Script references matter: scenes, prefabs, assets and materials.
SCAN_EXTENSIONS = (".asset", ".prefab", ".unity", ".mat", ".controller", ".playable")


def guid_of_meta(meta_path):
    try:
        text = io.open(meta_path, encoding="utf-8", errors="replace").read()
    except OSError:
        return None
    match = GUID_RE.search(text)
    return match.group(1) if match else None


def declared_types(source_path):
    """(namespace, type) pairs declared in a C# file; the package and the stubs are one type per file."""
    try:
        text = io.open(source_path, encoding="utf-8-sig", errors="replace").read()
    except OSError:
        return []
    namespace_match = NAMESPACE_RE.search(text)
    namespace = namespace_match.group(1) if namespace_match else ""
    return [(namespace, name) for name in TYPE_RE.findall(text)]


def index_folder(root, kind):
    """guid -> {'file', 'types': [...], 'kind'} for every .cs.meta under root."""
    index = {}
    for directory, _, files in os.walk(root):
        for name in files:
            if not name.endswith(".cs.meta"):
                continue
            meta_path = os.path.join(directory, name)
            source_path = meta_path[:-len(".meta")]
            guid = guid_of_meta(meta_path)
            if not guid:
                continue
            index[guid] = {"file": os.path.relpath(source_path, REPO),
                           "types": declared_types(source_path), "kind": kind}
    return index


def project_guids():
    """Every GUID that currently exists for the project, from .meta files.

    Scope matters: a GUID that lives in *any* package (TMP's default font, URP's shaders, ...) is alive, so
    scanning only `Assets` and the Naninovel package would report hundreds of perfectly healthy references as
    dangling.  PackageCache is included because that is where non-embedded packages actually live.
    """
    existing = set()
    roots = [ASSETS, PACKAGES, os.path.join(REPO, "UnityProject", "Library", "PackageCache")]
    for root in roots:
        if not os.path.isdir(root):
            continue
        for directory, _, files in os.walk(root):
            for name in files:
                if name.endswith(".meta"):
                    guid = guid_of_meta(os.path.join(directory, name))
                    if guid:
                        existing.add(guid)
    return existing


def scan_references():
    """asset file -> {referenced guid: occurrences}, for the asset types that hold script references."""
    references = {}
    for directory, _, files in os.walk(ASSETS):
        for name in files:
            if not name.endswith(SCAN_EXTENSIONS):
                continue
            path = os.path.join(directory, name)
            try:
                text = io.open(path, encoding="utf-8", errors="replace").read()
            except OSError:
                continue
            found = collections.Counter(GUID_RE.findall(text))
            if found:
                references[path] = found
    return references


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true", help="apply the remap (default is report only)")
    args = ap.parse_args()

    old_index = index_folder(DEPRECATED, "deprecated-stub")
    new_index = index_folder(os.path.join(PACKAGES, "com.elringus.naninovel"), "naninovel-package")
    existing = project_guids()
    references = scan_references()

    # type full name -> new guid (the real package)
    new_by_type = {}
    for guid, info in new_index.items():
        for namespace, name in info["types"]:
            new_by_type[(namespace + "." + name) if namespace else name] = guid

    # old guid -> new guid, plus the ones that cannot be mapped
    remap, unmapped = {}, []
    for guid, info in old_index.items():
        if guid in existing:
            continue    # still present (should not happen, but never remap something alive)
        resolved = None
        for namespace, name in info["types"]:
            full = (namespace + "." + name) if namespace else name
            if full in new_by_type:
                resolved = (full, new_by_type[full])
                break
        if resolved:
            remap[guid] = {"type": resolved[0], "new_guid": resolved[1], "old_file": info["file"]}
        else:
            unmapped.append({"old_guid": guid, "old_file": info["file"],
                             "types": [".".join(filter(None, t)) for t in info["types"]]})

    dangling = collections.Counter()
    files_by_old_guid = collections.defaultdict(list)
    untouched_guids = collections.Counter()
    for path, found in references.items():
        for guid, count in found.items():
            if guid in existing:
                continue
            if guid in remap:
                dangling[guid] += count
                files_by_old_guid[guid].append(os.path.relpath(path, REPO))
            else:
                untouched_guids[guid] += count

    print(f"deprecated stub scripts indexed : {len(old_index)}")
    print(f"real package scripts indexed    : {len(new_index)}")
    print(f"script references scanned       : {len(references)} asset files")
    print(f"remappable dangling GUIDs       : {len(dangling)} "
          f"({sum(dangling.values())} references in "
          f"{len({f for files in files_by_old_guid.values() for f in files})} files)")
    print(f"dangling GUIDs with no match    : {len(untouched_guids)} ({sum(untouched_guids.values())} references)")
    print("top remappable:")
    for guid, count in dangling.most_common(12):
        info = remap[guid]
        print(f"   {count:>5}  {info['type']:<58} -> {info['new_guid']}")
    if untouched_guids:
        print("unmatched (first 12):")
        for guid, count in untouched_guids.most_common(12):
            print(f"   {count:>5}  {guid}")

    document = {
        "scan": {"assets_with_guid_references": len(references), "deprecated_scripts": len(old_index),
                 "package_scripts": len(new_index)},
        "remap": remap,
        "remap_reference_counts": dict(dangling),
        "files_by_old_guid": {guid: sorted(set(files)) for guid, files in files_by_old_guid.items()},
        "unmatched_dangling": dict(untouched_guids),
        "unmapped_stub_scripts": unmapped,
    }

    if args.write:
        rewritten = 0
        for path, found in references.items():
            replacements = {guid: remap[guid]["new_guid"] for guid in found if guid in remap}
            if not replacements:
                continue
            text = io.open(path, encoding="utf-8", errors="surrogateescape").read()
            for old, new in replacements.items():
                text = re.sub(r"guid:\s*" + old, "guid: " + new, text)
            io.open(path, "w", encoding="utf-8", errors="surrogateescape", newline="").write(text)
            rewritten += 1
        document["rewritten_files"] = rewritten
        print(f"rewrote {rewritten} asset files")

    os.makedirs(os.path.dirname(OUT), exist_ok=True)
    with io.open(OUT, "w", encoding="utf-8") as fh:
        json.dump(document, fh, ensure_ascii=False, indent=1)
    print("wrote " + OUT)
    return 0


if __name__ == "__main__":
    sys.exit(main())
