"""Optional Switch story visuals through the existing content sync/converters.

The ordinary PC editable resource index is authoritative. This supplement caches
only required dumped bundles and merges visual changes without replacing that
index, dialogue, local edits or user save data.
"""
from __future__ import annotations

import hashlib
import json
from pathlib import Path
import re
import shutil

from switch_title import sha, source_folder, write

REPO = Path(__file__).resolve().parents[2]
CACHE = REPO / "res/.imported-source/switch-story"
OUTPUT = REPO / "res/.manosaba/switch-story"
PC_SCENARIOS = REPO / "res/Assets/#WitchTrials/Scenarios"


def contained(root: Path, relative: str) -> Path:
    if not isinstance(relative, str) or not relative or "\\" in relative:
        raise ValueError("Expected a forward-slash relative story path")
    path = Path(relative)
    if path.is_absolute() or ":" in relative or any(p in {"", ".", ".."} for p in relative.split("/")):
        raise ValueError(f"Unsafe story recipe path: {relative}")
    full = (root / path).resolve()
    if not full.is_relative_to(root.resolve()):
        raise ValueError(f"Story recipe path leaves its root: {relative}")
    return full


def scenario_sources(repo: Path | None = None) -> set[str]:
    """PC ledger records owned by an active, locally generated story plan."""
    repo = Path(repo or REPO)
    pc_root = repo / "res/Assets/#WitchTrials/Scenarios"
    path = repo / "res/.manosaba/switch-story/plan.json"
    if not path.is_file():
        return set()
    plan = json.loads(path.read_text(encoding="utf-8"))
    if plan.get("version") != 1 or not isinstance(plan.get("scenarios"), list):
        raise ValueError("Unsupported Switch story plan; prepare it again with sync")
    result = set()
    for row in plan["scenarios"]:
        source = contained(repo, row["pc_source"])
        if not source.is_relative_to(pc_root.resolve()) or source.suffix != ".json":
            raise ValueError("Story scenario must originate in the PC scenario tree")
        relative = source.relative_to(repo.resolve() / "res").as_posix()
        if relative in result:
            raise ValueError("Duplicate PC scenario in Switch story plan")
        result.add(relative)
    return result


def _valid_bundle(name: str) -> bool:
    return bool(re.fullmatch(r"[A-Za-z0-9_.-]+\.bundle", name))


def _recipe_inputs() -> list[Path]:
    from switch_story_scenario import baseline_inputs
    files = [Path(__file__), Path(__file__).with_name("switch_story_assets.py"),
             Path(__file__).with_name("switch_story_scenario.py"),
             Path(__file__).with_name("switch_title.py"),
             REPO / "tools/unity-rebuild/a13_import_assets.py"]
    editor = REPO / "UnityProject/Assets/Editor"
    files += [editor / name for name in ("SwitchStoryContentBuilder.cs", "SwitchStoryAssets.cs", "SwitchTitleContentBuilder.cs",
              "PrefabBuilder.cs", "JsonToSerializedApplier.cs", "ScriptableObjectConverter.cs")]
    files += baseline_inputs(REPO)
    for folder in ("ManosabaSwitchStory", "ManosabaCutIn"):
        files += sorted(p for p in (REPO / "UnityProject/Assets/Shaders" / folder).glob("*") if p.is_file())
    runtime = REPO / "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views"
    for name in ("SpriteLocalCoords.cs", "ObjectionCutIn.cs", "GosubToObjectionCutIn.cs", "PlayObjectionVoicePair.cs",
                 "SpawnableWithAudio.cs", "SetShaderTimerAsUnscale.cs"):
        files += [runtime / name, runtime / (name + ".meta")]
    audio = REPO / "UnityProject/Packages/com.elringus.naninovel/Runtime/Audio"
    for name in ("AudioManager.cs", "IAudioManager.cs", "VoicePlayback.cs"):
        files += [audio / name, audio / (name + ".meta")]
    # All PC input scripts participate: a local edit outside the previous merge
    # set must be re-evaluated rather than silently ignored on a cache hit.
    files += sorted(PC_SCENARIOS.rglob("*.asset.json"))
    return files


