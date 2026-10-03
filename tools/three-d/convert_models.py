"""Convert the supplied PMX/GLB models for the Unity 3D scene prototype.

Python 3.10+, standard library only; no Blender or downloaded importer needed.
Run: python tools/three-d/convert_models.py

Output uses Unity coordinates, clockwise triangle winding and bottom-left UVs.
The restricted JSON schema carries mesh data, bone weights and vertex morphs.
MMD shaders, IK constraints, physics and material morphs are not implemented.
"""

from __future__ import annotations

import argparse
import collections
import io
import json
import math
from pathlib import Path
import shutil
import stat
import struct
import zipfile


MAX_SOURCE_BYTES = 512 * 1024 * 1024
MAX_ITEMS = 10_000_000


def inside(root: Path, relative: str | Path) -> Path:
    """Resolve an asset path and reject absolute paths and directory traversal."""
    value = str(relative).replace("\\", "/")
    path = Path(value)
    if path.is_absolute() or ":" in value or "\x00" in value:
        raise ValueError(f"Absolute or invalid asset path: {relative!r}")
    result = (root.resolve() / path).resolve()
    if not result.is_relative_to(root.resolve()):
        raise ValueError(f"Asset path escapes its directory: {relative!r}")
    return result


def extract_archive(archive: Path, target: Path) -> None:
    """Validate all ZIP entries before extraction; never follow archive symlinks."""
    target = target.resolve()
    with zipfile.ZipFile(archive) as zipped:
        members = zipped.infolist()
        if len(members) > 10_000 or sum(i.file_size for i in members) > MAX_SOURCE_BYTES:
            raise ValueError("ZIP exceeds the extraction size limit")
        for entry in members:
            inside(target, entry.filename)
            if stat.S_ISLNK(entry.external_attr >> 16):
                raise ValueError(f"ZIP symlink is unsupported: {entry.filename}")
        target.mkdir(parents=True, exist_ok=True)
        for entry in members:
            output = inside(target, entry.filename)
            if entry.is_dir():
                output.mkdir(parents=True, exist_ok=True)
            else:
                output.parent.mkdir(parents=True, exist_ok=True)
                with zipped.open(entry) as source, output.open("wb") as destination:
                    shutil.copyfileobj(source, destination)


def write_json(path: Path, value: object) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    with path.open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(value, stream, ensure_ascii=False, allow_nan=False, separators=(",", ":"))
        stream.write("\n")


def vector3(values: tuple | list) -> list:
    return [values[0], values[1], -values[2]]


def node(name: str, parent: int = -1, position=None, rotation=None, scale=None) -> dict:
    return {"name": name, "parent": parent, "position": position or [0, 0, 0],
            "rotation": rotation or [0, 0, 0, 1], "scale": scale or [1, 1, 1]}


def png_alpha(path: Path) -> bool:
    with path.open("rb") as stream:
        header = stream.read(26)
    return len(header) == 26 and header[:8] == b"\x89PNG\r\n\x1a\n" and header[25] in (4, 6)


