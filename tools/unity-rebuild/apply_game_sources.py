"""Lay the 43 reconstructed game assemblies out as Unity assemblies (G1 step).

Input
  tools/unity-rebuild/converted/src/decompiled/<Assembly>/**.cs   language-converted sources
  tools/unity-rebuild/out/unity/dummy_ref_graph.json              metadata reference graph
  project/projects.json                                           assembly groups
  src/il2cpp/DummyDll/<X>.dll                                     compile-time stubs for the
                                                                  libraries that have no source

Output
  UnityProject/Assets/Scripts/Game/<Assembly>/**.cs               copied sources
  UnityProject/Assets/Scripts/Game/<Assembly>/<Assembly>.asmdef   one assembly definition
  UnityProject/Assets/Plugins/Il2CppStubs/<X>.dll                 referenced stub plug-ins
  tools/unity-rebuild/out/unity/game_sources_manifest.json        what was written, with counts

Reference resolution uses the *metadata* graph (Mono.Cecil over the Il2CppDumper stubs), not the
reconstruction csprojs: those carry at least one spurious edge (Elringus.Naninovel.Runtime ->
Assembly-CSharp) that would make the Unity assembly graph cyclic.  Evidence for every decision is
written into the manifest.

Usage
  python tools/unity-rebuild/apply_game_sources.py [--dry-run]
"""
from __future__ import annotations

import argparse
import hashlib
import json
import os
import re
import shutil
import sys
import datetime
from collections import defaultdict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
CONVERTED = os.path.join(REPO, "tools", "unity-rebuild", "converted", "src", "decompiled")
REF_GRAPH = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "dummy_ref_graph.json")
PROJECTS = os.path.join(REPO, "project", "projects.json")
DUMMY_DIR = os.path.join(REPO, "src", "il2cpp", "DummyDll")
ASSETS_GAME = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
ASSETS_PLUGINS = os.path.join(REPO, "UnityProject", "Assets", "Plugins", "Il2CppStubs")
MANIFEST = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "game_sources_manifest.json")

#: The reconstructed Assembly-CSharp must not collide with Unity's predefined assembly of the
#: same name, so the asmdef is named after the reconstruction's own compile group
#: (project/compile_groups.json -> "WitchTrials.Game").
REMAP = {"Assembly-CSharp": "WitchTrials.Game"}

#: Referenced assemblies satisfied by a Unity package assembly definition (asmdef name).
PACKAGE_ASMDEFS = {
    "Unity.Addressables",
    "Unity.ResourceManager",
    "Unity.InputSystem",
    "Unity.TextMeshPro",
    "Unity.Burst",
    "Unity.Mathematics",
    "Unity.Collections",
    "Unity.Profiling.Core",
    "Unity.ScriptableBuildPipeline",
    "Unity.Timeline",
    "Unity.RenderPipelines.Core.Runtime",
    "Unity.RenderPipelines.Universal.Runtime",
    "Unity.RenderPipelines.Universal.2D.Runtime",
    "Unity.2D.Animation.Runtime",
    "Unity.2D.Common.Runtime",
    "UnityEngine.UI",
    "Unity.TextMeshPro",
}

#: Referenced assemblies satisfied by a precompiled DLL that ships inside a Unity package
#: (com.unity.nuget.newtonsoft-json).  Nothing is copied into Assets/ for these -- copying the
#: Il2CppDumper stub of Newtonsoft.Json would collide with the package's real plug-in.
PACKAGE_PRECOMPILED = {
    "Newtonsoft.Json": "Newtonsoft.Json.dll",
}

#: Referenced assemblies that pull in another one by their public API surface.
#: Addressables returns/consumes AsyncOperationHandle, which is declared in Unity.ResourceManager;
#: without that reference the compiler fails with CS0012 on every member exposing it
#: (observed on WitchTrials.Game: AssetReference.InstantiateAsync / Addressables.ReleaseInstance).
IMPLIED_ASMDEF_REFS = {
    "Unity.Addressables": {"Unity.ResourceManager"},
}

