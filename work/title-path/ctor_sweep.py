"""Find reconstructed-skeleton constructors that silently drop field initializers.

    python work/title-path/ctor_sweep.py [assembly-dir-substring ...]

A C# field initializer (`private readonly List<X> _xs = new List<X>();`, `private float _move = 24f;`) is compiled into
every instance constructor.  The Il2CppDumper skeleton keeps the constructor with an empty body and drops the
initializers, so such fields are null / 0 at runtime (Pagination._disposables -> NRE in Clear()).  A constructor whose
native body is a shared thunk (identical-code folded with other methods, e.g. 0x30C000 `ret`, 0x30D8F0 MonoBehaviour
..ctor) stores nothing; one with its own body that writes fields or allocates objects has initializers to restore.

For every empty-bodied constructor in Assets/Scripts/Game with an [Address(RVA)], disassemble it and report the field
stores (`mov [this+off], ...`) and allocations it performs, next to whether the source fields already carry an
initializer.  Output: work/title-path/ctor_sweep.txt
"""
import os
import pickle
import re
import subprocess
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
ROOT = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
PY = os.path.join(REPO, "tools", "venv", "Scripts", "python.exe")
DISAS = os.path.join(REPO, "work", "reversing", "disas.py")
OUT = os.path.join(REPO, "work", "title-path", "ctor_sweep.txt")

CTOR = re.compile(r'\[Address\(RVA = "(0x[0-9A-F]+)"[^\n]*\n((?:\s*\[[^\n]*\n)*)\s*(?:public|protected|private|internal)[^\n(]*?\b(\w+)\(\)\s*\n\s*\{\s*\n\s*\}')
NS = re.compile(r"^namespace ([\w.]+)", re.M)


def main():
    filters = sys.argv[1:]
    db = pickle.load(open(os.path.join(REPO, "work", "reversing", "symdb.pkl"), "rb"))
    by_rva = db["methods_by_rva"]
    rows = []
    for dirpath, _, files in os.walk(ROOT):
        if filters and not any(f in dirpath for f in filters):
            continue
        for name in files:
            if not name.endswith(".cs"):
                continue
            path = os.path.join(dirpath, name)
            text = open(path, encoding="utf-8-sig").read()
            ns = NS.search(text)
            for m in CTOR.finditer(text):
                rva, cls = int(m.group(1), 16), m.group(3)
                if not re.search(r"\bclass %s\b" % re.escape(cls), text):
                    continue
                names = by_rva.get(rva, [])
                names = names if isinstance(names, list) else [names]
                if len(names) != 1:
                    continue  # identical-code folded: a shared thunk, no per-class initializers
                full = "%s.%s" % (ns.group(1) if ns else "", cls)
                out = subprocess.run([PY, DISAS, hex(rva), "--type", full], capture_output=True, text=True,
                                     encoding="utf-8", errors="replace").stdout
                body = [l for l in out.splitlines() if l.startswith("  0x18")]
                stores = sorted(set(re.findall(r"mov\s+(?:dword|qword|byte|word) ptr \[r(?:cx|di|bx|si) \+ (0x[0-9a-f]+)\], ", "\n".join(body))))
                allocs = len([l for l in body if "object_new" in l])
                if not stores and not allocs:
                    continue
                rows.append((os.path.relpath(path, ROOT), cls, hex(rva), len(body), stores, allocs))
    with open(OUT, "w", encoding="utf-8", newline="\n") as fh:
        for r in rows:
            fh.write("%s  %s  %s  instr=%d  stores=%s  allocs=%d\n" % (r[0], r[1], r[2], r[3], ",".join(r[4]), r[5]))
    print("%d constructors with dropped initializers -> %s" % (len(rows), OUT))


if __name__ == "__main__":
    main()
