"""Print the wired game-start evidence (G4b -Wire) in detail."""
import io
import json
import os
import sys

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")
NAME = sys.argv[1] if len(sys.argv) > 1 else "game_start_evidence_wired.json"
with io.open(os.path.join(BASE, NAME), encoding="utf-8-sig") as fh:
    doc = json.load(fh)

for key in ("status", "entry_type", "type_resolved", "component_created", "addressables_field_found",
            "addressables_manager_created", "addressables_manager_type", "declared_method_count",
            "start_method_found", "start_invocation", "log_errors", "log_exceptions", "first_exception"):
    print(f"{key:30} {doc.get(key)}")
print("wiring:")
for line in doc.get("wiring") or []:
    print("   -", line)
print("log:")
for entry in doc.get("log") or []:
    print(f"   [{entry['type']}] {entry['message'][:120]}")
    for line in (entry.get("stack") or "").split("\n")[:3]:
        print("        ", line.strip()[:140])
