#Requires -Version 5.1
<#
    一键构建（只需要 .NET SDK 8/9，不需要 IDE、不需要联网）

        .\build.ps1                        # Debug
        .\build.ps1 -Configuration Release
        .\build.ps1 -Clean                 # 先清 bin/obj 再全量重建
        .\build.ps1 -Pack                  # 额外导出 dist\Manosaba.Sources.zip（纯源码包）

    产物：dist\<程序集>.dll —— 43 个游戏程序集 + BepInEx 插件
#>
param(
    [ValidateSet('Debug','Release')][string]$Configuration = 'Debug',
    [switch]$Clean,
    [switch]$Pack,
    [switch]$Quiet
)

[Console]::OutputEncoding = [Text.Encoding]::UTF8
$Root   = $PSScriptRoot
$Sln    = Join-Path $Root 'project\Manosaba.sln'
$OutDir = Join-Path $Root 'dist'

function Info($m) { if (-not $Quiet) { Write-Host $m -ForegroundColor Cyan } }
function Bad($m)  { Write-Host $m -ForegroundColor Red }

# 1. 找编译器
$dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
if (-not $dotnet) {
    Bad '找不到 dotnet，请安装 .NET SDK 8.0+：https://dotnet.microsoft.com/download'
    exit 1
}
$sdks = (& dotnet --list-sdks 2>$null) -join "`n"
if ([string]::IsNullOrWhiteSpace($sdks)) {
    Bad '只装了 .NET 运行时，需要 .NET SDK'
    exit 1
}
Info ("dotnet " + (& dotnet --version).Trim())

# 2. 环境（NuGet 缓存留在本目录，不写 C 盘）
$env:NUGET_PACKAGES = Join-Path $Root 'tools\nuget'
$env:DOTNET_CLI_TELEMETRY_OPTOUT = '1'
$env:DOTNET_NOLOGO = '1'
New-Item -ItemType Directory -Force -Path $env:NUGET_PACKAGES | Out-Null
Info ("NUGET_PACKAGES = " + $env:NUGET_PACKAGES)

# 3. 清理
if ($Clean) {
    Info '清理 bin/obj ...'
    Get-ChildItem (Join-Path $Root 'project\src') -Directory | ForEach-Object {
        foreach ($d in 'bin','obj') {
            $p = Join-Path $_.FullName $d
            if (Test-Path $p) { Remove-Item $p -Recurse -Force -ErrorAction SilentlyContinue }
        }
    }
}

# 4. 构建
Info ("构建 " + $Sln)
$log = & dotnet build $Sln -c $Configuration --nologo 2>&1
$code = $LASTEXITCODE
New-Item -ItemType Directory -Force -Path (Join-Path $Root 'work') | Out-Null
$log | Out-File (Join-Path $Root 'work\build_last.log') -Encoding utf8
if ($code -ne 0) {
    $log | Select-String -Pattern ': error ' | Select-Object -First 10 | ForEach-Object { Write-Host $_.Line -ForegroundColor Red }
    Bad '构建失败，完整日志见 work\build_last.log'
    exit $code
}

# 5. 收集产物
if (Test-Path $OutDir) { Remove-Item $OutDir -Recurse -Force }
New-Item -ItemType Directory -Path $OutDir | Out-Null
Get-ChildItem (Join-Path $Root 'project\src') -Recurse -File -Filter *.dll |
    Where-Object { $_.FullName -match ('\\bin\\' + $Configuration + '\\') } |
    ForEach-Object { Copy-Item $_.FullName $OutDir -Force }

Write-Host ("构建成功：{0} 个程序集 -> {1}" -f (Get-ChildItem $OutDir -File).Count, $OutDir) -ForegroundColor Green
Get-ChildItem $OutDir -File | Sort-Object Name |
    Select-Object Name, @{n='KB';e={[math]::Round($_.Length/1KB,1)}} | Format-Table -AutoSize

# 6. 可选：打包纯源码
if ($Pack) { & (Join-Path $Root 'pack-sources.ps1') -Quiet:$Quiet }