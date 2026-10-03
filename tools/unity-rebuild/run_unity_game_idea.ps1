# IDEA entry point for the reconstructed Unity project.
# Starts Unity directly, without Unity Hub, and enters Play Mode through IdeaPlayModeBridge.
[CmdletBinding()]
param(
    [string]$ProjectPath = '',
    [switch]$SkipContentCheck,
    [ValidateRange(30, 3600)][int]$TimeoutSeconds = 180
)

$ErrorActionPreference = 'Stop'
$repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
if ([string]::IsNullOrWhiteSpace($ProjectPath)) { $ProjectPath = Join-Path $repo 'UnityProject' }
$project = [IO.Path]::GetFullPath($ProjectPath)
$versionFile = Join-Path $project 'ProjectSettings/ProjectVersion.txt'
$bridge = Join-Path $project 'Library/ManosabaIdeaLauncher'
$requestPath = Join-Path $bridge 'request.json'
$resultPath = Join-Path $bridge 'result.json'
$logPath = Join-Path $repo 'tools/unity-rebuild/out/logs/idea_unity_editor.log'
$reportPath = Join-Path $repo 'tools/unity-rebuild/out/unity/addressables_build_result.json'
$utf8 = New-Object System.Text.UTF8Encoding($false)

function Get-ConfiguredValue {
    param([string]$Key, [string]$EnvironmentName)
    if ($EnvironmentName -and (Test-Path "env:$EnvironmentName")) {
        $value = (Get-Item "env:$EnvironmentName").Value
        if ($value) { return $value }
    }
    foreach ($configPath in @(
        (Join-Path $repo 'tools/audit/audit.config.local.json'),
        (Join-Path $repo 'tools/audit/audit.config.json')
    )) {
        if (-not (Test-Path -LiteralPath $configPath -PathType Leaf)) { continue }
        try {
            $config = Get-Content -LiteralPath $configPath -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($config.PSObject.Properties.Name -contains $Key -and $config.$Key) { return $config.$Key }
        } catch { }
    }
    return $null
}

function Get-UnityEditor {
    $configured = Get-ConfiguredValue 'unity_editor_path' 'MANOSABA_UNITY_EDITOR'
    $version = ''
    if (Test-Path -LiteralPath $versionFile -PathType Leaf) {
        $match = Select-String -LiteralPath $versionFile -Pattern '^m_EditorVersion:\s*(\S+)'
        if ($match) { $version = $match.Matches[0].Groups[1].Value }
    }
    $candidates = @()
    if ($configured) { $candidates += $configured }
    if ($version) {
        $candidates += @(
            "E:\AI\$version\Editor\Unity.exe",
            "E:\Unity\$version\Editor\Unity.exe",
            "D:\Unity\$version\Editor\Unity.exe",
            "C:\Program Files\Unity\Hub\Editor\$version\Editor\Unity.exe",
            "E:\Unity\Hub\Editor\$version\Editor\Unity.exe"
        )
    }
    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate -PathType Leaf) {
            return [IO.Path]::GetFullPath($candidate)
        }
    }
    throw "Unity $version was not found. Set MANOSABA_UNITY_EDITOR or unity_editor_path in tools/audit/audit.config.local.json."
}

function Test-EditorOpen {
    $lockPath = Join-Path $project 'Temp/UnityLockfile'
    if (-not (Test-Path -LiteralPath $lockPath -PathType Leaf)) { return $false }
    try {
        $probe = [IO.File]::Open($lockPath, 'Open', 'ReadWrite', 'None')
        $probe.Dispose()
        return $false
    } catch [IO.IOException] { return $true }
}

function Test-BuiltContent {
    if (-not (Test-Path -LiteralPath $reportPath -PathType Leaf)) { return $false }
    try {
        $report = Get-Content -LiteralPath $reportPath -Raw -Encoding UTF8 | ConvertFrom-Json
        $settings = Join-Path $project 'Library/com.unity.addressables/aa/Windows/settings.json'
        return $report.status -eq 'OK' -and $report.player_streaming_source_ready -and
            (Test-Path -LiteralPath $settings -PathType Leaf)
    } catch { return $false }
}

if (-not (Test-Path -LiteralPath $project -PathType Container)) { throw "Unity project not found: $project" }
if (-not $SkipContentCheck -and -not (Test-BuiltContent)) {
    throw "Packed Addressables content is missing or stale. Run the content build first, or use -SkipContentCheck for an intentional editor-only launch."
}

$editor = Get-UnityEditor
[IO.Directory]::CreateDirectory($bridge) | Out-Null
[IO.Directory]::CreateDirectory((Split-Path -Parent $logPath)) | Out-Null
$requestId = [Guid]::NewGuid().ToString('N')
$request = @{ id = $requestId; expiresUtc = [DateTime]::UtcNow.AddSeconds($TimeoutSeconds).ToString('o') }
$temporary = Join-Path $bridge 'request.tmp'
[IO.File]::WriteAllText($temporary, ($request | ConvertTo-Json), $utf8)
Move-Item -LiteralPath $temporary -Destination $requestPath -Force

if (-not (Test-EditorOpen)) {
    Write-Host "[Unity] Starting $editor"
    Start-Process -FilePath $editor -WorkingDirectory (Split-Path -Parent $editor) -ArgumentList @(
        '-projectPath', $project,
        '-logFile', $logPath
    ) | Out-Null
} else {
    Write-Host '[Unity] Existing Editor detected; using it instead of starting a second instance.'
}

$deadline = [DateTime]::UtcNow.AddSeconds($TimeoutSeconds)
$lastStatus = ''
try {
    while ([DateTime]::UtcNow -lt $deadline) {
        if (Test-Path -LiteralPath $resultPath) {
            try { $result = Get-Content -LiteralPath $resultPath -Raw -Encoding UTF8 | ConvertFrom-Json } catch { $result = $null }
            if ($result -and $result.id -eq $requestId) {
                if ($result.status -ne $lastStatus) {
                    Write-Host "[Unity] $($result.status): $($result.message)"
                    $lastStatus = $result.status
                }
                if ($result.status -in @('PLAYING', 'ALREADY_PLAYING')) { exit 0 }
                if ($result.status -eq 'ERROR') { throw $result.message }
            }
        }
        Start-Sleep -Milliseconds 500
    }
    throw "Timed out waiting for Unity Play Mode. Check $logPath and the Unity Console."
} finally {
    if (Test-Path -LiteralPath $requestPath) {
        try {
            $pending = Get-Content -LiteralPath $requestPath -Raw -Encoding UTF8 | ConvertFrom-Json
            if ($pending.id -eq $requestId) { Remove-Item -LiteralPath $requestPath -Force }
        } catch { }
    }
}
