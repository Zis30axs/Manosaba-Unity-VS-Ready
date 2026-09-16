# Stub inventory & dependency report — Unity rebuild (Manosaba)

Deliverables in this directory: `dependency_matrix.csv`, `stub_inventory.csv`, `stub_report.md`.
All three were produced in this session. Nothing outside `tools/unity-rebuild/out/` was modified.

## 0. What was actually executed (and what was not)

Executed (all read-only over inputs):

* Read `UNITY_READINESS_CODEX_AUDIT.md` (275 lines) — §3 items 3/4/9 and §10 define these deliverables.
* Read `project/Directory.Build.props`, `project/src/Elringus.Naninovel.Runtime/Elringus.Naninovel.Runtime.csproj`; machine-extracted the `HintPath` references of all **44** `project/src/*/*.csproj` files (**41** distinct assemblies, 164 reference entries).
* Scanned **all 1651 `.cs` files / 43 assembly directories** under `src/decompiled/` with a PowerShell structural scanner (2 passes: identifier index, then member-block extraction). This is the source of every row in `stub_inventory.csv`.
* Read `manosaba_Data/ScriptingAssemblies.json` (**165** assembly names — authoritative list of assemblies in the shipped Player).
* Read `manosaba_Data/globalgamemanagers` bytes: ASCII `6000.0.48f1` at offset 24 → Unity version, measured, not quoted from a doc.
* Read `manosaba_Data/StreamingAssets/aa/settings.json` → `"m_AddressablesVersion":"2.4.6"`.
* Read `manosaba_Data/app.info`, `boot.config`, `BepInEx/interop/assembly-hash.txt` (`f8153833064a5fe75e1ec24b6bce6067`).
* Enumerated `BepInEx/interop/` (**157** DLLs) and `src/il2cpp/DummyDll/` (**128** DLLs); read `AssemblyName.GetAssemblyName(...).Version` for all interop DLLs (**19** carry a non-zero version).
* Read `src/il2cpp/config.json`, `src/decompiled/GigaCreation.Essentials.AddressablesUtils/.../AddressablesManager.cs` (L1–90, L452–536), `src/decompiled/Elringus.Naninovel.Runtime/Naninovel/Engine.cs` (L1–200, L398–477), `src/decompiled/Assembly-CSharp/WitchTrials.Views/GameInitializer.cs` (L1–159), `.../SteamManager.cs` (L60–89).
* Verified absence: no `Packages/manifest.json`, no `packages-lock.json`, no `ProjectVersion.txt` anywhere in the repo.

**NOT executed — no PASS is claimed anywhere:** no Unity Editor run (installed at `E:\Unity\6000.0.48f1\Editor\Unity.exe`, present; never launched), no compilation (`dotnet build` or Unity), no Play Mode, no Player build, no runtime test. **Every `test_status` in `dependency_matrix.csv` is `NOT_RUN` (187/187).**

### Method for the stub taxonomy (and its one heuristic)

`src/decompiled/` is **Il2CppDumper DummyDll-shaped source**: `[Token]`/`[Address(RVA=…)]`/`[Il2CppDummyDll.FieldOffset]` attributes throughout, empty bodies, `return null;`, `return default(T);`. Only **1042 `// RECONSTRUCTED` markers** exist, **all in `Assembly-CSharp`** (measured).

Discriminator used for `status`, measured across the corpus: the `RVA` of each member's `[Address(RVA=…)]` attribute. **7301 distinct RVAs** appear; **82 RVAs are shared by ≥5 members** (e.g. `0x30C000` by **740** members, `0x30D8F0` by **131**, all of them constructors). A shared address cannot implement many distinct behaviours, so those members are graded `confirmed-noop`; a unique address proves a distinct native body exists whose behaviour is absent from the source, so those are graded `unreconstructed`. Members with **no** `[Address]` attribute are graded `unreconstructed` by default. **This is a heuristic: no disassembly of `GameAssembly.dll` was performed** (see §5).

`evidence` quotes the source lines exactly as they appear (leading tabs preserved, source is CRLF). Lines are joined with the two-character sequence `\n` so each CSV record stays on one physical line; `called_by` is `; `-separated `path:line`.

## 1. Counts (exact — from the written CSVs)

### 1.1 `stub_inventory.csv` — 10 971 records (+1 header line = 10 972 physical lines)

