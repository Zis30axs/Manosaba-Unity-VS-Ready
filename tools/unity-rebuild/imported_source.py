"""Resolve locally imported resource evidence; original installs are research-only.

The cache deliberately preserves the manosaba_Data layout, without the executable,
native runtime, IL2CPP metadata, or BepInEx. Reconstruction planners can keep their
existing relative bundle/resource paths after an import.
"""
from __future__ import annotations

import os
from pathlib import Path
import sys

REPO = Path(__file__).resolve().parents[2]


def res_root() -> Path:
    return Path(os.environ.get("MANOSABA_RES_ROOT") or REPO / "res").resolve()


def cache_root() -> Path:
    return res_root() / ".imported-source"


def game_root() -> str:
    """Prefer imported resources. Explicit research mode may fall back to the install."""
    cache = cache_root()
    if (cache / "manosaba_Data").is_dir():
        return str(cache)
    if os.environ.get("MANOSABA_CONTENT_ONLY") == "1":
        raise SystemExit("Imported source cache is missing; run tools/content/import.ps1 first")
    sys.path.insert(0, str(REPO / "tools" / "audit"))
    import config
    if config.GAME_ROOT and Path(config.GAME_ROOT).is_dir():
        return config.GAME_ROOT
    raise SystemExit("No imported resource cache or research game root; run tools/content/import.ps1")


def within(root: Path, relative: str) -> Path:
    """Refuse absolute paths, traversal, and links pointing outside an owned root."""
    rel = Path(relative.replace("\\", "/"))
    if rel.is_absolute() or ".." in rel.parts:
        raise ValueError(f"path escapes root: {relative!r}")
    target = (root / rel).resolve()
    target.relative_to(root.resolve())
    return target
