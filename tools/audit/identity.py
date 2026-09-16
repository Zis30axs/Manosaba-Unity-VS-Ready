"""Lossless object identity model for the Manosaba reconstruction.

Background: the previous auditors keyed objects by ``(source_bundle, pathID)`` and by
bare basenames, which cannot express two different bundles that share a file name, nor
two SerializedFiles inside one bundle that reuse the same local pathID
(UNITY_READINESS_CODEX_AUDIT.md UR-009 / section 3.2.1).

The canonical key used everywhere from now on is::

    SourceObjectKey = (source identity, SerializedFile identity, signed int64 pathID)

* ``source identity``        relative logical path of the container file, plus its
                             SHA-256 when available.  Never a bare basename.
* ``SerializedFile identity``the SerializedFile's own internal name (CAB-xxxx) when
                             present, else ``sf#<index>``.  Never a shared list.
* ``pathID``                 **signed** 64-bit integer, carried as a decimal string so
                             it survives JSON / JavaScript / CSV without precision loss.

``pathID`` ranges observed in ``res/_index.csv``: min -9223201017708124729,
max 9223314696597965781 -- i.e. the full signed int64 domain, with 26,944 of 58,748
rows negative.  Any pipeline that treats pathID as unsigned or as a JS number is
lossy and must be considered a defect.
"""
from __future__ import annotations

import hashlib
import os
import re

INT64_MIN = -(2 ** 63)
INT64_MAX = 2 ** 63 - 1

#: Dump file names may carry a coarse sequence marker: ``Name~7.json``.
#: This is NOT a pathID -- see `dump_sequence` for the measured evidence.
_OUT_SUFFIX = re.compile(r"~(-?\d+)\.json$", re.IGNORECASE)


def dump_sequence(output_file):
    """Return the ``~N`` sequence marker from a dump file name, or ``None``.

    Measured on the real ``_index.csv``: only 163 of 28,340 containers carry a marker,
    the values are contiguous ``1..n`` per container, and 528 rows repeat a value when
    several SerializedFiles were written into one container.  Therefore this is a dump
    ordinal, **not** a pathID, and must never be used as an object key.
    """
    if not output_file:
        return None
    match = _OUT_SUFFIX.search(str(output_file))
    if not match:
        return None
    try:
        return int(match.group(1), 10)
    except ValueError:
        return None


def as_int64(value, what="pathID"):
    """Parse ``value`` as a signed 64-bit integer, raising on overflow/garbage."""
    if isinstance(value, bool) or value is None:
        raise ValueError(f"{what}: not an integer: {value!r}")
    if isinstance(value, int):
        iv = value
    elif isinstance(value, str):
        text = value.strip()
        if not text:
            raise ValueError(f"{what}: empty string")
        if not re.fullmatch(r"[+-]?\d+", text):
            raise ValueError(f"{what}: not a decimal integer: {value!r}")
        iv = int(text, 10)
    else:
        raise ValueError(f"{what}: unsupported type {type(value).__name__}: {value!r}")
    if not (INT64_MIN <= iv <= INT64_MAX):
        raise ValueError(f"{what}: out of signed int64 range: {iv}")
    return iv


def pid_str(value):
    """Canonical lossless decimal string for a pathID."""
    return str(as_int64(value))


def sf_key(name, index):
    """Identity for one SerializedFile inside a source container."""
    if name:
        return str(name)
    return f"sf#{index}"


def source_object_key(source, sf_id, path_id):
    """Build the canonical 3-part key string.

    ``\\x1f`` (unit separator) cannot occur in a Windows path, a CAB name or a
    decimal integer, so the key round-trips without quoting.
    """
    return f"{source}\x1f{sf_id}\x1f{pid_str(path_id)}"


def parse_source_object_key(key):
    source, sf_id, pid = str(key).split("\x1f")
    return source, sf_id, as_int64(pid)


def display_key(key):
    """Human readable form for logs and CSV."""
    source, sf_id, pid = parse_source_object_key(key)
    return f"{source} [{sf_id}] #{pid}"


def sha256_file(path, chunk=1 << 20):
    digest = hashlib.sha256()
    size = 0
    with open(path, "rb") as fh:
        while True:
            block = fh.read(chunk)
            if not block:
                break
            size += len(block)
            digest.update(block)
    return digest.hexdigest(), size


def normalize_rel(path):
    """Forward-slash, lower-cased-extension relative path used as a logical id."""
    return str(path).replace("\\", "/")


def stable_guid(*parts):
    """Deterministic 32-hex GUID from identity parts (Unity GUID form, no dashes).

    Only used for objects that have **no** recoverable original GUID.  Origins are
    recorded separately so a synthesised GUID is never mistaken for a recovered one.
    """
    h = hashlib.sha256()
    for part in parts:
        h.update(str(part).encode("utf-8"))
        h.update(b"\x1f")
    return h.hexdigest()[:32]


def bundle_logical_id(data_root, bundle_path):
    """Logical identity of a bundle file: path relative to the data root, plus size.

    Two bundles with the same basename in different groups must not collide.
    """
    rel = os.path.relpath(os.path.abspath(bundle_path), os.path.abspath(data_root))
    return normalize_rel(rel)
