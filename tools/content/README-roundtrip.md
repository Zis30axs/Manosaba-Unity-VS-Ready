# Real content round-trip verification

`roundtrip.py` tests the existing reconstruction pipeline using local editable
assets and reads the resulting Addressables bundles with UnityPy. The default
four-case route requires Python with UnityPy and Pillow, and Unity 6000.0.48f1
for import/build/Play Mode.
Only locally imported resources are used. The installed original game is not
opened; `run` sets `MANOSABA_GAME_ROOT` to a nonexistent path.

```powershell
$env:MANOSABA_UNITY_EDITOR = 'C:\Program Files\Unity\Hub\Editor\6000.0.48f1\Editor\Unity.exe'
$env:MANOSABA_PYTHON = (Resolve-Path tools\venv\Scripts\python.exe).Path
& $env:MANOSABA_PYTHON tools\content\roundtrip.py run --directory tools\unity-rebuild\out\content-roundtrip-01
```

Use a new `--directory` for each run so earlier evidence remains available. Close
the Editor before running batch commands. `run` selects its four source files
for both sync and build; an ordinary unfiltered `content build` synchronizes
other changed editable sources as well.

The default cases are:

| Case | Editable source | Verification |
| --- | --- | --- |
| TEXT | Opening Chinese localization `.txt` | Canonical UTF-8 bytes in the built TextAsset and Play Mode load |
| IMAGE | Alisa's existing `Body` sprite crop (`Alisa.sprite~67.png`) | Visible magenta square in source, imported Sprite, built Sprite, and Play Mode load |
| DATA | `CharacterData.asset.json` | First character's Chinese name string in JSON, imported ScriptableObject, built content, and Play Mode load |
| SCENARIO | Opening Naninovel `.asset.json` | First identified text through the built `Script.TextMap.GetTextOrNull` runtime accessor, with a nonempty playlist |

An optional `--master-data` adds the exported
`res/Assets/#WitchTrials/Data/MasterData.asset.json` case. It increments the safe
`_defaultHp` float and verifies the imported asset plus the `DefaultHp` getter
through `Resources.Load("MasterData")` in Play Mode. Existing four-case journals
remain supported. To test only this Resources asset without rebuilding
Addressables:

```powershell
& $env:MANOSABA_PYTHON tools\content\roundtrip.py run --master-data-only --directory tools\unity-rebuild\out\content-roundtrip-masterdata-01
```

`--master-data [source-path]` can select another exported MasterData JSON at the
same Resources target. The MasterData-only route uses neither image/bundle
Python dependencies nor the built Addressables catalog. Its `verify` command
records `PENDING_RUNTIME`; it becomes verified when the Play Mode getter probe
passes. `--no-runtime` deliberately leaves that case pending.

The four bundle sources receive a unique test marker; MasterData receives a
numeric increment recorded in its expected value. The backup journal is written before
any mutation and records SHA-256 for original source bytes, imported target
bytes, and `.meta` bytes. The verifier rejects changed metadata, missing/ambiguous
bundle assets, and stale values. Bundle cases load by the existing GUID and check
that the resource location uses `BundledAssetProvider`; it cannot pass by loading
the AssetDatabase. The Resources case uses the existing `MasterData` key and its
runtime getter. GPU pixel readback leaves `isReadable` and importer settings
unchanged. The image check allows compression error and configured downscaling.

An optional `--image` selects another indexed Sprite/Texture with its own planned
Addressables entry. Trimmed crops use the shared sync normalization for the
expected imported hash, canvas size and marker position. Read-only duplicate
Sprite sources and dependency-only images are rejected before modifying files.

`run` restores the original bytes in `finally`, reimports those restored assets,
and rebuilds the restored Addressables content when bundle cases were tested.
The MasterData-only route reimports its restored Resources asset without a
content rebuild. Existing generated differences are restored
as recorded instead of being reconverted from potentially different sources.
Interrupted or forcibly terminated runs can be recovered manually:

```powershell
& $env:MANOSABA_PYTHON tools\content\roundtrip.py restore --directory tools\unity-rebuild\out\content-roundtrip-01
$env:MANOSABA_ROUNDTRIP_PLAN = (Resolve-Path tools\unity-rebuild\out\content-roundtrip-01\journal.json).Path
$projectPath = (Resolve-Path UnityProject).Path
$unityArguments = @('-batchmode', '-nographics', '-quit', '-projectPath', ('"{0}"' -f $projectPath), '-logFile', '-')
$reimport = Start-Process -FilePath $env:MANOSABA_UNITY_EDITOR -ArgumentList ($unityArguments + @('-executeMethod', 'Manosaba.EditorTools.ContentRoundTripProbe.RestoreImports')) -Wait -WindowStyle Hidden -PassThru
if ($reimport.ExitCode -ne 0) { throw 'RestoreImports failed; inspect restore_import_result.json' }
$rebuild = Start-Process -FilePath $env:MANOSABA_UNITY_EDITOR -ArgumentList ($unityArguments + @('-executeMethod', 'Manosaba.EditorTools.AddressablesBuild.Run')) -Wait -WindowStyle Hidden -PassThru
if ($rebuild.ExitCode -ne 0) { throw 'Restored content build failed; inspect addressables_build_result.json' }
```

For diagnosis, use separate `prepare`, `verify`, and `restore` commands. After
`prepare`, synchronize the selected `source` paths recorded in `journal.json`,
build if bundle cases were selected, and run `verify`. Set
`MANOSABA_ROUNDTRIP_PLAN` to that journal and invoke
`ContentRoundTripProbe.Run` without `-quit` or `-nographics` to collect Play Mode
pixel evidence. `--no-runtime` skips that optional step in `run`.

Evidence and backups remain below the gitignored `tools/unity-rebuild/out/`
directory. They contain local game content and must not be committed. The
scenario marker verifies the runtime fallback text map; Chinese localization
has its own TEXT test and overrides fallback text during normal Chinese play.
The existing G4/G5 gates still test game boot and Player execution separately.

The manual recovery commands wait for each Unity process to exit before starting
the next. For a MasterData-only journal, omit the final Addressables rebuild.
