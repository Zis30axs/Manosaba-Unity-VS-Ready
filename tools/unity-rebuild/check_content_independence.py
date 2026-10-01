#!/usr/bin/env python3
"""Execute sync and Unity gates with the original install physically unavailable.

--disconnect-original temporarily renames the explicitly selected game directory,
restoring it in finally. Child processes receive nonexistent original-game roots.
A partial/static run never reports full independence PASS. This is an execution
gate: every selected existing G gate must produce fresh successful evidence.
"""
from __future__ import annotations

import argparse
from datetime import datetime, timezone
import json
import os
from pathlib import Path
import shutil
import subprocess
import sys
import time
import uuid

from imported_source import REPO
from import_source_cache import forbidden_resource

STAGES = ("sync", "compile", "import", "addressables", "play", "player")
GATES = {
    "compile": ("g1_compile.ps1", "g1_compile.json"),
    "import": ("g2_import_gate.ps1", "g2_import.json"),
    "addressables": ("g3_content_build.ps1", "g3_content_build.json"),
    "play": ("g4_play_mode.ps1", "g4_play_mode_gate.json"),
    "player": ("g5_player_smoke.ps1", "g5_player_smoke.json"),
}
FORBIDDEN_FILES = ("manosaba.exe", "GameAssembly.dll", "global-metadata.dat", "BepInEx")


def read_json(path: Path) -> dict:
    return json.loads(path.read_text(encoding="utf-8-sig")) if path.is_file() else {}


def static_checks() -> list[dict]:
    audit = read_json(REPO / "tools/audit/audit.config.json")
    common = (REPO / "tools/unity-rebuild/common.ps1").read_text(encoding="utf-8-sig")
    apply = (REPO / "tools/unity-rebuild/apply_shipped_aa.ps1").read_text(encoding="utf-8-sig")
    cache = REPO / "res/.imported-source"
    forbidden_cache_files = [str(p.relative_to(cache)) for p in cache.rglob("*")
                             if p.is_file() and forbidden_resource(p.relative_to(cache))]
    return [
        {"check": "public game_root is empty", "ok": not audit.get("game_root")},
        {"check": "ordinary common config never resolves original install",
         "ok": "'game_root'" not in common and "MANOSABA_GAME_ROOT" not in common},
        {"check": "runtime metadata staging uses imported cache",
         "ok": "ImportedSourceRoot" in apply and "GameRoot" not in apply and "MANOSABA_GAME_ROOT" not in apply},
        {"check": "ordinary content/build preflights are distinct",
         "ok": '"content", "build"' in (REPO / "tools/unity-rebuild/preflight.py").read_text(encoding="utf-8")},
        {"check": "imported resource cache contains no original runtime or plugin code",
         "ok": not forbidden_cache_files, "forbidden_files": forbidden_cache_files},
    ]


def configured_game() -> Path | None:
    candidate = os.environ.get("MANOSABA_GAME_ROOT")
    if not candidate:
        for name in ("audit.config.local.json", "audit.config.json"):
            candidate = read_json(REPO / "tools/audit" / name).get("game_root")
            if candidate:
                break
    return Path(candidate).resolve() if candidate else None


def checked_disconnect_target(game: Path) -> Path:
    game = game.resolve()
    # Rename is limited to this explicit game directory; never a drive root,
    # repository, editable workspace, or parent of the repository.
    if not game.name or game == Path(game.anchor) or game == REPO or game in REPO.parents or REPO in game.parents:
        raise ValueError("Refusing to disconnect a drive, repository, or repository ancestor")
    if not (game / "manosaba_Data/StreamingAssets").is_dir():
        raise ValueError("Disconnect target is not the selected game directory")
    hidden = game.with_name(game.name + ".manosaba-independence-" + uuid.uuid4().hex)
    if hidden.exists():
        raise ValueError("Disconnect destination already exists")
    return hidden


