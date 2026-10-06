"""The ObjectionCutIn glass sounds are extracted by identity, only when absent, and a missing bundle fails loudly."""

from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import content


class EnsureCutInAudioTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.project = Path(self.temp.name) / "UnityProject"
        self.res = Path(self.temp.name) / "res"
        for patcher in (patch.object(content, "PROJECT", self.project), patch.object(content, "RES", self.res)):
            patcher.start()
            self.addCleanup(patcher.stop)

    def targets(self):
        return sorted(f"{content.CUTIN_AUDIO_FOLDER}/ObjectionCutIn_{name}.ogg" for name in content.CUTIN_AUDIO.values())

    def test_existing_files_are_not_extracted_and_are_all_reported(self):
        for target in self.targets():
            path = self.project / target
            path.parent.mkdir(parents=True, exist_ok=True)
            path.write_bytes(b"OggS")
        # No cached bundle exists; reaching for it would raise, so success proves nothing was extracted.
        self.assertEqual(content.ensure_cutin_audio(), self.targets())

    def test_missing_file_without_cached_bundle_fails_instead_of_skipping(self):
        with self.assertRaisesRegex(FileNotFoundError, "cached bundle"):
            content.ensure_cutin_audio()

    def test_tables_cover_both_timeline_clips(self):
        self.assertEqual(sorted(content.CUTIN_AUDIO.values()), ["Crack_01", "Crack_02"])


if __name__ == "__main__":
    unittest.main()
