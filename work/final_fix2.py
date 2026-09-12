#!/usr/bin/env python3
"""Last batch of targeted fixes."""
import os, re, json, glob

DEC = r"E:\manosaba_re\src\decompiled"
PATCHES = r"E:\manosaba_re\work\patches.json"
logs = []


def rd(p):
    return open(p, encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")


def wr(p, t):
    open(p, "w", encoding="utf-8-sig", newline="").write(t)


# 1. every [Optional] parameter needs a real default value
n = 0
for p in glob.glob(os.path.join(DEC, "**", "*.cs"), recursive=True):
    t = rd(p)
    if "[Optional]" not in t:
        continue
    new, k = re.subn(r"\[Optional\](\s+[^,()]+\s+@?\w+)(\s*[,)])",
                     lambda m: m.group(1) + " = default" + m.group(2), t)
    if k:
        wr(p, new)
        n += k
        logs.append({"file": os.path.relpath(p, DEC), "fix": "optional-default", "count": k})
print("optional defaults:", n)

# 2. return null;  ->  return default;   where the return type is a value type (CS0037)
for rel in [r"CustomInspector\CustomInspector\SerializableDictionary.cs"]:
    p = os.path.join(DEC, rel)
    t = rd(p)
    new, k = re.subn(r"return null;", "return default;", t)
    if k:
        wr(p, new)
        logs.append({"file": rel, "fix": "return-default", "count": k})
        print("return null -> default:", rel, k)

# 3. drop IAwaiter from *every* nested type that declares it (UniTask.cs has several)
def strip_all(rel, tname, ifaces):
    p = os.path.join(DEC, rel)
    t = rd(p)
    lines = t.split("\n")
    changed = 0
    i = 0
    while i < len(lines):
        ln = lines[i]
        if ln is not None and re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"\b", ln) and ":" in ln:
            head, j = ln, i
            while "{" not in head and j + 1 < len(lines):
                j += 1
                head += "\n" + lines[j]
            brace = head.find("{")
            decl, tail = (head[:brace], head[brace:]) if brace >= 0 else (head, "")
            m = re.search(r"\s:\s", decl)
            if m:
                base = decl[:m.start()]
                rest = decl[m.end():].replace("\n", " ").strip()
                where = ""
                wm = re.search(r"\bwhere\b", rest)
                if wm:
                    where = " " + rest[wm.start():].strip()
                    rest = rest[:wm.start()]
                parts, depth, cur = [], 0, ""
                for ch in rest:
                    if ch in "(<[":
                        depth += 1
                    elif ch in ")>]":
                        depth -= 1
                    if ch == "," and depth == 0:
                        parts.append(cur); cur = ""
                    else:
                        cur += ch
                parts.append(cur)
                keep = [x.strip() for x in parts
                        if x.strip() and re.sub(r"<.*", "", x.strip()).split(".")[-1] not in ifaces]
                if len(keep) != len([x for x in parts if x.strip()]):
                    lines[i] = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + where + "\n" + tail
                    for k in range(i + 1, j + 1):
                        lines[k] = None
                    changed += 1
        i += 1
    if changed:
        wr(p, "\n".join(x for x in lines if x is not None))
        logs.append({"file": rel, "type": tname, "dropped": ifaces, "count": changed})
    print("strip_all", rel, tname, changed)


strip_all(r"Elringus.Naninovel.Runtime\Naninovel\UniTask.cs", "Awaiter", ["IAwaiter"])
strip_all(r"Elringus.Naninovel.Runtime\Naninovel\UniTask.cs", "IsCanceledAwaiter", ["IAwaiter"])

json.dump(logs, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
print("done")
