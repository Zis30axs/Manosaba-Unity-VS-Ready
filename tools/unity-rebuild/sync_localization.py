"""Synchronize editable Chinese localization text from res/ into the Unity project.

The extraction tree under res/ is NOT read by Unity directly.  This tool copies only the
small zh-Hans/Text subtree into Assets/GameContent so edits made in IDEA can reach the Unity
project without rerunning the full asset import.

Examples
--------
# One-shot incremental sync (default: all zh-Hans text, including Scripts/)
python tools/unity-rebuild/sync_localization.py

# Keep IDEA edits mirrored into Unity while you work
python tools/unity-rebuild/sync_localization.py --watch

# Sync and rebuild the core Addressables content so BundledAssetProvider sees the change
python tools/unity-rebuild/sync_localization.py --rebuild

# Only mirror one relative file
python tools/unity-rebuild/sync_localization.py --file Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt

# Preview without writing anything
python tools/unity-rebuild/sync_localization.py --dry-run
"""
from __future__ import annotations

import argparse
import os
from pathlib import Path
import shutil
import subprocess
import sys
import time
from typing import Iterable

REPO = Path(__file__).resolve().parents[2]
SOURCE_ROOT = REPO / "res" / "Assets" / "#WitchTrials" / "Localization" / "zh-Hans" / "Text"
TARGET_ROOT = REPO / "UnityProject" / "Assets" / "GameContent" / "#WitchTrials" / "Localization" / "zh-Hans" / "Text"
G3_SCRIPT = REPO / "tools" / "unity-rebuild" / "g3_content_build.ps1"


def same_bytes(a: Path, b: Path) -> bool:
    """Cheap size check followed by a byte comparison; localization files are small."""
    if not b.is_file():
        return False
    if a.stat().st_size != b.stat().st_size:
        return False
    with a.open("rb") as fa, b.open("rb") as fb:
        while True:
            ba = fa.read(1024 * 1024)
            bb = fb.read(1024 * 1024)
            if ba != bb:
                return False
            if not ba:
                return True


def iter_sources(relative_file: str | None) -> Iterable[Path]:
    if relative_file:
        rel = Path(relative_file.replace("\\", "/"))
        if rel.is_absolute() or ".." in rel.parts:
            raise ValueError("--file must be a relative path below Localization/zh-Hans/Text")
        src = SOURCE_ROOT / rel
        if not src.is_file():
            raise FileNotFoundError(src)
        yield src
        return

    if not SOURCE_ROOT.is_dir():
        raise FileNotFoundError(SOURCE_ROOT)

    for src in sorted(SOURCE_ROOT.rglob("*")):
        if src.is_file() and src.suffix.lower() == ".txt":
            yield src


def sync_once(relative_file: str | None, dry_run: bool, quiet_unchanged: bool = False) -> list[Path]:
    changed: list[Path] = []
    checked = 0
    for src in iter_sources(relative_file):
        checked += 1
        rel = src.relative_to(SOURCE_ROOT)
        dst = TARGET_ROOT / rel
        if same_bytes(src, dst):
            continue

        changed.append(rel)
        action = "WOULD_SYNC" if dry_run else "SYNC"
        print(f"[localization] {action}: {rel}")
        if not dry_run:
            dst.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(src, dst)

    if changed:
        print(f"[localization] changed={len(changed)} checked={checked} target={TARGET_ROOT}")
    elif not quiet_unchanged:
        print(f"[localization] up to date: checked={checked}")

    return changed


def rebuild_core() -> int:
    if not G3_SCRIPT.is_file():
        print(f"[localization] cannot rebuild: missing {G3_SCRIPT}", file=sys.stderr)
        return 2

    pwsh = shutil.which("pwsh") or shutil.which("powershell")
    if not pwsh:
        print("[localization] cannot rebuild: pwsh/powershell not found", file=sys.stderr)
        return 2

    cmd = [pwsh, "-NoProfile", "-File", str(G3_SCRIPT), "-Group", "core"]
    print("[localization] rebuilding core Addressables:")
    print("  " + subprocess.list2cmdline(cmd))
    return subprocess.run(cmd, cwd=REPO).returncode


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--file",
        help="sync one path relative to Localization/zh-Hans/Text, e.g. Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt",
    )
    parser.add_argument("--dry-run", action="store_true", help="show what would be copied without writing")
    parser.add_argument("--watch", action="store_true", help="poll for changes until Ctrl+C")
    parser.add_argument("--interval", type=float, default=0.75, help="watch polling interval in seconds (default: 0.75)")
    parser.add_argument(
        "--rebuild",
        action="store_true",
        help="after a sync with changes, run g3_content_build.ps1 -Group core so built Addressables pick them up",
    )
    args = parser.parse_args()

    if args.interval <= 0:
        parser.error("--interval must be > 0")
    if args.watch and args.dry_run:
        parser.error("--watch and --dry-run cannot be used together")

    try:
        changed = sync_once(args.file, args.dry_run)
    except (FileNotFoundError, ValueError) as exc:
        print(f"[localization] ERROR: {exc}", file=sys.stderr)
        return 2

    if args.rebuild and changed and not args.dry_run:
        code = rebuild_core()
        if code:
            return code

    if not args.watch:
        return 0

    print(f"[localization] watching {SOURCE_ROOT} every {args.interval:g}s (Ctrl+C to stop)")
    try:
        while True:
            time.sleep(args.interval)
            try:
                changed = sync_once(args.file, False, quiet_unchanged=True)
            except (FileNotFoundError, ValueError) as exc:
                print(f"[localization] ERROR: {exc}", file=sys.stderr)
                continue
            if args.rebuild and changed:
                code = rebuild_core()
                if code:
                    print(f"[localization] Addressables rebuild failed with exit {code}", file=sys.stderr)
    except KeyboardInterrupt:
        print("\n[localization] stopped")
        return 0


if __name__ == "__main__":
    raise SystemExit(main())
