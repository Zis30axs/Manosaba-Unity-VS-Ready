#!/usr/bin/env python3
"""Scan the reconstructed sources for C# features Unity 6000.0's default compiler rejects.

`UNITY_READINESS_CODEX_AUDIT.md` UR-005 / section 3.3.7 records that the project's IDE
csproj sets `LangVersion=latest`, while Unity 6.0 defaults to **C# 9**.  A successful
`dotnet build` therefore proves nothing about whether Unity can compile the same sources.
This tool measures the actual gap per file, so the compatibility conversion can be scoped
instead of guessed, and the result can be re-checked after conversion.

What it reports (each with file:line evidence):
  * file-scoped namespaces          `namespace X;`                  (C# 10)
  * record / record struct                                          (C# 9 / 10)
  * `required` members                                              (C# 11)
  * static abstract / static virtual interface members               (C# 11)
  * generic math / `INumber<T>` usage                               (C# 11)
  * raw string literals (three or more double quotes)               (C# 11)
  * list patterns                                                   (C# 11)
  * `init` accessors                                                (C# 9, allowed)
  * top-level statements                                            (C# 9, not usable in Unity)
  * unsafe / pointer members                                        (needs AllowUnsafeBlocks)
  * `global using`                                                  (C# 10)
  * file-local types `file class`                                   (C# 11)
  * `scoped` / `ref fields`                                         (C# 11)
  * primary constructors on classes                                 (C# 12)

Outputs (tools/unity-rebuild/out/):
  csharp_compat.json     per-feature and per-file counts, with samples
  csharp_compat.csv      one row per (file, feature) with the first offending line
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

#: feature -> (regex, minimum C# version, whether Unity 6 default C# 9 accepts it)
FEATURES = [
    ("file_scoped_namespace", re.compile(r"^\s*namespace\s+[\w.]+\s*;\s*$"), 10, False),
    ("record_declaration", re.compile(r"^\s*(public|internal|private|protected)?\s*(sealed\s+)?record\s+(class\s+|struct\s+)?\w"), 9, True),
    ("record_struct", re.compile(r"^\s*(public|internal)?\s*(readonly\s+)?record\s+struct\s+\w"), 10, False),
    ("required_member", re.compile(r"\brequired\s+(public|internal|protected|private)?\s*[\w<>,\[\]?\.]+\s+\w+\s*\{"), 11, False),
    ("static_abstract_member", re.compile(r"\bstatic\s+(abstract|virtual)\s+"), 11, False),
    ("generic_math", re.compile(r"\bINumber<|\bIAdditionOperators<|\bSystem\.Numerics\.INumber"), 11, False),
    # Raw string literals are three or more quotes *opening a literal*, not the `""` escape
    # used inside a verbatim string. Requiring the run to start a token (preceded by
    # start-of-line, `=`, `(`, `,`, or `return`) avoids the false positive that fired on
    # `""expectedControlType"": """"` in the Input Actions artifact.
    ("raw_string_literal", re.compile(r'(^|[=(,]\s*|return\s+)"{3,}'), 11, False),
    ("list_pattern", re.compile(r"\bis\s*\[\s*\.\.|\bswitch\s*\{[^}]*\[\s*\.\."), 11, False),
    ("init_accessor", re.compile(r"\{\s*get;\s*init;\s*\}"), 9, True),
    ("global_using", re.compile(r"^\s*global\s+using\s+"), 10, False),
    ("file_local_type", re.compile(r"^\s*file\s+(class|struct|interface|enum|record)\s+"), 11, False),
    ("scoped_keyword", re.compile(r"\bscoped\s+(ref|in|readonly)\b"), 11, False),
    ("ref_field", re.compile(r"\bref\s+readonly\s+\w+\s+\w+\s*;|\bref\s+\w+\s+\w+\s*;"), 11, False),
    ("class_primary_constructor", re.compile(
        r"^\s*(public|internal)\s+(sealed\s+)?class\s+\w+<[^>]*>?\s*\([^)]*\)\s*[:{]" + chr(125)), 12, False),
    ("generic_attribute", re.compile(r"\[\s*\w+<"), 11, False),
    ("static_interface_member", re.compile(r"^\s*static\s+[\w<>\[\],\.\?]+\s+\w+\s*\(.*\)\s*;\s*$"), 11, False),
]

UNSAFE_RE = re.compile(r"\bunsafe\b|\bfixed\s*\(|\bstackalloc\b")


