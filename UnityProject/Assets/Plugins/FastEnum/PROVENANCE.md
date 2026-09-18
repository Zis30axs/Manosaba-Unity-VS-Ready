# Provenance: FastEnum

* Upstream: https://github.com/xin9le/FastEnum
* Distribution: NuGet package `FastEnum` 1.8.0 (official release)
* Package: https://api.nuget.org/v3-flatcontainer/fastenum/1.8.0/fastenum.1.8.0.nupkg
* Package SHA-256: 9B35456CA091A98DB10E050C5D00E2D6D16EC01F2357E0F93949807D308EF833
* Binary used: `lib/netstandard2.0/FastEnum.dll`  (SHA-256: EBC57769D465D133190360B11B3757A467B43C956F25D3FFA6CA6BA8D338BB5F)
* License: MIT  (nuspec: <license type="expression">MIT</license>)
* Installed at: UnityProject/Assets/Plugins/FastEnum/FastEnum.dll

The Il2CppDumper stub had the same identity (`FastEnum, Version=1.8.0.0`) and the same public surface
(`FastEnumUtility.FastEnum` / `FastEnumExtensions` / `LabelAttribute` / `Member`1` /
`NotFoundException`), which is why it could be swapped without touching a single game source: the
genuine assembly is referenced directly and the game code now calls real enum parsing instead of empty
stub bodies.  v1.8 (this version) is a runtime library; xin9le's v2 line is a Roslyn source generator and
would need analyzer-style integration, which the shipped game did not use.

## Companion assembly: System.Runtime.CompilerServices.Unsafe.dll

`FastEnum.dll` carries a real assembly reference to
`System.Runtime.CompilerServices.Unsafe, Version=4.0.4.0, PublicKeyToken=b03f5f7f11d50a3a` (it uses
`Unsafe.As` for its enum conversions).  The Editor tolerates the reference because the asmdefs consuming
FastEnum are compiled with `validateReferences: 0`, but the **Player build's managed linker cannot** — it
fails the whole build:

```
Fatal error in Unity CIL Linker
Mono.Linker.LinkerFatalErrorException: ILLink: error IL1010: Assembly 'WitchTrials.Game, Version=0.0.0.0,
  Culture=neutral, PublicKeyToken=null' cannot be loaded due to failure in processing 'FastEnum,
  Version=1.8.0.0, ...'
  ---> Mono.Cecil.AssemblyResolutionException: Failed to resolve assembly:
       'System.Runtime.CompilerServices.Unsafe, Version=4.0.4.0, PublicKeyToken=b03f5f7f11d50a3a'
```

`PlayerSettings.GetManagedStrippingLevel` is already `Disabled` for Standalone and does **not** skip the
linker in Unity 6 (measured: `stripping_level_before = "Disabled"` and the link stage still ran), so the
assembly itself has to become resolvable.  Unity's Burst package ships exactly that assembly for this
purpose, and that is the copy installed here:

* Source: `Library/PackageCache/com.unity.burst@59eb6f11d242/.Runtime/System.Runtime.CompilerServices.Unsafe.dll`
  (the leading dot keeps that folder out of Unity's asset pipeline, which is why the linker cannot see it
  from there)
* SHA-256: `F53A6EB5584E23AFD094A0AD301A13913EEC0F58FDBFD854F3EE0AB69C76F8E4` (19208 bytes)
* Product version: `6.1.1+ab95a1f103b49919ba02577a99f3173405bb50ca`
* Installed at: `UnityProject/Assets/Plugins/FastEnum/System.Runtime.CompilerServices.Unsafe.dll`
