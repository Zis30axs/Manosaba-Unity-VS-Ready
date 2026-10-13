"""Merge the Switch CG and cut-in presentation into locally edited PC scripts.

The original PC bundles are a mandatory three-way baseline. Only the reviewed
presentation classes are selected; text, save/input logic and script flow are
retained from the editable PC graph. Conflicting local edits fail closed.
"""
from __future__ import annotations

from collections import Counter
from contextlib import closing
from copy import deepcopy
import difflib
import gc
import hashlib
import json
import os
from pathlib import Path
import re
import tempfile

import UnityPy

PREFIX = "naninovel-scripts_assets_naninovelscripts-"
BASELINE_ROOT = "res/.imported-source/manosaba_Data/StreamingAssets/aa/StandaloneWindows64"
CG_IDS = {*(f"710_{i}" for i in range(1, 9)), "720_1", "730_1", "740_1", "750_1", "750_2", "760_1", "770_1"}
CG_SCRIPTS = {
    "Act01_Chapter03/Act01_Chapter03_Bad02",
    "Act01_Chapter05/Act01_Chapter05_Adv07", "Act01_Chapter05/Act01_Chapter05_Adv10",
    "Act01_Chapter05/Act01_Chapter05_Bad02", "Act02_Chapter03/Act02_Chapter03_Adv12",
    "Act02_Chapter04/Act02_Chapter04_Adv13", "Act02_Chapter04/Act02_Chapter04_Adv16",
    "Act02_Chapter04/Act02_Chapter04_Bad01",
}
DYNAMIC_OBJECTION_SCRIPT = "Act02_Chapter06/Act02_Chapter06_Trial09"
OBJECTION_VOICE_KINDS = {"Ema", "Hiro", "CreatureHiro"}
OBJECTION_VOICE_FIELDS = ("VoicePath", "Volume", "GroupPath", "AuthorId")
OBJECTION_PAIR_VOICES = ("CommonTrial_Ema06197", "CommonTrial_Hiro06086")
CG_CLASSES = {
    "ModifyBackgroundExtended", "ModifyCharacterExtended", "HideActors", "HideAllCharacters",
    "AnimateActorExtended", "ChangeActorPivot", "ChangeActorMaskability", "SpawnBlur",
    "Wait", "ResetText", "HidePrinter",
}
CUTIN_CLASSES = {
    "SpawnExtended", "DestroySpawnedExtended", "ModifyCharacterExtended",
    "GosubToSplitPerspectiveStageScreens", "HidePerspectiveStageScreen",
    "ModifyPerspectiveStageCamera", "WaitForNextFrame", "GosubToObjectionCutIn",
    "Wait", "ResetText", "PlayVoice", "StopVoice", "PlaySfxModified", "PlayBgm",
}
_SKIP = {"rid", "playbackSpot", "spot", "lineIndex", "lineHash"}


def _inside(root: Path, path: Path) -> Path:
    root, path = root.resolve(), path.resolve()
    if not path.is_relative_to(root):
        raise ValueError(f"Switch story path escapes its root: {path}")
    return path


def _bundle_names() -> list[str]:
    names = [f"act{act:02}_chapter{chapter:02}_{kind}" for act in (1, 2)
             for chapter in range(1, 6) for kind in ("advbad", "trial")]
    names += ["act02_chapter06_trial", "common", "debug", "system"]
    return [PREFIX + name + ".bundle" for name in names]


def baseline_inputs(repo: Path) -> list[Path]:
    """All mandatory cached PC inputs, also included in the outer recipe hash."""
    folder = _inside(Path(repo), Path(repo) / BASELINE_ROOT)
    paths = [_inside(folder, folder / name) for name in _bundle_names()]
    missing = [p.name for p in paths if not p.is_file()]
    if missing:
        raise ValueError("Missing original PC scenario baseline bundle(s): " + ", ".join(missing))
    return paths


def _semantic(value):
    if isinstance(value, list):
        return [_semantic(v) for v in value]
    if isinstance(value, dict):
        return {k: _semantic(v) for k, v in value.items() if k not in _SKIP}
    return value


def _key(command: dict) -> str:
    return json.dumps(_semantic(command), sort_keys=True, ensure_ascii=False, separators=(",", ":"), allow_nan=False)


def _class(command: dict) -> str:
    return command["type"]["class"]


def _value(command: dict, key: str):
    p = command["data"].get(key, {})
    return p.get("value") if p.get("hasValue") else None


def _raw(command: dict, key: str) -> str:
    return "".join(p.get("text", "") for p in command["data"].get(key, {}).get("raw", {}).get("parts", []))


def _cutin(command: dict) -> str | None:
    if _class(command) not in {"SpawnExtended", "DestroySpawnedExtended"}:
        return None
    path = _raw(command, "Path")
    return path if re.fullmatch(r"CutinSmall_[A-Za-z]+|CutinW[1-4]", path) else None


def _cg(command: dict) -> str | None:
    if _class(command) != "ModifyBackgroundExtended" or _value(command, "Id") != "Stills":
        return None
    value = _value(command, "AppearanceAndTransition") or {}
    name = value.get("name", {}).get("value")
    return name if name in CG_IDS else None


def _identity(command: dict):
    """Anchors identify preserved commands, never substitute their NS values."""
    text = command["data"].get("Text", {}).get("raw", {}).get("parts", [])
    ids = tuple(p["id"] for p in text if p.get("id"))
    return _class(command), ids


