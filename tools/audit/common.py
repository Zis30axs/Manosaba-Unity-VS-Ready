"""Shared helpers for the Unity project reconstruction audit.

Rewritten so that:

* every path comes from :mod:`config` (env vars / JSON config), never a hard-coded
  drive letter (audit UR-012, section 3.1.4);
* object identity follows :mod:`identity` -- ``(source identity, SerializedFile
  identity, signed int64 pathID)`` -- instead of ``(source_bundle, pathID)`` or a
  bare basename (audit UR-009, section 3.2.1);
* index defects (duplicates, unparsable pathIDs, missing dumps) are reported instead
  of being silently collapsed.
"""
from __future__ import annotations

import collections
import csv
import json
import os
import struct
import sys

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

import identity  # noqa: E402  (local module, path set above)
from config import (  # noqa: E402,F401  re-exported for the a01..a08 scripts
    AA,
    AA_ROOT,
    AAW,
    ADDR_MAGIC,
    BUNDLE_MAGICS,
    DERIVED,
    GAME,
    GAME_DATA_ROOT,
    GAME_ROOT,
    MissingInput,
    OUT,
    OUTPUT_ROOT,
    PYTHON_EXE,
    REPO,
    REPO_ROOT,
    RES,
    RES_ROOT,
    SOURCE,
    SOURCE_ROOT,
    UNITY_EDITOR_PATH,
    UNITY_YAML_MAGIC,
    UNITY_YAML_TAG,
    ensure_out,
    out_path,
    require,
    resolve_within,
)

#: where the new integration pipeline writes its evidence
UNITY_REBUILD_OUT = OUTPUT_ROOT
REBUILD_OUT = os.path.join(REPO_ROOT, "tools", "unity-rebuild", "out")

_cfg = DERIVED
CATALOG_BIN = _cfg["catalog_bin"]
CATALOG_HASH = os.path.join(AA_ROOT, "catalog.hash") if AA_ROOT else ""
SETTINGS_JSON = _cfg["settings_json"]
INDEX_CSV = os.path.join(RES_ROOT, "_index.csv") if RES_ROOT else ""
INDEX_AUDIO_CSV = os.path.join(RES_ROOT, "_index_audio.csv") if RES_ROOT else ""
MONOSCRIPTS_CSV = os.path.join(RES_ROOT, "_monoscripts.csv") if RES_ROOT else ""


def dump(name, obj):
    """Write ``obj`` as JSON into the audit output directory."""
    path = out_path(name)
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, "w", encoding="utf-8") as fh:
        json.dump(obj, fh, ensure_ascii=False, indent=1)
    return path


def dump_jsonl(name, records):
    path = out_path(name)
    os.makedirs(os.path.dirname(path), exist_ok=True)
    n = 0
    with open(path, "w", encoding="utf-8", newline="\n") as fh:
        for rec in records:
            fh.write(json.dumps(rec, ensure_ascii=False) + "\n")
            n += 1
    return path, n


# --------------------------------------------------------------------------- #
# Index-derived object ledger
# --------------------------------------------------------------------------- #

class UnknownPathID(KeyError):
    """The row's pathID is unknown; it must not be faked as a valid 0.

    ``adopt_orphans`` wrote ``path_id=0`` for every row it could not attribute to a source
    bundle.  Those zeros are placeholders, not identifiers: several distinct objects carry
    the same 0.  Returning 0 here would silently collapse them into one bucket, which is
    exactly the defect the canonical ledger exists to prevent, so the access raises instead.
    """


class IndexRow(dict):
    """One row of ``res/_index.csv`` enriched with canonical identity."""

    __slots__ = ()

    @property
    def key(self):
        return self["key"]

    @property
    def pathid_known(self):
        """False for rows whose pathID the ripper never recovered."""
        return self.get("provenance") != "recovered_unattributed"

    @property
    def pid(self):
        """Signed int64 pathID -- the same value as the canonical ``pid_int`` field.

        Raises :class:`UnknownPathID` when the pathID is unknown, so a caller has to make
        an explicit decision instead of bucketing the row under a fabricated 0.
        """
        if not self.pathid_known:
            raise UnknownPathID(
                f"pathID unknown for {self.get('output_file')!r} "
                f"(provenance={self.get('provenance')!r}); use .pathid_known before reading .pid")
        return self["pid_int"]

    def __missing__(self, name):
        # ``row["pid"]`` is an alias for the canonical ``pid_int`` field and obeys the same
        # unknown-pathID rule as the property; every other missing key stays a KeyError.
        if name == "pid":
            return self.pid
        raise KeyError(name)


