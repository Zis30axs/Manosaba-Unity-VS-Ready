import os, collections, random, sys
import soundfile as sf

RES = r"E:\manosaba_re\res"
ext = collections.Counter()
size = collections.Counter()
per_top = collections.Counter()
dup = []
oggs = []
for root, dirs, files in os.walk(RES):
    for f in files:
        p = os.path.join(root, f)
        try:
            s = os.path.getsize(p)
        except OSError:
            continue
        e = os.path.splitext(f)[1].lower() or "(none)"
        ext[e] += 1
        size[e] += s
        rel = os.path.relpath(p, RES)
        per_top[os.sep.join(rel.split(os.sep)[:2])] += s
        if "~" in f:
            dup.append(rel)
        if e == ".ogg":
            oggs.append(p)

print("=== extensions ===")
for e, n in ext.most_common(14):
    print(f"  {e:8s} {n:7d}  {size[e]/2**20:10.1f} MiB")
print("=== top folders ===")
for k, v in per_top.most_common(12):
    print(f"  {v/2**20:9.1f} MiB  {k}")
print("files with '~' in name:", len(dup), dup[:5])

random.seed(0)
sample = random.sample(oggs, min(40, len(oggs)))
bad = []
for p in sample:
    try:
        i = sf.info(p)
        if i.frames <= 0:
            bad.append(p)
    except Exception as ex:
        bad.append(f"{p}: {ex}")
print(f"audio integrity: sampled {len(sample)}, bad {len(bad)}")
for b in bad[:5]:
    print("  ", b)
