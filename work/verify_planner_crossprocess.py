"""Cross-process determinism with a FROZEN input dump (isolates the planner from the dump builder)."""
import hashlib
import json
import os
import shutil
import subprocess
import sys
import tempfile

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
COMPLETE = os.path.join(OUT, "catalog_dump_shipped.json")   # the real Unity-regenerated dump
PLAN = os.path.join(OUT, "addressables_plan_all.json")

tmp = tempfile.mkdtemp(prefix="manosaba-freeze-")
frozen = os.path.join(tmp, "frozen.json")
shutil.copyfile(COMPLETE, frozen)
print("frozen dump sha:", hashlib.sha256(open(frozen, "rb").read()).hexdigest())

RUNNER = os.path.join(tmp, "run.py")
open(RUNNER, "w", encoding="utf-8").write(
    "import json,os,sys\n"
    "sys.path.insert(0, r'%s')\n"
    "import a13f_plan_addressables as planner\n"
    "planner.SHIPPED_DUMP = r'%s'\n"
    "sys.argv = ['x', '--group', 'all']\n"
    "rc = planner.main()\n"
    "print('EXIT', rc)\n" % (os.path.join(REPO, "tools", "unity-rebuild"), frozen))


def fingerprint(path):
    doc = json.load(open(path, encoding="utf-8"))
    doc.pop("generated", None)
    return hashlib.sha256(json.dumps(doc, sort_keys=True, ensure_ascii=False).encode("utf-8")).hexdigest()


fps = []
for i in (1, 2, 3):
    p = subprocess.run([sys.executable, RUNNER], capture_output=True, text=True, encoding="utf-8",
                       errors="replace", cwd=REPO)
    shutil.copyfile(PLAN, os.path.join(HERE, "frozen_p%d.json" % i))
    fps.append(fingerprint(PLAN))
    print("process run %d: exit-line=%r  fingerprint=%s" % (
        i, (p.stdout or "").strip().splitlines()[-1:], fps[-1]))
print("cross-process deterministic (frozen input):", len(set(fps)) == 1)

a = json.load(open(os.path.join(HERE, "frozen_p1.json"), encoding="utf-8"))
b = json.load(open(os.path.join(HERE, "frozen_p2.json"), encoding="utf-8"))
a.pop("generated", None); b.pop("generated", None)
print("run1 == run2:", a == b)
print("frozen-dump plan Naninovel:", a["labels_used"].get("Naninovel"),
      "entries_with_labels:", a["entries_with_labels"])
shutil.rmtree(tmp, ignore_errors=True)
