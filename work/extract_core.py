#!/usr/bin/env python3
"""Manosaba (Unity 6000.0.48f1, IL2CPP) asset extractor -> res/

Mirrors the in-game Addressables container paths:
    Assets/#WitchTrials/...          (Assets/<rest> of container path)
Objects without a container path land in Uncategorized/<Type>/.

Usage:  python extract_core.py [--only TYPE1,TYPE2] [--limit N] [--files SUBSTR]
"""
import os, sys, json, io, time, argparse, traceback, re
import UnityPy
from UnityPy.enums import ClassIDType

GAME = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game"
DATA = os.path.join(GAME, "manosaba_Data")
AA = os.path.join(DATA, r"StreamingAssets\aa\StandaloneWindows64")
OUT = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"

IMAGE_TYPES = {ClassIDType.Texture2D, ClassIDType.Sprite}
TEXT_TYPES = {ClassIDType.TextAsset}
FONT_TYPES = {ClassIDType.Font}
VIDEO_TYPES = {ClassIDType.VideoClip}
AUDIO_TYPES = {ClassIDType.AudioClip}
DATA_TYPES = {ClassIDType.MonoBehaviour, ClassIDType.Material, ClassIDType.AnimationClip,
              ClassIDType.AnimatorController, ClassIDType.AnimatorOverrideController,
              ClassIDType.PlayableDirector, ClassIDType.SpriteAtlas, ClassIDType.Mesh,
              ClassIDType.RenderTexture, ClassIDType.AudioMixerController,
              ClassIDType.AudioMixerGroupController, ClassIDType.AudioMixerSnapshotController,
              ClassIDType.Avatar, ClassIDType.AvatarMask,
              ClassIDType.LocalizationAsset}
SHADER_TYPES = {ClassIDType.Shader, ClassIDType.ComputeShader}
SKIP = {ClassIDType.AssetBundle}

SAFE = re.compile(r'[<>:"|?*\x00-\x1f]')


def safe_relpath(p):
    p = p.replace("\\", "/")
    parts = []
    for seg in p.split("/"):
        seg = SAFE.sub("_", seg).strip().rstrip(". ")
        if not seg:
            continue
        parts.append(seg)
    return "/".join(parts)


def collect_files():
    files = []
    for root, dirs, fs in os.walk(AA):
        for f in fs:
            files.append(os.path.join(root, f))
    for f in ("resources.assets", "globalgamemanagers.assets", "sharedassets0.assets",
              "level0", "globalgamemanagers"):
        p = os.path.join(DATA, f)
        if os.path.exists(p):
            files.append(p)
    return sorted(files)


