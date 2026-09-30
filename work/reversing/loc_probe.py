"""Ad-hoc probe (read-only): resolve the LocalizationPresenter.Awake lambda
METHODINFO slots to their folded native bodies, and dump the shared trivial
bodies 0x30C000 / 0x30D8F0 / LocaleData getters.

Prints to stdout only.
"""
import sys, os, re
HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE, make_cs
from il2cpp_db import build

IMAGEBASE = 0x180000000
db = build()
pe = PE()
md = make_cs()


def resolve(rva, n=0x200):
    """Return [(insn_va, datarva, kind, text, target_rva)] for the lea rip slots."""
    code = pe.read(rva, n).rstrip(b'\xcc')
    out = []
    for ins in md.disasm(code, IMAGEBASE + rva):
        m = re.search(r'\[rip \+ (0x[0-9a-f]+)\]|\[rip - (0x[0-9a-f]+)\]', ins.op_str)
        if not m:
            continue
        delta = int(m.group(1), 16) if m.group(1) else -int(m.group(2), 16)
        drva = (ins.address + ins.size + delta) - IMAGEBASE
        info = db.describe_data(drva)
        if info and info[0] == 'metamethod':
            out.append((ins.address - IMAGEBASE, drva, info[1], db.metamethod_target(drva)))
    return out


def dis(rva, n=24):
    code = pe.read(rva, 0x60).rstrip(b'\xcc')
    print("--- 0x%X (%d bytes)  managed=%s" % (rva, len(code), db.method_names(rva)))
    for ins in md.disasm(code, IMAGEBASE + rva):
        print("    0x%X  %-8s %s" % (ins.address, ins.mnemonic, ins.op_str))
        n -= 1
        if n <= 0:
            break
    print()


print("=== LocalizationPresenter.Awake (0xCD34B0) lambda METHODINFO slots ===")
for site, drva, text, tgt in resolve(0xCD34B0, 0x120):
    if 'b__3_' in text or 'Register' in text or 'Unregister' in text:
        print("  site 0x%X  data 0x%X" % (site, drva))
        print("     %s" % text)
        if tgt:
            print("     -> target RVA 0x%X   %s" % (tgt, db.method_name(tgt)))
print()

print("=== folded lambda bodies ===")
seen = set()
for site, drva, text, tgt in resolve(0xCD34B0, 0x120):
    if tgt and tgt not in seen and 'b__3_' in text:
        seen.add(tgt)
        dis(tgt)

print("=== shared trivial bodies ===")
for rva in (0x30C000, 0x30D8F0, 0x331650, 0x33A4E0, 0x33FBC0):
    dis(rva)

print("=== OnDestroy 0xCD39E0 slots ===")
for site, drva, text, tgt in resolve(0xCD39E0, 0x80):
    print("  site 0x%X  %s" % (site, text))