#: (precompiled trigger(s), referenced assembly trigger) -> extra precompiled assembly.
#: UniTask's Addressables integration is its own assembly; the stub metadata attributes most of
#: UnityAsyncExtensions to UniTask.dll, so UniTask.Addressables only becomes necessary when an
#: AsyncOperationHandle extension is actually called
#: (observed: PerspectiveStagePresenter.cs:249 `handle.ToUniTask(...)` -> CS1929).
IMPLIED_PRECOMPILED = [
    (["UniTask.dll", "Unity.Addressables"], {"UniTask.Addressables.dll"}),
]

BCL = ("mscorlib", "System", "netstandard", "Microsoft.", "Mono.", "Il2CppSystem", "Il2Cppmscorlib")
UNITY_MODULE_PREFIXES = ("UnityEngine", "UnityEditor")
UNSAFE_RE = re.compile(r"\bunsafe\b")
NAMESPACE_DECL_RE = re.compile(r"^\s*namespace\s+([A-Za-z_][\w.]*)\s*$", re.M)
USING_RE = re.compile(r"^\s*using\s+(?:static\s+)?([A-Za-z_][\w.]*)\s*;", re.M)
USING_ALIAS_RE = re.compile(r"^\s*using\s+[A-Za-z_]\w*\s*=\s*([A-Za-z_][\w.]*)\s*;", re.M)

#: Namespace prefixes provided by Unity packages / package plug-ins. Longest prefix first.
PACKAGE_NAMESPACE_PREFIXES = sorted([
    ("UnityEngine.AddressableAssets", "Unity.Addressables"),
    ("UnityEngine.ResourceManagement", "Unity.ResourceManager"),
    ("UnityEngine.InputSystem", "Unity.InputSystem"),
    ("UnityEngine.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime"),
    ("UnityEngine.Rendering", "Unity.RenderPipelines.Core.Runtime"),
    ("UnityEngine.Timeline", "Unity.Timeline"),
    ("UnityEngine.UI", "UnityEngine.UI"),
    ("UnityEngine.EventSystems", "UnityEngine.UI"),
    ("Unity.Mathematics", "Unity.Mathematics"),
    ("Unity.Collections", "Unity.Collections"),
    ("Unity.Jobs", "Unity.Collections"),
    ("Unity.Burst", "Unity.Burst"),
    ("Unity.Profiling", "Unity.Profiling.Core"),
    ("TMPro", "Unity.TextMeshPro"),
    ("Newtonsoft.Json", "Newtonsoft.Json.dll"),
], key=lambda t: -len(t[0]))

ENGINE_NAMESPACE_PREFIXES = ("UnityEngine", "UnityEditor", "NUnit", "UnityEngine.TestTools")

#: Namespaces the engine assemblies themselves declare, so nothing has to be referenced:
#: UnityEngine.CoreModule ships the JetBrains annotations (35 public types).
ENGINE_PROVIDED_NAMESPACES = ("JetBrains.Annotations",)

#: Unity's MonoScript source generator emits this type into every assembly it compiles.  The
#: decompiled tree still contains the copy the *original* build had generated, so keeping it
#: duplicates the generated type (CS0101/CS0111/CS0579 x7 per assembly).
UNITY_GENERATED_FILES = {"UnitySourceGeneratedAssemblyMonoScriptTypes_v1.cs"}

STRUCT_RE = re.compile(r"\bstruct\s+(\w+)")

#: `string?` / `T?` reference-type annotations in a file with no nullable context -- CS8632.
NULLABLE_ANNOTATION_RE = re.compile(r"\b(?:string|object|[A-Z]\w*)\?\s*[>\),\w]")
NULLABLE_DIRECTIVE_RE = re.compile(r"^#nullable", re.M)


def enable_nullable_annotations(text):
    """Give decompiled nullable annotations a context without enabling nullable warnings.

    The original assemblies were compiled with the nullable annotation context on; the decompiler
    kept `string?` but not the `#nullable` directive, so C# 9 reports CS8632.  `#nullable enable
    annotations` (annotations only) silences it without turning on the nullable *warning* context,
    so no new diagnostics can appear.  Returns (text, changed).
    """
    if NULLABLE_DIRECTIVE_RE.search(text) or not NULLABLE_ANNOTATION_RE.search(text):
        return text, False
    return "#nullable enable annotations\n" + text, True