class Extractor:
    def __init__(self, only=None, limit=0, files_filter=None, do_audio=False):
        self.only = set(only) if only else None
        self.limit = limit
        self.files_filter = files_filter
        self.do_audio = do_audio
        self.manifest = []
        self.stats = {}
        self.written = 0
        self.skipped = 0
        self.errors = 0
        self.bundle = None

    def want(self, typename):
        if self.only:
            return typename in self.only
        if typename in ("AudioClip",):
            return self.do_audio
        return True

    def dest_dir(self, container, typename):
        if container:
            rel = safe_relpath(container)
            if "/" in rel:
                d, _ = rel.rsplit("/", 1)
            else:
                d = ""
        else:
            d = "Uncategorized/" + typename
        return os.path.join(OUT, d.replace("/", os.sep))

    def dest_name(self, container, typename, name, ext, tag=""):
        if container:
            rel = safe_relpath(container)
            stem = rel.rsplit("/", 1)[-1]
        else:
            stem = name or "unnamed"
        if "." in stem:
            stem = stem.rsplit(".", 1)[0]
        if tag:
            stem = f"{stem}.{tag}"
        return stem + ext

    def unique(self, path):
        if not os.path.exists(path):
            return path
        base, ext = os.path.splitext(path)
        i = 1
        while True:
            cand = f"{base}~{i}{ext}"
            if not os.path.exists(cand):
                return cand
            i += 1

    def emit(self, path, data):
        path = self.unique(path)
        os.makedirs(os.path.dirname(path), exist_ok=True)
        mode = "wb" if isinstance(data, (bytes, bytearray, memoryview)) else "w"
        if mode == "wb":
            with open(path, "wb") as fh:
                fh.write(data)
        else:
            with open(path, "w", encoding="utf-8", newline="") as fh:
                fh.write(data)
        self.written += 1
        return path

    def emit_json(self, path, obj_tree):
        txt = json.dumps(obj_tree, ensure_ascii=False, indent=1, default=str)
        return self.emit(path, txt)

    # ---------- per-type handlers ----------
    def do_texture(self, obj, d, container):
        img = d.image
        if img is None:
            return None
        buf = io.BytesIO()
        img.save(buf, format="PNG")
        p = os.path.join(self.dest_dir(container, "Texture2D"),
                         self.dest_name(container, "Texture2D", d.m_Name, ".png"))
        return self.emit(p, buf.getvalue())

    def do_sprite(self, obj, d, container):
        img = d.image
        if img is None:
            return None
        buf = io.BytesIO()
        img.save(buf, format="PNG")
        p = os.path.join(self.dest_dir(container, "Sprite"),
                         self.dest_name(container, "Sprite", d.m_Name, ".png", tag="sprite"))
        return self.emit(p, buf.getvalue())

    def do_text(self, obj, d, container):
        raw = d.m_Script
        if isinstance(raw, str):
            raw = raw.encode("utf-8", "surrogateescape")
        raw = bytes(raw)
        name = d.m_Name or "text"
        ext = ".txt"
        if container:
            c = safe_relpath(container)
            if "." in c.rsplit("/", 1)[-1]:
                ext = "." + c.rsplit(".", 1)[-1]
        else:
            if "." in name:
                ext = "." + name.rsplit(".", 1)[-1]
        p = os.path.join(self.dest_dir(container, "TextAsset"),
                         self.dest_name(container, "TextAsset", name, ext))
        return self.emit(p, raw)

    def do_font(self, obj, d, container):
        data = bytes(d.m_FontData)
        ext = ".otf" if data[:4] in (b"OTTO", b"ttcf") else ".ttf"
        p = os.path.join(self.dest_dir(container, "Font"),
                         self.dest_name(container, "Font", d.m_Name, ext))
        return self.emit(p, data)

    def do_video(self, obj, d, container):
        tt = obj.read_typetree()
        ext = ".mp4"
        op = tt.get("m_OriginalPath") or ""
        if "." in op:
            ext = "." + op.rsplit(".", 1)[-1]
        name = d.m_Name or "video"
        p = os.path.join(self.dest_dir(container, "VideoClip"),
                         self.dest_name(container, "VideoClip", name, ext))
        er = tt.get("m_ExternalResources") or {}
        src = er.get("m_Source") or ""
        offset = int(er.get("m_Offset") or 0)
        size = int(er.get("m_Size") or 0)
        blob = self.read_stream(src, offset, size)
        if blob is None:
            return self.emit_json(p + ".meta.json", tt)
        return self.emit(p, blob)

    def read_stream(self, src, offset, size):
        """Pull bytes out of a .resource / .resS stream living inside the current bundle."""
        if not src or not size:
            return None
        inner = src.split("/")[-1]
        bundle = self.bundle
        if bundle is None:
            return None
        inner_files = getattr(bundle, "files", None) or {}
        r = inner_files.get(inner)
        if r is None:
            for k, v in inner_files.items():
                if k.endswith(inner) or inner.endswith(k):
                    r = v
                    break
        if r is None:
            return None
        try:
            r.seek(offset)
            return r.read_bytes(size)
        except Exception:
            try:
                data = r.bytes if hasattr(r, "bytes") else None
                if data is not None:
                    return data[offset:offset + size]
            except Exception:
                pass
        return None

    def do_json(self, obj, d, container, typename):
        try:
            tt = obj.read_typetree()
        except Exception as e:
            tt = {"__error__": f"{type(e).__name__}: {e}", "__name__": getattr(d, "m_Name", None)}
        name = getattr(d, "m_Name", None) or "unnamed"
        ext = ".asset.json" if typename == "MonoBehaviour" else ".json"
        p = os.path.join(self.dest_dir(container, typename),
                         self.dest_name(container, typename, name, ext))
        return self.emit_json(p, tt)

    def do_shader(self, obj, d, container):
        txt = None
        try:
            txt = d.dump()
        except Exception:
            base = getattr(d, "m_Script", None)
            if base:
                try:
                    txt = bytes(base).decode("utf-8", "replace")
                except Exception:
                    txt = None
        name = getattr(d, "m_Name", None) or "shader"
        p = os.path.join(self.dest_dir(container, "Shader"),
                         self.dest_name(container, "Shader", name, ".shader"))
        if txt is None:
            return self.do_json(obj, d, container, "Shader")
        return self.emit(p, txt)

    # ---------- driver ----------
    def run(self):
        files = collect_files()
        if self.files_filter:
            files = [f for f in files if self.files_filter.lower() in f.lower()]
        t0 = time.time()
        for i, fp in enumerate(files):
            rel = os.path.relpath(fp, DATA)
            try:
                env = UnityPy.load(fp)
            except Exception as e:
                print(f"[{i}] LOAD FAIL {rel}: {e}", flush=True)
                self.errors += 1
                continue
            self.bundle = None
            for fobj in env.files.values():
                if fobj.__class__.__name__ == "BundleFile" or hasattr(fobj, "files"):
                    self.bundle = fobj
                    break
            n = 0
            for obj in env.objects:
                tn = obj.type.name
                if obj.type in SKIP or not self.want(tn):
                    continue
                try:
                    d = obj.read()
                except Exception as e:
                    print(f"  READ FAIL {tn} pid={obj.path_id} in {rel}: {e}", flush=True)
                    self.errors += 1
                    continue
                container = obj.container
                path = None
                try:
                    if obj.type == ClassIDType.Texture2D:
                        path = self.do_texture(obj, d, container)
                    elif obj.type == ClassIDType.Sprite:
                        path = self.do_sprite(obj, d, container)
                    elif obj.type == ClassIDType.TextAsset:
                        path = self.do_text(obj, d, container)
                    elif obj.type == ClassIDType.Font:
                        path = self.do_font(obj, d, container)
                    elif obj.type == ClassIDType.VideoClip:
                        path = self.do_video(obj, d, container)
                    elif obj.type == ClassIDType.AudioClip:
                        path = None  # handled by extract_audio.py
                    elif obj.type in SHADER_TYPES:
                        path = self.do_shader(obj, d, container)
                    elif obj.type in DATA_TYPES:
                        path = self.do_json(obj, d, container, tn)
                    else:
                        self.skipped += 1
                        continue
                except Exception as e:
                    print(f"  EXPORT FAIL {tn} {getattr(d,'m_Name','?')} in {rel}: {type(e).__name__}: {e}", flush=True)
                    self.errors += 1
                    continue
                if path:
                    n += 1
                    self.stats[tn] = self.stats.get(tn, 0) + 1
                    self.manifest.append({
                        "src": rel, "path_id": obj.path_id, "type": tn,
                        "container": container, "name": getattr(d, "m_Name", None),
                        "out": os.path.relpath(path, OUT).replace(os.sep, "/"),
                    })
                if self.limit and self.written >= self.limit:
                    print("limit reached", flush=True)
                    self.save()
                    return
            print(f"[{i+1}/{len(files)}] {rel} -> {n} assets  ({time.time()-t0:.0f}s, total {self.written})", flush=True)
        self.save()

    def save(self):
        os.makedirs(WORK, exist_ok=True)
        with open(os.path.join(WORK, "manifest_core.json"), "w", encoding="utf-8") as fh:
            json.dump({"stats": self.stats, "items": self.manifest, "errors": self.errors}, fh,
                      ensure_ascii=False, indent=1)
        print("=== STATS ===")
        for k, v in sorted(self.stats.items(), key=lambda x: -x[1]):
            print(f"  {k}: {v}")
        print(f"written={self.written} skipped={self.skipped} errors={self.errors}")


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--only", default="")
    ap.add_argument("--limit", type=int, default=0)
    ap.add_argument("--files", default="")
    ap.add_argument("--audio", action="store_true")
    a = ap.parse_args()
    only = [x for x in a.only.split(",") if x] or None
    os.makedirs(OUT, exist_ok=True)
    Extractor(only=only, limit=a.limit, files_filter=a.files or None, do_audio=a.audio).run()


if __name__ == "__main__":
    main()
