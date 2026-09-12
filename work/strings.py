import json, os
p = r"E:\manosaba_re\src\il2cpp\stringliteral.json"
d = json.load(open(p, encoding="utf-8"))
out = r"E:\manosaba_re\src\il2cpp\stringliterals.txt"
lines = []
for e in d:
    v = e.get("value")
    if v is None:
        continue
    lines.append(e.get("address", "") + "\t" + v.replace("\n", "\\n"))
with open(out, "w", encoding="utf-8") as fh:
    fh.write("\n".join(lines))
print("wrote", len(lines), os.path.getsize(out))
