"""Per-subsystem reconstruction progress, measured the way rank2.py buckets totals.

Totals come from ranked.json (written by rank2.py): unique WitchTrials.* bodies,
folded trivial accessors (alias > 1 and size <= 0x40) excluded, each RVA counted
once under the first matching subsystem regex. Compiler-generated nested types
(async state machines `<X>d__N`, lambda holders `<>c`) are not counted at all:
dump.cs gives them an empty `// Namespace:` line, so their full names lack the
`WitchTrials.` prefix rank2.py filters on. An async method therefore counts only
its entry shell, and static lambdas count nowhere; instance lambdas compiled as
methods of the outer class (e.g. `<Ready>b__22_0`) are counted normally.

A body counts as done when its RVA appears on a `// RECONSTRUCTED` marker line
(or the line right after it) anywhere under src/decompiled/Assembly-CSharp.

Usage: progress.py [SYSTEM ...]    also lists the unmarked bodies of each SYSTEM
"""
import collections
import glob
import json
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
ROOT = os.path.normpath(os.path.join(HERE, '..', '..'))
SRC = os.path.join(ROOT, 'src', 'decompiled', 'Assembly-CSharp')

rows = json.load(open(os.path.join(HERE, 'ranked.json'), encoding='utf-8'))['rows']
by_rva = {r['rva']: r for r in rows}

done = set()
for path in glob.glob(os.path.join(SRC, '**', '*.cs'), recursive=True):
    lines = open(path, encoding='utf-8', errors='replace').read().splitlines()
    for i, line in enumerate(lines):
        if line.lstrip().startswith('//') and 'RECONSTRUCTED' in line:
            blob = line + (' ' + lines[i + 1] if i + 1 < len(lines) else '')
            for h in re.findall(r'0x[0-9A-Fa-f]+', blob):
                if int(h, 16) in by_rva:
                    done.add(int(h, 16))

tot_n, tot_b, dn_n, dn_b = (collections.Counter() for _ in range(4))
for r in rows:
    s = r['system']
    tot_n[s] += 1
    tot_b[s] += r['size']
    if r['rva'] in done:
        dn_n[s] += 1
        dn_b[s] += r['size']

print("%-18s %17s %21s" % ("system", "bodies", "bytes"))
for s in sorted(tot_n, key=lambda s: -tot_b[s]):
    print("%-18s %5d/%-5d %4.0f%%  %6d/%-6d %4.0f%%" % (
        s, dn_n[s], tot_n[s], 100.0 * dn_n[s] / tot_n[s],
        dn_b[s], tot_b[s], 100.0 * dn_b[s] / tot_b[s]))
print("%-18s %5d/%-5d        %6d/%-6d" % (
    "TOTAL", sum(dn_n.values()), len(rows), sum(dn_b.values()), sum(tot_b.values())))

for want in sys.argv[1:]:
    print("\nunmarked bodies in %s:" % want)
    pending = sorted((r for r in rows if r['system'] == want and r['rva'] not in done),
                     key=lambda r: (r['type'], r['rva']))
    for r in pending:
        print("  0x%-7X %5d  %s :: %s" % (
            r['rva'], r['size'], r['type'].replace('WitchTrials.', ''), r['sig'][:80]))
