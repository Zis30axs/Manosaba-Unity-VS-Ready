param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string]$GameRoot,
    [string]$UnityEditor,
    [switch]$Reextract,
    [switch]$SkipBuild,
    [switch]$AllowMapMismatch
)

$ErrorActionPreference = 'Stop'
$repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
$game = [IO.Path]::GetFullPath($GameRoot)
if (-not (Test-Path -LiteralPath (Join-Path $game 'manosaba_Data') -PathType Container)) {
    throw "GameRoot must contain manosaba_Data: $game"
}

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

$importArgs = @((Join-Path $repo 'tools/content/content.py'), 'import', $game)
if ($Reextract) { $importArgs += '--reextract' }
if ($AllowMapMismatch) { $importArgs += '--allow-resource-map-mismatch' }
& $python @importArgs
if ($LASTEXITCODE -ne 0) { throw "Resource import failed with exit code $LASTEXITCODE" }

$mapArgs = @((Join-Path $repo 'tools/content/resource_map.py'), 'verify', '--check-files')
if ($AllowMapMismatch) {
    Write-Warning 'Skipping the tracked dump identity check (-AllowMapMismatch). Regenerate resource-map.csv only after reviewing the game version.'
} else {
    & $python @mapArgs
    if ($LASTEXITCODE -ne 0) {
        throw 'The local dump does not match tools/content/resource-map.csv. Use the same game build, or explicitly review and regenerate the map.'
    }
}

if (-not $SkipBuild) {
    $buildArgs = @((Join-Path $repo 'tools/content/content.py'), 'build')
    if ($AllowMapMismatch) { $buildArgs += '--allow-resource-map-mismatch' }
    & $python @buildArgs
    if ($LASTEXITCODE -ne 0) { throw "Content build failed with exit code $LASTEXITCODE" }
}

Write-Host 'Portable resource setup completed. GameContent, .meta files, Addressables entries, and bundles remain local generated outputs.'
