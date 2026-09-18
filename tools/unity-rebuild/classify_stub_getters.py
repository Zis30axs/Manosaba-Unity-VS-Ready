#!/usr/bin/env python3
"""Classify the trivial-return stubs in the reconstructed game sources by disassembling their RVA.

The C# under `UnityProject/Assets/Scripts/Game/**` was machine-translated out of IL2CPP
metadata, so every method body whose IL was not hand-recovered is a placeholder:

    [Token(Token = "0x1700000D")]
    public string Name
    {
    	[Token(Token = "0x6000085")]
    	[Address(RVA = "0x43E960", Offset = "0x43D360", VA = "0x18043E960")]
    	get { return null; }
    }

The `[Address(RVA = ...)]` attribute is the ground truth for where the real body lives, and
almost every one of these bodies is one of a handful of shapes.  This tool finds every such
site, disassembles its RVA through `work/reversing/disas.py`, and sorts it into:

  FIELD_READ   the body is a single load of an instance field into the return register
               (`mov rax,[rcx+0x18]; ret`).  The offset is resolved back to the field *name*
               via the `[FieldOffset(Offset = "0x18")]` attribute on the field declaration in
               the same reconstructed source, so `proposed_body` is a real `return _field;`.

  SERVICE_GET  the lazy-cache pattern: read the field, test it for null, call a service
               accessor, store the result back (`??=`).  Two accessors are recognised and are
               told apart by the `METHODINFO` the disassembler prints plus the call target:

                 ServiceLocator.Get<T>(true)      -> `mov cl, 1` before the call
                 Naninovel.Engine.GetServiceOrErr<T>()

               The type argument comes from the `METHODINFO Method$...Get<T>()` annotation, not
               from the declared property type; when the two disagree it is reported.

  OTHER        anything else.  The RVA, `.pdata` size, a reason code and the first ~30
               annotated instructions are recorded so a human can decide.  No body is guessed.

Honesty rules enforced by construction:

  * A site is only classified when the disassembly *shows* the shape.  Every bucket carries the
    decisive instruction(s) in `evidence`.
  * `FIELD_READ` requires the member to be an instance member.  In a static method RCX is the
    first real argument (or `MethodInfo*`), not `this`, so `[rcx+off]` proves nothing there.
  * Nothing is inferred from `return null;` / `return default(T)` alone: the placeholder text
    only says where to look.

The toolchain in `work/reversing` is imported, not reimplemented (`il2cpp_pe`, `il2cpp_db`,
`disas`); `disas.annotate` is called with its module-level PE/symbol-DB/capstone factories
patched to shared instances so its own caches are loaded once instead of once per RVA.

Usage (the interpreter that has capstone is tools/venv):

    $py = "E:\\Manosaba-VSCodeReady\\tools\\venv\\Scripts\\python.exe"
    & $py tools/unity-rebuild/classify_stub_getters.py                 # full run, writes artifacts
    & $py tools/unity-rebuild/classify_stub_getters.py --limit 50      # first 50 sites only
    & $py tools/unity-rebuild/classify_stub_getters.py --file WitchTrials.Game/WitchTrials.Views/TitleUi.cs
    & $py tools/unity-rebuild/classify_stub_getters.py --check         # regression guard, exit 1 on drift

Artifacts (never source edits):

    tools/unity-rebuild/out/unity/stub_getter_plan.json
    tools/unity-rebuild/out/unity/stub_getter_plan.md

This script is read-only with respect to `UnityProject/`.

Two independent checkers live next to it and are worth re-running after any change here:

    work/reversing/verify_classifier.py   replays the classifier over the getters an earlier manual
                                          pass already recovered and whose `// RECONSTRUCTED | ...`
                                          comment states the real body shape (a golden set)
    work/reversing/spotcheck_plan.py      re-disassembles a sample of the plan with the standalone
                                          `disas.py` command line and checks that every claim in it
                                          (offset, METHODINFO string, accessor call, `mov cl,1`) is
                                          literally present in that raw output
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import os
import re
import sys

# --------------------------------------------------------------------------- #
# paths / toolchain
# --------------------------------------------------------------------------- #

HERE = os.path.dirname(os.path.abspath(__file__))
REPO = os.path.normpath(os.path.join(HERE, "..", ".."))
REVERSING = os.path.join(REPO, "work", "reversing")
SOURCE_ROOT = os.path.join(REPO, "UnityProject", "Assets", "Scripts", "Game")
OUT_DIR = os.path.join(HERE, "out", "unity")
PLAN_JSON = os.path.join(OUT_DIR, "stub_getter_plan.json")
PLAN_MD = os.path.join(OUT_DIR, "stub_getter_plan.md")

if REVERSING not in sys.path:
    sys.path.insert(0, REVERSING)

import disas  # noqa: E402  (work/reversing/disas.py -- annotated disassembler)
import il2cpp_db  # noqa: E402
import il2cpp_pe  # noqa: E402

IMAGEBASE = 0x180000000

#: IL2CPP runtime scaffolding: never evidence of game logic (README "已识别的运行时 helper").
BOILERPLATE_CALLS = {
    0x250FB0: "il2cpp_codegen_initialize_runtime_metadata",
    0x246350: "il2cpp_codegen_initialize_runtime_metadata (thunk)",
    0x2980E0: "il2cpp_codegen_object_new",
    0x2466E0: "il2cpp_codegen_object_new (thunk)",
    0x24CE60: "interface/virtual dispatch resolve",
    0x243A20: "resolve interface method slot",
    0x22E0: "interface call thunk",
    0x7EC0: "IList<T>.get_Item thunk",
    0x7F50: "IList<T>.get_Item thunk",
    0x246570: "il2cpp_value_box",
}
NRE_THROW = 0x246730

#: The shared empty body (`ret 0`) -- see README pitfall #1.
EMPTY_THUNK = 0x30C000

# --------------------------------------------------------------------------- #
# source lexing
# --------------------------------------------------------------------------- #

# One leftmost-match pass blanks comments and literal contents while preserving every character
# offset (each consumed character becomes a space, newlines survive), so offsets in `clean` index
# the original text directly.
_LITERALS = re.compile(
    r"//[^\n]*"
    r"|/\*.*?\*/"
    r'|@"(?:[^"]|"")*"'
    r'|"(?:\\.|[^"\\\n])*"'
    r"|'(?:\\.|[^'\\\n])*'",
    re.S,
)

TRIVIAL_BODY = re.compile(r"^return\s+(null|default\([^()]*\))\s*;$")

_MODIFIERS = (
    "public|private|protected|internal|static|virtual|override|sealed|abstract|unsafe|extern|"
    "new|readonly|ref|partial|async|volatile|const|fixed|required|scoped"
)
# An attribute is `[...]` at the start of a token.  Requiring a leading space (or the start of the
# header) keeps an indexer's `this[int row]` and an array type's `Row[]` intact -- those have no
# space before the bracket.
_ATTRS = re.compile(r"(?:^|\s)\[[^\[\]]*\]")
_TYPE_DECL = re.compile(
    r"\b(record\s+struct|record\s+class|record|class|struct|interface|enum)"
    r"\s+([A-Za-z_@][A-Za-z0-9_@]*)")


def _type_kind(raw):
    """`record`/`record class` are reference types; `record struct` is a value type."""
    raw = " ".join((raw or "").split())
    if raw == "record struct" or raw == "struct":
        return "struct"
    if raw.startswith("record"):
        return "class"
    return raw
_NS_DECL = re.compile(r"^\s*namespace\s+([A-Za-z0-9_.]+)\s*[;{]", re.M)
_CONTROL = re.compile(
    r"^(if|for|foreach|while|switch|try|catch|finally|lock|using|else|do|unsafe|fixed|checked|"
    r"unchecked|get|set|add|remove)\b"
)
_FIELD_OFFSET = re.compile(
    r"\[(?:[A-Za-z_][A-Za-z0-9_]*\.)?FieldOffset\(\s*Offset\s*=\s*\"(0x[0-9A-Fa-f]+)\"\s*\)\]")
_ADDRESS_ATTR = re.compile(r'Address\(RVA\s*=\s*"(0x[0-9A-Fa-f]+)"')
_TOKEN_ATTR = re.compile(r'Token\(Token\s*=\s*"(0x[0-9A-Fa-f]+)"\)')


def blank_literals(text: str) -> str:
    """Blank comments and string/char literal *contents*, preserving offsets and newlines."""
    def repl(m):
        return "".join("\n" if c == "\n" else " " for c in m.group(0))

    return _LITERALS.sub(repl, text)


def find_scopes(clean: str):
    """All brace blocks of `clean` as {'o','c','hs','header','parent'}.

    `header` is the text between the previous sibling boundary (`;`, `{`, `}` or file start) and
    the opening brace, whitespace-normalised -- i.e. the declaration *and* any attributes, plus
    the accessor keyword when the block is a property accessor.

    `parent` is the innermost enclosing block.
    """
    pairs = []
    stack = []
    for i, ch in enumerate(clean):
        if ch == "{":
            stack.append(i)
        elif ch == "}":
            if stack:
                pairs.append((stack.pop(), i))
    pairs.sort()

    spans = []
    for o, c in pairs:
        j = o - 1
        depth = 0
        while j >= 0:
            ch = clean[j]
            if ch == ")":
                depth += 1
            elif ch == "(":
                if depth == 0:
                    break
                depth -= 1
            elif depth == 0 and ch in ";{}":
                break
            j -= 1
        spans.append({"o": o, "c": c, "hs": j + 1,
                      "header": " ".join(clean[j + 1:o].split()), "parent": None})

    spans.sort(key=lambda s: (s["o"], -s["c"]))
    stack2 = []
    for s in spans:
        while stack2 and not (stack2[-1]["o"] < s["o"] and s["c"] < stack2[-1]["c"]):
            stack2.pop()
        s["parent"] = stack2[-1] if stack2 else None
        stack2.append(s)
    return spans


def _nodesc(header: str) -> str:
    return " ".join(_ATTRS.sub(" ", header).split())


def scope_kind(header: str) -> str:
    h = _nodesc(header)
    if re.search(r"\bget$", h):
        return "accessor-get"
    if re.search(r"\bset$", h):
        return "accessor-set"
    if re.search(r"\b(add|remove)$", h):
        return "accessor-event"
    if _TYPE_DECL.search(h):
        return "type"
    if _CONTROL.match(h):
        return "control"
    if "(" in h and ")" in h:
        return "method"
    return "block"


_MEMBER_DECL = re.compile(
    r"^(?:(?:%s)\s+)*(?P<type>.+?)\s+(?P<name>(?:this\s*\[[^\]]*\]|"
    r"[A-Za-z_@][A-Za-z0-9_@]*(?:\.[A-Za-z_@][A-Za-z0-9_@]*)*))\s*$" % _MODIFIERS
)
_METHOD_DECL = re.compile(
    r"^(?:(?:%s)\s+)*(?:(?P<type>.+?)\s+)?"
    r"(?P<name>operator\s*\S+|~?[A-Za-z_@][A-Za-z0-9_@]*|this\s*\[[^\]]*\])\s*\(" % _MODIFIERS
)


def split_member_decl(header: str):
    """-> (kind, declared_type, name, is_static) for a declaration header (attributes stripped)."""
    h = _nodesc(header)
    h = re.sub(r"\bget$", "", h).strip()
    h = re.sub(r"\bset$", "", h).strip()
    h = re.sub(r"\b(add|remove)$", "", h).strip()
    is_static = bool(re.search(r"\bstatic\b", h))
    h = re.sub(r"\bwhere\b.*$", "", h).strip()          # generic constraints carry no name info
    m = _MEMBER_DECL.match(h)
    if m and "(" not in m.group("type"):
        return "property", m.group("type").strip(), m.group("name").strip(), is_static
    m = _METHOD_DECL.match(h)
    if m:
        return "method", (m.group("type") or "").strip() or None, m.group("name").strip(), is_static
    return "unknown", None, None, is_static


# --------------------------------------------------------------------------- #
# site collection
# --------------------------------------------------------------------------- #

class Site:
    __slots__ = ("kind", "file_rel", "file_abs", "line", "class_full", "class_short", "class_kind",
                 "member", "member_kind", "declared_type", "is_static", "current_body",
                 "rva", "rva_source", "token", "fields")

    def __init__(self, **kw):
        for k in self.__slots__:
            setattr(self, k, kw.get(k))


def parse_fields_of_type(raw: str, left: int, right: int, nested=()):
    """Field-name table of one type body: offset -> [(name, type)] from `[FieldOffset]`.

    `nested` lists the spans of *nested* type bodies (compiler-generated state machines such as
    `<Foo>d__12` are declared inside their owner).  Their fields live at their own offsets and must
    not be attributed to the owner -- blanking them here is what keeps the resolution honest.
    """
    buf = list(raw[left:right])
    for o, c in nested:
        lo, hi = max(o, left) - left, min(c + 1, right) - left
        for i in range(lo, hi):
            if buf[i] != "\n":
                buf[i] = " "
    text = "".join(buf)
    fields = collections.defaultdict(list)
    pending = None
    for line in text.split("\n"):
        stripped = line.strip()
        if not stripped or stripped.startswith("//"):
            continue
        m = _FIELD_OFFSET.search(stripped)
        if m:
            pending = int(m.group(1), 16)
            rest = stripped[m.end():].strip()
            if rest and rest.endswith(";") and "(" not in rest:
                _add_field(fields, pending, rest)
                pending = None
            continue
        if pending is not None:
            if stripped.startswith("["):
                continue                                  # another attribute line
            if stripped.endswith(";"):
                decl = stripped.rstrip(";")
                if "=" in decl:                           # field initializer / property arrow
                    decl = decl.split("=", 1)[0]
                if "(" not in decl:
                    _add_field(fields, pending, decl)
                pending = None
    return dict(fields)


def _add_field(fields, off, decl):
    """Record one instance-field declaration; static fields are ignored.

    Both dump.cs and the reconstructed sources list `static` fields with their *static-block*
    offsets in the same field list, so a static field at 0x20 collides with an unrelated instance
    field at 0x20.  Instance resolution must therefore never see them.
    """
    decl = decl.rstrip(";").strip()
    if re.search(r"\bstatic\b", decl):
        return
    m = re.match(
        r"^(?:(?:%s)\s+)*(?P<type>.+?)\s+(?P<name>[A-Za-z_@][A-Za-z0-9_@]*)\s*$" % _MODIFIERS, decl)
    if not m:
        return
    name, ftype = m.group("name"), m.group("type").strip()
    entry = (name, ftype)
    if entry not in fields[off]:
        fields[off].append(entry)


def parse_bases(header):
    """Every base type / implemented interface named in a type declaration header."""
    h = _nodesc(header)
    if ":" not in h:
        return []
    tail = h.split(":", 1)[1]
    tail = re.split(r"\bwhere\b", tail)[0]
    return [b.strip() for b in tail.split(",") if b.strip()]


def build_type_index(root: str):
    """full type name -> {'fields', 'base', 'ns', 'short', 'kind'} over the whole source tree.

    Also keeps a short-name lookup so an inherited field (the common case in this codebase: the
    getter lives in a derived `Command`/`UiBehaviour` and the backing field is declared in the
    base) can be resolved by walking the base chain.
    """
    index = collections.defaultdict(list)
    for dirpath, dirnames, filenames in os.walk(root):
        dirnames.sort()
        for name in sorted(filenames):
            if not name.endswith(".cs"):
                continue
            path = os.path.join(dirpath, name)
            with open(path, encoding="utf-8-sig", newline="") as fh:
                raw = fh.read()
            clean = blank_literals(raw)
            ns = _NS_DECL.search(raw)
            namespace = ns.group(1) if ns else ""
            scopes = find_scopes(clean)

            # direct children of every scope, so nested types can be blanked out
            children = collections.defaultdict(list)
            for s in scopes:
                if s["parent"] is not None:
                    children[id(s["parent"])].append(s)

            for s in scopes:
                if scope_kind(s["header"]) != "type":
                    continue
                tm = _TYPE_DECL.search(_nodesc(s["header"]))
                if not tm:
                    continue
                chain, node = [tm.group(2)], s["parent"]
                while node is not None:
                    if scope_kind(node["header"]) == "type":
                        m2 = _TYPE_DECL.search(_nodesc(node["header"]))
                        if m2:
                            chain.append(m2.group(2))
                    node = node["parent"]
                chain.reverse()
                dotted = ".".join(chain)
                full = (namespace + "." + dotted) if namespace else dotted
                nested = [(c["o"], c["c"]) for c in children.get(id(s), ())
                          if scope_kind(c["header"]) == "type"]
                head = _nodesc(s["header"])
                bases = parse_bases(s["header"])
                index[full].append({
                    "fields": parse_fields_of_type(raw, s["o"] + 1, s["c"], nested),
                    "base": bases[0] if bases else None, "bases": bases,
                    "ns": namespace, "short": tm.group(2), "kind": _type_kind(tm.group(1)),
                })
    return index


def short_name_map(index):
    out = collections.defaultdict(list)
    for k, v in index.items():
        out[v[0]["short"]].append(k)
    return out


def derives_from(index, short_map, sub, sup, max_depth=6):
    """True when type `sub` is (transitively) `sup`, by short name, per the reconstructed sources."""
    if not sub or not sup:
        return False
    if _norm_type(sub) == _norm_type(sup):
        return True
    seen = set()
    frontier = list(_resolve_any(index, short_map, sub))
    for _ in range(max_depth):
        if not frontier:
            return False
        nxt = []
        for name in frontier:
            if name in seen:
                continue
            seen.add(name)
            entry = index.get(name)
            if not entry:
                continue
            for b in entry[0]["bases"]:
                if _norm_type(b) == _norm_type(sup):
                    return True
                nxt.extend(_resolve_any(index, short_map, b))
        frontier = nxt
    return False


def _resolve_any(index, short_map, name):
    if name in index:
        return [name]
    return list(short_map.get(_norm_type(name) or "", ()))


def resolve_type_name(index, name, namespace):
    """Best-effort full-name lookup for a (possibly namespace-less) base class name."""
    if name in index:
        return name
    cands = [k for k, v in index.items() if v[0]["short"] == name.rsplit(".", 1)[-1]]
    if not cands:
        return None
    same_ns = [c for c in cands if index[c][0]["ns"] == namespace]
    if len(same_ns) == 1:
        return same_ns[0]
    return cands[0] if len(cands) == 1 else None


def fields_with_bases(index, class_full, max_depth=8):
    """(fields, classes_walked) -- the class's own `[FieldOffset]` table plus its base chain.

    Derived fields are appended first, so when an offset is declared in both a derived and a base
    class the derived one wins.
    """
    fields = {}
    walked = []
    name = class_full
    for _ in range(max_depth):
        entry = index.get(name)
        if not entry:
            break
        e = entry[0]
        walked.append(name)
        for off, hits in e["fields"].items():
            for h in hits:
                if h not in fields.setdefault(off, []):
                    fields[off].append(h)
        if not e["base"]:
            break
        name = resolve_type_name(index, e["base"], e["ns"])
        if not name:
            break
    return fields, walked


def collect_sites(root: str):
    """Walk the reconstructed sources and return (getter_sites, method_sites)."""
    index = build_type_index(root)
    getters, methods = [], []
    for dirpath, dirnames, filenames in os.walk(root):
        dirnames.sort()
        for name in sorted(filenames):
            if not name.endswith(".cs"):
                continue
            path = os.path.join(dirpath, name)
            rel = os.path.relpath(path, REPO).replace(os.sep, "/")
            with open(path, encoding="utf-8-sig", newline="") as fh:
                raw = fh.read()
            clean = blank_literals(raw)
            if len(clean) != len(raw):                     # offsets must line up exactly
                raise AssertionError("lexer lost offset alignment in " + rel)
            ns = _NS_DECL.search(raw)
            namespace = ns.group(1) if ns else ""

            scopes = find_scopes(clean)
            for s in scopes:
                body = clean[s["o"] + 1:s["c"]].strip()
                if not TRIVIAL_BODY.match(body):
                    continue
                kind = scope_kind(s["header"])
                if kind not in ("accessor-get", "method"):
                    continue

                attr_text = raw[s["hs"]:s["o"]]
                addr = _ADDRESS_ATTR.search(attr_text)
                tok = _TOKEN_ATTR.search(attr_text)

                # enclosing type chain
                chain, node = [], s["parent"]
                while node is not None:
                    if scope_kind(node["header"]) == "type":
                        tm = _TYPE_DECL.search(_nodesc(node["header"]))
                        if tm:
                            chain.append((tm.group(2), _type_kind(tm.group(1))))
                    node = node["parent"]
                chain.reverse()
                if chain:
                    short = chain[-1][0]
                    class_kind = chain[-1][1]
                    dotted = ".".join(n for n, _k in chain)
                    class_full = (namespace + "." + dotted) if namespace else dotted
                    fields = fields_with_bases(index, class_full)[0]
                else:
                    short = class_full = None
                    class_kind = None
                    fields = {}

                if kind == "accessor-get":
                    owner = s["parent"]
                    if owner is None or scope_kind(owner["header"]) not in (
                            "block", "type", "method"):
                        continue
                    owner_header = owner["header"]
                    mkind, dtype, mname, is_static = split_member_decl(owner_header)
                    if mname is None or mkind == "unknown":
                        continue
                    if mkind == "method" and "(" in _nodesc(owner_header):
                        continue                              # a `{ return X; }` method body
                    member_kind = "property-getter"
                    if addr is None:
                        # some files carry [Token]/[Address] on the property declaration and leave
                        # the accessor bare -- the property's attributes are the only RVA source
                        owner_attr = raw[owner["hs"]:owner["o"]]
                        addr = _ADDRESS_ATTR.search(owner_attr)
                        tok = tok or _TOKEN_ATTR.search(owner_attr)
                        if addr is not None:
                            attr_text = owner_attr
                else:
                    mkind, dtype, mname, is_static = split_member_decl(s["header"])
                    if mname is None:
                        continue
                    member_kind = "method"

                site = Site(
                    kind="getter" if kind == "accessor-get" else "method",
                    file_rel=rel, file_abs=path,
                    line=clean.count("\n", 0, s["o"]) + 1,
                    class_full=class_full, class_short=short, class_kind=class_kind,
                    member=mname, member_kind=member_kind,
                    declared_type=dtype, is_static=is_static,
                    current_body=" ".join(raw[s["o"] + 1:s["c"]].split()),
                    rva=int(addr.group(1), 16) if addr else None,
                    rva_source="Address-attribute" if addr else None,
                    token=tok.group(1) if tok else None,
                    fields=fields,
                )
                (getters if kind == "accessor-get" else methods).append(site)
    getters.sort(key=lambda s: (s.file_rel, s.line))
    methods.sort(key=lambda s: (s.file_rel, s.line))
    return getters, methods, index


# --------------------------------------------------------------------------- #
# disassembly (toolchain reuse)
# --------------------------------------------------------------------------- #

class Disassembler:
    """`disas.annotate` with its factories pinned to shared, loaded-once instances."""

    def __init__(self):
        self.pe = il2cpp_pe.PE()
        self.db = il2cpp_db.build()
        self.md = il2cpp_pe.make_cs()
        self._pdata = dict(self.pe.pdata_functions())
        disas.PE = lambda *a, **k: self.pe
        disas.build_db = lambda *a, **k: self.db
        disas.make_cs = lambda *a, **k: self.md
        self._cache = {}
        self._by_short = {}
        for k in self.db.types:
            self._by_short.setdefault(k.rsplit(".", 1)[-1], []).append(k)

    def type_entry(self, full, short):
        """dump.cs field table for a type.  Nested types are keyed without their namespace."""
        if full and full in self.db.types:
            return self.db.types[full]
        if full:
            parts = full.split(".")
            for k in range(1, len(parts)):        # a nested type loses the namespace prefix
                key = ".".join(parts[k:])
                if key in self.db.types:
                    return self.db.types[key]
        cands = self._by_short.get(short or "", ())
        if len(cands) == 1:
            return self.db.types[cands[0]]
        if full:
            ns = full.rsplit(".", 1)[0] if "." in full else ""
            same = [c for c in cands if c.startswith(ns + ".") and c.count(".") == full.count(".")]
            if len(same) == 1:
                return self.db.types[same[0]]
        return None

    def pdata_size(self, rva):
        end = self._pdata.get(rva)
        return (end - rva) if end is not None else None

    def alias_count(self, rva):
        return len(self.db.method_names(rva))

    def dump(self, rva, typename):
        """Annotated disassembly text; cached per RVA (body folding makes RVAs repeat a lot).

        The cache also remembers which typename produced the `rcx->Type.field` annotations so a
        cache hit from a different owner never leaks a wrong field name into the evidence.
        """
        hit = self._cache.get(rva)
        if hit is not None:
            text, owner = hit
            return text, (owner == typename)
        text = disas.annotate(rva, typename, maxins=2000, show_boiler=True)
        self._cache[rva] = (text, typename)
        return text, True


_INSN = re.compile(r"^  (0x[0-9A-Fa-f]+)  (\S+)(?: +(.*?))?\s*$")
_HEADER_SIZE = re.compile(r"size 0x([0-9A-Fa-f]+) \(")


def parse_disas(text: str):
    """Annotated `disas.py` output -> (instructions, header_size_bytes).

    instructions: list of (va, mnemonic, op_str, [comments]).
    """
    lines = text.split("\n")
    size = None
    for ln in lines:
        m = _HEADER_SIZE.search(ln)
        if m:
            size = int(m.group(1), 16)
            break
    seps = [i for i, ln in enumerate(lines) if ln.startswith("====")]
    body = lines[seps[-1] + 1:] if len(seps) >= 2 else lines
    insns = []
    for ln in body:
        if not ln.startswith("  "):
            continue                                       # branch label / header line
        left, _, comment = ln.partition("; ")
        m = _INSN.match(left)
        if not m:
            continue
        va = int(m.group(1), 16)
        cmts = [c.strip() for c in comment.split(" | ")] if comment else []
        insns.append((va, m.group(2), (m.group(3) or "").strip(), cmts))
    return insns, size


_WIDTH = {"byte": 1, "word": 2, "dword": 4, "qword": 8, "xmmword": 16}
# capstone prints a displacement of 0..9 without the `0x` prefix (`[rcx + 4]`) and everything else
# with it (`[rcx + 0xa]`) -- missing the bare form silently drops every field at offset 0x1..0x9.
_DISP = r"(?: \+ (0x[0-9a-f]+|[0-9]))?"
_MEM = re.compile(r"^(?:(byte|word|dword|qword|xmmword) ptr )?\[(r[a-z0-9]+)%s\]$" % _DISP)
_RET_REGS = {"rax", "eax", "ax", "al", "xmm0"}
_NOP_LIKE = {"endbr64", "nop", "int3", "ud2", "hlt"}


def _split(ops):
    return [p.strip() for p in ops.split(",")] if ops else []


def _mem_src(op):
    m = _MEM.match(op)
    if not m:
        return None
    return _WIDTH.get(m.group(1), 0), m.group(2), int(m.group(3), 16) if m.group(3) else 0


def _mem_dst(op):
    return _mem_src(op)


#: mnemonics whose first operand is written (used only to tell a load from a store)
_WRITE_MNEMONICS = {
    "mov", "movss", "movsd", "movups", "movaps", "movdqu", "movdqa", "movd", "movq", "movzx",
    "movsx", "movsxd", "or", "and", "xor", "add", "sub", "inc", "dec", "not", "neg", "bts",
    "btr", "cmpxchg", "xchg", "shl", "sal", "shr", "sar",
}


def _insn_mems(insn):
    """[('r'|'w', (width, base, off))] for each *object* memory operand of one instruction.

    Two things are deliberately excluded:

      * `lea` -- an address computation, not an access.  The IL2CPP GC write barrier is full of
        `lea r8, [obj+off]` and must not be counted as a load or a store.
      * `[rsp+..]` / `[rbp+..]` -- stack spills and locals, not fields.

    Only single-operand memory references are recognised (`mov eax, [rcx+0x10]`,
    `mov [rcx+0x10], rax`), which is all these getters ever use.
    """
    _va, mn, ops, _c = insn
    if mn == "lea":
        return []
    out = []
    for k, p in enumerate(_split(ops)):
        m = _mem_src(p)
        if m and m[1] not in ("rsp", "rbp", "rip"):
            out.append(("w" if (k == 0 and mn in _WRITE_MNEMONICS) else "r", m))
    return out


def _call_target(op):
    m = re.match(r"^(0x[0-9A-Fa-f]+)$", op)
    return int(m.group(1), 16) - IMAGEBASE if m else None


# --------------------------------------------------------------------------- #
# classification
# --------------------------------------------------------------------------- #

ANY_RETURN_LOAD = {"mov", "movzx", "movsx", "movsxd", "movss", "movsd", "movups", "movaps",
                   "movdqu", "movdqa", "movd", "movq"}


class Classifier:
    def __init__(self, dis: Disassembler, type_index=None):
        self.dis = dis
        self.index = type_index or {}
        self.short_map = short_name_map(self.index)

    def type_arg_relation(self, type_arg, declared):
        """How the accessor's METHODINFO type argument relates to the declared member type.

        They usually match exactly.  When they do not, the interesting case is the accessor asking
        for a *derived* interface (`ServiceLocator.Get<IAddressablesManager>()` stored into an
        `IAddressableAssetLoader` field): legal, and the recovered body must name the METHODINFO's
        type argument, not the declared one.
        """
        if not type_arg or not declared:
            return "unknown"
        if _norm_type(type_arg) == _norm_type(declared):
            return "equal"
        if derives_from(self.index, self.short_map, type_arg, declared):
            return "type-arg-derives-from-declared"
        if derives_from(self.index, self.short_map, declared, type_arg):
            return "declared-derives-from-type-arg"
        return "unrelated"

    # ---- helpers ------------------------------------------------------- #
    @staticmethod
    def _this_regs(insns):
        """Registers that hold `this`: RCX plus anything it is copied into before the body."""
        regs = {"rcx"}
        for _va, mn, ops, _c in insns[:6]:
            if mn == "mov":
                parts = _split(ops)
                if len(parts) == 2 and parts[1] == "rcx":
                    regs.add(parts[0])
        return regs

    @staticmethod
    def _field_candidates(site, off, width):
        """Field declarations of the site's class at `off`, width-filtered."""
        hits = list(site.fields.get(off, ()))
        if width:
            filtered = [h for h in hits if _type_width(h[1]) in (None, width)]
            if filtered:
                hits = filtered
        return hits

    def _meta_field_candidates(self, site, off, width):
        """Field declarations of `site`'s type at `off`, from the dumper's metadata.

        This matters because IL2CPP folds identical bodies, so one 5-byte RVA can be shared by
        hundreds of getters of unrelated classes; only the *owner's* field table can name the
        field.  It also covers names the reconstructed source lost -- compiler-generated
        auto-property backing fields (`<ActorId>k__BackingField`) are in dump.cs but were dropped
        by the source conversion.
        """
        t = self.dis.type_entry(site.class_full, site.class_short)
        if not t:
            return []
        hits = [(n, ft) for n, ft in t["fields"].get(off, ())
                if not re.search(r"\bstatic\b", ft or "")]
        if width:
            filtered = [h for h in hits if _type_width(h[1]) in (None, width)]
            if filtered:
                hits = filtered
        return hits

    def _resolve_field(self, site, off, width, cmts=None):
        """(name, type, note, source) for the field at `off`.

        Ordered, and the chosen source is always reported:

          1. `[Il2CppDummyDll.FieldOffset(Offset = "0x..")]` in the reconstructed source, own class
             first and then its base chain -- this is what the source actually declares;
          2. the dumper's field table for the owner type (dump.cs) -- used when the source has no
             declaration at that offset, e.g. for a compiler-generated backing field.
        """
        hits = self._field_candidates(site, off, width)
        if hits:
            if len(hits) == 1:
                return hits[0][0], hits[0][1], None, "source-FieldOffset"
            decl = (site.declared_type or "").strip()
            if decl:
                tight = [h for h in hits if _types_agree(h[1], decl)]
                if len(tight) == 1:
                    return tight[0][0], tight[0][1], "resolved-by-declared-type", \
                        "source-FieldOffset"
            return None, None, "ambiguous-source-field:%s" % ",".join(h[0] for h in hits), \
                "source-FieldOffset"
        meta = self._meta_field_candidates(site, off, width)
        if meta:
            if len(meta) == 1:
                return meta[0][0], meta[0][1], "not-in-reconstructed-source", "dump.cs-metadata"
            decl = (site.declared_type or "").strip()
            if decl:
                tight = [h for h in meta if _types_agree(h[1], decl)]
                if len(tight) == 1:
                    return tight[0][0], tight[0][1], "not-in-reconstructed-source", \
                        "dump.cs-metadata"
            return None, None, "ambiguous-dump.cs-field:%s" % ",".join(h[0] for h in meta), \
                "dump.cs-metadata"
        return None, None, "no-field-at-offset-in-source-or-metadata", None

    # ---- buckets ------------------------------------------------------- #
    def classify(self, site):
        """-> dict(bucket, reason, proposed_body, evidence, extras, disas_head, ...)."""
        out = {"bucket": "OTHER", "reason": None, "proposed_body": None, "evidence": "",
               "disas_head": [], "field_offset": None, "field_name": None, "field_type": None,
               "methodinfo": None, "service_variant": None, "type_arg": None,
               "type_arg_matches_declared": None, "pdata_size": None, "body_bytes": None,
               "alias_count": None}

        if site.rva is None:
            out["reason"] = "no-rva-in-metadata"
            out["evidence"] = ("no [Address(RVA=...)] on this member in the reconstructed source "
                               "(the dumper emitted no address; typical for a generic type that "
                               "only has per-instantiation RVAs)")
            out["pdata_size"] = None
            return out

        rva = site.rva
        out["pdata_size"] = self.dis.pdata_size(rva)
        out["alias_count"] = self.dis.alias_count(rva)

        text, _same_owner = self.dis.dump(rva, site.class_full)
        insns, hdr_size = parse_disas(text)
        out["body_bytes"] = hdr_size
        out["disas_head"] = _format_head(insns, 30)

        if not insns:
            out["reason"] = "no-instructions-decoded"
            out["evidence"] = "disas.py decoded nothing at RVA 0x%X" % rva
            return out

        significant = [i for i in insns if i[1] not in _NOP_LIKE]
        # IL2CPP emits a one-time type-init guard prologue; it carries no game logic.  `this_regs`
        # is computed from the *unstripped* body, because the guard's `mov rbx, rcx` is what proves
        # the callee-saved register holds `this` further down.
        this_regs = self._this_regs(significant)
        significant = _strip_init_guard(significant)

        if not significant:
            out["reason"] = "empty-body"
            out["evidence"] = "body is only ret/nop"
            return out

        if all(i[1] == "ret" for i in significant):
            out["reason"] = ("shared-empty-thunk" if rva == EMPTY_THUNK else "empty-body-ret-only")
            out["evidence"] = ("body is a bare `ret` (RVA 0x%X, shared by %d managed methods) "
                               "-- no field read, no call: IL2CPP folded this into the empty thunk"
                               % (rva, out["alias_count"]))
            return out

        service = self._try_service_get(site, significant, this_regs)
        if service:
            out.update(service)
            return out

        field_read = self._try_field_read(site, significant)
        if field_read:
            out.update(field_read)
            return out

        out["reason"] = self._other_reason(site, significant)
        out["evidence"] = _summarize(significant)
        return out

    # ---- SERVICE_GET --------------------------------------------------- #
    def _try_service_get(self, site, insns, this_regs):
        """The lazily-cached `??=` accessor, or None when the body is not that shape.

        Every clause below is required; a body that only *resembles* the pattern falls through to
        FIELD_READ / OTHER instead of being guessed into this bucket.
        """
        mi_lines = []
        for _va, _mn, _ops, cmts in insns:
            for c in cmts:
                if c.startswith("METHODINFO "):
                    mi_lines.append(c[len("METHODINFO "):])
        if not mi_lines:
            return None
        calls = [(va, ops, cmts) for va, mn, ops, cmts in insns if mn == "call"]
        accessor = None
        for va, ops, cmts in calls:
            tgt = _call_target(ops)
            if tgt in BOILERPLATE_CALLS or tgt == NRE_THROW:
                continue
            blob = " ".join(cmts)
            if "ServiceLocator$$Get" in blob or "Engine$$GetServiceOrErr" in blob:
                accessor = (va, ops, cmts, blob)
        if accessor is None:
            return None

        # the backing-field load: a plain 8-byte load out of a `this` register at a known offset
        load = None
        for va, mn, ops, cmts in insns:
            if mn != "mov":
                continue
            parts = _split(ops)
            if len(parts) != 2 or not parts[0].startswith("r"):
                continue
            src = _mem_src(parts[1])
            if not src or src[1] not in this_regs or src[2] == 0:
                continue
            if src[2] in site.fields or self._meta_field_candidates(site, src[2], 8):
                load = (va, mn, ops, cmts, src)
                break
        if load is None:
            return None
        off = load[4][2]

        store = None
        for va, mn, ops, cmts in insns:
            if mn != "mov":
                continue
            parts = _split(ops)
            if len(parts) != 2 or not parts[1].startswith("r"):
                continue
            dst = _mem_dst(parts[0])
            if dst and dst[1] == load[4][1] and dst[2] == off:
                store = (va, mn, ops, cmts, dst)

        tests = [i for i in insns if i[1] == "test" and len(set(_split(i[2]))) == 1]
        branches = [i for i in insns if i[1].startswith("j") and i[1] != "jmp"]
        other_calls = [ops for va, ops, cmts in calls
                       if _call_target(ops) not in BOILERPLATE_CALLS
                       and _call_target(ops) != NRE_THROW
                       and va != accessor[0]]
        if not store or not tests or not branches or other_calls or insns[-1][1] != "ret":
            return None

        mi = None
        for line in mi_lines:
            if re.search(r"\.(Get|GetServiceOrErr)<", line):
                mi = line
                break
        if mi is None:
            return None

        loc = "ServiceLocator" if "ServiceLocator$$Get" in accessor[3] else "Engine"
        m = re.search(r"\.(Get|GetServiceOrErr)<(.+)>\(\)", mi)
        type_arg = m.group(2) if m else None

        # the boolean `includeDerivedType` argument ServiceLocator.Get<T>(bool) takes
        flag = None
        for _va, mn, ops, _c in insns:
            if mn != "mov":
                continue
            parts = _split(ops)
            if len(parts) == 2 and parts[0] in ("cl", "ecx") and re.match(r"^[01]$", parts[1]):
                flag = parts[1]

        field_name, field_type, note, fsrc = self._resolve_field(site, off, 8, load[3])
        evidence = ("null-test-and-cache: `%s` (+0x%X) / `%s` / `%s` / store-back `%s`; "
                    "accessor call -> %s; METHODINFO %s"
                    % (load[2], off, "`%s`" % tests[0][2], "`%s`" % branches[0][2],
                       store[2], accessor[3], mi))
        if flag is not None:
            evidence += "; flag `%s`" % flag
        if fsrc:
            evidence += "; field name from %s" % fsrc
        if note:
            evidence += "; field-resolution: " + note

        if site.is_static:
            return None                                    # RCX is not `this` in a static member

        nm = field_name or "_field"
        if loc == "ServiceLocator":
            if flag != "1":
                return {"bucket": "OTHER", "reason": "servicelocator-flag-not-1",
                        "evidence": evidence + "; `mov cl,1` (includeDerivedType) absent"}
            body = "return %s ??= ServiceLocator.Get<%s>(true);" % (nm, type_arg)
            body_q = ("return %s ??= GigaCreation.Tools.Service.ServiceLocator.Get<%s>(true);"
                      % (nm, type_arg))
            variant = "ServiceLocator.Get(includeDerivedType: true)"
        else:
            body = "return %s ??= Engine.GetServiceOrErr<%s>();" % (nm, type_arg)
            body_q = "return %s ??= Naninovel.Engine.GetServiceOrErr<%s>();" % (nm, type_arg)
            variant = "Naninovel.Engine.GetServiceOrErr"

        match = None
        if type_arg and site.declared_type:
            match = _types_agree(type_arg, site.declared_type)
        relation = self.type_arg_relation(type_arg, site.declared_type)
        return {
            "bucket": "SERVICE_GET",
            "reason": None,
            "proposed_body": body,
            "proposed_body_qualified": body_q,
            "evidence": evidence,
            "field_offset": off,
            "field_name": field_name,
            "field_type": field_type,
            "field_source": fsrc,
            "methodinfo": mi,
            "service_variant": variant,
            "type_arg": type_arg,
            "type_arg_matches_declared": match,
            "type_arg_relation": relation,
        }

    # ---- FIELD_READ ---------------------------------------------------- #
    def _try_field_read(self, site, insns):
        """Exactly `<one load into the return register>; ret`, or None.

        A second load (e.g. a 16-byte struct returned in rax:rdx) is *not* accepted: the shape
        would no longer prove which field is the result.
        """
        if site.is_static:
            return None            # in a static member RCX is the first argument, not `this`
        if len(insns) != 2:
            return None
        if insns[1][1] != "ret":
            return None
        va, mn, ops, cmts = insns[0]
        if mn not in ANY_RETURN_LOAD:
            return None
        parts = _split(ops)
        if len(parts) != 2:
            return None
        if parts[0] not in _RET_REGS:
            return None
        src = _mem_src(parts[1])
        if src is None or src[1] != "rcx":
            return None
        width = src[0]
        off = src[2]
        if off == 0 and site.class_kind != "struct":
            # +0x0 of a class instance is the Il2CppClass* header, not a field.  A struct has no
            # header, so offset 0 is a real field there.
            return None
        load = (va, mn, ops, cmts, src)
        field_name, field_type, note, fsrc = self._resolve_field(site, off, width, load[3])
        if field_name is None:
            return {
                "bucket": "FIELD_READ",
                "reason": note or "field-name-unresolved",
                "proposed_body": None,
                "evidence": "single field load & return: `%s` (+0x%X, %d-byte load); %s"
                            % (load[2], off, width or 0,
                               note or "no matching [FieldOffset] in the class"),
                "field_offset": off,
                "field_name": None,
                "field_type": None,
            }
        if site.member_kind == "property-getter" and site.declared_type and field_type:
            if not _types_agree(field_type, site.declared_type):
                note = "property-type-differs-from-field-type"
        if fsrc == "source-FieldOffset":
            evidence = ("single field load & return: `%s` (+0x%X -> %s %s via the class's "
                        "[FieldOffset] table)" % (load[2], off, field_type, field_name))
        else:
            evidence = ("single field load & return: `%s` (+0x%X -> %s, name from the dumper's "
                        "field table -- no [FieldOffset] found in the reconstructed source)"
                        % (load[2], off, field_name))
        if note:
            evidence += "; " + note
        backing = bool(re.match(r"^<.+>k__BackingField$", field_name))
        if backing:
            evidence += ("; this is a compiler-generated auto-property backing field, which the "
                         "source conversion dropped -- the field has to be declared before the "
                         "body compiles")
        return {
            "bucket": "FIELD_READ",
            "reason": note,
            "proposed_body": "return %s;" % field_name,
            "evidence": evidence,
            "field_offset": off,
            "field_name": field_name,
            "field_type": field_type,
            "field_source": fsrc,
            "compiler_generated_backing_field": backing,
        }

    # ---- OTHER reason codes -------------------------------------------- #
    def _other_reason(self, site, insns):
        """Name the reason, from the body itself, so the plan's OTHER groups are actionable.

        These are descriptions of what the disassembly shows -- never a guess at a body.
        """
        mem = [(role, m) for i in insns for role, m in _insn_mems(i)]
        bases = [m[1] for _role, m in mem]
        loads = [m for role, m in mem if role == "r"]
        stores = [m for role, m in mem if role == "w"]
        calls = [i for i in insns if i[1] == "call" and _call_kind(i) != "boiler"]
        jumps = [i for i in insns if i[1] == "jmp"]
        this_loads = [m for m in loads if m[1] == "rcx" and m[2] > 0]
        sret_writes = [m for m in stores if m[1] == "rcx" and m[2] == 0]
        rdx_loads = [m for m in loads if m[1] == "rdx"]

        if any("$$Get<" in c or "$$GetServiceOrErr<" in c
               for j in jumps for c in j[3]):
            return "tailcall-to-service-accessor-no-caching"
        if sret_writes and rdx_loads and not calls:
            return "struct-return-via-hidden-buffer-this-in-rdx"
        if site.is_static and "rcx" in bases and not calls:
            return "static-member-rcx-is-not-this"
        if len(this_loads) == 1 and not calls and not stores and len(insns) <= 10:
            return "single-field-load-plus-computation"
        if "rip" in bases and not calls and not stores:
            return "static-field-read-rip-relative"
        kinds = {_call_kind(i) for i in calls}
        if "managed" in kinds:
            return "calls-managed-code"
        if "unknown" in kinds:
            return "calls-unresolved-helper"
        if calls:
            return "calls-runtime-helper"
        if stores:
            return "writes-memory"
        if len(loads) > 1:
            return "multiple-field-loads"
        if len(insns) <= 6:
            return "short-unrecognised-body"
        return "long-unrecognised-body"