def fix_csharp9_struct_ctors(text):
    """Give decompiled struct constructors a definite assignment.

    IL2CPP keeps no managed method bodies, so Il2CppDumper emitted every struct constructor as an
    empty body; C# 11 auto-defaults the fields, C# 9 (what Unity compiles as) reports CS0171.
    `this = default;` reproduces exactly the C# 11 behaviour and keeps the reconstruction on the
    C# version Unity documents support.  Returns (text, patches).
    """
    names = set(STRUCT_RE.findall(text))
    if not names:
        return text, []
    patches = []
    for name in sorted(names):
        pattern = re.compile(
            r"(?P<head>(?:public|internal|private|protected)\s+" + re.escape(name) + r"\s*\([^)]*\)\s*)\{\s*\}")

        def repl(match, _name=name):
            line = text.count("\n", 0, match.start()) + 1
            patches.append({"line": line, "struct": _name,
                            "before": match.group(0).strip(), "after": match.group("head").strip() + " { this = default; }"})
            return match.group("head") + "{ this = default; }"

        text = pattern.sub(repl, text)
    return text, patches


def load_assemblies():
    with open(PROJECTS, encoding="utf-8-sig") as fh:
        projects = json.load(fh)
    groups = {}
    for p in projects:
        groups[os.path.splitext(os.path.basename(p["project"]))[0]] = p.get("group", "")
    return groups


def load_ref_graph():
    # utf-8-sig: the graph is written by Windows PowerShell 5.1 Set-Content, which emits a BOM.
    with open(REF_GRAPH, encoding="utf-8-sig") as fh:
        data = json.load(fh)
    return data["assemblies"], data.get("stub_namespaces", {})


def build_namespace_index(assemblies, game_asmdefs):
    """namespace -> set of asmdef names that declare it, read from the converted sources."""
    index = defaultdict(set)
    for asm in assemblies:
        root_dir = os.path.join(CONVERTED, asm)
        if not os.path.isdir(root_dir):
            continue
        for root, _dirs, names in os.walk(root_dir):
            for n in names:
                if not n.endswith(".cs"):
                    continue
                text = read_source(os.path.join(root, n))
                for m in NAMESPACE_DECL_RE.finditer(text):
                    index[m.group(1)].add(game_asmdefs[asm])
    return index


def read_source(path):
    """Read a converted source file with the BOM removed.

    Every file in the converted tree starts with a UTF-8 BOM, and `^\\s*using` does not match
    past it -- which silently hid the *first* using directive of every file.  In this decompiled
    tree the first using is usually the interesting one (MapArea.cs line 1 is
    `using GigaCreation.Tools.UndoExtensions;`), so the miss showed up as unresolved references.
    """
    with open(path, encoding="utf-8", errors="replace") as fh:
        return fh.read().lstrip("\ufeff")


def build_stub_namespace_index(stub_namespaces, game_assemblies):
    """namespace -> stub DLL file name, for the stubs that are not compiled from source."""
    index = defaultdict(set)
    for dll, namespaces in (stub_namespaces or {}).items():
        stem = dll[:-len(".dll")] if dll.endswith(".dll") else dll
        if stem in game_assemblies:
            continue  # compiled from source instead
        if stem in PACKAGE_ASMDEFS or stem.startswith(ENGINE_NAMESPACE_PREFIXES) or stem.startswith(BCL):
            continue  # provided by a real Unity package / the engine, never by an Il2CppDumper stub
        for ns in namespaces:
            if ns.startswith(ENGINE_NAMESPACE_PREFIXES) or ns.startswith(BCL):
                continue
            index[ns].add(dll)
    return index


def resolve_namespace(ns, ns_to_asmdef, ns_to_stub):
    """Resolve one namespace to (kind, target)."""
    candidate = ns
    for _ in range(6):
        if candidate in ns_to_asmdef:
            return "GAME_SOURCE", sorted(ns_to_asmdef[candidate])[0]
        # Packages win over stubs: the Il2CppDumper tree also contains stub copies of assemblies
        # that the real Unity packages provide (Unity.Collections, TMPro, ...), and importing
        # those stubs would duplicate every type they define.
        for prefix, target in PACKAGE_NAMESPACE_PREFIXES:
            if candidate == prefix or candidate.startswith(prefix + "."):
                # A package plug-in is referenced by name but must never be copied out of the
                # Il2CppDumper tree: the stub of Newtonsoft.Json would shadow the real
                # com.unity.nuget.newtonsoft-json assembly (no method bodies) at runtime.
                if target.endswith(".dll"):
                    return "PRECOMPILED_PACKAGE", target
                return "UNITY_PACKAGE", target
        if candidate in ns_to_stub:
            return "PRECOMPILED", sorted(ns_to_stub[candidate])[0]
        if candidate.startswith(ENGINE_PROVIDED_NAMESPACES):
            return "SKIP", None
        if candidate.startswith(ENGINE_NAMESPACE_PREFIXES) or candidate.startswith(BCL):
            return "SKIP", None
        if "." not in candidate:
            break
        candidate = candidate.rsplit(".", 1)[0]
    return "UNRESOLVED", ns


