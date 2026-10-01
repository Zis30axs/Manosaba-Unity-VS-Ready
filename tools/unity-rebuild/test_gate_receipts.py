"""Run the real PowerShell gate control flow with synthetic, isolated Unity receipts.

The common.ps1 copy replaces process launch, not receipt checks. These tests never
start Unity or a Player and never read/write the real editable resources.
"""
import json
from pathlib import Path
import shutil
import subprocess
import tempfile
import unittest

from imported_source import REPO


STUB_COMMON = r'''
$ErrorActionPreference = 'Stop'
$script:UnityExe = 'synthetic-editor-no-execution'
$script:UnityProject = Join-Path $PSScriptRoot 'UnityProject'
$script:UnityDataRoot = Join-Path $PSScriptRoot 'out'
$script:LogRoot = Join-Path $PSScriptRoot 'logs'
$script:CaseTest = Get-Content (Join-Path $PSScriptRoot 'case.json') -Raw | ConvertFrom-Json
New-Item -ItemType Directory -Force -Path $script:UnityProject, $script:UnityDataRoot, $script:LogRoot | Out-Null

function Write-Json {
    param([string]$Path, $Object)
    New-Item -ItemType Directory -Force -Path (Split-Path $Path) | Out-Null
    $Object | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath $Path -Encoding UTF8
}
function Case-Value {
    param([string]$Key, $Default)
    if ($script:CaseTest.PSObject.Properties.Name -contains $Key) { return $script:CaseTest.$Key }
    return $Default
}
function Write-FakeReceipt {
    param([string]$Name, [string]$Path, $Value)
    if ($Name -in @($script:CaseTest.missing)) { return }
    Write-Json $Path $Value
    if ($Name -in @($script:CaseTest.stale)) { (Get-Item -LiteralPath $Path).LastWriteTime = (Get-Date).AddDays(-1) }
}
function Smoke-Evidence {
    return @{ status = (Case-Value 'smoke_status' 'PASS'); content = @{
        status = 'LOADED'; records = @(@{ address = 'General/Data/Fixture';
            status = (Case-Value 'record_status' 'Succeeded');
            provider = (Case-Value 'provider' 'UnityEngine.ResourceManagement.ResourceProviders.BundledAssetProvider') })
    } }
}
function Restore-ContentEvidence { }
function Invoke-TmpEssentials { return [pscustomobject]@{ exit_code = 0; seconds = 0; log = $null; skipped = $true } }
function Invoke-Logged {
    param([string]$FilePath, [string[]]$Arguments, [string]$LogPath)
    Set-Content -LiteralPath $LogPath -Value 'synthetic process; no Unity launched'
    $method = $Arguments[([Array]::IndexOf($Arguments, '-executeMethod') + 1)]
    $code = 0
    switch -Wildcard ($method) {
        '*.ProjectBootstrap.Run' {
            Write-FakeReceipt 'bootstrap' (Join-Path $script:UnityDataRoot 'bootstrap_result.json') @{ status = (Case-Value 'bootstrap_status' 'OK') }
            $code = Case-Value 'bootstrap_exit' 0
        }
        '*.PlayModeGate.Run' {
            Write-FakeReceipt 'tool' (Join-Path $script:UnityDataRoot 'g4_play_mode.json') @{ status = 'PASS'; play_seconds = 1 }
            Write-FakeReceipt 'smoke' $env:MANOSABA_EVIDENCE (Smoke-Evidence)
            Write-FakeReceipt 'game' $env:MANOSABA_GAMESTART_EVIDENCE @{ status = (Case-Value 'game_status' 'STARTED') }
            $code = Case-Value 'play_exit' 0
        }
        '*.BuildPlayer.Run' {
            $exe = Join-Path $script:UnityProject 'synthetic-player.exe'
            Set-Content -LiteralPath $exe -Value 'fixture only; never executed'
            Write-FakeReceipt 'build' (Join-Path $script:UnityDataRoot 'build_player.json') @{ status = 'SUCCEEDED'; player_path = $exe; total_size_bytes = 1; total_time_seconds = 1 }
        }
        '*.AddressablesSetup.Run' {
            Write-FakeReceipt 'setup' (Join-Path $script:UnityDataRoot 'addressables_setup_result.json') @{
                status = (Case-Value 'setup_status' 'OK'); entries_planned = 1; entries_added = 1
                entries_missing_asset = (Case-Value 'missing_assets' 0); labels_unregistered = (Case-Value 'missing_labels' 0)
                entries_skipped = (Case-Value 'skipped_assets' 0); entries_excluded = (Case-Value 'excluded_assets' 0); entries_pruned = 0
            }
        }
        '*.AddressablesBuild.Run' {
            $relative = Case-Value 'build_root' 'Library/com.unity.addressables/aa/Windows'
            $root = Join-Path $script:UnityProject $relative
            New-Item -ItemType Directory -Force -Path $root | Out-Null
            Set-Content -LiteralPath (Join-Path $root 'fake.bundle') -Value 'bundle'
            $catalogs = @()
            if (-not $script:CaseTest.no_catalog) {
                $catalogName = Case-Value 'catalog_name' 'catalog.json'
                $catalog = Join-Path $root $catalogName
                Set-Content -LiteralPath $catalog -Value 'catalog'
                if (-not $script:CaseTest.unreported_catalog) {
                    $catalogs = @(@{ path = "$relative/$catalogName"; bytes = (Get-Item -LiteralPath $catalog).Length })
                }
            }
            if ($script:CaseTest.other_catalog) {
                $other = Join-Path $script:UnityProject 'Library/com.unity.addressables/aa/Android'
                New-Item -ItemType Directory -Force -Path $other | Out-Null
                Set-Content -LiteralPath (Join-Path $other 'catalog.json') -Value 'old other platform catalog'
            }
            Write-FakeReceipt 'build' (Join-Path $script:UnityDataRoot 'addressables_build_result.json') @{
                status = 'OK'; build_root = $relative; catalogs = $catalogs; bundle_count = 1; bundle_bytes = 8
                entry_count = 1; location_count = 2; duration_seconds = 1; staged_files = 0; staged_bytes = 0
            }
        }
        default { throw "Unknown synthetic method $method" }
    }
    return [pscustomobject]@{ exit_code = $code; seconds = 1; log = $LogPath; command = 'synthetic' }
}
function Start-Process {
    param([string]$FilePath, [string]$WorkingDirectory, [string]$WindowStyle, [switch]$PassThru, [string[]]$ArgumentList)
    $evidence = $ArgumentList[([Array]::IndexOf($ArgumentList, '-manosabaEvidence') + 1)]
    Write-FakeReceipt 'smoke' $evidence (Smoke-Evidence)
    Write-FakeReceipt 'game' $env:MANOSABA_GAMESTART_EVIDENCE @{ status = (Case-Value 'game_status' 'STARTED') }
    $process = [pscustomobject]@{ ExitCode = (Case-Value 'player_exit' 0) }
    $process | Add-Member -MemberType ScriptMethod -Name WaitForExit -Value { param($Timeout) return $true }
    return $process
}
'''