def _dialogue(command: dict):
    identity = _identity(command)
    return identity if identity[0] in {"PrintTextModified", "PrintDebateText"} and identity[1] else None


def _graph(script: dict):
    refs = {e["rid"]: e for e in script["references"]["RefIds"]}
    if len(refs) != len(script["references"]["RefIds"]):
        raise ValueError("Duplicate managed-reference IDs")
    commands = [refs[r["rid"]] for r in script["playlist"]["commands"]]
    line_commands, command_lines = [], {}
    for ref in script["lines"]:
        line = refs[ref["rid"]]
        data = line["data"]
        uses = [data["command"]] if "command" in data else data.get("inlinedCommands", [])
        for use in uses:
            line_commands.append(use["rid"])
            if use["rid"] in command_lines:
                raise ValueError("Command is owned by multiple script lines")
            command_lines[use["rid"]] = line
    if line_commands != [c["rid"] for c in commands]:
        raise ValueError("Scenario lines do not reconcile with its playlist")
    return refs, commands, command_lines


def _cutin_intervals(base: list[dict], source: list[dict]):
    base_dialogue = {}
    for i, c in enumerate(base):
        key = _dialogue(c)
        if key:
            base_dialogue.setdefault(key, []).append(i)
    intervals = set()
    for i, c in enumerate(source):
        if _class(c) != "SpawnExtended" or not _cutin(c):
            continue
        left = next((j for j in range(i - 1, -1, -1) if _dialogue(source[j])), -1)
        right = next((j for j in range(i + 1, len(source)) if _dialogue(source[j])), len(source))
        bounds = []
        for at in (left, right):
            if at == -1:
                bounds.append(-1)
            elif at == len(source):
                bounds.append(len(base))
            else:
                found = base_dialogue.get(_dialogue(source[at]), [])
                if len(found) != 1:
                    raise ValueError("Cut-in dialogue anchor is absent or ambiguous in the PC baseline")
                bounds.append(found[0])
        if bounds[0] >= bounds[1]:
            raise ValueError("Cut-in dialogue anchors changed order")
        intervals.add((bounds[0] + 1, bounds[1], left + 1, right))
    return sorted(intervals)


def _segment_patches(base, source, bounds, allowed):
    a, b, c, d = bounds
    fixed_b = [i for i in range(a, b) if _class(base[i]) not in allowed]
    fixed_s = [i for i in range(c, d) if _class(source[i]) not in allowed]
    matcher = difflib.SequenceMatcher(None, [_identity(base[i]) for i in fixed_b],
                                    [_identity(source[i]) for i in fixed_s], autojunk=False)
    anchors = [(a - 1, c - 1)]
    for block in matcher.get_matching_blocks():
        anchors.extend((fixed_b[block.a + k], fixed_s[block.b + k]) for k in range(block.size))
    anchors.append((b, d))
    patches = []
    for (bl, sl), (br, sr) in zip(anchors, anchors[1:]):
        bi = [i for i in range(bl + 1, br) if _class(base[i]) in allowed]
        si = [i for i in range(sl + 1, sr) if _class(source[i]) in allowed]
        diff = difflib.SequenceMatcher(None, [_key(base[i]) for i in bi], [_key(source[i]) for i in si], autojunk=False)
        for tag, x, y, u, v in diff.get_opcodes():
            if tag == "equal":
                continue
            removed = bi[x:y]
            anchor = removed[0] if removed else (bi[x] if x < len(bi) else br)
            patches.append({"remove": removed, "anchor": anchor, "insert": si[u:v]})
    return patches


def _set_positions(value, path, line):
    if isinstance(value, dict):
        if value.get("scriptPath") == path and "lineIndex" in value:
            value["lineIndex"] = line
        for nested in value.values():
            _set_positions(nested, path, line)
    elif isinstance(value, list):
        for nested in value:
            _set_positions(nested, path, line)


