"""Regression checks for edit preservation, retries and generated metadata identity."""
import argparse
from contextlib import ExitStack
import csv
from pathlib import Path
import tempfile
import unittest
from unittest.mock import patch

import content
import import_workspace


class ContentTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.root = Path(self.temp.name)
        self.stack = ExitStack()
        for name, path in {"REPO": self.root, "RES": self.root / "res", "PROJECT": self.root / "UnityProject",
                           "STATE": self.root / "res/.state.json", "BASELINE": self.root / "res/.manosaba/base.json",
                           "PIPELINE": self.root / "tools/unity-rebuild"}.items():
            self.stack.enter_context(patch.object(content, name, path))
        content.RES.mkdir()
        self.relative = "Assets/#WitchTrials/ManagedText/Characters.txt"
        self.source = content.RES / self.relative
        self.source.parent.mkdir(parents=True)
        self.source.write_text("new text", encoding="utf-8")
        self.target_rel = "Assets/GameContent/#WitchTrials/ManagedText/Characters.txt"
        self.target = content.PROJECT / self.target_rel
        self.target.parent.mkdir(parents=True)
        self.target.write_text("old text", encoding="utf-8")
        self.meta = Path(str(self.target) + ".meta")
        self.meta.write_bytes(b"guid: aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\npivot: preserved\n")
        self.meta_bytes = self.meta.read_bytes()
        self.record = {"relative_path": self.relative, "category": "TEXT", "targets": [self.target_rel],
                       "jobs": [], "dependencies": [], "addressables_group": "Manosaba.Text", "editability": "FULL"}
        self.stack.enter_context(patch.object(content, "inventory", return_value={self.relative: self.record}))

    def tearDown(self):
        self.stack.close()
        self.temp.cleanup()

    def args(self, **kwargs):
        values = dict(category="", file=[], force=False, dry_run=False, copy_only=False, timeout=30, verbose=False)
        values.update(kwargs)
        return argparse.Namespace(**values)

    def test_dry_run_does_not_write_or_create_manifest(self):
        self.assertEqual(content.sync(self.args(dry_run=True)), 0)
        self.assertEqual(self.target.read_text(), "old text")
        self.assertFalse(content.STATE.exists())
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_copy_preserves_metadata_and_leaves_import_pending(self):
        self.assertEqual(content.sync(self.args(copy_only=True)), 0)
        self.assertEqual(self.target.read_bytes(), self.source.read_bytes())
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)
        self.assertEqual(content.read_json(content.STATE)["files"][self.relative]["sync_status"], "PENDING_IMPORT")

    def test_failed_unity_conversion_is_retried_after_successful_copy(self):
        with patch.object(content, "apply_unity", side_effect=RuntimeError("compile failed")):
            with self.assertRaises(RuntimeError):
                content.sync(self.args())
        self.assertEqual(content.read_json(content.STATE)["files"][self.relative]["sync_status"], "PENDING_IMPORT")
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}) as convert:
            self.assertEqual(content.sync(self.args()), 0)
            convert.assert_called_once()
        self.assertEqual(content.read_json(content.STATE)["files"][self.relative]["sync_status"], "SYNCED")
        with patch.object(content, "apply_unity") as convert:
            self.assertEqual(content.sync(self.args()), 0)
            convert.assert_not_called()

    def test_target_drift_is_repaired_without_source_edit(self):
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}):
            content.sync(self.args())
            self.target.write_text("accidental generated edit")
            content.sync(self.args())
        self.assertEqual(self.target.read_bytes(), self.source.read_bytes())
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_shared_prefab_target_does_not_invalidate_unchanged_source_objects(self):
        other_relative = "Assets/other.json"
        other = content.RES / other_relative
        other.write_text("unchanged object")
        first = {**self.record, "category": "PREFAB", "jobs": [{"source": "res/" + self.relative}]}
        second = {**first, "relative_path": other_relative, "jobs": [{"source": "res/" + other_relative}]}
        records = {self.relative: first, other_relative: second}
        def converted(jobs, *_args, **_kwargs):
            self.target.write_text("generated " + self.source.read_text())
            return {"status": "OK", "jobs": [{**job, "status": "UPDATED", "editability": "FULL", "fields": []} for job in jobs]}
        with patch.object(content, "inventory", return_value=records), patch.object(content, "apply_unity", side_effect=converted) as convert:
            content.sync(self.args())
            self.source.write_text("edited one object")
            content.sync(self.args(file=[self.relative]))
            content.sync(self.args())
            self.assertEqual(convert.call_count, 2)
            self.assertEqual(len(convert.call_args_list[1].args[0]), 1)
            self.target.write_text("accidental generated edit")
            content.sync(self.args())
            self.assertEqual(convert.call_count, 3)
            self.assertEqual(self.target.read_text(), "generated edited one object")

    def test_batches_keep_prefab_source_objects_together(self):
        files = {"a": {**self.record, "category": "PREFAB", "jobs": [{}]},
                 "b": {**self.record, "category": "DATA", "jobs": [{}]},
                 "c": {**self.record, "category": "PREFAB", "jobs": [{}]}}
        self.assertEqual(content.conversion_batches(["a", "b", "c"], files, asset_limit=1), [["a", "c"], ["b"]])

    def test_completed_batch_survives_a_later_conversion_failure(self):
        other_relative = "Assets/other.json"
        other = content.RES / other_relative
        other.write_text("second object")
        other_target_rel = "Assets/GameContent/other.asset"
        other_target = content.PROJECT / other_target_rel
        other_target.write_text("old second object")
        Path(str(other_target) + ".meta").write_bytes(self.meta_bytes)
        first = {**self.record, "category": "DATA", "jobs": [{"source": "res/" + self.relative}]}
        second = {**first, "relative_path": other_relative, "targets": [other_target_rel], "jobs": [{"source": "res/" + other_relative}]}
        records = {self.relative: first, other_relative: second}
        batches = content.conversion_batches
        calls = []
        def convert(jobs, *_args, **_kwargs):
            relative = jobs[0]["source"].removeprefix("res/")
            calls.append(relative)
            if len(calls) == 2:
                raise RuntimeError("Unity memory limit")
            target = self.target if relative == self.relative else other_target
            target.write_text("converted " + relative)
            return {"status": "OK", "jobs": [{**jobs[0], "editability": "FULL", "fields": []}]}
        with patch.object(content, "inventory", return_value=records), patch.object(content, "conversion_batches", side_effect=lambda p, f: batches(p, f, asset_limit=1)), patch.object(content, "apply_unity", side_effect=convert):
            with self.assertRaises(RuntimeError):
                content.sync(self.args())
            entries = content.read_json(content.STATE)["files"]
            self.assertEqual(entries[self.relative]["sync_status"], "SYNCED")
            self.assertEqual(entries[other_relative]["sync_status"], "PENDING_IMPORT")
            content.sync(self.args())
        self.assertEqual(calls, [self.relative, other_relative, other_relative])

    def test_read_only_prefab_hierarchy_is_partial_and_skips_until_source_changes(self):
        record = {**self.record, "category": "PREFAB", "editability": "PARTIAL",
                  "jobs": [{"source": "res/" + self.relative}]}
        reason = {"field": "*", "editability": "READ_ONLY", "reason": "fixture hierarchy cannot load"}
        response = {"status": "OK", "jobs": [{"source": "res/" + self.relative,
                    "status": "READ_ONLY_HIERARCHY", "editability": "READ_ONLY", "fields": [reason]}]}
        with patch.object(content, "inventory", return_value={self.relative: record}), \
                patch.object(content, "apply_unity", return_value=response) as convert:
            self.assertEqual(content.sync(self.args()), 0)
            entry = content.read_json(content.STATE)["files"][self.relative]
            self.assertEqual(entry["sync_status"], "PARTIAL")
            self.assertEqual(entry["editability"], "READ_ONLY")
            self.assertEqual(entry["field_editability"], [{**reason, "occurrences": 1}])
            self.assertEqual(content.sync(self.args()), 0)
            self.assertEqual(convert.call_count, 1)
            self.assertEqual(content.read_json(content.STATE)["files"][self.relative]["field_editability"], [{**reason, "occurrences": 1}])
            self.source.write_text("new source edit after limitation")
            self.assertEqual(content.sync(self.args()), 0)
            self.assertEqual(convert.call_count, 2)
            self.assertEqual(self.target.read_text(), "old text")
            self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_source_deletion_fails_and_preserves_generated_asset(self):
        self.source.unlink()
        self.assertEqual(content.sync(self.args()), 1)
        self.assertEqual(self.target.read_text(), "old text")
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_restored_failed_source_is_imported_again(self):
        self.source.unlink()
        content.sync(self.args())
        self.source.write_bytes(self.target.read_bytes())
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}) as convert:
            self.assertEqual(content.sync(self.args()), 0)
            convert.assert_called_once()
        self.assertEqual(content.read_json(content.STATE)["files"][self.relative]["sync_status"], "SYNCED")

    def test_doctor_requires_sync_and_detects_generated_drift(self):
        args = argparse.Namespace(deep=True, no_report=True)
        self.assertEqual(content.doctor(args), 1)
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}):
            content.sync(self.args())
        self.assertEqual(content.doctor(args), 0)
        self.target.write_text("accidental generated edit")
        self.assertEqual(content.doctor(args), 1)

    def test_path_escape_is_rejected(self):
        for path in ("../outside", "Assets/../../outside", "C:/outside", "/outside"):
            with self.assertRaises(ValueError):
                content.source_path(path)

    def test_reimport_preserves_unknown_and_known_user_edits(self):
        stage = self.root / "staging"
        incoming = stage / "incoming.txt"
        incoming.parent.mkdir()
        incoming.write_text("new original")
        row = {"type": "TextAsset", "name": "Characters", "asset_path": "Assets/Characters.txt",
               "output_file": self.relative, "source_bundle": "aa.bundle", "path_id": "42"}
        with (content.RES / "_index.csv").open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(row))
            writer.writeheader()
            writer.writerow(row)
        for baseline in (None, content.digest(self.target)):
            content.write_json(content.BASELINE, {"version": 1, "files": {self.relative: {"sha256": baseline}}})
            copied = {**row, "output_file": "incoming.txt"}
            _, report = import_workspace.merge(stage, [copied])
            self.assertIn(self.relative, report["user_modified_preserved"])
            self.assertEqual(self.source.read_text(), "new text")
        _, report = import_workspace.merge(stage, [{**row, "output_file": "incoming.txt"}], overwrite=True)
        self.assertEqual(self.source.read_text(), "new original")
        self.assertFalse(report["user_modified_preserved"])

    def test_new_object_cannot_take_an_existing_objects_source_path(self):
        stage = self.root / "staging"
        incoming = stage / self.relative
        incoming.parent.mkdir(parents=True)
        incoming.write_text("different original object")
        old = {"type": "TextAsset", "name": "Characters", "asset_path": "Assets/Characters.txt",
               "output_file": self.relative, "source_bundle": "aa.bundle", "path_id": "42"}
        with (content.RES / "_index.csv").open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(old))
            writer.writeheader()
            writer.writerow(old)
        rows, _ = import_workspace.merge(stage, [{**old, "path_id": "99"}], overwrite=True)
        self.assertEqual(len(rows), 2)
        self.assertEqual(len({row["output_file"] for row in rows}), 2)
        self.assertEqual(self.source.read_text(), "new text")

    def test_multiple_samples_from_one_audio_object_are_not_collapsed(self):
        rows = [{"type": "AudioClip", "source_bundle": "aa.bundle", "path_id": "42", "output_file": name}
                for name in ("Assets/clip.left.ogg", "Assets/clip.right.ogg")]
        self.assertNotEqual(import_workspace.identity(rows[0]), import_workspace.identity(rows[1]))

    def test_repeated_audio_collision_keeps_object_identity_and_user_edit(self):
        relative = "Assets/voice.ogg"
        original = content.RES / relative
        original.write_bytes(b"existing object")
        old = {"type": "AudioClip", "name": "Voice", "asset_path": relative,
               "output_file": relative, "source_bundle": "aa.bundle", "path_id": "42"}
        with (content.RES / "_index.csv").open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(old))
            writer.writeheader()
            writer.writerow(old)
        stage = self.root / "staging"
        incoming = stage / relative
        incoming.parent.mkdir(parents=True)
        incoming.write_bytes(b"new object original")
        new = {**old, "path_id": "99"}
        rows, _ = import_workspace.merge(stage, [dict(new)])
        exported = next(row["output_file"] for row in rows if row["path_id"] == "99")
        self.assertEqual(exported, "Assets/voice~import1.ogg")
        (content.RES / exported).write_bytes(b"user audio edit")
        rows, report = import_workspace.merge(stage, [dict(new)])
        self.assertEqual(len(rows), 2)
        self.assertEqual([row["output_file"] for row in rows if row["path_id"] == "99"], [exported])
        self.assertEqual((content.RES / exported).read_bytes(), b"user audio edit")
        self.assertIn(exported, report["user_modified_preserved"])
        self.assertEqual(original.read_bytes(), b"existing object")

    def test_legacy_audio_duplicate_suffix_change_retains_original_path(self):
        relative = "Assets/clip~1.ogg"
        source = content.RES / relative
        source.write_bytes(b"user audio edit")
        old = {"type": "AudioClip", "name": "Clip", "asset_path": "Assets/clip.ogg",
               "output_file": relative, "source_bundle": "aa.bundle", "path_id": "42"}
        with (content.RES / "_index.csv").open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(old))
            writer.writeheader()
            writer.writerow(old)
        stage = self.root / "staging"
        incoming_rel = "Assets/clip~8.ogg"
        incoming = stage / incoming_rel
        incoming.parent.mkdir(parents=True)
        incoming.write_bytes(b"original audio")
        rows, report = import_workspace.merge(stage, [{**old, "output_file": incoming_rel}])
        self.assertEqual(len(rows), 1)
        self.assertEqual(rows[0]["output_file"], relative)
        self.assertEqual(rows[0]["sample_id"], "")
        self.assertEqual(source.read_bytes(), b"user audio edit")
        self.assertIn(relative, report["user_modified_preserved"])

    def test_legacy_audio_multiple_samples_keep_identity_across_suffix_changes(self):
        old_rows = [{"type": "AudioClip", "name": "Clip", "asset_path": "Assets/clip.ogg",
                     "output_file": f"Assets/clip.{sample}~{index}.ogg",
                     "source_bundle": "aa.bundle", "path_id": "42"}
                    for index, sample in enumerate(("left", "right"), 1)]
        for row in old_rows:
            (content.RES / row["output_file"]).write_bytes(row["output_file"].encode())
        content.write_json(content.BASELINE, {"version": 1, "files": {
            row["output_file"]: {"sha256": content.digest(content.RES / row["output_file"])} for row in old_rows}})
        left = content.RES / old_rows[0]["output_file"]
        left.write_bytes(b"user sample edit")
        with (content.RES / "_index.csv").open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(old_rows[0]))
            writer.writeheader()
            writer.writerows(old_rows)
        stage = self.root / "staging"
        incoming_rows = []
        for index, row in enumerate(old_rows, 7):
            sample = "left" if index == 7 else "right"
            relative = f"Assets/clip.{sample}~{index}.ogg"
            source = stage / relative
            source.parent.mkdir(parents=True, exist_ok=True)
            source.write_bytes(b"new original sample")
            incoming_rows.append({**row, "output_file": relative})
        rows, report = import_workspace.merge(stage, incoming_rows)
        self.assertEqual(len(rows), 2)
        self.assertEqual({row["output_file"] for row in rows}, {row["output_file"] for row in old_rows})
        self.assertEqual({row["sample_id"] for row in rows}, {"clip.left.ogg", "clip.right.ogg"})
        self.assertEqual(left.read_bytes(), b"user sample edit")
        self.assertEqual((content.RES / old_rows[1]["output_file"]).read_bytes(), b"new original sample")
        self.assertIn(old_rows[0]["output_file"], report["user_modified_preserved"])

    def test_ambiguous_legacy_audio_samples_fail_before_dropping_or_overwriting_files(self):
        rows = [{"type": "AudioClip", "name": "Clip", "asset_path": "Assets/clip.ogg",
                 "output_file": f"Assets/clip.sample~{number}.ogg",
                 "source_bundle": "aa.bundle", "path_id": "42"} for number in (1, 2)]
        for row in rows:
            (content.RES / row["output_file"]).write_bytes(b"existing sample")
        index = content.RES / "_index.csv"
        with index.open("w", newline="", encoding="utf-8") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(rows[0]))
            writer.writeheader()
            writer.writerows(rows)
        original_index = index.read_bytes()
        stage = self.root / "staging"
        for row in rows:
            incoming = stage / row["output_file"]
            incoming.parent.mkdir(parents=True, exist_ok=True)
            incoming.write_bytes(b"new sample")
        with self.assertRaisesRegex(ValueError, "Ambiguous AudioClip sample identity"):
            import_workspace.merge(stage, [dict(row) for row in rows])
        self.assertEqual(index.read_bytes(), original_index)
        self.assertTrue(all((content.RES / row["output_file"]).read_bytes() == b"existing sample" for row in rows))
        self.assertFalse(content.BASELINE.exists())


if __name__ == "__main__":
    unittest.main()
