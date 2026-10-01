"""Exercise real editable content, then restore every source and imported file.

Use the project's Python environment (UnityPy + Pillow). ``prepare`` saves a
recoverable journal before adding a visible text/data marker and a magenta image
square. Run content sync/build, ``verify``, and the optional Unity Play Mode probe.
``restore`` restores the exact pre-test bytes, including existing .meta files.
The ``run`` command performs those steps and rebuilds restored bundle content
even when verification fails. MasterData-only runs reimport restored Resources
without building Addressables. No installed game or shipped executable is read.
"""
from __future__ import annotations

import argparse
import csv
import datetime as dt
import hashlib
from io import BytesIO
import importlib.util
import json
import math
import os
from pathlib import Path
import re
import subprocess
import sys
import uuid

ROOT = Path(__file__).resolve().parents[2]
DEFAULT_TEXT = "Assets/#WitchTrials/Localization/zh-Hans/Text/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt"
DEFAULT_IMAGE = "Assets/#WitchTrials/Prefabs/Naninovel/Characters/LayeredCharacters/Alisa.sprite~67.png"
DEFAULT_DATA = "Assets/#WitchTrials/Data/ScriptableObjects/CharacterData.asset.json"
DEFAULT_SCENARIO = "Assets/#WitchTrials/Scenarios/Act01_Chapter01/Act01_Chapter01_Adv01.asset.json"
DEFAULT_MASTER_DATA = "Assets/#WitchTrials/Data/MasterData.asset.json"
MASTER_DATA_TARGET = "Assets/NaninovelData/Resources/MasterData.asset"
DATA_FIELD = ["_items", 0, "_name", 1, "_text"]
SCENARIO_FIELD = ["textMap", "idToText", "values", 0]


def checked(root: Path, relative: str) -> Path:
    """Journal paths stay inside the workspace, including symlink resolution."""
    root = root.resolve()
    result = (root / relative).resolve()
    if not result.is_relative_to(root):
        raise ValueError(f"path escapes workspace: {relative}")
    return result


def sha(data: bytes) -> str:
    return hashlib.sha256(data).hexdigest()


def now() -> str:
    return dt.datetime.now(dt.timezone.utc).isoformat()


def resource_evidence(root, name):
    candidates = (root / "res/.manosaba/reconstruction/unity" / name,
                  root / "res/.manosaba/reconstruction" / name,
                  root / "tools/unity-rebuild/out/unity" / name)
    return next((path for path in candidates if path.is_file()), None)


def image_expectation(root, target, source, pixels, leaf, is_sprite):
    width, height = pixels.size
    x, y = 64, 64
    imported = source.read_bytes()
    if is_sprite:
        plan = resource_evidence(root, "sprite_settings_plan.json")
        if plan:
            for asset, settings in json.loads(plan.read_text(encoding="utf-8-sig")).get("sprites", {}).items():
                asset = asset.replace("\\", "/")
                if "/UnityProject/Assets/" in asset:
                    asset = "Assets/" + asset.split("/UnityProject/Assets/", 1)[1]
                asset = asset.removeprefix("UnityProject/")
                if asset != target:
                    continue
                payload = leaf.leaf_payload(source, settings)
                if payload is not None:
                    from PIL import Image
                    imported = payload
                    with Image.open(BytesIO(payload)) as normalized:
                        width, height = normalized.size
                        offset = settings.get("texture_rect_offset", settings.get("trimmed", {}).get("texture_rect_offset"))
                        x += round(offset[0])
                        y += height - round(offset[1]) - pixels.height
                        if not (0 <= x < width and 0 <= y < height) or normalized.getpixel((x, y)) != (255, 0, 255, 255):
                            raise ValueError("normalized image probe marker is outside its Sprite rectangle")
                break
    return {"width": width, "height": height, "sample_x": x, "sample_y": y,
            "expected_import_sha256": sha(imported)}


def write_json(path: Path, data: dict) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = path.with_suffix(path.suffix + ".tmp")
    temporary.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    temporary.replace(path)


def dependencies(root: Path, *, bundles: bool = False):
    # The bundled desktop Python can use the already installed reconstruction
    # packages when the local venv executable cannot start inside its sandbox.
    site = root / "tools/venv/Lib/site-packages"
    if site.is_dir() and str(site) not in sys.path:
        sys.path.append(str(site))
    from PIL import Image, ImageDraw
    if bundles:
        import UnityPy
        return Image, ImageDraw, UnityPy
    return Image, ImageDraw


