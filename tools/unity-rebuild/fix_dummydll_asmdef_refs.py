"""Point the game asmdefs at Il2CppDummyDll as a *precompiled* reference.

    python tools/unity-rebuild/fix_dummydll_asmdef_refs.py [--write]

Why: the Il2CppDumper stub plug-ins (UniRx, UniTask, LitMotion, TrueShadow, MPUIKit ...) each carry an
assembly reference to `Il2CppDummyDll`, and Unity refuses to load a plug-in whose references cannot be
resolved ("Unloading broken assembly Assets/Plugins/Il2CppStubs/UniTask.dll, this assembly can cause
crashes in the runtime").  That is what made every type from those assemblies unusable and aborted the
content build's type walk.  `Il2CppDummyDll` only existed as an *asmdef* assembly, and a precompiled
DLL cannot reference an asmdef assembly, so the reference never resolved.

Shipping the real `src/il2cpp/DummyDll/Il2CppDummyDll.dll` as a plug-in fixes the stubs, but then the
game asmdefs (which use [Token] / [Address] / [Il2CppDummyDll.FieldOffset]) must reference it as a
precompiled reference: these asmdefs have `overrideReferences: true` and `autoReferenced: false`, so a
plug-in is invisible unless it is named in `precompiledReferences`.
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
ASMDEF_REF = "Il2CppDummyDll"
PRECOMPILED_REF = "Il2CppDummyDll.dll"


def patch(path: str) -> bool:
    """Return True when the file changed; idempotent."""
    text = io.open(path, encoding="utf-8-sig").read()
    doc = json.loads(text)

    references = doc.get("references") or []
    precompiled = doc.get("precompiledReferences") or []
    changed = False

    if ASMDEF_REF in references:
        references = [r for r in references if r != ASMDEF_REF]
        changed = True
    if doc.get("overrideReferences") and PRECOMPILED_REF not in precompiled:
        precompiled = sorted(set(precompiled) | {PRECOMPILED_REF})
        changed = True

    if not changed:
        return False
    doc["references"] = references
    if doc.get("overrideReferences"):
        doc["precompiledReferences"] = precompiled
    io.open(path, "w", encoding="utf-8", newline="\n").write(json.dumps(doc, indent=4) + "\n")
    return True


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    files = sorted(glob.glob(os.path.join(GAME_DIR, "*", "*.asmdef")))
    changed = 0
    for path in files:
        if patch(path) if args.write else (ASMDEF_REF in io.open(path, encoding="utf-8-sig").read()):
            changed += 1
            print(("patched  " if args.write else "would patch  ") + os.path.relpath(path, REPO))
    print(f"{'patched' if args.write else 'would patch'} {changed} of {len(files)} asmdef(s)")
    return 0


if __name__ == "__main__":
    sys.exit(main())
