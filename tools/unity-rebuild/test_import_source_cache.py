"""Resource import safety regressions; no game assets or Unity license required."""
import os
import json
from pathlib import Path
import tempfile
from types import SimpleNamespace
import unittest
from unittest.mock import patch

from imported_source import REPO, game_root, within
from import_source_cache import import_resources
import bootstrap_import
import check_content_independence as independence
import decode_master_data


class SourceCacheTests(unittest.TestCase):
    def setUp(self):
        scratch = REPO / "tools/unity-rebuild/out/cache-tests"
        scratch.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(dir=scratch)
        self.root = Path(self.temp.name).resolve()
        self.root.relative_to(scratch.resolve())  # Every recursive cleanup remains inside the workspace.
        self.game = self.root / "game"
        self.res = self.root / "res"
        self.streaming = self.game / "manosaba_Data/StreamingAssets"
        (self.streaming / "aa/StandaloneWindows64").mkdir(parents=True)
        (self.streaming / "aa/catalog.bin").write_bytes(b"catalog-source")
        (self.streaming / "config.json").write_text('{"locale":"zh-Hans"}')
        (self.streaming / "aa/StandaloneWindows64/sample.bundle").write_bytes(b"bundle-source")
        (self.game / "manosaba_Data/resources.assets").write_bytes(b"serialized-resource")
        (self.game / "GameAssembly.dll").write_bytes(b"native-runtime")
        (self.game / "manosaba.exe").write_bytes(b"executable")
        (self.streaming / "payload.dll").write_bytes(b"not-a-resource")
        (self.streaming / "native.so").write_bytes(b"native-runtime")
        (self.streaming / "global-metadata.dat").write_bytes(b"il2cpp-metadata")
        (self.streaming / "BepInEx").mkdir()
        (self.streaming / "BepInEx/config.json").write_text('{"plugin":"runtime-hook"}')

    def tearDown(self):
        self.root.relative_to((REPO / "tools/unity-rebuild/out/cache-tests").resolve())
        self.temp.cleanup()

    def test_only_resource_inputs_are_imported(self):
        result = import_resources(self.game, self.res, False, True)
        names = {row["path"] for row in result["files"]}
        self.assertIn("manosaba_Data/resources.assets", names)
        self.assertIn("manosaba_Data/StreamingAssets/aa/StandaloneWindows64/sample.bundle", names)
        self.assertFalse(any(name.endswith((".exe", ".dll", ".so", "global-metadata.dat")) or "BepInEx" in name for name in names))
        with patch.dict(os.environ, {"MANOSABA_RES_ROOT": str(self.res), "MANOSABA_GAME_ROOT": str(self.root / "missing"), "MANOSABA_CONTENT_ONLY": "1"}):
            self.assertEqual(Path(game_root()), self.res / ".imported-source")

    def test_reimport_preserves_user_changes_until_explicit_overwrite(self):
        initial = import_resources(self.game, self.res, False, True)
        relative = "manosaba_Data/StreamingAssets/config.json"
        local = self.res / ".imported-source" / relative
        original_hash = next(row for row in initial["files"] if row["path"] == relative)["original_sha256"]
        local.write_text('{"locale":"user-edit"}')
        (self.streaming / "config.json").write_text('{"locale":"new-base"}')
        result = import_resources(self.game, self.res, False, True)
        record = next(row for row in result["files"] if row["path"] == relative)
        self.assertEqual(record["status"], "USER_MODIFIED")
        self.assertEqual(record["original_sha256"], original_hash)
        self.assertIn("user-edit", local.read_text())
        result = import_resources(self.game, self.res, True, True)
        self.assertIn("new-base", local.read_text())
        self.assertFalse(result["user_modified"])

    def test_rejects_metadata_paths_outside_cache(self):
        with self.assertRaises(ValueError):
            within(self.res, "../../GameAssembly.dll")
        with self.assertRaises(ValueError):
            within(self.res, str(self.game / "GameAssembly.dll"))

    def test_font_source_seeding_uses_tracked_identity_and_preserves_meta(self):
        project = self.root / "UnityProject"
        asset = project / "Assets/GameContent/RuntimeFonts/TsukushiMincho/TsukushiMincho.asset"
        source = project / "Assets/GameContent/#WitchTrials/Fonts/TsukushiMincho/TsukushiMincho.otf"
        asset.parent.mkdir(parents=True)
        source.parent.mkdir(parents=True)
        asset.write_text("  m_SourceFontFileGUID: " + "a" * 32 + "\n")
        source.write_bytes(b"original-font")
        with patch.object(bootstrap_import, "PROJECT", project):
            seeded = bootstrap_import.seed_font_source_guids()
            meta = source.with_suffix(".otf.meta")
            self.assertEqual(seeded, [str(meta)])
            self.assertIn("guid: " + "a" * 32, meta.read_text())
            meta.write_text("guid: " + "b" * 32 + "\n")
            self.assertEqual(bootstrap_import.seed_font_source_guids(), [])
            self.assertIn("guid: " + "b" * 32, meta.read_text())

    def test_master_data_catalog_join_prefers_shipped_evidence(self):
        shipped = self.root / "catalog_dump_shipped.json"
        generic = self.root / "catalog_dump.json"
        shipped.write_text(json.dumps({"keys": [{"key": "shipped-key"}]}))
        generic.write_text(json.dumps({"keys": [{"key": "rebuilt-key"}]}))
        with patch.object(decode_master_data, "SHIPPED_CATALOG_KEYS", str(shipped)), \
                patch.object(decode_master_data, "CATALOG_KEYS", str(generic)):
            self.assertEqual(set(decode_master_data.load_catalog_keys()["by_key"]), {"shipped-key"})
            shipped.unlink()
            self.assertEqual(set(decode_master_data.load_catalog_keys()["by_key"]), {"rebuilt-key"})

    def test_bootstrap_rejects_incomplete_success_receipts(self):
        for status in ("", "NOT_RUN", "PARSE_FAILED", "ERROR"):
            with self.subTest(status=status), self.assertRaises(RuntimeError):
                bootstrap_import.validate_unity_receipt("CatalogDump.Run", {"status": status})
        with self.assertRaises(RuntimeError):
            bootstrap_import.validate_unity_receipt("AddressablesSetup.Run", {"status": "OK_WITH_MISSING_ASSETS", "entries_missing_asset": 1})
        with self.assertRaises(RuntimeError):
            bootstrap_import.validate_unity_receipt("MaterialImporter.Run", {"status": "PARTIAL", "failed": 1}, True)
        self.assertEqual(bootstrap_import.validate_unity_receipt("MaterialImporter.Run", {"status": "PARTIAL", "failed": 0}, True), "PARTIAL")

    def test_independence_rejects_new_bepinex_access_in_nested_log(self):
        repo = self.root / "repo"
        pipeline = repo / "tools/unity-rebuild"
        pipeline.mkdir(parents=True)
        (pipeline / "g1_compile.ps1").write_text("# synthetic gate")
        logs = pipeline / "out/logs"
        logs.mkdir(parents=True)
        (logs / "historical.log").write_text("BepInEx historical research record")
        output = repo / "report"
        output.mkdir()

        def run_synthetic_gate(*_args, **_kwargs):
            artifact = pipeline / "out/unity/g1_compile.json"
            artifact.parent.mkdir(parents=True)
            artifact.write_text('{"status":"PASS"}')
            nested = logs / "child/compile.log"
            nested.parent.mkdir(parents=True)
            nested.write_text("Loaded forbidden dependency from BepInEx/core")
            return SimpleNamespace(stdout="compile completed", stderr="", returncode=0)

        with patch.object(independence, "REPO", repo), \
                patch.object(independence.shutil, "which", return_value="powershell"), \
                patch.object(independence.subprocess, "run", side_effect=run_synthetic_gate):
            result = independence.run_stage("compile", {}, output, "all", 10, None)
        self.assertEqual(result["status"], "FAIL")
        self.assertEqual(result["forbidden_access_markers"], [{"log": str(logs / "child/compile.log"), "marker": "BepInEx"}])


if __name__ == "__main__":
    unittest.main()
