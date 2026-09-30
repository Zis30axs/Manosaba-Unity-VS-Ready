"""Independent, offline reader for the shipped Addressables 2.4.6 *binary* catalog.

Verification-only tool: it is NOT the authoritative dump (that is UnityProject/Assets/Editor/
CatalogDump.cs, which goes through the real Addressables runtime).  This script re-implements the
BinaryStorageBuffer layout that Addressables 2.4.6 itself uses (see the package sources
Library/PackageCache/com.unity.addressables@16eddacf275e/Runtime/ResourceManager/Util/BinaryStorageBuffer.cs
and Runtime/ResourceLocators/ContentCatalogData.cs -> ResourceLocator/ResourceLocation.Serializer)
so that a *complete* key -> location membership can be recovered from the shipped catalog.bin
without running Unity.

It is validated against out/unity/catalog_dump_shipped.json (keys, per-key location_count, the 64
recorded locations of every truncated key, primary keys, resource types).  Only validated output is
used as evidence.

Usage:
  python work/parse_catalog_membership.py --validate
  python work/parse_catalog_membership.py --emit work/catalog_membership.json
"""
from __future__ import annotations

import argparse
import collections
import json
import os
import struct
import sys

CATALOG = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\catalog.bin"
DUMP = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "tools", "unity-rebuild",
                    "out", "unity", "catalog_dump_shipped.json")

UNICODE_FLAG = 0x80000000
DYNAMIC_FLAG = 0x40000000
CLEAR_MASK = 0x3FFFFFFF
MAXU32 = 0xFFFFFFFF


