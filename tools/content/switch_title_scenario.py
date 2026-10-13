"""Apply the Switch title visuals to the PC title graph without replacing its UI/input flow."""
from __future__ import annotations

from copy import deepcopy
import hashlib
import json
import os
from pathlib import Path
import tempfile

import UnityPy


SCRIPT_PATH = "System/System_Title"
BACKGROUND_APPEARANCE = "AppearanceAndTransition"
VARIABLES = (
    'titleStillId="1_1"', 'titleScreenEffectId="TitleScreenEffect_1"',
    'g_gameProgress > 5', 'titleStillId="2_1"',
    'titleScreenEffectId="TitleScreenEffect_2"', 'g_gameProgress > 11',
    'titleStillId="3_1"', 'titleScreenEffectId="TitleScreenEffect_3"',
)


def _registry(script: dict) -> dict[int, dict]:
    if script.get("m_Name") != "System_Title" or script.get("path") != SCRIPT_PATH:
        raise ValueError("Expected the System/System_Title Script asset")
    entries = script["references"]["RefIds"]
    result = {entry["rid"]: entry for entry in entries}
    if len(result) != len(entries):
        raise ValueError("Duplicate managed-reference IDs in title script")
    for line in script["lines"]:
        item = result[line["rid"]]
        if item["type"]["class"] not in {"CommandScriptLine", "EmptyScriptLine", "LabelScriptLine"}:
            raise ValueError("Unexpected title line type; refusing an ambiguous graph rewrite")
        if "command" in item["data"]:
            result[item["data"]["command"]["rid"]]
    return result


def _commands(script: dict, refs: dict[int, dict]):
    for position, line_ref in enumerate(script["lines"]):
        line = refs[line_ref["rid"]]
        if "command" in line["data"]:
            yield position, line, refs[line["data"]["command"]["rid"]]


def _class(command: dict) -> str:
    return command["type"]["class"]


def _value(command: dict, parameter: str):
    return command["data"].get(parameter, {}).get("value")


def _expression(command: dict, parameter: str) -> str:
    parts = command["data"].get(parameter, {}).get("raw", {}).get("parts", [])
    return "".join(part.get("expression", "") for part in parts)


def _semantic(value):
    """Positions change after insertion; all other command fields remain significant."""
    if isinstance(value, list):
        return [_semantic(item) for item in value]
    if isinstance(value, dict):
        return {key: _semantic(item) for key, item in value.items()
                if key not in {"rid", "playbackSpot", "spot", "lineIndex"}}
    return value


def _one(items, description):
    items = list(items)
    if len(items) != 1:
        raise ValueError(f"Expected one {description}; found {len(items)}")
    return items[0]


def _refresh_background_hashes(script: dict) -> None:
    # The original source line text is unavailable. Naninovel's editor hot reload
    # only compares LineHash strings, so hash the final command semantics instead
    # of retaining the Switch hash when local PC parameters have been preserved.
    refs = _registry(script)
    for _, line, command in _visuals(script, refs)["backgrounds"]:
        semantic = json.dumps(_semantic(command), ensure_ascii=False, sort_keys=True,
                              separators=(",", ":"), allow_nan=False).encode("utf-8")
        line["data"]["lineHash"] = hashlib.sha256(b"switch-title-background-v1\0" + semantic).hexdigest()


