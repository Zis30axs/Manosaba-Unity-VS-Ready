"""a13e: plan prefab/scene reconstruction from the structural object dumps (G2, biggest remainder).

A prefab in the shipped build is a set of objects in one SerializedFile: GameObjects, their
transforms (Transform/RectTransform), engine components (CanvasRenderer, Image, ...) and script
components (MonoBehaviour), all wired by PPtrs (`{m_FileID, m_PathID}`) where m_FileID == 0 means
"same file" and m_FileID > 0 indexes the file's externals table.

This planner does all of the data work so the Editor-side builder stays mechanical:

  * groups `res/_index.csv` rows by asset path -> the objects of one prefab, each with its dump path
  * resolves each MonoBehaviour's script through out/unity/bundle_script_map.json (a13b)
  * walks every dump for PPtrs and resolves them:
      - intra-asset  (m_FileID == 0)                     -> left to the builder (pid -> created object)
      - external     (m_FileID > 0) -> external CAB -> bundle -> (bundle, pid) -> our asset path
        (the same table a13_import_assets.py used, plus the ScriptableObject targets)
  * reports, per prefab, how many references resolved and which did not, with the reason

Outputs
  tools/unity-rebuild/out/unity/prefab_plan_<group>.json     jobs for the builder
  tools/unity-rebuild/out/unity/prefab_plan_summary.json     coverage numbers

Usage
  python tools/unity-rebuild/a13e_plan_prefabs.py --group pilot --limit 5
  python tools/unity-rebuild/a13e_plan_prefabs.py --group ui --match naninovel-ui
"""
from __future__ import annotations

import argparse
import csv
import datetime
import json
import os
import sys
from collections import Counter, defaultdict

import UnityPy

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
RES = os.path.join(REPO, "res")
INDEX = os.path.join(RES, "_index.csv")
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
SCRIPT_MAP = os.path.join(OUT_DIR, "bundle_script_map.json")
PER_SOURCE = os.path.join(REPO, "tools", "audit", "out", "a03", "per_source")
CONTENT = os.path.join(REPO, "UnityProject", "Assets", "GameContent")

#: types that make up the object graph of a prefab/scene (the structural rows of the index)
GRAPH_TYPES = {
    "GameObject", "Transform", "RectTransform", "CanvasRenderer", "Canvas", "CanvasGroup",
    "Image", "RawImage", "Button", "Toggle", "Slider", "ScrollRect", "Mask", "RectMask2D",
    "TextMeshProUGUI", "Text", "SpriteRenderer", "SpriteMask", "MeshRenderer", "MeshFilter",
    "MeshCollider", "SortingGroup", "Animator", "AudioSource", "BoxCollider2D", "CircleCollider2D",
    "PolygonCollider2D", "Rigidbody2D", "LitMotion", "MonoBehaviour", "Animation",
    "ParticleSystem", "ParticleSystemRenderer", "RotationConstraint", "PlayableDirector",
    "GridLayoutGroup", "HorizontalLayoutGroup", "VerticalLayoutGroup", "ContentSizeFitter",
    "LayoutElement", "AspectRatioFitter", "CanvasScaler", "GraphicRaycaster", "EventTrigger",
}


def game_root():
    cfg = os.path.join(REPO, "tools", "audit", "audit.config.json")
    with open(cfg, encoding="utf-8-sig") as fh:
        return json.load(fh)["game_root"]


def dest_for(asset_path: str) -> str:
    rel = asset_path.replace("\\", "/").lstrip("/")
    if rel.startswith("Assets/"):
        rel = rel[len("Assets/"):]
    rel = rel.replace("Assets/#WitchTrials/", "#WitchTrials/")
    return os.path.join(CONTENT, rel.replace("/", os.sep))


def load_index():
    """ASSET path -> list of object rows; and (bundle, pid) -> our asset path."""
    by_asset = defaultdict(list)
    pid_to_asset = {}
    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            asset_path = (row.get("asset_path") or "").strip()
            if asset_path:
                by_asset[asset_path].append(row)
            bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
            path_id = (row.get("path_id") or "").strip()
            if bundle and path_id and (row.get("type") or "") != "MonoScript":
                pid_to_asset.setdefault((bundle.lower(), path_id), asset_path)
    return by_asset, pid_to_asset