def _call_kind(insn):
    """`boiler` | `managed` | `helper` | `unknown` for a `call` instruction."""
    _va, _mn, ops, cmts = insn
    tgt = _call_target(ops)
    if tgt in BOILERPLATE_CALLS or tgt == NRE_THROW:
        return "boiler"
    for c in cmts:
        if c.startswith("-> sub_"):
            return "helper"
        if c.startswith("-> "):
            return "managed"
    return "unknown"


_TYPE_TOKEN = re.compile(r"[A-Za-z_@][A-Za-z0-9_@]*(?:\.[A-Za-z_@][A-Za-z0-9_@]*)*")
_TYPE_MODS = re.compile(r"^(?:(?:public|private|protected|internal|static|readonly|const|volatile|"
                        r"ref|in|out|params|new|unsafe)\s+)+")


def _norm_type(t):
    """Normalise a C# type spelling for comparison.

    Removes modifiers (`readonly`, `static`), whitespace, namespace qualifiers and nullability
    spelling differences (`T?` vs `Nullable<T>`), all of which appear between the two sources for
    the *same* field.  Generic structure is preserved.
    """
    if not t:
        return None
    t = " ".join(str(t).split())
    t = _TYPE_MODS.sub("", t)
    t = t.replace(" ", "").replace("?", "")
    for _ in range(6):                       # unwrap Nullable<T> wherever it appears
        new = re.sub(r"(?:[A-Za-z_][A-Za-z0-9_\.]*\.)?Nullable<([^<>]*)>", r"\1", t)
        if new == t:
            break
        t = new
    out, i = [], 0
    while i < len(t):
        m = _TYPE_TOKEN.match(t, i)
        if m:
            out.append(m.group(0).rsplit(".", 1)[-1])
            i = m.end()
        else:
            out.append(t[i])
            i += 1
    return "".join(out)


