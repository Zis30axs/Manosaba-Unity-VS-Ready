# Run PrefabBuilder in batch mode against a chosen plan file.
#   .\work\serialization-loss\run_prefab_build.ps1 -Plan prefab_plan_probe.json -Out work/serialization-loss/prefab_probe_result.json -FieldDump
param(
    [Parameter(Mandatory)][string]$Plan,          # file name inside tools/unity-rebuild/out/unity
    [Parameter(Mandatory)][string]$Out,           # repo-relative result path
    [string]$Log = 'work/serialization-loss/prefab_build_run.log',
    [switch]$FieldDump
)
. "$PSScriptRoot\..\..\tools\unity-rebuild\common.ps1"
$exe = Assert-Unity
$env:MANOSABA_PREFAB_PLAN = $Plan
$env:MANOSABA_PREFAB_OUT = (Join-Path $script:RepoRoot $Out)
if ($FieldDump) { $env:MANOSABA_PREFAB_FIELD_DUMP = '1' } else { Remove-Item Env:\MANOSABA_PREFAB_FIELD_DUMP -ErrorAction SilentlyContinue }
$env:TEMP = Join-Path $script:OutRoot 'tmp'
New-Item -ItemType Directory -Force -Path $env:TEMP | Out-Null
$logPath = Join-Path $script:RepoRoot $Log
$savedEap = $ErrorActionPreference; $ErrorActionPreference = 'Continue'
try {
    & $exe -batchmode -nographics -quit -logFile $logPath -projectPath $script:UnityProject `
        -executeMethod Manosaba.EditorTools.PrefabBuilder.Run *>&1 | Out-Null
    $code = $LASTEXITCODE
} finally { $ErrorActionPreference = $savedEap }
Write-Host "unity exit code: $code ; log: $logPath ; result: $($env:MANOSABA_PREFAB_OUT)"
exit $code
