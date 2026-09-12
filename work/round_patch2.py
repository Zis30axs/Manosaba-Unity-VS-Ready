#!/usr/bin/env python3
"""Snapshot patcher v2: for every type the compiler complains about, drop *all* interface
entries from its declaration (keeping real base classes, detected via dump.cs), plus the
usual out-param / (T)null / attribute fixes.

Usage: python round_patch2.py <buildlog.txt>
"""
import os, re, sys, json, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
DUMP = os.path.join(ROOT, "src", "il2cpp", "dump.cs")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}

BASE_OF = {
    (r"CustomInspector\CustomInspector\SerializableDictionary.cs", "SerializableDictionary"): "Dictionary<TKey, TValue>",
    (r"CustomInspector\CustomInspector\SerializableSet.cs", "SerializableSet"): "HashSet<T>",
    (r"CustomInspector\CustomInspector\ListContainer.cs", "ListContainer"): "List<T>",
}

# all interface names known to the game (from dump.cs)
ifaces_known = set()
for line in open(DUMP, encoding="utf-8", errors="replace"):
    m = re.search(r"\binterface\s+([A-Za-z_][\w]*)", line)
    if m:
        ifaces_known.add(m.group(1))
print("interfaces known from dump.cs:", len(ifaces_known))

logs = []


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


def simple(n):
    return re.sub(r"<.*", "", n).strip().split(".")[-1]


def patch(path, items):
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")

    need = {QUOTED.findall(m)[0].split("<")[0].split(".")[-1]
            for _, c, m in items if c in IFACE_CODES and QUOTED.findall(m)}

    for tname in need:
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
                rest = decl[m.end():].replace("\n", " ").strip()
                where = ""
                wm = re.search(r"\bwhere\b", rest)
                if wm:
                    where = " " + rest[wm.start():].strip()
                    rest = rest[: wm.start()]
                entries = split_top(rest)
                forced = BASE_OF.get((rel, tname))
                cand = ([forced] if forced else []) + entries
                keep, seen = [], set()
                for e in cand:
                    s = simple(e)
                    if s in seen:
                        continue
                    if s in ifaces_known:          # interface -> not implemented in the stub
                        continue
                    seen.add(s)
                    keep.append(e)
                newdecl = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + where + "\n"
            else:
                newdecl = decl.rstrip() + "\n"
            lines[i] = newdecl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "type": tname, "kept": [e for e in keep] if m else []})
            break
    text = "\n".join(x for x in lines if x is not None)
    lines = text.split("\n")

    for line, code, msg in items:
        if code == "CS0037":
            i = line - 1
            if 0 <= i < len(lines) and "null" in lines[i]:
                new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", lines[i], count=1)
                if new != lines[i]:
                    lines[i] = new
                    logs.append({"file": rel, "fix": "cast-null-to-default"})

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
            print("   MANUAL out-param:", rel, line, q[-1])
    if ins:
        out = []
        for i, ln in enumerate(lines):
            indent = re.match(r"\s*", ln).group(0)
            for pname in ins.get(i, []):
                out.append(f"{indent}{pname} = default;")
                logs.append({"file": rel, "fix": "assign-out-param", "param": pname})
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
                    ln = ln[: m.start()] + ("(" + ", ".join(keep) + ")" if keep else "") + ln[m.end():]
                    n += 1
            res.append(ln)
        text = "\n".join(res)
        if n:
            logs.append({"file": rel, "fix": "strip-named-attr-args", "count": n})

    if any(c in ("CS1737", "CS1745") for _, c, _ in items):
        new, n = re.subn(r"\[Optional\](\s+[\w\.\<\>\[\]]+\s+\w+)(\s*[,)])",
                         lambda m: m.group(1) + " = default" + m.group(2), text)
        if n:
            text = new
            logs.append({"file": rel, "fix": "optional-default", "count": n})

    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write(text)


def main():
    txt = open(sys.argv[1], encoding="utf-8-sig", errors="replace").read()
    errs = ERR.findall(txt)
    print("errors in log:", len(errs))
    byfile = collections.defaultdict(list)
    for f, l, c, code, msg in errs:
        byfile[f].append((int(l), code, msg))
    for f, items in byfile.items():
        if os.path.exists(f):
            patch(f, items)
    old = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    old.extend(logs)
    json.dump(old, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    print("patched files:", len({x['file'] for x in logs}), "ops:", len(logs))


if __name__ == "__main__":
    main()