def _dynamic_objection_updates(path, source, baseline, local, source_lines, baseline_lines, local_lines):
    """Adapt the reviewed NS w/0 invocation in place, retaining the PC command.

    NS renamed CutInKind to CutInId and changed the command's base class. Only
    the two reviewed literal parameters cross that boundary; voice, flow and
    every other current PC field remain authoritative.
    """
    candidates = [i for i, c in enumerate(source) if _class(c) == "GosubToObjectionCutIn"
                  and _value(c, "CutInId") == "w"]
    if path != DYNAMIC_OBJECTION_SCRIPT:
        if candidates:
            raise ValueError("Unreviewed dynamic Switch objection invocation: " + path)
        return {}, []
    if len(candidates) != 1:
        raise ValueError("Expected exactly one reviewed dynamic Switch objection invocation")
    si = candidates[0]
    left = next((_dialogue(source[i]) for i in range(si - 1, -1, -1) if _dialogue(source[i])), None)
    right = next((_dialogue(source[i]) for i in range(si + 1, len(source)) if _dialogue(source[i])), None)
    if left is None or right is None or left == right:
        raise ValueError("Dynamic objection dialogue anchors are absent or ambiguous")

    def locate(commands):
        bounds = [[i for i, c in enumerate(commands) if _dialogue(c) == anchor] for anchor in (left, right)]
        if any(len(matches) != 1 for matches in bounds) or bounds[0][0] >= bounds[1][0]:
            raise ValueError("Dynamic objection dialogue anchors are absent, ambiguous or reordered")
        matches = [i for i in range(bounds[0][0] + 1, bounds[1][0])
                   if _class(commands[i]) == "GosubToObjectionCutIn"]
        if len(matches) != 1:
            raise ValueError("Dynamic objection command is absent or ambiguous between dialogue anchors")
        return matches[0]

    if locate(source) != si:
        raise ValueError("Dynamic objection source identity mismatch")
    bi, li = locate(baseline), locate(local)
    src, base, current = source[si], baseline[bi], local[li]
    for commands, lines, index in ((source, source_lines, si), (baseline, baseline_lines, bi)):
        command = commands[index]
        if (command["data"].get("indent", 0) or lines[command["rid"]]["data"].get("indent", 0)
                or command["data"].get("ConditionalExpression", {}).get("hasValue")):
            raise ValueError("Dynamic objection has an unreviewed conditional context")
    if local_lines[current["rid"]]["type"]["class"] != "CommandScriptLine":
        raise ValueError("Dynamic objection cannot modify an inline command")

    def literal(command, key, expected):
        parameter = command["data"].get(key, {})
        parts = parameter.get("raw", {}).get("parts", [])
        if (not parameter.get("hasValue") or type(parameter.get("value")) is not type(expected)
                or parameter.get("value") != expected or len(parts) != 1
                or parts[0].get("text") != str(expected) or parts[0].get("id") or parts[0].get("expression")):
            raise ValueError("Dynamic objection parameter differs from its reviewed literal: " + key)
        return parameter

    patched, fields = deepcopy(current), []
    for source_key, runtime_key, before, after in (("CutInId", "CutInKind", "Ema", "w"), ("Index", "Index", 2, 0)):
        literal(src, source_key, after)
        original = literal(base, runtime_key, before)
        desired = deepcopy(original)
        desired["value"] = after
        desired["raw"]["parts"][0]["text"] = str(after)
        actual = current["data"].get(runtime_key)
        if _semantic(actual) not in (_semantic(original), _semantic(desired)):
            raise ValueError("Local dynamic objection parameter edit conflicts with Switch story merge: " + runtime_key)
        if _semantic(actual) != _semantic(desired):
            # Keep the local parameter's location/metadata and alter only its literal value.
            patched["data"][runtime_key]["value"] = after
            patched["data"][runtime_key]["raw"]["parts"][0]["text"] = str(after)
            fields.append({"field": runtime_key, "source_field": source_key, "before": before, "after": after})
    evidence = {"command": "GosubToObjectionCutIn", "address": "ObjectionCutIn_w",
                "source_command": si, "baseline_command": bi, "local_command": li,
                "dialogue_anchors": [list(left[1]), list(right[1])], "fields": fields}
    return ({current["rid"]: patched} if fields else {}), [evidence]


def _has_objection_voice(command):
    return (_class(command) == "GosubToObjectionCutIn"
            and _value(command, "CutInId") in OBJECTION_VOICE_KINDS
            and command["data"].get("VoicePath", {}).get("hasValue"))


def _voice_parameter_key(parameter):
    # Missing fields in the old PC class and serialized unassigned parameters
    # have identical optional semantics. Assigned expressions remain distinct.
    if not parameter or not parameter.get("hasValue"):
        return None
    parts = parameter.get("raw", {}).get("parts", [])
    if all(not part.get("id") and not part.get("expression") for part in parts):
        value = parameter.get("value")
        raw = "".join(part.get("text", "") for part in parts)
        consistent = not parts or raw == str(value)
        if type(value) in (int, float) and raw:
            try:
                consistent = float(raw) == value
            except ValueError:
                consistent = False
        if consistent:
            return {"literal": value}
    return _semantic(parameter)