def _type_width(ft):
    ft = _norm_type(ft) or ""
    if ft in ("bool", "byte", "sbyte"):
        return 1
    if ft in ("short", "ushort", "char"):
        return 2
    if ft in ("int", "uint", "float"):
        return 4
    if ft in ("long", "ulong", "double"):
        return 8
    if ft.endswith("[]") or ft.startswith(("IList", "List", "IReadOnlyList", "IDictionary",
                                           "Dictionary", "string", "I", "Nullable")):
        return 8
    return None


def _types_agree(a, b):
    na, nb = _norm_type(a), _norm_type(b)
    return na is not None and na == nb


def _strip_init_guard(insns):
    """Drop the leading one-time type-init guard.

    Shape (README, "两种在正文中反复出现、应当忽略的样板"):

        cmp byte ptr [rip+X], 0 / jne L / <init_metadata calls> / mov byte ptr [rip+X], 1
        L: <real body>

    Started from where the `mov byte ptr [rip+X], 1` guard-set ends.  Only stripped when every
    instruction up to that point is guard material and every call in it is a runtime boilerplate
    call -- otherwise the body is left intact and the site will simply not match a known shape.
    """
    last = None
    for idx, (_va, mn, ops, _c) in enumerate(insns):
        if mn == "mov" and re.match(r"^byte ptr \[rip \+ 0x[0-9a-f]+\], 1$", ops):
            last = idx
    if last is None:
        return insns
    for idx in range(last + 1):
        _va, mn, ops, _c = insns[idx]
        if mn == "call":
            if _call_target(ops) not in BOILERPLATE_CALLS:
                return insns
        elif mn not in ("mov", "cmp", "test", "jne", "je", "jmp", "lea", "lock", "nop", "xor"):
            return insns
    return insns[last + 1:] or insns


