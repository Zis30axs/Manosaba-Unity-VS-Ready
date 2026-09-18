# Copy the shipped Addressables runtime files from the game install into the Unity project so a
# built Player actually contains them (Unity copies Assets/StreamingAssets verbatim).
#
#   pwsh -File tools/unity-rebuild/apply_shipped_aa.ps1 [-WhatIf]
#
# Copies ONLY the small runtime files:
#   shipped-aa/catalog.bin  ~5.5 MB   key/provider/location table the runtime reads
#   aa/catalog.hash     ~32 B     hash the runtime compares against the remote/cached catalog
#   aa/settings.json    ~2 KB     Addressables RuntimeData (version, build target, catalog provider)
#
# The 457 content bundles (6.4 GB) are deliberately NOT copied: they belong to the content build
# (G2/G3) and would make the project unreproducible.  Evidence (sizes + sha256) is written to
# tools/unity-rebuild/out/unity/streamingassets_manifest.json.
#
# Nothing is written into the game directory.
param([switch]$WhatIf)

. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate      = 'apply_shipped_streamingassets'
    generated = (Get-Date).ToUniversalTime().ToString('o')
    game_root = $script:GameRoot
    status    = 'NOT_RUN'
    reason    = $null
    files     = @()
}

if (-not $script:GameRoot) {
    $result.status = 'BLOCKED'
    $result.reason = 'game root not configured (MANOSABA_GAME_ROOT or game_root in tools/audit/audit.config.json)'
    Write-Json (Join-Path $script:UnityDataRoot 'streamingassets_manifest.json') $result
    Write-Host "BLOCKED: $($result.reason)" -ForegroundColor Red
    exit 2
}

$sourceDir = Join-Path $script:GameRoot 'manosaba_Data\StreamingAssets\aa'
$targetDir = Join-Path $script:UnityProject 'Assets\StreamingAssets\shipped-aa'
if (-not (Test-Path $sourceDir)) {
    $result.status = 'BLOCKED'
    $result.reason = "shipped Addressables folder not found: $sourceDir"
    Write-Json (Join-Path $script:UnityDataRoot 'streamingassets_manifest.json') $result
    Write-Host "BLOCKED: $($result.reason)" -ForegroundColor Red
    exit 2
}

New-Item -ItemType Directory -Force -Path $targetDir | Out-Null

foreach ($name in @('catalog.bin', 'catalog.hash', 'settings.json')) {
    $src = Join-Path $sourceDir $name
    $dst = Join-Path $targetDir $name
    $entry = [ordered]@{ name = $name; source = $src; target = $dst; exists = (Test-Path $src) }
    if ($entry.exists) {
        $entry.source_bytes = (Get-Item $src).Length
        $entry.sha256 = (Get-FileHash $src -Algorithm SHA256).Hash.ToLower()
        if ($WhatIf) {
            $entry.action = 'would copy'
        } else {
            Copy-Item -Path $src -Destination $dst -Force
            $entry.action = 'copied'
            $entry.target_bytes = (Get-Item $dst).Length
        }
    } else {
        $entry.action = 'missing'
    }
    $result.files += $entry
}

$result.status = if ($result.files | Where-Object { $_.action -eq 'copied' -or $_.action -eq 'would copy' }) { 'OK' } else { 'NOTHING_COPIED' }
Write-Json (Join-Path $script:UnityDataRoot 'streamingassets_manifest.json') $result

foreach ($f in $result.files) {
    Write-Host ("  {0,-16} {1,-12} {2,10} bytes  {3}" -f $f.name, $f.action, $(if ($f.source_bytes) { $f.source_bytes } else { 0 }), $f.target)
}
Write-Host "apply_shipped_aa: $($result.status) -> $targetDir"
exit 0

