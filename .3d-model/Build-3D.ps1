param([switch]$Rebuild, [switch]$NoLaunch, [switch]$Verify)
$ErrorActionPreference = 'Stop'
$taskRoot = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '..'))
$taskProject = Join-Path $taskRoot 'UnityProject'
$taskPlayer = Join-Path $taskRoot 'dist/3d-prototype/Manosaba-Cell3D.exe'
try {
    if ($Rebuild -or -not (Test-Path -LiteralPath $taskPlayer)) {
        $taskPython = $env:MANOSABA_PYTHON
        if (-not $taskPython) {
            $taskLocalPython = Join-Path $taskRoot 'tools/venv/Scripts/python.exe'
            if (Test-Path -LiteralPath $taskLocalPython) { $taskPython = $taskLocalPython }
            else { $taskPython = (Get-Command python.exe -ErrorAction Stop).Source }
        }
        Push-Location $taskRoot
        try {
            & $taskPython 'tools/three-d/convert_models.py' --root $taskRoot
            if ($LASTEXITCODE -ne 0) { throw 'Model conversion failed.' }
            & $taskPython 'tools/three-d/extract_story.py'
            if ($LASTEXITCODE -ne 0) { throw 'Original story extraction failed.' }
        } finally { Pop-Location }
        $taskUnity = $env:MANOSABA_UNITY_EDITOR
        if (-not $taskUnity) {
            $taskVersionText = Get-Content -LiteralPath (Join-Path $taskProject 'ProjectSettings/ProjectVersion.txt') -Raw
            $taskVersion = [regex]::Match($taskVersionText, 'm_EditorVersion: (\S+)').Groups[1].Value
            $taskCandidates = @(
                "E:/Unity/$taskVersion/Editor/Unity.exe",
                "D:/Unity/$taskVersion/Editor/Unity.exe",
                "C:/Program Files/Unity/Hub/Editor/$taskVersion/Editor/Unity.exe"
            )
            $taskUnity = $taskCandidates | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1
        }
        if (-not $taskUnity -or -not (Test-Path -LiteralPath $taskUnity)) {
            throw 'Set MANOSABA_UNITY_EDITOR to the Unity 6000.0.48f1 Editor executable.'
        }
        $taskEditors = Get-CimInstance Win32_Process -Filter "Name='Unity.exe'" | Where-Object {
            $_.CommandLine -and $_.CommandLine.Replace('\', '/').Contains($taskProject.Replace('\', '/'))
        }
        if ($taskEditors) { throw 'Close the Unity Editor for this project before rebuilding the standalone test.' }
        $taskLog = Join-Path $PSScriptRoot 'build.log'
        Write-Host 'Importing models, generating the prison scene, and building the 3D test...'
        $taskBuild = Start-Process -FilePath $taskUnity -WindowStyle Hidden -PassThru -Wait -ArgumentList @(
            '-batchmode', '-projectPath', ('"{0}"' -f $taskProject),
            '-executeMethod', 'Manosaba.EditorTools.CellPrototypeBuilder.CreateAndBuild',
            '-logFile', ('"{0}"' -f $taskLog)
        )
        if ($taskBuild.ExitCode -ne 0 -or -not (Test-Path -LiteralPath $taskPlayer)) {
            throw "Unity build failed. See $taskLog"
        }
    }
    if ($Verify) {
        $taskEvidence = Join-Path $PSScriptRoot 'preview'
        New-Item -ItemType Directory -Force -Path $taskEvidence | Out-Null
        $taskProbe = Start-Process -FilePath $taskPlayer -WindowStyle Hidden -PassThru -Wait -ArgumentList @(
            '-prototypeSmoke', '-prototypeEvidence', ('"{0}"' -f $taskEvidence),
            '-screen-width', '1440', '-screen-height', '900', '-screen-fullscreen', '0',
            '-logFile', ('"{0}"' -f (Join-Path $PSScriptRoot 'runtime.log'))
        )
        if ($taskProbe.ExitCode -ne 0) { throw '3D runtime verification failed. See preview/runtime-verification.json and runtime.log.' }
        Get-Content -LiteralPath (Join-Path $taskEvidence 'runtime-verification.json') -Encoding UTF8
    }
    if (-not $NoLaunch) { Start-Process -FilePath $taskPlayer -WorkingDirectory (Split-Path $taskPlayer) }
} catch {
    Write-Error $_
    exit 1
}
