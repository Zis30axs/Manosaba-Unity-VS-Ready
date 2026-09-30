"""Deterministic on-disk contract check, NOT a substitute for Unity/Play Mode.

Accepts a repository or transaction-copy root. No writes, downloads or imports.
Used for baseline/modified/rollback checks on the same fixture.
"""
import argparse
import json
from pathlib import Path
import re


def check(root):
    package = root / "UnityProject/Packages/com.le-tai.true-shadow"
    shaders = {m for p in (package / "Resources").rglob("*.shader")
               for m in re.findall(r'Shader\s+"([^"]+)"', p.read_text(encoding="utf-8-sig"))}
    needed = sorted({m for p in package.rglob("*.cs")
                     for m in re.findall(r'Shader\.Find\("([^"]+)"\)', p.read_text(encoding="utf-8-sig"))})
    if len(needed) != 9:
        raise ValueError("Expected the audited 9 Shader.Find contracts, not an empty/wrong source root")
    missing = ["shader:" + n for n in needed if n not in shaders]
    for filename in ("True Shadow Blue Noise.png", "True Shadow Project Settings.asset"):
        if not (package / "Resources" / filename).is_file():
            missing.append("resource:" + filename)
    group = root / "UnityProject/Assets/AddressableAssetsData/AssetGroups/Manosaba.RuntimeFonts.asset"
    text = group.read_text(encoding="utf-8-sig") if group.exists() else ""
    for name in ("TsukushiMincho", "SourceHanSerifSC", "SourceHanSansKR"):
        asset = root / "UnityProject/Assets/GameContent/RuntimeFonts" / name / (name + ".asset")
        data = asset.read_text(encoding="utf-8-sig") if asset.exists() else ""
        if not data or not re.search(r"m_AtlasPopulationMode: 1\b", data):
            missing.append("dynamic-TMP:" + name)
        elif not re.search(r"m_SourceFontFile:.*guid:", data):
            missing.append("font-source:" + name)
        if "Naninovel/Fonts/" + name + "/Assets/" + name not in text:
            missing.append("address:" + name)
    return {"boundary": "on-disk contract only; not runtime acceptance", "shader_contracts": len(needed),
            "missing": missing, "failures": len(missing), "status": "FAIL" if missing else "PASS"}


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("root", type=Path)
    args = parser.parse_args()
    result = check(args.root.resolve())
    print(json.dumps(result, ensure_ascii=True, indent=2))
    raise SystemExit(1 if result["failures"] else 0)
