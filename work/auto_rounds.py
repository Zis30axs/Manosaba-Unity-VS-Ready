#!/usr/bin/env python3
"""Auto rounds: build -> patch -> build ... until the solution compiles.

Fixes applied (all recorded in work/patches.json):
  CS0535/CS0737/CS0738  drop every interface from the offending type declaration
                        (real base classes are detected through dump.cs)
  CS0037                (T)null -> default(T)
  CS0177                assign the out parameter before the failing statement
  CS0617                strip named attribute arguments
  CS1737/CS1745         give [Optional] parameters a real default value
  CS0553/CS0246         delete the offending (stub) member
"""
import os, re, json, subprocess, collections, sys

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
DUMP = os.path.join(ROOT, "src", "il2cpp", "dump.cs")
PROJ = os.path.join(ROOT, "project")
PATCHES = os.path.join(ROOT, "work", "patches.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*)", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}

BASE_OF = {
    (r"CustomInspector\CustomInspector\SerializableDictionary.cs", "SerializableDictionary"): "Dictionary<TKey, TValue>",
    (r"CustomInspector\CustomInspector\SerializableSet.cs", "SerializableSet"): "HashSet<T>",
    (r"CustomInspector\CustomInspector\ListContainer.cs", "ListContainer"): "List<T>",
}

ifaces_known = set()
for line in open(DUMP, encoding="utf-8", errors="replace"):
    m = re.search(r"\binterface\s+([A-Za-z_][\w]*)", line)
    if m:
        ifaces_known.add(m.group(1))


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


def simple(n):
    return re.sub(r"<.*", "", n).strip().split(".")[-1]


def delete_member(lines, idx):
    """delete the member whose declaration contains lines[idx]"""
    start = idx
    while start > 0 and not re.search(r"[;{]\s*$", lines[start - 1]):
        start -= 1
    depth = 0
    end = idx
    for k in range(start, len(lines)):
        depth += lines[k].count("{") - lines[k].count("}")
        if depth <= 0 and "{" in "".join(lines[start:k + 1]):
            end = k
            break
        if ";" in lines[k] and depth == 0:
            end = k
            break
    else:
        end = idx
    return start, end


CS_KEYWORDS = {"var", "out", "ref", "in", "is", "as", "new", "base", "this", "string",
               "object", "int", "bool", "event", "params", "default", "class", "struct"}


def escape_param(p):
    return "@" + p if p in CS_KEYWORDS else p


def find_body_brace(lines, idx, span=6):
    for k in range(idx, min(len(lines), idx + span)):
        if "{" in lines[k]:
            return k
    return None


def patch(path, items, logs):
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")

    need = {QUOTED.findall(m)[0].split(".")[-1].split("<")[0]
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
                    if s in seen or s in ifaces_known:
                        continue
                    seen.add(s)
                    keep.append(e)
                newdecl = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + where + "\n"
            else:
                newdecl = decl.rstrip() + "\n"
            lines[i] = newdecl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "type": tname, "kept": keep if m else []})
            break
    lines = [x for x in lines if x is not None]

    for line, code, msg in items:
        if code == "CS0037":
            i = line - 1
            if 0 <= i < len(lines) and "null" in lines[i]:
                new = re.sub(r"\(([\w\.\<\>\[\]\?]+)\)\s*null", r"default(\1)", lines[i], count=1)
                if new != lines[i]:
                    lines[i] = new
                    logs.append({"file": rel, "fix": "cast-null-to-default"})

    lines = [x for x in lines if x is not None]
    ins = collections.defaultdict(list)
    for line, code, msg in items:
        if code != "CS0177":
            continue
        q = QUOTED.findall(msg)
        if not q:
            continue
        i = line - 1
        if not (0 <= i < len(lines)):
            continue
        # locate the enclosing method signature, then its opening brace
        sig = None
        for k in range(i, max(-1, i - 10), -1):
            if "(" in lines[k] and re.search(r"[\)\s;]*$", lines[k]) and lines[k].rstrip().endswith(")"):
                sig = k
                break
        at = find_body_brace(lines, sig if sig is not None else i)
        if at is None:
            at = i
        ins[at].append(escape_param(q[-1].strip()))
    if ins:
        out = []
        for i, ln in enumerate(lines):
            if ln is None:
                continue
            pnames = ins.get(i, [])
            if pnames:
                indent = re.match(r"\s*", ln).group(0)
                if ln.strip().startswith("{"):
                    out.append(ln)
                    for pname in pnames:
                        out.append(f"{indent}\t{pname} = default;")
                        logs.append({"file": rel, "fix": "assign-out-param", "param": pname})
                else:
                    for pname in pnames:
                        out.append(f"{indent}{pname} = default;")
                        logs.append({"file": rel, "fix": "assign-out-param", "param": pname})
                    out.append(ln)
            else:
                out.append(ln)
        lines = out

    # delete members that cannot work against the reference assemblies
    kill = set()
    if False:  # member deletion proved too destructive; handled by targeted fixes instead
        for line, code, msg in items:
            if code in ("CS0553", "CS0246"):
                s, e = delete_member(lines, line - 1)
                kill.update(range(s, e + 1))
                logs.append({"file": rel, "fix": "delete-member", "code": code, "lines": [s + 1, e + 1]})
    if kill:
        lines = [l for i, l in enumerate(lines) if i not in kill]

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
        new, n = re.subn(r"\[Optional\]\s*(\s*(?:\[[^\]]*\]\s*)*)([^,()]+\s+@?\w+)(\s*[,)])",
                         lambda m: (m.group(1).strip() + " " if m.group(1).strip() else "") + m.group(2) + " = default" + m.group(3),
                         text)
        if n:
            text = new
            logs.append({"file": rel, "fix": "optional-default (drop [Optional])", "count": n})

    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write(text)


