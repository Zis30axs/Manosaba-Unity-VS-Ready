#!/usr/bin/env python3
"""Manosaba asset extractor -> res/  (Unity 6000.0.48f1 / IL2CPP / Addressables)

Mirrors in-game Addressables container paths under res/Assets/...
Usage:
  python extract_res.py --stage core        # text, textures, sprites, fonts, video, data json
  python extract_res.py --stage audio       # AudioClip -> ogg
  python extract_res.py --stage core --workers 4 --files-substr naninovel
"""
import os, sys, json, io, time, argparse, traceback, re, subprocess, tempfile, shutil, threading
import multiprocessing as mp

GAME = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game"
DATA = os.path.join(GAME, "manosaba_Data")
AA = os.path.join(DATA, r"StreamingAssets\aa\StandaloneWindows64")
OUT = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"


def configure(game, output, work):
    """Portable entry used by content import; legacy research CLI stays available."""
    global GAME, DATA, AA, OUT, WORK
    GAME, OUT, WORK = map(os.path.abspath, (game, output, work))
    DATA = os.path.join(GAME, "manosaba_Data")
    AA = os.path.join(DATA, "StreamingAssets", "aa", "StandaloneWindows64")

CORE_TYPES = {"Texture2D", "Sprite", "TextAsset", "Font", "VideoClip", "MonoBehaviour", "Material",
              "AnimationClip", "AnimatorController", "AnimatorOverrideController", "PlayableDirector",
              "SpriteAtlas", "Mesh", "RenderTexture", "AudioMixerController", "AudioMixerGroupController",
              "AudioMixerSnapshotController", "Avatar", "AvatarMask", "LocalizationAsset",
              "Shader", "ComputeShader", "Cubemap", "Texture3D", "PrefabInstance"}

SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')

_BIG_STACK_READY = False


def ensure_big_stack():
    """Reserve stack for libsndfile's Vorbis encoder.

    The encoder puts a buffer proportional to the block size on the stack; long tracks
    (voice/BGM) overflow the default 1 MiB stack on a single big write and kill the
    process with STATUS_STACK_OVERFLOW (0xC00000FD) without a Python traceback.
    """
    global _BIG_STACK_READY
    if not _BIG_STACK_READY:
        try:
            threading.stack_size(64 * 1024 * 1024)
        except (ValueError, RuntimeError):
            pass
        _BIG_STACK_READY = True


def write_ogg(path, data, sr):
    """Chunked Vorbis write from a large-stack thread, matching work/audio_worker.py."""
    import soundfile as sf
    ensure_big_stack()
    errors = []

    def run():
        try:
            step = 65536
            with sf.SoundFile(path, "w", samplerate=sr, channels=data.shape[1],
                              format="OGG", subtype="VORBIS") as fh:
                for i in range(0, len(data), step):
                    fh.write(data[i:i + step])
        except Exception as e:  # noqa: BLE001
            errors.append(e)

    thread = threading.Thread(target=run)
    thread.start()
    thread.join()
    if errors:
        raise errors[0]


def safe_relpath(p):
    parts = []
    for seg in p.replace("\\", "/").split("/"):
        if seg in (".", ".."):
            raise ValueError("unsafe extracted container path")
        seg = SAFE.sub("_", seg).strip().rstrip(". ")
        if seg:
            parts.append(seg)
    return "/".join(parts)


def load_types():
    with open(os.path.join(WORK, "file_types.json"), encoding="utf-8") as fh:
        return json.load(fh)


def all_targets(stage, types_map, substr=None):
    out = []
    for rel, info in types_map.items():
        if "error" in info:
            continue
        if substr and substr.lower() not in rel.lower():
            continue
        ts = set(info["types"])
        need = {"AudioClip"} if stage == "audio" else CORE_TYPES
        if ts & need:
            out.append((rel, info["size"]))
    return out


def dst_root(rel):
    p = os.path.join(DATA, rel)
    return p


