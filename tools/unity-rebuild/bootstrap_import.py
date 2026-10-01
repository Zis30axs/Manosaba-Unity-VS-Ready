#!/usr/bin/env python3
"""Prepare a first resource import by running the existing reconstruction tools.

This is an import-only coordinator, not a second importer. Existing imported Unity
assets are adopted without rebuilding their prefabs/configs or replacing user edits.
All original serialized resource reads use the local imported-source cache.
"""
from __future__ import annotations

import argparse
import json
import os
from pathlib import Path
import re
import shutil
import subprocess
import sys
import time

from imported_source import REPO, cache_root, res_root
from import_source_cache import restore_evidence, snapshot_evidence

PIPELINE = REPO / "tools/unity-rebuild"
OUT = PIPELINE / "out/unity"
PROJECT = REPO / "UnityProject"


def validate_unity_receipt(method: str, value: dict, allow_partial: bool = False) -> str:
    """Fail closed on incomplete tool receipts, even when Unity exits zero."""
    status = value.get("status", "")
    partial = status == "PARTIAL"
    if partial:
        if not allow_partial or value.get("failed", 0) > 0 or value.get("references_failed", 0) > 0:
            raise RuntimeError(f"{method} reported failed imports/references: {status}")
    elif status != "PARSED" and not status.startswith("OK"):
        raise RuntimeError(f"{method} produced no successful receipt: {status or 'missing status'}")
    if method == "AddressablesSetup.Run" and (value.get("entries_missing_asset", 0) > 0 or value.get("labels_unregistered", 0) > 0):
        raise RuntimeError(f"{method} has missing assets or unregistered labels: {status}")
    return status


