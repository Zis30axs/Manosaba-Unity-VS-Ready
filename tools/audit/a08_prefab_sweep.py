#!/usr/bin/env python3
"""A08 -- hierarchy validator over EVERY prefab container in the dump.

Defects repaired relative to the previous revision (audit UR-007 / section 3.2.5).
The old code was measured to be unsound in five specific ways, each fixed here:

1. ``except Exception: objs[pid] = (type, {})`` swallowed JSON read errors and turned a
   broken object into an *empty* object, which then looked "clean".  Read failures are
   now reported per object and force the container to be non-clean.
2. ``orphan_go`` was computed and then never added to ``issues`` or to the output.
   Orphan GameObjects and orphan Transforms are now first-class issues with names.
3. The object table was keyed by ``pid`` alone, so a pointer from a different
   SerializedFile could match by accident.  Identity is now
   ``(source, serialized file, pathID)`` and every PPtr's ``m_FileID`` is honoured:
   ``m_FileID == 0`` resolves inside the container, anything else is recorded as a
   cross-file reference instead of silently matching a local object.
4. Component back-references were not verified.  Every component's ``m_GameObject``
   must point back at the GameObject that lists it; ``m_Father``/``m_Children`` are
   checked in both directions; ``m_Component`` entries that do not resolve, resolve to
   the wrong type, or appear twice are issues.
5. ``multi_root`` was counted but did not prevent ``clean``, and only 25 samples were
   kept.  ``clean`` now requires zero issues *and* exactly one root, and the full
   per-container result plus the full multi-root list are emitted.

Evidence for legitimately multi-root or legitimately empty containers is preserved in
``legal_multi_root_evidence`` instead of being flattened by a blanket "fix".

Outputs: out/prefab_sweep.json (full), out/prefab_sweep.csv (all containers),
out/prefab_issues.csv (every issue, not a sample)
"""
from __future__ import annotations

import collections
import csv
import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import identity  # noqa: E402
from common import OUT, RES_ROOT, load_index_ledger  # noqa: E402

A03_DIR = os.path.join(OUT, "a03")

#: component types that legitimately hang off a GameObject
TRANSFORM_TYPES = ("Transform", "RectTransform")


def load_container_targets():
    """container path -> list of {source, target pathID, preloadIndex} from a03.

    Used to decide whether a multi-root container is genuinely multi-root or whether
    the dump lost the real root (section 3.2.5 / 5.6).
    """
    index_path = os.path.join(A03_DIR, "index.json")
    targets = collections.defaultdict(list)
    if not os.path.exists(index_path):
        return targets, False
    with open(index_path, encoding="utf-8") as fh:
        manifest = json.load(fh)
    # The full container detail lives in the per-source scan results; read them lazily
    # by re-running only the AssetBundle part is expensive, so reuse bundles.json.
    bundles_path = os.path.join(OUT, "bundles.json")
    if not os.path.exists(bundles_path):
        return targets, False
    with open(bundles_path, encoding="utf-8") as fh:
        bundles = json.load(fh)
    for b in bundles:
        for c in b.get("containers", []):
            targets[c["key"]].append({
                "source": b["source"],
                "cab": b.get("name"),
                "target_path_id": c["target"]["m_PathID"],
                "target_file_id": c["target"]["m_FileID"],
                "preloadIndex": c.get("preloadIndex"),
                "preloadSize": c.get("preloadSize"),
            })
    return targets, True


def load_container_objects(rows):
    """Read every dumped JSON object of one container, keeping identity and file identity.

    Only ``.json`` dumps are parsed.  A container also lists rows whose ``output_file`` is
    the *binary payload* the ripper wrote (``.png``, ``.ogg``, ``.txt``, ``.otf``, ...).
    Parsing those as JSON produced 1,797 bogus ``object_read_error`` entries the first time
    this validator ran; they are payloads, not object dumps, and are surfaced separately.
    """
    objects = {}
    read_errors = []
    payload_rows = []
    for r in rows:
        output_file = r["output_file"]
        if not output_file.lower().endswith(".json"):
            payload_rows.append({"path_id": str(r["pid_int"]), "type": r["type"],
                                 "output_file": output_file,
                                 "note": "streamed payload, not a JSON object dump"})
            continue
        full = r.get("dump_path") or os.path.join(RES_ROOT, output_file.replace("/", os.sep))
        if not os.path.exists(full):
            read_errors.append({"path_id": str(r["pid_int"]), "type": r["type"],
                                "output_file": output_file, "error": "dump file missing"})
            continue
        try:
            with open(full, encoding="utf-8") as fh:
                doc = json.load(fh)
        except (OSError, ValueError, UnicodeError) as exc:
            read_errors.append({"path_id": str(r["pid_int"]), "type": r["type"],
                                "output_file": output_file,
                                "error": f"{type(exc).__name__}: {exc}"})
            continue
        if not isinstance(doc, dict):
            read_errors.append({"path_id": str(r["pid_int"]), "type": r["type"],
                                "output_file": output_file,
                                "error": f"unexpected JSON root type {type(doc).__name__}"})
            continue
        key = (r["source_identity"], r["pid_int"])
        if key in objects:
            read_errors.append({"path_id": str(r["pid_int"]), "type": r["type"],
                                "output_file": output_file,
                                "error": "duplicate identity in container"})
            continue
        objects[key] = {"type": r["type"], "doc": doc, "row": r}
    return objects, read_errors, payload_rows


