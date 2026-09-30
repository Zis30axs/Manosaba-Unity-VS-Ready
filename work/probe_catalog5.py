import struct, os, collections, json

AA = r'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa'
raw = open(os.path.join(AA, 'catalog.bin'), 'rb').read()
STR_REF = 0x80000000


def u32(p):
    return struct.unpack_from('<I', raw, p)[0]


def u64(p):
    return struct.unpack_from('<Q', raw, p)[0]


def i32(p):
    return struct.unpack_from('<i', raw, p)[0]


def deref(v):
    """String ref: high bit set, low 31 bits = absolute file offset of NUL-terminated utf8."""
    if not (v & STR_REF):
        return None
    q = v & 0x7fffffff
    end = raw.index(b'\x00', q)
    return raw[q:end].decode('utf-8', 'replace')


def cstr(p):
    end = raw.index(b'\x00', p)
    return raw[p:end].decode('utf-8', 'replace')


print('header: magic=%s version=%d tablecount=%d' % (hex(u32(0)), u32(4), u32(8)))
print('offsets:', [hex(u32(8 + 4 * i)) for i in range(u32(4))])
print()

# offs[0] = 36: this is the *previous* string table base.  Look at 0x003133b8 record.
rec = 3224504
print('=== record at 0x%06x (offset-array[0]) ===' % rec)
print('raw 64 bytes:', ' '.join('%02x' % b for b in raw[rec:rec + 64]))
for i in range(8):
    v = u32(rec + 4 * i)
    print('  +%2d u32=0x%08x (%d)  str=%r' % (4 * i, v, v, deref(v)))
print()

# The location array: find its start by looking for a rising run of string-refs
def scan_array(start, maxn=6):
    print('=== interpret 0x%06x as array of records ===' % start)
    print('raw:', ' '.join('%02x' % b for b in raw[start:start + 96]))
    pos = start
    for k in range(maxn):
        print('  entry @0x%06x:' % pos)
        for j in range(6):
            v = u32(pos + 4 * j)
            s = deref(v)
            print('     +%2d 0x%08x %s' % (4 * j, v, ('-> %r' % s) if s is not None else '(int %d)' % v))
        break


scan_array(3224504)
print()
# Look at the "keys" table: the second value 0x003133c4
scan_array(3224516)
print()
print('=== try to locate resource-location records by finding the .bundle string @0x3133d8 ===')
# from earlier dump we saw "e29a6d1ebe0abcfb17d895c21f2348b4.bundle" at 0x3133d8
p = 0x3133d8
print('at 0x%06x: %r' % (p, cstr(p)))
print('bytes before:', ' '.join('%02x' % b for b in raw[p - 16:p]))
print('bytes after :', ' '.join('%02x' % b for b in raw[p + 36:p + 36 + 48]))
print('as strings after:', [cstr(p + 36 + k) for k in (0, 8, 16)])
