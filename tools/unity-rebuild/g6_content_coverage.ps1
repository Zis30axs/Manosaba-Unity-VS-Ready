# G6 content-coverage gate: does this project provide what the shipped build contains?
#
#   pwsh -File tools/unity-rebuild/g6_content_coverage.ps1
#
# Unlike G1-G5 this gate needs no Unity: it is a static comparison of
#   * the shipped Addressables catalog (what the game's own code can ask for), and
#   * this project's plan, its Addressables groups on disk, and the assets themselves.
#
# Status semantics follow the audit's rule for G6 - "all content verified, unexecuted = NOT_RUN, missing inputs =
# BLOCKED, never generalise a sample":
#   COVERED  every planned family is configured for the build, has every planned asset on disk, and every
#            address matches a shipped one
#   PARTIAL  at least one family is planned but not built (its assets exist in the project yet the player cannot
#            load them by address), or assets/addresses are missing - the per-family reasons are in the artifact
#   BLOCKED  the inputs the check needs are absent
# Runtime-played coverage is reported separately as NOT_RUN until a Play Mode or Player run states which scripts
# it actually loaded; a partial playthrough is never reported as full coverage.
#
# Exit codes: 0 = COVERED; 1 = PARTIAL; 2 = BLOCKED
param()

. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate           = 'G6_content_coverage'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    status         = 'NOT_RUN'
    blocked_reason = $null
    coverage       = $null
}

$checker = Join-Path $PSScriptRoot 'check_content_coverage.py'
$python = if ($env:MANOSABA_PYTHON) { $env:MANOSABA_PYTHON }
          else { Join-Path $script:RepoRoot 'tools\venv\Scripts\python.exe' }
$artifact = Join-Path $script:UnityDataRoot 'g6_content_coverage.json'

if (-not (Test-Path $checker)) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "checker missing: $checker"
} elseif (-not (Test-Path $python)) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "python interpreter missing: $python (set MANOSABA_PYTHON)"
} else {
    Write-Host "=== G6 content coverage (static: plan vs shipped catalog vs disk) ===" -ForegroundColor Cyan
    $output = & $python $checker --write 2>&1
    $output | ForEach-Object { Write-Host "  $_" }
    $exit = $LASTEXITCODE
    $result.coverage = if (Test-Path $artifact) { Get-Content $artifact -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
    $result.status = switch ($exit) { 0 { 'PASS' } 1 { 'PARTIAL' } default { 'BLOCKED' } }
    if ($exit -eq 2) { $result.blocked_reason = 'coverage inputs missing (see output)' }
}

Write-Json $artifact $result
$colour = switch ($result.status) { 'PASS' { 'Green' } 'PARTIAL' { 'Yellow' } default { 'Red' } }
Write-Host ("G6 {0}" -f $result.status) -ForegroundColor $colour
if ($result.coverage) {
    foreach ($family in $result.coverage.families.PSObject.Properties) {
        Write-Host ("  {0,-22} {1,-13} planned={2,-6} on_disk={3}" -f `
            $family.Name, $family.Value.status, $family.Value.planned_entries, $family.Value.configured_entries_on_disk)
    }
    Write-Host ("  scenarios: {0}/{1} converted, {2} path fields match" -f `
        $result.coverage.scenarios.converted, $result.coverage.scenarios.planned, `
        $result.coverage.scenarios.path_field_matches_address)
    Write-Host ("  runtime-played: {0}" -f $result.coverage.runtime_played.status)
}

switch ($result.status) {
    'PASS'    { exit 0 }
    'PARTIAL' { exit 1 }
    default   { exit 2 }
}
