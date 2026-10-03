param([switch]$SkipBuild)

$ErrorActionPreference = 'Stop'
if (-not $env:MANOSABA_GAME_ROOT) {
    throw 'Set MANOSABA_GAME_ROOT to the collaborator''s readable game/dump directory before running this IDEA configuration.'
}
$arguments = @($env:MANOSABA_GAME_ROOT)
if ($SkipBuild) { $arguments += '-SkipBuild' }
& (Join-Path $PSScriptRoot 'setup_from_dump.ps1') @arguments
exit $LASTEXITCODE
