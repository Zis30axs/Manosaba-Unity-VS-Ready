# Comment/uncomment the fixed local-only opening block. Takes effect on the next Play session.
[CmdletBinding()]
param([ValidateSet('Enable', 'Disable')][string]$Mode = 'Disable')
$ErrorActionPreference = 'Stop'
$taskRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$taskSource = Join-Path $taskRoot 'funny/opening-bmpt72-test.nani'
$taskLockDir = Join-Path $taskRoot 'UnityProject/Library/ManosabaLocalOpening'
$taskLock = $null
$taskPrefix = '; LOCAL_BMPT_DISABLED '
try {
    [IO.Directory]::CreateDirectory($taskLockDir) | Out-Null
    $taskLock = [IO.File]::Open((Join-Path $taskLockDir 'toggle.lock'), 'OpenOrCreate', 'ReadWrite', 'None')
    $taskText = [IO.File]::ReadAllText($taskSource)
    $taskNewline = if ($taskText.Contains([string][char]13)) { [string][char]13 + [char]10 } else { [string][char]10 }
    $taskLines = $taskText -split '\r?\n'
    $taskBegin = @(); $taskEnd = @()
    for ($taskIndex = 0; $taskIndex -lt $taskLines.Count; $taskIndex++) {
        if ($taskLines[$taskIndex].Trim() -eq '; BEGIN_LOCAL_BMPT_TEST') { $taskBegin += $taskIndex }
        if ($taskLines[$taskIndex].Trim() -eq '; END_LOCAL_BMPT_TEST') { $taskEnd += $taskIndex }
    }
    if ($taskBegin.Count -ne 1 -or $taskEnd.Count -ne 1 -or $taskBegin[0] -ge $taskEnd[0]) {
        throw 'Expected one intact BEGIN_LOCAL_BMPT_TEST / END_LOCAL_BMPT_TEST block.'
    }
    for ($taskIndex = $taskBegin[0] + 1; $taskIndex -lt $taskEnd[0]; $taskIndex++) {
        $taskLine = $taskLines[$taskIndex]
        if ($Mode -eq 'Enable') {
            if ($taskLine.StartsWith($taskPrefix, [StringComparison]::Ordinal)) {
                $taskLines[$taskIndex] = $taskLine.Substring($taskPrefix.Length)
            }
        } elseif ($taskLine.Trim().Length -gt 0 -and -not $taskLine.TrimStart().StartsWith(';')) {
            $taskLines[$taskIndex] = $taskPrefix + $taskLine
        }
    }
    $taskUpdated = $taskLines -join $taskNewline
    if ($taskUpdated -ne $taskText) {
        $taskTemporary = $taskSource + '.toggle.tmp'
        [IO.File]::WriteAllText($taskTemporary, $taskUpdated, (New-Object Text.UTF8Encoding($false)))
        Move-Item -LiteralPath $taskTemporary -Destination $taskSource -Force
    }
    Write-Host "[LOCAL OPENING] $Mode. Start a NEW Unity Editor Play session; no Addressables rebuild required."
    Write-Host "[LOCAL OPENING] Source: $taskSource"
} catch {
    Write-Error $_ -ErrorAction Continue
    exit 1
} finally {
    if ($taskLock) { $taskLock.Dispose() }
}
