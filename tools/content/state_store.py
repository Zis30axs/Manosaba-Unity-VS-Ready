"""Read content manifests one resource at a time, retaining compact field reports."""
from __future__ import annotations

import json
from pathlib import Path
import re

_ARRAY_ELEMENT = re.compile(r"\.Array\.data\[\d+\]")


def compact_fields(fields):
    """Keep field patterns, support levels and occurrence counts without array expansion."""
    groups = {}
    reasons = {}
    for source in fields:
        if (not isinstance(source, dict) or not isinstance(source.get("field"), str)
                or source.get("editability") not in ("FULL", "PARTIAL", "READ_ONLY")
                or not isinstance(source.get("reason", ""), str)
                or type(source.get("occurrences", 1)) is not int):
            raise ValueError("Invalid field editability record")
        pattern = _ARRAY_ELEMENT.sub(".Array.data[*]", source["field"])
        count = max(1, source.get("occurrences", 1))
        if pattern not in groups:
            groups[pattern] = {**source, "field": pattern, "occurrences": 0}
            reasons[pattern] = dict.fromkeys(source.get("reason", "").split("; "))
        else:
            extras = lambda item: {key: value for key, value in item.items()
                                   if key not in ("field", "editability", "reason", "occurrences")}
            if extras(groups[pattern]) != extras(source):
                raise ValueError("Conflicting unknown field metadata; refusing lossy compaction")
            if groups[pattern]["editability"] != source["editability"]:
                groups[pattern]["editability"] = "PARTIAL"
            reasons[pattern].update(dict.fromkeys(source.get("reason", "").split("; ")))
        groups[pattern]["occurrences"] += count
    for pattern, group in groups.items():
        group["reason"] = "; ".join(reason for reason in reasons[pattern] if reason)
    return list(groups.values())


class _Reader:
    def __init__(self, stream, chunk_size):
        self.stream = stream
        self.chunk_size = chunk_size
        self.buffer = ""
        self.position = 0
        self.eof = False
        self.decoder = json.JSONDecoder()

    def fill(self, amount=None):
        self.buffer = self.buffer[self.position:]
        self.position = 0
        extra = self.stream.read(amount or self.chunk_size)
        self.buffer += extra
        self.eof = not extra

    def peek(self):
        while True:
            while self.position < len(self.buffer) and self.buffer[self.position] in " \t\r\n":
                self.position += 1
            if self.position < len(self.buffer):
                return self.buffer[self.position]
            if self.eof:
                return ""
            self.fill()

    def expect(self, character):
        if self.peek() != character:
            raise ValueError(f"Malformed content manifest: expected {character!r}")
        self.position += 1

    def value(self):
        if not self.peek():
            raise ValueError("Truncated content manifest")
        while True:
            try:
                result, end = self.decoder.raw_decode(self.buffer, self.position)
                # A number may continue in the next chunk. Require a value boundary.
                if end < len(self.buffer):
                    if self.buffer[end] in " \t\r\n,}:":
                        self.position = end
                        return result
                    if self.eof:
                        raise ValueError("Malformed content manifest value boundary")
                if self.eof:
                    self.position = end
                    return result
            except json.JSONDecodeError:
                if self.eof:
                    raise
            # Grow large individual records geometrically, without retaining earlier records.
            pending = len(self.buffer) - self.position
            self.fill(max(self.chunk_size, pending))


def read_content_state(path: Path, default=None, *, chunk_size=1024 * 1024):
    if not path.is_file():
        return default
    if chunk_size < 1:
        raise ValueError("chunk_size must be positive")
    with path.open(encoding="utf-8-sig") as stream:
        reader = _Reader(stream, chunk_size)
        reader.expect("{")
        state = {}
        while reader.peek() != "}":
            name = reader.value()
            if not isinstance(name, str) or name in state:
                raise ValueError("Invalid or duplicate content manifest key")
            reader.expect(":")
            if name == "files":
                reader.expect("{")
                files = {}
                while reader.peek() != "}":
                    relative = reader.value()
                    if not isinstance(relative, str) or relative in files:
                        raise ValueError("Invalid or duplicate resource key")
                    reader.expect(":")
                    record = reader.value()
                    if not isinstance(record, dict):
                        raise ValueError("Invalid resource record")
                    if "field_editability" in record:
                        if not isinstance(record["field_editability"], list):
                            raise ValueError("Field editability must be an array")
                        record["field_editability"] = compact_fields(record["field_editability"])
                    files[relative] = record
                    if reader.peek() == "}":
                        break
                    reader.expect(",")
                    if reader.peek() == "}":
                        raise ValueError("Trailing comma in resource records")
                reader.expect("}")
                state[name] = files
            else:
                state[name] = reader.value()
            if reader.peek() == "}":
                break
            reader.expect(",")
            if reader.peek() == "}":
                raise ValueError("Trailing comma in content manifest")
        reader.expect("}")
        if reader.peek():
            raise ValueError("Extra data after content manifest")
        return state
