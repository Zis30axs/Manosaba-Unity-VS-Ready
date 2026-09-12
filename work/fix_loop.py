#!/usr/bin/env python3
"""Iteratively build the IDE solution and patch the decompiler artefacts that break it."""
import os, re, json, subprocess, collections, sys

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
LOG = os.path.join(ROOT, "work", "build_fix.log")
PATCHES = os.path.join(ROOT, "work", "patches.json")

ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
QUOTED = re.compile(r"[“\"]([^”\"]+)[”\"]")
IFACE_CODES = {"CS0535", "CS0737", "CS0738"}


def build():
    env = dict(os.environ, NUGET_PACKAGES=os.path.join(ROOT, "tools", "nuget"))
    p = subprocess.run(["dotnet", "build", "Manosaba.sln", "-v", "q", "--nologo"],
                       cwd=PROJ, capture_output=True, text=True, encoding="utf-8", errors="replace", env=env)
    return p.stdout + p.stderr


def iface_of(member_expr):
    m = re.match(r"^([A-Za-z_][\w]*(?:\s*<[^<>]*(?:<[^<>]*>)?[^<>]*>)?)\s*\.", member_expr.strip())
    if m:
        return m.group(1).strip()
    return None


def simple(name):
    return re.sub(r"<.*", "", name).strip()


def fix_file(path, items, patched):
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read()
    text = text.replace("\r\n", "\n")
    lines = text.split("\n")

    # group: type -> {interfaces to drop}
    per_type = collections.defaultdict(set)
    for line, code, msg in items:
        q = QUOTED.findall(msg)
        if code in IFACE_CODES and len(q) >= 2:
            tname = q[0].split("<")[0].split(".")[-1]
            iface = iface_of(q[1])
            if iface:
                per_type[tname].add(iface)

    for tname, ifaces in per_type.items():
        for i, ln in enumerate(lines):
            if ln is None:
                continue
            if not re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"\b", ln):
                continue
            j = i
            head = ln
            while "{" not in head and j + 1 < len(lines):
                j += 1
                head += "\n" + lines[j]
            brace = head.find("{")
            decl = head if brace < 0 else head[:brace]
            tail = "" if brace < 0 else head[brace:]
            m = re.search(r"\s:\s", decl)
            if m:
                base = decl[: m.start()]
                items_ = [x.strip() for x in decl[m.end():].replace("\n", " ").split(",")]
                items_ = [x for x in items_ if x]
                keep = [x for x in items_ if simple(x) not in ifaces]
                if keep:
                    decl = base.rstrip() + " : " + ", ".join(keep) + "\n"
                else:
                    decl = base.rstrip() + "\n"
            lines[i] = decl + tail
            for k in range(i + 1, j + 1):
                lines[k] = None
            patched.append({"file": path, "type": tname, "dropped": sorted(ifaces)})
            break

    # [Optional] without default -> add default  (illegal together, so drop attribute)
    if any(c == "CS1737" for _, c, _ in items):
        new, n = re.subn(r"\[Optional\](\s+[A-Za-z0-9_.<>\[\]]+\s+\w+)(\s*)\)", r"[Optional]\1 = default\2)", "\n".join(x for x in lines if x is not None))
        if n:
            lines = new.split("\n")
            patched.append({"file": path, "fix": "optional-default", "count": n})

    lines = [x for x in lines if x is not None]
    text = "\n".join(lines)
    if any(c == "CS1745" for _, c, _ in items):
        n = text.count("[Optional] ")
        text = text.replace("[Optional] ", "")
        patched.append({"file": path, "fix": "drop [Optional] (CS1745)", "count": n})

    if any(c == "CS0617" for _, c, _ in items):
        bad = set()
        for _, _, msg in items:
            bad.update(QUOTED.findall(msg))
        out, n = [], 0
        for ln in text.split("\n"):
            if ln.lstrip().startswith("[") and any(b in ln for b in bad):
                out.append(re.sub(r",\s*\w+\s*=\s*[^,)\]]+", "", ln))
                n += 1
            else:
                out.append(ln)
        text = "\n".join(out)
        if n:
            patched.append({"file": path, "fix": "strip named attribute args", "count": n})

    if any(c == "CS0177" for _, c, _ in items):
        nl = text.split("\n")
        n = 0
        for line, _, msg in items:
            q = QUOTED.findall(msg)
            if not q:
                continue
            pname = q[-1]
            for i in range(line - 1, min(line + 4, len(nl))):
                if nl[i].strip() == "{":
                    nl[i] += "\n\t\t" + pname + " = default;"
                    n += 1
                    break
        text = "\n".join(nl)
        if n:
            patched.append({"file": path, "fix": "assign out param", "count": n})

    open(path, "w", encoding="utf-8-sig", newline="").write(text)


def main():
    allpatches = json.load(open(PATCHES, encoding="utf-8")) if os.path.exists(PATCHES) else []
    logfh = open(LOG, "a", encoding="utf-8")
    for it in range(1, 9):
        out = build()
        errs = ERR.findall(out)
        codes = collections.Counter(e[3] for e in errs)
        print(f"--- iteration {it}: {len(errs)} errors {dict(codes)}", flush=True)
        logfh.write(f"=== iteration {it}: {len(errs)} errors {dict(codes)}\n")
        if not errs:
            print("BUILD CLEAN")
            break
        byfile = collections.defaultdict(list)
        for f, l, c, code, msg in errs:
            byfile[f].append((int(l), code, msg))
        patched = []
        for f, items in byfile.items():
            if os.path.exists(f):
                fix_file(f, items, patched)
        print(f"    patched files: {len({p['file'] for p in patched})} ops: {len(patched)}", flush=True)
        logfh.write(json.dumps(patched, ensure_ascii=False) + "\n")
        allpatches.extend(patched)
        if not patched:
            print("    nothing patchable left")
            break
    json.dump(allpatches, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    logfh.close()


if __name__ == "__main__":
    main()
