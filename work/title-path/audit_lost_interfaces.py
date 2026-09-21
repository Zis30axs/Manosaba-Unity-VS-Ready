"""Find reconstructed game classes whose declaration lost an interface the shipped class implements.

    python work/title-path/audit_lost_interfaces.py [--json out.json]

The shipped declaration comes from src/il2cpp/dump.cs ("public class X : Base, IFoo, IBar // TypeDefIndex: n").  Only
interfaces DEFINED IN THE GAME SOURCES (Assets/Scripts/Game, "interface IFoo") are checked: package/BCL interfaces are
usually re-listed by the dumper because a base class implements them.  An interface is reported as lost when the
reconstructed declaration does not name it and none of the interfaces it does name (or its base class) is known to
extend it.  Found by G4b round 3: LocalizationManagerExtended lacked ILocalizationManagerExtended, so
Engine.GetService<ILocalizationManagerExtended>() failed and OptionsUi.Initialize stalled engine initialization.
"""
import json
import os
import re
import sys
from collections import defaultdict

ROOT = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
DUMP = os.path.join(ROOT, "src", "il2cpp", "dump.cs")
GAME = os.path.join(ROOT, "UnityProject", "Assets", "Scripts", "Game")

DECL = re.compile(r"^(?:public|internal|private|protected)?[\w\s]*?\b(class|struct|interface)\s+([\w.`<>,\s]+?)\s*(?::\s*(.+?))?\s*(?://.*)?$")


def split_bases(text):
    out, depth, cur = [], 0, ""
    for ch in text:
        if ch == "<":
            depth += 1
        elif ch == ">":
            depth -= 1
        if ch == "," and depth == 0:
            out.append(cur.strip())
            cur = ""
        else:
            cur += ch
    if cur.strip():
        out.append(cur.strip())
    return out


def simple(name):
    name = re.sub(r"<.*>", "", name).strip()
    return name.split(".")[-1]


def main():
    # game-defined interfaces and what they extend (from our sources, which keep interface inheritance)
    game_interfaces = {}
    source_decls = defaultdict(list)     # simple class name -> [(file, bases)]
    for dirpath, _, files in os.walk(GAME):
        for f in files:
            if not f.endswith(".cs"):
                continue
            path = os.path.join(dirpath, f)
            with open(path, encoding="utf-8-sig", errors="replace") as fh:
                for line in fh:
                    s = line.strip()
                    if not re.match(r"^(public|internal|private|protected|sealed|abstract|static|partial|\s)*\s*(class|struct|interface)\s", s):
                        continue
                    m = re.match(r"^[\w\s]*?\b(class|struct|interface)\s+(\w+)(?:<[^>]*>)?\s*(?::\s*([^{]+))?", s)
                    if not m:
                        continue
                    kind, name, bases = m.group(1), m.group(2), [simple(b) for b in split_bases(m.group(3) or "")]
                    if kind == "interface":
                        game_interfaces[name] = bases
                    else:
                        source_decls[name].append((os.path.relpath(path, ROOT), bases))

    def closure(names):
        seen, todo = set(), list(names)
        while todo:
            n = todo.pop()
            if n in seen:
                continue
            seen.add(n)
            todo.extend(game_interfaces.get(n, []))
        return seen

    lost = []
    with open(DUMP, encoding="utf-8", errors="replace") as fh:
        for line in fh:
            if not line.startswith(("public class", "internal class", "public sealed class", "internal sealed class",
                                    "public abstract class", "internal abstract class", "public struct", "internal struct")):
                continue
            m = re.match(r"^[\w\s]*?\b(class|struct)\s+([\w.`<>,\s]+?)\s*:\s*(.+?)\s*//", line)
            if not m:
                continue
            name = simple(m.group(2).split("<")[0])
            if "." in m.group(2).split("<")[0]:
                continue                            # nested (Outer.Inner): the source file nests it differently
            shipped = [simple(b) for b in split_bases(m.group(3))]
            wanted = [b for b in shipped if b in game_interfaces]
            if not wanted or name not in source_decls:
                continue
            for path, bases in source_decls[name]:
                have = closure(bases)
                missing = [w for w in wanted if w not in have]
                if missing:
                    lost.append({"class": name, "file": path, "missing": missing, "shipped_bases": shipped,
                                 "source_bases": bases})
    print("classes with a lost game interface: %d" % len(lost))
    for r in lost:
        print("  %-40s missing %-45s %s" % (r["class"], ",".join(r["missing"]), r["file"]))
    if "--json" in sys.argv:
        with open(sys.argv[sys.argv.index("--json") + 1], "w", encoding="utf-8") as fh:
            json.dump(lost, fh, ensure_ascii=False, indent=1)
    return 1 if lost else 0


if __name__ == "__main__":
    sys.exit(main())
