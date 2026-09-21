# Sample Unity's private memory and the system's free commit every 3 s while a Unity batch job runs; if free commit
# falls below -FloorMB, stop Unity so the rest of the machine does not run out of commit (on 2026-09-21 a content build
# took the whole system to ~0 free commit and even this kind of sampler died with OutOfMemoryException).
#
#   powershell -File work\title-path\memguard.ps1 -Out work\title-path\g3_memguard.csv [-FloorMB 600] [-Minutes 60]
param([string]$Out, [int]$FloorMB = 600, [int]$Minutes = 60)
"time,unity_priv_mb,free_commit_mb,free_phys_mb,action" | Out-File $Out -Encoding utf8
$end = (Get-Date).AddMinutes($Minutes)
$seen = $false
while ((Get-Date) -lt $end) {
    $u = Get-Process Unity -ErrorAction SilentlyContinue | Sort-Object PrivateMemorySize64 -Descending | Select-Object -First 1
    $os = Get-CimInstance Win32_OperatingSystem
    $commit = [math]::Round($os.FreeVirtualMemory / 1KB)
    $action = ''
    if ($u) {
        $seen = $true
        if ($commit -lt $FloorMB) {
            $action = 'STOPPED_UNITY'
            Get-Process Unity -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
        }
    }
    "{0},{1},{2},{3},{4}" -f (Get-Date -Format HH:mm:ss), $(if ($u) { [math]::Round($u.PrivateMemorySize64 / 1MB) } else { 0 }), $commit, [math]::Round($os.FreePhysicalMemory / 1KB), $action | Out-File $Out -Append -Encoding utf8
    if ($action) { exit 2 }
    Start-Sleep -Seconds 3
}
exit 0