def importer(root: Path):
    spec = importlib.util.spec_from_file_location("a13_roundtrip", root / "tools/unity-rebuild/a13_import_assets.py")
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    return module


def get_field(tree, path):
    for item in path:
        tree = tree[item]
    return tree


def set_field(tree, path, value):
    parent = get_field(tree, path[:-1])
    parent[path[-1]] = value


def canonical_text(data: bytes) -> bytes:
    return data.decode("utf-8-sig").replace("\r\n", "\n").encode("utf-8")


def load_journal(root: Path, journal: Path) -> dict:
    journal = checked(root, str(journal))
    state = json.loads(journal.read_text(encoding="utf-8"))
    if state.get("version") != 1:
        raise ValueError("unsupported round-trip journal")
    for item in state["backups"]:
        checked(root, item["path"])
        checked(journal.parent, item["backup"])
    return state


def prepare(root: Path, directory: Path, text=DEFAULT_TEXT, image=DEFAULT_IMAGE, data=DEFAULT_DATA,
            scenario=DEFAULT_SCENARIO, master_data=None, master_data_only=False) -> dict:
    root = root.resolve()
    Image, ImageDraw = dependencies(root) if not master_data_only else (None, None)
    directory = checked(root, str(directory))
    journal = directory / "journal.json"
    if journal.exists():
        raise ValueError("a round-trip journal already exists; restore it before preparing another test")
    leaf = importer(root)
    with (root / "res/_index.csv").open(encoding="utf-8-sig", newline="") as stream:
        rows = list(csv.DictReader(stream))
    selected = []
    sources = [] if master_data_only else [("TEXT", text), ("IMAGE", image), ("DATA", data), ("SCENARIO", scenario)]
    if master_data or master_data_only:
        sources.append(("MASTER_DATA", master_data or DEFAULT_MASTER_DATA))
    for category, source in sources:
        source = source.replace("\\", "/").removeprefix("res/")
        if category == "MASTER_DATA":
            # MasterData is exported from the existing resource decode/cache,
            # rather than from an Addressables index entry.
            row = {"type": "MonoBehaviour"}
            target = root / "UnityProject" / MASTER_DATA_TARGET
        else:
            matches = [r for r in rows if r["output_file"].replace("\\", "/") == source]
            if len(matches) != 1:
                raise ValueError(f"expected one index row for {source}; found {len(matches)}")
            row = matches[0]
        if category == "IMAGE":
            if row["type"] not in {"Sprite", "Texture2D"}:
                raise ValueError("image probe must select an indexed Sprite or Texture2D")
            target = leaf.dest_for_sprite(row["asset_path"], source, row["name"]) if row["type"] == "Sprite" else leaf.dest_for(row["asset_path"], source)
        elif category in {"DATA", "SCENARIO"}:
            target = leaf.dest_for(row["asset_path"], source[:-5])
        elif category == "TEXT":
            target = leaf.dest_for(row["asset_path"], source)
        target = checked(root, target)
        target_rel = target.relative_to(root / "UnityProject").as_posix()
        if category == "IMAGE":
            ledger = resource_evidence(root, "a13_import_ledger.csv")
            if ledger:
                with ledger.open(encoding="utf-8-sig", newline="") as stream:
                    if any(item.get("status") == "DUPLICATE_DESTINATION" and
                           item.get("output_file", "").replace("\\", "/").removeprefix("res/") == source
                           for item in csv.DictReader(stream)):
                        raise ValueError("image probe cannot edit a READ_ONLY duplicate Sprite source")
            plan = resource_evidence(root, "addressables_plan_all.json")
            if plan and not any(item["asset_path"].replace("\\", "/") == target_rel
                                for item in json.loads(plan.read_text(encoding="utf-8-sig")).get("entries", [])):
                raise ValueError("image probe needs its own planned Addressables entry")
        meta = target.with_suffix(target.suffix + ".meta")
        guid_match = re.search(r"^guid: ([a-f0-9]{32})$", meta.read_text(encoding="utf-8"), re.MULTILINE)
        if not guid_match:
            raise ValueError(f"missing existing Unity GUID: {target_rel}")
        selected.append({"category": category, "source": "res/" + source, "target": target_rel,
                         "guid": guid_match[1], "asset_type": row["type"]})
        if category == "MASTER_DATA":
            selected[-1].update({"delivery": "Resources", "resources_key": "MasterData", "runtime_property": "DefaultHp"})

    token = "MANOSABA_RT_" + uuid.uuid4().hex[:12]
    state = {"version": 1, "status": "PREPARED", "generated": now(), "token": token,
             "cases": selected, "backups": []}
    for case in selected:
        for path in (checked(root, case["source"]), checked(root / "UnityProject", case["target"]),
                     checked(root / "UnityProject", case["target"] + ".meta")):
            contents = path.read_bytes()
            name = f"backup-{len(state['backups']):02d}.bin"
            directory.mkdir(parents=True, exist_ok=True)
            (directory / name).write_bytes(contents)
            state["backups"].append({"path": path.relative_to(root).as_posix(), "backup": name,
                                     "sha256": sha(contents)})
    # Write recovery evidence before the first mutation, then update after each
    # change. A process crash still leaves all original bytes available.
    write_json(journal, state)
    try:
        for case in selected:
            source = checked(root, case["source"])
            if case["category"] == "TEXT":
                original = source.read_bytes()
                text_value = original.decode("utf-8-sig")
                lines = text_value.splitlines(keepends=True)
                line = next(i for i, line in enumerate(lines) if line.strip() and not line.startswith((";", "#")))
                newline = "\r\n" if lines[line].endswith("\r\n") else "\n"
                lines[line] = lines[line].rstrip("\r\n") + " [" + token + "]" + newline
                source.write_bytes((b"\xef\xbb\xbf" if original.startswith(b"\xef\xbb\xbf") else b"") + "".join(lines).encode("utf-8"))
                case["expected_sha256"] = sha(canonical_text(source.read_bytes()))
            elif case["category"] == "IMAGE":
                with Image.open(source) as opened:
                    pixels = opened.convert("RGBA")
                if min(pixels.size) < 128:
                    raise ValueError("image probe needs an image of at least 128 by 128 pixels")
                ImageDraw.Draw(pixels).rectangle((16, 16, 111, 111), fill=(255, 0, 255, 255))
                pixels.save(source)
                case.update({**image_expectation(root, case["target"], source, pixels, leaf, case["asset_type"] == "Sprite"),
                             "expected_rgba": [255, 0, 255, 255],
                             "color_tolerance": 20})
            else:
                tree = json.loads(source.read_text(encoding="utf-8-sig"))
                field = ["_defaultHp"] if case["category"] == "MASTER_DATA" else DATA_FIELD if case["category"] == "DATA" else SCENARIO_FIELD
                original = get_field(tree, field)
                if case["category"] == "MASTER_DATA":
                    if isinstance(original, bool) or not isinstance(original, (int, float)) or not math.isfinite(original):
                        raise ValueError("MasterData _defaultHp must be a finite number")
                    expected = float(original) + 1.0
                else:
                    if not isinstance(original, str):
                        raise ValueError("data probe expects a primitive string field")
                    expected = original + " [" + token + "]"
                set_field(tree, field, expected)
                write_json(source, tree)
                serialized = "_defaultHp" if case["category"] == "MASTER_DATA" else "_items.Array.data[0]._name.Array.data[1]._text" if case["category"] == "DATA" else "textMap.idToText.values.Array.data[0]"
                case.update({"field": field, "serialized_field": serialized,
                             "expected": expected})
                if case["category"] == "SCENARIO":
                    case["text_id"] = tree["textMap"]["idToText"]["keys"][0]
            case["source_sha256"] = sha(source.read_bytes())
            write_json(journal, state)
    except BaseException:
        restore(root, journal)
        raise
    return {"status": "PREPARED", "journal": str(journal), "token": token, "cases": selected}


