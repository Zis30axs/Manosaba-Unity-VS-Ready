"""Kill-safe cleanup: for bundles currently claimed, delete their partly written .ogg outputs
and release the claim, so they get re-extracted cleanly."""
import os, sys, json, re
sys.path.insert(0, r"E:\manosaba_re\work")
import UnityPy
from UnityPy.enums import ClassIDType
import audio_worker as AW

DATA = AW.DATA
claims = [f for f in os.listdir(AW.CLAIMS)]
tm = json.load(open(os.path.join(AW.WORK, "file_types.json"), encoding="utf-8"))
by_marker = {AW.marker(rel) + ".claim": rel for rel in tm}

for c in claims:
    rel = by_marker.get(c)
    if rel is None:
        print("unknown claim", c)
        continue
    fp = os.path.join(DATA, rel)
    env = UnityPy.load(fp)
    removed = 0
    for obj in env.objects:
        if obj.type != ClassIDType.AudioClip:
            continue
        cont = obj.container
        if not cont:
            continue
        d = obj.read()
        relc = AW.safe_relpath(cont)
        dstdir = os.path.join(AW.OUT, os.path.dirname(relc))
        stem = os.path.basename(relc)
        if "." in stem:
            stem = stem.rsplit(".", 1)[0]
        for cand in os.listdir(dstdir) if os.path.isdir(dstdir) else []:
            if cand == stem + ".ogg" or cand.startswith(stem + "~"):
                os.remove(os.path.join(dstdir, cand))
                removed += 1
    print(f"{os.path.basename(rel)}: removed {removed} outputs")
    os.remove(os.path.join(AW.CLAIMS, c))
print("claims left", len(os.listdir(AW.CLAIMS)))
