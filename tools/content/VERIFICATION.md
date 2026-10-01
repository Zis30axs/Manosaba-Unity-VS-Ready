# Content workflow verification

This record contains completed checks from 2026-10-01–02 (UTC+08:00). Local game
resources, text contents, images, and backup payloads are kept outside Git. The
evidence filenames below refer to gitignored output generated on the test host.

## Completed round-trip checks

The four default cases passed both actual built-bundle inspection and Editor
Play Mode loading. Verification used the existing reconstruction converters
and Addressables build, with the original-game environment variable pointing
to a nonexistent directory. The Play Mode probe required
`BundledAssetProvider`; AssetDatabase loading could not satisfy these checks.

| Case | Imported asset | Built bundle | Play Mode evidence |
| --- | --- | --- | --- |
| TEXT | Canonical UTF-8 hash matched the modified source | The built TextAsset had the same hash | Loaded TextAsset had the same hash and existing address/GUID |
| IMAGE | The existing character Sprite contained the artificial marker | Built Sprite retained its 1437 × 3838 dimensions and marker | GPU readback returned marker RGBA `[255, 0, 255, 255]` |
| DATA | Safe CharacterData string field contained the artificial marker | The serialized field matched the modified JSON | The loaded ScriptableObject field matched |
| SCENARIO | Converted Naninovel Script text map contained the artificial marker | Built Script text map matched the modified JSON | `TextMap.GetTextOrNull` returned the modified value; `Playlist.Count` was 38 |

The content build report recorded `OK`, 30,399 entries, 32,691 locations,
333 bundles, 4,067,588,533 bundle bytes, and 613.8 seconds. Built-bundle
verification completed at 17:46; Play Mode verification completed at 17:47.

Evidence directory: `tools/unity-rebuild/out/content-roundtrip-current/`.

| Artifact | Recorded result |
| --- | --- |
| `journal.json` | Four case definitions, expected values, GUIDs, and original backup hashes; final state `RESTORED` |
| `built_result.json` | All four cases `PASS`, read from rebuilt bundles with UnityPy |
| `imported_result.json` | All four imported assets `PASS` |
| `runtime_result.json` | All four Play Mode loads `PASS`, including original catalog addresses and bundle provider IDs |
| `restoration_result.json` | All 12 original source, target, and `.meta` SHA-256 checks `PASS` after restoration and reimport |
| `restore_import_result.json` | `PASS`; Unity process exited with code 0 |
| `runtime.log`, `restore.log` | The corresponding Unity execution logs |

The image importer metadata remained byte-for-byte unchanged during the test.
All four cases retained their existing GUIDs and addresses. The recovery journal
saved and restored pre-test source edits and generated target differences. A
later normal content sync can regenerate those targets; the restoration report
records the exact state immediately after restoration, rather than claiming
that generated files can never change afterward.

## Recovery tool tests

Run synthetic-fixture tests without game assets:

```powershell
python -m unittest discover -s tools/content/tests -p test_roundtrip.py -v
```

Ten recovery-tool tests passed. Coverage includes preserving pre-existing source and
generated differences, restoring after preparation failure, refusing corrupt
backups before any restoration, rejecting paths outside the workspace/backup
directory, optional MasterData compatibility with four-case journals, and the
MasterData-only route avoiding image/bundle dependencies and Addressables builds.
The synthetic checks also cover trimmed Sprite canvas coordinates, one-pixel
clipping, normalized import hashes and rejecting read-only/unloadable image
choices before mutation. The separate real MasterData probe below supplies
runtime evidence.

Tool files:

- `tools/content/roundtrip.py`: prepare, inspect bundles, run, and restore.
- `tools/content/tests/test_roundtrip.py`: synthetic recovery and Resources-mode tests.
- `UnityProject/Assets/Editor/ContentRoundTripProbe.cs`: imported asset and Play Mode verification, plus restoration imports.
- `tools/content/README-roundtrip.md`: reproducible commands and recovery instructions.

## Completed MasterData Resources round-trip

The actual exported MasterData JSON was edited from `_defaultHp: 10` to `11`,
synchronized into the existing `WitchTrials.Models.MasterData` asset, and loaded
in Editor Play Mode using `Resources.Load("MasterData")`. The actual `DefaultHp`
getter returned `11`. The imported asset and runtime checks both passed at
23:36; the existing Resources GUID remained unchanged. This route required no
Addressables rebuild.

