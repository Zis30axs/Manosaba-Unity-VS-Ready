#!/usr/bin/env python3
"""Targeted fixes for the last handful of compiler errors."""
import os, re, json

DEC = r"E:\manosaba_re\src\decompiled"
DUMP = r"E:\manosaba_re\src\il2cpp\dump.cs"
PATCHES = r"E:\manosaba_re\work\patches.json"
logs = []


def rd(rel):
    return open(os.path.join(DEC, rel), encoding="utf-8-sig", errors="replace", newline="").read().replace("\r\n", "\n")


def wr(rel, text):
    open(os.path.join(DEC, rel), "w", encoding="utf-8-sig", newline="").write(text)


def sub(rel, pat, repl, count=1, flags=0, note=""):
    t = rd(rel)
    new, n = re.subn(pat, repl, t, count=count, flags=flags)
    if n:
        wr(rel, new)
        logs.append({"file": rel, "fix": note or pat[:40], "count": n})
    else:
        print("   MISS:", rel, note or pat[:60])
    return n


# ---- 1. drop interfaces incl. CS0736 types --------------------------------------
ifaces_known = set()
for line in open(DUMP, encoding="utf-8", errors="replace"):
    m = re.search(r"\binterface\s+([A-Za-z_][\w]*)", line)
    if m:
        ifaces_known.add(m.group(1))

targets = {
    r"Elringus.Naninovel.Runtime\Naninovel\ArrayEqualityComparer.cs": [("ArrayEqualityComparer", ["IEqualityComparer"])],
    r"Elringus.Naninovel.Runtime\Naninovel\SerializableMap.cs": [("SerializableMap", ["IReadOnlyDictionary", "IReadOnlyCollection", "IDictionary", "ICollection", "IEnumerable"])],
    r"Elringus.Naninovel.Runtime\Naninovel.Async.Internal\LazyPromise.cs": [("LazyPromise", ["IAwaiter"])],
    r"Elringus.Naninovel.Runtime\Naninovel.Async.Internal\ReusablePromise.cs": [("ReusablePromise", ["IAwaiter"])],
    r"Elringus.Naninovel.Runtime\Naninovel\UniTask.cs": [("_003CRunTask_003Ed__6", ["IAsyncStateMachine"]),
                                                          ("IsCanceledAwaiter", ["IAwaiter"]),
                                                          ("Awaiter", ["IAwaiter"])],
    r"Elringus.Naninovel.Runtime\Naninovel\UniTaskCompletionSource.cs": [("UniTaskCompletionSource", ["IAwaiter", "ICriticalNotifyCompletion", "INotifyCompletion", "IValueTaskSource"])],
}


def strip_ifaces(rel, tname, ifaces):
    t = rd(rel)
    out = []
    changed = False
    lines = t.split("\n")
    for i, ln in enumerate(lines):
        if re.search(r"\b(class|struct)\s+" + re.escape(tname) + r"\b", ln) and ":" in ln:
            head = ln
            j = i
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
                keep = [p.strip() for p in parts if p.strip() and re.sub(r"<.*", "", p.strip()).split(".")[-1] not in ifaces]
                newdecl = base.rstrip() + (" : " + ", ".join(keep) if keep else "") + where + "\n"
                lines[i] = newdecl + tail
                for k in range(i + 1, j + 1):
                    lines[k] = None
                changed = True
                logs.append({"file": rel, "type": tname, "dropped": ifaces})
                break
    if changed:
        wr(rel, "\n".join(x for x in lines if x is not None))


for rel, tlist in targets.items():
    if not os.path.exists(os.path.join(DEC, rel)):
        print("   missing file", rel)
        continue
    for tname, ifs in tlist:
        strip_ifaces(rel, tname, ifs)

# ---- 2. [Optional] parameters, allow @names -------------------------------------
for rel in [r"Elringus.Naninovel.Runtime\Naninovel\InitializeAtRuntimeAttribute.cs"]:
    sub(rel, r"\[Optional\](\s+[\w\.\<\>\[\]]+\s+@?\w+)(\s*[,)])",
        lambda m: m.group(1) + " = default" + m.group(2), note="optional-default")

# ---- 3. trailing required parameters after defaulted ones (CS1737) --------------
def fix_param_order(rel):
    t = rd(rel)
    lines = t.split("\n")
    changed = False
    for i, ln in enumerate(lines):
        if "(" not in ln or not re.search(r"(public|private|internal|protected)\s", ln):
            continue
        head = ln
        j = i
        while ")" not in head and j + 1 < len(lines):
            j += 1
            head += " " + lines[j].strip()
        om = re.search(r"\((.*)\)", head)
        if not om:
            continue
        args, depth, cur = [], 0, ""
        for ch in om.group(1):
            if ch in "(<[":
                depth += 1
            elif ch in ")>]":
                depth -= 1
            if ch == "," and depth == 0:
                args.append(cur); cur = ""
            else:
                cur += ch
        args.append(cur)
        seen_default = False
        out_args = []
        for a in args:
            has_def = "=" in a.split("(")[0] or "[Optional]" in a
            if has_def:
                seen_default = True
            elif seen_default and a.strip():
                a = a.rstrip() + " = default"
            out_args.append(a)
        newhead = head[: om.start()] + "(" + ",".join(out_args) + ")" + head[om.end():]
        if newline := (newhead != head):
            lines[i] = newhead
            for k in range(i + 1, j + 1):
                lines[k] = None
            changed = True
            logs.append({"file": rel, "fix": "parameter-defaults"})
    if changed:
        wr(rel, "\n".join(x for x in lines if x is not None))


for rel in [r"Elringus.Naninovel.Runtime\Naninovel\ChoiceState.cs"]:
    fix_param_order(rel)

# ---- 4. conversion operators to/from the injected base classes -------------------
for rel, pats in {
    r"CustomInspector\CustomInspector\ListContainer.cs": [
        r"\n\s*\[Token\([^\)]*\)\]\n\s*public static implicit operator ListContainer<T>\(List<T> list\)\n\s*\{\n[^}]*\}\n",
        r"\n\s*\[Token\([^\)]*\)\]\n\s*public static implicit operator List<T>\(ListContainer<T> container\)\n\s*\{\n[^}]*\}\n"],
    r"CustomInspector\CustomInspector\SerializableDictionary.cs": [
        r"\n\s*\[Token\([^\)]*\)\]\n\s*public static explicit operator Dictionary<TKey, TValue>\(SerializableDictionary<TKey, TValue> d\)\n\s*\{\n[^}]*\}\n"],
}.items():
    for p in pats:
        sub(rel, p, "\n", note="remove conversion operator")

# ---- 5. mangled iterator state-machine attribute --------------------------------
for rel in [r"CustomInspector\CustomInspector\SerializableDictionary.cs"]:
    sub(rel, r"\n\s*\[IteratorStateMachine\(typeof\([^\)]*_003CSystem_002D[^\)]*\)\)\]", "\n",
        note="drop mangled IteratorStateMachine attribute", flags=re.M)

json.dump(logs, open(PATCHES, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
print("applied", len(logs), "targeted fixes")
