# G0/G1 gate: verify inputs, verify the Unity toolchain, then compile with the REAL
# Unity compiler and record the outcome.
#
#   pwsh -File tools/unity-rebuild/g1_compile.ps1
#
# Exit codes:  0 = compiled with 0 errors    1 = compile errors or gate failure
#              2 = BLOCKED (missing input / no licence / no editor)
#
# Nothing here may write into the original game directory or the dump (section 3.1.3).
. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate            = 'G1_project_and_compilation'
    generated       = (Get-Date).ToUniversalTime().ToString('o')
    unity_exe       = $script:UnityExe
    unity_version   = if ($script:UnityExe) { Get-UnityVersion $script:UnityExe } else { $null }
    status          = 'NOT_RUN'
    blocked_reason  = $null
    compile         = $null
    preflight       = $null
}

# --- G0: inputs ---------------------------------------------------------------
Write-Host "=== G0 preflight ===" -ForegroundColor Cyan
$preflightJson = Join-Path $script:OutRoot 'preflight.json'
& $script:PythonExe (Join-Path $PSScriptRoot 'preflight.py') | Out-Null
if (Test-Path $preflightJson) {
    $pf = Get-Content $preflightJson -Raw -Encoding UTF8 | ConvertFrom-Json
    $result.preflight = [ordered]@{
        status                  = $pf.status
        missing_required_count  = $pf.missing_required_count
        inputs_checked          = $pf.inputs.Count
        bundle_files            = $pf.bundles.bundle_count
        unity_found             = $pf.unity.found
    }
    Write-Host "  preflight status: $($pf.status) (missing required: $($pf.missing_required_count))"
}

# --- editor presence ----------------------------------------------------------
if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g1_compile.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

# --- licence ------------------------------------------------------------------
Write-Host "=== Unity licence check ===" -ForegroundColor Cyan
$probeLog = Join-Path $script:LogRoot 'license_probe.log'
$lic = Assert-UnityLicense -Exe $script:UnityExe -ProbeLog $probeLog
if (-not $lic.ok) {
    $result.status = 'BLOCKED'
    $result.blocked_kind = $lic.kind
    $result.blocked_reason = $lic.reason
    Write-Json (Join-Path $script:UnityDataRoot 'g1_compile.json') $result
    Write-Host "BLOCKED [$($lic.kind)]: $($lic.reason)" -ForegroundColor Red
    exit 2
}
Write-Host "  licence OK"

# --- compile gate -------------------------------------------------------------
Write-Host "=== Unity compile gate ===" -ForegroundColor Cyan
$log = Join-Path $script:LogRoot 'g1_compile.log'
$run = Invoke-Logged -FilePath $script:UnityExe -LogPath $log -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.CompileGate.Run',
    '-logFile', '-'
)

$text = if (Test-Path $log) { Get-Content $log -Raw -Encoding UTF8 } else { '' }
$csErrors = ([regex]::Matches($text, 'error CS\d+')).Count
$csWarnings = ([regex]::Matches($text, 'warning CS\d+')).Count
$importErrors = ([regex]::Matches($text, 'Import Error|Failed to import')).Count
$missingScript = ([regex]::Matches($text, 'Missing (MonoBehaviour|Script)')).Count
# A project can compile cleanly and still be unusable: Unity refuses to *load* an assembly whose
# references it cannot resolve ("Assembly 'X' will not be loaded due to errors: Unable to resolve
# reference 'Y'"), and every assembly that references it is dropped along with it.  That is exactly what
# happened when the genuine FastEnum.dll (which references System.Runtime.CompilerServices.Unsafe) was
# added: the compile gate said PASS while Play Mode ran with 42 of 43 assemblies and a failing content
# probe.  So these markers are failures here, not warnings.
$loadErrors = ([regex]::Matches($text, "will not be loaded due to errors|Unable to resolve reference")).Count

$result.compile = [ordered]@{
    command             = $run.command
    exit_code           = $run.exit_code
    seconds             = $run.seconds
    log                 = $run.log
    log_bytes           = if (Test-Path $log) { (Get-Item $log).Length } else { 0 }
    cs_errors           = $csErrors
    cs_warnings         = $csWarnings
    import_errors       = $importErrors
    missing_script_hits = $missingScript
    assembly_load_errors = $loadErrors
}

$unityResult = Join-Path $script:UnityDataRoot 'compile_result.json'
if (Test-Path $unityResult) {
    $result.compile.assembly_graph = (Get-Content $unityResult -Raw | ConvertFrom-Json)
}

if ($csErrors -gt 0 -or $loadErrors -gt 0 -or $run.exit_code -ne 0) {
    $result.status = 'FAIL'
} else {
    $result.status = 'PASS'
}

Write-Json (Join-Path $script:UnityDataRoot 'g1_compile.json') $result
Write-Host ("G1 {0}: cs_errors={1} cs_warnings={2} import_errors={3} assembly_load_errors={4} exit={5} ({6}s)" -f `
    $result.status, $csErrors, $csWarnings, $importErrors, $loadErrors, $run.exit_code, $run.seconds) `
    -ForegroundColor $(if ($result.status -eq 'PASS') { 'Green' } else { 'Red' })

if ($result.status -eq 'PASS') { exit 0 } else { exit 1 }
