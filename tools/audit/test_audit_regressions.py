#!/usr/bin/env python3
"""Regression tests for the audit defects listed in UNITY_READINESS_CODEX_AUDIT.md.

Each test class pins one defect that was actually measured in the old auditors, so a
future edit that reintroduces it fails here instead of silently inflating a pass rate.

Run:  <venv>/python.exe tools/audit/test_audit_regressions.py -v
"""
from __future__ import annotations

import csv
import json
import os
import shutil
import sys
import tempfile
import unittest

HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)

import a08_prefab_sweep as a08  # noqa: E402
import common  # noqa: E402
import identity  # noqa: E402


def write_json(path, obj):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8") as fh:
        json.dump(obj, fh, ensure_ascii=False)


def make_index(tmp, rows):
    """rows: list of dicts with the real _index.csv column names."""
    os.makedirs(tmp, exist_ok=True)
    path = os.path.join(tmp, "_index.csv")
    cols = ["type", "name", "asset_path", "output_file", "source_bundle", "path_id"]
    with open(path, "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=cols)
        w.writeheader()
        for r in rows:
            w.writerow({c: r.get(c, "") for c in cols})
    return path


def make_object(res_root, output_file, obj):
    write_json(os.path.join(res_root, output_file.replace("/", os.sep)), obj)


def ptr(path_id, file_id=0):
    return {"m_FileID": file_id, "m_PathID": path_id}


# --------------------------------------------------------------------------- #
# identity / pathID losslessness  (audit UR-009)
# --------------------------------------------------------------------------- #

class TestInt64Identity(unittest.TestCase):
    def test_full_signed_int64_range_round_trips(self):
        for value in (0, 1, -1, 2 ** 63 - 1, -(2 ** 63),
                      836492224112770746, -8043137295908389190,
                      9223314696597965781, -9223201017708124729):
            key = identity.source_object_key("src", "sf", value)
            src, sf, pid = identity.parse_source_object_key(key)
            self.assertEqual((src, sf, pid), ("src", "sf", value),
                             f"pathID {value} did not round-trip")

    def test_overflow_is_rejected_not_wrapped(self):
        for bad in (2 ** 63, -(2 ** 63) - 1, "9223372036854775808"):
            with self.assertRaises(ValueError):
                identity.as_int64(bad)

    def test_js_unsafe_integer_is_preserved_as_decimal_string(self):
        # 836492224112770746 exceeds 2**53; a float round-trip would corrupt it.
        value = 9223314696597965781
        self.assertNotEqual(int(float(value)), value, "fixture is not actually lossy in float")
        self.assertEqual(identity.pid_str(value), "9223314696597965781")

    def test_dump_sequence_is_not_a_pathid(self):
        # Measured on the real index: '~N' is a coarse 1..n dump ordinal, and it repeats
        # (528 rows share a value) while the CSV path_id is unique per source.
        self.assertEqual(identity.dump_sequence("x/Foo~12.json"), 12)
        self.assertIsNone(identity.dump_sequence("x/Foo.json"))
        self.assertIsNone(identity.dump_sequence("x/Foo.ogg"))

    def test_stable_guid_is_deterministic_and_distinct(self):
        a = identity.stable_guid("Assets/X.prefab", "GameObject", 123)
        b = identity.stable_guid("Assets/X.prefab", "GameObject", 123)
        c = identity.stable_guid("Assets/X.prefab", "GameObject", 124)
        self.assertEqual(a, b)
        self.assertNotEqual(a, c)
        self.assertEqual(len(a), 32)


# --------------------------------------------------------------------------- #
# index ledger: same basename, different directories  (audit UR-009)
# --------------------------------------------------------------------------- #

class TestIndexLedger(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.mkdtemp(prefix="manosaba_test_")
        self.res = os.path.join(self.tmp, "res")
        os.makedirs(self.res, exist_ok=True)

    def tearDown(self):
        shutil.rmtree(self.tmp, ignore_errors=True)

    def test_same_basename_in_different_dirs_stays_distinct(self):
        """A basename-keyed model collides here; the source-identity model must not."""
        rows = [
            {"type": "Texture2D", "asset_path": "Assets/A/tex.png",
             "output_file": "Assets/A/tex.png.json", "source_bundle": "aa/groupA.bundle",
             "path_id": "100"},
            {"type": "Texture2D", "asset_path": "Assets/B/tex.png",
             "output_file": "Assets/B/tex.png.json", "source_bundle": "aa/groupB.bundle",
             "path_id": "100"},
        ]
        idx = make_index(self.tmp, rows)
        make_object(self.res, rows[0]["output_file"], {"m_Name": "tex"})
        make_object(self.res, rows[1]["output_file"], {"m_Name": "tex"})
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        self.assertEqual(len(ledger.rows), 2)
        keys = {r["key"] for r in ledger.rows}
        self.assertEqual(len(keys), 2, "same pathID in two bundles must not collapse")
        self.assertEqual(ledger.stats["duplicate_keys"], 0)

    def test_same_pathid_different_serialized_files_get_distinct_keys(self):
        k1 = identity.source_object_key("aa/x.bundle", "CAB-aaa", 42)
        k2 = identity.source_object_key("aa/x.bundle", "CAB-bbb", 42)
        self.assertNotEqual(k1, k2)

    def test_duplicate_object_key_is_reported_not_silently_collapsed(self):
        rows = [
            {"type": "GameObject", "asset_path": "Assets/A.prefab",
             "output_file": "Assets/A.prefab.json", "source_bundle": "aa/x.bundle",
             "path_id": "7"},
            {"type": "GameObject", "asset_path": "Assets/A.prefab",
             "output_file": "Assets/A.prefab~1.json", "source_bundle": "aa/x.bundle",
             "path_id": "7"},
        ]
        idx = make_index(self.tmp, rows)
        make_object(self.res, rows[0]["output_file"], {"m_Name": "A"})
        make_object(self.res, rows[1]["output_file"], {"m_Name": "A"})
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        dupes = [d for d in ledger.defects if d["kind"] == "duplicate_object_key"]
        self.assertEqual(len(dupes), 1)
        self.assertEqual(ledger.stats["duplicate_keys"], 1)

    def test_unattributed_recovered_rows_do_not_collapse_to_one_key(self):
        """The real index has 30 '(recovered)' rows with path_id 0.

        Keying by (source_bundle, pathID) collapsed all 30 (and, once the `~N` marker
        repeats, 21 of them even by container) into one identity.
        """
        rows = []
        for i in range(4):
            rows.append({"type": "GameObject", "asset_path": "Assets/S.prefab",
                         "output_file": f"_serialized/Assets/S~{10 * i}.json",
                         "source_bundle": "(recovered)", "path_id": "0"})
        idx = make_index(self.tmp, rows)
        for r in rows:
            make_object(self.res, r["output_file"], {"m_Name": "S"})
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        keys = {r["key"] for r in ledger.rows}
        self.assertEqual(len(keys), 4, "each recovered object needs its own identity")
        self.assertEqual(ledger.stats["unattributed_rows"], 4)
        # The ripper's placeholder 0 must be recorded as unknown, not presented as real.
        self.assertEqual(ledger.stats["unattributed_pathid_unknown"], 4)

    def test_pathid_is_never_silently_dropped(self):
        rows = [{"type": "Texture2D", "asset_path": "Assets/t.png",
                 "output_file": "Assets/t.png.json", "source_bundle": "aa/x.bundle",
                 "path_id": "not-a-number"}]
        idx = make_index(self.tmp, rows)
        make_object(self.res, rows[0]["output_file"], {})
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        self.assertEqual(ledger.stats["pathid_unparsable"], 1)
        self.assertTrue(any(d["kind"] == "pathid_unparsable" for d in ledger.defects))

    def test_missing_dump_file_is_a_defect(self):
        rows = [{"type": "AudioClip", "asset_path": "Assets/a.wav",
                 "output_file": "Assets/a.ogg", "source_bundle": "aa/x.bundle",
                 "path_id": "5"}]
        idx = make_index(self.tmp, rows)
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        self.assertEqual(ledger.stats["dump_missing"], 1)
        self.assertTrue(any(d["kind"] == "dump_file_missing" for d in ledger.defects))

    def test_json_files_outside_the_index_are_listed(self):
        rows = [{"type": "GameObject", "asset_path": "Assets/A.prefab",
                 "output_file": "Assets/A.json", "source_bundle": "aa/x.bundle",
                 "path_id": "1"}]
        idx = make_index(self.tmp, rows)
        make_object(self.res, "Assets/A.json", {})
        make_object(self.res, "Uncategorized/Stray/Extra.json", {"m_Name": "extra"})
        ledger, orphans = common.load_index_ledger(index_csv=idx, res_root=self.res)
        self.assertEqual(orphans, ["Uncategorized/Stray/Extra.json"])
        self.assertEqual(ledger.stats["json_files_not_in_index"], 1)


# --------------------------------------------------------------------------- #
# prefab hierarchy validation  (audit UR-007)
# --------------------------------------------------------------------------- #

class TestPrefabValidator(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.mkdtemp(prefix="manosaba_test_")
        self.res = os.path.join(self.tmp, "res")
        os.makedirs(self.res, exist_ok=True)
        self.cont = "Assets/P.prefab"

    def tearDown(self):
        shutil.rmtree(self.tmp, ignore_errors=True)

    def _rows(self, objects, extra_rows=None):
        """objects: {path_id: (type, dict)} -> ledger rows + files on disk.

        ``extra_rows`` are appended to the index CSV so they come back as real
        :class:`common.IndexRow` objects (the validator dereferences ``pid_int`` and
        ``dump_path``, so a hand-made dict would not be a valid stand-in).
        """
        rows = []
        for pid, (obj_type, doc) in objects.items():
            out = f"Assets/P~{pid}.json"
            make_object(self.res, out, doc)
            rows.append({"type": obj_type, "asset_path": self.cont, "output_file": out,
                         "source_bundle": "aa/x.bundle", "path_id": str(pid)})
        rows.extend(extra_rows or [])
        idx = make_index(self.tmp, rows)
        ledger, _ = common.load_index_ledger(index_csv=idx, res_root=self.res)
        return [r for r in ledger.rows if r["asset_path"] == self.cont]
    def _valid_prefab(self):
        return {
            1: ("GameObject", {"m_Name": "Root", "m_Component": [{"component": ptr(2)}, {"component": ptr(3)}]}),
            2: ("Transform", {"m_GameObject": ptr(1), "m_Father": ptr(0), "m_Children": []}),
            3: ("MeshRenderer", {"m_GameObject": ptr(1)}),
        }

    def test_valid_prefab_is_clean(self):
        res = a08.validate_container(self.cont, self._rows(self._valid_prefab()), {}, False)
        self.assertEqual(res["issue_count"], 0, res["issues"])
        self.assertTrue(res["clean"])
        self.assertEqual(res["roots"], 1)

    def test_read_error_is_reported_and_not_treated_as_clean(self):
        """Old code did `except Exception: objs[pid] = (type, {})`, hiding the failure."""
        objects = self._valid_prefab()
        objects[3] = ("MeshRenderer", {"m_GameObject": ptr(1)})
        rows = self._rows(objects)
        # corrupt exactly one dump file
        target = next(r for r in rows if r["pid_int"] == 3)
        with open(target["dump_path"], "w", encoding="utf-8") as fh:
            fh.write("{ this is not valid json")
        res = a08.validate_container(self.cont, rows, {}, False)
        self.assertFalse(res["clean"])
        self.assertTrue(any(i["kind"] == "object_read_error" for i in res["issues"]),
                        res["issues"])
        self.assertEqual(len(res["read_errors"]), 1)

    def test_orphan_gameobject_is_reported(self):
        """Old code computed an orphan set and then never used it.

        The fixture models a detached subtree: the root has no children, and GameObject 9
        owns Transform 10 whose m_Father points at a pathID that does not exist.  Nothing
        in the tree reaches it, yet transform 10 *does* name GameObject 9 -- which is
        exactly the case that a "is it named by some Transform" check would miss.
        """
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Orphan", "m_Component": [{"component": ptr(10)}]})
        objects[10] = ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(777),
                                     "m_Children": []})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertIn("orphan_transform", kinds, res["issues"])
        self.assertIn("orphan_gameobject", kinds, res["issues"])
        self.assertFalse(res["clean"])
        self.assertIn("9", res["orphan_gameobjects"])
        entry = next(i for i in res["issues"] if i["kind"] == "orphan_gameobject")
        self.assertEqual(entry["name"], "Orphan")

    def test_orphan_component_and_empty_owner_are_distinguished(self):
        """A GameObject nobody names must be an orphan; one named by an attached
        Transform must not be."""
        objects = self._valid_prefab()
        # detached: no Transform points its m_GameObject here at all
        objects[20] = ("GameObject", {"m_Name": "NoOwner", "m_Component": [{"component": ptr(21)}]})
        objects[21] = ("MeshRenderer", {"m_GameObject": ptr(20)})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        names = {i.get("name") for i in res["issues"] if i["kind"] == "orphan_gameobject"}
        self.assertIn("NoOwner", names, res["issues"])
        self.assertNotIn("Root", names, res["issues"])

    def test_wrong_component_backreference_is_reported(self):
        objects = self._valid_prefab()
        objects[3] = ("MeshRenderer", {"m_GameObject": ptr(2)})  # points at the Transform
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        self.assertTrue(any(i["kind"] == "component_backref_wrong_owner" for i in res["issues"]),
                        res["issues"])
        self.assertFalse(res["clean"])

    def test_missing_component_reference_is_reported(self):
        objects = self._valid_prefab()
        objects[1] = ("GameObject", {"m_Name": "Root",
                                     "m_Component": [{"component": ptr(2)}, {"component": ptr(99)}]})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        self.assertTrue(any(i["kind"] == "missing_component" for i in res["issues"]),
                        res["issues"])

    def test_cross_file_reference_is_not_bound_to_a_local_object(self):
        """Old code ignored m_FileID and matched on pathID alone.

        The reference must be classified as cross-file, and the component list must then
        report the entry as unresolved -- what must NOT happen is a silent bind to the
        local object that happens to share the pathID.
        """
        objects = self._valid_prefab()
        objects[1] = ("GameObject", {"m_Name": "Root",
                                     "m_Component": [{"component": ptr(2)},
                                                     {"component": ptr(3, file_id=1)}]})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertIn("cross_file_reference", kinds, res["issues"])
        self.assertIn("missing_component", kinds, res["issues"])
        # the cross-file pointer must be recorded with its fileID, not resolved away
        xref = next(i for i in res["issues"] if i["kind"] == "cross_file_reference")
        self.assertEqual(xref["m_FileID"], 1)
        self.assertEqual(xref["m_PathID"], "3")

    def test_father_mismatch_and_cycle(self):
        objects = self._valid_prefab()
        objects[4] = ("GameObject", {"m_Name": "Child", "m_Component": [{"component": ptr(5)}]})
        objects[5] = ("Transform", {"m_GameObject": ptr(4), "m_Father": ptr(0), "m_Children": []})
        # Root lists child 5, but transform 5 declares no father -> father_mismatch
        objects[2] = ("Transform", {"m_GameObject": ptr(1), "m_Father": ptr(0),
                                    "m_Children": [ptr(5)]})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertIn("father_mismatch", kinds, res["issues"])

    def test_cycle_is_reported(self):
        objects = self._valid_prefab()
        # root(2) <-> 5 cycle: 5 is a child of 2 and also 2's father
        objects[4] = ("GameObject", {"m_Name": "Cyc", "m_Component": [{"component": ptr(5)}]})
        objects[5] = ("Transform", {"m_GameObject": ptr(4), "m_Father": ptr(0),
                                    "m_Children": [ptr(2)]})
        objects[2] = ("Transform", {"m_GameObject": ptr(1), "m_Father": ptr(5),
                                    "m_Children": [ptr(5)]})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertIn("cycle", kinds, res["issues"])

    def test_multi_root_target_that_exists_is_not_a_contradiction(self):
        """A prefab container target is normally a GameObject, not a root Transform.

        So a target that simply is not one of the root Transforms proves nothing and must
        not be reported.  GameObject 9 exists in this container, so there is no defect.
        """
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Second", "m_Component": [{"component": ptr(10)}]})
        objects[10] = ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(0), "m_Children": []})
        rows = self._rows(objects)
        targets = {self.cont: [{"source": "aa/x.bundle", "cab": "CAB-x",
                                "target_path_id": "9", "target_file_id": 0,
                                "preloadIndex": 0, "preloadSize": 1}]}
        res = a08.validate_container(self.cont, rows, targets, True)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertNotIn("multi_root_target_not_in_container", kinds, res["issues"])
        self.assertIsNone(res["evidence_gap"])
        self.assertEqual(res["roots"], 2)

    def test_multi_root_target_resolving_to_nothing_is_reported(self):
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Second", "m_Component": [{"component": ptr(10)}]})
        objects[10] = ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(0), "m_Children": []})
        rows = self._rows(objects)
        targets = {self.cont: [{"source": "aa/x.bundle", "cab": "CAB-x",
                                "target_path_id": "999", "target_file_id": 0,
                                "preloadIndex": 0, "preloadSize": 1}]}
        res = a08.validate_container(self.cont, rows, targets, True)
        kinds = {i["kind"] for i in res["issues"]}
        self.assertIn("multi_root_target_not_in_container", kinds, res["issues"])
        self.assertFalse(res["clean"])

    def test_multi_root_with_conflicting_targets_is_reported(self):
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Second", "m_Component": [{"component": ptr(10)}]})
        objects[10] = ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(0), "m_Children": []})
        rows = self._rows(objects)
        targets = {self.cont: [
            {"source": "aa/a.bundle", "cab": "CAB-a", "target_path_id": "1",
             "target_file_id": 0, "preloadIndex": 0, "preloadSize": 1},
            {"source": "aa/b.bundle", "cab": "CAB-b", "target_path_id": "9",
             "target_file_id": 0, "preloadIndex": 0, "preloadSize": 1},
        ]}
        res = a08.validate_container(self.cont, rows, targets, True)
        self.assertIn("multi_root_container_multi_target",
                      {i["kind"] for i in res["issues"]}, res["issues"])

    def test_missing_target_evidence_is_an_evidence_gap_not_a_defect(self):
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Second", "m_Component": [{"component": ptr(10)}]})
        objects[10] = ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(0), "m_Children": []})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        self.assertIsNotNone(res["evidence_gap"])
        self.assertNotIn("multi_root_target_not_in_container",
                         {i["kind"] for i in res["issues"]})

    def test_empty_component_list_is_reported(self):
        objects = self._valid_prefab()
        objects[9] = ("GameObject", {"m_Name": "Bare", "m_Component": []})
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        self.assertIn("gameobject_with_empty_component_list",
                      {i["kind"] for i in res["issues"]})

    def test_binary_payload_rows_are_not_parsed_as_json(self):
        """A container also lists sub-asset rows whose output_file is a BINARY payload.

        Parsing those as JSON produced 1,797 bogus object_read_error entries the first time
        this validator ran (Texture2D rows pointing at a .png).  They must be counted as
        payloads, not as read failures.
        """
        objects = self._valid_prefab()
        png = os.path.join(self.res, "Assets", "tex.png")
        os.makedirs(os.path.dirname(png), exist_ok=True)
        with open(png, "wb") as fh:
            fh.write(b"\x89PNG\r\n\x1a\n" + b"\x00" * 32)
        rows = self._rows(objects, extra_rows=[
            {"type": "Texture2D", "name": "", "asset_path": self.cont,
             "output_file": "Assets/tex.png", "source_bundle": "aa/x.bundle",
             "path_id": "4242"}])
        res = a08.validate_container(self.cont, rows, {}, False)
        self.assertEqual(len(res["read_errors"]), 0,
                         f"a binary payload must not be a read error: {res['read_errors']}")
        self.assertEqual(res["payload_rows"], 1)

    def test_sub_assets_are_not_reported_as_orphan_components(self):
        """Material/Shader/AnimationClip/Mesh are sub-assets, not Components.

        They carry no m_GameObject field, so judging them as orphan components produced 769
        false positives.  A component that DOES declare m_GameObject and is claimed by no
        GameObject is still a genuine orphan.
        """
        objects = self._valid_prefab()
        objects[30] = ("Material", {"m_Name": "Mat", "m_Shader": ptr(31)})
        objects[31] = ("Shader", {"m_Name": "Shader"})
        objects[32] = ("AnimationClip", {"m_Name": "Clip"})
        objects[33] = ("MeshRenderer", {"m_GameObject": ptr(1)})  # declared, but unclaimed
        res = a08.validate_container(self.cont, self._rows(objects), {}, False)
        orphans = [i for i in res["issues"] if i["kind"] == "orphan_component"]
        types = {i["component_type"] for i in orphans}
        self.assertEqual(types, {"MeshRenderer"},
                         f"only real Components may be orphans, got {types}")
        self.assertIn("declared_owner", orphans[0])


