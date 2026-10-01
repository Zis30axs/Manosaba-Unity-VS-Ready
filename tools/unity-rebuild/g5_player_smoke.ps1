# G5 smoke gate: build a real Windows x64 Player from this project and start it.
#
#   pwsh -File tools/unity-rebuild/g5_player_smoke.ps1
#
# Steps (each records its command, log, exit code and duration):
#   1. ProjectBootstrap  - author Assets/Scenes/Boot.unity and register it in the build settings
#   2. BuildPlayer       - build dist/player/manosaba-rebuild.exe
#   3. run the Player with -manosabaSmoke, which writes an evidence JSON and exits
#
# Exit codes: 0 = Player built and its runtime assertions passed
#             1 = built but a runtime assertion failed (or the Player crashed)
#             2 = BLOCKED (no Editor, no scene, build failed)
#
# Loads the reconstructed assemblies and newly built content in a standalone
# Player. The shipped catalog is imported resource evidence for a separate parsing
# probe; gameplay loads the reconstruction's own Addressables build. The existing
# game-start probe also records boot, Naninovel initialization and auto-advance.
. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate           = 'G5_standalone_player_smoke'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    unity_exe      = $script:UnityExe
    status         = 'NOT_RUN'
    blocked_reason = $null
    bootstrap      = $null
    build          = $null
    run            = $null
    evidence       = $null
}

if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

# --- 1. TMP essentials (asynchronous import; must run before the bootstrap probes it) ---
$result.tmp_essentials = Invoke-TmpEssentials

