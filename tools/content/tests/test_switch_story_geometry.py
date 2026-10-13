"""Validate the reviewed geometry against a collaborator's own cached NS bundle.

No source artwork/mesh is embedded in the repository. These checks skip when the
optional source cache is absent; Unity import/readback remains a separate gate.
"""
import copy
import hashlib
import json
from pathlib import Path
import struct
import sys
from types import SimpleNamespace
import unittest
from unittest.mock import patch

sys.path.insert(0, str(Path(__file__).resolve().parents[1]))
import switch_story_assets as assets


class ReviewedGlassGeometryTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.source = assets.REPO / "res/.imported-source/switch-story" / assets.GLASS_MESH_BUNDLE
        if not cls.source.is_file():
            raise unittest.SkipTest("Optional verified Switch story bundle cache is absent")
        import UnityPy
        cls.env = UnityPy.load(str(cls.source))
        cls.objects = {obj.path_id: obj for obj in cls.env.objects if obj.path_id in assets.GLASS_MESHES}
        cls.atlas = next(obj.read() for obj in cls.env.objects if obj.type.name == "SpriteAtlas")

    def inputs(self, pid=-6803846092496667308):
        obj = self.objects[pid]
        data = obj.read()
        rd = next(value for key, value in self.atlas.m_RenderDataMap if key == data.m_RenderDataKey)
        return obj, obj.read_typetree(), rd

    def recipe(self, pid=-6803846092496667308):
        return assets.reviewed_glass_geometry(*self.inputs(pid), assets.GLASS_MESH_BUNDLE, (1895, 817))

    def test_actual_source_topology_and_pixel_roundtrip(self):
        # Digests pin vertex order/winding/indices, not merely equal object counts.
        expected = {
            2912700173187937077: "24096181cecaa9e75d7d96ccb6c61d799ef103034c8edcc825b60aefbdc69165",
            1974279541955088290: "92e42e3da7afda6366c5c4ffe9e0ac282a8179a085a76c3a7eba175c56458ca1",
            -6803846092496667308: "8f3cf0f049f05f253c24f03e16ab813b74a8660d515c9fc05a1b99b839b6606c",
        }
        for pid, digest in expected.items():
            with self.subTest(pid=pid):
                geometry = self.recipe(pid)
                raw = b"".join(struct.pack("<ff", *v) for v in geometry["vertices"])
                raw += struct.pack("<%dH" % len(geometry["indices"]), *geometry["indices"])
                self.assertEqual(hashlib.sha256(raw).hexdigest(), digest)
                for x, y in geometry["vertices"]:
                    pixel = [x * 100 + 947.5, y * 100 + 408.5]
                    self.assertAlmostEqual((pixel[0] - 947.5) / 100, x)
                    self.assertAlmostEqual((pixel[1] - 408.5) / 100, y)
                self.assertEqual(geometry["source_degenerate_triangles"], 2 if pid < 0 else 0)

    def test_repeated_extraction_is_identical_and_preserves_source_bytes(self):
        before = hashlib.sha256(self.source.read_bytes()).digest()
        first = json.dumps(self.recipe(), sort_keys=True, allow_nan=False)
        second = json.dumps(self.recipe(), sort_keys=True, allow_nan=False)
        self.assertEqual(first, second)
        self.assertEqual(hashlib.sha256(self.source.read_bytes()).digest(), before)

    def test_rejects_incorrect_atlas_to_canvas_mapping(self):
        obj, tree, rd = self.inputs()
        wrong = copy.copy(rd)
        wrong.uvTransform = SimpleNamespace(x=100, y=rd.uvTransform.y + 49, z=100, w=rd.uvTransform.w)
        with self.assertRaisesRegex(ValueError, "UVs do not match"):
            assets.reviewed_glass_geometry(obj, tree, wrong, assets.GLASS_MESH_BUNDLE, (1895, 817))

    def test_rejects_nonfinite_vertices_and_out_of_range_indices(self):
        from UnityPy.helpers.MeshHelper import MeshHandler
        obj, tree, rd = self.inputs()
        original = MeshHandler(obj.read().m_RD, obj.version)
        original.process()
        triangles = [list(triangle) for submesh in original.get_triangles() for triangle in submesh]
        for corruption in ("nonfinite", "index"):
            with self.subTest(corruption=corruption):
                vertices = [list(vertex) for vertex in original.m_Vertices]
                faces = copy.deepcopy(triangles)
                if corruption == "nonfinite":
                    vertices[0][0] = float("nan")
                else:
                    faces[0][0] = len(vertices)
                bad = SimpleNamespace(m_Vertices=vertices, process=lambda: None, get_triangles=lambda: [faces])
                with patch("UnityPy.helpers.MeshHelper.MeshHandler", return_value=bad):
                    with self.assertRaisesRegex(ValueError, "Invalid W glass vertices or triangle indices"):
                        assets.reviewed_glass_geometry(obj, tree, rd, assets.GLASS_MESH_BUNDLE, (1895, 817))


if __name__ == "__main__":
    unittest.main()
