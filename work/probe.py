import os, sys, json, io
import UnityPy
from UnityPy.enums import ClassIDType

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"

targets = [
    "general-localization-zhhans-scripts-common_assets_all.bundle",
    "general-localization-zhhans-text_assets_all.bundle",
    "general-managedtext_assets_all.bundle",
    "general-data_assets_all.bundle",
    "naninovel-backgrounds_assets_naninovel",
]
try:
    targets += [d for d in os.listdir(os.path.join(AA, "naninovel-backgrounds_assets_naninovel"))][:0]
except Exception:
    pass

def probe(bundle_path):
    print("=" * 70)
    print("BUNDLE", os.path.relpath(bundle_path, AA))
    env = UnityPy.load(bundle_path)
    kinds = {}
    for obj in env.objects:
        kinds.setdefault(obj.type.name, []).append(obj)
    for k, v in sorted(kinds.items(), key=lambda x: -len(x[1])):
        print(f"  {k}: {len(v)}")
    # containers
    conts = []
    for obj in env.objects:
        c = obj.container
        if c:
            conts.append((c, obj.type.name))
    print("  containers:", len(conts))
    for c, t in conts[:15]:
        print("   ", t, c)
    return env, kinds

for t in targets:
    p = os.path.join(AA, t)
    if os.path.exists(p):
        probe(p)
    else:
        print("missing", t)
