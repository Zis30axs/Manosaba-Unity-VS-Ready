"""Classify exceptions without treating an absent/empty log as successful startup."""
import argparse
import json
from pathlib import Path
import re


def classify(path):
    text = path.read_text(encoding="utf-8-sig", errors="replace")
    if not text.strip():
        raise ValueError("Empty log: no execution evidence")
    names = ("TsukushiMincho", "SourceHanSerifSC", "SourceHanSansKR")
    font = {n: len(re.findall(r"Failed to load '" + n + r"/Assets/" + n + "'", text)) for n in names}
    nulls = sum("LeTai.TrueShadow" in m.group(0) and "Parameter name: shader" in m.group(0)
                for m in re.finditer(r"ArgumentNullException:[\s\S]{0,2500}", text))
    return {"log": str(path.resolve()), "true_shadow_shader_null": nulls,
            "font_load_failures": font, "file_not_found": text.count("FileNotFoundException:"),
            "null_reference": text.count("NullReferenceException:"),
            "invalid_key": text.count("InvalidKeyException:"),
            "duplicate_locale_selector_warning": text.count("A service of the same type is already registered: IDefaultLocaleSelector"),
            "game_probe_started": "[ManosabaGameStart]" in text,
            "naninovel_progress_seen": "INITIALIZED" in text or "Engine initialization completed" in text,
            "note": "Counts are log occurrences, not unique exceptions. Zero counts do not prove UI was exercised."}


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("log", type=Path)
    args = parser.parse_args()
    print(json.dumps(classify(args.log), indent=2))
