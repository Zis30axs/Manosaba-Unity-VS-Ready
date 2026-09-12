#!/usr/bin/env python3
"""Patch the few decompiler artefacts that stop the IDE project from compiling.

Only fixes reported by the compiler; every change is logged to work/patches.json.

  CS0535/CS0737/CS0738  type declares an interface the stub does not implement
                        -> drop the interface list from that type declaration
  CS1737                [Optional] parameter without a default value
                        -> add "= default"
  CS0617                named attribute argument that is not settable in the dummy
                        -> keep only the positional attribute arguments
  CS0177                "out" parameter not assigned in a stub body
                        -> assign default at the top of the body
"""
import os, re, json, collections, sys

ROOT = r"E:\manosaba_re"
BUILD_LOG = sys.argv[1] if len(sys.argv) > 1 else os.path.join(ROOT, "work", "build2.txt")
REPORT = os.path.join(ROOT, "work", "patches.json")

raw = open(BUILD_LOG, "rb").read()
txt = raw.decode("utf-16" if raw[:2] in (b"\xff\xfe", b"\xfe\xff") else "utf-8", "replace")

ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)
errors = ERR.findall(txt)
print("errors parsed:", len(errors))

byfile = collections.defaultdict(list)
for f, line, col, code, msg in errors:
    byfile[f].append((int(line), code, msg))

IFACE_CODES = {"CS0535", "CS0737", "CS0738"}
patches = []


def type_names(msg):
    return re.findall(r"[“\"]([^”\"]+)[”\"]", msg)


for path, errs in byfile.items():
    if not os.path.exists(path):
        print("missing", path)
        continue
    text = open(path, encoding="utf-8-sig", errors="replace").read()
    lines = text.split("\n")
    changed = False

    # --- interface list removal ------------------------------------------------
    names = []
    for line, code, msg in errs:
        if code in IFACE_CODES:
            for n in type_names(msg):
                if n not in names:
                    names.append(n)
    for name in names:
        simple = name.split("<")[0].split(".")[-1]
        for i, ln in enumerate(lines):
            if ln is None:
                continue
            if re.search(r"\b(class|struct)\s+" + re.escape(simple) + r"\b", ln):
                # join until '{'
                j = i
                joined = ln
                while "{" not in joined and j + 1 < len(lines):
                    j += 1
                    joined += "\n" + lines[j]
                brace = joined.find("{")
                head = joined if brace < 0 else joined[:brace]
                m = re.search(r"\s:\s", head)
                if not m:
                    continue
                newhead = head[: m.start()].rstrip() + ("\n" if brace < 0 else " ")
                rest = "" if brace < 0 else joined[brace:]
                lines[i] = newhead + rest
                for k in range(i + 1, j + 1):
                    lines[k] = None
                patches.append({"file": path, "type": name, "fix": "drop-interface-list"})
                changed = True
                break
    if changed:
        lines = [l for l in lines if l is not None]
        text = "\n".join(lines)

    # --- [Optional] parameter without default ---------------------------------
    if any(c == "CS1737" for _, c, _ in errs):
        new, n = re.subn(r"\[Optional\](\s+[A-Za-z0-9_.<>\[\]]+\s+\w+)(\s*)\)", r"[Optional]\1 = default\2)", text)
        if n:
            text = new
            patches.append({"file": path, "fix": "optional-default", "count": n})

    # --- invalid named attribute argument --------------------------------------
    if any(c == "CS0617" for _, c, _ in errs):
        bad = set()
        for _, _, msg in errs:
            bad.update(type_names(msg))
        newlines = []
        n = 0
        for ln in text.split("\n"):
            if ln.lstrip().startswith("[") and any(b in ln for b in bad):
                newlines.append(re.sub(r",\s*\w+\s*=\s*[^,)\]]+", "", ln))
                n += 1
            else:
                newlines.append(ln)
        if n:
            text = "\n".join(newlines)
            patches.append({"file": path, "fix": "strip-named-attribute-args", "count": n, "names": sorted(bad)})

    # --- out parameter never assigned -----------------------------------------
    if any(c == "CS0177" for _, c, _ in errs):
        nl = text.split("\n")
        n = 0
        for line, _, msg in errs:
            m = re.search(r"[“\"]([^”\"]+)[”\"]", msg)
            if not m:
                continue
            pname = m.group(1)
            for i in range(line - 1, min(line + 6, len(nl))):
                if nl[i].strip() == "{":
                    nl[i] = nl[i] + "\n                " + pname + " = default;"
                    n += 1
                    break
        if n:
            text = "\n".join(nl)
            patches.append({"file": path, "fix": "assign-out-param", "count": n})

    open(path, "w", encoding="utf-8-sig").write(text)

json.dump(patches, open(REPORT, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
print("patched files:", len({p['file'] for p in patches}), "operations:", len(patches))
for p in patches:
    print("  ", os.path.relpath(p["file"], ROOT), p["fix"], p.get("type", p.get("count", "")))
