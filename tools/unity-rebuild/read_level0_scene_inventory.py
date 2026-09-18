"""Inventory the shipped boot scene (`manosaba_Data/level0`) so "rebuild the scene" can be checked against
what the scene actually contains, instead of guessing.

    python tools/unity-rebuild/read_level0_scene_inventory.py [--write]

`level0` is a player-build scene: it has no type trees, so component *types* are not in the file.  They are
recoverable anyway, because every MonoBehaviour stores `m_Script` as a PPtr (m_FileID, m_PathID) into the
`globalgamemanagers.assets` MonoScript table, and the extraction index (`res/_monoscripts.csv`) records
class/namespace/assembly for those very path_ids.  Built-in components (Transform, Camera, ...) are named by
UnityPy's own type trees.

Output: per-object rows (path_id, type, name where known, script class), a type histogram, and the component
list of the GameInitializer GameObject - the object the game's own scene uses as its entry point.
"""
from __future__ import annotations

import argparse
import collections
import csv
import io
import json
import os
import struct
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "level0_scene_inventory.json")
MONOSCRIPTS = os.path.join(REPO, "res", "_monoscripts.csv")
ENTRY_OBJECT_PATH_ID = 17   # decoded from GameInitializer.m_GameObject; see decode_level0_boot.py

try:
    import UnityPy
