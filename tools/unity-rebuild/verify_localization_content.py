"""Check the actual built bundles, not only the imported source assets.

Run with a Python environment containing UnityPy.
"""
from pathlib import Path
import json
import UnityPy

ROOT = Path(__file__).resolve().parents[2]
BUILD = ROOT / "UnityProject/Library/com.unity.addressables/aa/Windows"
TEXT = ROOT / "UnityProject/Assets/GameContent/#WitchTrials/Localization/zh-Hans/Text"


def one_bundle(pattern):
    paths = list(BUILD.rglob(pattern))
    if len(paths) != 1:
        raise AssertionError(f"Expected one current bundle for {pattern}, found {len(paths)}")
    return UnityPy.load(str(paths[0]))


def verify():
    prefabs = one_bundle("manosaba.prefabs.general-prefabs_assets_all_*.bundle")
    labels = set()
    for obj in prefabs.objects:
        if obj.type.name != "MonoBehaviour":
            continue
        tree = obj.read_typetree()
        if isinstance(tree.get("m_text"), str):
            labels.add(tree["m_text"])
    expected = {"暂停", "时间倍率\n+0.2", "时间倍率\n-0.2", "调试主界面",
                "切换\n调试快进", "切换\n辩论文本推进", "解锁全部\nCG",
                "保存到桌面", "取消", "发布到 Slack", "调试报告", "报告详情"}
    missing = sorted(expected - labels)
    assert not missing, "Built Debug UI is stale: " + json.dumps(missing, ensure_ascii=True)
    text_bundle = one_bundle("manosaba.text_assets_all_*.bundle")
    checked = []
    for relative in ("Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt", "Scripts/Debug/Debug_Home.txt"):
        suffix = "/localization/zh-hans/text/" + relative.lower()
        matches = [obj for name, obj in text_bundle.container.items() if name.lower().endswith(suffix)]
        assert len(matches) == 1, f"Missing/ambiguous built text: {relative}"
        actual = matches[0].read().m_Script
        if isinstance(actual, bytes):
            actual = actual.decode("utf-8-sig")
        expected_text = (TEXT / relative).read_text(encoding="utf-8-sig")
        assert actual.lstrip("\ufeff").replace("\r\n", "\n") == expected_text, f"Stale built text: {relative}"
        checked.append(relative)
    print(json.dumps({"status": "PASS", "debug_labels": len(expected), "texts": checked}, ensure_ascii=True))


if __name__ == "__main__":
    verify()
