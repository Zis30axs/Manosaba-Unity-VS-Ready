"""Negative control: prove the csc harness really reports errors in this reference set."""
import os
import subprocess
import sys
import tempfile

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))
import typecheck_editor_scripts as tc

tmp = tempfile.mkdtemp(prefix="manosaba-csc-neg-")
broken = os.path.join(tmp, "Broken.cs")
open(broken, "w", encoding="utf-8").write(
    "namespace Manosaba.EditorTools { public static class Broken { public static void M() { "
    "ThisTypeDoesNotExist x = null; } } }\n")

raw = open(tc.RSP, encoding="utf-8", errors="replace").read()
quoted = [q for q, bare in tc.tokens(raw) if q]
refs = [q for q in quoted if q.lower().endswith(".dll")
        and not q.endswith("Assembly-CSharp-Editor.dll")]
lines = ["-nologo", "-nostdlib+", "-target:library", "-langversion:9.0", "-out:" + os.path.join(tmp, "neg.dll")]
lines += ['-r:"%s"' % (r if os.path.isabs(r) else os.path.join(tc.PROJECT, r)) for r in refs]
lines += ['"%s"' % broken]
rsp = os.path.join(tmp, "neg.rsp")
open(rsp, "w", encoding="utf-8").write("\n".join(lines))
proc = subprocess.run([tc.DOTNET, tc.CSC, "@" + rsp], cwd=tc.PROJECT, capture_output=True,
                      text=True, encoding="utf-8", errors="replace")
out = (proc.stdout or "") + (proc.stderr or "")
print("exit code:", proc.returncode)
print(out.strip())
print("harness reports the injected error:", "CS0246" in out and "Broken.cs" in out)
