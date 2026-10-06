# Android G5 gate: install the fresh Debug APK, push its fresh OBB, launch it on a
# connected Android 10+ device, and collect logcat.  This is deliberately separate
# from the Windows G5 smoke gate.
#
#   pwsh -File tools/unity-rebuild/g5_android_player_smoke.ps1
#
# Exit codes: 0 = PASS (install/launch evidence collected); 1 = FAIL after a device
# was contacted; 2 = BLOCKED (no adb, no device, or no fresh Android player build).

param(
    [string]$Apk = '',
    [string]$PackageId = 'com.acacia.manosaba',
    [int]$LaunchWaitSeconds = 15
)

. "$PSScriptRoot/common.ps1"

if (-not $Apk) { $Apk = Join-Path $script:RepoRoot 'dist/player-android/manosaba-rebuild.apk' }

$gatePath = Join-Path $script:UnityDataRoot 'g5_android_player_smoke.json'
$buildReceiptPath = Join-Path $script:UnityDataRoot 'build_android_player.json'
$result = [ordered]@{
    gate            = 'G5_android_player_smoke'
    generated       = (Get-Date).ToUniversalTime().ToString('o')
    package_id      = $PackageId
    status          = 'NOT_RUN'
    blocked_reason  = $null
    build_receipt   = $null
    adb             = $null
    device          = $null
    install         = $null
    obb             = $null
    sidecar         = $null
    launch          = $null
    logcat          = $null
}

function Finish-Gate {
    param([string]$Status, [string]$Reason, [int]$ExitCode)
    $result.status = $Status
    if ($Reason) { $result.blocked_reason = $Reason }
    Write-Json $gatePath $result
    $colour = if ($Status -eq 'PASS') { 'Green' } elseif ($Status -like 'BLOCKED*') { 'Yellow' } else { 'Red' }
    Write-Host ("G5 Android {0}: {1}" -f $Status, $gatePath) -ForegroundColor $colour
    exit $ExitCode
}

if (-not (Test-Path -LiteralPath $buildReceiptPath -PathType Leaf)) {
    Finish-Gate 'BLOCKED_NO_BUILD_RECEIPT' "Missing $buildReceiptPath; run g1/g3 and BuildAndroidPlayer first." 2
}
$buildReceipt = Get-Content $buildReceiptPath -Raw -Encoding UTF8 | ConvertFrom-Json
$result.build_receipt = $buildReceipt
if ($buildReceipt.status -ne 'SUCCEEDED' -or -not (Test-Path -LiteralPath $Apk -PathType Leaf)) {
    Finish-Gate 'BLOCKED_NO_ANDROID_BUILD' 'Android player receipt is not SUCCEEDED or the APK is missing.' 2
}
$apkInfo = Get-Item -LiteralPath $Apk
# A build whose bundles ship as zip parts folds the engine data into the APK (no expansion file); only a split build needs an OBB.
$obbExpected = [bool]$buildReceipt.obb_expected
$obbPath = $null
if ($obbExpected) {
    $obbRecord = @($buildReceipt.artifacts | Where-Object { $_.extension -eq '.obb' -and $_.fresh -and (Test-Path -LiteralPath $_.path) }) | Select-Object -First 1
    if (-not $obbRecord) { Finish-Gate 'BLOCKED_NO_FRESH_OBB' 'The Android build did not produce a fresh OBB.' 2 }
    $obbPath = (Resolve-Path -LiteralPath $obbRecord.path).Path
}

$adbCandidates = @()
if ($env:ANDROID_HOME) { $adbCandidates += (Join-Path $env:ANDROID_HOME 'platform-tools\adb.exe') }
if ($env:ANDROID_SDK_ROOT) { $adbCandidates += (Join-Path $env:ANDROID_SDK_ROOT 'platform-tools\adb.exe') }
if ($script:UnityExe) { $adbCandidates += (Join-Path (Split-Path $script:UnityExe -Parent) 'Data\PlaybackEngines\AndroidPlayer\SDK\platform-tools\adb.exe') }
$adbCommand = Get-Command adb -ErrorAction SilentlyContinue
if ($adbCommand) { $adbCandidates += $adbCommand.Source }
$adbPath = $adbCandidates | Where-Object { $_ -and (Test-Path -LiteralPath $_ -PathType Leaf) } | Select-Object -First 1
if (-not $adbPath) { Finish-Gate 'BLOCKED_NO_ADB' 'adb was not found; install Android SDK platform-tools or set ANDROID_HOME.' 2 }
$result.adb = $adbPath

