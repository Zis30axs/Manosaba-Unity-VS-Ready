#!/usr/bin/env python3
"""Reconcile Packages/manifest.json against the versions the Editor actually resolved.

Why this exists: the first `manifest.json` was assembled from the Editor's bundled
`.tgz` list and the Unity registry, which produced plausible but not authoritative
versions.  Once the Editor's Package Manager ran it wrote `packages-lock.json`, which is
the real resolution result.  Guesses must yield to it.

Measured drift (manifest -> lock):
  com.unity.render-pipelines.core      17.0.3 -> 17.0.4
  com.unity.render-pipelines.universal 17.0.3 -> 17.0.4
  com.unity.shadergraph                17.0.3 -> 17.0.4
  com.unity.test-framework             1.4.6  -> 1.5.1
  com.unity.textmeshpro            3.2.0-pre.10 -> 5.0.0   (builtin in this Editor)
  com.unity.2d.ik                      absent from the lock entirely

`com.unity.2d.ik` is dropped from the manifest: the Editor resolved every other declared
package but not that one, i.e. it is not required by this Editor version.  Declaring a
package that cannot resolve would break every future resolve.

The script never invents a version: it only writes versions that appear in the lock file,
and reports anything it cannot reconcile.
"""
from __future__ import annotations

import json
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
AUDIT = os.path.join(os.path.dirname(HERE), "audit")
sys.path.insert(0, AUDIT)
import config  # noqa: E402

PKG = os.path.join(config.REPO_ROOT, "UnityProject", "Packages")
MANIFEST = os.path.join(PKG, "manifest.json")
LOCK = os.path.join(PKG, "packages-lock.json")
REPORT = os.path.join(HERE, "out", "package_resolution.json")


def main():
    if not os.path.exists(LOCK):
        print(f"BLOCKED: {LOCK} not found -- run the Editor once so it resolves packages",
              file=sys.stderr)
        return 2
    with open(MANIFEST, encoding="utf-8") as fh:
        manifest = json.load(fh)
    with open(LOCK, encoding="utf-8") as fh:
        lock = json.load(fh)["dependencies"]

    declared = manifest["dependencies"]
    changed, dropped, kept, unresolved = [], [], [], []
    for name, want in sorted(declared.items()):
        entry = lock.get(name)
        if entry is None:
            dropped.append({"package": name, "declared": want,
                            "reason": "not present in packages-lock.json; the Editor did not "
                                      "resolve it, so declaring it would break resolution"})
            continue
        got = entry["version"]
        if got != want:
            changed.append({"package": name, "declared": want, "resolved": got,
                            "source": entry["source"]})
            declared[name] = got
        else:
            kept.append(name)

    for name, want in declared.items():
        if name not in lock:
            unresolved.append({"package": name, "declared": want})

    for d in dropped:
        declared.pop(d["package"], None)

    # Transitive dependencies that the lock pulled in but the manifest never listed are
    # deliberately NOT added: the manifest states direct intent, the lock records the graph.

    manifest["dependencies"] = dict(sorted(declared.items()))
    with open(MANIFEST, "w", encoding="utf-8") as fh:
        json.dump(manifest, fh, indent=2, ensure_ascii=False)
        fh.write("\n")

    report = {
        "manifest": MANIFEST,
        "lock": LOCK,
        "declared_before": len(declared) + len(dropped),
        "declared_after": len(declared),
        "unchanged": sorted(kept),
        "version_corrected": changed,
        "dropped": dropped,
        "unresolved": unresolved,
        "lock_entry_count": len(lock),
        "authoritative_source": "packages-lock.json written by the Unity Package Manager",
        "note": "Transitive packages are intentionally not added to the manifest.",
    }
    os.makedirs(os.path.dirname(REPORT), exist_ok=True)
    with open(REPORT, "w", encoding="utf-8") as fh:
        json.dump(report, fh, ensure_ascii=False, indent=1)

    print(json.dumps({k: v for k, v in report.items()
                      if k not in ("unchanged",)}, ensure_ascii=False, indent=1)[:3000])
    return 0


if __name__ == "__main__":
    sys.exit(main())
