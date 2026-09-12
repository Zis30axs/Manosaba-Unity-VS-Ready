import os, io, sys, threading, time
import UnityPy
from UnityPy.enums import ClassIDType
import soundfile as sf

DATA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data"
rel = sys.argv[1]
mode = sys.argv[2] if len(sys.argv) > 2 else "thread"
n = int(sys.argv[3]) if len(sys.argv) > 3 else 10000
start = int(sys.argv[4]) if len(sys.argv) > 4 else 0

if mode == "thread":
    threading.stack_size(64 * 1024 * 1024)

env = UnityPy.load(os.path.join(DATA, rel))
clips = [o for o in env.objects if o.type == ClassIDType.AudioClip]
print("clips", len(clips), flush=True)
t0 = time.time()
for i, obj in enumerate(clips[start:start + n], start):
    d = obj.read()
    for fn, wav in d.samples.items():
        print(f"i={i} {fn} wavbytes={len(wav)}", flush=True)
        data, sr = sf.read(io.BytesIO(wav), dtype="float32", always_2d=True)
        print(f"   shape={data.shape}", flush=True)
        buf = io.BytesIO()
        if mode == "thread":
            t = threading.Thread(target=lambda: sf.write(buf, data, sr, format="OGG", subtype="VORBIS"))
            t.start()
            t.join()
        else:
            sf.write(buf, data, sr, format="OGG", subtype="VORBIS")
        print(f"   ogg={len(buf.getvalue())}", flush=True)
print("done", time.time() - t0, flush=True)