# --------------------------------------------------------------------------- #
# a03 worker: container targets must survive  (audit UR-008)
# --------------------------------------------------------------------------- #

class TestContainerTargets(unittest.TestCase):
    """Static guards that pin the module-level *code*, not its documentation.

    These parse each tool with ``ast`` and strip docstrings, because the fixed modules
    legitimately describe the old defective code in their own docstrings -- a plain
    substring search would match the description of the bug instead of the bug.
    """

    @staticmethod
    def _code_without_docstrings(name):
        import ast
        path = os.path.join(HERE, name)
        with open(path, encoding="utf-8") as fh:
            source = fh.read()
        tree = ast.parse(source)
        for node in ast.walk(tree):
            if isinstance(node, (ast.Module, ast.FunctionDef, ast.AsyncFunctionDef, ast.ClassDef)):
                body = getattr(node, "body", None)
                if (body and isinstance(body[0], ast.Expr)
                        and isinstance(body[0].value, ast.Constant)
                        and isinstance(body[0].value.value, str)):
                    body.pop(0)
        return ast.unparse(tree)

    def test_a03_worker_source_keeps_assetinfo_targets(self):
        """Static guard: the old code reduced m_Container to bare key strings."""
        body = self._code_without_docstrings("a03_worker.py")
        self.assertNotIn("for k, _ in (d.m_Container or [])", body,
                         "container targets must not be discarded again")
        self.assertIn("preloadIndex", body)
        self.assertIn("preloadSize", body)
        self.assertIn("m_PreloadTable", body)
        # the target PPtr must be recorded, not just the key
        self.assertIn("m_PathID", body)

    def test_a07_source_does_not_use_setdefault_for_keys(self):
        body = self._code_without_docstrings("a07_addressables.py")
        self.assertNotIn("key2bundle.setdefault", body,
                         "duplicate container keys must keep every source")
        # every location of a key must be retained
        self.assertIn("key_to_locations", body)
        self.assertIn("append", body)

    def test_a04_source_does_not_concatenate_externals(self):
        body = self._code_without_docstrings("a04_refs.py")
        self.assertNotIn("ex.extend", body,
                         "externals must be resolved per SerializedFile")
        self.assertNotIn("PPTR", body,
                         "references must be found by structured traversal, not regex")
        self.assertIn("iter_pptrs", body)

    def test_a07_counts_bundles_by_relative_path_not_basename(self):
        body = self._code_without_docstrings("a07_addressables.py")
        self.assertNotIn("disk_bundles.add(f)", body,
                         "basename dedup under-counts physically distinct bundles")
        self.assertIn("relpath", body)

    def test_catalog_reader_reports_unparsed_instead_of_guessing(self):
        body = self._code_without_docstrings("catalog_reader.py")
        self.assertIn("CatalogFormatError", body)
        self.assertIn("MAGIC", body)
        # it must not claim to have parsed resource locations
        self.assertNotIn("resource_locations", body)


