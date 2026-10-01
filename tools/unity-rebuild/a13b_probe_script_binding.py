"""Probe: resolve a bundled MonoBehaviour's m_Script PPtr to a (namespace, class, assembly).

The composite converters (ScriptableObject, prefab) must rewrite every script reference onto *our*
MonoScript assets.  In the shipped build the reference is a PPtr -- (m_FileID, m_PathID) -- where
m_FileID indexes the referencing SerializedFile's externals table, and the target is a MonoScript
object whose m_ClassName/m_Namespace/m_AssemblyName identify the type.

This probe answers the one open question: can that chain be walked from the shipped bundles with
UnityPy alone?  Run: python tools/unity-rebuild/a13b_probe_script_binding.py
"""
from __future__ import annotations

import os
import sys

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))


def game_root():
    from imported_source import game_root as imported_game_root
    return imported_game_root()


GAME = game_root()
AA = os.path.join(GAME, "manosaba_Data", "StreamingAssets", "aa")
BUNDLE = os.path.join(AA, "StandaloneWindows64", "general-data_assets_all.bundle")
PER_SOURCE = os.path.join(REPO, "tools", "audit", "out", "a03", "per_source")


def build_cab_index():
    """CAB name -> bundle file, from the audit's per-source scan (rel + serialized_files).

    `rel` in those dumps is relative to <game>/manosaba_Data, not to the game root.
    """
    import json
    index = {}
    if not os.path.isdir(PER_SOURCE):
        return index
    data_root = os.path.join(GAME, "manosaba_Data")
    for name in os.listdir(PER_SOURCE):
        if not name.endswith(".json"):
            continue
        try:
            with open(os.path.join(PER_SOURCE, name), encoding="utf-8") as fh:
                data = json.load(fh)
        except (OSError, ValueError):
            continue
        rel = (data.get("rel") or "").replace("/", os.sep)
        if not rel:
            continue
        full = os.path.join(data_root, rel)
        if not os.path.exists(full):
            continue
        for cab in data.get("serialized_files") or []:
            cab_name = cab if isinstance(cab, str) else cab.get("name")
            if cab_name:
                index.setdefault(cab_name.lower(), full)
    return index


def main():
    if not os.path.exists(BUNDLE):
        print("bundle missing:", BUNDLE)
        return 2

    env = UnityPy.load(BUNDLE)
    print(f"objects in bundle: {len(env.objects)}")
    print(f"cabs: {list(getattr(env, 'cabs', {}).keys())[:5]}")

    externals_by_file = {}
    monoscripts = {}          # (cab, path_id) -> (namespace, class, assembly)
    monobehaviours = []

    for obj in env.objects:
        try:
            cab = getattr(obj.assets_file, "name", "?")
            externals_by_file.setdefault(cab, [getattr(e, "path", str(e)) for e in
                                               getattr(obj.assets_file, "externals", [])])
            if obj.type.name == "MonoScript":
                data = obj.read()
                monoscripts[(cab, obj.path_id)] = (
                    getattr(data, "m_Namespace", "") or "",
                    getattr(data, "m_ClassName", "") or "",
                    getattr(data, "m_AssemblyName", "") or "")
            elif obj.type.name == "MonoBehaviour":
                monobehaviours.append((cab, obj))
        except Exception as exc:  # noqa: BLE001 - probe, report everything
            print(f"  ! {obj.type.name} {obj.path_id}: {type(exc).__name__}: {exc}")

    print(f"MonoScripts in bundle : {len(monoscripts)}")
    print(f"MonoBehaviours        : {len(monobehaviours)}")
    for cab, ext in externals_by_file.items():
        print(f"externals[{cab}] = {ext}")

    # ---- collect the referenced external CABs and load the file that provides them -----------
    wanted = set()
    refs = []
    for cab, obj in monobehaviours:
        tree = obj.read_typetree()
        script = tree.get("m_Script") or {}
        refs.append((cab, tree.get("m_Name", "?"), script.get("m_FileID", 0), script.get("m_PathID", 0),
                     externals_by_file.get(cab, [])))

    for _cab, _name, file_id, _pid, externals in refs:
        if file_id > 0 and 0 < file_id <= len(externals):
            wanted.add(externals[file_id - 1].split("/")[-1])

    print(f"\nexternal CABs referenced: {sorted(wanted)}")

    script_map = {}   # (cab, path_id) -> (ns, cls, asm)
    cab_index = build_cab_index()
    print(f"\nCAB index entries: {len(cab_index)}")

    loaded_bundles = {}
    for cab_name in sorted(wanted):
        bundle_path = cab_index.get(cab_name.lower())
        if not bundle_path:
            print(f"  ! no bundle known for CAB {cab_name}")
            continue
        if bundle_path not in loaded_bundles:
            print(f"  opening {os.path.relpath(bundle_path, GAME)} for {cab_name}")
            loaded_bundles[bundle_path] = UnityPy.load(bundle_path)
        other = loaded_bundles[bundle_path]
        found = 0
        for obj in other.objects:
            if obj.type.name != "MonoScript":
                continue
            data = obj.read()
            key = (getattr(obj.assets_file, "name", "?").lower(), obj.path_id)
            script_map[key] = (getattr(data, "m_Namespace", "") or "",
                               getattr(data, "m_ClassName", "") or "",
                               getattr(data, "m_AssemblyName", "") or "")
            found += 1
        print(f"    MonoScripts in that bundle: {found}")

    print(f"  MonoScripts indexed: {len(script_map)}")

    print("\nresolution:")
    ok = 0
    for cab, name, file_id, pid, externals in refs[:12]:
        if file_id == 0:
            hit = monoscripts.get((cab, pid))
        else:
            target = externals[file_id - 1].split("/")[-1] if 0 < file_id <= len(externals) else None
            hit = script_map.get((target.lower(), pid)) if target else None
        if hit:
            ok += 1
            print(f"  {name:22} -> {hit[0]}.{hit[1]}   ({hit[2]})")
        else:
            print(f"  {name:22} -> UNRESOLVED (fileID={file_id} pid={pid})")

    print(f"\nresolved {ok} of {len(refs)}")
    return 0


if __name__ == "__main__":
    sys.exit(main())

