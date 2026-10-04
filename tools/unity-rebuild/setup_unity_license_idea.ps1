# Unity Hub license helper for IDEA/PowerShell 5.1.
#
# Personal users should use the Hub login button.  The optional access-token
# field is for a collaborator who was explicitly given a Unity Editor access
# token; it is encrypted with Windows DPAPI for the current user and never
# written to the repository or displayed after saving.
[CmdletBinding()]
param(
    [string]$ProjectPath = ''
)

$ErrorActionPreference = 'Stop'

$repo = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../..'))
if ([string]::IsNullOrWhiteSpace($ProjectPath)) {
    $ProjectPath = Join-Path $repo 'UnityProject'
}
$project = [IO.Path]::GetFullPath($ProjectPath)
$helperPath = Join-Path $PSScriptRoot 'unity_license_helpers.ps1'
$launcherPath = Join-Path $PSScriptRoot 'run_unity_game_idea.ps1'

# The helper is supplied by the repository and is intentionally optional while
# the UI is being used on a checkout that has not pulled the latest changes.
if (Test-Path -LiteralPath $helperPath -PathType Leaf) {
    . $helperPath
}

Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
[System.Windows.Forms.Application]::EnableVisualStyles()

function Test-FunctionAvailable {
    param([Parameter(Mandatory = $true)][string]$Name)
    return $null -ne (Get-Command -Name $Name -CommandType Function -ErrorAction SilentlyContinue)
}

function Get-UnityHubPath {
    if (Test-FunctionAvailable 'Get-UnityHubExecutable') {
        try {
            $candidate = & Get-UnityHubExecutable
            if ($candidate -and (Test-Path -LiteralPath ([string]$candidate) -PathType Leaf)) {
                return [IO.Path]::GetFullPath([string]$candidate)
            }
        } catch { }
    }

    $candidates = @()
    if ($env:ProgramFiles) {
        $candidates += (Join-Path $env:ProgramFiles 'Unity Hub/Unity Hub.exe')
    }
    if (${env:ProgramFiles(x86)}) {
        $candidates += (Join-Path ${env:ProgramFiles(x86)} 'Unity Hub/Unity Hub.exe')
    }
    if ($env:LOCALAPPDATA) {
        $candidates += (Join-Path $env:LOCALAPPDATA 'Programs/Unity Hub/Unity Hub.exe')
    }
    foreach ($candidate in $candidates) {
        if (Test-Path -LiteralPath $candidate -PathType Leaf) {
            return [IO.Path]::GetFullPath($candidate)
        }
    }
    return $null
}

function Get-HubProcessCount {
    try {
        $processes = @(Get-Process -ErrorAction SilentlyContinue | Where-Object {
            $_.ProcessName -match '^Unity ?Hub$'
        })
        return $processes.Count
    } catch {
        return 0
    }
}

function Get-LicensePipeSummary {
    $pipe = $null
    if (Test-FunctionAvailable 'Get-UnityHubLicensePipe') {
        try { $pipe = & Get-UnityHubLicensePipe } catch { $pipe = $null }
    }

    # The helper may return a string, an object, or no value.  Do not display
    # arbitrary output because a future implementation could contain a token.
    $pipeName = $null
    if ($pipe -is [string]) {
        $pipeName = [string]$pipe
    } elseif ($null -ne $pipe) {
        foreach ($propertyName in @('Name', 'PipeName', 'Path')) {
            $property = $pipe.PSObject.Properties[$propertyName]
            if ($property -and $property.Value) {
                $pipeName = [string]$property.Value
                break
            }
        }
    }

    if ($pipeName) {
        # Keep the diagnostic useful while removing any accidental query/token
        # portion from a helper result.
        $leaf = [IO.Path]::GetFileName($pipeName)
        if ($leaf -match '^(Unity-)?LicenseClient-[A-Za-z0-9_-]+$') {
            return "License IPC 通道已发现（$leaf）"
        }
        if ($leaf -match '(Unity-)?LicenseClient') {
            return 'License IPC 通道已发现'
        }
        return '已查询 License IPC 通道'
    }

    try {
        $pipeEntries = @([IO.Directory]::GetFiles('\\.\pipe\') | Where-Object {
            [IO.Path]::GetFileName($_) -like 'Unity-LicenseClient*'
        })
        if ($pipeEntries.Count -gt 0) {
            return 'License IPC 通道已发现'
        }
    } catch { }
    return '尚未发现 License IPC 通道（请先打开 Unity Hub）'
}

