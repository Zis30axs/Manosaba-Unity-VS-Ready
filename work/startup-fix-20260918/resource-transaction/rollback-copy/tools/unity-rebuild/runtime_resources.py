"""Run the real Unity resource recovery/check methods with observed exits and log files.

MANOSABA_UNITY_EDITOR must point to a licensed Unity Editor. Inputs are the existing
local res/ font dumps and imported GameContent OTFs. No network or font downloads.
"""
import argparse
import json
import os
from pathlib import Path
import subprocess
import sys
import time

METHODS = {"verify": "RuntimeResourceRepair.Verify", "repair": "RuntimeResourceRepair.Run",
           "verify-built": "RuntimeResourceRepair.VerifyBuilt", "build": "AddressablesBuild.Run"}

def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("phase", choices=METHODS)
    parser.add_argument("--graphics", action="store_true", help="Use the graphics device (needed for visual verification)")
    parser.add_argument("--tag", default="resources")
    args = parser.parse_args()
    if not args.tag.replace("-", "").replace("_", "").isalnum():
        parser.error("tag must contain only letters, numbers, hyphen or underscore")
    root = Path(__file__).resolve().parents[2]
    exe = Path(os.environ.get("MANOSABA_UNITY_EDITOR", ""))
    if not exe.is_file():
        parser.error("set MANOSABA_UNITY_EDITOR to Unity.exe")
    out = root / "tools/unity-rebuild/out/runtime-resources"
    out.mkdir(parents=True, exist_ok=True)
    stem = args.tag + "-" + args.phase
    report = (root / "tools/unity-rebuild/out/unity/addressables_build_result.json"
              if args.phase == "build" else out / (stem + ".json"))
    # Reusing a tag cannot turn a stale prior report into this run's result.
    if report.exists():
        report.rename(out / (stem + ".previous-" + str(time.time_ns()) + ".json"))
    env = dict(os.environ, MANOSABA_RESOURCE_REPORT=str(report))
    cmd = [str(exe), "-batchmode", "-quit", "-projectPath", str(root / "UnityProject"),
           "-executeMethod", "Manosaba.EditorTools." + METHODS[args.phase], "-logFile", str(out / (stem + ".log"))]
    if not args.graphics:
        cmd.append("-nographics")
    started = time.time()
    print("COMMAND " + json.dumps(cmd), flush=True)
    proc = subprocess.run(cmd, cwd=root, env=env, capture_output=True)
    (out / (stem + ".stdout")).write_bytes(proc.stdout)
    (out / (stem + ".stderr")).write_bytes(proc.stderr)
    record = {"command": cmd, "exit": proc.returncode, "seconds": round(time.time()-started, 2),
              "report_exists": report.is_file(), "report": str(report)}
    (out / (stem + ".execution.json")).write_text(json.dumps(record, indent=2), encoding="utf-8")
    print(json.dumps(record), flush=True)
    if proc.returncode == 0 and not report.is_file():
        print("FAIL: Unity returned success without the required fresh report", file=sys.stderr)
        return 2
    return proc.returncode

if __name__ == "__main__":
    raise SystemExit(main())
