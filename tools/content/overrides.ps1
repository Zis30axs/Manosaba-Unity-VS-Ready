$ErrorActionPreference = 'Stop'
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
& $python (Join-Path $PSScriptRoot 'overrides.py') @args
exit $LASTEXITCODE