function Get-LicenseReadySummary {
    # The CLI helper only returns a fixed, token-free summary.  Keep the
    # fallback for older checkouts that have not pulled that helper yet.
    if (Test-FunctionAvailable 'Get-UnityLicenseStatusSummary') {
        try {
            $summary = & Get-UnityLicenseStatusSummary
            if ($summary) { return [string]$summary }
        } catch { }
    }
    if (Test-FunctionAvailable 'Test-UnityHubLicenseReady') {
        try {
            $result = & Test-UnityHubLicenseReady
            if ($result -is [bool] -and $result) {
                return 'Hub 授权通道报告可用；启动器日志仍需最终确认。'
            }
        } catch { }
    }
    return '尚未确认 Unity 登录；请在 Hub 中登录 Unity。'
}

function Start-HubForLicense {
    if (Test-FunctionAvailable 'Start-UnityHubForLicense') {
        try {
            $command = Get-Command -Name Start-UnityHubForLicense -CommandType Function
            if ($command.Parameters.ContainsKey('WaitSeconds')) {
                $null = & Start-UnityHubForLicense -WaitSeconds 2
            } else {
                $null = & Start-UnityHubForLicense
            }
        } catch { }
    }

    $hubPath = Get-UnityHubPath
    if (-not $hubPath) {
        throw '找不到 Unity Hub。请安装 Unity Hub，或在系统中修复 Unity Hub 安装。'
    }
    # The helper warms up the licensing broker.  Starting the executable again
    # asks the existing Hub instance to show its normal interactive window.
    $null = Start-Process -FilePath $hubPath -WorkingDirectory (Split-Path -Parent $hubPath)
    return $true
}

function Start-GameLauncher {
    if (-not (Test-Path -LiteralPath $launcherPath -PathType Leaf)) {
        throw "找不到游戏启动脚本：$launcherPath"
    }
    $powershell = Join-Path $env:SystemRoot 'System32/WindowsPowerShell/v1.0/powershell.exe'
    if (-not (Test-Path -LiteralPath $powershell -PathType Leaf)) {
        $powershell = 'powershell.exe'
    }
    function Convert-LauncherArgument {
        param([Parameter(Mandatory = $true)][string]$Value)
        if (Test-FunctionAvailable 'ConvertTo-WindowsArgument') {
            try {
                return [string](& ConvertTo-WindowsArgument $Value)
            } catch { }
        }
        # Start-Process on Windows PowerShell 5.1 joins ArgumentList before
        # creating the child command line, so quote paths containing spaces.
        if ($Value -notmatch '[\s"]') { return $Value }
        return '"' + $Value.Replace('"', '\"') + '"'
    }
    $arguments = @(
        '-NoProfile',
        '-ExecutionPolicy', 'Bypass',
        '-File', (Convert-LauncherArgument $launcherPath),
        '-ProjectPath', (Convert-LauncherArgument $project)
    )
    return Start-Process -FilePath $powershell -ArgumentList $arguments -WorkingDirectory $repo -PassThru
}

$form = New-Object System.Windows.Forms.Form
$form.Text = 'Unity 授权设置（IDEA）'
$form.StartPosition = 'CenterScreen'
$form.ClientSize = New-Object System.Drawing.Size(640, 430)
$form.MinimumSize = New-Object System.Drawing.Size(640, 430)

$title = New-Object System.Windows.Forms.Label
$title.AutoSize = $true
$title.Font = New-Object System.Drawing.Font('Microsoft YaHei UI', 12, [System.Drawing.FontStyle]::Bold)
$title.Location = New-Object System.Drawing.Point(16, 14)
$title.Text = 'Unity Personal 授权设置'
$form.Controls.Add($title)