except ImportError:
    print("UnityPy is not available in this interpreter: " + sys.executable)
    raise SystemExit(3)


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def load_monoscripts():
    """path_id -> (class, namespace, assembly) for the shipped build's MonoScript table."""
    table = {}
    with io.open(MONOSCRIPTS, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            try:
                table[int(row["path_id"])] = (row["class"], row["namespace"], row["assembly"])
            except (KeyError, ValueError):
                continue
    return table


def script_pptr(raw):
    """(m_FileID, m_PathID) of m_Script, when the object is long enough to hold it."""
    if len(raw) < 28:
        return None
    return struct.unpack_from("<iq", raw, 16)


def vec(v):
    """x/y/z as available: Transform uses Vector3f, RectTransform anchors use Vector2f."""
    return [round(float(getattr(v, axis)), 6) for axis in ("x", "y", "z") if hasattr(v, axis)]


def as_int(value):
    """UnityPy exposes some integer fields as typed BitField wrappers rather than plain ints."""
    try:
        return int(value)
    except (TypeError, ValueError):
        return int(getattr(value, "value", 0))


def transform_state(obj):
    """Local TRS (+ RectTransform anchors) of one Transform/RectTransform, for scene reconstruction.

    These are built-in types, so UnityPy's type tree gives real values rather than a byte walk: copying them is
    what makes a rebuilt scene match the original layout instead of being an empty skeleton.
    """
    data = obj.read()
    state = {
        "local_position": vec(data.m_LocalPosition),
        "local_rotation": [round(float(data.m_LocalRotation.x), 6), round(float(data.m_LocalRotation.y), 6),
                           round(float(data.m_LocalRotation.z), 6), round(float(data.m_LocalRotation.w), 6)],
        "local_scale": vec(data.m_LocalScale),
    }
    if obj.type.name == "RectTransform":
        state.update({
            "anchor_min": vec(data.m_AnchorMin),
            "anchor_max": vec(data.m_AnchorMax),
            "anchored_position": vec(data.m_AnchoredPosition),
            "size_delta": vec(data.m_SizeDelta),
            "pivot": vec(data.m_Pivot),
        })
    return state


def component_state(obj):
    """Serialized values of the built-in components a scene cannot be rebuilt without."""
    try:
        data = obj.read()
    except Exception:
        return None
    if obj.type.name == "Canvas":
        return {"render_mode": as_int(data.m_RenderMode), "sorting_order": as_int(data.m_SortingOrder),
                "override_sorting": bool(getattr(data, "m_OverrideSorting", False)),
                "pixel_perfect": bool(getattr(data, "m_PixelPerfect", False))}
    if obj.type.name == "CanvasGroup":
        return {"alpha": round(float(data.m_Alpha), 6),
                "interactable": bool(data.m_Interactable),
                "blocks_raycasts": bool(data.m_BlocksRaycasts),
                "ignore_parent_groups": bool(data.m_IgnoreParentGroups)}
    if obj.type.name == "Camera":
        return {"orthographic": bool(data.orthographic),
                "orthographic_size": round(float(data.orthographic_size), 6),
                "field_of_view": round(float(data.field_of_view), 6), "depth": round(float(data.m_Depth), 6),
                "near_clip": round(float(data.near_clip_plane), 6), "far_clip": round(float(data.far_clip_plane), 6),
                "clear_flags": as_int(data.m_ClearFlags), "culling_mask": as_int(data.m_CullingMask),
                "background_color": [round(float(c), 6) for c in
                                     (data.m_BackGroundColor.r, data.m_BackGroundColor.g,
                                      data.m_BackGroundColor.b, data.m_BackGroundColor.a)]}
    return None


def hierarchy_of(env):
    """GameObject tree + component ownership, i.e. the blueprint a scene reconstruction needs.

    Component ownership is read from raw bytes: every component starts with PPtr<GameObject> m_GameObject, which
    works even without a type tree.  Parenting comes from Transform/RectTransform, which are built-in types and
    therefore *do* have a type tree inside UnityPy.
    """
    nodes = {}
    for obj in env.objects:
        if obj.type.name == "GameObject":
            try:
                name = obj.read().m_Name or ""
            except Exception:
                name = ""
            nodes[int(obj.path_id)] = {"path_id": int(obj.path_id), "name": name,
                                       "components": [], "children": [], "parent": 0}

    parent, children = {}, {}
    # m_Father / m_Children are PPtr<Transform>, not PPtr<GameObject>, so translation is required before the
    # edges can be stated in GameObject terms.
    transform_to_gameobject = {}
    raw_edges = []
    for obj in env.objects:
        if obj.type.name == "GameObject":
            continue
        raw = obj.get_raw_data()
        # component -> owning GameObject
        if len(raw) >= 12:
            file_id, owner = struct.unpack_from("<iq", raw, 0)
            if file_id == 0 and owner in nodes:
                nodes[owner]["components"].append({"path_id": int(obj.path_id), "type": obj.type.name})
        # Transform/RectTransform -> parent and children
        if obj.type.name in ("Transform", "RectTransform"):
            try:
                data = obj.read()
                transform_to_gameobject[int(obj.path_id)] = int(data.m_GameObject.path_id)
                father = int(data.m_Father.path_id) if data.m_Father is not None else 0
                kids = [int(child.path_id) for child in (data.m_Children or [])]
            except Exception:
                continue
            raw_edges.append((int(data.m_GameObject.path_id), father, kids))

    for gameobject, father, kids in raw_edges:
        parent[gameobject] = transform_to_gameobject.get(father, 0)
        children[gameobject] = [transform_to_gameobject.get(kid, 0) for kid in kids]

    for path_id, node in nodes.items():
        node["parent"] = parent.get(path_id, 0)
        node["children"] = children.get(path_id, [])
        node["components"].sort(key=lambda c: c["path_id"])

    # A name-only path makes the reconstruction order readable and diffable.
    def path_of(path_id, seen=None):
        seen = seen or set()
        node = nodes.get(path_id)
        if node is None or path_id in seen:
            return "?"
        seen.add(path_id)
        prefix = path_of(node["parent"], seen) if node["parent"] else ""
        name = node["name"] or ("node" + str(path_id))
        return (prefix + "/" + name) if prefix else name

    for path_id, node in nodes.items():
        node["path"] = path_of(path_id)

    roots = sorted(pid for pid, node in nodes.items() if node["parent"] == 0)
    declared = sum(len(node["children"]) for node in nodes.values())
    return {
        "gameobject_count": len(nodes),
        "roots": roots,
        "root_paths": [nodes[pid]["path"] for pid in roots],
        "validation": {
            "every_child_is_a_known_gameobject": all(
                child in nodes for node in nodes.values() for child in node["children"]),
            "every_parent_is_known_or_root": all(
                node["parent"] == 0 or node["parent"] in nodes for node in nodes.values()),
            "child_edges_match_parent_edges": declared == sum(1 for node in nodes.values() if node["parent"]),
            "every_gameobject_has_a_transform": all(
                any(c["type"] in ("Transform", "RectTransform") for c in node["components"])
                for node in nodes.values()),
        },
        "nodes": [nodes[pid] for pid in sorted(nodes)],
    }


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    scene = os.path.join(game_root(), "manosaba_Data", "level0")
    env = UnityPy.load(scene)
    monoscripts = load_monoscripts()

    objects, by_path = [], {}
    for obj in env.objects:
        raw_type = obj.type.name
        name = ""
        try:
            if raw_type in ("GameObject", "MonoBehaviour"):
                data = obj.read()
                name = getattr(data, "m_Name", "") or ""
        except Exception:
            name = ""
        row = {"path_id": int(obj.path_id), "type": raw_type, "name": name, "script": None}
        if raw_type == "GameObject":
            try:
                data = obj.read()
                row["tag"] = data.m_TagString or ""
                row["layer"] = int(data.m_Layer)
                row["active"] = bool(data.m_IsActive)
            except Exception as ex:
                row["data_error"] = f"{type(ex).__name__}: {ex}"
        if raw_type == "MonoBehaviour":
            pptr = script_pptr(obj.get_raw_data())
            if pptr:
                row["script_file_id"], row["script_path_id"] = pptr
                resolved = monoscripts.get(pptr[1])
                if resolved:
                    row["script"] = ".".join(part for part in (resolved[1], resolved[0]) if part)
                    row["script_assembly"] = resolved[2]
        objects.append(row)
        by_path[row["path_id"]] = row

        # Serialized values that make the rebuild a scene rather than a skeleton.  Failures are recorded, never
        # guessed: a missing "data" means "not recovered", not "default".
        try:
            if raw_type in ("Transform", "RectTransform"):
                row["data"] = transform_state(obj)
            elif raw_type in ("Canvas", "CanvasGroup", "Camera"):
                state = component_state(obj)
                if state:
                    row["data"] = state
        except Exception as ex:  # UnityPy could not read this object's fields
            row["data_error"] = f"{type(ex).__name__}: {ex}"

    histogram = collections.Counter(row["type"] for row in objects)
    script_histogram = collections.Counter(
        (row["script"] or "UNRESOLVED") for row in objects if row["type"] == "MonoBehaviour")

    # GameObject -> component edges, so the entry object's own components are visible.
    # Components carry a leading PPtr<GameObject>; read it for every non-GameObject object.
    entry_components = []
    for obj in env.objects:
        if obj.type.name == "GameObject":
            continue
        raw = obj.get_raw_data()
        if len(raw) < 12:
            continue
        file_id, owner = struct.unpack_from("<iq", raw, 0)
        if file_id == 0 and owner == ENTRY_OBJECT_PATH_ID:
            entry_components.append({
                "path_id": int(obj.path_id),
                "type": obj.type.name,
                "script": by_path.get(int(obj.path_id), {}).get("script"),
            })

    hierarchy = hierarchy_of(env)

    doc = {
        "scene": scene,
        "object_count": len(objects),
        "monoscripts_indexed": len(monoscripts),
        "type_histogram": dict(sorted(histogram.items(), key=lambda kv: (-kv[1], kv[0]))),
        "monobehaviour_histogram": dict(sorted(script_histogram.items(), key=lambda kv: (-kv[1], kv[0]))),
        "hierarchy": hierarchy,
        "entry_gameobject": {
            "path_id": ENTRY_OBJECT_PATH_ID,
            "name": by_path.get(ENTRY_OBJECT_PATH_ID, {}).get("name"),
            "components": sorted(entry_components, key=lambda c: c["path_id"]),
        },
        "scene_root_names": [row["name"] for row in objects
                             if row["type"] == "GameObject" and row["name"]][:40],
        "objects": objects,
    }

    print(f"scene: {scene}  objects={len(objects)}  monoscripts_indexed={len(monoscripts)}")
    print("types: " + ", ".join(f"{k}={v}" for k, v in list(doc["type_histogram"].items())[:12]))
    print("monobehaviours: " + ", ".join(f"{k}={v}" for k, v in list(doc["monobehaviour_histogram"].items())[:20]))
    print(f"entry GameObject path_id={ENTRY_OBJECT_PATH_ID} name={doc['entry_gameobject']['name']!r}")
    for comp in doc["entry_gameobject"]["components"]:
        print(f"  component {comp['path_id']:>5} {comp['type']:<14} {comp['script'] or ''}")
    print(f"gameobjects with a name: {len(doc['scene_root_names'])}")
    print("  " + ", ".join(doc["scene_root_names"][:25]))
    print(f"hierarchy: gameobjects={hierarchy['gameobject_count']} roots={len(hierarchy['roots'])}")
    for path in hierarchy["root_paths"]:
        print("  root " + path)
    for key, value in hierarchy["validation"].items():
        print(f"  validation {key:36} {value}")

    if args.write:
        os.makedirs(os.path.dirname(OUT), exist_ok=True)
        with io.open(OUT, "w", encoding="utf-8") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)
    return 0


if __name__ == "__main__":
    sys.exit(main())
