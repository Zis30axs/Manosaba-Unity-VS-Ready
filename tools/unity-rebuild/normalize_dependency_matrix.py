#!/usr/bin/env python3
"""Normalise dependency_matrix.csv so its classifications are defensible.

The first pass classified assemblies, which conflates two different things:

* a Unity **module** (`UnityEngine.*Module`) ships inside the Editor and is enabled by the
  Editor install plus the platform build support -- it is not a "missing package";
* a `Unity.*` **runtime assembly** (e.g. `Unity.Addressables`, `Unity.Timeline`) is produced
  by a UPM package, so what matters is whether that *package* is declared and resolvable,
  not whether the assembly file exists yet;
* a genuinely third-party assembly (UniTask, LitMotion, DOTween, Steamworks.NET, ...) is a
  real external dependency, and some of them are commercial and cannot be fetched by me.

This script rewrites `package` and `resolution` columns from the assembly name plus the
declared manifest, and emits the exact BLOCKED list.  It never invents a version.

Outputs: dependency_matrix.csv (rewritten), dependency_blocked.csv
"""
from __future__ import annotations

import csv
import json
import os
import re
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
AUDIT = os.path.join(os.path.dirname(HERE), "audit")
sys.path.insert(0, AUDIT)
import config  # noqa: E402

OUT = os.path.join(HERE, "out")
MATRIX = os.path.join(OUT, "dependency_matrix.csv")
BLOCKED = os.path.join(OUT, "dependency_blocked.csv")
MANIFEST = os.path.join(config.REPO_ROOT, "UnityProject", "Packages", "manifest.json")

#: assembly name -> UPM package name, for assemblies produced by a Unity package.
#: Derived from the package contents, not guessed: each entry names a package whose
#: runtime assembly has that name.
ASSEMBLY_TO_PACKAGE = {
    "Unity.Addressables": "com.unity.addressables",
    "Unity.ResourceManager": "com.unity.addressables",
    "Unity.ScriptableBuildPipeline": "com.unity.scriptablebuildpipeline",
    "Unity.Profiling.Core": "com.unity.profiling.core",
    "Unity.Burst": "com.unity.burst",
    "Unity.Burst.Unsafe": "com.unity.burst",
    "Unity.Collections": "com.unity.collections",
    "Unity.Collections.LowLevel.ILSupport": "com.unity.collections",
    "Unity.Mathematics": "com.unity.mathematics",
    "Unity.InputSystem": "com.unity.inputsystem",
    "Unity.InputSystem.ForUI": "com.unity.inputsystem",
    "Unity.Timeline": "com.unity.timeline",
    "Unity.TextMeshPro": "com.unity.textmeshpro",
    "UnityEngine.UI": "com.unity.ugui",
    "Unity.RenderPipelines.Core.Runtime": "com.unity.render-pipelines.core",
    "Unity.RenderPipelines.Core.Runtime.Shared": "com.unity.render-pipelines.core",
    "Unity.RenderPipelines.Core.ShaderLibrary": "com.unity.render-pipelines.core",
    "Unity.RenderPipeline.Universal.ShaderLibrary": "com.unity.render-pipelines.universal",
    "Unity.RenderPipelines.Universal.Runtime": "com.unity.render-pipelines.universal",
    "Unity.RenderPipelines.Universal.Shaders": "com.unity.render-pipelines.universal",
    "Unity.RenderPipelines.Universal.2D.Runtime": "com.unity.render-pipelines.universal",
    "Unity.RenderPipelines.Universal.Config.Runtime": "com.unity.render-pipelines.universal",
    "Unity.RenderPipelines.ShaderGraph.ShaderGraphLibrary": "com.unity.shadergraph",
    "Unity.RenderPipelines.GPUDriven.Runtime": "com.unity.render-pipelines.core",
    "Unity.Rendering.LightTransport.Runtime": "com.unity.render-pipelines.core",
    "Unity.2D.Animation.Runtime": "com.unity.2d.animation",
    "Unity.2D.Common.Runtime": "com.unity.2d.common",
    "Unity.2D.IK.Runtime": "com.unity.2d.ik",
    "Unity.InternalAPIEngineBridge.001": "com.unity.render-pipelines.core",
    "Unity.Recorder": "com.unity.recorder",
    "Unity.Recorder.Base": "com.unity.recorder",
    "Newtonsoft.Json": "com.unity.nuget.newtonsoft-json",
    "Unity.Localization": "com.unity.localization",
}