@unittest.skipUnless(shutil.which("pwsh") or shutil.which("powershell"), "PowerShell is unavailable")
class GateReceiptTests(unittest.TestCase):
    def setUp(self):
        self.scratch = REPO / "tools/unity-rebuild/out/gate-tests"
        self.scratch.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(dir=self.scratch)
        self.root = Path(self.temp.name).resolve()
        self.root.relative_to(self.scratch.resolve())
        self.counter = 0

    def tearDown(self):
        self.root.relative_to(self.scratch.resolve())
        self.temp.cleanup()

    def run_gate(self, gate, configuration, expected):
        self.counter += 1
        fixture = self.root / str(self.counter)
        fixture.mkdir()
        script = fixture / (gate + ".ps1")
        shutil.copy2(REPO / "tools/unity-rebuild" / script.name, script)
        (fixture / "common.ps1").write_text(STUB_COMMON, encoding="utf-8")
        (fixture / "case.json").write_text(json.dumps(configuration), encoding="utf-8")
        if gate.startswith("g3"):
            output = fixture / "out"
            output.mkdir()
            (output / "addressables_plan_all.json").write_text('{"entry_count":1,"excluded_count":0}')
        command = [shutil.which("pwsh") or shutil.which("powershell"), "-NoProfile", "-ExecutionPolicy", "Bypass", "-File", str(script)]
        if gate.startswith("g3"):
            command += ["-Group", "all"]
        process = subprocess.run(command, capture_output=True, encoding="utf-8", errors="replace", timeout=25)
        result_name = {"g3_content_build": "g3_content_build.json", "g4_play_mode": "g4_play_mode_gate.json",
                       "g5_player_smoke": "g5_player_smoke.json"}[gate]
        result_path = fixture / "out" / result_name
        self.assertTrue(result_path.is_file(), process.stdout + process.stderr)
        result = json.loads(result_path.read_text(encoding="utf-8-sig"))
        self.assertEqual(result["status"], expected, process.stdout + process.stderr)
        self.assertEqual(process.returncode == 0, expected == "PASS", process.stdout + process.stderr)
        return result

    def test_g3_current_windows_catalog_and_documented_exclusions_pass(self):
        self.run_gate("g3_content_build", {}, "PASS")
        self.run_gate("g3_content_build", {"setup_status": "OK_WITH_EXCLUSIONS", "excluded_assets": 1}, "PASS")

    def test_g3_missing_setup_assets_labels_or_skipped_assets_fail(self):
        for field in ("missing_assets", "missing_labels", "skipped_assets"):
            with self.subTest(field=field):
                self.run_gate("g3_content_build", {field: 1, "setup_status": "OK_WITH_MISSING_ASSETS"}, "FAIL")

    def test_g3_other_catalog_hash_only_and_unreported_catalog_cannot_pass(self):
        for case in ({"no_catalog": True, "other_catalog": True}, {"catalog_name": "catalog.hash"},
                     {"unreported_catalog": True}, {"build_root": "Library/com.unity.addressables/aa/Android"}):
            with self.subTest(case=case):
                self.run_gate("g3_content_build", case, "FAIL")

    def test_g4_requires_current_successful_bootstrap_and_zero_play_exit(self):
        self.run_gate("g4_play_mode", {}, "PASS")
        for case in ({"bootstrap_exit": 1}, {"stale": ["bootstrap"]}, {"play_exit": 1}):
            with self.subTest(case=case):
                self.run_gate("g4_play_mode", case, "FAIL")

    def test_g4_requires_fresh_smoke_and_started_game_evidence(self):
        for case in ({"stale": ["smoke"]}, {"stale": ["game"]}, {"missing": ["game"]},
                     {"game_status": "FAILED"}):
            with self.subTest(case=case):
                self.run_gate("g4_play_mode", case, "FAIL")

    def test_g5_requires_fresh_smoke_and_started_game_evidence(self):
        self.run_gate("g5_player_smoke", {}, "PASS")
        for case in ({"stale": ["smoke"]}, {"stale": ["game"]}, {"missing": ["game"]},
                     {"game_status": "FAILED"}, {"player_exit": 1}):
            with self.subTest(case=case):
                self.run_gate("g5_player_smoke", case, "FAIL")

    def test_play_and_player_reject_asset_database_and_failed_bundle_records(self):
        for gate in ("g4_play_mode", "g5_player_smoke"):
            for case in ({"provider": "UnityEditor.AddressableAssets.AssetDatabaseProvider"}, {"record_status": "Failed"}):
                with self.subTest(gate=gate, case=case):
                    self.run_gate(gate, case, "FAIL")


if __name__ == "__main__":
    unittest.main()
