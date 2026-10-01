# Stage imported runtime metadata without consulting the original game install.
# import_source_cache.py captures these files once. Rebuilt catalog/bundles remain
# the runtime source in Library/com.unity.addressables; shipped-aa is evidence for
# the existing standalone catalog smoke probe.
[CmdletBinding()]
param([switch]$WhatIf)

. "$PSScriptRoot\common.ps1"

$result = [ordered]@{
    gate = 'apply_imported_streamingassets'
    generated = (Get-Date).ToUniversalTime().ToString('o')
    source_cache = $script:ImportedSourceRoot
    status = 'NOT_RUN'
    reason = $null
    files = @()
}
$sourceStreaming = Join-Path $script:ImportedSourceRoot 'manosaba_Data\StreamingAssets'
$sourceAa = Join-Path $sourceStreaming 'aa'
$targetStreaming = Join-Path $script:UnityProject 'Assets\StreamingAssets'
$targetAa = Join-Path $targetStreaming 'shipped-aa'
$missing = @(@('catalog.bin', 'settings.json') | Where-Object { -not (Test-Path -LiteralPath (Join-Path $sourceAa $_) -PathType Leaf) })
if ($missing.Count -gt 0) {
    $result.status = 'BLOCKED'
    $result.reason = "Imported runtime metadata is missing ($($missing -join ', ')). Run tools/content/import.ps1 once."
    Write-Json (Join-Path $script:UnityDataRoot 'streamingassets_manifest.json') $result
    Write-Host "BLOCKED: $($result.reason)" -ForegroundColor Red
    exit 2
}

$sources = @()
foreach ($name in @('catalog.bin', 'catalog.hash', 'settings.json')) {
    $source = Join-Path $sourceAa $name
    if (Test-Path -LiteralPath $source -PathType Leaf) {
        $sources += [pscustomobject]@{ source = $source; target = Join-Path $targetAa $name; name = "shipped-aa/$name" }
    }
}
foreach ($file in Get-ChildItem -LiteralPath $sourceStreaming -File) {
    if ($file.Extension -in @('.exe', '.dll', '.ps1', '.bat', '.cmd', '.com', '.asi', '.pyd', '.so', '.dylib') -or
        $file.Name -eq 'global-metadata.dat') { continue }
    $sources += [pscustomobject]@{ source = $file.FullName; target = Join-Path $targetStreaming $file.Name; name = $file.Name }
}
foreach ($file in $sources) {
    $sha = (Get-FileHash -LiteralPath $file.source -Algorithm SHA256).Hash.ToLowerInvariant()
    $same = (Test-Path -LiteralPath $file.target -PathType Leaf) -and
            (Get-FileHash -LiteralPath $file.target -Algorithm SHA256).Hash.ToLowerInvariant() -eq $sha
    $action = if ($same) { 'unchanged' } elseif ($WhatIf) { 'would copy' } else { 'copied' }
    if (-not $same -and -not $WhatIf) {
        New-Item -ItemType Directory -Force -Path (Split-Path $file.target) | Out-Null
        Copy-Item -LiteralPath $file.source -Destination $file.target -Force
    }
    $result.files += [ordered]@{
        name = $file.name; source = $file.source; target = $file.target
        source_bytes = (Get-Item -LiteralPath $file.source).Length; sha256 = $sha; action = $action
    }
    Write-Host "  $($file.name): $action"
}
$result.status = 'OK'
Write-Json (Join-Path $script:UnityDataRoot 'streamingassets_manifest.json') $result
Write-Host "apply_shipped_aa: OK (imported cache) -> $targetStreaming"
exit 0
