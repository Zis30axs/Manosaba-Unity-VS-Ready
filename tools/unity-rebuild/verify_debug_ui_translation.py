"""Read-only audit of the 16 restored Debug labels in source, prefab and bundles.

Run tools/venv/Scripts/python.exe tools/unity-rebuild/verify_debug_ui_translation.py
The tracked mapping and extracted/imported game resources are required. Use
--static-only while Addressables is building. --restoration-receipt additionally
checks an earlier restoration's backups and non-text byte preservation. JSON goes
to stdout; no files are written, imported or rebuilt.
"""
from pathlib import Path
import argparse
from datetime import datetime, timezone
import hashlib
import json
import re
import sys


ROOT = Path(__file__).resolve().parents[2]
BUILD = ROOT / "UnityProject/Library/com.unity.addressables/aa/Windows"
PATCH = "tools/unity-rebuild/debug-ui-zh-Hans.patch"
PLAN = "res/.manosaba/reconstruction/unity/prefab_plan_all.json"
MAPPING = "tools/unity-rebuild/debug-ui-zh-Hans.mapping.json"
BACKUP = ".3d-model/preview/debug-translation-backup/"
PREFIX = "#WitchTrials/Prefabs/"
GAME = "General/Core/Game.prefab"
# Patch order, including the two separate ChoiceButton owners of the same label.
EXPECTED = [
    ("General/Core/Game~25.asset.json", GAME),
    ("General/Core/Game~46.asset.json", GAME),
    ("General/Core/Game~40.asset.json", GAME),
    ("General/Core/Game~104.asset.json", GAME),
    ("General/Core/Game~81.asset.json", GAME),
    ("General/Core/Game~94.asset.json", GAME),
    ("General/Core/Game~16.asset.json", GAME),
    ("General/Core/Game~62.asset.json", GAME),
    ("General/Core/Game~89.asset.json", GAME),
    ("General/Core/Game~83.asset.json", GAME),
    ("General/Core/Game~101.asset.json", GAME),
    ("General/Core/Game~69.asset.json", GAME),
    ("General/Core/Game~31.asset.json", "General/Debug/DebugLabel.prefab"),
    ("General/Debug/DebugCommandButton~2.asset.json", "General/Debug/DebugCommandButton.prefab"),
    ("Naninovel/UI/ChoiceButtons/ChoiceButton_Debug~3.asset.json", "Naninovel/UI/ChoiceButtons/ChoiceButton_Debug.prefab"),
    ("Naninovel/UI/ChoiceButtons/ChoiceButton_Debug@Cancel~2.asset.json", "Naninovel/UI/ChoiceButtons/ChoiceButton_Debug@Cancel.prefab"),
]


def require(condition, message):
    if not condition:
        raise ValueError(message)


def path(relative):
    """Keep receipt-derived paths inside this workspace; allowlists are checked too."""
    require(isinstance(relative, str), "Path must be a string")
    candidate = (ROOT / relative).resolve()
    require(candidate.is_relative_to(ROOT), "Path outside workspace")
    return candidate


def read_json(relative):
    return json.loads(path(relative).read_text(encoding="utf-8-sig"))


def digest(data):
    return hashlib.sha256(data).hexdigest()


def scalar(value):
    value = value.strip()
    return json.loads(value) if value.startswith('"') else value


def patch_pairs():
    pairs, prefab, old = [], None, None
    for line in path(PATCH).read_text(encoding="utf-8-sig").splitlines():
        if line.startswith("+++ b/"):
            prefab = line[6:]
        elif line.startswith(("+", "-")) and not line.startswith(("+++", "---")):
            require(line[1:].startswith("  m_text: "), "Patch changes a field other than m_text")
            value = scalar(line[len("-  m_text: "):])
            if line[0] == "-":
                require(old is None, "Unpaired removed label")
                old = value
            else:
                require(old is not None, "Unpaired added label")
                pairs.append((prefab, old, value))
                old = None
    require(old is None and len(pairs) == 16, "Patch must contain exactly 16 m_text changes")
    return pairs


def documents(text):
    matches = list(re.finditer(r"^--- !u!(\d+) &(-?\d+)\r?$", text, re.M))
    docs = {}
    for index, match in enumerate(matches):
        end = matches[index + 1].start() if index + 1 < len(matches) else len(text)
        require(match[2] not in docs, "Duplicate prefab local file ID")
        docs[match[2]] = (int(match[1]), text[match.end():end], match.end())
    return docs


