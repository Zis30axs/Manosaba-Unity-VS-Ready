#!/usr/bin/env python3
"""The editable res workspace front door; reuses the reconstruction converters."""
from __future__ import annotations

import argparse
from collections import Counter, defaultdict
from contextlib import contextmanager
import csv
from datetime import datetime, timedelta, timezone
import hashlib
import importlib.util
import json
import os
from pathlib import Path
import re
import shutil
import subprocess
import sys
import time
import uuid
from state_store import compact_fields, read_content_state

REPO = Path(__file__).resolve().parents[2]
PIPELINE = REPO / "tools/unity-rebuild"
sys.path.insert(0, str(PIPELINE))
# Existing local extraction environment, also usable with a portable Python runtime.
packages = REPO / "tools/venv/Lib/site-packages"
if packages.is_dir():
    sys.path.append(str(packages))
import a13_import_assets as importer
import a13f_plan_addressables as addressables
import resource_map

RES = REPO / "res"
PROJECT = REPO / "UnityProject"
OUT = PIPELINE / "out/unity"
STATE = RES / ".manosaba-content-state.json"
BASELINE = RES / ".manosaba/base-hashes.json"
CACHE = RES / ".manosaba/reconstruction"
BRIDGE = PROJECT / "Library/ManosabaContent"
CATEGORIES = ("TEXT", "TEXTURE", "SPRITE", "DATA", "SCENARIO", "PREFAB", "FONT", "AUDIO", "VIDEO", "OTHER")


def read_json(path, default=None):
    return json.loads(path.read_text(encoding="utf-8-sig")) if path.is_file() else default


def write_json(path, data):
    path.parent.mkdir(parents=True, exist_ok=True)
    temp = path.with_name(path.name + ".tmp")
    with temp.open("w", encoding="utf-8") as stream:
        json.dump(data, stream, ensure_ascii=False, indent=None if path == STATE else 1)
    os.replace(temp, path)


def digest(path):
    with path.open("rb") as stream:
        return hashlib.file_digest(stream, "sha256").hexdigest()


def fingerprint(path, old=None, force=False):
    stat = path.stat()
    if not force and old and stat.st_size == old.get("size") and stat.st_mtime_ns == old.get("mtime_ns"):
        return dict(old)
    return {"size": stat.st_size, "mtime_ns": stat.st_mtime_ns, "sha256": digest(path)}


def valid_fingerprint(value):
    return (isinstance(value, dict) and type(value.get("size")) is int and value["size"] >= 0
            and type(value.get("mtime_ns")) is int and isinstance(value.get("sha256"), str)
            and re.fullmatch(r"[0-9a-f]{64}", value["sha256"]) is not None)


def metadata_identity(destination, old=None, force=False):
    path = Path(str(destination) + ".meta")
    if not path.is_file():
        return None
    stamp = fingerprint(path, old, force)
    if old and stamp["sha256"] == old.get("sha256") and old.get("guid"):
        return {**stamp, "guid": old["guid"]}
    match = re.search(r"^guid:\s*([0-9a-fA-F]{32})\s*$", path.read_text(encoding="utf-8-sig"), re.MULTILINE)
    if not match:
        raise ValueError(f"Invalid GUID in metadata: {path}")
    return {**stamp, "guid": match.group(1).lower()}


def source_path(relative, resolve=True):
    return importer.checked_path(RES, relative, resolve=resolve)


def unity_path(value):
    """Normalize old absolute plans without adopting their machine-specific prefix."""
    text = str(value).replace("\\", "/")
    if "/UnityProject/Assets/" in text:
        text = "Assets/" + text.split("/UnityProject/Assets/", 1)[1]
    if text.startswith("UnityProject/"):
        text = text[len("UnityProject/"):]
    if not text.startswith("Assets/"):
        raise ValueError(f"Invalid Unity target: {value}")
    target = importer.checked_path(PROJECT, text, resolve=False)
    allowed = (PROJECT / "Assets/GameContent", PROJECT / "Assets/NaninovelData/Resources")
    if not any(target.is_relative_to(root) for root in allowed):
        raise ValueError(f"Target is outside generated game content: {value}")
    return text


def evidence(name):
    # Saved resource metadata is authoritative; out/ remains a regenerable work cache.
    cached = CACHE / "unity" / name
    if not cached.is_file():
        cached = CACHE / name
    return cached if cached.is_file() else OUT / name


# Import keeps a plan once it exists, so a fix in its tracked planner never reached an existing
# copy (the cached prefab plan predated Timeline/AnimationClip sub-assets in 68 prefabs).
# Regenerate a cached plan whenever its planner changes; the plan's hash then changes every
# dependent conversion signature.
PLANNERS = {"prefab_plan_all.json": ("a13e_plan_prefabs.py", "--group", "all")}


def refresh_plans():
    for name, (script, *parameters) in PLANNERS.items():
        planner = PIPELINE / script
        cached = CACHE / "unity" / name
        recorded = cached.with_name(name + ".planner.sha256")
        current = digest(planner)
        if cached.is_file() and recorded.is_file() and recorded.read_text(encoding="utf-8").strip() == current:
            continue
        restore_evidence()
        run([sys.executable, planner, *parameters])
        if not (OUT / name).is_file():
            raise RuntimeError(f"{script} produced no {name}")
        cached.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(OUT / name, cached)
        recorded.write_text(current, encoding="utf-8")
        # The evidence manifest records each snapshot file's hash and restore_evidence() rejects a mismatch, so the
        # regenerated plan must be re-recorded or every later gate stops with "evidence hash mismatch".
        run([sys.executable, PIPELINE / "import_source_cache.py", "--res-root", RES, "--snapshot-only"])