def _visuals(script: dict, refs: dict[int, dict]) -> dict:
    commands = list(_commands(script, refs))
    backgrounds = [item for item in commands if _class(item[2]) == "ModifyBackgroundExtended"
                   and _value(item[2], "Id") == "Stills"]
    if len(backgrounds) != 2:
        raise ValueError("Expected exactly two Stills title background commands")
    zoom = _one((item for item in commands if _class(item[2]) == "AnimateActorExtended"), "title zoom")
    blurs = [item for item in commands if _class(item[2]) == "SpawnBlur"]
    if len(blurs) != 2 or any(_value(item[2], "ActorId") != "Stills" for item in blurs):
        raise ValueError("Expected the two Stills blur commands")
    variables = [item for item in commands if _class(item[2]) in {"SetCustomVariable", "BeginIf"}
                 and _value(item[2], "Expression") in VARIABLES]
    spawns = [item for item in commands if _class(item[2]) == "SpawnExtended"
              and _expression(item[2], "Path") == "titleScreenEffectId"]
    destroys = [item for item in commands if _class(item[2]) == "DestroySpawnedExtended"
                and _expression(item[2], "Path") == "titleScreenEffectId"]
    return dict(commands=commands, backgrounds=backgrounds, zoom=zoom, blurs=blurs,
                variables=variables, spawns=spawns, destroys=destroys)


def _set_positions(value, index: int):
    if isinstance(value, dict):
        if value.get("scriptPath") == SCRIPT_PATH and "lineIndex" in value:
            value["lineIndex"] = index
        for item in value.values():
            _set_positions(item, index)
    elif isinstance(value, list):
        for item in value:
            _set_positions(item, index)


def _merge(source: dict, pc: dict) -> tuple[dict, dict]:
    source_refs = _registry(source)
    pc_refs = _registry(pc)
    source_visual = _visuals(source, source_refs)
    pc_visual = _visuals(pc, pc_refs)
    if tuple(_value(item[2], "Expression") for item in source_visual["variables"]) != VARIABLES:
        raise ValueError("Switch title progression is not the expected >5 / >11 three-stage sequence")
    source_spawn = _one(source_visual["spawns"], "Switch title effect spawn")
    source_destroy = _one(source_visual["destroys"], "Switch title effect cleanup")
    # Preserve PC blur and zoom commands only when they actually match the source timing/parameters.
    for local, original in zip(pc_visual["blurs"] + [pc_visual["zoom"]],
                               source_visual["blurs"] + [source_visual["zoom"]]):
        if _semantic(local[2]) != _semantic(original[2]):
            raise ValueError("PC blur/zoom differs from Switch; review local visual edits before exporting")

    already_applied = bool(pc_visual["variables"] or pc_visual["spawns"] or pc_visual["destroys"])
    if already_applied:
        for key in ("variables", "spawns", "destroys"):
            if [_semantic(item[2]) for item in pc_visual[key]] != [_semantic(item[2]) for item in source_visual[key]]:
                raise ValueError("Partially modified or conflicting Switch title visuals in PC input")
        for local, original in zip(pc_visual["backgrounds"], source_visual["backgrounds"]):
            if _semantic(local[2]["data"][BACKGROUND_APPEARANCE]) != _semantic(original[2]["data"][BACKGROUND_APPEARANCE]):
                raise ValueError("Conflicting Switch title background appearance in PC input")
        output = deepcopy(pc)
        _refresh_background_hashes(output)
        return output, {"mode": "already_applied", "inserted_commands": 0}

    output = deepcopy(pc)
    refs = _registry(output)
    next_rid = max(refs) + 1
    cloned = {}

    def clone_ref(rid: int):
        nonlocal next_rid
        if rid in cloned:
            return {"rid": cloned[rid]}
        assigned = next_rid
        next_rid += 1
        cloned[rid] = assigned
        entry = deepcopy(source_refs[rid])
        entry["rid"] = assigned

        def remap(value):
            if isinstance(value, dict):
                if set(value) == {"rid"}:
                    return clone_ref(value["rid"])
                return {key: remap(item) for key, item in value.items()}
            if isinstance(value, list):
                return [remap(item) for item in value]
            return value

        entry["data"] = remap(entry["data"])
        refs[assigned] = entry
        output["references"]["RefIds"].append(entry)
        return {"rid": assigned}

    # Only appearance selection belongs to this port. Keep the PC command type,
    # timing, tint, position, scale and other parameters, including local edits.
    for local, original in zip(pc_visual["backgrounds"], source_visual["backgrounds"]):
        current = refs[local[2]["rid"]]
        current["data"][BACKGROUND_APPEARANCE] = deepcopy(original[2]["data"][BACKGROUND_APPEARANCE])

    goto = _one((item for item in pc_visual["commands"] if _class(item[2]) == "GotoModified"
                 and _expression(item[2], "Path") == "nextScenario"), "PC StartGame destination")
    first, last = source_visual["variables"][0][0], source_visual["variables"][-1][0]
    additions = {
        pc_visual["backgrounds"][0][1]["rid"]: [clone_ref(line["rid"]) for line in source["lines"][first:last + 1]],
        pc_visual["zoom"][1]["rid"]: [clone_ref(source_spawn[1]["rid"])],
        goto[1]["rid"]: [clone_ref(source_destroy[1]["rid"])],
    }
    lines = []
    for line in output["lines"]:
        lines.extend(additions.get(line["rid"], []))
        lines.append(line)
    output["lines"] = lines
    output["playlist"]["commands"] = []
    for index, line_ref in enumerate(lines):
        line = refs[line_ref["rid"]]
        line["data"]["lineIndex"] = index
        if "command" in line["data"]:
            command_ref = line["data"]["command"]
            _set_positions(refs[command_ref["rid"]]["data"], index)
            output["playlist"]["commands"].append(deepcopy(command_ref))

    changed_ids = {item[2]["rid"] for item in pc_visual["backgrounds"]}
    preserved = 0
    for _, _, command in pc_visual["commands"]:
        if command["rid"] not in changed_ids:
            if _semantic(command) != _semantic(refs[command["rid"]]):
                raise AssertionError("An unrelated PC command changed during title export")
            preserved += 1
    _refresh_background_hashes(output)
    return output, {"mode": "pc_flow_with_switch_visuals", "inserted_commands": 10,
                    "replaced_background_commands": 2, "preserved_pc_commands": preserved}


