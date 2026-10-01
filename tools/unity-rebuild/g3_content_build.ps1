# G3 content gate: create the Addressables settings from a plan and build the content.
#
#   pwsh -File tools/unity-rebuild/g3_content_build.ps1 -Group prefabs
#
# Steps:
#   1. AddressablesSetup.Run  - apply out/unity/addressables_plan_<group>.json to the settings
#                               (adds/updates planned entries, prunes stale ones, skips exclusions)
#   2. AddressablesBuild.Run  - BuildPlayerContent into Library/com.unity.addressables/aa/<platform>
#
# The content is *not* copied into Assets/StreamingAssets/aa: com.unity.addressables'
# AddressablesPlayerBuildProcessor.PrepareForPlayerbuild -> GetStreamingAssetPaths() adds
# Addressables.BuildPath -> "aa" as an additional StreamingAssets path for every player build, so the
# Player picks the content up straight from Library.  What this gate therefore verifies is that
# Library/com.unity.addressables/aa/<platform> holds a catalog and bundles *after* the build: a failed
# build cleans that folder, and the next Player build would then ship a player with no content at all
# (observed: every address probe failing with InvalidKeyException).
#
# Each step's artifact is deleted before the run and required to be *fresh* afterwards: an artifact
# left over from an earlier round must never be mistaken for this round's result (the G2 gate once
# reported PASS from a stale file, so freshness is checked here from the start).
#
# Exit codes: 0 = setup and content build both succeeded and left a fresh, non-empty catalog
#             1 = FAIL (a step failed, or its artifact is stale/absent)
#             2 = BLOCKED (no Editor, no plan, no run at all)
#
# -Group names a plan file (out/unity/addressables_plan_<group>.json).  It used to be a ValidateSet of
# core/prefabs/all, which silently made the per-family plans unusable; the families are derived from the plans
# that exist instead, and a missing plan is already a BLOCKED outcome below.  Applying one family at a time is
# safe by construction: AddressablesSetup prunes only the groups the plan names (AddressablesSetup.cs:148-151).
param(
    [string]$Group = 'prefabs'
)

. "$PSScriptRoot\common.ps1"
Restore-ContentEvidence

$result = [ordered]@{
    gate           = 'G3_addressables_content_build'
    generated      = (Get-Date).ToUniversalTime().ToString('o')
    group          = $Group
    unity_exe      = $script:UnityExe
    status         = 'NOT_RUN'
    blocked_reason = $null
    plan           = $null
    setup          = $null
    build          = $null
    staging        = $null
}

