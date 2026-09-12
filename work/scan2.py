import os, json, time, collections
import UnityPy

DATA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data"
AA = os.path.join(DATA, r"StreamingAssets\aa\StandaloneWindows64")
OUT = r"E:\manosaba_re\work"

def collect_files():
    files = []
    for root, dirs, fs in os.walk(AA):
        for f in fs:
            files.append(os.path.join(root, f))
    for f in ("resources.assets", "globalgamemanagers.assets", "sharedassets0.assets",
              "level0", "globalgamemanagers"):
        p = os.path.join(DATA, f)
        if os.path.exists(p):
            files.append(p)
    return sorted(files)

res = {}
t0 = time.time()
files = collect_files()
for i, fp in enumerate(files):
    rel = os.path.relpath(fp, DATA)
    try:
        env = UnityPy.load(fp)
        types = collections.Counter(o.type.name for o in env.objects)
        res[rel] = {"size": os.path.getsize(fp), "types": dict(types)}
        del env
    except Exception as e:
        res[rel] = {"size": os.path.getsize(fp), "error": str(e)}
    if i % 25 == 0:
        print(f"[{i}/{len(files)}] {rel} ({time.time()-t0:.0f}s)", flush=True)

with open(os.path.join(OUT, "file_types.json"), "w", encoding="utf-8") as fh:
    json.dump(res, fh, ensure_ascii=False, indent=1)
print("done", time.time() - t0, flush=True)