_REBUILT_ASSETS = {}
# TMP assets the importer defers and PrefabBuilder regenerates; a prefab whose labels reference one is rebuilt, not patched.
DEFERRED_TMP_ASSETS = {
    "Assets/#WitchTrials/Fonts/Common/TrialsClockFont.asset",
    "Assets/#WitchTrials/Fonts/Common/DebatePrinterStyleSheet.asset",
}


def assert_rebuilt(batch_jobs, result):
    """Reject a stale Editor that patched a prefab whose recipe requires generated sub-assets."""
    expected = []
    for job in batch_jobs:
        if job.get("category") != "PREFAB" or not job.get("rebuild"):
            continue
        plan = job["plan"]
        if plan not in _REBUILT_ASSETS:
            _REBUILT_ASSETS[plan] = {
                j["asset_path"] for j in read_json(REPO / plan, {}).get("jobs", [])
                if any(o.get("type") == "AnimationClip" for o in j.get("objects", [])) or
                   j.get("asset_path", "").replace("\\", "/").endswith(
                       ("/Prefabs/General/PerspectiveStage/Court.prefab",
                        "/Prefabs/General/PerspectiveStage/Court_Final.prefab")) or
                   any(str(v).replace("\\", "/") in DEFERRED_TMP_ASSETS for v in j.get("references", {}).values())
            }
        if job["asset_path"] in _REBUILT_ASSETS[plan]:
            expected.append(job["source"])
    statuses = {j.get("source"): j.get("status") for j in result.get("jobs", [])}
    missing = [source for source in expected if statuses.get(source) != "REBUILT"]
    if missing:
        raise RuntimeError(f"Unity patched instead of rebuilding {len(missing)} generated-subasset prefab sources "
                           f"(first: {missing[0]}); let the Editor finish compiling the pulled converters, then rerun sync")


def restore_evidence():
    source_root = CACHE / "unity" if (CACHE / "unity").is_dir() else CACHE
    if source_root.is_dir():
        OUT.mkdir(parents=True, exist_ok=True)
        for path in source_root.iterdir():
            if path.is_file():
                target = OUT / path.name
                if not target.is_file():
                    shutil.copy2(path, target)


