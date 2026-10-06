# G3 Android content gate: build the reconstruction's Addressables content for Android.
#
#   pwsh -File tools/unity-rebuild/g3_android_content_build.ps1 -Group all
#
# This is intentionally a separate gate from g3_content_build.ps1.  The existing G3
# contract remains Windows-only; this gate starts Unity with -buildTarget Android and
# requires Library/com.unity.addressables/aa/Android.  It uses the project's Default
# Addressables profile (the local StreamingAssets path), never the AndroidPatch remote
# cache profile.  The Android build module must be installed in the selected Unity
# Editor before this gate can pass.
#
# Exit codes: 0 = PASS; 1 = FAIL (fresh build/receipt or shrink check failed);
#             2 = BLOCKED (missing Unity, plan, or Default profile).

param(
    [string]$Group = 'all'
)

. "$PSScriptRoot/common.ps1"
$gatePath = Join-Path $script:UnityDataRoot 'g3_android_content_build.json'
$restoreError = $null
try { Restore-ContentEvidence } catch { $restoreError = $_.Exception.Message }

$planPath = Join-Path $script:UnityDataRoot "addressables_plan_$Group.json"
$setupJsonPath = Join-Path $script:UnityDataRoot 'addressables_setup_result.json'
$buildJsonPath = Join-Path $script:UnityDataRoot 'addressables_build_result.json'
$settingsPath = Join-Path $script:UnityProject 'Assets\AddressableAssetsData\AddressableAssetSettings.asset'
$expectedBuildRoot = [System.IO.Path]::GetFullPath((Join-Path $script:UnityProject 'Library\com.unity.addressables\aa\Android'))
$baselinePath = Join-Path $script:UnityDataRoot ("g3_android_baseline_{0}.json" -f $Group)
# Remove prior gate/build receipts before any preflight so an interrupted run cannot
# leave an older PASS available to callers as if it belonged to this invocation.
Remove-Item -LiteralPath $gatePath, $setupJsonPath, $buildJsonPath -Force -ErrorAction SilentlyContinue

$result = [ordered]@{
    gate                  = 'G3_android_addressables_content_build'
    generated             = (Get-Date).ToUniversalTime().ToString('o')
    group                 = $Group
    target                = 'Android'
    profile_requested     = 'Default'
    unity_exe             = $script:UnityExe
    android_player_module = $null
    status                = 'NOT_RUN'
    blocked_reason        = $null
    plan                  = $null
    profile               = $null
    setup                 = $null
    build                 = $null
    staging               = $null
    shrink_guard          = $null
}

function Write-Gate {
    param([string]$Status, [string]$Reason)
    $result.status = $Status
    if ($Reason) { $result.blocked_reason = $Reason }
    Write-Json $gatePath $result
    $colour = if ($Status -eq 'PASS') { 'Green' } elseif ($Status -like 'BLOCKED*') { 'Yellow' } else { 'Red' }
    Write-Host ("G3 Android {0}: {1}" -f $Status, $gatePath) -ForegroundColor $colour
    if ($Status -eq 'PASS') { exit 0 }
    if ($Status -like 'BLOCKED*') { exit 2 }
    exit 1
}

