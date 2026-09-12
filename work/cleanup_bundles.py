"""Delete all outputs produced for a set of bundles (manifest + files), so they can be re-extracted cleanly."""
import json, os, re, sys

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
MAN = os.path.join(WORK, "manifest_core.jsonl")

bad = set()
for line in open(os.path.join(WORK, "core_log.txt"), encoding="utf-8", errors="replace"):
    m = re.search(r"ERR (\S+) (export|fatal)", line)
    if m:
        bad.add(m.group(1))
if len(sys.argv) > 1:
    bad = set(sys.argv[1:])

keep, removed = [], 0
for line in open(MAN, encoding="utf-8"):
    line = line.strip()
    if not line:
        continue
    e = json.loads(line)
    if e.get("src") in bad:
        p = os.path.join(RES, e["out"].replace("/", os.sep))
        if os.path.exists(p):
            os.remove(p)
            removed += 1
        continue
    keep.append(e)

with open(MAN, "w", encoding="utf-8") as fh:
    for e in keep:
        fh.write(json.dumps(e, ensure_ascii=False) + "\n")
print("targets", len(bad), "files removed", removed, "entries kept", len(keep))

DONE = os.path.join(WORK, "done_core")
SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')
for b in bad:
    parts = [SAFE.sub("_", s).strip().rstrip(". ") for s in b.replace("\\", "/").split("/")]
    mk = os.path.join(DONE, "__".join([p for p in parts if p]) + ".done")
    if os.path.exists(mk):
        os.remove(mk)
print("markers cleared")
