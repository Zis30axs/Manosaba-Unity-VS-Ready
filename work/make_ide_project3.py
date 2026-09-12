#!/usr/bin/env python3
"""Generate one csproj per decompiled assembly, with a cycle-safe reference graph.

Inter-assembly references are ProjectReferences when that keeps the graph acyclic;
references inside a strongly-connected component (IL2CPP metadata really does contain
circular assembly references) fall back to the Il2CppDumper reference assembly.
"""
import os, json, shutil, sys

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
DUMMY = os.path.join(ROOT, "src", "il2cpp", "DummyDll")
PROJ = os.path.join(ROOT, "project")
SRCDIR = os.path.join(PROJ, "src")
REFJSON = os.path.join(ROOT, "work", "dummy_refs.json")

SKIP_REFS = {
    "mscorlib", "System", "System.Core", "System.Data", "System.Drawing", "System.Xml",
    "System.Xml.Linq", "System.Numerics", "System.Configuration", "System.Runtime.Serialization",
    "Mono.Security", "System.Runtime.CompilerServices.Unsafe", "netstandard", "UnityEngine",
    "System.Runtime", "Windows",
}

GROUP_OF = [
    (("Assembly-CSharp", "CustomInspector", "ppt-noise", "__Generated"), "01-Game"),
    (("Elringus.", "Naninovel"), "02-Naninovel"),
    (("GigaCreation.",), "03-GigaCreation"),
]


def group_for(asm):
    for prefixes, g in GROUP_OF:
        if any(asm == p or asm.startswith(p) for p in prefixes):
            return g
    return "04-Other"


def tarjan(nodes, edges):
    index, stack, on_stack, comps = {}, [], set(), []
    counter = [0]
    sys.setrecursionlimit(10000)

    def strongconnect(v):
        work = [(v, 0)]
        while work:
            node, pi = work[-1]
            if pi == 0:
                index[node] = counter[0]
                low = counter[0]
                counter[0] += 1
                stack.append(node)
                on_stack.add(node)
                lowlink[node] = low
            recurse = False
            for i in range(pi, len(edges[node])):
                w = edges[node][i]
                if w not in index:
                    work[-1] = (node, i + 1)
                    work.append((w, 0))
                    recurse = True
                    break
                elif w in on_stack:
                    lowlink[node] = min(lowlink[node], index[w])
            if recurse:
                continue
            if lowlink[node] == index[node]:
                comp = []
                while True:
                    w = stack.pop()
                    on_stack.discard(w)
                    comp.append(w)
                    if w == node:
                        break
                comps.append(comp)
            work.pop()
            if work:
                parent = work[-1][0]
                lowlink[parent] = min(lowlink[parent], lowlink[node])

    lowlink = {}
    for n in nodes:
        if n not in index:
            strongconnect(n)
    return comps


CS = '''<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>latest</LangVersion>
    <AssemblyName>{asm}</AssemblyName>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <Deterministic>true</Deterministic>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <DocumentationFile></DocumentationFile>
    <NoWarn>$(NoWarn);CS0436;CS0433</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="..\\..\\..\\src\\decompiled\\{asm}\\**\\*.cs" />
    <Compile Remove="..\\..\\..\\src\\decompiled\\{asm}\\obj\\**" />
    <Compile Remove="..\\..\\..\\src\\decompiled\\{asm}\\bin\\**" />
  </ItemGroup>

{refs}</Project>
'''


def main():
    refs = json.load(open(REFJSON, encoding="utf-8"))
    compiled = sorted(d for d in os.listdir(DEC) if os.path.isdir(os.path.join(DEC, d)))
    cset = set(compiled)
    dummies = {f[:-4] for f in os.listdir(DUMMY) if f.lower().endswith(".dll")}

    edges = {a: [b for b in refs.get(a, []) if b in cset and b != a] for a in compiled}
    comps = tarjan(compiled, edges)
    comp_of = {n: i for i, c in enumerate(comps) for n in c}
    cyclic = [c for c in comps if len(c) > 1]
    print("strongly connected components (cycles):", [c for c in cyclic])

    # DFS post-order: keep a ProjectReference only when it points "backwards" in post-order,
    # which guarantees the resulting project graph is acyclic.
    post, seen = {}, set()
    order = 0
    for root in compiled:
        if root in seen:
            continue
        stack = [(root, 0)]
        seen.add(root)
        while stack:
            node, i = stack[-1]
            nb = edges[node]
            if i < len(nb):
                stack[-1] = (node, i + 1)
                w = nb[i]
                if w not in seen:
                    seen.add(w)
                    stack.append((w, 0))
            else:
                stack.pop()
                post[node] = order
                order += 1
    demoted = []

    if os.path.isdir(SRCDIR):
        shutil.rmtree(SRCDIR)
    os.makedirs(SRCDIR, exist_ok=True)

    made = []
    for asm in compiled:
        r = refs.get(asm, [])
        proj_refs, dll_refs = [], []
        for x in r:
            if x == asm:
                continue
            if x in cset:
                if post.get(x, 10 ** 9) < post.get(asm, -1):
                    proj_refs.append(x)
                else:
                    dll_refs.append(x)        # back edge -> would create a cycle
                    demoted.append((asm, x))
            elif x in dummies and x not in SKIP_REFS:
                dll_refs.append(x)
        proj_refs = sorted(set(proj_refs))
        dll_refs = sorted(set(dll_refs))

        parts = []
        if proj_refs:
            parts.append("  <ItemGroup>\n" + "".join(
                f'    <ProjectReference Include="..\\{x}\\{x}.csproj" />\n' for x in proj_refs)
                + "  </ItemGroup>\n\n")
        if dll_refs:
            parts.append("  <ItemGroup>\n" + "".join(
                f'    <Reference Include="{x}">\n'
                f'      <HintPath>$(ManosabaRefs)\\{x}.dll</HintPath>\n'
                f'      <Private>false</Private>\n'
                f'    </Reference>\n' for x in dll_refs)
                + "  </ItemGroup>\n\n")

        d = os.path.join(SRCDIR, asm)
        os.makedirs(d, exist_ok=True)
        with open(os.path.join(d, asm + ".csproj"), "w", encoding="utf-8") as fh:
            fh.write(CS.format(asm=asm, refs="".join(parts)))
        made.append({"project": f"src/{asm}/{asm}.csproj", "group": group_for(asm),
                     "projectRefs": proj_refs, "dllRefs": dll_refs})
    json.dump(made, open(os.path.join(PROJ, "projects.json"), "w", encoding="utf-8"), indent=1)
    print("generated", len(made), "projects")


if __name__ == "__main__":
    main()
