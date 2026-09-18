"""Compare the Editor Play Mode evidence (G4) with the standalone Player evidence (G5) side by side."""
import io
import json
import os

BASE = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity")


def load(path):
    with io.open(path, encoding="utf-8-sig") as fh:
        return json.load(fh)


g4 = load(os.path.join(BASE, "g4_play_mode.json"))
g5 = load(os.path.join(BASE, "player_smoke_evidence.json"))
gate4 = load(os.path.join(BASE, "g4_play_mode_gate.json"))

print(f"G4 gate : {gate4['status']}  (editor exit {gate4['play_mode']['exit_code']})")
print(f"G4 tool : {g4['status']}  scene={g4.get('scene')}  play_seconds={g4.get('play_seconds')}")
if g4.get("play_mode_builder_note"):
    print(f"          note: {g4['play_mode_builder_note']}")

for label, evidence in (("G4 editor", g4.get("evidence")), ("G5 player", g5)):
    content = evidence["content"]
    providers = sorted({(r.get("provider") or "?").split(".")[-1] for r in content["records"]})
    prefabs = content.get("prefabs") or []
    print(f"\n{label}: status={evidence['status']} is_editor={evidence['is_editor']} "
          f"platform={evidence['platform']}")
    print(f"  assemblies={evidence['assemblies_found']} probes={evidence['types_probed_ok']} "
          f"shipped_catalog={(evidence.get('catalog') or {}).get('status')} "
          f"{(evidence.get('catalog') or {}).get('keys')} keys")
    print(f"  own catalog={content['locator_id']} {content['locator_keys']} keys | "
          f"loaded={content['loaded']} failed={content['failed']} | providers={providers}")
    print(f"  game scripts bound: {content['game_script_components']}")
    for prefab in prefabs:
        print(f"    prefab {prefab['status']:10} {prefab['address']:46} -> {prefab['instance']}")