def load_cab_index():
    """CAB (lowercase) -> bundle path, from the audit's per-source scan."""
    root = os.path.join(game_root(), "manosaba_Data")
    index = {}
    if not os.path.isdir(PER_SOURCE):
        return index
    for name in sorted(os.listdir(PER_SOURCE)):
        if not name.endswith(".json"):
            continue
        try:
            with open(os.path.join(PER_SOURCE, name), encoding="utf-8") as fh:
                data = json.load(fh)
        except (OSError, ValueError):
            continue
        rel = (data.get("rel") or "").replace("/", os.sep)
        full = os.path.join(root, rel) if rel else ""
        if not full or not os.path.exists(full):
            continue
        for cab in (data.get("object_counts_by_file") or {}):
            index.setdefault(cab.lower(), full)
    return index


def iter_ptrs(node, out, skip_keys=("m_Script",)):
    """Collect every {m_FileID, m_PathID} node in a dump.

    m_Script is skipped: a script reference is not an asset pointer, it is what identifies the
    component's type, and the binding table (a13b) already resolved it into the object's `script`
    field.  Counting it here would inflate the unresolved-pointer statistics.
    """
    if isinstance(node, dict):
        if "m_FileID" in node and "m_PathID" in node and len(node) <= 3:
            out.append((node.get("m_FileID", 0), node.get("m_PathID", 0)))
            return
        for key, value in node.items():
            if key in skip_keys:
                continue
            iter_ptrs(value, out, skip_keys)
    elif isinstance(node, list):
        for value in node:
            iter_ptrs(value, out, skip_keys)