def namespaces_used_by(root_dir):
    """Every namespace named by a using directive in the assembly's sources."""
    used = set()
    for root, _dirs, names in os.walk(root_dir):
        for n in names:
            if not n.endswith(".cs"):
                continue
            text = read_source(os.path.join(root, n))
            for m in USING_RE.finditer(text):
                used.add(m.group(1))
            for m in USING_ALIAS_RE.finditer(text):
                used.add(m.group(1))
    return used


def edges_of(entry):
    """Referenced assembly names that the stub actually *uses*.

    The AssemblyRef table of an Il2CppDumper stub lists assemblies that no type reference points
    at (the original build recorded them, IL2CPP keeps no usage flag).  Taking those unused rows
    as dependencies creates a phantom cycle --
    Elringus.Naninovel.Runtime -> Assembly-CSharp while Assembly-CSharp -> Elringus.Naninovel.Runtime --
    which Unity rejects with "One or more cyclic dependencies detected between assemblies".
    A dependency is therefore only real when at least one TypeReference resolves into it.
    """
    counts = entry.get("type_ref_counts") or {}
    used = {name for name, n in counts.items() if n}
    if used:
        return used, sorted(set(entry.get("assembly_refs") or []) - used)
    return set(entry.get("assembly_refs") or []), []


def find_cycles(nodes, edges):
    """Tarjan SCC; returns the strongly connected components with more than one node."""
    index, stack, on_stack = [0], [], set()
    indices, low, out = {}, {}, []
    sys.setrecursionlimit(20000)

    def strongconnect(v):
        indices[v] = low[v] = index[0]
        index[0] += 1
        stack.append(v)
        on_stack.add(v)
        for w in sorted(edges.get(v, ())):
            if w not in indices:
                strongconnect(w)
                low[v] = min(low[v], low[w])
            elif w in on_stack:
                low[v] = min(low[v], indices[w])
        if low[v] == indices[v]:
            comp = []
            while True:
                w = stack.pop()
                on_stack.discard(w)
                comp.append(w)
                if w == v:
                    break
            if len(comp) > 1:
                out.append(sorted(comp))

    for v in sorted(nodes):
        if v not in indices:
            strongconnect(v)
    return out


