"""Tracked epilogue recipe consumed by the existing content sync/build gates.

The editable PC ending is never written. Its one terminal command is changed in
an output graph only after comparison with the cached original PC ending.
"""
from __future__ import annotations

from copy import deepcopy
import hashlib
import json
from pathlib import Path
import re

from switch_story_scenario import _load_bundles, _write

REPO = Path(__file__).resolve().parents[2]
PAYLOAD = "content-overrides/fireworks-epilogue/"
PLAN = "res/.manosaba/fireworks-epilogue/plan.json"
OUTPUT = "res/.manosaba/fireworks-epilogue/"
SCRIPT = "System/System_FireworksEpilogue"
ENDING = "Act02_Chapter05/Act02_Chapter05_Adv09"
PC_SOURCE = "res/Assets/#WitchTrials/Scenarios/" + ENDING + ".asset.json"
TARGET = "Assets/GameContent/#WitchTrials/Scenarios/" + ENDING + ".asset"
SCRIPT_TARGET = "Assets/GameContent/#WitchTrials/Scenarios/" + SCRIPT + ".asset"
EMPTY_TARGET = "Assets/GameContent/#WitchTrials/FireworksEpilogue/Textures/EpilogueEmpty.png"
POSTER_TARGET = "Assets/GameContent/#WitchTrials/FireworksEpilogue/Textures/SourcePoster.png"
TEXT_TARGET = "Assets/GameContent/#WitchTrials/ManagedText/Scripts/" + SCRIPT + ".txt"
LOCALIZED_TEXT_TARGET = "Assets/GameContent/#WitchTrials/Localization/zh-Hans/Text/Scripts/" + SCRIPT + ".txt"
BASELINE = ("res/.imported-source/manosaba_Data/StreamingAssets/aa/StandaloneWindows64/"
            "naninovel-scripts_assets_naninovelscripts-act02_chapter05_advbad.bundle")


def contained(root: Path, relative: str) -> Path:
    if not isinstance(relative, str) or "\\" in relative or ":" in relative or any(
            part in ("", ".", "..") for part in relative.split("/")):
        raise ValueError("Unsafe epilogue recipe path: " + str(relative))
    path = (root / relative).resolve()
    if not path.is_relative_to(root.resolve()):
        raise ValueError("Epilogue recipe path leaves its root: " + relative)
    return path


def sha(path: Path) -> str:
    if not path.is_file():
        raise FileNotFoundError("Missing fireworks epilogue input: " + str(path))
    return hashlib.sha256(path.read_bytes()).hexdigest()


def _canonical(value):
    if isinstance(value, dict):
        return {k: _canonical(v) for k, v in value.items()
                if k not in {"rid", "playbackSpot", "spot", "lineIndex", "lineHash"}}
    if isinstance(value, list):
        return [_canonical(v) for v in value]
    return value


def _registry(script):
    refs = script.get("references", {}).get("RefIds", [])
    result = {row["rid"]: row for row in refs}
    if len(result) != len(refs) or script.get("path") != ENDING:
        raise ValueError("Expected the complete PC Adv09 graph with unique reference IDs")
    return result