# ---------------------------------------------------------------- core worker
def _patch_unitypy():
    """UnityPy tries to open bogus dependency paths (empty / directory) for textures whose
    stream data is missing (e.g. 0x0 'Font Texture' placeholders) -> guard it."""
    import UnityPy.environment as E
    if getattr(E.Environment, "_manosaba_patched", False):
        return
    orig = E.Environment.load_file

    def load_file(self, path, *a, **k):
        p = str(path) if path is not None else ""
        if not p or os.path.isdir(p):
            raise FileNotFoundError(p)
        return orig(self, path, *a, **k)

    E.Environment.load_file = load_file
    E.Environment._manosaba_patched = True


def safe_image(d, errs):
    """Return a PIL image for a Texture2D/Sprite, tolerating common edge cases."""
    if getattr(d, "m_Width", 1) == 0 or getattr(d, "m_Height", 1) == 0:
        return None
    try:
        img = d.image
        if img is not None:
            return img
    except Exception as e:
        errs.append(f"image {type(e).__name__}: {e}")
    try:
        rd = getattr(d, "m_RD", None)
        tex = getattr(rd, "texture", None) if rd is not None else None
        if tex is not None:
            return tex.read().image
    except Exception as e:
        errs.append(f"image-fallback {type(e).__name__}: {e}")
    return None


