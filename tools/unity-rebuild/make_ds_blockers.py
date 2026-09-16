#!/usr/bin/env python3
"""Build the machine-readable DS blocker hand-off from the classified stub inventory.

Companion to `DS_CSHARP_BLOCKERS.md`, in the column set required by
`UNITY_REBUILD_AGENT_BOUNDARIES.md` lines 84-95:

  assembly, file, type, member, symptom, mechanical_or_semantic,
  required_transformation, evidence, priority_or_gate, acceptance_test

The integration-level blockers (duplicate type, missing commercial package, namespace
compilation) are added explicitly, because they are not rows in the stub inventory.

Output: tools/unity-rebuild/out/ds_csharp_blockers.csv
"""
from __future__ import annotations

import csv
import os
import sys

HERE = os.path.dirname(os.path.abspath(__file__))
AUDIT = os.path.join(os.path.dirname(HERE), "audit")
sys.path.insert(0, AUDIT)
import config  # noqa: E402

OUT = os.path.join(HERE, "out")
SRC = os.path.join(OUT, "ds_runtime_blockers.csv")
DST = os.path.join(OUT, "ds_csharp_blockers.csv")

COLUMNS = ["assembly", "file", "type", "member", "symptom", "mechanical_or_semantic",
           "required_transformation", "evidence", "priority_or_gate", "acceptance_test",
           "source_of_row", "status"]

#: Integration-level blockers that the routed stub inventory cannot express.
INTEGRATION_BLOCKERS = [
    {
        "assembly": "GigaCreation.Essentials.AddressablesUtils",
        "file": "src/decompiled/GigaCreation.Essentials.AddressablesUtils/"
                "GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs",
        "type": "GigaCreation.Essentials.AddressablesUtils.AddressablesManager",
        "member": "(whole type)",
        "symptom": "CS0101 duplicate type definition when an integration reconstruction of "
                   "the same type is compiled alongside this file",
        "mechanical_or_semantic": "semantic",
        "required_transformation": "choose exactly one owner: keep this file, keep the "
                                    "reconstruction as an integration shim, or use the real "
                                    "upstream GigaCreation.Essentials package",
        "evidence": "UnityProject/Deferred/AddressablesManager.cs (parked outside Assets/); "
                    "UNITY_REBUILD_AGENT_BOUNDARIES.md:80",
        "priority_or_gate": "G1",
        "acceptance_test": "Unity compiles the assembly with 0 CS0101 and exactly one "
                           "AddressablesManager definition",
        "source_of_row": "integration",
        "status": "BLOCKED_ON_DECISION",
    },
    {
        "assembly": "Elringus.Naninovel.Runtime;Naninovel.Common",
        "file": "(package)",
        "type": "Naninovel.Engine;Naninovel.ScriptPlayer",
        "member": "Initialize;GetService;GetServiceOrErr;GetConfiguration;TryGetService;"
                  "Play;PlayRoutine;SelectNextCommand;InitializeService",
        "symptom": "service lookups return null and the engine never initialises, so nothing "
                   "in the game can run",
        "mechanical_or_semantic": "semantic",
        "required_transformation": "supply the real commercial package; do NOT reconstruct it",
        "evidence": "8,006 stub rows; manosaba_game/LICENSE.md:133-135 carries an Elringus MIT "
                    "notice recorded as evidence only, not as an entitlement",
        "priority_or_gate": "G1;G4",
        "acceptance_test": "Engine.Initialize completes and GetServiceOrErr<T>() returns "
                           "non-null for a registered service",
        "source_of_row": "integration",
        "status": "BLOCKED_MISSING_PACKAGE",
    },
    {
        "assembly": "all src/decompiled/**",
        "file": "10,260 of 10,487 .cs",
        "type": "(syntax)",
        "member": "(file-scoped namespace declarations)",
        "symptom": "Unity 6000.0.48f1 defaults to C# 9; 'namespace X;' is C# 10 syntax",
        "mechanical_or_semantic": "mechanical",
        "required_transformation": "convert to block form, or raise the language version "
                                   "through a supported and verified route",
        "evidence": "tools/unity-rebuild/out/csharp_compat.json (record x13 is C# 9 and is "
                    "not a blocker); converter at tools/unity-rebuild/convert_namespaces.py",
        "priority_or_gate": "G1",
        "acceptance_test": "Unity's real compiler reports 0 errors attributable to "
                           "namespace syntax",
        "source_of_row": "integration",
        "status": "BLOCKED_ON_DECISION",
    },
    {
        "assembly": "any assembly using [UsedImplicitly]",
        "file": "115 of 1,651 files",
        "type": "(attribute)",
        "member": "(attribute usage)",
        "symptom": "missing JetBrains.Annotations reference",
        "mechanical_or_semantic": "mechanical",
        "required_transformation": "drop the attributes or add the annotations package to "
                                   "the runtime assembly set",
        "evidence": "JetBrains.Annotations absent from the shipped BepInEx/interop set",
        "priority_or_gate": "G1",
        "acceptance_test": "affected assemblies compile without the annotations reference",
        "source_of_row": "integration",
        "status": "OPEN",
    },
    {
        "assembly": "all src/decompiled/**",
        "file": "(tree-wide)",
        "type": "(attributes/usings)",
        "member": "(Il2CppDummyDll markers)",
        "symptom": "runtime source cannot reference the dummy assembly",
        "mechanical_or_semantic": "mechanical",
        "required_transformation": "remove Il2CppDummyDll usings and [Token]/[Address]/"
                                   "[FieldOffset] attributes WITHOUT removing real "
                                   "serialization attributes such as [SerializeField] and "
                                   "[Serializable]",
        "evidence": "audit UR-005 / section 3.3.8; dependency_matrix.csv "
                    "il2cpp-interop-proxy rows must never be the new Player's implementation",
        "priority_or_gate": "G1",
        "acceptance_test": "runtime assemblies compile with no Il2CppDummyDll reference and "
                           "serialized-field counts are unchanged",
        "source_of_row": "integration",
        "status": "OPEN",
    },
]

