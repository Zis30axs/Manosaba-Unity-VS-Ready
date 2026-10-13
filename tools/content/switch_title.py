"""Opt-in Switch title recipe for the existing content sync/converters.

Only the three title effects and their dependencies are cached; game installations
and the ordinary PC resource index are never modified by this supplement.
"""
from __future__ import annotations

import hashlib
from io import BytesIO
import json
import os
from pathlib import Path
import re
import shutil
import sys

REPO = Path(__file__).resolve().parents[2]
CACHE = REPO / "res/.imported-source/switch-title"
OUTPUT = REPO / "res/.manosaba/switch-title"
ROOT = "Assets/GameContent/#WitchTrials/SwitchTitle"
PC_SCRIPT = REPO / "res/Assets/#WitchTrials/Scenarios/System/System_Title.asset.json"
SCRIPT_BUNDLE = "naninovel-scripts_assets_naninovelscripts-system.bundle"
NAMES = [SCRIPT_BUNDLE, "general-animations_assets_all.bundle",
         "general-materials_assets_all.bundle", "general-spawncommon-title_assets_all.bundle"]
NAMES += [f"naninovel-spawn_assets_naninovel_spawn_titlescreeneffect_{i}.bundle" for i in (1, 2, 3)]
NAMES += [f"naninovel-backgrounds_assets_naninovel_backgrounds_stills_{i}_1.bundle" for i in (1, 2, 3)]
GRAPH = {"GameObject", "Transform", "RectTransform", "SpriteRenderer", "SpriteMask", "Animator",
         "ParticleSystem", "ParticleSystemRenderer", "MonoBehaviour", "AnimationClip", "SortingGroup"}


def sha(path):
    with Path(path).open("rb") as stream:
        return hashlib.file_digest(stream, "sha256").hexdigest()


def write(path, data):
    path = Path(path)
    payload = data if isinstance(data, bytes) else (json.dumps(data, ensure_ascii=False, indent=2,
        default=lambda value: list(value) if isinstance(value, bytes) else str(value)) + "\n").encode("utf-8")
    if path.is_file() and path.read_bytes() == payload:
        return
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_suffix(path.suffix + ".tmp")
    temporary.write_bytes(payload)
    os.replace(temporary, path)


def source_folder(source):
    path = Path(source).resolve()
    for candidate in (path, path / "StreamingAssets/aa/Switch", path / "Data/StreamingAssets/aa/Switch"):
        if (candidate / SCRIPT_BUNDLE).is_file():
            return candidate
    raise ValueError("Switch title source must contain the dumped Data/StreamingAssets/aa/Switch bundles")


