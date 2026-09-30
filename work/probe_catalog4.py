import struct, os, re

AA = r'E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa'
raw = open(os.path.join(AA, 'catalog.bin'), 'rb').read()
print('size', len(raw))


def show(pos, n=96, label=''):
    print('--- %s @0x%06x ---' % (label, pos))
    chunk = raw[pos:pos + n]
    print('  hex :', ' '.join('%02x' % b for b in chunk[:48]))
    print('  txt :', repr(chunk.decode('utf-8', 'replace')))


for pos, label in [(0, 'magic'), (8, 'offset table'), (16, 'data start'),
                   (36, 'offs[0]'), (3224504, 'int 3224504'),
                   (457736, 'offs[1]'), (458063, 'int 458063'),
                   (3065462, '3065462'), (1919181, '1919181')]:
    show(pos, 80, label)

print()
print('=== scan for UTF-8 text runs >= 24 chars ===')
runs = []
i = 0
n = len(raw)
while i < n:
    if 0x20 <= raw[i] < 0x7f:
        j = i
        while j < n and (0x20 <= raw[j] < 0x7f):
            j += 1
        if j - i >= 24:
            runs.append((i, j - i))
        i = j
    else:
        i += 1
print('text runs >=24:', len(runs))
print('first 10:')
for r in runs[:10]:
    print('   0x%06x len=%d %r' % (r[0], r[1], raw[r[0]:r[0] + 60].decode()))
print('last 5:')
for r in runs[-5:]:
    print('   0x%06x len=%d %r' % (r[0], r[1], raw[r[0]:r[0] + 60].decode()))
# where do runs start clustering?
import collections
buckets = collections.Counter(r[0] // 100000 for r in runs)
print('run-start buckets (x100000):', sorted(buckets.items()))