def inventory():
    index = RES / "_index.csv"
    if not index.is_file():
        raise FileNotFoundError("res/_index.csv is missing. Run tools/content/import.ps1 <game> first.")
    records = {}
    addresses = defaultdict(list)
    sprite_plan = {unity_path(path): settings for path, settings in read_json(evidence("sprite_settings_plan.json"), {}).get("sprites", {}).items()}
    plan = read_json(evidence("addressables_plan_all.json"), {})
    for entry in plan.get("entries", []):
        addresses[unity_path(entry["asset_path"])].append({
            "address": entry["address"], "group": entry["group"], "labels": entry.get("labels", [])})

    def add(relative, category, target=None, job=None, dependency=None):
        relative = relative.replace("\\", "/")
        if relative.startswith("res/"):
            relative = relative[4:]
        source_path(relative, resolve=False)  # Resolve again before any actual read/write.
        rec = records.setdefault(relative, {"relative_path": relative, "category": category,
                                          "targets": [], "jobs": [], "dependencies": [],
                                          "editability": "READ_ONLY"})
        if target:
            target = unity_path(target)
            if target not in rec["targets"]:
                rec["targets"].append(target)
            rec["editability"] = "FULL" if category in importer.LEAF_CATEGORIES or category in ("TEXT", "TEXTURE", "SPRITE", "FONT", "AUDIO", "VIDEO") else "PARTIAL"
        if job and job not in rec["jobs"]:
            rec["jobs"].append(job)
        if dependency and dependency not in rec["dependencies"]:
            rec["dependencies"].append(dependency)

    with index.open(encoding="utf-8-sig", newline="") as stream:
        rows = list(csv.DictReader(stream))
    duplicate_sources = set()
    ledger = evidence("a13_import_ledger.csv")
    if ledger.is_file():
        with ledger.open(encoding="utf-8-sig", newline="") as stream:
            duplicate_sources = {row["output_file"].replace("\\", "/") for row in csv.DictReader(stream)
                                 if row["status"] == "DUPLICATE_DESTINATION"}
    for row in rows:
        rel = row.get("output_file", "").strip()
        if not rel:
            continue
        cat = importer.CATEGORY_BY_TYPE.get(row["type"])
        if cat and rel not in duplicate_sources:
            target = importer.dest_for_sprite(row["asset_path"], rel, row["name"]) if cat == "sprite" else importer.dest_for(row["asset_path"], rel)
            add(rel, cat.upper(), Path(target).relative_to(PROJECT).as_posix())
            if cat == "sprite" and records[rel]["targets"][0] in sprite_plan:
                settings = sprite_plan[records[rel]["targets"][0]]
                records[rel]["sprite_settings"] = {
                    "rect_size": settings["rect_size"],
                    "texture_rect_offset": settings.get("texture_rect_offset", settings.get("trimmed", {}).get("texture_rect_offset"))}
        else:
            add(rel, "OTHER")
            if rel in duplicate_sources:
                records[rel]["reason"] = "Existing a13 duplicate destination; kept READ_ONLY to preserve the canonical Sprite GUID/address."
    for family, category in (("data", "DATA"), ("scenarios", "SCENARIO")):
        for job in read_json(evidence(f"so_convert_plan_{family}.json"), {}).get("jobs", []):
            source = job["dump"].replace("\\", "/")
            if source.startswith("res/"):
                source = source[4:]
            elif "/res/" in source:
                source = source.split("/res/", 1)[1]
            target = unity_path(job["target"])
            records.pop(source, None)
            add(source, category, target, {"category": category, "source": "res/" + source,
                                          "target": target, "type": job["type"], "assembly": job.get("assembly", "")})
    prefab_plan = evidence("prefab_plan_all.json")
    for job in read_json(prefab_plan, {}).get("jobs", []):
        target = unity_path(job["target"])
        for obj in job.get("objects", []):
            source = obj["dump"].replace("\\", "/")
            if source.startswith("res/"):
                source = source[4:]
            records.pop(source, None)
            add(source, "PREFAB", target, {"category": "PREFAB", "source": "res/" + source,
                                           "target": target, "plan": prefab_plan.relative_to(REPO).as_posix(),
                                           "asset_path": job["asset_path"]})
    master = "Assets/#WitchTrials/Data/MasterData.asset.json"
    if source_path(master).is_file():
        add(master, "DATA", "Assets/NaninovelData/Resources/MasterData.asset", {
            "category": "DATA", "source": "res/" + master,
            "target": "Assets/NaninovelData/Resources/MasterData.asset",
            "type": "WitchTrials.Models.MasterData", "assembly": "Assembly-CSharp"})
    # All languages and managed text use the existing working localization copy chain.
    for subtree in ("Localization", "ManagedText"):
        root = RES / "Assets/#WitchTrials" / subtree
        if root.is_dir():
            for source in root.rglob("*.txt"):
                rel = source.relative_to(RES).as_posix()
                if rel not in records:
                    add(rel, "TEXT", "Assets/GameContent/" + rel.removeprefix("Assets/"))
    for rec in records.values():
        rec["addressables"] = [a for target in rec["targets"] for a in addresses.get(target, [])]
        if not rec["addressables"]:
            rec["addressables_group"] = addressables.group_for("Assets/" + rec["relative_path"].removeprefix("Assets/"))
        else:
            rec["addressables_group"] = rec["addressables"][0]["group"]
    # A cached source hash alone cannot acknowledge a changed conversion recipe.
    # Hash each shared converter/plan once, then bind it to the selected job.
    dependency_hashes = {}
    for rec in records.values():
        if rec["jobs"]:
            paths = [PROJECT / "Assets/Editor/ContentSyncBridge.cs", PROJECT / "Assets/Editor/JsonToSerializedApplier.cs"]
            paths.append(PROJECT / "Assets/Editor" / ("PrefabBuilder.cs" if rec["category"] == "PREFAB" else "ScriptableObjectConverter.cs"))
            if rec["category"] == "PREFAB":
                paths += [REPO / job["plan"] for job in rec["jobs"]]
            elif rec["relative_path"] != master:
                paths.append(evidence("so_convert_plan_" + ("scenarios" if rec["category"] == "SCENARIO" else "data") + ".json"))
            for path in paths:
                name = path.relative_to(REPO).as_posix()
                if name not in dependency_hashes:
                    dependency_hashes[name] = digest(path)
                if name not in rec["dependencies"]:
                    rec["dependencies"].append(name)
            recipe = {"jobs": rec["jobs"], "dependencies": {name: dependency_hashes[name] for name in rec["dependencies"]}}
            rec["conversion_signature"] = hashlib.sha256(json.dumps(recipe, sort_keys=True).encode("utf-8")).hexdigest()
        elif rec.get("sprite_settings"):
            name = "tools/unity-rebuild/a13_import_assets.py"
            if name not in dependency_hashes:
                dependency_hashes[name] = digest(REPO / name)
            rec["normalization_signature"] = dependency_hashes[name]
    return records


def settings():
    values = {}
    for name in ("audit.config.json", "audit.config.local.json"):
        values.update(read_json(REPO / "tools/audit" / name, {}))
    return values


def unity_editor():
    candidate = os.environ.get("MANOSABA_UNITY_EDITOR") or settings().get("unity_editor_path")
    if candidate and Path(candidate).is_file():
        return str(candidate)
    version = (PROJECT / "ProjectSettings/ProjectVersion.txt").read_text().splitlines()[0].split(":", 1)[1].strip()
    for root in ("E:/Unity", "C:/Program Files/Unity/Hub/Editor", "D:/Unity", "E:/Unity/Hub/Editor"):
        candidate = Path(root) / version / "Editor/Unity.exe"
        if candidate.is_file():
            return str(candidate)
    raise FileNotFoundError("Unity Editor not found; set MANOSABA_UNITY_EDITOR.")


def run(command, env=None, timeout=None):
    print("[content] " + subprocess.list2cmdline(list(map(str, command))), flush=True)
    try:
        result = subprocess.run(list(map(str, command)), cwd=REPO, env=env, timeout=timeout)
    except subprocess.TimeoutExpired as error:
        raise TimeoutError(f"Command timed out after {timeout}s: {command[0]}; pending content remains retryable") from error
    if result.returncode:
        raise RuntimeError(f"Command failed with exit {result.returncode}: {command[0]}")


