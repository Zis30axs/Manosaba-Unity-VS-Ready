import struct, os

AA = r'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa'
raw = open(os.path.join(AA, 'catalog.bin'), 'rb').read()
STR_REF = 0x80000000
ID_MASK = 0x7fffffff


def deref(v):
    if not (v & STR_REF):
        return None
    q = v & ID_MASK
    end = raw.index(b'\x00', q)
    return raw[q:end].decode('utf-8', 'replace')


# hypothesis: at offs[0] there is a length-prefixed array of string refs
base = 36
print('=== dump u32 at base=36, 40 values ===')
vals = struct.unpack_from('<40I', raw, base)
for i, v in enumerate(vals):
    s = deref(v)
    print('  [%2d] 0x%08x  %s  %r' % (i, v, ('str' if s is not None else 'int'), s if s is not None else v))
