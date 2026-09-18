# Provenance: TrueShadow (placeholder — user-supplied decompiled source)

!!! THIS IS NOT THE LICENSED PACKAGE. !!!
!!! It is a DECOMPILED copy taken from an unrelated project's reverse-engineering dump, supplied by the
!!! project owner to unblock the content build.  The source repository declares NO license, and the
!!! original "True Shadow - UI Soft Shadow and Glow" is a paid Unity Asset Store asset by Le Tai.
!!! Replace this folder with the licensed package when available: the swap is a folder replacement, and
!!! no game source touches it.

* Source repository: https://github.com/dwgx/DrawGuessHelper  (branch `master`)
* Vendored subtree: `game_source/LeTai.TrueShadow/**` (56 `.cs` files fetched individually over
  raw.githubusercontent.com; the dump's `UnitySourceGeneratedAssemblyMonoScriptTypes_v1.cs`, which
  belongs to the *other* game, is excluded)
* Files vendored: 55 `.cs` (namespaces `LeTai`, `LeTai.Effects`, `LeTai.TrueShadow`,
  `LeTai.TrueShadow.PluginInterfaces`)

## Why it is here

Every remaining content-build exclusion was a prefab carrying a `LeTai.TrueShadow.TrueShadow` component,
and Unity refuses to load the Il2CppDumper stub assembly, so the type walk aborted the whole bundle build.
This copy replaces the stub so those prefabs can enter the build.

## What was verified before using it (field-level, against the shipped game)

The stub in `Assets/Plugins/Il2CppStubs/LeTai.TrueShadow.dll` was generated from the *shipped game*, so it
is the authority on which TrueShadow version this project needs.  Comparison:

* stub `LeTai.TrueShadow.TrueShadow`: 46 fields; this source: 43 field declarations
* every field the prefabs serialize is present in both:
  `algorithm, baked, bakedShadows, blendMode, color, cutout, deallocateOnDisable, disableFitCompensation,
  ignoreCasterColor, inset, modifiedFromInspector, offset, offsetAngle, offsetDistance, shadowAsSibling,
  size, skipTextHashing, spread, useCasterAlpha, useGlobalAngle`
* every plugin-interface field is present too: `casterClearColorProvider,
  casterMaterialPropertiesModifier, casterMaterialProvider, casterMeshModifier, casterMeshProvider,
  casterSubMeshMaterialProvider, rendererMaterialModifier, rendererMaterialProvider, rendererMeshModifier`
* the type shape matches: `public class TrueShadow : UIBehaviour, IMeshModifier, ICanvasElement`

## Known limitation

2026-09-18 update: the missing runtime interfaces are now supplied by independently
authored shaders/settings/noise under `Resources/`. See
`Resources/COMPATIBILITY.md`. These are **COMPATIBILITY RECONSTRUCTION**, not
recovered original commercial assets. The following paragraph records the
original pre-repair limitation; it no longer describes shader availability.

The dump contains **only C#** — no `.shader`, `.mat`, textures or `.asmdef`.  An asmdef was authored here
(`LeTai.TrueShadow.asmdef`, assembly name identical to the stub's so the 42 game asmdefs can reference it by
name).  Consequently the *types* resolve and the prefabs can be built, but TrueShadow's rendering has no
shaders/materials behind it; prefab fields pointing at the original package's materials remain unresolved
and are reported as unresolved PPtrs like the other outstanding ones.

## Mechanical adaptations (no semantic change)

Two edits were needed to make decompiler output compile under this project's C# 9; both are recorded here
because they touch vendored code:

1. **File-scoped namespaces → block-scoped** in all 54 affected files (the decompiler emits C# 10
   `namespace X;`, which is CS8773 under C# 9).  Performed by
   `tools/unity-rebuild/convert_file_scoped_namespaces.py` (idempotent; `namespace X;` … EOF becomes
   `namespace X {` … `}`).
2. `LeTai.TrueShadow/TrueShadow.cs`: removed the decompiler's bogus explicit implementation
   `Transform ICanvasElement.get_transform() { return base.transform; }`.  C# rejects an accessor written as
   a method (CS0683), and it is redundant anyway — `Component.transform` already satisfies
   `ICanvasElement.transform`.