def ps_script(name, *args):
    executable = shutil.which("pwsh") or shutil.which("powershell")
    if not executable:
        raise FileNotFoundError("PowerShell not found")
    return [executable, "-NoProfile", "-File", PIPELINE / name, *args]


# The ObjectionCutIn Timelines play two glass sounds, but the shared dump lacks both: Crack_01 has no row in
# res/_index.csv or the resource map, and Crack_02 is SOURCE_MISSING in older imports.  They are extracted by
# identity (bundle + path id) straight from the cached bundle; PrefabBuilder binds them by the same path ids.
CUTIN_AUDIO_BUNDLE = "naninovel-spawn_assets_all.bundle"
CUTIN_AUDIO_FOLDER = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/CutIn"
CUTIN_AUDIO = {4370497403900473225: "Crack_01", -3434779548957938459: "Crack_02"}


def ensure_cutin_audio():
    """Write the cut-in glass sounds into GameContent when absent; return their project paths (all of them)."""
    targets = {path_id: f"{CUTIN_AUDIO_FOLDER}/ObjectionCutIn_{name}.ogg" for path_id, name in CUTIN_AUDIO.items()}
    missing = {path_id: path for path_id, path in targets.items() if not (PROJECT / path).is_file()}
    if missing:
        bundle = RES / ".imported-source/manosaba_Data/StreamingAssets/aa/StandaloneWindows64" / CUTIN_AUDIO_BUNDLE
        if not bundle.is_file():
            raise FileNotFoundError(f"cached bundle {bundle} is missing; run the content import first")
        import io
        import soundfile
        import UnityPy
        # The existing extractor owns the Vorbis writer (it needs a large-stack thread on Windows).
        spec = importlib.util.spec_from_file_location("manosaba_existing_extractor", REPO / "work/extract_res.py")
        extractor = importlib.util.module_from_spec(spec)
        spec.loader.exec_module(extractor)
        written = set()
        for obj in UnityPy.load(str(bundle)).objects:
            if obj.type.name != "AudioClip" or obj.path_id not in missing:
                continue
            clip = obj.read()
            if clip.m_Name != CUTIN_AUDIO[obj.path_id]:
                raise RuntimeError(f"{bundle.name}: path id {obj.path_id} is {clip.m_Name!r}, expected {CUTIN_AUDIO[obj.path_id]!r}")
            samples = clip.samples
            if len(samples) != 1:
                raise RuntimeError(f"{clip.m_Name}: expected one decoded sample, got {len(samples)}")
            data, rate = soundfile.read(io.BytesIO(next(iter(samples.values()))), dtype="float32", always_2d=True)
            destination = PROJECT / missing[obj.path_id]
            destination.parent.mkdir(parents=True, exist_ok=True)
            partial = destination.with_name(destination.name + ".partial")
            extractor.write_ogg(str(partial), data, rate)
            partial.replace(destination)
            written.add(obj.path_id)
        if written != set(missing):
            raise RuntimeError(f"{bundle.name}: cut-in audio not found for path ids {sorted(set(missing) - written)}")
    return sorted(targets.values())


def apply_unity(jobs, import_paths, build=False, export=False, timeout=1800,
                setup_addressables=False, addressables_plan="", ensure_curtain_skin=False):
    # Every Unity run (sync, build, setup) sees the cut-in sounds and imports them with the other leaf assets.
    import_paths = [*import_paths, *ensure_cutin_audio()]
    BRIDGE.mkdir(parents=True, exist_ok=True)
    request_id = uuid.uuid4().hex
    request = {"id": request_id, "expiresUtc": (datetime.now(timezone.utc) + timedelta(seconds=timeout)).isoformat(),
               "jobs": jobs, "import_paths": sorted(set(import_paths)), "build_addressables": build,
               "export_master_data": export, "ensure_curtain_skin": ensure_curtain_skin}
    if setup_addressables:
        request["setup_addressables"] = True
        request["addressables_plan"] = addressables_plan or "addressables_plan_all.json"
    pending = BRIDGE / "request.json"
    if pending.is_file():
        previous = read_json(pending, {})
        expiry = previous.get("expiresUtc", "")
        if expiry and datetime.fromisoformat(expiry.replace("Z", "+00:00")) > datetime.now(timezone.utc):
            raise RuntimeError("Another content request is pending in Unity.")
    write_json(pending, request)
    result_path = BRIDGE / "result.json"
    try:
        # Windows advisory lock: a stale lockfile does not count as an open Editor.
        opened = False
        lockfile = PROJECT / "Temp/UnityLockfile"
        if os.name == "nt" and lockfile.is_file():
            import msvcrt
            try:
                with lockfile.open("r+b") as handle:
                    msvcrt.locking(handle.fileno(), msvcrt.LK_NBLCK, 1)
                    msvcrt.locking(handle.fileno(), msvcrt.LK_UNLCK, 1)
            except OSError:
                opened = True
        if not opened:
            logs = PIPELINE / "out/logs"
            logs.mkdir(parents=True, exist_ok=True)
            log = logs / "content_sync.log"
            try:
                run([unity_editor(), "-batchmode", "-nographics", "-quit", "-projectPath", PROJECT,
                     "-executeMethod", "Manosaba.EditorTools.ContentSyncBridge.Apply", "-logFile", log], timeout=timeout)
            except RuntimeError as error:
                report = read_json(result_path, {})
                if report.get("id") == request_id and report.get("status") == "ERROR":
                    details = list(dict.fromkeys(j.get("error") for j in report.get("jobs", []) if j.get("error")))
                    raise RuntimeError(report.get("message", report.get("error", str(error))) +
                                       (": " + "; ".join(details[:10]) if details else "") +
                                       (f"; {len(details) - 10} further errors in result.json" if len(details) > 10 else "")) from error
                raise
        deadline = time.monotonic() + timeout
        last_status = None
        while time.monotonic() < deadline:
            try:
                result = read_json(result_path, {})
            except (OSError, json.JSONDecodeError):
                result = {}
            if result.get("id") == request_id:
                status = result.get("status")
                if status != last_status:
                    print(f"[Unity] {status}: {result.get('message', '')}", flush=True)
                    last_status = status
                if status in ("OK", "PARTIAL"):
                    return result
                if status in ("ERROR", "FAILED", "FAIL"):
                    raise RuntimeError(result.get("message", "Unity content conversion failed"))
            time.sleep(0.5)
        raise TimeoutError("Unity did not complete content sync; see Library/ManosabaContent/result.json and content_sync.log")
    finally:
        if pending.is_file() and read_json(pending, {}).get("id") == request_id:
            pending.unlink()


