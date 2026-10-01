"""Import original resources through the existing extractor into a protected res tree."""
from __future__ import annotations

import csv
import importlib.util
from collections import Counter
from pathlib import Path
import re
import shutil

import content


def object_identity(row):
    return row.get("source_bundle", "").replace("\\", "/"), str(row.get("path_id", "")), row["type"]


def audio_sample_name(row):
    path = Path(row["output_file"].replace("\\", "/"))
    # Legacy extractors append ~N; this merger appends ~importN. Neither suffix
    # belongs to the decoded sample, and their order can change on re-extraction.
    stem = re.sub(r"(?:~(?:import)?\d+)+$", "", path.stem)
    return stem + path.suffix


def identity(row):
    # A single AudioClip object can export several decoded samples. Persist the
    # sample identity before remapping an output path so a collision cannot change it.
    sample = row.get("sample_id", audio_sample_name(row)) if row["type"] == "AudioClip" else ""
    return (*object_identity(row), sample)


def merge(staging, incoming, overwrite=False):
    """Merge by original object identity, never by incidental duplicate file suffix."""
    old_rows = []
    index = content.RES / "_index.csv"
    if index.is_file():
        with index.open(encoding="utf-8-sig", newline="") as stream:
            old_rows = list(csv.DictReader(stream))
    counts = [Counter(object_identity(row) for row in rows if row["type"] == "AudioClip")
              for rows in (old_rows, incoming)]
    multiple_samples = {key for count in counts for key, number in count.items() if number > 1}
    for row in old_rows + incoming:
        if row["type"] == "AudioClip" and not row.get("sample_id"):
            # Existing six-column indexes need no sample name for one decoded
            # result. Multiple results retain the legacy sample discriminator.
            row["sample_id"] = audio_sample_name(row) if object_identity(row) in multiple_samples else ""
    for rows in (old_rows, incoming):
        sample_counts = Counter(identity(row) for row in rows if row["type"] == "AudioClip")
        if any(number > 1 for number in sample_counts.values()):
            # A legacy sample name can itself end in ~N. Refuse an ambiguous
            # migration before any file copy instead of silently dropping samples.
            raise ValueError("Ambiguous AudioClip sample identity; supply distinct stable sample_id values")
    prior = {identity(row): row for row in old_rows}
    owners = {row["output_file"]: identity(row) for row in old_rows}
    baseline = content.read_json(content.BASELINE, {"version": 1, "files": {}})
    written, preserved, unchanged = 0, [], 0
    for row in incoming:
        previous = prior.get(identity(row))
        destination_rel = previous["output_file"] if previous else row["output_file"]
        if previous is None and destination_rel in owners and owners[destination_rel] != identity(row):
            original = Path(destination_rel)
            number = 1
            while True:
                candidate = original.with_name(f"{original.stem}~import{number}{original.suffix}").as_posix()
                if candidate not in owners and not content.source_path(candidate).exists():
                    destination_rel = candidate
                    break
                number += 1
        source = content.importer.checked_path(staging, row["output_file"])
        target = content.source_path(destination_rel)
        original_hash = content.digest(source)
        current_hash = content.digest(target) if target.is_file() else None
        base_hash = baseline["files"].get(destination_rel, {}).get("sha256")
        modified = current_hash is not None and current_hash != original_hash and (base_hash is None or current_hash != base_hash)
        if modified and not overwrite:
            preserved.append(destination_rel)
        elif current_hash != original_hash:
            target.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(source, target)
            written += 1
        else:
            unchanged += 1
        baseline["files"][destination_rel] = {"sha256": original_hash, "status": "USER_MODIFIED" if modified and not overwrite else "BASE"}
        row["output_file"] = destination_rel
        prior[identity(row)] = row
        owners[destination_rel] = identity(row)
    rows = sorted(prior.values(), key=lambda row: (row["type"], row["output_file"]))
    index.parent.mkdir(parents=True, exist_ok=True)
    temporary_index = index.with_suffix(".csv.tmp")
    with temporary_index.open("w", encoding="utf-8-sig", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=["type", "name", "asset_path", "output_file", "source_bundle", "path_id", "sample_id"])
        writer.writeheader()
        writer.writerows(rows)
    temporary_index.replace(index)
    content.write_json(content.BASELINE, baseline)
    report = {"written": written, "unchanged": unchanged, "user_modified_preserved": preserved,
              "overwrite_user_changes": overwrite}
    content.write_json(content.PIPELINE / "out/content_import_merge.json", report)
    print(f"[import] written={written} unchanged={unchanged} user_modified_preserved={len(preserved)}")
    return rows, report


def extract_and_merge(game, overwrite=False):
    staging = content.REPO / "tools/content/.work/extracted"
    if staging.exists():
        # Resolve and check the recursive deletion in the same process before touching it.
        if staging.resolve() != (content.REPO / "tools/content/.work/extracted").resolve() or not staging.resolve().is_relative_to(content.REPO.resolve()):
            raise ValueError("unsafe import staging directory")
        shutil.rmtree(staging)
    spec = importlib.util.spec_from_file_location("manosaba_existing_extractor", content.REPO / "work/extract_res.py")
    extractor = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(extractor)
    entries, errors = extractor.extract_portable(str(game), str(staging), str(content.REPO / "tools/content/.work"))
    incoming = [{"type": e["type"], "name": e.get("name") or "", "asset_path": e.get("container") or "",
                 "output_file": e["out"], "source_bundle": e["src"], "path_id": str(e["path_id"]),
                 "sample_id": e.get("sample_id", "")} for e in entries]
    if not incoming:
        raise RuntimeError("No assets extracted; the existing res workspace was retained")
    rows, report = merge(staging, incoming, overwrite)
    # Preserve the existing decoder's expected MonoScript metadata table.
    scripts = []
    for row in rows:
        if row["type"] == "MonoScript" and Path(row["source_bundle"].replace("\\", "/")).name == "globalgamemanagers.assets":
            tree = content.read_json(content.source_path(row["output_file"]), {})
            scripts.append({"class": tree.get("m_ClassName", ""), "namespace": tree.get("m_Namespace", ""),
                            "assembly": tree.get("m_AssemblyName", "").removesuffix(".dll"),
                            "path_id": row["path_id"], "out": row["output_file"]})
    if scripts:
        with (content.RES / "_monoscripts.csv").open("w", encoding="utf-8-sig", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=list(scripts[0]))
            writer.writeheader()
            writer.writerows(scripts)
    # Extraction limitations are explicit and remain reviewable, even when useful assets succeeded.
    content.write_json(content.PIPELINE / "out/content_extraction.json", {"assets": len(incoming), "errors": errors})
    if any("load:" in e["message"] or "fatal:" in e["message"] for e in errors):
        raise RuntimeError("Some source bundles could not be read; inspect out/content_extraction.json")
    return report
