"""Parse src/il2cpp/dump.cs into a navigable type index (CSV + markdown overview)."""
import re, os, csv, collections

SRC = r"E:\manosaba_re\src"
DUMP = os.path.join(SRC, "il2cpp", "dump.cs")

re_img = re.compile(r"^// Image (\d+): (\S+?) - (\d+)")
re_ns = re.compile(r"^// Namespace: (.*)$")
re_type = re.compile(r"^\s*(?:\[.*\]\s*)?(?:public |internal |private |protected )?"
                     r"(?:sealed |abstract |static |readonly |unsafe |partial )*"
                     r"(class|struct|enum|interface|delegate)\s+([A-Za-z_<>,\.\[\]@0-9]+).*?//\s*TypeDefIndex:\s*(\d+)")

images = []
rows = []
ns = ""
with open(DUMP, encoding="utf-8", errors="replace") as fh:
    for line in fh:
        m = re_img.match(line)
        if m:
            images.append((int(m.group(3)), m.group(2)))
            continue
        m = re_ns.match(line)
        if m:
            ns = m.group(1).strip()
            continue
        m = re_type.match(line)
        if m:
            kind, name, idx = m.group(1), m.group(2), int(m.group(3))
            name = name.split("<")[0]
            rows.append({"namespace": ns, "type": name, "kind": kind, "typedef": idx})

images.sort()
starts = [s for s, _ in images]


def asm_of(idx):
    lo, hi = 0, len(images) - 1
    while lo < hi:
        mid = (lo + hi + 1) // 2
        if starts[mid] <= idx:
            lo = mid
        else:
            hi = mid - 1
    return images[lo][1]


for r in rows:
    r["assembly"] = asm_of(r["typedef"])

with open(os.path.join(SRC, "type_index.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.DictWriter(fh, fieldnames=["assembly", "namespace", "type", "kind", "typedef"])
    w.writeheader()
    for r in rows:
        w.writerow(r)

per_asm = collections.Counter(r["assembly"] for r in rows)
per_ns = collections.defaultdict(list)
for r in rows:
    per_ns[r["assembly"]].append(r["namespace"])

with open(os.path.join(SRC, "type_index.md"), "w", encoding="utf-8") as fh:
    fh.write(f"# 类型索引（来自 src/il2cpp/dump.cs）\n\n共 **{len(rows)}** 个类型 / {len(per_asm)} 个程序集\n\n")
    for asm, cnt in per_asm.most_common():
        fh.write(f"## {asm} ({cnt})\n\n")
        c2 = collections.Counter(per_ns[asm])
        for n, c in sorted(c2.items(), key=lambda x: (-x[1], x[0]))[:40]:
            fh.write(f"* `{n or '(global)'}` — {c}\n")
        fh.write("\n")
print("types", len(rows), "assemblies", len(per_asm))
for k, v in per_asm.most_common(10):
    print(f"  {k}: {v}")
