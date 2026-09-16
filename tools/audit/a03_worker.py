#!/usr/bin/env python3
"""A03 worker -- scan ONE source file in an isolated process (memory safety).

Fixes over the previous revision (audit UR-008 / section 3.2.2):

* ``m_Container`` entries keep the **whole** ``AssetInfo`` -- target PPtr
  (``m_FileID`` + ``m_PathID``), ``preloadIndex``, ``preloadSize`` -- instead of
  ``[k for k, _ in d.m_Container]``, which threw the target away and made the
  "28,332 container paths hit" figure meaningless.
* ``m_PreloadTable`` and ``m_Dependencies`` raw PPtrs are preserved.
* every object is attributed to its owning ``SerializedFile`` (``obj.assets_file.name``)
  so identity is ``(source, serialized file, pathID)`` and never a shared externals list.
* per-source SHA-256 / size / Unity version are recorded for the preflight ledger.
* the result is written to a file so a single huge JSON object does not have to travel
  through a pipe.
"""
from __future__ import annotations

import collections
import hashlib
import json
import os
import sys

# Child processes must not depend on the console code page: this machine's default is
# cp936 while paths and CAB/asset names routinely contain non-ASCII characters.  The
# parent (a03_sources.py) sets the same variable; reconfiguring here also keeps a manual
# invocation correct.
try:
    sys.stdout.reconfigure(encoding="utf-8")
    sys.stderr.reconfigure(encoding="utf-8")
except (AttributeError, ValueError):
    pass

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import GAME_DATA_ROOT  # noqa: E402

import UnityPy  # noqa: E402


def _jsonable(value, depth=0):
    """Best-effort conversion of UnityPy structures to plain JSON."""
    if depth > 6:
        return "<max-depth>"
    if value is None or isinstance(value, (bool, int, float, str)):
        return value
    if isinstance(value, (bytes, bytearray)):
        return {"__bytes__": bytes(value).hex()}
    if isinstance(value, dict):
        return {str(k): _jsonable(v, depth + 1) for k, v in value.items()}
    if isinstance(value, (list, tuple, set)):
        return [_jsonable(v, depth + 1) for v in value]
    if hasattr(value, "m_FileID") and hasattr(value, "m_PathID"):
        return {"m_FileID": int(value.m_FileID), "m_PathID": int(value.m_PathID)}
    return repr(value)[:200]


