#!/usr/bin/env python3
"""Strict ``(source, SerializedFile, pathID)`` resolution shared by a04 and a08.

Why this module exists (audit E63-001 / E63-002)
------------------------------------------------
Two different defects had the same root cause: something other than a SerializedFile was
used as the address space of a pointer.

* a04 registered every index row under *every* SerializedFile of its source and then
  accepted the first file that happened to declare the pathID.
* a08 grouped objects by ``asset_path`` -- the Addressables *container* path -- and
  resolved ``m_FileID == 0`` pointers inside that group, with ``by_pid`` returning
  ``keys[0]``.

``asset_path`` is an index label describing which asset a dumped object belongs to.  It is
not an address space, and one SerializedFile's objects can be filed under several container
paths.  The measured consequence in the shipped report: ``general-prefabs_assets_all.bundle``
declares MonoBehaviour ``-2807983474179293437`` and GameObject ``6314399593608855299`` in the
same SerializedFile (``CAB-20b10ea4f83373087a80a70a33e02ead``), yet a08 reported the component
as ``missing_component`` under ``DebugLabel.prefab`` *and* as ``orphan_component`` under
``Game.prefab``, because the two objects landed in different container groups.

The owner of an object is therefore taken from the a03 scan, which records
``obj.assets_file.name`` per object, and never inferred from a container path.

Ambiguity policy
----------------
``(source, pathID)`` is not guaranteed unique: a source may hold several SerializedFiles.
When it does, this module reports ``ambiguous`` and refuses to pick one.  In the currently
scanned data every one of the 465 sources holds exactly one SerializedFile, so the ambiguous
path is exercised by fixtures rather than by the shipped bundles -- it is implemented because
the format allows it, not because it has been observed here.
"""
from __future__ import annotations

import collections
import json
import os
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import a03_sources  # noqa: E402  (safe_name: the per-source record naming scheme)
from common import OUT  # noqa: E402

A03_DIR = os.path.join(OUT, "a03")

#: outcome kinds returned by :meth:`OwnerTable.resolve`
NULL = "null"                                   # m_PathID == 0: a legitimate null pointer
RESOLVED = "resolved"                           # bound inside the pointer's own SerializedFile
UNRESOLVED_LOCAL = "unresolved_local"           # fileID 0, pathID not declared by that file
AMBIGUOUS_LOCAL = "ambiguous_local"             # several SerializedFiles declare the pathID
EXTERNAL_RESOLVED = "external_resolved"         # fileID N -> object found in the target file
EXTERNAL_NOT_DUMPED = "external_not_dumped"     # target file known, object absent from a03
EXTERNAL_OUT_OF_RANGE = "external_out_of_range"  # fileID beyond this file's externals list
EXTERNAL_UNKNOWN_TARGET = "external_unknown_target"  # externals entry names no scanned source
EXTERNAL_BUILTIN = "external_builtin"           # Unity builtin resources, not an editable asset
OWNER_UNKNOWN = "owner_unknown"                 # the pointer's own file is not in the scan
MALFORMED = "malformed"                         # not a PPtr shape


class Resolution:
    """Outcome of one pointer resolution.  ``key`` is set only for the bound kinds."""

    __slots__ = ("kind", "key", "detail")

    def __init__(self, kind, key=None, **detail):
        self.kind = kind
        self.key = key
        self.detail = detail

    @property
    def bound(self):
        return self.kind in (RESOLVED, EXTERNAL_RESOLVED)

    def __repr__(self):  # pragma: no cover - debugging aid
        return f"Resolution({self.kind}, key={self.key!r}, {self.detail!r})"


class AmbiguousOwner(LookupError):
    """``(source, pathID)`` is declared by more than one SerializedFile."""


