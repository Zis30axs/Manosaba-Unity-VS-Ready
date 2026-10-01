# G4 Play Mode gate: run the reconstructed project inside the Editor and collect the same evidence the
# standalone Player produces.
#
#   pwsh -File tools/unity-rebuild/g4_play_mode.ps1
#
# Steps:
#   1. ProjectBootstrap - author Assets/Scenes/Boot.unity and register it in the build settings
#   2. PlayModeGate.Run - switch Addressables to "Use Existing Build" (so Play Mode loads the content the
#      G3 build produced, not the AssetDatabase), open the Boot scene, enter Play Mode, wait for
#      ManosabaBoot's evidence, leave Play Mode and exit
#
# The evidence file is deleted before the run and required to be *fresh* afterwards, and the gate's own
# artifact is checked the same way: a stale file must never be read as this round's result.
#
# Exit codes: 0 = Play Mode ran and the runtime assertions passed
#             1 = Play Mode ran but a runtime assertion failed
#             2 = BLOCKED (no Editor, no scene, no settings)
#             3 = FAIL: Play Mode produced no evidence within the timeout
. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate           = 'G4_editor_play_mode'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    unity_exe      = $script:UnityExe
    status         = 'NOT_RUN'
    blocked_reason = $null
    bootstrap      = $null
    play_mode      = $null
    evidence       = $null
    game_start     = $null
}

if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g4_play_mode.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