# --- 1b. bootstrap -----------------------------------------------------------------
Write-Host "=== bootstrap (Boot scene + build settings) ===" -ForegroundColor Cyan
$bootLog = Join-Path $script:LogRoot 'bootstrap.log'
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
$result.bootstrap = [ordered]@{
    exit_code = $boot.exit_code
    seconds   = $boot.seconds
    log       = $boot.log
    fresh     = $bootFresh
    result    = if ($bootFresh) { Get-Content $bootJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
}
Write-Host "  bootstrap exit=$($boot.exit_code)"
if ($boot.exit_code -ne 0 -or -not $bootFresh -or $result.bootstrap.result.status -ne 'OK') {
    $result.status = 'FAIL'
    $result.blocked_reason = 'Bootstrap failed or produced no fresh successful artifact.'
    Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
    exit 1
}

# --- 2. build the Player -----------------------------------------------------------
Write-Host "=== build Windows x64 Player ===" -ForegroundColor Cyan
$buildLog = Join-Path $script:LogRoot 'build_player.log'
$buildJsonPath = Join-Path $script:UnityDataRoot 'build_player.json'
Remove-Item -LiteralPath $buildJsonPath -ErrorAction SilentlyContinue
$buildStarted = Get-Date
$build = Invoke-Logged -FilePath $script:UnityExe -LogPath $buildLog -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.BuildPlayer.Run',
    '-logFile', '-'
)
$buildFresh = (Test-Path -LiteralPath $buildJsonPath) -and (Get-Item -LiteralPath $buildJsonPath).LastWriteTime -ge $buildStarted
$buildJson = if ($buildFresh) { Get-Content $buildJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$result.build = [ordered]@{
    exit_code = $build.exit_code
    seconds   = $build.seconds
    log       = $build.log
    fresh     = $buildFresh
    result    = $buildJson
}
if ($build.exit_code -ne 0 -or -not $buildJson -or $buildJson.status -ne 'SUCCEEDED') {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "Player build did not succeed ($($buildJson.status))"
    Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}
Write-Host "  build $($buildJson.status): $($buildJson.player_path) ($([math]::Round($buildJson.total_size_bytes/1MB,1)) MB, $($buildJson.total_time_seconds)s)"

# --- 3. run the Player -------------------------------------------------------------
Write-Host "=== run the Player (smoke) ===" -ForegroundColor Cyan
$exe = $buildJson.player_path
if (-not (Test-Path $exe)) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "Player executable missing: $exe"
    Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

$playerLog = Join-Path $script:LogRoot 'player_smoke.log'
$evidence = Join-Path $script:UnityDataRoot 'player_smoke_evidence.json'
$gameEvidence = Join-Path $script:UnityDataRoot 'player_game_start_evidence.json'
Remove-Item $evidence -ErrorAction SilentlyContinue
Remove-Item $gameEvidence -ErrorAction SilentlyContinue

# The same game-start probe the Editor gate runs, so "the reconstructed boot chain executes" is measured in
# the standalone Player too -- which is what the objective asks for.  The probe needs the boot prefab's GUID
# and instance name; the Player cannot read .meta files, so they travel in the environment.  The instance
# name comes from the shipped boot scene rather than being invented (see decode_level0_boot.py).
$gamePrefabGuid = ''
$metaPath = Join-Path $script:UnityProject 'Assets\GameContent\#WitchTrials\Prefabs\General\Core\Game.prefab.meta'
if (Test-Path $metaPath) {
    $gamePrefabGuid = (Select-String -Path $metaPath -Pattern '^guid: ([0-9a-f]{32})').Matches[0].Groups[1].Value
}
$gamePrefabName = 'Game(Clone)'
$bootRefsPath = Join-Path $script:UnityDataRoot 'level0_boot_references.json'
$bootRefs = if (Test-Path $bootRefsPath) { Get-Content $bootRefsPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
if ($bootRefs -and $bootRefs.status -eq 'OK') { $gamePrefabName = $bootRefs._gamePrefabInstanceName }
$result.boot_scene = $bootRefs
$env:MANOSABA_GAMESTART = '1'
$env:MANOSABA_GAMESTART_EVIDENCE = $gameEvidence
$env:MANOSABA_GAMESTART_GAMEPREFAB_GUID = $gamePrefabGuid
$env:MANOSABA_GAMESTART_GAMEPREFAB_NAME = $gamePrefabName
$env:MANOSABA_GAMESTART_AUTO_ADVANCE = '1'
$env:MANOSABA_GAMESTART_AUTO_ADVANCE_SECONDS = '12'

$savedEap = $ErrorActionPreference
$ErrorActionPreference = 'Continue'
$started = Get-Date
$proc = Start-Process -FilePath $exe -WorkingDirectory (Split-Path $exe) -WindowStyle Hidden -PassThru -ArgumentList @(
    '-batchmode', '-nographics', '-manosabaSmoke', '-manosabaContent', '-manosabaEvidence', $evidence, '-logFile', $playerLog
)
$exited = $proc.WaitForExit(240000)
if (-not $exited) {
    try { $proc.Kill() } catch { }
    $result.status = 'FAIL'
    $result.blocked_reason = $null
    $result.run = [ordered]@{ status = 'TIMEOUT'; seconds = 240; log = $playerLog }
    Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
    Write-Host "FAIL: Player did not exit within 240s" -ForegroundColor Red
    exit 1
}
$exitCode = $proc.ExitCode
$seconds = [math]::Round(((Get-Date) - $started).TotalSeconds, 2)
$ErrorActionPreference = $savedEap

$evidenceJson = if (Test-Path $evidence) { Get-Content $evidence -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$gameJson = if (Test-Path $gameEvidence) { Get-Content $gameEvidence -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$evidenceFresh = $evidenceJson -and (Get-Item -LiteralPath $evidence).LastWriteTime -ge $started
$gameFresh = $gameJson -and (Get-Item -LiteralPath $gameEvidence).LastWriteTime -ge $started
$contentRecords = @($evidenceJson.content.records)
$builtContent = $evidenceJson.content.status -eq 'LOADED' -and $contentRecords.Count -gt 0 -and
    @($contentRecords | Where-Object { $_.status -ne 'Succeeded' -or $_.provider -ne 'UnityEngine.ResourceManagement.ResourceProviders.BundledAssetProvider' }).Count -eq 0
$result.run = [ordered]@{
    exit_code      = $exitCode
    seconds        = $seconds
    log            = $playerLog
    evidence_path  = $evidence
    evidence       = $evidenceJson
    evidence_fresh = [bool]$evidenceFresh
    built_content_verified = [bool]$builtContent
}
$result.game_start = [ordered]@{
    evidence_path = $gameEvidence
    fresh         = [bool]$gameFresh
    result        = $gameJson
}

if ($exitCode -eq 0 -and $evidenceFresh -and $evidenceJson.status -eq 'PASS' -and $gameFresh -and $gameJson.status -eq 'STARTED' -and $builtContent) {
    $result.status = 'PASS'
} else {
    $result.status = 'FAIL'
    $result.blocked_reason = 'Player requires exit 0, fresh successful boot and STARTED game evidence, and successful BundledAssetProvider Addressables records.'
}

$catalog = if ($evidenceJson) { $evidenceJson.catalog } else { $null }
Write-Json (Join-Path $script:UnityDataRoot 'g5_player_smoke.json') $result
Write-Host ("G5 {0}: player exit={1} assemblies={2} types={3} catalog={4} keys={5} ({6}s)" -f `
    $result.status, $exitCode,
    $(if ($evidenceJson) { "$($evidenceJson.assemblies_found)" } else { 'n/a' }),
    $(if ($evidenceJson) { "$($evidenceJson.types_probed_ok)" } else { 'n/a' }),
    $(if ($catalog) { $catalog.status } else { 'n/a' }),
    $(if ($catalog) { $catalog.keys } else { 'n/a' }),
    $seconds) -ForegroundColor $(if ($result.status -eq 'PASS') { 'Green' } else { 'Red' })
if ($gameJson) {
    Write-Host ("  game start: {0}  spawned={1}  completion={2}" -f `
        $gameJson.status, ($gameJson.spawned_roots -join ', '), $gameJson.initialize_completion)
    Write-Host ("  naninovel: initialized={0} initializing={1} playing={2} played='{3}' spot='{4}'" -f `
        $gameJson.naninovel_initialized, $gameJson.naninovel_initializing, $gameJson.naninovel_playing, `
        $gameJson.naninovel_played_script, $gameJson.naninovel_playback_spot)
    Write-Host ("  auto-advance: {0} transitions={1} releases={2} final='{3}'" -f `
        $gameJson.auto_advance, $gameJson.auto_advance_transitions, $gameJson.auto_advance_wait_releases, `
        $gameJson.auto_advance_final)
    Write-Host ("  engine: services={0} progress_last={1} post_tasks={2} [{3}]" -f `
        $gameJson.engine_services_count, $gameJson.engine_progress_last, $gameJson.post_init_task_count, `
        $gameJson.post_init_tasks)
    Write-Host ("  post-init chain: engine states settings='{0}' global='{1}'" -f `
        $gameJson.engine_settings_state, $gameJson.engine_global_state)
    Write-Host ("    probe: settings slot='{0}'" -f $gameJson.settings_slot_load)
    Write-Host ("    probe: global   slot='{0}'" -f $gameJson.global_slot_load)
    Write-Host ("  LoadServiceState(SettingsStateMap): {0}" -f $gameJson.settings_state_services)
    Write-Host ("  UI Initialize(): {0}" -f $gameJson.managed_ui_initialize)
    Write-Host ("  providers: {0}" -f $gameJson.provider_state)
    Write-Host ("  post-init re-invoke: {0}" -f $gameJson.post_init_reinvoke)
    Write-Host ("  post-init capture:   {0}" -f $gameJson.post_init_capture)
    Write-Host ("  post-init original:  {0}" -f $gameJson.post_init_original)
    Write-Host ("  engine initializeTCS: {0}" -f $gameJson.engine_tcs)
    Write-Host ("  capture caller:      {0}" -f $gameJson.post_init_capture_stack)
    Write-Host ("  file IO: exists={0} read='{1}' write='{2}'" -f `
        $gameJson.naninovel_file_io_exists, $gameJson.naninovel_file_io_read, $gameJson.naninovel_file_io_write)
    Write-Host ("  log: errors={0} exceptions={1} first='{2}'" -f `
        $gameJson.log_errors, $gameJson.log_exceptions, $gameJson.first_exception)
}

if ($result.status -eq 'PASS') { exit 0 } else { exit 1 }