def _format_head(insns, n):
    out = []
    for va, mn, ops, cmts in insns[:n]:
        line = "0x%X %s %s" % (va, mn, ops)
        if cmts:
            line += " ; " + " | ".join(cmts)
        out.append(" ".join(line.split()))
    return out


def _summarize(insns, n=4):
    bits = []
    for va, mn, ops, cmts in insns[:n]:
        s = ("%s %s" % (mn, ops)).strip()
        keep = [c for c in cmts if not c.startswith("[boilerplate]")]
        if keep:
            s += "   ; " + " | ".join(keep)
        bits.append(s)
    tail = "" if len(insns) <= n else "  ... (+%d more)" % (len(insns) - n)
    return " | ".join(bits) + tail


# --------------------------------------------------------------------------- #
# plan assembly
# --------------------------------------------------------------------------- #

def build_plan(sites_getters, sites_methods, filters, type_index=None):
    dis = Disassembler()
    clf = Classifier(dis, type_index)
    results = {"getter": [], "method": []}
    for kind, sites in (("getter", sites_getters), ("method", sites_methods)):
        for site in sites:
            if not _passes(site, filters):
                continue
            if filters["limit"] is not None and \
                    len(results["getter"]) + len(results["method"]) >= filters["limit"]:
                break
            verdict = clf.classify(site)
            rec = collections.OrderedDict()
            rec["file"] = site.file_rel
            rec["line"] = site.line
            rec["member_kind"] = site.member_kind
            rec["class"] = site.class_full
            rec["member"] = site.member
            rec["declared_type"] = site.declared_type
            rec["is_static"] = site.is_static
            rec["current_body"] = site.current_body
            rec["rva"] = ("0x%X" % site.rva) if site.rva is not None else None
            rec["rva_source"] = site.rva_source
            rec["token"] = site.token
            rec["pdata_size"] = verdict.pop("pdata_size", None)
            rec["body_bytes"] = verdict.pop("body_bytes", None)
            rec["alias_count"] = verdict.pop("alias_count", None)
            rec["bucket"] = verdict.pop("bucket")
            rec["reason"] = verdict.pop("reason", None)
            rec["proposed_body"] = verdict.pop("proposed_body", None)
            for k in ("proposed_body_qualified", "field_offset", "field_name", "field_type",
                      "field_source", "compiler_generated_backing_field", "methodinfo",
                      "service_variant", "type_arg", "type_arg_matches_declared",
                      "type_arg_relation", "evidence", "disas_head"):
                if k in verdict:
                    rec[k] = verdict.pop(k)
            results[kind].append(rec)
    return results


