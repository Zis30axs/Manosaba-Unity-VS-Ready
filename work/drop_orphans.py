"""Remove orphan PNGs left behind by an early (aborted) audio-stage run that also exported
images.  Kills a file only when an identical-size sibling without the ~N suffix exists."""
import os, re, csv, hashlib

RES = r"E:\manosaba_re\res"
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

print("orphans", len(orphans))
deleted = kept = 0
for rel in orphans:
    p = os.path.join(RES, rel.replace("/", os.sep))
    base = re.sub(r"~\d+(?=\.[^.]+$)", "", p)
    if base != p and os.path.exists(base) and os.path.getsize(base) == os.path.getsize(p):
        os.remove(p)
        deleted += 1
    else:
        kept += 1
        print("  KEEP", rel)
print("deleted", deleted, "kept", kept)
