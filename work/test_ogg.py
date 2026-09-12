import os, io, sys, json, threading, traceback, time
import UnityPy
from UnityPy.enums import ClassIDType
import soundfile as sf

DATA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data"
rel = sys.argv[1]
mode = sys.argv[2] if len(sys.argv) > 2 else "main"
n = int(sys.argv[3]) if len(sys.argv) > 3 else 5

env = UnityPy.load(os.path.join(DATA, rel))
clips = [o for o in env.objects if o.type == ClassIDType.AudioClip]
print("clips", len(clips), flush=True)

if mode == "thread":
    threading.stack_size(64 * 1024 * 1024)

ok = bad = 0
for i, obj in enumerate(clips[:n]):
    d = obj.read()
    for fn, wav in d.samples.items():
        data, sr = sf.read(io.BytesIO(wav), dtype="float32", always_2d=True)
        buf = io.BytesIO()
        err = []

        def enc():
            try:
                sf.write(buf, data, sr, format="OGG", subtype="VORBIS")
            except Exception as e:
                err.append(e)

        t0 = time.time()
        if mode == "thread":
            t = threading.Thread(target=enc)
            t.start()
            t.join()
        else:
            enc()
        if err:
            bad += 1
            print("  fail", fn, err[0], flush=True)
        else:
            ok += 1
            print(f"  ok {fn} {len(buf.getvalue())}B {time.time()-t0:.2f}s", flush=True)
print("ok", ok, "bad", bad)
