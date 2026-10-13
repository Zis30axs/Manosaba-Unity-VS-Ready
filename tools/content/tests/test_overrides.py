"""Shared edits must keep a collaborator's identity and unrelated progress."""
from contextlib import redirect_stderr, redirect_stdout
import copy
import csv
import hashlib
from io import StringIO
import json
from pathlib import Path
import sys
import tempfile
import unittest

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import overrides


class OverrideTests(unittest.TestCase):
    def setUp(self):
        temporary = tempfile.TemporaryDirectory()
        self.addCleanup(temporary.cleanup)
        self.root = Path(temporary.name)
        self.source = self.root / "collaborator-res"
        self.base = self.root / "base-res"
        self.local = self.root / "local-res"
        self.path = "Assets/Prefabs/Debug~81.asset.json"
        self.remapped = "Assets/Prefabs/Debug~12.asset.json"
        self.text_path = "Assets/Localization/zh-Hans/Debug.txt"
        self.object_identity = {"type": "MonoBehaviour", "source_bundle": "StreamingAssets/aa/ui.bundle", "path_id": "1234"}
        self.original = {"m_text": "デバッグ", "m_Script": {"m_FileID": 1, "m_PathID": 987},
                         "m_GameObject": {"m_FileID": 0, "m_PathID": 555}, "m_fontSize": 24}
        self.write_source(self.base, self.path, self.original)
        self.write_source(self.local, self.path, {**self.original, "m_text": "调试"})
        self.write_source(self.source, self.remapped, {**self.original, "m_fontSize": 30, "coauthor": "advanced"})
        for root in (self.base, self.local, self.source):
            self.write_text(root, self.text_path, b"original text\n")
        self.write_text(self.local, self.text_path, "共享中文\n".encode("utf-8"))
        self.manifest = overrides.export_manifest(self.local, self.base, [self.path, self.text_path])

    def write_source(self, root, path, data):
        destination = root / path
        destination.parent.mkdir(parents=True, exist_ok=True)
        # Match the resource exporter's line-oriented JSON token layout.
        destination.write_text(json.dumps(data, indent=2), encoding="utf-8")
        root.mkdir(parents=True, exist_ok=True)
        with (root / "_index.csv").open("w", encoding="utf-8", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=["type", "source_bundle", "path_id", "output_file"])
            writer.writeheader()
            writer.writerow({**self.object_identity, "output_file": path})
        return destination

    def write_text(self, root, path, data):
        destination = root / path
        destination.parent.mkdir(parents=True, exist_ok=True)
        destination.write_bytes(data)
        return destination

    def test_preview_first_apply_remaps_identity_and_retains_coauthor_fields(self):
        target = self.source / self.remapped
        meta = target.with_suffix(target.suffix + ".meta")
        meta.write_text("guid: " + "a" * 32, encoding="utf-8")
        original = target.read_bytes()
        preview = overrides.apply_manifest(self.manifest, self.source)
        self.assertEqual(len(preview["changed"]), 2)
        self.assertEqual(target.read_bytes(), original)
        result = overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertIn(self.remapped, result["changed"])
        document = json.loads(target.read_bytes())
        self.assertEqual(document["m_text"], "调试")
        self.assertEqual(document["m_fontSize"], 30)
        self.assertEqual(document["coauthor"], "advanced")
        self.assertEqual(document["m_Script"], self.original["m_Script"])
        self.assertEqual(document["m_GameObject"], self.original["m_GameObject"])
        self.assertEqual(meta.read_text(), "guid: " + "a" * 32)
        self.assertEqual((self.source / self.text_path).read_bytes(), "共享中文\n".encode("utf-8"))
        written = target.read_bytes()
        again = overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertEqual(again["changed"], [])
        self.assertEqual(len(again["already_applied"]), 2)
        self.assertEqual(target.read_bytes(), written)

    def test_any_text_conflict_prevents_all_writes_and_preserves_newer_translation(self):
        target = self.source / self.remapped
        before = target.read_bytes()
        self.write_text(self.source, self.text_path, "协作者更新的中文\n".encode("utf-8"))
        with self.assertRaisesRegex(overrides.OverrideError, "resolve conflicts first"):
            overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertEqual(target.read_bytes(), before)
        self.assertEqual((self.source / self.text_path).read_text(encoding="utf-8"), "协作者更新的中文\n")

    def test_same_field_conflict_preserves_every_resource(self):
        target = self.write_source(self.source, self.remapped, {**self.original, "m_text": "协作者新版调试"})
        before = target.read_bytes()
        with self.assertRaisesRegex(overrides.OverrideError, "m_text differs"):
            overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertEqual(target.read_bytes(), before)
        self.assertEqual((self.source / self.text_path).read_bytes(), b"original text\n")

    def test_missing_identity_refuses_same_named_object_from_a_different_version(self):
        target = self.source / self.path
        target.parent.mkdir(parents=True, exist_ok=True)
        target.write_text(json.dumps(self.original), encoding="utf-8")
        (self.source / "_index.csv").unlink()
        with self.assertRaisesRegex(overrides.OverrideError, "0 matches"):
            overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertEqual(json.loads(target.read_bytes())["m_text"], "デバッグ")

    def test_identity_fields_cannot_be_injected(self):
        for field in ("m_Script", "m_GameObject", "m_Name", "m_text/nested", "__proto__"):
            with self.subTest(field=field):
                manifest = copy.deepcopy(self.manifest)
                manifest["entries"][0]["fields"][field] = {"before": "", "after": "injected"}
                with self.assertRaisesRegex(overrides.OverrideError, "Only the m_text"):
                    overrides.apply_manifest(manifest, self.source, apply=True)

    def test_traversal_and_metadata_paths_are_rejected(self):
        for path in ("Assets/../../outside.txt", "C:/outside.txt", "/outside.txt", "Assets/file.meta", "_index.csv", "Assets//file.txt"):
            with self.subTest(path=path):
                manifest = copy.deepcopy(self.manifest)
                manifest["entries"][1]["path"] = path
                with self.assertRaises(overrides.OverrideError):
                    overrides.apply_manifest(manifest, self.source, apply=True)

    def test_symlink_cannot_redirect_an_edit_outside_or_to_local_metadata(self):
        target = self.source / self.text_path
        target.unlink()
        outside = self.root / "outside.txt"
        outside.write_bytes(b"original text\n")
        try:
            target.symlink_to(outside)
        except OSError as exc:
            self.skipTest(f"Symlink creation is unavailable: {exc}")
        with self.assertRaisesRegex(overrides.OverrideError, "symbolic links"):
            overrides.apply_manifest(self.manifest, self.source, apply=True)
        self.assertEqual(outside.read_bytes(), b"original text\n")

    def test_export_refuses_unreviewed_other_fields_and_null_baseline(self):
        self.write_source(self.local, self.path, {**self.original, "m_text": "调试", "m_fontSize": 60})
        with self.assertRaisesRegex(overrides.OverrideError, "outside supported m_text"):
            overrides.export_manifest(self.local, self.base, [self.path])
        manifest = copy.deepcopy(self.manifest)
        manifest["entries"][1]["before_sha256"] = None
        with self.assertRaisesRegex(overrides.OverrideError, "real baseline"):
            overrides.apply_manifest(manifest, self.source, apply=True)

    def test_cli_export_and_apply_require_explicit_write_flag(self):
        output = self.root / "overrides.json"
        with redirect_stdout(StringIO()), redirect_stderr(StringIO()):
            export = ["export", "--source-root", str(self.local), "--base-root", str(self.base),
                      "--file", self.path, "--output", str(output)]
            self.assertEqual(overrides.main(export), 0)
            self.assertFalse(output.exists())
            self.assertEqual(overrides.main([*export, "--apply"]), 0)
            self.assertTrue(output.exists())
            self.assertEqual(overrides.main([*export, "--apply"]), 1)
            apply = ["apply", str(output), "--source-root", str(self.source)]
            original = (self.source / self.remapped).read_bytes()
            self.assertEqual(overrides.main(apply), 0)
            self.assertEqual((self.source / self.remapped).read_bytes(), original)
            self.assertEqual(overrides.main([*apply, "--apply"]), 0)
        self.assertEqual(json.loads((self.source / self.remapped).read_bytes())["m_text"], "调试")


if __name__ == "__main__":
    unittest.main()
