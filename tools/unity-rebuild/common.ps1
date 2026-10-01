# Shared configuration for the Manosaba Unity reconstruction batch tooling.
#
# Every path comes from an environment variable or from tools/audit/audit.config.json;
# nothing machine specific is hard-coded here (audit section 3.1.4).
#
# Dot-source this from the other scripts:  . "$PSScriptRoot\common.ps1"

$ErrorActionPreference = 'Stop'

$script:RepoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$script:UnityProject = Join-Path $script:RepoRoot 'UnityProject'
$script:OutRoot = Join-Path $script:RepoRoot 'tools\unity-rebuild\out'
$script:LogRoot = Join-Path $script:OutRoot 'logs'
$script:UnityDataRoot = Join-Path $script:OutRoot 'unity'

New-Item -ItemType Directory -Force -Path $script:LogRoot, $script:UnityDataRoot | Out-Null

function Get-ConfigValue {
    param([string]$Key, [string]$EnvName, [string]$Default)
    if ($EnvName -and (Test-Path "env:$EnvName")) {
        $v = (Get-Item "env:$EnvName").Value
        if ($v) { return $v }
    }
    $cfg = Join-Path $script:RepoRoot 'tools\audit\audit.config.json'
    $local = Join-Path $script:RepoRoot 'tools\audit\audit.config.local.json'
    foreach ($f in @($local, $cfg)) {
        if (Test-Path $f) {
            try {
                # -Encoding UTF8 is required: Windows PowerShell 5.1 would otherwise read the
                # file as ANSI and corrupt non-ASCII roots (the game path is under a CJK folder).
                $j = Get-Content $f -Raw -Encoding UTF8 | ConvertFrom-Json
                if ($j.PSObject.Properties.Name -contains $Key -and $j.$Key) { return $j.$Key }
            } catch { }
        }
    }
    return $Default
}

# --- Unity editor -------------------------------------------------------------------
# Resolved from MANOSABA_UNITY_EDITOR, then the audit config, then the usual locations.
$script:UnityExe = $null
$candidate = Get-ConfigValue -Key 'unity_editor_path' -EnvName 'MANOSABA_UNITY_EDITOR' -Default ''
if ($candidate -and (Test-Path $candidate)) {
    $script:UnityExe = (Resolve-Path $candidate).Path
} else {
    $roots = @('E:\Unity', 'C:\Program Files\Unity\Hub\Editor', 'D:\Unity', 'E:\Unity\Hub\Editor')
    foreach ($r in $roots) {
        if (-not (Test-Path $r)) { continue }
        foreach ($ver in (Get-ChildItem $r -Directory -ErrorAction SilentlyContinue)) {
            $exe = Join-Path $ver.FullName 'Editor\Unity.exe'
            if (Test-Path $exe) { $script:UnityExe = $exe; break }
        }
        if ($script:UnityExe) { break }
    }
}

