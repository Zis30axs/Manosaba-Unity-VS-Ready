# Unity rebuild agent boundaries

This file is an **authoritative scope override** for `UNITY_READINESS_CODEX_AUDIT.md` whenever any wording there could be read as permission to perform broad mechanical edits to the reconstructed C# skeleton.

## Ownership split

### DS owns bulk / mechanical C# skeleton work

DS is the owner of coarse, large-scale, or mechanical transformations under `src/decompiled/**` and related reconstructed source trees, including but not limited to:

- mass syntax normalization (for example file-scoped namespace conversion across many files);
- bulk removal or rewriting of decompiler/IL2CPP attributes;
- large-scale namespace/import cleanup;
- project-wide stub pattern normalization;
- mechanical C# language-version compatibility rewrites;
- broad generated-code cleanup;
- repetitive skeleton reshaping across many classes or assemblies;
- any operation whose main value is throughput rather than local semantic reasoning.

Unity-resource/integration agents must **not** take over this work simply because it is convenient for getting the Unity project to compile.

## Claude / Unity integration scope

Claude (or another Unity integration agent) should focus on:

- resource conversion and import;
- Unity project/package setup;
- dependency mapping and package wiring;
- GUID/localFileID/object-reference reconstruction;
- Scene/Prefab/ScriptableObject reconstruction;
- Addressables reconstruction and validation;
- import/build/play-mode/player validation;
- identifying exact C# blockers and producing a machine-readable/request list for DS;
- narrow, high-evidence runtime fixes only when they are inseparable from the integration task and cannot reasonably be delegated to DS without blocking progress.

### Default rule for `src/decompiled/**`

Treat `src/decompiled/**` as **read-mostly** during Unity integration.

Do not mass-edit it.

Before changing a reconstructed C# file, the integration agent must satisfy all of the following:

1. the change is directly required to unblock the current Unity integration/runtime path;
2. the change is local and semantically specific, not a mechanical class of edits repeated across the tree;
3. there is sufficient evidence for the behavior/change;
4. the exact file/method and reason are recorded;
5. the change cannot be more appropriately handed to DS as a bulk skeleton task.

If the same edit pattern is needed in multiple files, stop after identifying the pattern and produce a DS task instead of applying it broadly.

## How to handle Unity compile failures caused by skeleton shape

When Unity reports source-language or skeleton-shape errors such as unsupported syntax, attribute noise, generated-code artifacts, or repetitive namespace/import issues:

1. collect the complete error set;
2. cluster errors by mechanical root cause;
3. identify representative files and exact transformation rules;
4. write a DS task/prompt with acceptance criteria;
5. continue integration work that does not require those files to be changed;
6. do **not** sweep `src/decompiled/**` to fix the class of errors yourself.

A single local edit may be made only when it is uniquely tied to the integration point and is not the beginning of a broader mechanical cleanup.

## Runtime stubs

The presence of a stub does not automatically authorize the Unity integration agent to implement it.

For each reachable stub (`return null`, `return default`, empty method, etc.), classify it as one of:

- `REAL_NOOP` — evidence shows the native method is intentionally empty/no-op;
- `ALREADY_RECONSTRUCTED` — implementation exists elsewhere/current branch;
- `DS_RECONSTRUCTION_REQUIRED` — native C# method-body reconstruction belongs to DS;
- `INTEGRATION_SHIM_REQUIRED` — a narrowly scoped compatibility/integration layer is acceptable;
- `BLOCKED_EXTERNAL_DEPENDENCY` — must come from a real package/library;
- `UNKNOWN` — insufficient evidence.

Do not bulk-fill stubs for the purpose of reaching a build or completion percentage.

In particular, findings in Naninovel, `GigaCreation.*`, Boot/Init, or other assemblies should first be turned into a dependency/stub inventory. If restoring original method bodies is required, hand those method bodies to DS unless the user explicitly assigns them to the integration agent.

## Output expected from Claude instead of bulk C# edits

When C# skeleton work is needed, produce `DS_CSHARP_BLOCKERS.md` (or an equivalent machine-readable CSV/JSON plus Markdown summary) containing for each blocker:

- assembly;
- file;
- type;
- method/member if applicable;
- compiler/runtime symptom;
- whether it is mechanical or semantic;
- exact required transformation or reconstruction target;
- evidence/source references;
- priority/blocking gate;
- acceptance test after DS fixes it.

This keeps DS responsible for throughput-heavy skeleton work while Claude stays on Unity/resource integration and high-level validation.

## Relationship to `UNITY_READINESS_CODEX_AUDIT.md`

The following instructions in `UNITY_READINESS_CODEX_AUDIT.md` must be interpreted through this ownership split:

- “AST scan actual source” means **detect and inventory compatibility blockers**; it does not authorize a project-wide rewrite by Claude.
- “convert unsupported C# syntax” means **delegate mechanical conversions to DS** unless a single local integration-only change is justified.
- “remove unnecessary Il2CppDummyDll marker dependencies” means **design the target runtime assembly boundary and inventory required cleanup**; bulk attribute/source cleanup belongs to DS.
- “scan runtime stubs” means **classify and route them**; it does not authorize mass implementation.
- “fix Naninovel / AddressablesManager / GameInitializer” means **close the runtime dependency**, using real packages, an explicit integration shim, or DS-reconstructed method bodies as appropriate. It does not mean Claude should freely rewrite the reconstructed source tree.

If this file conflicts with the earlier audit prompt on ownership, **this file wins**.

## Provenance

Defined by Codex after user clarification of the intended agent division of labor.

`Co-authored-by: Codex <codex@openai.com>`