def scan(rel, path, size, compute_hash=False):
    rec = {
        "rel": rel, "size": size, "serialized_files": [], "assetbundles": [],
        "types": {}, "objects_total": 0, "error": None,
        "object_counts_by_file": {}, "objects": [],
    }
    if compute_hash:
        try:
            digest = hashlib.sha256()
            with open(path, "rb") as fh:
                for block in iter(lambda: fh.read(1 << 22), b""):
                    digest.update(block)
            rec["sha256"] = digest.hexdigest()
        except OSError as exc:
            rec["sha256"] = None
            rec["hash_error"] = f"{type(exc).__name__}: {exc}"

    env = None
    try:
        env = UnityPy.load(path)
    except Exception as exc:  # noqa: BLE001 - reported, never swallowed
        rec["error"] = f"load:{type(exc).__name__}: {exc}"
        return rec

    try:
        # ---- per-SerializedFile records, keyed by the file's own identity ------
        sf_records = {}
        for outer_name, fobj in env.files.items():
            inner = getattr(fobj, "files", None)
            if inner is None:
                inner = {outer_name: fobj}
            for idx, (iname, sf) in enumerate(inner.items()):
                if type(sf).__name__ != "SerializedFile":
                    continue
                exts = []
                for e in (getattr(sf, "externals", None) or []):
                    g = getattr(e, "guid", None)
                    if isinstance(g, (bytes, bytearray)):
                        g = bytes(g).hex()
                    exts.append({
                        "index": len(exts) + 1,  # 1-based: m_FileID == index
                        "path": getattr(e, "path", None),
                        "type": getattr(e, "type", None),
                        "guid": g,
                    })
                name = getattr(sf, "name", None) or f"sf#{idx}"
                sf_records[str(name)] = {
                    "name": str(name),
                    "outer_file": str(outer_name),
                    "index_in_outer": idx,
                    "unity_version": getattr(sf, "unity_version", None),
                    "version": getattr(sf, "version", None),
                    "target_platform": getattr(sf, "target_platform", None),
                    "big_id_enabled": getattr(sf, "big_id_enabled", None),
                    "type_count": len(getattr(sf, "types", []) or []),
                    "externals": exts,
                    "objects_total": 0,
                    "types": {},
                }
        rec["serialized_files"] = list(sf_records.values())

        # ---- objects, attributed to their owning SerializedFile ---------------
        type_counter = collections.Counter()
        per_file_counter = collections.defaultdict(collections.Counter)
        objects = rec["objects"]
        for o in env.objects:
            sf = getattr(o, "assets_file", None)
            sf_name = str(getattr(sf, "name", None) or "unknown")
            try:
                pid = int(o.path_id)
            except (TypeError, ValueError):
                pid = None
            tname = o.type.name
            type_counter[tname] += 1
            per_file_counter[sf_name][tname] += 1
            objects.append({"file": sf_name, "path_id": str(pid) if pid is not None else None,
                            "type": tname, "byte_size": getattr(o, "byte_size", None),
                            "class_id": getattr(o, "class_id", None)})

        rec["types"] = dict(type_counter.most_common())
        rec["objects_total"] = sum(type_counter.values())
        for name, counter in per_file_counter.items():
            if name in sf_records:
                sf_records[name]["objects_total"] = sum(counter.values())
                sf_records[name]["types"] = dict(counter.most_common())
        rec["object_counts_by_file"] = {k: sum(v.values()) for k, v in per_file_counter.items()}
        rec["serialized_files"] = list(sf_records.values())

        # ---- AssetBundle manifests: keep the container targets ----------------
        for o in env.objects:
            if o.type.name != "AssetBundle":
                continue
            sf_name = str(getattr(getattr(o, "assets_file", None), "name", None) or "unknown")
            entry = {"file": sf_name, "path_id": str(o.path_id)}
            try:
                d = o.read()
            except Exception as exc:  # noqa: BLE001
                entry["error"] = f"read:{type(exc).__name__}: {exc}"
                rec["assetbundles"].append(entry)
                continue
            containers = []
            for item in (getattr(d, "m_Container", None) or []):
                ckey, info = item
                target = getattr(info, "asset", None)
                containers.append({
                    "key": ckey,
                    "target": {
                        "m_FileID": int(getattr(target, "m_FileID", 0)) if target is not None else None,
                        "m_PathID": str(int(getattr(target, "m_PathID", 0))) if target is not None else None,
                    },
                    "preloadIndex": getattr(info, "preloadIndex", None),
                    "preloadSize": getattr(info, "preloadSize", None),
                })
            preload = []
            for p in (getattr(d, "m_PreloadTable", None) or []):
                preload.append({"m_FileID": int(getattr(p, "m_FileID", 0)),
                                "m_PathID": str(int(getattr(p, "m_PathID", 0)))})
            entry.update({
                "name": getattr(d, "m_Name", None),
                "asset_bundle_name": getattr(d, "m_AssetBundleName", None),
                "is_streamed_scene": bool(getattr(d, "m_IsStreamedSceneAssetBundle", False)),
                "container_count": len(containers),
                "containers": containers,
                "preload_count": len(preload),
                "preload_table": preload,
                "dependencies": _jsonable(list(getattr(d, "m_Dependencies", None) or [])),
            })
            rec["assetbundles"].append(entry)
    except Exception as exc:  # noqa: BLE001
        rec["error"] = f"scan:{type(exc).__name__}: {exc}"
    finally:
        del env
    return rec


if __name__ == "__main__":
    args = [a for a in sys.argv[1:] if not a.startswith("--")]
    flags = {a for a in sys.argv[1:] if a.startswith("--")}
    rel = args[0]
    out_file = args[1] if len(args) > 1 else None
    full = os.path.join(GAME_DATA_ROOT, rel.replace("/", os.sep).replace("\\", os.sep))
    if not os.path.exists(full):
        print(json.dumps({"rel": rel, "error": f"input missing: {full}", "objects_total": 0,
                          "types": {}, "serialized_files": [], "assetbundles": []}))
        sys.exit(0)
    result = scan(rel, full, os.path.getsize(full), compute_hash="--hash" in flags)
    if out_file:
        with open(out_file, "w", encoding="utf-8") as fh:
            json.dump(result, fh, ensure_ascii=False)
        print(json.dumps({"rel": rel, "out": out_file,
                          "objects_total": result["objects_total"],
                          "bytes": os.path.getsize(out_file), "error": result["error"]},
                         ensure_ascii=False))
    else:
        print(json.dumps(result, ensure_ascii=False))