def clockwise_winding(vertices: list, normals: list, submeshes: list) -> dict:
    """Choose Unity winding from actual normals, accounting for exporter variation.

    In Unity's left-handed coordinates, clockwise front-face geometry has a
    positive dot product between the edge cross product and its outward normal.
    Apply one consistent reversal per mesh rather than changing intentional
    back faces. Screen winding must not be inferred from a right-handed camera.
    """
    opposed, aligned, degenerate = 0, 0, 0
    for submesh in submeshes:
        indices = submesh["indices"]
        stride = max(3, (len(indices) // 12000 // 3) * 3)
        for offset in range(0, len(indices), stride):
            triangle = indices[offset:offset + 3]
            if len(triangle) != 3:
                raise ValueError("Invalid triangle index count")
            points = [vertices[i * 3:i * 3 + 3] for i in triangle]
            if any(len(point) != 3 for point in points):
                raise ValueError("Invalid triangle vertex index")
            a = [points[1][i] - points[0][i] for i in range(3)]
            b = [points[2][i] - points[0][i] for i in range(3)]
            cross = [a[1] * b[2] - a[2] * b[1], a[2] * b[0] - a[0] * b[2],
                     a[0] * b[1] - a[1] * b[0]]
            normal = [sum(normals[j * 3 + i] for j in triangle) for i in range(3)]
            dot = sum(cross[i] * normal[i] for i in range(3))
            if dot < -1e-12:
                opposed += 1
            elif dot > 1e-12:
                aligned += 1
            else:
                degenerate += 1
    reversed_indices = opposed > aligned
    if reversed_indices:
        for submesh in submeshes:
            indices = submesh["indices"]
            for i in range(0, len(indices), 3):
                indices[i + 1], indices[i + 2] = indices[i + 2], indices[i + 1]
    return {"sourceIndicesReversedAfterZMirror": reversed_indices,
            "unityFrontFaceNormalAgreement": "cross(v1-v0,v2-v0) dot normal > 0",
            "sampledNormalsOpposedCrossBeforeCorrection": opposed,
            "sampledNormalsAlignedCrossBeforeCorrection": aligned,
            "sampledDegenerateTriangles": degenerate}


class PmxReader:
    def __init__(self, path: Path):
        if path.stat().st_size > MAX_SOURCE_BYTES:
            raise ValueError("PMX exceeds the source size limit")
        self.stream = io.BytesIO(path.read_bytes())
        if self.read(4) != b"PMX ":
            raise ValueError("Invalid PMX signature")
        self.version = self.number("f")
        if not (1.99 < self.version < 2.11):
            raise ValueError(f"Unsupported PMX version {self.version}")
        header = self.read(self.number("B"))
        if len(header) < 8 or header[0] not in (0, 1) or header[1] > 4:
            raise ValueError("Invalid PMX header")
        self.encoding = "utf-16-le" if header[0] == 0 else "utf-8"
        self.extra_uv = header[1]
        self.sizes = dict(zip(("vertex", "texture", "material", "bone", "morph", "rigid"), header[2:8]))
        if any(size not in (1, 2, 4) for size in self.sizes.values()):
            raise ValueError("Invalid PMX index size")

    def read(self, size: int) -> bytes:
        if size < 0 or size > MAX_SOURCE_BYTES:
            raise ValueError("Invalid binary field size")
        value = self.stream.read(size)
        if len(value) != size:
            raise ValueError(f"Truncated PMX at byte {self.stream.tell()}")
        return value

    def number(self, fmt: str):
        return struct.unpack("<" + fmt, self.read(struct.calcsize("<" + fmt)))[0]

    def floats(self, count: int) -> list:
        return list(struct.unpack("<" + "f" * count, self.read(4 * count)))

    def count(self) -> int:
        value = self.number("i")
        if not 0 <= value <= MAX_ITEMS:
            raise ValueError(f"Invalid PMX count: {value}")
        return value

    def text(self) -> str:
        return self.read(self.count()).decode(self.encoding)

    def index(self, kind: str) -> int:
        size = self.sizes[kind]
        return self.number({1: "B" if kind == "vertex" else "b",
                            2: "H" if kind == "vertex" else "h", 4: "i"}[size])


def convert_pmx(source: Path, output: Path) -> dict:
    reader = PmxReader(source)
    name, english, comment, english_comment = [reader.text() for _ in range(4)]
    vertices, normals, uv, bone_indices, weights = [], [], [], [], []
    deform_types = collections.Counter()
    vertex_count = reader.count()
    for _ in range(vertex_count):
        vertices.extend(vector3(reader.floats(3)))
        normals.extend(vector3(reader.floats(3)))
        u, v = reader.floats(2)
        uv.extend((u, 1 - v))
        reader.read(reader.extra_uv * 16)
        deform = reader.number("B")
        deform_types[deform] += 1
        if deform == 0:
            indices, influences = [reader.index("bone")], [1.0]
        elif deform in (1, 3):
            indices = [reader.index("bone"), reader.index("bone")]
            influence = reader.number("f")
            influences = [influence, 1 - influence]
            if deform == 3:
                reader.read(36)  # SDEF C/R0/R1; ordinary skinning for this prototype.
        elif deform in (2, 4):
            indices = [reader.index("bone") for _ in range(4)]
            influences = reader.floats(4)
        else:
            raise ValueError(f"Unsupported PMX deform type {deform}")
        indices.extend([0] * (4 - len(indices)))
        influences.extend([0.0] * (4 - len(influences)))
        for i in range(4):
            if indices[i] < 0:
                indices[i], influences[i] = 0, 0.0
        total = sum(influences)
        if total <= 0 or any(w < 0 or not math.isfinite(w) for w in influences):
            raise ValueError("Invalid PMX bone weight")
        bone_indices.extend(indices)
        weights.extend(w / total for w in influences)
        reader.read(4)  # MMD outline scale.
    faces = [reader.index("vertex") for _ in range(reader.count())]
    if len(faces) % 3:
        raise ValueError("PMX triangle index count is not a multiple of three")
    textures = [reader.text() for _ in range(reader.count())]
    texture_outputs, texture_alpha = {}, {}
    for i, relative in enumerate(textures):
        texture = inside(source.parent, relative)
        if not texture.is_file():
            raise ValueError(f"Missing PMX texture: {relative}")
        destination = inside(output, "textures/" + texture.name)
        destination.parent.mkdir(parents=True, exist_ok=True)
        shutil.copyfile(texture, destination)
        texture_outputs[i] = destination.relative_to(output).as_posix()
        texture_alpha[i] = png_alpha(texture)
    materials, submeshes, cursor = [], [], 0
    for material_index in range(reader.count()):
        local, universal = reader.text(), reader.text()
        color = reader.floats(4)
        reader.read(12 + 4 + 12)
        flags = reader.number("B")
        reader.read(16 + 4)
        texture_index = reader.index("texture")
        reader.index("texture")  # Sphere map; not supported by the simple shader.
        reader.read(1)
        shared_toon = reader.number("B")
        reader.number("B") if shared_toon else reader.index("texture")
        reader.text()
        index_count = reader.count()
        if index_count % 3 or cursor + index_count > len(faces):
            raise ValueError("Invalid PMX material index range")
        if texture_index != -1 and texture_index not in texture_outputs:
            raise ValueError("Invalid PMX material texture index")
        materials.append({"name": local or universal or f"Material_{material_index}",
                          "color": color, "texture": texture_outputs.get(texture_index, ""),
                          "doubleSided": bool(flags & 1),
                          "alpha": color[3] < 0.999 or texture_alpha.get(texture_index, False)})
        submeshes.append({"material": material_index, "indices": faces[cursor:cursor + index_count]})
        cursor += index_count
    if cursor != len(faces):
        raise ValueError("PMX material ranges do not cover all triangle indices")
    winding_info = clockwise_winding(vertices, normals, submeshes)
    bones = []
    for _ in range(reader.count()):
        local, universal = reader.text(), reader.text()
        position, parent = vector3(reader.floats(3)), reader.index("bone")
        reader.read(4)
        flags = reader.number("H")
        reader.read(reader.sizes["bone"] if flags & 1 else 12)
        if flags & 0x300:
            reader.read(reader.sizes["bone"] + 4)
        if flags & 0x400:
            reader.read(12)
        if flags & 0x800:
            reader.read(24)
        if flags & 0x2000:
            reader.read(4)
        if flags & 0x20:
            reader.read(reader.sizes["bone"] + 8)
            for _ in range(reader.count()):
                reader.index("bone")
                if reader.number("B"):
                    reader.read(24)
        bones.append({"name": local or universal, "position": position, "parent": parent})
    nodes = [node(name or "Ema")]
    for i, bone in enumerate(bones):
        parent = bone["parent"]
        if parent < -1 or parent >= len(bones) or parent == i:
            raise ValueError("Invalid PMX bone parent")
        parent_pos = bones[parent]["position"] if parent >= 0 else [0, 0, 0]
        local_pos = [a - b for a, b in zip(bone["position"], parent_pos)]
        nodes.append(node(bone["name"], parent + 1 if parent >= 0 else 0, local_pos))
    morph_data, morph_types = [], collections.Counter()
    for _ in range(reader.count()):
        local, universal = reader.text(), reader.text()
        panel, kind, count = reader.number("B"), reader.number("B"), reader.count()
        morph_types[kind] += 1
        record = {"name": local or universal, "type": kind, "panel": panel, "data": []}
        if kind in (0, 9):
            record["data"] = [(reader.index("morph"), reader.number("f")) for _ in range(count)]
        elif kind == 1:
            record["data"] = [(reader.index("vertex"), vector3(reader.floats(3))) for _ in range(count)]
        else:
            size = {2: reader.sizes["bone"] + 28,
                    3: reader.sizes["vertex"] + 16, 4: reader.sizes["vertex"] + 16,
                    5: reader.sizes["vertex"] + 16, 6: reader.sizes["vertex"] + 16,
                    7: reader.sizes["vertex"] + 16, 8: reader.sizes["material"] + 113,
                    10: reader.sizes["rigid"] + 25}.get(kind)
            if size is None:
                raise ValueError(f"Unsupported PMX morph type {kind}")
            reader.read(count * size)
        morph_data.append(record)

    def expand_morph(index: int, stack: set) -> dict:
        if not 0 <= index < len(morph_data) or index in stack:
            raise ValueError("Invalid or cyclic PMX group morph")
        current = morph_data[index]
        accumulated = {}
        if current["type"] == 1:
            for vertex_index, offset in current["data"]:
                if not 0 <= vertex_index < vertex_count:
                    raise ValueError("Invalid PMX morph vertex index")
                target = accumulated.setdefault(vertex_index, [0.0, 0.0, 0.0])
                for axis in range(3):
                    target[axis] += offset[axis]
        elif current["type"] == 0:
            for referenced, influence in current["data"]:
                for vertex_index, offset in expand_morph(referenced, stack | {index}).items():
                    target = accumulated.setdefault(vertex_index, [0.0, 0.0, 0.0])
                    for axis in range(3):
                        target[axis] += offset[axis] * influence
        return accumulated

    morphs = []
    for i, record in enumerate(morph_data):
        if record["type"] in (0, 1):
            expanded = expand_morph(i, set())
            morphs.append({"name": record["name"], "indices": list(expanded),
                           "offsets": [v for value in expanded.values() for v in value]})
    # Consume and count the non-rendering PMX sections for a complete file check.
    display_count = reader.count()
    for _ in range(display_count):
        reader.text(); reader.text(); reader.read(1)
        for _ in range(reader.count()):
            reader.index("morph" if reader.number("B") else "bone")
    rigid_count = reader.count()
    for _ in range(rigid_count):
        reader.text(); reader.text(); reader.read(reader.sizes["bone"] + 61)
    joint_count = reader.count()
    for _ in range(joint_count):
        reader.text(); reader.text(); joint_type = reader.number("B")
        if joint_type != 0:
            raise ValueError("This prototype converter supports PMX spring 6DOF joints only")
        reader.read(2 * reader.sizes["rigid"] + 96)
    if reader.version > 2.05:
        if reader.count() != 0:
            raise ValueError("PMX soft bodies are not supported by this prototype")
    if reader.stream.read(1):
        raise ValueError("Unexpected trailing PMX data")
    mesh = {"name": name or "Ema", "node": 0, "vertices": vertices, "normals": normals,
            "uv": uv, "submeshes": submeshes, "bones": list(range(1, len(bones) + 1)),
            "weights": weights, "boneIndices": bone_indices, "morphs": morphs}
    info = {"sourceFormat": "PMX", "sourceFile": source.name, "vertexCount": vertex_count,
            "triangleCount": len(faces) // 3, "boneCount": len(bones),
            "sourceMorphCount": len(morph_data), "exportedMorphCount": len(morphs),
            "morphTypes": dict(morph_types), "deformTypes": dict(deform_types),
            "winding": winding_info,
            "rigidBodyCount": rigid_count, "jointCount": joint_count, "animationCount": 0,
            "limitations": ["MMD effect shaders, toon/sphere maps, IK/inherited bone constraints and rigid-body physics are not executed.",
                            "Bone, UV, material and impulse morphs are omitted; group morphs retain their vertex contribution."]}
    result = {"schemaVersion": 1, "name": "Ema", "info": info, "nodes": nodes,
              "materials": materials, "meshes": [mesh]}
    validate_model(result, output)
    write_json(output / "Ema.model.json", result)
    return info


def decompose_matrix(matrix: list) -> tuple:
    if len(matrix) != 16 or any(abs(matrix[i] - v) > 1e-5 for i, v in ((3, 0), (7, 0), (11, 0), (15, 1))):
        raise ValueError("Invalid glTF affine matrix")
    position = matrix[12:15]
    columns = [matrix[i:i + 3] for i in (0, 4, 8)]
    scales = [math.sqrt(sum(v * v for v in column)) for column in columns]
    if any(s <= 1e-10 for s in scales):
        raise ValueError("Cannot decompose a glTF node with zero scale")
    columns = [[v / scale for v in column] for column, scale in zip(columns, scales)]
    determinant = (columns[0][0] * (columns[1][1] * columns[2][2] - columns[1][2] * columns[2][1])
                   - columns[1][0] * (columns[0][1] * columns[2][2] - columns[0][2] * columns[2][1])
                   + columns[2][0] * (columns[0][1] * columns[1][2] - columns[0][2] * columns[1][1]))
    if determinant < 0:
        scales[0] *= -1
        columns[0] = [-v for v in columns[0]]
    if any(abs(sum(a * b for a, b in zip(columns[i], columns[j]))) > 1e-5
           for i, j in ((0, 1), (0, 2), (1, 2))):
        raise ValueError("Sheared glTF node matrices cannot be represented by TRS")
    m = [[columns[c][r] for c in range(3)] for r in range(3)]
    trace = sum(m[i][i] for i in range(3))
    if trace > 0:
        s = math.sqrt(trace + 1) * 2
        rotation = [(m[2][1] - m[1][2]) / s, (m[0][2] - m[2][0]) / s,
                    (m[1][0] - m[0][1]) / s, s / 4]
    else:
        i = max(range(3), key=lambda axis: m[axis][axis])
        j, k = (i + 1) % 3, (i + 2) % 3
        s = math.sqrt(max(0, 1 + m[i][i] - m[j][j] - m[k][k])) * 2
        if s <= 1e-10:
            raise ValueError("Invalid glTF rotation matrix")
        rotation = [0.0] * 4
        rotation[i] = s / 4
        rotation[j] = (m[j][i] + m[i][j]) / s
        rotation[k] = (m[k][i] + m[i][k]) / s
        rotation[3] = (m[k][j] - m[j][k]) / s
    return position, rotation, scales


def convert_glb(source: Path, output: Path) -> dict:
    if source.stat().st_size > MAX_SOURCE_BYTES:
        raise ValueError("GLB exceeds the source size limit")
    binary = source.read_bytes()
    if len(binary) < 20:
        raise ValueError("Truncated GLB")
    magic, version, declared_size = struct.unpack_from("<4sII", binary)
    if magic != b"glTF" or version != 2 or declared_size != len(binary):
        raise ValueError("Invalid GLB header")
    chunks, offset = {}, 12
    while offset < len(binary):
        if offset + 8 > len(binary):
            raise ValueError("Truncated GLB chunk header")
        length, kind = struct.unpack_from("<II", binary, offset)
        offset += 8
        if offset + length > len(binary) or kind in chunks:
            raise ValueError("Invalid GLB chunk")
        chunks[kind] = binary[offset:offset + length]
        offset += length
    if 0x4E4F534A not in chunks or 0x004E4942 not in chunks:
        raise ValueError("GLB must contain JSON and embedded binary chunks")
    gltf, blob = json.loads(chunks[0x4E4F534A]), chunks[0x004E4942]
    if gltf.get("skins"):
        raise ValueError("Skinned GLB is outside this static placeholder converter")
    if len(gltf.get("buffers", [])) != 1 or gltf["buffers"][0].get("uri"):
        raise ValueError("Only single-buffer self-contained GLB is supported")

    def buffer_view(index: int) -> tuple:
        view = gltf["bufferViews"][index]
        start, length = view.get("byteOffset", 0), view["byteLength"]
        if view.get("buffer", 0) != 0 or start < 0 or length < 0 or start + length > len(blob):
            raise ValueError("Invalid GLB bufferView")
        return view, start, length

    def accessor(index: int) -> list:
        record = gltf["accessors"][index]
        if "sparse" in record:
            raise ValueError("Sparse GLB accessors are unsupported by this prototype")
        formats = {5120: ("b", 1), 5121: ("B", 1), 5122: ("h", 2),
                   5123: ("H", 2), 5125: ("I", 4), 5126: ("f", 4)}
        fmt, size = formats[record["componentType"]]
        width = {"SCALAR": 1, "VEC2": 2, "VEC3": 3, "VEC4": 4}[record["type"]]
        view, start, length = buffer_view(record["bufferView"])
        local_start, count = record.get("byteOffset", 0), record["count"]
        stride = view.get("byteStride", width * size)
        if not 0 <= count <= MAX_ITEMS or local_start < 0 or stride < width * size:
            raise ValueError("Invalid GLB accessor dimensions")
        if count and local_start + (count - 1) * stride + width * size > length:
            raise ValueError("GLB accessor exceeds its bufferView")
        unpack = struct.Struct("<" + fmt * width)
        result = [list(unpack.unpack_from(blob, start + local_start + i * stride)) for i in range(count)]
        if record.get("normalized") and record["componentType"] != 5126:
            bits = size * 8
            signed = fmt in ("b", "h")
            divisor = (1 << (bits - 1)) - 1 if signed else (1 << bits) - 1
            result = [[max(-1, x / divisor) if signed else x / divisor for x in row] for row in result]
        return result

    image_paths, image_alpha = [], []
    for i, image in enumerate(gltf.get("images", [])):
        if "bufferView" not in image:
            raise ValueError("External GLB images are unsupported")
        _, start, length = buffer_view(image["bufferView"])
        extension = {"image/png": ".png", "image/jpeg": ".jpg"}.get(image.get("mimeType"))
        if extension is None:
            raise ValueError("Unsupported GLB image format")
        relative = f"textures/image_{i:02d}{extension}"
        texture = inside(output, relative)
        texture.parent.mkdir(parents=True, exist_ok=True)
        texture.write_bytes(blob[start:start + length])
        image_paths.append(relative)
        image_alpha.append(png_alpha(texture))
    materials = []
    for i, material in enumerate(gltf.get("materials", [])):
        legacy = material.get("extensions", {}).get("KHR_materials_pbrSpecularGlossiness")
        pbr = material.get("pbrMetallicRoughness", {})
        color = (legacy or pbr).get("diffuseFactor" if legacy else "baseColorFactor", [1, 1, 1, 1])
        texture_spec = (legacy or pbr).get("diffuseTexture" if legacy else "baseColorTexture")
        texture_path, has_alpha = "", False
        if texture_spec:
            if texture_spec.get("texCoord", 0) != 0:
                raise ValueError("Only primary GLB UV coordinates are supported")
            texture_source = gltf["textures"][texture_spec["index"]]["source"]
            texture_path, has_alpha = image_paths[texture_source], image_alpha[texture_source]
        materials.append({"name": material.get("name", f"Material_{i}"), "color": color,
                          "texture": texture_path, "doubleSided": material.get("doubleSided", False),
                          "alpha": material.get("alphaMode", "OPAQUE") != "OPAQUE" or color[3] < 0.999})
    parents = [-1] * len(gltf.get("nodes", []))
    for i, current in enumerate(gltf["nodes"]):
        for child in current.get("children", []):
            if not 0 <= child < len(parents) or parents[child] != -1:
                raise ValueError("Invalid GLB node hierarchy")
            parents[child] = i
    nodes, meshes = [], []
    for i, current in enumerate(gltf["nodes"]):
        if "matrix" in current:
            position, rotation, scale = decompose_matrix(current["matrix"])
        else:
            position, rotation, scale = current.get("translation", [0, 0, 0]), current.get("rotation", [0, 0, 0, 1]), current.get("scale", [1, 1, 1])
        nodes.append(node(current.get("name", f"Node_{i}"), parents[i], vector3(position),
                          [-rotation[0], -rotation[1], rotation[2], rotation[3]], scale))
        if "mesh" not in current:
            continue
        original = gltf["meshes"][current["mesh"]]
        vertices, normals, uv, submeshes = [], [], [], []
        for primitive in original["primitives"]:
            if primitive.get("mode", 4) != 4 or primitive.get("extensions") or primitive.get("targets"):
                raise ValueError("Only uncompressed static GLB triangle primitives are supported")
            attributes = primitive["attributes"]
            positions = accessor(attributes["POSITION"])
            primitive_normals = accessor(attributes["NORMAL"]) if "NORMAL" in attributes else [[0, 1, 0]] * len(positions)
            primitive_uv = accessor(attributes["TEXCOORD_0"]) if "TEXCOORD_0" in attributes else [[0, 0]] * len(positions)
            if len(primitive_normals) != len(positions) or len(primitive_uv) != len(positions):
                raise ValueError("GLB vertex attribute counts disagree")
            base_vertex = len(vertices) // 3
            vertices.extend(v for xyz in positions for v in vector3(xyz))
            normals.extend(v for xyz in primitive_normals for v in vector3(xyz))
            uv.extend(v for xy in primitive_uv for v in (xy[0], 1 - xy[1]))
            indices = [row[0] for row in accessor(primitive["indices"])] if "indices" in primitive else list(range(len(positions)))
            if any(not isinstance(index, int) or not 0 <= index < len(positions) for index in indices):
                raise ValueError("Invalid GLB primitive vertex index")
            submeshes.append({"material": primitive.get("material", 0),
                              "indices": [index + base_vertex for index in indices]})
        winding_info = clockwise_winding(vertices, normals, submeshes)
        meshes.append({"name": original.get("name", f"Mesh_{current['mesh']}"), "node": i,
                       "vertices": vertices, "normals": normals, "uv": uv,
                       "submeshes": submeshes, "bones": [], "weights": [], "boneIndices": [], "morphs": [],
                       "winding": winding_info})
    info = {"sourceFormat": "glTF 2.0 GLB", "sourceFile": source.name,
            "vertexCount": sum(len(m["vertices"]) // 3 for m in meshes),
            "triangleCount": sum(len(s["indices"]) // 3 for m in meshes for s in m["submeshes"]),
            "boneCount": 0, "sourceMorphCount": 0, "exportedMorphCount": 0,
            "animationCount": len(gltf.get("animations", [])), "imageCount": len(image_paths),
            "sourceMetadata": gltf.get("asset", {}).get("extras", {}),
            "limitations": ["Specular/glossiness and normal textures are extracted but only the diffuse texture is assigned.",
                            "The supplied model has no bones or animation and is used as a static placeholder."]}
    result = {"schemaVersion": 1, "name": "BMPT72", "info": info,
              "nodes": nodes, "materials": materials, "meshes": meshes}
    validate_model(result, output)
    write_json(output / "BMPT72.model.json", result)
    return info


def validate_model(model: dict, output: Path) -> None:
    nodes, materials = model["nodes"], model["materials"]
    for i, current in enumerate(nodes):
        seen, parent = {i}, current["parent"]
        while parent != -1:
            if not 0 <= parent < len(nodes) or parent in seen:
                raise ValueError("Invalid or cyclic output node hierarchy")
            seen.add(parent)
            parent = nodes[parent]["parent"]
        for key, length in (("position", 3), ("rotation", 4), ("scale", 3)):
            if len(current[key]) != length or any(not math.isfinite(v) for v in current[key]):
                raise ValueError(f"Invalid output node {key}")
    for material in materials:
        if len(material["color"]) != 4 or any(not math.isfinite(v) for v in material["color"]):
            raise ValueError("Invalid output material color")
        if material["texture"] and not inside(output, material["texture"]).is_file():
            raise ValueError("Output material texture is missing")
    for mesh in model["meshes"]:
        vertex_count = len(mesh["vertices"]) // 3
        if len(mesh["vertices"]) % 3 or len(mesh["normals"]) != vertex_count * 3 or len(mesh["uv"]) != vertex_count * 2:
            raise ValueError("Output mesh attribute lengths disagree")
        if not 0 <= mesh["node"] < len(nodes):
            raise ValueError("Invalid output mesh node")
        for key in ("vertices", "normals", "uv", "weights"):
            if any(not math.isfinite(value) for value in mesh[key]):
                raise ValueError("Output model contains a non-finite value")
        for submesh in mesh["submeshes"]:
            if not 0 <= submesh["material"] < len(materials) or len(submesh["indices"]) % 3:
                raise ValueError("Invalid output submesh")
            if any(not 0 <= index < vertex_count for index in submesh["indices"]):
                raise ValueError("Invalid output vertex index")
        if mesh["bones"]:
            if any(not 0 <= bone < len(nodes) for bone in mesh["bones"]):
                raise ValueError("Invalid output bone node")
            if len(mesh["weights"]) != vertex_count * 4 or len(mesh["boneIndices"]) != vertex_count * 4:
                raise ValueError("Invalid output skinning attribute lengths")
            if any(not 0 <= index < len(mesh["bones"]) for index in mesh["boneIndices"]):
                raise ValueError("Invalid output bone index")
            if any(abs(sum(mesh["weights"][i:i + 4]) - 1) > 1e-5 for i in range(0, len(mesh["weights"]), 4)):
                raise ValueError("Output bone weights do not sum to one")
        for morph in mesh["morphs"]:
            if len(morph["offsets"]) != 3 * len(morph["indices"]):
                raise ValueError("Invalid output morph length")
            if any(not 0 <= index < vertex_count for index in morph["indices"]) or any(not math.isfinite(v) for v in morph["offsets"]):
                raise ValueError("Invalid output morph values")


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=Path(__file__).resolve().parents[2],
                        help="Project root containing the supplied .3d-model directory")
    parser.add_argument("--only", choices=("Ema", "BMPT72"), help="Convert a single model")
    args = parser.parse_args()
    project = args.root.resolve()
    model_dir = inside(project, ".3d-model")
    generated = inside(model_dir, "generated")
    inspection = inside(model_dir, "inspection")
    manifest = {}
    if args.only in (None, "Ema"):
        archive = inside(model_dir, "SakurabaEma_ByPOWER_v1_0.zip")
        extract_archive(archive, inspection)
        manifest["Ema"] = convert_pmx(inside(inspection, "SakurabaEma_ByPOWER_v1_0/SakurabaEma_ByPOWER.pmx"),
                                       inside(generated, "Ema"))
    if args.only in (None, "BMPT72"):
        manifest["BMPT72"] = convert_glb(inside(model_dir, "bmpt-72_war_thunder_line_of_contact.glb"),
                                         inside(generated, "BMPT72"))
    write_json(generated / ("manifest.json" if args.only is None else f"{args.only}.manifest.json"), manifest)
    print(json.dumps(manifest, ensure_ascii=True, allow_nan=False, indent=2))


if __name__ == "__main__":
    main()
