import copy
import json
from pathlib import Path
import sys
import tempfile
import unittest

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
from state_store import compact_fields, read_content_state


class StateStoreTests(unittest.TestCase):
    def load_text(self, text, chunk=7):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / "state.json"
            path.write_text(text, encoding="utf-8-sig")
            return read_content_state(path, chunk_size=chunk)

    def test_all_chunk_boundaries_unicode_escapes_numbers_and_unknown_fields(self):
        data = {"version": 123456789, "files": {"角色/\"a\\.json": {
            "status": "PENDING", "value": "原文\\\"\n}", "float": -1.234e-19}},
            "unknown": [True, False, None, {"nested": [1, 2]}]}
        text = json.dumps(data, ensure_ascii=False)
        for chunk in (1, 2, 3, 7, 17, 1024):
            with self.subTest(chunk=chunk):
                self.assertEqual(self.load_text(text, chunk), data)

    def test_compacts_large_record_and_preserves_status_identity_and_counts(self):
        fields = [{"field": f"list.Array.data[{index}].value", "editability": "FULL", "reason": "integer"}
                  for index in range(2000)]
        data = {"files": {"a": {"field_editability": fields, "status": "FAILED", "guid": "a" * 32}}}
        result = self.load_text(json.dumps(data), 31)["files"]["a"]
        self.assertEqual(result["status"], "FAILED")
        self.assertEqual(result["guid"], "a" * 32)
        self.assertEqual(result["field_editability"], [{"field": "list.Array.data[*].value",
            "editability": "FULL", "reason": "integer", "occurrences": 2000}])

    def test_mixed_support_is_partial_reasons_unique_nested_pattern_idempotent(self):
        fields = [{"field": "a.Array.data[0].b.Array.data[9]", "editability": "FULL", "reason": "one", "occurrences": 4},
                  {"field": "a.Array.data[1].b.Array.data[2]", "editability": "READ_ONLY", "reason": "two"},
                  {"field": "a.Array.data[2].b.Array.data[3]", "editability": "FULL", "reason": "one"}]
        before = copy.deepcopy(fields)
        result = compact_fields(fields)
        self.assertEqual(result[0], {"field": "a.Array.data[*].b.Array.data[*]", "editability": "PARTIAL",
            "reason": "one; two", "occurrences": 6})
        self.assertEqual(compact_fields(result), result)
        self.assertEqual(fields, before)

    def test_rejects_truncation_malformed_duplicate_and_trailing_data(self):
        for text in ('', '{', '{"files":{"a":', '{"files":{"a":{}}', '{"files":{"a":{},}}',
                     '{"files":{},}', '{"files":{},"files":{}}', '{"files":{"a":{},"a":{}}}',
                     '{"files":{"a":null}}', '{"version":12x}', '{} []', '{"files":[]}'):
            with self.subTest(text=text), self.assertRaises((ValueError, TypeError)):
                self.load_text(text, 1)

    def test_empty_and_missing_state(self):
        self.assertEqual(self.load_text('{"files":{}}', 1), {"files": {}})
        self.assertEqual(self.load_text('{}', 1), {})
        self.assertIsNone(read_content_state(Path("__absent_content_state__.json")))

    def test_invalid_field_metadata_cannot_be_silently_discarded(self):
        for fields in ({}, [None], [{"field": "a"}],
                       [{"field": "a", "editability": "FULL", "occurrences": True}],
                       [{"field": "a", "editability": "FULL", "occurrences": 1.5}]):
            with self.subTest(fields=fields), self.assertRaises(ValueError):
                self.load_text(json.dumps({"files": {"a": {"field_editability": fields}}}))

    def test_unknown_metadata_is_preserved_or_conflicts_fail(self):
        field = {"field": "a.Array.data[0]", "editability": "FULL", "future": "keep"}
        self.assertEqual(compact_fields([field])[0]["future"], "keep")
        with self.assertRaises(ValueError):
            compact_fields([field, {**field, "field": "a.Array.data[1]", "future": "different"}])


if __name__ == "__main__":
    unittest.main()
