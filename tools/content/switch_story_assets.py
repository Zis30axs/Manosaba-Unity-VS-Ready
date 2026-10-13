"""Verified Switch story visual delta, feeding the ordinary prefab/leaf converters.

This recipe intentionally names the reviewed 2.0.4b additions. It is not a general
bundle importer: an unfamiliar object, controller shape or script fails closed.
"""
from __future__ import annotations

from io import BytesIO
import json
import math
from pathlib import Path
import re
import sys
import zlib

from switch_title import REPO, GRAPH, write

ROOT = "Assets/GameContent/#WitchTrials/SwitchStory"
STILLS = [f"710_{n}" for n in range(1, 9)] + ["720_1", "730_1", "740_1", "750_1", "750_2", "760_1", "770_1"]
CUTINS = ["CutinSmall_" + name for name in (
    "Alisa", "Anan", "Coco", "Ema", "Hanna", "Hiro", "Leia", "Margo", "Meruru", "Miria", "Nanoka", "Noah", "Sherry")]
CUTINS += [f"CutinW{i}" for i in range(1, 5)] + ["ObjectionCutIn_w"]
GRAPH = GRAPH | {"AudioSource", "PlayableDirector"}
TIMELINE = {"TimelineAsset", "GroupTrack", "AnimationTrack", "AnimationPlayableAsset",
            "ActivationTrack", "ActivationPlayableAsset", "AudioTrack", "AudioPlayableAsset",
            "ControlTrack", "ControlPlayableAsset"}
RUNTIME = {"ObjectionCutIn", "SetShaderTimerAsUnscale", "SpriteLocalCoords"}
GLASS_MESH_BUNDLE = "general-sprites-cutin_normal_w_assets_all.bundle"
GLASS_MESHES = {
    2912700173187937077: ("CutIn_Normal_W_Fx_StainedGlass_001", 211, 627),
    1974279541955088290: ("CutIn_Normal_W_Fx_StainedGlass_002", 207, 615),
    -6803846092496667308: ("CutIn_Normal_W_Fx_StainedGlass_003", 230, 708),
}


def reviewed_glass_geometry(obj, tree, atlas_rd, bundle, image_size):
    """Keep the reviewed tight mesh, in Sprite-local units, over the padded PNG.

    These sprites use rectangular atlas packing with a tight render mesh. UnityPy
    therefore exports the rectangle, including edge extrusion, without clipping it.
    The importer persists the original triangles instead of altering texture pixels.
    """
    expected = GLASS_MESHES.get(obj.path_id)
    if expected is None:
        return None
    name, vertex_count, index_count = expected
    rect, pivot = tree["m_Rect"], tree["m_Pivot"]
    if (bundle != GLASS_MESH_BUNDLE or obj.type.name != "Sprite" or tree["m_Name"] != name
            or [rect[k] for k in ("x", "y", "width", "height")] != [0, 0, 1895, 817]
            or [pivot[k] for k in ("x", "y")] != [.5, .5] or tree["m_PixelsToUnits"] != 100
            or list(image_size) != [1895, 817] or atlas_rd.settingsRaw != 67
            or atlas_rd.downscaleMultiplier != 1):
        raise ValueError("Unreviewed W glass mesh/canvas identity: " + name)
    # Integer crop and padding cancel the atlas translation exactly. Do not add
    # textureRectOffset again when converting local vertices to importer pixels.
    offset = [atlas_rd.uvTransform.y - round(atlas_rd.textureRect.x) + round(atlas_rd.textureRectOffset.x),
              atlas_rd.uvTransform.w - round(atlas_rd.textureRect.y) + round(atlas_rd.textureRectOffset.y)]
    if (atlas_rd.uvTransform.x != 100 or atlas_rd.uvTransform.z != 100
            or offset != [947.5, 408.5]):
        raise ValueError("W glass atlas UVs do not match its restored canvas: " + name)
    from UnityPy.helpers.MeshHelper import MeshHandler
    mesh = MeshHandler(obj.read().m_RD, obj.version)
    mesh.process()
    vertices = mesh.m_Vertices
    indices = [index for submesh in mesh.get_triangles() for triangle in submesh for index in triangle]
    if (len(vertices) != vertex_count or len(indices) != index_count
            or any(len(v) != 3 or not all(math.isfinite(c) for c in v) or v[2] != 0
                   or not 0 <= v[0] * 100 + 947.5 <= 1895
                   or not 0 <= v[1] * 100 + 408.5 <= 817 for v in vertices)
            or any(isinstance(i, bool) or not isinstance(i, int) or not 0 <= i < vertex_count for i in indices)):
        raise ValueError("Invalid W glass vertices or triangle indices: " + name)
    degenerate = 0
    for a, b, c in zip(indices[::3], indices[1::3], indices[2::3]):
        va, vb, vc = vertices[a], vertices[b], vertices[c]
        if len({a, b, c}) != 3:
            raise ValueError("Repeated W glass triangle index: " + name)
        degenerate += abs((vb[0] - va[0]) * (vc[1] - va[1])
                          - (vb[1] - va[1]) * (vc[0] - va[0])) < 1e-10
    # Stage003 contains two collinear source triangles. Preserve the original
    # topology, including these non-rendering triangles, instead of silently repairing it.
    if degenerate != (2 if obj.path_id == -6803846092496667308 else 0):
        raise ValueError("Unreviewed W glass degenerate triangle count: " + name)
    return {"version": 1, "coordinate_space": "sprite_local_units", "source_bundle": bundle,
            "source_path_id": str(obj.path_id), "source_name": name,
            "source_rect": [rect[k] for k in ("x", "y", "width", "height")],
            "source_pivot": [pivot[k] for k in ("x", "y")], "source_ppu": tree["m_PixelsToUnits"],
            "texture_rect_offset": [atlas_rd.textureRectOffset.x, atlas_rd.textureRectOffset.y],
            "atlas_settings_raw": atlas_rd.settingsRaw, "canvas_uv_offset": offset,
            "source_degenerate_triangles": degenerate,
            "vertices": [[v[0], v[1]] for v in vertices], "indices": indices}