class OwnerTable:
    """Authoritative object ownership, built from the a03 per-source scan records."""

    def __init__(self):
        self.owner = {}                                   # (src, sf, pid) -> record
        self.pid_index = collections.defaultdict(list)    # (src, pid) -> [sf, ...]
        self.externals = {}                               # (src, sf) -> [external, ...]
        self.serialized_files = collections.defaultdict(list)   # src -> [sf, ...]
        self.cab_owner = collections.defaultdict(list)    # cab.lower() -> [(src, sf), ...]
        self.stats = collections.Counter()
        self.defects = []

    # -- construction ------------------------------------------------------ #

    def add_source(self, src, sf_records, objects):
        """Register one scanned source.

        ``sf_records``: the ``serialized_files`` list from the a03 manifest entry.
        ``objects``:    the per-source ``objects`` list (``file``/``path_id``/``type``/...).
        """
        for sf in sf_records:
            name = sf.get("name")
            if not name:
                self.defects.append({"kind": "serialized_file_without_name", "source": src})
                continue
            self.serialized_files[src].append(name)
            self.externals[(src, name)] = sf.get("externals") or []
            entry = (src, name)
            if entry not in self.cab_owner[name.lower()]:
                self.cab_owner[name.lower()].append(entry)
        for obj in objects or []:
            sf_name = obj.get("file")
            raw_pid = obj.get("path_id")
            if sf_name is None or raw_pid is None:
                self.defects.append({"kind": "object_without_owner", "source": src,
                                     "object": {k: obj.get(k) for k in ("file", "path_id", "type")}})
                self.stats["object_without_owner"] += 1
                continue
            pid = int(raw_pid)
            key = (src, sf_name, pid)
            if key in self.owner:
                self.stats["duplicate_owner_entry"] += 1
                self.defects.append({"kind": "duplicate_owner_entry", "source": src,
                                     "serialized_file": sf_name, "path_id": str(pid)})
                continue
            self.owner[key] = {"type": obj.get("type"), "class_id": obj.get("class_id"),
                               "byte_size": obj.get("byte_size")}
            self.pid_index[(src, pid)].append(sf_name)
            self.stats["objects"] += 1

    # -- queries ----------------------------------------------------------- #

    def owning_file(self, src, pid):
        """Return the SerializedFile that declares ``pid`` in ``src``.

        ``None`` when nothing declares it.  Raises :class:`AmbiguousOwner` when several
        files do -- the caller must report that, never pick one.
        """
        sfs = self.pid_index.get((src, int(pid)))
        if not sfs:
            return None
        if len(sfs) > 1:
            raise AmbiguousOwner(f"{src} pathID {pid} declared by {len(sfs)} files: {sfs}")
        return sfs[0]

    def objects_in_file(self, src, sf):
        """Every ``(src, sf, pid)`` key declared by one SerializedFile."""
        return [k for k in self.owner if k[0] == src and k[1] == sf]

    def resolve(self, src, sf, ptr):
        """Resolve one PPtr that lives inside ``(src, sf)``.

        ``m_FileID == 0`` is resolved **only** against the objects that this very
        SerializedFile declares; a different file in the same source is a different address
        space and is never substituted.  ``m_FileID != 0`` indexes this file's own externals
        list, never a concatenation of every externals list in the source.
        """
        if not isinstance(ptr, dict):
            return Resolution(MALFORMED, value=repr(ptr)[:120])
        fid, pid = ptr.get("m_FileID"), ptr.get("m_PathID")
        if not isinstance(fid, int) or not isinstance(pid, int):
            return Resolution(MALFORMED, value=json.dumps(ptr)[:120])
        if fid == 0 and pid == 0:
            return Resolution(NULL)
        if (src, sf) not in self.externals and src not in self.serialized_files:
            return Resolution(OWNER_UNKNOWN, source=src, serialized_file=sf,
                              m_FileID=fid, m_PathID=str(pid))

        if fid == 0:
            key = (src, sf, pid)
            if key in self.owner:
                return Resolution(RESOLVED, key, type=self.owner[key]["type"])
            others = [name for name in self.pid_index.get((src, pid), []) if name != sf]
            if others:
                # Declared by a *different* file of the same source: report it, do not bind.
                return Resolution(AMBIGUOUS_LOCAL, m_PathID=str(pid),
                                  declared_by=others, asked_in=sf)
            return Resolution(UNRESOLVED_LOCAL, m_PathID=str(pid), asked_in=sf)

        externals = self.externals.get((src, sf), [])
        if not (1 <= fid <= len(externals)):
            return Resolution(EXTERNAL_OUT_OF_RANGE, m_FileID=fid, m_PathID=str(pid),
                              externals_available=len(externals), asked_in=sf)
        ext = externals[fid - 1] or {}
        ext_path = str(ext.get("path") or "")
        target = self._external_target(ext_path)
        if target is None:
            return Resolution(EXTERNAL_UNKNOWN_TARGET, m_FileID=fid, m_PathID=str(pid),
                              external_path=ext_path)
        if target == "builtin":
            return Resolution(EXTERNAL_BUILTIN, m_FileID=fid, m_PathID=str(pid),
                              external_path=ext_path)
        tsrc, tsf = target
        key = (tsrc, tsf, pid)
        if key in self.owner:
            return Resolution(EXTERNAL_RESOLVED, key, external_path=ext_path,
                              type=self.owner[key]["type"])
        return Resolution(EXTERNAL_NOT_DUMPED, m_FileID=fid, m_PathID=str(pid),
                          external_path=ext_path, target_source=tsrc, target_file=tsf)

    def _external_target(self, ext_path):
        """``archive:/CAB-x/CAB-x`` or a library path -> the owning (source, file)."""
        if not ext_path:
            return None
        if ext_path.startswith("archive:/"):
            parts = [p for p in ext_path.split("/") if p]
            cab = parts[-1] if parts else ""
            owners = self.cab_owner.get(cab.lower())
            if not owners:
                return None
            if len(owners) > 1:
                # Two bundles carrying the same CAB name: ambiguous, so it is not bound.
                self.stats["external_cab_ambiguous"] += 1
                return None
            return owners[0]
        base = os.path.basename(ext_path).lower()
        if base in ("unity default resources", "unity_builtin_extra", "unity editor resources"):
            return "builtin"
        owners = self.cab_owner.get(base)
        if owners and len(owners) == 1:
            return owners[0]
        return None


