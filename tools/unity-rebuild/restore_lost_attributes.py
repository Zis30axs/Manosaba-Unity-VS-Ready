"""One-off repair: put back the [NonSerialized] attributes that an over-eager revert removed.

The attribute is re-inserted directly above the same field declaration the original src/decompiled
file had it on, in both the converted tree and the applied copy under
UnityProject/Assets/Scripts/Game."""
import io
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
TARGETS = [
    (r"Assembly-CSharp\WitchTrials.Models\VersionedItem.cs", "WitchTrials.Game"),
    (r"Elringus.Naninovel.Runtime\Naninovel.UI\CustomUI.cs", "Elringus.Naninovel.Runtime"),
    (r"GigaCreation.Tools.UI\GigaCreation.Tools.Ui\SlicedFilledImage.cs", "GigaCreation.Tools.UI"),
]
#: converted path -> applied path (the applier nests files under their namespace directory)
APPLIED = {
    r"Assembly-CSharp\WitchTrials.Models\VersionedItem.cs":
        r"WitchTrials.Game\WitchTrials.Models\VersionedItem.cs",
    r"Elringus.Naninovel.Runtime\Naninovel.UI\CustomUI.cs":
        r"Elringus.Naninovel.Runtime\Naninovel.UI\CustomUI.cs",
    r"GigaCreation.Tools.UI\GigaCreation.Tools.Ui\SlicedFilledImage.cs":
        r"GigaCreation.Tools.UI\GigaCreation.Tools.Ui\SlicedFilledImage.cs",
}
FIELD = re.compile(r"^\s*(?:private|protected|internal|public)[^\n;=]*[;=]", re.MULTILINE)


def attribute_fields(text):
    """Field declarations preceded by [NonSerialized] in the original (line after the attribute)."""
    lines = text.splitlines()
    found = []
    for index, line in enumerate(lines):
        if line.strip() in ("[NonSerialized]", "[System.NonSerialized]"):
            for follow in lines[index + 1:index + 4]:
                if ";" in follow or "=" in follow:
                    found.append(follow.strip())
                    break
    return found


def restore(path, fields):
    text = io.open(path, encoding="utf-8-sig", newline="").read()
    out = []
    inserted = 0
    for line in text.splitlines(keepends=True):
        stripped = line.strip()
        preceded = out[-1].strip() if out else ""
        if stripped in fields and preceded not in ("[NonSerialized]", "[System.NonSerialized]"):
            indent = line[:len(line) - len(line.lstrip())]
            out.append(indent + "[NonSerialized]\n")
            inserted += 1
            fields = [f for f in fields if f != stripped]
        out.append(line)
    if inserted:
        io.open(path, "w", encoding="utf-8", newline="").write("".join(out))
    return inserted


for rel, assembly in TARGETS:
    source = io.open(os.path.join(REPO, "src", "decompiled", rel), encoding="utf-8-sig",
                     errors="replace").read()
    fields = attribute_fields(source)
    print(f"\n{rel}: {len(fields)} annotated field(s) in the original")
    for field in fields:
        print("   ", field[:100])
    for base in (os.path.join(REPO, "tools", "unity-rebuild", "converted", "src", "decompiled", rel),
                 os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game", APPLIED[rel])):
        if not os.path.isfile(base):
            print(f"  skip (missing): {os.path.relpath(base, REPO)}")
            continue
        count = restore(base, list(fields))
        print(f"  restored {count} attribute(s) in {os.path.relpath(base, REPO)}")