function Read-DefaultProfileState {
    if (-not (Test-Path -LiteralPath $settingsPath -PathType Leaf)) {
        return [ordered]@{
            path = $settingsPath; active_id = $null; default_id = $null; active_name = $null
            local_build_path = $null; local_load_path = $null; local_paths_verified = $false
            verified = $false; error = 'settings asset missing'
        }
    }
    $text = Get-Content -LiteralPath $settingsPath -Raw -Encoding UTF8
    $defaultMatch = [regex]::Match($text, '(?ms)m_Id:\s*([0-9a-f]{32})\s*\r?\n\s+m_ProfileName:\s*Default\b')
    $activeMatch = [regex]::Match($text, '(?m)^\s*m_ActiveProfileId:\s*([0-9a-f]{32})\s*$')
    $defaultId = if ($defaultMatch.Success) { $defaultMatch.Groups[1].Value } else { $null }
    $activeId = if ($activeMatch.Success) { $activeMatch.Groups[1].Value } else { $null }
    $activeName = $null
    if ($activeId -and $defaultId -and $activeId -eq $defaultId) { $activeName = 'Default' }
    elseif ($activeId) {
        $nameMatch = [regex]::Match($text, "(?ms)m_Id:\s*$([regex]::Escape($activeId))\s*\r?\n\s+m_ProfileName:\s*([^\r\n]+)")
        if ($nameMatch.Success) { $activeName = $nameMatch.Groups[1].Value.Trim() }
    }

    # Verify the tracked Default profile's local variables without requiring it to be
    # active in the serialized settings.  AddressablesBuild.RunAndroid temporarily
    # selects Default and restores the prior profile after building.
    $defaultBlock = if ($defaultMatch.Success) {
        $nextProfile = [regex]::Match($text.Substring($defaultMatch.Index + $defaultMatch.Length),
            '(?ms)^\s*- m_InheritedParent:')
        if ($nextProfile.Success) {
            $text.Substring($defaultMatch.Index, $defaultMatch.Length + $nextProfile.Index)
        } else {
            $text.Substring($defaultMatch.Index)
        }
    } else { '' }
    $entryNames = @{}
    foreach ($entry in [regex]::Matches($text, '(?ms)- m_Id:\s*([0-9a-f]{32})\s*\r?\n\s+m_Name:\s*([^\r\n]+)')) {
        $entryNames[$entry.Groups[2].Value.Trim()] = $entry.Groups[1].Value
    }
    $localBuildId = if ($entryNames.ContainsKey('Local.BuildPath')) { $entryNames['Local.BuildPath'] } else { $null }
    $localLoadId = if ($entryNames.ContainsKey('Local.LoadPath')) { $entryNames['Local.LoadPath'] } else { $null }
    function Get-ProfileValue([string]$Block, [string]$Id) {
        if (-not $Block -or -not $Id) { return $null }
        $m = [regex]::Match($Block, "(?ms)- m_Id:\s*$([regex]::Escape($Id))\s*\r?\n\s+m_Value:\s*([^\r\n]+)")
        if (-not $m.Success) { return $null }
        $value = $m.Groups[1].Value.Trim()
        if ($value.Length -ge 2 -and $value.StartsWith("'") -and $value.EndsWith("'")) {
            $value = $value.Substring(1, $value.Length - 2)
        }
        return $value
    }
    $localBuildPath = Get-ProfileValue $defaultBlock $localBuildId
    $localLoadPath = Get-ProfileValue $defaultBlock $localLoadId
    $localPathsVerified = $localBuildPath -eq '[UnityEngine.AddressableAssets.Addressables.BuildPath]/[BuildTarget]' -and
        $localLoadPath -eq '{UnityEngine.AddressableAssets.Addressables.RuntimePath}/[BuildTarget]'
    return [ordered]@{
        path = $settingsPath; active_id = $activeId; default_id = $defaultId
        active_name = $activeName; local_build_path = $localBuildPath; local_load_path = $localLoadPath
        local_paths_verified = [bool]$localPathsVerified
        verified = [bool]($defaultId -and $localPathsVerified)
        error = $null
    }
}

function Unity-Arguments {
    param([string]$Method)
    return @('-batchmode', '-nographics', '-quit', '-buildTarget', 'Android',
        '-projectPath', $script:UnityProject, '-executeMethod', $Method, '-logFile', '-')
}

if ($restoreError) {
    Write-Gate 'BLOCKED_IMPORT_EVIDENCE' "Content evidence restore failed: $restoreError"
}

if (-not $script:UnityExe) {
    Write-Gate 'BLOCKED_NO_UNITY' 'Unity Editor not found; set MANOSABA_UNITY_EDITOR.'
}
$editorRoot = Split-Path $script:UnityExe -Parent
$androidPlayerRoot = Join-Path $editorRoot 'Data\PlaybackEngines\AndroidPlayer'
$result.android_player_module = [ordered]@{
    path = $androidPlayerRoot
    present = Test-Path -LiteralPath $androidPlayerRoot -PathType Container
}
if (-not $result.android_player_module.present) {
    Write-Gate 'BLOCKED_NO_ANDROID_MODULE' "Unity Android Build Support is not installed at $androidPlayerRoot; install the matching Android module and re-run."
}

