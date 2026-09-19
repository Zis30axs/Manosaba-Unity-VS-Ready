"""Summarize a G4b (Boot -> Title) Unity log against the symptoms this work is meant to remove.

    python work/title-path/analyze_g4b.py <unity.log> [evidence.json]

Reports the pattern counts the serialization-loss audit started from (LocalizeImage NREs, the empty `@processInput`
warning, AnimateActor NRE, the Blur "Can't find" error), every distinct exception type with its first stack frame, and the
evidence JSON's playback fields (script path, spot, transitions).  Baseline numbers from the 2026-09-18 23:36 run
(work/spawn-nre/game_start_fixed2.log): NullReferenceException 2955, LocalizeImage 914, processInput 7, "No parameters" 7,
"Can't find" 7, AnimateActor 2, playback spot " #1.0".
"""
import collections, json, re, sys


def read(path):
    b = open(path, "rb").read()
    if b[:2] in (b"\xff\xfe", b"\xfe\xff") or b.count(b"\x00") > len(b) // 4:
        return b.decode("utf-16", errors="replace")
    return b.decode("utf-8", errors="replace")


def main():
    text = read(sys.argv[1])
    lines = text.splitlines()
    print("log:", sys.argv[1], "lines:", len(lines))
    pats = [("NullReferenceException", r"NullReferenceException"), ("LocalizeImage", r"LocalizeImage"),
            ("@processInput warning", r"processInput"), ("'No parameters were specified'", r"No parameters were specified"),
            ("Blur can't find actor", r"Can't find '.*' actor|Can't find a manager"),
            ("AnimateActor", r"AnimateActor"), ("ArgumentNullException", r"ArgumentNullException"),
            ("any '... Exception:' line", r"\b\w+Exception\b:")]
    for name, p in pats:
        print("  %-34s %d" % (name, len(re.findall(p, text))))

    excs = collections.OrderedDict()
    for i, line in enumerate(lines):
        m = re.match(r"\s*([\w.]+Exception)\b(?::| )", line)
        if not m:
            continue
        frame = ""
        for j in range(i + 1, min(i + 8, len(lines))):
            if re.match(r"\s*(at |in |\w[\w.]+\.\w+ ?\()", lines[j]):
                frame = lines[j].strip()
                break
        excs.setdefault((m.group(1), frame[:150]), 0)
        excs[(m.group(1), frame[:150])] += 1
    print("distinct exception (type, first frame):", len(excs))
    for (t, f), n in sorted(excs.items(), key=lambda kv: -kv[1])[:15]:
        print("  %5d  %s  %s" % (n, t, f))

    ev = sys.argv[2] if len(sys.argv) > 2 else None
    if ev:
        d = json.load(open(ev, encoding="utf-8-sig"))
        for k in ("status", "auto_advance", "naninovel_played_script", "naninovel_playback_spot", "naninovel_total_commands",
                  "naninovel_playing", "auto_advance_transitions", "log_errors"):
            print("  evidence %-26s %s" % (k, d.get(k)))


if __name__ == "__main__":
    main()