def field(doc, name):
    matches = list(re.finditer(r"^  " + re.escape(name) + r": ([^\n]*)", doc, re.M))
    require(len(matches) == 1, "Missing/ambiguous prefab field: " + name)
    return matches[0]


def file_id(doc, name):
    value = field(doc, name)[1]
    match = re.fullmatch(r"\{fileID: (-?\d+)\}", value)
    require(match is not None, "Invalid local reference: " + name)
    return match[1]


def hierarchy(docs, owner):
    transforms = {}
    for tid, (kind, doc, _) in docs.items():
        if kind in (4, 224):
            go = file_id(doc, "m_GameObject")
            require(go not in transforms, "Multiple transforms on a GameObject")
            transforms[go] = tid
    names, seen = [], set()
    while owner != "0":
        require(owner not in seen and owner in docs, "Invalid/cyclic prefab hierarchy")
        seen.add(owner)
        require(docs[owner][0] == 1, "Hierarchy owner is not a GameObject")
        names.append(scalar(field(docs[owner][1], "m_Name")[1]))
        require(owner in transforms, "GameObject has no transform")
        parent = file_id(docs[transforms[owner]][1], "m_Father")
        owner = file_id(docs[parent][1], "m_GameObject") if parent != "0" else "0"
    return "/".join(reversed(names))


def masked(text, spans):
    for start, end in sorted(spans, reverse=True):
        text = text[:start] + "<verified-m_text>" + text[end:]
    return text


def check_static(result):
    document = read_json(MAPPING)
    mappings = document["labels"]
    require(document["schemaVersion"] == 1 and len(mappings) == 16, "Unsupported/invalid label mapping")
    pairs = patch_pairs()
    state = read_json("res/.manosaba-content-state.json")["files"]
    jobs = read_json(PLAN)["jobs"]
    sources, targets, identities = set(), set(), set()
    prefab_text, prefab_docs = {}, {}
    for index, (mapping, expected, pair) in enumerate(zip(mappings, EXPECTED, pairs), 1):
        source_hint = "res/Assets/" + PREFIX + expected[0]
        prefab = "UnityProject/Assets/GameContent/" + PREFIX + expected[1]
        require(mapping["source"] == source_hint and mapping["prefab"] == prefab,
                f"Mapping conflict at label {index}")
        require(pair == (prefab, mapping["old"], mapping["new"]),
                f"Patch/mapping conflict at label {index}")
        identity = (prefab, mapping["targetHierarchyPath"])
        require(identity not in identities, "Duplicate target label identity")
        targets.add(prefab)
        identities.add(identity)
        target = prefab.removeprefix("UnityProject/")
        asset_path = "Assets/" + PREFIX + expected[1]
        plan_jobs = [j for j in jobs if j["asset_path"] == asset_path]
        require(len(plan_jobs) == 1, "Prefab plan asset-path conflict")
        # Extracted filenames can change; prefer the source object's identity.
        objects = [o for o in plan_jobs[0]["objects"] if str(o["pid"]) == mapping["componentPathId"]]
        if not objects:
            objects = [o for o in plan_jobs[0]["objects"] if o.get("dump") == source_hint]
        require(len(objects) == 1 and objects[0]["script"]["class"] == "TextMeshProUGUI"
                and objects[0]["script"]["namespace"] == "TMPro", "Source component identity conflict")
        source = objects[0]["dump"]
        require(source.startswith("res/Assets/" + PREFIX) and source not in sources,
                "Invalid/duplicate source component path")
        require(path(source).is_relative_to(path("res/Assets/" + PREFIX)),
                "Extracted source path escapes the prefab directory")
        sources.add(source)
        record = state[source.removeprefix("res/")]
        require(record["category"] == "PREFAB" and record["targets"] == [target],
                "Content-state target conflict: " + source)
        matching = [j for j in record["jobs"] if j["source"] == source and j["target"] == target
                    and j["plan"] == PLAN and j["asset_path"] == asset_path]
        require(len(matching) == 1, "Content-state job conflict: " + source)
        require(any(f["field"] == "m_text" and f["editability"] == "FULL"
                    for f in record["field_editability"]), "m_text not editable")
        data = path(source).read_text(encoding="utf-8")
        tree = json.loads(data)
        owners = [o for o in plan_jobs[0]["objects"] if str(o["pid"]) == str(tree["m_GameObject"]["m_PathID"])
                  and o["type"] == "GameObject"]
        require(len(owners) == 1 and tree["m_GameObject"]["m_FileID"] == 0 and tree["m_text"] == mapping["new"],
                "Source owner/label conflict")
        if prefab not in prefab_text:
            prefab_text[prefab] = path(prefab).read_bytes().decode("utf-8")
            prefab_docs[prefab] = documents(prefab_text[prefab])
        docs = prefab_docs[prefab]
        owners = [oid for oid, (kind, _, _) in docs.items() if kind == 1
                  and hierarchy(docs, oid) == mapping["targetHierarchyPath"]]
        require(len(owners) == 1, "Missing/ambiguous prefab hierarchy")
        components = [(cid, doc) for cid, (kind, doc, _) in docs.items() if kind == 114
                      and file_id(doc, "m_GameObject") == owners[0]
                      and re.search(r"^  m_text:", doc, re.M)]
        require(len(components) == 1, "Missing/ambiguous prefab text component")
        component, doc = components[0]
        value = field(doc, "m_text")
        require(scalar(value[1]) == mapping["new"], "Prefab label conflict")
        result["items"].append({"id": index, "source": source.removeprefix("res/Assets/" + PREFIX),
                                "sourcePathId": str(objects[0]["pid"]),
                                "sourceGameObjectPathId": str(tree["m_GameObject"]["m_PathID"]),
                                "prefab": expected[1],
                                "targetComponentFileId": component, "targetGameObjectFileId": owners[0],
                                "hierarchy": mapping["targetHierarchyPath"], "expected": mapping["new"],
                                "staticPass": True, "bundlePass": None})
    result["staticPass"] = True


