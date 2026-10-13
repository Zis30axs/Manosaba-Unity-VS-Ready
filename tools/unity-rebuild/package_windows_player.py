#!/usr/bin/env python3
"""Package a built Windows player into independent ZIP parts below release upload limits.

Extract every part into the same directory, then start Manosaba-Windows/manosaba-rebuild.exe.
Only built player files are input; debug symbols, diagnostics and runtime saves are excluded.
Existing release outputs are never overwritten. No Unity import or build is performed here.
"""
import argparse
import hashlib
import json
import zipfile
from pathlib import Path

REPO = Path(__file__).resolve().parents[2]
PART_BYTES = 1_900_000_000


def digest(path):
    h = hashlib.sha256()
    with path.open("rb") as stream:
        for block in iter(lambda: stream.read(1024 * 1024), b""):
            h.update(block)
    return h.hexdigest()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--source", type=Path, default=REPO / "dist/player-release")
    parser.add_argument("--out", type=Path, required=True)
    args = parser.parse_args()
    source, output = args.source.resolve(), args.out.resolve()
    if output == source or source in output.parents:
        raise SystemExit("The release output must be outside the player directory.")
    if not (source / "manosaba-rebuild.exe").is_file() or not (source / "UnityPlayer.dll").is_file():
        raise SystemExit("A complete built Windows player is required.")
    if not (source / "manosaba-rebuild_Data/StreamingAssets/aa").is_dir():
        raise SystemExit("The player's built Addressables directory is missing.")
    files, excluded = [], []
    for path in sorted(source.rglob("*")):
        relative = path.relative_to(source)
        # FILE_ATTRIBUTE_REPARSE_POINT also covers Windows junctions on Python 3.11.
        if path.is_symlink() or getattr(path.lstat(), "st_file_attributes", 0) & 0x400 or not path.resolve().is_relative_to(source):
            raise SystemExit(f"Unexpected link or path outside the player: {relative}")
        if not path.is_file():
            continue
        if any(p.endswith(("_DoNotShip", "_BackUpThisFolder_ButDontShipItWithYourGame")) or p in {"NaninovelData", "Saves_v1"} for p in relative.parts) or path.suffix.lower() in {".pdb", ".log"}:
            excluded.append(relative.as_posix())
            continue
        size = path.stat().st_size
        if size + 4096 >= PART_BYTES:
            raise SystemExit(f"One player file exceeds the ZIP part limit: {relative}")
        files.append((path, relative.as_posix(), size))
    parts = []
    for item in sorted(files, key=lambda row: (-row[2], row[1])):
        for part in parts:
            if part["bytes"] + item[2] + 4096 <= PART_BYTES:
                part["items"].append(item)
                part["bytes"] += item[2] + 4096
                break
        else:
            parts.append({"items": [item], "bytes": item[2] + 4096})
    output.mkdir(parents=True, exist_ok=True)
    targets = [output / f"manosaba-windows-part{i}of{len(parts)}.zip" for i in range(1, len(parts) + 1)]
    metadata = [output / "windows-manifest.json", output / "WINDOWS-INSTALL.txt", output / "WINDOWS-SHA256SUMS.txt"]
    if any(path.exists() for path in targets + metadata):
        raise SystemExit("Release outputs already exist; select a fresh output directory.")
    manifest = {"format": 1, "files": [], "archives": [], "excluded": excluded}
    for target, part in zip(targets, parts):
        with zipfile.ZipFile(target, "x", compression=zipfile.ZIP_STORED, allowZip64=False) as archive:
            for path, relative, size in sorted(part["items"], key=lambda row: row[1]):
                file_hash = digest(path)
                archive.write(path, "Manosaba-Windows/" + relative)
                if digest(path) != file_hash:
                    raise SystemExit(f"Player file changed while packaging: {relative}")
                manifest["files"].append({"path": relative, "bytes": size, "sha256": file_hash, "archive": target.name})
        with zipfile.ZipFile(target) as archive:
            if archive.testzip() is not None:
                raise SystemExit(f"ZIP CRC verification failed: {target.name}")
        if target.stat().st_size >= PART_BYTES:
            raise SystemExit(f"ZIP exceeds the conservative release size limit: {target.name}")
        manifest["archives"].append({"name": target.name, "bytes": target.stat().st_size, "sha256": digest(target)})
        print(f"Verified {target.name}: {target.stat().st_size:,} bytes", flush=True)
    metadata[0].write_text(json.dumps(manifest, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    metadata[1].write_text(
        f"Windows 测试版\n\n下载全部 {len(parts)} 个 manosaba-windows-part*.zip。\n"
        "依次把所有 ZIP 解压到同一目录，允许合并同名文件夹。每个 ZIP 都是独立压缩包，不需要拼接。\n"
        "进入 Manosaba-Windows，双击 manosaba-rebuild.exe。不要只移动 EXE；保留同目录的数据文件和 DLL。\n"
        "本版包含独立运行所需资源；下载和解压时请预留两倍于全部 ZIP 大小的空闲空间。\n"
        "该构建未做 Windows 代码签名，属于测试版；已知限制见 GitHub 发布说明。\n"
        "SHA256 校验值见 WINDOWS-SHA256SUMS.txt。\n", encoding="utf-8")
    metadata[2].write_text("".join(f"{row['sha256']} *{row['name']}\n" for row in manifest["archives"]), encoding="ascii")
    print(f"Packaged {len(files)} player files in {len(parts)} independent ZIP parts.")


if __name__ == "__main__":
    main()
