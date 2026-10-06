"""Regression checks for prefab recipes that require a complete rebuild."""

from pathlib import Path
import json
import sys
import tempfile
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import content


class AssertRebuiltTests(unittest.TestCase):
    """Keep the stale-Editor guard aligned with prefab recipe semantics."""

    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.root = Path(self.temp.name)
        self.plan_name = "prefab_plan.json"
        self.plan = self.root / self.plan_name
        self.court = "Assets/GameContent/#WitchTrials/Prefabs/General/PerspectiveStage/Court.prefab"
        self.court_final = "Assets/GameContent/#WitchTrials/Prefabs/General/PerspectiveStage/Court_Final.prefab"
        self.debate_ui = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/DebateUI.prefab"
        self.printer = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/TextPrinters/DebatePrinter.prefab"
        self.log_ui = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/LogUI.prefab"
        self.animated = "Assets/GameContent/#WitchTrials/Prefabs/Animated.prefab"
        self.plain = "Assets/GameContent/#WitchTrials/Prefabs/Plain.prefab"
        self.sources = {
            self.court: "res/Assets/Prefabs/Court.asset.json",
            self.court_final: "res/Assets/Prefabs/Court_Final.asset.json",
            self.debate_ui: "res/Assets/Prefabs/DebateUI.asset.json",
            self.printer: "res/Assets/Prefabs/DebatePrinter.asset.json",
            self.log_ui: "res/Assets/Prefabs/LogUI.asset.json",
            self.animated: "res/Assets/Prefabs/Animated.asset.json",
            self.plain: "res/Assets/Prefabs/Plain.asset.json",
        }
        # Court recipes must rebuild even when their plan has no AnimationClip entry;
        # the animated recipe keeps the original AnimationClip-based rule intact.
        self.plan.write_text(json.dumps({"jobs": [
            {"asset_path": self.court, "objects": []},
            {"asset_path": self.court_final, "objects": []},
            # Reference-driven: the clock sprite asset and the debate style sheet are regenerated, so their
            # prefabs rebuild; LogMessageStyleSheet is not regenerated, so LogUI may still be patched.
            {"asset_path": self.debate_ui, "objects": [],
             "references": {"1:5:8008108086768730382": "Assets/#WitchTrials/Fonts/Common/TrialsClockFont.asset"}},
            {"asset_path": self.printer, "objects": [],
             "references": {"1:3:8401695592146518297": "Assets/#WitchTrials/Fonts/Common/DebatePrinterStyleSheet.asset"}},
            {"asset_path": self.log_ui, "objects": [],
             "references": {"1:5:7541902232382682901": "Assets/#WitchTrials/Fonts/Common/LogMessageStyleSheet.asset"}},
            {"asset_path": self.animated, "objects": [{"type": "AnimationClip"}]},
            {"asset_path": self.plain, "objects": [{"type": "SpriteRenderer"}]},
        ]}), encoding="utf-8")
        self.repo_patch = patch.object(content, "REPO", self.root)
        self.repo_patch.start()
        self.addCleanup(self.repo_patch.stop)
        self.rebuilt_before = content._REBUILT_ASSETS.copy()
        content._REBUILT_ASSETS.clear()
        self.addCleanup(self._restore_rebuilt_cache)

    def _restore_rebuilt_cache(self):
        content._REBUILT_ASSETS.clear()
        content._REBUILT_ASSETS.update(self.rebuilt_before)
        self.temp.cleanup()

    def jobs(self):
        return [{"source": source, "category": "PREFAB", "rebuild": True,
                 "plan": self.plan_name, "asset_path": asset_path}
                for asset_path, source in self.sources.items()]

    def result(self, statuses):
        return {"jobs": [{"source": source, "status": statuses[asset_path]}
                          for asset_path, source in self.sources.items()]}

    def test_court_and_animationclip_prefabs_require_rebuilt_but_plain_prefab_may_patch(self):
        required = (self.court, self.court_final, self.debate_ui, self.printer, self.animated)
        statuses = {asset_path: ("REBUILT" if asset_path in required else "PATCHED")
                    for asset_path in self.sources}

        # A normal prefab may still be field-patched during a recipe rebuild batch.
        content.assert_rebuilt(self.jobs(), self.result(statuses))

        # Each generated-subasset recipe must be acknowledged as a full rebuild.
        for asset_path in required:
            stale = dict(statuses)
            stale[asset_path] = "PATCHED"
            with self.subTest(asset_path=asset_path), self.assertRaisesRegex(
                    RuntimeError, rf"first: {self.sources[asset_path]}"):
                content.assert_rebuilt(self.jobs(), self.result(stale))


class RefreshPlansTests(unittest.TestCase):
    """A regenerated plan must be re-recorded in the evidence manifest, or restore_evidence() rejects it later."""

    def test_regenerated_plan_is_snapshotted(self):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            pipeline, cache, out, res = root / "pipeline", root / "res/.manosaba/reconstruction", root / "out", root / "res"
            pipeline.mkdir()
            (pipeline / "a13e_plan_prefabs.py").write_text("print('planner')\n", encoding="utf-8")
            (cache / "unity").mkdir(parents=True)
            calls = []

            def fake_run(command, env=None, timeout=None):
                calls.append([Path(str(part)).name for part in command[1:]])
                if calls[-1][0] == "a13e_plan_prefabs.py":
                    out.mkdir(parents=True, exist_ok=True)
                    (out / "prefab_plan_all.json").write_text("{}", encoding="utf-8")

            with patch.multiple(content, PIPELINE=pipeline, CACHE=cache, OUT=out, RES=res, run=fake_run,
                                restore_evidence=lambda: None):
                content.refresh_plans()
                self.assertEqual(calls[0][0], "a13e_plan_prefabs.py")
                self.assertEqual(calls[1][0], "import_source_cache.py")
                self.assertIn("--snapshot-only", calls[1])
                calls.clear()
                content.refresh_plans()          # planner unchanged: nothing is regenerated or re-recorded
                self.assertEqual(calls, [])


if __name__ == "__main__":
    unittest.main()
