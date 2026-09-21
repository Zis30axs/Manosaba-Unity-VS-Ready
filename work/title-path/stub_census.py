"""Rank the members of the reconstructed game sources whose bodies are still Il2CppDumper stubs.

    python work/title-path/stub_census.py [assembly-dir-substring ...]

A member counts as a stub when it has an [Address(RVA)] and its body is empty or a single `return default(...)/null/
false/0;` -- and no `// RECONSTRUCTED` comment sits above it.  Its native size comes from the .pdata function table of
the shipped GameAssembly.dll: a stub whose native function is a few bytes (a shared `ret` thunk, a field getter the
dumper already names) is harmless; one backed by hundreds of bytes is behaviour that is silently missing.  Members that
share an RVA are counted once per member (identical-code folding).  Sizes are .pdata function lengths, or, for
leaf / tail-call functions without unwind data, the distance to the next known function start.

Output: work/title-path/stub_census.txt (per assembly totals, then every non-trivial stub, largest first) and
stub_census.json.
"""
import bisect
import json
import pickle
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, "..", ".."))
sys.path.insert(0, os.path.join(REPO, "work", "reversing"))
from il2cpp_pe import PE  # noqa: E402

ROOT = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
TRIVIAL_BYTES = 24          # a `ret`, `xor eax,eax; ret`, `mov rax,[rcx+x]; ret` and friends
MEMBER = re.compile(
    r'\[Address\(RVA = "(0x[0-9A-F]+)"[^\n]*\n((?:[ \t]*\[[^\n]*\n)*)[ \t]*([^\n]*?)\n[ \t]*\{\n(.*?)^[ \t]*\}$', re.S | re.M)
STUB_BODY = re.compile(r"^(return (default\([^)]*\)|null|false|true|0|0f|0L|0u|0\.0f?|\"\"|default);)?$")
CTOR = re.compile(r"^(?:(?:public|protected|private|internal|static)\s+)+\w+\s*\(")
TYPE_DECL = re.compile(r"\b(class|struct|interface)\s+(\w+)")


def main():
    filters = sys.argv[1:]
    pe = PE()
    funcs = pe.pdata_functions()
    exact = {start: end - start for start, end in funcs}
    # Leaf and tail-call functions have no .pdata unwind entry (TweeningButton.OnHighlightingChanged is one), so their
    # length is measured up to the next known function start: .pdata starts plus every method RVA the metadata names.
    db = pickle.load(open(os.path.join(REPO, "work", "reversing", "symdb.pkl"), "rb"))
    boundaries = sorted(set(exact) | set(db["methods_by_rva"]))

    def native_size(rva):
        if rva in exact:
            return exact[rva]
        i = bisect.bisect_right(boundaries, rva)
        return boundaries[i] - rva if i < len(boundaries) else None

    stubs, per_asm = [], {}
    for asm in sorted(os.listdir(ROOT)):
        if filters and not any(f in asm for f in filters):
            continue
        for dirpath, _, files in os.walk(os.path.join(ROOT, asm)):
            for name in files:
                if not name.endswith(".cs"):
                    continue
                path = os.path.join(dirpath, name)
                text = open(path, encoding="utf-8-sig").read()
                for m in MEMBER.finditer(text):
                    body = " ".join((m.group(4) or "").split())
                    stats = per_asm.setdefault(asm, {"members": 0, "stubs": 0, "stub_bytes": 0, "nontrivial": 0})
                    stats["members"] += 1
                    before = text[max(0, m.start() - 600):m.start()]
                    if "RECONSTRUCTED" in before.split("}")[-1]:
                        continue
                    if not STUB_BODY.match(body):
                        continue
                    signature = m.group(3).strip()
                    if signature in ("get", "set", "add", "remove"):
                        prop = re.findall(r"\n[ \t]*(?:public|private|protected|internal)[^\n{;=]*?(\w+)\s*\n[ \t]*\{",
                                          text[:m.start()])
                        signature = (prop[-1] if prop else "?") + "." + signature
                    if "MoveNext" in signature or "SetStateMachine" in signature:
                        continue  # compiler state machines are rebuilt together with their async kickoff
                    if CTOR.match(signature):
                        continue  # constructors: field initializers are ctor_sweep.py's job, the bodies stay empty
                    rva = int(m.group(1), 16)
                    size = native_size(rva)
                    owners = TYPE_DECL.findall(text[:m.start()])
                    owner = owners[-1][1] if owners else "?"
                    stats["stubs"] += 1
                    if size and size > TRIVIAL_BYTES:
                        stats["nontrivial"] += 1
                        stats["stub_bytes"] += size
                        stubs.append({"assembly": asm, "file": os.path.relpath(path, ROOT).replace("\\", "/"),
                                      "type": owner, "member": signature, "rva": "0x%X" % rva, "bytes": size})
    stubs.sort(key=lambda s: -s["bytes"])
    out_txt = os.path.join(HERE, "stub_census.txt")
    with open(out_txt, "w", encoding="utf-8", newline="\n") as fh:
        fh.write("%-58s %8s %6s %10s %10s\n" % ("assembly", "members", "stubs", "nontrivial", "bytes"))
        for asm, s in sorted(per_asm.items(), key=lambda kv: -kv[1]["stub_bytes"]):
            fh.write("%-58s %8d %6d %10d %10d\n" % (asm, s["members"], s["stubs"], s["nontrivial"], s["stub_bytes"]))
        fh.write("\n")
        for s in stubs:
            fh.write("%7d  %-9s %-44s %-40s %s\n" % (s["bytes"], s["rva"], s["type"][:44], s["member"][:40], s["assembly"]))
    json.dump({"trivial_bytes": TRIVIAL_BYTES, "assemblies": per_asm, "stubs": stubs},
              open(os.path.join(HERE, "stub_census.json"), "w", encoding="utf-8"), indent=1)
    print("%d non-trivial stubs, %d bytes -> %s" % (len(stubs), sum(s["bytes"] for s in stubs), out_txt))


if __name__ == "__main__":
    main()
