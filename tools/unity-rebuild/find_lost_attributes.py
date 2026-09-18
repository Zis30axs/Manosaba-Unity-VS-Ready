"""One-off: find reconstructed files whose pre-existing [NonSerialized] attributes were removed by an
over-eager revert, by comparing the converted tree against its src/decompiled original."""
import io
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
CONVERTED = os.path.join(REPO, "tools", "unity-rebuild", "converted", "src", "decompiled")
ORIGINAL = os.path.join(REPO, "src", "decompiled")
ATTR = re.compile(r"NonSerialized")

missing = []
for root, _dirs, files in os.walk(CONVERTED):
    for name in files:
        if not name.endswith(".cs"):
            continue
        path = os.path.join(root, name)
        rel = os.path.relpath(path, CONVERTED)
        src = os.path.join(ORIGINAL, rel)
        if not os.path.isfile(src):
            continue
        a = len(ATTR.findall(io.open(path, encoding="utf-8", errors="replace").read()))
        b = len(ATTR.findall(io.open(src, encoding="utf-8", errors="replace").read()))
        if a != b:
            missing.append((rel, a, b))

print(f"files where converted/source [NonSerialized] counts differ: {len(missing)}")
for rel, a, b in missing:
    print(f"  converted={a} source={b}  {rel}")

# The applied tree (UnityProject/Assets/Scripts/Game) mirrors converted/, except that
# Assembly-CSharp ships as WitchTrials.Game.  Check it the same way.
APPLIED = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
applied_missing = []
for root, _dirs, files in os.walk(APPLIED):
    for name in files:
        if not name.endswith(".cs"):
            continue
        path = os.path.join(root, name)
        rel = os.path.relpath(path, APPLIED).replace("WitchTrials.Game", "Assembly-CSharp", 1)
        src = os.path.join(ORIGINAL, rel)
        if not os.path.isfile(src):
            continue
        a = len(ATTR.findall(io.open(path, encoding="utf-8", errors="replace").read()))
        b = len(ATTR.findall(io.open(src, encoding="utf-8", errors="replace").read()))
        if a != b:
            applied_missing.append((rel, a, b))

print(f"files where applied/source [NonSerialized] counts differ: {len(applied_missing)}")
for rel, a, b in applied_missing:
    print(f"  applied={a} source={b}  {rel}")