def worker(rel, stage):
    import UnityPy
    from UnityPy.enums import ClassIDType
    import PIL.Image
    _patch_unitypy()
    entries, errs = [], []
    fp = dst_root(rel)
    try:
        env = UnityPy.load(fp)
    except Exception as e:
        return rel, entries, [f"load: {type(e).__name__}: {e}"]
    bundle = None
    for fobj in env.files.values():
        if hasattr(fobj, "files"):
            bundle = fobj
            break
    try:
        env.cab_file = bundle
    except Exception:
        pass
    inner = getattr(bundle, "files", None) or {}
    for obj in env.objects:
        tn = obj.type.name
        if stage == "audio" and tn != "AudioClip":
            continue
        if obj.type == ClassIDType.AssetBundle:
            continue
        try:
            d = obj.read()
        except Exception as e:
            errs.append(f"read {tn} {obj.path_id}: {type(e).__name__}: {e}")
            continue
        cont = obj.container
        name = getattr(d, "m_Name", None)
        try:
            if tn in ("Texture2D", "Cubemap", "Texture3D"):
                img = safe_image(d, errs)
                if img is None:
                    continue
                buf = io.BytesIO()
                img.save(buf, format="PNG", compress_level=1)
                ext, tag = ".png", ""
            elif tn == "Sprite":
                img = safe_image(d, errs)
                if img is None:
                    continue
                buf = io.BytesIO()
                img.save(buf, format="PNG", compress_level=1)
                ext, tag = ".png", "sprite"
            elif tn == "RenderTexture":
                continue
            else:
                buf = None
                ext = tag = None

            if buf is None and tn == "TextAsset":
                raw = d.m_Script
                if isinstance(raw, str):
                    raw = raw.encode("utf-8", "surrogateescape")
                buf = io.BytesIO(bytes(raw))
                ext = ".txt"
                if cont and "." in cont.rsplit("/", 1)[-1]:
                    ext = "." + cont.rsplit(".", 1)[-1]
                elif name and "." in name:
                    ext = "." + name.rsplit(".", 1)[-1]
                tag = None
            if buf is None and tn == "Font":
                data = bytes(d.m_FontData)
                buf = io.BytesIO(data)
                ext = ".otf" if data[:4] in (b"OTTO", b"ttcf") else ".ttf"
                tag = None
            if buf is None and tn == "VideoClip":
                tt = obj.read_typetree()
                er = tt.get("m_ExternalResources") or {}
                src = er.get("m_Source") or ""
                off, size = int(er.get("m_Offset") or 0), int(er.get("m_Size") or 0)
                blob = None
                if src and size and inner:
                    key = src.split("/")[-1]
                    r = inner.get(key)
                    if r is not None:
                        try:
                            r.seek(off)
                            blob = r.read_bytes(size)
                        except Exception as e:
                            errs.append(f"video stream: {e}")
                ext = ".mp4"
                op = tt.get("m_OriginalPath") or ""
                if "." in op:
                    ext = "." + op.rsplit(".", 1)[-1]
                tag = None
                if blob is None:
                    continue
                buf = io.BytesIO(blob)
            if buf is None and tn == "AudioClip":
                if stage != "audio":
                    continue
                try:
                    samples = d.samples
                except Exception as e:
                    errs.append(f"audio decode {name}: {type(e).__name__}: {e}")
                    continue
                import soundfile as sf
                written = 0
                for fn, wav in samples.items():
                    try:
                        data, sr = sf.read(io.BytesIO(wav), dtype="float32", always_2d=True)
                    except Exception as e:
                        errs.append(f"audio read {name}/{fn}: {type(e).__name__}: {e}")
                        continue
                    if cont:
                        relc = safe_relpath(cont)
                        dstdir = os.path.join(OUT, os.path.dirname(relc))
                        stem = os.path.basename(relc)
                        if "." in stem:
                            stem = stem.rsplit(".", 1)[0]
                    else:
                        dstdir = os.path.join(OUT, "Uncategorized", "AudioClip")
                        stem = SAFE.sub("_", name or f"pathid_{obj.path_id}")
                    if len(samples) > 1:
                        stem = f"{stem}.{os.path.splitext(fn)[0]}"
                    dst = os.path.join(dstdir, stem + ".ogg")
                    os.makedirs(dstdir, exist_ok=True)
                    if os.path.exists(dst):
                        i = 1
                        while os.path.exists(f"{dst[:-4]}~{i}.ogg"):
                            i += 1
                        dst = f"{dst[:-4]}~{i}.ogg"
                    try:
                        write_ogg(dst, data, sr)
                    except Exception as e:
                        errs.append(f"ogg write {dst}: {type(e).__name__}: {e}")
                        try:
                            os.remove(dst)
                        except OSError:
                            pass
                        continue
                    written += 1
                    entries.append({"src": rel, "path_id": obj.path_id, "type": tn, "container": cont,
                                    "name": name, "out": os.path.relpath(dst, OUT).replace(os.sep, "/")})
                continue
            if buf is None and stage == "audio" and tn not in ("AudioClip",):
                continue
            if buf is None:
                # everything else -> typetree json
                try:
                    tt = obj.read_typetree()
                except Exception as e:
                    errs.append(f"typetree {tn} {name}: {type(e).__name__}: {e}")
                    continue
                txt = json.dumps(tt, ensure_ascii=False, indent=1, default=str)
                buf = io.BytesIO(txt.encode("utf-8"))
                ext = ".asset.json" if tn == "MonoBehaviour" else ".json"
                tag = None

            if cont:
                relc = safe_relpath(cont)
                dstdir = os.path.join(OUT, os.path.dirname(relc))
                stem = os.path.basename(relc)
                if "." in stem:
                    stem = stem.rsplit(".", 1)[0]
            else:
                dstdir = os.path.join(OUT, "Uncategorized", tn)
                stem = name or f"pathid_{obj.path_id}"
                stem = SAFE.sub("_", stem)
            if tn not in CORE_TYPES and tn not in ("MonoScript", "AudioClip"):
                dstdir = os.path.join(OUT, "_serialized", os.path.relpath(dstdir, OUT))
            if tag:
                stem = f"{stem}.{tag}"
            dst = os.path.join(dstdir, stem + ext)
            os.makedirs(dstdir, exist_ok=True)
            if os.path.exists(dst):
                i = 1
                base = dst[: -len(ext)]
                while os.path.exists(f"{base}~{i}{ext}"):
                    i += 1
                dst = f"{base}~{i}{ext}"
            with open(dst, "wb") as fh:
                fh.write(buf.getvalue())
            entries.append({"src": rel, "path_id": obj.path_id, "type": tn, "container": cont,
                            "name": name, "out": os.path.relpath(dst, OUT).replace(os.sep, "/")})
        except Exception as e:
            errs.append(f"export {tn} {name}: {type(e).__name__}: {e}")
    del env
    return rel, entries, errs


