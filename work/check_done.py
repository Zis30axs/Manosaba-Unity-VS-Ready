import os, json, re
W = r"E:\manosaba_re\work"
ft = json.load(open(os.path.join(W, "file_types.json"), encoding="utf-8"))
SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')


def marker(p):
    parts = [SAFE.sub("_", s).strip().rstrip(". ") for s in p.replace("\\", "/").split("/")]
    return "__".join([x for x in parts if x]) + ".done"


done = set(os.listdir(os.path.join(W, "done_audio")))
tot = 0
rows = []
for k, v in ft.items():
    if "AudioClip" in v.get("types", {}):
        if marker(k) in done:
            rows.append((v["types"]["AudioClip"], k))
            tot += v["types"]["AudioClip"]
rows.sort(reverse=True)
print("done bundles", len(rows), "clips in them", tot)
for c, k in rows[:20]:
    print(f"  {c:5d}  {os.path.basename(k)}")
ml = sum(1 for _ in open(os.path.join(W, "manifest_audio.jsonl"), encoding="utf-8"))
print("manifest_audio lines", ml)
