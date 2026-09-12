"""Core PE + capstone helpers for the Manosaba GameAssembly.dll analysis."""
import struct, os

GAME_DLL = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\GameAssembly.dll"

class PE:
    def __init__(self, path=GAME_DLL):
        self.path = path
        self.data = open(path, 'rb').read()
        d = self.data
        e = struct.unpack_from('<I', d, 0x3C)[0]
        assert d[e:e+4] == b'PE\0\0'
        coff = e + 4
        machine, nsec, tstamp = struct.unpack_from('<HHI', d, coff)
        sizeopt = struct.unpack_from('<H', d, coff+16)[0]
        opt = coff + 20
        magic = struct.unpack_from('<H', d, opt)[0]
        assert magic == 0x20b, "expected PE32+"
        self.imagebase = struct.unpack_from('<Q', d, opt+24)[0]
        numdd = struct.unpack_from('<I', d, opt+108)[0]
        self.datadirs = [struct.unpack_from('<II', d, opt+112+i*8) for i in range(numdd)]
        self.sections = []
        so = opt + sizeopt
        for i in range(nsec):
            b = d[so+i*40: so+i*40+40]
            name = b[:8].rstrip(b'\0').decode('latin1')
            vsize, vaddr, rawsize, rawptr = struct.unpack_from('<IIII', b, 8)
            self.sections.append((name, vaddr, vsize, rawptr, rawsize))

    def rva2off(self, rva):
        for name, va, vs, rp, rs in self.sections:
            if va <= rva < va + max(vs, rs):
                off = rp + (rva - va)
                return off if off < len(self.data) else None
        return None

    def section_of(self, rva):
        for name, va, vs, rp, rs in self.sections:
            if va <= rva < va + max(vs, rs):
                return name
        return None

    def read(self, rva, n):
        o = self.rva2off(rva)
        if o is None:
            return b''
        return self.data[o:o+n]

    def u32(self, rva):
        b = self.read(rva, 4)
        return struct.unpack('<I', b)[0] if len(b) == 4 else None

    def u64(self, rva):
        b = self.read(rva, 8)
        return struct.unpack('<Q', b)[0] if len(b) == 8 else None

    def pdata_functions(self):
        """Parse .pdata (IMAGE_DIRECTORY_ENTRY_EXCEPTION) -> sorted list of (start_rva, end_rva)."""
        rva, size = self.datadirs[3]
        off = self.rva2off(rva)
        out = []
        seen = set()
        for i in range(size // 12):
            s, e, u = struct.unpack_from('<III', self.data, off + i*12)
            if s == 0 and e == 0:
                continue
            if s in seen:
                continue
            seen.add(s)
            out.append((s, e))
        out.sort()
        return out


def make_cs():
    from capstone import Cs, CS_ARCH_X86, CS_MODE_64
    md = Cs(CS_ARCH_X86, CS_MODE_64)
    md.detail = True
    return md
