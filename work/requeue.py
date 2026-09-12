import os, re, sys
LOG = r"E:\manosaba_re\work\core_log.txt"
DONE = r"E:\manosaba_re\work\done_core"

def safe_relpath(p):
    SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')
    parts = []
    for seg in p.replace("\\", "/").split("/"):
        seg = SAFE.sub("_", seg).strip().rstrip(". ")
        if seg:
            parts.append(seg)
    return "/".join(parts)

bad = set()
for line in open(LOG, encoding="utf-8", errors="replace"):
    m = re.search(r"ERR (\S+) (export|fatal)", line)
    if m:
        bad.add(m.group(1))
print("bundles with export errors:", len(bad))
for b in sorted(bad):
    mk = os.path.join(DONE, safe_relpath(b).replace("/", "__") + ".done")
    if os.path.exists(mk):
        os.remove(mk)
        print("  requeued", b)
    else:
        print("  (no marker)", b)
