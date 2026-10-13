import copy
import importlib.util
import json
from pathlib import Path
import tempfile
from types import SimpleNamespace
import unittest
from unittest.mock import patch
import weakref

MODULE = Path(__file__).resolve().parents[1] / "switch_story_scenario.py"
spec = importlib.util.spec_from_file_location("switch_story_scenario", MODULE)
story = importlib.util.module_from_spec(spec)
spec.loader.exec_module(story)


def parameter(value):
    return {"hasValue": 1, "value": value, "raw": {"parts": [{"id": "", "text": str(value), "expression": ""}]}}


def command(kind, **values):
    return {"type": {"class": kind, "ns": "Test", "asm": "Test"},
            "data": {"indent": 0, **{k: parameter(v) for k, v in values.items()}}}


def dialogue(key):
    c = command("PrintTextModified")
    c["data"]["Text"] = {"hasValue": 1, "value": {"parts": [{"id": key, "text": ""}]},
                          "raw": {"parts": [{"id": key, "text": "", "expression": ""}]}}
    return c


def cg(name):
    c = command("ModifyBackgroundExtended", Id="Stills")
    c["data"]["AppearanceAndTransition"] = parameter({"name": {"value": name, "hasValue": 1}})
    return c


def graph(commands, path="Act01_Chapter03/Act01_Chapter03_Bad02"):
    refs, lines, playlist = [], [], []
    for i, command_ in enumerate(commands):
        c = copy.deepcopy(command_)
        c["rid"] = 1000 + i
        c["data"]["playbackSpot"] = {"scriptPath": path, "lineIndex": i, "inlineIndex": 0}
        refs.append(c)
        inline = c["type"]["class"] == "PrintTextModified"
        data = {"lineIndex": i, "indent": 0, "lineHash": f"original-{i}"}
        data["inlinedCommands" if inline else "command"] = [{"rid": c["rid"]}] if inline else {"rid": c["rid"]}
        refs.append({"rid": 2000 + i, "type": {"class": "GenericTextScriptLine" if inline else "CommandScriptLine", "ns": "Test", "asm": "Test"}, "data": data})
        lines.append({"rid": 2000 + i}); playlist.append({"rid": c["rid"]})
    return {"m_Name": path.split("/")[-1], "path": path, "lines": lines,
            "playlist": {"scriptPath": path, "commands": playlist},
            "textMap": {"idToText": {"keys": ["a", "b"], "values": ["Original A", "Original B"]}},
            "references": {"RefIds": refs}}


def dynamic_objection_graphs():
    path = story.DYNAMIC_OBJECTION_SCRIPT
    base_commands = [dialogue("intro"), command("GosubToObjectionCutIn", CutInKind="Ema", Index=2),
                     dialogue("before-double"), command("PlayVoice", VoicePath=story.OBJECTION_PAIR_VOICES[0]),
                     command("PlayVoice", VoicePath=story.OBJECTION_PAIR_VOICES[1]),
                     command("GosubToObjectionCutIn", CutInKind="Ema", Index=2),
                     command("Gosub", Path="System/System_Subroutine.EndDebate-Last"), dialogue("after-double")]
    source_commands = copy.deepcopy(base_commands)
    source_commands[1] = command("GosubToObjectionCutIn", CutInId="Ema", Index=2)
    source_commands[5] = command("GosubToObjectionCutIn", CutInId="w", Index=0,
                                 Path="System/System_Subroutine.EndDebate-CutIn", VoicePath="source_voice")
    return graph(base_commands, path), graph(source_commands, path)


