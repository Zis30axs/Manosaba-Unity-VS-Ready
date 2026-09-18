"""One-off: object counts actually written into the imported prefab files vs the prefab build report.

A prefab whose file holds fewer objects than the builder reported creating lost hierarchy on save --
that is a fidelity gap the report alone does not show."""
import collections
import io
import json
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
BASE = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
DOC = re.compile(r"(?m)^--- !u!")

result = json.load(io.open(os.path.join(BASE, "prefab_build_result.json"), encoding="utf-8"))
rows = []
for job in result["jobs"]:
    path = os.path.join(REPO, "UnityProject", job["target"].split("UnityProject", 1)[-1].lstrip("\\/"))
    docs = 0
    if os.path.isfile(path):
        docs = len(DOC.findall(io.open(path, encoding="utf-8", errors="replace").read()))
    rows.append((job["asset_path"], job.get("objects_planned", 0), job.get("objects_created", 0), docs))

print(f"prefabs: {len(rows)}")
print(f"planned objects : {sum(r[1] for r in rows)}")
print(f"report created  : {sum(r[2] for r in rows)}")
print(f"written to file : {sum(r[3] for r in rows)}")
short = [r for r in rows if r[3] != r[2]]
print(f"prefabs whose file holds fewer objects than the report claims: {len(short)}")
for path, planned, created, docs in sorted(short, key=lambda r: r[3] - r[2])[:15]:
    print(f"  file={docs:5} created={created:5} planned={planned:5}  {path}")
