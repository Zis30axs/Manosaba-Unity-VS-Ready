#!/usr/bin/env python3
"""Script-type identity and inheritance, read from the IL2CPP dump (audit E63-003).

Identity used here
------------------
* source object  : ``(source, SerializedFile, pathID)``      -- see :mod:`resolution`
* script type    : ``(assembly, namespace, type)``           -- from the MonoScript table

Why inheritance is needed
-------------------------
A serialised ``MonoBehaviour`` object is not necessarily a Component.  Unity writes
ScriptableObject assets with the same class id, and their ``m_GameObject`` is a null
pointer by design.  Deciding "orphan component" from a null host alone mislabels every
ScriptableObject sub-asset; deciding it from a non-null host alone assumes the back
reference is correct.  Only the script's own base chain separates the two.

What this module refuses to do
------------------------------
* pick one declaration when a name resolves to several (``ambiguous``);
* invent a base when the dump has no declaration (``no_declaration``);
* follow a chain that loops or runs away (``cycle`` / ``depth_limit``).

Each of those is reported as its own reason, never collapsed into a single UNKNOWN blob,
and never resolved by first match.

dump.cs is a 25 MB analysis input and is git-ignored.  ``write_evidence`` therefore records
only the declarations actually consulted, so a reviewer can re-check a decision without the
dump, and :func:`regeneration_command` states how to rebuild it.
"""
from __future__ import annotations

import collections
import json
import os
import re
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
from common import OUT, SOURCE_ROOT  # noqa: E402

DUMP_CS = os.path.join(SOURCE_ROOT, "il2cpp", "dump.cs")

#: the two roots that decide how a serialised MonoBehaviour must be read
COMPONENT_ROOT = "MonoBehaviour"
ASSET_ROOT = "ScriptableObject"

_NS = re.compile(r"^// Namespace: (.*)$")
#: every access level, not just ``public`` -- ActivationPlayableAsset is ``internal`` and a
#: public-only pattern silently turned 15 real ScriptableObjects into UNKNOWN.
_CLASS = re.compile(
    r"^(?:public|internal|private|protected)(?: protected| internal)?"
    r"(?: sealed| abstract| static| partial)* class "
    r"([A-Za-z0-9_.`]+)(?:<[^>]*>)?(?: : (.+?))? // TypeDefIndex: (\d+)")


class Declaration:
    __slots__ = ("namespace", "name", "base", "line", "type_def_index")

    def __init__(self, namespace, name, base, line, type_def_index):
        self.namespace = namespace
        self.name = name
        self.base = base
        self.line = line
        self.type_def_index = type_def_index

    @property
    def full_name(self):
        return f"{self.namespace}.{self.name}" if self.namespace else self.name

    def as_evidence(self):
        return {"type": self.full_name, "base": self.base,
                "dump_line": self.line, "type_def_index": self.type_def_index}


class Classification:
    """Outcome for one script type."""

    __slots__ = ("kind", "reason", "chain", "requested")

    def __init__(self, kind, requested, chain=None, reason=None):
        self.kind = kind              # COMPONENT_ROOT | ASSET_ROOT | "UNKNOWN"
        self.requested = requested    # (assembly, namespace, type) as asked
        self.chain = chain or []      # [Declaration, ...] from the type up to the root
        self.reason = reason          # set only when kind == "UNKNOWN"

    @property
    def is_component(self):
        return self.kind == COMPONENT_ROOT

    @property
    def is_asset(self):
        return self.kind == ASSET_ROOT

    def as_evidence(self):
        out = {"assembly": self.requested[0], "namespace": self.requested[1],
               "type": self.requested[2], "kind": self.kind,
               "chain": [d.as_evidence() for d in self.chain]}
        if self.reason:
            out["reason"] = self.reason
        return out


