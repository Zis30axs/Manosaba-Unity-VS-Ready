"""List the functions that contain a direct `call rel32` / `jmp rel32` (tail call) to a given RVA (GameAssembly.dll, from the pdata table).

    tools/venv/Scripts/python.exe work/reversing/xref_calls.py 0x2067110 [0x...]

A raw byte scan (E8 / E9 + rel32 whose target is the RVA) over every pdata function, so it can report a false positive
where an E8 byte happens to sit inside another instruction; confirm each hit with disas.py.  Indirect calls
(through a method pointer / vtable) are not found.  Each hit is named from the il2cpp dump when an RVA line matches.
"""
import bisect
import re
import struct
import sys

sys.path.insert(0, __file__.rsplit("\\", 1)[0].rsplit("/", 1)[0])
from il2cpp_pe import PE  # noqa: E402

DUMP = r"E:\Manosaba-VSCodeReady\tools\unity-rebuild\converted\src\il2cpp\dump.cs"


def names_by_rva():
    names, klass = {}, None
    rva = None
    with open(DUMP, encoding="utf-8", errors="replace") as fh:
        for line in fh:
            m = re.match(r"^(?:public |internal |private |protected )?(?:static |sealed |abstract )*(?:class|struct) (\S+)", line)
            if m:
                klass = m.group(1)
            m = re.search(r"// RVA: (0x[0-9A-F]+)", line)
            if m:
                rva = int(m.group(1), 16)
                continue
            if rva is not None:
                m = re.search(r"([\w.<>|`,\[\] ]+)\(", line.strip())
                if m and rva not in names:
                    names[rva] = "%s :: %s" % (klass, line.strip()[:120])
                rva = None
    return names


def main():
    targets = [int(a, 16) for a in sys.argv[1:]]
    pe = PE()
    funcs = sorted(pe.pdata_functions())
    names = names_by_rva()
    for target in targets:
        print("callers of 0x%X (%s)" % (target, names.get(target, "?")))
        for begin, end in funcs:
            try:
                code = pe.read(begin, end - begin)
            except Exception:
                continue
            for opcode, kind in ((b"\xE8", "call"), (b"\xE9", "jmp ")):
                i = code.find(opcode)
                while i >= 0 and i + 5 <= len(code):
                    rel = struct.unpack_from("<i", code, i + 1)[0]
                    if begin + i + 5 + rel == target:
                        print("  0x%X %s in 0x%X  %s" % (begin + i, kind, begin, names.get(begin, "?")))
                    i = code.find(opcode, i + 1)


if __name__ == "__main__":
    main()