class Container:
    """Object table for one prefab container, with an O(1) pathID index.

    ``pid_index`` maps a local pathID to the set of keys that declare it.  A prefab
    container can hold several SerializedFiles, so the same pathID may legitimately
    appear more than once; ambiguity is reported rather than silently resolved to
    whichever object happened to be inserted first.
    """

    __slots__ = ("objects", "pid_index", "ambiguous")

    def __init__(self, objects):
        self.objects = objects
        self.pid_index = collections.defaultdict(list)
        for key in objects:
            self.pid_index[key[1]].append(key)
        self.ambiguous = {pid: keys for pid, keys in self.pid_index.items() if len(keys) > 1}

    def by_pid(self, pid):
        keys = self.pid_index.get(pid) or []
        if not keys:
            return None
        return keys[0]

    def is_ambiguous(self, pid):
        return len(self.pid_index.get(pid) or []) > 1


def resolve_local(table, ptr, issues, ctx, what):
    """Resolve a PPtr inside the container; return (key, entry) or (None, None).

    ``m_FileID != 0`` means the pointer leaves the container's own SerializedFile.
    The old validator ignored that and could silently bind to an unrelated local object.
    """
    objects = table.objects
    if not isinstance(ptr, dict):
        issues.append({"kind": "malformed_pointer", "where": ctx, "what": what,
                       "value": repr(ptr)[:120]})
        return None, None
    fid = ptr.get("m_FileID")
    pid = ptr.get("m_PathID")
    if not isinstance(fid, int) or not isinstance(pid, int):
        issues.append({"kind": "malformed_pointer", "where": ctx, "what": what,
                       "value": json.dumps(ptr)[:120]})
        return None, None
    if fid != 0:
        issues.append({"kind": "cross_file_reference", "where": ctx, "what": what,
                       "m_FileID": fid, "m_PathID": str(pid)})
        return None, None
    if pid == 0:
        return None, None  # legitimate null reference
    key = table.by_pid(pid)
    if key is None:
        issues.append({"kind": "unresolved_local_reference", "where": ctx, "what": what,
                       "m_PathID": str(pid)})
        return None, None
    if table.is_ambiguous(pid):
        issues.append({"kind": "ambiguous_local_reference", "where": ctx, "what": what,
                       "m_PathID": str(pid),
                       "candidates": [str(k[1]) for k in table.pid_index[pid]]})
    return key, objects[key]


