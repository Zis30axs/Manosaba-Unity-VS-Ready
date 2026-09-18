"""Path / environment configuration for the audit + integration tooling.

The previous revision of this package hard-coded `E:\\Manosaba-VSCodeReady` and the
Xunlei game directory inside `common.py`, which made the auditors unrunnable on any
other machine (UNITY_READINESS_CODEX_AUDIT.md UR-012 / section 3.1.4).

Resolution order for every root, highest priority first:

  1. explicit environment variable  (e.g. ``MANOSABA_RES_ROOT``)
  2. ``tools/audit/audit.config.json``      (versioned, machine independent)
  3. ``tools/audit/audit.config.local.json``(git-ignored, machine specific)
  4. built-in default derived from the repository layout

Local absolute paths must never be committed.  Only the ``.local.json`` file may
hold machine-specific absolute paths, and it is listed in ``.gitignore``.
"""
from __future__ import annotations

import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
TOOLS = os.path.dirname(HERE)
REPO_DEFAULT = os.path.dirname(TOOLS)

CONFIG_NAME = "audit.config.json"
LOCAL_CONFIG_NAME = "audit.config.local.json"

#: env var -> config key
_ENV_KEYS = {
    "MANOSABA_REPO_ROOT": "repo_root",
    "MANOSABA_RES_ROOT": "res_root",
    "MANOSABA_GAME_ROOT": "game_root",
    "MANOSABA_GAME_DATA_ROOT": "game_data_root",
    "MANOSABA_AA_ROOT": "aa_root",
    "MANOSABA_SOURCE_ROOT": "source_root",
    "MANOSABA_DUMP_ROOT": "dump_root",
    "MANOSABA_OUTPUT_ROOT": "output_root",
    "MANOSABA_UNITY_EDITOR": "unity_editor_path",
    "MANOSABA_PYTHON": "python_exe",
}


def _read_json(path):
    try:
        with open(path, encoding="utf-8") as fh:
            data = json.load(fh)
    except FileNotFoundError:
        return {}
    except (OSError, ValueError) as exc:
        print(f"[config] WARNING: cannot read {path}: {exc}", file=sys.stderr)
        return {}
    if not isinstance(data, dict):
        print(f"[config] WARNING: {path} is not a JSON object; ignored", file=sys.stderr)
        return {}
    return data


def _norm(p):
    return os.path.normpath(os.path.expanduser(str(p))) if p else None


def load_config():
    """Return the merged configuration dict plus the files that contributed."""
    merged = {}
    sources = []
    for name in (CONFIG_NAME, LOCAL_CONFIG_NAME):
        path = os.path.join(HERE, name)
        data = _read_json(path)
        if data:
            sources.append(path)
        for key, value in data.items():
            if value not in (None, ""):
                merged[key] = _norm(value)
    for env, key in _ENV_KEYS.items():
        value = os.environ.get(env)
        if value:
            merged[key] = _norm(value)
            sources.append(f"env:{env}")
    return merged, sources


CFG, CFG_SOURCES = load_config()

REPO_ROOT = CFG.get("repo_root") or REPO_DEFAULT


def _repo_relative(value):
    """Absolutise a config path against the repo root.

    audit.config.json deliberately holds machine-independent, repo-relative defaults
    ("output_root": "tools/unity-rebuild/out").  Left relative, such a path resolves against the
    *caller's* working directory: running a07_addressables.py from tools/audit made it look for
    tools/audit/tools/unity-rebuild/out/unity/catalog_dump.json and silently report the Unity
    runtime dump as absent (observed 2026-09-17).
    """
    if not value or os.path.isabs(value):
        return value
    return os.path.normpath(os.path.join(REPO_ROOT, value))


