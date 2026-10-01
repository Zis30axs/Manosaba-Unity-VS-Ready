"""Recovery tests use synthetic assets; they never need copyrighted content."""
import csv
import importlib.util
import json
from pathlib import Path
import tempfile
import types
import unittest
from unittest.mock import patch

MODULE = Path(__file__).resolve().parents[1] / "roundtrip.py"
spec = importlib.util.spec_from_file_location("content_roundtrip", MODULE)
rt = importlib.util.module_from_spec(spec)
spec.loader.exec_module(rt)


class RecoveryTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.paths = {"TEXT": "Assets/Text.txt", "IMAGE": "Assets/Actor.sprite.png",
                      "DATA": "Assets/CharacterData.asset.json", "SCENARIO": "Assets/Opening.asset.json"}
        self.targets = {"TEXT": "Assets/GameContent/Text.txt", "IMAGE": "Assets/GameContent/Actor.sprites/Body.png",
                        "DATA": "Assets/GameContent/CharacterData.asset", "SCENARIO": "Assets/GameContent/Opening.asset"}
        Image, _ = rt.dependencies(rt.ROOT)
        for category, path in self.paths.items():
            source = self.root / "res" / path
            source.parent.mkdir(parents=True, exist_ok=True)
            if category == "TEXT":
                source.write_bytes(b"\xef\xbb\xbf; comment\r\n# id\r\nExisting user edit\r\n")
            elif category == "IMAGE":
                Image.new("RGBA", (160, 160), (10, 20, 30, 255)).save(source)
            elif category == "DATA":
                source.write_text(json.dumps({"_items": [{"_name": [{"_text": "one"}, {"_text": "user data edit"}]}]}), encoding="utf-8")
            else:
                source.write_text(json.dumps({"textMap": {"idToText": {"keys": ["opening"], "values": ["user scenario edit"]}}}), encoding="utf-8")
            target = self.root / "UnityProject" / self.targets[category]
            target.parent.mkdir(parents=True, exist_ok=True)
            target.write_bytes(b"pre-existing generated differences: " + category.encode())
            target.with_suffix(target.suffix + ".meta").write_text("fileFormatVersion: 2\nguid: " + "1" * 32 + "\n", encoding="utf-8")
        with (self.root / "res/_index.csv").open("w", encoding="utf-8", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=["type", "name", "asset_path", "output_file"])
            writer.writeheader()
            for category, source in self.paths.items():
                writer.writerow({"type": {"TEXT": "TextAsset", "IMAGE": "Sprite"}.get(category, "MonoBehaviour"),
                                 "name": "Body", "asset_path": source, "output_file": source})
        reverse = {p: c for c, p in self.paths.items()}
        def dest(asset_path, output):
            key = output if output in reverse else output + ".json"
            return str(self.root / "UnityProject" / self.targets[reverse[key]])
        self.real_leaf = rt.importer(rt.ROOT)
        helper = types.SimpleNamespace(dest_for=dest, dest_for_sprite=lambda asset, output, name: dest(asset, output),
                                       leaf_payload=self.real_leaf.leaf_payload)
        self.patch = patch.object(rt, "importer", return_value=helper)
        self.patch.start()
        self.addCleanup(self.patch.stop)
        self.directory = self.root / "out/probe"

    def prepare(self):
        return rt.prepare(self.root, self.directory, *(self.paths[c] for c in ("TEXT", "IMAGE", "DATA", "SCENARIO")))

    def add_master_data(self):
        source = self.root / "res" / rt.DEFAULT_MASTER_DATA
        source.parent.mkdir(parents=True, exist_ok=True)
        source.write_text(json.dumps({"_defaultHp": 10.0, "_assetKeyNames": ["test-label"]}), encoding="utf-8")
        target = self.root / "UnityProject" / rt.MASTER_DATA_TARGET
        target.parent.mkdir(parents=True, exist_ok=True)
        target.write_bytes(b"original Resources asset")
        target.with_suffix(target.suffix + ".meta").write_text("fileFormatVersion: 2\nguid: " + "2" * 32 + "\n", encoding="utf-8")
        return source, target

    def test_restore_retains_user_sources_generated_differences_and_metadata(self):
        original = {p: p.read_bytes() for p in self.root.rglob("*") if p.is_file()}
        prepared = self.prepare()
        state = rt.load_journal(self.root, Path(prepared["journal"]))
        self.assertEqual(12, len(state["backups"]))
        self.assertTrue(all(c["source_sha256"] for c in state["cases"]))
        self.assertIn(prepared["token"], (self.root / "res" / self.paths["TEXT"]).read_text(encoding="utf-8-sig"))
        for category, target in self.targets.items():
            (self.root / "UnityProject" / target).write_bytes(b"test-generated replacement")
        rt.restore(self.root, Path(prepared["journal"]))
        self.assertTrue(all(path.read_bytes() == content for path, content in original.items()))

    def sprite_plan(self, settings):
        plan = self.root / "res/.manosaba/reconstruction/unity/sprite_settings_plan.json"
        rt.write_json(plan, {"sprites": {self.targets["IMAGE"]: settings}})

    def test_trimmed_sprite_uses_canvas_dimensions_marker_and_import_hash(self):
        self.sprite_plan({"rect_size": [240, 200], "texture_rect_offset": [80, 40]})
        prepared = self.prepare()
        case = next(case for case in prepared["cases"] if case["category"] == "IMAGE")
        self.assertEqual((case["width"], case["height"], case["sample_x"], case["sample_y"]), (240, 200, 144, 64))
        source = self.root / case["source"]
        payload = self.real_leaf.leaf_payload(source, {"rect_size": [240, 200], "texture_rect_offset": [80, 40]})
        self.assertEqual(case["expected_import_sha256"], rt.sha(payload))
        self.assertNotEqual(case["source_sha256"], case["expected_import_sha256"])
        target = self.root / "UnityProject" / case["target"]
        target.write_bytes(payload)
        journal = Path(prepared["journal"])
        state = rt.load_journal(self.root, journal)
        state["cases"] = [case]
        rt.write_json(journal, state)
        Image, ImageDraw = rt.dependencies(rt.ROOT)
        with Image.open(target) as image:
            pixels = image.copy()
        obj = types.SimpleNamespace(type=types.SimpleNamespace(name="Sprite"), read=lambda: types.SimpleNamespace(image=pixels))
        unity = types.SimpleNamespace(load=lambda path: types.SimpleNamespace(container={case["target"].lower(): obj}))
        rt.write_json(self.root / "tools/unity-rebuild/out/unity/addressables_plan_all.json", {
            "entries": [{"asset_path": case["target"], "group": "sprites", "address": "existing"}]})
        build = self.root / "UnityProject/Library/com.unity.addressables/aa/Windows"
        build.mkdir(parents=True)
        (build / "sprites_assets_all_fixture.bundle").write_bytes(b"synthetic")
        with patch.object(rt, "dependencies", return_value=(Image, ImageDraw, unity)):
            self.assertEqual(rt.verify(self.root, journal)["status"], "PASS")
            target.write_bytes(b"stale imported asset")
            with self.assertRaisesRegex(AssertionError, "imported source is stale"):
                rt.verify(self.root, journal)
        rt.restore(self.root, journal)

    def test_trimmed_sprite_one_pixel_clip_uses_signed_offset(self):
        self.sprite_plan({"rect_size": [240, 200], "texture_rect_offset": [-1, 0]})
        prepared = self.prepare()
        case = next(case for case in prepared["cases"] if case["category"] == "IMAGE")
        self.assertEqual((case["sample_x"], case["sample_y"]), (63, 104))
        rt.restore(self.root, Path(prepared["journal"]))

    def test_unloadable_image_choices_fail_before_resource_mutation(self):
        originals = {path: path.read_bytes() for path in (self.root / "res").rglob("*") if path.is_file()}
        ledger = self.root / "res/.manosaba/reconstruction/unity/a13_import_ledger.csv"
        ledger.parent.mkdir(parents=True)
        ledger.write_text("output_file,status\n" + self.paths["IMAGE"] + ",DUPLICATE_DESTINATION\n")
        with self.assertRaisesRegex(ValueError, "READ_ONLY"):
            self.prepare()
        ledger.unlink()
        rt.write_json(ledger.parent / "addressables_plan_all.json", {"entries": []})
        with self.assertRaisesRegex(ValueError, "own planned Addressables"):
            self.prepare()
        self.assertTrue(all(path.read_bytes() == original for path, original in originals.items()))
        self.assertFalse((self.directory / "journal.json").exists())

    def test_corrupt_backup_aborts_before_restoring_any_file(self):
        prepared = self.prepare()
        journal = Path(prepared["journal"])
        state = rt.load_journal(self.root, journal)
        (journal.parent / state["backups"][-1]["backup"]).write_bytes(b"corrupt")
        marked = (self.root / "res" / self.paths["TEXT"]).read_bytes()
        with self.assertRaisesRegex(ValueError, "backup hash mismatch"):
            rt.restore(self.root, journal)
        self.assertEqual(marked, (self.root / "res" / self.paths["TEXT"]).read_bytes())

    def test_prepare_failure_restores_all_prepared_sources(self):
        Image, _ = rt.dependencies(rt.ROOT)
        Image.new("RGBA", (16, 16)).save(self.root / "res" / self.paths["IMAGE"])
        before = {p: p.read_bytes() for p in (self.root / "res").rglob("*") if p.is_file()}
        with self.assertRaisesRegex(ValueError, "at least 128"):
            self.prepare()
        self.assertTrue(all(p.read_bytes() == content for p, content in before.items()))
        self.assertEqual("RESTORED", json.loads((self.directory / "journal.json").read_text())["status"])

    def test_paths_cannot_escape_workspace_or_backup_directory(self):
        with self.assertRaisesRegex(ValueError, "escapes workspace"):
            rt.checked(self.root, "../outside")
        prepared = self.prepare()
        journal = Path(prepared["journal"])
        state = json.loads(journal.read_text(encoding="utf-8"))
        state["backups"][0]["backup"] = "../other.bin"
        rt.write_json(journal, state)
        with self.assertRaisesRegex(ValueError, "escapes workspace"):
            rt.load_journal(self.root, journal)

    def test_master_data_only_needs_no_index_entry_or_image_bundle_dependencies(self):
        source, target = self.add_master_data()
        originals = {p: p.read_bytes() for p in self.root.rglob("*") if p.is_file()}
        with patch.object(rt, "dependencies", side_effect=AssertionError("unexpected image/bundle dependency")):
            prepared = rt.prepare(self.root, self.directory, master_data_only=True)
            self.assertEqual(["MASTER_DATA"], [c["category"] for c in prepared["cases"]])
            self.assertEqual(11.0, prepared["cases"][0]["expected"])
            self.assertEqual(11.0, json.loads(source.read_text())["_defaultHp"])
            self.assertEqual("PENDING_RUNTIME", rt.verify(self.root, Path(prepared["journal"]))["status"])
        self.assertEqual(3, len(rt.load_journal(self.root, Path(prepared["journal"]))["backups"]))
        rt.restore(self.root, Path(prepared["journal"]))
        self.assertTrue(all(path.read_bytes() == contents for path, contents in originals.items()))

    def test_optional_master_data_keeps_all_four_original_cases(self):
        self.add_master_data()
        prepared = rt.prepare(self.root, self.directory,
                              *(self.paths[c] for c in ("TEXT", "IMAGE", "DATA", "SCENARIO")),
                              master_data=rt.DEFAULT_MASTER_DATA)
        self.assertEqual(["TEXT", "IMAGE", "DATA", "SCENARIO", "MASTER_DATA"],
                         [c["category"] for c in prepared["cases"]])
        self.assertEqual(15, len(rt.load_journal(self.root, Path(prepared["journal"]))["backups"]))
        rt.restore(self.root, Path(prepared["journal"]))

    def test_master_data_only_run_restores_without_addressables_build(self):
        self.add_master_data()
        originals = {p: p.read_bytes() for p in self.root.rglob("*") if p.is_file()}
        commands = []
        def execute(root, command, env):
            commands.append(command)
            if "Manosaba.EditorTools.ContentRoundTripProbe.Run" in command:
                rt.write_json(self.directory / "runtime_result.json", {"status": "PASS"})
        with patch.object(rt, "execute", side_effect=execute):
            result = rt.run(self.root, self.directory, "test-unity", rt.DEFAULT_TEXT, rt.DEFAULT_IMAGE,
                            rt.DEFAULT_DATA, master_data_only=True)
        self.assertEqual("PASS", result["status"])
        self.assertFalse(any("build" in c or "Manosaba.EditorTools.AddressablesBuild.Run" in c for c in commands))
        self.assertTrue(any("Manosaba.EditorTools.ContentRoundTripProbe.RestoreImports" in c for c in commands))
        self.assertTrue(all(path.read_bytes() == contents for path, contents in originals.items()))


if __name__ == "__main__":
    unittest.main()