@contextmanager
def workspace_lock():
    directory = RES / ".manosaba"
    directory.mkdir(parents=True, exist_ok=True)
    with (directory / "content.lock").open("a+b") as stream:
        if os.name == "nt":
            import msvcrt
            if stream.tell() == 0:
                stream.write(b"\0")
                stream.flush()
            stream.seek(0)
            try:
                msvcrt.locking(stream.fileno(), msvcrt.LK_NBLCK, 1)
            except OSError as error:
                raise RuntimeError("Another content operation is running") from error
        try:
            yield
        finally:
            if os.name == "nt":
                stream.seek(0)
                msvcrt.locking(stream.fileno(), msvcrt.LK_UNLCK, 1)


def selected(rec, args):
    categories = {c.strip().upper() for c in args.category.split(",")} if args.category else None
    if categories and categories - set(CATEGORIES):
        raise ValueError(f"Unknown categories: {sorted(categories - set(CATEGORIES))}")
    files = {p.replace("\\", "/").removeprefix("res/") for p in args.file}
    return (not categories or rec["category"] in categories) and (not files or rec["relative_path"] in files)


def conversion_batches(pending, files, asset_limit=24, job_limit=2000):
    """Bound loaded graphs/reports; all source objects of one prefab stay atomic."""
    units = {}
    for relative in pending:
        rec = files[relative]
        key = ("PREFAB", tuple(rec["targets"])) if rec["category"] == "PREFAB" else ("SOURCE", relative)
        units.setdefault(key, []).append(relative)
    batches, current, assets, jobs = [], [], 0, 0
    for unit in units.values():
        count = sum(len(files[relative]["jobs"]) for relative in unit)
        if current and (assets >= asset_limit or jobs + count > job_limit):
            batches.append(current)
            current, assets, jobs = [], 0, 0
        current.extend(unit)
        assets += 1
        jobs += count
    if current:
        batches.append(current)
    return batches


