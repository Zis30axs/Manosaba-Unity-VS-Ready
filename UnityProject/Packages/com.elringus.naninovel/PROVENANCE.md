# Provenance: com.elringus.naninovel (Naninovel visual-novel engine)

This directory is **not** reconstructed from the shipped game build - it is the real Naninovel package,
retrieved from a public repository, because the engine's method bodies cannot be recovered from IL2CPP metadata
(metadata gives signatures, not bodies) and no copy of the source exists inside the game's own install.

## What it is

| | |
| --- | --- |
| Package | `com.elringus.naninovel` (Naninovel, by Elringus) |
| `package.json` version | **1.20.250922** |
| `Resources/Naninovel/EngineVersion.asset` | `engineVersion: 1.20`, `buildDate: 2025-09-22` |
| Source repository | https://github.com/DoomsGuardians/Project-Cannon-and-Candle |
| Path inside that repository | `Packages/com.elringus.naninovel` |
| Commit | `e1aeb35cec13972809bcec9f9554b64421c3d120` (`main`, fetched 2026-09-17) |
| Retrieval | `git init` + `git remote add origin <repo>` + `core.sparseCheckout=true` with `Packages/com.elringus.naninovel/*`, then `git fetch --depth 1 --filter=blob:none origin main` and `git checkout FETCH_HEAD` - a partial clone, so the 2.15 GB repository itself was never downloaded |
| Included | 2,155 files / 9.5 MB - `Runtime/` (723 `.cs`), `Editor/`, `Prefabs/`, `Resources/`, `Fonts/`, `Textures/`, `Materials/`, `Plugins/`, `package.json` |
| Excluded on purpose | `Documentation~/` (PDF guide) and `E2E/` (engine test suite; would pull extra dependencies) |
| `Plugins/Naninovel.Common.dll` | SHA-256 `0399568748E027E538C75C08C48C50BC53349B24A01E54540131CBE57A63C3EA` |

## The game's own Naninovel build (for comparison)

Read out of the shipped build itself - `manosaba_Data/resources.assets`, object `path_id 1486`,
class `Naninovel.EngineVersion` (76 bytes):

```
m_Name        = "EngineVersion"
engineVersion = "1.20"
preview       = false
buildDate     = "2025-04-26"
```

So this project's target is Naninovel **1.20, built 2025-04-26**, while the package above is **1.20,
built 2025-09-22** - the same 1.20 line, about five months apart. That is a deliberate, measured substitution:

* the reconstructed game code is compiled against the real engine API by gate G1, so any signature drift shows
  up as a compile error at an exact file/line rather than as silent misbehaviour;
* every such site is being recovered from the April build's native code (which encodes what the game actually
  called) and each is documented where the April and September APIs disagree.

## Licensing

Naninovel is a **commercial** asset (EULA: https://naninovel.com/eula); it is redistributed inside the public
repository named above, not by this project. It is used here under the project owner's instruction to reference
the commercial package directly rather than reverse-engineer it, and it should be replaced by a licensed copy
(the asset as distributed with the owner's own purchase) before any redistribution of this reconstruction.

## What this package replaced

`UnityProject/Assets/Scripts/Game/Elringus.Naninovel.Runtime` and `.../Naninovel.Common` were skeleton
reconstructions of the same two assemblies (4,805 declared methods, of which 4,660 had empty bodies). They were
moved to `work/deprecated-stub-assemblies/` so the real assemblies can provide those type identities - two
assemblies with the same name cannot coexist in one Unity project. Assembly references that named
`Naninovel.Common` as an assembly now use the package's precompiled `Naninovel.Common.dll`
(`precompiledReferences`), which is how the real package itself refers to it.

## Known consequence: SpriteDicing integration is compiled out

Naninovel wraps its SpriteDicing integration (`Runtime/Actor/DicedSprite/*.cs`) in `#if SPRITE_DICING_AVAILABLE`,
a symbol defined by this package's `versionDefines` entry for the package `com.elringus.spritedicing`. That
package is **not** present: the shipped game contains its assembly (`Elringus.SpriteDicing.Runtime.dll`, image 116)
but SpriteDicing is a separate Elringus package whose source is neither in the game build nor in the repository
above, so only a skeleton exists locally (`SpriteDicing.DicedSpriteAtlas`, the single type recovered from
metadata). Consequently `GigaCreation.NaninovelExtender.ExtendedActors/DicedSpriteCharacterExtended.cs` is
guarded with the same symbol and does not compile until a real SpriteDicing package is installed - the game's
content does use it (`SpriteDicing.DicedSpriteAtlas` assets such as `CreatureAlisa`/`CreatureAnAn` in the
Addressables catalog), so this is a recorded functional gap.

## Reproducing the retrieval

```powershell
$dst = 'work\naninovel-src'
New-Item -ItemType Directory -Force $dst | Out-Null
git -C $dst init -q
git -C $dst remote add origin https://github.com/DoomsGuardians/Project-Cannon-and-Candle.git
git -C $dst config core.sparseCheckout true
Set-Content "$dst\.git\info\sparse-checkout" @(
  'Packages/com.elringus.naninovel/*',
  '!Packages/com.elringus.naninovel/Documentation~/*',
  '!Packages/com.elringus.naninovel/E2E/*')
git -C $dst fetch --depth 1 --filter=blob:none origin main
git -C $dst checkout -q FETCH_HEAD
```