def validate_container(container_path, rows, targets, have_targets):
    issues = []
    objects, read_errors, payload_rows = load_container_objects(rows)
    for err in read_errors:
        issues.append({"kind": "object_read_error", **err})

    table = Container(objects)
    gos = {k: v for k, v in objects.items() if v["type"] == "GameObject"}
    trs = {k: v for k, v in objects.items() if v["type"] in TRANSFORM_TYPES}

    # ---- duplicate names -------------------------------------------------
    name_counts = collections.Counter()
    for k, v in objects.items():
        nm = v["doc"].get("m_Name")
        if isinstance(nm, str) and nm:
            name_counts[nm] += 1

    # ---- GameObject -> components ----------------------------------------
    comp_owner = {}          # component key -> owning GameObject key
    for gk, gv in gos.items():
        comps = gv["doc"].get("m_Component")
        if comps is None:
            issues.append({"kind": "gameobject_without_component_list", "where": gk[1],
                           "name": gv["doc"].get("m_Name")})
            continue
        if not isinstance(comps, list):
            issues.append({"kind": "component_list_not_array", "where": gk[1],
                           "value": repr(comps)[:80]})
            continue
        if not comps:
            issues.append({"kind": "gameobject_with_empty_component_list", "where": gk[1],
                           "name": gv["doc"].get("m_Name")})
            continue
        seen_comps = set()
        for i, entry in enumerate(comps):
            ptr = entry.get("component") if isinstance(entry, dict) else None
            ck, cv = resolve_local(table, ptr, issues, f"GameObject#{gk[1]}",
                                   f"m_Component[{i}]")
            if ck is None:
                issues.append({"kind": "missing_component", "where": gk[1],
                               "name": gv["doc"].get("m_Name"), "index": i,
                               "target": json.dumps(ptr)[:120] if ptr else None})
                continue
            if ck in seen_comps:
                issues.append({"kind": "duplicate_component_entry", "where": gk[1],
                               "component_path_id": str(ck[1]), "index": i})
                continue
            seen_comps.add(ck)
            # back-reference check (defect 4)
            back = cv["doc"].get("m_GameObject")
            bk, _ = resolve_local(table, back, [], f"component#{ck[1]}", "m_GameObject")
            if bk is None:
                issues.append({"kind": "component_missing_gameobject_backref",
                               "where": ck[1], "component_type": cv["type"],
                               "expected_owner": str(gk[1]),
                               "backref": json.dumps(back)[:120] if back else None})
            elif bk != gk:
                issues.append({"kind": "component_backref_wrong_owner", "where": ck[1],
                               "component_type": cv["type"],
                               "expected_owner": str(gk[1]), "actual_owner": str(bk[1])})
            comp_owner[ck] = gk

    # ---- components that no GameObject claims (orphan components) ---------
    # A prefab container legitimately holds *sub-assets* next to its GameObjects:
    # Material, Shader, AnimationClip, AnimatorController, Mesh, Texture2D, AudioClip ...
    # Those are referenced by components, not by a GameObject, so they must NOT be judged
    # as orphan components -- doing so produced 769 false positives on the first run.
    #
    # A real Component is an object that has an m_GameObject field pointing at a
    # GameObject.  It is orphaned only when that GameObject does not list it back.
    for k, v in objects.items():
        if v["type"] == "GameObject":
            continue
        if k in comp_owner:
            continue
        if "m_GameObject" not in v["doc"]:
            continue  # a sub-asset, not a Component
        issues.append({"kind": "orphan_component", "where": k[1],
                       "component_type": v["type"],
                       "declared_owner": json.dumps(v["doc"].get("m_GameObject"))[:120]})

    # ---- Transform tree --------------------------------------------------
    def father_of(v):
        return v["doc"].get("m_Father")

    # Classify every Transform by its parenting, not by what a walk happens to reach.
    # A dangling m_Father (non-zero pointer that resolves to nothing) is a defect, and
    # such a Transform must NOT be promoted to a root -- if it were, a detached subtree
    # would look like a legitimate second root and its members would look reachable.
    roots = []
    unparented = []          # m_Father points somewhere that does not exist
    parented = {}            # transform key -> father key
    for tk, tv in trs.items():
        father_ptr = father_of(tv)
        fk, _ = resolve_local(table, father_ptr, [], f"Transform#{tk[1]}", "m_Father")
        if fk is not None:
            parented[tk] = fk
            continue
        pid = father_ptr.get("m_PathID") if isinstance(father_ptr, dict) else None
        fid = father_ptr.get("m_FileID") if isinstance(father_ptr, dict) else None
        if pid in (None, 0) and fid in (None, 0):
            roots.append(tk)          # genuine root: m_Father == null
        else:
            unparented.append(tk)
            issues.append({"kind": "dangling_father", "where": tk[1],
                           "m_Father": json.dumps(father_ptr)[:120]})
    for tk in unparented:
        issues.append({"kind": "transform_without_reachable_root", "where": tk[1],
                       "name": trs[tk]["doc"].get("m_Name")})

    # A Transform is attached only when its whole ancestry terminates at a real root.
    # Cycles must fail this test, so it walks with an explicit visited set instead of
    # relying on recursion depth or insertion order.
    attached = set()

    def ancestor_chain_ok(tk):
        seen = set()
        cur = tk
        while cur is not None:
            if cur in seen:
                return False          # cycle on the father chain
            seen.add(cur)
            cur = parented.get(cur)
        return True

    reachable = set()

    def walk(tk, stack):
        if tk in stack:
            issues.append({"kind": "cycle", "where": tk[1],
                           "chain": [s[1] for s in stack[stack.index(tk):]] + [tk[1]]})
            return
        if tk in reachable:
            return
        reachable.add(tk)
        tv = trs[tk]
        # Transform -> GameObject
        gk, gv = resolve_local(table, tv["doc"].get("m_GameObject"), issues,
                               f"Transform#{tk[1]}", "m_GameObject")
        if gk is None:
            issues.append({"kind": "transform_without_gameobject", "where": tk[1]})
        else:
            if gv["type"] != "GameObject":
                issues.append({"kind": "transform_points_to_non_gameobject", "where": tk[1],
                               "target_type": gv["type"]})
            tf_owner = comp_owner.get(tk)
            if tf_owner is not None and tf_owner != gk:
                issues.append({"kind": "transform_gameobject_disagreement", "where": tk[1],
                               "transform_says": str(gk[1]), "component_says": str(tf_owner[1])})
        # children
        kids = tv["doc"].get("m_Children")
        if kids is None:
            issues.append({"kind": "transform_without_children_field", "where": tk[1]})
            kids = []
        elif not isinstance(kids, list):
            issues.append({"kind": "children_not_array", "where": tk[1]})
            kids = []
        for i, child_ptr in enumerate(kids):
            ck, cv = resolve_local(table, child_ptr, issues, f"Transform#{tk[1]}",
                                   f"m_Children[{i}]")
            if ck is None:
                issues.append({"kind": "missing_child_transform", "where": tk[1], "index": i,
                               "target": json.dumps(child_ptr)[:120]})
                continue
            if cv["type"] not in TRANSFORM_TYPES:
                issues.append({"kind": "child_is_not_transform", "where": tk[1],
                               "child_type": cv["type"], "child_path_id": str(ck[1])})
                continue
            bfk, _ = resolve_local(table, father_of(cv), [], f"Transform#{ck[1]}", "m_Father")
            if bfk != tk:
                issues.append({"kind": "father_mismatch", "where": tk[1], "child": str(ck[1]),
                               "child_father": str(bfk[1]) if bfk else None})
            walk(ck, stack + [tk])
        if ancestor_chain_ok(tk):
            attached.add(tk)
    for r in roots:
        walk(r, [])

    orphan_transforms = [k for k in trs if k not in attached]
    for k in orphan_transforms:
        issues.append({"kind": "orphan_transform", "where": k[1],
                       "name": trs[k]["doc"].get("m_Name")})

    # orphan GameObjects (defect 2 -- the old code computed this set and never used it).
    #
    # Being named by *some* Transform is not enough: a GameObject whose Transform sits in
    # a detached subtree is just as unreachable as one that nobody names.  A GameObject
    # counts as attached only when it (a) is named by a Transform and (b) that Transform
    # was reached by the walk from a real root.
    named_by = {}
    for tk, tv in trs.items():
        gk, _ = resolve_local(table, tv["doc"].get("m_GameObject"), [], f"T#{tk[1]}", "go")
        if gk is not None:
            named_by.setdefault(gk, []).append(tk)
    detached_gos = []
    for gk, gv in gos.items():
        owners = named_by.get(gk, [])
        if not owners:
            reason = "no Transform names this GameObject"
        elif not any(o in attached for o in owners):
            reason = "named only by Transforms whose ancestry never reaches a root"
        else:
            continue
        detached_gos.append((gk, {"path_id": str(gk[1]), "name": gv["doc"].get("m_Name"),
                                  "reason": reason,
                                  "transform_owners": [str(o[1]) for o in owners]}))
    orphan_gos = [k for k, _ in detached_gos]
    for _, entry in detached_gos:
        issues.append({"kind": "orphan_gameobject", **entry})

    # ---- multi-root judgement using the container target -----------------
    # The AssetBundle container target for a prefab is normally a *GameObject*, not a
    # Transform, so "target is not one of the root Transforms" is expected and proves
    # nothing.  The contradiction that does matter is a target that resolves to no object
    # in the container at all, or several targets that disagree.
    #
    # Missing evidence is recorded as an evidence gap in the result, not as a per-container
    # defect -- otherwise a source that has not been scanned yet would make every prefab
    # look broken.
    multi_root_evidence = None
    evidence_gap = None
    if len(roots) > 1:
        cands = targets.get(container_path, [])
        root_ids = {str(r[1]) for r in roots}
        multi_root_evidence = {"root_path_ids": sorted(root_ids),
                               "container_targets": cands}
        tgt_ids = {c["target_path_id"] for c in cands}
        if not cands:
            evidence_gap = "no AssetBundle container target recorded for this prefab path"
        elif len(tgt_ids) > 1:
            issues.append({"kind": "multi_root_container_multi_target",
                           "targets": sorted(tgt_ids), "roots": sorted(root_ids)})
        else:
            only = list(tgt_ids)[0]
            # The target must at least name something that exists in this container.
            if not table.is_ambiguous(only) and table.by_pid(int(only)) is None:
                issues.append({"kind": "multi_root_target_not_in_container",
                               "target": only, "objects": len(objects),
                               "roots": sorted(root_ids)})

    dup_names = {n: c for n, c in name_counts.items() if c > 1}
    result = {
        "container": container_path,
        "objects": len(objects),
        "gameobjects": len(gos),
        "transforms": len(trs),
        "components": len(objects) - len(gos) - len(trs),
        "roots": len(roots),
        "root_path_ids": [str(r[1]) for r in roots],
        "reachable_transforms": len(reachable),
        "orphan_transforms": [str(k[1]) for k in orphan_transforms],
        "orphan_gameobjects": [str(k[1]) for k in orphan_gos],
        "duplicate_names": dup_names,
        "read_errors": read_errors,
        "payload_rows": len(payload_rows),
        "multi_root_evidence": multi_root_evidence,
        "evidence_gap": evidence_gap,
        "issue_count": len(issues),
        "issues": issues,
    }
    result["clean"] = (not issues) and len(roots) == 1
    return result


