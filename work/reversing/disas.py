"""Annotated disassembler for Manosaba IL2CPP native functions.

Resolves, per instruction:
  * call targets        -> managed method names (script.json ScriptMethod)
  * rip-relative loads  -> string literals / Il2CppClass* / MethodInfo* slots
  * [reg+imm] accesses  -> candidate field names for a supplied type

Usage:
    python disas.py 0x39DA50 --type WitchTrials.Models.TrialStateManager
    python disas.py "WitchTrials.Models.TrialStateManager::RegisterCameraState"
"""
import sys, os, re, argparse

HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE, make_cs
from il2cpp_db import build as build_db, EMPTY_THUNK

IMAGEBASE = 0x180000000

# Hot IL2CPP runtime helpers, identified empirically from this binary by
# call-site context + tail-call chains (see ident_helpers.py).
# Marked BOILERPLATE if they are codegen scaffolding rather than game logic.
HELPERS = {
    0x250FB0: ('il2cpp_codegen_initialize_runtime_metadata', True),
    0x246350: ('il2cpp_codegen_initialize_runtime_metadata (thunk)', True),
    0x246730: ('THROW NullReferenceException', False),
    0x2980E0: ('il2cpp_codegen_object_new', False),
    0x2466E0: ('il2cpp_codegen_object_new (thunk)', False),
    0x24CE60: ('interface/virtual dispatch resolve', False),
    0x243A20: ('resolve interface method slot', False),
    0x22E0:   ('interface call thunk (get_Count sites)', False),
    # Per-instantiation IList<T>.get_Item thunks. Identified from usage, not
    # from a symbol: called as (r8 = list, r9d = index) -> element pointer in
    # rax, whose fields are then written. Observed element types below; the
    # 0x7EC0/0x7F50 split tracks the instantiation but is not perfectly clean,
    # so treat the element type as a hint, not a guarantee.
    0x7EC0:   ('IList<T>.get_Item thunk (seen: ScreenState)', False),
    0x7F50:   ('IList<T>.get_Item thunk (seen: CameraState)', False),
    0x246570: ('il2cpp_value_box', False),
}


def func_bounds(pe, db, rva):
    """Function extent: next known symbol/address, trimmed of int3 padding."""
    cands = []
    e = db.func_end(rva)
    if e > rva:
        cands.append(e)
    import bisect
    i = bisect.bisect_right(db.addresses, rva)
    if i < len(db.addresses):
        cands.append(db.addresses[i])
    end = min(cands) if cands else rva + 0x400
    if end - rva > 0x8000:
        end = rva + 0x8000
    raw = pe.read(rva, end - rva)
    raw = raw.rstrip(b'\xcc')
    return rva, rva + len(raw), raw


                          # width of the memory operand, for disambiguation
WIDTH_OF = {'byte': 1, 'word': 2, 'dword': 4, 'qword': 8, 'xmmword': 16}

# rough C#-type -> byte width, used to reject offset matches of the wrong size
def _type_width(ft):
    ft = ft.strip()
    if ft in ('bool', 'byte', 'sbyte'):            return 1
    if ft in ('short', 'ushort', 'char'):          return 2
    if ft in ('int', 'uint', 'float'):             return 4
    if ft in ('long', 'ulong', 'double'):          return 8
    if ft.endswith('[]') or ft.startswith(('IList', 'List', 'IReadOnlyList',
                                           'IDictionary', 'Dictionary', 'string',
                                           'I', 'Nullable')):
        return 8
    return None            # enums / structs / refs: unknown, accept any


def candidate_types(db, typename):
    """The declaring type plus its nested types -- the realistic set of objects
    a method of `typename` dereferences."""
    if not typename:
        return []
    out = [typename]
    # Il2CppDumper emits nested types with an empty namespace line, so they are
    # keyed as "Outer.Nested" rather than "Ns.Outer.Nested". Try both forms.
    short = typename.rsplit('.', 1)[-1]
    for pre in (typename + '.', short + '.'):
        out += [t for t in db.types if t.startswith(pre) and t not in out]
    return out


def fields_at(db, typenames, off, width=None):
    """All (type, field) candidates having a field at `off`, width-filtered."""
    hits = []
    for tn in typenames:
        t = db.types.get(tn)
        if not t:
            continue
        for fn, ft in t['fields'].get(off, []):
            w = _type_width(ft)
            if width and w and w != width:
                continue
            hits.append((tn.rsplit('.', 1)[-1], fn, ft))
    return hits


