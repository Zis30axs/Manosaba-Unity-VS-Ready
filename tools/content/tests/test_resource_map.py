import csv
import json
import tempfile
import unittest
from pathlib import Path

try:
    from tools.content import resource_map
except ModuleNotFoundError:  # unittest discovery from tools/content
    import resource_map


class ResourceMapTests(unittest.TestCase):
    def write_index(self, root, rows, name="_index.csv"):
        path = Path(root) / name
        with path.open("w", encoding="utf-8", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=resource_map.FIELDS, lineterminator="\n")
            writer.writeheader()
            writer.writerows(rows)
        return path

    def row(self, path_id="1", output="Assets/A.asset.json"):
        return {"type": "Texture2D", "name": "A", "asset_path": "Assets/A.png",
                "source_bundle": "aa/bundle", "path_id": path_id, "sample_id": "", "output_file": output}

    def test_verify_accepts_output_suffix_remap(self):
        with tempfile.TemporaryDirectory() as temporary:
            root = Path(temporary)
            expected = self.row(output="Assets/A.json")
            actual = self.row(output="Assets/A~import1.json")
            index = self.write_index(root, [actual])
            mapping = root / "resource-map.csv"
            metadata = root / "resource-map.meta.json"
            resource_map.write_map(mapping, [expected])
            resource_map.write_meta(metadata, mapping, [expected])
            report = resource_map.verify(index, mapping, metadata, check_files=False)
            self.assertEqual(report["status"], "PASS")
            self.assertEqual(report["output_remapped"], 1)

    def test_verify_rejects_identity_or_container_drift(self):
        with tempfile.TemporaryDirectory() as temporary:
            root = Path(temporary)
            expected = self.row()
            actual = self.row()
            actual["asset_path"] = "Assets/Other.png"
            index = self.write_index(root, [actual])
            mapping = root / "resource-map.csv"
            resource_map.write_map(mapping, [expected])
            report = resource_map.verify(index, mapping, None, check_files=False)
            self.assertEqual(report["status"], "FAIL")
            self.assertEqual(report["changed"], 1)

    def test_metadata_digest_is_checked(self):
        with tempfile.TemporaryDirectory() as temporary:
            root = Path(temporary)
            row = self.row()
            index = self.write_index(root, [row])
            mapping = root / "resource-map.csv"
            metadata = root / "resource-map.meta.json"
            resource_map.write_map(mapping, [row])
            metadata.write_text(json.dumps({"schema": 1, "semantic_sha256": "wrong"}), encoding="utf-8")
            report = resource_map.verify(index, mapping, metadata, check_files=False)
            self.assertEqual(report["status"], "FAIL")
            self.assertIn("does not match", report["meta_error"])


if __name__ == "__main__":
    unittest.main()
