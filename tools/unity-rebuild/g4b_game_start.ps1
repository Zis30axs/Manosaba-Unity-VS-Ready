# G4b game-start gate: inside Editor Play Mode, actually start the *reconstructed game* and record how
# far it gets.
#
#   pwsh -File tools/unity-rebuild/g4b_game_start.ps1
#
# Steps:
#   1. ProjectBootstrap  - Boot scene + build settings
#   2. PlayModeGate.Run    with MANOSABA_GAMESTART=1: the Boot scene enters Play Mode, ManosabaBoot runs
#      its usual probes *and* spawns ManosabaGameStart, which reflects the game's boot component
#      (WitchTrials.Views.GameInitializer) into existence and records the outcome; the gate waits for
#      both evidence files before leaving Play Mode.
#
# Status semantics (this is a measurement, not a verdict on the game):
#   STARTED        - the entry component was created and Awake's reconstructed body ran (the private
#                    _addressablesManager field it assigns is non-null)
#   COMPONENT_ONLY - the component existed but Awake's effect is missing
#   BLOCKED_TYPE   - the entry type did not resolve
#   ERROR          - the probe itself failed
# The gate PASSes on STARTED and reports the boot-chain exception separately: Start() is expected to
# stop where the game's own scene data is missing, and that boundary is the useful result.
#
# Exit codes: 0 = entry point executed; 1 = it did not (details in the artifact)
#             2 = BLOCKED (no Editor / no scene / no fresh artifact); 3 = Play Mode timed out
param(
    [switch]$AllowComponentOnly,
    # Supply the serialized references the game's own scene would provide (SteamManager, SplashScreen,
    # the boot prefab's AssetReference and instance name) so the boot chain can move past the first
    # missing reference and show what stops it next.
    [switch]$Wire
)

. "$PSScriptRoot\common.ps1"

$gamePrefabGuid = ''
$gamePrefabName = 'Game(Clone)'   # fallback only; the shipped scene's own name is preferred below
$gamePrefabRelative = 'Assets\GameContent\#WitchTrials\Prefabs\General\Core\Game.prefab'

