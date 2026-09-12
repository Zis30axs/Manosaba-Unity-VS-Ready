"""Flatten res/_serialized/_serialized/* -> res/_serialized/* and fix the manifest/index accordingly."""
import json, os, shutil, csv

RES = r"E:\manosaba_re\res"
MAN = r"E:\manosaba_re\work\manifest_core.jsonl"
NEST = os.path.join(RES, "_serialized", "_serialized")

moved = dropped = 0
for root, dirs, files in os.walk(NEST, topdown=False):
    for f in files:
        src = os.path.join(root, f)
        dst = os.path.join(RES, "_serialized", os.path.relpath(src, NEST))
        os.makedirs(os.path.dirname(dst), exist_ok=True)
        if os.path.exists(dst):
            os.remove(src)
            dropped += 1
        else:
            shutil.move(src, dst)
            moved += 1
    for d in dirs:
        p = os.path.join(root, d)
        try:
            os.rmdir(p)
        except OSError:
            pass
if os.path.isdir(NEST):
    try:
        os.rmdir(NEST)
    except OSError:
        print("nest not empty")
print("flattened", moved, "dropped dups", dropped)

entries = []
with open(MAN, encoding="utf-8") as fh:
    for line in fh:
        line = line.strip()
        if not line:
            continue
        e = json.loads(line)
        if e["out"].startswith("_serialized/_serialized/"):
            e["out"] = "_serialized/" + e["out"][len("_serialized/_serialized/"):]
        entries.append(e)
with open(MAN, "w", encoding="utf-8") as fh:
    for e in entries:
        fh.write(json.dumps(e, ensure_ascii=False) + "\n")

with open(os.path.join(RES, "_index.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.writer(fh)
    w.writerow(["type", "name", "asset_path", "output_file", "source_bundle", "path_id"])
    for e in sorted(entries, key=lambda x: (x["type"], x["out"])):
        w.writerow([e["type"], e.get("name") or "", e.get("container") or "", e["out"],
                    e.get("src") or "", e.get("path_id")])
print("entries", len(entries))
miss = sum(1 for e in entries if not os.path.exists(os.path.join(RES, e["out"].replace("/", os.sep))))
print("manifest entries whose file is missing:", miss)