def prepare(source=None, dry_run=False):
    manifest_path = CACHE / "manifest.json"
    if source:
        folder = source_folder(source)
        files = [folder / name for name in NAMES]
        for suffix in ("monoscripts", "unitybuiltinassets"):
            matches = list(folder.glob("*_" + suffix + ".bundle"))
            if len(matches) != 1:
                raise ValueError(f"Expected one Switch {suffix} bundle")
            files += matches
        manifest = {"version": 1, "files": {p.name: sha(p) for p in files}}
        if dry_run:
            return {"status": "WOULD_PREPARE", "bundles": len(files)}
        for path in files:
            target = CACHE / path.name
            if not target.is_file() or sha(target) != manifest["files"][path.name]:
                target.parent.mkdir(parents=True, exist_ok=True)
                shutil.copyfile(path, target)
        write(manifest_path, manifest)
    elif not manifest_path.is_file():
        return {"status": "DISABLED"}
    if dry_run:
        return {"status": "CACHED_RECIPE"}
    manifest = json.loads(manifest_path.read_text(encoding="utf-8"))
    for name, expected in manifest["files"].items():
        if Path(name).name != name or not name.endswith(".bundle"):
            raise ValueError("Invalid Switch source cache filename")
        if sha(CACHE / name) != expected:
            raise ValueError(f"Switch source cache changed: {name}; reselect the verified dump")
    recipes = [Path(__file__), Path(__file__).with_name("switch_title_scenario.py"),
        REPO / "tools/unity-rebuild/a13_import_assets.py"]
    recipes += [REPO / "UnityProject/Assets/Editor" / name for name in (
        "PrefabBuilder.cs", "JsonToSerializedApplier.cs", "ScriptableObjectConverter.cs",
        "SwitchTitleContentBuilder.cs", "SwitchTitleAssets.cs")]
    recipes += list((REPO / "UnityProject/Assets/Shaders/ManosabaSwitchTitle").glob("*"))
    recipes += [REPO / "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/TitleScreenEffect.cs", PC_SCRIPT]
    signature = hashlib.sha256(json.dumps({"bundles": manifest["files"],
        "recipes": {p.relative_to(REPO).as_posix(): sha(p) for p in recipes if p.is_file()}}, sort_keys=True).encode()).hexdigest()
    plan_path = OUTPUT / "plan.json"
    if plan_path.is_file():
        previous = json.loads(plan_path.read_text(encoding="utf-8"))
        generated = previous.get("generated_hashes", {})
        if previous.get("signature") == signature and generated and all(
            (REPO / p).is_file() and sha(REPO / p) == digest for p, digest in generated.items()):
            return {"status": "UNCHANGED", "signature": signature}
    plan = Planner(manifest).build()
    plan["signature"] = signature
    generated = [row["source"] for row in plan["leaves"]] + [plan["scenario"]["source"]]
    generated += [obj["dump"] for job in plan["jobs"] for obj in job["objects"]]
    plan["generated_hashes"] = {p: sha(REPO / p) for p in sorted(set(generated))}
    write(plan_path, plan)
    return {"status": "PREPARED", "signature": signature, "prefabs": len(plan["jobs"]), "textures": len(plan["leaves"])}