def sync(args):
    if (not getattr(args, "allow_resource_map_mismatch", False)
            and resource_map.DEFAULT_MAP.is_file() and (RES / "_index.csv").is_file()):
        report = resource_map.verify(RES / "_index.csv", resource_map.DEFAULT_MAP,
                                     resource_map.DEFAULT_META, check_files=False)
        if report["status"] != "PASS":
            raise RuntimeError("Resource identity map mismatch; run resource_map.py verify and import the matching dump "
                               "(or explicitly regenerate the reviewed map before using --allow-resource-map-mismatch)")
    if not args.dry_run:
        refresh_plans()
    records = inventory()
    for path in args.file:
        normalized = path.replace("\\", "/").removeprefix("res/")
        source_path(normalized)
        if normalized not in records:
            raise ValueError(f"File is not in the resource index or a supported editable tree: {path}")
    state = read_content_state(STATE, {"version": 1, "files": {}})
    files = state["files"]
    changed, unchanged, failed, readonly, pending = Counter(), 0, [], 0, []
    jobs, import_paths = [], []
    target_cache, metadata_cache = {}, {}
    for checked, (relative, rec) in enumerate(sorted(records.items()), 1):
        if checked % 5000 == 0 and not args.file:
            print(f"[scan] {checked}/{len(records)} sources checked; changed={sum(changed.values())}", flush=True)
        if not selected(rec, args):
            continue
        source = source_path(relative)
        old = files.get(relative, {})
        try:
            if not source.is_file():
                raise FileNotFoundError(f"Source removed: {relative}; Unity asset retained. Restore it or explicitly update the reconstruction plan.")
            stamp = fingerprint(source, old.get("source"), args.force)
            if not rec["targets"]:
                readonly += 1
                files[relative] = {**rec, "source": stamp, "sync_status": "READ_ONLY"}
                continue
            payload = None
            expected_sha256 = stamp["sha256"]
            sprite_settings = rec.get("sprite_settings")
            if sprite_settings:
                if not args.force and old.get("source", {}).get("sha256") == stamp["sha256"] and old.get("sprite_settings") == sprite_settings and old.get("normalization_signature") == rec.get("normalization_signature") and old.get("import_sha256"):
                    expected_sha256 = old["import_sha256"]
                else:
                    payload = importer.leaf_payload(source, sprite_settings)
                    if payload is not None:
                        expected_sha256 = hashlib.sha256(payload).hexdigest()
            targets, metadata = {}, {}
            target_changed = False
            for target in rec["targets"]:
                destination = importer.checked_path(PROJECT, target)
                if target not in metadata_cache:
                    metadata_cache[target] = metadata_identity(destination, old.get("target_metadata", {}).get(target), args.force)
                identity = metadata_cache[target]
                previous_guid = old.get("target_metadata", {}).get(target, {}).get("guid")
                if previous_guid and not identity:
                    raise ValueError(f"META_MISSING {target}; restore its original .meta before syncing")
                if previous_guid and identity and previous_guid != identity["guid"]:
                    raise ValueError(f"META_GUID_CHANGED {target}; restore its original .meta before syncing")
                if identity:
                    metadata[target] = identity
                else:
                    target_changed = True
                if destination.is_file():
                    if target not in target_cache:
                        target_cache[target] = fingerprint(destination, old.get("target_fingerprints", {}).get(target), args.force)
                    targets[target] = target_cache[target]
                    if not rec["jobs"] and targets[target]["sha256"] != expected_sha256:
                        target_changed = True
                    if rec["jobs"] and old.get("target_fingerprints", {}).get(target, {}).get("sha256") not in (None, targets[target]["sha256"]):
                        target_changed = True
                else:
                    target_changed = True
            recipe_changed = bool(rec["jobs"]) and rec.get("conversion_signature") != old.get("conversion_signature")
            must_apply = args.force or recipe_changed or target_changed or old.get("sync_status") not in ("SYNCED", "PARTIAL") or stamp["sha256"] != old.get("source", {}).get("sha256")
            # Existing copied leaves are authoritative even before a manifest exists.
            if not rec["jobs"] and not target_changed and not args.force and old.get("sync_status") in (None, "SYNCED", "PARTIAL"):
                must_apply = False
            if not must_apply:
                unchanged += 1
                files[relative] = {**rec, "source": stamp, "target_fingerprints": targets, "sync_status": old.get("sync_status", "SYNCED"),
                                   "target_metadata": metadata,
                                   "import_sha256": expected_sha256,
                                   "editability": old.get("editability", rec["editability"]),
                                   "field_editability": old.get("field_editability", [])}
                continue
            changed[rec["category"]] += 1
            if args.verbose:
                print(f"[{rec['category']}] {'WOULD_SYNC' if args.dry_run else 'SYNC'} {relative}")
            if args.dry_run:
                continue
            if not rec["jobs"]:
                if sprite_settings and payload is None:
                    payload = importer.leaf_payload(source, sprite_settings)
                for target in rec["targets"]:
                    destination = importer.checked_path(PROJECT, target)
                    importer.sync_leaf(source, destination, payload=payload)
                    import_paths.append(target)
            jobs.extend(rec["jobs"])
            files[relative] = {**rec, "source": stamp, "target_fingerprints": targets, "target_metadata": metadata,
                               "import_sha256": expected_sha256, "sync_status": "PENDING_IMPORT",
                               # A changed converter must regenerate, not just patch, its output; kept
                               # until acknowledged so an interrupted sync still rebuilds on retry.
                               "rebuild": recipe_changed or bool(old.get("rebuild"))}
            pending.append(relative)
        except (OSError, ValueError) as error:
            failed.append({"source": relative, "reason": str(error)})
            if not args.dry_run:
                files[relative] = {**old, **rec, "sync_status": "FAILED", "reason": str(error)}
    if not args.dry_run:
        # Persist the retry set before invoking Unity; a crash cannot falsely mark conversion complete.
        write_json(STATE, {**state, "generated": datetime.now(timezone.utc).isoformat(), "files": files})
        if pending and not args.copy_only:
            batches = conversion_batches(pending, files)
            for number, batch in enumerate(batches, 1):
                print(f"[batch] {number}/{len(batches)}: {len(batch)} source objects", flush=True)
                batch_jobs = [{**job, "rebuild": True} if files[relative].get("rebuild") else job
                              for relative in batch for job in files[relative]["jobs"]]
                batch_paths = sorted({target for relative in batch if not files[relative]["jobs"] for target in files[relative]["targets"]})
                result = apply_unity(batch_jobs, batch_paths, timeout=args.timeout, ensure_curtain_skin=True)
                assert_rebuilt(batch_jobs, result)
                acknowledge_batch(state, files, batch, result)
                # Only successful batches advance; a later crash retries the remaining sources.
                write_json(STATE, {**state, "files": files, "generated": datetime.now(timezone.utc).isoformat()})
        # A pull can change the tracked recipe while every res source remains unchanged. Run the
        # same Unity gate once even when there was no pending source conversion, so generated
        # GameContent cannot silently retain a collaborator's stale SpriteSkin metadata.
        if not args.dry_run and not args.copy_only and not pending:
            apply_unity([], [], timeout=args.timeout, ensure_curtain_skin=True)
    for category in CATEGORIES:
        if category != "OTHER":
            print(f"{category:<10} changed {changed[category]:6}")
    print(f"unchanged: {unchanged}; read_only: {readonly}; pending_import: {len(pending) if args.copy_only else 0}; failed: {len(failed)}")
    for item in failed[:20]:
        print(f"[FAIL] {item['source']}: {item['reason']}", file=sys.stderr)
    return 1 if failed else 0


