# Wait until the machine can hold the Addressables content build, then run G3 and (on PASS) G4b.
#
#   powershell -File work\title-path\wait_then_verify.ps1 [-Tag round3] [-Hours 12]
#
# G3 (30,819 entries) needs well over 5 GB of free commit: on 2026-09-21 it died twice at ~4.2 GB of Unity private
# memory with ~5 GB free, and the commit limit could not grow because C: (page file) was full.  Exhausting commit also
# crashes other applications, so this never starts the build while the condition is not met.
# Condition: free commit >= $MinCommitGB, or C: has >= $MinCFreeGB (the system-managed page file can then grow) and
# free commit >= 4 GB.  Checked once a minute.
param(
    [string]$Tag = 'round3',
    [double]$Hours = 12,
    [double]$MinCommitGB = 9,
    [double]$MinCFreeGB = 15
)
Set-Location E:\Manosaba-VSCodeReady
$deadline = (Get-Date).AddHours($Hours)
$log = "work\title-path\wait_then_verify_$Tag.log"
function Note([string]$m) { $line = "{0} {1}" -f (Get-Date -Format 'MM-dd HH:mm:ss'), $m; $line | Out-File $log -Append -Encoding utf8; Write-Host $line }

Note "waiting: need free commit >= $MinCommitGB GB, or C: >= $MinCFreeGB GB free with commit >= 4 GB (until $deadline)"
$last = ''
while ($true) {
    $os = Get-CimInstance Win32_OperatingSystem
    $c = Get-CimInstance Win32_LogicalDisk -Filter "DeviceID='C:'"
    $commit = [math]::Round($os.FreeVirtualMemory / 1MB, 1)
    $cfree = [math]::Round($c.FreeSpace / 1GB, 1)
    $state = "free commit $commit GB, C: $cfree GB"
    if ($state -ne $last) { Note $state; $last = $state }
    if ($commit -ge $MinCommitGB -or ($cfree -ge $MinCFreeGB -and $commit -ge 4)) { break }
    if ((Get-Date) -gt $deadline) { Note "TIMEOUT: resources never became available"; exit 3 }
    Start-Sleep -Seconds 60
}

Note "resources available -> G3 content build"
Get-Process Unity, bee_backend, 'Unity.ILPP.Runner' -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
# Memory guard alongside the build: Unity is stopped if free commit drops below 600 MB, so an unattended build can never
# starve the rest of the machine (see memguard.ps1).
$guard = Start-Process powershell -PassThru -WindowStyle Hidden -ArgumentList @('-NoProfile', '-ExecutionPolicy', 'Bypass',
    '-File', (Resolve-Path 'work\title-path\memguard.ps1').Path, '-Out', "work\title-path\g3_${Tag}_memguard.csv",
    '-FloorMB', '600', '-Minutes', '60')
& .\tools\unity-rebuild\g3_content_build.ps1 -Group all *>&1 | Tee-Object -FilePath "work\title-path\g3_$Tag.console.txt" | Out-Null
if ($guard -and -not $guard.HasExited) { Stop-Process -Id $guard.Id -Force -ErrorAction SilentlyContinue }
$g3 =Get-Content tools\unity-rebuild\out\unity\g3_content_build.json -Raw -Encoding UTF8 | ConvertFrom-Json
Copy-Item tools\unity-rebuild\out\unity\g3_content_build.json "work\title-path\g3_$Tag.json" -Force
Note ("G3 status={0} {1}" -f $g3.status, $g3.blocked_reason)
if ($g3.status -ne 'PASS') { exit 4 }

Note "G4b $Tag"
& .\work\title-path\run_g4b.ps1 -Tag $Tag *>&1 | Out-File "work\title-path\g4b_$Tag.runner.txt" -Encoding utf8
Note "G4b finished exit=$LASTEXITCODE"
exit 0