def _objection_voice_updates(path, source, baseline, local, source_lines, baseline_lines, local_lines):
    """Import assigned NS voice parameters at unique dialogue anchors only.

    The existing PC kind, index, navigation, conditions and separate PlayVoice
    commands remain untouched. In particular, NS index 0 can mean PC index 3;
    the w invocation has two independent voices and is deliberately excluded.
    """
    updates, evidence = {}, []
    for si, src in enumerate(source):
        if not _has_objection_voice(src):
            continue
        left = next((_dialogue(source[i]) for i in range(si - 1, -1, -1) if _dialogue(source[i])), None)
        right = next((_dialogue(source[i]) for i in range(si + 1, len(source)) if _dialogue(source[i])), None)
        if left is None or right is None or left == right:
            raise ValueError("Objection voice dialogue anchors are absent or ambiguous: " + path)

        def locate(commands):
            bounds = [[i for i, c in enumerate(commands) if _dialogue(c) == anchor] for anchor in (left, right)]
            if any(len(matches) != 1 for matches in bounds) or bounds[0][0] >= bounds[1][0]:
                raise ValueError("Objection voice dialogue anchors are absent, ambiguous or reordered: " + path)
            matches = [i for i in range(bounds[0][0] + 1, bounds[1][0])
                       if _class(commands[i]) == "GosubToObjectionCutIn"]
            if len(matches) != 1:
                raise ValueError("Objection voice command is absent or ambiguous between dialogue anchors: " + path)
            return matches[0]

        if locate(source) != si:
            raise ValueError("Objection voice source identity mismatch: " + path)
        bi, li = locate(baseline), locate(local)
        base, current = baseline[bi], local[li]
        kind = _value(src, "CutInId")
        if _value(base, "CutInKind") != kind or _semantic(current["data"].get("CutInKind")) != _semantic(base["data"].get("CutInKind")):
            raise ValueError("Objection voice character identity changed or conflicts with local edit: " + path)
        for command, lines in ((src, source_lines), (base, baseline_lines)):
            if (command["data"].get("indent", 0) or lines[command["rid"]]["data"].get("indent", 0)
                    or command["data"].get("ConditionalExpression", {}).get("hasValue")):
                raise ValueError("Objection voice has an unreviewed conditional context: " + path)
        if local_lines[current["rid"]]["type"]["class"] != "CommandScriptLine":
            raise ValueError("Objection voice cannot modify an inline command: " + path)
        patched, fields = deepcopy(current), []
        for field in OBJECTION_VOICE_FIELDS:
            desired = src["data"].get(field, {})
            if not desired.get("hasValue"):
                continue  # An unspecified NS option cannot erase a local option.
            value, parts = desired.get("value"), desired.get("raw", {}).get("parts", [])
            valid_value = (type(value) in (int, float) and 0 <= value <= 1) if field == "Volume" else isinstance(value, str) and bool(value)
            if not valid_value or any(part.get("id") or part.get("expression") for part in parts):
                raise ValueError("Objection voice parameter is not a reviewed static literal: " + field)
            raw = "".join(part.get("text", "") for part in parts)
            if (field != "Volume" and raw != value) or (field == "Volume" and raw and float(raw) != value):
                raise ValueError("Objection voice raw text disagrees with its literal value: " + field)
            original, actual = base["data"].get(field), current["data"].get(field)
            if _voice_parameter_key(actual) not in (_voice_parameter_key(original), _voice_parameter_key(desired)):
                raise ValueError(f"Local objection voice parameter edit conflicts with Switch story merge: {path}, {field}")
            if _voice_parameter_key(actual) == _voice_parameter_key(desired):
                continue
            replacement = deepcopy(desired)
            if actual:
                # Retain local parameter metadata, including source location.
                replacement = deepcopy(actual)
                for key in ("value", "hasValue", "raw"):
                    replacement[key] = deepcopy(desired[key])
            patched["data"][field] = replacement
            fields.append({"field": field, "before": _value(base, field), "after": value})
        if fields:
            if current["rid"] in updates:
                raise ValueError("Overlapping objection voice parameter updates: " + path)
            updates[current["rid"]] = patched
        voice = _value(src, "VoicePath")
        evidence.append({"command": "GosubToObjectionCutIn", "kind": kind,
                         "voice_path": voice, "address": "Naninovel/Voice/" + voice,
                         "source_command": si, "baseline_command": bi, "local_command": li,
                         "dialogue_anchors": [list(left[1]), list(right[1])], "fields": fields,
                         "parameters": {field: deepcopy(patched["data"][field]) for field in OBJECTION_VOICE_FIELDS
                                        if field in patched["data"]}})
    return updates, evidence