def patch_ending(baseline: dict, current: dict) -> tuple[dict, dict]:
    """Replace exactly the terminal return; preserve all other locally edited data."""
    base_refs, refs = _registry(baseline), _registry(current)
    base_commands = [base_refs[r["rid"]] for r in baseline["playlist"]["commands"]]
    commands = [refs[r["rid"]] for r in current["playlist"]["commands"]]
    if not commands or not base_commands:
        raise ValueError("Missing Adv09 ending commands")
    is_return = lambda row: row["type"]["class"] == "ReturnToTitle"
    if not is_return(base_commands[-1]) or sum(map(is_return, base_commands)) != 1:
        raise ValueError("Original PC baseline no longer has the reviewed terminal ReturnToTitle")
    if not is_return(commands[-1]) or sum(map(is_return, commands)) != 1:
        raise ValueError("Adv09 ending conflict: expected one terminal ReturnToTitle")
    if _canonical(commands[-1]) != _canonical(base_commands[-1]):
        raise ValueError("Adv09 ending conflict: locally edited ReturnToTitle must be reviewed")
    anchors = base_commands[-4:-1]
    if ([row["type"]["class"] for row in anchors] != ["SetCustomVariable", "Gosub", "ClearBacklog"]
            or anchors[0]["data"].get("Expression", {}).get("value") != "g_gameProgress = max(12, g_gameProgress)"
            or anchors[1]["data"].get("Path", {}).get("value") != {
                "name": {"value": "System/System_Subroutine", "hasValue": 1},
                "value": {"value": "EndAdv", "hasValue": 1}}):
        raise ValueError("Original PC Adv09 baseline has different completion/cleanup anchors")
    # These anchors establish that the transition remains after the original
    # completion/cleanup, rather than after the earlier ED or before the coda.
    for expected in base_commands[-4:-1]:
        if not any(_canonical(row) == _canonical(expected) for row in commands[:-1]):
            raise ValueError("Adv09 ending conflict: completion/cleanup anchor was changed")
    anchor_positions = [next(i for i, row in enumerate(commands)
                             if _canonical(row) == _canonical(expected))
                        for expected in base_commands[-4:-1]]
    if anchor_positions != sorted(set(anchor_positions)):
        raise ValueError("Adv09 completion/cleanup anchors were reordered")
    output = deepcopy(current)
    out_refs = _registry(output)
    terminal = out_refs[commands[-1]["rid"]]
    # The route has the original ReturnToTitle parameters. Change only its type:
    # the disabled branch retains the original fade, minimum delay and wait,
    # plus all parameter expressions/spots and command control-flow metadata.
    if set(terminal["data"]) != {"playbackSpot", "indent", "ConditionalExpression",
                                  "FadeDuration", "MinDelayDuration", "Wait"}:
        raise ValueError("Original PC ReturnToTitle has an unreviewed parameter schema")
    terminal["type"] = {"class": "RouteFireworksEpilogue", "ns": "WitchTrials.Views", "asm": "Assembly-CSharp"}
    touched_lines = []
    for row in out_refs.values():
        if row["data"].get("command", {}).get("rid") == terminal["rid"]:
            row["data"]["lineHash"] = hashlib.sha256(
                json.dumps(_canonical(terminal), sort_keys=True).encode()).hexdigest()
            touched_lines.append(row["rid"])
    if len(touched_lines) != 1:
        raise ValueError("Adv09 terminal command must belong to exactly one script line")
    # Exact structural comparison of the whole graph, not just command counts.
    check = deepcopy(output)
    check_refs = _registry(check)
    check_refs[terminal["rid"]].clear()
    check_refs[terminal["rid"]].update(deepcopy(commands[-1]))
    for rid in touched_lines:
        check_refs[rid]["data"]["lineHash"] = refs[rid]["data"]["lineHash"]
    if check != current:
        raise AssertionError("Epilogue patch changed data outside the terminal command")
    return output, {"changed_commands": 1, "preserved_commands": len(commands) - 1,
                    "original_terminal": deepcopy(commands[-1]), "text_map_preserved": True,
                    "command_order_preserved": True, "terminal_rid": terminal["rid"],
                    "enabled_destination": SCRIPT, "disabled_destination": "ReturnToTitle",
                    "return_parameters_preserved": True}


def scenario_sources(repo: Path | None = None) -> set[str]:
    repo = Path(repo or REPO)
    path = contained(repo, PLAN)
    if not path.is_file():
        return set()
    plan = json.loads(path.read_text(encoding="utf-8"))
    if plan.get("version") != 1 or [r.get("pc_source") for r in plan.get("scenarios", [])] != [PC_SOURCE]:
        raise ValueError("Invalid fireworks epilogue ownership plan")
    return {PC_SOURCE.removeprefix("res/")}


def _write_json(path: Path, value) -> bool:
    return _write(path, (json.dumps(value, ensure_ascii=False, indent=2) + "\n").encode("utf-8"))