if (-not $script:UnityExe) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = 'Unity Editor not found; set MANOSABA_UNITY_EDITOR'
    Write-Json (Join-Path $script:UnityDataRoot 'g3_content_build.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}

# --- 0. plan ------------------------------------------------------------------------
$planPath = Join-Path $script:UnityDataRoot "addressables_plan_$Group.json"
if (-not (Test-Path $planPath)) {
    $result.status = 'BLOCKED'
    $result.blocked_reason = "missing plan $planPath; run a13f_plan_addressables.py --group $Group"
    Write-Json (Join-Path $script:UnityDataRoot 'g3_content_build.json') $result
    Write-Host "BLOCKED: $($result.blocked_reason)" -ForegroundColor Red
    exit 2
}
$plan = Get-Content $planPath -Raw -Encoding UTF8 | ConvertFrom-Json
$result.plan = [ordered]@{
    path            = $planPath
    entry_count     = $plan.entry_count
    excluded_count  = $(if ($plan.PSObject.Properties.Name -contains 'excluded_count') { $plan.excluded_count } else { 0 })
    generated       = $plan.generated
}
Write-Host "plan: $($plan.entry_count) buildable entries, $($result.plan.excluded_count) excluded ($Group)" -ForegroundColor Cyan

$setupJsonPath = Join-Path $script:UnityDataRoot 'addressables_setup_result.json'
$buildJsonPath = Join-Path $script:UnityDataRoot 'addressables_build_result.json'
Remove-Item $setupJsonPath -ErrorAction SilentlyContinue
Remove-Item $buildJsonPath -ErrorAction SilentlyContinue

$env:MANOSABA_ADDRESSABLES_PLAN = "addressables_plan_$Group.json"
$env:MANOSABA_ADDRESSABLES_PRUNE = '1'      # the plan is authoritative for the groups it names
$env:MANOSABA_STAGE_STREAMINGASSETS = '0'   # the Player build adds Library aa itself (see header)

# --- 1. setup -----------------------------------------------------------------------
Write-Host "=== AddressablesSetup ($Group) ===" -ForegroundColor Cyan
$setupStarted = Get-Date
$setupLog = Join-Path $script:LogRoot "addressables_setup_$Group.log"
$setup = Invoke-Logged -FilePath $script:UnityExe -LogPath $setupLog -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.AddressablesSetup.Run',
    '-logFile', '-'
)
$setupJson = if (Test-Path $setupJsonPath) { Get-Content $setupJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$result.setup = [ordered]@{
    exit_code     = $setup.exit_code
    seconds       = $setup.seconds
    log           = $setup.log
    artifact      = $setupJsonPath
    fresh         = $(if ($setupJson -and (Get-Item $setupJsonPath).LastWriteTime -ge $setupStarted) { $true } else { $false })
    result        = $setupJson
}
if (-not $setupJson -or -not $result.setup.fresh -or $setup.exit_code -ne 0 -or $setupJson.status -notlike 'OK*' -or
    $setupJson.entries_missing_asset -gt 0 -or $setupJson.labels_unregistered -gt 0 -or $setupJson.entries_skipped -gt 0) {
    $result.status = 'FAIL'
    $result.blocked_reason = "AddressablesSetup did not succeed (exit=$($setup.exit_code), status=$(if ($setupJson) { $setupJson.status } else { 'no artifact' }), missing=$($setupJson.entries_missing_asset), labels_unregistered=$($setupJson.labels_unregistered), skipped=$($setupJson.entries_skipped))"
    Write-Json (Join-Path $script:UnityDataRoot 'g3_content_build.json') $result
    Write-Host "FAIL: $($result.blocked_reason); see $setupLog" -ForegroundColor Red
    exit 1
}
Write-Host ("  setup {0}: planned={1} added={2} excluded={3} pruned={4} missing={5}" -f `
    $setupJson.status, $setupJson.entries_planned, $setupJson.entries_added, `
    $setupJson.entries_excluded, $setupJson.entries_pruned, $setupJson.entries_missing_asset)

# --- 2. content build ---------------------------------------------------------------
Write-Host "=== AddressablesBuild ===" -ForegroundColor Cyan
$buildStarted = Get-Date
$buildLog = Join-Path $script:LogRoot "addressables_build_$Group.log"
$build = Invoke-Logged -FilePath $script:UnityExe -LogPath $buildLog -Arguments @(
    '-batchmode', '-nographics', '-quit',
    '-projectPath', $script:UnityProject,
    '-executeMethod', 'Manosaba.EditorTools.AddressablesBuild.Run',
    '-logFile', '-'
)
$buildJson = if (Test-Path $buildJsonPath) { Get-Content $buildJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
$buildFresh = $buildJson -and (Get-Item $buildJsonPath).LastWriteTime -ge $buildStarted
$result.build = [ordered]@{
    exit_code     = $build.exit_code
    seconds       = $build.seconds
    log           = $build.log
    artifact      = $buildJsonPath
    fresh         = [bool]$buildFresh
    result        = $buildJson
}
if (-not $buildJson -or -not $buildFresh -or $build.exit_code -ne 0 -or $buildJson.status -ne 'OK') {
    $result.status = 'FAIL'
    $result.blocked_reason = "content build did not succeed (exit=$($build.exit_code), status=$(if ($buildJson) { $buildJson.status } else { 'no artifact' }))"
    if ($buildJson -and $buildJson.error) { $result.blocked_reason += ": $($buildJson.error)" }
    Write-Json (Join-Path $script:UnityDataRoot 'g3_content_build.json') $result
    Write-Host "FAIL: $($result.blocked_reason); see $buildLog" -ForegroundColor Red
    exit 1
}
Write-Host ("  build OK: entries={0} locations={1} bundles={2} ({3} MB) catalogs={4} in {5}s" -f `
    $buildJson.entry_count, $buildJson.location_count, $buildJson.bundle_count, `
    [math]::Round($buildJson.bundle_bytes / 1MB, 1), $buildJson.catalogs.Count, $buildJson.duration_seconds)

# --- 3. verify what a Player build would ship --------------------------------------
# AddressablesPlayerBuildProcessor maps Addressables.BuildPath -> StreamingAssets/aa for the player,
# so this folder *is* the content the player will carry.  Check it on disk, not just in the report.
# Addressables.BuildPath is *relative* ("Library/com.unity.addressables/aa/<platform>"), so it must be
# resolved against the Unity project root -- Test-Path on the raw value silently finds nothing.
$buildRoot = if ($buildJson.build_root) { $buildJson.build_root } else { 'Library\com.unity.addressables\aa\Windows' }
if (-not [System.IO.Path]::IsPathRooted($buildRoot)) { $buildRoot = Join-Path $script:UnityProject $buildRoot }
$buildRoot = [System.IO.Path]::GetFullPath($buildRoot)
$windowsRoot = [System.IO.Path]::GetFullPath((Join-Path $script:UnityProject 'Library\com.unity.addressables\aa\Windows'))
$windowsBuild = $buildRoot.TrimEnd('\', '/') -eq $windowsRoot.TrimEnd('\', '/')
$contentFiles = @()
$catalogFiles = @()
$contentCatalog = $false
if (Test-Path $buildRoot) {
    $contentFiles = @(Get-ChildItem $buildRoot -Recurse -File)
    $catalogFiles = @($contentFiles | Where-Object { $_.Name -like 'catalog*' -and $_.Extension -in '.json', '.bin' -and $_.Length -gt 0 })
    # The fresh native build receipt must identify the same nonempty catalog
    # that the Windows Player build will read; a hash file alone is insufficient.
    foreach ($catalog in $catalogFiles) {
        foreach ($record in @($buildJson.catalogs)) {
            if (-not $record.path) { continue }
            $reportedPath = $record.path
            if (-not [System.IO.Path]::IsPathRooted($reportedPath)) { $reportedPath = Join-Path $script:UnityProject $reportedPath }
            $reportedPath = [System.IO.Path]::GetFullPath($reportedPath)
            if ($reportedPath -eq $catalog.FullName -and $record.bytes -eq $catalog.Length) { $contentCatalog = $true }
        }
    }
}
$contentBytes = ($contentFiles | Measure-Object -Property Length -Sum).Sum

# An older or other-platform catalog is useful diagnostic context only. It
# cannot satisfy the current Windows build's catalog requirement.
$catalogElsewhere = $null
if (-not $contentCatalog) {
    $aaRoot = Join-Path $script:UnityProject 'Library\com.unity.addressables'
    if (Test-Path $aaRoot) {
        $found = Get-ChildItem $aaRoot -Recurse -File -Filter 'catalog*' -ErrorAction SilentlyContinue | Select-Object -First 1
        if ($found) {
            $catalogElsewhere = $found.FullName
        }
    }
}
$leftoverStage = Join-Path $script:UnityProject 'Assets\StreamingAssets\aa'
$result.staging = [ordered]@{
    player_streaming_source         = $buildRoot
    player_streaming_source_files   = $contentFiles.Count
    player_streaming_source_bytes   = $contentBytes
    player_streaming_source_catalog = [bool]($contentFiles | Where-Object { $_.Name -like 'catalog*' })
    catalog_matches_build_receipt   = $contentCatalog
    expected_windows_build_root    = $windowsRoot
    windows_build_root             = $windowsBuild
    catalog_found_elsewhere         = $catalogElsewhere
    build_reports                   = [ordered]@{ files = $buildJson.staged_files; bytes = $buildJson.staged_bytes }
    leftover_assets_stage           = $(if (Test-Path $leftoverStage) { 'present (redundant copy; the player build adds Library aa itself)' } else { 'absent' })
}

$ok = $windowsBuild -and $contentFiles.Count -gt 0 -and $contentCatalog -and $buildJson.bundle_count -gt 0 -and $buildJson.location_count -gt 0
if (-not $windowsBuild) { $result.blocked_reason = "content build must target Windows at $windowsRoot; reported $buildRoot" }

# --- 4. guard against silent shrinkage ---------------------------------------------
# A content build does not fail when an asset's scripting assembly is unavailable: it simply leaves that
# asset out.  Round 12 hit exactly this -- while the genuine FastEnum.dll was being rejected by Unity, a
# G3 run produced a catalog with 4,296 locations instead of 4,304, silently dropping the eight
# Manosaba.Data ScriptableObjects, and only the Play Mode probe (G4) noticed ("No Location found for
# Key=General/Data/AuthorData").  So the location/entry counts are compared against the last healthy
# build and a drop is a FAIL, not a footnote.
$baselinePath = Join-Path $script:UnityDataRoot 'g3_baseline.json'
$baseline = if (Test-Path $baselinePath) { Get-Content $baselinePath -Raw -Encoding UTF8 | ConvertFrom-Json } else { $null }
# A *tiny* drop is not the failure this guard exists for: the original incident dropped eight assets and
# produced a clear signal, while a rebuild that legitimately loses a handful of locations to a planning change
# (measured: 33,220 -> 33,212 after the umbrella-label rebuild, -0.02%) must not be reported as a silent
# shrink.  The guard therefore fails only past a proportional tolerance, and always records the raw delta so a
# human can see it.  Anything at or above the baseline is, as before, simply healthy.
$shrinkTolerance = 0.005
$baselineLocations = if ($baseline) { [double]$baseline.location_count } else { 0 }
$locationDelta = if ($baseline) { $buildJson.location_count - $baseline.location_count } else { 0 }
$result.shrink_guard = [ordered]@{
    baseline_file        = $baselinePath
    baseline_locations   = if ($baseline) { $baseline.location_count } else { $null }
    baseline_entries     = if ($baseline) { $baseline.entry_count } else { $null }
    locations            = $buildJson.location_count
    entries              = $buildJson.entry_count
    location_delta       = $locationDelta
    tolerance_fraction   = $shrinkTolerance
    tolerance_locations  = [math]::Floor($baselineLocations * $shrinkTolerance)
    shrank               = $false
}
if ($ok -and $baseline -and $locationDelta -lt 0 -and ([math]::Abs($locationDelta) -gt $result.shrink_guard.tolerance_locations)) {
    $result.shrink_guard.shrank = $true
    $ok = $false
    $result.blocked_reason = ("content silently shrank: {0} locations, baseline {1} (delta {2}, tolerance {3}) -- an asset was " +
                              "dropped by the build (check for an assembly that Unity refused to load; see g1_compile.json " +
                              "assembly_load_errors)") -f `
                             $buildJson.location_count, $baseline.location_count, $locationDelta,
                             $result.shrink_guard.tolerance_locations
}
if ($ok -and (-not $baseline -or $buildJson.location_count -ge $baseline.location_count)) {
    $baselineDoc = [ordered]@{
        generated      = (Get-Date).ToUniversalTime().ToString('o')
        group          = $Group
        entry_count    = $buildJson.entry_count
        location_count = $buildJson.location_count
        bundle_count   = $buildJson.bundle_count
    }
    Write-Json $baselinePath $baselineDoc
}$result.status = if ($ok) { 'PASS' } else { 'FAIL' }
# The shrink guard sets its own reason above; only fall back to the "no catalog" text when nothing more specific was recorded
if (-not $ok -and -not $result.blocked_reason) { $result.blocked_reason = "content built but $buildRoot is empty or has no catalog, so the next Player build would ship no content" }

$gatePath = Join-Path $script:UnityDataRoot 'g3_content_build.json'
Write-Json $gatePath $result
Write-Host ("G3 {0}: entries={1} locations={2} bundles={3} player-content={4} files ({5} MB) -> {6}" -f `
    $result.status, $buildJson.entry_count, $buildJson.location_count, $buildJson.bundle_count, `
    $contentFiles.Count, [math]::Round($contentBytes / 1MB, 1), $gatePath) `
    -ForegroundColor $(if ($result.status -eq 'PASS') { 'Green' } else { 'Red' })

if ($result.status -eq 'PASS') { exit 0 } else { exit 1 }
