"""Remove out-parameter assignments that the patcher inserted into the wrong method."""
import os, re, glob

DEC = r"E:\manosaba_re\src\decompiled"
ASSIGN = re.compile(r"^(\s*)(@?\w+) = default;$")
removed = kept = 0

for p in glob.glob(os.path.join(DEC, "**", "*.cs"), recursive=True):
    t = open(p, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    lines = t.split("\n")
    out = []
    for i, ln in enumerate(lines):
        m = ASSIGN.match(ln)
        if not m:
            out.append(ln)
            continue
        name = m.group(2).lstrip("@")
        # find the enclosing method signature
        sig = None
        for k in range(i, max(-1, i - 60), -1):
            if "(" in lines[k] and lines[k].rstrip().endswith(")"):
                sig = lines[k]
                for j in range(k + 1, min(len(lines), k + 4)):
                    if "(" in lines[j] and lines[j].rstrip().endswith(")"):
                        sig = lines[j]
                break
            if "(" in lines[k] and ")" in lines[k]:
                sig = lines[k]
                break
        ok = False
        if sig:
            om = re.search(r"\((.*)\)", sig)
            if om:
                params = [x.strip() for x in om.group(1).split(",")]
                for prm in params:
                    parts = prm.replace("[", " ").replace("]", " ").split()
                    if parts and parts[-1].lstrip("@") == name:
                        ok = True
        if ok:
            out.append(ln)
            kept += 1
        else:
            removed += 1
    if removed:
        open(p, "w", encoding="utf-8-sig", newline="").write("\n".join(out))

print("stray assignments removed:", removed, "kept:", kept)