def check_restoration(result, receipt_path):
    receipt = read_json(receipt_path)
    mappings = receipt["mappings"]
    require(receipt["patch"] == PATCH and len(mappings) == 16, "Invalid restoration receipt")
    for mapping, current, pair in zip(mappings, result["items"], patch_pairs()):
        require((mapping["prefab"], mapping["old"], mapping["new"]) == pair
                and mapping["source"] == "res/Assets/" + PREFIX + current["source"]
                and mapping["componentPathId"] == current["sourcePathId"]
                and mapping["gameObjectPathId"] == current["sourceGameObjectPathId"]
                and mapping["targetComponentFileId"] == current["targetComponentFileId"]
                and mapping["targetGameObjectFileId"] == current["targetGameObjectFileId"]
                and mapping["targetHierarchyPath"] == current["hierarchy"], "Restoration/current mapping conflict")
    sources = {m["source"] for m in mappings}
    targets = {m["prefab"] for m in mappings}
    files = receipt["files"]
    require(len(files) == 21 and len({f["relativePath"] for f in files}) == 21,
            "Receipt must identify 21 unique changed files")
    recorded = {f["relativePath"]: f for f in files}
    require(set(recorded) == sources | targets, "Unexpected/missing changed file")
    state = read_json("res/.manosaba-content-state.json")["files"]
    for relative, info in recorded.items():
        require(info["backup"] == BACKUP + relative, "Backup path conflict")
        old_bytes = path(info["backup"]).read_bytes()
        new_bytes = path(relative).read_bytes()
        require(digest(old_bytes) == info["oldSha256"] and digest(new_bytes) == info["newSha256"],
                "Restoration hash drift: " + relative)
        old, new = old_bytes.decode("utf-8"), new_bytes.decode("utf-8")
        relevant = [m for m in mappings if relative in (m["source"], m["prefab"])]
        require(info["labels"] == [{"old": m["old"], "new": m["new"]} for m in relevant],
                "Receipt file-label conflict")
        if relative in sources:
            spans = list(re.finditer(r'"m_text"\s*:\s*("(?:\\.|[^"\\])*")', old))
            require(len(spans) == 1 and json.loads(spans[0][1]) == relevant[0]["old"],
                    "Backup source label conflict")
            current_spans = list(re.finditer(r'"m_text"\s*:\s*("(?:\\.|[^"\\])*")', new))
            require(len(current_spans) == 1, "Missing/ambiguous current JSON m_text")
            require(masked(old, [spans[0].span(1)]) == masked(new, [current_spans[0].span(1)]),
                    "Source changed outside m_text")
        else:
            docs, current_docs, spans, current_spans = documents(old), documents(new), [], []
            for m in relevant:
                _, doc, offset = docs[m["targetComponentFileId"]]
                value = field(doc, "m_text")
                require(scalar(value[1]) == m["old"], "Backup prefab label conflict")
                spans.append((offset + value.start(1), offset + value.end(1)))
                _, doc, offset = current_docs[m["targetComponentFileId"]]
                value = field(doc, "m_text")
                current_spans.append((offset + value.start(1), offset + value.end(1)))
                record = state[m["source"].removeprefix("res/")]
                meta = record["target_metadata"][relative.removeprefix("UnityProject/")]
                meta_bytes = path(relative + ".meta").read_bytes()
                require(digest(meta_bytes) == meta["sha256"] and ("guid: " + meta["guid"]).encode() in meta_bytes,
                        "Prefab metadata changed")
            require(masked(old, spans) == masked(new, current_spans),
                    "Prefab changed outside m_text")
    result["restorationPass"] = True
    result["nonTextBytesPreserved"] = True
    result["metadataHashesUnchanged"] = True


