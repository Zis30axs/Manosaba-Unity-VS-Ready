"""Decode the shipped boot scene's `GameInitializer` references (the `level0` scene of the retail build).

    python tools/unity-rebuild/decode_level0_boot.py [--write]

Why this exists: `manosaba_Data/level0` is a *player* build scene, so it carries no type tree for its
MonoBehaviours and UnityPy refuses it ("Expected to read 120 bytes, but only read 32 bytes").  The bytes are
there, though, and the layout is derivable from the reconstructed C# type plus the serialized shape of
`AssetReference`, so the game's own values can be recovered instead of synthesized.

Layout of the GameInitializer MonoBehaviour (validated: it consumes exactly the object's 120 bytes):

    +0    PPtr<GameObject> m_GameObject        (int m_FileID, long m_PathID)
    +12   int32             m_Enabled
    +16   PPtr<MonoScript>  m_Script           (m_FileID 1 = globalgamemanagers.assets; path_id 1504 is the
                                                same MonoScript the extraction index records for
                                                "MonoScript GameInitializer")
    +28   string            m_Name             (empty for this component; consumed, never assumed away)
    +32   string            _gamePrefab.m_AssetGUID       (Addressables AssetReference serialized fields:
    +..   string            _gamePrefab.m_SubObjectName    m_AssetGUID, m_SubObjectName, m_SubObjectType)
    +..   string            _gamePrefab.m_SubObjectType
    +..   PPtr              _steamManager
    +..   PPtr              _splashScreen
    +..   string            _gamePrefabInstanceName

Self-validation: the byte budget must close exactly, the GUID must be 32 hex characters, and the two PPtrs
must resolve to objects present in the same scene file (m_FileID == 0).
"""
from __future__ import annotations

import argparse
import io
import json
import os
import re
import struct
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "level0_boot_references.json")
SHIPPED_CATALOG_DUMP = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "catalog_dump_shipped.json")
REBUILT_PREFAB = "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab"
#: PPtr<GameObject> (12) + m_Enabled (4) + PPtr<MonoScript> (12); `m_Name` follows the header.
HEADER_BYTES = 28


def resolve_asset_guid(guid):
    """Map the serialized `AssetReference.m_AssetGUID` to the asset it names.

    A GUID alone is opaque, so this asks the shipped Addressables catalog (dumped from inside Unity by
    `Assets/Editor/CatalogDump.cs`, the authoritative parser) which key equals it and what internal id that
    key resolves to.  That internal id is compared against the prefab this project rebuilt, which turns a
    recovered 32-hex string into a checkable statement about content.
    """
    if not os.path.exists(SHIPPED_CATALOG_DUMP):
        return {"resolved": False, "reason": "catalog dump not present: " + SHIPPED_CATALOG_DUMP}
    with io.open(SHIPPED_CATALOG_DUMP, encoding="utf-8") as fh:
        dump = json.load(fh)
    for key in dump.get("keys", []):
        if key.get("key") != guid:
            continue
        locations = key.get("locations") or []
        internal_id = locations[0] if locations else ""
        return {
            "resolved": bool(internal_id),
            "catalog": dump.get("catalog_source"),
            "key": key.get("key"),
            "internal_id": internal_id,
            "location_count": key.get("location_count"),
            "locate_error": key.get("locate_error"),
            "matches_rebuilt_prefab": internal_id.endswith(REBUILT_PREFAB.split("Assets/GameContent/")[-1]),
            "rebuilt_prefab": REBUILT_PREFAB,
        }
    return {"resolved": False, "reason": "guid is not a catalog key", "rebuilt_prefab": REBUILT_PREFAB}

try:
    import UnityPy
except ImportError:
    print("UnityPy is not available in this interpreter: " + sys.executable)
    raise SystemExit(3)


def game_root():
    with io.open(os.path.join(REPO, "tools", "audit", "audit.config.json"), encoding="utf-8") as fh:
        return json.load(fh)["game_root"]


def read_ptr(raw, offset):
    file_id, path_id = struct.unpack_from("<iq", raw, offset)
    return {"m_FileID": file_id, "m_PathID": path_id}


