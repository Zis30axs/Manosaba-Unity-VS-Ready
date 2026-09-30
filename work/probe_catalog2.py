import struct, os, collections

AA = r'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa'
raw = open(os.path.join(AA, 'catalog.bin'), 'rb').read()
n = struct.unpack_from('<I', raw, 4)[0]
offs = struct.unpack_from('<%dI' % n, raw, 8)
print('offset table count', n)
print('offsets:', [hex(o) for o in offs])
print()

STR_REF = 0x80000000
ID_MASK = 0x7fffffff


def sref_at(pos):
    v = struct.unpack_from('<I', raw, pos)[0]
    if v & STR_REF:
        q = v & ID_MASK
        end = raw.index(b'\x00', q)
        return raw[q:end].decode('utf-8', 'replace')
    return None


for i, o in enumerate(offs):
    print('--- offsets[%d] = 0x%06x (%d) ---' % (i, o, o))
    print('   first 8 u32:', struct.unpack_from('<8I', raw, o))
    if i < 14:
        # try interpreting as string ref
        s = sref_at(o)
        print('   as str-ref ->', repr(s))
        # first few entries as string refs
        vals = struct.unpack_from('<4I', raw, o)
        strs = []
        for v in vals:
            if v & STR_REF:
                q = v & ID_MASK
                try:
                    end = raw.index(b'\x00', q)
                    strs.append(raw[q:end].decode('utf-8', 'replace'))
                except ValueError:
                    strs.append('<noNUL>')
            else:
                strs.append('int:%d' % v)
        print('   first4 as mixed ->', strs)
    print()

print('=== is there a strings region? look at offsets[13] ===')
o = offs[13]
print('at offs[13]:', repr(raw[o:o + 120]))