def inspect_source(folder):
    folder = Path(folder)
    bundles = [f"naninovel-backgrounds_assets_naninovel_backgrounds_stills_{name}.bundle" for name in STILLS]
    bundles += [f"naninovel-spawn_assets_naninovel_spawn_{name.lower()}.bundle" for name in CUTINS]
    bundles += [p.name for p in folder.glob("general-*cutin*.bundle")]
    bundles += ["general-animations_assets_all.bundle", "general-materials_assets_all.bundle",
                "naninovel-audio_assets_audio-sfxnaninovel.bundle", "naninovel-audio_assets_naninovel.bundle",
                "naninovel-ui_assets_all.bundle", "general-voice-commontrial_assets_all.bundle",
                "general-sprites-separately_assets_general_textures_separately_noise.bundle"]
    for suffix in ("monoscripts", "unitybuiltinassets"):
        matches = list(folder.glob("*_" + suffix + ".bundle"))
        if len(matches) != 1:
            raise ValueError(f"Expected one Switch {suffix} bundle")
        bundles += [matches[0].name]
    for name in bundles:
        if not (folder / name).is_file():
            raise ValueError("Missing Switch story dependency bundle: " + name)
    return {"version": 1, "bundles": sorted(set(bundles)), "stills": STILLS, "cutins": CUTINS}


def build_assets(cache, output, spec):
    if spec.get("version") != 1 or spec.get("stills") != STILLS or spec.get("cutins") != CUTINS:
        raise ValueError("Unrecognized Switch story asset selection; inspect the source again")
    return Planner(Path(cache), Path(output), spec).build()


