"""Recipe completeness and ordering at the shared import boundary."""
import argparse
from contextlib import ExitStack
import hashlib
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import switch_story
import switch_story_assets
import switch_story_scenario
import content
import switch_title


class StoryRecipeCompletenessTests(unittest.TestCase):
    def setUp(self):
        temporary = tempfile.TemporaryDirectory()
        self.addCleanup(temporary.cleanup)
        self.root = Path(temporary.name)
        self.cache = self.root / "res/.imported-source/switch-story"
        self.source = self.root / "new-dump"
        self.cache.mkdir(parents=True)
        self.source.mkdir()
        self.names = [f"scenario-{i:02}.bundle" for i in range(24)]
        self.missing = self.names[-1]
        for name in [*self.names, "art.bundle"]:
            (self.cache / name).write_bytes(b"previous complete source")
            if name != self.missing:
                (self.source / name).write_bytes(b"new source")
        self.specification = {"version": 1, "bundles": ["art.bundle"], "stills": [], "cutins": []}
        stack = ExitStack()
        self.addCleanup(stack.close)
        for name, value in {"REPO": self.root, "CACHE": self.cache,
                            "OUTPUT": self.root / "res/.manosaba/switch-story"}.items():
            stack.enter_context(patch.object(switch_story, name, value))
        stack.enter_context(patch.object(switch_story_scenario, "baseline_inputs",
                                        return_value=[self.root / "pc-baseline" / name for name in self.names]))
        stack.enter_context(patch.object(switch_story, "source_folder", return_value=self.source))
        stack.enter_context(patch.object(switch_story_assets, "inspect_source", return_value=self.specification))
        self.build = stack.enter_context(patch.object(switch_story_assets, "build_assets"))
        self.export = stack.enter_context(patch.object(switch_story_scenario, "export_scenarios"))
        self.copy = stack.enter_context(patch.object(switch_story.shutil, "copyfile"))
        self.write = stack.enter_context(patch.object(switch_story, "write"))

    def test_missing_replacement_script_fails_before_copy_even_with_old_cached_file(self):
        cached_before = {p.name: p.read_bytes() for p in self.cache.iterdir()}
        for dry_run in (False, True):
            with self.subTest(dry_run=dry_run):
                with self.assertRaisesRegex(ValueError, "Missing Switch story source bundle.*" + self.missing):
                    switch_story.prepare(self.source, dry_run=dry_run)
        self.copy.assert_not_called()
        self.write.assert_not_called()
        self.export.assert_not_called()
        self.build.assert_not_called()
        self.assertEqual({p.name: p.read_bytes() for p in self.cache.iterdir()}, cached_before)

    def test_manifest_cannot_omit_a_script_present_in_old_cache(self):
        files = {p.name: hashlib.sha256(p.read_bytes()).hexdigest() for p in self.cache.iterdir()
                 if p.name != self.missing}
        manifest = {"version": 1, "files": files, "assets": self.specification}
        (self.cache / "manifest.json").write_text(json.dumps(manifest), encoding="utf-8")
        with patch.object(switch_story, "sha") as sha:
            with self.assertRaisesRegex(ValueError, "cache manifest is missing required scenario bundles"):
                switch_story.prepare()
            sha.assert_not_called()
        self.copy.assert_not_called()
        self.write.assert_not_called()
        self.export.assert_not_called()
        self.build.assert_not_called()


class ImportRecipeOrderingTests(unittest.TestCase):
    def test_refreshes_selected_and_cached_recipes_before_first_unity_export(self):
        with tempfile.TemporaryDirectory() as temporary, ExitStack() as stack:
            root = Path(temporary)
            res = root / "res"
            pipeline = root / "tools/unity-rebuild"
            game = root / "game"
            (game / "manosaba_Data").mkdir(parents=True)
            manifest = res / ".imported-source/switch-story/manifest.json"
            manifest.parent.mkdir(parents=True)
            manifest.write_text("{}", encoding="utf-8")
            (res / "_index.csv").write_text("", encoding="utf-8")
            pipeline.mkdir(parents=True)
            (pipeline / "bootstrap_import.py").write_text("", encoding="utf-8")
            for name, value in {
                "REPO": root, "RES": res, "PROJECT": root / "UnityProject",
                "PIPELINE": pipeline, "BASELINE": res / ".manosaba/base-hashes.json",
            }.items():
                stack.enter_context(patch.object(content, name, value))

            events = []
            args = argparse.Namespace(
                game=game, overwrite_user_changes=False, reextract=False, timeout=30,
                switch_title_source=root / "selected-title-source", switch_story_source=None,
            )
            stack.enter_context(patch.object(content, "run", side_effect=lambda command:
                                             events.append(Path(command[1]).name)))
            stack.enter_context(patch.object(content, "restore_evidence",
                                             side_effect=lambda: events.append("restore")))
            stack.enter_context(patch.object(content, "inventory", return_value={}))
            title = stack.enter_context(patch.object(switch_title, "prepare", side_effect=lambda source:
                (events.append("title"), {"status": "UNCHANGED"})[1]))
            story = stack.enter_context(patch.object(switch_story, "prepare", side_effect=lambda source:
                (events.append("story"), {"status": "UNCHANGED"})[1]))
            unity = stack.enter_context(patch.object(content, "apply_unity", side_effect=lambda *a, **kw:
                                                     events.append("unity-export")))
            sync = stack.enter_context(patch.object(content, "sync", side_effect=lambda actual:
                (events.append("sync"), 17)[1]))

            self.assertEqual(content.import_content(args), 17)
            self.assertEqual(events, ["import_source_cache.py", "restore", "bootstrap_import.py",
                                      "title", "story", "unity-export", "sync"])
            title.assert_called_once_with(args.switch_title_source)
            story.assert_called_once_with(None)
            unity.assert_called_once_with([], [], export=True, timeout=30, ensure_curtain_skin=True)
            sync.assert_called_once_with(args)
            self.assertEqual((args.category, args.file, args.force, args.dry_run, args.copy_only, args.verbose),
                             ("", [], False, False, False, False))


