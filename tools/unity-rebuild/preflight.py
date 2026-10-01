#!/usr/bin/env python3
"""Preflight: prove which inputs exist before any reconstruction claim is made.

Section 3.1.5 of UNITY_READINESS_CODEX_AUDIT.md requires the original bundles, Player
SerializedFiles, resS/resource streams, catalog/settings, IL2CPP metadata/native binary,
the toolchain and Unity to be checked for existence, size, SHA-256, version and origin,
and requires a *precise* list when something is missing rather than an empty result.

Outputs (under tools/unity-rebuild/out/):
  preflight.json          machine-readable result, with per-item status
  preflight.md            human-readable summary
  input_manifest.csv      every verified input with size + SHA-256
  blocked.csv             every missing input with the exact path and what it blocks

Exit code is non-zero when any REQUIRED input is missing, so a batch build fails loudly.
"""
from __future__ import annotations

import collections
import argparse
import csv
import hashlib
import json
import os
import re
import sys
import time

HERE = os.path.dirname(os.path.abspath(__file__))
# The path configuration and identity helpers live in tools/audit so that the auditors and
# the integration pipeline share exactly one definition of every root.
AUDIT = os.path.join(os.path.dirname(HERE), "audit")
sys.path.insert(0, AUDIT)
sys.path.insert(0, HERE)
import config  # noqa: E402
from identity import sha256_file  # noqa: E402

OUT = config.OUTPUT_ROOT
GAME = config.GAME_ROOT
DATA = config.GAME_DATA_ROOT
AA = config.AA_ROOT
AAW = config.DERIVED["aa_platform_root"]

#: (kind, path, required, what it blocks)
def enumerate_inputs():
    items = []

    def add(kind, path, required, blocks):
        items.append({"kind": kind, "path": path, "required": required, "blocks": blocks})

    # --- original Player / IL2CPP -------------------------------------------------
    for name, blocks in (
        ("manosaba.exe", "original Player executable (reference only; NOT the new build)"),
        ("UnityPlayer.dll", "identifies the exact Unity runtime version"),
        ("GameAssembly.dll", "IL2CPP native binary; needed to resolve native method bodies"),
        ("baselib.dll", "IL2CPP runtime support"),
        ("manosaba_Data/boot.config", "Unity boot configuration"),
        ("manosaba_Data/app.info", "build identity (version/company)"),
        ("manosaba_Data/globalgamemanagers", "global asset settings (builtin resources, manager refs)"),
        ("manosaba_Data/ScriptingAssemblies.json", "assembly list actually shipped"),
        ("manosaba_Data/RuntimeInitializeOnLoads.json", "which types Unity instantiates at startup"),
    ):
        add("player", os.path.join(GAME, name.replace("/", os.sep)), True, blocks)

    # --- IL2CPP metadata ----------------------------------------------------------
    for name, blocks in (
        ("il2cpp_data/Metadata/global-metadata.dat", "type/method metadata for re-implementation"),
        ("il2cpp_data/Metadata/stringliteral.json", "string literals referenced by game code"),
    ):
        add("il2cpp", os.path.join(DATA, name.replace("/", os.sep)), False, blocks)

    # --- Player SerializedFiles + their streams ----------------------------------
    for name, blocks in (
        ("level0", "Boot scene (the scene that must load first)"),
        ("globalgamemanagers.assets", "builtin resources referenced by every bundle"),
        ("globalgamemanagers.assets.resS", "external stream for globalgamemanagers.assets"),
        ("resources.assets", "Resources/ objects loaded by path at runtime"),
        ("resources.assets.resS", "external stream for resources.assets"),
        ("sharedassets0.assets", "shared assets for level0"),
        ("sharedassets0.assets.resS", "external stream for sharedassets0.assets"),
    ):
        add("serializedfile", os.path.join(DATA, name), True, blocks)

    # --- Addressables ------------------------------------------------------------
    add("addressables", os.path.join(AA, "catalog.bin"), True, "every Addressables key/location")
    add("addressables", os.path.join(AA, "settings.json"), True, "Addressables runtime settings")
    add("addressables", os.path.join(AA, "catalog.hash"), False, "catalog integrity check")
    add("addressables", os.path.join(AA, "AddressablesLink", "link.xml"), False,
        "IL2CPP link preservation list")
    return items


def scan_bundles():
    """One record per physical bundle, keyed by relative path (never by basename)."""
    records = []
    if not AAW or not os.path.isdir(AAW):
        return records, {"error": f"platform root missing: {AAW}"}
    total = 0
    streamed = 0
    for root, dirs, files in os.walk(AAW):
        for f in sorted(files):
            full = os.path.join(root, f)
            rel = os.path.relpath(full, AAW).replace(os.sep, "/")
            size = os.path.getsize(full)
            total += size
            entry = {"rel": rel, "size": size, "is_bundle": f.endswith(".bundle")}
            if not f.endswith(".bundle"):
                streamed += 1
                entry["note"] = "not a .bundle file (streams/link data live here)"
            records.append(entry)
    return records, {"count": len(records), "bundle_count": sum(1 for r in records if r["is_bundle"]),
                     "other_count": streamed, "total_bytes": total}