# --- 1. bootstrap ------------------------------------------------------------------
Write-Host "=== bootstrap (Boot scene + build settings) ===" -ForegroundColor Cyan
$bootLog = Join-Path $script:LogRoot 'bootstrap_g4.log'
$bootJsonPath = Join-Path $script:UnityDataRoot 'bootstrap_result.json'
Remove-Item -LiteralPath $bootJsonPath -ErrorAction SilentlyContinue
$bootStarted = Get-Date
$boot = Invoke-Logged -FilePath $script:UnityExe -LogPath $bootLog -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.ProjectBootstrap.Run',
    '-logFile', '-'
)
$bootFresh = (Test-Path -LiteralPath $bootJsonPath) -and (Get-Item -LiteralPath $bootJsonPath).LastWriteTime -ge $bootStarted
$bootJson = if ($bootFresh) { Get-Content $bootJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$result.bootstrap = [ordered]@{ exit_code = $boot.exit_code; seconds = $boot.seconds; log = $boot.log; fresh = $bootFresh; result = $bootJson }
Write-Host "  bootstrap exit=$($boot.exit_code)"
if ($boot.exit_code -ne 0 -or -not $bootFresh -or $bootJson.status -ne 'OK') {
    $result.status = 'FAIL'
    $result.blocked_reason = 'Bootstrap failed or produced no fresh successful artifact.'
    Write-Json (Join-Path $script:UnityDataRoot 'g4_play_mode_gate.json') $result
    exit 1
}

# --- 2. Play Mode ------------------------------------------------------------------
$evidencePath = Join-Path $script:UnityDataRoot 'play_mode_evidence.json'
$gameEvidence = Join-Path $script:UnityDataRoot 'play_mode_game_start_evidence.json'
$toolPath     = Join-Path $script:UnityDataRoot 'g4_play_mode.json'        # written by PlayModeGate
$gatePath     = Join-Path $script:UnityDataRoot 'g4_play_mode_gate.json'   # written by this script
Remove-Item $evidencePath -ErrorAction SilentlyContinue
Remove-Item $gameEvidence -ErrorAction SilentlyContinue
Remove-Item $toolPath -ErrorAction SilentlyContinue
Remove-Item $gatePath -ErrorAction SilentlyContinue

$env:MANOSABA_EVIDENCE = $evidencePath
$env:MANOSABA_SMOKE = '1'      # keep the probe's own "smoke" semantics (Application.Quit is a no-op in the Editor)
$env:MANOSABA_CONTENT = '1'    # require the content probe to succeed, exactly like the Player gate

# Run the same reconstructed game-start probe as G5.  PlayModeGate watches this path too and won't
# leave Play Mode until the game-start evidence exists (or the session deadline expires).
$gamePrefabGuid = ''
$metaPath = Join-Path $script:UnityProject 'Assets\GameContent\#WitchTrials\Prefabs\General\Core\Game.prefab.meta'
if (Test-Path $metaPath) {
    $gamePrefabGuid = (Select-String -Path $metaPath -Pattern '^guid: ([0-9a-f]{32})').Matches[0].Groups[1].Value
}
$gamePrefabName = 'Game(Clone)'
$bootRefsPath = Join-Path $script:UnityDataRoot 'level0_boot_references.json'
$bootRefs = if (Test-Path $bootRefsPath) { Get-Content $bootRefsPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
if ($bootRefs -and $bootRefs.status -eq 'OK') { $gamePrefabName = $bootRefs._gamePrefabInstanceName }

$env:MANOSABA_GAMESTART = '1'
$env:MANOSABA_GAMESTART_EVIDENCE = $gameEvidence
$env:MANOSABA_GAMESTART_GAMEPREFAB_GUID = $gamePrefabGuid
$env:MANOSABA_GAMESTART_GAMEPREFAB_NAME = $gamePrefabName
$env:MANOSABA_GAMESTART_AUTO_ADVANCE = '1'
$env:MANOSABA_GAMESTART_AUTO_ADVANCE_SECONDS = '12'

Write-Host "=== Editor Play Mode ===" -ForegroundColor Cyan
$started = Get-Date
$log = Join-Path $script:LogRoot 'play_mode.log'
$run = Invoke-Logged -FilePath $script:UnityExe -LogPath $log -Arguments @(
    '-batchmode', '-nographics',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.PlayModeGate.Run',
    '-logFile', '-'
)
$gate = if (Test-Path $toolPath) { Get-Content $toolPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$gateFresh = $gate -and (Get-Item $toolPath).LastWriteTime -ge $started
$evidence = if (Test-Path $evidencePath) { Get-Content $evidencePath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$gameStart = if (Test-Path $gameEvidence) { Get-Content $gameEvidence -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$evidenceFresh = $evidence -and (Get-Item -LiteralPath $evidencePath).LastWriteTime -ge $started
$gameFresh = $gameStart -and (Get-Item -LiteralPath $gameEvidence).LastWriteTime -ge $started
$contentRecords = @($evidence.content.records)
$builtContent = $evidence.content.status -eq 'LOADED' -and $contentRecords.Count -gt 0 -and
    @($contentRecords | Where-Object { $_.status -ne 'Succeeded' -or $_.provider -ne 'UnityEngine.ResourceManagement.ResourceProviders.BundledAssetProvider' }).Count -eq 0

$result.play_mode = [ordered]@{
    exit_code = $run.exit_code
    seconds   = $run.seconds
    log       = $log
    artifact  = $toolPath
    fresh     = [bool]$gateFresh
    evidence_fresh = [bool]$evidenceFresh
    built_content_verified = [bool]$builtContent
    result    = $gate
}
$result.evidence = $evidence
$result.game_start = [ordered]@{
    evidence_path = $gameEvidence
    fresh         = [bool]$gameFresh
    result        = $gameStart
}

if (-not $gate -or -not $gateFresh) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "PlayModeGate produced no fresh artifact (exit=$($run.exit_code)); see $log"
    Write-Json $gatePath $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

switch ($gate.status) {
    'PASS'          { $result.status = 'PASS' }
    'BLOCKED_NO_EVIDENCE_PATH' { $result.status = 'BLOCKED'; $result.blocked_reason = $gate.error }
    'BLOCKED_NO_SETTINGS'      { $result.status = 'BLOCKED'; $result.blocked_reason = $gate.error }
    'BLOCKED_NO_SCENE'         { $result.status = 'BLOCKED'; $result.blocked_reason = $gate.error }
    'FAIL_TIMEOUT'  { $result.status = 'FAIL'; $result.blocked_reason = $gate.error }
    default         { $result.status = 'FAIL'; $result.blocked_reason = "play mode status $($gate.status)" }
}
if ($result.status -eq 'PASS' -and ($run.exit_code -ne 0 -or -not $evidenceFresh -or $evidence.status -ne 'PASS' -or
    -not $gameFresh -or $gameStart.status -ne 'STARTED')) {
    $result.status = 'FAIL'
    $result.blocked_reason = 'Play Mode requires exit 0, fresh successful boot evidence, and fresh STARTED game evidence.'
}
if ($result.status -eq 'PASS' -and -not $builtContent) {
    $result.status = 'FAIL'
    $result.blocked_reason = 'Play Mode requires successful Addressables records served by BundledAssetProvider.'
}

Write-Json $gatePath $result
$content = if ($evidence) { $evidence.content } else { $null }
Write-Host ("G4 {0}: editor exit={1} play={2} assemblies={3} probes={4} content={5} ({6}s)" -f `
    $result.status, $run.exit_code, $gate.play_seconds,
    $(if ($evidence) { $evidence.assemblies_found } else { 'n/a' }),
    $(if ($evidence) { $evidence.types_probed_ok } else { 'n/a' }),
    $(if ($content) { $content.status } else { 'n/a' }),
    $run.seconds) -ForegroundColor $(if ($result.status -eq 'PASS') { 'Green' } else { 'Red' })
if ($gameStart) {
    Write-Host ("  game start: {0} completion={1} played='{2}' spot='{3}'" -f `
        $gameStart.status, $gameStart.initialize_completion, $gameStart.naninovel_played_script, `
        $gameStart.naninovel_playback_spot)
    Write-Host ("  auto-advance: {0} transitions={1} releases={2} final='{3}'" -f `
        $gameStart.auto_advance, $gameStart.auto_advance_transitions, $gameStart.auto_advance_wait_releases, `
        $gameStart.auto_advance_final)
}

if ($result.status -eq 'PASS') { exit 0 }
if ($result.status -eq 'BLOCKED') { exit 2 }
exit 1
