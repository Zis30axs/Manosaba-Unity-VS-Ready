"""Global capstone sweep over every IL2CPP method body.

Produces facts.pkl:
  size[rva]      function byte length
  calls[rva]     set of direct call target RVAs
  callers[rva]   reverse edge (in-degree source)
  nbranch[rva]   conditional-jump count  (control-flow complexity proxy)
  strs[rva]      string-literal RVAs referenced
  tinfo[rva]     Il2CppClass*/TypeInfo slot RVAs referenced
  minfo[rva]     MethodInfo slot RVAs referenced (virtual/interface/generic targets)
  offs[rva]      [reg+imm] displacement values seen (candidate field offsets)
"""
import sys, os, re, pickle, time, bisect
HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE, make_cs
from il2cpp_db import build as build_db

IMAGEBASE = 0x180000000
OUT = os.path.join(HERE, 'facts.pkl')

from capstone import CS_GRP_JUMP, CS_GRP_CALL
from capstone.x86 import X86_OP_MEM, X86_OP_IMM, X86_REG_RIP

def main():
    pe = PE()
    db = build_db()
    md = make_cs()

    starts = sorted(set(db.methods_by_rva.keys()) | set(db.addresses))
    starts = [s for s in starts if pe.section_of(s) in ('il2cpp', '.text')]
    print("sweeping %d function starts" % len(starts))

    size, calls, nbranch, strs, tinfo, minfo, offs = {}, {}, {}, {}, {}, {}, {}
    callers = {}

    strings, metadata, metamethod = db.strings, db.metadata, db.metamethod
    t0 = time.time()
    for i, rva in enumerate(starts):
        if i % 10000 == 0:
            print("  %d/%d  %.0fs" % (i, len(starts), time.time() - t0), flush=True)
        j = bisect.bisect_right(starts, rva)
        end = starts[j] if j < len(starts) else rva + 0x400
        n = end - rva
        if n <= 0 or n > 0x10000:
            n = min(max(n, 0), 0x10000)
        raw = pe.read(rva, n)
        if not raw:
            continue
        raw = raw.rstrip(b'\xcc')
        size[rva] = len(raw)
        if not raw:
            continue

        c_calls = set(); c_str = set(); c_ti = set(); c_mi = set(); c_off = set()
        nb = 0
        try:
            for ins in md.disasm(raw, IMAGEBASE + rva):
                grps = ins.groups
                if CS_GRP_JUMP in grps and ins.mnemonic != 'jmp':
                    nb += 1
                for op in ins.operands:
                    if op.type == X86_OP_IMM and CS_GRP_CALL in grps:
                        t = op.imm - IMAGEBASE
                        c_calls.add(t)
                    elif op.type == X86_OP_MEM:
                        if op.mem.base == X86_REG_RIP:
                            t = (ins.address + ins.size + op.mem.disp) - IMAGEBASE
                            if t in strings:   c_str.add(t)
                            elif t in metadata: c_ti.add(t)
                            elif t in metamethod: c_mi.add(t)
                        elif op.mem.base and 0 < op.mem.disp < 0x1000 and op.mem.index == 0:
                            c_off.add(op.mem.disp)
        except Exception:
            pass

        if c_calls: calls[rva] = c_calls
        if nb: nbranch[rva] = nb
        if c_str: strs[rva] = c_str
        if c_ti: tinfo[rva] = c_ti
        if c_mi: minfo[rva] = c_mi
        if c_off: offs[rva] = c_off
        for t in c_calls:
            callers.setdefault(t, set()).add(rva)

    print("sweep done in %.0fs" % (time.time() - t0))
    with open(OUT, 'wb') as f:
        pickle.dump(dict(size=size, calls=calls, callers=callers, nbranch=nbranch,
                         strs=strs, tinfo=tinfo, minfo=minfo, offs=offs,
                         starts=starts), f, protocol=4)
    print("wrote", OUT)

    # top called .text functions = the runtime helpers
    print("\n== most-called .text helpers (unnamed runtime) ==")
    ranked = sorted(callers.items(), key=lambda kv: -len(kv[1]))
    shown = 0
    for t, cs in ranked:
        if pe.section_of(t) != '.text':
            continue
        if db.method_names(t):
            continue
        print("  0x%-8X  %6d callers" % (t, len(cs)))
        shown += 1
        if shown >= 30:
            break

if __name__ == '__main__':
    main()