function Invoke-Adb {
    param([string[]]$Arguments)
    $savedEap = $ErrorActionPreference; $ErrorActionPreference = 'Continue'
    try {
        $output = (& $adbPath @Arguments 2>&1 | Out-String).Trim()
        return [pscustomobject]@{ exit_code = $LASTEXITCODE; output = $output }
    } finally { $ErrorActionPreference = $savedEap }
}

$devices = Invoke-Adb @('devices')
$deviceLines = @($devices.output -split "`r?`n" | Where-Object { $_ -match "\tdevice$" })
if ($devices.exit_code -ne 0 -or $deviceLines.Count -eq 0) {
    $result.device = [ordered]@{ devices_exit_code = $devices.exit_code; devices = $devices.output }
    Finish-Gate 'BLOCKED_NO_DEVICE' 'adb found no connected Android device in the device state.' 2
}
$serial = ($deviceLines[0] -split "\t")[0]
$sdk = Invoke-Adb @('-s', $serial, 'shell', 'getprop', 'ro.build.version.sdk')
$sdkValue = 0; [int]::TryParse($sdk.output.Trim(), [ref]$sdkValue) | Out-Null
$result.device = [ordered]@{ serial = $serial; sdk = $sdkValue; sdk_raw = $sdk.output; devices = $devices.output }
if ($sdkValue -lt 29) { Finish-Gate 'BLOCKED_UNSUPPORTED_ANDROID' "Connected device API $sdkValue is below Android 10/API 29." 2 }

$install = Invoke-Adb @('-s', $serial, 'install', '-r', $Apk)
$result.install = [ordered]@{ exit_code = $install.exit_code; output = $install.output; apk = $Apk; bytes = $apkInfo.Length }
if ($install.exit_code -ne 0 -or $install.output -notmatch 'Success') {
    Finish-Gate 'FAIL_INSTALL' "adb install failed: $($install.output)" 1
}
$obbDir = "/sdcard/Android/obb/$PackageId"
# Unity's expansion-file loader only finds main.<bundle version code>.<package>.obb, not the build output's own file name.
$projectSettings = Get-Content (Join-Path $script:UnityProject 'ProjectSettings\ProjectSettings.asset') -Raw -Encoding UTF8
$versionCodeMatch = [regex]::Match($projectSettings, '(?m)^\s*AndroidBundleVersionCode:\s*(\d+)\s*$')
if (-not $versionCodeMatch.Success) { Finish-Gate 'BLOCKED_NO_VERSION_CODE' 'AndroidBundleVersionCode is missing from ProjectSettings.asset.' 2 }
$obbDeviceName = "main.$($versionCodeMatch.Groups[1].Value).$PackageId.obb"
if ($obbExpected) {
    $mkdir = Invoke-Adb @('-s', $serial, 'shell', 'mkdir', '-p', $obbDir)
    $push = Invoke-Adb @('-s', $serial, 'push', $obbPath, "$obbDir/$obbDeviceName")
    $result.obb = [ordered]@{ directory = $obbDir; path = $obbPath; bytes = (Get-Item $obbPath).Length; mkdir = $mkdir.output; mkdir_exit_code = $mkdir.exit_code; push = $push.output; push_exit_code = $push.exit_code }
    if ($mkdir.exit_code -ne 0 -or $push.exit_code -ne 0) { Finish-Gate 'FAIL_OBB_PUSH' "adb OBB push failed: $($push.output)" 1 }
} else {
    # An expansion file left by an earlier split build would be mounted against this APK and rejected ("mismatching GUID").
    $stale = Invoke-Adb @('-s', $serial, 'shell', 'rm', '-f', "$obbDir/$obbDeviceName")
    $result.obb = [ordered]@{ expected = $false; stale_removed_exit_code = $stale.exit_code }
}

# Content too large for an expansion file ships beside the app (see BuildAndroidPlayer / AndroidAddressablesSidecar): the
# bundles go to the app's external files directory, where the player's Addressables hook looks for them.
if ($buildReceipt.sidecar) {
    $sidecarSource = [string]$buildReceipt.sidecar_source
    $sidecarDevice = [string]$buildReceipt.sidecar_device_dir
    if (-not (Test-Path -LiteralPath $sidecarSource -PathType Container) -or -not $sidecarDevice) {
        Finish-Gate 'BLOCKED_NO_SIDECAR_CONTENT' "The build receipt asks for a content sidecar but '$sidecarSource' is missing." 2
    }
    $sidecarMkdir = Invoke-Adb @('-s', $serial, 'shell', 'mkdir', '-p', $sidecarDevice)
    $sidecarPush = Invoke-Adb @('-s', $serial, 'push', '--sync', ($sidecarSource.TrimEnd([char[]]@([char]92, [char]47)) + '/.'), $sidecarDevice)
    $result.sidecar = [ordered]@{ source = $sidecarSource; device_dir = $sidecarDevice; bundles = $buildReceipt.sidecar_bundles; bytes = $buildReceipt.sidecar_bytes
        mkdir_exit_code = $sidecarMkdir.exit_code; push_exit_code = $sidecarPush.exit_code; push = $sidecarPush.output }
    if ($sidecarMkdir.exit_code -ne 0 -or $sidecarPush.exit_code -ne 0) { Finish-Gate 'FAIL_SIDECAR_PUSH' "adb content push failed: $($sidecarPush.output)" 1 }
}

