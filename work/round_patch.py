#!/usr/bin/env python3
"""One-shot snapshot patcher: read a build log, patch the sources it complains about.

Usage: python round_patch.py <buildlog.txt>
"""
import os, re, sys, json, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}

BASE_OF = {
    (r"CustomInspector\CustomInspector\SerializableDictionary.cs", "SerializableDictionary"): "Dictionary<TKey, TValue>",
    (r"CustomInspector\CustomInspector\SerializableSet.cs", "SerializableSet"): "HashSet<T>",
    (r"CustomInspector\CustomInspector\ListContainer.cs", "ListContainer"): "List<T>",
}

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


def iface_of(expr):
    m = re.match(r"^([A-Za-z_][\w]*(?:\s*<.*>)?)\s*\.", expr.strip())
    return m.group(1).strip() if m else None


def simple(n):
    return re.sub(r"<.*", "", n).strip()


def patch(path, items):
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")

    # 1. interfaces that are not implemented -----------------------------------
    drop = collections.defaultdict(set)
    for line, code, msg in items:
        if code in IFACE_CODES:
            q = QUOTED.findall(msg)
            if len(q) >= 2:
                drop[q[0].split("<")[0].split(".")[-1]].add(iface_of(q[1]))

    for tname, ifaces in drop.items():
        if not ifaces:
            continue
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
                    if s in ifaces or s in seen:
                        continue
                    seen.add(s)
                    keep.append(e)
                newdecl = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + where + "\n"
            else:
                newdecl = decl.rstrip() + "\n"
            lines[i] = newdecl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "type": tname, "dropped": sorted(x for x in ifaces if x)})
            break
    text = "\n".join(x for x in lines if x is not None)
    lines = text.split("\n")

    # 2. (T)null -> default(T) -------------------------------------------------
    for line, code, msg in items:
        if code == "CS0037":
            i = line - 1
            if 0 <= i < len(lines) and "null" in lines[i]:
                new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", lines[i], count=1)
                if new != lines[i]:
                    lines[i] = new
                    logs.append({"file": rel, "fix": "cast-null-to-default"})

    # 3. out parameters -------------------------------------------------------
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

    # 4. attribute named arguments -------------------------------------------
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

    # 5. [Optional] parameter without a real default --------------------------
    if any(c in ("CS1737", "CS1745") for _, c, _ in items):
        new, n = re.subn(r"\[Optional\](\s+[\w\.\<\>\[\]]+\s+\w+)(\s*[,)])",
                         lambda m: m.group(1) + " = default" + m.group(2), text)
        if n:
            text = new
            logs.append({"file": rel, "fix": "optional-default", "count": n})

    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write(text)


def main():
    log = sys.argv[1]
    txt = open(log, encoding="utf-8-sig", errors="replace").read()
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