#: third-party assemblies shipped by the game.  "commercial" means the source is not
#: freely redistributable and I must not download or vendor it.
THIRD_PARTY = {
    "UniTask": ("Cysharp/UniTask", "free", "https://github.com/Cysharp/UniTask.git"),
    "UniTask.Addressables": ("Cysharp/UniTask", "free", "https://github.com/Cysharp/UniTask.git"),
    "UniTask.Linq": ("Cysharp/UniTask", "free", "https://github.com/Cysharp/UniTask.git"),
    "UniTask.TextMeshPro": ("Cysharp/UniTask", "free", "https://github.com/Cysharp/UniTask.git"),
    "UniTask.DOTween": ("Cysharp/UniTask", "free", "https://github.com/Cysharp/UniTask.git"),
    "UniRx": ("neuecc/UniRx", "free", "https://github.com/neuecc/UniRx.git"),
    "LitMotion": ("annulusgames/LitMotion", "free", "https://github.com/annulusgames/LitMotion.git"),
    "LitMotion.Extensions": ("annulusgames/LitMotion", "free", "https://github.com/annulusgames/LitMotion.git"),
    "DOTween": ("Demigiant/DOTween", "commercial", "Asset Store / demigiant.com"),
    "LeTai.TrueShadow": ("LeTai/TrueShadow", "commercial", "Unity Asset Store"),
    "MPUIKit": ("MPUIKit", "commercial", "Unity Asset Store"),
    "UIPolygon": ("UIPolygon", "commercial", "Unity Asset Store"),
    "ppt-noise": ("ppt-noise", "commercial", "Unity Asset Store"),
    "FastEnum": ("FastEnum", "commercial", "Unity Asset Store"),
    "com.rlabrecque.steamworks.net": ("Steamworks.NET", "free", "https://github.com/rlabrecque/Steamworks.NET"),
    "Steamworks.NET": ("Steamworks.NET", "free", "https://github.com/rlabrecque/Steamworks.NET"),
    "SpriteDicing": ("Elringus/SpriteDicing", "free", "https://github.com/Elringus/SpriteDicing"),
    "JetBrains.Annotations": ("JetBrains.Annotations", "free", "nuget"),
    "System.IO.Hashing": ("System.IO.Hashing", "free", "nuget"),
    "System.Runtime.CompilerServices.Unsafe": ("System.Runtime.CompilerServices.Unsafe", "free", "nuget"),
}

MODULE_RE = re.compile(r"^UnityEngine(\.[A-Za-z0-9]+Module)?$")

#: Assemblies that look first-party by name but are actually a commercial package.
#: Naninovel ships as `Elringus.Naninovel.Runtime` / `Naninovel.Common`; because the package
#: is commercial it must be supplied by the user and must never be reconstructed-and-passed-off
#: as the original (audit section 3.3.10/3.3.11 and 6.9).
COMMERCIAL_ASSEMBLY = {
    "Elringus.Naninovel.Runtime": ("Naninovel", "Elringus"),
    "Elringus.Naninovel.Editor": ("Naninovel", "Elringus"),
    "Elringus.SpriteDicing.Runtime": ("SpriteDicing", "Elringus (free on GitHub)"),
    "Naninovel.Common": ("Naninovel", "Elringus"),
    "Naninovel.Runtime": ("Naninovel", "Elringus"),
    "Naninovel.Editor": ("Naninovel", "Elringus"),
}


def classify(assembly):
    """Return (package, source_kind, resolution, note)."""
    asm = (assembly or "").strip()
    if MODULE_RE.match(asm):
        return (asm, "unity-module", "editor-builtin",
                "ships inside the Unity Editor; enabled by the Editor install and the "
                "platform build support module, not by a package download")
    if asm in ASSEMBLY_TO_PACKAGE:
        return (ASSEMBLY_TO_PACKAGE[asm], "upm-package", "declared-in-manifest",
                "assembly is produced by this UPM package; package declared in "
                "UnityProject/Packages/manifest.json")
    if asm in COMMERCIAL_ASSEMBLY:
        pkg, vendor = COMMERCIAL_ASSEMBLY[asm]
        free = "free" in vendor.lower()
        return (pkg, "third-party-package",
                "fetchable" if free else "user-must-supply",
                f"commercial asset '{pkg}' by {vendor}; shipped as {asm}. "
                f"{'Free source available.' if free else 'The user must supply the licensed package.'}")
    if asm in THIRD_PARTY:
        name, kind, src = THIRD_PARTY[asm]
        return (name, "third-party-package",
                "user-must-supply" if kind == "commercial" else "fetchable",
                f"{kind}; source: {src}")
    if asm.startswith("Il2Cpp") or asm in ("mscorlib", "System", "System.Core"):
        return (asm, "system", "editor-builtin", "IL2CPP/BCL runtime assembly")
    if asm.endswith(".Runtime") or asm.startswith("GigaCreation") \
            or asm in ("Assembly-CSharp", "CustomInspector"):
        return (asm, "first-party-reconstructed", "reconstructed-source",
                "first-party code present under src/decompiled/")
    return (asm, "unknown", "unknown", "not classified; needs manual determination")