def classify(ref, game_asmdefs, dummy_files):
    """Return (kind, target) for one referenced assembly name."""
    if ref in game_asmdefs:
        return "GAME_SOURCE", game_asmdefs[ref]
    if ref in PACKAGE_PRECOMPILED:
        return "PRECOMPILED_PACKAGE", PACKAGE_PRECOMPILED[ref]
    if ref in PACKAGE_ASMDEFS:
        return "UNITY_PACKAGE", ref
    if ref.startswith(UNITY_MODULE_PREFIXES):
        return "UNITY_MODULE", None
    if ref.startswith(BCL) or ref == "Il2CppDummyDll":
        return "BCL" if ref != "Il2CppDummyDll" else "GAME_SOURCE", game_asmdefs.get(ref)
    if ref + ".dll" in dummy_files:
        return "PRECOMPILED", ref + ".dll"
    return "UNRESOLVED", None


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--dry-run", action="store_true")
    args = ap.parse_args()

    groups = load_assemblies()
    graph, stub_namespaces = load_ref_graph()
    assemblies = sorted(graph)
    game_asmdefs = {a: REMAP.get(a, a) for a in assemblies}
    dummy_files = set(os.listdir(DUMMY_DIR))
    game_assemblies = set(assemblies)
    ns_to_asmdef = build_namespace_index(assemblies, game_asmdefs)
    ns_to_stub = build_stub_namespace_index(stub_namespaces, game_assemblies)

    if os.path.isdir(CONVERTED):
        on_disk = {d for d in os.listdir(CONVERTED) if os.path.isdir(os.path.join(CONVERTED, d))}
    else:
        raise SystemExit(f"converted tree missing: {CONVERTED}")
    missing_src = sorted(set(assemblies) - on_disk)

    if not args.dry_run and os.path.isdir(ASSETS_GAME):
        shutil.rmtree(ASSETS_GAME)
    if not args.dry_run and os.path.isdir(ASSETS_PLUGINS):
        shutil.rmtree(ASSETS_PLUGINS)

    manifest = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "converted_tree": os.path.relpath(CONVERTED, REPO).replace("\\", "/"),
        "ref_graph": os.path.relpath(REF_GRAPH, REPO).replace("\\", "/"),
        "assemblies": [],
        "stub_plugins": [],
        "unresolved_references": {},
        "unresolved_namespaces": {},
        "notes": [
            "Assembly-CSharp is renamed to WitchTrials.Game so it cannot collide with Unity's "
            "predefined assembly of the same name (compile group name from project/compile_groups.json).",
            "References come from the Il2CppDumper stub metadata (dummy_ref_graph.json). The "
            "reconstruction csprojs additionally list Assembly-CSharp for 8 assemblies where the "
            "metadata contains no type reference to it; that edge is dropped because Unity "
            "assembly definitions cannot be cyclic.",
            "Assemblies resolved to a precompiled DLL are Il2CppDumper stubs: they satisfy the "
            "compile but have no method bodies, so they must be replaced before G4/G5.",
        ],
    }

    total_cs = 0
    plugins_needed = set()
    game_edges = {}
    csharp9_patches = []
    nullable_patches = []
    skipped_for_acyclicity = []

    # ---- pass 1: resolve every assembly's references -----------------------
    # Metadata edges are authoritative; namespace edges only *add* what the stub metadata missed.
    # A namespace can be declared by several assemblies (partial namespaces), so a namespace edge
    # is a guess: it is dropped again when it would close a cycle, which Unity refuses outright.
    plan = {}
    for asm in assemblies:
        src_dir = os.path.join(CONVERTED, asm)
        if not os.path.isdir(src_dir):
            continue

        asmdef_name = game_asmdefs[asm]
        refs, precompiled, kinds, unresolved = set(), set(), {}, []
        used_refs, unused_refs = edges_of(graph[asm])
        for ref in sorted(used_refs):
            if ref == asm:
                continue
            kind, target = classify(ref, game_asmdefs, dummy_files)
            kinds[ref] = kind
            if kind in ("GAME_SOURCE", "UNITY_PACKAGE"):
                if target:
                    refs.add(target)
            elif kind == "PRECOMPILED":
                precompiled.add(target)
                plugins_needed.add(target)
            elif kind == "PRECOMPILED_PACKAGE":
                precompiled.add(target)
            elif kind == "UNRESOLVED":
                unresolved.append(ref)

        # namespace-level candidates (metadata under-reports namespaces that are only imported)
        ns_kinds = {}
        unresolved_ns = []
        ns_candidates = []
        for ns in sorted(namespaces_used_by(src_dir)):
            kind, target = resolve_namespace(ns, ns_to_asmdef, ns_to_stub)
            ns_kinds[ns] = kind
            if kind == "GAME_SOURCE":
                if target and target != asmdef_name:
                    ns_candidates.append(target)
            elif kind == "UNITY_PACKAGE":
                if target:
                    refs.add(target)
            elif kind in ("PRECOMPILED", "PRECOMPILED_PACKAGE"):
                if target:
                    precompiled.add(target)
                    if kind == "PRECOMPILED":
                        plugins_needed.add(target)
            elif kind == "UNRESOLVED":
                unresolved_ns.append(ns)

        plan[asm] = {
            "asmdef_name": asmdef_name,
            "src_dir": src_dir,
            "refs": refs,
            "ns_candidates": sorted(set(ns_candidates) - refs),
            "metadata_game_refs": {r for r in refs if r in set(game_asmdefs.values())} - {asmdef_name},
            "precompiled": precompiled,
            "kinds": kinds,
            "unresolved": unresolved,
            "ns_kinds": ns_kinds,
            "unresolved_ns": unresolved_ns,
            "unused_refs": unused_refs,
        }

    # ---- pass 2: add namespace edges, never closing a cycle ----------------
    # Seed with the metadata edges first: they are authoritative, and a cycle check that ignored
    # them would happily add a namespace edge that closes a cycle through a metadata edge.
    for asm in assemblies:
        entry = plan.get(asm)
        if not entry:
            continue
        if entry["metadata_game_refs"]:
            game_edges.setdefault(entry["asmdef_name"], set()).update(entry["metadata_game_refs"])

    for asm in assemblies:
        entry = plan.get(asm)
        if not entry:
            continue
        for target in entry["ns_candidates"]:
            game_edges.setdefault(entry["asmdef_name"], set())
            game_edges[entry["asmdef_name"]].add(target)
            if find_cycles(list(plan[a]["asmdef_name"] for a in plan), game_edges):
                game_edges[entry["asmdef_name"]].discard(target)
                skipped_for_acyclicity.append(
                    {"from": entry["asmdef_name"], "to": target, "reason": "namespace edge would close an assembly cycle"})
            else:
                entry["refs"].add(target)

    # ---- pass 3: write sources and asmdefs --------------------------------
    for asm in assemblies:
        entry = plan.get(asm)
        if not entry:
            manifest["assemblies"].append({"assembly": asm, "status": "MISSING_SOURCE"})
            continue

        asmdef_name = entry["asmdef_name"]
        src_dir = entry["src_dir"]
        for implied in list(entry["refs"]):
            entry["refs"].update(IMPLIED_ASMDEF_REFS.get(implied, set()))
        for triggers, extra in IMPLIED_PRECOMPILED:
            if all(t in entry["precompiled"] or t in entry["refs"] for t in triggers):
                entry["precompiled"].update(extra)
                plugins_needed.update(extra)
        refs = entry["refs"]
        precompiled = entry["precompiled"]
        kinds = entry["kinds"]
        unresolved = entry["unresolved"]
        unused_refs = entry["unused_refs"]
        ns_kinds = entry["ns_kinds"]
        unresolved_ns = entry["unresolved_ns"]

        # ---- copy sources, measure -------------------------------
        files = []
        unsafe_files = 0
        skipped_generated = 0
        for root, _dirs, names in os.walk(src_dir):
            for n in names:
                if not n.endswith(".cs"):
                    continue
                if n in UNITY_GENERATED_FILES:
                    skipped_generated += 1
                    continue
                src = os.path.join(root, n)
                rel = os.path.relpath(src, src_dir)
                files.append(rel)
                with open(src, encoding="utf-8", errors="replace") as fh:
                    text = fh.read()
                if UNSAFE_RE.search(text):
                    unsafe_files += 1
                text, patches = fix_csharp9_struct_ctors(text)
                for p in patches:
                    p["file"] = f"{asmdef_name}/{rel.replace(os.sep, '/')}"
                    csharp9_patches.append(p)
                text, nullable_fixed = enable_nullable_annotations(text)
                if nullable_fixed:
                    nullable_patches.append(f"{asmdef_name}/{rel.replace(os.sep, '/')}")
                if not args.dry_run:
                    dst = os.path.join(ASSETS_GAME, asmdef_name, rel)
                    os.makedirs(os.path.dirname(dst), exist_ok=True)
                    with open(dst, "w", encoding="utf-8", newline="") as fh:
                        fh.write(text)

        total_cs += len(files)
        editor_only = "UnityEditor" in " ".join(graph[asm].get("assembly_refs", []))
        asmdef = {
            "name": asmdef_name,
            "rootNamespace": "",
            "references": sorted(refs),
            "includePlatforms": ["Editor"] if editor_only else [],
            "excludePlatforms": [],
            "allowUnsafeCode": unsafe_files > 0,
            "overrideReferences": True,
            "precompiledReferences": sorted(precompiled),
            "autoReferenced": False,
            "defineConstraints": [],
            "versionDefines": [],
            "noEngineReferences": False,
        }
        if not args.dry_run:
            path = os.path.join(ASSETS_GAME, asmdef_name, asmdef_name + ".asmdef")
            with open(path, "w", encoding="utf-8") as fh:
                json.dump(asmdef, fh, indent=4)
                fh.write("\n")

        digest = hashlib.sha256("\n".join(sorted(files)).encode("utf-8")).hexdigest()[:16]
        manifest["assemblies"].append({
            "assembly": asm,
            "asmdef": asmdef_name,
            "group": groups.get(asm, ""),
            "source_files": len(files),
            "skipped_unity_generated_files": skipped_generated,
            "unsafe_files": unsafe_files,
            "editor_only": editor_only,
            "references": sorted(refs),
            "precompiled_references": sorted(precompiled),
            "reference_kinds": kinds,
            "namespace_reference_kinds": ns_kinds,
            "unresolved_references": sorted(unresolved),
            "unresolved_namespaces": unresolved_ns,
            "unused_assembly_refs": unused_refs,
            "file_list_sha256_16": digest,
        })
        if unresolved:
            manifest["unresolved_references"][asm] = sorted(unresolved)
        if unresolved_ns:
            manifest["unresolved_namespaces"][asm] = unresolved_ns
        game_edges[asmdef_name] = {r for r in refs if r in set(game_asmdefs.values())}

    # ---- stub plug-ins -----------------------------------------------------
    for dll in sorted(plugins_needed):
        src = os.path.join(DUMMY_DIR, dll)
        entry = {"dll": dll, "exists": os.path.exists(src)}
        if os.path.exists(src):
            entry["bytes"] = os.path.getsize(src)
            entry["sha256"] = hashlib.sha256(open(src, "rb").read()).hexdigest()[:16]
            entry["kind"] = "il2cppdumper-stub"
            if not args.dry_run:
                os.makedirs(ASSETS_PLUGINS, exist_ok=True)
                shutil.copy2(src, os.path.join(ASSETS_PLUGINS, dll))
        manifest["stub_plugins"].append(entry)

    manifest["assembly_count"] = len(assemblies)
    manifest["source_file_total"] = total_cs
    manifest["missing_source_assemblies"] = missing_src
    manifest["dry_run"] = args.dry_run
    manifest["csharp9_source_patch_count"] = len(csharp9_patches)
    manifest["csharp9_source_patches"] = csharp9_patches
    manifest["nullable_annotation_files"] = nullable_patches

    # ---- acyclic guard -----------------------------------------------------
    # Unity refuses to compile a cyclic assembly graph ("One or more cyclic dependencies detected
    # between assemblies"), and the failure is reported far away from its cause, so the graph is
    # verified here before the manifest is published.
    cycles = find_cycles(list(game_edges), game_edges)
    manifest["assembly_edges"] = {k: sorted(v) for k, v in sorted(game_edges.items()) if v}
    manifest["cyclic_groups"] = cycles
    manifest["edges_skipped_for_acyclicity"] = skipped_for_acyclicity
    if cycles:
        manifest["status"] = "CYCLIC"
        print(f"CYCLIC assembly groups: {cycles}", file=sys.stderr)
    else:
        manifest["status"] = "ACYCLIC"

    if not args.dry_run:
        os.makedirs(os.path.dirname(MANIFEST), exist_ok=True)
    with open(MANIFEST, "w", encoding="utf-8") as fh:
        json.dump(manifest, fh, ensure_ascii=False, indent=1)

    print(f"assemblies      : {len(assemblies)}  (missing sources: {len(missing_src)})")
    print(f"source files    : {total_cs}   (skipped Unity-generated: see manifest)")
    print(f"csharp9 patches : {len(csharp9_patches)} empty struct constructors -> 'this = default;'")
    print(f"stub plug-ins   : {[p['dll'] for p in manifest['stub_plugins']]}")
    print(f"unresolved refs : {manifest['unresolved_references']}")
    print(f"unresolved ns   : {manifest['unresolved_namespaces']}")
    print(f"cyclic groups   : {cycles}")
    print(f"unsafe asmdefs  : {[a['asmdef'] for a in manifest['assemblies'] if isinstance(a, dict) and a.get('unsafe_files')]}")
    print(f"editor-only     : {[a['asmdef'] for a in manifest['assemblies'] if isinstance(a, dict) and a.get('editor_only')]}")
    print(f"manifest        : {MANIFEST}")
    return 2 if cycles else 0


if __name__ == "__main__":
    sys.exit(main())