Evidence directory: `tools/unity-rebuild/out/content-roundtrip-masterdata-current/`.
`runtime_result.json` records the Resources provider, getter, and actual value.
`journal.json` finished `RESTORED`; `restore_import_result.json` passed. A separate
`restoration_result.json` confirmed all three original JSON, asset, and `.meta`
SHA-256 values after restoration and reimport.

## Completed safe-field and memory regression checks

An actual Unity Editor fixture loaded a temporary copy of the existing Court
ParticleSystem. Shrinking its burst array was rejected because its elements
contain retained AnimationCurve values. The whole component remained unchanged;
a same-size scalar edit succeeded while the curve hashes remained unchanged.
Synthetic SerializedObjects in the same Editor process verified integer/vector
list growth and shrink, retained unsupported empty lists, invalid GUID rejection,
and traversal of an existing cyclic managed graph.

A full-sync attempt exposed a scenario regression in the new resize guard:
recreating existing managed commands left nested arrays empty. The safe editor
now reuses exact-type existing instances, rejects new/replaced managed types and
conflicting source RIDs, and checks source graph depth before applying. The real
opening Script then wrote all 292 expected assigned parameters, with its GUID
unchanged. The Editor fixture also confirmed that managed scalar edits preserve
existing native curves and that conflicting/null managed edits are rejected.
Evidence: `tools/content/.work/review-patches/array-resize-safety/` and
`tools/content/.work/scenario-managed-reuse-sync.log` (gitignored).

The shared applier's signed LayerMask and packed Color32 conversions also
passed actual C# helper fixtures and 14 saved-asset YAML checks. The checks
include an all-bits mask (`4294967295` reinterpreted as `-1`), white Color32,
alpha values, finite HDR colours, and malformed inputs retaining their original
values. An actual Windows file-sharing fixture confirmed successful retried
atomic publication, a bounded persistent-lock failure, and later recovery.
Evidence: `tools/content/.work/safe-field-checks/` and
`tools/content/.work/review-patches/safe-helper-fixture/` (gitignored).

The existing expanded manifest was 2,016,192,840 bytes. Streaming migration
compacted it to 197,113,893 bytes, retaining 58,749 records and 12,432,096 field
occurrences. New manifests use field patterns and compact streamed writes;
successful batches retain their own conversion checkpoints. Seven state-store
tests cover JSON chunk boundaries, Unicode, malformed/truncated input, counts,
mixed support levels and preservation of unknown metadata.

All 89 Python regression tests passed with no failures, errors or skipped tests:
19 unified content/import tests, 38 content metadata/Sprite/recovery/state/CLI
tests, and 32 cache/bootstrap/gate receipt tests. The 13 gate test groups execute
58 isolated PowerShell receipt scenarios, including rejection of stale
build/runtime evidence, incomplete or invalid GUID maps and AssetDatabase
providers. Three CLI tests exercise all four entrypoints in actual Windows
PowerShell 5.1 and PowerShell 7, including empty arguments, aliases, paths with
spaces and failure exit codes. Evidence:
`tools/content/.work/final-regression-result.json` and `final-regressions.log`.
These synthetic receipt checks do not substitute for actual G1–G5 execution.

## Completed full import, sync and deep doctor

The final import adopted the existing complete resource tree, preserved edits,
and successfully completed all 33 bounded synchronization batches. It changed
21 Sprite, 9 DATA, 440 SCENARIO and 22,886 PREFAB source records; 30,281 were
unchanged and 5,112 were read-only. Both pending imports and failures were zero.
Evidence: `tools/content/.work/import-adopt-final-sync.log`.

A separate final check verified all 232 padded Sprites: each res source hash
remained unchanged, each Unity PNG matched the expected normalized image, and
each original `.meta` remained byte-for-byte unchanged. All 58,749 manifest
records had completed statuses: 30,311 `SYNCED`, 23,326 `PARTIAL`, and 5,112
`READ_ONLY`, with no invalid statuses. The compact manifest was 185,990,317
bytes. Evidence: `tools/content/.work/final-workspace-result.json`.

`doctor.ps1 --deep` then passed with no problems. Editability counts were 30,311
FULL, 17,375 PARTIAL and 11,063 READ_ONLY. These counts describe safe editing
coverage; they are distinct from synchronization status counts. Evidence:
`tools/unity-rebuild/out/content_doctor.json` and
`tools/content/.work/doctor-deep-final.log`.

## Completed physically disconnected independence gate

The complete `all` run passed at 02:50 on 2026-10-02 (UTC+08:00). The selected
original installation was physically renamed to a unique sibling before the
run. All child processes received nonexistent original game/data/AA roots and
the fixed current workspace roots. The original path remained unavailable for
all six stages; the `finally` restoration completed. A separate final check
confirmed that the original directory exists and the renamed sibling does not.