def compile_recipe(dialogue: dict) -> tuple[str, dict, list[dict]]:
    """Emit only reviewed commands; authored text is data, never executable .nani."""
    if (dialogue.get("version") != 1 or dialogue.get("script_path") != SCRIPT
            or dialogue.get("locale") != "zh-Hans" or not isinstance(dialogue.get("lines"), list)
            or not dialogue["lines"] or not isinstance(dialogue.get("credits"), list)):
        raise ValueError("Invalid fireworks epilogue dialogue schema")
    lines = ["; Generated by the tracked fireworks epilogue recipe.",
             "@beginFireworksEpilogue",
             "@processInput true set:Continue.true,Pause.true,Skip.true,ToggleSkip.true,AutoPlay.true,ToggleUI.true,ShowBacklog.true,Rollback.false",
             "@hideUI WitchBookButtonUI",
             "@showUI ControlPanel,AutoToggle",
             "@printer Normal.Default default:true",
             "@showPrinter Normal",
             "@back id:* visible:false time:0 wait:true",
             "@back EpilogueEmpty id:Stills pos:50,50 scale:1 tint:#ffffff visible:true time:0 wait:true",
             "@back Transparent subId:Overlay tint:#000000 time:1 wait:true"]
    text_map, pages, cues = {}, [], []
    for i, row in enumerate(dialogue["lines"]):
        if not isinstance(row, dict) or not re.fullmatch(r"Epilogue_[0-9]{3}", str(row.get("id", ""))):
            raise ValueError("Every epilogue page needs a stable Epilogue_NNN identifier")
        ident, text = row["id"], row.get("text")
        if ident in text_map or not isinstance(text, str) or not text.strip() or "\x00" in text:
            raise ValueError("Duplicate ID or empty/invalid epilogue text: " + ident)
        speaker, cue, color = row.get("speaker"), row.get("cue"), row.get("color")
        if speaker not in (None, "Ema", "Hiro") or cue not in (None, "reveal", "quiet", "together"):
            raise ValueError("Unsupported epilogue speaker/cue: " + ident)
        if color is not None and not re.fullmatch(r"#[0-9A-Fa-f]{6}", str(color)):
            raise ValueError("Invalid epilogue text color: " + ident)
        if cue:
            cues.append((i, cue))
            if cue == "reveal":
                # The preloaded stationary effect has no entrance tween. Start
                # the picture/music transitions together with this dialogue page.
                lines.extend(["@spawn TitleScreenEffect_3 params:stationary wait:true",
                              "@back 3_1 id:Stills pos:50,50 scale:1 tint:#ffffff visible:true time:2 wait:false",
                              "@fireworksMood reveal wait:false"])
            else:
                lines.append("@fireworksMood " + cue)
        rendered = text.replace("\r\n", "\n").replace("\n", "<br>")
        if color:
            rendered = f"<color={color}>{rendered}</color>"
        text_map[ident] = rendered
        if i == len(dialogue["lines"]) - 1:
            # Show the intact source-poster footer beside the final body page;
            # it adds neither an authored-text record nor an extra input wait.
            lines.append("@fireworksCredits")
        lines.append(f'@print "|#{ident}|" author:"{speaker or ""}" printer:Normal reset:true waitInput:true wait:true')
        pages.append({"id": ident, "speaker": speaker, "cue": cue, "source_text": text, "rendered_text": rendered})
    if [cue for _, cue in cues] != ["reveal", "quiet", "together"] or cues[0][0] != 3:
        raise ValueError("Expected three empty-scene pages, then reveal/quiet/together exactly once in order")
    for text in dialogue["credits"]:
        if not isinstance(text, str) or not text.strip() or "\x00" in text:
            raise ValueError("Invalid epilogue credit")
    lines.extend(["@hidePrinter time:0.5 wait:true", "@finishFireworksEpilogue", "@returnToTitle wait:true", ""])
    return "\n".join(lines), text_map, pages