class StoryMergeTests(unittest.TestCase):
    def test_dynamic_objection_and_voice_pair_preserve_other_local_fields(self):
        base, source = dynamic_objection_graphs()
        local = copy.deepcopy(base)
        _, commands, _ = story._graph(local)
        commands[3]["data"]["Volume"] = parameter(0.6)
        commands[4]["data"]["AuthorId"] = parameter("local_author")
        commands[5]["data"]["VoicePath"] = parameter("local_command_voice")
        commands[5]["data"]["Volume"] = parameter(0.4)
        local["textMap"]["idToText"]["values"][0] = "Local translation"
        before = copy.deepcopy(local)
        result, report = story.merge_scenario(source, base, local)
        expected = copy.deepcopy(local)
        expected_commands = story._graph(expected)[1]
        edited = expected_commands[5]
        edited["data"]["CutInKind"] = parameter("w")
        edited["data"]["Index"] = parameter(0)
        expected_commands[3]["type"] = {"class": "PlayObjectionVoicePair", "ns": "WitchTrials.Views", "asm": "Assembly-CSharp"}
        for field in story.OBJECTION_VOICE_FIELDS:
            if field in expected_commands[4]["data"]:
                expected_commands[3]["data"]["Second" + field] = copy.deepcopy(expected_commands[4]["data"][field])
        del expected_commands[4]
        actual_commands = story._graph(result)[1]
        self.assertEqual([story._key(c) for c in actual_commands], [story._key(c) for c in expected_commands])
        self.assertEqual(story._value(actual_commands[1], "CutInKind"), "Ema")
        self.assertNotIn("CutInId", actual_commands[4]["data"])
        self.assertNotIn("Path", actual_commands[4]["data"])
        self.assertEqual(result["textMap"], local["textMap"])
        self.assertEqual(local, before)
        self.assertEqual(report["dynamic_objection_calls"], {"ObjectionCutIn_w": 1})
        self.assertEqual(report["cutin_calls"], {})
        self.assertEqual(report["parameter_updated_commands"], 1)
        self.assertEqual(report["retained_commands_verified"], len(commands) - 3)
        self.assertEqual(report["removed_commands"], 1)
        self.assertEqual(report["objection_voice_pairs"][0]["local_parameters_preserved"], ["Volume", "SecondAuthorId"])
        self.assertEqual([row["field"] for row in report["parameter_updates"][0]["fields"]], ["CutInKind", "Index"])
        refs, _, lines = story._graph(result)
        self.assertNotEqual(lines[actual_commands[4]["rid"]]["data"]["lineHash"], "original-5")
        self.assertEqual(lines[actual_commands[1]["rid"]]["data"]["lineHash"], "original-1")
        self.assertEqual(story.merge_scenario(source, base, local)[0], result)

    def test_dynamic_objection_conflicting_local_kind_or_index_fails_without_changes(self):
        base, source = dynamic_objection_graphs()
        for field, value in (("CutInKind", "Hiro"), ("Index", 1)):
            with self.subTest(field=field):
                local = copy.deepcopy(base)
                story._graph(local)[1][5]["data"][field] = parameter(value)
                before = copy.deepcopy(local)
                with self.assertRaisesRegex(ValueError, "parameter edit conflicts.*" + field):
                    story.merge_scenario(source, base, local)
                self.assertEqual(local, before)

    def test_dynamic_objection_accepts_an_identical_local_fix(self):
        base, source = dynamic_objection_graphs()
        local, _ = story.merge_scenario(source, base, base)
        result, report = story.merge_scenario(source, base, local)
        self.assertEqual(result, local)
        self.assertEqual(report["parameter_updated_commands"], 0)
        self.assertEqual(report["objection_voice_pair_updated_commands"], 0)
        self.assertEqual(report["dynamic_objection_calls"], {"ObjectionCutIn_w": 1})

    def test_dynamic_objection_rejects_missing_or_ambiguous_local_target(self):
        base, source = dynamic_objection_graphs()
        commands = story._graph(base)[1]
        for replacement in ([], [commands[5], commands[5]]):
            with self.subTest(count=len(replacement)):
                local = graph(commands[:5] + replacement + commands[6:], story.DYNAMIC_OBJECTION_SCRIPT)
                with self.assertRaisesRegex(ValueError, "absent or ambiguous"):
                    story.merge_scenario(source, base, local)

    def test_dynamic_objection_rejects_changed_source_contract_or_anchor(self):
        base, source = dynamic_objection_graphs()
        changed = copy.deepcopy(source)
        story._graph(changed)[1][5]["data"]["Index"] = parameter(1)
        with self.assertRaisesRegex(ValueError, "reviewed literal: Index"):
            story.merge_scenario(changed, base, base)
        local_commands = story._graph(base)[1]
        duplicate_anchor = graph([dialogue("before-double"), *local_commands], story.DYNAMIC_OBJECTION_SCRIPT)
        with self.assertRaisesRegex(ValueError, "dialogue anchors.*ambiguous"):
            story.merge_scenario(source, base, duplicate_anchor)

    def test_three_way_preserves_local_text_music_and_input(self):
        base = graph([dialogue("a"), command("PlayBgm", BgmPath="pc_song"), cg("old"), command("ProcessInput", InputEnabled=True), dialogue("b")])
        source = graph([dialogue("a"), command("PlayBgm", BgmPath="switch_song"), command("AutoSave"), cg("730_1"), command("ProcessInput", InputEnabled=False), dialogue("b")])
        local = copy.deepcopy(base)
        local["textMap"]["idToText"]["values"][0] = "Local translation"
        story._graph(local)[1][1]["data"]["BgmPath"] = parameter("local_song")
        result, report = story.merge_scenario(source, base, local)
        _, commands, _ = story._graph(result)
        self.assertEqual(result["textMap"], local["textMap"])
        self.assertEqual([story._value(c, "BgmPath") for c in commands if story._class(c) == "PlayBgm"], ["local_song"])
        self.assertEqual([story._value(c, "InputEnabled") for c in commands if story._class(c) == "ProcessInput"], [True])
        self.assertNotIn("AutoSave", [story._class(c) for c in commands])
        self.assertEqual(report["cg_calls"], {"730_1": 1})
        self.assertEqual(story._graph(result)[0][result["lines"][0]["rid"]]["data"]["lineHash"], "original-0")
        self.assertEqual(story.merge_scenario(source, base, local)[0], result)

    def test_conflicting_local_visual_edit_fails(self):
        base, source = graph([dialogue("a"), cg("old"), dialogue("b")]), graph([dialogue("a"), cg("730_1"), dialogue("b")])
        local = copy.deepcopy(base)
        story._graph(local)[1][1]["data"]["Scale"] = parameter(2)
        before = copy.deepcopy(local)
        with self.assertRaisesRegex(ValueError, "conflicts"):
            story.merge_scenario(source, base, local)
        self.assertEqual(local, before)

    def test_cutin_replaces_camera_preserves_input_recovery(self):
        path = "Act02_Chapter06/Act02_Chapter06_Trial01"
        recovery = command("Gosub", Path="System/System_Subroutine.EndDebate-Last")
        base = graph([dialogue("a"), command("GosubToSplitPerspectiveStageScreens"), command("Wait", WaitMode="1"), command("PlayVoice", VoicePath="voice"), command("GosubToObjectionCutIn"), recovery, command("HidePerspectiveStageScreen"), dialogue("b")], path)
        source = graph([dialogue("a"), command("SpawnExtended", Path="CutinW1"), command("Wait", WaitMode="0.6"), command("PlayVoice", VoicePath="voice"), command("DestroySpawnedExtended", Path="CutinW1"), command("StopVoice"), dialogue("b")], path)
        result, report = story.merge_scenario(source, base, base)
        commands = story._graph(result)[1]
        kinds = [story._class(c) for c in commands]
        self.assertEqual(report["cutin_calls"], {"CutinW1": 1})
        self.assertEqual(kinds.count("Gosub"), 1)
        self.assertNotIn("GosubToSplitPerspectiveStageScreens", kinds)
        self.assertNotIn("GosubToObjectionCutIn", kinds)
        self.assertEqual([story._raw(c, "Path") for c in commands if story._class(c) == "Gosub"], ["System/System_Subroutine.EndDebate-Last"])

    def test_missing_baseline_does_not_use_editable_source(self):
        with tempfile.TemporaryDirectory() as folder:
            with self.assertRaisesRegex(ValueError, "Missing original PC"):
                story.baseline_inputs(Path(folder))

    def test_local_conditional_cannot_be_escaped_by_new_visual(self):
        base = graph([dialogue("a"), dialogue("b")])
        source = graph([dialogue("a"), cg("730_1"), dialogue("b")])
        local = copy.deepcopy(base)
        refs, commands, lines = story._graph(local)
        commands[1]["data"]["indent"] = 1
        lines[commands[1]["rid"]]["data"]["indent"] = 1
        with self.assertRaisesRegex(ValueError, "conditional context"):
            story.merge_scenario(source, base, local)

    def test_output_boundary_rejects_escape(self):
        with tempfile.TemporaryDirectory() as folder:
            with self.assertRaisesRegex(ValueError, "escapes"):
                story._inside(Path(folder), Path(folder) / ".." / "outside")

    def test_single_voice_only_imports_parameters_and_preserves_pc_index_and_flow(self):
        path = "Act02_Chapter06/Act02_Chapter06_Trial04"
        base = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInKind="Hiro", Index=3),
                      command("Gosub", Path="PC_recovery"), dialogue("b")], path)
        source = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInId="Hiro", Index=0,
                       VoicePath="CommonTrial_Hiro00024", Volume=1.0, GroupPath="group", AuthorId="Hiro"),
                        command("Gosub", Path="NS_different_recovery"), dialogue("b")], path)
        local = copy.deepcopy(base)
        story._graph(local)[1][1]["data"]["Index"] = parameter(2)
        local["textMap"]["idToText"]["values"][0] = "Local dialogue"
        result, report = story.merge_scenario(source, base, local)
        commands = story._graph(result)[1]
        self.assertEqual(story._value(commands[1], "Index"), 2)
        self.assertEqual(story._value(commands[1], "CutInKind"), "Hiro")
        self.assertEqual(story._value(commands[2], "Path"), "PC_recovery")
        self.assertEqual(result["textMap"], local["textMap"])
        for field in story.OBJECTION_VOICE_FIELDS:
            self.assertEqual(story._value(commands[1], field), story._value(story._graph(source)[1][1], field))
        self.assertEqual(report["removed_commands"], 0)
        self.assertEqual(report["inserted_commands"], 0)
        self.assertEqual(report["objection_voice_updated_commands"], 1)
        self.assertEqual(report["objection_voice_calls"][0]["merged_command"], 1)
        self.assertEqual(story.merge_scenario(source, base, result)[0], result)
        self.assertEqual(story.merge_scenario(source, base, result)[1]["objection_voice_updated_commands"], 0)

    def test_single_voice_conflicting_local_parameter_fails_without_mutating_input(self):
        path = "Act02_Chapter06/Act02_Chapter06_Trial04"
        base = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInKind="Ema", Index=1), dialogue("b")], path)
        source = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInId="Ema", Index=1,
                       VoicePath="CommonTrial_Ema05122", Volume=1.0), dialogue("b")], path)
        for field, value in (("VoicePath", "local_voice"), ("Volume", 0.4)):
            local = copy.deepcopy(base)
            story._graph(local)[1][1]["data"][field] = parameter(value)
            before = copy.deepcopy(local)
            with self.assertRaisesRegex(ValueError, "voice parameter edit conflicts.*" + field):
                story.merge_scenario(source, base, local)
            self.assertEqual(local, before)

    def test_single_voice_keeps_unspecified_options_and_accepts_serialized_default_volume(self):
        path = "Act02_Chapter05/Act02_Chapter05_Trial01"
        base = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInKind="CreatureHiro", Index=3), dialogue("b")], path)
        source = graph([dialogue("a"), command("GosubToObjectionCutIn", CutInId="CreatureHiro", Index=0,
                       VoicePath="CommonTrial_Hiro00024", Volume=1.0), dialogue("b")], path)
        story._graph(source)[1][1]["data"]["Volume"]["raw"]["parts"] = []
        local = copy.deepcopy(base)
        command_ = story._graph(local)[1][1]
        command_["data"]["AuthorId"] = parameter("local_author")
        result, report = story.merge_scenario(source, base, local)
        updated = story._graph(result)[1][1]
        self.assertEqual(story._value(updated, "AuthorId"), "local_author")
        self.assertEqual(story._value(updated, "Index"), 3)
        self.assertEqual(story._value(updated, "Volume"), 1.0)
        self.assertEqual(report["objection_voice_calls"][0]["kind"], "CreatureHiro")

    def test_voice_pair_conflicting_voice_or_nonconsecutive_commands_fail(self):
        base, source = dynamic_objection_graphs()
        local = copy.deepcopy(base)
        story._graph(local)[1][3]["data"]["VoicePath"] = parameter("local_voice")
        with self.assertRaisesRegex(ValueError, "voice pair edit conflicts"):
            story.merge_scenario(source, base, local)
        commands = story._graph(base)[1]
        local = graph(commands[:4] + [command("Wait", WaitMode="1")] + commands[4:], story.DYNAMIC_OBJECTION_SCRIPT)
        with self.assertRaisesRegex(ValueError, "voice pair edit conflicts"):
            story.merge_scenario(source, base, local)

    def test_unrelated_playvoice_and_source_double_voice_count_are_preserved(self):
        base, source = dynamic_objection_graphs()
        base = graph([command("PlayVoice", VoicePath="unrelated_voice"), *story._graph(base)[1]], story.DYNAMIC_OBJECTION_SCRIPT)
        source = graph([command("PlayVoice", VoicePath="unrelated_voice"), *story._graph(source)[1]], story.DYNAMIC_OBJECTION_SCRIPT)
        result, report = story.merge_scenario(source, base, base)
        commands = story._graph(result)[1]
        pairs = [c for c in commands if story._class(c) == "PlayObjectionVoicePair"]
        self.assertEqual(len(pairs), 1)
        self.assertEqual([story._value(pairs[0], f) for f in ("VoicePath", "SecondVoicePath")], list(story.OBJECTION_PAIR_VOICES))
        self.assertEqual(report["objection_voice_pairs"][0]["result_commands"], 1)
        self.assertEqual(report["objection_voice_pairs"][0]["replaced_commands"], 2)
        self.assertEqual([story._value(c, "VoicePath") for c in commands if story._class(c) == "PlayVoice"], ["unrelated_voice"])


