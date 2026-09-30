import json, collections, os, sys
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from parse_catalog_membership import Catalog, CATALOG, DUMP

cat = Catalog(CATALOG)
header, keys = cat.keys()

loc = {}
key_locs = {}
for k, offs in keys:
    ids = []
    for o in offs:
        if o not in loc:
            loc[o] = cat.location(o)
        ids.append(o)
    key_locs[k] = ids

def pk(o):
    return loc[o][0]

def iid(o):
    return loc[o][1]

GUID = lambda s: len(s) == 32 and all(c in "0123456789abcdef" for c in s)

# --- the umbrella key ---------------------------------------------------------------
N = key_locs["Naninovel"]
n_edges = len(N)
n_distinct = len(set(N))
print("key 'Naninovel': edges=%d distinct_locations=%d" % (n_edges, n_distinct))
dup = collections.Counter(N)
print("  duplicated offsets inside the key's location set:", sum(1 for v in dup.values() if v > 1))

Nset = set(N)
ns_set = set()
ns_keys = 0
for k, offs in key_locs.items():
    if k.startswith("Naninovel/") and not GUID(k):
        ns_keys += 1
        ns_set.update(offs)
print("keys starting 'Naninovel/': %d  reach %d distinct locations" % (ns_keys, len(ns_set)))

print("Naninovel - Naninovel/* :", len(Nset - ns_set))
print("Naninovel/* - Naninovel :", len(ns_set - Nset))
multi_ns = [k for k in key_locs if k.startswith("Naninovel/") and len(key_locs[k]) > 1]
print("keys starting 'Naninovel/' reaching >1 location:", len(multi_ns),
      "extra edges:", sum(len(key_locs[k]) - 1 for k in multi_ns))
print("  -> 27861 keys + %d extra edges = %d" % (sum(len(key_locs[k]) - 1 for k in multi_ns), ns_keys + sum(len(key_locs[k]) - 1 for k in multi_ns)))
print("is 'Naninovel' ever a location's primary_key:",
      any(pk(o) == "Naninovel" for o in loc))

# what are the extra ones?
extra = sorted(Nset - ns_set)
print("\nextra locations (reached by 'Naninovel' but by no 'Naninovel/*' key):")
by_prefix = collections.Counter()
for o in extra:
    p = pk(o) or ""
    by_prefix[p.split("/")[0] if "/" in p else p] += 1
print("  by primary_key first segment:", by_prefix.most_common(20))
for o in extra[:15]:
    print("   pk=%r\n     iid=%r" % (pk(o), iid(o)))

# do they have any other non-GUID key at all?
loc_to_keys = collections.defaultdict(list)
for k, offs in key_locs.items():
    if GUID(k):
        continue
    for o in offs:
        loc_to_keys[o].append(k)

other_key_count = collections.Counter()
for o in extra:
    n = sum(1 for k in loc_to_keys[o] if k != pk(o))
    other_key_count[n] += 1
print("  number of non-primary non-GUID keys per extra location:", dict(other_key_count))
print("  sample other keys of the first 5 extra locations:")
for o in extra[:5]:
    print("    pk=%r others=%r" % (pk(o), [k for k in loc_to_keys[o] if k != pk(o)][:6]))

# --- how the planner would see it ----------------------------------------------------
# container path : what the ledger calls asset_path -> the location's internal_id
primary_to_offsets = collections.defaultdict(list)
for o in loc:
    primary_to_offsets[pk(o)].append(o)

print("\nmulti-location keys (>1 location):", sum(1 for k, v in key_locs.items() if len(v) > 1))
print("empty keys:", sum(1 for k, v in key_locs.items() if not v))

# distinct label-ish keys: all non-GUID keys that are not someone's primary key
all_pks = set(primary_to_offsets)
labelish = set()
for k in key_locs:
    if GUID(k):
        continue
    if k in all_pks:
        continue
    labelish.add(k)
print("non-GUID keys that are never a primary_key (label-like):", len(labelish))
print("  of those, reaching 1 location:", sum(1 for k in labelish if len(key_locs[k]) == 1))
print("  reaching >1 location:", sum(1 for k in labelish if len(key_locs[k]) > 1))

# Sanity: for the shipped scheme the planner builds 'labels' = every non-GUID key != primary_key
with open(os.path.join(os.path.dirname(DUMP), "a13_import_ledger.csv"), encoding="utf-8-sig", newline="") as fh:
    import csv
    rows = [r for r in csv.DictReader(fh) if r["status"] == "IMPORTED"]
container_paths = sorted({(r["asset_path"] or "").strip() for r in rows if (r["asset_path"] or "").strip()})
print("\nIMPORTED ledger asset_paths:", len(container_paths))
in_shipped = [cp for cp in container_paths if cp in primary_to_offsets]
print("  present in the shipped catalog as a primary_key (=address):", len(in_shipped))
