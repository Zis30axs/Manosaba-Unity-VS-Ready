"""Type-check specific Editor scripts with Unity's own Roslyn and the reference set of the real
Assembly-CSharp-Editor compile, without running Unity.

The reference set comes from Library/Bee/artifacts/rsp/2781082588993088484.rsp, the response file
Unity's build for Assembly-CSharp-Editor.dll, whose `-r` list is the exact reference set of that
assembly.
"""
import os
import re
import subprocess
import sys
import tempfile

PROJECT = r"E:\Manosaba-VSCodeReady\UnityProject"
RSP = os.path.join(PROJECT, r"Library\Bee\artifacts\rsp\2781082588993088484.rsp")
UNITY = r"E:\Unity\6000.0.48f1"
DOTNET = os.path.join(UNITY, r"Editor\Data\NetCoreRuntime\dotnet.exe")
CSC = os.path.join(UNITY, r"Editor\Data\DotNetSdkRoslyn\csc.dll")

SOURCES = [os.path.join(PROJECT, r"Assets\Editor\CatalogDump.cs"),
           os.path.join(PROJECT, r"Assets\Editor\AddressablesSetup.cs")]


def tokens(text):
    return re.findall(r'"([^"]*)"|(\S+)', text)


def main():
    raw = open(RSP, encoding="utf-8", errors="replace").read()
    quoted, flags = [], []
    for q, bare in tokens(raw):
        if q:
            quoted.append(q)
        else:
            flags.append(bare)
    print("quoted tokens:", len(quoted), "bare flags:", sorted(set(flags)))

    out_dll = next(q for q in quoted if q.endswith("Assembly-CSharp-Editor.dll"))
    refs = [q for q in quoted if q.lower().endswith(".dll") and q != out_dll]
    defines = [q for q in quoted if re.fullmatch(r"[A-Z0-9_]+", q)]
    print("references:", len(refs))
    print("defines:", len(defines))

    missing = [r for r in refs if not os.path.exists(r if os.path.isabs(r) else os.path.join(PROJECT, r))]
    print("references that do not exist on disk:", len(missing))
    for m in missing[:10]:
        print("   MISSING", m)

    outdir = tempfile.mkdtemp(prefix="manosaba-csc-")
    rsp_out = os.path.join(outdir, "typecheck.rsp")
    dll_out = os.path.join(outdir, "typecheck.dll")
    lines = ["-nologo", "-nostdlib+", "-target:library", "-langversion:9.0",
             "-nowarn:0169,0649,0414,0162,0067,0108,0169",
             "-define:" + ";".join(defines),
             "-out:" + dll_out]
    for r in refs:
        lines.append('-r:"%s"' % (r if os.path.isabs(r) else os.path.join(PROJECT, r)))
    for s in SOURCES:
        lines.append('"%s"' % s)
    with open(rsp_out, "w", encoding="utf-8") as fh:
        fh.write("\n".join(lines))
    print("rsp:", rsp_out)

    cmd = [DOTNET, CSC, "@" + rsp_out]
    print("command:", " ".join(cmd))
    proc = subprocess.run(cmd, cwd=PROJECT, capture_output=True, text=True, encoding="utf-8",
                          errors="replace")
    out = (proc.stdout or "") + (proc.stderr or "")
    print("\n---- compiler output (exit code %d) ----" % proc.returncode)
    print(out.strip() or "(no output)")
    interesting = [l for l in out.splitlines()
                   if "CatalogDump" in l or "AddressablesSetup" in l]
    print("\ndiagnostics naming the modified files:", len(interesting))
    for l in interesting:
        print("   ", l)
    print("output dll exists:", os.path.exists(dll_out),
          "bytes:", os.path.getsize(dll_out) if os.path.exists(dll_out) else 0)
    if os.path.exists(dll_out):
        blob = open(dll_out, "rb").read()
        for want in (b"CatalogDump", b"AddressablesSetup", b"label_membership", b"labels_unregistered"):
            print("   %-22s present in the emitted assembly: %s" % (want.decode(), want in blob))
    return 0 if (proc.returncode == 0 and not interesting) else 1


if __name__ == "__main__":
    sys.exit(main())