class ScenarioExportTests(unittest.TestCase):
    def setUp(self):
        self.temporary = tempfile.TemporaryDirectory()
        self.addCleanup(self.temporary.cleanup)
        self.repo = Path(self.temporary.name)
        self.source = self.repo / "switch"
        self.output = self.repo / "output"
        self.source.mkdir()
        self.baseline = self.repo / story.BASELINE_ROOT
        self.baseline.mkdir(parents=True)
        self.names = ["first.bundle", "second.bundle"]
        for name in self.names:
            (self.source / name).write_bytes(b"switch")
            (self.baseline / name).write_bytes(b"pc")
        self.paths = sorted(story.CG_SCRIPTS | {f"Act01_Chapter01/Extra{i:02}" for i in range(42)})
        self.paths.append("Act02_Chapter06/VoiceOnly")
        for path in self.paths:
            target = self.repo / f"res/Assets/#WitchTrials/Scenarios/{path}.asset.json"
            target.parent.mkdir(parents=True, exist_ok=True)
            local = graph([dialogue("local")], path)
            local["textMap"]["idToText"]["values"][0] = "本地译文：烟花🌟"
            target.write_bytes(("\ufeff" + json.dumps(local, ensure_ascii=False)).encode())

    def scripts(self, bundles):
        is_source = bundles[0].parent.resolve() == self.source.resolve()
        for i, path in enumerate(reversed(self.paths)):
            commands = ([command("GosubToObjectionCutIn", CutInId="Hiro", VoicePath="voice")]
                        if path.endswith("VoiceOnly") else [cg("730_1")])
            yield graph(commands if is_source else [dialogue("base")], path), bundles[i % 2], "sha", i

    @staticmethod
    def merged(source, baseline, local):
        first = source["path"] == "Act01_Chapter01/Extra00"
        return local, {"cg_calls": {"730_1": 22} if first else {},
                       "cutin_calls": {"CutinW1": 62} if first else {},
                       "dynamic_objection_calls": {"ObjectionCutIn_w": 1} if first else {},
                       "objection_voice_calls": [{}] * (189 if first else 0),
                       "objection_voice_pairs": [{}] if first else []}

    def export(self, merge=None):
        with patch.object(story, "_bundle_names", return_value=self.names), \
             patch.object(story, "_iter_bundle_scripts", side_effect=self.scripts), \
             patch.object(story, "merge_scenario", side_effect=merge or self.merged):
            return story.export_scenarios(self.repo, self.source, self.output)

    def test_staged_export_preserves_bytes_records_order_and_idempotence(self):
        records = self.export()
        self.assertEqual(len(records), 51)
        self.assertEqual([r["pc_source"] for r in records],
                         [f"res/Assets/#WitchTrials/Scenarios/{p}.asset.json" for p in sorted(self.paths)])
        mtimes = {}
        for record in records:
            output = self.repo / record["source"]
            expected = json.loads((self.repo / record["pc_source"]).read_bytes())
            expected_bytes = (json.dumps(expected, ensure_ascii=False, indent=1, allow_nan=False) + "\n").encode()
            self.assertEqual(output.read_bytes(), expected_bytes)
            self.assertEqual(story._file_sha256(output), record["merge"]["output_sha256"])
            mtimes[output] = output.stat().st_mtime_ns
        self.assertEqual(records, self.export())
        self.assertEqual(mtimes, {p: p.stat().st_mtime_ns for p in mtimes})
        self.assertEqual(list(self.output.glob(".scenario-prepare-*")), [])

    def test_late_merge_failure_does_not_publish_or_change_previous_outputs(self):
        records = self.export()
        before = {self.repo / r["source"]: (self.repo / r["source"]).read_bytes() for r in records}
        def fail_last(source, baseline, local):
            if source["path"] == sorted(self.paths)[-1]:
                raise ValueError("late local conflict")
            local["textMap"]["idToText"]["values"][0] = "different candidate"
            return self.merged(source, baseline, local)
        with self.assertRaisesRegex(ValueError, "late local conflict"):
            self.export(fail_last)
        self.assertEqual(before, {p: p.read_bytes() for p in before})
        self.assertEqual(list(self.output.glob(".scenario-prepare-*")), [])

    def test_aggregate_validation_failure_does_not_publish_any_outputs(self):
        def missing_count(source, baseline, local):
            merged, report = self.merged(source, baseline, local)
            report["objection_voice_calls"] = []
            return merged, report
        with self.assertRaisesRegex(ValueError, "call counts changed"):
            self.export(missing_count)
        self.assertFalse((self.output / "scenarios").exists())
        self.assertEqual(list(self.output.iterdir()), [])

    def test_bundle_iterator_releases_environment_cycles_and_rejects_duplicate_ids(self):
        references = []
        duplicate = False
        class Environment:
            def __init__(self, script):
                self.cycle = self
                self.objects = [SimpleNamespace(type=SimpleNamespace(name="MonoBehaviour"), path_id=1,
                                                read_typetree=lambda: script)]
        def load(raw):
            if references:
                self.assertIsNone(references[-1](), "previous UnityPy bundle is still alive")
            path = "duplicate" if duplicate else str(len(references))
            env = Environment(graph([], path))
            references.append(weakref.ref(env))
            return env
        with patch.object(story.UnityPy, "load", side_effect=load):
            records = list(story._iter_bundle_scripts([self.source / n for n in self.names]))
            self.assertEqual([r[0]["path"] for r in records], ["0", "1"])
            self.assertTrue(all(ref() is None for ref in references))
            duplicate = True
            with self.assertRaisesRegex(ValueError, "Duplicate script identity"):
                list(story._iter_bundle_scripts([self.source / n for n in self.names]))
            self.assertTrue(all(ref() is None for ref in references))


if __name__ == "__main__":
    unittest.main()