def restore(root: Path, journal: Path) -> dict:
    state = load_journal(root, journal)
    for item in state["backups"]:
        backup = checked(journal.parent, item["backup"])
        contents = backup.read_bytes()
        if sha(contents) != item["sha256"]:
            raise ValueError(f"backup hash mismatch: {backup}")
    for item in state["backups"]:
        target = checked(root, item["path"])
        target.write_bytes(checked(journal.parent, item["backup"]).read_bytes())
    state["status"] = "RESTORED"
    state["restored"] = now()
    write_json(journal, state)
    return {"status": "RESTORED", "files": len(state["backups"]), "journal": str(journal)}


def sample_matches(actual, expected, tolerance):
    return all(abs(int(a) - int(b)) <= tolerance for a, b in zip(actual, expected))


def verify(root: Path, journal: Path, build_root: Path | None = None) -> dict:
    state = load_journal(root, journal)
    if state["status"] == "RESTORED":
        raise ValueError("test sources are restored; prepare a new round-trip test")
    build_root = checked(root, str(build_root or root / "UnityProject/Library/com.unity.addressables/aa/Windows"))
    if any(c.get("delivery") != "Resources" for c in state["cases"]):
        _, _, UnityPy = dependencies(root, bundles=True)
        plan = json.loads((root / "tools/unity-rebuild/out/unity/addressables_plan_all.json").read_text(encoding="utf-8"))
        entries = {e["asset_path"].lower(): e for e in plan["entries"]}
    else:
        UnityPy, entries = None, {}
    results = []
    for case in state["cases"]:
        target = checked(root / "UnityProject", case["target"])
        source = checked(root, case["source"])
        if sha(source.read_bytes()) != case["source_sha256"]:
            raise AssertionError(f"test source changed after preparation: {case['source']}")
        saved_meta = next(b for b in state["backups"] if b["path"] == "UnityProject/" + case["target"] + ".meta")
        if sha(target.with_suffix(target.suffix + ".meta").read_bytes()) != saved_meta["sha256"]:
            raise AssertionError(f"Unity importer settings/GUID changed: {case['target']}")
        if case["category"] in {"TEXT", "IMAGE"} and sha(target.read_bytes()) != case.get("expected_import_sha256", case["source_sha256"]):
            raise AssertionError(f"imported source is stale: {case['target']}")
        if case.get("delivery") == "Resources":
            results.append({"category": case["category"], "target": case["target"], "guid": case["guid"],
                            "delivery": "Resources", "resources_key": case["resources_key"], "status": "REQUIRES_RUNTIME"})
            continue
        entry = entries.get(case["target"].lower())
        if entry is None:
            raise AssertionError(f"no planned Addressables entry: {case['target']}")
        pattern = entry["group"].lower() + "_assets_all_*.bundle"
        bundles = list(build_root.rglob(pattern))
        if len(bundles) != 1:
            raise AssertionError(f"expected one current bundle for {pattern}; found {len(bundles)}")
        bundle = UnityPy.load(str(bundles[0]))
        matches = [obj for path, obj in bundle.container.items()
                   if path.lower() == case["target"].lower() and obj.type.name == case["asset_type"]]
        if len(matches) != 1:
            raise AssertionError(f"built asset missing/ambiguous: {case['target']} ({len(matches)})")
        obj = matches[0]
        result = {"category": case["category"], "target": case["target"], "bundle": str(bundles[0]),
                  "address": entry["address"], "guid": case["guid"], "type": obj.type.name}
        if case["category"] == "TEXT":
            actual = obj.read().m_Script
            actual = actual if isinstance(actual, bytes) else actual.encode("utf-8")
            result["sha256"] = sha(canonical_text(actual))
            if result["sha256"] != case["expected_sha256"]:
                raise AssertionError("built text bytes do not match the edited text")
        elif case["category"] == "IMAGE":
            pixels = obj.read().image.convert("RGBA")
            result["size"] = list(pixels.size)
            # Importer max size can intentionally downscale. Sample by source
            # normalized coordinates; never change the user's import settings.
            x = int(case["sample_x"] * pixels.width / case["width"])
            y = int(case["sample_y"] * pixels.height / case["height"])
            result["pixel"] = list(pixels.getpixel((x, y)))
            if not sample_matches(result["pixel"], case["expected_rgba"], case["color_tolerance"]):
                raise AssertionError(f"built image marker missing: {result['pixel']}")
        else:
            result["field"] = case["field"]
            result["actual"] = get_field(obj.read_typetree(), case["field"])
            if result["actual"] != case["expected"]:
                raise AssertionError("built data field does not match the edited JSON")
        result["status"] = "PASS"
        results.append(result)
    report = {"status": "PENDING_RUNTIME" if any(c["status"] == "REQUIRES_RUNTIME" for c in results) else "PASS",
              "generated": now(), "token": state["token"], "cases": results}
    write_json(journal.parent / "built_result.json", report)
    return report


