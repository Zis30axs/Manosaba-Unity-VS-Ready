#!/usr/bin/env python3
"""CS7036: add default values to the constructors that are called implicitly as base().

The base class usually lives in another file, so this pass is global.
"""
import os, re, glob, json, subprocess, collections

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


def main():
    want = {}          # class name -> set of arities
    for it in range(1, 8):
        txt = build()
        errs = ERR.findall(txt)
        targets = collections.defaultdict(set)
        for f, l, c, code, msg in errs:
            if code != "CS7036":
                continue
            m = QUOTED.search(msg)
            if not m or "(" not in m.group(1):
                continue
            full = m.group(1)
            cls = full[: full.find("(")].split(".")[-1].split("<")[0]
            args = split_top(full[full.find("(") + 1: full.rfind(")")])
            targets[cls].add(len(args))
        print(f"--- iteration {it}: {len(errs)} errors; CS7036 classes: {len(targets)}", flush=True)
        if not targets:
            break
        changed = 0
        for p in glob.glob(os.path.join(DEC, "**", "*.cs"), recursive=True):
            t = open(p, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
            lines = t.split("\n")
            dirty = False
            for i, ln in enumerate(lines):
                for cls, arities in targets.items():
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
                    if len(args) not in arities:
                        continue
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
        print("    ctors patched:", changed, flush=True)
        if not changed:
            break


if __name__ == "__main__":
    main()
