#!/usr/bin/env python3
"""Curated, restartable patcher: makes the decompiled stub sources compile.

Everything it changes is recorded in work/patches.json.  Rules:
  * drop exactly the interfaces the compiler complains about (depth-aware),
    never touching the first entry of a class declaration (= its base class)
  * (T)null  ->  default(T)          (CS0037)
  * assign missing out parameters    (CS0177, guarded)
  * strip named attribute arguments  (CS0617)
  * [Optional] + default value       (CS1737/CS1745)
"""
import os, re, json, subprocess, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}

# declarations known to need their base class re-added explicitly
PREPEND_BASE = {
    r"CustomInspector\CustomInspector\SerializableDictionary.cs":
        ("SerializableDictionary<TKey, TValue>", "Dictionary<TKey, TValue>"),
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
    return out


def iface_of(expr):
    m = re.match(r"^([A-Za-z_][\w]*(?:\s*<.*>)?)\s*\.", expr.strip())
    return m.group(1).strip() if m else None


def simple(n):
    return re.sub(r"<.*", "", n).strip()


def patch_ifaces(path, items, patched):
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    lines = text.split("\n")
    text = "\n".join(lines)

    # optional: make sure a known base class is present
    rel = os.path.relpath(path, DEC)
    if rel in PREPEND_BASE:
        tname, base = PREPEND_BASE[rel]
        m = re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"([^\n{]*)", text)
        if m and base not in m.group(0):
            head = m.group(0)
            decl = head[: head.find(":")] if ":" in head else head
            rest = head[head.find(":") + 1:] if ":" in head else ""
            new = decl.rstrip() + " : " + base + (", " + rest.strip() if rest.strip() else "")
            text = text.replace(head, new, 1)
            patched.append({"file": path, "fix": "prepend base class", "base": base})
    lines = text.split("\n")

    drop = collections.defaultdict(set)
    for line, code, msg in items:
        if code not in IFACE_CODES:
            continue
        q = QUOTED.findall(msg)
        if len(q) >= 2:
            t = q[0].split("<")[0].split(".")[-1]
            i = iface_of(q[1])
            if i:
                drop[t].add(i)

    for tname, ifaces in drop.items():
        for i, ln in enumerate(lines):
            if ln is None:
                continue
            if not re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"\b", ln):
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
                entries = [x.strip() for x in split_top(rest) if x.strip()]
                # never drop the very first entry of a class (it may be the base class)
                keep = [e for k, e in enumerate(entries)
                        if not (simple(e) in ifaces and not (k == 0 and "class" in decl))]
                if not keep and any("class" in decl for _ in [0]):
                    keep = []          # class without base class is fine
                newdecl = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + "\n"
            else:
                newdecl = decl.rstrip() + "\n"
            lines[i] = newdecl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            patched.append({"file": path, "type": tname, "dropped": sorted(ifaces)})
            break
    lines = [x for x in lines if x is not None]
    text = "\n".join(lines)

    for line, code, msg in items:
        if code == "CS0037":
            i = line - 1
            if 0 <= i < len(lines) and "null" in lines[i]:
                new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", lines[i], count=1)
                if new != lines[i]:
                    lines[i] = new
                    patched.append({"file": path, "fix": "cast-null-to-default"})

    out = []
    idx = 0
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
            print("    MANUAL out-param:", os.path.relpath(path, ROOT), line, q[-1])
    for i, ln in enumerate(lines):
        indent = re.match(r"\s*", ln).group(0)
        for pname in ins.get(i, []):
            out.append(f"{indent}{pname} = default;")
            patched.append({"file": path, "fix": "assign-out-param", "param": pname})
        out.append(ln)
    lines = out

    text = "\n".join(lines)
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
                    ln = ln[: m.start()] + ("(" + ", ".join(a.strip() for a in keep) + ")" if keep else "") + ln[m.end():]
                    n += 1
            res.append(ln)
        text = "\n".join(res)
        if n:
            patched.append({"file": path, "fix": "strip-named-attr-args", "count": n})

    if any(c in ("CS1737", "CS1745") for _, c, _ in items):
        n1 = len(re.findall(r"\[Optional\]", text))
        text = re.sub(r"\[Optional\]\s*", "", text)
        if n1:
            patched.append({"file": path, "fix": "drop [Optional]", "count": n1})

    open(path, "w", encoding="utf-8-sig", newline="").write(text)


def main():
    allp = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    for it in range(1, 7):
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
                patch_ifaces(f, items, patched)
        print("    ops:", len(patched), flush=True)
        allp.extend(patched)
        if not patched:
            print("    nothing left to auto-patch")
            break
    json.dump(allp, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
