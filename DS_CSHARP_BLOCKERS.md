# DS C# blockers — integration agent → DS

Produced by the Unity integration line in the format required by
`UNITY_REBUILD_AGENT_BOUNDARIES.md` §"Output expected from Claude instead of bulk C# edits"
(lines 82-97). The machine-readable companion is
`tools/unity-rebuild/out/ds_csharp_blockers.csv`.

**Nothing in this document has been compiled by Unity.** `test_status` is `NOT_RUN`
everywhere, because the Editor cannot start work without a licence (see §6).

---

## 1. Scope note

| | |
|---|---|
| Compiler | Unity 6000.0.48f1, default language version **C# 9** |
| Baseline commit | `0a961fa` (`origin/main` after DS pushed its round) |
| Sources in scope | `src/decompiled/**` (10,487 `.cs`, of which 1,651 under `src/decompiled/`) |
| Proven so far | `dotnet build project\Manosaba.sln --no-incremental` → **0 error / 140 warning**; that is a *different* compiler and proves nothing about Unity |

## 2. Blockers

### B-01 — Duplicate type definition: `AddressablesManager`

| field | value |
|---|---|
| assembly | `GigaCreation.Essentials.AddressablesUtils` |
| file | `src/decompiled/GigaCreation.Essentials.AddressablesUtils/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs` |
| type | `GigaCreation.Essentials.AddressablesUtils.AddressablesManager` |
| member | whole type |
| symptom | **CS0101** "The namespace already contains a definition for 'AddressablesManager'" as soon as an integration-authored reconstruction of the same type is compiled alongside this file |
| mechanical or semantic | **semantic** — a scope decision, not a text transformation |
| required transformation | Choose exactly one: (a) keep the `src/decompiled` copy and drop the reconstruction; (b) keep the reconstruction as an integration shim and stub/remove the `src/decompiled` copy; (c) use the real upstream `GigaCreation.Essentials` package and drop both |
| evidence | the reconstruction lives at `UnityProject/Deferred/AddressablesManager.cs` (parked outside `Assets/` so it cannot break a build); boundaries doc `L80` routes `GigaCreation.*` bodies to DS unless the user assigns otherwise |
| priority / blocking gate | **G1** — no assembly containing both copies can compile |
| acceptance test | Unity compile of the assembly yields 0 CS0101; `AddressablesManager` resolves from exactly one source |
| owner | **user decision required** (see §5) |

### B-02 — Missing commercial dependency: Naninovel

| field | value |
|---|---|
| assembly | `Elringus.Naninovel.Runtime`, `Naninovel.Common` |
| file | 8,006 stub rows across the Naninovel tree |
| type | e.g. `Naninovel.Engine`, `Naninovel.ScriptPlayer` |
| member | `Engine.Initialize`, `Engine.GetService/GetServiceOrErr/GetConfiguration/TryGetService`, `ScriptPlayer.Play/PlayRoutine/SelectNextCommand/InitializeService` |
| symptom | every service lookup returns null and the engine never initialises → nothing in the game can run |
| mechanical or semantic | **semantic**, and additionally **not ours to reconstruct**: Naninovel is a commercial Unity Asset Store product |
| required transformation | supply the real package; do **not** reconstruct it |
| evidence | `Elringus.Naninovel.Runtime` holds 5,707 of 10,971 stub rows; the shipped `manosaba_game/LICENSE.md:133-135` carries an Elringus MIT notice, which is recorded as evidence only and **not** treated as an entitlement |
| priority / blocking gate | **G1/G4** |
| acceptance test | `Naninovel.Engine.Initialize` completes and `Engine.GetServiceOrErr<T>()` returns non-null for a registered service |
| owner | **user** — supply the package |

### B-03 — `src/decompiled` is not decompiled for 42 of 43 assemblies

| field | value |
|---|---|
| assembly | all except `Assembly-CSharp` |
| file | 1,651 files scanned |
| type | whole tree |
| member | n/a |
| symptom | bodies are empty or `return null/default`; `[Address(RVA=…)]` / `[Il2CppDummyDll.FieldOffset]` markers throughout |
| mechanical or semantic | semantic |
| required transformation | method-body reconstruction, assembly by assembly |
| evidence | only **1,042 `// RECONSTRUCTED` markers exist and all are in `Assembly-CSharp`**; the other 42 directories have zero |
| priority / blocking gate | G4 |
| acceptance test | per-assembly reconstructed-body count rises and the corresponding gameplay path runs |
| owner | DS |

### B-04 — Compile-blocking attribute dependency

| field | value |
|---|---|
| assembly | any using `[UsedImplicitly]` |
| file | 115 of 1,651 files |
| type | n/a |
| member | n/a |
| symptom | missing `JetBrains.Annotations` reference |
| mechanical or semantic | **mechanical** |
| required transformation | drop the attributes, or add the annotations package to the runtime assembly set |
| evidence | `JetBrains.Annotations` is absent from the shipped build's `BepInEx/interop` |
| priority / blocking gate | G1 |
| acceptance test | the affected assemblies compile without the annotations reference |
| owner | DS |