def run_stage(stage: str, env: dict, output: Path, group: str, timeout: int, forbidden_root: Path | None) -> dict:
    shell = shutil.which("pwsh") or shutil.which("powershell")
    if not shell:
        raise ValueError("PowerShell is required for the existing Unity gates")
    if stage == "sync":
        script = REPO / "tools/content/sync.ps1"
        artifact = None
    else:
        name, result_name = GATES[stage]
        script = REPO / "tools/unity-rebuild" / name
        artifact = REPO / "tools/unity-rebuild/out/unity" / result_name
    if not script.is_file():
        raise ValueError(f"Required workflow entry point missing: {script}")
    command = [shell, "-NoProfile", "-ExecutionPolicy", "Bypass", "-File", str(script)]
    if stage == "addressables":
        command += ["-Group", group]
    before = time.time_ns()
    log_root = REPO / "tools/unity-rebuild/out/logs"
    old_logs = {p: p.stat().st_mtime_ns for p in log_root.rglob("*.log")}
    started = time.monotonic()
    completed = subprocess.run(command, cwd=REPO, env=env, capture_output=True,
                               timeout=timeout, encoding="utf-8", errors="replace")
    text = completed.stdout + "\n" + completed.stderr
    (output / f"{stage}.log").write_text(text, encoding="utf-8")
    fresh = artifact is None or (artifact.is_file() and artifact.stat().st_mtime_ns >= before)
    evidence = read_json(artifact) if artifact and fresh else None
    violations = []
    historical_references = []
    inspected = [(str(output / f"{stage}.log"), text)]
    for path in log_root.rglob("*.log"):
        if path.stat().st_mtime_ns != old_logs.get(path):
            inspected.append((str(path), path.read_text(encoding="utf-8-sig", errors="replace")))
    if artifact and fresh:
        execution = dict(evidence)
        # G5 embeds a previously decoded scene/catalog record for provenance.
        # Its historical source path describes the one-time import, rather than
        # a read performed by this build. Inspect every other current gate field.
        provenance = execution.pop("boot_scene", None)
        if provenance and forbidden_root:
            normalized = json.dumps(provenance, ensure_ascii=False).lower().replace("\\\\", "/").replace("\\", "/")
            marker = str(forbidden_root).lower().replace("\\", "/")
            if marker in normalized:
                historical_references.append({"artifact": str(artifact), "field": "boot_scene", "marker": str(forbidden_root)})
        inspected.append((str(artifact), json.dumps(execution, ensure_ascii=False)))
    for path, contents in inspected:
        lower = contents.lower().replace("\\\\", "/").replace("\\", "/")
        markers = list(FORBIDDEN_FILES)
        # UnityPlayer.dll in the rebuilt Player/Editor is legitimate. Only an
        # original install path identifies forbidden Unity runtime binary access.
        if forbidden_root:
            markers.append(str(forbidden_root).replace("\\", "/"))
        for marker in markers:
            if marker.lower() in lower:
                violations.append({"log": path, "marker": marker})
    ok = completed.returncode == 0 and fresh and (evidence is None or evidence.get("status") == "PASS") and not violations
    return {"stage": stage, "command": command, "exit_code": completed.returncode,
            "seconds": round(time.monotonic() - started, 2), "artifact_fresh": fresh,
            "artifact": str(artifact) if artifact else None,
            "evidence_status": evidence.get("status") if evidence else None,
            "historical_reference_markers": historical_references,
            "forbidden_access_markers": violations, "status": "PASS" if ok else "FAIL"}


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--game-root", type=Path, help="Original directory to disconnect (defaults to research config)")
    parser.add_argument("--disconnect-original", action="store_true")
    parser.add_argument("--static-only", action="store_true")
    parser.add_argument("--stages", nargs="+", choices=STAGES, default=list(STAGES))
    parser.add_argument("--group", default="all")
    parser.add_argument("--timeout", type=int, default=7200, help="Maximum seconds per workflow stage")
    args = parser.parse_args()
    output = REPO / "tools/unity-rebuild/out/content-independence" / datetime.now(timezone.utc).strftime("%Y%m%dT%H%M%S")
    output.mkdir(parents=True, exist_ok=True)
    result = {"status": "NOT_RUN", "generated": datetime.now(timezone.utc).isoformat(),
              "static_checks": static_checks(), "stages": [], "original_disconnected": False}
    game = args.game_root.resolve() if args.game_root else configured_game()
    hidden = None
    try:
        if not all(c["ok"] for c in result["static_checks"]):
            result["status"] = "FAIL"
        elif args.static_only:
            result["status"] = "STATIC_ONLY"
        else:
            if game is None:
                raise ValueError("Select the original installation path with --game-root so its physical absence can be verified")
            if game and game.exists():
                if not args.disconnect_original:
                    raise ValueError("Original install is still present; use --disconnect-original for a physical independence test")
                hidden = checked_disconnect_target(game)
                # Persist the recovery paths before the reversible rename, so an
                # interrupted host still leaves an exact restoration record.
                (output / "disconnect.json").write_text(json.dumps({"original": str(game), "disconnected": str(hidden)}, ensure_ascii=False, indent=2), encoding="utf-8")
                game.rename(hidden)
                result["original_disconnected"] = not game.exists()
            else:
                result["original_disconnected"] = True
            poison = REPO / "tools/unity-rebuild/out/__original_game_must_not_exist__"
            if poison.exists():
                raise ValueError("Poison original-game path unexpectedly exists")
            env = dict(os.environ)
            for key in ("MANOSABA_GAME_ROOT", "MANOSABA_GAME_DATA_ROOT", "MANOSABA_AA_ROOT"):
                env.pop(key, None)
                env[key] = str(poison / key)
            env["MANOSABA_CONTENT_ONLY"] = "1"
            env["MANOSABA_PYTHON"] = sys.executable
            result["environment"] = {k: env[k] for k in ("MANOSABA_GAME_ROOT", "MANOSABA_GAME_DATA_ROOT", "MANOSABA_AA_ROOT", "MANOSABA_CONTENT_ONLY")}
            for stage in STAGES:
                if stage not in args.stages:
                    continue
                print(f"[independence] {stage}: original install unavailable", flush=True)
                step = run_stage(stage, env, output, args.group, args.timeout, game)
                result["stages"].append(step)
                print(f"[independence] {stage}: {step['status']} ({step['seconds']}s)", flush=True)
                if step["status"] != "PASS":
                    break
            passed = len(result["stages"]) == len(set(args.stages)) and all(s["status"] == "PASS" for s in result["stages"])
            complete = set(args.stages) == set(STAGES)
            result["status"] = ("PASS" if complete else "PARTIAL") if passed else "FAIL"
    except (OSError, ValueError, subprocess.SubprocessError) as error:
        result["status"] = "BLOCKED"
        result["error"] = str(error)
    finally:
        if hidden and hidden.exists():
            if game.exists():
                result["status"] = "FAIL"
                result["restore_error"] = "Original path appeared during gate; disconnected directory remains at " + str(hidden)
            else:
                try:
                    hidden.rename(game)
                    result["original_restored"] = game.exists()
                except OSError as error:
                    result["status"] = "FAIL"
                    result["restore_error"] = f"Restore {hidden} to {game}: {error}"
        report = output / "result.json"
        report.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
        print(json.dumps({"status": result["status"], "report": str(report)}, ensure_ascii=False))
    return 0 if result["status"] in ("PASS", "PARTIAL", "STATIC_ONLY") else 1


if __name__ == "__main__":
    raise SystemExit(main())
