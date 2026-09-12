"""Move structural/serialized-object JSON dumps out of the mirrored asset tree into res/_serialized/,
then rewrite the manifest and build res/_index.csv"""
import json, os, shutil, csv, collections

RES = r"E:\manosaba_re\res"
WORK = r"E:\manosaba_re\work"
MAN = os.path.join(WORK, "manifest_core.jsonl")
KEEP = {"Texture2D", "Sprite", "TextAsset", "Font", "VideoClip", "MonoBehaviour", "Material",
        "AnimationClip", "AnimatorController", "AnimatorOverrideController", "PlayableDirector",
        "SpriteAtlas", "Mesh", "RenderTexture", "AudioMixerController", "AudioMixerGroupController",
        "AudioMixerSnapshotController", "Avatar", "AvatarMask", "LocalizationAsset", "Shader",
        "ComputeShader", "AudioClip", "Cubemap", "Texture3D"}

entries = []
moved = 0
with open(MAN, encoding="utf-8") as fh:
    for line in fh:
        line = line.strip()
        if not line:
            continue
        e = json.loads(line)
        src = os.path.join(RES, e["out"].replace("/", os.sep))
        if e["type"] not in KEEP and not e["out"].startswith("_serialized/") and os.path.exists(src):
            rel = e["out"]
            newrel = "_serialized/" + rel
            dst = os.path.join(RES, newrel.replace("/", os.sep))
            os.makedirs(os.path.dirname(dst), exist_ok=True)
            try:
                shutil.move(src, dst)
                e["out"] = newrel
                moved += 1
            except Exception as ex:
                print("MOVE FAIL", rel, ex)
        entries.append(e)

with open(MAN, "w", encoding="utf-8") as fh:
    for e in entries:
        fh.write(json.dumps(e, ensure_ascii=False) + "\n")

with open(os.path.join(RES, "_index.csv"), "w", encoding="utf-8-sig", newline="") as fh:
    w = csv.writer(fh)
    w.writerow(["type", "name", "asset_path", "output_file", "source_bundle", "path_id"])
    for e in sorted(entries, key=lambda x: (x["type"], x["out"])):
        w.writerow([e["type"], e.get("name") or "", e.get("container") or "", e["out"],
                    e.get("src") or "", e.get("path_id")])

c = collections.Counter(e["type"] for e in entries)
print("moved", moved, "entries", len(entries))
for k, v in c.most_common(15):
    print(" ", k, v)
