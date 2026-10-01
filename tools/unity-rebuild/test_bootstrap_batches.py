"""Fresh-import batch plans and native-schema reconciliation without launching Unity."""
from contextlib import redirect_stdout
from copy import deepcopy
from datetime import datetime, timezone
from io import StringIO
import json
import os
from pathlib import Path
import re
import tempfile
import unittest
from unittest.mock import patch

import bootstrap_import as bootstrap


class BootstrapBatchTests(unittest.TestCase):
    def setUp(self):
        scratch = bootstrap.REPO / "tools/unity-rebuild/out/bootstrap-tests"
        scratch.mkdir(parents=True, exist_ok=True)
        self.temp = tempfile.TemporaryDirectory(dir=scratch)
        self.out = Path(self.temp.name).resolve()
        self.out.relative_to(scratch.resolve())
        self.addCleanup(self.cleanup)
        self.override = patch.object(bootstrap, "OUT", self.out)
        self.override.start()
        self.addCleanup(self.override.stop)
        self.calls = []

    def cleanup(self):
        self.out.relative_to((bootstrap.REPO / "tools/unity-rebuild/out/bootstrap-tests").resolve())
        self.temp.cleanup()

    def write_plan(self, name, count, prefab=False, absolute_targets=False):
        jobs = [{"asset_path": f"Assets/Shipped/{name}-{number}", "target": f"Assets/GameContent/{name}-{number}.asset",
                 "dump": f"res/{name}-{number}.json", "type": "Naninovel.Script", "assembly": "Elringus.Naninovel.Runtime"}
                for number in range(count)]
        if absolute_targets:
            for job in jobs:
                job["target"] = str(self.out / "UnityProject" / job["target"])
        if prefab:
            for number, job in enumerate(jobs):
                job["objects"] = [{"path_id": str(number * 3 + index), "dump": f"res/node-{number}-{index}.json"}
                                  for index in range(3)]
                job["references"] = {f"{number}:1:42": "Assets/Shipped/Texture.png"}
                job["depends_on"] = [jobs[number - 1]["asset_path"]] if number else []
        doc = {"generated": "canonical-plan", "family": "fixture", "job_count": count, "jobs": jobs}
        bootstrap.write_json(self.out / name, doc)
        return doc

    def fake_unity(self, method, receipt_name, extra, tag):
        kind = "so" if "ScriptableObjectConverter" in method else "prefab"
        prefix = "MANOSABA_SO" if kind == "so" else "MANOSABA_PREFAB"
        plan_name = extra[prefix + "_PLAN"]
        self.assertEqual(Path(plan_name).name, plan_name)
        self.assertTrue(plan_name.startswith("bootstrap_"))
        self.assertFalse(Path(plan_name).match("so_convert_plan_*.json"))
        self.assertFalse(Path(plan_name).match("prefab_plan_*.json"))
        self.assertEqual(Path(extra[prefix + "_OUT"]), self.out / receipt_name)
        self.assertFalse((self.out / receipt_name).exists())
        plan = json.loads((self.out / plan_name).read_text(encoding="utf-8"))
        self.assertLessEqual(len(plan["jobs"]), 24)
        self.calls.append({"jobs": deepcopy(plan["jobs"]), "plan": plan_name, "receipt": receipt_name, "tag": tag})
        values = {field: len(plan["jobs"]) for field in bootstrap.CONVERSION_COUNTERS[kind]}
        values["failed"] = 0
        values["jobs_total"] = len(plan["jobs"])
        full, partial = ("converted", "converted_partial") if kind == "so" else ("built", "built_partial")
        values[full], values[partial] = (len(plan["jobs"]), 0) if kind == "so" else (0, len(plan["jobs"]))
        values.update({"status": "OK" if kind == "so" else "OK_WITH_PARTIAL_ASSETS", "jobs": [],
                       "generated": datetime.now(timezone.utc).isoformat(), "plan_pattern": plan_name,
                       "plan_files": [plan_name]})
        if kind == "prefab":
            values["incomplete"] = values["guid_changed"] = 0
            values["messages"] = ["diagnostic " + tag]
        for job in plan["jobs"]:
            target = job["target"]
            if kind == "prefab":
                target = target.replace("\\", "/")
                index = target.find("/Assets/")
                if index >= 0:
                    target = target[index + 1:]
            values["jobs"].append({"target": target, "dump": job["dump"], "plan_file": plan_name,
                                   "status": "CONVERTED" if kind == "so" else "BUILT_PARTIAL",
                                   "guid_before": "a" * 32, "guid_after": "a" * 32,
                                   "references_planned": 7, "pointers_sidecar": 3,
                                   "missing_asset_paths": ["Assets/ReadOnly/Unsupported.asset"],
                                   "extra_root_prefabs": ["Assets/GameContent/ExtraRoot.prefab"]})
        bootstrap.write_json(self.out / receipt_name, values)

    def run_batches(self, kind, names, callback=None):
        with redirect_stdout(StringIO()):
            return bootstrap.convert_in_batches(kind, names, callback or self.fake_unity)

    def test_full_so_families_run_in_twenty_bounded_exact_plan_batches(self):
        names = ["so_convert_plan_data.json", "so_convert_plan_scenarios.json"]
        data = self.write_plan(names[0], 8)
        scenarios = self.write_plan(names[1], 440)
        original = {name: (self.out / name).read_bytes() for name in names}
        result = self.run_batches("so", names)
        self.assertEqual(len(self.calls), 20)
        self.assertEqual(self.calls[0]["jobs"], data["jobs"])
        self.assertEqual([job for call in self.calls[1:] for job in call["jobs"]], scenarios["jobs"])
        self.assertEqual(len({call["tag"] for call in self.calls}), 20)
        self.assertEqual(result["jobs_total"], 448)
        self.assertEqual(result["converted"], 448)
        self.assertEqual(result["params_with_value_dump"], 448)
        self.assertEqual(result["params_with_value_written"], 448)
        self.assertEqual(result["status"], "OK")
        self.assertEqual(result["plan_files"], names)
        self.assertEqual(result["jobs"][0]["plan_file"], names[0])
        self.assertEqual(result["jobs"][8]["plan_file"], names[1])
        self.assertEqual(json.loads((self.out / "so_convert_result.json").read_text()), result)
        for name in names:
            self.assertEqual((self.out / name).read_bytes(), original[name])

    def test_prefab_batches_preserve_order_complete_trees_and_all_reference_counters(self):
        name = "prefab_plan_all.json"
        source = self.write_plan(name, 156, prefab=True, absolute_targets=True)
        result = self.run_batches("prefab", [name])
        self.assertEqual(len(self.calls), 7)
        self.assertEqual([job for call in self.calls for job in call["jobs"]], source["jobs"])
        for field in bootstrap.CONVERSION_COUNTERS["prefab"]:
            expected = 0 if field in ("built", "failed", "incomplete", "guid_changed") else 156
            self.assertEqual(result[field], expected, field)
        self.assertEqual(result["status"], "OK_WITH_PARTIAL_ASSETS")
        self.assertEqual(len(result["messages"]), 7)
        for job in result["jobs"]:
            self.assertTrue(job["target"].startswith("Assets/"))
            self.assertEqual(job["references_planned"], 7)
            self.assertEqual(job["pointers_sidecar"], 3)
            self.assertEqual(job["missing_asset_paths"], ["Assets/ReadOnly/Unsupported.asset"])
            self.assertEqual(job["extra_root_prefabs"], ["Assets/GameContent/ExtraRoot.prefab"])
            self.assertEqual(job["plan_file"], name)

    def test_every_native_result_integer_counter_is_aggregated(self):
        for kind, filename, result_class in (("so", "ScriptableObjectConverter.cs", "ConvertResult"),
                                             ("prefab", "PrefabBuilder.cs", "BuildResult")):
            source = (bootstrap.REPO / "UnityProject/Assets/Editor" / filename).read_text(encoding="utf-8-sig")
            body = source.split("public class " + result_class, 1)[1].split("public class JobRecord", 1)[0]
            expected = set(re.findall(r"public int (\w+);", body))
            self.assertEqual(set(bootstrap.CONVERSION_COUNTERS[kind]), expected)

    def test_second_batch_failure_never_keeps_a_previous_aggregate_success(self):
        self.write_plan("so_convert_plan_scenarios.json", 25)
        original = {"status": "OK", "jobs_total": 999}
        bootstrap.write_json(self.out / "so_convert_result.json", original)

        def fail_second(method, name, extra, tag):
            self.fake_unity(method, name, extra, tag)
            if len(self.calls) == 2:
                raise RuntimeError("synthetic Unity process failed")

        with self.assertRaisesRegex(RuntimeError, "synthetic Unity"):
            self.run_batches("so", ["so_convert_plan_scenarios.json"], fail_second)
        self.assertFalse((self.out / "so_convert_result.json").exists())
        self.assertEqual(json.loads((self.out / "so_convert_result.previous.json").read_text()), original)
        self.assertTrue((self.out / self.calls[0]["receipt"]).is_file())

    def test_stale_receipt_and_missing_asset_records_cannot_publish_success(self):
        self.write_plan("so_convert_plan_data.json", 1)
        for defect in ("stale", "old_generated", "wrong_plan_pattern", "wrong_plan_files", "wrong_job_plan",
                       "missing_job", "wrong_target", "guid_changed", "counter_mismatch"):
            with self.subTest(defect=defect):
                def malformed(method, name, extra, tag):
                    self.fake_unity(method, name, extra, tag)
                    path = self.out / name
                    value = json.loads(path.read_text())
                    if defect == "missing_job":
                        value["jobs"] = []
                    elif defect == "wrong_target":
                        value["jobs"][0]["target"] = "Assets/GameContent/Other.asset"
                    elif defect == "guid_changed":
                        value["jobs"][0]["guid_after"] = "b" * 32
                    elif defect == "counter_mismatch":
                        value["converted"], value["converted_partial"] = 0, 1
                    elif defect == "old_generated":
                        value["generated"] = "2000-01-01T00:00:00.0000000Z"
                    elif defect == "wrong_plan_pattern":
                        value["plan_pattern"] = "so_convert_plan_*.json"
                    elif defect == "wrong_plan_files":
                        value["plan_files"] = ["so_convert_plan_data.json"]
                    elif defect == "wrong_job_plan":
                        value["jobs"][0]["plan_file"] = "so_convert_plan_data.json"
                    bootstrap.write_json(path, value)
                    if defect == "stale":
                        os.utime(path, ns=(1_000_000_000, 1_000_000_000))
                with self.assertRaises(RuntimeError):
                    self.run_batches("so", ["so_convert_plan_data.json"], malformed)
                self.assertFalse((self.out / "so_convert_result.json").exists())

    def test_native_dotnet_timestamp_and_absolute_prefab_target_are_accepted(self):
        name = "prefab_plan_all.json"
        self.write_plan(name, 1, prefab=True, absolute_targets=True)

        def native_receipt(method, receipt, extra, tag):
            self.fake_unity(method, receipt, extra, tag)
            path = self.out / receipt
            value = json.loads(path.read_text())
            value["generated"] = datetime.now(timezone.utc).strftime("%Y-%m-%dT%H:%M:%S.%f") + "0Z"
            bootstrap.write_json(path, value)

        result = self.run_batches("prefab", [name], native_receipt)
        self.assertTrue(result["jobs"][0]["target"].startswith("Assets/"))
        self.assertEqual(result["jobs_total"], 1)

    def test_prefab_dependency_cannot_move_behind_its_user_when_slicing(self):
        name = "prefab_plan_all.json"
        source = self.write_plan(name, 2, prefab=True)
        source["jobs"].reverse()
        bootstrap.write_json(self.out / name, source)
        with self.assertRaisesRegex(RuntimeError, "dependency must precede"):
            self.run_batches("prefab", [name])
        self.assertEqual(self.calls, [])
        self.assertFalse((self.out / "prefab_build_result.json").exists())


if __name__ == "__main__":
    unittest.main()
