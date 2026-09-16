#!/usr/bin/env python3
"""A03 -- scan every AssetBundle / Player data file, then aggregate.

Runs ``a03_worker.py`` in one isolated process per source file so that a >300 MiB
bundle cannot exhaust the parent's memory, and writes:

  out/a03/per_source/<safe>.json   full scan result per source (containers + objects)
  out/a03/index.json               lightweight per-source summary + run manifest
  out/bundles.json                 aggregated AssetBundle manifest data (with targets)
  out/a03_errors.json              every failure, with the exact command and stderr

Every subprocess runs with ``PYTHONUTF8=1``: the default code page here is cp936 and
source paths / CAB names contain non-ASCII characters, so a locale-dependent decode
silently corrupted earlier output.
"""
from __future__ import annotations

import collections
import json
import os
import subprocess
import sys
import time
from concurrent.futures import ThreadPoolExecutor

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import (  # noqa: E402
    AA_ROOT, GAME_DATA_ROOT, OUT, PYTHON_EXE, load_index_ledger,
)

HERE = os.path.dirname(os.path.abspath(__file__))
WORKER = os.path.join(HERE, "a03_worker.py")
A03_DIR = os.path.join(OUT, "a03")
PER_SOURCE = os.path.join(A03_DIR, "per_source")

PLAYER_FILES = ["level0", "globalgamemanagers", "globalgamemanagers.assets",
                "sharedassets0.assets", "resources.assets",
                "sharedassets0.assets.resS", "globalgamemanagers.assets.resS",
                "resources.assets.resS"]


def safe_name(rel):
    """Filesystem-safe, collision-free name for a logical source id.

    A basename alone is not enough (audit UR-009): two sources may share a file name.
    The full relative path is encoded and a short digest is appended.
    """
    import hashlib
    norm = rel.replace("\\", "/")
    digest = hashlib.sha256(norm.encode("utf-8")).hexdigest()[:10]
    flat = "".join(ch if ch.isalnum() or ch in "._-" else "_" for ch in norm)
    return f"{flat[:150]}.{digest}.json"


def collect_sources():
    """Every bundle on disk plus every Player serialized file, as forward-slash rels."""
    out = []
    if AA_ROOT and os.path.isdir(AA_ROOT):
        for root, dirs, files in os.walk(AA_ROOT):
            for f in files:
                if f.endswith(".bundle"):
                    p = os.path.join(root, f)
                    out.append(os.path.relpath(p, GAME_DATA_ROOT).replace(os.sep, "/"))
    for f in PLAYER_FILES:
        full = os.path.join(GAME_DATA_ROOT, f)
        if os.path.exists(full):
            out.append(f)
    return sorted(set(out))


def child_env():
    env = dict(os.environ)
    env["PYTHONUTF8"] = "1"
    env["PYTHONIOENCODING"] = "utf-8"
    return env


def run_one(rel, force=False):
    out_file = os.path.join(PER_SOURCE, safe_name(rel))
    if not force and os.path.exists(out_file) and os.path.getsize(out_file) > 2:
        try:
            with open(out_file, encoding="utf-8") as fh:
                rec = json.load(fh)
            rec["_cached"] = True
            rec["_elapsed"] = 0.0
            return rec
        except (OSError, ValueError) as exc:
            # A corrupt cache entry must be reported and rebuilt, not silently reused.
            print(f"  [cache] unreadable {out_file}: {exc}", flush=True)
    started = time.time()
    cmd = [PYTHON_EXE, WORKER, rel, out_file]
    try:
        proc = subprocess.run(cmd, capture_output=True, timeout=3600, env=child_env())
    except subprocess.TimeoutExpired:
        return {"rel": rel, "error": "timeout after 3600s", "objects_total": 0, "types": {},
                "serialized_files": [], "assetbundles": [], "_cmd": cmd, "_elapsed": 3600.0}
    if proc.returncode != 0:
        return {"rel": rel, "error": f"exit {proc.returncode}", "objects_total": 0, "types": {},
                "serialized_files": [], "assetbundles": [], "_cmd": cmd,
                "_stderr": proc.stderr.decode("utf-8", "replace")[-1500:],
                "_elapsed": round(time.time() - started, 2)}
    try:
        with open(out_file, encoding="utf-8") as fh:
            rec = json.load(fh)
    except (OSError, ValueError) as exc:
        return {"rel": rel, "error": f"result unreadable: {type(exc).__name__}: {exc}",
                "objects_total": 0, "types": {}, "serialized_files": [], "assetbundles": [],
                "_cmd": cmd, "_stdout": proc.stdout.decode("utf-8", "replace")[-500:],
                "_elapsed": round(time.time() - started, 2)}
    rec["_cached"] = False
    rec["_elapsed"] = round(time.time() - started, 2)
    rec["_cmd"] = cmd
    return rec


