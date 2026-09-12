"""Re-extract a single bundle from scratch (files + manifest entries + done marker)."""
import os, json, shutil, sys

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
BUNDLE = sys.argv[1] if len(sys.argv) > 1 else r"StreamingAssets\aa\StandaloneWindows64\naninovel-spawn_assets_all.bundle"
PREFIX = sys.argv[2] if len(sys.argv) > 2 else "Assets/#WitchTrials/Prefabs/Naninovel/Spawn"

# 1. manifest entries for this bundle
MAN = os.path.join(WORK, "manifest_core.jsonl")
keep = []
removed = 0
for line in open(MAN, encoding="utf-8"):
    line = line.strip()
    if not line:
        continue
    e = json.loads(line)
    if e.get("src") == BUNDLE:
        removed += 1
        continue
    keep.append(e)
with open(MAN, "w", encoding="utf-8") as fh:
    for e in keep:
        fh.write(json.dumps(e, ensure_ascii=False) + "\n")
print("manifest entries removed:", removed, "kept:", len(keep))

# 2. files
d = os.path.join(RES, PREFIX.replace("/", os.sep))
if os.path.isdir(d):
    n = sum(len(f) for _, _, f in os.walk(d))
    shutil.rmtree(d)
    print("deleted files under", PREFIX, "=", n)

# 3. marker
import re
SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')
parts = [SAFE.sub("_", s).strip().rstrip(". ") for s in BUNDLE.replace("\\", "/").split("/")]
mk = os.path.join(WORK, "done_core", "__".join([p for p in parts if p]) + ".done")
if os.path.exists(mk):
    os.remove(mk)
    print("marker cleared")