def _passes(site, filters):
    """`--file` matches an exact repo-relative path, an absolute path, or a path suffix."""
    if filters["files"]:
        rel = site.file_rel
        abs_path = os.path.abspath(site.file_abs).replace(os.sep, "/") if site.file_abs else None
        ok = False
        for f in filters["files"]:
            f = os.path.abspath(f).replace(os.sep, "/") if os.path.isabs(f) else f.replace("\\", "/")
            if rel == f or rel.endswith(f) or (abs_path is not None and abs_path == f):
                ok = True
                break
        if not ok:
            return False
    if filters["only"] and site.kind not in filters["only"]:
        return False
    return True


def summarize(results):
    summary = collections.OrderedDict()
    for kind in ("getter", "method"):
        rows = results[kind]
        buckets = collections.Counter(r["bucket"] for r in rows)
        summary["%s_count" % kind] = len(rows)
        summary["%s_buckets" % kind] = collections.OrderedDict(sorted(
            buckets.items(), key=lambda kv: (-kv[1], kv[0])))
    total = collections.Counter()
    for kind in ("getter", "method"):
        total.update(r["bucket"] for r in results[kind])
    summary["total"] = sum(total.values())
    summary["buckets"] = collections.OrderedDict(sorted(total.items(), key=lambda kv: (-kv[1], kv[0])))

    reasons = collections.Counter(r["reason"] for r in results["getter"] + results["method"]
                                  if r["bucket"] == "OTHER")
    summary["other_reasons"] = collections.OrderedDict(sorted(
        reasons.items(), key=lambda kv: (-kv[1], str(kv[0]))))

    per_file = collections.Counter()
    per_file_bucket = collections.defaultdict(collections.Counter)
    for kind in ("getter", "method"):
        for r in results[kind]:
            per_file[r["file"]] += 1
            per_file_bucket[r["file"]][r["bucket"]] += 1
    summary["per_file"] = collections.OrderedDict(sorted(
        per_file.items(), key=lambda kv: (-kv[1], kv[0])))
    summary["per_file_buckets"] = collections.OrderedDict(
        (f, collections.OrderedDict(sorted(c.items(), key=lambda kv: (-kv[1], kv[0]))))
        for f, c in sorted(per_file_bucket.items(), key=lambda kv: (-per_file[kv[0]], kv[0])))

    variants = collections.Counter()
    for kind in ("getter", "method"):
        for r in results[kind]:
            if r["service_variant"]:
                variants[r["service_variant"]] += 1
    summary["service_variants"] = collections.OrderedDict(sorted(
        variants.items(), key=lambda kv: (-kv[1], kv[0])))

    fsrc_named = collections.Counter(
        (r.get("field_source") or "unresolved")
        for kind in ("getter", "method") for r in results[kind] if r["bucket"] != "OTHER")
    summary["field_name_source"] = collections.OrderedDict(
        sorted(fsrc_named.items(), key=lambda kv: (-kv[1], str(kv[0]))))
    backing = sum(1 for kind in ("getter", "method") for r in results[kind]
                  if r.get("compiler_generated_backing_field"))
    summary["compiler_generated_backing_fields"] = backing
    summary["property_type_differs_from_field_type"] = sum(
        1 for kind in ("getter", "method") for r in results[kind]
        if r.get("reason") == "property-type-differs-from-field-type")

    mismatch = [r for r in results["getter"] + results["method"]
                if r.get("type_arg_matches_declared") is False]
    summary["type_arg_mismatch_count"] = len(mismatch)
    summary["type_arg_mismatches"] = [
        collections.OrderedDict((k, r[k]) for k in
                                ("file", "line", "class", "member", "declared_type", "type_arg",
                                 "type_arg_relation", "methodinfo"))
        for r in mismatch]
    unresolved = [r for r in results["getter"] + results["method"]
                  if r["bucket"] == "FIELD_READ" and not r["proposed_body"]]
    summary["field_read_without_body"] = len(unresolved)
    return summary