def tool_versions():
    import platform
    import subprocess

    info = {
        "python": sys.version.split()[0],
        "python_exe": sys.executable,
        "platform": platform.platform(),
        "machine": platform.machine(),
    }
    for name, cmd in (("dotnet", ["dotnet", "--version"]),
                      ("git", ["git", "--version"]),
                      ("node", ["node", "--version"])):
        try:
            proc = subprocess.run(cmd, capture_output=True, timeout=60,
                                  env={**os.environ, "PYTHONUTF8": "1"})
            info[name] = proc.stdout.decode("utf-8", "replace").strip() or None
        except (OSError, subprocess.SubprocessError) as exc:
            info[name] = f"unavailable: {type(exc).__name__}"
    try:
        import UnityPy
        info["UnityPy"] = getattr(UnityPy, "__version__", "unknown")
    except ImportError as exc:
        info["UnityPy"] = f"unavailable: {exc}"
    return info


def unity_info():
    out = {"configured_path": config.UNITY_EDITOR_PATH or None, "found": None, "version": None,
           "candidates": []}
    candidates = []
    if config.UNITY_EDITOR_PATH:
        candidates.append(config.UNITY_EDITOR_PATH)
    for root in (r"E:\Unity", r"C:\Program Files\Unity\Hub\Editor", r"D:\Unity", r"E:\Unity\Hub\Editor"):
        if os.path.isdir(root):
            for name in os.listdir(root):
                exe = os.path.join(root, name, "Editor", "Unity.exe")
                if os.path.exists(exe):
                    candidates.append(exe)
    out["candidates"] = candidates
    for exe in candidates:
        if os.path.exists(exe):
            out["found"] = exe
            version_file = os.path.join(os.path.dirname(exe), "Data", "PackageManager",
                                        "UnityEditor", "UnityEditor.dll")
            m = re.search(r"[\\/](\d+\.\d+\.\d+[a-z]\d+)[\\/]", exe)
            out["version"] = m.group(1) if m else None
            out["editor_dll_present"] = os.path.exists(version_file)
            break
    return out


