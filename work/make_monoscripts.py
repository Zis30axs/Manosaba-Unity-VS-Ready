"""Build res/_monoscripts.csv : MonoScript path_id -> class / namespace / assembly."""
import json, os, csv

RES = r"E:\manosaba_re\res"
MAN = r"E:\manosaba_re\work\manifest_core.jsonl"

rows = []
with open(MAN, encoding="utf-8") as fh:
    for line in fh:
        line = line.strip()
        if not line:
            continue
        e = json.loads(line)
        if e["type"] != "MonoScript":
            continue
        p = os.path.join(RES, e["out"].replace("/", os.sep))
        try:
            j = json.load(open(p, encoding="utf-8"))
        except Exception:
            continue
        rows.append({
            "path_id": e["path_id"],
            "class": j.get("m_ClassName", ""),
            "namespace": j.get("m_Namespace", ""),
            "assembly": j.get("m_AssemblyName", ""),
            "script_path": j.get("m_FileID_0_Path", "") or "",
            "out": e["out"],
        })

seen = set()
uniq = []
for r in rows:
    k = (r["assembly"], r["namespace"], r["class"])
    if k in seen:
        continue
    seen.add(k)
    uniq.append(r)

with open(os.path.join(RES, "_monoscripts.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.DictWriter(fh, fieldnames=["class", "namespace", "assembly", "path_id", "out"])
    w.writeheader()
    for r in sorted(uniq, key=lambda x: (x["assembly"], x["namespace"], x["class"])):
        w.writerow({k: r[k] for k in w.fieldnames})
print("monoscripts", len(rows), "unique", len(uniq))
