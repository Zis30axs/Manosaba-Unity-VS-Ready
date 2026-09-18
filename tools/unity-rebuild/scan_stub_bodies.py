"""Scan the converted game sources for methods/properties whose body is still an IL2CPP-dump stub.

Background: the game's assemblies were decompiled with Il2CppDumper, which emits empty bodies
(`{ }`) and constant returns (`return default(T);`, `return null;`, `return 0;`) for every member.
Reconstruction replaces those, one member at a time, with a body recovered from the native code and
marked with a `// RECONSTRUCTED | Confidence: ... | RVA: ...` banner.  So "how much reconstruction is
left?" is exactly "how many members still have a stub body and no RECONSTRUCTED banner above them".

This script answers that, and nothing else: it does not judge whether a body is *correct*, only whether
it is still the dumper's placeholder.  It is deliberately textual (no Roslyn): the point is to be able to
run it against the shipped decompile without a build.

Output: a per-assembly count plus the highest-value individual hits, written to stdout and (with
--json) to a machine-readable file.

Usage:
    python tools/unity-rebuild/scan_stub_bodies.py
    python tools/unity-rebuild/scan_stub_bodies.py --json out/unity/stub_scan.json
"""
import argparse
import json
import os
import re
import sys
from collections import Counter, defaultdict

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.dirname(os.path.dirname(HERE))
GAME_ROOT = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")

# Bodies that are the dumper's placeholders.  `return default(bool)` etc. are what Il2CppDumper emits
# for a value type; the `default(T)` form is the generic one.
STUB_BODIES = re.compile(
    r"^\s*(?:"
    r"return\s+default\s*(?:\([^)]*\))?\s*;"
    r"|return\s+null\s*;"
    r"|return\s+(?:false|true|-?[0-9]+(?:\.[0-9]+)?f?)\s*;"
    r"|return\s+default\s*;"
    r")\s*$")

# A member declaration: captures the signature up to the opening brace.  Deliberately permissive; the
# brace matching below decides what is really a body.
DECL = re.compile(
    r"(?P<sig>(?:public|private|protected|internal|static|virtual|override|sealed|async|new|"
    r"partial|extern|unsafe|readonly|abstract|\[[^\]]*\]|\s)*"
    r"[\w<>,\[\]\.\?]+\s+[\w<>\[\]\.]+\s*\([^;{}]*\)\s*)\{",
    re.MULTILINE)

SKIP_DIRS = {"obj", "bin", ".vs"}


def strip_comments(text):
    """Blank out // and /* */ comments while preserving offsets and line count."""
    out = []
    i, n = 0, len(text)
    while i < n:
        ch = text[i]
        nxt = text[i + 1] if i + 1 < n else ""
        if ch == "/" and nxt == "/":
            j = text.find("\n", i)
            if j < 0:
                j = n
            out.append(" " * (j - i))
            i = j
        elif ch == "/" and nxt == "*":
            j = text.find("*/", i + 2)
            j = n if j < 0 else j + 2
            out.append("".join("\n" if c == "\n" else " " for c in text[i:j]))
            i = j
        elif ch == '"':
            j = i + 1
            while j < n:
                if text[j] == "\\":
                    j += 2
                    continue
                if text[j] == '"':
                    j += 1
                    break
                j += 1
            out.append(text[i:j])
            i = j
        else:
            out.append(ch)
            i += 1
    return "".join(out)


def match_brace(text, open_index):
    """Index just past the `}` matching the `{` at open_index, or -1."""
    depth = 0
    i = open_index
    n = len(text)
    while i < n:
        c = text[i]
        if c == "{":
            depth += 1
        elif c == "}":
            depth -= 1
            if depth == 0:
                return i + 1
        elif c == '"':
            i += 1
            while i < n:
                if text[i] == "\\":
                    i += 2
                    continue
                if text[i] == '"':
                    break
                i += 1
        i += 1
    return -1


def body_is_stub(body):
    """True when the (comment-stripped) body holds only placeholder statements."""
    statements = [s.strip() for s in body.split(";") if s.strip()]
    if not statements:
        return True
    if len(statements) > 1:
        return False
    return bool(STUB_BODIES.match(statements[0] + ";"))


def has_reconstructed_banner(prefix):
    return "RECONSTRUCTED" in prefix


def scan_file(path):
    with open(path, "r", encoding="utf-8", errors="replace") as fh:
        raw = fh.read()
    clean = strip_comments(raw)
    hits = []
    for m in DECL.finditer(clean):
        brace = m.end() - 1
        end = match_brace(clean, brace)
        if end < 0:
            continue
        body = clean[brace + 1:end - 1]
        # Nested members (a body containing another declaration) are containers, not members.
        if "{" in body:
            continue
        if not body_is_stub(body):
            continue
        # Look back a bounded window for the reconstruction banner that would explain this body.
        prefix = clean[max(0, m.start() - 3000):m.start()]
        if has_reconstructed_banner(prefix):
            continue
        line = clean.count("\n", 0, m.start()) + 1
        signature = " ".join(m.group("sig").split())
        hits.append({"line": line, "signature": signature})
    return hits


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--json", default=None)
    ap.add_argument("--root", default=GAME_ROOT)
    args = ap.parse_args()

    if not os.path.isdir(args.root):
        print(f"game source root not found: {args.root}", file=sys.stderr)
        return 2

    per_assembly = Counter()
    per_file = defaultdict(list)
    for dirpath, dirnames, filenames in os.walk(args.root):
        dirnames[:] = [d for d in dirnames if d not in SKIP_DIRS]
        for name in filenames:
            if not name.endswith(".cs"):
                continue
            path = os.path.join(dirpath, name)
            hits = scan_file(path)
            if not hits:
                continue
            rel = os.path.relpath(path, args.root)
            assembly = rel.split(os.sep)[0]
            per_assembly[assembly] += len(hits)
            per_file[rel] = hits

    total = sum(per_assembly.values())
    print(f"game source root : {args.root}")
    print(f"files with stubs : {len(per_file)}")
    print(f"stub members     : {total}")
    print()
    print("per assembly:")
    for assembly, count in per_assembly.most_common():
        print(f"  {count:5d}  {assembly}")
    print()
    print("worst files:")
    for rel, hits in sorted(per_file.items(), key=lambda kv: -len(kv[1]))[:25]:
        print(f"  {len(hits):5d}  {rel}")
    print()
    print("sample stub members (first 40):")
    shown = 0
    for rel, hits in sorted(per_file.items(), key=lambda kv: -len(kv[1])):
        for hit in hits:
            print(f"  {rel}:{hit['line']}  {hit['signature'][:150]}")
            shown += 1
            if shown >= 40:
                break
        if shown >= 40:
            break

    if args.json:
        out = os.path.abspath(args.json)
        os.makedirs(os.path.dirname(out), exist_ok=True)
        with open(out, "w", encoding="utf-8") as fh:
            json.dump({
                "root": args.root,
                "total_stub_members": total,
                "per_assembly": dict(per_assembly.most_common()),
                "files": {rel: hits for rel, hits in sorted(per_file.items())},
            }, fh, indent=2)
        print(f"\njson -> {out}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