#: Routed rows that are genuinely actionable for DS.
ACTIONABLE_ROUTING = {"DS_RECONSTRUCTION_REQUIRED", "INTEGRATION_SHIM_REQUIRED"}


def main():
    if not os.path.exists(SRC):
        print(f"BLOCKED: {SRC} not found", file=sys.stderr)
        return 2

    with open(SRC, encoding="utf-8-sig", newline="") as fh:
        routed = list(csv.DictReader(fh))

    rows = []
    for r in routed:
        if r.get("routing") not in ACTIONABLE_ROUTING:
            continue
        rows.append({
            "assembly": r.get("assembly", ""),
            "file": r.get("file", ""),
            "type": r.get("type", ""),
            "member": r.get("method", ""),
            "symptom": r.get("symptom", ""),
            "mechanical_or_semantic": r.get("mechanical_or_semantic", ""),
            "required_transformation": r.get("required_transformation", ""),
            "evidence": r.get("evidence", ""),
            "priority_or_gate": r.get("gate", ""),
            "acceptance_test": r.get("acceptance_test", ""),
            "source_of_row": "ds_runtime_blockers.csv:" + r.get("routing", ""),
            "status": r.get("status", ""),
        })
    rows.extend(INTEGRATION_BLOCKERS)

    os.makedirs(OUT, exist_ok=True)
    with open(DST, "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=COLUMNS)
        w.writeheader()
        for r in rows:
            w.writerow(r)

    print(f"wrote {DST}")
    print(f"  routed rows considered: {len(routed)}")
    print(f"  actionable routed rows: {len(rows) - len(INTEGRATION_BLOCKERS)}")
    print(f"  integration blockers  : {len(INTEGRATION_BLOCKERS)}")
    print(f"  total rows            : {len(rows)}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