try {
    $profileState = Read-DefaultProfileState
} catch {
    Write-Gate 'BLOCKED_PROFILE_READ' ("Could not read Addressables profile configuration: " + $_.Exception.Message)
}
$result.profile = $profileState
if (-not $profileState.verified) {
    Write-Gate 'BLOCKED_WRONG_PROFILE' ("Android content gate requires tracked Default profile with local paths; active='{0}' id='{1}' default_id='{2}' local_build='{3}' local_load='{4}'." -f `
        $profileState.active_name, $profileState.active_id, $profileState.default_id,
        $profileState.local_build_path, $profileState.local_load_path)
}

if (-not (Test-Path -LiteralPath $planPath -PathType Leaf)) {
    Write-Gate 'BLOCKED_NO_PLAN' "missing $planPath; run a13f_plan_addressables.py --group $Group."
}
try {
    $plan = Get-Content -LiteralPath $planPath -Raw -Encoding UTF8 | ConvertFrom-Json
} catch {
    Write-Gate 'BLOCKED_BAD_PLAN' ("Addressables plan is not valid JSON: " + $_.Exception.Message)
}
$planRows = @($plan.entries)
$buildableRows = @($planRows | Where-Object { $_.status -ne 'excluded' })
$excludedRows = @($planRows | Where-Object { $_.status -eq 'excluded' })
$planEntryCount = [int]$plan.entry_count
$planRowsMatch = $planEntryCount -eq $buildableRows.Count
$planIncludingCount = if ($plan.PSObject.Properties.Name -contains 'entry_count_including_excluded') {
    [int]$plan.entry_count_including_excluded
} else { $planRows.Count }
$planFullMatch = $planIncludingCount -eq $planRows.Count -and $planRowsMatch
$planAllValid = $planFullMatch -and $planEntryCount -gt 0 -and ($Group -ne 'all' -or $excludedRows.Count -eq 0)
$result.plan = [ordered]@{
    path = $planPath
    entry_count = $planEntryCount
    entry_rows = $planRows.Count
    buildable_rows = $buildableRows.Count
    entry_count_including_excluded = $planIncludingCount
    excluded_count = $excludedRows.Count
    full_entry_count_match = [bool]$planFullMatch
    counts_verified = [bool]$planAllValid
    generated = $plan.generated
}
if (-not $planAllValid) {
    Write-Gate 'BLOCKED_PLAN_MISMATCH' ("Addressables plan counts are inconsistent: declared=$planEntryCount buildable=$($buildableRows.Count) rows=$($planRows.Count) including_excluded=$planIncludingCount excluded=$($excludedRows.Count) group=$Group.")
}
Write-Host "plan: $($plan.entry_count) buildable entries, $($result.plan.excluded_count) excluded ($Group)" -ForegroundColor Cyan

# Each Unity invocation writes a fixed receipt.  Removing both first prevents an old
# Windows receipt from being accepted as evidence for this Android run.
Remove-Item -LiteralPath $setupJsonPath, $buildJsonPath -Force -ErrorAction SilentlyContinue
$env:MANOSABA_ADDRESSABLES_PLAN = "addressables_plan_$Group.json"
$env:MANOSABA_ADDRESSABLES_PRUNE = '1'
$env:MANOSABA_STAGE_STREAMINGASSETS = '0'

Write-Host "=== AddressablesSetup Android ($Group) ===" -ForegroundColor Cyan
$setupStarted = Get-Date
$setupLog = Join-Path $script:LogRoot "addressables_setup_android_$Group.log"
$setup = Invoke-Logged -FilePath $script:UnityExe -LogPath $setupLog -Arguments (Unity-Arguments 'Manosaba.EditorTools.AddressablesSetup.Run')
$setupJson = $null
if (Test-Path -LiteralPath $setupJsonPath -PathType Leaf) {
    try { $setupJson = Get-Content $setupJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json }
    catch { Write-Gate 'FAIL_SETUP' ("AddressablesSetup receipt is not valid JSON: " + $_.Exception.Message) }
}
$setupFresh = $setupJson -and ((Get-Item -LiteralPath $setupJsonPath).LastWriteTime -ge $setupStarted)
$result.setup = [ordered]@{
    exit_code = $setup.exit_code; seconds = $setup.seconds; log = $setup.log
    artifact = $setupJsonPath; fresh = [bool]$setupFresh; result = $setupJson
}
if (-not $setupJson -or -not $setupFresh -or $setup.exit_code -ne 0 -or $setupJson.status -notlike 'OK*' -or
    $setupJson.entries_missing_asset -gt 0 -or $setupJson.labels_unregistered -gt 0 -or $setupJson.entries_skipped -gt 0) {
    Write-Gate 'FAIL_SETUP' ("AddressablesSetup did not succeed (exit=$($setup.exit_code), status=$(if ($setupJson) { $setupJson.status } else { 'no artifact' }), missing=$($setupJson.entries_missing_asset), labels_unregistered=$($setupJson.labels_unregistered), skipped=$($setupJson.entries_skipped)); see $setupLog")
}

Write-Host "=== AddressablesBuild Android ===" -ForegroundColor Cyan
$buildStarted = Get-Date
$buildLog = Join-Path $script:LogRoot "addressables_build_android_$Group.log"
$build = Invoke-Logged -FilePath $script:UnityExe -LogPath $buildLog -Arguments (Unity-Arguments 'Manosaba.EditorTools.AddressablesBuild.RunAndroid')
$buildJson = $null
if (Test-Path -LiteralPath $buildJsonPath -PathType Leaf) {
    try { $buildJson = Get-Content $buildJsonPath -Raw -Encoding UTF8 | ConvertFrom-Json }
    catch { Write-Gate 'FAIL_BUILD' ("AddressablesBuild receipt is not valid JSON: " + $_.Exception.Message) }
}
$buildFresh = $buildJson -and ((Get-Item -LiteralPath $buildJsonPath).LastWriteTime -ge $buildStarted)
$result.build = [ordered]@{
    exit_code = $build.exit_code; seconds = $build.seconds; log = $build.log
    artifact = $buildJsonPath; fresh = [bool]$buildFresh; result = $buildJson
}
if (-not $buildJson -or -not $buildFresh -or $build.exit_code -ne 0 -or $buildJson.status -ne 'OK') {
    $reason = "AddressablesBuild did not succeed (exit=$($build.exit_code), status=$(if ($buildJson) { $buildJson.status } else { 'no artifact' })); see $buildLog"
    if ($buildJson -and $buildJson.error) { $reason += ": $($buildJson.error)" }
    Write-Gate 'FAIL_BUILD' $reason
}
$offlineGroups = @($buildJson.offline_groups)
$buildTargetEvidence = $buildJson.active_build_target -eq 'Android' -and
    $buildJson.profile -eq 'Default' -and [bool]$buildJson.offline_groups_verified -and
    $offlineGroups.Count -gt 0
if (-not $buildTargetEvidence) {
    Write-Gate 'BLOCKED_WRONG_BUILD_RECEIPT' 'Android Addressables receipt is missing active_build_target=Android, profile=Default, or verified local/offline group evidence.'
}

$buildRootValue = if ($buildJson.build_root) { [string]$buildJson.build_root } else { 'Library/com.unity.addressables/aa/Android' }
$buildRoot = if ([System.IO.Path]::IsPathRooted($buildRootValue)) { [System.IO.Path]::GetFullPath($buildRootValue) } else { [System.IO.Path]::GetFullPath((Join-Path $script:UnityProject $buildRootValue)) }
$androidBuild = $buildRoot.TrimEnd('\', '/') -eq $expectedBuildRoot.TrimEnd('\', '/')
$contentFiles = if (Test-Path -LiteralPath $buildRoot -PathType Container) { @(Get-ChildItem -LiteralPath $buildRoot -Recurse -File) } else { @() }
$catalogFiles = @($contentFiles | Where-Object { $_.Name -like 'catalog*' -and $_.Extension -in '.json', '.bin' -and $_.Length -gt 0 })
$bundleFiles = @($contentFiles | Where-Object { $_.Extension -eq '.bundle' -and $_.Length -gt 0 })
$contentCatalog = $false
$catalogFresh = $false
foreach ($catalog in $catalogFiles) {
    foreach ($record in @($buildJson.catalogs)) {
        if (-not $record.path) { continue }
        $reportedPath = [string]$record.path
        if (-not [System.IO.Path]::IsPathRooted($reportedPath)) { $reportedPath = Join-Path $script:UnityProject $reportedPath }
        if ([System.IO.Path]::GetFullPath($reportedPath) -eq $catalog.FullName -and [int64]$record.bytes -eq $catalog.Length) {
            $contentCatalog = $true
            if ($catalog.LastWriteTime -ge $buildStarted) { $catalogFresh = $true }
        }
    }
}
$catalogBytes = ($catalogFiles | Measure-Object -Property Length -Sum).Sum
$contentBytes = ($contentFiles | Measure-Object -Property Length -Sum).Sum
$bundleBytes = ($bundleFiles | Measure-Object -Property Length -Sum).Sum
$bundleCountMatchesReceipt = $bundleFiles.Count -eq [int]$buildJson.bundle_count
$bundleBytesMatchesReceipt = $bundleBytes -eq [int64]$buildJson.bundle_bytes
$result.staging = [ordered]@{
    player_streaming_source = $buildRoot
    expected_android_build_root = $expectedBuildRoot
    android_build_root = $androidBuild
    files = $contentFiles.Count
    bytes = $contentBytes
    catalogs = $catalogFiles.Count
    catalog_bytes = $catalogBytes
    catalog_fresh = $catalogFresh
    bundles = $bundleFiles.Count
    bundle_bytes = $bundleBytes
    bundle_count_matches_build_receipt = $bundleCountMatchesReceipt
    bundle_bytes_matches_build_receipt = $bundleBytesMatchesReceipt
    catalog_matches_build_receipt = $contentCatalog
    build_reports = [ordered]@{
        entry_count = $buildJson.entry_count; location_count = $buildJson.location_count
        bundle_count = $buildJson.bundle_count; bundle_bytes = $buildJson.bundle_bytes
        staged_files = $buildJson.staged_files; staged_bytes = $buildJson.staged_bytes
        active_build_target = $buildJson.active_build_target; profile = $buildJson.profile
        offline_groups_verified = $buildJson.offline_groups_verified; offline_groups = $offlineGroups.Count
    }
}

$ok = $androidBuild -and $contentFiles.Count -gt 0 -and $catalogFiles.Count -gt 0 -and $contentCatalog -and $catalogFresh -and
    [int]$buildJson.bundle_count -gt 0 -and [int]$buildJson.location_count -gt 0 -and
    $bundleCountMatchesReceipt -and $bundleBytesMatchesReceipt
if (-not $androidBuild) { $result.blocked_reason = "content build must target Android at $expectedBuildRoot; reported $buildRoot" }
elseif (-not $contentCatalog) { $result.blocked_reason = 'Android content folder has no nonempty catalog matching the fresh build receipt.' }
elseif (-not $catalogFresh) { $result.blocked_reason = 'Android catalog does not have a fresh write time from this content build.' }
elseif (-not $bundleCountMatchesReceipt -or -not $bundleBytesMatchesReceipt) {
    $result.blocked_reason = "Android bundle files do not match the fresh build receipt (disk=$($bundleFiles.Count)/$bundleBytes receipt=$($buildJson.bundle_count)/$($buildJson.bundle_bytes))."
}

# Compare against an Android-specific healthy baseline.  A Windows baseline is never
# reused because platform-specific packing can legitimately produce different counts.
$baseline = $null
if (Test-Path -LiteralPath $baselinePath -PathType Leaf) {
    try { $baseline = Get-Content $baselinePath -Raw -Encoding UTF8 | ConvertFrom-Json }
    catch { Write-Gate 'BLOCKED_BAD_BASELINE' ("Android baseline is not valid JSON: " + $_.Exception.Message) }
}
$tolerance = 0.005
$baselineLocations = if ($baseline) { [double]$baseline.location_count } else { 0 }
$baselineEntries = if ($baseline) { [double]$baseline.entry_count } else { 0 }
$locationDelta = if ($baseline) { [int]$buildJson.location_count - [int]$baseline.location_count } else { 0 }
$entryDelta = if ($baseline) { [int]$buildJson.entry_count - [int]$baseline.entry_count } else { 0 }
$locationTolerance = [math]::Floor($baselineLocations * $tolerance)
$entryTolerance = [math]::Floor($baselineEntries * $tolerance)
$result.shrink_guard = [ordered]@{
    baseline_file = $baselinePath; baseline_locations = if ($baseline) { $baseline.location_count } else { $null }
    baseline_entries = if ($baseline) { $baseline.entry_count } else { $null }
    locations = $buildJson.location_count; entries = $buildJson.entry_count
    location_delta = $locationDelta; entry_delta = $entryDelta; tolerance_fraction = $tolerance
    tolerance_locations = $locationTolerance; tolerance_entries = $entryTolerance; shrank = $false
}
if ($ok -and $baseline -and (($locationDelta -lt 0 -and [math]::Abs($locationDelta) -gt $locationTolerance) -or
        ($entryDelta -lt 0 -and [math]::Abs($entryDelta) -gt $entryTolerance))) {
    $result.shrink_guard.shrank = $true
    $ok = $false
    $result.blocked_reason = ("Android content silently shrank: entries delta {0}, locations delta {1}; baseline entries={2}, locations={3}, tolerances entries={4}, locations={5}. Check g1_compile.json assembly_load_errors." -f `
        $entryDelta, $locationDelta, $baseline.entry_count, $baseline.location_count, $entryTolerance, $locationTolerance)
}
if ($ok -and (-not $baseline -or ($locationDelta -ge 0 -and $entryDelta -ge 0))) {
    Write-Json $baselinePath ([ordered]@{
        generated = (Get-Date).ToUniversalTime().ToString('o'); target = 'Android'; group = $Group
        entry_count = $buildJson.entry_count; location_count = $buildJson.location_count
        bundle_count = $buildJson.bundle_count; bundle_bytes = $buildJson.bundle_bytes
    })
}

if ($ok) { Write-Gate 'PASS' $null }
if (-not $result.blocked_reason) {
    $result.blocked_reason = "Android content build produced no acceptable catalog/bundle set at $expectedBuildRoot."
}
Write-Gate 'FAIL' $result.blocked_reason