def write_artifacts(results, summary, meta, out_dir):
    os.makedirs(out_dir, exist_ok=True)
    plan = collections.OrderedDict()
    plan["schema"] = "tools/unity-rebuild/classify_stub_getters.py v1"
    plan.update(meta)
    plan["summary"] = summary
    plan["getters"] = results["getter"]
    plan["methods"] = results["method"]
    json_path = os.path.join(out_dir, "stub_getter_plan.json")
    with open(json_path, "w", encoding="utf-8", newline="\n") as fh:
        json.dump(plan, fh, ensure_ascii=False, indent=1)
        fh.write("\n")
    md_path = os.path.join(out_dir, "stub_getter_plan.md")
    with open(md_path, "w", encoding="utf-8", newline="\n") as fh:
        fh.write(render_md(plan))
    return json_path, md_path


def render_md(plan):
    s = plan["summary"]
    L = []
    L.append("# Stub getter classification plan")
    L.append("")
    L.append("Generated by `tools/unity-rebuild/classify_stub_getters.py` (read-only with respect to "
             "`UnityProject/`).")
    L.append("")
    L.append("- binary: `%s`" % plan["binary"]["path"])
    L.append("  (sha256 `%s`, PE timestamp `%s`, imagebase `0x%X`)"
             % (plan["binary"]["sha256"], plan["binary"]["timestamp"], plan["binary"]["imagebase"]))
    L.append("- sources scanned: `%s`" % plan["source_root"])
    L.append("- sites in this plan: %d getters + %d trivial-return methods"
             % (s["getter_count"], s["method_count"]))
    if plan.get("filters"):
        L.append("- filters: `%s`" % json.dumps(plan["filters"], ensure_ascii=False))
    L.append("")
    L.append("## Totals per bucket")
    L.append("")
    L.append("| bucket | all | getters | methods |")
    L.append("|---|---|---|---|")
    for b in ("FIELD_READ", "SERVICE_GET", "OTHER"):
        L.append("| `%s` | %d | %d | %d |"
                 % (b, s["buckets"].get(b, 0), s["getter_buckets"].get(b, 0),
                    s["method_buckets"].get(b, 0)))
    L.append("| **total** | **%d** | **%d** | **%d** |"
             % (s["total"], s["getter_count"], s["method_count"]))
    L.append("")
    if s["service_variants"]:
        L.append("`SERVICE_GET` variants: " + ", ".join(
            "`%s` x%d" % (k, v) for k, v in s["service_variants"].items()))
        L.append("")
    L.append("Where the field *name* in a proposal came from: " + ", ".join(
        "`%s` x%d" % (k, v) for k, v in s["field_name_source"].items()) + ".")
    L.append("")
    L.append("Of the `FIELD_READ` proposals, **%d** name a compiler-generated auto-property backing "
             "field (`<Name>k__BackingField`) that the source conversion dropped -- the field has to "
             "be declared before such a body compiles. **%d** sites resolve to a field whose declared "
             "type differs from the member's declared type (e.g. an `IReadOnlyList<T>` property over "
             "a `List<T>` field): the byte offset is certain, the types simply differ."
             % (s["compiler_generated_backing_fields"], s["property_type_differs_from_field_type"]))
    L.append("")
    L.append("`SERVICE_GET` sites where the `METHODINFO` type argument disagrees with the declared "
             "member type: **%d**. `relation` is resolved by walking the interface/class base lists "
             "declared in the reconstructed sources; `type-arg-derives-from-declared` means the "
             "accessor asks for a *derived* interface and the recovered body must name the "
             "METHODINFO's argument (`Get<IAddressablesManager>()` stored into an "
             "`IAddressableAssetLoader` field), not the declared type."
             % s["type_arg_mismatch_count"])
    if s["type_arg_mismatches"]:
        L.append("")
        L.append("| file | line | class | member | declared | METHODINFO arg | relation |")
        L.append("|---|---|---|---|---|---|---|")
        for r in s["type_arg_mismatches"][:40]:
            L.append("| `%s` | %d | `%s` | `%s` | `%s` | `%s` | `%s` |"
                     % (r["file"], r["line"], r["class"], r["member"], r["declared_type"],
                        r["type_arg"], r.get("type_arg_relation")))
        if len(s["type_arg_mismatches"]) > 40:
            L.append("")
            L.append("_(%d more in the JSON)_" % (len(s["type_arg_mismatches"]) - 40))
    if s["field_read_without_body"]:
        L.append("")
        L.append("`FIELD_READ` sites whose field name could not be resolved to a single "
                 "`[FieldOffset]` declaration (**%d**): the load shape is certain, the name is not, "
                 "so no body is proposed." % s["field_read_without_body"])
    L.append("")
    L.append("## Top 20 files by candidate count")
    L.append("")
    L.append("| # | file | candidates | FIELD_READ | SERVICE_GET | OTHER |")
    L.append("|---|---|---|---|---|---|")
    for i, (f, n) in enumerate(list(s["per_file"].items())[:20], 1):
        b = s["per_file_buckets"][f]
        L.append("| %d | `%s` | %d | %d | %d | %d |"
                 % (i, f, n, b.get("FIELD_READ", 0), b.get("SERVICE_GET", 0), b.get("OTHER", 0)))
    L.append("")
    L.append("## `OTHER` cases, grouped by reason")
    L.append("")
    L.append("Every unclassified site is in `stub_getter_plan.json` with its RVA, `.pdata` size and "
             "the first 30 annotated instructions. Below: one row per reason, with up to 8 examples.")
    L.append("")
    by_reason = collections.defaultdict(list)
    for kind in ("getter", "method"):
        for r in (plan["getters"] if kind == "getter" else plan["methods"]):
            if r["bucket"] == "OTHER":
                by_reason[r["reason"]].append(r)
    for reason, n in s["other_reasons"].items():
        L.append("### `%s` -- %d site(s)" % (reason, n))
        L.append("")
        for r in by_reason[reason][:8]:
            L.append("- `%s:%d` `%s.%s` (%s, RVA %s, pdata %s)"
                     % (r["file"], r["line"], r["class"], r["member"],
                        r["current_body"], r["rva"], r["pdata_size"]))
        if n > 8:
            L.append("- _(%d more in the JSON)_" % (n - 8))
        L.append("")
    return "\n".join(L) + "\n"