$info = New-Object System.Windows.Forms.TextBox
$info.Multiline = $true
$info.ReadOnly = $true
$info.BorderStyle = [System.Windows.Forms.BorderStyle]::None
$info.BackColor = $form.BackColor
$info.Location = New-Object System.Drawing.Point(18, 48)
$info.Size = New-Object System.Drawing.Size(600, 65)
$info.Text = "请在 Unity Hub 中登录你自己的 Unity 账号并取得 Personal 授权。`r`nPersonal 通常不需要手动令牌；如协作者确实收到 Editor access token，可在下方输入并加密保存。`r`n登录或保存后可检查通道，再用启动游戏按钮运行 IDEA 启动器。"
$form.Controls.Add($info)

$loginButton = New-Object System.Windows.Forms.Button
$loginButton.Location = New-Object System.Drawing.Point(18, 128)
$loginButton.Size = New-Object System.Drawing.Size(170, 32)
$loginButton.Text = '获取授权 / 登录 Unity'
$form.Controls.Add($loginButton)

$checkButton = New-Object System.Windows.Forms.Button
$checkButton.Location = New-Object System.Drawing.Point(198, 128)
$checkButton.Size = New-Object System.Drawing.Size(130, 32)
$checkButton.Text = '检查授权通道'
$form.Controls.Add($checkButton)

$gameButton = New-Object System.Windows.Forms.Button
$gameButton.Location = New-Object System.Drawing.Point(338, 128)
$gameButton.Size = New-Object System.Drawing.Size(130, 32)
$gameButton.Text = '启动 Unity 游戏'
$form.Controls.Add($gameButton)

$docsLink = New-Object System.Windows.Forms.LinkLabel
$docsLink.AutoSize = $true
$docsLink.Location = New-Object System.Drawing.Point(486, 136)
$docsLink.Text = '官方激活说明'
$form.Controls.Add($docsLink)

$tokenLabel = New-Object System.Windows.Forms.Label
$tokenLabel.AutoSize = $true
$tokenLabel.Location = New-Object System.Drawing.Point(18, 174)
$tokenLabel.Text = '可选 Editor access token（当前用户 DPAPI 加密）：'
$form.Controls.Add($tokenLabel)

$tokenBox = New-Object System.Windows.Forms.TextBox
$tokenBox.Location = New-Object System.Drawing.Point(18, 196)
$tokenBox.Size = New-Object System.Drawing.Size(390, 24)
$tokenBox.UseSystemPasswordChar = $true
$form.Controls.Add($tokenBox)

$saveTokenButton = New-Object System.Windows.Forms.Button
$saveTokenButton.Location = New-Object System.Drawing.Point(418, 194)
$saveTokenButton.Size = New-Object System.Drawing.Size(92, 28)
$saveTokenButton.Text = '保存令牌'
$form.Controls.Add($saveTokenButton)

$clearTokenButton = New-Object System.Windows.Forms.Button
$clearTokenButton.Location = New-Object System.Drawing.Point(516, 194)
$clearTokenButton.Size = New-Object System.Drawing.Size(92, 28)
$clearTokenButton.Text = '清除令牌'
$form.Controls.Add($clearTokenButton)

$statusLabel = New-Object System.Windows.Forms.Label
$statusLabel.AutoSize = $true
$statusLabel.Location = New-Object System.Drawing.Point(18, 238)
$statusLabel.Text = '状态：等待操作。'
$form.Controls.Add($statusLabel)

$statusBox = New-Object System.Windows.Forms.TextBox
$statusBox.Multiline = $true
$statusBox.ReadOnly = $true
$statusBox.ScrollBars = [System.Windows.Forms.ScrollBars]::Vertical
$statusBox.Location = New-Object System.Drawing.Point(18, 264)
$statusBox.Size = New-Object System.Drawing.Size(600, 135)
$statusBox.Font = New-Object System.Drawing.Font('Consolas', 9)
$form.Controls.Add($statusBox)

$script:gameProcess = $null
$timer = New-Object System.Windows.Forms.Timer
$timer.Interval = 1000
$timer.Add_Tick({
    if ($null -eq $script:gameProcess) { return }
    try {
        if ($script:gameProcess.HasExited) {
            $exitCode = $script:gameProcess.ExitCode
            $statusBox.AppendText("`r`n[$(Get-Date -Format 'HH:mm:ss')] 启动器已退出，代码 $exitCode。请查看 tools/unity-rebuild/out/logs/idea_unity_editor.log。")
            $script:gameProcess = $null
            $timer.Stop()
        }
    } catch { }
})

