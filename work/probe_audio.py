import os, time, io, struct
import UnityPy
from UnityPy.enums import ClassIDType

AA = r"E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\StreamingAssets\aa\StandaloneWindows64"
p = os.path.join(AA, "general-voice-act01_chapter01_trial_assets_all.bundle")

t0 = time.time()
env = UnityPy.load(p)
print(f"bundle load: {time.time()-t0:.2f}s")

clips = [o for o in env.objects if o.type == ClassIDType.AudioClip]
print("clips:", len(clips))

d0 = clips[0].read()
raw = bytes(d0.m_AudioData) if d0.m_AudioData else None
if raw is None and d0.m_Resource:
    from UnityPy.helpers.ResourceReader import get_resource_data
    r = d0.m_Resource
    raw = get_resource_data(r.m_Source, clips[0].assets_file, r.m_Offset, r.m_Size)
print("raw magic:", raw[:16], "len", len(raw))
if raw[:4] == b"FSB5":
    ver, nsamp, hdrsize, nch, freq, datasz, mode, zero, hashv, version = struct.unpack("<IIIIIIIIII", raw[4:44])
    print(f"FSB5 ver={ver} samples={nsamp} hdrsize={hdrsize} ch={nch} freq={freq} datasz={datasz} mode={mode}")

# time: raw extraction for 50 clips
t0 = time.time()
n = 0
for c in clips[:50]:
    d = c.read()
    _ = bytes(d.m_AudioData)[:16] if d.m_AudioData else b""
    n += 1
print(f"read+rawdata x{n}: {time.time()-t0:.2f}s")

# time: fmod decode for 10 clips
t0 = time.time()
for c in clips[:10]:
    d = c.read()
    s = d.samples
print(f"samples decode x10: {time.time()-t0:.2f}s")

# time: vorbis encode of one decoded wav
import soundfile as sf
d = clips[0].read()
wav = list(d.samples.values())[0]
t0 = time.time()
data, sr = sf.read(io.BytesIO(wav), dtype="float32", always_2d=True)
t1 = time.time()
buf = io.BytesIO()
sf.write(buf, data, sr, format="OGG", subtype="VORBIS")
t2 = time.time()
print(f"wav read {t1-t0:.3f}s  ogg encode {t2-t1:.3f}s  out={len(buf.getvalue())}B from {len(wav)}B")
