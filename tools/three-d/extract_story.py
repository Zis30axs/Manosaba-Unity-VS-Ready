"""Extract the prison opening from the real Naninovel command playlist.

The generated JSON is prototype input, not a replacement for the game scenario.
Only the complete Adv02 segment is included; Adv03 opens the cell door and exits.
"""

from __future__ import annotations

import hashlib
import json
import re
from pathlib import Path


ROOT = Path(__file__).resolve().parents[2]
SCRIPT = "Act01_Chapter01/Act01_Chapter01_Adv02"
RES = ROOT / "res"
SCENARIO = RES / f"Assets/#WitchTrials/Scenarios/{SCRIPT}.asset.json"
LOCALIZATION = RES / f"Assets/#WitchTrials/Localization/zh-Hans/Text/Scripts/{SCRIPT}.txt"
OUTPUT = ROOT / ".3d-model/generated/story.json"

SPEAKERS = {
    "": "旁白",
    "Ema": "樱羽艾玛",
    "Hiro": "二阶堂希罗",
    "Warden": "典狱长",
    "Unknown": "？？？",
}

# Blocking transitions keyed to actual text command identities. These are only
# prototype stage cues; all displayed text and command order come from the game.
STAGE_START = {
    "0101Adv02_Ema001": "wake",
    "0101Adv02_Ema008": "bars",
    "0101Adv02_Unknown004": "hiro_bunk",
    "0101Adv02_Narrative026": "hiro_floor",
    "0101Adv02_Narrative031": "monitor",
    "0101Adv02_Narrative032": "after_monitor",
    "0101Adv02_Narrative034": "jailer",
    "0101Adv02_Narrative040": "cling",
    "0101Adv02_Narrative045": "rejection",
    "0101Adv02_Narrative046": "threshold",
}


def localized_texts(path: Path) -> dict[str, str]:
    """Read Naninovel localization sections without using commented JP text."""
    sections: dict[str, str] = {}
    key: str | None = None
    body: list[str] = []

    def finish() -> None:
        if key is None:
            return
        if key in sections:
            raise ValueError(f"Duplicate localization key: {key}")
        value = "\n".join(body).strip()
        value = re.sub(r"<br\s*/?>\s*\n?", "\n", value)
        # Ruby readings are display markup; retain their visible base characters.
        value = re.sub(r"</?ruby(?:\s+[^>]*|=[^>]*)?>", "", value)
        if not value:
            raise ValueError(f"Empty localization section: {key}")
        sections[key] = value

    for line in path.read_text(encoding="utf-8-sig").splitlines():
        if line.startswith("# "):
            finish()
            key = line[2:].strip()
            body = []
        elif key is not None and line.strip() and not line.lstrip().startswith(";"):
            body.append(line)
    finish()
    return sections


def extract() -> dict:
    scenario = json.loads(SCENARIO.read_text(encoding="utf-8-sig"))
    localized = localized_texts(LOCALIZATION)
    references = {
        item["rid"]: item for item in scenario["references"]["RefIds"]
    }
    source_keys = scenario["textMap"]["idToText"]["keys"]
    source_japanese = dict(zip(source_keys, scenario["textMap"]["idToText"]["values"]))
    lines: list[dict] = []
    used_keys: list[str] = []
    stage = "wake"

    for link in scenario["playlist"]["commands"]:
        command = references[link["rid"]]
        if command["type"]["class"] != "PrintTextModified":
            continue
        data = command["data"]
        author_parameter = data.get("AuthorId", {})
        author = author_parameter.get("value", "") if author_parameter.get("hasValue") else ""
        parts = data["Text"]["value"]["parts"]
        keys = [part["id"] for part in parts if part.get("id")]
        if len(keys) != 1:
            raise ValueError(f"Unexpected print text parts at rid {link['rid']}: {parts}")
        key = keys[0]
        if key not in localized or key not in source_japanese:
            raise ValueError(f"Missing source/localized text for {key}")
        stage = STAGE_START.get(key, stage)
        line = {
            "speaker": SPEAKERS.get(author, author),
            "text": localized[key],
            "stage": stage,
            "authorId": author,
            "key": key,
            "scriptLine": data["playbackSpot"]["lineIndex"],
        }
        audio = Path(f"Assets/#WitchTrials/Audio/Voice/{SCRIPT}/{key}.ogg")
        if (RES / audio).is_file():
            line["voice"] = audio.as_posix()
        elif author:
            raise FileNotFoundError(f"Expected original dialogue audio missing: {RES / audio}")
        lines.append(line)
        used_keys.append(key)

    if len(set(used_keys)) != len(used_keys):
        raise ValueError("Repeated text keys need explicit handling")
    if set(used_keys) != set(source_keys):
        raise ValueError("Playlist/textMap mismatch: the prototype must preserve the full Adv02 slice")
    if used_keys[0] != "0101Adv02_Ema001" or used_keys[-1] != "0101Adv02_Narrative046":
        raise ValueError("Unexpected start or endpoint for the prison opening")
    if any("Adv03" in key for key in used_keys):
        raise ValueError("The cell-exit segment must not enter this prototype")

    return {
        "schemaVersion": 1,
        "script": SCRIPT,
        "locale": "zh-Hans",
        "sourceScenario": SCENARIO.relative_to(ROOT).as_posix(),
        "sourceLocalization": LOCALIZATION.relative_to(ROOT).as_posix(),
        "sourceScenarioSha256": hashlib.sha256(SCENARIO.read_bytes()).hexdigest(),
        "sourceLocalizationSha256": hashlib.sha256(LOCALIZATION.read_bytes()).hexdigest(),
        "stopBefore": "Act01_Chapter01/Act01_Chapter01_Adv03:0101Adv03_Narrative001",
        "stageNote": "stage 为依据原剧情事件设置的原型调度提示，正文严格按原始 playlist 顺序读取中文本地化。",
        "lines": lines,
    }


def main() -> None:
    result = extract()
    OUTPUT.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    voice_count = sum("voice" in line for line in result["lines"])
    print(f"Wrote {len(result['lines'])} original lines and {voice_count} original voices to {OUTPUT}")


if __name__ == "__main__":
    main()
