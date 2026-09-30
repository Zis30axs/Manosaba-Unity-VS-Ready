import struct, os, sys, collections

AA = r'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa'
p = os.path.join(AA, 'catalog.bin')
raw = open(p, 'rb').read()
print('size', len(raw))
magic, ver = struct.unpack_from('<II', raw, 0)
print('magic', hex(magic), 'expected', hex(0x0DE38942), '| version', ver)
n = ver
offs = struct.unpack_from('<%dI' % n, raw, 8)
print('offset table entries:', n)
print('offsets:', offs)
print('header size =', 8 + 4 * n)
hdr = 8 + 4 * n
print('first offset', offs[0], 'gap from header', offs[0] - hdr)
print()
print('bytes right after header (hdr..hdr+64):')
print(' '.join('%02x' % b for b in raw[hdr:hdr + 64]))
print('as u32:', struct.unpack_from('<16I', raw, hdr))
print()
for i, o in enumerate(offs):
    print('off[%d]=%-9d  u32=%s  bytes=%s' % (
        i, o, struct.unpack_from('<I', raw, o)[0],
        ' '.join('%02x' % b for b in raw[o:o + 16])))
print()
# find where the offset table's data region starts/ends
mx = max(offs)
print('max offset', mx, 'file size', len(raw), 'trailing bytes', len(raw) - mx)
