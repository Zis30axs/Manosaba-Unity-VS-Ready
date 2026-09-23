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
import heapq
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
    # The layered-character prefabs' render cameras (LayeredActorBehaviour.renderCamera: shipped clear Nothing,
    # orthographic, culling mask 0 - Naninovel disables them and renders into a texture) and the court stage's
    # lights.  Skipping them left renderCamera unbound and a default camera (Skybox, perspective, all layers)
    # drawing every frame over the main camera in story scenes.
    "Camera", "Light",
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


# (bundle, pid) -> object type, from the same index: classifies the intra-bundle pointers nothing resolves
PID_TYPE = {}


def load_index():
    """ASSET path -> list of object rows; and (bundle, pid) -> our asset path."""
    by_asset = defaultdict(list)
    pid_to_asset = {}
    global PID_TYPE
    with open(INDEX, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            asset_path = (row.get("asset_path") or "").strip()
            if asset_path:
                by_asset[asset_path].append(row)
            bundle = os.path.basename((row.get("source_bundle") or "").replace("\\", "/"))
            path_id = (row.get("path_id") or "").strip()
            if bundle and path_id and (row.get("type") or "") != "MonoScript":
                pid_to_asset.setdefault((bundle.lower(), path_id), asset_path)
            if bundle and path_id:
                PID_TYPE.setdefault((bundle.lower(), path_id), (row.get("type") or "").strip())
    return by_asset, pid_to_asset


LEDGER = os.path.join(OUT_DIR, "a13_import_ledger.csv")
EMBEDDED_MATERIALS = os.path.join(OUT_DIR, "material_embedded_pointer_map.json")


def load_ledger():
    """(bundle, pid) -> project file (Assets/...) of an object the importer already wrote out as a standalone asset.

    A prefab dump points at Sprites and Texture2Ds that live in the SAME bundle as the prefab (GalleryUI.prefab ->
    its own SlotLocked / SlotLoading textures, every LayeredCharacters prefab -> its 300+ sprites).  Those objects are
    not part of the object graph the builder recreates, so an intra-file pointer to them had nowhere to go; the
    import ledger (a13_import_ledger.csv, status IMPORTED) already says which file each one became."""
    sidecar = {}
    if not os.path.exists(LEDGER):
        return sidecar
    with open(LEDGER, encoding="utf-8-sig", newline="") as fh:
        for row in csv.DictReader(fh):
            if (row.get("status") or "") != "IMPORTED":
                continue
            if (row.get("type") or "") not in ("Sprite", "Texture2D", "Material", "AudioClip", "TextAsset"):
                continue
            target = (row.get("target_path") or "").replace("\\", "/")
            marker = "UnityProject/Assets/"
            if marker not in target:
                continue
            bundle = os.path.basename((row.get("bundle") or "").replace("\\", "/")).lower()
            pid = (row.get("path_id") or "").strip()
            if bundle and pid:
                sidecar.setdefault((bundle, pid), "Assets/" + target.split(marker, 1)[1])
    # Materials the bundles embed (the layered characters' Naninovel_* layer materials): no importer writes them,
    # plan_embedded_materials.py plans one project material per distinct shipped copy and MaterialImporter creates
    # it; the pointer map says which project material each (bundle, path_id) copy became.
    if os.path.exists(EMBEDDED_MATERIALS):
        with open(EMBEDDED_MATERIALS, encoding="utf-8") as fh:
            for key, target in (json.load(fh).get("pointers") or {}).items():
                bundle, _, pid = key.rpartition("|")
                if bundle and pid:
                    sidecar.setdefault((bundle.lower(), pid), target)
    return sidecar


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


def dump_json(row):
    """The row's structural dump, or None when it is missing or unreadable."""
    rel = (row.get("output_file") or "").strip().replace("\\", "/")
    path = os.path.join(RES, rel.replace("/", os.sep))
    if not rel or not os.path.exists(path):
        return None
    try:
        with open(path, encoding="utf-8") as fh:
            return json.load(fh)
    except ValueError:
        return None


def row_bundle(row):
    return os.path.basename((row.get("source_bundle") or "").replace("\\", "/")).lower()


def pptr_pid(node):
    """m_PathID of an intra-file pointer ({m_FileID: 0, m_PathID: x}) as a string, else ""."""
    if isinstance(node, dict) and not node.get("m_FileID") and node.get("m_PathID"):
        return str(node["m_PathID"])
    return ""


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--group", default="pilot", help="name used in the output file name")
    ap.add_argument("--match", default="", help="only asset paths containing this substring")
    ap.add_argument("--limit", type=int, default=0)
    ap.add_argument("--ext", default=".prefab")
    args = ap.parse_args()

    by_asset, pid_to_asset = load_index()
    cab_index = load_cab_index()
    ledger = load_ledger()
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

    # ---- component ownership -------------------------------------------------------------
    # The index attributes an object to the first container that reached it.  A component on the root of ANOTHER
    # prefab of the same bundle is therefore listed under the prefab that references it: DebugLabel.prefab's
    # TextMeshProUGUI and ProvidableDialog.prefab's ProvidableDialog under Game.prefab, RebuildableToggleLabel.prefab's
    # RebuildableLabel under RebuildableToggle.prefab.  The owner was then built without its component and the
    # referrer could not create it ("component without owner GameObject", 3 in the 2026-09-21 build).  A component
    # belongs to the prefab that holds its m_GameObject.
    candidate_set = set(candidates)
    go_owner = {}
    for asset_path in candidates:
        for row in by_asset[asset_path]:
            if (row.get("type") or "") == "GameObject":
                go_owner.setdefault((row_bundle(row), (row.get("path_id") or "").strip()), asset_path)
    moves = []
    rehomed = {}            # (bundle, pid) -> owner asset path
    for asset_path in candidates:
        for row in by_asset[asset_path]:
            obj_type = (row.get("type") or "").strip()
            if obj_type not in GRAPH_TYPES or obj_type == "GameObject":
                continue
            data = dump_json(row)
            go_pid = pptr_pid(data.get("m_GameObject")) if isinstance(data, dict) else ""
            owner = go_owner.get((row_bundle(row), go_pid)) if go_pid else None
            if owner and owner != asset_path:
                moves.append((asset_path, owner, row))
    for source, owner, row in moves:
        by_asset[source].remove(row)
        by_asset[owner].append(row)
        rehomed[(row_bundle(row), (row.get("path_id") or "").strip())] = owner
        stats["component_rehomed"] += 1
    rehomed_samples = [{"from": s, "to": o, "pid": (r.get("path_id") or "").strip(), "type": r.get("type")}
                       for s, o, r in moves[:12]]

    root_cache = {}
    row_at = {}             # (bundle, pid) -> row, for every candidate row (after re-homing)
    for asset_path in candidates:
        for row in by_asset[asset_path]:
            row_at[(row_bundle(row), (row.get("path_id") or "").strip())] = row
    component_cache = {}

    def is_component(where):
        """A GameObject-bound object (has m_GameObject), as opposed to a Material, a TimelineAsset or a track that
        the index merely attributed to some other prefab because that prefab reached it first."""
        if where not in component_cache:
            row = row_at.get(where)
            data = dump_json(row) if row and (row.get("type") or "") != "GameObject" else None
            component_cache[where] = bool(isinstance(data, dict) and pptr_pid(data.get("m_GameObject")))
        return component_cache[where]

    def single_root(asset_path):
        """The one root GameObject pid of a candidate prefab (Transform with m_Father 0), or "" if not exactly one."""
        if asset_path not in root_cache:
            roots = []
            for row in by_asset[asset_path]:
                if (row.get("type") or "") in ("Transform", "RectTransform"):
                    data = dump_json(row)
                    if isinstance(data, dict) and not pptr_pid(data.get("m_Father")):
                        roots.append(pptr_pid(data.get("m_GameObject")))
            root_cache[asset_path] = roots[0] if len(roots) == 1 else ""
        return root_cache[asset_path]

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
        intra_unresolved = Counter()
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
                    if str(path_id) not in pid_to_dump:
                        sidecar = ledger.get((bundle.lower(), str(path_id))) if bundles else None
                        key = f"{record['pid']}:0:{path_id}"
                        # Another prefab of the same bundle (a template field: DialogPresenter._providableDialogPrefab,
                        # RebuildableToggle._labelPrefab): the reference names that prefab, and the applier takes the
                        # component of the field's type on its root (or the only one in it).  A GameObject target is
                        # only accepted when it is that prefab's single root, the object loading the prefab returns.
                        other = None
                        if not sidecar and bundles:
                            where = (bundle.lower(), str(path_id))
                            other = rehomed.get(where) or pid_to_asset.get(where)
                            if other == asset_path or other not in candidate_set:
                                other = None
                            elif PID_TYPE.get(where) == "GameObject":
                                if single_root(other) != str(path_id):
                                    stats["ptr_intra_cross_prefab_non_root_gameobject"] += 1
                                    other = None
                            elif not is_component(where):
                                other = None      # a shared asset listed under another prefab: stays "not in plan"
                        if sidecar:
                            resolved_refs[key] = sidecar
                            stats["ptr_intra_sidecar_resolved"] += 1
                        elif other:
                            resolved_refs[key] = other
                            stats["ptr_intra_cross_prefab"] += 1
                        else:
                            stats["ptr_intra_not_in_plan"] += 1
                            target_type = PID_TYPE.get((bundle.lower(), str(path_id)), "?") if bundles else "?"
                            stats["ptr_intra_not_in_plan:" + (target_type or "?")] += 1
                            intra_unresolved[target_type or "?"] += 1
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
            "intra_not_in_plan_by_type": dict(intra_unresolved),
        })
        stats["assets"] += 1

    # ---- build order ------------------------------------------------------------------------
    # PrefabBuilder deletes and re-saves each prefab, which gives its objects new local file IDs.  A prefab that
    # points into another prefab of this plan must therefore be built AFTER it, or it keeps the target's old ID:
    # smallest-first built AdvChoicePanel (job 32) before ChoiceButton_Adv@Bad (job 38) and left a dangling
    # {fileID: 9072052669164707076} on 2026-09-21.  Topological order, smallest-first among the ready jobs.
    position = {j["asset_path"]: i for i, j in enumerate(jobs)}
    depends = {j["asset_path"]: {v for v in j["references"].values() if v in position and v != j["asset_path"]}
               for j in jobs}
    users = defaultdict(list)
    for a, deps in depends.items():
        for b in deps:
            users[b].append(a)
    pending = {a: len(deps) for a, deps in depends.items()}
    ready = [(position[a], a) for a, n in pending.items() if n == 0]
    heapq.heapify(ready)
    ordered = []
    while ready:
        _, a = heapq.heappop(ready)
        ordered.append(a)
        for u in users[a]:
            pending[u] -= 1
            if pending[u] == 0:
                heapq.heappush(ready, (position[u], u))
    cyclic = [j["asset_path"] for j in jobs if pending[j["asset_path"]] > 0]
    stats["order_cycle_jobs"] = len(cyclic)
    stats["order_prefab_dependencies"] = sum(len(d) for d in depends.values())
    by_path = {j["asset_path"]: j for j in jobs}
    jobs = [by_path[a] for a in ordered + cyclic]
    for j in jobs:
        j["depends_on"] = sorted(depends[j["asset_path"]])
    stats["order_changed_jobs"] = sum(1 for i, j in enumerate(jobs) if position[j["asset_path"]] != i)

    plan = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "group": args.group,
        "match": args.match,
        "exists": args.ext,
        "job_count": len(jobs),
        "stats": dict(stats),
        "unresolved_samples": unresolved_samples,
        "rehomed_samples": rehomed_samples,
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