class IndexLedger:
    """A defect-reporting view over the ripper's ``_index.csv``."""

    def __init__(self, rows, defects, stats):
        self.rows = rows
        self.defects = defects
        self.stats = stats

    def by_key(self):
        return {r["key"]: r for r in self.rows}

    def by_container(self):
        out = collections.defaultdict(list)
        for r in self.rows:
            out[r["asset_path"]].append(r)
        return out

    def by_source(self):
        out = collections.defaultdict(list)
        for r in self.rows:
            out[r["source_identity"]].append(r)
        return out

    def by_type(self):
        out = collections.Counter()
        for r in self.rows:
            out[r["type"]] += 1
        return out


def load_index_ledger(index_csv=None, res_root=None, check_files=True):
    """Read the index and build canonical, lossless object identities.

    Returns an :class:`IndexLedger`.  ``defects`` is a list of dicts, each with a
    ``kind`` plus the identifying fields needed to reproduce it.
    """
    index_csv = index_csv or INDEX_CSV
    res_root = res_root or RES_ROOT
    defects = []
    rows = []
    stats = collections.Counter()

    if not index_csv or not os.path.exists(index_csv):
        raise MissingInput(f"object index not found: {index_csv}")

    seen_keys = {}
    with open(index_csv, encoding="utf-8-sig", newline="") as fh:
        reader = csv.DictReader(fh)
        for lineno, raw in enumerate(reader, start=2):
            stats["csv_rows"] += 1
            raw_source = (raw.get("source_bundle") or "").strip()
            output_file = identity.normalize_rel(raw.get("output_file") or "")
            asset_path = (raw.get("asset_path") or "").strip()
            obj_type = (raw.get("type") or "").strip()

            # --- pathID -----------------------------------------------------------
            # NOTE (verified against the real index, not assumed):
            #   * `_index.csv:path_id` IS the real signed int64 pathID.  It spans the
            #     full signed range (min -9223201017708124729, max 9223314696597965781)
            #     and is unique per source for 58,719 of 58,748 rows.
            #   * The `~N` in `output_file` is NOT a pathID.  It is a coarse per-asset
            #     sequence marker: only 163 of 28,340 containers carry one, it is only
            #     1..n per container, and it repeats when several SerializedFiles went
            #     into one container (528 rows share a value).  Parsing it as a pathID
            #     would silently corrupt identity, so it is recorded as `dump_seq`
            #     evidence only and never used as the key.
            csv_pid = None
            raw_pid = (raw.get("path_id") or "").strip()
            if raw_pid:
                try:
                    csv_pid = identity.as_int64(raw_pid)
                except ValueError as exc:
                    defects.append({"kind": "pathid_unparsable", "line": lineno,
                                    "raw": raw_pid, "error": str(exc),
                                    "source": raw_source, "output_file": output_file})
                    stats["pathid_unparsable"] += 1
            dump_seq = identity.dump_sequence(output_file)
            if csv_pid is None:
                defects.append({"kind": "pathid_absent", "line": lineno,
                                "source": raw_source, "output_file": output_file,
                                "dump_seq": dump_seq})
                stats["pathid_absent"] += 1
                continue
            pid = csv_pid

            # --- source identity ---------------------------------------------------
            if raw_source and raw_source != "(recovered)":
                source_identity = raw_source.replace("\\", "/")
                provenance = "index"
            else:
                # The ripper could not attribute these 30 objects to a source bundle and
                # wrote path_id 0 for all of them.  Keying them by container path alone
                # collapses 21 distinct objects into one key, and the `~N` marker is not
                # unique either, so the dump file's own relative path -- the only
                # per-object fact actually present -- is used as the logical source.
                # The real pathID is NOT invented; it is reported as unknown below.
                stem = output_file
                if stem.startswith("_serialized/"):
                    stem = stem[len("_serialized/"):]
                if stem.lower().endswith(".json"):
                    stem = stem[:-5]
                source_identity = f"(unattributed)/{stem}"
                provenance = "recovered_unattributed"
                stats["unattributed_rows"] += 1

            if provenance == "recovered_unattributed":
                sf_id = "(recovered)"
                if pid != 0:
                    defects.append({"kind": "recovered_row_with_pathid", "line": lineno,
                                    "path_id": str(pid), "output_file": output_file})
                # pathID is unknown for these rows.  Record that explicitly rather than
                # presenting the ripper's 0 placeholder as a real object identifier.
                stats["unattributed_pathid_unknown"] += 1
                key = identity.source_object_key(source_identity, sf_id, 0)
            else:
                sf_id = "index"
                key = identity.source_object_key(source_identity, sf_id, pid)
            rec = IndexRow(raw)
            rec.update({
                "line": lineno,
                "type": obj_type,
                "name": (raw.get("name") or "").strip(),
                "asset_path": asset_path,
                "output_file": output_file,
                "source_identity": source_identity,
                "source_bundle_raw": raw_source,
                "provenance": provenance,
                "serialized_file_id": sf_id,
                "pid_int": pid,
                "csv_path_id": str(csv_pid),
                "dump_seq": dump_seq,
                "key": key,
            })
            if check_files and res_root:
                full = os.path.join(res_root, output_file.replace("/", os.sep))
                rec["dump_exists"] = os.path.exists(full)
                rec["dump_path"] = full
                if not rec["dump_exists"]:
                    stats["dump_missing"] += 1
                    defects.append({"kind": "dump_file_missing", "line": lineno,
                                    "output_file": output_file, "type": obj_type,
                                    "source": source_identity, "path_id": str(pid)})

            if key in seen_keys:
                stats["duplicate_keys"] += 1
                defects.append({"kind": "duplicate_object_key", "key_display": identity.display_key(key),
                                "first_line": seen_keys[key], "line": lineno,
                                "source": source_identity, "path_id": str(pid),
                                "type": obj_type, "output_file": output_file})
            else:
                seen_keys[key] = lineno
            rows.append(rec)
            stats["rows"] += 1

    stats["distinct_keys"] = len(seen_keys)
    # Detect dump JSON files that the index never mentions (audit 3.2.8: files
    # outside the old index must not be skipped silently).
    orphans = []
    if check_files and res_root and os.path.isdir(res_root):
        on_disk = set()
        for dirpath, dirnames, files in os.walk(res_root):
            dirnames[:] = [d for d in dirnames if d not in (".git", "__pycache__")]
            for f in files:
                if f.lower().endswith(".json"):
                    rel = os.path.relpath(os.path.join(dirpath, f), res_root).replace(os.sep, "/")
                    on_disk.add(rel)
        indexed = {r["output_file"] for r in rows}
        orphans = sorted(on_disk - indexed)
        stats["json_files_on_disk"] = len(on_disk)
        stats["json_files_not_in_index"] = len(orphans)
    return IndexLedger(rows, defects, stats), orphans