def _objection_voice_pair(path, source, baseline, local, source_lines, baseline_lines, local_lines, dynamic):
    """Coalesce only the reviewed consecutive voices immediately before w.

    Ordinary PlayVoice honors PreventOverlap; the dedicated command starts
    these two existing voices together without changing that global policy.
    No NS audio is inserted and conflicting local edits are rejected.
    """
    if path != DYNAMIC_OBJECTION_SCRIPT:
        return {}, set(), []
    if len(dynamic) != 1:
        raise ValueError("Objection voice pair requires the unique reviewed w invocation")
    row = dynamic[0]
    si, bi, li = (row[key] for key in ("source_command", "baseline_command", "local_command"))
    for commands, lines, at in ((source, source_lines, si), (baseline, baseline_lines, bi)):
        pair = commands[at - 2:at] if at >= 2 else []
        if (len(pair) != 2 or any(_class(c) != "PlayVoice" for c in pair)
                or tuple(_value(c, "VoicePath") for c in pair) != OBJECTION_PAIR_VOICES):
            raise ValueError("Objection voice pair differs from the reviewed source/baseline voices")
        for command in pair:
            if (command["data"].get("indent", 0) or lines[command["rid"]]["data"].get("indent", 0)
                    or command["data"].get("ConditionalExpression", {}).get("hasValue")
                    or lines[command["rid"]]["type"]["class"] != "CommandScriptLine"):
                raise ValueError("Objection voice pair has an unreviewed conditional/inline context")
    if [_key(c) for c in source[si-2:si]] != [_key(c) for c in baseline[bi-2:bi]]:
        raise ValueError("Objection voice pair source parameters changed relative to the PC baseline")
    original = baseline[bi - 2:bi]
    pair_type = {"class": "PlayObjectionVoicePair", "ns": "WitchTrials.Views", "asm": "Assembly-CSharp"}
    all_fields = (*OBJECTION_VOICE_FIELDS, *("Second" + field for field in OBJECTION_VOICE_FIELDS))
    def without_parameters(command):
        copy = deepcopy(command)
        for field in all_fields:
            copy["data"].pop(field, None)
        return _key(copy)
    preserved = []
    updates, removed = {}, set()
    if li and _class(local[li - 1]) == "PlayObjectionVoicePair":
        current = local[li - 1]
        expected = deepcopy(original[0]); expected["type"] = pair_type
        if (without_parameters(current) != without_parameters(expected)
                or tuple(_value(current, field) for field in ("VoicePath", "SecondVoicePath")) != OBJECTION_PAIR_VOICES):
            raise ValueError("Local objection voice pair edit conflicts with Switch story merge")
        first = li - 1
        combined = deepcopy(current)
    else:
        actual = local[li - 2:li] if li >= 2 else []
        if (len(actual) != 2 or [without_parameters(c) for c in actual] != [without_parameters(c) for c in original]
                or any(_voice_parameter_key(c["data"].get("VoicePath")) != _voice_parameter_key(b["data"].get("VoicePath"))
                       for c, b in zip(actual, original))):
            raise ValueError("Local objection voice pair edit conflicts with Switch story merge")
        first = li - 2
        for command in actual:
            line = local_lines[command["rid"]]
            if line["type"]["class"] != "CommandScriptLine" or line["data"].get("indent", 0):
                raise ValueError("Local objection voice pair has an unreviewed conditional/inline context")
        combined = deepcopy(actual[0]); combined["type"] = pair_type
        for field in OBJECTION_VOICE_FIELDS:
            if field in actual[1]["data"]:
                combined["data"]["Second" + field] = deepcopy(actual[1]["data"][field])
        updates[combined["rid"]] = combined
        removed.add(li - 1)
    first_line = local_lines[local[first]["rid"]]
    if first_line["type"]["class"] != "CommandScriptLine" or first_line["data"].get("indent", 0):
        raise ValueError("Local objection voice pair has an unreviewed conditional/inline context")
    for prefix, original_command in zip(("", "Second"), original):
        for field in OBJECTION_VOICE_FIELDS:
            if _semantic(combined["data"].get(prefix + field)) != _semantic(original_command["data"].get(field)):
                preserved.append(prefix + field)
    evidence = {"command": "PlayObjectionVoicePair", "source_commands": [si - 2, si - 1],
                "baseline_commands": [bi - 2, bi - 1], "local_command": first,
                "voice_paths": list(OBJECTION_PAIR_VOICES),
                "addresses": ["Naninovel/Voice/" + voice for voice in OBJECTION_PAIR_VOICES],
                "dialogue_anchors": row["dialogue_anchors"], "replaced_commands": 2, "result_commands": 1,
                "local_parameters_preserved": preserved}
    return updates, removed, [evidence]


