"""Independent structural check of the converted C# tree.

Counts braces after removing comments, string literals, char literals, verbatim strings and
preprocessor lines. This is a sanity check on the namespace converter, not a compiler: it
proves the converter did not unbalance the files it rewrote.
"""
import json
import os
import re
import sys

TREE = sys.argv[1] if len(sys.argv) > 1 else \
    r"E:\Manosaba-VSCodeReady\tools\unity-rebuild\converted"

BLOCK_COMMENT = re.compile(r"/\*.*?\*/", re.S)
LINE_COMMENT = re.compile(r"//[^\n]*")
VERBATIM = re.compile(r'@"(?:[^"]|"")*"', re.S)
INTERPOLATED_VERBATIM = re.compile(r'\$@"(?:[^"]|"")*"', re.S)
STRING = re.compile(r'"(?:\\.|[^"\\])*"', re.S)
CHAR = re.compile(r"'(?:\\.|[^'\\])*'")


def strip_code(text):
    text = BLOCK_COMMENT.sub(" ", text)
    text = LINE_COMMENT.sub("", text)
    text = INTERPOLATED_VERBATIM.sub(" ", text)
    text = VERBATIM.sub(" ", text)
    text = STRING.sub(" ", text)
    text = CHAR.sub(" ", text)
    return "\n".join(l for l in text.split("\n") if not l.lstrip().startswith("#"))


def main():
    n = 0
    balanced = 0
    unbalanced = []
    file_scoped = []
    for dirpath, dirnames, files in os.walk(TREE):
        dirnames[:] = [d for d in dirnames if d not in ("obj", "bin", "__pycache__")]
        for f in sorted(files):
            if not f.endswith(".cs"):
                continue
            path = os.path.join(dirpath, f)
            n += 1
            with open(path, encoding="utf-8", errors="replace") as fh:
                raw = fh.read()
            code = strip_code(raw)
            if re.search(r"^\s*namespace\s+[\w.]+\s*;\s*$", code, re.M):
                file_scoped.append(os.path.relpath(path, TREE))
            if code.count("{") == code.count("}"):
                balanced += 1
            else:
                unbalanced.append({"file": os.path.relpath(path, TREE).replace(os.sep, "/"),
                                   "open": code.count("{"), "close": code.count("}")})
    result = {
        "tree": TREE,
        "files": n,
        "brace_balanced": balanced,
        "brace_unbalanced": len(unbalanced),
        "remaining_file_scoped_namespaces": len(file_scoped),
        "unbalanced_sample": unbalanced[:20],
        "file_scoped_sample": file_scoped[:20],
    }
    print(json.dumps(result, ensure_ascii=False, indent=1))
    return 0 if not unbalanced and not file_scoped else 1


if __name__ == "__main__":
    sys.exit(main())