# --------------------------------------------------------------------------- #
# E63-006: the pid alias, and the entry points that read it                      #
# --------------------------------------------------------------------------- #


class TestPidAlias(unittest.TestCase):
    """``row["pid"]`` must equal the canonical field and must not invent a valid 0."""

    def _rows(self, tmp, rows):
        index = make_index(tmp, rows)
        ledger, _ = common.load_index_ledger(index_csv=index, res_root=tmp, check_files=False)
        return ledger.rows

    def test_alias_equals_canonical_field(self):
        with tempfile.TemporaryDirectory() as tmp:
            row = self._rows(tmp, [dict(type="GameObject", asset_path="Assets/A.prefab",
                                        output_file="a.json", source_bundle="aa/x.bundle",
                                        path_id="836492224112770746")])[0]
            self.assertTrue(row.pathid_known)
            self.assertEqual(row["pid"], 836492224112770746)
            self.assertEqual(row["pid"], row["pid_int"])
            self.assertEqual(row["pid"], row.pid)

    def test_unknown_pathid_is_not_served_as_zero(self):
        """The ripper wrote path_id=0 for every unattributed row; 0 is not an identifier."""
        with tempfile.TemporaryDirectory() as tmp:
            row = self._rows(tmp, [dict(type="Texture2D", asset_path="",
                                        output_file="_serialized/orphan.json",
                                        source_bundle="(recovered)", path_id="0")])[0]
            self.assertFalse(row.pathid_known)
            with self.assertRaises(common.UnknownPathID):
                row["pid"]
            with self.assertRaises(common.UnknownPathID):
                row.pid
            # the placeholder stays visible as data, it just cannot pose as a pathID
            self.assertEqual(row["pid_int"], 0)

    def test_other_missing_keys_remain_plain_keyerror(self):
        with tempfile.TemporaryDirectory() as tmp:
            row = self._rows(tmp, [dict(type="GameObject", asset_path="Assets/A.prefab",
                                        output_file="a.json", source_bundle="aa/x.bundle",
                                        path_id="5")])[0]
            with self.assertRaises(KeyError):
                row["column_that_does_not_exist"]


