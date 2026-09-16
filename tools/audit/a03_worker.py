#!/usr/bin/env python3
"""A03 worker - scan ONE source file in an isolated process (memory safety) and
print a single JSON object to stdout."""
import os, sys, json, collections
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import DATA, AAW
import UnityPy

def scan(rel, path, size):
    rec = {"rel": rel, "size": size, "serialized_files": [], "assetbundles": [],
           "types": {}, "objects_total": 0, "error": None}
    try:
        env = UnityPy.load(path)
    except Exception as e:
        rec["error"] = f"load:{type(e).__name__}: {e}"
        return rec
    try:
        for name, fobj in env.files.items():
            inner = getattr(fobj, "files", None)
            if inner is None:
                inner = {name: fobj}
            for iname, sf in inner.items():
                if type(sf).__name__ != "SerializedFile":
                    continue
                exts = []
                for e in (getattr(sf, "externals", None) or []):
                    g = getattr(e, "guid", None)
                    if isinstance(g, (bytes, bytearray)):
                        g = bytes(g).hex()
                    exts.append({"path": getattr(e, "path", None),
                                 "type": getattr(e, "type", None), "guid": g})
                rec["serialized_files"].append({
                    "name": str(iname),
                    "unity_version": getattr(sf, "unity_version", None),
                    "type_tree": getattr(sf, "enable_type_tree", None),
                    "externals": exts,
                    "type_count": len(getattr(sf, "types", []) or []),
                })
        c = collections.Counter()
        for o in env.objects:
            c[o.type.name] += 1
        rec["types"] = dict(c.most_common())
        rec["objects_total"] = sum(c.values())
        for o in env.objects:
            if o.type.name != "AssetBundle":
                continue
            try:
                d = o.read()
                cont = [k for k, _ in (d.m_Container or [])]
                rec["assetbundles"].append({
                    "name": getattr(d, "m_Name", None),
                    "container_count": len(cont),
                    "containers": cont,
                    "dependencies": list(getattr(d, "m_Dependencies", []) or []),
                })
            except Exception as e:
                rec["assetbundles"].append({"error": f"{type(e).__name__}: {e}"})
        del env
    except Exception as e:
        rec["error"] = f"scan:{type(e).__name__}: {e}"
    return rec

if __name__ == "__main__":
    rel = sys.argv[1]
    path = os.path.join(DATA, rel.replace("/", os.sep))
    size = os.path.getsize(path)
    print(json.dumps(scan(rel, path, size), ensure_ascii=False), flush=True)