def check_bundles(result):
    import UnityPy
    groups = ("general-prefabs", "general-choicebuttons")
    environments = {}
    for group in groups:
        candidates = list(BUILD.rglob(f"manosaba.prefabs.{group}_assets_all_*.bundle"))
        require(len(candidates) == 1, f"Expected one built {group} bundle, found {len(candidates)}")
        bundle = candidates[0].resolve()
        require(bundle.is_relative_to(BUILD.resolve()), "Bundle outside build directory")
        before = bundle.stat()
        bundle_hash = digest(bundle.read_bytes())
        environments[group] = UnityPy.load(str(bundle))
        after = bundle.stat()
        require((before.st_size, before.st_mtime_ns) == (after.st_size, after.st_mtime_ns),
                "Bundle changed during audit")
        result["bundles"].append({"group": group, "file": bundle.name,
                                  "sha256": bundle_hash, "bytes": after.st_size})
    for prefab in sorted({item["prefab"] for item in result["items"]}):
        group = "general-choicebuttons" if "/ChoiceButtons/" in prefab else "general-prefabs"
        env = environments[group]
        container = "Assets/GameContent/" + PREFIX + prefab
        matches = [pointer for name, pointer in env.container.items() if name.casefold() == container.casefold()]
        require(len(matches) == 1, "Missing/ambiguous built prefab container: " + prefab)
        wanted = [item for item in result["items"] if item["prefab"] == prefab]
        nodes = {}
        def walk(go, parent="", ancestors=frozenset()):
            identity = (id(go.object_reader.assets_file), go.object_reader.path_id)
            require(identity not in ancestors, "Cyclic bundle hierarchy")
            breadcrumb = parent + "/" + go.m_Name if parent else go.m_Name
            nodes.setdefault(breadcrumb, []).append(go)
            transforms = [pair.component.deref() for pair in go.m_Component
                          if pair.component.deref().type.name in ("Transform", "RectTransform")]
            require(len(transforms) == 1, "Missing/ambiguous built transform")
            for child in transforms[0].read().m_Children:
                walk(child.read().m_GameObject.read(), breadcrumb, ancestors | {identity})
        walk(matches[0].read())
        for item in wanted:
            owners = nodes.get(item["hierarchy"], [])
            require(len(owners) == 1, "Missing/ambiguous built owner: " + item["hierarchy"])
            texts = []
            for pair in owners[0].m_Component:
                obj = pair.component.deref()
                if obj.type.name == "MonoBehaviour":
                    tree = obj.read_typetree()
                    if isinstance(tree.get("m_text"), str):
                        texts.append((obj.path_id, tree["m_text"]))
            require(len(texts) == 1, "Missing/ambiguous built text component")
            item["bundlePathId"], item["actual"] = texts[0]
            item["bundlePass"] = item["actual"] == item["expected"]
    require(all(item["bundlePass"] for item in result["items"]), "Built Debug labels differ from restored source/prefabs")
    result["bundlePass"] = True


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--static-only", action="store_true")
    parser.add_argument("--restoration-receipt", metavar="REPO_RELATIVE_JSON",
                        help="Additionally prove this local restoration against its recorded backups")
    args = parser.parse_args()
    result = {"status": "FAIL", "generated": datetime.now(timezone.utc).isoformat(),
              "mode": "STATIC" if args.static_only else "FULL",
              "staticPass": False, "bundlePass": None, "items": [], "bundles": []}
    try:
        check_static(result)
        if args.restoration_receipt:
            check_restoration(result, args.restoration_receipt)
        if not args.static_only:
            check_bundles(result)
        result["status"] = "PASS_STATIC" if args.static_only else "PASS"
    except Exception as error:
        result["error"] = str(error)
    print(json.dumps(result, ensure_ascii=True, separators=(",", ":")))
    return 0 if result["status"] in ("PASS", "PASS_STATIC") else 1


if __name__ == "__main__":
    sys.exit(main())
