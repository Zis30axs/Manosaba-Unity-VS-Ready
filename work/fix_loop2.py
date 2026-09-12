#!/usr/bin/env python3
"""Second-round patcher: out-parameters, (T)null casts, attribute named args."""
import os, re, json, subprocess, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")


def build():
    env = dict(os.environ, NUGET_PACKAGES=os.path.join(ROOT, "tools", "nuget"))
    p = subprocess.run(["dotnet", "build", "Manosaba.sln", "-v", "q", "--nologo"],
                       cwd=PROJ, capture_output=True, text=True, encoding="utf-8",
                       errors="replace", env=env)
    return p.stdout + p.stderr


def split_args(s):
    out, depth, cur = [], 0, ""
    for ch in s:
        if ch in "(<[":
            depth += 1
        elif ch in ")>]":
            depth -= 1
        if ch == "," and depth == 0:
            out.append(cur)
            cur = ""
        else:
            cur += ch
    out.append(cur)
    return out


def fix(path, items, patched):
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    lines = text.split("\n")

    # CS0037:  return (T)null;   ->   return default(T);
    for line, code, msg in items:
        if code != "CS0037":
            continue
        i = line - 1
        if 0 <= i < len(lines) and "null" in lines[i]:
            new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", lines[i], count=1)
            if new != lines[i]:
                lines[i] = new
                patched.append({"file": path, "fix": "cast-null-to-default", "code": code})

    # CS0177: assign the out parameter right before the failing return
    for line, code, msg in items:
        if code != "CS0177":
            continue
        q = QUOTED.findall(msg)
        if not q:
            continue
        pname = q[-1].strip()
        i = line - 1
        if 0 <= i < len(lines):
            indent = re.match(r"\s*", lines[i]).group(0)
            lines.insert(i, f"{indent}{pname} = default;")
            patched.append({"file": path, "fix": "assign-out-param", "param": pname})

    # CS0617: named attribute arguments that the reference assembly cannot set
    if any(c == "CS0617" for _, c, _ in items):
        bad = set()
        for _, _, msg in items:
            bad.update(QUOTED.findall(msg))
        for i, ln in enumerate(lines):
            if not (ln.lstrip().startswith("[") and any(b in ln for b in bad)):
                continue
            m = re.search(r"\(([^()]*)\)", ln)
            if not m:
                continue
            keep = [a for a in split_args(m.group(1)) if a.split("=")[0].strip() not in bad]
            if not keep:
                newln = ln[: m.start()] + ln[m.end():]
            else:
                newln = ln[: m.start()] + "(" + ", ".join(a.strip() for a in keep) + ")" + ln[m.end():]
            lines[i] = newln
            patched.append({"file": path, "fix": "strip-named-attr-args", "names": sorted(bad)})

    open(path, "w", encoding="utf-8-sig", newline="").write("\n".join(lines))


def main():
    allp = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    for it in range(1, 7):
        out = build()
        errs = ERR.findall(out)
        codes = collections.Counter(e[3] for e in errs)
        print(f"--- iter {it}: {len(errs)} errors {dict(codes)}", flush=True)
        if not errs:
            print("BUILD CLEAN")
            break
        byfile = collections.defaultdict(list)
        for f, l, c, code, msg in errs:
            byfile[f].append((int(l), code, msg))
        patched = []
        for f, items in byfile.items():
            if os.path.exists(f):
                fix(f, items, patched)
        print("    ops:", len(patched), flush=True)
        allp.extend(patched)
        if not patched:
            break
    json.dump(allp, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
