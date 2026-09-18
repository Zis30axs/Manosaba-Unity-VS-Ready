"""Index the reconstructed MonoScripts: (assembly, namespace, class) -> Unity GUID.

Composite converters (ScriptableObject, MonoBehaviour, prefab) need to rewrite every
`m_Script: {fileID: 11500000, guid: <original>}` reference onto *our* script assets.  The shipped
build does not carry the original .meta GUIDs, but it does carry, for every MonoScript object, its
class/namespace/assembly (`res/_serialized/Uncategorized/MonoScript/*.json` and
`res/_monoscripts.csv`), and a MonoBehaviour points at a MonoScript by (bundle, path_id).  So the
binding is:  reference -> MonoScript row -> (class, namespace, assembly) -> this index -> our GUID.

This tool builds that index from the laid-out project (Assets/Scripts/Game/<Asm>/...) and reports
coverage against the 2,870 MonoScripts the shipped build actually contains.

Outputs
  tools/unity-rebuild/out/unity/reconstructed_script_index.json
  tools/unity-rebuild/out/unity/monoscript_coverage.csv
"""
from __future__ import annotations

import csv
import datetime
import json
import os
import re
import sys
from collections import defaultdict

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
SCRIPTS = os.path.join(REPO, "UnityProject", "Assets", "Scripts")
EDITOR_SCRIPTS = os.path.join(REPO, "UnityProject", "Assets", "Editor")
MONOSCRIPTS_CSV = os.path.join(REPO, "res", "_monoscripts.csv")
OUT_DIR = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
INDEX_OUT = os.path.join(OUT_DIR, "reconstructed_script_index.json")
COVERAGE_OUT = os.path.join(OUT_DIR, "monoscript_coverage.csv")

NAMESPACE_RE = re.compile(r"^\s*namespace\s+([A-Za-z_][\w.]*)", re.M)
TYPE_RE = re.compile(
    r"^\s*(?:\[[^\]]*\]\s*)*(?:public|internal|private|protected)?\s*"
    r"(?:static\s+|sealed\s+|abstract\s+|partial\s+|readonly\s+|unsafe\s+)*"
    r"(class|struct|interface|enum|record)\s+([A-Za-z_]\w*)", re.M)
GUID_RE = re.compile(r"^guid:\s*([0-9a-fA-F]{32})\s*$", re.M)

#: The shipped assembly names that this project had to rename (see apply_game_sources.py):
#: an assembly definition cannot reuse Unity's predefined assembly name.
ALIASES = {"Assembly-CSharp": "WitchTrials.Game"}


def read_meta_guid(cs_path):
    meta = cs_path + ".meta"
    if not os.path.exists(meta):
        return None
    with open(meta, encoding="utf-8", errors="replace") as fh:
        m = GUID_RE.search(fh.read())
    return m.group(1) if m else None


def assembly_of(rel_parts, default):
    """Assembly directory name for a script under the given root.

    Assets/Scripts/Game/<Assembly>/<Namespace>/<Type>.cs -> <Assembly>
    Assets/Scripts/Runtime/<...>                        -> Manosaba.Integration
    """
    if len(rel_parts) >= 2 and rel_parts[-1].endswith(".cs"):
        candidate = rel_parts[0]
        if candidate not in ("Runtime", "Editor") and "." in candidate:
            return ALIASES.get(candidate, candidate)
    return default


def scan_scripts(root, default_assembly):
    entries = []
    for dirpath, _dirs, files in os.walk(root):
        for name in files:
            if not name.endswith(".cs"):
                continue
            path = os.path.join(dirpath, name)
            with open(path, encoding="utf-8", errors="replace") as fh:
                text = fh.read().lstrip("\ufeff")
            rel = os.path.relpath(path, root).replace("\\", "/")
            assembly = assembly_of(rel.split("/"), default_assembly)
            ns_match = NAMESPACE_RE.search(text)
            namespace = ns_match.group(1) if ns_match else ""
            types = TYPE_RE.findall(text)
            if not types:
                types = [("class", os.path.splitext(name)[0])]
            entries.append({
                "asset_path": os.path.relpath(path, REPO).replace("\\", "/")
                              .replace("UnityProject/", ""),
                "assembly": assembly,
                "namespace": namespace,
                "file_type": os.path.splitext(name)[0],
                "types": [{"kind": k, "name": t} for k, t in types],
                "guid": read_meta_guid(path),
            })
    return entries


