from contextlib import ExitStack, redirect_stdout
from copy import deepcopy
import argparse
import io
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

from PIL import Image

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import fireworks_epilogue as epilogue
import content


def parameter(value, assigned=1):
    return {"value": value, "hasValue": assigned, "raw": {"parts": []},
            "spot": {"scriptPath": epilogue.ENDING, "lineIndex": 0, "inlineIndex": 0}}


def command(rid, name, **fields):
    return {"rid": rid, "type": {"class": name, "ns": "Example", "asm": "Example"},
            "data": {"playbackSpot": {"scriptPath": epilogue.ENDING, "lineIndex": rid, "inlineIndex": 0},
                     "indent": 0, "ConditionalExpression": parameter("", 0), **fields}}


def graph():
    commands = [command(1, "PrintTextModified", Text=parameter("original dialogue")),
                command(2, "SetCustomVariable", Expression=parameter("g_gameProgress = max(12, g_gameProgress)")),
                command(3, "Gosub", Path=parameter({"name": {"value": "System/System_Subroutine", "hasValue": 1},
                                                       "value": {"value": "EndAdv", "hasValue": 1}})),
                command(4, "ClearBacklog"), command(5, "ReturnToTitle", FadeDuration=parameter(1.8),
                                                  MinDelayDuration=parameter(0.9), Wait=parameter(1))]
    lines = [{"rid": 100 + row["rid"], "type": {"class": "CommandScriptLine"},
              "data": {"lineIndex": row["rid"], "lineHash": "original", "command": {"rid": row["rid"]}}}
             for row in commands]
    return {"m_Name": "Act02_Chapter05_Adv09", "path": epilogue.ENDING,
            "textMap": {"idToText": {"keys": ["line"], "values": ["original text"]}},
            "lines": [{"rid": x["rid"]} for x in lines],
            "playlist": {"scriptPath": epilogue.ENDING, "commands": [{"rid": x["rid"]} for x in commands]},
            "references": {"RefIds": commands + lines}, "unrelated": {"keep": True}}


def dialogue():
    return {"version": 1, "script_path": epilogue.SCRIPT, "locale": "zh-Hans", "title": "后日谈",
            "credits": ["Original author"], "lines": [
                {"id": f"Epilogue_{i+1:03}", "text": f"第{i+1}页", "speaker": None,
                 "cue": None if i < 3 else ("reveal", "quiet", "together")[i-3], "color": None}
                for i in range(6)]}


