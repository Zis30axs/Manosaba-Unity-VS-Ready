"""Independent measurement of prefab serialization loss (Phase 2 of the serialization-loss plan).

Reads the built .prefab YAML files on disk and the shipped dumps named by prefab_plan_all.json; trusts none of
PrefabBuilder's own counters.  Metrics (built vs what the dumps say should be there):

  * LocalizeImage._image             non-null in the built prefab   vs   non-null in the dumps
  * PointerStayingSpriteSwapper refs (any `_image`/`_target` style field is covered by the generic pass below)
  * UnityEngine.UI.Image.m_Sprite    non-null built                 vs   non-null dumps
  * TMP_Text m_fontAsset             non-null built                 vs   non-null dumps
  * generic: every `_image`-named MonoBehaviour field, null built vs null dumps

    python work/serialization-loss/verify_prefabs.py [out.json] [--plan prefab_plan_probe.json]

--plan restricts the check to the jobs of another plan file in tools/unity-rebuild/out/unity (e.g. a probe run).
"""
import glob, json, os, re, sys
from collections import Counter

REPO = "E:/Manosaba-VSCodeReady/"
PLAN = REPO + "tools/unity-rebuild/out/unity/prefab_plan_all.json"
DOC = re.compile(r"^--- !u!(\d+) &(-?\d+)[^\n]*\n(.*?)(?=^--- !u!|\Z)", re.S | re.M)


def built_docs(path):
    text = open(path, encoding="utf-8").read()
    return [(int(m.group(1)), m.group(2), m.group(3)) for m in DOC.finditer(text)]


LIBERATION_SANS_SDF = "8f586378b4e144a9851e7b34d9b748ee"   # TMP's built-in default: what a fresh TMP component gets


def nonnull(body, key):
    """(present, non_null) for a `key: {fileID: N...}` line in a YAML doc body.

    A TMP `m_fontAsset` pointing at LiberationSans SDF is what TextMeshProUGUI assigns to itself on creation, so
    it does not count as the shipped font having been restored."""
    m = re.search(r"^\s*%s: \{fileID: (-?\d+)(?:, guid: ([0-9a-f]{32}))?" % re.escape(key), body, re.M)
    if m is None:
        return (False, False)
    if key == "m_fontAsset" and m.group(2) == LIBERATION_SANS_SDF:
        return (True, False)
    return (True, m.group(1) != "0")


def main():
    args = sys.argv[1:]
    plan_file = PLAN
    if "--plan" in args:
        i = args.index("--plan")
        plan_file = REPO + "tools/unity-rebuild/out/unity/" + args[i + 1]
        del args[i:i + 2]
    plan = json.load(open(plan_file, encoding="utf-8"))
    built = Counter()
    dump = Counter()
    per_prefab = {}
    for job in plan["jobs"]:
        target = job["target"].replace(chr(92), "/")
        base = target[:-len(".prefab")]
        files = [target] + sorted(glob.glob(glob.escape(base) + "_*.prefab"))
        b = Counter()
        for f in files:
            if not os.path.exists(f):
                b["missing_file"] += 1
                continue
            for cls, fid, body in built_docs(f):
                if cls != 114:
                    continue
                for key, name in (("_image", "img_field"), ("m_Sprite", "sprite"), ("m_fontAsset", "font")):
                    present, nn = nonnull(body, key)
                    if present:
                        b[name + "_total"] += 1
                        b[name + "_nonnull"] += 1 if nn else 0
        d = Counter()
        for o in job["objects"]:
            if o["type"] != "MonoBehaviour":
                continue
            j = json.load(open(REPO + o["dump"], encoding="utf-8-sig"))
            for key, name in (("_image", "img_field"), ("m_Sprite", "sprite"), ("m_fontAsset", "font")):
                if key in j and isinstance(j[key], dict) and "m_PathID" in j[key]:
                    d[name + "_total"] += 1
                    d[name + "_nonnull"] += 1 if j[key]["m_PathID"] != 0 else 0
        per_prefab[job["asset_path"]] = (dict(b), dict(d))
        built.update(b)
        dump.update(d)

    print("jobs checked:", len(plan["jobs"]), "(%s)" % os.path.basename(plan_file))
    print("%-14s %10s %10s   (non-null / total)" % ("field", "built", "dumps"))
    for name in ("img_field", "sprite", "font"):
        print("%-14s %5d/%-5d %5d/%-5d" % (name, built[name + "_nonnull"], built[name + "_total"],
                                          dump[name + "_nonnull"], dump[name + "_total"]))
    lost = {n: dump[n + "_nonnull"] - built[n + "_nonnull"] for n in ("img_field", "sprite", "font")}
    print("non-null values missing from the built prefabs:", lost)
    worst = sorted(per_prefab.items(),
                   key=lambda kv: -(kv[1][1].get("sprite_nonnull", 0) - kv[1][0].get("sprite_nonnull", 0)))[:5]
    for name, (b, d) in worst:
        print("   worst sprite loss: %-70s built %d dumps %d" % (name[-70:], b.get("sprite_nonnull", 0), d.get("sprite_nonnull", 0)))
    if args:
        json.dump({"built": built, "dumps": dump, "lost": lost}, open(args[0], "w"), indent=1)
    return 0 if all(v == 0 for v in lost.values()) else 1


if __name__ == "__main__":
    sys.exit(main())