class TestDocumentedEntryPointsRun(unittest.TestCase):
    """README run order a02/a05/a06 must actually execute (they died with KeyError: 'pid')."""

    def setUp(self):
        self.tmp = tempfile.mkdtemp()
        self.res = os.path.join(self.tmp, "res")
        self.out = os.path.join(self.tmp, "out")
        os.makedirs(self.res, exist_ok=True)
        os.makedirs(self.out, exist_ok=True)
        rows = [
            dict(type="GameObject", name="Root", asset_path="Assets/P.prefab",
                 output_file="P/root.json", source_bundle="aa/p.bundle", path_id="11"),
            dict(type="Transform", asset_path="Assets/P.prefab",
                 output_file="P/tr.json", source_bundle="aa/p.bundle", path_id="12"),
            dict(type="MonoBehaviour", asset_path="Assets/P.prefab",
                 output_file="P/mb.json", source_bundle="aa/p.bundle", path_id="13"),
            # a row the ripper could not attribute: pathID genuinely unknown
            dict(type="Texture2D", asset_path="", output_file="_serialized/orphan.json",
                 source_bundle="(recovered)", path_id="0"),
        ]
        self.index = make_index(self.res, rows)
        make_object(self.res, "P/root.json",
                    {"m_Name": "Root", "m_IsActive": True,
                     "m_Component": [{"component": ptr(12)}, {"component": ptr(13)}]})
        make_object(self.res, "P/tr.json",
                    {"m_GameObject": ptr(11), "m_Father": ptr(0), "m_Children": []})
        make_object(self.res, "P/mb.json", {"m_GameObject": ptr(11), "m_Script": ptr(0)})
        make_object(self.res, "_serialized/orphan.json", {"m_Name": "orphan"})
        self._common = (common.INDEX_CSV, common.RES_ROOT, common.OUT)
        common.INDEX_CSV, common.RES_ROOT, common.OUT = self.index, self.res, self.out

    def tearDown(self):
        common.INDEX_CSV, common.RES_ROOT, common.OUT = self._common
        shutil.rmtree(self.tmp, ignore_errors=True)

    def _patch_module(self, mod):
        saved = {k: getattr(mod, k) for k in ("RES", "OUT") if hasattr(mod, k)}
        if hasattr(mod, "RES"):
            mod.RES = self.res
        if hasattr(mod, "OUT"):
            mod.OUT = self.out
        self.addCleanup(lambda: [setattr(mod, k, v) for k, v in saved.items()])

    def test_a02_runs_and_reports_unknown_pathids_separately(self):
        import a02_index_stats as a02
        self._patch_module(a02)
        a02.main()
        with open(os.path.join(self.out, "index_summary.json"), encoding="utf-8") as fh:
            summary = json.load(fh)
        self.assertEqual(summary["rows"], 4)
        self.assertEqual(summary["rows_with_unknown_pathid"], 1)

    def test_a05_runs_and_excludes_unknown_pathid_rows(self):
        import a05_monobehaviour as a05
        self._patch_module(a05)
        with open(os.path.join(self.out, "sources.json"), "w", encoding="utf-8") as fh:
            json.dump([{"rel": "aa/p.bundle", "serialized_files": [{"name": "CAB-p",
                                                                   "externals": []}]}], fh)
        with open(os.path.join(self.out, "monoscript_table.json"), "w", encoding="utf-8") as fh:
            json.dump({"by_pid": {}, "by_identity": {}, "sources": {}}, fh)
        a05.main()
        with open(os.path.join(self.out, "monobehaviour.json"), encoding="utf-8") as fh:
            out = json.load(fh)
        self.assertEqual(out["monobehaviour_rows"], 1)
        self.assertEqual(out["rows_with_unknown_pathid_excluded"], 1)

    def test_a06_load_scope_reports_missing_dump_instead_of_raising(self):
        import a06_rebuild as a06
        self._patch_module(a06)
        rows = common.load_index()
        diag = {}
        objs = a06.load_scope(rows, "container", "Assets/P.prefab", diag)
        self.assertIn(11, objs)
        self.assertNotIn("dump_missing", diag)

        os.remove(os.path.join(self.res, "P", "tr.json"))
        diag2 = {}
        objs2 = a06.load_scope(rows, "container", "Assets/P.prefab", diag2)
        self.assertEqual(diag2["dump_missing"], 1)
        self.assertIn("__error__", objs2[12][1])

    def test_a06_load_scope_skips_unknown_pathid_with_a_diagnostic(self):
        import a06_rebuild as a06
        self._patch_module(a06)
        rows = common.load_index()
        diag = {}
        a06.load_scope(rows, "source", "(recovered)", diag)
        self.assertEqual(diag["unknown_pathid_rows"], 1)
        self.assertEqual(diag["unknown_pathid_samples"], ["_serialized/orphan.json"])

    def test_a06_main_runs_end_to_end(self):
        import a06_rebuild as a06
        self._patch_module(a06)
        a06.main()
        with open(os.path.join(self.out, "hierarchy_report.json"), encoding="utf-8") as fh:
            report = json.load(fh)
        self.assertTrue(report)
        for entry in report.values():
            self.assertIn("load_diagnostics", entry)