def read_string(raw, offset):
    (length,) = struct.unpack_from("<i", raw, offset)
    if length < 0 or length > 4096 or offset + 4 + length > len(raw):
        raise ValueError(f"implausible string length {length} at offset {offset}")
    text = raw[offset + 4: offset + 4 + length].decode("utf-8", "replace")
    end = offset + 4 + length
    return text, end + ((4 - end % 4) % 4)


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    scene = os.path.join(game_root(), "manosaba_Data", "level0")
    env = UnityPy.load(scene)
    objects = {str(obj.path_id): obj for obj in env.objects}

    # The GameInitializer GameObject is the one whose only MonoBehaviour component decodes as this layout;
    # the extraction index records it as GameObject path_id 17 with components 37 (Transform) and 116.
    target = objects.get("116")
    if target is None:
        print("BLOCKED: path_id 116 (GameInitializer MonoBehaviour) not found in " + scene)
        return 2
    raw = target.get_raw_data()

    doc = {
        "scene": scene,
        "object_count": len(objects),
        "monobehaviour_path_id": 116,
        "raw_bytes": len(raw),
        "validation": {},
    }
    doc["m_GameObject"] = read_ptr(raw, 0)
    doc["m_Enabled"] = struct.unpack_from("<i", raw, 12)[0]
    doc["m_Script"] = read_ptr(raw, 16)

    # m_Name follows the 28-byte header and precedes the script's fields.  For this component it is empty, which
    # is why the fields start at 32 - but that is *checked*, not assumed: a non-empty name would shift every
    # following field by its padded length.  (The shipped `Naninovel.EngineVersion` asset in resources.assets,
    # path_id 1486, shows the full shape: header | "EngineVersion" | "1.20" | false | "2025-04-26".)
    name_length = struct.unpack_from("<i", raw, HEADER_BYTES)[0]
    offset = HEADER_BYTES + 4
    doc["m_Name"] = ""
    if name_length:
        doc["m_Name"] = raw[offset:offset + name_length].decode("utf-8", "replace")
        offset += name_length + ((4 - (offset + name_length) % 4) % 4)
    doc["m_Name_length_prefix"] = name_length

    doc["_gamePrefab_m_AssetGUID"], offset = read_string(raw, offset)
    doc["_gamePrefab_m_SubObjectName"], offset = read_string(raw, offset)
    doc["_gamePrefab_m_SubObjectType"], offset = read_string(raw, offset)
    doc["_steamManager"] = read_ptr(raw, offset); offset += 12
    doc["_splashScreen"] = read_ptr(raw, offset); offset += 12
    doc["_gamePrefabInstanceName"], offset = read_string(raw, offset)
    doc["bytes_consumed"] = offset

    doc["_gamePrefab"] = resolve_asset_guid(doc["_gamePrefab_m_AssetGUID"])

    doc["validation"] = {
        "bytes_close_exactly": offset == len(raw),
        "m_Name_is_absent_or_empty": name_length == 0,
        "asset_guid_is_32_hex": bool(re.fullmatch(r"[0-9a-f]{32}", doc["_gamePrefab_m_AssetGUID"] or "")),
        "steam_manager_in_same_scene": doc["_steamManager"]["m_FileID"] == 0,
        "splash_screen_in_same_scene": doc["_splashScreen"]["m_FileID"] == 0,
        "referenced_objects_present": all(
            str(doc[key]["m_PathID"]) in objects for key in ("_steamManager", "_splashScreen")),
        "asset_guid_resolves_to_rebuilt_prefab": bool(doc["_gamePrefab"].get("matches_rebuilt_prefab")),
    }

    print(f"scene                : {scene}  ({len(objects)} objects)")
    print(f"m_GameObject         : {doc['m_GameObject']}")
    print(f"m_Script             : {doc['m_Script']}")
    print(f"_gamePrefab GUID     : {doc['_gamePrefab_m_AssetGUID']}")
    print(f"  sub-object         : name={doc['_gamePrefab_m_SubObjectName']!r} type={doc['_gamePrefab_m_SubObjectType']!r}")
    print(f"  catalog resolves to: {doc['_gamePrefab'].get('internal_id')!r} "
          f"(matches rebuilt prefab: {doc['_gamePrefab'].get('matches_rebuilt_prefab')})")
    print(f"_steamManager        : {doc['_steamManager']}")
    print(f"_splashScreen        : {doc['_splashScreen']}")
    print(f"_gamePrefabInstanceName: {doc['_gamePrefabInstanceName']!r}")
    print(f"bytes consumed       : {offset} of {len(raw)}")
    for key, value in doc["validation"].items():
        print(f"  validation {key:32} {value}")

    ok = all(doc["validation"].values())
    doc["status"] = "OK" if ok else "SUSPECT"
    if args.write:
        os.makedirs(os.path.dirname(OUT), exist_ok=True)
        with io.open(OUT, "w", encoding="utf-8") as fh:
            json.dump(doc, fh, ensure_ascii=False, indent=1)
        print("wrote " + OUT)
    return 0 if ok else 1


if __name__ == "__main__":
    sys.exit(main())
