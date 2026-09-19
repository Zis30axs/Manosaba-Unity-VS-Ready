"""Type-check the Editor assembly (Assets/Editor/*.cs) with Unity's own Roslyn and the reference set
Unity recorded for Assembly-CSharp-Editor (Library/Bee/artifacts/rsp).  Read-only; no Unity launch.
Usage: python work/serialization-loss/typecheck_editor.py [override_dir]
"""
import glob, os, re, subprocess, sys, tempfile

PROJECT = r"E:\Manosaba-VSCodeReady\UnityProject"
UNITY = r"E:\Unity\6000.0.48f1"
DOTNET = os.path.join(UNITY, r"Editor\Data\NetCoreRuntime\dotnet.exe")
CSC = os.path.join(UNITY, r"Editor\Data\DotNetSdkRoslyn\csc.dll")
RSP_DIR = os.path.join(PROJECT, r"Library\Bee\artifacts\rsp")


def find_rsp(dll_suffix):
    for f in glob.glob(os.path.join(RSP_DIR, "*.rsp")):
        raw = open(f, encoding="utf-8", errors="replace").read()
        m = re.search(r'"([^"]*\.dll)"', raw)
        if m and m.group(1).replace("\\", "/").endswith("1900b0aE.dag/" + dll_suffix):
            return f, raw
    raise SystemExit("no rsp for " + dll_suffix)


def main():
    sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    rsp, raw = find_rsp("Assembly-CSharp-Editor.dll")
    quoted = [q for q, b in re.findall(r'"([^"]*)"|(\S+)', raw) if q]
    out_dll = next(q for q in quoted if q.endswith(".dll"))
    refs = [q for q in quoted if q.lower().endswith(".dll") and q != out_dll]
    defines = [q for q in quoted if re.fullmatch(r"[A-Z0-9_]+", q)]
    sources = sorted(glob.glob(os.path.join(PROJECT, r"Assets\Editor", "*.cs")))
    if len(sys.argv) > 1:                       # override dir: files there replace the same-named project sources
        over = {os.path.basename(f): f for f in glob.glob(os.path.join(sys.argv[1], "*.cs"))}
        sources = [over.get(os.path.basename(s), s) for s in sources]
        print("overriding with:", sorted(over))
    tmp = tempfile.mkdtemp(prefix="tc_editor_")
    out = os.path.join(tmp, "Assembly-CSharp-Editor.dll")
    rsp_path = os.path.join(tmp, "a.rsp")
    with open(rsp_path, "w", encoding="utf-8") as w:
        w.write("-target:library\n-nologo\n-langversion:9.0\n-nowarn:0169,0649,0414,0108,0109,8632,0219,0162,0114,0618\n")
        w.write('-out:"%s"\n' % out)
        for d in defines:
            w.write("-define:%s\n" % d)
        for r in refs:
            w.write('-r:"%s"\n' % (r if os.path.isabs(r) else os.path.join(PROJECT, r)))
        for s in sources:
            w.write('"%s"\n' % s)
    print("== compiling Assembly-CSharp-Editor: sources", len(sources), "refs", len(refs), "rsp", os.path.basename(rsp))
    p = subprocess.run([DOTNET, CSC, "@" + rsp_path], capture_output=True, text=True, cwd=PROJECT,
                       encoding="utf-8", errors="replace")
    errs = [l for l in p.stdout.splitlines() if "error CS" in l]
    for l in errs[:40]:
        print("  ", l)
    print("   exit", p.returncode, "errors", len(errs))
    return 0 if p.returncode == 0 else 1


if __name__ == "__main__":
    sys.exit(main())