def merge_scenario(source: dict, base: dict, local: dict) -> tuple[dict, dict]:
    path = source["path"]
    if path != base["path"] or path != local["path"] or not re.fullmatch(r"Act\d{2}_Chapter\d{2}/[A-Za-z0-9_]+", path):
        raise ValueError("Switch story scenario identity mismatch")
    sr, sc, sl = _graph(source)
    _, bc, bl = _graph(base)
    lr, lc, ll = _graph(local)
    parameter_updates, dynamic_evidence = _dynamic_objection_updates(path, sc, bc, lc, sl, bl, ll)
    voice_updates, voice_evidence = _objection_voice_updates(path, sc, bc, lc, sl, bl, ll)
    if parameter_updates.keys() & voice_updates.keys():
        raise ValueError("Dynamic and single-person objection parameter updates overlap")
    parameter_updates.update(voice_updates)
    pair_updates, pair_removed, pair_evidence = _objection_voice_pair(path, sc, bc, lc, sl, bl, ll, dynamic_evidence)
    command_updates = {**parameter_updates, **pair_updates}
    patches = []
    if path in CG_SCRIPTS:
        patches += _segment_patches(bc, sc, (0, len(bc), 0, len(sc)), CG_CLASSES)
    intervals = _cutin_intervals(bc, sc)
    for bounds in intervals:
        patches += _segment_patches(bc, sc, bounds, CUTIN_CLASSES)
    if not patches and not dynamic_evidence and not voice_evidence:
        raise ValueError(f"Expected Switch story presentation changes in {path}")

    mapping = {}
    matcher = difflib.SequenceMatcher(None, [_key(c) for c in bc], [_key(c) for c in lc], autojunk=False)
    for tag, a, b, c, d in matcher.get_opcodes():
        if tag == "equal":
            mapping.update((a + i, c + i) for i in range(b - a))
        elif tag == "replace" and b - a == d - c and all(_identity(bc[a+i]) == _identity(lc[c+i]) for i in range(b-a)):
            mapping.update((a + i, c + i) for i in range(b - a))
    mapping[len(bc)] = len(lc)
    removed, insertions = set(), {}
    for patch in patches:
        for index in patch["remove"]:
            if index not in mapping or _key(bc[index]) != _key(lc[mapping[index]]):
                raise ValueError(f"Local presentation edit conflicts with Switch story merge: {path}, PC command {index}")
            if bc[index]["data"].get("indent", 0) or bc[index]["data"].get("ConditionalExpression", {}).get("hasValue"):
                raise ValueError("Story presentation removal crosses an unreviewed conditional context")
            local_index = mapping[index]
            if local_index in removed:
                raise ValueError("Overlapping Switch story presentation patches")
            removed.add(local_index)
        if patch["insert"]:
            if patch["anchor"] not in mapping:
                raise ValueError(f"Local edit removed a Switch story insertion anchor: {path}")
            local_anchor = mapping[patch["anchor"]]
            if local_anchor < len(lc):
                anchor_command = lc[local_anchor]
                if anchor_command["data"].get("indent", 0) or ll[anchor_command["rid"]]["data"].get("indent", 0):
                    raise ValueError("Local conditional context conflicts with a Switch story insertion")
            for index in patch["insert"]:
                if (sc[index]["data"].get("indent", 0) or sl[sc[index]["rid"]]["data"].get("indent", 0)
                        or sc[index]["data"].get("ConditionalExpression", {}).get("hasValue")):
                    raise ValueError("Switch presentation has an unreviewed conditional context")
            insertions.setdefault(local_anchor, []).extend(patch["insert"])

    if removed & pair_removed or any(lc[i]["rid"] in command_updates for i in removed):
        raise ValueError("Objection voice pair overlaps a whole-command presentation replacement")
    removed.update(pair_removed)
    output = deepcopy(local)
    out_refs = {e["rid"]: e for e in output["references"]["RefIds"]}
    if any(lc[i]["rid"] in command_updates for i in removed):
        raise ValueError("Objection parameter update overlaps a whole-command presentation replacement")
    for rid, command in command_updates.items():
        out_refs[rid] = command
        line = out_refs[ll[rid]["rid"]]
        line["data"]["lineHash"] = hashlib.sha256(("switch-story-v1\0" + _key(command)).encode()).hexdigest()
    next_rid = max(out_refs, default=999) + 1
    imported = {}
    def clone_rid(rid):
        nonlocal next_rid
        if rid in imported:
            return imported[rid]
        if rid < 0:
            return rid
        if rid not in sr:
            raise ValueError("Switch graph contains an unresolved managed reference")
        new_rid, next_rid = next_rid, next_rid + 1
        imported[rid] = new_rid
        item = deepcopy(sr[rid])
        item["rid"] = new_rid
        def remap(value):
            if isinstance(value, dict):
                if set(value) == {"rid"}:
                    value["rid"] = clone_rid(value["rid"])
                else:
                    for x in value.values(): remap(x)
            elif isinstance(value, list):
                for x in value: remap(x)
        remap(item["data"])
        out_refs[new_rid] = item
        return new_rid

    remove_lines = set()
    for i in removed:
        line = ll[lc[i]["rid"]]
        if line["type"]["class"] != "CommandScriptLine":
            raise ValueError("Presentation patch attempted to remove a dialogue or inline command")
        remove_lines.add(line["rid"])
    by_line = {}
    for index, uses in sorted(insertions.items()):
        anchor = ll[lc[index]["rid"]]["rid"] if index < len(lc) else None
        for source_index in uses:
            c = sc[source_index]
            line = sl[c["rid"]]
            if line["type"]["class"] != "CommandScriptLine":
                raise ValueError("Presentation patch attempted to import an inline command")
            new_line = clone_rid(line["rid"])
            out_refs[new_line]["data"]["lineHash"] = hashlib.sha256(("switch-story-v1\0" + _key(c)).encode()).hexdigest()
            by_line.setdefault(anchor, []).append({"rid": new_line})
    lines = []
    for line in output["lines"]:
        lines += by_line.get(line["rid"], [])
        if line["rid"] not in remove_lines:
            lines.append(line)
    lines += by_line.get(None, [])
    playlist = []
    for index, ref in enumerate(lines):
        entry = out_refs[ref["rid"]]
        entry["data"]["lineIndex"] = index
        _set_positions(entry["data"], path, index)
        data = entry["data"]
        uses = [data["command"]] if "command" in data else data.get("inlinedCommands", [])
        for use in uses:
            _set_positions(out_refs[use["rid"]]["data"], path, index)
            playlist.append(deepcopy(use))
    output["lines"], output["playlist"]["commands"] = lines, playlist
    reachable = set()
    def visit(value):
        if isinstance(value, dict):
            if set(value) == {"rid"}:
                rid = value["rid"]
                if rid >= 0 and rid not in reachable:
                    if rid not in out_refs: raise ValueError("Merged graph contains an unresolved reference")
                    reachable.add(rid); visit(out_refs[rid]["data"])
            else:
                for x in value.values(): visit(x)
        elif isinstance(value, list):
            for x in value: visit(x)
    visit(lines); visit(playlist)
    output["references"]["RefIds"] = [v for k, v in out_refs.items() if k in reachable]
    _, oc, _ = _graph(output)
    if output["textMap"] != local["textMap"]:
        raise ValueError("Story merge modified the local text map")
    # Every retained PC command must survive in its original order and semantics.
    retained = [_key(c) for i, c in enumerate(lc) if i not in removed and c["rid"] not in command_updates]
    retained_rids = {c["rid"] for i, c in enumerate(lc) if i not in removed and c["rid"] not in command_updates}
    if [_key(c) for c in oc if c["rid"] in retained_rids] != retained:
        raise ValueError("Story merge altered an unowned local command")
    expected_retained = [_key(command_updates.get(c["rid"], c)) for i, c in enumerate(lc) if i not in removed]
    local_rids = {c["rid"] for c in lc}
    if [_key(c) for c in oc if c["rid"] in local_rids] != expected_retained:
        raise ValueError("Story merge altered fields outside its reviewed parameter updates")
    dynamic_calls = Counter("ObjectionCutIn_w" for c in oc
                            if _class(c) == "GosubToObjectionCutIn" and _value(c, "CutInKind") == "w")
    if dynamic_calls != Counter(e["address"] for e in dynamic_evidence):
        raise ValueError("Merged dynamic objection usage differs from its reviewed Switch source")
    expected_cg = Counter(_cg(c) for c in sc if _cg(c))
    expected_cutins = Counter(_cutin(c) for c in sc if _class(c) == "SpawnExtended" and _cutin(c))
    if Counter(_cg(c) for c in oc if _cg(c)) != expected_cg:
        raise ValueError("Merged story CG usage differs from the Switch source")
    if Counter(_cutin(c) for c in oc if _class(c) == "SpawnExtended" and _cutin(c)) != expected_cutins:
        raise ValueError("Merged story cut-in usage differs from the Switch source")
    for item in voice_evidence + pair_evidence:
        rid = lc[item["local_command"]]["rid"]
        index, command = next((i, c) for i, c in enumerate(oc) if c["rid"] == rid)
        item["merged_command"] = index
        item["line_index"] = command["data"]["playbackSpot"]["lineIndex"]
        fields = OBJECTION_VOICE_FIELDS if item["command"] == "GosubToObjectionCutIn" else (*OBJECTION_VOICE_FIELDS, *("Second" + f for f in OBJECTION_VOICE_FIELDS))
        item["parameters"] = {field: deepcopy(command["data"][field]) for field in fields if field in command["data"]}
    audio = sorted({(_class(sc[i]), _raw(sc[i], key)) for uses in insertions.values() for i in uses
                    for key in ("VoicePath", "SfxPath", "BgmPath") if _raw(sc[i], key)}
                   | {("GosubToObjectionCutIn", item["voice_path"]) for item in voice_evidence}
                   | {("PlayObjectionVoicePair", voice) for item in pair_evidence for voice in item["voice_paths"]})
    report = {"script_path": path, "pc_commands": len(lc), "merged_commands": len(oc),
              "removed_commands": len(removed), "inserted_commands": sum(map(len, insertions.values())),
              "patches": len(patches), "cg_calls": dict(expected_cg), "cutin_calls": dict(expected_cutins),
              "audio_references": [{"command": k, "path": v} for k, v in audio],
              "dynamic_objection_calls": dict(dynamic_calls), "parameter_updates": dynamic_evidence,
              "objection_voice_calls": voice_evidence,
              "objection_voice_updated_commands": len(voice_updates),
              "objection_voice_pairs": pair_evidence,
              "objection_voice_pair_updated_commands": len(pair_updates),
              "parameter_updated_commands": len(parameter_updates),
              "text_preserved": True, "retained_commands_verified": len(retained)}
    return output, report


