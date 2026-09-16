"""Classify the native body behind an RVA: real no-op / trivial ctor / has behavior.

Used to re-validate the `confirmed-noop` heuristic in
tools/unity-rebuild/out/stub_inventory.csv (shared RVA >= 5 => no-op).

Usage:
    python rva_classify.py 0x30C000 0x30D8F0 ...
    python rva_classify.py --file rvas.txt
    python rva_classify.py --json out.json 0x30C000 ...
"""
import json
import struct
import sys

from capstone import CS_ARCH_X86, CS_MODE_64, Cs

from il2cpp_pe import PE

# IL2CPP runtime boilerplate that carries no game-logic meaning (see
# work/reversing/README.md "已识别的运行时 helper").
BOILERPLATE_CALLS = {
    0x250FB0: "il2cpp_codegen_initialize_runtime_metadata",
    0x246350: "il2cpp_codegen_initialize_runtime_metadata(thunk)",
    0x2980E0: "il2cpp_codegen_object_new",
    0x2466E0: "il2cpp_codegen_object_new(thunk)",
    0x24CE60: "dispatch_resolve",
    0x243A20: "resolve_interface_slot",
    0x22E0: "interface_call_thunk",
}
# Null-reference-check landing pad: a `call` here is the inlined throw, which
# still means "the original code dereferenced something".
NRE_THROW = 0x246730


_PDATA = None
_DB = None


def _db():
    global _DB
    if _DB is None:
        import il2cpp_db
        _DB = il2cpp_db.build()
    return _DB


def func_extent(pe, rva):
    """Real end RVA of the function containing `rva`.

    Mirrors disas.py::func_bounds -- leaf functions (e.g. a 3-byte `ret`) carry
    no unwind info and are therefore absent from .pdata, so the symbol DB's
    "next known address" is the reliable upper bound. Returns None if neither
    source can bound the function.
    """
    global _PDATA
    if _PDATA is None:
        _PDATA = pe.pdata_functions()
    for s, e in _PDATA:
        if s == rva:
            return e
    try:
        import bisect
        db = _db()
        cands = []
        e = db.func_end(rva)
        if e and e > rva:
            cands.append(e)
        i = bisect.bisect_right(db.addresses, rva)
        if i < len(db.addresses):
            cands.append(db.addresses[i])
        if cands:
            return min(cands)
    except Exception:
        pass
    return None


def decode(pe, md, rva):
    off = pe.rva2off(rva)
    if off is None:
        return []
    end = func_extent(pe, rva)
    n = (end - rva) if end else 64
    n = max(1, min(n, 4096))
    raw = pe.data[off:off + n].rstrip(b'\xcc')   # strip int3 alignment padding
    return list(md.disasm(raw, 0x180000000 + rva))


WRITE_MNEMONICS = (
    "mov", "movss", "movsd", "movups", "movaps", "movdqu", "movdqa", "movzx",
    "movsx", "movsxd", "or", "and", "xor", "add", "sub", "inc", "dec", "not",
    "neg", "bts", "btr", "cmpxchg", "xchg", "stosb", "stosd", "stosq", "sal",
    "shl", "shr", "sar",
)

# registers that carry a managed method's return value
RET_REGS = {"rax", "eax", "ax", "al", "xmm0"}


def _mem_dst(ins):
    """Return the destination operand if it is a register-based memory write."""
    if ins.mnemonic not in WRITE_MNEMONICS:
        return None
    if not ins.op_str:
        return None
    dst = ins.op_str.split(",")[0].strip()
    if "[" not in dst:
        return None
    if "rip" in dst or "rsp" in dst:      # rip-relative = IL2CPP globals; rsp = spill
        return None
    return ins.mnemonic + " " + dst


def _mem_read(ins):
    """Return the source operand if the instruction reads register-based memory."""
    if not ins.op_str or "[" not in ins.op_str:
        return None
    parts = [p.strip() for p in ins.op_str.split(",")]
    src = parts[-1]
    if "[" not in src:
        return None
    if "rip" in src or "rsp" in src or "rbp" in src:
        return None
    return ins.mnemonic + " " + src


