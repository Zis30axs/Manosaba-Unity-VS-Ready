"""Print the game-start probe evidence (G4b) plus the reconstructed-vs-stub census of the applied sources.

    python tools/unity-rebuild/print_game_start.py
"""
import io
import json
import os
import re

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
EVIDENCE = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "game_start_evidence.json")
GATE = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "g4b_game_start.json")
SOURCES = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")

RECONSTRUCTED = re.compile(r"//\s*RECONSTRUCTED")
METHOD = re.compile(
    r"(?m)^[ \t]*(?:public|private|protected|internal)[^\n;{}=]*\([^;{}\n]*\)[ \t\r]*$"
)


def census():
    rows = []
    for asm in sorted(os.listdir(SOURCES)):
        asm_dir = os.path.join(SOURCES, asm)
        if not os.path.isdir(asm_dir):
            continue
        files = methods = reconstructed = 0
        for root, _dirs, names in os.walk(asm_dir):
            for name in names:
                if not name.endswith(".cs"):
                    continue
                files += 1
                text = io.open(os.path.join(root, name), encoding="utf-8", errors="replace").read()
                methods += len(METHOD.findall(text))
                reconstructed += len(RECONSTRUCTED.findall(text))
        if files:
            rows.append((asm, files, reconstructed, methods))
    return rows


def main():
    with io.open(GATE, encoding="utf-8-sig") as fh:
        gate = json.load(fh)
    with io.open(EVIDENCE, encoding="utf-8-sig") as fh:
        doc = json.load(fh)

    print(f"gate status        : {gate['status']}  (play mode exit {gate['play_mode']['exit_code']}, "
          f"{gate['play_mode']['seconds']}s)")
    print(f"evidence status    : {doc['status']}")
    print(f"entry type         : {doc['entry_type']}  resolved={doc['type_resolved']}")
    print(f"component created  : {doc['component_created']}  ({doc.get('component_type')})")
    print(f"Awake effect       : addressables field found={doc['addressables_field_found']} "
          f"manager created={doc['addressables_manager_created']} ({doc.get('addressables_manager_type')})")
    print(f"declared methods   : {doc['declared_method_count']}")
    print(f"Start() (direct)   : {doc['start_invocation']}")
    print(f"log                : errors={doc['log_errors']} exceptions={doc['log_exceptions']}")
    if doc.get("first_exception"):
        print(f"first exception    : {doc['first_exception']}")
    for entry in doc.get("log", [])[:4]:
        print(f"  [{entry['type']}] {entry['message'][:160]}")
        for line in (entry.get("stack") or "").split("\n")[:3]:
            print(f"        {line.strip()[:150]}")

    print("\nreconstructed-method census of the applied game sources:")
    rows = census()
    total_files = sum(r[1] for r in rows)
    total_recon = sum(r[2] for r in rows)
    total_methods = sum(r[3] for r in rows)
    print(f"  {total_files} files, {total_methods} method signatures, "
          f"{total_recon} RECONSTRUCTED markers")
    for asm, files, recon, methods in sorted(rows, key=lambda r: -r[2])[:8]:
        print(f"    {asm:52} {recon:5} reconstructed / {methods:5} methods")


if __name__ == "__main__":
    main()
