import os, json, io, traceback
import UnityPy
from UnityPy.enums import ClassIDType

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"
DATA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data"
OUT = r"E:\manosaba_re\work\probe_out"
os.makedirs(OUT, exist_ok=True)

def find_first(path, cid):
    env = UnityPy.load(path)
    for obj in env.objects:
        if obj.type == cid:
            return env, obj
    return env, None

# 1) Texture2D
env, obj = find_first(os.path.join(AA, "naninovel-backgrounds_assets_naninovel"), ClassIDType.Texture2D)
if obj:
    d = obj.read()
    img = d.image
    p = os.path.join(OUT, "tex_test.png")
    img.save(p)
    print("TEX", d.m_Name, d.m_Width, d.m_Height, d.m_TextureFormat, "->", os.path.getsize(p), "bytes")

# 2) Audio
env, obj = find_first(os.path.join(AA, "naninovel-audio_assets_audio-bgm_047naninovel.bundle"), ClassIDType.AudioClip)
if obj:
    d = obj.read()
    print("AUDIO", d.m_Name, d.m_Length, d.m_Frequency, d.m_Channels, "fmt", d.m_CompressionFormat)
    try:
        s = d.samples
        print("  samples keys:", list(s.keys())[:3], "sizes", [len(v) for v in s.values()][:3])
        fn = list(s.keys())[0]
        open(os.path.join(OUT, "audio_test.wav"), "wb").write(s[fn])
        print("  wrote wav", os.path.getsize(os.path.join(OUT, "audio_test.wav")))
    except Exception as e:
        print("  decode FAIL", type(e).__name__, e)

# 3) Video
found = False
for root, dirs, files in os.walk(AA):
    for f in files:
        if "movie" in f.lower() or "movie" in root.lower():
            p = os.path.join(root, f)
            try:
                env = UnityPy.load(p)
            except Exception:
                continue
            for obj in env.objects:
                if obj.type == ClassIDType.VideoClip:
                    d = obj.read()
                    print("VIDEO", d.m_Name, "in", os.path.relpath(p, AA))
                    try:
                        print("  tree keys:", list(obj.read_typetree().keys()))
                    except Exception as e:
                        print("  tree fail", e)
                    found = True
                    break
            if found:
                break
    if found:
        break

# 4) Font
env = UnityPy.load(os.path.join(DATA, "resources.assets"))
for obj in env.objects:
    if obj.type == ClassIDType.Font:
        d = obj.read()
        data = bytes(d.m_FontData)
        ext = ".otf" if data[:4] == b"OTTO" else ".ttf"
        open(os.path.join(OUT, "font_test" + ext), "wb").write(data)
        print("FONT", d.m_Name, len(data), ext)
