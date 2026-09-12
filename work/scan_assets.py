import os, sys, json, traceback, collections
import UnityPy

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"
DATA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data"
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
    return files

def main():
    files = sorted(collect_files())
    print("total files", len(files), flush=True)
    counts = collections.Counter()
    inv = []
    for i, fp in enumerate(files):
        rel = os.path.relpath(fp, DATA)
        try:
            env = UnityPy.load(fp)
            n = 0
            for obj in env.objects:
                counts[obj.type.name] += 1
                n += 1
            inv.append({"file": rel, "objects": n, "size": os.path.getsize(fp)})
        except Exception as e:
            print("FAIL", rel, e, flush=True)
            inv.append({"file": rel, "error": str(e), "size": os.path.getsize(fp)})
        if i % 20 == 0:
            print(f"[{i}/{len(files)}] {rel}", flush=True)
    with open(os.path.join(OUT, "inventory.json"), "w", encoding="utf-8") as fh:
        json.dump({"counts": dict(counts), "files": inv}, fh, ensure_ascii=False, indent=1)
    print("=== TYPE COUNTS ===")
    for k, v in counts.most_common():
        print(f"{k}\t{v}")

if __name__ == "__main__":
    main()
