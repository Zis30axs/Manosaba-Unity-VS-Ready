# G2 gate: verify that the files a13_import_assets.py wrote are real Unity assets, and that their
# GUIDs survive closing and reopening the Editor.
#
#   pwsh -File tools/unity-rebuild/g2_import_gate.ps1
#
# Steps:
#   1. Unity run A  - ImportVerifier.Run (refresh + resolve every requested asset path)
#   2. Unity run B  - the same, in a *fresh* Editor process (the close/reopen requirement)
#   3. compare the two GUID maps: identical GUIDs == the import settled
#
# Exit codes: 0 = every requested asset resolved and GUIDs are stable
#             1 = missing assets or unstable GUIDs
#             2 = BLOCKED (no Editor / no input list)
. "$PSScriptRoot\common.ps1"
Restore-ContentEvidence

$result = [ordered]@{
    gate           = 'G2_asset_import'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    unity_exe      = $script:UnityExe
    status         = 'NOT_RUN'
    blocked_reason = $null
    run_a          = $null
    run_b          = $null
    guid_stability = $null
}

if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g2_import.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

$pathsFile = Join-Path $script:UnityDataRoot 'a13_imported_paths.txt'
if (-not (Test-Path $pathsFile)) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "missing $pathsFile (run tools/unity-rebuild/a13_import_assets.py first)"
    Write-Json (Join-Path $script:UnityDataRoot 'g2_import.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}
$requested = @(Get-Content -LiteralPath $pathsFile -Encoding UTF8 | Where-Object { $_.Trim() }).Count
if ($requested -eq 0) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "empty $pathsFile (run tools/unity-rebuild/a13_import_assets.py first)"
    Write-Json (Join-Path $script:UnityDataRoot 'g2_import.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}
Write-Host "[g2] leaf input paths: $requested (Native verifier includes composite lists)"

function Test-VerifyReceipt($verify) {
    $errors = [System.Collections.Generic.List[string]]::new()
    # The Native verifier de-duplicates its union using ordinal string identity.
    $map = [System.Collections.Generic.Dictionary[string,string]]::new([System.StringComparer]::Ordinal)
    if (-not $verify) {
        $errors.Add('missing Native receipt')
    } else {
        $requestedInteger = $verify.requested -is [int] -or $verify.requested -is [long]
        $foundInteger = $verify.found -is [int] -or $verify.found -is [long]
        $missingInteger = $verify.missing_count -is [int] -or $verify.missing_count -is [long]
        if (-not $requestedInteger -or $verify.requested -le 0) { $errors.Add('requested must be a positive integer') }
        if (-not $foundInteger -or -not $requestedInteger -or $verify.found -ne $verify.requested) { $errors.Add('found must equal requested') }
        if (-not $missingInteger -or $verify.missing_count -ne 0) { $errors.Add('missing_count must be zero') }
        $keys = $verify.guid_map.keys
        $values = $verify.guid_map.values
        if (-not $requestedInteger -or -not ($keys -is [array]) -or -not ($values -is [array]) -or
            $keys.Count -ne $values.Count -or $keys.Count -ne $verify.requested) {
            $errors.Add('GUID map arrays must cover every requested asset')
        } else {
            for ($i = 0; $i -lt $keys.Count; $i++) {
                $key = $keys[$i]
                $guid = $values[$i]
                if (-not ($key -is [string]) -or [string]::IsNullOrWhiteSpace($key) -or $map.ContainsKey($key)) {
                    $errors.Add('GUID map contains an empty or duplicate asset path')
                } elseif (-not ($guid -is [string]) -or $guid -notmatch '\A[0-9a-fA-F]{32}\z' -or $guid -eq ('0' * 32)) {
                    $errors.Add('GUID map contains an invalid asset GUID')
                } else {
                    $map.Add($key, $guid)
                }
            }
        }
    }
    return [ordered]@{ complete = $errors.Count -eq 0; errors = @($errors.ToArray()); map = $map }
}

function Invoke-VerifyRun([string]$tag) {
    $log = Join-Path $script:LogRoot "import_verify_$tag.log"
    $json = Join-Path $script:UnityDataRoot 'import_verify.json'
    # A run that dies before executing the method (compile error, crash) leaves the previous run's
    # JSON in place.  Removing it first, then checking exit code + freshness, is what stops a stale
    # artifact from being reported as a pass -- observed 2026-09-17: AudioImportPolicy.cs failed to
    # compile (CS0619), Unity aborted, and the gate happily re-read the earlier successful JSON.
    Remove-Item $json -ErrorAction SilentlyContinue
    $started = Get-Date
    $run = Invoke-Logged -FilePath $script:UnityExe -LogPath $log -Arguments @(
        '-batchmode', '-nographics', '-quit',
        '-projectPath', $script:UnityProject,
        '-executeMethod', 'Manosaba.EditorTools.ImportVerifier.Run',
        '-logFile', '-'
    )
    $logText = if (Test-Path $log) { Get-Content $log -Raw -ErrorAction SilentlyContinue } else { '' }
    $csErrors = ([regex]::Matches($logText, 'error CS\d+')).Count
    $aborted = $logText -match 'Aborting batchmode due to failure'

    $parsed = $null
    $fresh = $false
    if (Test-Path $json) {
        $parsed = Get-Content $json -Raw -Encoding UTF8 | ConvertFrom-Json
        $fresh = (Get-Item $json).LastWriteTime -ge $started
        Copy-Item $json (Join-Path $script:UnityDataRoot "import_verify_$tag.json") -Force
    }

    return [ordered]@{
        exit_code     = $run.exit_code
        seconds       = $run.seconds
        log           = $run.log
        cs_errors     = $csErrors
        aborted       = $aborted
        artifact_fresh = $fresh
        verify        = $parsed
    }
}

Write-Host "=== Unity run A (import + resolve) ===" -ForegroundColor Cyan
$result.run_a = Invoke-VerifyRun 'a'
Write-Host "  A: status=$($result.run_a.verify.status) found=$($result.run_a.verify.found)/$($result.run_a.verify.requested) missing=$($result.run_a.verify.missing_count)"

Write-Host "=== Unity run B (fresh Editor, GUID stability) ===" -ForegroundColor Cyan
$result.run_b = Invoke-VerifyRun 'b'
Write-Host "  B: status=$($result.run_b.verify.status) found=$($result.run_b.verify.found)/$($result.run_b.verify.requested) missing=$($result.run_b.verify.missing_count)"

# --- compare GUID maps --------------------------------------------------------------
$validationA = Test-VerifyReceipt $result.run_a.verify
$validationB = Test-VerifyReceipt $result.run_b.verify
$result.run_a.receipt_complete = $validationA.complete
$result.run_b.receipt_complete = $validationB.complete
$result.run_a.receipt_errors = $validationA.errors
$result.run_b.receipt_errors = $validationB.errors
$mapA = $validationA.map
$mapB = $validationB.map
$changed = @()
foreach ($k in $mapA.Keys) {
    if (-not $mapB.ContainsKey($k)) { $changed += "$k (missing in run B)" }
    elseif ($mapB[$k] -ne $mapA[$k]) { $changed += "$k ($($mapA[$k]) -> $($mapB[$k]))" }
}
$result.guid_stability = [ordered]@{
    assets_run_a   = $mapA.Count
    assets_run_b   = $mapB.Count
    changed_guids  = $changed.Count
    changed_sample = $changed | Select-Object -First 20
}

$okA = $result.run_a.verify -and $result.run_a.verify.status -eq 'OK' -and $result.run_a.artifact_fresh -and
       $result.run_a.exit_code -eq 0 -and $result.run_a.cs_errors -eq 0 -and -not $result.run_a.aborted -and $validationA.complete
$okB = $result.run_b.verify -and $result.run_b.verify.status -eq 'OK' -and $result.run_b.artifact_fresh -and
       $result.run_b.exit_code -eq 0 -and $result.run_b.cs_errors -eq 0 -and -not $result.run_b.aborted -and $validationB.complete

# Both runs must have verified the same input list: if the list changed between them (e.g. a13 was
# re-run in between), the GUID comparison would be meaningless even though both runs "passed".
$sameInput = $validationA.complete -and $validationB.complete -and
             $result.run_a.verify.input_sha256 -and
             ($result.run_a.verify.input_sha256 -eq $result.run_b.verify.input_sha256) -and
             ($result.run_a.verify.requested -eq $result.run_b.verify.requested)
$result.input_list = [ordered]@{
    run_a_count = if ($result.run_a.verify) { $result.run_a.verify.requested } else { 0 }
    run_b_count = if ($result.run_b.verify) { $result.run_b.verify.requested } else { 0 }
    run_a_sha256 = if ($result.run_a.verify) { $result.run_a.verify.input_sha256 } else { $null }
    run_b_sha256 = if ($result.run_b.verify) { $result.run_b.verify.input_sha256 } else { $null }
    identical    = $sameInput
}

$stable = $changed.Count -eq 0

if (-not ($okA -and $okB)) {
    $result.status = 'FAIL'
    $reasons = @()
    foreach ($pair in @(@('A', $result.run_a), @('B', $result.run_b))) {
        $tag = $pair[0]; $r = $pair[1]
        if ($r.cs_errors -gt 0) { $reasons += "run $tag had $($r.cs_errors) compiler errors" }
        if ($r.exit_code -ne 0) { $reasons += "run $tag exited $($r.exit_code)" }
        if ($r.aborted) { $reasons += "run $tag aborted before executing the verifier" }
        if (-not $r.artifact_fresh) { $reasons += "run $tag produced no fresh verification artifact" }
        elseif ($r.verify.status -ne 'OK') { $reasons += "run $tag verify status=$($r.verify.status)" }
        if (-not $r.receipt_complete) { $reasons += "run ${tag}: $($r.receipt_errors -join '; ')" }
    }
    $result.blocked_reason = ($reasons -join '; ')
} elseif (-not $sameInput) {
    $result.status = 'FAIL'
    $result.blocked_reason = 'the two runs verified different input lists; the GUID comparison is not meaningful'
} elseif (-not $stable) {
    $result.status = 'FAIL'
    $result.blocked_reason = "GUIDs changed across reopen: $($changed.Count)"
} else {
    $result.status = 'PASS'
}

Write-Json (Join-Path $script:UnityDataRoot 'g2_import.json') $result
Write-Host ("G2 {0}: found={1}/{2} missing={3} guids_a={4} guids_b={5} changed={6}" -f `
    $result.status, $result.run_b.verify.found, $result.run_b.verify.requested, $result.run_b.verify.missing_count,
    $mapA.Count, $mapB.Count, $changed.Count) `
    -ForegroundColor $(if ($result.status -eq 'PASS') { 'Green' } else { 'Red' })

if ($result.status -eq 'PASS') { exit 0 } else { exit 1 }
