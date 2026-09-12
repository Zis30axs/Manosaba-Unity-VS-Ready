"""Identify the hot .text runtime helpers by disassembling them and following
tail-calls / internal calls to exported il2cpp_* symbols."""
import sys, os, pickle, re
HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE, make_cs
from il2cpp_db import build as build_db

IMAGEBASE = 0x180000000
pe = PE(); db = build_db(); md = make_cs()
facts = pickle.load(open(os.path.join(HERE, 'facts.pkl'), 'rb'))
exports = pickle.load(open(os.path.join(HERE, 'exports.pkl'), 'rb'))

TOP = [0x250FB0, 0x246730, 0x24CE60, 0x2466F0, 0x2980E0, 0x246350, 0x2466E0,
       0x25BFD0, 0x25E5A0, 0x25E240, 0x243570, 0x246740, 0x2D3060, 0x246EC0,
       0x306510, 0x2435F0, 0x246720, 0x22E0, 0x298020, 0x297400, 0x306BB0,
       0x29F650, 0x297D30, 0x2390, 0x2460D0, 0x2981F0, 0x299D20, 0x246900,
       0x243A20, 0x4A40, 0x7EC0]

for rva in TOP:
    n = min(facts['size'].get(rva, 96), 220)
    raw = pe.read(rva, n).rstrip(b'\xcc')
    print("=" * 92)
    print("0x%X   %d callers   size %d   export=%s" % (
        rva, len(facts['callers'].get(rva, ())), facts['size'].get(rva, 0),
        exports.get(rva, '-')))
    lines = []
    for ins in md.disasm(raw, IMAGEBASE + rva):
        c = ''
        m = re.match(r'^0x([0-9a-f]+)$', ins.op_str)
        if m and ins.mnemonic in ('call', 'jmp'):
            t = int(m.group(1), 16) - IMAGEBASE
            nm = exports.get(t) or db.method_name(t)
            c = '   ; -> %s' % (nm if nm else 'sub_%X' % t)
        m2 = re.search(r'\[rip \+ (0x[0-9a-f]+)\]', ins.op_str)
        if m2:
            t = ins.address + ins.size + int(m2.group(1), 16) - IMAGEBASE
            info = db.describe_data(t)
            if info:
                c += '   ; %s %s' % (info[0], str(info[1])[:60])
        lines.append("   %-7s %-40s%s" % (ins.mnemonic, ins.op_str, c))
    print("\n".join(lines[:22]))
