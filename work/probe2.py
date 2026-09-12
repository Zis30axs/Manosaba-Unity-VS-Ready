import os, json
import UnityPy
from UnityPy.enums import ClassIDType

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"

def load_bundle(name):
    return UnityPy.load(os.path.join(AA, name))

print("### TextAsset sample (naninovel script)")
env = load_bundle("general-localization-zhhans-scripts-common_assets_all.bundle")
for obj in env.objects:
    if obj.type == ClassIDType.TextAsset:
        d = obj.read()
        print("name:", d.m_Name, "bytes:", len(d.m_Script) if isinstance(d.m_Script, bytes) else "text")
        raw = d.m_Script
        if isinstance(raw, str):
            raw = raw.encode("utf-8", "surrogateescape")
        print("---- first 400 bytes ----")
        print(raw[:400].decode("utf-8", "replace"))
        print("---- end ----")
        break

print()
print("### MonoBehaviour typetree (general-data)")
env = load_bundle("general-data_assets_all.bundle")
for obj in env.objects:
    if obj.type == ClassIDType.MonoBehaviour:
        d = obj.read()
        print("name:", d.m_Name, "script:", getattr(d, "m_Script", None))
        try:
            serialized = obj.read_typetree()
            s = json.dumps(serialized, ensure_ascii=False, indent=1, default=str)
            print("typetree len", len(s))
            print(s[:800])
        except Exception as e:
            print("typetree FAILED:", type(e).__name__, e)
            print("raw bytes len", len(obj.get_raw_data()) if hasattr(obj, "get_raw_data") else "n/a")
        break
