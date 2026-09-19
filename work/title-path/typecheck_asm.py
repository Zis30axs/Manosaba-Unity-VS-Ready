"""Offline Roslyn type-check of one reconstructed game assembly, with Unity's own compiler and the reference set Unity
recorded for that assembly (Library/Bee/artifacts/rsp, the 1900b0aE.dag Editor compile).  No Unity launch.

    python work/title-path/typecheck_asm.py GigaCreation.NaninovelExtender.ExtendedActors [override_dir ...] [--ref extra.dll ...] [--out draft.dll]

Sources are every .cs under Assets/Scripts/Game/<assembly>/.  Files in override_dir replace project files of the same
basename, so a draft can be checked before it is copied into Assets/ (useful while a Unity batch run owns the project).
Exit code 0 = no compiler errors.
"""
import glob, os, re, subprocess, sys, tempfile

PROJECT = r"E:\Manosaba-VSCodeReady\UnityProject"
UNITY = r"E:\Unity\6000.0.48f1"
DOTNET = os.path.join(UNITY, r"Editor\Data\NetCoreRuntime\dotnet.exe")
CSC = os.path.join(UNITY, r"Editor\Data\DotNetSdkRoslyn\csc.dll")
RSP_DIR = os.path.join(PROJECT, r"Library\Bee\artifacts\rsp")


def find_rsp(asm):
    for f in glob.glob(os.path.join(RSP_DIR, "*.rsp")):
        raw = open(f, encoding="utf-8", errors="replace").read()
        m = re.search(r'"([^"]*\.dll)"', raw)
        if m and m.group(1).replace("\\", "/").endswith("1900b0aE.dag/" + asm + ".dll"):
            return f, raw
    raise SystemExit("no Bee rsp for " + asm)


def main():
    sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    args = sys.argv[1:]
    extra_refs = []
    keep_out = None
    while "--out" in args:                   # copy the compiled dll here (so a later check can --ref a draft assembly)
        i = args.index("--out")
        keep_out = args[i + 1]
        del args[i:i + 2]
    while "--ref" in args:
        i = args.index("--ref")
        extra_refs.append(args[i + 1])
        del args[i:i + 2]
    asm = args[0]
    over = {}
    for d in args[1:]:                       # override dirs (searched recursively): same basename replaces the project file
        for f in glob.glob(os.path.join(d, "**", "*.cs"), recursive=True):
            over[os.path.basename(f)] = f
    rsp, raw = find_rsp(asm)
    quoted = [q for q, b in re.findall(r'"([^"]*)"|(\S+)', raw) if q]
    out_dll = next(q for q in quoted if q.endswith(".dll"))
    refs = [q for q in quoted if q.lower().endswith(".dll") and q != out_dll] + extra_refs
    defines = [q for q in quoted if re.fullmatch(r"[A-Z0-9_]+", q)]
    src_dir = os.path.join(PROJECT, "Assets", "Scripts", "Game", asm)
    sources = [os.path.join(dp, f) for dp, _, fs in os.walk(src_dir) for f in fs if f.endswith(".cs")]
    sources = [over.get(os.path.basename(s), s) for s in sources]
    tmp = tempfile.mkdtemp(prefix="tc_asm_")
    out = os.path.join(tmp, asm + ".dll")
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
    print("== compiling %s: %d sources (%d overridden), %d refs" % (asm, len(sources), len(over), len(refs)))
    p = subprocess.run([DOTNET, CSC, "@" + rsp_path], capture_output=True, text=True, cwd=PROJECT,
                       encoding="utf-8", errors="replace")
    errs = [l for l in p.stdout.splitlines() if "error CS" in l]
    for l in errs[:40]:
        print("  ", l[:300])
    print("   exit", p.returncode, "errors", len(errs))
    if keep_out and p.returncode == 0:
        import shutil
        shutil.copyfile(out, keep_out)
        print("   dll ->", keep_out)
    return 0 if p.returncode == 0 else 1


if __name__ == "__main__":
    sys.exit(main())
