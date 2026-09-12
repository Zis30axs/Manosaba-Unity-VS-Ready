#!/usr/bin/env python3
"""Final patcher: makes the decompiled stub sources of the IDE project compile.

Recorded in work/patches.json; safe to re-run (idempotent).
"""
import os, re, json, subprocess, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}

# class declarations whose base class must be (re)inserted before the interface list
BASE_OF = {
    ("CustomInspector\\CustomInspector\\SerializableDictionary.cs", "SerializableDictionary"): "Dictionary<TKey, TValue>",
}


def build():
    env = dict(os.environ, NUGET_PACKAGES=os.path.join(ROOT, "tools", "nuget"))
    p = subprocess.run(["dotnet", "build", "Manosaba.sln", "-v", "q", "--nologo"],
                       cwd=PROJ, capture_output=True, text=True, encoding="utf-8",
                       errors="replace", env=env)
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


def iface_of(expr):
    m = re.match(r"^([A-Za-z_][\w]*(?:\s*<.*>)?)\s*\.", expr.strip())
    return m.group(1).strip() if m else None


def simple(n):
    return re.sub(r"<.*", "", n).strip()


def patch(path, items, patched):
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    lines = text.split("\n")

    # ---- 1. drop the interfaces the compiler says are not implemented --------
    drop = collections.defaultdict(set)
    for line, code, msg in items:
        if code in IFACE_CODES:
            q = QUOTED.findall(msg)
            if len(q) >= 2:
                t = q[0].split("<")[0].split(".")[-1]
                i = iface_of(q[1])
                if i:
                    drop[t].add(i)

    for tname, ifaces in drop.items():
        for i, ln in enumerate(lines):
            if ln is None or not re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"\b", ln):
                continue
            j, head = i, ln
            while "{" not in head and j + 1 < len(lines):
                j += 1
                head += "\n" + lines[j]
            brace = head.find("{")
            decl, tail = (head[:brace], head[brace:]) if brace >= 0 else (head, "")
            m = re.search(r"\s:\s", decl)
            if m:
                base = decl[: m.start()]
                rest = decl[m.end():].replace("\n", " ")
                where = ""
                wm = re.search(r"\bwhere\b", rest)
                if wm:
                    where = " " + rest[wm.start():].strip()
                    rest = rest[: wm.start()]
                entries = split_top(rest)
                forced = BASE_OF.get((rel, tname))
                keep = ([forced] if forced else []) + [e for e in entries if simple(e) not in ifaces]
                seen, uniq = set(), []
                for e in keep:
                    if simple(e) not in seen:
                        seen.add(simple(e))
                        uniq.append(e)
                newdecl = base.rstrip() + (" : " + ", ".join(uniq) if uniq else "") + where + "\n"
            else:
                newdecl = decl.rstrip() + "\n"
            lines[i] = newdecl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            patched.append({"file": path, "type": tname, "dropped": sorted(ifaces)})
            break
    lines = [x for x in lines if x is not None]
    text = "\n".join(lines)

    # ---- 2. (T)null -> default(T) -------------------------------------------
    if any(c == "CS0037" for _, c, _ in items):
        out = []
        for n, ln in enumerate(text.split("\n"), 1):
            if any(l == n and c == "CS0037" for l, c, _ in items) and "null" in ln:
                new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", ln, count=1)
                if new != ln:
                    patched.append({"file": path, "fix": "cast-null-to-default"})
                    ln = new
            out.append(ln)
        text = "\n".join(out)

    # ---- 3. assign out parameters -------------------------------------------
    if any(c == "CS0177" for _, c, _ in items):
        lines = text.split("\n")
        ins = collections.defaultdict(list)
        for line, code, msg in items:
            if code != "CS0177":
                continue
            q = QUOTED.findall(msg)
            if not q:
                continue
            i = line - 1
            if 0 <= i < len(lines) and (lines[i].strip() == "}" or lines[i].strip().startswith("return")):
                ins[i].append(q[-1].strip())
            else:
                print("    MANUAL out-param:", rel, line, q[-1])
        out = []
        for i, ln in enumerate(lines):
            indent = re.match(r"\s*", ln).group(0)
            for pname in ins.get(i, []):
                out.append(f"{indent}{pname} = default;")
                patched.append({"file": path, "fix": "assign-out-param", "param": pname})
            out.append(ln)
        text = "\n".join(out)

    # ---- 4. named attribute arguments ---------------------------------------
    if any(c == "CS0617" for _, c, _ in items):
        bad = set()
        for _, _, m in items:
            bad.update(QUOTED.findall(m))
        res, n = [], 0
        for ln in text.split("\n"):
            if ln.lstrip().startswith("[") and any(b in ln for b in bad):
                m = re.search(r"\(([^()]*)\)", ln)
                if m:
                    keep = [a for a in split_top(m.group(1)) if a.split("=")[0].strip() not in bad]
                    ln = ln[: m.start()] + ("(" + ", ".join(keep) + ")" if keep else "") + ln[m.end():]
                    n += 1
            res.append(ln)
        text = "\n".join(res)
        if n:
            patched.append({"file": path, "fix": "strip-named-attr-args", "count": n})

    # ---- 5. [Optional] parameter needs a real default value ------------------
    if any(c in ("CS1737", "CS1745") for _, c, _ in items):
        new, n = re.subn(r"\[Optional\](\s+[\w\.\<\>\[\]]+\s+\w+)(\s*[,)])",
                         lambda m: m.group(1) + " = default" + m.group(2), text)
        if n:
            text = new
            patched.append({"file": path, "fix": "optional-default", "count": n})

    open(path, "w", encoding="utf-8-sig", newline="").write(text)


def main():
    allp = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    for it in range(1, 8):
        out = build()
        errs = ERR.findall(out)
        codes = collections.Counter(e[3] for e in errs)
        print(f"--- iter {it}: {len(errs)} errors {dict(codes)}", flush=True)
        if not errs:
            print("*** BUILD CLEAN ***")
            break
        byfile = collections.defaultdict(list)
        for f, l, c, code, msg in errs:
            byfile[f].append((int(l), code, msg))
        patched = []
        for f, items in byfile.items():
            if os.path.exists(f):
                patch(f, items, patched)
        print("    ops:", len(patched), flush=True)
        allp.extend(patched)
        if not patched:
            break
    json.dump(allp, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
