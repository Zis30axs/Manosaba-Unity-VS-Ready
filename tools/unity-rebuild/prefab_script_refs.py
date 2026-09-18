"""One-off: resolve the m_Script GUIDs in the four excluded prefabs to asset paths, so an
unresolvable script reference ("references runtime script in scene file. Fixing!") is visible as
data instead of guesswork."""
import io
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
PREFABS = os.path.join(REPO, "UnityProject", "Assets", "GameContent", "#WitchTrials", "Prefabs")
FILES = [
    r"Naninovel\UI\CustomUI\SaveLoadUI.prefab",
    r"Naninovel\UI\CustomUI\GalleryUI.prefab",
    r"Naninovel\UI\CustomUI\WitchBook\WitchBookUI.prefab",
    r"General\UI\Modules\Pagination.prefab",
    r"General\Debug\DebugLabel.prefab",
    r"General\UI\Buttons\SimpleCloseButton.prefab",
]

# guid -> asset path, from every .meta the project can see: Assets/, Packages/ and the package cache
# (script GUIDs for TextMeshPro, UniRx, LitMotion ... live in the package cache, not in Assets/).
guid_to_path = {}
roots = [
    os.path.join(REPO, "UnityProject", "Assets"),
    os.path.join(REPO, "UnityProject", "Packages"),
    os.path.join(REPO, "UnityProject", "Library", "PackageCache"),
]
for base in roots:
    for root, _dirs, files in os.walk(base):
        for name in files:
            if not name.endswith(".meta"):
                continue
            path = os.path.join(root, name)
            try:
                text = io.open(path, encoding="utf-8", errors="replace").read(400)
            except OSError:
                continue
            m = re.search(r"^guid:\s*([0-9a-f]{32})", text, re.M)
            if m:
                guid_to_path.setdefault(m.group(1), os.path.relpath(path[:-5], os.path.join(REPO, "UnityProject")))

SCRIPT = re.compile(r"m_Script:\s*\{fileID:\s*(-?\d+),\s*guid:\s*([0-9a-f]{32})")
for rel in FILES:
    path = os.path.join(PREFABS, rel)
    text = io.open(path, encoding="utf-8", errors="replace").read()
    refs = SCRIPT.findall(text)
    print(f"\n{rel}  ({len(refs)} script refs)")
    seen = {}
    for file_id, guid in refs:
        seen.setdefault(guid, 0)
        seen[guid] += 1
    for guid, count in seen.items():
        target = guid_to_path.get(guid)
        print(f"  x{count:<3} {guid}  ->  {target if target else '*** UNRESOLVED GUID ***'}")