class TypeIndex:
    """Declarations parsed out of dump.cs, keyed by ``(namespace, type)``."""

    def __init__(self, declarations, duplicates):
        self.declarations = declarations                 # (ns, name) -> Declaration
        self.duplicates = duplicates                     # (ns, name) -> [Declaration, ...]
        self.by_name = collections.defaultdict(list)      # name -> [(ns, Declaration)]
        for (ns, name), decl in declarations.items():
            self.by_name[name].append((ns, decl))
        self._cache = {}
        self.consulted = {}                               # full_name -> Classification

    # -- lookup ------------------------------------------------------------ #

    def declaration(self, namespace, name):
        return self.declarations.get((namespace or "", name))

    def _resolve_base(self, decl):
        """Find the declaration of ``decl.base``.

        Prefers the base's own namespace, then the declaring type's namespace, and only
        then a unique match by simple name.  Several candidates -> ambiguous, never first.
        """
        base = decl.base
        if not base:
            return None, "no_base"
        if "." in base:
            ns, _, simple = base.rpartition(".")
            found = self.declaration(ns, simple)
            return (found, None) if found else (None, "no_declaration")
        same_ns = self.declaration(decl.namespace, base)
        if same_ns is not None:
            return same_ns, None
        candidates = self.by_name.get(base) or []
        if not candidates:
            return None, "no_declaration"
        if len(candidates) > 1:
            return None, "ambiguous"
        return candidates[0][1], None

    def classify(self, assembly, namespace, name, limit=64):
        """Walk the base chain until it reaches a known root, or explain why it cannot."""
        key = (assembly or "", namespace or "", name)
        if key in self._cache:
            return self._cache[key]

        chain = []
        decl = self.declaration(namespace, name)
        if decl is None:
            candidates = self.by_name.get(name) or []
            if len(candidates) == 1:
                decl = candidates[0][1]
            elif len(candidates) > 1:
                result = Classification("UNKNOWN", key, reason="ambiguous_declaration")
                self._cache[key] = result
                self.consulted[result.as_evidence()["type"]] = result
                return result
        if decl is None:
            result = Classification("UNKNOWN", key, reason="no_declaration")
            self._cache[key] = result
            self.consulted[f"{namespace}.{name}" if namespace else name] = result
            return result

        seen = set()
        reason = None
        while decl is not None and limit:
            chain.append(decl)
            if decl.name in (COMPONENT_ROOT, ASSET_ROOT):
                result = Classification(decl.name, key, chain)
                break
            ident = (decl.namespace, decl.name)
            if ident in seen:
                reason = "cycle"
                result = Classification("UNKNOWN", key, chain, reason)
                break
            seen.add(ident)
            limit -= 1
            nxt, why = self._resolve_base(decl)
            if nxt is None:
                reason = why or "no_declaration"
                result = Classification("UNKNOWN", key, chain, reason)
                break
            decl = nxt
        else:
            result = Classification("UNKNOWN", key, chain, "depth_limit")

        self._cache[key] = result
        self.consulted[chain[0].full_name if chain else name] = result
        return result

    # -- evidence ---------------------------------------------------------- #

    def write_evidence(self, path=None, extra=None):
        """Persist only the declarations actually consulted (dump.cs is not committed)."""
        path = path or os.path.join(OUT, "type_chains.json")
        payload = {
            "generated_from": os.path.relpath(DUMP_CS, os.path.dirname(OUT)).replace("\\", "/"),
            "regeneration": regeneration_command(),
            "declarations_parsed": len(self.declarations),
            "names_declared_more_than_once": len(self.duplicates),
            "types": {name: c.as_evidence() for name, c in sorted(self.consulted.items())},
        }
        if extra:
            payload.update(extra)
        os.makedirs(os.path.dirname(path), exist_ok=True)
        with open(path, "w", encoding="utf-8") as fh:
            json.dump(payload, fh, ensure_ascii=False, indent=1)
        return path


def regeneration_command():
    return ("Il2CppDumper on manosaba_Data/il2cpp_data + GameAssembly.dll -> src/il2cpp/dump.cs "
            "(25 MB, git-ignored analysis input)")


def load_type_index(dump_path=None):
    """Parse dump.cs once.  Duplicate ``(namespace, type)`` declarations are kept apart."""
    dump_path = dump_path or DUMP_CS
    if not os.path.exists(dump_path):
        raise SystemExit(f"IL2CPP dump missing: {dump_path}\n{regeneration_command()}")
    declarations = {}
    duplicates = collections.defaultdict(list)
    namespace = ""
    with open(dump_path, encoding="utf-8", errors="replace") as fh:
        for lineno, line in enumerate(fh, 1):
            m = _NS.match(line)
            if m:
                namespace = m.group(1).strip()
                continue
            m = _CLASS.match(line)
            if not m:
                continue
            name, bases, tdi = m.group(1), m.group(2) or "", m.group(3)
            base = bases.split(",")[0].strip() or None
            decl = Declaration(namespace, name, base, lineno, int(tdi))
            key = (namespace, name)
            if key in declarations and declarations[key].base != decl.base:
                duplicates[key].append(decl)
            else:
                declarations.setdefault(key, decl)
    return TypeIndex(declarations, dict(duplicates))


def load_monoscript_types(path=None):
    """pathID (int) -> (assembly, namespace, type) from the a05 MonoScript table."""
    path = path or os.path.join(OUT, "monoscript_table.json")
    if not os.path.exists(path):
        raise SystemExit(f"MonoScript table missing: {path}\nrun a05_monobehaviour.py first")
    with open(path, encoding="utf-8") as fh:
        table = json.load(fh)
    out = {}
    for pid, rec in (table.get("by_pid") or {}).items():
        out[int(pid)] = (rec.get("assembly") or "", rec.get("namespace") or "",
                         rec.get("class") or "")
    return out
