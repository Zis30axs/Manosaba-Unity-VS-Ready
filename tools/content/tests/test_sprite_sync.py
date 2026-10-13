"""Real PNG conversion and incremental-sync regressions without launching Unity."""
from contextlib import redirect_stderr, redirect_stdout
from copy import deepcopy
import hashlib
from io import StringIO
import json
import os
from pathlib import Path
import sys
import tempfile
from types import SimpleNamespace
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import content
from PIL import Image


class SpriteSyncTests(unittest.TestCase):
    def setUp(self):
        scratch = content.REPO / "tools/content/.work/sprite-tests"
        scratch.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(dir=scratch)
        self.root = Path(self.temp.name).resolve()
        self.root.relative_to(scratch.resolve())
        self.addCleanup(self.cleanup)
        self.res = self.root / "res"
        self.project = self.root / "UnityProject"
        self.relative = "Assets/Trimmed.sprite.png"
        self.target = "Assets/GameContent/Trimmed.sprite.sprites/Body.png"
        self.source = self.res / self.relative
        self.destination = self.project / self.target
        self.source.parent.mkdir(parents=True)
        self.destination.parent.mkdir(parents=True)
        self.meta = Path(str(self.destination) + ".meta")
        self.meta_bytes = b"fileFormatVersion: 2\r\nguid: 12345678901234567890123456789012\r\nTextureImporter:\r\n  spritePixelsToUnits: 45.5\r\n"
        self.meta.write_bytes(self.meta_bytes)
        self.state = self.res / ".manosaba-content-state.json"
        self.settings = {"rect_size": [7, 6], "texture_rect_offset": [2, 1]}
        self.args = SimpleNamespace(category="", file=[], force=False, dry_run=False,
                                    copy_only=False, verbose=False, timeout=30, batch_size=32)
        self.write_crop()

    def cleanup(self):
        self.root.relative_to((content.REPO / "tools/content/.work/sprite-tests").resolve())
        self.temp.cleanup()

    def write_crop(self, size=(3, 2), color=(21, 35, 49, 255)):
        image = Image.new("RGBA", size, color)
        image.putpixel((0, 0), (210, 90, 30, 80))
        image.putpixel((size[0] - 1, size[1] - 1), (40, 70, 190, 120))
        image.save(self.source)
        return image

    def record(self, settings=None):
        return {"relative_path": self.relative, "category": "SPRITE", "targets": [self.target],
                "jobs": [], "dependencies": [], "editability": "FULL",
                "sprite_settings": deepcopy(settings if settings is not None else self.settings)}

    def run_sync(self, record=None):
        with patch.object(content, "RES", self.res), patch.object(content, "PROJECT", self.project), \
                patch.object(content, "STATE", self.state), \
                patch.object(content, "refresh_plans"), \
                patch.object(content, "inventory", return_value={self.relative: record or self.record()}), \
                redirect_stdout(StringIO()), redirect_stderr(StringIO()):
            return content.sync(self.args)

    def run_doctor(self, record=None):
        with patch.object(content, "RES", self.res), patch.object(content, "PROJECT", self.project), \
                patch.object(content, "STATE", self.state), \
                patch.object(content, "inventory", return_value={self.relative: record or self.record()}), \
                redirect_stdout(StringIO()), redirect_stderr(StringIO()):
            return content.doctor(SimpleNamespace(deep=True, no_report=True))

    def test_canvas_places_pixels_using_bottom_left_offset_and_preserves_alpha(self):
        source_before = self.source.read_bytes()
        full, report = content.importer.sprite_canvas(self.source, [7, 6], [2, 1])
        self.assertEqual(report["status"], "RESTORED")
        self.assertEqual(full.size, (7, 6))
        self.assertEqual(full.getpixel((2, 3)), (210, 90, 30, 80))
        self.assertEqual(full.getpixel((4, 4)), (40, 70, 190, 120))
        self.assertEqual(full.getpixel((2, 2)), (0, 0, 0, 0))
        self.assertEqual(full.getpixel((2, 5)), (0, 0, 0, 0))
        self.assertEqual(self.source.read_bytes(), source_before)

    def test_canvas_clips_one_pixel_rounding_overhang(self):
        image = self.write_crop((5, 3))
        full, report = content.importer.sprite_canvas(self.source, [4, 5], [-0.6, 1.2])
        self.assertEqual(report["status"], "RESTORED")
        self.assertEqual(full.size, (4, 5))
        for x in range(4):
            for y in range(3):
                self.assertEqual(full.getpixel((x, y + 1)), image.getpixel((x + 1, y)))
        self.assertEqual(full.getpixel((0, 0)), (0, 0, 0, 0))

    def test_canvas_clips_other_edges_and_rejects_overhang_beyond_one_pixel(self):
        image = self.write_crop((3, 3))
        cases = [([3, 1], (3, 1), (0, 0), (2, 3)),
                 ([1, 3], (1, 0), (0, 1), (3, 2)),
                 ([1, -1], (1, 3), (0, 0), (3, 2))]
        for offset, destination, source, size in cases:
            with self.subTest(offset=offset):
                full, report = content.importer.sprite_canvas(self.source, [5, 5], offset)
                self.assertEqual(report["status"], "RESTORED")
                for x in range(size[0]):
                    for y in range(size[1]):
                        self.assertEqual(full.getpixel((destination[0] + x, destination[1] + y)),
                                         image.getpixel((source[0] + x, source[1] + y)))
        for offset in ([-2, 1], [4, 1], [1, 4], [1, -2]):
            with self.subTest(rejected_offset=offset):
                full, report = content.importer.sprite_canvas(self.source, [5, 5], offset)
                self.assertIsNone(full)
                self.assertEqual(report["status"], "OUT_OF_RECT")

    def test_sync_tracks_transformed_hash_and_skips_unchanged_import_and_write(self):
        original_source = self.source.read_bytes()
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}) as unity, \
                patch.object(content.importer, "sync_leaf", wraps=content.importer.sync_leaf) as copy:
            self.assertEqual(self.run_sync(), 0)
            self.assertEqual(unity.call_count, 1)
            self.assertEqual(copy.call_count, 1)
            with Image.open(self.destination) as image:
                self.assertEqual(image.size, (7, 6))
                self.assertEqual(image.getpixel((2, 3)), (210, 90, 30, 80))
            state = json.loads(self.state.read_text(encoding="utf-8"))["files"][self.relative]
            destination_hash = hashlib.sha256(self.destination.read_bytes()).hexdigest()
            self.assertEqual(state["import_sha256"], destination_hash)
            self.assertNotEqual(state["source"]["sha256"], destination_hash)
            self.assertEqual(state["sync_status"], "SYNCED")
            self.assertEqual(self.meta.read_bytes(), self.meta_bytes)
            previous = (self.destination.read_bytes(), self.destination.stat().st_mtime_ns)

            self.assertEqual(self.run_sync(), 0)
            self.assertEqual(unity.call_count, 2)
            unity.assert_called_with([], [], timeout=30, ensure_curtain_skin=True)
            self.assertEqual(copy.call_count, 1)
            self.assertEqual((self.destination.read_bytes(), self.destination.stat().st_mtime_ns), previous)
            self.assertEqual(self.source.read_bytes(), original_source)

            old_stamp = self.source.stat().st_mtime_ns
            with Image.open(self.source) as image:
                edited = image.copy()
            edited.putpixel((1, 1), (9, 220, 31, 77))
            edited.save(self.source)
            os.utime(self.source, ns=(old_stamp + 1_000_000, old_stamp + 1_000_000))
            self.assertEqual(self.run_sync(), 0)
            self.assertEqual(unity.call_count, 3)
            self.assertEqual(copy.call_count, 2)
            with Image.open(self.destination) as image:
                self.assertEqual(image.size, (7, 6))
                self.assertEqual(image.getpixel((3, 4)), (9, 220, 31, 77))
            self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_offset_metadata_change_recomputes_transformed_hash_without_source_edit(self):
        source_before = self.source.read_bytes()
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}) as unity:
            self.assertEqual(self.run_sync(), 0)
            original_hash = json.loads(self.state.read_text(encoding="utf-8"))["files"][self.relative]["import_sha256"]
            changed = self.record({"rect_size": [7, 6], "texture_rect_offset": [1, 2]})
            self.assertEqual(self.run_doctor(changed), 1)
            self.assertEqual(self.run_sync(changed), 0)
            self.assertEqual(self.run_doctor(changed), 0)
            self.assertEqual(unity.call_count, 2)
            state = json.loads(self.state.read_text(encoding="utf-8"))["files"][self.relative]
            self.assertNotEqual(state["import_sha256"], original_hash)
            self.assertEqual(state["import_sha256"], hashlib.sha256(self.destination.read_bytes()).hexdigest())
            with Image.open(self.destination) as image:
                self.assertEqual(image.getpixel((1, 2)), (210, 90, 30, 80))
                self.assertEqual(image.getpixel((4, 4)), (0, 0, 0, 0))
            self.assertEqual(self.run_sync(changed), 0)
            self.assertEqual(unity.call_count, 3)
            unity.assert_called_with([], [], timeout=30, ensure_curtain_skin=True)
        self.assertEqual(self.source.read_bytes(), source_before)
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_destination_tamper_or_deletion_regenerates_cached_padding_then_skips(self):
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}) as unity, \
                patch.object(content.importer, "sync_leaf", wraps=content.importer.sync_leaf) as copy:
            self.assertEqual(self.run_sync(), 0)
            expected = self.destination.read_bytes()
            for deleted in (False, True):
                with self.subTest(deleted=deleted):
                    if deleted:
                        self.destination.unlink()
                    else:
                        self.destination.write_bytes(self.source.read_bytes())
                    previous_imports = unity.call_count
                    previous_copies = copy.call_count
                    self.assertEqual(self.run_sync(), 0)
                    self.assertEqual(unity.call_count, previous_imports + 1)
                    self.assertEqual(copy.call_count, previous_copies + 1)
                    self.assertEqual(self.destination.read_bytes(), expected)
                    self.assertEqual(self.meta.read_bytes(), self.meta_bytes)
                    self.assertEqual(self.run_sync(), 0)
                    self.assertEqual(unity.call_count, previous_imports + 2)
                    unity.assert_called_with([], [], timeout=30, ensure_curtain_skin=True)
                    self.assertEqual(copy.call_count, previous_copies + 1)

    def test_invalid_crop_metadata_fails_without_target_or_meta_changes(self):
        Image.new("RGBA", (7, 6), (11, 22, 33, 44)).save(self.destination)
        original = self.destination.read_bytes()
        cases = [({"rect_size": [7, 6]}, "no imported textureRectOffset"),
                 ({"rect_size": [1, 6], "texture_rect_offset": [0, 1]}, "OUT_OF_RECT")]
        with patch.object(content, "apply_unity", side_effect=self.assert_gate_only):
            for settings, reason in cases:
                with self.subTest(reason=reason):
                    self.assertEqual(self.run_sync(self.record(settings)), 1)
                    state = json.loads(self.state.read_text(encoding="utf-8"))["files"][self.relative]
                    self.assertEqual(state["sync_status"], "FAILED")
                    self.assertIn(reason, state["reason"])
                    self.assertEqual(self.destination.read_bytes(), original)
                    self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def test_malformed_offset_is_a_file_failure_and_retains_target_and_meta(self):
        Image.new("RGBA", (7, 6), (11, 22, 33, 44)).save(self.destination)
        original = self.destination.read_bytes()
        with patch.object(content, "apply_unity", side_effect=self.assert_gate_only):
            for offset in ([2], ["2", 1], [2, None], [float("inf"), 1]):
                with self.subTest(offset=offset):
                    self.assertEqual(self.run_sync(self.record({"rect_size": [7, 6], "texture_rect_offset": offset})), 1)
                    state = json.loads(self.state.read_text(encoding="utf-8"))["files"][self.relative]
                    self.assertEqual(state["sync_status"], "FAILED")
                    self.assertTrue(state["reason"])
                    self.assertEqual(self.destination.read_bytes(), original)
                    self.assertEqual(self.meta.read_bytes(), self.meta_bytes)

    def assert_gate_only(self, jobs, paths, **kwargs):
        # Failed leaves must never reach Unity import; the shared-content gate
        # still executes even if there are no successfully queued resources.
        self.assertEqual((jobs, paths), ([], []))
        self.assertEqual(kwargs, {"timeout": 30, "ensure_curtain_skin": True})
        return {"status": "OK", "jobs": []}

    def test_full_rectangle_source_keeps_its_original_png_bytes(self):
        self.write_crop((7, 6))
        self.assertIsNone(content.importer.leaf_payload(self.source, self.settings))
        with patch.object(content, "apply_unity", return_value={"status": "OK", "jobs": []}):
            self.assertEqual(self.run_sync(), 0)
        self.assertEqual(self.destination.read_bytes(), self.source.read_bytes())
        self.assertEqual(self.meta.read_bytes(), self.meta_bytes)


if __name__ == "__main__":
    unittest.main()
