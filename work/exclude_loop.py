#!/usr/bin/env python3
"""Build-fix loop by exclusion: remove the files the compiler cannot digest from the
compilation (they stay on disk) until the solution builds clean."""
import os, re, json, subprocess, collections

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
PROJ = os.path.join(ROOT, "project")
EXCL = os.path.join(ROOT, "work", "excluded.json")
ERR = re.compile(r"^(.*?\.cs)\((\d+),(\d+)\): error (CS\d+): (.*?)(?: \[.*)?$", re.M)


def build():
    env = dict(os.environ, NUGET_PACKAGES=os.path.join(ROOT, "tools", "nuget"))
    p = subprocess.run(["dotnet", "build", "Manosaba.sln", "-v", "q", "--nologo"], cwd=PROJ,
                       capture_output=True, text=True, encoding="utf-8", errors="replace", env=env)
    return p.stdout + p.stderr


def gen():
    subprocess.run(["python", os.path.join(ROOT, "work", "make_ide_project4.py")],
                   capture_output=True, text=True)


def main():
    excluded = json.load(open(EXCL, encoding="utf-8")) if os.path.exists(EXCL) else []
    for it in range(1, 15):
        out = build()
        errs = ERR.findall(out)
        codes = collections.Counter(e[3] for e in errs)
        print(f"--- round {it}: {len(errs)} errors {dict(codes)} excluded={len(excluded)}", flush=True)
        if not errs:
            print("*** BUILD CLEAN ***")
            break
        files = sorted({os.path.relpath(f, DEC).replace(os.sep, "/") for f, _, _, _, _ in errs})
        new = [f for f in files if f not in excluded]
        print("    new files to exclude:", len(new), flush=True)
        if not new:
            print("    nothing new")
            break
        excluded.extend(new)
        json.dump(excluded, open(EXCL, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
        gen()
    json.dump(excluded, open(EXCL, "w", encoding="utf-8"), ensure_ascii=False, indent=1)


if __name__ == "__main__":
    main()
