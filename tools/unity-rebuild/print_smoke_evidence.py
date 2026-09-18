"""Print the Player smoke evidence in a readable form (assemblies list elided)."""
import io
import json
import os

PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "out", "unity",
                    "player_smoke_evidence.json")
doc = json.load(io.open(PATH, encoding="utf-8"))
content = doc["content"]

print("overall      :", doc["status"])
print("assemblies   :", doc["assemblies_found"], "missing:", doc["assemblies_missing"])
print("type probes  :", doc["types_probed_ok"])
print("catalog      :", doc["catalog"]["status"], doc["catalog"]["keys"], "keys")
print("content      :", content["status"], "| locator", content["locator_id"],
      "|", content["locator_keys"], "keys | loaded", content["loaded"], "failed", content["failed"])
print("game scripts :", content["game_script_components"])
for record in content["records"]:
    print("  asset   ", record["status"], record["address"], "->", record["type"], "|", record["detail"])
for prefab in content["prefabs"]:
    print("  prefab  ", prefab["status"], prefab["address"], "->", prefab["instance"],
          "|", prefab["transforms"], "transforms /", prefab["components"], "components")
    print("            types:", prefab["component_types"])
    if prefab["error"]:
        print("            error:", prefab["error"][:300])
