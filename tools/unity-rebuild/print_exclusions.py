"""Print the current content-build exclusions with the offending type/field pulled out of the reason."""
import io
import json
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
PATH = os.path.join(REPO, "tools", "unity-rebuild", "content_build_exclusions.json")
with io.open(PATH, encoding="utf-8-sig") as fh:
    doc = json.load(fh)

print(f"excluded prefabs: {len(doc['excluded'])}")
for entry in doc["excluded"]:
    reason = entry["reason"]
    match = re.search(r"Contains ([^.]+)\.", reason)
    contains = match.group(1) if match else "?"
    field = re.search(r"offending field is typed by a stub assembly \(([^)]*)\)", reason)
    print(f"  {os.path.basename(entry['asset_path']):34} contains: {contains}")
    if field:
        print(f"      offending: {field.group(1)}")
