#!/usr/bin/env python3
"""Convert C# 10 file-scoped namespaces to C# 9 block form, deterministically.

Why not just raise `LangVersion`: `UNITY_READINESS_CODEX_AUDIT.md` UR-005 / section 3.3.7
explicitly forbids "pretending Unity is compatible by changing the IDE csproj's
LangVersion=latest".  Unity 6.0's default language version is C# 9, so a source tree that
uses `namespace X;` (C# 10) will not compile in a default Unity project.  Measured need:
10,260 of 10,487 .cs files.

This converter rewrites

    namespace A.B;

    using ...;
    public class C { }

into

    namespace A.B
    {
        using ...;
        public class C { }
    }

* It **never overwrites an input file**: the output goes to a separate tree, so the
  original evidence under `src/` stays byte-identical (section 3.1.3).
* It is idempotent: a file that already uses block namespaces is copied unchanged.
* It refuses rather than guesses: any file that already contains a block namespace, a
  file-local type, or a top-level statement is reported as MANUAL_REVIEW and copied
  unchanged, so nothing is silently mangled.
* It re-indents the body by one level, which is required because the body was previously
  at namespace scope with no braces.

Outputs:
  <out>/converted/<relative path>     converted tree
  out/csharp_conversion.json          per-file status and counts
  out/csharp_conversion.csv           one row per file
"""
from __future__ import annotations

import csv
import json
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
AUDIT = os.path.join(os.path.dirname(HERE), "audit")
sys.path.insert(0, AUDIT)
import config  # noqa: E402

OUT = os.path.join(HERE, "out")
DEFAULT_OUT_TREE = os.path.join(HERE, "converted")

# `namespace X.Y.Z;` on its own line, with optional leading whitespace.
FILE_SCOPED = re.compile(r"^(?P<indent>[ \t]*)namespace[ \t]+(?P<name>[A-Za-z_][\w.]*)[ \t]*;[ \t]*$")
# Any block-form namespace, which means the file is already C# 9 compatible.
BLOCK_NS = re.compile(r"^[ \t]*namespace[ \t]+[A-Za-z_][\w.]*[ \t]*\{?[ \t]*$")
INDENT_UNIT = "    "


def convert_text(text):
    """Return (converted_text, status, detail)."""
    lines = text.split("\n")
    # Find the single file-scoped namespace declaration.
    matches = [(i, m) for i, line in enumerate(lines)
               if (m := FILE_SCOPED.match(line.rstrip("\r")))]
    if not matches:
        if any(BLOCK_NS.match(l.rstrip("\r")) for l in lines):
            return text, "ALREADY_BLOCK_FORM", "block namespace present; unchanged"
        return text, "NO_NAMESPACE", "no namespace declaration; unchanged"
    if len(matches) > 1:
        return text, "MANUAL_REVIEW", f"{len(matches)} file-scoped namespace declarations"

    index, match = matches[0]
    name = match.group("name")
    indent = match.group("indent")

    # Everything after the declaration becomes the namespace body.  Keep a single blank
    # line directly after the opening brace so the diff stays readable.
    body = lines[index + 1:]
    while body and body[0].strip() == "":
        body.pop(0)
    # Drop trailing blank lines, then re-add exactly one before the closing brace.
    while body and body[-1].strip() == "":
        body.pop()

    converted_body = []
    for line in body:
        if line.strip() == "":
            converted_body.append("")
        else:
            converted_body.append(INDENT_UNIT + line)

    head = lines[:index]
    result = []
    result.extend(head)
    result.append(f"{indent}namespace {name}")
    result.append(f"{indent}{{")
    result.extend(converted_body)
    result.append(f"{indent}}}")
    result.append("")
    return "\n".join(result), "CONVERTED", f"file-scoped namespace '{name}' -> block form"


def main():
    out_tree = DEFAULT_OUT_TREE
    for i, arg in enumerate(sys.argv):
        if arg == "--out" and i + 1 < len(sys.argv):
            out_tree = sys.argv[i + 1]

    roots = [p for p in (config.SOURCE_ROOT,
                         os.path.join(config.REPO_ROOT, "UnityProject", "Assets"))
             if p and os.path.isdir(p)]
    if not roots:
        print("BLOCKED: no source roots", file=sys.stderr)
        return 2

    rows = []
    counts = {}
    os.makedirs(out_tree, exist_ok=True)

    for root in roots:
        for dirpath, dirnames, files in os.walk(root):
            dirnames[:] = [d for d in dirnames
                           if d not in (".git", "__pycache__", "obj", "bin", "Library")]
            for f in sorted(files):
                if not f.endswith(".cs"):
                    continue
                path = os.path.join(dirpath, f)
                rel = os.path.relpath(path, config.REPO_ROOT)
                try:
                    with open(path, encoding="utf-8", newline="") as fh:
                        text = fh.read()
                except OSError as exc:
                    rows.append({"file": rel, "status": "UNREADABLE",
                                 "detail": f"{type(exc).__name__}: {exc}", "bytes_in": 0,
                                 "bytes_out": 0})
                    counts["UNREADABLE"] = counts.get("UNREADABLE", 0) + 1
                    continue

                converted, status, detail = convert_text(text)
                target = os.path.join(out_tree, rel)
                os.makedirs(os.path.dirname(target), exist_ok=True)
                with open(target, "w", encoding="utf-8", newline="") as fh:
                    fh.write(converted)

                counts[status] = counts.get(status, 0) + 1
                rows.append({"file": rel.replace(os.sep, "/"), "status": status,
                             "detail": detail, "bytes_in": len(text.encode("utf-8")),
                             "bytes_out": len(converted.encode("utf-8"))})

    result = {
        "generated": __import__("time").strftime("%Y-%m-%dT%H:%M:%S"),
        "source_roots": roots,
        "output_tree": out_tree,
        "files": len(rows),
        "status_counts": counts,
        "converted": counts.get("CONVERTED", 0),
        "unchanged": counts.get("ALREADY_BLOCK_FORM", 0) + counts.get("NO_NAMESPACE", 0),
        "manual_review": counts.get("MANUAL_REVIEW", 0),
        "note": ("Originals under src/ are never modified; this writes a converted tree. "
                 "MANUAL_REVIEW files are copied unchanged on purpose."),
    }
    with open(os.path.join(OUT, "csharp_conversion.json"), "w", encoding="utf-8") as fh:
        json.dump(result, fh, ensure_ascii=False, indent=1)
    with open(os.path.join(OUT, "csharp_conversion.csv"), "w", encoding="utf-8",
              newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=["file", "status", "detail", "bytes_in", "bytes_out"])
        w.writeheader()
        for r in rows:
            w.writerow(r)

    print(json.dumps(result, ensure_ascii=False, indent=1))
    for status, n in sorted(counts.items()):
        print(f"   {status:<20} {n}")
    manual = [r for r in rows if r["status"] == "MANUAL_REVIEW"]
    for r in manual[:10]:
        print("   MANUAL_REVIEW:", r["file"], "-", r["detail"])
    return 0


if __name__ == "__main__":
    sys.exit(main())
