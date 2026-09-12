param(
    [string]$GameDir = 'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game'
)
Write-Host "launching $GameDir\manosaba.exe"
Start-Process -FilePath (Join-Path $GameDir 'manosaba.exe') -WorkingDirectory $GameDir