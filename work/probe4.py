import os, UnityPy
from UnityPy.enums import ClassIDType

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"
p = os.path.join(AA, r"general-localization-zhhans-movies_assets_naninovel\localization\zh-hans\movies\act01_opening.bundle")
print("bundle size", os.path.getsize(p))
env = UnityPy.load(p)
for name, f in env.files.items():
    print("FILE", name, type(f).__name__, getattr(f, "size", "?"))
bf = env.files[p] if p in env.files else list(env.files.values())[0]
print("inner files:", getattr(bf, "files", None))
for obj in env.objects:
    print("OBJ", obj.type.name, obj.path_id)
    if obj.type == ClassIDType.VideoClip:
        d = obj.read()
        tt = obj.read_typetree()
        print(" external:", tt.get("m_ExternalResources"))
        print(" original path:", tt.get("m_OriginalPath"))
        raw = obj.get_raw_data()
        print(" raw len", len(raw))