Evidence directory:
`tools/unity-rebuild/out/content-independence/20261001T182214/`.
`disconnect.json` records the recovery paths; `result.json` records overall
`PASS`, `original_disconnected: true`, `original_restored: true`, exit code 0 and
fresh artifacts for every stage. All execution-marker checks were empty.

| Stage | Result | Seconds | Actual evidence |
| --- | --- | ---: | --- |
| Incremental sync | PASS | 109.26 | Every category changed 0; 53,637 unchanged, 5,112 read-only; pending 0, failed 0 |
| G1 compilation | PASS | 47.72 | Fresh Unity assembly graph; 0 compiler errors, warnings or assembly-load errors |
| G2 import/reopen | PASS | 174.61 | Both new Editor processes found 30,905/30,905 assets; missing 0; GUID changes 0 |
| G3 Addressables | PASS | 986.03 | Fresh successful setup/build; 333 bundles and a newly built Windows catalog |
| G4 Play Mode | PASS | 83.64 | Fresh PASS boot evidence, STARTED game evidence and successful bundled content loads |
| G5 Player build/run | PASS | 292.41 | Fresh SUCCEEDED Windows Player build; runtime exited 0 with PASS/STARTED evidence |

G3 registered all 30,382 planned entries with no missing or skipped assets and
no unregistered labels. The actual build recorded 30,399 entries, 32,691
locations, 333 bundles totalling 4,067,588,115 bytes, and a 5,897,464-byte
`catalog.bin` in `Library/com.unity.addressables/aa/Windows`. Its native build
duration was 923.6 seconds; the table includes stage/process overhead.

Both G4 and G5 loaded 43 expected assemblies and passed five type probes. All
four Addressables probe records (Script, AuthorData, Chinese CharacterNames and
font) were `Succeeded` through `BundledAssetProvider`. Naninovel initialized,
`System/System_Title` ran, and the automatic advance probe completed. G5 built
the existing Mono development Player at `dist/player/manosaba-rebuild.exe`,
totalling 4,330,865,712 bytes. This verifies the current reconstructed boot/content
path, not every gameplay branch or an IL2CPP release build.

G5 retains a historical original source path inside its decoded `boot_scene`
provenance record. The independence report explicitly identifies this field;
it was excluded from execution-access checks, while the original directory was
physically unavailable. Runtime content was loaded from the reconstruction's
new bundles. Cached shipped catalog parsing remains a separate evidence probe.

Unity still emitted native `pred(*previous, *i)` serialization assertions,
also present before successful historical content and Player builds. This run
nevertheless produced fresh successful builds and verified runtime loads;
these logs are not represented as warning-free.

The separate cached shipped-catalog parsing probe logged eight
`Assembly-CSharp` lookup exceptions through `ManosabaBoot.ProbeCatalog`; the
same stack appears in older successful runs. The reconstruction's own four
bundle loads succeeded. Game-start counters recorded 18 errors and 0 exceptions,
matching the older Player result, but those counters cover only the subscribed
probe interval and do not describe the whole log. G4/G5 use `-nographics` with
a Null GPU; shader/video warnings are present, and this final run does not
verify displayed graphics or audiovisual playback. The log also records
missing transient Settings paths during shutdown. These residual runtime
diagnostics are outside the verified content round-trips and boot assertions.

Earlier attempts failed on unbounded conversion memory, Windows result-file
sharing, managed-array replacement and PowerShell 7 empty argument forwarding.
The completed run above used bounded batches, compact reports, retried atomic
result publication, existing managed-instance reuse and the corrected CLI.
Earlier failed/stale receipts were not used to establish this PASS.

After every Unity process finished, all four tracked files that were already
modified when the task began were restored from their initial backups, with
matching SHA-256 values. Evidence:
`tools/content/.work/initial-dirty-restoration-result.json`. Local resource,
manifest, cache, bundle, Player and generated linker metadata remain gitignored.

## Scope of import verification

The actual full import uses this host's existing complete `res` and Unity
reconstruction and adopts them while preserving edits. Actual isolated extraction
of two cached bundles also completed (219 objects, 211 ScriptableObjects, no
errors), including preservation of a modified source and explicit overwrite
recovery. A fully empty `res`/GameContent/Library import has not been timed or
run end to end. Its reused converter batches and receipts passed seven synthetic
tests; this remains a separate cold-import coverage limitation.
