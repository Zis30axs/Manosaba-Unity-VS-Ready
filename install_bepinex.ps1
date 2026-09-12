param(
    [switch]$Uninstall,
    [string]$GameDir = 'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game',
    [string]$PayloadDir = 'E:\manosaba_re\tools\bepinex'
)

$ErrorActionPreference = 'Stop'
$items = @('winhttp.dll', 'doorstop_config.ini', '.doorstop_version', 'changelog.txt', 'BepInEx', 'mono')

if ($Uninstall) {
    foreach ($i in $items) {
        $p = Join-Path $GameDir $i
        if (Test-Path $p) { Remove-Item $p -Recurse -Force; Write-Host "removed $i" }
    }
    Write-Host 'BepInEx removed - the game is back to vanilla.'
    return
}

if (-not (Test-Path $GameDir)) { throw "game dir not found: $GameDir" }
if (-not (Test-Path $PayloadDir)) { throw "bepinex payload not found: $PayloadDir" }

foreach ($i in $items) {
    $src = Join-Path $PayloadDir $i
    if (Test-Path $src) {
        Copy-Item $src $GameDir -Recurse -Force
        Write-Host "installed $i"
    }
}
Write-Host 'Done. The first game launch generates proxy assemblies under BepInEx\Il2CppAssemblies.'