class EndingPatchTests(unittest.TestCase):
    def test_keeps_local_dialogue_non_target_fields_and_command_identity(self):
        baseline = graph()
        current = deepcopy(baseline)
        current["textMap"]["idToText"]["values"] = ["本地台词"]
        current["references"]["RefIds"][0]["data"]["Text"]["value"] = "local mutation"
        current["unrelated"]["keep"] = "user setting"
        before = deepcopy(current)
        merged, report = epilogue.patch_ending(baseline, current)
        self.assertEqual(current, before)
        self.assertEqual(merged["textMap"], current["textMap"])
        self.assertEqual(merged["playlist"], current["playlist"])
        self.assertEqual(merged["unrelated"], current["unrelated"])
        self.assertEqual(merged["references"]["RefIds"][0], current["references"]["RefIds"][0])
        tail = merged["references"]["RefIds"][4]
        self.assertEqual(tail["type"], {"class": "RouteFireworksEpilogue", "ns": "WitchTrials.Views", "asm": "Assembly-CSharp"})
        self.assertEqual(tail["data"], current["references"]["RefIds"][4]["data"])
        self.assertTrue(report["return_parameters_preserved"])
        self.assertEqual(report["changed_commands"], 1)
        self.assertEqual(report["preserved_commands"], 4)

    def test_rejects_local_terminal_parameter_change(self):
        current = graph()
        current["references"]["RefIds"][4]["data"]["FadeDuration"]["value"] = 8
        with self.assertRaisesRegex(ValueError, "locally edited ReturnToTitle"):
            epilogue.patch_ending(graph(), current)

    def test_rejects_changed_progress_anchor(self):
        current = graph()
        current["references"]["RefIds"][1]["data"]["Expression"]["value"] = "g_gameProgress = 8"
        with self.assertRaisesRegex(ValueError, "anchor was changed"):
            epilogue.patch_ending(graph(), current)

    def test_rejects_reordered_cleanup(self):
        current = graph()
        current["playlist"]["commands"][1:3] = reversed(current["playlist"]["commands"][1:3])
        with self.assertRaisesRegex(ValueError, "reordered"):
            epilogue.patch_ending(graph(), current)

    def test_does_not_accept_already_patched_pc_source(self):
        patched, _ = epilogue.patch_ending(graph(), graph())
        with self.assertRaisesRegex(ValueError, "terminal ReturnToTitle"):
            epilogue.patch_ending(graph(), patched)

    def test_disabled_route_retains_nondefault_timings_raw_values_and_wait(self):
        baseline = graph()
        original = baseline["references"]["RefIds"][4]["data"]
        for name, literal in (("FadeDuration", "1.8"), ("MinDelayDuration", "0.9"), ("Wait", "true")):
            original[name]["raw"]["parts"] = [{"id": "", "text": literal, "expression": ""}]
        merged, report = epilogue.patch_ending(baseline, deepcopy(baseline))
        tail = merged["references"]["RefIds"][4]
        self.assertEqual(tail["data"], original)
        self.assertEqual(tail["data"]["FadeDuration"]["value"], 1.8)
        self.assertEqual(tail["data"]["MinDelayDuration"]["value"], 0.9)
        self.assertEqual(tail["data"]["Wait"]["value"], 1)
        self.assertEqual(report["disabled_destination"], "ReturnToTitle")
        self.assertEqual(report["enabled_destination"], epilogue.SCRIPT)

    def test_unknown_return_schema_requires_review(self):
        baseline = graph()
        baseline["references"]["RefIds"][4]["data"]["UnreviewedBehavior"] = parameter(1)
        with self.assertRaisesRegex(ValueError, "unreviewed parameter schema"):
            epilogue.patch_ending(baseline, deepcopy(baseline))

    def test_delay_or_wait_local_change_is_not_silently_overwritten(self):
        for field, value in (("MinDelayDuration", 3), ("Wait", 0)):
            with self.subTest(field=field):
                current = graph()
                current["references"]["RefIds"][4]["data"][field]["value"] = value
                with self.assertRaisesRegex(ValueError, "locally edited ReturnToTitle"):
                    epilogue.patch_ending(graph(), current)


class DialogueRecipeTests(unittest.TestCase):
    def test_text_remains_data_and_cues_precede_pages(self):
        source = dialogue()
        source["lines"][0]["text"] = '@set "g_gameProgress=999"\n{unsafeExpression}'
        script, text_map, pages = epilogue.compile_recipe(source)
        self.assertNotIn('g_gameProgress=999', script)
        self.assertEqual(text_map["Epilogue_001"], '@set "g_gameProgress=999"<br>{unsafeExpression}')
        self.assertEqual(pages[0]["source_text"], source["lines"][0]["text"])
        self.assertLess(script.index('@back 3_1'), script.index('@fireworksMood reveal'))
        self.assertIn('time:2 wait:false', script)
        self.assertIn('@spawn TitleScreenEffect_3 params:stationary wait:true', script)
        self.assertLess(script.index('@spawn TitleScreenEffect_3'), script.index('@back 3_1'))
        self.assertIn('@fireworksMood reveal wait:false', script)
        self.assertLess(script.index('@spawn TitleScreenEffect_3'), script.index('|#Epilogue_004|'))
        self.assertEqual(script.count('@print '), 6)
        self.assertNotIn('Original author', script)
        self.assertNotIn('Epilogue_Credit', script)
        self.assertNotIn('@autoSave', script)

    def test_original_credits_share_last_page_without_new_text_or_wait(self):
        source = dialogue()
        script, text_map, pages = epilogue.compile_recipe(source)
        commands = [line for line in script.splitlines() if line.startswith('@')]
        final_print = max(i for i, line in enumerate(commands) if line.startswith('@print '))
        self.assertEqual(commands[final_print - 1], '@fireworksCredits')
        self.assertEqual(commands.count('@fireworksCredits'), 1)
        self.assertEqual(len(text_map), len(source['lines']))
        self.assertEqual(len(pages), len(source['lines']))
        self.assertIn('|#Epilogue_006|', commands[final_print])
        self.assertEqual(commands[final_print + 1:], [
            '@hidePrinter time:0.5 wait:true', '@finishFireworksEpilogue', '@returnToTitle wait:true'])
        self.assertNotIn('Original author', '\n'.join(text_map.values()))

    def test_rejects_duplicate_id_invalid_speaker_and_wrong_reveal(self):
        for mutation in ('duplicate', 'speaker', 'reveal'):
            with self.subTest(mutation=mutation):
                source = dialogue()
                if mutation == 'duplicate': source['lines'][1]['id'] = source['lines'][0]['id']
                if mutation == 'speaker': source['lines'][0]['speaker'] = 'Injected\n@set'
                if mutation == 'reveal': source['lines'][0]['cue'], source['lines'][3]['cue'] = 'reveal', None
                with self.assertRaises(ValueError): epilogue.compile_recipe(source)

    def test_path_traversal_rejected(self):
        for relative in ('../outside', '/absolute', 'a/../../x', 'C:/outside', 'a\\x'):
            with self.subTest(relative=relative), self.assertRaises(ValueError):
                epilogue.contained(Path.cwd(), relative)


