import os, json, collections
W = r"E:\manosaba_re\work"
done = sorted(os.listdir(os.path.join(W, "done_audio")))
print("markers:", len(done))
for d in done[:40]:
    print("  ", d)
srcs = collections.Counter()
with open(os.path.join(W, "manifest_audio.jsonl"), encoding="utf-8") as fh:
    for line in fh:
        line = line.strip()
        if line:
            srcs[json.loads(line)["src"]] += 1
print("manifest bundles:", len(srcs))
for k, v in list(srcs.items())[:40]:
    print(f"  {v:5d}  {k}")