def load_index():
    """Backwards-compatible flat row list (old callers expect a list of dicts)."""
    ledger, _ = load_index_ledger()
    return ledger.rows


def iter_res_files(root=None, exts=None):
    root = root or RES_ROOT or "."
    for dirpath, dirnames, files in os.walk(root):
        dirnames[:] = [d for d in dirnames if d not in (".git", "__pycache__")]
        for f in files:
            if exts and not f.lower().endswith(exts):
                continue
            yield os.path.join(dirpath, f)


def sniff_header(path):
    """Return a coarse content classification independent of extension."""
    try:
        with open(path, "rb") as fh:
            head = fh.read(64)
            size = os.path.getsize(path)
            if size > 64:
                fh.seek(0)
                fh.read(4)
    except OSError:
        return "unreadable", None
    if head.startswith(BUNDLE_MAGICS[0]):
        return "AssetBundle(UnityFS)", None
    if head.startswith(BUNDLE_MAGICS[2]):
        return "AssetBundle(UnityRaw)", None
    if head.startswith(UNITY_YAML_MAGIC):
        return ("UnityYAML", "editor-ready") if UNITY_YAML_TAG in head else ("UnityYAML?", None)
    if head[:4] == b"\x1f\x8b\x08\x00":
        return "gzip", None
    if head[:2] == b"PK":
        return "zip", None
    if head[:4] == struct.pack("<I", ADDR_MAGIC):
        return "AddressablesCatalog(BinaryStorageBuffer v2)", None
    if head[:4] in (b"OTTO", b"ttcf"):
        return "font", None
    if head[:4] == b"OggS":
        return "ogg-audio", None
    if head[:8] == b"\x89PNG\r\n\x1a\n":
        return "png", None
    if head[:4] == b"AF\x1b\xb1\xfa":
        return "il2cpp-metadata", None
    if head[:2] == b"MZ":
        return "PE-binary", None
    if head.startswith(b"{\rtf") or head.startswith(b"{\n") or head.startswith(b"{"):
        return "json/text", None
    if head.startswith(b"ftyp") or b"ftyp" in head[:16]:
        return "mp4", None
    # Unity SerializedFile heuristic: (metadata_size, file_size, version, data_offset)
    if len(head) >= 20:
        m0, m1, m2, m3 = struct.unpack("<IIII", head[:16])
        if 6 <= m2 <= 30 and m0 != 0 and m1 >= m0 and size >= (m3 or 0):
            return "UnitySerializedFile", None
    try:
        head.decode("utf-8")
        return "text", None
    except UnicodeDecodeError:
        return "binary", None


