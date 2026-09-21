# Unattended G4b (Boot -> Naninovel -> System_Title) run with the settings that reach script playback, then the
# symptom analysis against the 2026-09-18 baseline.  Kills stale Unity processes of this project first.
#   .\work\title-path\run_g4b.ps1 -Tag round1
param([string]$Tag = (Get-Date -Format 'MMdd-HHmm'))
$ErrorActionPreference = 'Continue'
Set-Location E:\Manosaba-VSCodeReady
Get-Process Unity,bee_backend,'Unity.ILPP.Runner' -ErrorAction SilentlyContinue | ForEach-Object {
    Write-Host ("killing stale {0} {1}" -f $_.Name, $_.Id); Stop-Process -Id $_.Id -Force -ErrorAction SilentlyContinue }
Start-Sleep -Seconds 2
$env:MANOSABA_GAMESTART_CHAIN_SECONDS = if ($env:G4B_CHAIN) { $env:G4B_CHAIN } else { "60" }
$env:MANOSABA_GAMESTART_AUTO_ADVANCE = '1'
$env:MANOSABA_GAMESTART_AUTO_ADVANCE_SECONDS = if ($env:G4B_ADVANCE) { $env:G4B_ADVANCE } else { "12" }
& .\tools\unity-rebuild\g4b_game_start.ps1 -Wire *>&1 | Tee-Object -FilePath "work\title-path\g4b_$Tag.console.txt"
$code = $LASTEXITCODE
$log = Get-ChildItem tools\unity-rebuild\out\logs -Filter 'game_start.log' -ErrorAction SilentlyContinue |
       Sort-Object LastWriteTime -Descending | Select-Object -First 1
if ($log) { Copy-Item $log.FullName "work\title-path\g4b_$Tag.log" -Force }
foreach ($e in 'game_start_evidence_wired.json','game_start_evidence_wired.json.partial.json','g4b_game_start_wired.json') {
    $p = "tools\unity-rebuild\out\unity\$e"
    if (Test-Path $p) { Copy-Item $p "work\title-path\g4b_$Tag.$e" -Force }
}
Write-Host "g4b exit=$code log=$($log.FullName)"
$ev = if (Test-Path "work\title-path\g4b_$Tag.game_start_evidence_wired.json") { "work\title-path\g4b_$Tag.game_start_evidence_wired.json" }
      elseif (Test-Path "work\title-path\g4b_$Tag.game_start_evidence_wired.json.partial.json") { "work\title-path\g4b_$Tag.game_start_evidence_wired.json.partial.json" }
if ($log) { python work\title-path\analyze_g4b.py "work\title-path\g4b_$Tag.log" $ev }
exit $code