# A locked screen pauses the player right after launch (Unity gets onPause before its first frame, then the system kills it), so
# a run on a locked device proves nothing about the game.
$null = Invoke-Adb @('-s', $serial, 'shell', 'input', 'keyevent', 'KEYCODE_WAKEUP')
Start-Sleep -Seconds 2
$trust = Invoke-Adb @('-s', $serial, 'shell', 'dumpsys', 'trust')
$deviceLocked = $trust.output -match 'deviceLocked=1'
$result.device.locked = [bool]$deviceLocked
# A Development player carries android:showWhenLocked (AndroidDevelopmentManifest), so only a build without it needs an unlocked phone.
if ($deviceLocked -and -not $buildReceipt.show_when_locked) {
    Finish-Gate 'BLOCKED_DEVICE_LOCKED' 'The device is locked; unlock it and keep the screen on (Settings > Display > Sleep, or "Stay awake" in developer options), then rerun.' 2
}

# The system log is flooded by vendor services, so the unfiltered buffer no longer holds the first seconds of the game after
# a while; keep a live capture of the Unity / crash tags from before the launch.
$liveLogPath = Join-Path $script:LogRoot 'g5_android_unity_logcat.log'
$null = Invoke-Adb @('-s', $serial, 'logcat', '-c')
Remove-Item -LiteralPath $liveLogPath -ErrorAction SilentlyContinue
$liveLog = Start-Process -FilePath $adbPath -ArgumentList @('-s', $serial, 'logcat', '-v', 'threadtime', 'Unity:V', 'AndroidRuntime:V', 'DEBUG:V', 'ActivityManager:W', '*:S') `
    -RedirectStandardOutput $liveLogPath -NoNewWindow -PassThru
$launch = Invoke-Adb @('-s', $serial, 'shell', 'monkey', '-p', $PackageId, '1')
$result.launch = [ordered]@{ exit_code = $launch.exit_code; output = $launch.output; wait_seconds = $LaunchWaitSeconds }
Start-Sleep -Seconds $LaunchWaitSeconds
$gamePid = Invoke-Adb @('-s', $serial, 'shell', 'pidof', $PackageId)
$alive = [bool]($gamePid.output -match '^\d+')
Start-Sleep -Seconds 1
if (-not $liveLog.HasExited) { Stop-Process -Id $liveLog.Id -Force -ErrorAction SilentlyContinue }
$logPath = Join-Path $script:LogRoot 'g5_android_logcat.log'
$log = Invoke-Adb @('-s', $serial, 'logcat', '-d', '-v', 'threadtime')
Set-Content -LiteralPath $logPath -Value $log.output -Encoding UTF8
$liveText = if (Test-Path -LiteralPath $liveLogPath) { Get-Content -LiteralPath $liveLogPath -Raw -Encoding UTF8 } else { '' }
$fatal = @(($liveText + "`n" + $log.output) -split "`r?`n" | Where-Object { $_ -match 'FATAL EXCEPTION|AndroidRuntime: FATAL|IL2CPP.*Error|MissingMethodException|DllNotFoundException|Fatal signal' })
$unityErrors = @($liveText -split "`r?`n" | Where-Object { $_ -match ' E Unity |Exception|InvalidKeyException' } | Select-Object -First 40)
$result.logcat = [ordered]@{ path = $logPath; unity_log = $liveLogPath; clear_exit_code = 0; dump_exit_code = $log.exit_code
    lines = @($log.output -split "`r?`n").Count; process_alive = $alive; fatal_markers = $fatal; unity_error_sample = $unityErrors }
if ($launch.exit_code -ne 0 -or $log.exit_code -ne 0 -or $fatal.Count -gt 0 -or -not $alive) {
    Finish-Gate 'FAIL_RUNTIME' 'Android launch completed with a command failure, a fatal logcat marker or without a running process; inspect the recorded logcat.' 1
}
Finish-Gate 'PASS' $null 0