class RecipeCacheTests(unittest.TestCase):
    def setUp(self):
        temp = tempfile.TemporaryDirectory()
        self.addCleanup(temp.cleanup)
        self.repo = Path(temp.name).resolve()
        self.payload = self.repo / epilogue.PAYLOAD
        self.payload.mkdir(parents=True)
        (self.payload / 'dialogue.zh-Hans.json').write_text(json.dumps(dialogue()), encoding='utf-8')
        Image.new('RGB', (1774, 887)).save(self.payload / 'empty-rooftop.png')
        Image.new('RGB', (1260, 709)).save(self.payload / 'source-poster.png')
        self.pc = self.repo / epilogue.PC_SOURCE
        self.pc.parent.mkdir(parents=True)
        self.pc.write_text(json.dumps(graph()), encoding='utf-8')
        self.baseline = self.repo / epilogue.BASELINE
        self.baseline.parent.mkdir(parents=True)
        self.baseline.write_bytes(b'fixture baseline')
        self.recipe = self.repo / 'recipe.py'
        self.recipe.write_text('version1', encoding='utf-8')
        self.inputs = [self.payload/'dialogue.zh-Hans.json', self.payload/'empty-rooftop.png',
                       self.payload/'source-poster.png', self.pc, self.baseline, self.recipe]
        stack = ExitStack()
        self.addCleanup(stack.close)
        stack.enter_context(patch.object(epilogue, '_recipe_inputs', return_value=self.inputs))
        stack.enter_context(patch.object(epilogue, '_load_bundles', return_value={epilogue.ENDING: (graph(),)}))

    def test_repeated_prepare_does_not_rewrite_sources_or_generated_files(self):
        pc_before = self.pc.read_bytes()
        self.assertEqual(epilogue.prepare(self.repo)['status'], 'PREPARED')
        output = self.repo / epilogue.OUTPUT
        stamps = {p.name: (p.read_bytes(), p.stat().st_mtime_ns) for p in output.iterdir()}
        self.assertEqual(epilogue.prepare(self.repo)['status'], 'UNCHANGED')
        self.assertEqual(stamps, {p.name: (p.read_bytes(), p.stat().st_mtime_ns) for p in output.iterdir()})
        self.assertEqual(self.pc.read_bytes(), pc_before)
        plan = json.loads((self.repo/epilogue.PLAN).read_text(encoding='utf-8'))
        self.assertEqual(plan['leaves'][0]['ppu'], 50)
        self.assertEqual(epilogue.scenario_sources(self.repo), {epilogue.PC_SOURCE.removeprefix('res/')})

    def test_non_source_locale_has_managed_document_and_source_fallback(self):
        epilogue.prepare(self.repo)
        plan = json.loads((self.repo / epilogue.PLAN).read_text(encoding='utf-8'))
        documents = plan.get('text_documents', [])
        self.assertEqual(len(documents), 2, 'Non-ja TextLocalizer requires a loaded managed document before TextMap fallback')
        addresses = {row['path']: row['address'] for row in plan['addresses']}
        self.assertEqual({addresses[row['target']] for row in documents}, {
            'Naninovel/Localization/zh-Hans/Text/Scripts/' + epilogue.SCRIPT,
            'Naninovel/Text/Scripts/' + epilogue.SCRIPT})
        for document in documents:
            self.assertEqual(document['script_path'], epilogue.SCRIPT)
            self.assertEqual(document['content_locale'], 'zh-Hans')
            self.assertEqual(document['records'], plan['scenario']['text_map'])
            self.assertEqual(list(document['records']), [page['id'] for page in plan['scenario']['pages']])

    def test_source_poster_is_one_intact_non_gallery_texture_and_required_input(self):
        source = self.payload / 'source-poster.png'
        original = source.read_bytes()
        epilogue.prepare(self.repo)
        plan = json.loads((self.repo / epilogue.PLAN).read_text(encoding='utf-8'))
        posters = [leaf for leaf in plan['leaves'] if leaf['target'] == epilogue.POSTER_TARGET]
        self.assertEqual(len(posters), 1)
        self.assertFalse(posters[0]['sprite'])
        self.assertEqual(posters[0]['size'], [1260, 709])
        self.assertEqual(posters[0]['source'], epilogue.PAYLOAD + 'source-poster.png')
        self.assertEqual([row['address'] for row in plan['addresses'] if row['path'] == epilogue.POSTER_TARGET],
                         ['Naninovel/FireworksEpilogue/SourcePoster'])
        self.assertEqual(source.read_bytes(), original)
        output = self.repo / epilogue.OUTPUT
        before = {p.name: p.read_bytes() for p in output.iterdir()}
        source.unlink()
        with self.assertRaisesRegex(FileNotFoundError, 'Missing fireworks'):
            epilogue.prepare(self.repo)
        self.assertEqual(before, {p.name: p.read_bytes() for p in output.iterdir()})

    def test_empty_texture_normalization_keeps_authored_png_and_excludes_poster(self):
        source = self.payload / 'empty-rooftop.png'
        before = source.read_bytes()
        epilogue.prepare(self.repo)
        plan = json.loads((self.repo / epilogue.PLAN).read_text(encoding='utf-8'))
        empty, poster = plan['leaves']
        self.assertEqual(empty['target'], epilogue.EMPTY_TARGET)
        self.assertFalse(empty['sprite'])
        self.assertEqual(empty['size'], [1774, 887])
        self.assertEqual(empty['imported_size'], [2048, 1024])
        self.assertEqual(empty['npot_scale'], 'ToNearest')
        self.assertEqual(empty['max_texture_size'], 2048)
        self.assertEqual(source.read_bytes(), before)
        self.assertNotIn('npot_scale', poster)
        self.assertNotIn('imported_size', poster)
        output = self.repo / epilogue.OUTPUT
        generated = {p.name: p.read_bytes() for p in output.iterdir()}
        Image.new('RGB', (128, 64)).save(source)
        with self.assertRaisesRegex(ValueError, 'normalize to 2048x1024'):
            epilogue.prepare(self.repo)
        self.assertEqual(generated, {p.name: p.read_bytes() for p in output.iterdir()})

    def test_recipe_change_invalidates_cache_and_missing_output_is_regenerated(self):
        first = epilogue.prepare(self.repo)
        self.recipe.write_text('version2', encoding='utf-8')
        second = epilogue.prepare(self.repo)
        self.assertNotEqual(first['signature'], second['signature'])
        (self.repo/epilogue.OUTPUT/'System_FireworksEpilogue.nani').unlink()
        self.assertEqual(epilogue.prepare(self.repo)['status'], 'PREPARED')

    def test_missing_baseline_and_conflict_fail_before_output_writes(self):
        epilogue.prepare(self.repo)
        output = self.repo / epilogue.OUTPUT
        before = {p.name: p.read_bytes() for p in output.iterdir()}
        self.baseline.unlink()
        with self.assertRaisesRegex(FileNotFoundError, 'Missing fireworks'): epilogue.prepare(self.repo)
        self.baseline.write_bytes(b'fixture baseline')
        altered = graph()
        altered['references']['RefIds'][4]['data']['Wait']['value'] = 0
        self.pc.write_text(json.dumps(altered), encoding='utf-8')
        with self.assertRaisesRegex(ValueError, 'conflict'): epilogue.prepare(self.repo)
        self.assertEqual(before, {p.name: p.read_bytes() for p in output.iterdir()})

    def test_dry_run_writes_nothing(self):
        self.assertEqual(epilogue.prepare(self.repo, dry_run=True)['status'], 'WOULD_PREPARE')
        self.assertFalse((self.repo / epilogue.OUTPUT).exists())


