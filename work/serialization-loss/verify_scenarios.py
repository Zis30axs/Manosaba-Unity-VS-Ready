"""Independent verification of the scenario regeneration (Phase 1 of the serialization-loss plan).

Nothing here trusts the C# converter's own counters: the expected number of assigned Naninovel parameters
per script is recomputed from the raw dump (expanding {rid} references at every use, as the applier does) and
compared with the number of `hasValue: 1` lines actually present in the .asset YAML on disk.  GUIDs are
compared with the snapshot taken before the run.

    python work/serialization-loss/verify_scenarios.py [result.json]
"""
import json, re, sys
from collections import Counter

REPO = "E:/Manosaba-VSCodeReady/"
RESULT = sys.argv[1] if len(sys.argv) > 1 else REPO + "tools/unity-rebuild/out/unity/so_convert_result_scenarios.json"
PLAN = REPO + "tools/unity-rebuild/out/unity/so_convert_plan_scenarios.json"
SNAP = REPO + "work/serialization-loss/guid_snapshot_before.json"


def expected(dump):
    refs = {r["rid"]: r for r in dump.get("references", {}).get("RefIds", []) if "rid" in r}

    def walk(t, depth=0):
        if depth > 64:
            raise RuntimeError("nesting > 64")
        n = 0
        if isinstance(t, dict):
            rid = t.get("rid")
            if isinstance(rid, int) and not isinstance(rid, bool):
                r = refs.get(rid)
                return walk(r["data"], depth + 1) if r and r.get("data") is not None else 0
            for k, v in t.items():
                if k == "hasValue":
                    if v in (1, True):
                        n += 1
                else:
                    n += walk(v, depth)
        elif isinstance(t, list):
            for x in t:
                n += walk(x, depth)
        return n

    return sum(walk(v) for k, v in dump.items() if k != "references" and not k.startswith("m_"))


def main():
    res = json.load(open(RESULT, encoding="utf-8"))
    plan = json.load(open(PLAN, encoding="utf-8"))
    snap = json.load(open(SNAP, encoding="utf-8"))
    print("converter status:", res["status"], {k: res[k] for k in ("jobs_total", "converted", "converted_partial", "failed",
                                                                  "params_with_value_dump", "params_with_value_written")})
    st = Counter(j["status"] for j in res["jobs"])
    print("job statuses:", dict(st))

    bad = []
    tot_exp = tot_yaml = 0
    guid_diff = []
    spot_empty = 0
    for job in plan["jobs"]:
        target = job["target"].replace(chr(92), "/")
        dump = json.load(open(REPO + job["dump"], encoding="utf-8-sig"))
        exp = expected(dump)
        text = open(target, encoding="utf-8").read()
        got = text.count("hasValue: 1")
        tot_exp += exp
        tot_yaml += got
        if exp != got:
            bad.append((job["asset_path"], exp, got))
        # every command's playbackSpot.scriptPath must be filled whenever the dump has a non-empty one
        if '"scriptPath": "' in json.dumps(dump).replace('"scriptPath": ""', "") and "scriptPath: \n" in text and \
                text.count("scriptPath: \n") == text.count("scriptPath:") and text.count("scriptPath:") > 0:
            spot_empty += 1
        meta = target + ".meta"
        key = meta.replace(REPO, "")
        g = re.search(r"guid: ([0-9a-f]{32})", open(meta, encoding="utf-8").read()).group(1)
        if snap.get(key) != g:
            guid_diff.append((key, snap.get(key), g))

    print("assets checked:", len(plan["jobs"]))
    print("expected hasValue:1 (from dumps): %d   present in YAML: %d   assets differing: %d" % (tot_exp, tot_yaml, len(bad)))
    for b in bad[:10]:
        print("   MISMATCH", b)
    print("assets whose every scriptPath is still empty although the dump has one:", spot_empty)
    print("GUID changes vs snapshot: %d (snapshot has %d scenario entries)" % (
        len(guid_diff), sum(1 for k in snap if "/Scenarios/" in k)))
    for g in guid_diff[:10]:
        print("   GUID", g)
    ok = (not bad and not guid_diff and res["failed"] == 0 and res["converted_partial"] == 0 and spot_empty == 0
          and tot_yaml > 0)
    print("RESULT:", "PASS" if ok else "FAIL")
    return 0 if ok else 1


if __name__ == "__main__":
    sys.exit(main())