| `stub_kind` | rows |
|---|---:|
| empty-body | 5991 |
| return-default | 2625 |
| return-null | 1858 |
| interface | 421 |
| abstract | 72 |
| constant-return | 4 |
| throw-notimplemented | 0 (`NotImplementedException` occurs **0** times in `src/decompiled/`) |
| native-noop | 0 (folded into `empty-body` + `status=confirmed-noop`; see §1.3) |
| **total** | **10 971** |

| `status` | rows |
|---|---:|
| unreconstructed | 8422 |
| confirmed-noop | 1989 |
| interface-or-abstract | 493 |
| already-reconstructed | 67 |
| unreachable | 0 — see §5 |

| `impact` | rows |
|---|---:|
| no-call-site-found | 6638 |
| blocks-gameplay | 3554 |
| editor-or-devtool-only | 628 |
| blocks-content-load | 89 |
| blocks-boot | 50 |
| undetermined | 12 |

`return-default` rows whose quoted body names `UniTask`: **894** (note: `default(UniTask)` is a *valid completed task* in C#, so a `return default(UniTask)` body is not by itself proof of a stub — only the RVA/call-site evidence separates the two cases).

### 1.2 Per assembly (rows, descending) — 42 of the 43 `src/decompiled/` assembly directories contain stubs

| assembly | rows | assembly | rows |
|---|---:|---|---:|
| Elringus.Naninovel.Runtime | 7098 | GigaCreation.Essentials.Overlays | 27 |
| Naninovel.Common | 908 | GigaCreation.Tools.Debugging.TextDisplays | 27 |
| Assembly-CSharp | 716 | GigaCreation.Essentials.Input | 27 |
| CustomInspector | 467 | GigaCreation.Tools.Debugging.UI | 23 |
| GigaCreation.Essentials.UI | 323 | GigaCreation.NaninovelExtender.Localization | 23 |
| GigaCreation.NaninovelExtender.ExtendedActors | 217 | GigaCreation.Tools.Debugging.General | 22 |
| GigaCreation.NaninovelExtender.UI | 158 | GigaCreation.NaninovelExtender.Core | 21 |
| GigaCreation.NaninovelExtender.Common | 118 | CustomInspector.Helpers | 20 |
| GigaCreation.Tools.General | 117 | GigaCreation.Essentials.WebRequestUtils | 19 |
| GigaCreation.Tools.UI | 113 | GigaCreation.Tools.UniTasks | 16 |
| GigaCreation.NaninovelExtender.Effects | 84 | ppt-noise | 14 |
| GigaCreation.Essentials.AddressablesUtils | 82 | GigaCreation.Tools.Private.PlayableDirectors | 14 |
| GigaCreation.Essentials.Dialogs | 70 | GigaCreation.NaninovelExtender.Audio | 12 |
| GigaCreation.NaninovelExtender.ExtendedSpawn | 54 | GigaCreation.Essentials.SaveLoad | 11 |
| GigaCreation.NaninovelExtender.Cameras | 42 | GigaCreation.Tools.Service | 11 |
| GigaCreation.Essentials.Localization | 41 | GigaCreation.Essentials.Platforms | 10 |
| GigaCreation.Tools.BuildTimestampDisplay | 10 | GigaCreation.Essentials.UniTasks | 3 |
| GigaCreation.Tools.Debugging.Core | 9 | GigaCreation.NaninovelExtender.Movies | 7 |
| GigaCreation.Essentials.Toggleables | 9 | GigaCreation.NaninovelExtender.ResourceProvider | 7 |
| Elringus.SpriteDicing.Runtime | 6 | GigaCreation.Tools.UndoExtensions | 5 |
| GigaCreation.Tools.Debugging.TextDisplays.BuildTimestamps | 5 | GigaCreation.Essentials.MasterData | 5 |

`Il2CppDummyDll` is the only assembly directory with **0** stub rows (it contains 6 attribute-definition files only).

### 1.3 `dependency_matrix.csv` — 187 records

`source_kind`: unity-module 74, first-party-reconstructed 39, upm-package 31, il2cpp-interop-proxy 20, third-party-package 20, system 2, unknown 1.
`implementation_status`: missing 120, stub 43, not-needed 24, real 0.
`test_status`: NOT_RUN 187.
Seed list = every one of the 165 names in `ScriptingAssemblies.json`, plus the 12 interop-only `Il2Cpp*` proxies + `__Generated`, plus BepInEx/Harmony/Il2CppInterop/Il2CppDummyDll/`Manosaba.Plugin`/`JetBrains.Annotations`, plus a `DOTween (DG.Tweening)` row recorded as *verified not a dependency* (0 occurrences of `DG.Tweening` in 1651 files).

## 2. Ranked list of stubs that actually block Boot / gameplay

Ranking = position on the boot→content→gameplay chain, then fan-out of `called_by`.

| # | assembly | type.member (file:line) | kind / status | Why it blocks | Evidence (exact source) |
|---|---|---|---|---|---|
| 1 | Elringus.Naninovel.Runtime | `Naninovel.Engine.Initialize` (`Engine.cs:404`) | return-default / unreconstructed | The engine constructor of the whole runtime. Nothing else in Naninovel (services, configs, scripts) can come up. Unique RVA `0x565720`. | `public static UniTask Initialize(EngineParams @params)\n{\n\treturn default(UniTask);\n}` |
| 2 | Elringus.Naninovel.Runtime | `Naninovel.Engine.GetService<T>` `:448`, `GetService(Type)` `:469`, `GetServiceOrErr<T>` `:461`, `GetConfiguration<T>` `:434`, `GetConfiguration(Type)` `:441`, `TryGetService<T>` `:454` | return-null / unreconstructed | The service locator returns `null` for every query, so code that does `Engine.GetService<IX>()` gets `null` and throws NRE downstream. | `public static TService GetService<TService>() where TService : class, IEngineService\n{\n\treturn null;\n}` |
| 3 | Elringus.Naninovel.Runtime | `Naninovel.Engine` state accessors `Types` `:178`, `Configuration` `:190`, `Behaviour` `:208`, `Time` `:226` (+`CreateObject :512`, `FindObject :525`, `FormatMessage :562`) | return-null / unreconstructed | Every Naninovel sub-system reads these; the runtime has no object/behaviour/time root. `Engine.Types` has call sites (see `called_by`, e.g. `Naninovel/IResourceProviderManager.cs:20`). | `get\n{\n\treturn null;\n}` |
| 4 | Assembly-CSharp | `WitchTrials.Views.GameInitializer.InitializeAsync` (`GameInitializer.cs:149`) | return-default / unreconstructed | `Start()` (`:137`) calls `InitializeAsync(destroyCancellationToken).Forget()`; with an already-completed default task the whole async boot body (locale, splash, entry sequence, Addressables init) never runs. Unique RVA `0x30FE20`. | `private UniTask InitializeAsync([Optional] CancellationToken ct)\n{\n\treturn default(UniTask);\n}` |
| 5 | Assembly-CSharp | `WitchTrials.Views.GameInitializer.Awake` (`:107`) and `OnDestroy` (`:142`) | empty-body / unreconstructed | `UNITY_READINESS_CODEX_AUDIT.md:45` records this exact finding ("`Awake()` 为空，`InitializeAsync(...)` 返回默认任务"). Unique RVA `0x30FA80` ⇒ a distinct native body exists and is absent from the source. | `private void Awake()\n{\n}` |
| 6 | GigaCreation.Essentials.AddressablesUtils | `AddressablesManager.CheckIfMainCatalogExistsAsync :461`, `CheckIfAdditionalCatalogExistsAsync :469`, `LoadAdditionalCatalogAsync :477`, `GetResourceLocationsAsync :485`, `GetOrLoadAddressableAsset<T>(string) :492`, `GetOrLoadAddressableAsset<T>(AssetReference) :499`, `LoadResourceLocationsAsync :513`, `ReleaseHandles :520`, `BuildAdditionalCatalogLocationsWithoutExt :526` | return-default / return-null / empty-body, all unreconstructed | This is the project's Addressables front-end. Catalog existence, catalog loading and resource-location enumeration all no-op, so **no Addressable asset can ever load**, regardless of whether `com.unity.addressables` is installed. | `public UniTask<List<IResourceLocation>> GetResourceLocationsAsync([Optional] CancellationToken ct)\n{\n\treturn default(UniTask<List<IResourceLocation>>);\n}` |
| 7 | Elringus.Naninovel.Runtime | `Naninovel.ScriptPlayer.InitializeService :917`, `Play :1000`, `PlayRoutine :1162`, `SelectNextCommand :1177`, `Resume :1006`, `Stop :1012`, `Rewind :1019`, `ExecutePlayedCommand :1146` | return-default / empty-body / return-null, all unreconstructed | Dialogue playback. Even if the engine booted, no script line can advance. | (see `stub_inventory.csv` rows for `Naninovel.ScriptPlayer`) |
| 8 | Assembly-CSharp | `WitchTrials.Views.SteamManager.Awake :67`, `InitOnPlayMode :61`, `Update :140`, `SteamAPIDebugTextHook :54` | empty-body / unreconstructed | Boot-scene Steam init; `GameInitializer` dereferences `_steamManager.gameObject` (`:136`). | `private void Awake()\n{\n}` |
| 9 | Assembly-CSharp | `WitchTrials.Views.EntrySequence.PlayInitialSequenceAsync :255`, `EnterTitleScreenAsync :296` | return-default / unreconstructed | The splash→title transition. Returns a completed task, so the sequence advances instantly and skips its work. | (see `stub_inventory.csv`) |
| 10 | Assembly-CSharp | `WitchTrials.Presenters.DefaultLocalePresenter.LoadAsync :74` (+ `Dispose :67`), `WitchTrials.Views.SplashScreen.PlaySequenceAsync :136`, `SplashScreen.Reset :129` | return-default / empty-body, all unreconstructed | Locale and splash are two of the tasks `GameInitializer.InitializeAsync` was meant to await; a no-op locale load leaves localization tables unset. Recorded call sites for `LoadAsync`: `Assembly-CSharp\WitchTrials.Views\LoadPerspectiveStage.cs:96` and `:112`. | (see `stub_inventory.csv`) |

Runner-up on the content path: `GigaCreation.NaninovelExtender.ResourceProvider.AddressableResourceProviderExtended.LoadAllLocations` (7 rows in that assembly, `impact=blocks-content-load`) — Naninovel's resource resolution goes through it.

Assemblies with **0** boot-blocking rows but heavy gameplay impact: `CustomInspector` (467) and `CustomInspector.Helpers` (20) are classified `editor-or-devtool-only`; `GigaCreation.Tools.Debugging.*` (81 rows total) likewise.

## 3. Assemblies that are pure Il2CppDummyDll / Il2CppInterop — unusable as the new Player's runtime implementation

* `src/il2cpp/DummyDll/` — **128** reference assemblies. They exist only to make the IDE compile the DummyDll-shaped source. Listed in `dependency_matrix.csv` with `source_kind=il2cpp-interop-proxy` / `implementation_status=not-needed` where they are their own row (`Il2CppDummyDll`); where a real package exists, the row names the real source kind and states in `version_evidence` that the only copy in the repo is the DummyDll stub.
* `BepInEx/interop/` — **157** Il2CppInterop-generated proxies (real metadata, but they are call-forwarding proxies into `GameAssembly.dll`; they contain no managed implementation). **12** of them are `Il2Cpp*` System proxies (`Il2Cppmscorlib`, `Il2CppSystem`, `Il2CppSystem.Core`, …) plus `__Generated`; these are exactly the 13 rows marked `il2cpp-interop-proxy` in `dependency_matrix.csv` beyond `Il2CppDummyDll`/BepInEx/`Manosaba.Plugin`. `assembly-hash.txt = f8153833064a5fe75e1ec24b6bce6067` pins the interop generation.
* Analysis-only stack, excluded from any Player: `Il2CppDummyDll`, `Manosaba.Plugin` (source `project/src/Manosaba.Plugin/Plugin.cs`), `BepInEx.Core`, `BepInEx.Unity.IL2CPP`, `BepInEx.Unity.Common`, `0Harmony`, `Il2CppInterop.Runtime` — **20** rows total with `source_kind=il2cpp-interop-proxy`.
* The decisive point is not the DLLs but the **source**: all 43 `src/decompiled/` assembly directories are DummyDll-shaped. Only `Assembly-CSharp` has been partially reconstructed (**1042** `// RECONSTRUCTED` markers); the other 42 directories (every `GigaCreation.*`, `Elringus.Naninovel.Runtime`, `Naninovel.Common`, `CustomInspector*`, `ppt-noise`) contain **0** reconstructed bodies. Recompiling them yields a reference assembly, not a game.

## 4. BLOCKED — inputs the user must supply (exact names and evidence)

| What | Exact identifier | Version evidence | What is missing |
|---|---|---|---|
| Naninovel (paid Asset Store plugin) | `Elringus.Naninovel.Runtime`, `Naninovel.Common` | `Naninovel.Common` assembly manifest `Version=2025.3.19.1546` (Il2CppInterop copy); `UNITY_PROJECT_RECONSTRUCTION_AUDIT.md:337` marks it paid | Real runtime implementation. `src/decompiled/Elringus.Naninovel.Runtime` (789 files) + `Naninovel.Common` (176 files) are stubs: **7098 + 908 = 8006** rows. No `Packages/manifest.json` exists, so the package version cannot be resolved from the repo. |
| LeTai.TrueShadow | `LeTai.TrueShadow` | 388 serialized instances (`UNITY_PROJECT_RECONSTRUCTION_AUDIT.md:341`); **no** interop DLL, **no** decompiled source | The whole assembly + its shaders. Commercial. |
| MPUIKit, UIPolygon | `MPUIKit`, `UIPolygon` | `UNITY_PROJECT_RECONSTRUCTION_AUDIT.md:345` (third-party/Asset Store); interop DLLs exist (`BepInEx/interop/MPUIKit.dll` 116 224 B, `UIPolygon.dll` 15 360 B) but there is **no** decompiled source, so no reusable implementation | Managed implementation must come from the package. |
| ppt-noise | `ppt-noise` | `src/decompiled/ppt-noise` holds only 2 implementation files (`PPT.Noise/Noise.cs`, `Perlin.cs`) = **14** stub rows; no interop DLL; licence unknown | Real noise implementation / licence clarity. |
| GigaCreation.Tools.CsvParser | `GigaCreation.Tools.CsvParser` | in `ScriptingAssemblies.json`; **absent** from `BepInEx/interop` (157 DLLs) **and** from `src/decompiled` (43 dirs) | Origin unidentifiable from repo evidence — first-party but not present anywhere in this checkout. |
| MasyoLab-FavoritesAssetExamples | `MasyoLab-FavoritesAssetExamples` | in `ScriptingAssemblies.json`; **absent** from `BepInEx/interop` and from `src/decompiled`; no version metadata anywhere | Origin/provider unknown (row is the single `source_kind=unknown` in the matrix). |
| Steamworks.NET | `com.rlabrecque.steamworks.net` | `ScriptingAssemblies.json`; interop copy `BepInEx/interop/com.rlabrecque.steamworks.net.dll`; `src/decompiled/com.rlabrecque.steamworks.net` is **not** present (no decompiled source) | Package must be supplied in a Unity 6 / IL2CPP-compatible build. Free but not in the repo. |
| Missing IL2CPP metadata copies (free packages) | `UniTask.DOTween`, `UniTask.Linq`, `UniTask.TextMeshPro`, `Unity.2D.Common.Runtime`, `Unity.2D.IK.Runtime`, `Unity.Recorder.Base`, `Unity.Recorder`, `Unity.Rendering.LightTransport.Runtime`, `Unity.RenderPipelines.Core.ShaderLibrary`, `Unity.RenderPipelines.ShaderGraph.ShaderGraphLibrary`, `Unity.RenderPipelines.Universal.2D.Runtime`, `Unity.RenderPipelines.Universal.Config.Runtime`, `Unity.RenderPipelines.Universal.Shaders`, `Unity.ScriptableBuildPipeline`, `System.IO.Hashing` | these 15 names, together with the 6 covered by their own rows above (`UnityEngine.AMDModule`, `UnityEngine.ARModule`, `UnityEngine.NVIDIAModule`, `GigaCreation.Tools.CsvParser`, `MasyoLab-FavoritesAssetExamples`, `System.Runtime.CompilerServices.Unsafe`), are the exact 21 entries of `ScriptingAssemblies.json` that are **not** present in `BepInEx/interop` | Must be re-installed from Unity's registry at 6000.0.48f1-compatible versions (or, for `System.IO.Hashing`, resolved as a NuGet/profile dependency). |
| Compile-time attribute package | `JetBrains.Annotations` | `using JetBrains.Annotations;` in **115 / 1651** `.cs` files; **absent** from `ScriptingAssemblies.json` and from `BepInEx/interop` | Attribute assembly (NuGet) or an equivalent source shim, otherwise the reconstructed sources do not compile. |
| Unity Editor modules/packages (all `unity-module` + `upm-package` rows) | **105** rows | Unity `6000.0.48f1` measured from `globalgamemanagers`; `m_AddressablesVersion = 2.4.6` measured from `aa/settings.json`; `ScriptingAssemblies.json` lists all 165 | No `Packages/manifest.json`, no `packages-lock.json`, no `ProjectSettings/` exist ⇒ nothing is installed. `E:\Unity\6000.0.48f1\Editor\Unity.exe` is present but was never run, so module availability is unverified. |
| Exact expected paths for missing inputs | `E:\Manosaba-VSCodeReady\Assets\`, `E:\Manosaba-VSCodeReady\Packages\manifest.json`, `E:\Manosaba-VSCodeReady\ProjectSettings\ProjectVersion.txt` | — | These three paths do not exist (recursive search returned 0 hits). |

## 5. Could NOT be determined

1. **Whether the 82 shared-RVA bodies are truly empty.** No disassembly of `GameAssembly.dll` (53 876 736 B) at those addresses was performed. `0x30C000` (740 users) and `0x30D8F0` (131 users, all constructors) are *consistent with* shared empty thunks but were not proven. All 1989 `confirmed-noop` rows depend on this heuristic.
2. **Whether `[Address(RVA=…)]` values are real method pointers.** They are internally consistent (shared RVAs cluster on constructors, unique RVAs on methods with evidence blocks) but were not validated against `dump.cs`/`script.py.json`.
3. **True call sites.** `called_by` is a **name-based** match over 3 918 distinct call identifiers — it does not resolve types, so e.g. `Engine.Initialize`'s `called_by` includes unrelated `Initialize(` call sites. Consequently 6638 rows read `none-found`, which is a signal, not proof of dead code. A separately produced, type-resolved subset exists in this directory (`engine_callsites.csv`, 255 records, columns `callee_assembly,callee_type,callee_member,caller_file,caller_line,caller_symbol,notes`) — it was **not** produced by this task and was only inspected (header + 3 rows).
4. **`status=unreachable` was therefore never assigned.** Unity reachability cannot be established from source alone (reflection, UnityEvents, serialized scene wiring, Addressables keys, `RuntimeInitializeOnLoadMethod`).
5. **Exact UPM/package versions.** No `manifest.json`/`packages-lock.json` exists; only 19 assemblies carry a non-zero `AssemblyVersion` and most of those are `0.0.0.0`. Versions in the matrix are therefore cited either from a measured file or marked not determinable — none were guessed.
6. **Licences.** No licence text for any third-party package exists in the repo, so `license_input` uses categories (`COMMERCIAL LICENSE REQUIRED` only where `UNITY_PROJECT_RECONSTRUCTION_AUDIT.md` states it, otherwise `open-source style … verify`).
7. **`GigaCreation.Tools.CsvParser` and `MasyoLab-FavoritesAssetExamples`** — provider, purpose and version: no evidence available.
8. **Which assemblies the serialized assets need.** The matrix reflects source/csproj usage; component types instantiated only from scenes/prefabs/`ScriptableObject`s are not covered. `UnityEngine.SpriteMaskModule`, `Physics2DModule` etc. exist for that reason; no module was graded `not-needed` except the three vendor modules (`AMDModule`, `ARModule`, `NVIDIAModule`).
9. **Unity Editor health.** `E:\Unity\6000.0.48f1\Editor\Unity.exe` exists; whether the installation is complete and which modules it carries was not verified (Editor not launched).
10. **Runtime behaviour of the 421 `interface` + 72 `abstract` rows** — bodyless by language rules; graded `interface-or-abstract` (493 rows) and excluded from the blocker ranking. Whether their eventual implementations are stubs cannot be known until the implementations exist.

## 6. Files written

| file | records | notes |
|---|---:|---|
| `tools/unity-rebuild/out/dependency_matrix.csv` | 187 | columns `assembly,source_kind,version_evidence,install_method,implementation_status,license_input,used_by,test_status`; 10 972-free: one record per line |
| `tools/unity-rebuild/out/stub_inventory.csv` | 10 971 | columns `assembly,type,member,file,line,stub_kind,evidence,called_by,impact,status`; 10 972 physical lines (header + records) |
| `tools/unity-rebuild/out/stub_report.md` | this file | |

Scratch intermediates used by the scanner live in `tools/unity-rebuild/out/tmp/` (`_rows.xml`, `_tok.tsv`, `_rva.json`, `_dep_inputs.json`, `empty_bodies.csv`) and contain no claims beyond what is cited above.