def execute(root: Path, command: list[str], env: dict | None = None) -> None:
    subprocess.run(command, cwd=root, env=env, check=True)


def run(root: Path, directory: Path, unity: str, text, image, data, scenario=DEFAULT_SCENARIO,
        master_data=None, master_data_only=False, *, runtime: bool = True) -> dict:
    prepared = prepare(root, directory, text, image, data, scenario, master_data, master_data_only)
    journal = Path(prepared["journal"])
    env = os.environ.copy()
    env.pop("MANOSABA_GAME_ROOT", None)
    env["MANOSABA_GAME_ROOT"] = str(root / "__disconnected_original_game__")
    env["MANOSABA_PYTHON"] = sys.executable
    env["MANOSABA_UNITY_EDITOR"] = unity
    cli = [sys.executable, str(root / "tools/content/content.py")]
    arguments = [arg for c in prepared["cases"] for arg in ("--file", c["source"])]
    needs_bundles = any(c.get("delivery") != "Resources" for c in prepared["cases"])
    result = None
    try:
        execute(root, cli + ["sync"] + arguments, env)
        if needs_bundles:
            execute(root, cli + ["build"] + arguments, env)
        result = verify(root, journal)
        if runtime:
            env["MANOSABA_ROUNDTRIP_PLAN"] = str(journal)
            log = journal.parent / "runtime.log"
            execute(root, [unity, "-batchmode", "-projectPath", str(root / "UnityProject"),
                           "-executeMethod", "Manosaba.EditorTools.ContentRoundTripProbe.Run", "-logFile", str(log)], env)
            result["runtime"] = json.loads((journal.parent / "runtime_result.json").read_text(encoding="utf-8"))
            if result["runtime"]["status"] != "PASS":
                raise AssertionError("Play Mode did not verify the edited built content")
            result["status"] = "PASS"
        return result
    finally:
        restore(root, journal)
        # Restoration imports existing YAML/PNG/TXT, preserving pre-test generated
        # differences instead of reconverting them from a possibly different res.
        env["MANOSABA_ROUNDTRIP_PLAN"] = str(journal)
        execute(root, [unity, "-batchmode", "-nographics", "-quit", "-projectPath", str(root / "UnityProject"),
                       "-executeMethod", "Manosaba.EditorTools.ContentRoundTripProbe.RestoreImports",
                       "-logFile", str(journal.parent / "restore.log")], env)
        if needs_bundles:
            execute(root, [unity, "-batchmode", "-nographics", "-quit", "-projectPath", str(root / "UnityProject"),
                           "-executeMethod", "Manosaba.EditorTools.AddressablesBuild.Run",
                           "-logFile", str(journal.parent / "restore_build.log")], env)


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("command", choices=["prepare", "verify", "restore", "run"])
    parser.add_argument("--directory", type=Path, default=ROOT / "tools/unity-rebuild/out/content-roundtrip")
    parser.add_argument("--text", default=DEFAULT_TEXT)
    parser.add_argument("--image", default=DEFAULT_IMAGE)
    parser.add_argument("--data", default=DEFAULT_DATA)
    parser.add_argument("--scenario", default=DEFAULT_SCENARIO)
    parser.add_argument("--master-data", nargs="?", const=DEFAULT_MASTER_DATA,
                        help="also probe editable MasterData via Resources.Load (optional source path)")
    parser.add_argument("--master-data-only", action="store_true", help="probe only MasterData; no Addressables rebuild")
    parser.add_argument("--unity", default=os.environ.get("MANOSABA_UNITY_EDITOR", ""))
    parser.add_argument("--no-runtime", action="store_true")
    args = parser.parse_args()
    directory = checked(ROOT, str(args.directory))
    journal = directory / "journal.json"
    try:
        if args.command == "prepare":
            result = prepare(ROOT, directory, args.text, args.image, args.data, args.scenario, args.master_data, args.master_data_only)
        elif args.command == "verify":
            result = verify(ROOT, journal)
        elif args.command == "restore":
            result = restore(ROOT, journal)
        else:
            if not args.unity or not Path(args.unity).is_file():
                raise ValueError("run requires --unity or MANOSABA_UNITY_EDITOR")
            result = run(ROOT, directory, args.unity, args.text, args.image, args.data, args.scenario,
                         args.master_data, args.master_data_only, runtime=not args.no_runtime)
        print(json.dumps(result, ensure_ascii=True, indent=2))
        return 0
    except (AssertionError, OSError, ValueError, ImportError, subprocess.CalledProcessError) as exc:
        print(json.dumps({"status": "FAIL", "error": str(exc)}, ensure_ascii=True), file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
