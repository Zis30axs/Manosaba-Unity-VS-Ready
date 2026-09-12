"""Adopt the last few unindexed files back into the manifest by deriving their metadata."""
import os, json, csv, re

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
MAN = os.path.join(WORK, "manifest_core.jsonl")

known = set()
with open(os.path.join(RES, "_index.csv"), encoding="utf-8-sig") as fh:
    for row in csv.DictReader(fh):
        known.add(row["output_file"])

def guess_type(obj):
    keys = set(obj.keys())
    if "m_Component" in keys:
        return "GameObject"
    if "m_Children" in keys or "m_LocalRotation" in keys:
        return "Transform"
    if "m_AnchorMin" in keys:
        return "RectTransform"
    if "m_ClassName" in keys:
        return "MonoScript"
    if "m_GameObject" in keys:
        return "MonoBehaviour"
    return "SerializedObject"

added = 0
with open(MAN, "a", encoding="utf-8") as out:
    for root, dirs, files in os.walk(RES):
        for f in files:
            rel = os.path.relpath(os.path.join(root, f), RES).replace(os.sep, "/")
            if rel in known or rel.startswith("_index") or rel.startswith("_monoscripts"):
                continue
            ser = rel.startswith("_serialized/")
            body = rel[len("_serialized/"):] if ser else rel
            body = re.sub(r"~\d+(?=\.[^.]+$)", "", body)
            name = os.path.basename(body)
            for suf in (".asset.json", ".json"):
                if name.endswith(suf):
                    name = name[: -len(suf)]
                    break
            cont = None
            if not ser:
                # mirrored container path (strip our file suffix)
                cont = re.sub(r"\.(asset\.)?json$", "", rel)
            t = "SerializedObject"
            if rel.endswith(".json"):
                try:
                    t = guess_type(json.load(open(os.path.join(RES, rel.replace("/", os.sep)), encoding="utf-8")))
                except Exception:
                    pass
            out.write(json.dumps({"src": "(recovered)", "path_id": 0, "type": t, "container": cont,
                                  "name": name, "out": rel}, ensure_ascii=False) + "\n")
            added += 1
print("adopted", added)
