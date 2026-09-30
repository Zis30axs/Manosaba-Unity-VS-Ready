"""Verification harness: replay the classifier over the *already hand-recovered* getters.

The reconstructed sources carry `// RECONSTRUCTED | Confidence: A | RVA: 0x..., N B (...)` comments
written by earlier, manual recovery passes.  Where such a comment names the body shape, it is an
independent golden set: this script re-derives the shape from the native binary with the exact
machinery `classify_stub_getters.py` uses, and a disagreement is a bug in the classifier.

Only comments that actually *claim* one of the two shapes the tool detects are counted as claims --
a comment like "plain field store, shared setter body" describes a setter and makes no getter claim,
so it is skipped rather than being scored.

    python work/reversing/_verify_classifier.py
"""
import os
import re
import sys
import collections

sys.path.insert(0, r"E:\Manosaba-VSCodeReady\tools\unity-rebuild")
sys.path.insert(0, r"E:\Manosaba-VSCodeReady\work\reversing")

import classify_stub_getters as M  # noqa: E402

GAME = os.path.join(M.REPO, "UnityProject", "Assets", "Scripts", "Game")
REC = re.compile(
    r"RECONSTRUCTED\s*\|\s*Confidence:\s*\w+\s*\|\s*RVA:\s*(0x[0-9A-Fa-f]+),\s*(\d+)\s*B"
    r"\s*\((?P<body>[^)]*)\)")
FIELD_READ_CLAIM = re.compile(r"`mov \w+,\[rcx\+0x[0-9a-f]+\]; ret`")

## find every documented RVA + documented shape
docs = []
for root, dirs, names in os.walk(GAME):
    dirs.sort()
    for name in sorted(names):
        if not name.endswith(".cs"):
            continue
        p = os.path.join(root, name)
        with open(p, encoding="utf-8-sig", errors="replace") as fh:
            for i, line in enumerate(fh, 1):
                m = REC.search(line)
                if m:
                    docs.append({"file": os.path.relpath(p, M.REPO), "line": i,
                                 "rva": int(m.group(1), 16), "bytes": int(m.group(2)),
                                 "doc": m.group("body").strip()})
print("documented RECONSTRUCTED getters: %d" % len(docs))

dis = M.Disassembler()
clf = M.Classifier(dis)


def site_for(rec):
    """A Site with just enough context: the RVA and the field table of the declaring class."""
    # take the class declaration that owns the comment: nearest preceding `class X` in the file
    with open(os.path.join(M.REPO, rec["file"]), encoding="utf-8-sig", errors="replace") as fh:
        raw = fh.read()
    clean = M.blank_literals(raw)
    off = 0
    for ln in raw.split("\n")[:rec["line"] - 1]:
        off += len(ln) + 1
    scopes = M.find_scopes(clean)
    owner = None
    for s in scopes:
        if s["o"] < off < s["c"] and M.scope_kind(s["header"]) == "type":
            if owner is None or s["o"] > owner["o"]:
                owner = s
    fields, cname, ckind = {}, None, None
    if owner is not None:
        m = M._TYPE_DECL.search(M._nodesc(owner["header"]))
        cname, ckind = m.group(2), m.group(1)
        fields = M.parse_fields_of_type(raw, owner["o"] + 1, owner["c"])
    # the property declaration sits just above the RECONSTRUCTED comment
    decl = None
    lines = raw.split("\n")
    for k in range(rec["line"] - 1, max(-1, rec["line"] - 12), -1):
        t = lines[k].strip()
        if re.match(r"^(public|private|protected|internal|static|virtual|override)", t) and "(" not in t:
            decl = t
            break
    prop = None
    if decl:
        mm = re.match(r"^(?:(?:%s)\s+)*(?P<type>.+?)\s+(?P<name>[A-Za-z_@][\w@]*)$"
                      % M._MODIFIERS, decl)
        if mm:
            prop = (mm.group("type"), mm.group("name"))
    site = M.Site(kind="getter", file_rel=rec["file"], file_abs="", line=rec["line"],
                  class_full=cname, class_short=cname, class_kind=ckind,
                  member=prop[1] if prop else "?", member_kind="property-getter",
                  declared_type=prop[0] if prop else None, is_static=False,
                  current_body=rec["doc"], rva=rec["rva"], rva_source="doc-comment",
                  token=None, fields=fields)
    return site


agree = disagree = skipped = 0
bucket = collections.Counter()
bad = []
for rec in docs:
    doc = rec["doc"]
    site = site_for(rec)
    v = clf.classify(site)
    bucket[v["bucket"]] += 1
    want = None
    if "??=" in doc and "ServiceLocator" in doc:
        want = "SERVICE_GET/ServiceLocator"
    elif "??=" in doc and "GetServiceOrErr" in doc:
        want = "SERVICE_GET/Engine"
    elif FIELD_READ_CLAIM.search(doc):
        want = "FIELD_READ"
    if want is None:
        skipped += 1
        continue
    got = v["bucket"]
    if got == "SERVICE_GET":
        got += "/ServiceLocator" if "ServiceLocator" in (v["service_variant"] or "") else "/Engine"
    if got == want:
        agree += 1
    else:
        disagree += 1
        bad.append((rec, want, got, v))

print("buckets produced over the golden set:", dict(bucket))
print("agree %d   disagree %d   not-a-shape-claim %d" % (agree, disagree, skipped))
for rec, want, got, v in bad[:25]:
    print("  MISMATCH %s:%d RVA %s  doc=%r  want=%s got=%s  reason=%s"
          % (rec["file"], rec["line"], hex(rec["rva"]), rec["doc"][:60], want, got, v["reason"]))
    print("     evidence: %s" % v["evidence"][:200])
sys.exit(1 if disagree else 0)
