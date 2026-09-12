#!/usr/bin/env python3
"""Robust multi-process audio extractor (AudioClip -> OGG Vorbis).

Launch several copies; each one claims bundles through exclusive .claim files,
so any number of workers can run in parallel and the job resumes after a crash.

    python audio_worker.py            # run one worker in the foreground
"""
import os, sys, json, io, re, time, traceback, threading

GAME = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game"
DATA = os.path.join(GAME, "manosaba_Data")
OUT = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
DONE = os.path.join(WORK, "done_audio")
CLAIMS = os.path.join(WORK, "claims_audio")
MAN = os.path.join(WORK, f"manifest_audio_{os.getpid()}.jsonl")

SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')


def safe_relpath(p):
    parts = []
    for seg in p.replace("\\", "/").split("/"):
        seg = SAFE.sub("_", seg).strip().rstrip(". ")
        if seg:
            parts.append(seg)
    return "/".join(parts)


def marker(rel):
    return safe_relpath(rel).replace("/", "__") + ".done"


def load_pending():
    tm = json.load(open(os.path.join(WORK, "file_types.json"), encoding="utf-8"))
    rows = []
    for rel, info in tm.items():
        if "AudioClip" in info.get("types", {}):
            rows.append((info["size"], rel))
    rows.sort(reverse=True)
    return [rel for _, rel in rows]


def patch_unitypy():
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


def claim(pending):
    now = time.time()
    for rel in pending:
        if os.path.exists(os.path.join(DONE, marker(rel))):
            continue
        cp = os.path.join(CLAIMS, marker(rel) + ".claim")
        try:
            fd = os.open(cp, os.O_CREAT | os.O_EXCL | os.O_WRONLY)
            os.write(fd, str(os.getpid()).encode())
            os.close(fd)
            return rel, cp
        except FileExistsError:
            try:
                if now - os.path.getmtime(cp) > 1800:      # stale claim
                    os.remove(cp)
            except OSError:
                pass
            continue
    return None, None


def process(rel, mfh):
    import UnityPy
    from UnityPy.enums import ClassIDType
    import soundfile as sf
    patch_unitypy()
    fp = os.path.join(DATA, rel)
    env = UnityPy.load(fp)
    n_new = n_skip = n_err = 0

    def ogg_write(path, data, sr):
        """libsndfile's Vorbis encoder puts a buffer proportional to the block size on the
        stack -> a single big write overflows the stack (STATUS_STACK_OVERFLOW) for long
        tracks.  Write in chunks from a thread that has a large stack."""
        err = []

        def run():
            try:
                step = 65536
                with sf.SoundFile(path, "w", samplerate=sr, channels=data.shape[1],
                                  format="OGG", subtype="VORBIS") as fh:
                    for i in range(0, len(data), step):
                        fh.write(data[i:i + step])
            except Exception as e:            # noqa: BLE001
                err.append(e)

        t = threading.Thread(target=run)
        t.start()
        t.join()
        if err:
            raise err[0]

    for obj in env.objects:
        if obj.type != ClassIDType.AudioClip:
            continue
        try:
            d = obj.read()
        except Exception as e:
            n_err += 1
            continue
        cont = obj.container
        name = getattr(d, "m_Name", None) or f"pathid_{obj.path_id}"
        try:
            samples = d.samples
        except Exception as e:
            n_err += 1
            print("   ERR decode", rel, name, type(e).__name__, e, flush=True)
            continue
        for fn, wav in samples.items():
            if cont:
                relc = safe_relpath(cont)
                dstdir = os.path.join(OUT, os.path.dirname(relc))
                stem = os.path.basename(relc)
                if "." in stem:
                    stem = stem.rsplit(".", 1)[0]
            else:
                dstdir = os.path.join(OUT, "Uncategorized", "AudioClip")
                stem = SAFE.sub("_", name)
            if len(samples) > 1:
                stem = f"{stem}.{os.path.splitext(fn)[0]}"
            dst = os.path.join(dstdir, stem + ".ogg")
            os.makedirs(dstdir, exist_ok=True)
            if os.path.exists(dst) and os.path.getsize(dst) > 512:
                n_skip += 1
            else:
                try:
                    data, sr = sf.read(io.BytesIO(wav), dtype="float32", always_2d=True)
                    ogg_write(dst, data, sr)
                    n_new += 1
                except Exception as e:
                    n_err += 1
                    print("   ERR write", dst, type(e).__name__, e, flush=True)
                    continue
            mfh.write(json.dumps({"src": rel, "path_id": obj.path_id, "type": "AudioClip",
                                  "container": cont, "name": name,
                                  "out": os.path.relpath(dst, OUT).replace(os.sep, "/")},
                                 ensure_ascii=False) + "\n")
    return n_new, n_skip, n_err


def main():
    os.makedirs(DONE, exist_ok=True)
    os.makedirs(CLAIMS, exist_ok=True)
    try:
        threading.stack_size(64 * 1024 * 1024)   # room for libsndfile's Vorbis encoder
    except (ValueError, RuntimeError):
        pass
    pending = load_pending()
    print(f"[{os.getpid()}] pending {len(pending)}", flush=True)
    t0 = time.time()
    mfh = open(MAN, "a", encoding="utf-8")
    while True:
        rel, cp = claim(pending)
        if rel is None:
            break
        try:
            n_new, n_skip, n_err = process(rel, mfh)
            mfh.flush()
            open(os.path.join(DONE, marker(rel)), "w").close()
            print(f"[{os.getpid()}] OK {os.path.basename(rel)} new={n_new} skip={n_skip} err={n_err} "
                  f"({time.time()-t0:.0f}s)", flush=True)
        except Exception as e:
            print(f"[{os.getpid()}] FAIL {rel}: {type(e).__name__}: {e}\n{traceback.format_exc()}", flush=True)
        finally:
            try:
                os.remove(cp)
            except OSError:
                pass
    mfh.close()
    print(f"[{os.getpid()}] done {time.time()-t0:.0f}s", flush=True)


if __name__ == "__main__":
    main()