def annotate(rva, typename=None, maxins=4000, show_boiler=True):
    pe = PE()
    db = build_db()
    md = make_cs()

    start, end, code = func_bounds(pe, db, rva)
    names = db.method_names(rva)
    out = []
    out.append("=" * 100)
    out.append("FUNCTION RVA 0x%X  VA 0x%X  size 0x%X (%d bytes)" % (start, IMAGEBASE + start, end - start, end - start))
    for n in names:
        out.append("  managed: %s" % n)
    sig = db.sig_by_rva.get(rva)
    if sig:
        out.append("  csig   : %s" % sig)
    owners = db.rva_owner.get(rva, [])
    for t, s in owners[:6]:
        out.append("  dump.cs: %s :: %s" % (t, s))
    if len(owners) > 6:
        out.append("  dump.cs: ... +%d more aliases (shared body)" % (len(owners) - 6))
    cand_types = candidate_types(db, typename)
    for tn in cand_types:
        t = db.types.get(tn)
        if not t or not t['fields']:
            continue
        out.append("  fields of %s:" % tn)
        for off in sorted(t['fields']):
            for fn, ft in t['fields'][off]:
                out.append("      +0x%-4X %-30s %s" % (off, fn, ft))
    out.append("=" * 100)

    # collect branch targets so we can emit labels
    insns = list(md.disasm(code, IMAGEBASE + start))
    targets = set()
    for ins in insns:
        if ins.group(1) or ins.mnemonic.startswith('j'):  # GRP_JUMP
            m = re.match(r'^0x([0-9a-f]+)$', ins.op_str)
            if m:
                targets.add(int(m.group(1), 16))

    for ins in insns[:maxins]:
        va = ins.address
        comment = []

        # --- call / jmp target resolution
        m = re.match(r'^0x([0-9a-f]+)$', ins.op_str)
        if m and (ins.mnemonic == 'call' or ins.mnemonic.startswith('j')):
            t_va = int(m.group(1), 16)
            t_rva = t_va - IMAGEBASE
            nm = db.method_name(t_rva)
            if nm:
                comment.append("-> %s" % nm)
            elif t_rva in HELPERS:
                hn, boiler = HELPERS[t_rva]
                comment.append(("[boilerplate] " if boiler else "") + hn)
            elif ins.mnemonic == 'call':
                comment.append("-> sub_%X (runtime/helper)" % t_rva)

        # --- rip-relative data resolution
        m = re.search(r'\[rip \+ (0x[0-9a-f]+)\]|\[rip - (0x[0-9a-f]+)\]', ins.op_str)
        if m:
            delta = int(m.group(1), 16) if m.group(1) else -int(m.group(2), 16)
            t_rva = (va + ins.size + delta) - IMAGEBASE
            info = db.describe_data(t_rva)
            if info:
                kind, text = info
                if kind == 'str':
                    comment.append('STR %r' % text)
                elif kind == 'meta':
                    comment.append('TYPE %s' % text)
                elif kind == 'metamethod':
                    tgt = db.metamethod_target(t_rva)
                    tn = db.method_name(tgt) if tgt else None
                    comment.append('METHODINFO %s%s' % (text, (' => ' + tn) if tn else ''))
            else:
                comment.append('data_%X' % t_rva)

        # --- field access resolution (all candidate types, width-filtered)
        if cand_types:
            wm = re.match(r'^(byte|word|dword|qword|xmmword) ptr ', ins.op_str)
            width = WIDTH_OF.get(wm.group(1)) if wm else None
            for mm in re.finditer(r'\[(r[a-z0-9]+) \+ (0x[0-9a-f]+)\]', ins.op_str):
                reg, off = mm.group(1), int(mm.group(2), 16)
                if reg in ('rsp', 'rip'):
                    continue
                hits = fields_at(db, cand_types, off, width)
                if hits:
                    comment.append('%s->%s' % (reg, ' | '.join(
                        '%s.%s' % (t, f) for t, f, _ in hits[:3])))

        c = ("   ; " + " | ".join(comment)) if comment else ""
        label = "\n%X:" % va if va in targets else ""
        if label:
            out.append(label)
        out.append("  0x%X  %-7s %-42s%s" % (va, ins.mnemonic, ins.op_str, c))

    return "\n".join(out)


if __name__ == '__main__':
    ap = argparse.ArgumentParser()
    ap.add_argument('target')
    ap.add_argument('--type', dest='typename', default=None)
    ap.add_argument('--max', type=int, default=4000)
    a = ap.parse_args()

    db = build_db()
    if a.target.startswith('0x') or a.target.isdigit():
        rva = int(a.target, 0)
        tn = a.typename
    else:
        tn, mn = a.target.split('::')
        t = db.types.get(tn)
        if not t:
            sys.exit("type not found: " + tn)
        cands = [r for r, s in t['methods'] if re.search(r'\b%s\s*\(' % re.escape(mn), s)]
        if not cands:
            sys.exit("method not found: " + mn)
        rva = cands[0]
        a.typename = a.typename or tn
        tn = a.typename

    print(annotate(rva, tn, a.max))