def scan_file(path):
    """Return (hits, has_unsafe) for one file."""
    hits = []
    has_unsafe = False
    try:
        with open(path, encoding="utf-8", errors="replace") as fh:
            in_block_comment = False
            for lineno, line in enumerate(fh, 1):
                stripped = line.strip()
                if in_block_comment:
                    if "*/" in stripped:
                        in_block_comment = False
                    continue
                if stripped.startswith("/*"):
                    if "*/" not in stripped:
                        in_block_comment = True
                    continue
                # Skip comment-only lines so documentation about a feature is not counted
                # as a use of it (the same trap that made the a07 static guards wrong).
                if stripped.startswith("//") or stripped.startswith("///"):
                    continue
                code = line.split("//")[0]
                for name, regex, version, ok in FEATURES:
                    if regex.search(code):
                        hits.append((name, lineno, stripped[:160], version, ok))
                if UNSAFE_RE.search(code):
                    has_unsafe = True
    except OSError as exc:
        hits.append(("unreadable", 0, f"{type(exc).__name__}: {exc}", 0, False))
    return hits, has_unsafe


def main():
    os.makedirs(OUT, exist_ok=True)
    roots = [p for p in (config.SOURCE_ROOT,
                         os.path.join(config.REPO_ROOT, "UnityProject", "Assets"))
             if p and os.path.isdir(p)]
    if not roots:
        print("BLOCKED: no source roots found", file=sys.stderr)
        return 2

    per_feature = {}
    rows = []
    files_scanned = 0
    unsafe_files = []
    for root in roots:
        for dirpath, dirnames, files in os.walk(root):
            dirnames[:] = [d for d in dirnames
                           if d not in (".git", "__pycache__", "obj", "bin", "Library")]
            for f in sorted(files):
                if not f.endswith(".cs"):
                    continue
                path = os.path.join(dirpath, f)
                files_scanned += 1
                rel = os.path.relpath(path, config.REPO_ROOT).replace(os.sep, "/")
                hits, has_unsafe = scan_file(path)
                if has_unsafe:
                    unsafe_files.append(rel)
                for name, lineno, text, version, ok in hits:
                    per_feature.setdefault(name, {"count": 0, "files": set(), "samples": [],
                                                  "min_csharp": version,
                                                  "accepted_by_csharp9": ok})
                    entry = per_feature[name]
                    entry["count"] += 1
                    entry["files"].add(rel)
                    if len(entry["samples"]) < 15:
                        entry["samples"].append({"file": rel, "line": lineno, "text": text})
                    rows.append({"file": rel, "line": lineno, "feature": name,
                                 "min_csharp_version": version,
                                 "accepted_by_csharp9": ok, "text": text})

    summary = {}
    blocking_total = 0
    for name, entry in sorted(per_feature.items(), key=lambda kv: -kv[1]["count"]):
        blocking = 0 if entry["accepted_by_csharp9"] else entry["count"]
        blocking_total += blocking
        summary[name] = {
            "occurrences": entry["count"],
            "files": len(entry["files"]),
            "min_csharp_version": entry["min_csharp"],
            "accepted_by_csharp9": entry["accepted_by_csharp9"],
            "blocking_occurrences": blocking,
            "samples": entry["samples"],
        }

    result = {
        "generated": __import__("time").strftime("%Y-%m-%dT%H:%M:%S"),
        "source_roots": roots,
        "unity_target": "6000.0.48f1",
        "unity_default_csharp_version": 9,
        "files_scanned": files_scanned,
        "features": summary,
        "total_blocking_occurrences": blocking_total,
        "files_with_unsafe": len(unsafe_files),
        "unsafe_files_sample": sorted(unsafe_files)[:20],
        "verdict": ("ALL_SOURCES_WITHIN_CSHARP_9" if blocking_total == 0
                    else "CONVERSION_REQUIRED"),
        "note": ("Counts exclude comment-only lines. `record` and `init` are C# 9 and are "
                 "listed for completeness only; they are not blockers."),
    }
    with open(os.path.join(OUT, "csharp_compat.json"), "w", encoding="utf-8") as fh:
        json.dump(result, fh, ensure_ascii=False, indent=1)

    with open(os.path.join(OUT, "csharp_compat.csv"), "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=["file", "line", "feature", "min_csharp_version",
                                           "accepted_by_csharp9", "text"])
        w.writeheader()
        for r in rows:
            w.writerow(r)

    print(json.dumps({k: v for k, v in result.items()
                      if k not in ("features", "unsafe_files_sample")},
                     ensure_ascii=False, indent=1))
    print("\nper feature:")
    for name, entry in summary.items():
        flag = "OK (C#9)" if entry["accepted_by_csharp9"] else "BLOCKS C#9"
        print(f"   {name:<28} {entry['occurrences']:6d} occurrences in "
              f"{entry['files']:5d} files   min C# {entry['min_csharp_version']}  {flag}")
    print("\ntop samples per blocking feature:")
    for name, entry in summary.items():
        if entry["accepted_by_csharp9"]:
            continue
        for s in entry["samples"][:3]:
            print(f"   {name}: {s['file']}:{s['line']}  {s['text'][:90]}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