def extract_portable(game, output, work, workers=1):
    """Reuse the existing object exporter on readable local resources only.

    Deterministic single-worker traversal keeps duplicate output suffixes stable across
    reimports. No Player, IL2CPP, Steam or BepInEx files are read.
    """
    configure(game, output, work)
    paths = []
    if os.path.isdir(AA):
        for root, dirs, files in os.walk(AA):
            dirs.sort()
            paths.extend(os.path.join(root, name) for name in sorted(files) if name.endswith(".bundle"))
    for name in ("resources.assets", "globalgamemanagers.assets", "sharedassets0.assets"):
        path = os.path.join(DATA, name)
        if os.path.isfile(path):
            paths.append(path)
    if not paths:
        raise FileNotFoundError("No readable Addressables bundles or serialized assets in manosaba_Data")
    os.makedirs(OUT, exist_ok=True)
    os.makedirs(WORK, exist_ok=True)
    entries, errors = [], []
    for i, path in enumerate(sorted(paths), 1):
        rel = os.path.relpath(path, DATA)
        for stage in ("core", "audio"):
            _, exported, failures = worker(rel, stage)
            entries.extend(exported)
            errors.extend({"source": rel, "stage": stage, "message": message} for message in failures)
        print(f"[extract] {i}/{len(paths)} {rel} objects={len(entries)} errors={len(errors)}", flush=True)
    with open(os.path.join(WORK, "extraction_report.json"), "w", encoding="utf-8") as fh:
        json.dump({"entries": len(entries), "errors": errors}, fh, ensure_ascii=False, indent=1)
    return entries, errors


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--stage", default="core", choices=["core", "audio"])
    ap.add_argument("--workers", type=int, default=4)
    ap.add_argument("--files-substr", default="")
    ap.add_argument("--limit", type=int, default=0)
    a = ap.parse_args()

    types_map = load_types()
    targets = all_targets(a.stage, types_map, a.files_substr or None)
    targets.sort(key=lambda x: -x[1])
    if a.limit:
        targets = targets[: a.limit]
    print(f"stage={a.stage} bundles={len(targets)} workers={a.workers} "
          f"total={sum(s for _, s in targets)/2**20:.0f} MiB", flush=True)

    os.makedirs(OUT, exist_ok=True)
    donedir = os.path.join(WORK, "done_" + a.stage)
    os.makedirs(donedir, exist_ok=True)
    mpath = os.path.join(WORK, f"manifest_{a.stage}.jsonl")
    mfh = open(mpath, "a", encoding="utf-8")

    t0 = time.time()
    n_obj = n_err = 0
    batch = []
    pending = [rel for rel, _ in targets
               if not os.path.exists(os.path.join(donedir, safe_relpath(rel).replace("/", "__") + ".done"))]
    print(f"pending={len(pending)}", flush=True)

    ctx = mp.get_context("spawn")
    with ctx.Pool(processes=a.workers, maxtasksperchild=1) as pool:
        it = pool.imap_unordered(_worker_entry, [(rel, a.stage) for rel in pending], chunksize=1)
        for i, (rel, entries, errs) in enumerate(it, 1):
            for e in entries:
                mfh.write(json.dumps(e, ensure_ascii=False) + "\n")
            n_obj += len(entries)
            n_err += len(errs)
            open(os.path.join(donedir, safe_relpath(rel).replace("/", "__") + ".done"), "w").close()
            for e in errs[:5]:
                print("   ERR", rel, e, flush=True)
            if i % 10 == 0 or i == len(pending):
                print(f"[{i}/{len(pending)}] {os.path.basename(rel)} objs={len(entries)} "
                      f"total={n_obj} errs={n_err} {time.time()-t0:.0f}s", flush=True)
                mfh.flush()
    mfh.close()
    print(f"DONE stage={a.stage} objects={n_obj} errors={n_err} in {time.time()-t0:.0f}s", flush=True)


def _worker_entry(arg):
    rel, stage = arg
    try:
        return worker(rel, stage)
    except Exception as e:
        return rel, [], [f"fatal: {type(e).__name__}: {e}\n{traceback.format_exc()}"]


if __name__ == "__main__":
    main()