def load_owner_table(a03_dir=None, manifest=None):
    """Build an :class:`OwnerTable` from the a03 scan output.

    Raises ``SystemExit`` with an actionable message when the scan is absent, because every
    caller needs real ownership data and silently degrading would re-introduce guessing.
    """
    a03_dir = a03_dir or A03_DIR
    index_path = os.path.join(a03_dir, "index.json")
    if manifest is None:
        if not os.path.exists(index_path):
            raise SystemExit(f"a03 scan missing: {index_path}\nrun a03_sources.py first")
        with open(index_path, encoding="utf-8") as fh:
            manifest = json.load(fh)

    table = OwnerTable()
    for src_entry in manifest.get("sources", []):
        rel = str(src_entry.get("rel", "")).replace("\\", "/")
        record_path = os.path.join(a03_dir, "per_source", a03_sources.safe_name(rel))
        objects = []
        if os.path.exists(record_path):
            try:
                with open(record_path, encoding="utf-8") as fh:
                    objects = (json.load(fh) or {}).get("objects") or []
            except (OSError, ValueError) as exc:
                table.defects.append({"kind": "per_source_record_unreadable", "source": rel,
                                      "error": f"{type(exc).__name__}: {exc}"})
                table.stats["per_source_unreadable"] += 1
        else:
            table.defects.append({"kind": "per_source_record_missing", "source": rel,
                                  "expected": record_path})
            table.stats["per_source_missing"] += 1
        table.add_source(rel, src_entry.get("serialized_files") or [], objects)
    table.stats["sources"] = len(manifest.get("sources", []))
    return table


def iter_pptrs(node, path="", out=None):
    """Yield ``(field_path, m_FileID, m_PathID)`` for every PPtr-shaped dict in ``node``.

    A PPtr is recognised structurally -- a two-key dict of integers -- rather than by a
    regex over the serialised text, so a field that merely mentions the words cannot be
    mistaken for a reference.

    ``a04_refs`` still carries an identical private copy; unifying the two is deliberately
    left out of this batch so the change stays reviewable.
    """
    if out is None:
        out = []
    if isinstance(node, dict):
        if "m_FileID" in node and "m_PathID" in node and len(node) == 2:
            fid, pid = node["m_FileID"], node["m_PathID"]
            if isinstance(fid, int) and isinstance(pid, int):
                out.append((path or "<root>", fid, pid))
                return out
        for key, value in node.items():
            iter_pptrs(value, f"{path}.{key}" if path else str(key), out)
    elif isinstance(node, list):
        for i, value in enumerate(node):
            iter_pptrs(value, f"{path}[{i}]", out)
    return out


def summarise(table):
    """Small dict for reports: what the ownership data itself looks like."""
    multi = {f"{src}|{pid}": sfs for (src, pid), sfs in table.pid_index.items() if len(sfs) > 1}
    return {
        "sources": table.stats.get("sources", 0),
        "objects_with_owner": table.stats.get("objects", 0),
        "serialized_files": sum(len(v) for v in table.serialized_files.values()),
        "sources_with_multiple_serialized_files":
            sum(1 for v in table.serialized_files.values() if len(v) > 1),
        "ambiguous_source_pathid_pairs": len(multi),
        "ambiguous_samples": dict(list(multi.items())[:10]),
        "per_source_records_missing": table.stats.get("per_source_missing", 0),
        "per_source_records_unreadable": table.stats.get("per_source_unreadable", 0),
        "objects_without_owner": table.stats.get("object_without_owner", 0),
        "duplicate_owner_entries": table.stats.get("duplicate_owner_entry", 0),
    }
