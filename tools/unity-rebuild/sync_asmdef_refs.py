"""Keep the game asmdefs' references consistent with how each third-party assembly is provided.

    python tools/unity-rebuild/sync_asmdef_refs.py [--write]

Two directions, both discovered from the project instead of a hand-kept list:

* a vendored **source** package (``Assets/Plugins/ThirdParty/<X>/<X>.asmdef``) must be an asmdef
  ``references`` entry; a leftover ``<X>.dll`` in ``precompiledReferences`` would not resolve;
* a **precompiled** stub plug-in (``Assets/Plugins/Il2CppStubs/<X>.dll``) must be a
  ``precompiledReferences`` entry, because these asmdefs set ``overrideReferences: true`` and
  ``autoReferenced: false`` -- a plug-in is invisible to them otherwise.

Round 12 context: ``Il2CppDummyDll`` had to become a plug-in (the Il2CppDumper stubs reference it, and a
plug-in cannot reference an asmdef assembly), and the genuine UniRx package replaces its stub as source.
"""
from __future__ import annotations

import argparse
import glob
import io
import json
import os
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
GAME_DIR = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
PLUGIN_DIR = os.path.join(REPO, "UnityProject", "Assets", "Plugins", "Il2CppStubs")
#: source assemblies can live in Assets (vendored drop-ins) or as local UPM packages
SOURCE_DIRS = (
    os.path.join(REPO, "UnityProject", "Assets", "Plugins", "ThirdParty"),
    os.path.join(REPO, "UnityProject", "Packages"),
)


def discover():
    source_assemblies = {}
    for base in SOURCE_DIRS:
        if not os.path.isdir(base):
            continue
        for path in glob.glob(os.path.join(base, "**", "*.asmdef"), recursive=True):
            try:
                name = json.loads(io.open(path, encoding="utf-8-sig").read())["name"]
            except Exception:
                continue
            source_assemblies[name] = os.path.relpath(path, REPO)
    plugin_assemblies = {}
    for path in glob.glob(os.path.join(PLUGIN_DIR, "*.dll")):
        name = os.path.splitext(os.path.basename(path))[0]
        plugin_assemblies[name] = os.path.basename(path)
    return source_assemblies, plugin_assemblies


def patch(path, source_assemblies, plugin_assemblies):
    """Return a list of changes; empty when the file is already consistent."""
    doc = json.loads(io.open(path, encoding="utf-8-sig").read())
    references = list(doc.get("references") or [])
    precompiled = list(doc.get("precompiledReferences") or [])
    changes = []

    for name in sorted(source_assemblies):
        dll = name + ".dll"
        if dll in precompiled:
            precompiled.remove(dll)
            if name not in references:
                references.append(name)
            changes.append(f"{dll} -> asmdef reference {name}")
        elif name not in references:
            references.append(name)
            changes.append(f"+ asmdef reference {name}")

    for name, dll in sorted(plugin_assemblies.items()):
        if name in references:
            references.remove(name)
            if dll not in precompiled:
                precompiled.append(dll)
            changes.append(f"{name} -> precompiled {dll}")

    if not changes:
        return []
    doc["references"] = sorted(set(references))
    if doc.get("overrideReferences"):
        doc["precompiledReferences"] = sorted(set(precompiled))
    io.open(path, "w", encoding="utf-8", newline="\n").write(json.dumps(doc, indent=4) + "\n")
    return changes


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    source_assemblies, plugin_assemblies = discover()
    print(f"vendored source assemblies ({len(source_assemblies)}): {', '.join(sorted(source_assemblies))}")
    print(f"stub plug-ins ({len(plugin_assemblies)}): {', '.join(sorted(plugin_assemblies))}")

    files = sorted(glob.glob(os.path.join(GAME_DIR, "*", "*.asmdef")))
    touched = 0
    for path in files:
        changes = patch(path, source_assemblies, plugin_assemblies)
        if changes:
            touched += 1
            print(f"  {'changed' if args.write else 'would change'} {os.path.relpath(path, REPO)}: "
                  + "; ".join(changes))
    print(f"{'changed' if args.write else 'would change'} {touched} of {len(files)} asmdef(s)")
    return 0


if __name__ == "__main__":
    sys.exit(main())