def prepare(source=None, dry_run=False):
    manifest_path = CACHE / "manifest.json"
    if not source and not manifest_path.is_file():
        return {"status": "DISABLED"}
    from switch_story_assets import inspect_source, build_assets
    from switch_story_scenario import export_scenarios, baseline_inputs
    required_scripts = {p.name for p in baseline_inputs(REPO)}

    if source:
        folder = source_folder(source)
        specification = inspect_source(folder)
        names = set(specification["bundles"])
        names.update(required_scripts)
        missing = sorted(name for name in names if not contained(folder, name).is_file())
        if missing:
            raise ValueError("Missing Switch story source bundle(s): " + ", ".join(missing))
        if not names or any(not _valid_bundle(name) for name in names):
            raise ValueError("Invalid or empty Switch story bundle selection")
        files = {name: sha(contained(folder, name)) for name in sorted(names)}
        manifest = {"version": 1, "files": files, "assets": specification}
        if dry_run:
            return {"status": "WOULD_PREPARE", "bundles": len(files)}
        for name, expected in files.items():
            target = contained(CACHE, name)
            if not target.is_file() or sha(target) != expected:
                target.parent.mkdir(parents=True, exist_ok=True)
                shutil.copyfile(contained(folder, name), target)
                if sha(target) != expected:
                    raise ValueError(f"Copied Switch bundle failed verification: {name}")
        write(manifest_path, manifest)
    if dry_run:
        return {"status": "CACHED_RECIPE"}
    manifest = json.loads(manifest_path.read_text(encoding="utf-8"))
    if manifest.get("version") != 1 or not isinstance(manifest.get("files"), dict) or not manifest["files"]:
        raise ValueError("Invalid Switch story source cache manifest")
    if not required_scripts.issubset(manifest["files"]):
        raise ValueError("Switch story cache manifest is missing required scenario bundles; reselect a complete dump")
    for name, expected in manifest["files"].items():
        if not _valid_bundle(name) or not isinstance(expected, str) or not re.fullmatch(r"[0-9a-f]{64}", expected):
            raise ValueError("Invalid Switch story source cache entry")
        if sha(contained(CACHE, name)) != expected:
            raise ValueError(f"Switch story source cache changed: {name}; reselect the verified dump")
    assets = manifest.get("assets", {})
    if not isinstance(assets, dict) or not isinstance(assets.get("bundles"), list) or not assets["bundles"]:
        raise ValueError("Invalid Switch story asset specification")
    if not set(assets["bundles"]).issubset(manifest["files"]):
        raise ValueError("Switch story asset bundles are missing from the verified cache")
    recipes = {p.relative_to(REPO).as_posix(): sha(p) for p in _recipe_inputs()}
    signature = hashlib.sha256(json.dumps({"source": manifest, "recipes": recipes}, sort_keys=True).encode()).hexdigest()
    plan_path = OUTPUT / "plan.json"
    if plan_path.is_file():
        previous = json.loads(plan_path.read_text(encoding="utf-8"))
        generated = previous.get("generated_hashes", {})
        if previous.get("signature") == signature and generated and all(
                contained(REPO, name).is_file() and sha(contained(REPO, name)) == expected
                for name, expected in generated.items()):
            return {"status": "UNCHANGED", "signature": signature}

    scenarios = export_scenarios(REPO, CACHE, OUTPUT)
    plan = build_assets(CACHE, OUTPUT, manifest["assets"])
    plan.update(version=1, signature=signature, scenarios=scenarios)
    voice_calls = [call for row in scenarios for call in row["merge"].get("objection_voice_calls", [])]
    voice_pairs = [pair for row in scenarios for pair in row["merge"].get("objection_voice_pairs", [])]
    plan["objection_voice_summary"] = {
        "single_calls": len(voice_calls), "single_scripts": sum(bool(row["merge"].get("objection_voice_calls")) for row in scenarios),
        "pair_calls": len(voice_pairs), "voice_addresses": sorted({call["address"] for call in voice_calls}
            | {address for pair in voice_pairs for address in pair["addresses"]}),
        "reuses_existing_audio_groups": True,
    }
    addresses = plan.setdefault("addresses", [])
    for row in scenarios:
        script_path = row["pc_source"].removeprefix("res/Assets/#WitchTrials/Scenarios/").removesuffix(".asset.json")
        addresses.append({"path": row["target"], "address": "Naninovel/Scripts/" + script_path,
                          "labels": ["Naninovel", "Scripts-" + script_path.split("/")[0]]})
    generated = [row["source"] for row in plan.get("leaves", [])] + [row["source"] for row in scenarios]
    generated += [obj["dump"] for job in plan.get("jobs", []) for obj in job["objects"]]
    plan["generated_hashes"] = {name: sha(contained(REPO, name)) for name in sorted(set(generated))}
    if not generated:
        raise ValueError("Switch story comparison produced no content to apply")
    write(plan_path, plan)
    return {"status": "PREPARED", "signature": signature, "scenarios": len(scenarios),
            "textures": sum(row.get("kind") != "audio" for row in plan.get("leaves", [])),
            "audio": sum(row.get("kind") == "audio" for row in plan.get("leaves", [])),
            "prefabs": len(plan.get("jobs", []))}
