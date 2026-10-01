$ErrorActionPreference = 'Stop'

function Invoke-ManosabaContent {
    param([string]$Command, [string[]]$ContentArguments)
    $repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
    $python = $env:MANOSABA_PYTHON
    if (-not $python) {
        $local = Join-Path $repo 'tools/venv/Scripts/python.exe'
        if (Test-Path -LiteralPath $local) { $python = $local }
        else {
            $found = Get-Command python -ErrorAction SilentlyContinue
            if ($found) { $python = $found.Source }
        }
    }
    if (-not $python) { throw 'Python 3.11+ is required; set MANOSABA_PYTHON.' }
    # Accept both Python-style flags and familiar PowerShell spelling.
    $aliases = @{
        '-DryRun'='--dry-run'; '-Force'='--force'; '-Category'='--category'; '-File'='--file'
        '-CopyOnly'='--copy-only'; '-VerboseContent'='--verbose'; '-Timeout'='--timeout'
        '-Reextract'='--reextract'; '-OverwriteUserChanges'='--overwrite-user-changes'
        '-Deep'='--deep'; '-NoReport'='--no-report'; '-Player'='--player'
    }
    $forward = @($ContentArguments | ForEach-Object { if ($aliases.ContainsKey($_)) { $aliases[$_] } else { $_ } })
    & $python (Join-Path $PSScriptRoot 'content.py') $Command @forward
    exit $LASTEXITCODE
}
