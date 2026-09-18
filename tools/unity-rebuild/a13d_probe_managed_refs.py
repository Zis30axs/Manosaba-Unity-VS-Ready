"""Probe: can UnityPy expose the concrete type of each [SerializeReference] element?

The a13c converter left 304,280 fields at default because the flat typetree JSON does not carry the
concrete class of a polymorphic element.  Unity stores that in the SerializedFile's RefType table
(type hash -> assembly qualified name), so this probe checks whether UnityPy surfaces it.
"""
from __future__ import annotations

import json
import os
import sys

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))


def game_root():
    cfg = os.path.join(REPO, "tools", "audit", "audit.config.json")
    with open(cfg, encoding="utf-8-sig") as fh:
        return json.load(fh)["game_root"]


BUNDLE = os.path.join(game_root(), "manosaba_Data", "StreamingAssets", "aa", "StandaloneWindows64",
                      "naninovel-scripts_assets_naninovelscripts-act01_chapter01_advbad.bundle")


def walk(node, depth=0, prefix="", out=None, limit=6):
    """Print the typetree node shape (m_Type / m_Name / m_MetaFlag) around managed references."""
    if out is None:
        out = []
    if depth > 3 or len(out) > limit:
        return out
    out.append(("  " * depth) + f"{prefix}{node.m_Type} {node.m_Name} meta={node.m_MetaFlag:#x}")
    for child in getattr(node, "m_Children", []) or []:
        walk(child, depth + 1, "", out, limit)
    return out


def main():
    if not os.path.exists(BUNDLE):
        print("bundle missing:", BUNDLE)
        return 2
    env = UnityPy.load(BUNDLE)
    targets = [o for o in env.objects if o.type.name == "MonoBehaviour"]
    print(f"MonoBehaviours: {len(targets)}")
    obj = targets[0]
    sf = obj.assets_file

    print("\n=== SerializedFile attributes that could hold RefTypes ===")
    for name in dir(sf):
        if "ref" in name.lower() or "type" in name.lower():
            value = getattr(sf, name, None)
            kind = type(value).__name__
            size = len(value) if hasattr(value, "__len__") and not callable(value) else ""
            print(f"  {name:24} {kind:18} {size}")

    ref_types = getattr(sf, "m_RefTypes", None) or getattr(sf, "ref_types", None)
    print(f"\n=== RefType table ({0 if ref_types is None else len(ref_types)} entries) ===")
    if ref_types:
        for rt in list(ref_types)[:8]:
            print("  ", rt)

    print("\n=== typetree nodes (first lines) ===")
    tree = obj.read_typetree()
    print("  top-level keys:", list(tree.keys())[:12])
    st = getattr(obj, "serialized_type", None)
    node = getattr(st, "node", None) if st is not None else None
    if node is not None:
        for line in walk(node):
            print("  ", line)
    else:
        print("  serialized_type/node unavailable:", type(st).__name__)

    print("\n=== what the resolver puts in the flat dict ===")
    for key in ("lines", "playlist", "textMap", "references"):
        if key in tree:
            value = tree[key]
            print(f"  {key}: {type(value).__name__}")
            if isinstance(value, list) and value:
                first = value[0]
                print("    first element type:", type(first).__name__)
                if isinstance(first, dict):
                    print("    keys:", list(first.keys())[:12])
                    print("    sample:", json.dumps(first, ensure_ascii=False)[:300])
            elif isinstance(value, dict):
                print("    keys:", list(value.keys())[:8])
                print("    sample:", json.dumps(value, ensure_ascii=False)[:300])
    return 0


if __name__ == "__main__":
    sys.exit(main())
