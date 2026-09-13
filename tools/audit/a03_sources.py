#!/usr/bin/env python3
"""A03 - dispatch a03_worker.py for every AssetBundle / player data file (isolated
processes so >1 GiB bundles can't exhaust memory), then aggregate.

Outputs: out/sources.json, out/bundles.json, out/a03_errors.json
"""
import os, sys, json, time, collections, subprocess
from concurrent.futures import ThreadPoolExecutor
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import DATA, AA, OUT, load_index, WT

PY = r"E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe"
WORKER = os.path.join(WT, "tools", "audit", "a03_worker.py")
PLAYER_FILES = ["level0", "globalgamemanagers", "globalgamemanagers.assets",
                "sharedassets0.assets", "resources.assets"]

def collect_sources():
    out = []
    for root, dirs, files in os.walk(AA):
        for f in files:
            if f.endswith(".bundle"):
                p = os.path.join(root, f)
                out.append(os.path.relpath(p, DATA).replace(os.sep, "\\"))
    for f in PLAYER_FILES:
        if os.path.exists(os.path.join(DATA, f)):
            out.append(f)
    return sorted(out)

CACHE = os.path.join(OUT, "a03_cache")
os.makedirs(CACHE, exist_ok=True)

def cache_key(rel):
    return rel.replace("\\", "__").replace("/", "__").replace(":", "_")

def run_one(rel):
    import hashlib
    cf = os.path.join(CACHE, cache_key(rel) + ".json")
    if os.path.exists(cf) and os.path.getsize(cf) > 2:
        try:
            rec = json.load(open(cf, encoding="utf-8"))
            rec["elapsed"] = 0.0
            return rec
        except Exception:
            pass
    t = time.time()
    try:
        p = subprocess.run([PY, WORKER, rel], capture_output=True, timeout=900)
        if p.returncode != 0:
            return {"rel": rel, "error": f"exit {p.returncode}: {p.stderr.decode('utf-8','replace')[-400:]}",
                    "size": os.path.getsize(os.path.join(DATA, rel.replace('/', os.sep))), "objects_total": 0,
                    "types": {}, "serialized_files": [], "assetbundles": []}
        rec = json.loads(p.stdout.decode("utf-8").strip().splitlines()[-1])
    except subprocess.TimeoutExpired:
        rec = {"rel": rel, "error": "timeout", "objects_total": 0, "types": {},
               "serialized_files": [], "assetbundles": []}
    except Exception as e:
        rec = {"rel": rel, "error": f"runner:{type(e).__name__}: {e}", "objects_total": 0, "types": {},
               "serialized_files": [], "assetbundles": []}
    rec["elapsed"] = round(time.time() - t, 2)
    try:
        json.dump(rec, open(os.path.join(CACHE, cache_key(rel) + ".json"), "w", encoding="utf-8"), ensure_ascii=False)
    except Exception as e:
        print("cache write fail", e)
    return rec

def main():
    sources = collect_sources()
    print(f"sources: {len(sources)}", flush=True)
    idx = load_index()
    per_src_rows = collections.Counter(r["source_bundle"] for r in idx)
    per_src_types = collections.Counter((r["source_bundle"], r["type"]) for r in idx)
    results = []
    t0 = time.time()
    with ThreadPoolExecutor(max_workers=int(os.environ.get("A03_WORKERS", "2"))) as ex:
        for i, rec in enumerate(ex.map(run_one, sources)):
            rel = rec["rel"]
            rec["indexed_rows"] = per_src_rows.get(rel, 0)
            rec["indexed_types"] = {t: n for (s, t), n in per_src_types.items() if s == rel}
            results.append(rec)
            if (i + 1) % 25 == 0 or i == len(sources) - 1:
                el = time.time() - t0
                bad = sum(1 for r in results if r.get("error"))
                print(f"  [{i+1}/{len(sources)}] {rel} objs={rec.get('objects_total')} "
                      f"dumped={rec['indexed_rows']} err={bad} ({el:.0f}s)", flush=True)
    with open(os.path.join(OUT, "sources.json"), "w", encoding="utf-8") as fh:
        json.dump(results, fh, ensure_ascii=False, indent=1)
    bundles = []
    for r in results:
        for ab in r.get("assetbundles", []):
            bundles.append({"source": r["rel"], **ab})
    with open(os.path.join(OUT, "bundles.json"), "w", encoding="utf-8") as fh:
        json.dump(bundles, fh, ensure_ascii=False, indent=1)
    errors = [{"rel": r["rel"], "error": r["error"]} for r in results if r.get("error")]
    with open(os.path.join(OUT, "a03_errors.json"), "w", encoding="utf-8") as fh:
        json.dump(errors, fh, ensure_ascii=False, indent=1)
    tot_obj = sum(r.get("objects_total", 0) for r in results)
    tot_idx = sum(r.get("indexed_rows", 0) for r in results)
    print(f"TOTAL objects={tot_obj} indexed={tot_idx} errors={len(errors)}")
    print("sample errors:", errors[:5])

if __name__ == "__main__":
    main()