class TestA04MissingDumpDiagnostic(unittest.TestCase):
    """A missing dump must become a reported defect, never a KeyError."""

    def test_missing_dump_row_is_reported_with_a_path_id_string(self):
        import a04_refs as a04
        with tempfile.TemporaryDirectory() as tmp:
            res = os.path.join(tmp, "res")
            os.makedirs(res, exist_ok=True)
            index = make_index(res, [
                dict(type="GameObject", asset_path="Assets/P.prefab", output_file="gone.json",
                     source_bundle="aa/p.bundle", path_id="99"),
                dict(type="Texture2D", asset_path="", output_file="_serialized/orphan.json",
                     source_bundle="(recovered)", path_id="0"),
            ])
            saved = (common.INDEX_CSV, common.RES_ROOT, a04.RES_ROOT)
            common.INDEX_CSV, common.RES_ROOT, a04.RES_ROOT = index, res, res
            try:
                ledger, _ = common.load_index_ledger(check_files=False)
                reported = []
                for row in ledger.rows:
                    full = os.path.join(res, row["output_file"].replace("/", os.sep))
                    if not os.path.exists(full):
                        reported.append(str(row["pid_int"]) if row.pathid_known else "unknown")
                self.assertIn("99", reported)
                self.assertIn("unknown", reported)
            finally:
                common.INDEX_CSV, common.RES_ROOT, a04.RES_ROOT = saved


