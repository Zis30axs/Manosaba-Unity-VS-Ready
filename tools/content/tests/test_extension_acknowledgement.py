"""A gallery extension updates shared output stamps without acknowledging sources."""
import argparse
from contextlib import ExitStack, redirect_stdout
from copy import deepcopy
from io import StringIO
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import content


class ExtensionAcknowledgementTests(unittest.TestCase):
    def setUp(self):
        temporary = tempfile.TemporaryDirectory()
        self.addCleanup(temporary.cleanup)
        self.root = Path(temporary.name)
        stack = ExitStack()
        self.addCleanup(stack.close)
        for name, path in {"REPO": self.root, "RES": self.root / "res",
                           "PROJECT": self.root / "UnityProject", "STATE": self.root / "res/state.json",
                           "PIPELINE": self.root / "tools/unity-rebuild"}.items():
            stack.enter_context(patch.object(content, name, path))
        stack.enter_context(patch.object(content, "refresh_plans"))
        stack.enter_context(redirect_stdout(StringIO()))
        self.target = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/GalleryUI.prefab"
        self.destination = content.PROJECT / self.target
        self.destination.parent.mkdir(parents=True)
        self.destination.write_text("original gallery", encoding="utf-8")
        self.meta = Path(str(self.destination) + ".meta")
        self.meta.write_text("guid: " + "a" * 32 + "\n", encoding="utf-8")
        stamp = content.fingerprint(self.destination)
        metadata = content.metadata_identity(self.destination)
        self.files = {}
        for i in range(119):
            self.files[f"Assets/Gallery/source-{i:03}.json"] = {
                "targets": [self.target, "Assets/other.prefab"], "jobs": [],
                "source": {"size": i, "mtime_ns": i + 1, "sha256": "b" * 64},
                "conversion_signature": "local-converter", "rebuild": True,
                "sync_status": ("SYNCED", "PARTIAL", "PENDING_IMPORT", "FAILED")[i % 4],
                "field_editability": [{"field": "local-field", "editability": "READ_ONLY", "reason": "retained", "occurrences": 1}],
                "target_fingerprints": {self.target: deepcopy(stamp), "Assets/other.prefab": {"preserved": True}},
                "target_metadata": {self.target: deepcopy(metadata), "Assets/other.prefab": {"guid": "c" * 32}},
            }
        self.files["Assets/unrelated.json"] = {"targets": [], "sync_status": "FAILED"}
        self.before = deepcopy(self.files)
        self.destination.write_text("gallery with new order", encoding="utf-8")
        self.plan_path = content.RES / ".manosaba/switch-story/plan.json"
        content.write_json(self.plan_path, {"version": 1, "signature": "d" * 64,
                                           "gallery": {"target": self.target}, "scenarios": []})
        self.result = {"status": "OK", "jobs": [], "switch_story": {
            "status": "OK", "signature": "d" * 64,
            "plan": "res/.manosaba/switch-story/plan.json", "plan_sha256": content.digest(self.plan_path),
            "gallery": {"status": "OK", "target": self.target,
                        "preserved_existing_order": True, "saved_unlock_state_changed": False},
            "output_sha256": {self.target: content.digest(self.destination), self.target + ".meta": content.digest(self.meta)},
        }}
        self.args = argparse.Namespace(category="", file=[], force=False, dry_run=False,
                                       copy_only=False, verbose=False, timeout=30, player=False)

    def assert_gallery_only_updated(self, actual):
        stamp = content.fingerprint(self.destination, force=True)
        metadata = content.metadata_identity(self.destination, force=True)
        for name, before in self.before.items():
            expected = deepcopy(before)
            if self.target in before["targets"]:
                expected["target_fingerprints"][self.target] = stamp
                expected["target_metadata"][self.target] = metadata
            self.assertEqual(actual[name], expected, name)

    def test_updates_all_shared_outputs_but_preserves_source_and_pending_state(self):
        self.assertEqual(content.acknowledge_extension_targets(self.files, self.result), 119)
        self.assert_gallery_only_updated(self.files)

    def test_rejects_inconsistent_evidence_before_updating_any_record(self):
        for mismatch in ("output", "meta", "plan", "signature", "scope", "failed", "guid", "missing_guid"):
            with self.subTest(mismatch=mismatch):
                files, result = deepcopy(self.before), deepcopy(self.result)
                story = result["switch_story"]
                if mismatch in ("output", "meta"):
                    story["output_sha256"][self.target + (".meta" if mismatch == "meta" else "")] = "e" * 64
                elif mismatch == "plan":
                    story["plan_sha256"] = "e" * 64
                elif mismatch == "signature":
                    story["signature"] = "e" * 64
                elif mismatch == "scope":
                    story["gallery"]["target"] = "Assets/unrelated.prefab"
                elif mismatch == "failed":
                    result["status"] = "ERROR"
                else:
                    files["Assets/Gallery/source-118.json"]["target_metadata"][self.target]["guid"] = (
                        "e" * 32 if mismatch == "guid" else None)
                before = deepcopy(files)
                with self.assertRaises(RuntimeError):
                    content.acknowledge_extension_targets(files, result)
                self.assertEqual(files, before)

    def test_absent_or_disabled_extension_cannot_change_ledger(self):
        for result in ({"status": "OK"}, {"status": "OK", "switch_story": {"status": "disabled"}}):
            self.assertEqual(content.acknowledge_extension_targets(self.files, result), 0)
            self.assertEqual(self.files, self.before)

    def test_empty_sync_gate_persists_extension_without_converter_jobs(self):
        content.write_json(content.STATE, {"version": 1, "files": self.files})
        with patch.object(content, "inventory", return_value={}), \
                patch.object(content, "apply_unity", return_value=self.result) as unity:
            self.assertEqual(content.sync(self.args), 0)
        unity.assert_called_once_with([], [], timeout=30, ensure_curtain_skin=True)
        self.assert_gallery_only_updated(content.read_content_state(content.STATE)["files"])

    def test_successful_batch_persists_extension_without_gallery_converter_jobs(self):
        relative, target = "Assets/ordinary.txt", "Assets/GameContent/ordinary.txt"
        source = content.RES / relative
        source.parent.mkdir(parents=True)
        source.write_text("ordinary edit", encoding="utf-8")
        destination = content.PROJECT / target
        destination.write_text("previous text", encoding="utf-8")
        Path(str(destination) + ".meta").write_text("guid: " + "f" * 32 + "\n", encoding="utf-8")
        record = {"relative_path": relative, "category": "TEXT", "targets": [target], "jobs": [],
                  "dependencies": [], "editability": "FULL"}
        content.write_json(content.STATE, {"version": 1, "files": self.files})
        with patch.object(content, "inventory", return_value={relative: record}), \
                patch.object(content, "apply_unity", return_value=self.result) as unity:
            self.assertEqual(content.sync(self.args), 0)
        unity.assert_called_once_with([], [target], timeout=30, ensure_curtain_skin=True)
        saved = content.read_content_state(content.STATE)["files"]
        self.assert_gallery_only_updated(saved)
        self.assertEqual(saved[relative]["sync_status"], "SYNCED")

    def test_build_gate_persists_extension_after_ordinary_sync(self):
        content.write_json(content.STATE, {"version": 1, "files": self.files})
        with patch.object(content, "restore_evidence"), patch.object(content, "sync", return_value=0), \
                patch.object(content, "apply_unity", return_value=self.result) as unity:
            self.assertEqual(content.build(self.args), 0)
        unity.assert_called_once_with([], [], build=True, timeout=30, setup_addressables=True,
                                     addressables_plan="addressables_plan_all.json", ensure_curtain_skin=True)
        self.assert_gallery_only_updated(content.read_content_state(content.STATE)["files"])


if __name__ == "__main__":
    unittest.main()
