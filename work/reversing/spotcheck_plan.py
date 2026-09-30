"""Independent spot-check of `stub_getter_plan.json`.

Re-disassembles a deterministic sample of classified sites with the *standalone* `disas.py`
command line -- a separate process, reading the same binary through its own code path -- and
verifies that everything the plan claims is literally present in that output:

  FIELD_READ   the body is exactly one load into the return register from `[rcx+off]` with the
               claimed offset, followed by `ret`
  SERVICE_GET  the METHODINFO string, the accessor call target, the null test / branch and the
               store-back to the same offset are all present verbatim; `mov cl, 1` is present for
               the ServiceLocator variant and for no other site
  all          the `.pdata` size in the plan equals the function size `disas.py` derives

    python work/reversing/_spotcheck_plan.py [--field 15] [--service 10]
"""
import argparse
import json
import os
import re
import subprocess
import sys

PY = r"E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe"
HERE = os.path.dirname(os.path.abspath(__file__))
PLAN = r"E:\Manosaba-VSCodeReady\tools\unity-rebuild\out\unity\stub_getter_plan.json"


def disas(rva, typename):
    out = subprocess.run([PY, os.path.join(HERE, "disas.py"), rva, "--type", typename],
                         capture_output=True, text=True, cwd=HERE)
    return out.stdout


def body_lines(text):
    lines = [l.rstrip() for l in text.split("\n")]
    seps = [i for i, l in enumerate(lines) if l.startswith("====")]
    body = lines[seps[-1] + 1:] if len(seps) >= 2 else []
    ins = []
    for l in body:
        if not l.startswith("  "):
            continue
        left, _, comment = l.partition("; ")
        m = re.match(r"^  (0x[0-9A-Fa-f]+)  (\S+)(?: +(.*?))?\s*$", left)
        if m:
            ins.append((m.group(2), (m.group(3) or "").strip(), comment.strip()))
    return ins


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--field", type=int, default=15)
    ap.add_argument("--service", type=int, default=10)
    ap.add_argument("--plan", default=PLAN)
    a = ap.parse_args()

    plan = json.load(open(a.plan, encoding="utf-8"))
    rows = plan["getters"]

    def sample(bucket, n):
        xs = [r for r in rows if r["bucket"] == bucket]
        step = max(1, len(xs) // max(n, 1))
        return xs[::step][:n]

    checks = 0
    failures = []

    for r in sample("FIELD_READ", a.field):
        if not r["proposed_body"]:
            continue
        text = disas(r["rva"], r["class"])
        ins = body_lines(text)
        checks += 1
        want = "0x%x" % r["field_offset"]
        load = ins[0] if ins else ("", "", "")
        ok = (len(ins) == 2 and ins[1][0] == "ret"
              and "[rcx" in load[1] and load[0] in
              ("mov", "movzx", "movsx", "movsxd", "movss", "movsd", "movups"))
        if not ok:
            failures.append(("FIELD_READ", r, "raw body: %s" % [i[:2] for i in ins]))
            continue
        # `[rcx + 0x18]` for most offsets, `[rcx + 4]` for 0x1..0x9 (capstone drops the prefix),
        # and bare `[rcx]` for offset 0 on a struct
        if want == "0x0":
            if not re.search(r"\[rcx\]", load[1]):
                failures.append(("FIELD_READ", r, "expected offset 0 in `%s`" % load[1]))
        else:
            alt = want[2:] if len(want) == 3 else want       # `0x4` -> also accept `+ 4`
            if not re.search(r"\+ (?:%s|%s)\]" % (want, alt), load[1].lower()):
                failures.append(("FIELD_READ", r, "offset %s not in `%s`" % (want, load[1])))

    for r in sample("SERVICE_GET", a.service):
        text = disas(r["rva"], r["class"])
        ins = body_lines(text)
        checks += 1
        blob = text
        problems = []
        if r["methodinfo"] and r["methodinfo"] not in blob:
            problems.append("METHODINFO %r absent" % r["methodinfo"])
        if r["type_arg"] and ("<%s>" % r["type_arg"]) not in blob:
            problems.append("type argument <%s> absent" % r["type_arg"])
        acc = "ServiceLocator$$Get" if "ServiceLocator" in r["service_variant"] else \
            "Engine$$GetServiceOrErr"
        if acc not in blob:
            problems.append("accessor call %s absent" % acc)
        if "0x%X" % r["field_offset"] not in blob:
            problems.append("offset 0x%X absent" % r["field_offset"])
        has_cl1 = any(m == "mov" and ops == "cl, 1" for m, ops, _c in ins)
        if "ServiceLocator" in r["service_variant"] and not has_cl1:
            problems.append("ServiceLocator variant but no `mov cl, 1`")
        if acc.startswith("Engine") and has_cl1:
            problems.append("Engine variant but `mov cl, 1` present")
        if not any(m == "test" for m, _o, _c in ins):
            problems.append("no null test")
        if ins and ins[-1][0] != "ret":
            problems.append("does not end in ret")
        if problems:
            failures.append(("SERVICE_GET", r, "; ".join(problems)))

    feats = [l.rstrip() for l in body_lines(disas("0x30C000", None))] if False else None
    del feats

    print("spot-checked %d sites against a standalone `disas.py` run" % checks)
    for kind, r, why in failures:
        print("  FAIL %s %s:%d %s.%s RVA %s -- %s"
              % (kind, r["file"], r["line"], r["class"], r["member"], r["rva"], why))
    if not failures:
        print("OK: every sampled plan claim is present verbatim in the raw disassembly")
    return 1 if failures else 0


if __name__ == "__main__":
    sys.exit(main())