def load_monoscript_table(force=False):
    """pathID -> {class, namespace, assembly} for the two MonoScript stores.

    ``res/_monoscripts.csv`` only covers globalgamemanagers.assets.  Addressables
    bundles reference a *separate* ``monoscripts.bundle`` whose 211 MonoScript
    pathIDs do not overlap, so it must be read from the bundle as well.
    """
    p = out_path("monoscript_table.json")
    if os.path.exists(p) and not force:
        try:
            with open(p, encoding="utf-8") as fh:
                return json.load(fh)
        except (OSError, ValueError) as exc:
            print(f"[common] WARNING: monoscript_table.json unreadable ({exc}); rebuilding",
                  file=sys.stderr)
    import UnityPy
    table = {"by_pid": {}, "by_identity": {}, "sources": {}}
    with open(MONOSCRIPTS_CSV, encoding="utf-8-sig", newline="") as fh:
        for r in csv.DictReader(fh):
            pid = int(r["path_id"])
            rec = {"class": r["class"], "namespace": r["namespace"], "assembly": r["assembly"],
                   "store": "globalgamemanagers.assets"}
            table["by_pid"][str(pid)] = rec
            table["by_identity"][f"{r['assembly']}|{r['namespace']}|{r['class']}"] = rec
    fp = os.path.join(AAW, "bc78c2a56704c3e9425984fc8925c526_monoscripts.bundle")
    n = 0
    if os.path.exists(fp):
        env = UnityPy.load(fp)
        for o in env.objects:
            if o.type.name != "MonoScript":
                continue
            d = o.read()
            rec = {"class": d.m_ClassName, "namespace": d.m_Namespace, "assembly": d.m_AssemblyName,
                   "store": "bc78c2a56704c3e9425984fc8925c526_monoscripts.bundle"}
            table["by_pid"][str(o.path_id)] = rec
            table["by_identity"][f"{d.m_AssemblyName}|{d.m_Namespace}|{d.m_ClassName}"] = rec
            n += 1
        del env
    table["sources"] = {"ggm_assets_unique": len(table["by_identity"]), "monoscripts_bundle": n}
    dump("monoscript_table.json", table)
    return table


if __name__ == "__main__":
    import config
    print("config sources:", config.CFG_SOURCES or "(defaults only)")
    for key, value in config.describe():
        print(f"  {key:18s} {value}")
    ledger, orphans = load_index_ledger()
    print("\nindex stats:", dict(ledger.stats))
    print("defect kinds:", dict(collections.Counter(d["kind"] for d in ledger.defects)))
    print("json files not in index:", len(orphans), orphans[:5])
