#!/usr/bin/env python3
"""Generate an IDE-ready solution that compiles the decompiled game sources against the
Il2CppDumper reference assemblies.

  E:\manosaba_re\project\Manosaba.sln
  E:\manosaba_re\project\src\<Project>\<Project>.csproj   -> ..\..\..\src\decompiled\<Asm>\**\*.cs

Only the game's own assemblies are compiled; every other assembly (Unity, plugins) is
referenced from src\il2cpp\DummyDll.  BCL assemblies are NOT referenced (the real
netstandard/.NET ones are used instead).
"""
import os, json, re

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
DUMMY = os.path.join(ROOT, "src", "il2cpp", "DummyDll")
PROJ = os.path.join(ROOT, "project")
SRCDIR = os.path.join(PROJ, "src")

# assemblies that are compiled from the decompiled sources, grouped into projects
GROUPS = {
    "WitchTrials.Game": ["Assembly-CSharp", "CustomInspector", "CustomInspector.Helpers",
                         "ppt-noise", "__Generated"],
    "Elringus.Naninovel": ["Elringus.Naninovel.Runtime", "Elringus.SpriteDicing.Runtime",
                           "Naninovel.Common"],
    "GigaCreation": None,          # filled below with every GigaCreation.*
}

# never referenced: the BCL dummies (the real BCL is used) and empty facades
SKIP_REFS = {
    "mscorlib", "System", "System.Core", "System.Data", "System.Drawing", "System.Xml",
    "System.Xml.Linq", "System.Numerics", "System.Configuration", "System.Runtime.Serialization",
    "Mono.Security", "System.Runtime.CompilerServices.Unsafe", "UnityEngine",
    "netstandard", "Il2CppDummyDll",
}

# files that must not be duplicated when several assemblies live in one project
SKIP_GLOBS = ["**/Properties/AssemblyInfo.cs", "**/UnitySourceGeneratedAssemblyMonoScriptTypes_v1.cs",
              "**/obj/**", "**/bin/**"]


def read_dummy_list():
    return sorted(f[:-4] for f in os.listdir(DUMMY) if f.lower().endswith(".dll"))


def render_csproj(name, asms, refs):
    inc = "\n".join(f'    <Compile Include="..\\..\\..\\src\\decompiled\\{a}\\**\\*.cs" />' for a in asms)
    exc = "\n".join(f'    <Compile Remove="..\\..\\..\\src\\decompiled\\{a}\\{g}" />'
                    for a in asms for g in SKIP_GLOBS)
    ref = "\n".join(
        f'    <Reference Include="{r}">\n'
        f'      <HintPath>$(ManosabaRefs)\\{r}.dll</HintPath>\n'
        f'      <Private>false</Private>\n'
        f'    </Reference>' for r in refs)
    return f'''<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.1</TargetFramework>
    <LangVersion>latest</LangVersion>
    <AssemblyName>{name}</AssemblyName>
    <RootNamespace>{name}</RootNamespace>
    <EnableDefaultCompileItems>false</EnableDefaultCompileItems>
    <GenerateAssemblyInfo>false</GenerateAssemblyInfo>
    <NoWarn>$(NoWarn);CS0108;CS0114;CS0169;CS0414;CS0649;CS1591;CS0628;CS0824;CS8509;CS0067</NoWarn>
    <DebugType>portable</DebugType>
    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>
  </PropertyGroup>

  <ItemGroup>
{inc}
{exc}
  </ItemGroup>

  <ItemGroup>
{ref}
  </ItemGroup>

</Project>
'''


def main():
    os.makedirs(SRCDIR, exist_ok=True)
    all_asms = sorted(d for d in os.listdir(DEC) if os.path.isdir(os.path.join(DEC, d)))
    GROUPS["GigaCreation"] = [a for a in all_asms if a.startswith("GigaCreation.")]
    compiled = {a for v in GROUPS.values() for a in v}
    refs = [r for r in read_dummy_list() if r not in SKIP_REFS and r not in compiled]

    projs = []
    for name, asms in GROUPS.items():
        asms = [a for a in asms if a in all_asms]
        d = os.path.join(SRCDIR, name)
        os.makedirs(d, exist_ok=True)
        with open(os.path.join(d, name + ".csproj"), "w", encoding="utf-8") as fh:
            fh.write(render_csproj(name, asms, refs))
        projs.append((name, asms))
        print(f"{name}: {len(asms)} assemblies, {len(refs)} references")
    print("reference-only assemblies:", len(refs))
    print("NOT compiled (external libs):",
          ", ".join(a for a in all_asms if a not in compiled)[:400])

    # solution + shared props + vscode config are emitted by the sibling scripts
    with open(os.path.join(PROJ, "compile_groups.json"), "w", encoding="utf-8") as fh:
        json.dump({k: v for k, v in projs}, fh, ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
