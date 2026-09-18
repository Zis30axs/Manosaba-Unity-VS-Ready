"""Convert file-scoped namespaces (``namespace X;``) to block-scoped ones in vendored sources.

    python tools/unity-rebuild/convert_file_scoped_namespaces.py <dir> [--write]

Why: the vendored TrueShadow copy was produced by a decompiler that emits C# 10 file-scoped namespaces,
while this project compiles C# 9 (Unity default), so every such file fails with CS8773.  The two forms are
semantically identical -- ``namespace X;`` ... end-of-file becomes ``namespace X {`` ... ``}`` -- so this is
a mechanical format adaptation of a vendored file, not a code change, and it is recorded in that package's
PROVENANCE.md.
"""
from __future__ import annotations

import argparse
import io
import os
import re
import sys

FILE_SCOPED = re.compile(r"(?m)^([ \t]*)namespace\s+([A-Za-z_][\w\.]*)\s*;\s*$")


def convert(text: str):
    """Return (new_text, converted_count)."""
    matches = list(FILE_SCOPED.finditer(text))
    if not matches:
        return text, 0
    if len(matches) > 1:
        raise ValueError("more than one file-scoped namespace in one file")
    match = matches[0]
    head = text[:match.start()]
    tail = text[match.end():]
    new = f"{head}{match.group(1)}namespace {match.group(2)}\n{match.group(1)}{{\n{tail}"
    if not new.endswith("\n"):
        new += "\n"
    new += "}\n"
    return new, 1


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("directory")
    ap.add_argument("--write", action="store_true")
    args = ap.parse_args()

    total_files = 0
    total_converted = 0
    for root, _dirs, files in os.walk(args.directory):
        for name in sorted(files):
            if not name.endswith(".cs"):
                continue
            path = os.path.join(root, name)
            text = io.open(path, encoding="utf-8-sig", newline="").read()
            try:
                new, count = convert(text)
            except ValueError as ex:
                print(f"  SKIP {path}: {ex}")
                continue
            if not count:
                continue
            total_files += 1
            total_converted += count
            print(f"  {'converted' if args.write else 'would convert'} {os.path.relpath(path, args.directory)}")
            if args.write:
                io.open(path, "w", encoding="utf-8", newline="").write(new)

    print(f"{'converted' if args.write else 'would convert'} {total_converted} file(s) "
          f"of {total_converted + 0} in {args.directory}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