def content_preflight(mode, compute_hash=False):
    """Content/build input checks are intentionally separate from research inputs.

    No original Player, DLL, IL2CPP metadata, or BepInEx path is enumerated here.
    Only locally editable resources and this Unity project participate.
    """
    from pathlib import Path
    from imported_source import res_root
    repo = Path(HERE).parents[1]
    res = res_root()
    project = repo / "UnityProject"
    required = [
        ("content_index", res / "_index.csv", "indexed editable resource sync"),
        ("editable_content", res / "Assets", "editable game resources"),
    ]
    if mode == "build":
        required += [
            ("unity_project", project / "ProjectSettings/ProjectVersion.txt", "Unity project compilation"),
            ("unity_packages", project / "Packages/manifest.json", "Unity package resolution"),
            ("imported_content", project / "Assets/GameContent", "Addressables content build"),
            ("imported_locale", res / ".imported-source/manosaba_Data/StreamingAssets/config.json", "locale selection"),
        ]
    rows = []
    for kind, path, blocks in required:
        row = {"kind": kind, "path": str(path), "required": True,
               "blocks": blocks, "exists": path.exists(), "size": None}
        if path.is_file():
            row["size"] = path.stat().st_size
            if compute_hash:
                row["sha256"] = sha256_file(str(path))[0]
        rows.append(row)
    missing = [r for r in rows if not r["exists"]]
    editor = unity_info() if mode == "build" else {"found": None, "version": None}
    result = {"mode": mode, "status": "BLOCKED" if missing else "INPUTS_PRESENT",
              "generated": time.strftime("%Y-%m-%dT%H:%M:%S"), "inputs": rows,
              "missing_required_count": len(missing), "missing_optional_count": 0,
              "unity": editor, "bundles": {"bundle_count": 0},
              "roots": {"res_root": str(res), "unity_project": str(project)},
              "original_game_inputs_required": False}
    os.makedirs(OUT, exist_ok=True)
    with open(os.path.join(OUT, f"preflight_{mode}.json"), "w", encoding="utf-8") as fh:
        json.dump(result, fh, ensure_ascii=False, indent=2)
    lines = [f"# {mode.title()} preflight", "", f"Status: **{result['status']}**", "",
             "Original game executables, IL2CPP binaries and BepInEx are not inputs.", ""]
    lines += [f"- {'OK' if r['exists'] else 'BLOCKED'} `{r['path']}`: {r['blocks']}" for r in rows]
    with open(os.path.join(OUT, f"preflight_{mode}.md"), "w", encoding="utf-8") as fh:
        fh.write("\n".join(lines) + "\n")
    print(json.dumps(result, ensure_ascii=False, indent=2))
    return 1 if missing else 0


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--mode", choices=("research", "content", "build"), default="research")
    parser.add_argument("--force", action="store_true")
    parser.add_argument("--hash", action="store_true")
    args = parser.parse_args()
    if args.mode != "research":
        return content_preflight(args.mode, args.hash or args.force)
    force = "--force" in sys.argv
    compute_hash = "--hash" in sys.argv or force
    os.makedirs(OUT, exist_ok=True)
    started = time.time()

    inputs = enumerate_inputs()
    rows = []
    blocked = []
    for item in inputs:
        path = item["path"]
        exists = bool(path) and os.path.exists(path)
        record = dict(item)
        record["exists"] = exists
        if exists:
            record["size"] = os.path.getsize(path)
            if compute_hash:
                try:
                    digest, size = sha256_file(path)
                    record["sha256"] = digest
                    record["size"] = size
                except OSError as exc:
                    record["sha256"] = None
                    record["error"] = f"{type(exc).__name__}: {exc}"
            else:
                record["sha256"] = "(not computed; pass --hash)"
        else:
            record["size"] = None
            record["sha256"] = None
            blocked.append({"kind": item["kind"], "path": path, "required": item["required"],
                            "blocks": item["blocks"],
                            "status": "BLOCKED" if item["required"] else "OPTIONAL_MISSING"})
        rows.append(record)

    bundles, bundle_stats = scan_bundles()
    if compute_hash:
        for entry in bundles:
            full = os.path.join(AAW, entry["rel"].replace("/", os.sep))
            try:
                digest, size = sha256_file(full)
                entry["sha256"] = digest
                entry["size"] = size
            except OSError as exc:
                entry["sha256"] = None
                entry["error"] = f"{type(exc).__name__}: {exc}"
    else:
        for entry in bundles:
            entry["sha256"] = "(not computed; pass --hash)"

    missing_required = [b for b in blocked if b["required"]]
    result = {
        "generated": time.strftime("%Y-%m-%dT%H:%M:%S"),
        "elapsed_seconds": round(time.time() - started, 2),
        "config_sources": config.CFG_SOURCES or ["(defaults only)"],
        "roots": dict(config.describe()),
        "tools": tool_versions(),
        "unity": unity_info(),
        "inputs": rows,
        "bundles": bundle_stats,
        "bundle_files": bundles,
        "missing_required_count": len(missing_required),
        "missing_optional_count": len(blocked) - len(missing_required),
        "status": "BLOCKED" if missing_required else "INPUTS_PRESENT",
    }
    with open(os.path.join(OUT, "preflight.json"), "w", encoding="utf-8") as fh:
        json.dump(result, fh, ensure_ascii=False, indent=1)

    with open(os.path.join(OUT, "input_manifest.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["kind", "path", "exists", "size", "sha256", "required", "blocks"])
        for r in rows:
            w.writerow([r["kind"], r["path"], r["exists"], r["size"], r["sha256"],
                        r["required"], r["blocks"]])
        for b in bundles:
            full = os.path.join(AAW, b["rel"].replace("/", os.sep))
            w.writerow(["assetbundle", full, True, b["size"], b["sha256"], True,
                        "Addressables content"])

    with open(os.path.join(OUT, "blocked.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["status", "kind", "path", "required", "blocks"])
        for b in blocked:
            w.writerow([b["status"], b["kind"], b["path"], b["required"], b["blocks"]])

    lines = [
        "# Input preflight",
        "",
        f"- generated: {result['generated']}  ({result['elapsed_seconds']}s)",
        f"- status: **{result['status']}**",
        f"- config sources: {', '.join(result['config_sources'])}",
        f"- Unity editor: {result['unity']['found'] or 'NOT FOUND'} "
        f"(version {result['unity']['version'] or 'UNKNOWN'})",
        "",
        "## Roots",
        "",
    ]
    for key, value in config.describe():
        lines.append(f"- `{key}` = `{value}`")
    lines += ["", "## Required inputs", "",
              "| kind | exists | size | path |", "|---|---|---|---|"]
    for r in rows:
        if r["required"]:
            lines.append(f"| {r['kind']} | {r['exists']} | {r['size']} | `{r['path']}` |")
    lines += ["", "## AssetBundles", "",
              f"- files under platform root: {bundle_stats.get('count')}",
              f"- `.bundle` files: {bundle_stats.get('bundle_count')}",
              f"- other files: {bundle_stats.get('other_count')}",
              f"- total bytes: {bundle_stats.get('total_bytes')}",
              "", "## Missing", ""]
    if blocked:
        for b in blocked:
            lines.append(f"- **{b['status']}** `{b['path']}` ({b['kind']}) -> blocks: {b['blocks']}")
    else:
        lines.append("- none")
    with open(os.path.join(OUT, "preflight.md"), "w", encoding="utf-8") as fh:
        fh.write("\n".join(lines) + "\n")

    print(json.dumps({
        "status": result["status"],
        "inputs_checked": len(rows),
        "missing_required": len(missing_required),
        "missing_optional": len(blocked) - len(missing_required),
        "bundles": bundle_stats,
        "unity": result["unity"]["found"],
        "hash_computed": compute_hash,
    }, ensure_ascii=False, indent=1))
    for b in blocked:
        print(f"  {b['status']:16s} {b['path']}")
    return 1 if missing_required else 0


if __name__ == "__main__":
    sys.exit(main())