class Planner:
    def __init__(self, cache, output, spec):
        import UnityPy
        self.output, self.spec = output.resolve(), spec
        if not self.output.is_relative_to((REPO / "res/.manosaba").resolve()):
            raise ValueError("Story recipe output must be in res/.manosaba")
        self.env = UnityPy.Environment()
        self.objects, self.origins, self.by_bundle = {}, {}, {}
        for name in spec["bundles"]:
            if not re.fullmatch(r"[A-Za-z0-9_.-]+\.bundle", name):
                raise ValueError("Invalid story cache bundle name")
            loaded = self.env.load_file(str(cache / name))
            group = []
            for file in loaded.files.values():
                if not hasattr(file, "objects"):
                    continue
                self.origins[file.name] = name
                for obj in file.objects.values():
                    self.objects[self.key(obj)] = obj
                    group.append(obj)
            self.by_bundle[name] = group
        self.leaves, self.assets, self.addresses = {}, {}, []

    @staticmethod
    def key(obj):
        return obj.assets_file.name, obj.path_id

    def resolve(self, owner, pointer):
        pid, fid = pointer["m_PathID"], pointer["m_FileID"]
        if not pid:
            return None
        cab = owner.assets_file.name if fid == 0 else owner.assets_file.externals[fid - 1].path.rsplit("/", 1)[-1]
        if (cab, pid) not in self.objects:
            raise ValueError(f"Uncached story dependency {cab}:{pid} from {owner.path_id}")
        return self.objects[(cab, pid)]

    def script(self, obj, tree):
        script = self.resolve(obj, tree["m_Script"]).read_typetree()
        name, ns = script["m_ClassName"], script["m_Namespace"]
        if not ((ns == "UnityEngine.Timeline" and name in TIMELINE) or (ns == "WitchTrials.Views" and name in RUNTIME)):
            raise ValueError(f"Unreviewed story script {ns}.{name}")
        return {"class": name, "namespace": ns, "assembly": script["m_AssemblyName"].removesuffix(".dll")}

    def leaf(self, obj):
        key = self.key(obj)
        if key in self.assets:
            return self.assets[key]
        tree, data = obj.read_typetree(), obj.read()
        name = re.sub(r"[^A-Za-z0-9_.-]", "_", tree["m_Name"]) + "_" + str(obj.path_id) + "_" + obj.assets_file.name.removeprefix("CAB-")[:8]
        if obj.type.name == "AudioClip":
            import soundfile
            samples = data.samples
            if len(samples) != 1:
                raise ValueError(f"Expected one decoded sound stream for {name}")
            audio, rate = soundfile.read(BytesIO(next(iter(samples.values()))), dtype="float32", always_2d=True)
            stream = BytesIO()
            soundfile.write(stream, audio, rate, format="WAV", subtype="PCM_16")
            source = self.output / "audio" / (name + ".wav")
            target = ROOT + "/Audio/" + source.name
            leaf = {"kind": "audio", "samples": len(audio), "channels": audio.shape[1],
                    "frequency": rate, "length": len(audio) / rate}
        else:
            sprite = obj.type.name == "Sprite"
            if obj.type.name not in {"Sprite", "Texture2D"}:
                raise ValueError("Unsupported story leaf " + obj.type.name)
            image = data.image
            restore = {"status": "UNCHANGED"}
            texture = obj
            if sprite:
                # Packed sprites' crop/texture live in the atlas render map, not m_RD.
                rd = data.m_RD
                atlas = data.m_SpriteAtlas.deref_parse_as_object() if data.m_SpriteAtlas else None
                if atlas is None and data.m_AtlasTags:
                    atlas_obj = next((a for a in obj.assets_file.objects.values()
                                      if a.type.name == "SpriteAtlas" and a.peek_name() == data.m_AtlasTags[0]), None)
                    if atlas_obj is not None:
                        atlas = atlas_obj.read()
                if atlas is not None:
                    rd = next(v for k, v in atlas.m_RenderDataMap if k == data.m_RenderDataKey)
                texture = rd.texture.deref()
                sys.path.insert(0, str(REPO / "tools/unity-rebuild"))
                from a13_import_assets import sprite_canvas
                stream = BytesIO()
                image.save(stream, format="PNG")
                stream.seek(0)
                rect = tree["m_Rect"]
                full, restore = sprite_canvas(stream, [round(rect["width"]), round(rect["height"])],
                                              [rd.textureRectOffset.x, rd.textureRectOffset.y])
                if restore["status"] == "OUT_OF_RECT":
                    raise ValueError(f"Invalid story Sprite crop: {name}: {restore}")
                if full is not None:
                    image = full
            stream = BytesIO()
            image.save(stream, format="PNG")
            source = self.output / "images" / (name + ("_sprite" if sprite else "_texture") + ".png")
            target = ROOT + "/Textures/" + source.name
            settings = texture.read_typetree().get("m_TextureSettings", {})
            leaf = {"sprite": sprite, "ppu": tree.get("m_PixelsToUnits", 100),
                    "pivot": [tree.get("m_Pivot", {}).get(k, .5) for k in ("x", "y")],
                    "border": [tree.get("m_Border", {}).get(k, 0) for k in ("x", "y", "z", "w")],
                    "wrap_mode": settings.get("m_WrapU", 1), "size": list(image.size), "crop_restoration": restore}
            if sprite:
                geometry = reviewed_glass_geometry(obj, tree, rd, self.origins[obj.assets_file.name], image.size)
                if geometry is not None:
                    leaf["sprite_geometry"] = geometry
        if target in self.leaves:
            raise ValueError("Ambiguous story leaf name: " + target)
        write(source, stream.getvalue())
        leaf.update(source=source.relative_to(REPO).as_posix(), target=target)
        self.leaves[target], self.assets[key] = leaf, target
        return target

    def material(self, obj):
        tree = obj.read_typetree()
        shader = self.resolve(obj, tree["m_Shader"]).read_typetree()
        textures = {}
        for name, value in tree["m_SavedProperties"]["m_TexEnvs"]:
            texture = self.resolve(obj, value["m_Texture"])
            if texture:
                textures[name] = self.leaf(texture)
        return {"pid": obj.path_id, "name": tree["m_Name"] + "_" + str(obj.path_id),
                "shader": shader.get("m_ParsedForm", {}).get("m_Name") or shader.get("m_Name"),
                "properties": tree["m_SavedProperties"], "textures": textures}

    def controller(self, obj, add):
        tree = obj.read_typetree()
        controller = tree["m_Controller"]
        if len(controller["m_LayerArray"]) != 1 or len(controller["m_StateMachineArray"]) != 1:
            raise ValueError("Unreviewed story controller layer count")
        machine = controller["m_StateMachineArray"][0]["data"]
        if machine["m_AnyStateTransitionConstantArray"] or controller["m_Values"]["data"]["m_ValueArray"]:
            raise ValueError("Unreviewed story animator transitions/parameters")
        names, states = dict(tree["m_TOS"]), []
        for wrapped in machine["m_StateConstantArray"]:
            state = wrapped["data"]
            if state["m_TransitionConstantArray"] or any(state[k] for k in (
                    "m_SpeedParamID", "m_MirrorParamID", "m_CycleOffsetParamID", "m_TimeParamID")):
                raise ValueError("Unreviewed story state transitions/parameter binding")
            blends, clip = state["m_BlendTreeConstantArray"], None
            if blends:
                nodes = blends[0]["data"]["m_NodeArray"]
                if len(blends) != 1 or len(nodes) != 1 or nodes[0]["data"]["m_ChildIndices"]:
                    raise ValueError("Unreviewed story blend tree")
                clip = self.resolve(obj, tree["m_AnimationClips"][nodes[0]["data"]["m_ClipID"]])
                add(clip)
            states.append({"name": names[state["m_NameID"]], "clip_pid": str(clip.path_id) if clip else None,
                           "speed": state["m_Speed"], "loop": state["m_Loop"], "cycle_offset": state["m_CycleOffset"],
                           "mirror": state["m_Mirror"], "ik_on_feet": state["m_IKOnFeet"],
                           "write_default_values": state["m_WriteDefaultValues"]})
        return {"pid": obj.path_id, "name": tree["m_Name"], "default_state": states[machine["m_DefaultState"]]["name"],
                "states": states, "animator_pids": []}

    def job(self, name):
        bundle = f"naninovel-spawn_assets_naninovel_spawn_{name.lower()}.bundle"
        nodes, materials, controllers, references, identities = {}, {}, {}, {}, {}
        def claim(obj):
            key = self.key(obj)
            if obj.path_id in identities and identities[obj.path_id] != key:
                raise ValueError(f"Colliding story path IDs: {obj.path_id}")
            identities[obj.path_id] = key
        def add(obj):
            claim(obj)
            pid = obj.path_id
            if pid in nodes:
                return
            tree = obj.read_typetree()
            if obj.type.name == "AnimationClip":
                floats = {((zlib.crc32(key.encode()) & 0x0fffffff) | 0x80000000)
                          for key in ("_Float", "_Tick", "_Fader", "_Fader2", "_Speed", "_EdgeSize", "_FogAmount", "_transparent")}
                colors = {(zlib.crc32(b"_Color") & 0x0fffffff) | (channel << 28) for channel in range(4, 8)}
                channels = {2526845255, 4215373228, 2334886179, 304273561}
                for binding in tree.get("m_ClipBindingConstant", {}).get("genericBindings", []):
                    kind, attribute = binding["typeID"], binding["attribute"]
                    known = ((kind == 4 and attribute in {1, 3, 4}) or
                             (kind == 212 and attribute in floats | colors | channels) or
                             (kind == 1 and attribute == 2086281974))
                    if not known or binding["isPPtrCurve"]:
                        raise ValueError(f"Unreviewed story animation binding: {kind}:{attribute}")
                # Different source CABs reuse human names; saved prefab local IDs can
                # change, so controllers rebind to this unique, stable imported name.
                tree["m_Name"] += "__" + str(pid)
            nodes[pid] = (obj, tree)
            if obj.type.name == "MonoBehaviour":
                self.script(obj, tree)
            def rewrite(value, field=""):
                if isinstance(value, dict):
                    if "m_FileID" in value and "m_PathID" in value:
                        if field == "m_Script" or not value["m_PathID"]:
                            return
                        target = self.resolve(obj, value)
                        claim(target)
                        kind = target.type.name
                        if kind in GRAPH:
                            add(target)
                        elif kind in {"Sprite", "Texture2D", "AudioClip"}:
                            references[f"{pid}:0:{target.path_id}"] = self.leaf(target)
                        elif kind == "Material":
                            materials[target.path_id] = self.material(target)
                        elif kind == "AnimatorController":
                            if target.path_id not in controllers:
                                controllers[target.path_id] = self.controller(target, add)
                            if obj.type.name != "Animator":
                                raise ValueError("Story controller reference is not on an Animator")
                            controllers[target.path_id]["animator_pids"].append(str(pid))
                        else:
                            raise ValueError(f"Unsupported story reference {field}: {kind}")
                        value["m_FileID"] = 0
                    else:
                        for key, item in value.items():
                            rewrite(item, key)
                elif isinstance(value, list):
                    for item in value:
                        rewrite(item, field)
            rewrite(tree)
        for obj in self.by_bundle[bundle]:
            if obj.type.name in GRAPH:
                add(obj)
        roots = [t for o, t in nodes.values() if o.type.name == "Transform" and t["m_Father"]["m_PathID"] == 0]
        if len(roots) != 1 or nodes[roots[0]["m_GameObject"]["m_PathID"]][1]["m_Name"] != name:
            raise ValueError("Unexpected story prefab root: " + name)
        rows = []
        for pid, (obj, tree) in sorted(nodes.items()):
            path = self.output / "objects" / name / f"{obj.type.name}_{pid}.json"
            write(path, tree)
            row = {"pid": str(pid), "type": obj.type.name, "name": tree.get("m_Name", ""),
                   "dump": path.relative_to(REPO).as_posix(), "bundle": self.origins[obj.assets_file.name]}
            if obj.type.name == "MonoBehaviour":
                row["script"] = self.script(obj, tree)
            rows.append(row)
        target = ROOT + "/Prefabs/" + name + ".prefab"
        self.addresses.append({"path": target, "address": "Naninovel/Spawn/" + name, "labels": ["Naninovel"]})
        return {"adapter": "switch-story-cutin-v1", "asset_path": target, "target": target, "bundle": bundle,
                "objects": rows, "references": references, "story_materials": list(materials.values()),
                "story_controllers": list(controllers.values()), "unresolved_counts": {}, "intra_not_in_plan_by_type": {}}

    def build(self):
        jobs = [self.job(name) for name in self.spec["cutins"]]
        for name in self.spec["stills"]:
            bundle = f"naninovel-backgrounds_assets_naninovel_backgrounds_stills_{name}.bundle"
            sprites = [o for o in self.by_bundle[bundle] if o.type.name == "Sprite"]
            if len(sprites) != 1:
                raise ValueError("Expected one Sprite for new story still: " + name)
            self.addresses.append({"path": self.leaf(sprites[0]), "address": "Naninovel/Backgrounds/Stills/" + name,
                                   "labels": ["Naninovel"]})
        gallery = next(o for o in self.by_bundle["naninovel-ui_assets_all.bundle"] if o.path_id == -4398152851352888326)
        order = gallery.read_typetree()["_order"]
        if not all(re.fullmatch(r"\d+", item) for item in order) or len(set(order)) != len(order):
            raise ValueError("Invalid source StillGrid order")
        voices = [o for o in self.by_bundle["general-voice-commontrial_assets_all.bundle"]
                  if o.type.name == "AudioClip" and o.peek_name() == "CommonTrial_Noah00704"]
        if len(voices) != 1:
            raise ValueError("Missing unambiguous new CommonTrial_Noah00704 voice")
        self.addresses.append({"path": self.leaf(voices[0]), "address": "Naninovel/Voice/CommonTrial_Noah00704",
                               "labels": ["Naninovel", "Voice"]})
        return {"leaves": list(self.leaves.values()), "jobs": jobs, "addresses": self.addresses,
                "gallery": {"target": "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/GalleryUI.prefab", "source_order": order},
                "limitations": ["New procedural shaders are built-in pipeline approximations, not identical Switch shader programs."]}