RES_ROOT = _repo_relative(CFG.get("res_root")) or os.path.join(REPO_ROOT, "res")
GAME_ROOT = _repo_relative(CFG.get("game_root")) or ""
GAME_DATA_ROOT = _repo_relative(CFG.get("game_data_root")) or (
    os.path.join(GAME_ROOT, "manosaba_Data") if GAME_ROOT else ""
)
AA_ROOT = _repo_relative(CFG.get("aa_root")) or (
    os.path.join(GAME_DATA_ROOT, "StreamingAssets", "aa") if GAME_DATA_ROOT else ""
)
SOURCE_ROOT = _repo_relative(CFG.get("source_root")) or os.path.join(REPO_ROOT, "src")
DUMP_ROOT = _repo_relative(CFG.get("dump_root")) or os.path.join(REPO_ROOT, "work")
OUTPUT_ROOT = _repo_relative(CFG.get("output_root")) or os.path.join(REPO_ROOT, "tools", "unity-rebuild", "out")
UNITY_EDITOR_PATH = _repo_relative(CFG.get("unity_editor_path")) or ""
PYTHON_EXE = CFG.get("python_exe") or sys.executable


def derived():
    """Paths that depend on the roots above."""
    aaw = os.path.join(AA_ROOT, "StandaloneWindows64") if AA_ROOT else ""
    return {
        "aa_platform_root": aaw,
        "catalog_bin": os.path.join(AA_ROOT, "catalog.bin") if AA_ROOT else "",
        "settings_json": os.path.join(AA_ROOT, "settings.json") if AA_ROOT else "",
        "audit_out": os.path.join(REPO_ROOT, "tools", "audit", "out"),
    }


DERIVED = derived()

# Kept for backwards compatibility with the a01..a08 scripts written against the
# pre-existing module level constants.
REPO, RES, GAME, DATA, AA = REPO_ROOT, RES_ROOT, GAME_ROOT, GAME_DATA_ROOT, AA_ROOT
AAW = DERIVED["aa_platform_root"]
OUT = DERIVED["audit_out"]
SOURCE = SOURCE_ROOT

UNITY_YAML_MAGIC = b"%YAML 1.1"
UNITY_YAML_TAG = b"tag:unity3d.com,2011"
BUNDLE_MAGICS = (b"UnityFS", b"UnityWeb", b"UnityRaw", b"RawBundle")
ADDR_MAGIC = 0x0DE38942


class MissingInput(Exception):
    """Raised when a required input root is absent; callers must report BLOCKED."""


def require(path, what):
    """Return ``path`` if it exists, else raise :class:`MissingInput` with the exact path."""
    if not path:
        raise MissingInput(f"{what}: no path configured (set MANOSABA_* env or audit.config.json)")
    if not os.path.exists(path):
        raise MissingInput(f"{what}: not found at {path}")
    return path


def resolve_within(root, relative):
    """Resolve ``relative`` under ``root``, refusing to escape the root."""
    if not root:
        raise MissingInput("root not configured")
    norm_rel = str(relative).replace("\\", "/").lstrip("/")
    full = os.path.normpath(os.path.join(root, norm_rel.replace("/", os.sep)))
    root_norm = os.path.normpath(root)
    if os.path.commonpath([root_norm, full]) != root_norm:
        raise ValueError(f"path escapes root: {relative!r}")
    return full


def ensure_out(path=None):
    target = path or OUTPUT_ROOT
    os.makedirs(target, exist_ok=True)
    return target


def out_path(name):
    """Path of a named artifact inside the audit output directory."""
    target = ensure_out(DERIVED["audit_out"])
    return os.path.join(target, name)


def describe():
    rows = [
        ("repo_root", REPO_ROOT),
        ("res_root", RES_ROOT),
        ("game_root", GAME_ROOT),
        ("game_data_root", GAME_DATA_ROOT),
        ("aa_root", AA_ROOT),
        ("aa_platform_root", AAW),
        ("source_root", SOURCE_ROOT),
        ("output_root", OUTPUT_ROOT),
        ("unity_editor", UNITY_EDITOR_PATH or "(not configured)"),
        ("python", PYTHON_EXE),
    ]
    return rows


if __name__ == "__main__":
    print("config sources:", CFG_SOURCES or "(defaults only)")
    for key, value in describe():
        mark = "OK " if value and os.path.exists(value) else ("" if value else "?  ")
        print(f"  {mark} {key:18s} {value}")
