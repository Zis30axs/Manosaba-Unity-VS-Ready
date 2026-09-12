#!/usr/bin/env python3
"""CS1729 (base class has no parameterless ctor) + CS0104 (ambiguous reference)."""
import os, re, glob, subprocess, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
QUOTED = re.compile(r"[\u201c\"]([^\u201d\"]+)[\u201d\"]")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*)", re.M)


def build():
    env = dict(os.environ, NUGET_PACKAGES=os.path.join(ROOT, "tools", "nuget"))
    p = subprocess.run(["dotnet", "build", "Manosaba.sln", "-v", "q", "--nologo"], cwd=PROJ,
                       capture_output=True, text=True, encoding="utf-8", errors="replace", env=env)
    return p.stdout + p.stderr


def split_top(s):
    out, depth, cur = [], 0, ""
    for ch in s:
        if ch in "(<[":
            depth += 1
        elif ch in ")>]":
            depth -= 1
        if ch == "," and depth == 0:
            out.append(cur); cur = ""
        else:
            cur += ch
    out.append(cur)
    return [x.strip() for x in out if x.strip()]


txt = build()
errs = ERR.findall(txt)
print("errors:", len(errs), collections.Counter(e[3] for e in errs).most_common())

# CS1729 -> give every ctor of the named class a parameterless form
ctrs = set()
amb = []
for f, l, c, code, msg in errs:
    q = QUOTED.findall(msg)
    if code == "CS1729" and q:
        ctrs.add(q[0].split("<")[0].strip())
    if code == "CS0104":
        amb.append((f, int(l), msg))
print("CS1729 classes:", sorted(ctrs))
changed = 0
for p in glob.glob(os.path.join(DEC, "**", "*.cs"), recursive=True):
    t = open(p, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    lines = t.split("\n")
    dirty = False
    for i, ln in enumerate(lines):
        for cls in ctrs:
            if not re.search(r"\b(public|protected|internal|private)\s+" + re.escape(cls) + r"\s*\(", ln):
                continue
            head, j = ln, i
            while ")" not in head and j + 1 < len(lines):
                j += 1
                head += " " + lines[j].strip()
            om = re.search(r"\((.*)\)", head)
            if not om:
                continue
            args = split_top(om.group(1))
            new_args = [a if ("=" in a.split("(")[0] or "[Optional]" in a) else a.rstrip() + " = default"
                        for a in args]
            if new_args == args:
                continue
            lines[i] = head[: om.start()] + "(" + ", ".join(new_args) + ")" + head[om.end():]
            for k in range(i + 1, j + 1):
                lines[k] = None
            dirty = True
            changed += 1
            break
    if dirty:
        open(p, "w", encoding="utf-8-sig", newline="").write("\n".join(x for x in lines if x is not None))
print("ctors given defaults:", changed)
for f, l, m in amb:
    print("  AMBIGUOUS:", f.split("decompiled")[-1], l, m[:150])
