import sys, os, struct
HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE

pe = PE()
rva, size = pe.datadirs[0]
off = pe.rva2off(rva)
d = pe.data
(flags, ts, mj, mn, nameRva, ordBase, nFunc, nName, funcRva, nameRvaT, ordRvaT) = struct.unpack_from('<IIHHIIIIIII', d, off)
print("exports: %d functions, %d names, ordinal base %d" % (nFunc, nName, ordBase))

fo = pe.rva2off(funcRva); no = pe.rva2off(nameRvaT); oo = pe.rva2off(ordRvaT)
exports = {}
for i in range(nName):
    nr = struct.unpack_from('<I', d, no + i*4)[0]
    so = pe.rva2off(nr)
    end = d.index(b'\0', so)
    nm = d[so:end].decode('latin1')
    ordi = struct.unpack_from('<H', d, oo + i*2)[0]
    fr = struct.unpack_from('<I', d, fo + ordi*4)[0]
    exports[fr] = nm

print("resolved %d exported RVAs" % len(exports))
for r in sorted(exports)[:25]:
    print("  0x%-8X %s" % (r, exports[r]))

print("\n-- helpers seen in RegisterScreenVisibilityState --")
for r in (0x250FB0, 0x22E0, 0x2980E0, 0x243A20, 0x7EC0, 0x246730):
    print("  0x%-8X %s  [section %s]" % (r, exports.get(r, "(not exported)"), pe.section_of(r)))

import pickle
pickle.dump(exports, open(os.path.join(HERE, 'exports.pkl'), 'wb'))