class Catalog:
    def __init__(self, path):
        self.raw = open(path, "rb").read()
        self._str_cache = {}

    # -- primitives ---------------------------------------------------------------
    def u32(self, off):
        return struct.unpack_from("<I", self.raw, off)[0]

    def i32(self, off):
        return struct.unpack_from("<i", self.raw, off)[0]

    def value_array_u32(self, uid):
        """Reader.ReadValueArray<uint>(id): uint size prefix lives *before* the data."""
        if uid == MAXU32:
            return []
        size = self.u32(uid - 4)
        return list(struct.unpack_from("<%dI" % (size // 4), self.raw, uid))

    # -- strings ------------------------------------------------------------------
    def _string_internal(self, off, unicode_):
        size = self.u32(off - 4)
        data = self.raw[off:off + size]
        return data.decode("utf-16-le" if unicode_ else "ascii", "replace")

    def _auto_string(self, sid):
        if sid & UNICODE_FLAG:
            return self._string_internal(sid & CLEAR_MASK, True)
        return self._string_internal(sid, False)

    def read_string(self, uid, sep="\0"):
        """Reader.ReadString(id, sep): sep == 0 -> auto encoded, else dynamic (split/join on sep)."""
        if uid == MAXU32:
            return None
        key = (uid, sep)
        if key in self._str_cache:
            return self._str_cache[key]
        if sep == "\0":
            out = self._auto_string(uid)
        elif uid & DYNAMIC_FLAG:
            parts = []
            nxt = uid
            while nxt != MAXU32:
                sid, nxt = struct.unpack_from("<II", self.raw, nxt & CLEAR_MASK)
                parts.append(sid)
            # the writer chains the LAST part at the head, so the chain walks backwards
            out = sep.join(self._auto_string(s) for s in reversed(parts))
        else:
            out = self._auto_string(uid)
        self._str_cache[key] = out
        return out

    # -- catalog ------------------------------------------------------------------
    def header(self):
        magic, version, keys_offset, id_offset = struct.unpack_from("<iiII", self.raw, 0)
        return {"magic": magic, "version": version, "keys_offset": keys_offset, "id_offset": id_offset}

    def type_names(self, type_id):
        """TypeSerializer.Data {uint assemblyId; uint classId} -> (assembly, class)."""
        if type_id == MAXU32:
            return (None, None)
        assembly_id, class_id = struct.unpack_from("<II", self.raw, type_id)
        return (self.read_string(assembly_id, "."), self.read_string(class_id, "."))

    def key_of(self, key_name_offset):
        """ReadObject(keyNameOffset) for a string key: ObjectTypeData -> BuiltinTypesSerializer."""
        type_id, object_id = struct.unpack_from("<II", self.raw, key_name_offset)
        assembly, class_name = self.type_names(type_id)
        string_id, sep = struct.unpack_from("<IH", self.raw, object_id)
        return self.read_string(string_id, chr(sep) if sep else "\0"), class_name

    def location(self, data_off):
        """ResourceLocation.Serializer.Data at data_off -> (primary_key, internal_id, resource_type)."""
        (primary_key_offset, internal_id_offset, _provider_offset, _dep_set,
         _dep_hash, _extra_data, type_id) = struct.unpack_from("<IIIIiII", self.raw, data_off)
        primary_key = self.read_string(primary_key_offset, "/")
        internal_id = self.read_string(internal_id_offset, "/")
        assembly, class_name = self.type_names(type_id)
        return primary_key, internal_id, (class_name or None)

    def keys(self):
        """-> OrderedDict-like list of (key, [location data offsets]) in catalog order."""
        h = self.header()
        records = self.value_array_u32(h["keys_offset"])
        out = []
        for i in range(0, len(records), 2):
            key_name_offset, location_set_offset = records[i], records[i + 1]
            key, _ = self.key_of(key_name_offset)
            out.append((key, self.value_array_u32(location_set_offset)))
        return h, out


def load_dump_locations(dump):
    """internal_id (as the runtime hands it out) -> shipped_internal_id, for cross-checking."""
    m = {}
    for loc in dump["locations"]:
        m[loc["internal_id"]] = loc.get("shipped_internal_id")
    return m


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--catalog", default=CATALOG)
    ap.add_argument("--dump", default=DUMP)
    ap.add_argument("--validate", action="store_true")
    ap.add_argument("--emit")
    ap.add_argument("--emit-tsv")
    args = ap.parse_args()

    cat = Catalog(args.catalog)
    header, keys = cat.keys()
    print("catalog bytes  :", len(cat.raw))
    print("header         :", header)
    print("keys           :", len(keys))

    # resolve every location once
    loc_cache = {}
    edges = 0
    for _key, offsets in keys:
        edges += len(offsets)
    print("key->loc edges :", edges)

    key_locations = collections.OrderedDict()
    for key, offsets in keys:
        ids = []
        for off in offsets:
            if off not in loc_cache:
                loc_cache[off] = cat.location(off)
            ids.append(off)
        key_locations[key] = ids
    print("distinct locs  :", len(loc_cache))

    if args.validate:
        dump = json.load(open(args.dump, encoding="utf-8"))
        shipped_by_internal = load_dump_locations(dump)

        # 1. counts
        print("\n== validation against", os.path.basename(args.dump), "==")
        ok = True
        def check(name, got, want):
            nonlocal ok
            good = got == want
            ok = ok and good
            print("  %-42s got=%-8s want=%-8s %s" % (name, got, want, "OK" if good else "MISMATCH"))

        check("key_count", len(keys), dump["key_count"])
        check("location_edges", edges, dump["location_edges"])
        check("distinct location records", len(loc_cache), dump["location_count"])

        # 2. per-key location_count, and the recorded (truncated) prefix must match exactly
        dump_keys = {r["key"]: r for r in dump["keys"]}
        check("keys only in dump", len(set(dump_keys) - set(key_locations)), 0)
        check("keys only in catalog", len(set(key_locations) - set(dump_keys)), 0)
        count_mismatch = [k for k in dump_keys if len(key_locations.get(k, [])) != dump_keys[k]["location_count"]]
        check("per-key location_count mismatches", len(count_mismatch), 0)
        if count_mismatch[:5]:
            print("     e.g.", count_mismatch[:5])

        # raw catalog internal id -> the ids / values the runtime handed out
        raw_ids = {off: loc_cache[off][1] for off in loc_cache}
        raw_pk = {off: loc_cache[off][0] for off in loc_cache}
        raw_rt = {off: loc_cache[off][2] for off in loc_cache}
        raw_to_dump = {}
        dump_by_internal = {loc["internal_id"]: loc for loc in dump["locations"]}
        placeholder = "{UnityEngine.AddressableAssets.Addressables.RuntimePath}"
        runtime_path = dump.get("runtime_path_used") or ""
        for loc in dump["locations"]:
            shipped = loc.get("shipped_internal_id")
            # the dump stores InternalId *after* Addressables.ResolveInternalId; the catalog stores
            # the "{...RuntimePath}" placeholder form, so expand it back the same way for matching
            raw_to_dump[loc["internal_id"]] = loc["internal_id"]
            if shipped:
                raw_to_dump[shipped] = loc["internal_id"]

        def normalise(raw):
            return raw_to_dump.get(raw) or raw.replace(placeholder, runtime_path)

        check("raw ids without a dump counterpart",
              len({normalise(v) for v in set(raw_ids.values())} - set(dump_by_internal)), 0)

        prefix_mismatch = []
        for k, rec in dump_keys.items():
            n = len(rec["locations"])
            got = [normalise(raw_ids[o]) for o in key_locations[k][:n]]
            if got != rec["locations"]:
                prefix_mismatch.append((k, n))
        check("recorded-location prefixes that differ", len(prefix_mismatch), 0)
        for k, n in prefix_mismatch[:5]:
            print("     e.g.", repr(k), "n=", n)
            print("        got :", [normalise(raw_ids[o]) for o in key_locations[k][:3]])
            print("        want:", dump_keys[k]["locations"][:3])

        # 3. primary key + (internal id, resource type) pair of every location record
        pk_mismatch = 0
        for off in loc_cache:
            rec = dump_by_internal.get(normalise(raw_ids[off]))
            if rec is None:
                continue
            if rec.get("primary_key") != raw_pk[off]:
                pk_mismatch += 1
        check("location primary_key mismatches", pk_mismatch, 0)

        # two catalog entries may share an InternalId and differ only in ResourceType (a sprite and
        # its texture), so the identity the dump de-duplicates on is the (id, type) pair
        mine = collections.Counter((normalise(raw_ids[o]), raw_rt[o]) for o in loc_cache)
        theirs = collections.Counter((loc["internal_id"], loc.get("resource_type") or None)
                                     for loc in dump["locations"])
        only_mine = mine - theirs
        only_theirs = theirs - mine
        check("(internal_id, resource_type) pairs only in catalog", sum(only_mine.values()), 0)
        check("(internal_id, resource_type) pairs only in dump", sum(only_theirs.values()), 0)
        if only_mine or only_theirs:
            print("  pair breakdown (only in catalog):")
            for k, n in only_mine.most_common(10):
                print("     %-24s %-42s %d" % (k[0][:24], k[1], n))
            print("  pair breakdown (only in dump):")
            for k, n in only_theirs.most_common(10):
                print("     %-24s %-42s %d" % (k[0][:24], k[1], n))

        # 4. resource types (the dump's histogram)
        rt_hist = collections.Counter()
        for off in loc_cache:
            rt_hist[raw_rt[off]] += 1
        want_hist = collections.Counter({(h["key"] or None): h["count"] for h in dump["resource_type_histogram"]})
        check("resource_type histogram distinct", len(rt_hist), len(want_hist))
        diff = {k: (rt_hist.get(k, 0), want_hist.get(k, 0)) for k in set(rt_hist) | set(want_hist)
                if rt_hist.get(k, 0) != want_hist.get(k, 0)}
        print("  resource_type histogram differences (ours, dump):", diff or "none")
        print("\nVALIDATION:", "PASS" if ok else "FAIL")
        return 0 if ok else 1

    if args.emit or args.emit_tsv:
        locs_sorted = {}
        for off in sorted(loc_cache):
            pk, iid, rt = loc_cache[off]
            locs_sorted[str(off)] = {"primary_key": pk, "internal_id": iid, "resource_type": rt}
        if args.emit:
            doc = {
                "source": args.catalog,
                "header": header,
                "key_count": len(keys),
                "location_count": len(loc_cache),
                "location_edges": edges,
                "locations_by_offset": locs_sorted,
                "keys": [{"key": k, "locations": v} for k, v in key_locations.items()],
            }
            with open(args.emit, "w", encoding="utf-8") as fh:
                json.dump(doc, fh, ensure_ascii=False)
            print("wrote", args.emit)
        if args.emit_tsv:
            with open(args.emit_tsv, "w", encoding="utf-8", newline="\n") as fh:
                for k, offsets in key_locations.items():
                    if len(offsets) > 1:
                        for off in offsets:
                            fh.write("%s\t%s\n" % (k, loc_cache[off][1]))
            print("wrote", args.emit_tsv)
    return 0


if __name__ == "__main__":
    sys.exit(main())