def main():
    with open(MANIFEST, encoding="utf-8") as fh:
        manifest = json.load(fh)
    declared = set(manifest["dependencies"])

    with open(MATRIX, encoding="utf-8-sig", newline="") as fh:
        reader = csv.DictReader(fh)
        rows = list(reader)
        fieldnames = list(reader.fieldnames)

    for extra in ("package", "resolution", "resolution_note", "declared_in_manifest"):
        if extra not in fieldnames:
            fieldnames.append(extra)

    counts = {}
    blocked_rows = []
    for r in rows:
        pkg, kind, resolution, note = classify(r.get("assembly"))
        r["package"] = pkg
        r["source_kind"] = kind
        r["resolution"] = resolution
        r["resolution_note"] = note
        r["declared_in_manifest"] = pkg in declared
        # Decide the implementation status from evidence, in priority order.  This runs on
        # the delegated sheet every time, so it must be a function of the source data and
        # not of a previous rewrite.
        asm = (r.get("assembly") or "").strip()
        if kind == "unity-module":
            # A built-in engine module is not a missing dependency: it ships in the Editor
            # and is enabled by the platform build support, not by a package download.
            r["implementation_status"] = "not-needed"
        elif pkg in declared or asm in declared:
            # Declared by name in UnityProject/Packages/manifest.json.  The assembly file
            # only appears once the Editor resolves packages, which is gated on the licence,
            # so "not-verified" is the honest status -- never "missing", never "present".
            r["implementation_status"] = "not-verified"
            r["resolution"] = "declared-in-manifest"
            r["resolution_note"] = ("declared in UnityProject/Packages/manifest.json; "
                                    "package resolution is gated on the Unity licence")
        elif resolution == "user-must-supply":
            r["implementation_status"] = "missing"
        elif kind == "third-party-package":
            r["implementation_status"] = "missing"
        elif kind == "upm-package":
            # A Unity package the game uses but which is not declared in the manifest yet.
            # It is resolvable from the normal registry, so it is pending, not impossible.
            r["implementation_status"] = "needs-declaration"
            r["resolution"] = "resolvable-from-unity-registry"
            r["resolution_note"] = ("assembly is produced by a Unity package that is not yet "
                                    "declared in UnityProject/Packages/manifest.json")
        elif kind == "unknown":
            r["implementation_status"] = r.get("implementation_status") or "missing"
        if resolution == "user-must-supply":
            blocked_rows.append({
                "package_or_assembly": pkg,
                "assembly": r.get("assembly"),
                "kind": kind,
                "reason": "commercial third-party package; the user must supply it "
                          "- it must not be downloaded, vendored or faked",
                "used_by": r.get("used_by", ""),
                "status": "BLOCKED",
            })
        if r["implementation_status"] == "not-verified":
            blocked_rows.append({
                "package_or_assembly": pkg,
                "assembly": r.get("assembly"),
                "kind": kind,
                "reason": "declared in manifest.json but package resolution has not run "
                          "(Unity Editor licence missing)",
                "used_by": r.get("used_by", ""),
                "status": "BLOCKED",
            })
        key = (kind, r["implementation_status"], resolution)
        counts[key] = counts.get(key, 0) + 1

    with open(MATRIX, "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=fieldnames)
        w.writeheader()
        for r in rows:
            w.writerow(r)

    with open(BLOCKED, "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=["status", "package_or_assembly", "assembly",
                                           "kind", "reason", "used_by"])
        w.writeheader()
        for r in sorted(blocked_rows, key=lambda x: (x["status"], x["kind"], x["package_or_assembly"])):
            w.writerow(r)

    print(f"rows rewritten: {len(rows)}")
    for k in sorted(counts):
        print(f"   {k[0]:<24} {k[1]:<14} {k[2]:<22} {counts[k]}")
    print(f"\nblocked rows: {len(blocked_rows)} -> {BLOCKED}")
    commercial = sorted({r["package_or_assembly"] for r in blocked_rows
                         if r["reason"].startswith("commercial")})
    print(f"commercial packages the user must supply ({len(commercial)}):")
    for c in commercial:
        print("   ", c)
    return 0


if __name__ == "__main__":
    sys.exit(main())