def export_scenario(source_bundle: Path, pc_json: Path, output_json: Path) -> dict:
    """Write standard typetree JSON atomically; never overwrite either source input."""
    source_bundle, pc_json, output_json = map(lambda path: Path(path).resolve(),
                                             (source_bundle, pc_json, output_json))
    if output_json in {source_bundle, pc_json}:
        raise ValueError("Title scenario output must be separate from both source inputs")
    source_bytes = source_bundle.read_bytes()
    pc_bytes = pc_json.read_bytes()
    env = UnityPy.load(source_bytes)
    candidates = []
    for obj in env.objects:
        if obj.type.name != "MonoBehaviour":
            continue
        tree = obj.read_typetree()
        if tree.get("m_Name") == "System_Title":
            candidates.append((obj.path_id, tree))
    path_id, source = _one(candidates, "Switch System_Title asset")
    pc = json.loads(pc_bytes.decode("utf-8-sig"))
    output, report = _merge(source, pc)
    payload = (json.dumps(output, ensure_ascii=False, indent=1, allow_nan=False) + "\n").encode("utf-8")
    changed = not output_json.is_file() or output_json.read_bytes() != payload
    if changed:
        output_json.parent.mkdir(parents=True, exist_ok=True)
        temporary = None
        try:
            with tempfile.NamedTemporaryFile(dir=output_json.parent, prefix=output_json.name + ".", delete=False) as stream:
                temporary = Path(stream.name)
                stream.write(payload)
            os.replace(temporary, output_json)
        finally:
            if temporary is not None and temporary.exists():
                temporary.unlink()
    report.update({"source_bundle": source_bundle.name, "source_path_id": str(path_id),
                   "source_sha256": hashlib.sha256(source_bytes).hexdigest(),
                   "pc_sha256": hashlib.sha256(pc_bytes).hexdigest(),
                   "output_sha256": hashlib.sha256(payload).hexdigest(), "written": changed,
                   "script_path": SCRIPT_PATH, "output_json": str(output_json),
                   "note": "PC inputs, BGM order, labels and menu commands are retained; only title visuals are ported."})
    return report