def acknowledge_batch(state, files, batch, result):
    results_by_source = defaultdict(list)
    for job_result in result.get("jobs", []):
        results_by_source[job_result.get("source", "").replace("\\", "/").removeprefix("res/")].append(job_result)
    updated_targets, updated_metadata = {}, {}
    for relative in batch:
        rec = files[relative]
        rec.pop("rebuild", None)
        converted = results_by_source[relative]
        limited = any(j.get("editability") != "FULL" or "PARTIAL" in j.get("status", "") for j in converted)
        rec["sync_status"] = "PARTIAL" if limited or (result.get("status") == "PARTIAL" and rec["jobs"]) else "SYNCED"
        if converted:
            rec["editability"] = "READ_ONLY" if all(j.get("editability") == "READ_ONLY" for j in converted) else ("PARTIAL" if limited else "FULL")
            rec["field_editability"] = compact_fields(field for job in converted for field in job.get("fields", []))
        for target in rec["targets"]:
            if target not in updated_targets and importer.checked_path(PROJECT, target).is_file():
                updated_targets[target] = fingerprint(importer.checked_path(PROJECT, target), force=True)
                updated_metadata[target] = metadata_identity(importer.checked_path(PROJECT, target), force=True)
            identity = updated_metadata.get(target)
            before_guid = rec.get("target_metadata", {}).get(target, {}).get("guid")
            if not identity or (before_guid and before_guid != identity["guid"]):
                raise RuntimeError(f"Unity import did not preserve metadata GUID: {target}")
        rec["target_fingerprints"] = {target: updated_targets[target] for target in rec["targets"] if target in updated_targets}
        rec["target_metadata"] = {target: updated_metadata[target] for target in rec["targets"] if target in updated_metadata}
    # Each prefab has several editable source objects. One successful patch changes
    # their shared target; acknowledge it without invalidating all other source objects.
    for rec in files.values():
        for target in set(rec.get("targets", [])) & updated_targets.keys():
            rec.setdefault("target_fingerprints", {})[target] = updated_targets[target]
            previous_guid = rec.get("target_metadata", {}).get(target, {}).get("guid")
            if not previous_guid or previous_guid == updated_metadata[target]["guid"]:
                rec.setdefault("target_metadata", {})[target] = updated_metadata[target]
    state["unity_result"] = {k: v for k, v in result.items() if k != "jobs"}
    state["unity_result"]["job_count"] = len(result.get("jobs", []))


def import_content(args):
    game = Path(args.game).resolve()
    if not (game / "manosaba_Data").is_dir():
        raise FileNotFoundError(f"Missing readable game resource directory: {game / 'manosaba_Data'}")
    cache_args = [sys.executable, PIPELINE / "import_source_cache.py", "--game-root", game, "--res-root", RES]
    if args.overwrite_user_changes:
        cache_args.append("--overwrite-user-changes")
    run(cache_args)
    if args.reextract or not (RES / "_index.csv").is_file():
        import import_workspace
        import_workspace.extract_and_merge(game, args.overwrite_user_changes)
    else:
        print("[import] Adopting existing res workspace; edits retained. Use --reextract to refresh original resources.")
    restore_evidence()
    bootstrap = PIPELINE / "bootstrap_import.py"
    if bootstrap.is_file():
        run([sys.executable, bootstrap, "--timeout", str(args.timeout)])
    apply_unity([], [], export=True, timeout=args.timeout, ensure_curtain_skin=True)
    # Establish hashes only for original files we can prove; unknown baseline remains protected.
    baseline = read_json(BASELINE, {"version": 1, "files": {}})
    for relative, rec in inventory().items():
        source = source_path(relative)
        if source.is_file() and relative not in baseline["files"]:
            baseline["files"][relative] = {"sha256": None, "status": "USER_MODIFIED_OR_UNKNOWN"}
    write_json(BASELINE, baseline)
    args.category, args.file, args.force, args.dry_run, args.copy_only, args.verbose = "", [], False, False, False, False
    return sync(args)


