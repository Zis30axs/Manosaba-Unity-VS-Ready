"""Which remaining stub bodies does the shipped CONTENT actually reach?

    tools/venv/Scripts/python.exe work/title-path/content_stub_census.py

stub_census.py ranks every stub by native size.  This narrows it to the stubs content makes Unity call directly, which
are the ones that fail silently (no caller in reconstructed code to notice): LayerModifier.ModifyLayers was one - a
persistent call of a LayeredActorLayer.onLayerHeld event, so no code path in the sources ever named it.

  * components: every MonoBehaviour script GUID used by the rebuilt prefabs / scenes / assets under Assets/GameContent
    and Assets/Scenes, mapped to its class through the .cs.meta next to the script;
  * Unity messages: stubs named Awake / OnEnable / Start / Update / LateUpdate / FixedUpdate / OnDisable / OnDestroy /
    OnValidate / OnRectTransformDimensionsChange / OnTransformParentChanged / OnDidApplyAnimationProperties /
    OnPointer* / OnBeginDrag / OnDrag / OnEndDrag / OnSelect / OnDeselect / OnSubmit / OnCancel / OnMove on a used
    component (or a base class of one);
  * persistent calls: every m_TargetAssemblyTypeName + m_MethodName of a UnityEvent in that content.

Reads work/title-path/stub_census.json (run stub_census.py first).  Output: content_stub_census.txt.
"""
import collections
import io
import json
import os
import re

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, "..", ".."))
ASSETS = os.path.join(REPO, "UnityProject", "Assets")
GAME = os.path.join(ASSETS, "Scripts", "Game")
CONTENT_DIRS = [os.path.join(ASSETS, "GameContent"), os.path.join(ASSETS, "Scenes"),
                os.path.join(ASSETS, "NaninovelData")]
MESSAGES = {"Awake", "OnEnable", "Start", "Update", "LateUpdate", "FixedUpdate", "OnDisable", "OnDestroy",
            "OnValidate", "OnRectTransformDimensionsChange", "OnTransformParentChanged",
            "OnDidApplyAnimationProperties", "OnPointerEnter", "OnPointerExit", "OnPointerDown", "OnPointerUp",
            "OnPointerClick", "OnBeginDrag", "OnDrag", "OnEndDrag", "OnSelect", "OnDeselect", "OnSubmit", "OnCancel",
            "OnMove", "OnApplicationFocus", "OnApplicationPause", "OnApplicationQuit", "OnBecameVisible",
            "OnBecameInvisible", "OnCanvasGroupChanged", "OnScroll", "OnUpdateSelected", "OnInitializePotentialDrag"}
SCRIPT_REF = re.compile(r"m_Script: \{fileID: 11500000, guid: ([0-9a-f]{32})")
CALL = re.compile(r"m_TargetAssemblyTypeName: ([^\n]*(?:\n {10}[^\n]*)?)\n\s+m_MethodName: (\S+)")
DECL = re.compile(r"\b(?:class|struct)\s+(\w+)(?:<[^>{]*>)?\s*(?::\s*([^{\n]+))?")
NAME = re.compile(r"(\w+)\s*\(")


def script_classes():
    """guid -> (class name, file) for every .cs under Assets/Scripts and Packages (one class per file assumption)."""
    out = {}
    for base in (os.path.join(ASSETS, "Scripts"), os.path.join(REPO, "UnityProject", "Packages")):
        for dirpath, _dirs, files in os.walk(base):
            for f in files:
                if not f.endswith(".cs.meta"):
                    continue
                with io.open(os.path.join(dirpath, f), encoding="utf-8", errors="replace") as fh:
                    m = re.search(r"guid: ([0-9a-f]{32})", fh.read())
                if m:
                    out[m.group(1)] = (f[:-len(".cs.meta")], os.path.join(dirpath, f[:-len(".meta")]))
    return out


def base_classes():
    """class -> direct base class name, from every declaration in the game sources."""
    bases = {}
    for dirpath, _dirs, files in os.walk(GAME):
        for f in files:
            if f.endswith(".cs"):
                with io.open(os.path.join(dirpath, f), encoding="utf-8-sig", errors="replace") as fh:
                    for m in DECL.finditer(fh.read()):
                        if m.group(2):
                            first = m.group(2).split(",")[0].strip()
                            bases.setdefault(m.group(1), re.sub(r"<.*", "", first).split(".")[-1])
    return bases


def main():
    census = json.load(io.open(os.path.join(HERE, "stub_census.json"), encoding="utf-8"))
    classes = script_classes()
    bases = base_classes()
    used = collections.Counter()
    calls = collections.Counter()
    for base in CONTENT_DIRS:
        for dirpath, _dirs, files in os.walk(base):
            for f in files:
                if not f.endswith((".prefab", ".unity", ".asset")):
                    continue
                with io.open(os.path.join(dirpath, f), encoding="utf-8", errors="replace") as fh:
                    text = fh.read()
                for guid in SCRIPT_REF.findall(text):
                    if guid in classes:
                        used[classes[guid][0]] += 1
                for type_name, method in CALL.findall(text):
                    type_name = re.sub(r"\s+", " ", type_name).split(",")[0].strip().split(".")[-1]
                    calls[(type_name, method)] += 1
    # a component also runs its base classes' messages
    used_with_bases = collections.Counter()
    for cls, n in used.items():
        seen = set()
        while cls and cls not in seen:
            seen.add(cls)
            used_with_bases[cls] += n
            cls = bases.get(cls)

    rows = []
    for s in census["stubs"]:
        m = NAME.search(s["member"])
        member = m.group(1) if m else s["member"]
        typ = s["type"]
        why = []
        if member in MESSAGES and used_with_bases.get(typ):
            why.append("message on used component x%d" % used_with_bases[typ])
        if (typ, member) in calls:
            why.append("persistent call x%d" % calls[(typ, member)])
        if why:
            rows.append((s["bytes"], typ, s["member"], s["rva"], s["file"], "; ".join(why)))
    rows.sort(reverse=True)
    lines = ["%6d B  %-40s %-60s %s  %s  [%s]" % r for r in rows]
    lines.append("")
    lines.append("persistent-call targets in content (type, method, count):")
    lines += ["  %-45s %-35s %d" % (t, m, n) for (t, m), n in calls.most_common()]
    out = os.path.join(HERE, "content_stub_census.txt")
    with io.open(out, "w", encoding="utf-8", newline="\n") as fh:
        fh.write("\n".join(lines) + "\n")
    print("%d content-reached stubs, %d bytes -> %s" % (len(rows), sum(r[0] for r in rows), out))
    for line in lines[:40]:
        print(line)


if __name__ == "__main__":
    main()