def main():
    os.makedirs(PER_SOURCE, exist_ok=True)
    force = "--force" in sys.argv
    sources = collect_sources()
    # --only <substring> limits the run to matching sources (fast smoke tests / regressions)
    only = None
    for i, arg in enumerate(sys.argv):
        if arg == "--only" and i + 1 < len(sys.argv):
            only = sys.argv[i + 1]
    if only:
        sources = [s for s in sources if only in s]
    if not sources:
        print("no sources selected", file=sys.stderr)
        return 2
    print(f"sources: {len(sources)} (force={force}, only={only})", flush=True)

    # Index cross-check uses the canonical ledger, not the raw CSV.
    ledger, _ = load_index_ledger(check_files=False)
    per_src_rows = collections.Counter(r["source_identity"] for r in ledger.rows)
    per_src_types = collections.defaultdict(collections.Counter)
    for r in ledger.rows:
        per_src_types[r["source_identity"]][r["type"]] += 1

    results = []
    t0 = time.time()
    workers = int(os.environ.get("A03_WORKERS", "3"))
    with ThreadPoolExecutor(max_workers=workers) as ex:
        for i, rec in enumerate(ex.map(lambda rel: run_one(rel, force), sources)):
            rel = rec["rel"]
            norm = rel.replace("\\", "/")
            rec["indexed_rows"] = per_src_rows.get(norm, 0)
            rec["indexed_types"] = dict(per_src_types.get(norm, {}))
            results.append(rec)
            if (i + 1) % 20 == 0 or i == len(sources) - 1:
                elapsed = time.time() - t0
                bad = sum(1 for r in results if r.get("error"))
                print(f"  [{i+1}/{len(sources)}] objs={rec.get('objects_total')} "
                      f"idx={rec.get('indexed_rows')} err={bad} ({elapsed:.0f}s)", flush=True)

    # ---- lightweight index -------------------------------------------------
    summary = []
    for rec in results:
        summary.append({
            "rel": rec["rel"],
            "size": rec.get("size"),
            "sha256": rec.get("sha256"),
            "error": rec.get("error"),
            "objects_total": rec.get("objects_total", 0),
            "types": rec.get("types", {}),
            "serialized_files": rec.get("serialized_files", []),
            "object_counts_by_file": rec.get("object_counts_by_file", {}),
            "assetbundle_count": len(rec.get("assetbundles", [])),
            "container_count": sum(a.get("container_count", 0) for a in rec.get("assetbundles", [])),
            "indexed_rows": rec.get("indexed_rows", 0),
            "indexed_types": rec.get("indexed_types", {}),
            "cached": rec.get("_cached"),
            "elapsed": rec.get("_elapsed"),
        })
    manifest = {
        "generated": time.strftime("%Y-%m-%dT%H:%M:%S"),
        "python": PYTHON_EXE,
        "workers": workers,
        "source_count": len(sources),
        "sources": summary,
        "totals": {
            "objects_total": sum(s["objects_total"] for s in summary),
            "serialized_files": sum(len(s["serialized_files"]) for s in summary),
            "containers": sum(s["container_count"] for s in summary),
            "indexed_rows": sum(s["indexed_rows"] for s in summary),
            "failed_sources": sum(1 for s in summary if s["error"]),
        },
    }
    with open(os.path.join(A03_DIR, "index.json"), "w", encoding="utf-8") as fh:
        json.dump(manifest, fh, ensure_ascii=False, indent=1)

    bundles = []
    for rec in results:
        for ab in rec.get("assetbundles", []):
            bundles.append({"source": rec["rel"].replace("\\", "/"), **ab})
    with open(os.path.join(OUT, "bundles.json"), "w", encoding="utf-8") as fh:
        json.dump(bundles, fh, ensure_ascii=False, indent=1)

    errors = [{"rel": r["rel"], "error": r.get("error"), "cmd": r.get("_cmd"),
               "stderr": r.get("_stderr"), "stdout": r.get("_stdout")}
              for r in results if r.get("error")]
    with open(os.path.join(OUT, "a03_errors.json"), "w", encoding="utf-8") as fh:
        json.dump(errors, fh, ensure_ascii=False, indent=1)

    print(json.dumps(manifest["totals"], ensure_ascii=False, indent=1))
    print("errors:", len(errors))
    for e in errors[:10]:
        print("  ", e["rel"], "->", e["error"])
    return 0 if not errors else 1


if __name__ == "__main__":
    sys.exit(main())