def seed_font_source_guids() -> list[str]:
    """Restore only missing local OTF metadata referenced by tracked TMP assets.

    These fonts already have stable generated assets in a fresh checkout. Their
    local copyrighted OTFs/.meta are ignored, so letting Unity allocate a random
    source GUID would leave the tracked TMP font's sourceFontFile null. Existing
    metadata is never edited, including metadata created by a user's own import.
    """
    seeded = []
    for name in ("TsukushiMincho", "SourceHanSerifSC", "SourceHanSansKR"):
        asset = PROJECT / f"Assets/GameContent/RuntimeFonts/{name}/{name}.asset"
        source = PROJECT / f"Assets/GameContent/#WitchTrials/Fonts/{name}/{name}.otf"
        meta = source.with_suffix(source.suffix + ".meta")
        if not asset.is_file() or not source.is_file() or meta.exists():
            continue
        matches = re.findall(r"^\s*m_SourceFontFileGUID:\s*([0-9a-f]{32})\s*$", asset.read_text(encoding="utf-8-sig"), re.M)
        if len(matches) != 1:
            raise RuntimeError(f"Tracked TMP font carries no unique source GUID: {asset}")
        meta.write_text("fileFormatVersion: 2\nguid: " + matches[0] + "\nTrueTypeFontImporter:\n  externalObjects: {}\n  includeFontData: 1\n", encoding="utf-8")
        seeded.append(str(meta))
    return seeded


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--unity-editor", type=Path)
    parser.add_argument("--timeout", type=int, default=7200)
    args = parser.parse_args()
    OUT.mkdir(parents=True, exist_ok=True)
    log_dir = PIPELINE / "out/logs/import-bootstrap"
    log_dir.mkdir(parents=True, exist_ok=True)
    report = {"status": "NOT_RUN", "steps": [], "original_game_reads": False}
    env = dict(os.environ)
    # Reconstruction probes permit output/plan overrides for research. Import
    # controls these explicitly so a prior probe cannot redirect its receipts.
    for key in ("MANOSABA_SO_OUT", "MANOSABA_SO_PLAN", "MANOSABA_PREFAB_OUT", "MANOSABA_PREFAB_PLAN",
                "MANOSABA_PREFAB_FIELD_DUMP", "MANOSABA_RESOURCE_REPORT", "MANOSABA_MATERIAL_PLAN",
                "MANOSABA_CATALOG_PATH", "MANOSABA_CATALOG_OUT", "MANOSABA_ADDRESSABLES_PLAN",
                "MANOSABA_ADDRESSABLES_PRUNE"):
        env.pop(key, None)
    env.update({"MANOSABA_CONTENT_ONLY": "1", "MANOSABA_GAME_ROOT": str(OUT / "__original_install_unavailable__"),
                "MANOSABA_GAME_DATA_ROOT": str(cache_root() / "manosaba_Data"),
                "MANOSABA_AA_ROOT": str(cache_root() / "manosaba_Data/StreamingAssets/aa"),
                "MANOSABA_PYTHON": sys.executable, "PYTHONUTF8": "1"})
    packages = REPO / "tools/venv/Lib/site-packages"
    env["PYTHONPATH"] = str(packages) + (os.pathsep + env["PYTHONPATH"] if env.get("PYTHONPATH") else "")

    def run(tag, command, allowed_exit_codes=(0,)):
        print("[import] " + tag, flush=True)
        started = time.monotonic()
        completed = subprocess.run(list(map(str, command)), cwd=REPO, env=env, capture_output=True,
                                   encoding="utf-8", errors="replace", timeout=args.timeout)
        log = log_dir / (tag + ".log")
        log.write_text(completed.stdout + "\n" + completed.stderr, encoding="utf-8")
        report["steps"].append({"step": tag, "exit_code": completed.returncode,
                                "seconds": round(time.monotonic() - started, 2), "log": str(log)})
        if completed.returncode not in allowed_exit_codes:
            raise RuntimeError(f"{tag} failed (exit {completed.returncode}); see {log}")

    def python(name, *parameters):
        run(Path(name).stem, [sys.executable, PIPELINE / name, *parameters])

    def ensure_python(artifact, name, *parameters):
        # A fresh reconstruction may have restored evidence from another checkout.
        # Recompute targets from current res instead of reusing old absolute paths.
        if not adopted or not (OUT / artifact).is_file():
            python(name, *parameters)
        if not (OUT / artifact).is_file():
            raise RuntimeError(f"{name} produced no required input {artifact}")

    try:
        if not (res_root() / "_index.csv").is_file():
            raise RuntimeError("Extract resources with tools/content/import.ps1 before bootstrapping Unity")
        restore_evidence(res_root())
        shell = shutil.which("pwsh") or shutil.which("powershell")
        if not shell:
            raise RuntimeError("PowerShell is required to stage imported runtime metadata")
        run("stage-imported-runtime-metadata", [shell, "-NoProfile", "-ExecutionPolicy", "Bypass", "-File", PIPELINE / "apply_shipped_aa.ps1"])
        game_prefab = PROJECT / "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab"
        adopted = game_prefab.is_file() and (OUT / "a13_import_ledger.csv").is_file()
        required = ("bundle_script_map.json", "so_convert_plan_data.json", "so_convert_plan_scenarios.json",
                    "prefab_plan_all.json", "addressables_plan_all.json", "catalog_dump_shipped.json")
        if adopted and all((OUT / name).is_file() for name in required):
            snapshot_evidence(res_root())
            report["status"] = "ADOPTED_EXISTING_CONTENT"
            print("[import] Existing imported Unity content and plans adopted; user assets preserved.", flush=True)
            return 0
        data = cache_root() / "manosaba_Data"
        if not data.is_dir():
            raise RuntimeError("Imported serialized resource cache is missing")
        editor = args.unity_editor or Path(os.environ.get("MANOSABA_UNITY_EDITOR", ""))
        if not editor.is_file():
            version = (PROJECT / "ProjectSettings/ProjectVersion.txt").read_text().splitlines()[0].split(":", 1)[1].strip()
            for root in ("E:/Unity", "C:/Program Files/Unity/Hub/Editor", "D:/Unity", "E:/Unity/Hub/Editor"):
                candidate = Path(root) / version / "Editor/Unity.exe"
                if candidate.is_file():
                    editor = candidate
                    break
        if not editor.is_file():
            raise RuntimeError("Licensed Unity Editor required for first import; set MANOSABA_UNITY_EDITOR")

        def unity(method, artifact=None, extra=None, allow_partial=False, quit=True):
            tag = method.split(".")[-2] + "-" + method.split(".")[-1]
            expected = OUT / artifact if artifact else None
            previous = None
            if expected and expected.is_file():
                previous = expected.with_suffix(".previous.json")
                shutil.copy2(expected, previous)
                expected.unlink()
            old = dict(env)
            env.update(extra or {})
            try:
                command = [editor, "-batchmode", "-nographics", "-projectPath", PROJECT,
                           "-executeMethod", "Manosaba.EditorTools." + method, "-logFile", "-"]
                if quit:
                    command.append("-quit")
                run(tag, command, (0, 1) if allow_partial else (0,))
                if expected:
                    if not expected.is_file():
                        raise RuntimeError(f"{method} produced no fresh {artifact}")
                    value = json.loads(expected.read_text(encoding="utf-8-sig"))
                    status = validate_unity_receipt(method, value, allow_partial)
                    if status != "OK" and status != "PARSED":
                        report.setdefault("limitations", []).append({"step": method, "report": str(expected), "status": status})
            finally:
                env.clear()
                env.update(old)

        per_source = REPO / "tools/audit/out/a03/per_source"
        if not adopted or not per_source.is_dir() or not any(per_source.glob("*.json")):
            run("audit-a03-imported-resources", [sys.executable, REPO / "tools/audit/a03_sources.py"])
        ensure_python("reconstructed_script_index.json", "a13_script_index.py")
        # Saved ledgers describe prior files, not whether this checkout has them.
        # A missing game prefab requires rebuilding the generated tree from res.
        if not adopted or not (OUT / "a13_import_ledger.csv").is_file():
            python("a13_import_assets.py")
        if not adopted:
            report["source_font_metadata_seeded"] = seed_font_source_guids()
        ensure_python("bundle_script_map.json", "a13b_script_binding.py", "--from-index", "--max-bundles", "0")
        ensure_python("so_convert_plan_data.json", "a13c_plan_scriptableobjects.py", "--family", "data")
        ensure_python("so_convert_plan_scenarios.json", "a13c_plan_scriptableobjects.py", "--family", "scenarios")
        # Boot/MasterData decoders join against catalog evidence, so capture it
        # before running those existing planners on a fresh workspace.
        if not adopted or not (OUT / "catalog_dump_shipped.json").is_file():
            unity("CatalogDump.Run", "catalog_dump_shipped.json", {
                "MANOSABA_CATALOG_PATH": str(data / "StreamingAssets/aa/catalog.bin"),
                "MANOSABA_CATALOG_OUT": str(OUT / "catalog_dump_shipped.json")})
        ensure_python("naninovel_configs.json", "decode_naninovel_configs.py", "--write")
        ensure_python("master_data_decode.json", "decode_master_data.py", "--write")
        ensure_python("material_import_plan.json", "plan_material_import.py", "--write", "--quiet")
        ensure_python("material_embedded_plan.json", "plan_embedded_materials.py", "--write")
        ensure_python("texture_settings_plan.json", "plan_texture_settings.py", "--write", "--scope", "all")
        sprite_parameters = ["--write"] + (["--restore-crops"] if not adopted else [])
        ensure_python("sprite_settings_plan.json", "plan_sprite_settings.py", *sprite_parameters)
        ensure_python("prefab_plan_all.json", "a13e_plan_prefabs.py", "--group", "all")
        ensure_python("level0_boot_references.json", "decode_level0_boot.py", "--write")
        ensure_python("level0_scene_inventory.json", "read_level0_scene_inventory.py", "--write")
        ensure_python("level0_component_values.json", "decode_level0_component_values.py", "--write")
        if not adopted:
            if not (PROJECT / "Assets/TextMesh Pro/Resources/TMP Settings.asset").is_file():
                unity("TmpEssentialsSetup.Run", "tmp_essentials.json", quit=False)
            unity("TextureSettingsImporter.Run", "texture_settings_import.json")
            unity("SpriteImportPolicy.ReimportPlanned")
            unity("RuntimeResourceRepair.Run", "runtime_resource_repair.json")
            unity("MaterialImporter.Run", "material_import.json", allow_partial=True)
            unity("MaterialImporter.Run", "material_import_material_embedded_plan.json", {"MANOSABA_MATERIAL_PLAN": "material_embedded_plan.json"}, allow_partial=True)
            unity("ScriptableObjectConverter.Run", "so_convert_result.json", {"MANOSABA_SO_PLAN": "so_convert_plan_*.json"})
            unity("PrefabBuilder.Run", "prefab_build_result.json", {"MANOSABA_PREFAB_PLAN": "prefab_plan_all.json"})
            unity("DicedSpriteAtlasBuilder.Run", "diced_atlas_result.json")
            unity("NaninovelConfigBuilder.Run", "naninovel_config_import.json")
            # The existing material planner's reference bindings come from the
            # config import receipt and prefab plan, which now both exist.
            python("plan_material_import.py", "--write", "--quiet")
            unity("MaterialImporter.Run", "material_import.json", allow_partial=True)
            unity("MasterDataImporter.Run", "master_data_import.json")
            python("align_asset_guids.py", "--apply")
            python("remap_naninovel_guids.py", "--write")
            unity("Level0SceneBuilder.Run", "level0_scene_build.json")
            unity("ProjectBootstrap.Run", "bootstrap_result.json")
        ensure_python("addressables_plan_all.json", "a13f_plan_addressables.py", "--group", "all")
        if not adopted:
            python("align_editor_resources.py", "--apply")
        unity("AddressablesSetup.Run", "addressables_setup_result.json", {"MANOSABA_ADDRESSABLES_PLAN": "addressables_plan_all.json"})
        snapshot_evidence(res_root())
        report["status"] = "PREPARED"
        return 0
    except (OSError, ValueError, RuntimeError, subprocess.SubprocessError) as error:
        report["status"] = "BLOCKED"
        report["error"] = str(error)
        print("BLOCKED: " + str(error), file=sys.stderr)
        return 1
    finally:
        (OUT / "content_import_bootstrap.json").write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")


if __name__ == "__main__":
    raise SystemExit(main())
