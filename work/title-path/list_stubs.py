"""List Il2CppDumper skeleton methods (RVA + signature + body kind) of the given reconstructed C# files.

A body is a STUB when it is empty or a single `return null|default(...)|false|0` statement, the shape the dumper emits.
    python work/title-path/list_stubs.py <file.cs>...
"""
import re, sys

for path in sys.argv[1:]:
    lines = open(path, encoding="utf-8").read().split("\n")
    name = path.replace(chr(92), "/").split("/")[-1]
    i = 0
    while i < len(lines):
        m = re.search(r'\[Address\(RVA = "(0x[0-9A-Fa-f]+)"', lines[i])
        if not m:
            i += 1
            continue
        rva = m.group(1)
        j = i + 1
        # signature = first line that is not an attribute
        while j < len(lines) and lines[j].strip().startswith("["):
            j += 1
        sig = lines[j].strip() if j < len(lines) else "?"
        # body between the first { and its matching }
        k = j
        depth = 0
        body = []
        started = False
        while k < len(lines):
            for ch in lines[k]:
                if ch == "{":
                    depth += 1
                    started = True
                elif ch == "}":
                    depth -= 1
            if started:
                body.append(lines[k].strip())
            if started and depth == 0:
                break
            k += 1
        inner = [b for b in body[1:-1] if b and b not in ("{", "}")]
        kind = "STUB" if (not inner or (len(inner) == 1 and re.match(r"return (null|default\(.*\)|false|true|0);", inner[0]))) else "REAL(%d)" % len(inner)
        print("%-34s %-9s %-10s %s" % (name[:34], rva, kind, sig[:110]))
        i = k + 1
