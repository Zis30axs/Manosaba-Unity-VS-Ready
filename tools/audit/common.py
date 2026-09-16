"""Shared helpers for the Unity project reconstruction audit."""
import json, os, re, sys, csv, struct, collections

REPO = r"E:\Manosaba-VSCodeReady"          # main checkout (holds ignored res/ + tools/)
WT   = r"E:\Manosaba-DS"                   # this audit worktree
RES  = os.path.join(REPO, "res")
GAME = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game"
DATA = os.path.join(GAME, "manosaba_Data")
AA   = os.path.join(DATA, r"StreamingAssets\aa")
AAW  = os.path.join(AA, "StandaloneWindows64")
OUT  = os.path.join(WT, "tools", "audit", "out")
os.makedirs(OUT, exist_ok=True)

UNITY_YAML_MAGIC = b"%YAML 1.1"
UNITY_YAML_TAG   = b"tag:unity3d.com,2011"
BUNDLE_MAGICS = (b"UnityFS", b"UnityWeb", b"UnityRaw", b"RawBundle")
ADDR_MAGIC = 0x0DE38942

def out_path(name):
    return os.path.join(OUT, name)

def dump(name, obj):
    p = out_path(name)
    with open(p, "w", encoding="utf-8") as fh:
        json.dump(obj, fh, ensure_ascii=False, indent=1)
    return p

def load_index():
    """res/_index.csv -> list of dict rows (type,name,asset_path,output_file,source_bundle,path_id)."""
    rows = []
    with open(os.path.join(RES, "_index.csv"), encoding="utf-8-sig", newline="") as fh:
        for r in csv.DictReader(fh):
            r["pid"] = int(r["path_id"]) if r["path_id"].strip() else 0
            rows.append(r)
    return rows

def iter_res_files(root=None, exts=None):
    root = root or RES
    for dirpath, dirnames, files in os.walk(root):
        dirnames[:] = [d for d in dirnames if d not in (".git", "__pycache__")]
        for f in files:
            if exts and not f.lower().endswith(exts):
                continue
            yield os.path.join(dirpath, f)

def sniff_header(path):
    """Return a coarse content classification independent of extension."""
    try:
        with open(path, "rb") as fh:
            head = fh.read(64)
            size = os.path.getsize(path)
            if size > 64:
                fh.seek(0)
                full = fh.read(4)
    except OSError:
        return "unreadable", None
    if head.startswith(BUNDLE_MAGICS[:1]):
        return "AssetBundle(UnityFS)", None
    if head.startswith(BUNDLE_MAGICS[2]):
        return "AssetBundle(UnityRaw)", None
    if head.startswith(UNITY_YAML_MAGIC):
        return ("UnityYAML", "editor-ready") if UNITY_YAML_TAG in head else ("UnityYAML?", None)
    if head[:4] == b"\x1f\x8b\x08\x00":
        return "gzip", None
    if head[:2] == b"PK":
        return "zip", None
    if head[:4] == struct.pack("<I", ADDR_MAGIC):
        return "AddressablesCatalog(BinaryStorageBuffer v2)", None
    if head[:4] in (b"OTTO", b"ttcf"):
        return "font", None
    if head[:4] == b"OggS":
        return "ogg-audio", None
    if head[:8] == b"\x89PNG\r\n\x1a\n":
        return "png", None
    if head[:4] == b"AF\x1b\xb1\xfa":
        return "il2cpp-metadata", None
    if head[:2] == b"MZ":
        return "PE-binary", None
    if head.startswith(b"{\rtf") or head.startswith(b"{\n") or head.startswith(b"{"):
        return "json/text", None
    if head.startswith(b"ftyp") or b"ftyp" in head[:16]:
        return "mp4", None
    # Unity SerializedFile heuristic: (metadata_size, file_size, version, data_offset)
    if len(head) >= 20:
        m0, m1, m2, m3 = struct.unpack("<IIII", head[:16])
        if 6 <= m2 <= 30 and m0 != 0 and m1 >= m0 and size >= (m3 or 0):
            return "UnitySerializedFile", None
    try:
        head.decode("utf-8")
        return "text", None
    except UnicodeDecodeError:
        return "binary", None


def load_monoscript_table(force=False):
    """pathID -> {class, namespace, assembly} for the two MonoScript stores.

    res/_monoscripts.csv only covers globalgamemanagers.assets (3045 rows, 2869 unique).
    Addressables bundles reference a *separate* monoscripts.bundle whose 211
    MonoScript pathIDs do not overlap, so it must be read from the bundle.
    """
    p = os.path.join(OUT, "monoscript_table.json")
    if os.path.exists(p) and not force:
        return json.load(open(p, encoding="utf-8"))
    import UnityPy
    table = {"by_pid": {}, "by_identity": {}, "sources": {}}
    # 1) globalgamemanagers.assets (via the CSV the earlier pipeline produced)
    import csv as _csv
    with open(os.path.join(RES, "_monoscripts.csv"), encoding="utf-8-sig", newline="") as fh:
        for r in _csv.DictReader(fh):
            pid = int(r["path_id"])
            rec = {"class": r["class"], "namespace": r["namespace"], "assembly": r["assembly"],
                   "store": "globalgamemanagers.assets"}
            table["by_pid"][str(pid)] = rec
            table["by_identity"][f"{r['assembly']}|{r['namespace']}|{r['class']}"] = rec
    # 2) monoscripts.bundle (raw, 211 entries)
    fp = os.path.join(AAW, "bc78c2a56704c3e9425984fc8925c526_monoscripts.bundle")
    env = UnityPy.load(fp)
    n = 0
    for o in env.objects:
        if o.type.name != "MonoScript":
            continue
        d = o.read()
        rec = {"class": d.m_ClassName, "namespace": d.m_Namespace, "assembly": d.m_AssemblyName,
               "store": "bc78c2a56704c3e9425984fc8925c526_monoscripts.bundle"}
        table["by_pid"][str(o.path_id)] = rec
        table["by_identity"][f"{d.m_AssemblyName}|{d.m_Namespace}|{d.m_ClassName}"] = rec
        n += 1
    table["sources"] = {"ggm_assets_unique": len(table["by_identity"]), "monoscripts_bundle": n}
    json.dump(table, open(p, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
    return table
