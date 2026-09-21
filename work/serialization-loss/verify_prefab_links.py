"""Check every reference INTO a prefab asset of the project: {fileID: X, guid: G, type: 3} where G is a .prefab.

    python work/serialization-loss/verify_prefab_links.py

PrefabBuilder deletes and re-saves each prefab, which gives its objects new local file IDs, so a prefab built before
the prefab it points into keeps a stale ID (2026-09-21: AdvChoicePanel -> ChoiceButton_Adv@Bad).  The planner now
orders jobs by these dependencies; this script is the independent check that no reference dangles.  Read-only.
Exit code 0 = no dangling reference.
"""
import os
import re
import sys
from collections import Counter

ROOT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "..", "UnityProject", "Assets")
REF = re.compile(r"\{fileID: (-?\d+), guid: ([0-9a-f]{32}), type: 3\}")
HEADER = re.compile(r"^--- !u!\d+ &(-?\d+)", re.M)


def main():
    guid_to_prefab = {}
    for dirpath, _, files in os.walk(ROOT):
        for name in files:
            if name.endswith(".prefab.meta"):
                with open(os.path.join(dirpath, name), encoding="utf-8", errors="replace") as fh:
                    m = re.search(r"^guid: ([0-9a-f]{32})", fh.read(), re.M)
                if m:
                    guid_to_prefab[m.group(1)] = os.path.join(dirpath, name[:-5])
    ids = {}
    ok = 0
    dangling = Counter()
    for dirpath, _, files in os.walk(ROOT):
        for name in files:
            if not name.endswith((".prefab", ".asset", ".unity")):
                continue
            path = os.path.join(dirpath, name)
            with open(path, encoding="utf-8", errors="replace") as fh:
                text = fh.read()
            for file_id, guid in REF.findall(text):
                target = guid_to_prefab.get(guid)
                if not target:
                    continue
                if target not in ids:
                    with open(target, encoding="utf-8", errors="replace") as fh:
                        ids[target] = set(HEADER.findall(fh.read()))
                if file_id in ids[target]:
                    ok += 1
                else:
                    dangling[(os.path.relpath(path, ROOT), os.path.relpath(target, ROOT), file_id)] += 1
    print("references into prefabs: %d resolve, %d dangle" % (ok, sum(dangling.values())))
    for (source, target, file_id), n in dangling.most_common():
        print("  %d  %s -> %s  fileID %s" % (n, source, target, file_id))
    return 1 if dangling else 0


if __name__ == "__main__":
    sys.exit(main())