$loginButton.Add_Click({
    try {
        Start-HubForLicense | Out-Null
        $statusLabel.Text = '状态：Unity Hub 已打开。'
        $statusBox.Text = "请在 Hub 中登录 Unity 账号，并在许可证页面选择 Personal。`r`n登录完成后点击检查授权通道；凭据由 Hub 保存。"
    } catch {
        $statusLabel.Text = '状态：无法打开 Unity Hub。'
        $statusBox.Text = $_.Exception.Message
    }
})

$saveTokenButton.Add_Click({
    try {
        if (-not (Test-FunctionAvailable 'Set-UnityAccessToken')) {
            throw '授权 helper 不完整，请先同步仓库。'
        }
        if ([string]::IsNullOrWhiteSpace($tokenBox.Text)) {
            throw '令牌为空；Personal 用户请直接点击“获取授权 / 登录 Unity”。'
        }
        $secureToken = ConvertTo-SecureString -String $tokenBox.Text -AsPlainText -Force
        Set-UnityAccessToken -Token $secureToken
        $tokenBox.Clear()
        $statusLabel.Text = '状态：令牌已为当前 Windows 用户加密保存。'
        $statusBox.Text = '令牌不会写入仓库，也不会在界面中回显。启动游戏时仅在本机进程内使用。'
    } catch {
        $statusLabel.Text = '状态：令牌保存失败。'
        $statusBox.Text = $_.Exception.Message
    }
})

$clearTokenButton.Add_Click({
    try {
        if (Test-FunctionAvailable 'Clear-UnityAccessToken') { Clear-UnityAccessToken }
        $tokenBox.Clear()
        $statusLabel.Text = '状态：已清除本机令牌。'
        $statusBox.Text = '之后可使用 Unity Hub 登录授权。'
    } catch {
        $statusLabel.Text = '状态：令牌清除失败。'
        $statusBox.Text = $_.Exception.Message
    }
})

$checkButton.Add_Click({
    try {
        $hubCount = Get-HubProcessCount
        $pipeSummary = Get-LicensePipeSummary
        $readySummary = Get-LicenseReadySummary
        $statusLabel.Text = '状态：已完成本机通道诊断。'
        $statusBox.Text = @(
            "Unity Hub 进程：$hubCount 个",
            $pipeSummary,
            $readySummary,
            '',
            '这只是 IPC/Hub 诊断，不等同于 Editor 已取得许可证。请点击启动 Unity 游戏，以启动器日志作为最终结果。'
        ) -join "`r`n"
    } catch {
        $statusLabel.Text = '状态：诊断失败。'
        $statusBox.Text = $_.Exception.Message
    }
})

$gameButton.Add_Click({
    try {
        if ($null -ne $script:gameProcess -and -not $script:gameProcess.HasExited) {
            $statusLabel.Text = "状态：启动器已经运行（PID $($script:gameProcess.Id)）。"
            return
        }
        $script:gameProcess = Start-GameLauncher
        $statusLabel.Text = "状态：已启动 IDEA 游戏启动器（PID $($script:gameProcess.Id)）。"
        $statusBox.Text = '启动器正在等待 Unity Editor 和游戏结果。`r`n授权错误会显示在启动器窗口与 idea_unity_editor.log。'
        $timer.Start()
    } catch {
        $statusLabel.Text = '状态：无法启动游戏。'
        $statusBox.Text = $_.Exception.Message
    }
})

$docsLink.Add_LinkClicked({
    try {
        Start-Process 'https://support.unity.com/hc/en-us/articles/29616668287124-How-do-I-activate-my-license-in-the-Unity-Hub' | Out-Null
    } catch {
        $statusBox.Text = '无法打开浏览器，请访问 Unity Support 的 Unity Hub license activation 说明。'
    }
})

$form.Add_FormClosed({
    $timer.Stop()
    $timer.Dispose()
})

[System.Windows.Forms.Application]::Run($form)