class Planner:
    def __init__(self, manifest):
        import UnityPy
        self.env = UnityPy.Environment()
        self.objects, self.by_bundle, self.origins = {}, {}, {}
        for name in manifest["files"]:
            loaded = self.env.load_file(str(CACHE / name))
            objects = []
            for file in loaded.files.values():
                if not hasattr(file, "objects"):
                    continue
                self.origins[file.name] = name
                for obj in file.objects.values():
                    self.objects[(file.name, obj.path_id)] = obj
                    objects.append(obj)
            self.by_bundle[name] = objects
        self.leaves, self.assets, self.addresses = {}, {}, []

    def resolve(self, owner, pointer):
        pid = pointer["m_PathID"]
        if not pid:
            return None
        fid = pointer["m_FileID"]
        cab = owner.assets_file.name if fid == 0 else owner.assets_file.externals[fid - 1].path.rsplit("/", 1)[-1]
        try:
            return self.objects[(cab, pid)]
        except KeyError as error:
            raise ValueError(f"Uncached title dependency: {cab}:{pid} from {owner.path_id}") from error

    def leaf(self, obj):
        key = (obj.assets_file.name, obj.path_id)
        if key in self.assets:
            return self.assets[key]
        tree, data = obj.read_typetree(), obj.read()
        sprite = obj.type.name == "Sprite"
        name = re.sub(r"[^A-Za-z0-9_.-]", "_", tree["m_Name"])
        if not re.fullmatch(r"Still_00[123]_001", name):
            name += ("_sprite_" if sprite else "_texture_") + str(obj.path_id)
        elif not sprite:
            name += "_texture"
        image = data.image
        restore = {"status": "UNCHANGED"}
        if sprite:
            sys.path.insert(0, str(REPO / "tools/unity-rebuild"))
            from a13_import_assets import sprite_canvas
            rect, offset = tree["m_Rect"], tree["m_RD"]["textureRectOffset"]
            stream = BytesIO()
            image.save(stream, format="PNG")
            stream.seek(0)
            full, restore = sprite_canvas(stream, [round(rect["width"]), round(rect["height"])], [offset["x"], offset["y"]])
            if restore["status"] == "OUT_OF_RECT":
                raise ValueError(f"Title sprite crop invalid: {name}: {restore}")
            if full is not None:
                image = full
        output = OUTPUT / "images" / (name + ".png")
        stream = BytesIO()
        image.save(stream, format="PNG")
        write(output, stream.getvalue())
        target = ROOT + "/Textures/" + output.name
        texture = self.resolve(obj, tree["m_RD"]["texture"]) if sprite else obj
        texture_tree = texture.read_typetree()
        leaf = {"source": output.relative_to(REPO).as_posix(), "target": target,
                "sprite": sprite, "ppu": tree.get("m_PixelsToUnits", 100),
                "pivot": [tree.get("m_Pivot", {}).get(k, .5) for k in ("x", "y")],
                "border": [tree.get("m_Border", {}).get(k, 0) for k in ("x", "y", "z", "w")],
                "wrap_mode": texture_tree.get("m_TextureSettings", {}).get("m_WrapU", 1),
                "size": list(image.size), "crop_restoration": restore}
        self.leaves[target], self.assets[key] = leaf, target
        return target

    def material(self, obj):
        tree = obj.read_typetree()
        shader = self.resolve(obj, tree["m_Shader"])
        shader_tree = shader.read_typetree()
        shader_name = shader_tree.get("m_ParsedForm", {}).get("m_Name") or shader_tree.get("m_Name")
        textures = {}
        for name, value in tree["m_SavedProperties"]["m_TexEnvs"]:
            texture = self.resolve(obj, value["m_Texture"])
            if texture:
                textures[name] = self.leaf(texture)
        return {"pid": obj.path_id, "name": tree["m_Name"] + "_" + str(obj.path_id),
                "shader": shader_name, "properties": tree["m_SavedProperties"], "textures": textures}

    def controller(self, obj, animator, add):
        tree = obj.read_typetree()
        controller = tree["m_Controller"]
        if len(controller["m_LayerArray"]) != 1 or len(controller["m_StateMachineArray"]) != 1:
            raise ValueError("Title controller is no longer single-layer")
        machine = controller["m_StateMachineArray"][0]["data"]
        if machine["m_AnyStateTransitionConstantArray"] or controller["m_Values"]["data"]["m_ValueArray"]:
            raise ValueError("Unexpected title animator transitions/parameters")
        names = dict(tree["m_TOS"])
        states = []
        for wrapped in machine["m_StateConstantArray"]:
            state = wrapped["data"]
            if state["m_TransitionConstantArray"] or len(state["m_BlendTreeConstantArray"]) != 1:
                raise ValueError("Unsupported title animation state")
            nodes = state["m_BlendTreeConstantArray"][0]["data"]["m_NodeArray"]
            if len(nodes) != 1 or nodes[0]["data"]["m_ChildIndices"]:
                raise ValueError("Unsupported title blend tree")
            clip = self.resolve(obj, tree["m_AnimationClips"][nodes[0]["data"]["m_ClipID"]])
            add(clip)
            states.append({"name": names[state["m_NameID"]], "clip_pid": str(clip.path_id),
                           "speed": state["m_Speed"], "loop": state["m_Loop"]})
        return {"pid": obj.path_id, "name": tree["m_Name"], "animator_pid": str(animator.path_id),
                "default_state": states[machine["m_DefaultState"]]["name"], "states": states}

    def job(self, number):
        bundle = f"naninovel-spawn_assets_naninovel_spawn_titlescreeneffect_{number}.bundle"
        nodes, materials, references = {}, {}, {}
        title_controller = None
        def add(obj):
            nonlocal title_controller
            pid = obj.path_id
            if pid in nodes:
                if nodes[pid][0].assets_file.name != obj.assets_file.name:
                    raise ValueError("Colliding title path IDs")
                return
            tree = obj.read_typetree()
            nodes[pid] = (obj, tree)
            def rewrite(value, field=""):
                nonlocal title_controller
                if isinstance(value, dict):
                    if "m_FileID" in value and "m_PathID" in value:
                        if field == "m_Script" or not value["m_PathID"]:
                            return
                        target = self.resolve(obj, value)
                        kind = target.type.name
                        if kind in GRAPH:
                            add(target)
                        elif kind in ("Sprite", "Texture2D"):
                            references[f"{pid}:0:{target.path_id}"] = self.leaf(target)
                        elif kind == "Material":
                            materials[target.path_id] = self.material(target)
                        elif kind == "AnimatorController":
                            title_controller = self.controller(target, obj, add)
                        else:
                            raise ValueError(f"Unsupported title reference {field}: {kind}")
                        value["m_FileID"] = 0
                    else:
                        for k, item in value.items():
                            rewrite(item, k)
                elif isinstance(value, list):
                    for item in value:
                        rewrite(item, field)
            rewrite(tree)
        for obj in self.by_bundle[bundle]:
            if obj.type.name in GRAPH:
                add(obj)
        objects = []
        for pid, (obj, tree) in sorted(nodes.items()):
            output = OUTPUT / "objects" / f"title{number}_{pid}.json"
            write(output, tree)
            row = {"pid": str(pid), "type": obj.type.name, "name": tree.get("m_Name", ""),
                   "dump": output.relative_to(REPO).as_posix(), "bundle": self.origins[obj.assets_file.name]}
            if obj.type.name == "MonoBehaviour":
                script = self.resolve(obj, tree["m_Script"]).read_typetree()
                if script["m_ClassName"] != "TitleScreenEffect" or script["m_Namespace"] != "WitchTrials.Views":
                    raise ValueError("Unexpected title MonoBehaviour")
                row["script"] = {"class": "TitleScreenEffect", "namespace": "WitchTrials.Views",
                                 "assembly": "WitchTrials.Game", "our_guid": "a83a6487ce8b4df197c92779aab2fd51"}
            objects.append(row)
        target = ROOT + f"/Prefabs/TitleScreenEffect_{number}.prefab"
        return {"asset_path": target, "target": target, "bundle": bundle, "objects": objects,
                "references": references, "title_materials": list(materials.values()),
                "title_controller": title_controller, "unresolved_counts": {}, "intra_not_in_plan_by_type": {}}

    def build(self):
        from switch_title_scenario import export_scenario
        jobs = [self.job(i) for i in (1, 2, 3)]
        for i in (1, 2, 3):
            bundle = f"naninovel-backgrounds_assets_naninovel_backgrounds_stills_{i}_1.bundle"
            sprites = [o for o in self.by_bundle[bundle] if o.type.name == "Sprite"]
            if len(sprites) != 1:
                raise ValueError("Expected a single still Sprite")
            target = self.leaf(sprites[0])
            # Existing PC appearances 1/2 keep their addresses; new copies also
            # serve the title-only fog mesh and deterministic visual probe.
            if i == 3:
                self.addresses.append({"path": target, "address": "Naninovel/Backgrounds/Stills/3_1", "labels": ["Naninovel"]})
            # Runtime providers enumerate the Naninovel label before resolving a path;
            # an address alone is invisible to both the base and extended provider.
            self.addresses.append({"path": jobs[i-1]["target"], "address": f"Naninovel/Spawn/TitleScreenEffect_{i}", "labels": ["Naninovel"]})
        scenario = OUTPUT / "System_Title.asset.json"
        report = export_scenario(CACHE / SCRIPT_BUNDLE, PC_SCRIPT, scenario)
        self.addresses.append({"path": "Assets/GameContent/#WitchTrials/Scenarios/System/System_Title.asset",
            "address": "Naninovel/Scripts/System/System_Title", "labels": ["Naninovel", "Scripts-System"]})
        return {"version": 1, "leaves": list(self.leaves.values()), "jobs": jobs,
                "scenario": {"source": scenario.relative_to(REPO).as_posix(),
                    "target": "Assets/GameContent/#WitchTrials/Scenarios/System/System_Title.asset",
                    "type": "Naninovel.Script", "assembly": "Elringus.Naninovel.Runtime"},
                "scenario_merge": report, "addresses": self.addresses,
                "limitations": ["Fog/Noise shader programs are PC reconstructions; source images, curves and particle parameters are retained."]}