def main():
    os.makedirs(OUT_DIR, exist_ok=True)
    entries = scan_scripts(os.path.join(SCRIPTS, "Game"), "WitchTrials.Game")
    entries += scan_scripts(os.path.join(SCRIPTS, "Runtime"), "Manosaba.Integration")
    entries += scan_scripts(EDITOR_SCRIPTS, "Assembly-CSharp-Editor")

    by_key = defaultdict(list)   # (assembly, namespace, class) -> entries
    by_class = defaultdict(list)  # class -> entries
    for e in entries:
        for t in e["types"]:
            by_key[(e["assembly"], e["namespace"], t["name"])].append(
                {"asset_path": e["asset_path"], "guid": e["guid"], "kind": t["kind"]})
            by_class[t["name"]].append(
                {"asset_path": e["asset_path"], "guid": e["guid"], "assembly": e["assembly"],
                 "namespace": e["namespace"]})

    index = {
        "generated": datetime.datetime.now(datetime.timezone.utc).isoformat(),
        "scripts_scanned": len(entries),
        "with_guid": sum(1 for e in entries if e["guid"]),
        "types_indexed": sum(len(e["types"]) for e in entries),
        "entries": entries,
    }
    with open(INDEX_OUT, "w", encoding="utf-8") as fh:
        json.dump(index, fh, ensure_ascii=False, indent=1)

    # ---- coverage against the shipped MonoScripts -------------------------------------
    coverage = []
    counts = defaultdict(int)
    per_assembly = defaultdict(lambda: defaultdict(int))
    if os.path.exists(MONOSCRIPTS_CSV):
        with open(MONOSCRIPTS_CSV, encoding="utf-8-sig", newline="") as fh:
            for row in csv.DictReader(fh):
                cls = (row.get("class") or "").strip()
                ns = (row.get("namespace") or "").strip()
                asm = (row.get("assembly") or "").strip()
                our_asm = ALIASES.get(asm, asm)
                exact = by_key.get((our_asm, ns, cls), [])
                loose = by_class.get(cls, [])
                if exact:
                    status, target = "EXACT", exact[0]["asset_path"]
                elif loose:
                    status, target = "LOOSE_NAME_MATCH", loose[0]["asset_path"]
                else:
                    status, target = "NOT_IN_RECONSTRUCTION", ""
                counts[status] += 1
                per_assembly[asm][status] += 1
                coverage.append({"class": cls, "namespace": ns, "assembly": asm,
                                 "path_id": row.get("path_id", ""), "status": status,
                                 "target": target, "candidates": len(loose)})

    with open(COVERAGE_OUT, "w", encoding="utf-8", newline="") as fh:
        writer = csv.DictWriter(fh, fieldnames=["class", "namespace", "assembly", "path_id",
                                                "status", "target", "candidates"])
        writer.writeheader()
        writer.writerows(coverage)

    print(f"scripts scanned : {index['scripts_scanned']} (with .meta guid: {index['with_guid']})")
    print(f"types indexed   : {index['types_indexed']}")
    print(f"shipped MonoScripts: {len(coverage)}  coverage: {dict(sorted(counts.items()))}")
    print("  per shipped assembly (EXACT / LOOSE / MISSING):")
    for asm, stat in sorted(per_assembly.items(), key=lambda kv: -sum(kv[1].values())):
        total = sum(stat.values())
        if asm in ALIASES or asm.startswith(("GigaCreation", "Elringus", "Naninovel", "WitchTrials", "CustomInspector", "ppt-noise")):
            print(f"    {asm:52} {stat['EXACT']:5} / {stat['LOOSE_NAME_MATCH']:5} / "
                  f"{stat['NOT_IN_RECONSTRUCTION']:5}   of {total}")
    missing = [c for c in coverage if c["status"] == "NOT_IN_RECONSTRUCTION"]
    if missing:
        by_asm = defaultdict(int)
        for c in missing:
            by_asm[c["assembly"]] += 1
        print("  missing by assembly:", dict(sorted(by_asm.items(), key=lambda kv: -kv[1])[:12]))
    print(f"index           : {INDEX_OUT}")
    print(f"coverage        : {COVERAGE_OUT}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
