"""Compress an annotated `disas.py` dump into a semantic skeleton (a reconstruction blueprint).

    python tools/unity-rebuild/disas_skeleton.py <disas-log> [--state]

It keeps the annotation-bearing lines (call targets, strings, type/method-info slots, field accesses)
and drops the IL2CPP boilerplate, so a 1,000-line state machine becomes a readable sequence of what the
recovered method actually does.  Lines are grouped per `<>1__state` value where the dump marks them.
"""
from __future__ import annotations

import argparse
import io
import os
import re
import sys

BOILERPLATE = ("il2cpp_codegen_initialize_runtime_metadata", "[boilerplate]", "lock or dword",
               "InitializeRuntimeMetadata", "GC write barrier")
KEEP = ("-> ", "STR '", "TYPE ", "METHODINFO ", "rcx->", "rdx->", "r8->", "r9->", "rax->",
        "call    0x", "state", "UniTask", "awaiter")
STATE = re.compile(r"\[state\s+(\d+)\]|<>1__state\s*[:=]\s*(-?\d+)|mov\s+dword ptr \[r\w+ \+ 0x0\],\s*(-?\d+)")


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("log")
    ap.add_argument("--max", type=int, default=400)
    args = ap.parse_args()

    lines = io.open(args.log, encoding="utf-8", errors="replace").read().splitlines()
    printed = 0
    for line in lines:
        stripped = line.strip()
        if not stripped or stripped.startswith("=") or stripped.startswith("FUNCTION"):
            continue
        if any(b in line for b in BOILERPLATE):
            continue
        state = STATE.search(line)
        interesting = any(k in line for k in KEEP)
        if not (interesting or state):
            continue
        if printed >= args.max:
            print(f"... (truncated at {args.max} lines; full dump: {args.log})")
            break
        print(line.rstrip())
        printed += 1
    print(f"\n[{printed} semantic lines from {len(lines)} disassembly lines]")
    return 0


if __name__ == "__main__":
    sys.exit(main())
