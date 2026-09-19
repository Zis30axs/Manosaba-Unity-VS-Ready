"""Apply the Phase 5 reconstructions (drafted and type-checked in the session scratch folder) to the Unity project.

    python work/title-path/apply_phase5.py <scratch p5 dir>

Copies only the files that carry a reconstruction (not the untouched staging copies), and adds the assembly references the
real code needs:
  ExtendedActors: GigaCreation.NaninovelExtender.Cameras (CameraConfigurationUtility), GigaCreation.NaninovelExtender.Common
                  (SpecialSubId), GigaCreation.Tools.Service (ServiceLocator), Unity.Addressables (AssetReference is part of the
                  IAddressableAssetLoader signature), FastEnum.dll (FastEnum.TryParse / GetNames)
  Common:         FastEnum.dll (SpecialSubId)
"""
import json, os, shutil, sys

ROOT = "E:/Manosaba-VSCodeReady/UnityProject/Assets/Scripts/Game/"
ACTORS = ROOT + "GigaCreation.NaninovelExtender.ExtendedActors/"
COMMON = ROOT + "GigaCreation.NaninovelExtender.Common/"

FILES = {
    "Common/SpecialSubId.cs": COMMON + "GigaCreation.NaninovelExtender.Common/SpecialSubId.cs",
    "Common/WaitForNextFrame.cs": COMMON + "GigaCreation.NaninovelExtender.Common/WaitForNextFrame.cs",
}
for name in ("AnimateActorExtended", "AnimateExtended", "BackgroundManagerExtended", "IMultipliableActor",
             "ModifyActorUtility", "ModifyBackgroundExtended", "MultipliableActorManagerExtensions",
             "MultipliableActorManagerUtility"):
    FILES["ExtendedActors/%s.cs" % name] = ACTORS + "GigaCreation.NaninovelExtender.ExtendedActors/%s.cs" % name


def add(asmdef, key, values):
    with open(asmdef, encoding="utf-8") as fh:
        raw = fh.read()
    data = json.loads(raw)
    for v in values:
        if v not in data[key]:
            data[key].append(v)
    with open(asmdef, "w", encoding="utf-8", newline="\n") as fh:
        fh.write(json.dumps(data, indent=4) + "\n")


def main():
    src = sys.argv[1]
    for rel, dst in FILES.items():
        s = os.path.join(src, rel)
        if not os.path.exists(s):
            raise SystemExit("missing draft " + s)
        if not os.path.exists(dst):
            raise SystemExit("missing target " + dst)
        # keep the target's UTF-8 BOM state: the generated skeletons carry one, the drafts inherit it from the copy
        shutil.copyfile(s, dst)
        print("copied", rel)
    ea = ACTORS + "GigaCreation.NaninovelExtender.ExtendedActors.asmdef"
    add(ea, "references", ["GigaCreation.NaninovelExtender.Cameras", "GigaCreation.NaninovelExtender.Common",
                           "GigaCreation.Tools.Service", "Unity.Addressables"])
    add(ea, "precompiledReferences", ["FastEnum.dll"])
    add(COMMON + "GigaCreation.NaninovelExtender.Common.asmdef", "precompiledReferences", ["FastEnum.dll"])
    print("asmdefs updated")


if __name__ == "__main__":
    main()