# The boot scene ships as a player-build scene (no type tree), so its serialized references are recovered
# byte-by-byte by tools/unity-rebuild/decode_level0_boot.py.  Using the game's real instance name instead of
# an invented one keeps the reconstruction faithful and proves the recovered GUID names the prefab this
# project rebuilt (the decoder checks that against the shipped catalog).
$bootRefsPath = Join-Path $script:UnityDataRoot 'level0_boot_references.json'
$bootRefs = if (Test-Path $bootRefsPath) { Get-Content $bootRefsPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
if ($bootRefs -and $bootRefs.status -eq 'OK') {
    $gamePrefabName = $bootRefs._gamePrefabInstanceName
    Write-Host ("boot scene: decoded _gamePrefabInstanceName='{0}', _gamePrefab GUID {1} -> {2}" -f `
        $gamePrefabName, $bootRefs._gamePrefab_m_AssetGUID, $bootRefs._gamePrefab.internal_id) -ForegroundColor Cyan
} else {
    Write-Host "boot scene: no decoded references at $bootRefsPath; using fallback name '$gamePrefabName'" -ForegroundColor Yellow
}

if ($Wire) {
    $meta = Join-Path $script:UnityProject "$gamePrefabRelative.meta"
    if (-not (Test-Path $meta)) {
        Write-Host "BLOCKED: boot prefab meta not found at $meta" -ForegroundColor Red
        exit 2
    }
    $gamePrefabGuid = (Select-String -Path $meta -Pattern '^guid: ([0-9a-f]{32})').Matches[0].Groups[1].Value
    Write-Host "wiring: boot prefab guid=$gamePrefabGuid" -ForegroundColor Cyan
}

$result = [ordered]@{
    gate           = 'G4b_reconstructed_game_start'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    unity_exe      = $script:UnityExe
    status         = 'NOT_RUN'
    blocked_reason = $null
    bootstrap      = $null
    play_mode      = $null
    game_start     = $null
    game_start_partial = $null
    boot_scene     = $bootRefs
    chain_seconds  = $null
    play_mode_timeout_seconds = $null
    warmup         = $null
    warmup_reloads = $null
    warmup_compile_requests = $null
}

if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g4b_game_start.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

# --- 1. bootstrap ------------------------------------------------------------------
Write-Host "=== bootstrap (Boot scene + build settings) ===" -ForegroundColor Cyan
$bootLog = Join-Path $script:LogRoot 'bootstrap_g4b.log'
$boot = Invoke-Logged -FilePath $script:UnityExe -LogPath $bootLog -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.ProjectBootstrap.Run',
    '-logFile', '-'
)
$result.bootstrap = [ordered]@{ exit_code = $boot.exit_code; seconds = $boot.seconds; log = $boot.log }

# --- 2. Play Mode with the game-start probe ----------------------------------------
$bootEvidence = Join-Path $script:UnityDataRoot 'play_mode_evidence.json'
# Keep the two modes apart: the rejected-references run and the wired run answer different questions, and
# overwriting one with the other would lose the "where does it stop without scene data" baseline.
$gameEvidence = if ($Wire) { Join-Path $script:UnityDataRoot 'game_start_evidence_wired.json' }
                else { Join-Path $script:UnityDataRoot 'game_start_evidence.json' }
$toolPath     = Join-Path $script:UnityDataRoot 'g4_play_mode.json'
$gatePath     = if ($Wire) { Join-Path $script:UnityDataRoot 'g4b_game_start_wired.json' }
                else { Join-Path $script:UnityDataRoot 'g4b_game_start.json' }
Remove-Item $bootEvidence, $gameEvidence, $toolPath, $gatePath -ErrorAction SilentlyContinue

$env:MANOSABA_EVIDENCE = $bootEvidence
$env:MANOSABA_GAMESTART = '1'
$env:MANOSABA_GAMESTART_EVIDENCE = $gameEvidence
$env:MANOSABA_SMOKE = '1'
$env:MANOSABA_CONTENT = '1'

# The probe's chain window and the gate's play-mode budget have to agree: when the chain window exceeds the
# budget, the gate tears the session down before the probe can write anything, and the run reports BLOCKED
# with no evidence at all (measured: a 45 s window inside the fixed 240 s budget).  So the budget is derived
# here from the same knob the probe reads, with room for the probe's other phases (start window, prefab
# handle wait, state collection) and for the engine to actually finish initialising.
$chainSeconds = 0.0
[void][double]::TryParse($env:MANOSABA_GAMESTART_CHAIN_SECONDS,
    [System.Globalization.NumberStyles]::Float,
    [System.Globalization.CultureInfo]::InvariantCulture, [ref]$chainSeconds)
if ($chainSeconds -le 0) { $chainSeconds = 8.0 }   # the probe's own default (2 x WindowSeconds)
$playModeTimeout = [Math]::Ceiling($chainSeconds * 2.0 + 180.0)
$env:MANOSABA_PLAYMODE_TIMEOUT_SECONDS = [string]$playModeTimeout
Write-Host ("chain window: {0}s -> play-mode budget {1}s" -f $chainSeconds, $playModeTimeout) -ForegroundColor Cyan
$result.chain_seconds = $chainSeconds
$result.play_mode_timeout_seconds = $playModeTimeout

if ($Wire) {
    $env:MANOSABA_GAMESTART_WIRE = '1'
    $env:MANOSABA_GAMESTART_GAMEPREFAB_GUID = $gamePrefabGuid
    $env:MANOSABA_GAMESTART_GAMEPREFAB_NAME = $gamePrefabName
} else {
    Remove-Item Env:\MANOSABA_GAMESTART_WIRE -ErrorAction SilentlyContinue
}

Write-Host "=== warm-up Play Mode (absorb Unity's own mid-session recompilation) ===" -ForegroundColor Cyan
# Unity 6 can request a full script recompilation *inside* a Play Mode session ("[ScriptCompilation] Requested
# script compilation because: Define symbols changed"); the domain reload that follows destroys every coroutine
# and all engine static state.  Measured on this project: the reload landed ~17 s into the session, so a 60 s
# measurement window produced no evidence at all while an 8 s one happened to finish first.  This pre-session
# enters Play Mode without the game-start probe, waits for the compilation to settle
# (MANOSABA_PLAYMODE_WAIT_FOR_COMPILE), and leaves; the measured session that follows then runs on an
# already-consistent assembly set.
$warmLog = Join-Path $script:LogRoot 'playmode_warmup.log'
$warmEvidence = Join-Path $script:UnityDataRoot 'play_mode_evidence_warmup.json'
$saved = @{
    MANOSABA_GAMESTART                = $env:MANOSABA_GAMESTART
    MANOSABA_GAMESTART_EVIDENCE       = $env:MANOSABA_GAMESTART_EVIDENCE
    MANOSABA_PLAYMODE_WAIT_FOR_COMPILE = $env:MANOSABA_PLAYMODE_WAIT_FOR_COMPILE
    MANOSABA_SMOKE                    = $env:MANOSABA_SMOKE
    MANOSABA_CONTENT                  = $env:MANOSABA_CONTENT
    MANOSABA_EVIDENCE                 = $env:MANOSABA_EVIDENCE
}
Remove-Item $warmEvidence, $toolPath, $gatePath -ErrorAction SilentlyContinue
$env:MANOSABA_GAMESTART = '0'
Remove-Item Env:\MANOSABA_GAMESTART_EVIDENCE -ErrorAction SilentlyContinue
$env:MANOSABA_PLAYMODE_WAIT_FOR_COMPILE = '1'
$env:MANOSABA_EVIDENCE = $warmEvidence
$warm = Invoke-Logged -FilePath $script:UnityExe -LogPath $warmLog -Arguments @(
    '-batchmode', '-nographics',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.PlayModeGate.Run',
    '-logFile', '-'
)
$result.warmup = [ordered]@{ exit_code = $warm.exit_code; seconds = $warm.seconds; log = $warmLog }
$warmReloads = if (Test-Path $warmLog) { (Select-String -Path $warmLog -Pattern 'Reloading assemblies after finishing script compilation' -SimpleMatch).Count } else { 0 }
$warmCompiles = if (Test-Path $warmLog) { (Select-String -Path $warmLog -Pattern 'Requested script compilation because' -SimpleMatch).Count } else { 0 }
Write-Host ("  warm-up exit={0} in {1:N1}s; compile requests={2}, post-compilation reloads={3}" -f `
    $warm.exit_code, $warm.seconds, $warmCompiles, $warmReloads)
$result.warmup_reloads = $warmReloads
$result.warmup_compile_requests = $warmCompiles
foreach ($name in $saved.Keys) {
    if ($saved[$name]) { Set-Item -Path "Env:\$name" -Value $saved[$name] }
    else { Remove-Item -Path "Env:\$name" -ErrorAction SilentlyContinue }
}
# The warm-up wrote its own gate artifact; it must not be mistaken for the measured run's.
Remove-Item $toolPath, $gatePath, $bootEvidence -ErrorAction SilentlyContinue

Write-Host "=== Editor Play Mode + game start ===" -ForegroundColor Cyan
$started = Get-Date
$log = Join-Path $script:LogRoot 'game_start.log'
$run = Invoke-Logged -FilePath $script:UnityExe -LogPath $log -Arguments @(
    '-batchmode', '-nographics',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.PlayModeGate.Run',
    '-logFile', '-'
)
$tool = if (Test-Path $toolPath) { Get-Content $toolPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$toolFresh = $tool -and (Get-Item $toolPath).LastWriteTime -ge $started
$bootJson = if (Test-Path $bootEvidence) { Get-Content $bootEvidence -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$gameJson = if (Test-Path $gameEvidence) { Get-Content $gameEvidence -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
# The probe also streams its state to a sidecar the gate does not watch, so a run the gate cut short still
# reports where the boot chain was when it was interrupted (see ManosabaGameStart.WritePartial).
$gamePartialPath = "$gameEvidence.partial.json"
$gamePartial = if (Test-Path $gamePartialPath) { Get-Content $gamePartialPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }

$result.play_mode = [ordered]@{
    exit_code      = $run.exit_code
    seconds        = $run.seconds
    log            = $log
    artifact       = $toolPath
    fresh          = [bool]$toolFresh
    result         = $tool
    boot_evidence  = $bootJson
}
$result.game_start = $gameJson
$result.game_start_partial = $gamePartial

if (-not $tool -or -not $toolFresh) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "PlayModeGate produced no fresh artifact (exit=$($run.exit_code)); see $log"
    Write-Json $gatePath $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}
if ($tool.status -eq 'FAIL_TIMEOUT') {
    $result.status = 'FAIL'
    $result.blocked_reason = "Play Mode timed out: $($tool.error)"
    Write-Json $gatePath $result
    Write-Host "FAIL: $($result.blocked_reason)" -ForegroundColor Red
    exit 3
}
if (-not $gameJson) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "game-start probe wrote no evidence; see $log"
    Write-Json $gatePath $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    if ($gamePartial) {
        Write-Host "  the probe's streamed sidecar says where the chain was when the gate cut it off:" -ForegroundColor Yellow
        Write-Host ("    start={0} chain={1}" -f $gamePartial.start_invocation, $gamePartial.initialize_completion)
        if ($gamePartial.engine_sample_trace) { Write-Host "    samples: $($gamePartial.engine_sample_trace)" }
        if ($gamePartial.engine_progress_trace) { Write-Host "    progress: $($gamePartial.engine_progress_trace)" }
        Write-Host ("    naninovel: initialized={0} initializing={1} total_commands={2}" -f `
            $gamePartial.naninovel_initialized, $gamePartial.naninovel_initializing, $gamePartial.naninovel_total_commands)
    } else {
        Write-Host "  (no sidecar either: the probe never reached its first phase boundary)" -ForegroundColor Yellow
    }
    exit 2
}

$startStatus = $gameJson.status
$ok = $startStatus -eq 'STARTED' -or ($AllowComponentOnly -and $startStatus -eq 'COMPONENT_ONLY')

# The measurement is only about the rebuilt boot scene if the entry component came *from* that scene.  When the
# probe has to create its own, it measures an instance without serialized data, so the run would say nothing
# about the scene - that is a failure of this gate, not a pass with a footnote.  (-Wire is the explicit
# exception: there the probe synthesizes references on purpose.)
$result.scene_component_used = ($gameJson.component_source -eq 'scene')
if (-not $Wire -and -not $result.scene_component_used) {
    $ok = $false
    Write-Host "  entry component came from the probe ('$($gameJson.component_source)'), not the rebuilt scene" -ForegroundColor Yellow
}

$result.status = if ($ok) { 'PASS' } else { 'FAIL' }
if (-not $ok) { $result.blocked_reason = "game start status: $startStatus" }

Write-Json $gatePath $result
Write-Host ("G4b {0}: entry={1} resolved={2} component={3} source={4} manager_created={5}" -f `
    $result.status, $gameJson.entry_type, $gameJson.type_resolved, $gameJson.component_created, `
    $gameJson.component_source, $gameJson.addressables_manager_created) -ForegroundColor $(if ($ok) { 'Green' } else { 'Red' })
if ($gameJson.start_invocation) { Write-Host "  boot chain stops at: $($gameJson.start_invocation)" }
Write-Host "  log: errors=$($gameJson.log_errors) exceptions=$($gameJson.log_exceptions)"
Write-Host ("  naninovel: initialized={0} initializing={1} total_commands={2} playing={3} played='{4}'" -f `
    $gameJson.naninovel_initialized, $gameJson.naninovel_initializing, $gameJson.naninovel_total_commands, `
    $gameJson.naninovel_playing, $gameJson.naninovel_played_script)
Write-Host ("  engine: services={0} progress_events={1} progress_last={2}" -f `
    $gameJson.engine_services_count, $gameJson.engine_progress_events, $gameJson.engine_progress_last)
if ($gameJson.engine_progress_trace) { Write-Host "    progress: $($gameJson.engine_progress_trace)" }
if ($gameJson.engine_sample_trace) { Write-Host "    samples:  $($gameJson.engine_sample_trace)" }
Write-Host ("  resources: master_data={0} keys='{1}' locations={2}" -f `
    $gameJson.master_data_state, $gameJson.master_data_asset_key_names, $gameJson.addressables_resource_locations)
Write-Host ("  naninovel file IO: path exists={0} read='{1}' write='{2}'" -f `
    $gameJson.naninovel_file_io_exists, $gameJson.naninovel_file_io_read, $gameJson.naninovel_file_io_write)
if ($gameJson.wiring -and $gameJson.wiring.Count -gt 0) {
    Write-Host "  wiring:"
    $gameJson.wiring | ForEach-Object { Write-Host "    - $_" }
}

if ($result.status -eq 'PASS') { exit 0 } else { exit 1 }
