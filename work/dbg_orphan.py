import sys, os, json, tempfile, csv, shutil
sys.path.insert(0, r'E:\Manosaba-VSCodeReady\tools\audit')
import a08_prefab_sweep as a08
import common

tmp = tempfile.mkdtemp(prefix='dbg_')
res = os.path.join(tmp, 'res')
os.makedirs(res, exist_ok=True)
cont = 'Assets/P.prefab'


def ptr(pid, fid=0):
    return {"m_FileID": fid, "m_PathID": pid}


objects = {
    1: ("GameObject", {"m_Name": "Root", "m_Component": [{"component": ptr(2)}, {"component": ptr(3)}]}),
    2: ("Transform", {"m_GameObject": ptr(1), "m_Father": ptr(0), "m_Children": []}),
    3: ("MeshRenderer", {"m_GameObject": ptr(1)}),
    # detached subtree: root has no children; T10's father points nowhere
    9: ("GameObject", {"m_Name": "Orphan", "m_Component": [{"component": ptr(10)}]}),
    10: ("Transform", {"m_GameObject": ptr(9), "m_Father": ptr(777), "m_Children": []}),
}
rows = []
for pid, (t, doc) in objects.items():
    out = f'Assets/P~{pid}.json'
    p = os.path.join(res, out.replace('/', os.sep))
    os.makedirs(os.path.dirname(p), exist_ok=True)
    json.dump(doc, open(p, 'w', encoding='utf-8'))
    rows.append({"type": t, "name": "", "asset_path": cont, "output_file": out,
                 "source_bundle": "aa/x.bundle", "path_id": str(pid)})

idx = os.path.join(tmp, '_index.csv')
with open(idx, 'w', encoding='utf-8', newline='') as fh:
    w = csv.DictWriter(fh, fieldnames=["type", "name", "asset_path", "output_file", "source_bundle", "path_id"])
    w.writeheader()
    for r in rows:
        w.writerow(r)

ledger, _ = common.load_index_ledger(index_csv=idx, res_root=res)
rows2 = [r for r in ledger.rows if r['asset_path'] == cont]
print('rows:', len(rows2), [ (r['pid_int'], r['type']) for r in rows2])
resd = a08.validate_container(cont, rows2, {}, False)
print('clean', resd['clean'], 'roots', resd['roots'], 'root_path_ids', resd['root_path_ids'])
print('reachable', resd['reachable_transforms'])
print('orphan_tr', resd['orphan_transforms'], 'orphan_go', resd['orphan_gameobjects'])
print('issues:')
for i in resd['issues']:
    print('   ', json.dumps(i, ensure_ascii=False))
shutil.rmtree(tmp, ignore_errors=True)