def doctor(args):
    records = inventory()
    problems = []
    state = read_content_state(STATE, {"files": {}})
    target_stamps, metadata_stamps = {}, {}
    for relative, rec in records.items():
        source = source_path(relative)
        entry = state["files"].get(relative, {})
        saved_targets = entry.get("target_fingerprints", {})
        saved_metadata = entry.get("target_metadata", {})
        saved_targets = saved_targets if isinstance(saved_targets, dict) else {}
        saved_metadata = saved_metadata if isinstance(saved_metadata, dict) else {}
        if rec["targets"] and entry:
            if entry.get("sync_status") not in ("SYNCED", "PARTIAL", "FAILED", "PENDING_IMPORT"):
                problems.append(f"STATE_INVALID {relative}; run content sync")
            complete = valid_fingerprint(entry.get("source"))
            for target in rec["targets"]:
                baseline = saved_metadata.get(target)
                complete = complete and valid_fingerprint(saved_targets.get(target)) and valid_fingerprint(baseline)
                guid = baseline.get("guid") if isinstance(baseline, dict) else None
                complete = complete and isinstance(guid, str) and re.fullmatch(r"[0-9a-f]{32}", guid) is not None
            if not complete:
                problems.append(f"STATE_INCOMPLETE {relative}; run content sync")
        if rec["targets"] and not entry:
            problems.append(f"UNSYNCED {relative}; run content sync")
        elif entry and rec.get("conversion_signature") != entry.get("conversion_signature"):
            problems.append(f"CONVERSION_CHANGED {relative}; run content sync")
        elif entry and (rec.get("normalization_signature") != entry.get("normalization_signature") or
                        rec.get("sprite_settings") != entry.get("sprite_settings")):
            problems.append(f"NORMALIZATION_CHANGED {relative}; run content sync")
        if not source.is_file():
            problems.append(f"SOURCE_MISSING {relative}")
        for target in rec["targets"]:
            destination = importer.checked_path(PROJECT, target)
            if not destination.is_file():
                problems.append(f"TARGET_MISSING {target}")
            elif not Path(str(destination) + ".meta").is_file():
                problems.append(f"META_MISSING {target}")
            else:
                try:
                    if target not in metadata_stamps:
                        previous = saved_metadata.get(target)
                        metadata_stamps[target] = metadata_identity(destination, previous if valid_fingerprint(previous) else None, args.deep)
                    previous = saved_metadata.get(target)
                    expected_guid = previous.get("guid") if isinstance(previous, dict) else None
                    if expected_guid and metadata_stamps[target]["guid"] != expected_guid:
                        problems.append(f"META_GUID_CHANGED {target}; restore its original .meta")
                except (OSError, ValueError) as error:
                    problems.append(str(error))
            if destination.is_file() and valid_fingerprint(saved_targets.get(target)):
                if target not in target_stamps:
                    target_stamps[target] = fingerprint(destination, saved_targets[target], args.deep)
                if target_stamps[target]["sha256"] != saved_targets[target]["sha256"]:
                    problems.append(f"TARGET_CHANGED {target}; run content sync")
        if entry.get("sync_status") in ("FAILED", "PENDING_IMPORT"):
            problems.append(f"{entry['sync_status']} {relative}")
        if source.is_file() and valid_fingerprint(entry.get("source")) and fingerprint(source, entry["source"], args.deep)["sha256"] != entry["source"]["sha256"]:
            problems.append(f"SOURCE_CHANGED {relative}; run content sync")
    report = {"profile": "ContentPreflight", "status": "FAIL" if problems else "PASS",
              "resources": len(records), "editability": dict(Counter(state["files"].get(relative, {}).get("editability", rec["editability"]) for relative, rec in records.items())),
              "problems": problems, "original_game_required": False}
    if not args.no_report:
        write_json(PIPELINE / "out/content_doctor.json", report)
    print(json.dumps({k: v for k, v in report.items() if k != "problems"}, ensure_ascii=False))
    for problem in problems[:30]:
        print(problem)
    return 1 if problems else 0


def build(args):
    restore_evidence()
    # Sync before every build so res remains the only ordinary editing source.
    args.force, args.dry_run, args.copy_only, args.verbose = False, False, False, False
    if sync(args):
        return 1
    # Generated GameContent .meta files are local and are intentionally ignored by Git. Rebind every
    # Addressables entry from the all-plan asset_path before building, otherwise a clone can retain the
    # collaborator's stale GUIDs and silently drop most resources from the catalog.
    apply_unity([], [], build=True, timeout=args.timeout, setup_addressables=True,
                addressables_plan="addressables_plan_all.json", ensure_curtain_skin=True)
    if args.player:
        run(ps_script("g5_player_smoke.ps1"))
    return 0


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    commands = parser.add_subparsers(dest="command", required=True)
    imp = commands.add_parser("import", help="one-time resource import; retained edits are protected")
    imp.add_argument("game")
    imp.add_argument("--reextract", action="store_true")
    imp.add_argument("--overwrite-user-changes", action="store_true")
    imp.add_argument("--allow-resource-map-mismatch", action="store_true",
                     help="bypass the tracked dump identity check; regenerate and review the map afterward")
    sync_parser = commands.add_parser("sync", help="incrementally apply res edits to generated Unity assets")
    sync_parser.add_argument("--dry-run", action="store_true")
    sync_parser.add_argument("--force", action="store_true")
    sync_parser.add_argument("--category", default="")
    sync_parser.add_argument("--file", action="append", default=[])
    sync_parser.add_argument("--copy-only", action="store_true", help="stage changes without Unity, leaving an explicit retry set")
    sync_parser.add_argument("--verbose", action="store_true")
    sync_parser.add_argument("--allow-resource-map-mismatch", action="store_true",
                             help="bypass the tracked dump identity check")
    doc = commands.add_parser("doctor")
    doc.add_argument("--deep", action="store_true", help="rehash sources instead of trusting unchanged size/mtime")
    doc.add_argument("--no-report", action="store_true")
    bld = commands.add_parser("build", help="build current reconstruction Addressables settings")
    bld.add_argument("--player", action="store_true", help="also build and run the G5 Player smoke gate")
    bld.add_argument("--category", default="")
    bld.add_argument("--file", action="append", default=[], help="limit the pre-build sync; all configured Addressables still build")
    bld.add_argument("--allow-resource-map-mismatch", action="store_true",
                     help="bypass the tracked dump identity check")
    for command in (imp, sync_parser, bld):
        command.add_argument("--timeout", type=int, default=7200 if command is imp else 1800)
    args = parser.parse_args(argv)
    if args.command != "import":
        os.environ["MANOSABA_CONTENT_ONLY"] = "1"
    try:
        if args.command == "doctor":
            return doctor(args)
        if args.command == "sync" and args.dry_run:
            return sync(args)
        with workspace_lock():
            return {"import": import_content, "sync": sync, "build": build}[args.command](args)
    except (OSError, ValueError, RuntimeError, TimeoutError) as error:
        print(f"[content] FAIL: {error}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    sys.exit(main())
