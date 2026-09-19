# Run ScriptableObjectConverter in batch mode against a chosen plan file.
#   pwsh work/serialization-loss/run_so_convert.ps1 -Plan so_probe_title.json -Out work/serialization-loss/so_probe_title_result.json
param(
    [Parameter(Mandatory)][string]$Plan,          # file name inside tools/unity-rebuild/out/unity
    [Parameter(Mandatory)][string]$Out,           # repo-relative result path
    [string]$Log = 'work/serialization-loss/so_convert_run.log'
)
. "$PSScriptRoot\..\..\tools\unity-rebuild\common.ps1"
$exe = Assert-Unity
$env:MANOSABA_SO_PLAN = $Plan
$env:MANOSABA_SO_OUT = (Join-Path $script:RepoRoot $Out)
$env:TEMP = Join-Path $script:OutRoot 'tmp'
New-Item -ItemType Directory -Force -Path $env:TEMP | Out-Null
$logPath = Join-Path $script:RepoRoot $Log
$savedEap = $ErrorActionPreference; $ErrorActionPreference = 'Continue'
try {
    & $exe -batchmode -nographics -quit -logFile $logPath -projectPath $script:UnityProject `
        -executeMethod Manosaba.EditorTools.ScriptableObjectConverter.Run *>&1 | Out-Null
    $code = $LASTEXITCODE
} finally { $ErrorActionPreference = $savedEap }
Write-Host "unity exit code: $code ; log: $logPath ; result: $($env:MANOSABA_SO_OUT)"
exit $code