### B-05 — `Il2CppDummyDll` marker dependencies

| field | value |
|---|---|
| assembly | all `src/decompiled/**` |
| file | tree-wide |
| type | n/a |
| member | n/a |
| symptom | runtime source cannot reference the dummy assembly |
| mechanical or semantic | **mechanical** (bulk attribute/`using` cleanup) |
| required transformation | remove `Il2CppDummyDll` `using`s and `[Token]`/`[Address]`/`[FieldOffset]` attributes **without** removing real serialization attributes such as `[SerializeField]` / `[Serializable]` |
| evidence | audit UR-005 / §3.3.8, and the dependency matrix (`il2cpp-interop-proxy` rows must never become the new Player's implementation) |
| priority / blocking gate | G1 |
| acceptance test | the runtime assemblies compile with no reference to `Il2CppDummyDll`, and serialized-field counts are unchanged |
| owner | DS |

### B-06 — C# 10 file-scoped namespaces (10,260 files)

| field | value |
|---|---|
| assembly | all of `src/decompiled/**` |
| file | 10,260 of 10,487 `.cs` |
| type | n/a |
| member | n/a |
| symptom | Unity 6.0 defaults to **C# 9**; `namespace X;` is C# 10 syntax |
| mechanical or semantic | **mechanical** |
| required transformation | convert `namespace X;` to block form, or raise the language version through a supported, verified route |
| evidence | `tools/unity-rebuild/out/csharp_compat.json` (counts exclude comment-only lines; `record` ×13 is C# 9 and is **not** a blocker) |
| priority / blocking gate | G1 |
| acceptance test | Unity's real compiler reports 0 errors attributable to namespace syntax |
| owner | **DS — see §5 decision D-2** |

A deterministic converter already exists at `tools/unity-rebuild/convert_namespaces.py`.
It writes to a **separate tree** (`tools/unity-rebuild/converted/`), never overwriting
`src/`, and is verified by `check_converted_tree.py` (0 remaining file-scoped namespaces;
brace balance unchanged from the original). Whether it should be applied to `src/`, applied
to a build tree, or superseded by a language-version change is the pending decision.

## 3. Boot-gate blocker list (`GATE_A_BOOT`, 19 rows)

From `ds_runtime_blockers.csv`; all `Assembly-CSharp`:

`DefaultLocaleSelector` · `DefaultLocalePresenter` (×3) · `EngineInitializationUiModified` (×2) ·
`EntrySequence` (×2) · `GameInitializer` (×3) · `SplashScreen` (×2) · `SteamManager` (×6)

DS has since recovered 5 of these (`GameInitializer.Awake`/`OnDestroy`,
`DefaultLocalePresenter..ctor`/`Dispose`, `SteamManager.Update`) — independently verified
here: `git diff --numstat -- src/` is +42/+51/+34 with **0 deletions**, BOM state matches
`HEAD` for each file, and the build gate re-runs at 0 errors.

Still open per DS's own report: `SteamManager.Awake` (1,446 B), the two `SteamManager`
static getters, and `EngineInitializationUiModified.OnEnable` (needs vtable slot
`0x3c8` = 121 resolved first).

## 4. Routing totals (all 10,971 rows, no UNKNOWN)

| routing | rows |
|---|---:|
| `EXTERNAL_PACKAGE_REQUIRED` | 8,684 |
| `REAL_NOOP` | 1,281 |
| `EDITOR_OR_DEV_ONLY` | 550 |
| `DS_RECONSTRUCTION_REQUIRED` | 350 (GATE_A_BOOT 19 + GATE_B_OR_C 336, minus 5 since recovered) |
| `ALREADY_RECONSTRUCTED` | 72 |
| `INTEGRATION_SHIM_REQUIRED` | 34 |

## 5. Decisions required from the user

| id | decision | why it blocks |
|---|---|---|
| **D-1** | Who owns reconstructing `GigaCreation.*` method bodies — DS (boundaries `L80`) or the integration line? | determines whether `UnityProject/Deferred/AddressablesManager.cs` is adopted, handed over, or deleted; until then B-01 keeps any combined build from compiling |
| **D-2** | Should the 10,260-file namespace conversion be applied by DS, or should the language version be raised? | `UNITY_READINESS_CODEX_AUDIT.md` §3.3.7 forbids "pretending Unity is compatible by changing LangVersion" without verification; the boundaries doc `L104` routes mechanical conversion to DS |
| **D-3** | Does the user supply the Naninovel package? | B-02 blocks everything downstream |

## 6. What was not done

- No Unity compile, no Play Mode, no Player build: **Unity licence missing**
  (`No valid Unity Editor license found`; `g1_compile.ps1` exits 2/`BLOCKED`).
- No bulk edits to `src/decompiled/**` by the integration line. `src/` currently carries
  only DS's 3-file, +127-line change.
- No `dotnet build` result presented as evidence of Unity compilability.
