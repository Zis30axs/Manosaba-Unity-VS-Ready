#!/usr/bin/env python3
"""Generate one csproj per decompiled assembly (sources stay pristine).

Files that the C# compiler rejects because of decompiler artefacts are listed in
work/excluded.json and simply left out of the compilation - they remain readable in
src/decompiled and are listed in the report.
"""
import os, json, shutil, sys

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
SRCDIR = os.path.join(ROOT, "project", "src")
REFJSON = os.path.join(ROOT, "work", "dummy_refs.json")
EXCL = os.path.join(ROOT, "work", "excluded.json")

SKIP_REFS = {
    "mscorlib", "System", "System.Core", "System.Data", "System.Drawing", "System.Xml",
    "System.Xml.Linq", "System.Numerics", "System.Configuration", "System.Runtime.Serialization",
    "Mono.Security", "System.Runtime.CompilerServices.Unsafe", "netstandard", "UnityEngine",
    "System.Runtime", "Windows",
}
# extra reference assemblies a project needs but the IL2CPP metadata does not list
EXTRA_REFS = {
    "ppt-noise": ["UnityEngine.UI", "UnityEngine.UIModule"],
    "GigaCreation.Essentials.Dialogs": ["UnityEngine.CoreModule", "UnityEngine.UIModule", "UnityEngine.UI"],
}

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
    <NoWarn>$(NoWarn);CS0436;CS0433;CS0108;CS0114</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="$(ManosabaSrc)\\{asm}\\**\\*.cs" />
{removes}  </ItemGroup>

{refs}</Project>
'''


def main():
    refs = json.load(open(REFJSON, encoding="utf-8"))
    excluded = json.load(open(EXCL, encoding="utf-8")) if os.path.exists(EXCL) else []
    excluded = [e.replace("/", os.sep) for e in excluded]
    compiled = sorted(d for d in os.listdir(DEC) if os.path.isdir(os.path.join(DEC, d)))
    cset = set(compiled)
    dummies = {f[:-4] for f in os.listdir(os.path.join(ROOT, "src", "il2cpp", "DummyDll"))
               if f.lower().endswith(".dll")}

    # dependency graph -> keep the project reference graph acyclic
    edges = {a: [b for b in refs.get(a, []) if b in cset and b != a] for a in compiled}
    post, seen, order = {}, set(), 0
    for root in compiled:
        if root in seen:
            continue
        st = [(root, 0)]
        seen.add(root)
        while st:
            n, i = st[-1]
            if i < len(edges[n]):
                st[-1] = (n, i + 1)
                w = edges[n][i]
                if w not in seen:
                    seen.add(w)
                    st.append((w, 0))
            else:
                st.pop()
                post[n] = order
                order += 1

    if os.path.isdir(SRCDIR):
        shutil.rmtree(SRCDIR)
    os.makedirs(SRCDIR, exist_ok=True)

    made = []
    for asm in compiled:
        proj_refs, dll_refs = [], []
        for x in refs.get(asm, []):
            if x == asm:
                continue
            if x in cset:
                if post.get(x, 10 ** 9) < post.get(asm, -1):
                    proj_refs.append(x)
                else:
                    dll_refs.append(x)
            elif x in dummies and x not in SKIP_REFS:
                dll_refs.append(x)
        for x in EXTRA_REFS.get(asm, []):
            dll_refs.append(x)
        proj_refs, dll_refs = sorted(set(proj_refs)), sorted(set(dll_refs))

        rem = "".join(f'    <Compile Remove="$(ManosabaSrc)\\{e}" />\n'
                      for e in excluded if e.split(os.sep)[0] == asm)
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
            fh.write(CS.format(asm=asm, removes=rem, refs="".join(parts)))
        made.append(asm)
    print(f"generated {len(made)} projects; excluded files: {len(excluded)}")
    for e in excluded:
        print("   -", e)


if __name__ == "__main__":
    main()