class SyncGateTests(unittest.TestCase):
    def test_import_prepares_epilogue_after_bootstrap_before_first_unity_gate(self):
        with tempfile.TemporaryDirectory() as temp, ExitStack() as stack, redirect_stdout(io.StringIO()):
            repo = Path(temp).resolve()
            res, pipeline, game = repo / 'res', repo / 'tools/unity-rebuild', repo / 'game'
            res.mkdir(parents=True)
            pipeline.mkdir(parents=True)
            (game / 'manosaba_Data').mkdir(parents=True)
            (res / '_index.csv').write_text('', encoding='utf-8')
            (pipeline / 'bootstrap_import.py').write_text('', encoding='utf-8')
            for name, value in {'REPO': repo, 'RES': res, 'PROJECT': repo/'UnityProject',
                                'PIPELINE': pipeline, 'BASELINE': res/'.manosaba/base-hashes.json'}.items():
                stack.enter_context(patch.object(content, name, value))
            events = []
            stack.enter_context(patch.object(content, 'run', side_effect=lambda command:
                events.append(Path(command[1]).name)))
            stack.enter_context(patch.object(content, 'restore_evidence', side_effect=lambda: events.append('restore')))
            stack.enter_context(patch.object(content, 'inventory', return_value={}))
            prepare = stack.enter_context(patch.object(epilogue, 'prepare', side_effect=lambda root:
                (events.append('epilogue'), {'status': 'UNCHANGED'})[1]))
            stack.enter_context(patch.object(content, 'apply_unity', side_effect=lambda *a, **k: events.append('unity')))
            stack.enter_context(patch.object(content, 'sync', side_effect=lambda args: (events.append('sync'), 0)[1]))
            args = argparse.Namespace(game=game, overwrite_user_changes=False, reextract=False, timeout=30)
            self.assertEqual(content.import_content(args), 0)
            self.assertEqual(events, ['import_source_cache.py', 'restore', 'bootstrap_import.py', 'epilogue', 'unity', 'sync'])
            prepare.assert_called_once_with(repo)

    def test_no_pending_sync_prepares_recipe_before_unity_gate(self):
        with tempfile.TemporaryDirectory() as temp, ExitStack() as stack, redirect_stdout(io.StringIO()):
            repo = Path(temp)
            for name, value in {'REPO': repo, 'RES': repo/'res', 'PROJECT': repo/'UnityProject',
                                'STATE': repo/'res/state.json'}.items():
                stack.enter_context(patch.object(content, name, value))
            events = []
            stack.enter_context(patch.object(epilogue, 'prepare', side_effect=lambda *a, **k:
                (events.append('prepare'), {'status': 'DISABLED'})[1]))
            stack.enter_context(patch.object(content, 'refresh_plans', side_effect=lambda: events.append('plans')))
            stack.enter_context(patch.object(content, 'inventory', return_value={}))
            stack.enter_context(patch.object(content, 'read_content_state', return_value={'version': 1, 'files': {}}))
            stack.enter_context(patch.object(content, 'apply_unity', side_effect=lambda *a, **k:
                (events.append('unity'), {'status': 'OK'})[1]))
            stack.enter_context(patch.object(content, 'acknowledge_extension_targets', return_value=0))
            args = argparse.Namespace(dry_run=False, copy_only=False, file=[], category='', force=False,
                                      verbose=False, timeout=30, allow_resource_map_mismatch=True)
            self.assertEqual(content.sync(args), 0)
            self.assertEqual(events, ['prepare', 'plans', 'unity'])


if __name__ == '__main__':
    unittest.main()
