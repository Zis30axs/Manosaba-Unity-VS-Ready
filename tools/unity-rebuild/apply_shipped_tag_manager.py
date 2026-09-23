"""Write the shipped TagManager (tags, layers, sorting layers, rendering layers) into ProjectSettings/TagManager.asset.

    python tools/unity-rebuild/apply_shipped_tag_manager.py [--write]

Why
---
The project was created with Unity's default TagManager: layers 0-5 only.  The shipped globalgamemanagers TagManager
names layers 12..31 "Naninovel 1" .. "Naninovel 20", and Naninovel's LayeredDrawer needs them: a layered character
prefab whose LayeredActorBehaviour has a render camera ("camera mode") renders its layers on a private culling layer
taken from the layers whose name starts with "Naninovel", and throws when there is none left -

    Naninovel.Error: Failed to to render 'LeiaLayeredPrefab' layered actor in camera mode: no available culling
    layers. Make sure enough layers named 'Naninovel ...' are added.

That exception is raised while ScriptPlaylist.LoadResources preloads @char commands, so the first story script
never starts playing (the New Game screen stays black).  The rendering layer "UI" (URP rendering layer 1) is shipped
too.  Nothing here is a reconstruction choice: every name is copied from the shipped object.

Unity must not be running (it rewrites ProjectSettings on exit).  Without --write, prints the difference only.
"""
from __future__ import annotations

import argparse
import io
import json
import os
import sys

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
TARGET = os.path.join(REPO, "UnityProject", "ProjectSettings", "TagManager.asset")


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def shipped_tag_manager():
    env = UnityPy.load(os.path.join(game_root(), "manosaba_Data", "globalgamemanagers"))
    for obj in env.objects:
        if obj.type.name == "TagManager":
            return obj.read_typetree()
    raise SystemExit("no TagManager object in the shipped globalgamemanagers")


def yaml_scalar(text):
    """A layer / tag name as Unity writes it: bare when plain, quoted when YAML would misread it."""
    if text == "":
        return ""
    if any(ch in text for ch in ":#'\"{}[],&*!|>%@`") or text != text.strip():
        return "'" + text.replace("'", "''") + "'"
    return text


def render(tm):
    lines = ["%YAML 1.1", "%TAG !u! tag:unity3d.com,2011:", "--- !u!78 &1", "TagManager:", "  serializedVersion: 3"]
    tags = tm.get("tags") or []
    if tags:
        lines.append("  tags:")
        lines += ["  - " + yaml_scalar(t) for t in tags]
    else:
        lines.append("  tags: []")
    lines.append("  layers:")
    lines += [("  - " + yaml_scalar(layer)).rstrip() if layer else "  - " for layer in tm["layers"]]
    lines.append("  m_SortingLayers:")
    for layer in tm.get("m_SortingLayers") or []:
        lines.append("  - name: " + yaml_scalar(layer["name"]))
        lines.append("    uniqueID: %d" % int(layer["uniqueID"]))
        lines.append("    locked: %d" % int(layer.get("locked", 0)))
    lines.append("  m_RenderingLayers:")
    lines += ["  - " + yaml_scalar(name) for name in tm.get("m_RenderingLayers") or []]
    return "\n".join(lines) + "\n"


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()
    tm = shipped_tag_manager()
    new_text = render(tm)
    with io.open(TARGET, encoding="utf-8", newline="") as fh:
        old_text = fh.read()
    if "\r\n" in old_text:
        new_text = new_text.replace("\n", "\r\n")
    named = [(i, name) for i, name in enumerate(tm["layers"]) if name]
    print("shipped layers: " + ", ".join("%d=%s" % pair for pair in named))
    print("shipped tags: %s; sorting layers: %s; rendering layers: %s"
          % (tm.get("tags"), [s["name"] for s in tm.get("m_SortingLayers") or []], tm.get("m_RenderingLayers")))
    if old_text == new_text:
        print("TagManager.asset already matches the shipped TagManager")
        return 0
    if not args.write:
        print("TagManager.asset differs (run with --write to apply)")
        return 1
    with io.open(TARGET, "w", encoding="utf-8", newline="") as fh:
        fh.write(new_text)
    print("wrote " + TARGET)
    return 0


if __name__ == "__main__":
    sys.exit(main())