def classify(pe, insns):
    """Return (verdict, notes).

    verdict:
      real-noop       -- body is literally just `ret`: the managed method has no
                         behaviour at all
      trivial-getter  -- `mov rax,[rcx+off]; ret`: returns one instance field
      trivial-setter  -- `mov [rcx+off],<arg>; ret`: stores one argument
      guard-only      -- only IL2CPP runtime boilerplate (one-time type-init
                         guard + tail dispatch), no instance memory access and
                         no managed call
      has-behavior    -- reads string literals, calls managed code, or does more
                         than a single load/store
      unknown         -- not reducible
    """
    if not insns:
        return "unknown", ["no instructions decoded"]

    body = []
    managed_calls = []
    has_nre = False
    tail_dispatch = False
    for ins in insns:
        if ins.mnemonic == "lock":
            continue
        if ins.mnemonic in ("call", "jmp"):
            op = ins.op_str.split()[0] if ins.op_str else ""
            tgt = int(op, 16) - 0x180000000 if op.startswith("0x") else None
            if tgt in BOILERPLATE_CALLS:
                if tgt == 0x24CE60:
                    tail_dispatch = True
                continue
            if tgt == NRE_THROW:
                has_nre = True
                continue
            managed_calls.append("0x%X" % tgt if tgt else ins.op_str)
            continue
        body.append(ins)

    writes = [w for w in (_mem_dst(i) for i in body) if w]
    reads = [r for r in (_mem_read(i) for i in body) if r]

    # --- exact 2-instruction forms -------------------------------------------
    if not managed_calls and not has_nre and len(body) == 2 and body[1].mnemonic == "ret":
        first = body[0]
        dst = first.op_str.split(",")[0].strip() if first.op_str else ""
        if writes:
            return "trivial-setter", ["stores one argument: " + writes[0]]
        if dst in RET_REGS and first.mnemonic in ("mov", "movzx", "movsx", "movsxd",
                                                  "movss", "movsd"):
            if "[" in first.op_str:
                return "trivial-getter", ["loads and returns one instance field: " + first.op_str]
            return "const-return", ["returns a constant: " + first.mnemonic + " " + first.op_str]
        if first.mnemonic == "xor" and dst in ("eax", "rax", "ax", "al"):
            return "const-return", ["returns constant zero/false: xor %s" % dst]
        if first.mnemonic in ("mov", "and", "or") and "[" in first.op_str and reads:
            return "trivial-getter", ["loads and returns one instance field: " + reads[0]]

    if not writes and not managed_calls and not has_nre:
        if len(body) == 1 and body[0].mnemonic == "ret":
            return "real-noop", ["single `ret` (3-byte shared empty body)"]
        if len(body) <= 2 and all(i.mnemonic in ("ret", "nop", "endbr64") for i in body):
            return "real-noop", ["only ret/nop"]
        if len(body) <= 6:
            return "real-noop", ["prologue/epilogue only (%d insn)" % len(body)]
        return "guard-only", [
            "only IL2CPP runtime boilerplate, no instance memory access, no "
            "managed call (%d insn%s)" % (len(body),
                                          ", one-time type-init guard + tail dispatch"
                                          if tail_dispatch else "")
        ]

    if writes:
        return "has-behavior", ["writes memory: %s" % writes[0]]
    if managed_calls:
        return "has-behavior", ["calls managed method(s): %s" % ", ".join(managed_calls[:3])]
    return "has-behavior", ["contains the inlined NullReferenceException throw"]


def summarize(pe, md, rva):
    off = pe.rva2off(rva)
    insns = decode(pe, md, rva)
    verdict, notes = classify(pe, insns)
    return {
        "rva": "0x%X" % rva,
        "section": pe.section_of(rva),
        "bytes": len(insns) and (insns[-1].address + insns[-1].size) - (0x180000000 + rva) or 0,
        "n_insn": len(insns),
        "verdict": verdict,
        "notes": notes,
        "text": ["%s %s" % (i.mnemonic, i.op_str) for i in insns],
    }


def main(argv):
    as_json = None
    rvas = []
    i = 0
    while i < len(argv):
        a = argv[i]
        if a == "--json":
            as_json = argv[i + 1]
            i += 2
            continue
        if a == "--file":
            with open(argv[i + 1]) as fh:
                rvas += [ln.strip() for ln in fh if ln.strip()]
            i += 2
            continue
        rvas.append(a)
        i += 1

    pe = PE()
    md = Cs(CS_ARCH_X86, CS_MODE_64)
    md.detail = False

    out = [summarize(pe, md, int(r, 16)) for r in rvas]

    if as_json:
        with open(as_json, "w") as fh:
            json.dump(out, fh, indent=1)
    else:
        for s in out:
            print("=" * 90)
            print("RVA %s  bytes=%d  insns=%d  => %s" % (s["rva"], s["bytes"], s["n_insn"], s["verdict"]))
            for n in s["notes"]:
                print("   note: %s" % n)
            for t in s["text"][:40]:
                print("   %s" % t)
            if len(s["text"]) > 40:
                print("   ... (%d more instructions)" % (len(s["text"]) - 40))
    return 0


if __name__ == "__main__":
    sys.exit(main(sys.argv[1:]))