# --------------------------------------------------------------------------- #
# --check
# --------------------------------------------------------------------------- #

def run_check(plan_path, results, meta):
    with open(plan_path, encoding="utf-8") as fh:
        old = json.load(fh)
    problems = []
    old_map = {}
    for kind in ("getters", "methods"):
        for r in old.get(kind, []):
            old_map[(r["file"], r["line"])] = r
    new_map = {}
    for kind in ("getter", "method"):
        for r in results[kind]:
            new_map[(r["file"], r["line"])] = r

    for key, o in sorted(old_map.items()):
        n = new_map.get(key)
        if n is None:
            problems.append("%s:%d %s.%s -- site disappeared" % (key[0], key[1], o["class"], o["member"]))
            continue
        for field in ("bucket", "reason", "proposed_body", "rva", "pdata_size", "field_offset",
                      "field_name", "field_source", "service_variant", "type_arg",
                      "type_arg_relation", "methodinfo"):
            if o.get(field) != n.get(field):
                problems.append("%s:%d %s.%s -- %s changed: %r -> %r"
                                % (key[0], key[1], o["class"], o["member"], field,
                                   o.get(field), n.get(field)))
    added = [k for k in new_map if k not in old_map]
    print("checked %d previously classified sites against %d freshly classified"
          % (len(old_map), len(new_map)))
    if added:
        print("note: %d site(s) present now but not in the plan (new stubs since it was written):"
              % len(added))
        for k in sorted(added)[:20]:
            print("   + %s:%d" % k)
        if len(added) > 20:
            print("   ... +%d more" % (len(added) - 20))
    if problems:
        print("FAIL: %d mismatch(es)" % len(problems))
        for p in problems[:60]:
            print("   ! " + p)
        if len(problems) > 60:
            print("   ... +%d more" % (len(problems) - 60))
        return 1
    print("OK: every previously classified site still classifies identically")
    return 0