$script:ResRoot = Get-ConfigValue -Key 'res_root' -EnvName 'MANOSABA_RES_ROOT' -Default (Join-Path $script:RepoRoot 'res')
if (-not [IO.Path]::IsPathRooted($script:ResRoot)) { $script:ResRoot = Join-Path $script:RepoRoot $script:ResRoot }
$script:ImportedSourceRoot = Join-Path $script:ResRoot '.imported-source'
# Ordinary Unity gates never resolve the original install. Research tools retain
# their independent tools/audit/config.py game-root configuration.
$script:PythonExe = Get-ConfigValue -Key 'python_exe' -EnvName 'MANOSABA_PYTHON' `
    -Default (Join-Path $script:RepoRoot 'tools\venv\Scripts\python.exe')
if (-not (Test-Path -LiteralPath $script:PythonExe -PathType Leaf)) {
    $pythonCommand = Get-Command python -ErrorAction SilentlyContinue
    if ($pythonCommand) { $script:PythonExe = $pythonCommand.Source }
}

function Restore-ContentEvidence {
    # Only fill absent reconstruction inputs; current plans and ledgers win.
    $snapshot = Join-Path $script:ResRoot '.manosaba\reconstruction\manifest.json'
    if (Test-Path -LiteralPath $snapshot -PathType Leaf) {
        & $script:PythonExe (Join-Path $PSScriptRoot 'import_source_cache.py') --res-root $script:ResRoot --restore-evidence
        if ($LASTEXITCODE -ne 0) { throw 'Imported reconstruction evidence could not be restored.' }
    }
}

function Assert-Unity {
    if (-not $script:UnityExe) {
        throw "BLOCKED: Unity Editor not found. Set MANOSABA_UNITY_EDITOR or unity_editor_path in tools/audit/audit.config.json."
    }
    if (-not (Test-Path $script:UnityExe)) {
        throw "BLOCKED: Unity Editor path does not exist: $($script:UnityExe)"
    }
    return $script:UnityExe
}

function Get-UnityVersion {
    param([string]$Exe)
    $vi = (Get-Item $Exe).VersionInfo
    return "$($vi.ProductVersion)"
}

function Assert-UnityLicense {
    <#
      A missing licence makes every Editor-dependent gate BLOCKED, never PASS.
      The Editor is run once in batchmode with `-logFile -`, which streams the log to
      stdout; that avoids depending on Unity honouring an out-of-project log path.

      The reasons are returned distinctly so a licence problem is never confused with a
      broken Editor installation -- an earlier version of this probe reported "editor
      terminated" for both and hid a corrupt install behind the licence message.
    #>
    param([string]$Exe, [string]$ProbeLog)
    New-Item -ItemType Directory -Force -Path (Split-Path $ProbeLog) | Out-Null
    $env:TEMP = Join-Path $script:OutRoot 'tmp'
    New-Item -ItemType Directory -Force -Path $env:TEMP | Out-Null
    # The Editor writes progress and warnings to stderr (for example
    # "debugger-agent: Unable to listen on 3200"); with $ErrorActionPreference='Stop' the caller
    # turns that into a terminating NativeCommandError and the gate dies before it compiles.
    # Native stderr is data here: it belongs in the log, and the licence decision is made from the
    # log text below, never from the exit code.
    $savedEap = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try {
        & $Exe -batchmode -nographics -quit -logFile - -projectPath $script:UnityProject *>&1 |
            Out-File -FilePath $ProbeLog -Encoding UTF8
    } finally {
        $ErrorActionPreference = $savedEap
    }
    if (-not (Test-Path $ProbeLog)) { return @{ ok = $false; kind = 'no-log'; reason = 'probe log not produced' } }
    $text = Get-Content $ProbeLog -Raw -Encoding UTF8 -ErrorAction SilentlyContinue
    if (-not $text) { return @{ ok = $false; kind = 'no-log'; reason = "empty probe log at $ProbeLog" } }

    # 1. A corrupt/partial Editor install is a different problem from a missing licence and
    #    must be reported as such, because the fix is completely different.
    if ($text -match 'Could not find Unity Package Manager local server application' -or
        $text -match 'Error creating external process .*UnityPackageManager\.exe') {
        return @{ ok = $false; kind = 'broken-install'
                  reason = ('Unity Editor installation is incomplete: UnityPackageManager.exe is missing. ' +
                            'Reinstall the Editor (UnitySetup64-6000.0.48f1.exe /S /D=<editor dir>) and re-run.') }
    }
    if ($text -match 'No valid Unity Editor license found' -or
        $text -match 'No ULF license found') {
        # A missing licence makes every Editor-dependent gate BLOCKED, never PASS -- but the
        # negative markers must not fire on a licence that did resolve.  The licence client
        # reports "No ULF license found.,Token not found in cache" while falling back to the
        # entitlement licence and then logs "Serial number assigned to", so positive evidence
        # is checked first (observed 2026-09-17: entitlement licence present, probe still said
        # BLOCKED, and the Editor had in fact compiled the project).
        if ($text -match 'Serial number assigned to' -or
            $text -match 'Successfully resolved entitlement details') {
            return @{ ok = $true; kind = 'ok'
                      reason = 'editor started; licence resolved through the licensing client (entitlement/ULF)' }
        }
        return @{ ok = $false; kind = 'no-licence'
                  reason = 'No valid Unity Editor license found - activate Unity (Hub sign-in) and re-run' }
    }
    if ($text -match 'Exiting without the bug reporter') {
        return @{ ok = $false; kind = 'editor-terminated'
                  reason = "editor terminated during the probe; inspect $ProbeLog" }
    }
    return @{ ok = $true; kind = 'ok'; reason = 'editor started and a licence is present' }
}

function Write-Json {
    param([string]$Path, $Object)
    New-Item -ItemType Directory -Force -Path (Split-Path $Path) | Out-Null
    $Object | ConvertTo-Json -Depth 12 | Set-Content -Path $Path -Encoding UTF8
}

function Invoke-Logged {
    <#
      Run a command, tee stdout+stderr to a log, and return a result object with the
      exit code, elapsed time and log path.  Never swallows a non-zero exit (section 9).
    #>
    param(
        [string]$FilePath,
        [string[]]$Arguments,
        [string]$LogPath
    )
    New-Item -ItemType Directory -Force -Path (Split-Path $LogPath) | Out-Null
    $env:TEMP = Join-Path $script:OutRoot 'tmp'
    New-Item -ItemType Directory -Force -Path $env:TEMP | Out-Null
    $started = Get-Date
    # Native stderr is captured into the log instead of aborting the batch job.
    $savedEap = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try {
        & $FilePath @Arguments 2>&1 | Tee-Object -FilePath $LogPath | Out-Null
    } finally {
        $ErrorActionPreference = $savedEap
    }
    $code = $LASTEXITCODE
    return [pscustomobject]@{
        exit_code = $code
        seconds   = [math]::Round(((Get-Date) - $started).TotalSeconds, 2)
        log       = $LogPath
        command   = "$FilePath $($Arguments -join ' ')"
    }
}

# --- TMP essentials ----------------------------------------------------------------
# TMPro's global settings asset (`Assets/TextMesh Pro/Resources/TMP Settings.asset`) is absent from this
# rebuild, and its absence makes every TMP_Text throw on LoadDefaultSettings -- measured: 330 NullReference
# exceptions once the engine had built its UI hierarchy, which is what the title screen and the ADV views are
# made of.  `AssetDatabase.ImportPackage` is asynchronous, so this runs as its OWN Editor invocation WITHOUT
# `-quit`: the tool polls from EditorApplication.update and exits itself once the asset exists.
function Invoke-TmpEssentials {
    param([string]$LogPath = (Join-Path $script:LogRoot 'tmp_essentials.log'))

    $settings = Join-Path $script:UnityProject 'Assets\TextMesh Pro\Resources\TMP Settings.asset'
    if (Test-Path $settings) {
        return [pscustomobject]@{ exit_code = 0; seconds = 0; log = $null; skipped = $true }
    }
    Write-Host '=== TMP essential resources (missing -> importing) ===' -ForegroundColor Cyan
    $r = Invoke-Logged -FilePath $script:UnityExe -LogPath $LogPath -Arguments @(
        '-batchmode', '-nographics',
        '-projectPath', $script:UnityProject,
        '-executeMethod', 'Manosaba.EditorTools.TmpEssentialsSetup.Run',
        '-logFile', '-'
    )
    Write-Host ("  TMP import exit={0} in {1:N1}s (settings present: {2})" -f `
        $r.exit_code, $r.seconds, (Test-Path $settings))
    return $r
}

Write-Host "[common] repo        = $script:RepoRoot"
Write-Host "[common] unity       = $(if ($script:UnityExe) { $script:UnityExe } else { '(not found)' })"
Write-Host "[common] unity ver   = $(if ($script:UnityExe) { Get-UnityVersion $script:UnityExe } else { 'n/a' })"
Write-Host "[common] content     = $script:ResRoot"
Write-Host "[common] source cache= $script:ImportedSourceRoot"
Write-Host "[common] out root    = $script:OutRoot"
