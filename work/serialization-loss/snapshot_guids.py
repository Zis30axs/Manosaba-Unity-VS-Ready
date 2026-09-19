"""Snapshot the .meta GUIDs of every rebuilt scenario/prefab asset (pre-regeneration baseline)."""
import glob, json, re, sys
out = sys.argv[1] if len(sys.argv) > 1 else "work/serialization-loss/guid_snapshot_before.json"
snap = {}
for m in glob.glob("UnityProject/Assets/GameContent/**/*.meta", recursive=True):
    if not m.endswith((".asset.meta", ".prefab.meta")):
        continue
    g = re.search(r"guid: ([0-9a-f]{32})", open(m, encoding="utf-8").read())
    if g:
        snap[m.replace("\\", "/")] = g.group(1)
json.dump(snap, open(out, "w"), indent=0, sort_keys=True)
print("snapshot entries", len(snap), "->", out)
