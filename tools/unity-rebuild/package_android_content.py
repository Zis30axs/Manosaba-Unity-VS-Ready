#!/usr/bin/env python3
"""Cut the Android Addressables bundles into plain zip parts the player's content gate can import.

The bundles (about 4.3 GB) fit neither the APK nor the OBB (32-bit zip offsets), so BuildAndroidPlayer leaves them out and writes
tools/unity-rebuild/out/unity/android_content_manifest.json (name + size of every bundle, plus a build id).  This script packs
exactly those files, uncompressed (they are LZ4 bundles already), into parts of at most --part-bytes each, so that no part needs zip64
and every part can be sent through channels with a file-size limit.  The player (ContentGateActivity) asks for the parts on first
launch, streams the bundles out of them and starts the game; the order and number of parts selected do not matter.

    python tools/unity-rebuild/package_android_content.py
        [--manifest M] [--source DIR] [--out DIR] [--part-bytes N]

Output (default dist/player-android/content/): manosaba-android-content-<buildId>-partXofN.zip, SHA256SUMS.txt, README.txt.
The packs hold the original game's assets: share them privately, to people who own the game.
"""
import argparse
import hashlib
import json
import sys
import zipfile
from pathlib import Path

REPO = Path(__file__).resolve().parents[2]
DEFAULT_MANIFEST = REPO / "tools/unity-rebuild/out/unity/android_content_manifest.json"
DEFAULT_SOURCE = REPO / "UnityProject/Library/com.unity.addressables/aa/Android/Android"
DEFAULT_OUT = REPO / "dist/player-android/content"
DEFAULT_PART_BYTES = 1_900_000_000        # below 2 GiB: plain zip, and friendly to upload limits

README = """\
魔法少女的魔女审判（安卓重建版）— 游戏资源包

资源版本：{build}    共 {count} 个文件，{total:.2f} GB，分成 {parts} 个压缩包

安装步骤
1. 安装 manosaba-rebuild.apk，并把本目录的 {parts} 个 .zip 都放到手机上（下载目录即可，不要解压）。
2. 打开游戏。首次启动会提示"导入游戏资源"，点"选择资源包"，在文件选择器里把 {parts} 个 .zip 一次全部选中。
3. 等待导入完成（需要约 {need:.1f} GB 空闲空间，导入时请保持屏幕常亮），游戏会自动启动。
4. 导入成功后可以删除这些 .zip。以后启动不再询问。

注意
- 资源包的版本必须与 APK 相同（文件名里的 {build}）。
- 导入被中断也没关系，再次选择同样的文件会继续，已导入的部分会跳过。
- 校验值见 SHA256SUMS.txt。
- 这些资源来自原版游戏，请只在拥有游戏的人之间私下传递。
"""


def sha256(path):
    digest = hashlib.sha256()
    with open(path, "rb") as stream:
        for block in iter(lambda: stream.read(1 << 20), b""):
            digest.update(block)
    return digest.hexdigest()


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--manifest", type=Path, default=DEFAULT_MANIFEST)
    parser.add_argument("--source", type=Path, default=DEFAULT_SOURCE)
    parser.add_argument("--out", type=Path, default=DEFAULT_OUT)
    parser.add_argument("--part-bytes", type=int, default=DEFAULT_PART_BYTES)
    args = parser.parse_args(argv)

    manifest = json.loads(args.manifest.read_text(encoding="utf-8-sig"))
    build = manifest["buildId"]
    files = [(item["n"], item["s"]) for item in manifest["files"]]
    if not files:
        raise SystemExit("The content manifest lists no bundles.")
    for name, size in files:
        path = args.source / name
        if not path.is_file() or path.stat().st_size != size:
            raise SystemExit(f"{path} is missing or differs from the manifest ({size} bytes expected); rebuild the Android player.")
        if size >= args.part_bytes:
            raise SystemExit(f"{name} ({size} bytes) does not fit a part of {args.part_bytes} bytes.")

    # first-fit decreasing keeps the parts close to equal size
    parts = []
    for name, size in sorted(files, key=lambda item: -item[1]):
        for part in parts:
            if part["bytes"] + size <= args.part_bytes:
                part["files"].append(name)
                part["bytes"] += size
                break
        else:
            parts.append({"files": [name], "bytes": size})

    args.out.mkdir(parents=True, exist_ok=True)
    for stale in args.out.glob("manosaba-android-content-*.zip"):
        stale.unlink()                    # packs of any older build must not be mistaken for this one
    total = sum(size for _, size in files)
    written = []
    for index, part in enumerate(parts, 1):
        target = args.out / f"manosaba-android-content-{build}-part{index}of{len(parts)}.zip"
        temporary = target.with_suffix(".zip.tmp")
        with zipfile.ZipFile(temporary, "w", zipfile.ZIP_STORED, allowZip64=False) as archive:
            for name in sorted(part["files"]):
                archive.write(args.source / name, name)
        temporary.replace(target)
        written.append(target)
        print(f"{target.name}: {len(part['files'])} files, {part['bytes'] / 1e9:.2f} GB", flush=True)

    (args.out / "SHA256SUMS.txt").write_text("".join(f"{sha256(path)} *{path.name}\n" for path in written), encoding="ascii")
    (args.out / "README.txt").write_text(README.format(build=build, count=len(files), total=total / 1e9, parts=len(parts), need=total / 1e9 + 0.3),
                                         encoding="utf-8")
    print(f"build {build}: {len(files)} bundles, {total / 1e9:.2f} GB in {len(parts)} parts -> {args.out}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
