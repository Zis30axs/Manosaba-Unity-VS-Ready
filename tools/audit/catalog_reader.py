#!/usr/bin/env python3
"""Minimal reader for the Addressables `BinaryStorageBuffer` v2 catalog.

Why this exists
---------------
`UNITY_READINESS_CODEX_AUDIT.md` UR-008 / section 3.2.4 and 7.1 record that the previous
revision only read the catalog header and then regex-searched ASCII strings, which cannot
report real keys, GUIDs, labels, providers or resource locations.

The **authoritative** parse is done by the real Addressables runtime inside Unity (see
``UnityProject/Assets/Editor/CatalogDump.cs``), and ``a07_addressables.py`` prefers that
output.  This module is the fallback used when no Editor is available, so a preflight can
still state exactly what is and is not understood instead of returning a string count.

Format (measured on this build, Addressables 2.4.6, catalog.bin 5,551,003 bytes):

    0   u32  magic   0x0DE38942
    4   u32  version 2
    8   u32  entryCount            (== number of following offsets; here 2)
    12  u32[entryCount]            offsets to the sub-buffers
    ...                            sub-buffers, then the string blob

Addresses inside the sub-buffers are plain u32 file offsets; string fields are tagged with
0x80000000 and their low 31 bits give the offset of a NUL-terminated UTF-8 string.

Anything not understood is reported as ``unparsed`` -- never guessed.  Field semantics
beyond what is proven below are deliberately *not* asserted.
"""
from __future__ import annotations

import struct

MAGIC = 0x0DE38942
STRING_TAG = 0x80000000
OFFSET_MASK = 0x7FFFFFFF


class CatalogFormatError(Exception):
    pass


class BinaryStorageBuffer:
    def __init__(self, raw: bytes):
        self.raw = raw
        if len(raw) < 16:
            raise CatalogFormatError(f"file too small: {len(raw)} bytes")
        self.magic, self.version, self.entry_count = struct.unpack_from("<3I", raw, 0)
        if self.magic != MAGIC:
            raise CatalogFormatError(f"bad magic 0x{self.magic:08X}, expected 0x{MAGIC:08X}")
        if self.version != 2:
            raise CatalogFormatError(f"unsupported buffer version {self.version} (expected 2)")
        table_end = 8 + 4 * self.entry_count
        if table_end > len(raw):
            raise CatalogFormatError(f"offset table runs past EOF ({table_end} > {len(raw)})")
        self.offsets = list(struct.unpack_from(f"<{self.entry_count}I", raw, 8)) \
            if self.entry_count else []
        self.errors = []

    # -- primitives --------------------------------------------------------
    def u32(self, at):
        if at < 0 or at + 4 > len(self.raw):
            raise CatalogFormatError(f"u32 out of range at {at}")
        return struct.unpack_from("<I", self.raw, at)[0]

    def u64(self, at):
        if at < 0 or at + 8 > len(self.raw):
            raise CatalogFormatError(f"u64 out of range at {at}")
        return struct.unpack_from("<Q", self.raw, at)[0]

    def is_string_ref(self, value):
        return bool(value & STRING_TAG)

    def string(self, value):
        """Dereference a tagged string reference, or None if it is not one."""
        if not self.is_string_ref(value):
            return None
        at = value & OFFSET_MASK
        if at >= len(self.raw):
            self.errors.append({"kind": "string_offset_out_of_range", "offset": at})
            return None
        end = self.raw.find(b"\x00", at)
        if end < 0:
            self.errors.append({"kind": "string_not_terminated", "offset": at})
            return None
        try:
            return self.raw[at:end].decode("utf-8")
        except UnicodeDecodeError:
            self.errors.append({"kind": "string_not_utf8", "offset": at})
            return None

    def string_at(self, at):
        """Read a length-prefixed (u32 length) UTF-8 string at an absolute offset."""
        if at + 4 > len(self.raw):
            raise CatalogFormatError(f"string length out of range at {at}")
        length = self.u32(at)
        start = at + 4
        end = start + length
        if end > len(self.raw):
            raise CatalogFormatError(f"string payload out of range at {at} (len {length})")
        try:
            return self.raw[start:end].decode("utf-8")
        except UnicodeDecodeError:
            self.errors.append({"kind": "string_not_utf8_at", "offset": at})
            return None

    def read_string_array(self, at):
        """An array of tagged string refs: u32 count followed by count u32 refs."""
        count = self.u32(at)
        if count > 1_000_000:
            raise CatalogFormatError(f"implausible string-array count {count} at {at}")
        values = []
        for i in range(count):
            values.append(self.string(self.u32(at + 4 + 4 * i)))
        return values

    # -- discovery ---------------------------------------------------------
    def probe(self):
        """Report what the buffer contains without claiming unproven semantics.

        The provider class names are the one structure that is unambiguous: they appear as
        a run of ``Namespace.Type`` strings and are what the runtime uses to instantiate
        providers.  The string blob is scanned for them so the caller can at least verify
        provider coverage, and every offset in the header table is dereferenced or reported.
        """
        table = []
        for i, off in enumerate(self.offsets):
            entry = {"index": i, "offset": off, "in_range": off < len(self.raw)}
            if entry["in_range"]:
                entry["first_u32"] = self.u32(off)
                entry["first_string"] = self.string(self.u32(off))
            table.append(entry)
        strings = self.iter_strings()
        return {"header_offsets": table, "string_count": len(strings)}

    def iter_strings(self):
        """Every NUL-terminated printable UTF-8 run of length >= 4 in the blob."""
        raw = self.raw
        out = []
        i = 0
        n = len(raw)
        while i < n:
            if 0x20 <= raw[i] < 0x7F:
                j = i
                while j < n and 0x20 <= raw[j] < 0x7F:
                    j += 1
                if j - i >= 4:
                    out.append(raw[i:j].decode("ascii"))
                i = j
            else:
                i += 1
        return out

    def provider_names(self):
        """Strings that look like fully-qualified provider type names."""
        names = set()
        for s in self.iter_strings():
            if "." in s and ("Provider" in s or "Provider" in s.split(".")[-1]):
                names.add(s)
        return sorted(names)

    def bundle_names(self):
        """Strings that look like AssetBundle file names."""
        return sorted({s for s in self.iter_strings() if s.endswith(".bundle")})

    def runtime_path_literals(self):
        return sorted({s for s in self.iter_strings()
                       if s in ("StandaloneWindows64", "StandaloneWindows", "Android",
                                "iOS", "WebGL", "Local", "Remote")})


def load_catalog(path):
    with open(path, "rb") as fh:
        raw = fh.read()
    return BinaryStorageBuffer(raw)


if __name__ == "__main__":
    import json
    import sys
    buf = load_catalog(sys.argv[1])
    info = buf.probe()
    info["providers"] = buf.provider_names()
    info["bundles_mentioned"] = len(buf.bundle_names())
    info["bundle_sample"] = buf.bundle_names()[:5]
    info["runtime_paths"] = buf.runtime_path_literals()
    print(json.dumps(info, ensure_ascii=False, indent=1)[:4000])
