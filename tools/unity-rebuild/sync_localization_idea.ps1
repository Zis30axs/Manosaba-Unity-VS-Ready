# IDEA entry point: copy Chinese text, then rebuild the content used by Play Mode.
[CmdletBinding()]
param(
    [switch]$SyncOnly,
    [ValidateRange(10, 7200)][int]$TimeoutSeconds = 1800
)
$ErrorActionPreference = 'Stop'
$repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$project = Join-Path $repo 'UnityProject'
$source = Join-Path $repo 'res/Assets/#WitchTrials/Localization/zh-Hans/Text'
$target = Join-Path $project 'Assets/GameContent/#WitchTrials/Localization/zh-Hans/Text'
$bridge = Join-Path $project 'Library/ManosabaLocalization'
$requestPath = Join-Path $bridge 'request.json'
$resultPath = Join-Path $bridge 'result.json'
$utf8 = New-Object System.Text.UTF8Encoding($false)
$lock = $null
$requestId = $null

function Test-ProjectOpen {
    $unityLock = Join-Path $project 'Temp/UnityLockfile'
    if (-not (Test-Path -LiteralPath $unityLock)) { return $false }
    try {
        $probe = [IO.File]::Open($unityLock, 'Open', 'ReadWrite', 'None')
        $probe.Dispose()
        return $false
    } catch [IO.IOException] { return $true }
}

try {
    if (-not (Test-Path -LiteralPath $source -PathType Container)) { throw "Missing localization source: $source" }
    if (-not (Test-Path -LiteralPath $target -PathType Container)) { throw "Import the game assets first: $target" }
    [IO.Directory]::CreateDirectory($bridge) | Out-Null
    try { $lock = [IO.File]::Open((Join-Path $bridge 'sync.lock'), 'OpenOrCreate', 'ReadWrite', 'None') }
    catch [IO.IOException] { throw 'Another localization sync is already running.' }

    $changed = 0
    $files = @(Get-ChildItem -LiteralPath $source -Recurse -File -Filter '*.txt')
    if ($files.Count -eq 0) { throw "No localization text found in $source" }
    foreach ($file in $files) {
        $relative = $file.FullName.Substring($source.Length).TrimStart([char[]]'\/')
        $destination = Join-Path $target $relative
        # Imported files must already have a GUID used by Addressables.
        if (-not (Test-Path -LiteralPath ($destination + '.meta'))) {
            throw "Missing imported metadata for $relative; import/register this asset first."
        }
        if ((Test-Path -LiteralPath $destination) -and
            (Get-FileHash -LiteralPath $file.FullName).Hash -eq (Get-FileHash -LiteralPath $destination).Hash) { continue }
        [IO.File]::Copy($file.FullName, $destination, $true)
        Write-Host "[SYNC] $relative"
        $changed++
    }
    Write-Host "[SYNC] changed=$changed checked=$($files.Count); Unity .meta files preserved."
    if ($SyncOnly) { exit 0 }

    # Always build, even when a previous sync already copied the bytes but its build failed.
    if (-not (Test-ProjectOpen)) {
        . (Join-Path $PSScriptRoot 'common.ps1')
        $editor = Assert-Unity
        $log = Join-Path $script:LogRoot 'localization_build.log'
        Write-Host "[BUILD] Building existing Addressables settings; log: $log"
        $started = Get-Date
        $build = Invoke-Logged -FilePath $editor -LogPath $log -Arguments @(
            '-batchmode', '-nographics', '-quit', '-projectPath', $project,
            '-executeMethod', 'Manosaba.EditorTools.AddressablesBuild.Run', '-logFile', '-'
        )
        $reportPath = Join-Path $script:UnityDataRoot 'addressables_build_result.json'
        if ($build.exit_code -ne 0 -or -not (Test-Path -LiteralPath $reportPath) -or
            (Get-Item -LiteralPath $reportPath).LastWriteTime -lt $started) {
            throw "Unity content build failed or produced no fresh report. See $log"
        }
        $report = Get-Content -LiteralPath $reportPath -Raw -Encoding UTF8 | ConvertFrom-Json
        if ($report.status -ne 'OK' -or -not $report.player_streaming_source_ready) {
            throw "Addressables content is not ready. See $reportPath"
        }
        Write-Host '[BUILD] OK: content is ready for a new Unity Play Mode session.'
        exit 0
    }

    $requestId = [Guid]::NewGuid().ToString('N')
    $request = @{ id = $requestId; expiresUtc = [DateTime]::UtcNow.AddSeconds($TimeoutSeconds).ToString('o') }
    $temporary = Join-Path $bridge 'request.tmp'
    [IO.File]::WriteAllText($temporary, ($request | ConvertTo-Json), $utf8)
    Move-Item -LiteralPath $temporary -Destination $requestPath -Force
    Write-Host '[BUILD] Request sent to the open Unity Editor.'
    Write-Host '[BUILD] Focus Unity to import scripts; stop Play Mode if active. Waiting for the content build...'
    $deadline = [DateTime]::UtcNow.AddSeconds($TimeoutSeconds)
    $lastStatus = ''
    while ([DateTime]::UtcNow -lt $deadline) {
        if (Test-Path -LiteralPath $resultPath) {
            $result = $null
            try { $result = Get-Content -LiteralPath $resultPath -Raw -Encoding UTF8 | ConvertFrom-Json } catch { }
            if ($result -and $result.id -eq $requestId) {
                if ($result.status -ne $lastStatus) {
                    Write-Host "[BUILD] $($result.status): $($result.message)"
                    $lastStatus = $result.status
                }
                if ($result.status -eq 'OK') { exit 0 }
                if ($result.status -eq 'ERROR') { throw $result.message }
            }
        }
        Start-Sleep -Milliseconds 500
    }
    throw "Timed out waiting for Unity. Focus the Editor, stop Play Mode, check compile errors, and rerun. Details: $resultPath"
} catch {
    Write-Error $_ -ErrorAction Continue
    exit 1
} finally {
    if ($requestId -and (Test-Path -LiteralPath $requestPath)) {
        try {
            $pending = Get-Content -LiteralPath $requestPath -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($pending.id -eq $requestId) { Remove-Item -LiteralPath $requestPath }
        } catch { }
    }
    if ($lock) { $lock.Dispose() }
}
