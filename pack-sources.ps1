#Requires -Version 5.1
<#
    Export a self-contained, compiler-ready source package.

        .\pack-sources.ps1              -> dist\Manosaba.Sources.zip
        .\pack-sources.ps1 -Folder      -> keep dist\Manosaba.Sources\ as well

    The package contains only relative paths, so on any machine with .NET SDK 8+:

        dotnet build project\Manosaba.sln

    produces all 43 game assemblies (plus the BepInEx plugin project).
#>
param([switch]$Folder, [switch]$Quiet)

$Root = $PSScriptRoot
$Dist = Join-Path $Root 'dist'
$Zip = Join-Path $Dist 'Manosaba.Sources.zip'
$Tmp = Join-Path $Dist 'Manosaba.Sources'

function Info($m) { if (-not $Quiet) { Write-Host $m -ForegroundColor Cyan } }

if (Test-Path $Tmp) { Remove-Item $Tmp -Recurse -Force }
New-Item -ItemType Directory -Force -Path $Tmp, $Dist | Out-Null

Info 'collecting src\decompiled ...'
robocopy (Join-Path $Root 'src\decompiled') (Join-Path $Tmp 'src\decompiled') /E /NFL /NDL /NJH /NJS /NP | Out-Null

Info 'collecting src\il2cpp ...'
$keep = 'dump.cs', 'il2cpp.h', 'script.json', 'stringliteral.json', 'stringliterals.txt', 'config.json',
        'ghidra_with_struct_py3.py', 'ida_with_struct_py3.py'
New-Item -ItemType Directory -Force -Path (Join-Path $Tmp 'src\il2cpp') | Out-Null
foreach ($f in $keep) {
    $p = Join-Path $Root "src\il2cpp\$f"
    if (Test-Path $p) { Copy-Item $p (Join-Path $Tmp 'src\il2cpp') -Force }
}
foreach ($f in 'type_index.csv', 'type_index.md') {
    $p = Join-Path $Root "src\$f"
    if (Test-Path $p) { Copy-Item $p (Join-Path $Tmp 'src') -Force }
}
robocopy (Join-Path $Root 'src\il2cpp\DummyDll') (Join-Path $Tmp 'src\il2cpp\DummyDll') /E /NFL /NDL /NJH /NJS /NP | Out-Null

Info 'collecting project ...'
robocopy (Join-Path $Root 'project') (Join-Path $Tmp 'project') /E /XD bin obj .vs /NFL /NDL /NJH /NJS /NP | Out-Null

foreach ($f in 'build.ps1', 'build.cmd', 'build.sh', 'pack-sources.ps1', 'README.md') {
    $p = Join-Path $Root $f
    if (Test-Path $p) { Copy-Item $p $Tmp -Force }
}

New-Item -ItemType Directory -Force -Path (Join-Path $Tmp 'tools\nuget') | Out-Null
Set-Content (Join-Path $Tmp 'tools\nuget\.keep') 'NuGet cache directory (filled on first build)' -Encoding ascii

if (Test-Path $Zip) { Remove-Item $Zip -Force }
Info 'compressing ...'
Compress-Archive -Path (Join-Path $Tmp '*') -DestinationPath $Zip -CompressionLevel Optimal

$mb = [math]::Round((Get-Item $Zip).Length / 1MB, 1)
$n = (Get-ChildItem $Tmp -Recurse -File).Count
Write-Host ("package : {0}" -f $Zip) -ForegroundColor Green
Write-Host ("          {0} MB, {1} files" -f $mb, $n) -ForegroundColor Green
Write-Host 'usage   : unzip, then  dotnet build project\Manosaba.sln' -ForegroundColor Gray

if (-not $Folder) { Remove-Item $Tmp -Recurse -Force }
