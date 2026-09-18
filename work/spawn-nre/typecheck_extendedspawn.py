"""Type-check the reconstructed ExtendedSpawn / Cameras sources with Unity's own Roslyn, using the
reference sets Unity recorded for the Editor (1900b0aE) compile of those assemblies.

Cameras is compiled first into a temp dll, and that dll replaces the stale Cameras reference when
ExtendedSpawn is compiled (ExtendedSpawn now references Cameras).
"""
import os, re, subprocess, sys, tempfile, glob

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

def tokens(text):
    return re.findall(r'"([^"]*)"|(\S+)', text)

def compile_asm(dll_suffix, src_dir, extra_ref=None, drop_ref_suffix=None, extra_refs=()):
    rsp, raw = find_rsp(dll_suffix)
    quoted = [q for q, b in tokens(raw) if q]
    out_dll = next(q for q in quoted if q.endswith(".dll"))
    refs = [q for q in quoted if q.lower().endswith(".dll") and q != out_dll]
    if drop_ref_suffix:
        refs = [r for r in refs if not r.replace("\\", "/").endswith(drop_ref_suffix)]
    if extra_ref:
        refs.append(extra_ref)
    refs.extend(extra_refs)
    defines = [q for q in quoted if re.fullmatch(r"[A-Z0-9_]+", q)]
    sources = [os.path.join(dp, f) for dp, _, fs in os.walk(src_dir) for f in fs if f.endswith(".cs")]
    tmp = tempfile.mkdtemp(prefix="tc_")
    out = os.path.join(tmp, os.path.basename(dll_suffix))
    rsp_path = os.path.join(tmp, "a.rsp")
    with open(rsp_path, "w", encoding="utf-8") as w:
        w.write("-target:library\n-nologo\n-langversion:9.0\n-nowarn:0169,0649,0414,0108,0109,8632,0219,0162,0114\n")
        w.write('-out:"%s"\n' % out)
        for d in defines: w.write('-define:%s\n' % d)
        for r in refs: w.write('-r:"%s"\n' % os.path.join(PROJECT, r) if not os.path.isabs(r) else '-r:"%s"\n' % r)
        for s in sources: w.write('"%s"\n' % s)
    print("== compiling", dll_suffix, "sources:", len(sources), "refs:", len(refs), "rsp:", os.path.basename(rsp))
    p = subprocess.run([DOTNET, CSC, "@" + rsp_path], capture_output=True, text=True, cwd=PROJECT)
    errs = [l for l in p.stdout.splitlines() if "error CS" in l]
    for l in errs[:40]: print("  ", l)
    print("   exit", p.returncode, "errors", len(errs))
    return out if p.returncode == 0 else None

cam = compile_asm("GigaCreation.NaninovelExtender.Cameras.dll",
                  os.path.join(PROJECT, r"Assets\Scripts\Game\GigaCreation.NaninovelExtender.Cameras"))
if not cam: sys.exit(1)
es = compile_asm("GigaCreation.NaninovelExtender.ExtendedSpawn.dll",
                 os.path.join(PROJECT, r"Assets\Scripts\Game\GigaCreation.NaninovelExtender.ExtendedSpawn"),
                 extra_ref=cam, drop_ref_suffix="GigaCreation.NaninovelExtender.Cameras.dll",
                 extra_refs=[os.path.join(PROJECT, r"Packages\com.elringus.naninovel\Plugins\Naninovel.Common.dll")])
sys.exit(0 if es else 1)
