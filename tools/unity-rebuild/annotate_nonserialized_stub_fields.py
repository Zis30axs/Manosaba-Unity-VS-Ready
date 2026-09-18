"""Mark stub-typed fields `[NonSerialized]` so the content build can serialize the prefabs that carry
them.

Run this *after* the conversion and *before* apply_game_sources.py (it patches both the converted
sources and the already-applied copies, and is idempotent):

    python tools/unity-rebuild/annotate_nonserialized_stub_fields.py            # dry run
    python tools/unity-rebuild/annotate_nonserialized_stub_fields.py --write

Why (measured, not assumed -- see UNITY_RUNTIME_INTEGRATION.md section 18):

* The Addressables content build aborts on the first component whose field type is a *stub* type:
  "Invalid type UniRx.ReactiveProperty`1<int> for instance field WitchTrials.Views.SaveLoadUi:
  _loadingSlotCount" (AddressableAssetEntry.GatherMainAndReferencedSerializedTypes ->
  ContentBuildInterface.GetTypeForObjects), and with that prefab removed it aborts instead on
  "TypeLoadException: VTable setup of type UniRx.CompositeDisposable failed"
  (ContentBuildInterface.WriteSerializedFile).
* UniRx (and UniTask/LitMotion/TrueShadow/MPUIKit/UIPolygon/FastEnum) exist in this reconstruction
  only as Il2CppDumper stub assemblies (Assets/Plugins/Il2CppStubs/*.dll): their metadata is
  incomplete, so Unity cannot build a type tree for those types and Mono cannot set up their vtables.
* The shipped serialized data does **not** contain these fields: the dumps in res/_serialized (the
  authority for what the real build serialized) list e.g. SaveLoadUi's serialized keys as
  `_deleteSaveSlotDialogData, _fadeColorOnLoad, _fadeInLoadDurationOnLoad, _loadDelay, _loadSfxPath,
  _naninovelScreen, _overwriteSaveSlotDialogData, _panelReferences, _saveGameTimeout, _saveSfxPath,
  adaptToInputMode, allowedSamplers, blockInputWhenVisible` -- no `_loadingSlotCount`, and Pagination's
  as `_currentPageLabel, _indicatorStyle, _indicators, _nextPageButton, _originalDot,
  _previousPageButton, _totalPageLabel, _wrapped` -- no `_currentPageIndex`.  Marking them
  [NonSerialized] therefore removes nothing the real game serialized; it makes the reconstruction
  agree with the shipped data instead of fighting it.

The unblock path that removes the need for this script is a real UniRx dependency (the game shipped a
real UniRx; only the stub is available here).
"""
from __future__ import annotations

import argparse
import os
import re
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
TREES = [
    os.path.join(REPO, "tools", "unity-rebuild", "converted", "src", "decompiled"),
    os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game"),
]

#: field types that only exist as Il2CppDumper stubs in this reconstruction
STUB_TYPE = (
    r"(?:UniRx\.)?(?:ReadOnly|Float|Bool|Int|String|Double|Long|Byte|Vector2|Vector3|Vector4|Color|"
    r"Quaternion|Rect|TimeSpan)?ReactiveProperty(?:<[^;=]+>)?"
    r"|(?:UniRx\.)?(?:Composite|Serial|SingleAssignment|MultipleAssignment|Cancellation)Disposable"
    r"|(?:UniRx\.)?I?Subject(?:<[^;=]+>)?"
    r"|(?:UniRx\.)?IReactiveProperty(?:<[^;=]+>)?"
    r"|(?:UniRx\.)?ReactiveCommand(?:<[^;=]+>)?"
    r"|LeTai\.TrueShadow\.TrueShadow|TrueShadow"
    r"|MPUIKIT\.MPImage|MPImage"
    r"|UIPolygon\.PolygonImage|PolygonImage"
    r"|LitMotion\.\w+(?:<[^;=]+>)?|MotionHandle"
)

#: a field declaration of such a type, with an access modifier (properties and locals never match)
FIELD = re.compile(
    r"^(?P<indent>[ \t]*)(?P<mods>(?:private|protected|internal)[ \t]+(?:readonly[ \t]+)?)"
    r"(?P<type>" + STUB_TYPE + r")[ \t]+(?P<name>[A-Za-z_][A-Za-z0-9_]*)[ \t]*(?P<tail>[;=])",
    re.MULTILINE,
)
ATTRIBUTE = "[NonSerialized]"
QUALIFIED_ATTRIBUTE = "[System.NonSerialized]"
HAS_ATTRIBUTE = re.compile(r"\[NonSerialized\]|\[System\.NonSerialized\]|NonSerializedAttribute")
USING_SYSTEM = re.compile(r"^\s*using\s+System\s*;", re.MULTILINE)


def patch(text: str):
    """Return (new_text, patched_count); already-annotated fields are left alone.

    The attribute is written as `[NonSerialized]` only when the file imports System; otherwise it is
    qualified as `[System.NonSerialized]`, so no file gains an implicit dependency on a using
    directive it does not have.
    """
    attribute = ATTRIBUTE if USING_SYSTEM.search(text) else QUALIFIED_ATTRIBUTE
    out = []
    patched = 0
    for line in text.splitlines(keepends=True):
        match = FIELD.match(line)
        if match:
            stripped_previous = out[-1].strip() if out else ""
            if HAS_ATTRIBUTE.search(stripped_previous):
                # normalize an annotation written by an earlier run of this script
                if stripped_previous != attribute and stripped_previous in (ATTRIBUTE, QUALIFIED_ATTRIBUTE):
                    out[-1] = match.group("indent") + attribute + "\n"
            else:
                out.append(match.group("indent") + attribute + "\n")
                patched += 1
        out.append(line)
    return "".join(out), patched


def revert(text: str):
    """Remove the attribute lines this script inserts (a line that is exactly the attribute)."""
    inserted = {ATTRIBUTE, QUALIFIED_ATTRIBUTE}
    out = []
    removed = 0
    for line in text.splitlines(keepends=True):
        if line.strip() in inserted:
            removed += 1
            continue
        out.append(line)
    return "".join(out), removed


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true", help="actually rewrite the files (default: dry run)")
    ap.add_argument("--revert", action="store_true",
                    help="remove the annotations this script inserts (for undoing an ineffective attempt)")
    args = ap.parse_args()

    total_files = 0
    total_fields = 0
    for tree in TREES:
        if not os.path.isdir(tree):
            print(f"skip (missing): {tree}")
            continue
        for root, _dirs, files in os.walk(tree):
            for name in sorted(files):
                if not name.endswith(".cs"):
                    continue
                path = os.path.join(root, name)
                with open(path, encoding="utf-8-sig", newline="") as fh:
                    original = fh.read()
                if args.revert:
                    updated, patched = revert(original)
                else:
                    updated, patched = patch(original)
                if updated == original:
                    continue
                if patched:
                    total_files += 1
                    total_fields += patched
                    rel = os.path.relpath(path, REPO)
                    print(f"  {patched:3} field(s)  {rel}")
                if args.write:
                    with open(path, "w", encoding="utf-8", newline="") as fh:
                        fh.write(updated)

    mode = "REVERTED" if args.revert else ("WROTE" if args.write else "DRY RUN")
    print(f"{mode}: {total_fields} field(s) in {total_files} file(s)")
    return 0


if __name__ == "__main__":
    sys.exit(main())
