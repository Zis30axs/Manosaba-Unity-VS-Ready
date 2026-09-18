"""Print the status of every gate artifact, so a completion claim can be checked at a glance."""
import glob
import io
import json
import os

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
for path in sorted(glob.glob(os.path.join(BASE, "g*.json"))):
    name = os.path.basename(path)
    if "evidence" in name:
        continue
    try:
        with io.open(path, encoding="utf-8-sig") as fh:
            doc = json.load(fh)
    except Exception as ex:
        print(f"{name:30} unreadable: {ex}")
        continue
    status = doc.get("status") or "NO_STATUS_FIELD"
    detail = ""
    ground = doc.get("play_mode") or doc.get("setup")
    if isinstance(doc.get("plan"), dict):
        detail = f"entries={doc['plan'].get('entry_count')} excluded={doc['plan'].get('excluded_count')}"
    if name.startswith("g2"):
        detail = f"found={doc.get('found')} missing={doc.get('missing_count')} changed={doc.get('guid_changed_count')}"
    print(f"{name:30} {status:26} {detail}")
