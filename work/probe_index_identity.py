import csv, os, re, collections, json, sys

RES = r'E:\Manosaba-VSCodeReady\res'
SUF = re.compile(r'~(-?\d+)\.json$', re.I)

rows = []
with open(os.path.join(RES, '_index.csv'), encoding='utf-8-sig', newline='') as fh:
    for r in csv.DictReader(fh):
        rows.append(r)
print('rows', len(rows))

# --- A. is the '~N' suffix per-asset sequential?
per = collections.defaultdict(list)
bad = 0
for r in rows:
    m = SUF.search(r['output_file'])
    if m is None:
        bad += 1
        continue
    per[r['asset_path']].append(int(m.group(1)))
print('rows whose output_file has no ~N suffix:', bad)
print('containers:', len(per))
seq = nonseq = 0
examples = []
for a, v in per.items():
    sv = sorted(v)
    if sv == list(range(sv[0], sv[0] + len(sv))):
        seq += 1
    else:
        nonseq += 1
        if len(examples) < 3:
            examples.append((a, len(v), sv[:6], sv[-3:]))
print('per-asset suffix is contiguous 1..n :', seq, 'not:', nonseq)
for e in examples:
    print('    nonseq example:', e[0][:60], 'n=%d' % e[1], 'head', e[2], 'tail', e[3])

# --- B. uniqueness of (source_bundle, csv path_id)
k = collections.Counter((r['source_bundle'], int(r['path_id'])) for r in rows)
d = [(x, v) for x, v in k.items() if v > 1]
print()
print('distinct (source_bundle, csv path_id):', len(k), ' duplicated:', len(d))
for x, v in sorted(d, key=lambda t: -t[1])[:5]:
    print('    ', x, v)

# --- C. uniqueness of (source_bundle, asset_path, suffix)
k2 = collections.Counter()
for r in rows:
    m = SUF.search(r['output_file'])
    k2[(r['source_bundle'], r['asset_path'], int(m.group(1)) if m else None)] += 1
d2 = [(x, v) for x, v in k2.items() if v > 1]
print()
print('distinct (source, container, suffix):', len(k2), ' duplicated:', len(d2))
for x, v in d2[:5]:
    print('    ', x, v)

# --- D. is csv path_id range plausible as int64 & does it look like a pathID?
vals = [int(r['path_id']) for r in rows]
print()
print('csv path_id min', min(vals), 'max', max(vals))
print('csv path_id is permutation of 0..n-1 ?', sorted(vals) == list(range(0, len(vals))))
print('csv path_id is permutation of 1..n ?', sorted(vals) == list(range(1, len(vals) + 1)))

# --- E. does the suffix ever equal a known pathID? sample check against asset_path level
samp = rows[:5]
for r in samp:
    print('   sample:', r['type'], 'csv_pid=' + r['path_id'], 'suffix=' + str(SUF.search(r['output_file']).group(1)))