class StoryVoiceRecipeTests(unittest.TestCase):
    def test_voice_runtime_dependencies_participate_in_recipe_inputs(self):
        with tempfile.TemporaryDirectory() as folder:
            root = Path(folder)
            with patch.object(switch_story, "REPO", root), patch.object(switch_story, "PC_SCENARIOS", root / "pc"), \
                 patch.object(switch_story_scenario, "baseline_inputs", return_value=[]):
                paths = set(switch_story._recipe_inputs())
            runtime = root / "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views"
            audio = root / "UnityProject/Packages/com.elringus.naninovel/Runtime/Audio"
            for path in [runtime / "GosubToObjectionCutIn.cs", runtime / "PlayObjectionVoicePair.cs",
                         *(audio / name for name in ("AudioManager.cs", "IAudioManager.cs", "VoicePlayback.cs"))]:
                self.assertIn(path, paths)
                self.assertIn(Path(str(path) + ".meta"), paths)

    def test_runtime_change_refreshes_cached_recipe_without_pc_edits_and_then_is_idempotent(self):
        with tempfile.TemporaryDirectory() as folder, ExitStack() as stack:
            root = Path(folder); cache = root / "res/.imported-source/switch-story"
            output = root / "res/.manosaba/switch-story"
            cache.mkdir(parents=True); output.mkdir(parents=True)
            (cache / "script.bundle").write_bytes(b"verified source")
            files = {"script.bundle": hashlib.sha256(b"verified source").hexdigest()}
            (cache / "manifest.json").write_text(json.dumps({"version": 1, "files": files,
                "assets": {"bundles": ["script.bundle"]}}), encoding="utf-8")
            runtime = root / "GosubToObjectionCutIn.cs"; runtime.write_text("initial runtime", encoding="utf-8")
            pc = root / "res/Assets/#WitchTrials/Scenarios/Act01/Test.asset.json"
            pc.parent.mkdir(parents=True); pc.write_bytes(b"local PC source")
            generated = output / "scenario.json"; generated.write_bytes(b"merged data")
            record = {"source": generated.relative_to(root).as_posix(), "target": "Assets/GameContent/Test.asset",
                      "pc_source": pc.relative_to(root).as_posix(), "merge": {"objection_voice_calls": [], "objection_voice_pairs": []}}
            for name, value in {"REPO": root, "CACHE": cache, "OUTPUT": output}.items():
                stack.enter_context(patch.object(switch_story, name, value))
            stack.enter_context(patch.object(switch_story_scenario, "baseline_inputs", return_value=[root / "script.bundle"]))
            stack.enter_context(patch.object(switch_story, "_recipe_inputs", return_value=[runtime, pc]))
            export = stack.enter_context(patch.object(switch_story_scenario, "export_scenarios", return_value=[record]))
            build = stack.enter_context(patch.object(switch_story_assets, "build_assets", side_effect=lambda *args:
                {"leaves": [], "jobs": [], "addresses": []}))
            first = switch_story.prepare()
            self.assertEqual(first["status"], "PREPARED")
            self.assertEqual(switch_story.prepare()["status"], "UNCHANGED")
            runtime.write_text("voice playback fix", encoding="utf-8")
            second = switch_story.prepare()
            self.assertEqual(second["status"], "PREPARED")
            self.assertNotEqual(first["signature"], second["signature"])
            self.assertEqual(switch_story.prepare()["status"], "UNCHANGED")
            self.assertEqual(pc.read_bytes(), b"local PC source")
            self.assertEqual((export.call_count, build.call_count), (2, 2))


if __name__ == "__main__":
    unittest.main()
