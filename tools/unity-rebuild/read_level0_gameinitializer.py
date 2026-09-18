"""Read the shipped level0 scene's GameInitializer object: the serialized references the reconstructed
boot chain needs (they are what the game's own scene supplies and our reconstruction does not have).

    python tools/unity-rebuild/read_level0_gameinitializer.py

The scene is the player's built-in first scene (manosaba_Data/level0), not an Addressables bundle; the
extraction index maps its GameObject "GameInitializer" to path_id 17 with components 37 and 116.
"""
from __future__ import annotations

import json
import os
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))

try:
    import UnityPy
except ImportError:  # pragma: no cover - reported, not guessed
    print("UnityPy is not available in this interpreter: " + sys.executable)
    raise SystemExit(3)


def game_root():
    with open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def main():
    scene = os.path.join(game_root(), "manosaba_Data", "level0")
    print(f"scene: {scene} ({os.path.getsize(scene)} bytes)")
    env = UnityPy.load(scene)
    print(f"objects: {len(env.objects)}")
    print(f"container: {list(env.container)[:10]}")

    wanted = {"17", "37", "116"}
    for obj in env.objects:
        pid = str(obj.path_id)
        if pid not in wanted:
            continue
        print(f"\n=== path_id {pid}  type={obj.type.name}")
        try:
            data = obj.read()
        except Exception as ex:
            print(f"  read() failed: {type(ex).__name__}: {ex}")
            continue
        try:
            tree = obj.read_typetree()
            print("  typetree:")
            for key, value in list(tree.items())[:30]:
                print(f"    {key} = {value}")
        except Exception as ex:
            print(f"  read_typetree() failed: {type(ex).__name__}: {ex}")
            try:
                print("  raw:", str(data)[:400])
            except Exception:
                pass
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