# --------------------------------------------------------------------------- #
# main
# --------------------------------------------------------------------------- #

def sha256_of(path):
    h = hashlib.sha256()
    with open(path, "rb") as fh:
        for chunk in iter(lambda: fh.read(1 << 22), b""):
            h.update(chunk)
    return h.hexdigest()


def main(argv=None):
    ap = argparse.ArgumentParser(description=__doc__.split("\n")[0],
                                 formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("--limit", type=int, default=None,
                    help="classify at most N sites (stable order: getters then methods, by file/line)")
    ap.add_argument("--file", dest="files", action="append", default=[],
                    help="restrict to one source file (repo-relative, absolute, or path suffix); repeatable")
    ap.add_argument("--only", choices=["getter", "method"], action="append", default=[],
                    help="restrict to getters or to trivial-return methods; repeatable")
    ap.add_argument("--source-root", default=SOURCE_ROOT,
                    help="root of the reconstructed sources (default: %s)" % SOURCE_ROOT)
    ap.add_argument("--out-dir", default=OUT_DIR,
                    help="artifact directory (default: %s; a filtered run defaults to "
                         "<it>/subset so it cannot clobber the full plan)" % OUT_DIR)
    ap.add_argument("--plan", default=PLAN_JSON, help="plan JSON to compare against in --check")
    ap.add_argument("--check", action="store_true",
                    help="re-run the classification and fail if a previously classified site no longer matches")
    args = ap.parse_args(argv)

    if args.limit is not None and args.limit < 0:
        ap.error("--limit must be >= 0")

    getters, methods, type_index = collect_sites(args.source_root)
    filters = {"files": args.files, "limit": args.limit, "only": args.only}
    if args.only == ["getter"]:
        methods = []
    if args.only == ["method"]:
        getters = []
    results = build_plan(getters, methods, filters, type_index)

    if args.check:
        return run_check(args.plan, results, None)

    filtered = args.limit is not None or bool(args.files) or bool(args.only)
    if filtered and args.out_dir == OUT_DIR:
        # a subset run must not clobber the canonical full plan
        args.out_dir = os.path.join(OUT_DIR, "subset")
        print("note: filtered run -> writing to %s (pass --out-dir to choose)"
              % os.path.relpath(args.out_dir, REPO))

    summary = summarize(results)
    pe = il2cpp_pe.PE()
    meta = collections.OrderedDict()
    meta["binary"] = collections.OrderedDict([
        ("path", il2cpp_pe.GAME_DLL),
        ("sha256", sha256_of(il2cpp_pe.GAME_DLL)),
        ("timestamp", "0x%X" % _pe_timestamp(pe)),
        ("imagebase", pe.imagebase),
        ("pdata_functions", len(pe.pdata_functions())),
    ])
    meta["source_root"] = os.path.relpath(args.source_root, REPO).replace(os.sep, "/")
    meta["filters"] = collections.OrderedDict([
        ("limit", args.limit), ("file", args.files), ("only", args.only),
    ])
    meta["classification_rules"] = collections.OrderedDict([
        ("FIELD_READ", "body is `ret` preceded by exactly one load into a return register "
                       "(rax/eax/ax/al/xmm0) from `[rcx+off]`, off != 0, member is not static, "
                       "no call and no store; off is resolved to the field name through the "
                       "`[Il2CppDummyDll.FieldOffset(Offset = \"0x..\")]` attribute in the class."),
        ("SERVICE_GET", "the backing-field load is followed by a null test and a null branch, the "
                        "result of a `ServiceLocator.Get<T>` / `Naninovel.Engine.GetServiceOrErr<T>` "
                        "call is stored back into the same field, there is exactly one such call "
                        "and no other managed call, and the body ends in `ret`; the type argument "
                        "comes from the `METHODINFO Method$...<T>()` annotation."),
        ("OTHER", "anything else -- no body is proposed; RVA, .pdata size and the first 30 "
                  "annotated instructions are recorded for a human."),
    ])
    json_path, md_path = write_artifacts(results, summary, meta, args.out_dir)

    print("getters: %d   trivial-return methods: %d" % (summary["getter_count"], summary["method_count"]))
    for b, n in summary["buckets"].items():
        print("  %-12s %5d   (getters %d / methods %d)"
              % (b, n, summary["getter_buckets"].get(b, 0), summary["method_buckets"].get(b, 0)))
    print("wrote %s" % os.path.relpath(json_path, REPO))
    print("wrote %s" % os.path.relpath(md_path, REPO))
    return 0


def _pe_timestamp(pe):
    import struct
    e = struct.unpack_from("<I", pe.data, 0x3C)[0]
    return struct.unpack_from("<I", pe.data, e + 8)[0]


if __name__ == "__main__":
    sys.exit(main())