def _iter_bundle_scripts(paths):
    """Read one bundle at a time; do not retain every decoded scenario graph."""
    seen = set()
    for bundle in paths:
        raw = bundle.read_bytes()
        digest = hashlib.sha256(raw).hexdigest()
        env = UnityPy.load(raw)
        raw = None
        obj = script = None
        try:
            for obj in env.objects:
                if obj.type.name != "MonoBehaviour":
                    continue
                script = obj.read_typetree()
                if not {"path", "lines", "playlist", "references"}.issubset(script):
                    continue
                path = script["path"]
                if path in seen:
                    raise ValueError(f"Duplicate script identity in cached bundles: {path}")
                seen.add(path)
                yield script, bundle, digest, obj.path_id
                script = None
        finally:
            # UnityPy environments contain cycles. Release each bundle before
            # opening the next rather than relying on allocation-count GC.
            obj = script = env = None
            gc.collect()


def _load_bundles(paths):
    # Retained for the single-bundle epilogue baseline reader.
    return {s["path"]: (s, bundle, digest, path_id)
            for s, bundle, digest, path_id in _iter_bundle_scripts(paths)}


def _dump_json(path: Path, value) -> None:
    # json.dump iterates encoder chunks instead of allocating another full
    # Unicode document and UTF-8 byte array beside the decoded graph.
    with path.open("w", encoding="utf-8", newline="\n") as stream:
        json.dump(value, stream, ensure_ascii=False, indent=1, allow_nan=False)
        stream.write("\n")


def _file_sha256(path: Path) -> str:
    with path.open("rb") as stream:
        return hashlib.file_digest(stream, "sha256").hexdigest()


def _publish_staged(path: Path, staged: Path) -> bool:
    if path.is_file() and path.stat().st_size == staged.stat().st_size:
        with path.open("rb") as current, staged.open("rb") as candidate:
            while True:
                chunk = current.read(1024 * 1024)
                if chunk != candidate.read(1024 * 1024):
                    break
                if not chunk:
                    return False
    path.parent.mkdir(parents=True, exist_ok=True)
    os.replace(staged, path)
    return True


def _write(path: Path, payload: bytes) -> bool:
    if path.is_file() and path.read_bytes() == payload:
        return False
    path.parent.mkdir(parents=True, exist_ok=True)
    temporary = None
    try:
        with tempfile.NamedTemporaryFile(dir=path.parent, prefix=path.name + ".", delete=False) as stream:
            temporary = Path(stream.name)
            stream.write(payload)
        os.replace(temporary, path)
    finally:
        if temporary is not None and temporary.exists(): temporary.unlink()
    return True


