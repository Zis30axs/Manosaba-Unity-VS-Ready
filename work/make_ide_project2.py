#!/usr/bin/env python3
"""Generate one csproj per decompiled assembly (faithful to the original game layout).

  project/src/<Assembly>/<Assembly>.csproj
    <Compile>  -> ..\..\..\src\decompiled\<Assembly>\**\*.cs
    <ProjectReference> for assemblies we also compile   (full source navigation)
    <Reference HintPath> for third-party / Unity dummies the game links against
"""
import os, json, shutil

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
DUMMY = os.path.join(ROOT, "src", "il2cpp", "DummyDll")
PROJ = os.path.join(ROOT, "project")
SRCDIR = os.path.join(PROJ, "src")
REFJSON = os.path.join(ROOT, "work", "dummy_refs.json")

# never referenced as files: the real BCL is used, and these are empty facades
SKIP_REFS = {
    "mscorlib", "System", "System.Core", "System.Data", "System.Drawing", "System.Xml",
    "System.Xml.Linq", "System.Numerics", "System.Configuration", "System.Runtime.Serialization",
    "Mono.Security", "System.Runtime.CompilerServices.Unsafe", "netstandard", "UnityEngine",
    "Windows", "UnityEngine.UI?", "System.Runtime",
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


CS = '''<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>latest</LangVersion>
    <AssemblyName>{asm}</AssemblyName>
    <RootNamespace>{rootns}</RootNamespace>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
    <Deterministic>true</Deterministic>
    <AppendTargetFrameworkToOutputPath>false</AppendTargetFrameworkToOutputPath>
    <DocumentationFile />
  </PropertyGroup>

  <ItemGroup>
    <Compile Include="..\\..\\..\\src\\decompiled\\{asm}\\**\\*.cs" />
    <Compile Remove="..\\..\\..\\src\\decompiled\\{asm}\\obj\\**" />
    <Compile Remove="..\\..\\..\\src\\decompiled\\{asm}\\bin\\**" />
  </ItemGroup>

{projectrefs}{dllrefs}</Project>
'''

PREF = '''  <ItemGroup>
{items}  </ItemGroup>

'''

DREF = '''  <ItemGroup>
{items}  </ItemGroup>

'''


def main():
    refs = json.load(open(REFJSON, encoding="utf-8"))
    compiled = sorted(d for d in os.listdir(DEC) if os.path.isdir(os.path.join(DEC, d)))
    compiled_set = set(compiled)
    dummy_set = {f[:-4] for f in os.listdir(DUMMY) if f.lower().endswith(".dll")}

    if os.path.isdir(SRCDIR):
        shutil.rmtree(SRCDIR)
    os.makedirs(SRCDIR, exist_ok=True)

    made = []
    for asm in compiled:
        # root namespace = the most common namespace-ish folder? keep it simple:
        rootns = asm.replace(".", "_")
        r = refs.get(asm, [])
        pj = sorted(x for x in r if x in compiled_set and x != asm)
        dll = sorted(x for x in r if x not in compiled_set and x not in SKIP_REFS and x in dummy_set)

        projectrefs = ""
        if pj:
            projectrefs = PREF.format(items="".join(
                f'    <ProjectReference Include="..\\{x}\\{x}.csproj" />\n' for x in pj))
        dllrefs = ""
        if dll:
            dllrefs = DREF.format(items="".join(
                f'    <Reference Include="{x}">\n'
                f'      <HintPath>$(ManosabaRefs)\\{x}.dll</HintPath>\n'
                f'      <Private>false</Private>\n'
                f'    </Reference>\n' for x in dll))
        d = os.path.join(SRCDIR, asm)
        os.makedirs(d, exist_ok=True)
        with open(os.path.join(d, asm + ".csproj"), "w", encoding="utf-8") as fh:
            fh.write(CS.format(asm=asm, rootns=rootns, projectrefs=projectrefs, dllrefs=dllrefs))
        made.append((asm, group_for(asm), len(pj), len(dll)))

    print(f"generated {len(made)} projects")
    for asm, g, a, b in made:
        print(f"  {g:16s} {asm:52s} proj={a:2d} dll={b:2d}")
    json.dump([{"project": f"src/{a}/{a}.csproj", "group": g} for a, g, _, _ in made],
              open(os.path.join(PROJ, "projects.json"), "w", encoding="utf-8"), indent=1)


if __name__ == "__main__":
    main()