def _recipe_inputs(repo: Path) -> list[Path]:
    names = [PAYLOAD + "dialogue.zh-Hans.json", PAYLOAD + "empty-rooftop.png", PAYLOAD + "source-poster.png", PC_SOURCE, BASELINE,
             "res/.manosaba/switch-title/plan.json", "tools/content/fireworks_epilogue.py",
             "tools/content/switch_story_scenario.py", "UnityProject/Assets/Editor/FireworksEpilogueContentBuilder.cs",
             "UnityProject/Assets/Editor/ScriptableObjectConverter.cs", "UnityProject/Assets/Editor/JsonToSerializedApplier.cs",
             "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/GalleryUi.cs",
             "UnityProject/Assets/Scripts/Game/GigaCreation.NaninovelExtender.ExtendedActors/GigaCreation.NaninovelExtender.ExtendedActors/ModifyBackgroundExtended.cs",
             "UnityProject/Packages/com.elringus.naninovel/Runtime/UI/CGGallery/CGGalleryPanel.cs",
             "UnityProject/Packages/com.elringus.naninovel/Runtime/Localization/TextLocalizer.cs",
             "UnityProject/Packages/com.elringus.naninovel/Runtime/Localization/ScriptLocalizationParser.cs",
             "UnityProject/Packages/com.elringus.naninovel/Runtime/ManagedText/ManagedTextUtils.cs"]
    names.extend("UnityProject/Assets/Shaders/ManosabaSwitchTitle/" + name
                 for name in ("Additive.shader", "Fog.shader", "Noise.shader"))
    runtime = "UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/"
    names.extend(runtime + name for name in ("FireworksEpilogueSession.cs", "BeginFireworksEpilogue.cs",
                 "SetFireworksEpilogueMood.cs", "FinishFireworksEpilogue.cs", "TitleScreenEffect.cs",
                 "ShowFireworksEpilogueCredits.cs", "FireworksEpilogueCreditsOverlay.cs", "OptionsUi.cs", "TitleUi.cs",
                 "RouteFireworksEpilogue.cs", "RouteFireworksEpilogue.cs.meta", "ReturnToTitle.cs"))
    names.extend("UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Models/" + name
                 for name in ("EnhancementSettings.cs", "EnhancementSettings.cs.meta"))
    return [contained(repo, name) for name in names]


