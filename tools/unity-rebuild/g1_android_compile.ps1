# Android G1 gate: compile the project with Unity's actual compiler while Android is the
# active build target.  The Windows G1 contract is intentionally left untouched.
#
#   pwsh -File tools/unity-rebuild/g1_android_compile.ps1
#
# Exit codes: 0 = PASS; 1 = compile failure; 2 = BLOCKED (missing inputs, licence,
# or Android Build Support).

. "$PSScriptRoot/common.ps1"

$gatePath = Join-Path $script:UnityDataRoot 'g1_android_compile.json'
$compileResult = Join-Path $script:UnityDataRoot 'compile_result.json'
# Never leave an older Android gate/compile receipt available when this invocation
# blocks before reaching Unity (module, preflight, licence, or target evidence).
Remove-Item -LiteralPath $gatePath, $compileResult -Force -ErrorAction SilentlyContinue
$result = [ordered]@{
    gate            = 'G1_android_project_and_compilation'
    generated       = (Get-Date).ToUniversalTime().ToString('o')
    target          = 'Android'
    unity_exe       = $script:UnityExe
    unity_version   = if ($script:UnityExe) { Get-UnityVersion $script:UnityExe } else { $null }
    status          = 'NOT_RUN'
    blocked_reason  = $null
    preflight       = $null
    compile         = $null
}

function Finish-Gate {
    param([string]$Status, [string]$Reason, [int]$ExitCode)
    $result.status = $Status
    if ($Reason) { $result.blocked_reason = $Reason }
    Write-Json $gatePath $result
    $colour = if ($Status -eq 'PASS') { 'Green' } elseif ($Status -like 'BLOCKED*') { 'Yellow' } else { 'Red' }
    Write-Host ("G1 Android {0}: {1}" -f $Status, $gatePath) -ForegroundColor $colour
    exit $ExitCode
}

if (-not $script:UnityExe) { Finish-Gate 'BLOCKED_NO_UNITY' 'Unity Editor not found; set MANOSABA_UNITY_EDITOR.' 2 }

$editorRoot = Split-Path $script:UnityExe -Parent
$androidPlayerRoot = Join-Path $editorRoot 'Data\PlaybackEngines\AndroidPlayer'
if (-not (Test-Path -LiteralPath $androidPlayerRoot -PathType Container)) {
    Finish-Gate 'BLOCKED_NO_ANDROID_MODULE' "Unity Android Build Support is not installed at $androidPlayerRoot; install the 6000.0.48f1 Android module and re-run." 2
}

# Keep the same imported-source preflight as Windows G1.  It is evidence validation,
# not a second import pipeline.
$preflightJson = Join-Path $script:OutRoot 'preflight_build.json'
$preflightStarted = Get-Date
Remove-Item -LiteralPath $preflightJson -Force -ErrorAction SilentlyContinue
try {
    & $script:PythonExe (Join-Path $PSScriptRoot 'preflight.py') --mode build | Out-Null
    $preflightExit = $LASTEXITCODE
} catch {
    Finish-Gate 'BLOCKED_PREFLIGHT' "Build preflight could not start Python: $($_.Exception.Message)"
}
if (Test-Path -LiteralPath $preflightJson -PathType Leaf) {
    try { $pf = Get-Content $preflightJson -Raw -Encoding UTF8 | ConvertFrom-Json }
    catch { Finish-Gate 'BLOCKED_PREFLIGHT' "Build preflight receipt is not valid JSON: $($_.Exception.Message)" }
    $result.preflight = [ordered]@{
        status = $pf.status; missing_required_count = $pf.missing_required_count
        inputs_checked = $pf.inputs.Count; bundle_files = $pf.bundles.bundle_count
        unity_found = $pf.unity.found
        artifact_fresh = (Get-Item -LiteralPath $preflightJson).LastWriteTime -ge $preflightStarted
    }
}
$preflightFresh = (Test-Path -LiteralPath $preflightJson -PathType Leaf) -and
    ((Get-Item -LiteralPath $preflightJson).LastWriteTime -ge $preflightStarted)
if ($preflightExit -ne 0 -or -not $preflightFresh -or -not $pf -or $pf.status -ne 'INPUTS_PRESENT' -or
    [int]$pf.missing_required_count -gt 0) {
    Finish-Gate 'BLOCKED_PREFLIGHT' "Build preflight requires a fresh INPUTS_PRESENT receipt with zero missing required inputs; see $preflightJson" 2
}

Write-Host '=== Unity licence check (Android) ===' -ForegroundColor Cyan
$lic = Assert-UnityLicense -Exe $script:UnityExe -ProbeLog (Join-Path $script:LogRoot 'g1_android_license_probe.log')
if (-not $lic.ok) { Finish-Gate 'BLOCKED_NO_LICENSE' $lic.reason 2 }

Remove-Item -LiteralPath $compileResult -Force -ErrorAction SilentlyContinue
$log = Join-Path $script:LogRoot 'g1_android_compile.log'
$started = Get-Date
$run = Invoke-Logged -FilePath $script:UnityExe -LogPath $log -Arguments @(
    '-batchmode', '-nographics', '-quit', '-buildTarget', 'Android',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.CompileGate.Run', '-logFile', '-')
$text = if (Test-Path -LiteralPath $log) { Get-Content $log -Raw -Encoding UTF8 } else { '' }
$csErrors = ([regex]::Matches($text, 'error CS\d+')).Count
$csWarnings = ([regex]::Matches($text, 'warning CS\d+')).Count
$loadErrors = ([regex]::Matches($text, 'will not be loaded due to errors|Unable to resolve reference')).Count
$fresh = (Test-Path -LiteralPath $compileResult -PathType Leaf) -and ((Get-Item $compileResult).LastWriteTime -ge $started)
$graph = if ($fresh) { Get-Content $compileResult -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$targetEvidence = $fresh -and $graph.active_build_target -eq 'Android' -and
    $graph.build_target_group -eq 'Android'
$receiptOk = $fresh -and $graph.status -eq 'COMPILED_NO_DIAGNOSTICS_COLLECTED' -and
    $graph.assembly_graph_recorded -and $graph.player_assembly_count -gt 0 -and $targetEvidence
$result.compile = [ordered]@{
    command = $run.command; exit_code = $run.exit_code; seconds = $run.seconds
    log = $run.log; artifact = $compileResult; artifact_fresh = [bool]$fresh
    cs_errors = $csErrors; cs_warnings = $csWarnings; assembly_load_errors = $loadErrors
    target_evidence = [ordered]@{
        active_build_target = if ($graph) { $graph.active_build_target } else { $null }
        build_target_group = if ($graph) { $graph.build_target_group } else { $null }
        matches_android = [bool]$targetEvidence
    }
    assembly_graph = $graph
}
if (-not $targetEvidence) {
    Finish-Gate 'BLOCKED_WRONG_TARGET_RECEIPT' 'CompileGate receipt is missing active_build_target/build_target_group=Android; the Unity compile cannot be credited to the Android target.' 2
}
if ($run.exit_code -ne 0 -or $csErrors -gt 0 -or $loadErrors -gt 0 -or -not $receiptOk) {
    Finish-Gate 'FAIL' 'Unity Android compile did not produce a fresh successful assembly graph with zero compiler or assembly-load errors.' 1
}
Finish-Gate 'PASS' $null 0

