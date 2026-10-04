# Local Unity authorization helpers for the IDEA launcher.
#
# These helpers deliberately do not read Unity Hub's refresh token or write an
# access token to the repository. Unity Personal is normally authorized by the
# Hub's local licensing client. A manually supplied Editor access token is an
# optional per-Windows-user value protected with DPAPI under LocalAppData.

Set-StrictMode -Version Latest

function Get-UnityHubExecutable {
    $candidates = @(
        (Join-Path ${env:ProgramFiles} 'Unity Hub/Unity Hub.exe'),
        (Join-Path ${env:LOCALAPPDATA} 'Programs/Unity Hub/Unity Hub.exe')
    )
    foreach ($candidate in $candidates) {
        if ($candidate -and (Test-Path -LiteralPath $candidate -PathType Leaf)) {
            return [IO.Path]::GetFullPath($candidate)
        }
    }
    return $null
}

function Get-UnityCliExecutable {
    $candidates = @(
        (Join-Path ${env:LOCALAPPDATA} 'Unity/bin/unity.exe'),
        (Join-Path ${env:ProgramFiles} 'Unity/Hub/Editor/unity.exe')
    )
    foreach ($candidate in $candidates) {
        if ($candidate -and (Test-Path -LiteralPath $candidate -PathType Leaf)) {
            return [IO.Path]::GetFullPath($candidate)
        }
    }
    $command = Get-Command unity.exe -ErrorAction SilentlyContinue
    if ($command) { return $command.Source }
    return $null
}

function ConvertTo-WindowsArgument {
    param([Parameter(Mandatory)][string]$Value)
    if ($Value -notmatch '[\s"]') { return $Value }
    $escaped = $Value.Replace('"', '\"')
    return '"' + $escaped + '"'
}

function Get-UnityHubLicensePipes {
    $pipes = @()
    try {
        $pipes = [IO.Directory]::GetFiles('\\.\pipe\') |
            ForEach-Object { [IO.Path]::GetFileName($_) } |
            Where-Object {
                $_ -like 'Unity-LicenseClient-*' -and
                $_ -notlike '*-notifications'
            } |
            Sort-Object -Unique
    } catch {
        return @()
    }
    return @($pipes | ForEach-Object { $_ -replace '^Unity-', '' })
}

function Get-UnityHubLicensePipe {
    return @(Get-UnityHubLicensePipes) | Select-Object -First 1
}

function Test-UnityHubLicenseReady {
    return [bool](Get-UnityHubLicensePipe)
}

function Start-UnityHubForLicense {
    param([ValidateRange(1, 90)][int]$WaitSeconds = 25)

    $existing = Get-UnityHubLicensePipe
    if ($existing) { return $existing }

    $hub = Get-UnityHubExecutable
    if ($hub) {
        try {
            # --silent starts/activates the broker without stealing focus. Login
            # itself is intentionally done by the visible Setup window.
            Start-Process -FilePath $hub -ArgumentList @('--silent') -WindowStyle Hidden | Out-Null
        } catch { }
    }

    $deadline = [DateTime]::UtcNow.AddSeconds($WaitSeconds)
    do {
        $pipe = Get-UnityHubLicensePipe
        if ($pipe) { return $pipe }
        Start-Sleep -Milliseconds 500
    } while ([DateTime]::UtcNow -lt $deadline)
    return $null
}

function Get-UnityAccessTokenPath {
    return Join-Path ${env:LOCALAPPDATA} 'ManosabaUnity/unity-editor-access-token.dpapi'
}

function Set-UnityAccessToken {
    param([Parameter(Mandatory)][Security.SecureString]$Token)
    $path = Get-UnityAccessTokenPath
    [IO.Directory]::CreateDirectory((Split-Path -Parent $path)) | Out-Null
    $encrypted = ConvertFrom-SecureString -SecureString $Token
    [IO.File]::WriteAllText($path, $encrypted, (New-Object Text.UTF8Encoding($false)))
}

function Clear-UnityAccessToken {
    $path = Get-UnityAccessTokenPath
    if (Test-Path -LiteralPath $path -PathType Leaf) {
        Remove-Item -LiteralPath $path -Force
    }
}

function Get-UnityAccessToken {
    $path = Get-UnityAccessTokenPath
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { return $null }
    try {
        $encrypted = Get-Content -LiteralPath $path -Raw -Encoding UTF8
        if ([string]::IsNullOrWhiteSpace($encrypted)) { return $null }
        $secure = ConvertTo-SecureString -String $encrypted
        $bstr = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure)
        try { return [Runtime.InteropServices.Marshal]::PtrToStringBSTR($bstr) }
        finally { [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($bstr) }
    } catch {
        return $null
    }
}

function Get-UnityLicenseStatusSummary {
    $cli = Get-UnityCliExecutable
    if (-not $cli) { return 'Unity CLI 未找到；请通过 Unity Hub 登录并激活 Personal。' }
    $status = & $cli auth status --format json --no-pager --no-banner 2>$null
    if ($LASTEXITCODE -eq 0 -and $status) {
        try {
            $json = $status | ConvertFrom-Json
            if ($json.data -and $json.data.authenticated -eq $true) {
                return 'Unity 登录已配置（凭据由 Unity 官方 broker/keyring 管理）。'
            }
        } catch { }
    }
    return '尚未确认 Unity 登录；请点击“获取授权”完成浏览器登录。'
}