def export_scenarios(repo: Path, source_aa: Path, output_dir: Path) -> list[dict]:
    repo = Path(repo).resolve()
    source_aa = _inside(repo, Path(source_aa))
    output_dir = _inside(repo, Path(output_dir))
    original_paths = baseline_inputs(repo)
    source_paths = [_inside(source_aa, source_aa / n) for n in _bundle_names()]
    missing = [p.name for p in source_paths if not p.is_file()]
    if missing:
        raise ValueError("Missing cached Switch scenario bundle(s): " + ", ".join(missing))
    output_dir.mkdir(parents=True, exist_ok=True)
    # Keep only small identity/report records in memory. Temporary JSON files
    # hold the selected source graphs, PC baselines and validated outputs until
    # *all* merges and aggregate counts pass, including on a low-memory host.
    with tempfile.TemporaryDirectory(prefix=".scenario-prepare-", dir=output_dir) as folder:
        staging = _inside(output_dir, Path(folder))
        sources, visual_targets, voice_targets = {}, set(), set()
        with closing(_iter_bundle_scripts(source_paths)) as scripts:
            for index, (script, bundle, digest, path_id) in enumerate(scripts):
                path = script["path"]
                if path.startswith("Act"):
                    commands = _graph(script)[1]
                    if any(_cg(c) or (_class(c) == "SpawnExtended" and _cutin(c))
                           or (_class(c) == "GosubToObjectionCutIn" and _value(c, "CutInId") == "w") for c in commands):
                        visual_targets.add(path)
                    if any(_has_objection_voice(c) for c in commands):
                        voice_targets.add(path)
                    del commands
                    if path in visual_targets or path in voice_targets:
                        staged = staging / f"source-{index}.json"
                        _dump_json(staged, script)
                        sources[path] = (staged, bundle, digest, path_id)
                del script
        if not CG_SCRIPTS.issubset(visual_targets) or len(visual_targets) != 50:
            raise ValueError("Switch story source does not match the reviewed 50-scenario presentation recipe")
        targets = visual_targets | voice_targets
        needed = {sources[p][1].name for p in targets}
        baselines = {}
        with closing(_iter_bundle_scripts([p for p in original_paths if p.name in needed])) as scripts:
            for index, (script, bundle, digest, path_id) in enumerate(scripts):
                path = script["path"]
                if path in targets:
                    staged = staging / f"baseline-{index}.json"
                    _dump_json(staged, script)
                    baselines[path] = (staged, bundle, digest, path_id)
                del script
        prepared, totals = [], Counter()
        for index, path in enumerate(sorted(targets)):
            if path not in baselines:
                raise ValueError(f"Missing original PC baseline Script: {path}")
            source_file, bundle, source_sha, path_id = sources[path]
            baseline_file, pc_bundle, baseline_sha, _ = baselines[path]
            pc_file = _inside(repo / "res/Assets/#WitchTrials/Scenarios", repo / f"res/Assets/#WitchTrials/Scenarios/{path}.asset.json")
            if not pc_file.is_file():
                raise ValueError(f"Missing editable PC scenario: {pc_file}")
            with source_file.open(encoding="utf-8") as stream:
                source = json.load(stream)
            with baseline_file.open(encoding="utf-8") as stream:
                baseline = json.load(stream)
            pc_bytes = pc_file.read_bytes()
            merged, report = merge_scenario(source, baseline, json.loads(pc_bytes))
            target_file = _inside(output_dir, output_dir / "scenarios" / (path + ".asset.json"))
            payload_file = staging / f"merged-{index}.json"
            _dump_json(payload_file, merged)
            report.update({"source_bundle": bundle.name, "source_sha256": source_sha, "source_path_id": str(path_id),
                           "baseline_bundle": pc_bundle.relative_to(repo).as_posix(), "baseline_sha256": baseline_sha,
                           "output_sha256": _file_sha256(payload_file)})
            totals["cg_calls"] += sum(report["cg_calls"].values())
            totals["cutin_calls"] += sum(report["cutin_calls"].values())
            totals["dynamic_objection_calls"] += sum(report["dynamic_objection_calls"].values())
            totals["objection_voice_calls"] += len(report["objection_voice_calls"])
            totals["objection_voice_pairs"] += len(report["objection_voice_pairs"])
            record = {"source": target_file.relative_to(repo).as_posix(),
                      "target": f"Assets/GameContent/#WitchTrials/Scenarios/{path}.asset",
                      "type": "Naninovel.Script", "assembly": "Elringus.Naninovel.Runtime",
                      "pc_source": pc_file.relative_to(repo).as_posix(), "pc_sha256": hashlib.sha256(pc_bytes).hexdigest(),
                      "merge": report}
            prepared.append((target_file, payload_file, record))
            del source, baseline, pc_bytes, merged
        if totals != Counter(cg_calls=22, cutin_calls=62, dynamic_objection_calls=1, objection_voice_calls=189, objection_voice_pairs=1):
            raise ValueError(f"Switch story presentation call counts changed: {dict(totals)}")
        # Validate every merge before publishing any new scenario output.
        for target_file, payload_file, record in prepared:
            _publish_staged(target_file, payload_file)
        return [record for _, _, record in prepared]