class ExternalsCache:
    """cab -> externals list (in m_FileID order) for the bundles we touch."""

    def __init__(self):
        self._env = {}

    def externals(self, bundle_path):
        if bundle_path in self._env:
            return self._env[bundle_path]
        table = {}
        try:
            env = UnityPy.load(bundle_path)
            for obj in env.objects:
                cab = getattr(obj.assets_file, "name", "?").lower()
                if cab in table:
                    continue
                table[cab] = [getattr(e, "path", "").split("/")[-1].lower()
                              for e in getattr(obj.assets_file, "externals", [])]
        except Exception as exc:  # noqa: BLE001
            table["__error__"] = f"{type(exc).__name__}: {exc}"
        self._env[bundle_path] = table
        return table


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--group", default="pilot", help="name used in the output file name")
    ap.add_argument("--match", default="", help="only asset paths containing this substring")
    ap.add_argument("--limit", type=int, default=0)
    ap.add_argument("--ext", default=".prefab")
    args = ap.parse_args()

    by_asset, pid_to_asset = load_index()
    cab_index = load_cab_index()
    externals_cache = ExternalsCache()

    scripts = {}
    if os.path.exists(SCRIPT_MAP):
        with open(SCRIPT_MAP, encoding="utf-8") as fh:
            for key, rec in (json.load(fh).get("entries") or {}).items():
                scripts[key] = rec

    candidates = [p for p, rows in by_asset.items()
                  if p.endswith(args.ext)
                  and any((r.get("type") or "") == "GameObject" for r in rows)
                  and (args.match.lower() in p.lower() if args.match else True)]
    candidates.sort(key=lambda p: len(by_asset[p]))          # smallest first
    if args.limit:
        candidates = candidates[:args.limit]

    jobs = []
    stats = Counter()
    unresolved_samples = []
    for asset_path in candidates:
        rows = by_asset[asset_path]
        objects = []
        pid_to_dump = {}
        bundles = Counter()
        for row in rows:
            obj_type = (row.get("type") or "").strip()
            if obj_type not in GRAPH_TYPES:
                stats["skipped_type:" + obj_type] += 1
                continue
            output_file = (row.get("output_file") or "").strip()
            dump_rel = output_file.replace("\\", "/")
            dump = os.path.join(RES, dump_rel.replace("/", os.sep))
            if not dump_rel or not os.path.exists(dump):
                stats["skipped_missing_dump"] += 1
                continue
            bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
            bundles[bundle] += 1
            pid = str((row.get("path_id") or "").strip())
            # repo-relative, like the ScriptableObject plan: the Editor resolves plan paths against
            # the repository root, not against res/
            record = {"pid": pid, "type": obj_type, "dump": os.path.relpath(dump, REPO).replace("\\", "/"),
                      "name": (row.get("name") or "").strip()}
            if obj_type == "MonoBehaviour":
                hit = scripts.get(f"{bundle}|{pid}")
                if hit:
                    record["script"] = {"class": hit["class"], "namespace": hit["namespace"],
                                        "assembly": hit["assembly"], "our_guid": hit.get("our_guid", "")}
                    stats["script_bound"] += 1
                else:
                    stats["script_unbound"] += 1
                    record["script"] = None
            objects.append(record)
            pid_to_dump[pid] = os.path.relpath(dump, REPO).replace("\\", "/")

        # ---- resolve external references ------------------------------------------------
        bundle_path = None
        if bundles:
            main_bundle = bundles.most_common(1)[0][0]
            # bundles are keyed by file name; find the full path through the cab index
            for cab, path in cab_index.items():
                if os.path.basename(path) == main_bundle:
                    bundle_path = path
                    break
        externals_per_cab = externals_cache.externals(bundle_path) if bundle_path else {}
        main_cab = next((c for c in externals_per_cab if c != "__error__"), "")

        resolved_refs = {}
        unresolved = Counter()
        for record in objects:
            with open(os.path.join(REPO, record["dump"].replace("/", os.sep)), encoding="utf-8") as fh:
                try:
                    data = json.load(fh)
                except ValueError:
                    stats["bad_dump"] += 1
                    continue
            ptrs = []
            iter_ptrs(data, ptrs)
            for file_id, path_id in ptrs:
                if not path_id:
                    continue
                if file_id == 0:
                    stats["ptr_intra"] += 1
                    continue
                externals = externals_per_cab.get(main_cab, [])
                cab = externals[file_id - 1] if 0 < file_id <= len(externals) else ""
                provider = cab_index.get(cab)
                provider_bundle = os.path.basename(provider).lower() if provider else ""
                target = pid_to_asset.get((provider_bundle, str(path_id)))
                key = f"{record['pid']}:{file_id}:{path_id}"
                if target:
                    resolved_refs[key] = target
                    stats["ptr_external_resolved"] += 1
                else:
                    unresolved[f"{cab or 'fileID' + str(file_id)}"] += 1
                    stats["ptr_external_unresolved"] += 1
                    if len(unresolved_samples) < 8:
                        unresolved_samples.append({"asset": asset_path, "from": record["pid"],
                                                   "cab": cab, "pid": str(path_id)})

        asset_dir = os.path.dirname(asset_path)
        jobs.append({
            "asset_path": asset_path,
            "target": dest_for(asset_path),
            "bundle": bundles.most_common(1)[0][0] if bundles else "",
            "cab": main_cab,
            "externals": externals_per_cab.get(main_cab, []),
            "objects": objects,
            "references": resolved_refs,
            "unresolved_counts": dict(unresolved),
        })
        stats["assets"] += 1

    plan = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "group": args.group,
        "match": args.match,
        "exists": args.ext,
        "job_count": len(jobs),
        "stats": dict(stats),
        "unresolved_samples": unresolved_samples,
        "jobs": jobs,
    }
    os.makedirs(OUT_DIR, exist_ok=True)
    plan_path = os.path.join(OUT_DIR, f"prefab_plan_{args.group}.json")
    with open(plan_path, "w", encoding="utf-8") as fh:
        json.dump(plan, fh, ensure_ascii=False, indent=1)

    # Paths list for the Editor-side verifier: the G2 gate collects every *_imported_paths*.txt, so a
    # rebuilt prefab gets the same LoadMainAssetAtPath + GUID-stability check as the leaf assets.
    paths_path = os.path.join(OUT_DIR, f"prefab_imported_paths_{args.group}.txt")
    unity_paths = []
    for job in jobs:
        target = job["target"].replace("\\", "/")
        index = target.find("/Assets/")
        unity_paths.append(target[index + 1:] if index >= 0 else target)
    with open(paths_path, "w", encoding="utf-8") as fh:
        fh.write("\n".join(sorted(unity_paths)) + "\n")

    print(f"assets        : {stats['assets']}")
    print(f"objects       : {sum(len(j['objects']) for j in jobs)}")
    print(f"scripts bound : {stats['script_bound']}   unbound: {stats['script_unbound']}")
    print(f"PPtr intra    : {stats['ptr_intra']}   external resolved: {stats['ptr_external_resolved']}"
          f"   external unresolved: {stats['ptr_external_unresolved']}")
    print(f"plan          : {plan_path}")
    if unresolved_samples:
        print("  unresolved samples:")
        for s in unresolved_samples[:5]:
            print("   ", s)
    return 0


if __name__ == "__main__":
    sys.exit(main())

