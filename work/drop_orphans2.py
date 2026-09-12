"""Second pass: delete orphan PNGs that are byte-identical to a same-container sibling."""
import os, re, csv, hashlib, json

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
known = set()
with open(os.path.join(RES, "_index.csv"), encoding="utf-8-sig") as fh:
    for row in csv.DictReader(fh):
        known.add(row["output_file"])

orphans = []
for root, dirs, files in os.walk(RES):
    for f in files:
        rel = os.path.relpath(os.path.join(root, f), RES).replace(os.sep, "/")
        if rel in known or rel.startswith("_index") or rel.startswith("_monoscripts"):
            continue
        orphans.append(rel)


def md5(p, limit=None):
    h = hashlib.md5()
    with open(p, "rb") as fh:
        for chunk in iter(lambda: fh.read(1 << 20), b""):
            h.update(chunk)
    return h.hexdigest()


deleted = 0
left = []
for rel in orphans:
    p = os.path.join(RES, rel.replace("/", os.sep))
    base = re.sub(r"~\d+(?=\.[^.]+$)", "", p)
    if base != p and os.path.exists(base) and os.path.getsize(base) == os.path.getsize(p):
        if md5(base) == md5(p):
            os.remove(p)
            deleted += 1
            continue
    left.append(rel)

print("deleted identical dups:", deleted, "left:", len(left))
for r in left[:10]:
    print("  left", r)
with open(os.path.join(WORK, "orphans_left.json"), "w", encoding="utf-8") as fh:
    json.dump(left, fh, ensure_ascii=False, indent=1)