def main():
    ledger, _ = load_index_ledger()
    by_container = collections.defaultdict(list)
    for r in ledger.rows:
        if r["asset_path"].endswith(".prefab"):
            by_container[r["asset_path"]].append(r)

    targets, have_targets = load_container_targets()
    print(f"prefab containers: {len(by_container)}  container-target evidence: {have_targets}",
          flush=True)

    results = []
    agg = collections.Counter()
    for i, (cont, rows) in enumerate(sorted(by_container.items())):
        res = validate_container(cont, rows, targets, have_targets)
        res["sources"] = sorted({r["source_identity"] for r in rows})
        results.append(res)
        if res["clean"]:
            agg["clean"] += 1
        else:
            agg["not_clean"] += 1
        for issue in res["issues"]:
            agg["issue_" + issue["kind"]] += 1
        if res["roots"] > 1:
            agg["multi_root"] += 1
        if res["read_errors"]:
            agg["with_read_errors"] += 1
        if (i + 1) % 25 == 0:
            print(f"  [{i+1}/{len(by_container)}] clean={agg['clean']} "
                  f"not_clean={agg['not_clean']}", flush=True)

    multi_root = [r for r in results if r["roots"] > 1]
    with open(os.path.join(OUT, "prefab_sweep.json"), "w", encoding="utf-8") as fh:
        json.dump({
            "prefabs": len(results),
            "containers_with_target_evidence": have_targets,
            "aggregate": dict(agg),
            "definition_of_clean": "zero issues AND exactly one root transform",
            "multi_root_containers": multi_root,
            "results": results,
        }, fh, ensure_ascii=False, indent=1)

    with open(os.path.join(OUT, "prefab_sweep.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["container", "objects", "gameobjects", "transforms", "components",
                    "roots", "reachable", "orphan_transforms", "orphan_gameobjects",
                    "read_errors", "issue_count", "clean", "sources"])
        for r in results:
            w.writerow([r["container"], r["objects"], r["gameobjects"], r["transforms"],
                        r["components"], r["roots"], r["reachable_transforms"],
                        len(r["orphan_transforms"]), len(r["orphan_gameobjects"]),
                        len(r["read_errors"]), r["issue_count"], r["clean"],
                        ";".join(r["sources"])])

    with open(os.path.join(OUT, "prefab_issues.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.writer(fh)
        w.writerow(["container", "kind", "where", "details"])
        for r in results:
            for issue in r["issues"]:
                details = {k: v for k, v in issue.items() if k not in ("kind", "where")}
                w.writerow([r["container"], issue["kind"], issue.get("where", ""),
                            json.dumps(details, ensure_ascii=False)])

    print(json.dumps({"prefabs": len(results), "aggregate": dict(agg)}, ensure_ascii=False, indent=1))
    print("multi-root containers:", len(multi_root))
    for r in multi_root[:10]:
        print("   ", r["container"], "roots=", r["roots"], "issues=", r["issue_count"])
    return 0


if __name__ == "__main__":
    sys.exit(main())
