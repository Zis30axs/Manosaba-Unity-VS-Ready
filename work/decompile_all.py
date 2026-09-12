#!/usr/bin/env python3
"""Decompile Il2CppDumper dummy assemblies into readable C# projects -> src/decompiled/"""
import os, sys, subprocess, time, argparse, concurrent.futures as cf

DUMMY = r"E:\manosaba_re\src\il2cpp\DummyDll"
SRC = r"E:\manosaba_re\src"
ILSPY = os.path.join(os.environ["USERPROFILE"], ".dotnet", "tools", "ilspycmd.exe")

GAME_PREFIXES = ("Assembly-CSharp", "CustomInspector", "Elringus.", "GigaCreation.", "Naninovel",
                 "ppt-noise", "__Generated", "Il2CppDummyDll")
SKIP_ALWAYS = ("mscorlib", "System", "Mono.Security", "Microsoft", "netstandard", "UnityEngine",
               "Unity.", "Newtonsoft.Json", "UniTask", "UniRx", "LitMotion", "FastEnum",
               "LeTai.TrueShadow", "MPUIKit", "UIPolygon", "com.rlabrecque.steamworks.net")


def classify(name):
    stem = os.path.splitext(name)[0]
    if any(stem == p or stem.startswith(p) for p in GAME_PREFIXES):
        return "game"
    if any(stem == p or stem.startswith(p) for p in SKIP_ALWAYS):
        return "external"
    return "other"


def run_one(args):
    dll, outdir = args
    os.makedirs(outdir, exist_ok=True)
    env = dict(os.environ)
    env["DOTNET_ROLL_FORWARD"] = "LatestMajor"
    t0 = time.time()
    p = subprocess.run([ILSPY, "-p", "-o", outdir, dll], capture_output=True, text=True, env=env)
    dt = time.time() - t0
    ok = p.returncode == 0
    n = sum(len(f) for _, _, f in os.walk(outdir))
    return (os.path.basename(dll), ok, round(dt, 1), n, (p.stderr or "")[-300:])


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--group", default="game", choices=["game", "other", "external", "all"])
    ap.add_argument("--workers", type=int, default=3)
    a = ap.parse_args()

    dlls = sorted(f for f in os.listdir(DUMMY) if f.lower().endswith(".dll"))
    jobs = []
    for d in dlls:
        g = classify(d)
        if a.group != "all" and g != a.group:
            continue
        jobs.append((os.path.join(DUMMY, d), os.path.join(SRC, "decompiled" if g != "external" else "decompiled_external",
                                                          os.path.splitext(d)[0])))
    print(f"decompiling {len(jobs)} assemblies (group={a.group}, workers={a.workers})", flush=True)
    t0 = time.time()
    done = 0
    with cf.ThreadPoolExecutor(max_workers=a.workers) as ex:
        for name, ok, dt, n, err in ex.map(run_one, jobs):
            done += 1
            print(f"[{done}/{len(jobs)}] {name} ok={ok} {dt}s files={n} {err[:120]}", flush=True)
    print(f"ALL DONE in {time.time()-t0:.0f}s", flush=True)


if __name__ == "__main__":
    main()
