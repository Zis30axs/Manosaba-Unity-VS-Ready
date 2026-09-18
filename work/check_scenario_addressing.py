"""Measure how the Manosaba.Scenarios plan addresses relate to the converted Script assets' `path`.

G6's scenario detail keeps reporting `0 path fields match their address`, because it derives the expected
value as `address.split("Scenarios/")[-1][:-5]` and bails to None unless the address ends in `.nani`:

    expected = address.split("Scenarios/", 1)[-1][:-5] if address.endswith(".nani") else None

That was right while the plan addressed scripts by their shipped `.nani` asset path. The plan now
addresses them under the Naninovel key namespace, so the `.nani` form never matches. This script
measures what the relationship actually is, over all 440 entries, so the check can be restated from
evidence instead of from the older assumption.
"""
import collections
import io
import json
import os
import re

REPO = r"E:\Manosaba-VSCodeReady"
UNITY_DATA = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity")
PROJECT = os.path.join(REPO, "UnityProject")

plan = json.load(io.open(os.path.join(UNITY_DATA, "addressables_plan_all.json"), encoding="utf-8"))
entries = [e for e in plan.get("entries", []) if isinstance(e, dict)]
scenarios = [e for e in entries if (e.get("group") or "").endswith("Scenarios")]
print("scenario entries: %d" % len(scenarios))

stats = collections.Counter()
samples = []
for e in scenarios:
    asset_path = (e.get("asset_path") or "").replace("/", os.sep)
    address = e.get("address") or ""
    full = os.path.join(PROJECT, asset_path)
    if not os.path.exists(full):
        stats["asset_missing"] += 1
        continue
    text = io.open(full, encoding="utf-8", errors="replace").read()
    m = re.search(r"^\s*path:\s*(\S+)\s*$", text, re.M)
    script_path = m.group(1) if m else ""
    if not script_path:
        stats["path_field_absent"] += 1
        continue
    if address.endswith(".nani"):
        stats["address_is_nani"] += 1
    if address == "Naninovel/Scripts/" + script_path:
        stats["address_eq_naninovel_scripts_plus_path"] += 1
    elif address.endswith(script_path):
        stats["address_endswith_path"] += 1
    else:
        stats["neither"] += 1
    if len(samples) < 5:
        samples.append((asset_path, script_path, address))

print()
for k, v in sorted(stats.items()):
    print("  %-40s %d" % (k, v))
print()
print("=== samples (asset / path field / address) ===")
for a, p, ad in samples:
    print("  %s\n     path=%-30s address=%s" % (a, p, ad))

# What do the addresses look like at the head, and are they shipped keys?
print()
prefixes = collections.Counter("/".join((e.get("address") or "").split("/")[:2]) for e in scenarios)
print("address prefixes: %s" % dict(prefixes.most_common(6)))
