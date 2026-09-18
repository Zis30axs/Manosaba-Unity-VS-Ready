"""Compute which prefabs cannot enter an Addressables content build because a component's field type
comes (directly or transitively) from a stub assembly, and write them into the exclusion file.

    python tools/unity-rebuild/plan_exclusions_by_stub_fields.py            # report only
    python tools/unity-rebuild/plan_exclusions_by_stub_fields.py --write    # rewrite content_build_exclusions.json

Why this exists (measured in round 9): the content build aborts in
AddressableAssetEntry.GatherMainAndReferencedSerializedTypes ->
ContentBuildInterface.GetTypeForObjects with

    "Invalid type UniRx.ReactiveProperty`1<int> for instance field WitchTrials.Views.SaveLoadUi:_loadingSlotCount"
    "TypeLoadException: VTable setup of type UniRx.CompositeDisposable failed"
    "Invalid type GigaCreation.Essentials.Ui.SelectableUtility for instance field
     GigaCreation.Essentials.Ui.TweeningToggle:_selectableUtil"

i.e. Unity walks a component's whole field graph, and UniRx/UniTask/LitMotion/... exist here only as
Il2CppDumper stubs (Assets/Plugins/Il2CppStubs/*.dll) whose types cannot be loaded.  Building is
all-or-nothing per bundle, so every prefab carrying such a component has to be excluded until a real
dependency replaces the stub.  Doing that by hand costs one 4-minute build per offender (the build
stops at the first one), which is why the set is computed here instead.

The report is deliberately conservative: it names the offending component type and the field that
drags the stub type in, so each exclusion can be re-checked against the source.
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
BASE = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
PLAN = os.path.join(BASE, "prefab_plan_all.json")
EXCLUSIONS = os.path.join(REPO, "tools", "unity-rebuild", "content_build_exclusions.json")
SOURCES = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")

#: Documentation of what used to be stubs and how each is provided now (round 25: none are stubs any
#: more).  The *decision* below never reads this table -- it is derived from the project -- but keeping the
#: mapping documents why a namespace is or is not in play.
STUB_NAMESPACE_ASSEMBLIES = {
    "UniRx": "UniRx",
    "UniTask": "UniTask",
    "LitMotion": "LitMotion",
    "LeTai": "LeTai.TrueShadow",
    "MPUIKit": "MPUIKit",
    "UIPolygon": "UIPolygon",
    "FastEnum": "FastEnum",
    "ppt-noise": "ppt-noise",
}
PLUGIN_DIR = os.path.join(REPO, "UnityProject", "Assets", "Plugins", "Il2CppStubs")
GAME_DIR = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
PACKAGE_DIR = os.path.join(REPO, "UnityProject", "Packages")
#: Il2CppDummyDll lives in the stub folder but is NOT a stub: it carries the [Token]/[Address]/
#: [FieldOffset] attributes the reconstruction uses, and the Il2CppDumper stubs referenced it (see §23.1).
NOT_A_STUB = {"Il2CppDummyDll"}


def stub_assembly_names():
    """Assemblies that are still Il2CppDumper stubs = the DLLs left in Assets/Plugins/Il2CppStubs.

    Round 25: that set is empty.  Everything downstream (which namespaces count as stub-provided, which
    prefabs need excluding) is derived from it, so vendoring a genuine package or adding a stub is picked
    up automatically instead of needing a hand-kept list -- the two earlier regressions in this file were
    both stale hand-kept lists.
    """
    if not os.path.isdir(PLUGIN_DIR):
        return set()
    return {name[:-4] for name in os.listdir(PLUGIN_DIR)
            if name.endswith(".dll") and name[:-4] not in NOT_A_STUB}


#: The shipped player's predefined assembly is rebuilt as WitchTrials.Game (see
#: tools/unity-rebuild/apply_game_sources.py); the plan records the shipped name.
ASSEMBLY_ALIASES = {"Assembly-CSharp": "WitchTrials.Game"}


def project_assembly_names():
    """Assemblies this project can actually compile: source asmdefs, package asmdefs and plug-in DLLs."""
    names = set()
    bases = (
        GAME_DIR,
        PACKAGE_DIR,
        os.path.join(REPO, "UnityProject", "Assets", "Plugins"),
        # engine/package assemblies are not under Packages/ -- they live in the package cache
        os.path.join(REPO, "UnityProject", "Library", "PackageCache"),
    )
    for base in bases:
        if not os.path.isdir(base):
            continue
        for root, _dirs, files in os.walk(base):
            for name in files:
                if name.endswith(".asmdef"):
                    try:
                        names.add(json.load(io.open(os.path.join(root, name), encoding="utf-8-sig"))["name"])
                    except Exception:
                        pass
                elif name.endswith(".dll"):
                    names.add(name[:-4])
    return names


def effective_stub_namespaces():
    """Namespaces whose assembly is *still* a stub plug-in (empty once every stub is replaced)."""
    stubs = stub_assembly_names()
    return tuple(ns for ns, asm in STUB_NAMESPACE_ASSEMBLIES.items() if asm in stubs)

FIELD = re.compile(
    r"^\s*(?:public|private|protected|internal)\s+(?:readonly\s+|static\s+)*"
    r"(?P<type>[A-Za-z_][A-Za-z0-9_.]*(?:<[^;=]*>)?)\s+(?P<name>[A-Za-z_][A-Za-z0-9_]*)\s*[;=]",
    re.MULTILINE,
)


def unity_path(repo_relative: str) -> str:
    """Plan target (an absolute path under the Unity project) -> Unity asset path."""
    path = str(repo_relative).replace("\\", "/")
    index = path.find("/Assets/")
    return path[index + 1:] if index >= 0 else path


def type_to_assembly():
    """full type name -> assembly directory, from the union of the converted sources."""
    mapping = {}
    for asm in sorted(os.listdir(SOURCES)):
        asm_dir = os.path.join(SOURCES, asm)
        if not os.path.isdir(asm_dir):
            continue
        for root, _dirs, files in os.walk(asm_dir):
            for name in files:
                if not name.endswith(".cs"):
                    continue
                rel = os.path.relpath(os.path.join(root, name), asm_dir).replace("\\", "/")
                mapping[rel[:-3].replace("/", ".")] = asm
    return mapping


def stub_typed_field_types():
    """component type -> the fields that pull a stub type in (directly)."""
    hits = collections.defaultdict(list)
    for asm in sorted(os.listdir(SOURCES)):
        asm_dir = os.path.join(SOURCES, asm)
        if not os.path.isdir(asm_dir):
            continue
        for root, _dirs, files in os.walk(asm_dir):
            for name in files:
                if not name.endswith(".cs"):
                    continue
                path = os.path.join(root, name)
                rel = os.path.relpath(path, asm_dir).replace("\\", "/")
                type_name = rel[:-3].replace("/", ".")
                text = io.open(path, encoding="utf-8", errors="replace").read()
                for match in FIELD.finditer(text):
                    declared = match.group("type").split("<")[0]
                    field_name = match.group("name")
                    # Compiler-generated state-machine members (async/iterator lowering) live in nested
                    # structs, are not serialized, and are not part of the MonoBehaviour's field graph:
                    # including them flagged 224 types instead of the handful that really matter.
                    if "_003C" in field_name or "_003C" in declared or "Awaiter" in declared:
                        continue
                    root_ns = declared.split(".")[0]
                    # bare `ReactiveProperty` / `CompositeDisposable` are UniRx types written without the
                    # namespace prefix, so they only count while UniRx itself is still a stub
                    stub_namespaces = effective_stub_namespaces()
                    uni_rx_is_stub = "UniRx" in stub_namespaces
                    is_stub_type = root_ns in stub_namespaces or (
                        uni_rx_is_stub and declared in ("ReactiveProperty", "CompositeDisposable"))
                    if is_stub_type:
                        hits[type_name].append(type_name + "." + field_name + " : " + match.group("type"))
    return hits


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    direct = stub_typed_field_types()
    print(f"component types with a stub-typed field: {len(direct)}")
    for type_name, fields in sorted(direct.items()):
        print(f"  {type_name}: {fields[0]}")

    # types whose fields reference a type from `direct` also drag the stub type into the walk
    mapping = type_to_assembly()
    affected = set(direct)
    frontier = set(direct)
    for _round in range(4):
        nxt = set()
        for asm in sorted(os.listdir(SOURCES)):
            asm_dir = os.path.join(SOURCES, asm)
            if not os.path.isdir(asm_dir):
                continue
            for root, _dirs, files in os.walk(asm_dir):
                for name in files:
                    if not name.endswith(".cs"):
                        continue
                    path = os.path.join(root, name)
                    rel = os.path.relpath(path, asm_dir).replace("\\", "/")
                    type_name = rel[:-3].replace("/", ".")
                    if type_name in affected:
                        continue
                    text = io.open(path, encoding="utf-8", errors="replace").read()
                    for match in FIELD.finditer(text):
                        declared = match.group("type").split("<")[0]
                        if declared in frontier:
                            nxt.add(type_name)
                            break
        if not nxt:
            break
        affected |= nxt
        frontier = nxt
    print(f"\ncomponent types affected transitively: {len(affected)} "
          f"({len(affected) - len(direct)} via a field referencing an affected type)")

    plan = json.load(io.open(PLAN, encoding="utf-8"))
    stub_assemblies = stub_assembly_names()
    project_assemblies = project_assembly_names()
    not_buildable = collections.Counter()
    prefab_hits = []
    for job in plan["jobs"]:
        scripts = set()
        stub_components = set()
        for obj in job["objects"]:
            if obj.get("type") != "MonoBehaviour":
                continue
            script = obj.get("script") or {}
            name = (script.get("namespace", "") + "." + script.get("class", "")).strip(".")
            if not name:
                continue
            scripts.add(name)
            # A component whose *own* type comes from a stub assembly is fatal: Unity refuses to load the
            # stub assemblies at all ("Unloading broken assembly ... this assembly can cause crashes in the
            # runtime"), which is how LeTai.TrueShadow.TrueShadow ended up failing the type walk.
            #
            # "Is this assembly a stub?" is answered from the project, never from a hand-kept list: stubs
            # are the DLLs still in Il2CppStubs.  Two regressions in this file came from stale lists (the
            # Assets/-vs-Packages/ path bug, and a hard-coded name tuple that kept MPUIKit prefabs excluded
            # after the real package had been vendored).
            #
            # A component whose assembly this project does not have at all is NOT a blocker either: the
            # prefab rebuild cannot create it (PrefabBuilder reports it under missing_types), so no such
            # type ever reaches the content build's type walk.  Those are counted and printed, not excluded.
            assembly = script.get("assembly") or ""
            resolved = ASSEMBLY_ALIASES.get(assembly, assembly)
            if resolved in stub_assemblies:
                stub_components.add(name)
            elif resolved not in project_assemblies:
                not_buildable[assembly] += 1
        hit = sorted(scripts & affected)
        if hit or stub_components:
            prefab_hits.append((unity_path(job["target"]), hit, sorted(stub_components)))

    if not_buildable:
        print("\ncomponents this project cannot build at all (not exclusions, they are simply absent):")
        for assembly, count in not_buildable.most_common(8):
            print(f"  {assembly:24} {count} component(s)")
    print(f"\nprefabs containing such a component: {len(prefab_hits)} of {len(plan['jobs'])}")
    for asset_path, hit, stub in prefab_hits[:30]:
        print(f"  {asset_path.split('Prefabs/')[-1]:58} {(hit or stub)[:3]}")

    if args.write:
        entries = []
        for asset_path, hit, stub_components in prefab_hits:
            field = direct.get(hit[0], ["(transitive)"])[0] if hit else "(component type itself is a stub)"
            what = ", ".join(hit + stub_components)
            entries.append({
                "asset_path": asset_path,
                "reason": ("Contains " + what + ". Unity cannot type-walk it: the offending field is typed by a "
                           "stub assembly (" + field + "), and Unity refuses to load the stub assemblies at all "
                           "(\"Unloading broken assembly Assets/Plugins/Il2CppStubs/UniTask.dll, this assembly can "
                           "cause crashes in the runtime\"). The whole bundle build aborts (observed: Invalid type "
                           "UniRx.ReactiveProperty`1<int> for instance field WitchTrials.Views.SaveLoadUi:_loadingSlotCount; "
                           "Invalid type GigaCreation.Essentials.Ui.SelectableUtility for instance field "
                           "GigaCreation.Essentials.Ui.TweeningToggle:_selectableUtil; VTable setup of type "
                           "LeTai.TrueShadow.TrueShadow failed). Unblock path: real UniRx/UniTask/LitMotion/TrueShadow/"
                           "MPUIKit/UIPolygon dependencies."),
                "evidence": "tools/unity-rebuild/plan_exclusions_by_stub_fields.py (this list is computed, not hand-written) + out/logs/addressables_build_prefabs*.log",
            })
        doc = {
            "_comment": ("Assets that cannot enter an Addressables content build yet. Generated by "
                         "tools/unity-rebuild/plan_exclusions_by_stub_fields.py --write; every entry is "
                         "evidence-backed (see the observed errors in _investigation)."),
            "_investigation": ("The content build aborts on the first component whose field graph reaches a "
                               "stub type (UniRx/UniTask/LitMotion/TrueShadow/MPUIKit/UIPolygon/FastEnum): "
                               "GenerateLocationListsTask -> AddressableAssetEntry.GatherMainAndReferencedSerializedTypes "
                               "-> ContentBuildInterface.GetTypeForObjects throws 'Invalid type ... for instance field ...' "
                               "or 'TypeLoadException: VTable setup of type UniRx.CompositeDisposable failed'. "
                               "Because the prefab graph is rebuilt in full since round 9, more components are present "
                               "and the walk reaches more stub types; the affected prefab set is therefore computed "
                               "from the sources and the plan instead of being discovered one 4-minute build at a time. "
                               "Attempted fix that did NOT work: marking 98 stub-typed fields [NonSerialized] "
                               "(tools/unity-rebuild/annotate_nonserialized_stub_fields.py) -- Unity's type walk "
                               "looks at fields regardless. Real unblock: a genuine UniRx dependency."),
            "excluded": entries,
        }
        with io.open(EXCLUSIONS, "w", encoding="utf-8") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print(f"\nwrote {len(entries)} exclusion(s) to {os.path.relpath(EXCLUSIONS, REPO)}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
