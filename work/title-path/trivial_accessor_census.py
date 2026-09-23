"""Find stub property getters whose native body is a plain backing-field read, and (optionally) reconstruct them.

    tools/venv/Scripts/python.exe work/title-path/trivial_accessor_census.py            # report
    tools/venv/Scripts/python.exe work/title-path/trivial_accessor_census.py --apply    # rewrite the bodies

stub_census.py deliberately calls a stub whose native function is under 24 bytes "trivial" - but a trivial native body
is not a trivial stub: `LocalizableText.ChangeFont` is `movzx eax, byte ptr [rcx+0x20]; ret` (return _changeFont), and
the Il2CppDumper skeleton's `return default(bool);` turns every font change off.  This finds every `get` accessor
that (a) has an [Address(RVA)], (b) still has a stub body (empty / `return default(...)|null|false|0;`) and no
RECONSTRUCTED comment above it, and (c) whose native code is exactly one load from [rcx + off] followed by `ret`:
    mov rax, [rcx+off] / mov eax, [rcx+off] / movzx eax, byte|word ptr [rcx+off] / movsx ... / movss xmm0, [rcx+off]
    / movsd xmm0, [rcx+off]
and resolves `off` to the field of the SAME class declared with [FieldOffset(Offset = "off")] (identical-code folding
means the RVA is shared across unrelated classes, so the offset is always resolved against the accessor's own class;
an offset that is not one of the class's own fields - a base-class field, a struct-returning getter - is reported and
left alone).  The rewritten getter is `return <field>;` with a comment naming the instruction.
"""
import io
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, "..", ".."))
sys.path.insert(0, os.path.join(REPO, "work", "reversing"))
from il2cpp_pe import PE, make_cs  # noqa: E402

ROOT = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
STUB = re.compile(r"^(return (default\([^)]*\)|null|false|true|0|0f|0L|0u|0\.0f?|\"\"|default);)?$")
LOAD = re.compile(r"^(mov|movzx|movsx|movsxd|movss|movsd)\s+(rax|eax|ax|al|xmm0),\s*(byte|word|dword|qword)?\s*(ptr\s*)?\[rcx \+ (0x[0-9a-f]+)\]$")
FIELD = re.compile(r'\[(?:Il2CppDummyDll\.)?FieldOffset\(Offset = "(0x[0-9A-F]+)"\)\]\s*\n(?:[ \t]*\[[^\n]*\n)*[ \t]*(?:(?:public|private|protected|internal|readonly|static|new)\s+)*([\w<>\[\],. ?]+?)\s+(\w+)\s*(?:=|;)')
CLASS = re.compile(r"^(\s*)(?:(?:public|private|protected|internal|sealed|abstract|static|partial)\s+)*(?:class|struct)\s+(\w+)", re.M)


def classes_in(text):
    """(name, start, end) for each class/struct body, by brace matching from its declaration."""
    out = []
    for m in CLASS.finditer(text):
        brace = text.find("{", m.end())
        if brace < 0:
            continue
        depth = 0
        for i in range(brace, len(text)):
            if text[i] == "{":
                depth += 1
            elif text[i] == "}":
                depth -= 1
                if depth == 0:
                    out.append((m.group(2), brace, i))
                    break
    return out


def own_fields(text, start, end, nested):
    """offset -> (type, name) of fields declared directly in [start, end), skipping nested type bodies."""
    fields = {}
    for m in FIELD.finditer(text, start, end):
        if any(s < m.start() < e for (_n, s, e) in nested):
            continue
        fields[int(m.group(1), 16)] = (m.group(2).strip(), m.group(3))
    return fields


def main():
    apply = "--apply" in sys.argv
    pe, cs = PE(), make_cs()
    report, changed_files, total = [], 0, 0
    for dirpath, _dirs, files in os.walk(ROOT):
        for f in files:
            if not f.endswith(".cs"):
                continue
            path = os.path.join(dirpath, f)
            raw = open(path, "rb").read()
            bom = raw.startswith(b"\xef\xbb\xbf")
            text = raw.decode("utf-8-sig")
            crlf = "\r\n" in text
            if crlf:
                text = text.replace("\r\n", "\n")
            classes = classes_in(text)
            edits = []
            for m in re.finditer(r'\[Address\(RVA = "(0x[0-9A-F]+)"[^\n]*\n((?:[ \t]*\[[^\n]*\n)*)([ \t]*)get\n\3\{\n(.*?)\n\3\}', text, re.S):
                body = m.group(4).strip()
                if not STUB.match(body):
                    continue
                before = text[max(0, m.start() - 400):m.start()]
                if "RECONSTRUCTED" in before.split("get\n")[-1] or before.rstrip().endswith("*/"):
                    pass
                # the RECONSTRUCTED marker sits in the comment lines right above the attributes
                prev_lines = text[:m.start()].rstrip("\n").split("\n")[-3:]
                if any("RECONSTRUCTED" in line for line in prev_lines):
                    continue
                rva = int(m.group(1), 16)
                try:
                    code = pe.read(rva, 16)
                except Exception:
                    continue
                ins = list(cs.disasm(code, 0x180000000 + rva))
                if len(ins) < 2 or ins[1].mnemonic != "ret":
                    continue
                lm = LOAD.match((ins[0].mnemonic + " " + ins[0].op_str).strip())
                if not lm:
                    continue
                off = int(lm.group(5), 16)
                owner = [c for c in classes if c[1] < m.start() < c[2]]
                if not owner:
                    continue
                name, start, end = min(owner, key=lambda c: c[2] - c[1])
                nested = [c for c in classes if start < c[1] and c[2] < end]
                fields = own_fields(text, start, end, nested)
                total += 1
                instr = "%s %s" % (ins[0].mnemonic, ins[0].op_str)
                if off not in fields:
                    report.append("SKIP  %-60s %s get @0x%X [%s]: +0x%X is not a field of %s" % (
                        os.path.relpath(path, ROOT)[:60], name, rva, instr, off, name))
                    continue
                ftype, fname = fields[off]
                report.append("FIX   %-60s %s get @0x%X [%s] -> return %s;" % (
                    os.path.relpath(path, ROOT)[:60], name, rva, instr, fname))
                indent = m.group(3)
                comment = ("%s// RECONSTRUCTED | Confidence: A | RVA: 0x%X, %d B -- `%s; ret`: the backing field %s "
                           "(+0x%X in dump.cs)\n" % (indent, rva, ins[0].size + 1, instr, fname, off))
                edits.append((m, indent, fname, comment))
            if apply and edits:
                for m, indent, fname, comment in sorted(edits, key=lambda e: -e[0].start()):
                    # the comment goes on its own line above the [Address] line (whose indentation is kept); the
                    # stub body is replaced
                    line_start = text.rfind("\n", 0, m.start()) + 1
                    new_block = (comment + text[line_start:m.start(4)] + indent + "\treturn " + fname + ";" +
                                 text[m.end(4):m.end()])
                    text = text[:line_start] + new_block + text[m.end():]
                out = text.replace("\n", "\r\n") if crlf else text
                open(path, "wb").write((b"\xef\xbb\xbf" if bom else b"") + out.encode("utf-8"))
                changed_files += 1
    for line in report:
        print(line)
    print("%d field-read getter stubs; %d fixable; %s" % (
        total, sum(1 for r in report if r.startswith("FIX")),
        ("rewrote %d files" % changed_files) if apply else "report only"))


if __name__ == "__main__":
    main()
