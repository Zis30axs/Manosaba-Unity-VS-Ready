import ast

p = r"E:\manosaba_re\work\auto_rounds.py"
t = open(p, encoding="utf-8", newline="").read().replace("\r\n", "\n")

old = '    if text != orig:\n        open(path, "w", encoding="utf-8-sig", newline="").write(text)\n'
assert old in t, "anchor missing"

new = old + '''

def patch_ctor_defaults(path, items, logs):
    """CS7036: derived constructor implicitly calling a base constructor with required params."""
    rel = os.path.relpath(path, DEC)
    text = open(path, encoding="utf-8-sig", errors="replace", newline="").read().replace("\\r\\n", "\\n")
    orig = text
    lines = text.split("\\n")
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
            if not re.search(r"\\b(public|protected|internal|private)\\s+" + re.escape(base) + r"\\s*\\(", ln):
                continue
            head, j = ln, i
            while ")" not in head and j + 1 < len(lines):
                j += 1
                head += " " + lines[j].strip()
            om = re.search(r"\\((.*)\\)", head)
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
        open(path, "w", encoding="utf-8-sig", newline="").write("\\n".join(x for x in lines if x is not None))
'''
t = t.replace(old, new, 1)

old2 = """        for f, items in byfile.items():
            if os.path.exists(f):
                patch(f, items, logs)"""
new2 = """        for f, items in byfile.items():
            if os.path.exists(f):
                patch(f, items, logs)
                patch_ctor_defaults(f, items, logs)"""
assert old2 in t, "main anchor missing"
t = t.replace(old2, new2, 1)

open(p, "w", encoding="utf-8", newline="").write(t)
ast.parse(t)
print("auto_rounds.py updated, syntax ok")
