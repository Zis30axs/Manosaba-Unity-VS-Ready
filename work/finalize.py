"""Merge audio manifests, rebuild res/_index.csv (+ audio CSV), report final stats."""
import os, json, csv, collections

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"

audio = []
for f in sorted(os.listdir(WORK)):
    if f.startswith("manifest_audio_") and f.endswith(".jsonl"):
        for line in open(os.path.join(WORK, f), encoding="utf-8"):
            line = line.strip()
            if line:
                audio.append(json.loads(line))

# de-duplicate (same output path -> keep first)
seen = set()
uniq = []
for e in audio:
    if e["out"] in seen:
        continue
    seen.add(e["out"])
    uniq.append(e)
audio = uniq
with open(os.path.join(WORK, "manifest_audio.jsonl"), "w", encoding="utf-8") as fh:
    for e in audio:
        fh.write(json.dumps(e, ensure_ascii=False) + "\n")
print("audio entries", len(audio))

core = []
with open(os.path.join(WORK, "manifest_core.jsonl"), encoding="utf-8") as fh:
    for line in fh:
        line = line.strip()
        if line:
            core.append(json.loads(line))
print("core entries", len(core))

allE = core + audio
missing = [e for e in allE if not os.path.exists(os.path.join(RES, e["out"].replace("/", os.sep)))]
print("missing files:", len(missing))
for e in missing[:5]:
    print("  ", e["out"])

with open(os.path.join(RES, "_index.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.writer(fh)
    w.writerow(["type", "name", "asset_path", "output_file", "source_bundle", "path_id"])
    for e in sorted(allE, key=lambda x: (x["type"], x["out"])):
        w.writerow([e["type"], e.get("name") or "", e.get("container") or "", e["out"],
                    e.get("src") or "", e.get("path_id")])

with open(os.path.join(RES, "_index_audio.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.writer(fh)
    w.writerow(["name", "asset_path", "output_file", "source_bundle"])
    for e in sorted(audio, key=lambda x: x["out"]):
        w.writerow([e.get("name") or "", e.get("container") or "", e["out"], e.get("src") or ""])

c = collections.Counter(e["type"] for e in allE)
print("=== final type counts ===")
for k, v in c.most_common(30):
    print(f"  {k:28s} {v}")
tot = sum(os.path.getsize(os.path.join(r, f)) for r, d, fs in os.walk(RES) for f in fs)
print("res total GB", round(tot / 2 ** 30, 2))
