"""GUID drift must never become an implicitly accepted new resource identity."""
import argparse
from contextlib import ExitStack, redirect_stderr, redirect_stdout
from io import StringIO
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import content


class MetadataIdentityTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        root = Path(self.temp.name)
        stack = ExitStack()
        self.addCleanup(stack.close)
        for name, path in {"RES": root / "res", "PROJECT": root / "UnityProject",
                           "STATE": root / "res/.state.json"}.items():
            stack.enter_context(patch.object(content, name, path))
        self.relative = "Assets/ManagedText/example.txt"
        self.source = content.RES / self.relative
        self.source.parent.mkdir(parents=True)
        self.source.write_text("edited")
        self.target = "Assets/GameContent/ManagedText/example.txt"
        self.destination = content.PROJECT / self.target
        self.destination.parent.mkdir(parents=True)
        self.destination.write_text("original")
        self.meta = Path(str(self.destination) + ".meta")
        self.original_meta = "guid: " + "a" * 32 + "\nfilterMode: 1\n"
        self.meta.write_text(self.original_meta)
        record = {"relative_path": self.relative, "category": "TEXT", "targets": [self.target],
                  "jobs": [], "dependencies": [], "editability": "FULL"}
        self.record = record
        stack.enter_context(patch.object(content, "inventory", return_value={self.relative: record}))
        stack.enter_context(redirect_stdout(StringIO()))
        stack.enter_context(redirect_stderr(StringIO()))
        self.args = argparse.Namespace(category="", file=[], force=False, dry_run=False,
                                       copy_only=False, verbose=False, timeout=30)
        self.doctor_args = argparse.Namespace(deep=True, no_report=True)

    def synchronize(self):
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}):
            self.assertEqual(content.sync(self.args), 0)

    def test_guid_drift_is_rejected_and_original_identity_survives_retries(self):
        self.synchronize()
        before = self.destination.read_bytes()
        self.meta.write_text(self.original_meta.replace("a" * 32, "b" * 32))
        self.source.write_text("another edit")
        with patch.object(content, "apply_unity") as unity:
            for _ in range(2):
                self.assertEqual(content.doctor(self.doctor_args), 1)
                self.assertEqual(content.sync(self.args), 1)
            unity.assert_not_called()
        self.assertEqual(self.destination.read_bytes(), before)
        saved = content.read_json(content.STATE)["files"][self.relative]
        self.assertEqual(saved["target_metadata"][self.target]["guid"], "a" * 32)
        self.meta.write_text(self.original_meta)
        self.synchronize()
        self.assertEqual(self.destination.read_text(), "another edit")

    def test_other_importer_settings_can_change_without_changing_guid(self):
        self.synchronize()
        self.meta.write_text(self.original_meta.replace("filterMode: 1", "filterMode: 0"))
        self.assertEqual(content.doctor(self.doctor_args), 0)
        with patch.object(content, "apply_unity") as unity:
            self.assertEqual(content.sync(self.args), 0)
            unity.assert_not_called()
        self.assertIn("filterMode: 0", self.meta.read_text())

    def test_deleting_known_metadata_cannot_generate_a_new_guid(self):
        self.synchronize()
        self.meta.unlink()
        self.source.write_text("another edit")
        before = self.destination.read_bytes()
        with patch.object(content, "apply_unity") as unity:
            self.assertEqual(content.sync(self.args), 1)
            self.assertEqual(content.sync(self.args), 1)
            unity.assert_not_called()
        self.assertEqual(self.destination.read_bytes(), before)
        saved = content.read_json(content.STATE)["files"][self.relative]
        self.assertEqual(saved["target_metadata"][self.target]["guid"], "a" * 32)
        self.assertEqual(content.doctor(self.doctor_args), 1)

    def test_unity_guid_change_cannot_be_checkpointed_as_success(self):
        def replace_guid(*_args, **_kwargs):
            self.meta.write_text(self.original_meta.replace("a" * 32, "b" * 32))
            return {"status": "OK", "jobs": []}
        with patch.object(content, "apply_unity", side_effect=replace_guid):
            with self.assertRaisesRegex(RuntimeError, "preserve metadata GUID"):
                content.sync(self.args)
        saved = content.read_json(content.STATE)["files"][self.relative]
        self.assertEqual(saved["sync_status"], "PENDING_IMPORT")
        self.assertEqual(saved["target_metadata"][self.target]["guid"], "a" * 32)
        self.assertEqual(content.doctor(self.doctor_args), 1)

    def test_invalid_metadata_is_rejected_before_target_copy(self):
        self.meta.write_text("guid: invalid\n")
        with patch.object(content, "apply_unity") as unity:
            self.assertEqual(content.sync(self.args), 1)
            unity.assert_not_called()
        self.assertEqual(self.destination.read_text(), "original")

    def test_missing_metadata_requires_import_even_when_leaf_bytes_match(self):
        self.destination.write_bytes(self.source.read_bytes())
        self.meta.unlink()
        def import_metadata(*_args, **_kwargs):
            self.meta.write_text(self.original_meta)
            return {"status": "OK", "jobs": []}
        with patch.object(content, "apply_unity", side_effect=import_metadata) as unity:
            self.assertEqual(content.sync(self.args), 0)
            unity.assert_called_once()
        self.assertEqual(content.doctor(self.doctor_args), 0)

    def test_changed_conversion_recipe_reapplies_an_unchanged_source(self):
        self.record.update(category="DATA", jobs=[{"source": "res/" + self.relative}], conversion_signature="recipe-one")
        result = {"status": "OK", "jobs": [{"source": "res/" + self.relative, "editability": "FULL", "fields": []}]}
        with patch.object(content, "apply_unity", return_value=result) as unity:
            self.assertEqual(content.sync(self.args), 0)
            self.assertEqual(content.sync(self.args), 0)
            self.assertEqual(unity.call_count, 1)
            self.record["conversion_signature"] = "recipe-two"
            self.assertEqual(content.doctor(self.doctor_args), 1)
            self.assertEqual(content.sync(self.args), 0)
            self.assertEqual(unity.call_count, 2)
            self.assertEqual(content.doctor(self.doctor_args), 0)


if __name__ == "__main__":
    unittest.main()