def prepare(repo: Path | None = None, dry_run=False):
    repo = Path(repo or REPO).resolve()
    if not contained(repo, PAYLOAD + "dialogue.zh-Hans.json").is_file():
        if contained(repo, PLAN).exists():
            raise ValueError("Fireworks epilogue plan remains but its tracked dialogue input is missing")
        return {"status": "DISABLED"}
    inputs = {p.relative_to(repo).as_posix(): sha(p) for p in _recipe_inputs(repo)}
    dialogue = json.loads(contained(repo, PAYLOAD + "dialogue.zh-Hans.json").read_text(encoding="utf-8-sig"))
    script, text_map, pages = compile_recipe(dialogue)
    from PIL import Image
    with Image.open(contained(repo, PAYLOAD + "empty-rooftop.png")) as img:
        if img.format != "PNG" or img.width > 8192 or img.height > 8192 or img.width != 2 * img.height:
            raise ValueError("Epilogue empty background must be a PNG with the original 2:1 composition")
        size = [img.width, img.height]
        nearest = []
        for dimension in size:
            lower = 1 << (dimension.bit_length() - 1)
            nearest.append(lower if dimension - lower < lower * 2 - dimension else lower * 2)
        if nearest != [2048, 1024]:
            raise ValueError("Epilogue empty background must normalize to 2048x1024 with Unity ToNearest")
    with Image.open(contained(repo, PAYLOAD + "source-poster.png")) as img:
        if img.format != "PNG" or img.width > 8192 or img.height > 8192:
            raise ValueError("Epilogue source poster must be a PNG within the full-resolution texture limit")
        poster_size = [img.width, img.height]
    signature = hashlib.sha256(json.dumps(inputs, sort_keys=True).encode()).hexdigest()
    path = contained(repo, PLAN)
    if path.is_file():
        old = json.loads(path.read_text(encoding="utf-8"))
        if old.get("signature") == signature and old.get("generated_hashes") and all(
                contained(repo, n).is_file() and sha(contained(repo, n)) == h
                for n, h in old["generated_hashes"].items()):
            return {"status": "UNCHANGED", "signature": signature}
    original = _load_bundles([contained(repo, BASELINE)])
    if ENDING not in original:
        raise ValueError("Missing original PC Adv09 baseline")
    source = json.loads(contained(repo, PC_SOURCE).read_text(encoding="utf-8-sig"))
    merged, merge = patch_ending(original[ENDING][0], source)
    if dry_run:
        return {"status": "WOULD_PREPARE", "signature": signature, "pages": len(pages), "ending_merge": merge}
    generated_script = OUTPUT + "System_FireworksEpilogue.nani"
    generated_ending = OUTPUT + "Act02_Chapter05_Adv09.asset.json"
    _write(contained(repo, generated_script), script.encode("utf-8"))
    _write_json(contained(repo, generated_ending), merged)
    # Naninovel's SpriteBackground loads Texture2D and its transition mesh uses
    # texture dimensions, not Sprite PPU. Normalize only the imported texture;
    # retain the original authored PNG and the collaborator's asset GUID.
    leaf = {"source": PAYLOAD + "empty-rooftop.png", "target": EMPTY_TARGET, "sprite": False,
            "ppu": 50.0, "pivot": [0.5, 0.5], "border": [0, 0, 0, 0],
            "wrap_mode": 1, "size": size, "npot_scale": "ToNearest", "max_texture_size": 2048,
            "imported_size": [2048, 1024]}
    poster = {"source": PAYLOAD + "source-poster.png", "target": POSTER_TARGET, "sprite": False,
              "ppu": 100, "pivot": [0.5, 0.5], "border": [0, 0, 0, 0], "wrap_mode": 1, "size": poster_size}
    plan = {"version": 1, "signature": signature, "input_sha256": inputs,
            "leaves": [leaf, poster], "scenario": {"source": generated_script, "target": SCRIPT_TARGET,
                "script_path": SCRIPT, "text_map": text_map, "pages": pages},
            "credits": deepcopy(dialogue["credits"]),
            # TextMap alone is only used in the project's Japanese source locale.
            # Other locales first require a managed document to be loaded. Keep
            # the authored Chinese available as the normal source fallback too;
            # this is not a claim that the epilogue has additional translations.
            "text_documents": [{"target": target, "script_path": SCRIPT, "content_locale": "zh-Hans",
                                "records": deepcopy(text_map)}
                               for target in (TEXT_TARGET, LOCALIZED_TEXT_TARGET)],
            "scenarios": [{"source": generated_ending, "target": TARGET, "pc_source": PC_SOURCE,
                "pc_sha256": inputs[PC_SOURCE], "type": "Naninovel.Script", "assembly": "Elringus.Naninovel.Runtime",
                "merge": merge}],
            "addresses": [{"path": SCRIPT_TARGET, "address": "Naninovel/Scripts/" + SCRIPT,
                           "labels": ["Naninovel", "Scripts-System"]},
                          {"path": TARGET, "address": "Naninovel/Scripts/" + ENDING,
                           "labels": ["Naninovel", "Scripts-Act02_Chapter05"]},
                          {"path": EMPTY_TARGET, "address": "Naninovel/Backgrounds/Stills/EpilogueEmpty",
                           "labels": ["Naninovel"]},
                          {"path": POSTER_TARGET, "address": "Naninovel/FireworksEpilogue/SourcePoster",
                           "labels": ["Naninovel"]},
                          {"path": TEXT_TARGET, "address": "Naninovel/Text/Scripts/" + SCRIPT,
                           "labels": ["Naninovel"]},
                          {"path": LOCALIZED_TEXT_TARGET, "address": "Naninovel/Localization/zh-Hans/Text/Scripts/" + SCRIPT,
                           "labels": ["Naninovel"]}],
            "generated_hashes": {n: sha(contained(repo, n)) for n in (generated_script, generated_ending)}}
    _write_json(path, plan)
    return {"status": "PREPARED", "signature": signature, "pages": len(pages), "ending_merge": merge}