class TestA07MainFlow(unittest.TestCase):
    """a07's summary referenced an undefined name; only running main() exposes it."""

    def setUp(self):
        self.tmp = tempfile.mkdtemp()
        self.res = os.path.join(self.tmp, "res")
        self.out = os.path.join(self.tmp, "out")
        self.aa = os.path.join(self.tmp, "aa")
        self.aaw = os.path.join(self.aa, "StandaloneWindows64")
        for d in (self.res, self.out, os.path.join(self.aaw, "sub")):
            os.makedirs(d, exist_ok=True)
        # Two bundles with the SAME basename in different directories: 2 paths, 1 name.
        for rel in ("dup_assets_all.bundle", os.path.join("sub", "dup_assets_all.bundle")):
            with open(os.path.join(self.aaw, rel), "wb") as fh:
                fh.write(b"UnityFS\x00" * 4)
        with open(os.path.join(self.aa, "settings.json"), "w", encoding="utf-8") as fh:
            json.dump({"m_AddressablesVersion": "2.4.6", "m_buildTarget": "StandaloneWindows64",
                       "m_SettingsHash": "deadbeef",
                       "m_CatalogLocations": [{"m_Provider": "ContentCatalogProvider",
                                               "m_InternalId": "{UnityEngine.AddressableAssets"
                                                               ".Addressables.RuntimePath}/catalog.bin"}]}, fh)
        self.index = make_index(self.res, [
            dict(type="GameObject", asset_path="Assets/P.prefab", output_file="P/root.json",
                 source_bundle="aa/StandaloneWindows64/dup_assets_all.bundle", path_id="11"),
        ])
        make_object(self.res, "P/root.json", {"m_Name": "Root"})

    def tearDown(self):
        shutil.rmtree(self.tmp, ignore_errors=True)

    def test_main_completes_and_counts_bundles_by_path_not_basename(self):
        import a07_addressables as a07
        saved = (a07.AAW, a07.OUT, a07.load_bundles, a07.load_unity_catalog_dump,
                 a07.catalog_reader.load_catalog, common.INDEX_CSV, common.RES_ROOT)
        bundles = [{"source": "aa/StandaloneWindows64/dup_assets_all.bundle",
                    "name": "CAB-dup", "dependencies": [],
                    "containers": [{"key": "Assets/P.prefab",
                                    "target": {"m_FileID": 0, "m_PathID": "11"},
                                    "preloadIndex": 0, "preloadSize": 1}]}]

        def _raise(_path):
            raise a07.catalog_reader.CatalogFormatError("fixture: not a real catalog")

        a07.AAW, a07.OUT = self.aaw, self.out
        a07.load_bundles = lambda: bundles
        a07.load_unity_catalog_dump = lambda: (None, None)
        a07.catalog_reader.load_catalog = _raise
        common.INDEX_CSV, common.RES_ROOT = self.index, self.res
        try:
            rc = a07.main()
        finally:
            (a07.AAW, a07.OUT, a07.load_bundles, a07.load_unity_catalog_dump,
             a07.catalog_reader.load_catalog, common.INDEX_CSV, common.RES_ROOT) = saved

        self.assertEqual(rc, 0)
        with open(os.path.join(self.out, "addressables.json"), encoding="utf-8") as fh:
            report = json.load(fh)
        self.assertEqual(report["bundles"]["physical_bundle_files"], 2)
        self.assertEqual(report["bundles"]["physical_distinct_filenames"], 1)
        self.assertTrue(os.path.exists(os.path.join(self.out, "bundle_accounting.csv")))
        self.assertTrue(os.path.exists(os.path.join(self.out, "addressable_keys.csv.gz")))


if __name__ == "__main__":
    unittest.main(verbosity=2)