def patch_ctor_defaults(path, items, logs):
    """CS7036: derived constructor implicitly calling a base constructor with required params."""
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")
    for line, code, msg in items:
        if code != "CS7036":
            continue
        m = QUOTED.search(msg)
        if not m:
            continue
        full = m.group(1)
        if "(" not in full:
            continue
        base = full[: full.find("(")].split(".")[-1]
        ntypes = len([x for x in split_top(full[full.find("(") + 1: full.rfind(")")]) if x.strip()])
        done = False
        for i, ln in enumerate(lines):
            if ln is None or "(" not in ln:
                continue
            if not re.search(r"\b(public|protected|internal|private)\s+" + re.escape(base) + r"\s*\(", ln):
                continue
            head, j = ln, i
            while ")" not in head and j + 1 < len(lines):
                j += 1
                head += " " + lines[j].strip()
            om = re.search(r"\((.*)\)", head)
            if not om:
                continue
            args = split_top(om.group(1))
            if len(args) != ntypes:
                continue
            new_args = [a if ("=" in a.split("(")[0] or "[Optional]" in a) else a.rstrip() + " = default"
                        for a in args]
            if new_args == args:
                continue
            lines[i] = head[: om.start()] + "(" + ", ".join(new_args) + ")" + head[om.end():]
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "fix": "ctor-param-default", "ctor": base})
            done = True
            break
        if not done:
            print("   MANUAL ctor:", rel, full[:70], flush=True)
    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write("\n".join(x for x in lines if x is not None))


def patch_ctor_defaults(path, items, logs):
    """CS7036: derived constructor implicitly calling a base constructor with required params."""
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")
    for line, code, msg in items:
        if code != "CS7036":
            continue
        m = QUOTED.search(msg)
        if not m:
            continue
        full = m.group(1)
        if "(" not in full:
            continue
        base = full[: full.find("(")].split(".")[-1]
        ntypes = len([x for x in split_top(full[full.find("(") + 1: full.rfind(")")]) if x.strip()])
        done = False
        for i, ln in enumerate(lines):
            if ln is None or "(" not in ln:
                continue
            if not re.search(r"\b(public|protected|internal|private)\s+" + re.escape(base) + r"\s*\(", ln):
                continue
            head, j = ln, i
            while ")" not in head and j + 1 < len(lines):
                j += 1
                head += " " + lines[j].strip()
            om = re.search(r"\((.*)\)", head)
            if not om:
                continue
            args = split_top(om.group(1))
            if len(args) != ntypes:
                continue
            new_args = [a if ("=" in a.split("(")[0] or "[Optional]" in a) else a.rstrip() + " = default"
                        for a in args]
            if new_args == args:
                continue
            lines[i] = head[: om.start()] + "(" + ", ".join(new_args) + ")" + head[om.end():]
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "fix": "ctor-param-default", "ctor": base})
            done = True
            break
        if not done:
            print("   MANUAL ctor:", rel, full[:70], flush=True)
    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write("\n".join(x for x in lines if x is not None))


def patch_ctor_defaults(path, items, logs):
    """CS7036: derived constructor implicitly calling a base constructor with required params."""
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")
    orig = text
    lines = text.split("\n")
    for line, code, msg in items:
        if code != "CS7036":
            continue
        m = QUOTED.search(msg)
        if not m:
            continue
        full = m.group(1)
        if "(" not in full:
            continue
        base = full[: full.find("(")].split(".")[-1]
        ntypes = len([x for x in split_top(full[full.find("(") + 1: full.rfind(")")]) if x.strip()])
        done = False
        for i, ln in enumerate(lines):
            if ln is None or "(" not in ln:
                continue
            if not re.search(r"\b(public|protected|internal|private)\s+" + re.escape(base) + r"\s*\(", ln):
                continue
            head, j = ln, i
            while ")" not in head and j + 1 < len(lines):
                j += 1
                head += " " + lines[j].strip()
            om = re.search(r"\((.*)\)", head)
            if not om:
                continue
            args = split_top(om.group(1))
            if len(args) != ntypes:
                continue
            new_args = [a if ("=" in a.split("(")[0] or "[Optional]" in a) else a.rstrip() + " = default"
                        for a in args]
            if new_args == args:
                continue
            lines[i] = head[: om.start()] + "(" + ", ".join(new_args) + ")" + head[om.end():]
            for k in range(i + 1, j + 1):
                lines[k] = None
            logs.append({"file": rel, "fix": "ctor-param-default", "ctor": base})
            done = True
            break
        if not done:
            print("   MANUAL ctor:", rel, full[:70], flush=True)
    if text != orig:
        open(path, "w", encoding="utf-8-sig", newline="").write("\n".join(x for x in lines if x is not None))


def main():
    allp = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    for it in range(1, 13):
        out = build()
        errs = ERR.findall(out)
        codes = collections.Counter(e[3] for e in errs)
        print(f"--- round {it}: {len(errs)} errors {dict(codes)}", flush=True)
        if not errs:
            print("*** BUILD CLEAN ***")
            break
        byfile = collections.defaultdict(list)
        for f, l, c, code, msg in errs:
            byfile[f].append((int(l), code, msg.split(' [E:')[0]))
        logs = []
        for f, items in byfile.items():
            if os.path.exists(f):
                patch(f, items, logs)
                patch_ctor_defaults(f, items, logs)
                patch_ctor_defaults(f, items, logs)
                patch_ctor_defaults(f, items, logs)
        print("    ops:", len(logs), flush=True)
        allp.extend(logs)
        if not logs:
            print("    no progress")
            break
    json.dump(allp, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
