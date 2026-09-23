"""Write reviewable evidence for the shipped build's own shaders: parsed ShaderLab state + GLSL of every variant.

    python tools/unity-rebuild/dump_shipped_shaders.py [--match "Naninovel Extender/"] [--out work/shaders]

A player build keeps no shader *source*, but it does keep two things that together pin a shader down:

  * `m_ParsedForm` - the ShaderLab structure: properties (name, description, type, flags, default, attributes),
    subshader/pass tags, and every render-state value (blend factors and ops, ZWrite/ZTest/Cull, stencil),
    with the property a state is bound to (`[_StencilComp]`) where it is bound rather than constant;
  * the compiled programs per platform.  For OpenGLCore (platform 15) Unity stores the programs as GLSL *text*
    (HLSLcc output), one per keyword variant, so the vertex/fragment logic can be read directly.

For each shipped shader whose name matches, this writes `<out>/<name with '/' and ' ' -> '_'>.txt` with the
state summary and the GLSL of every program.  These files are the evidence the hand-written reconstructions
under UnityProject/Assets/Shaders cite.
"""
from __future__ import annotations

import argparse
import csv
import io
import json
import os
import re

import lz4.block

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
RES = os.path.join(REPO, "res")
BACKSLASH = chr(92)
GLCORE = 15
BLEND = ["Zero", "One", "DstColor", "SrcColor", "OneMinusDstColor", "SrcAlpha", "OneMinusSrcColor", "DstAlpha",
         "OneMinusDstAlpha", "SrcAlphaSaturate", "OneMinusSrcAlpha"]
BLENDOP = ["Add", "Sub", "RevSub", "Min", "Max"]
COMPARE = ["Disabled", "Never", "Less", "Equal", "LEqual", "Greater", "NotEqual", "GEqual", "Always"]
STENCILOP = ["Keep", "Zero", "Replace", "IncrSat", "DecrSat", "Invert", "IncrWrap", "DecrWrap"]
CULL = ["Off", "Front", "Back"]
PROPTYPE = ["Color", "Vector", "Float", "Range", "Texture"]
PROPFLAGS = [(1, "HideInInspector"), (2, "PerRendererData"), (4, "NoScaleOffset"), (8, "Normal"), (16, "HDR"),
             (32, "Gamma"), (64, "NonModifiableTextureData"), (128, "MainTexture"), (256, "MainColor")]


def dump_path(row):
    rel = row["output_file"].replace(BACKSLASH, "/")
    for candidate in (os.path.join(RES, rel), os.path.join(RES, "_serialized", rel)):
        if os.path.exists(candidate):
            return candidate
    return None


def state_value(x, names=None):
    """A state value: constant (mapped through `names`) or `[_Property]` when bound to a material property."""
    if isinstance(x, dict) and "val" in x:
        if x.get("name") not in (None, "", "<noninit>"):
            return "[" + x["name"] + "]"
        v = x["val"]
        return names[int(v)] if names and 0 <= int(v) < len(names) else ("%g" % v)
    return str(x)


def glsl_programs(d):
    """The GLSL texts of the GLCore platform, read from the LZ4-compressed program blob."""
    if GLCORE not in d.get("platforms", []):
        return []
    i = d["platforms"].index(GLCORE)
    blob = bytes(d["compressedBlob"])
    off, clen, dlen = d["offsets"][i][0], d["compressedLengths"][i][0], d["decompressedLengths"][i][0]
    raw = lz4.block.decompress(blob[off:off + clen], uncompressed_size=dlen)
    return [t.decode("ascii") for t in re.findall(rb"[\x09\x0a\x0d\x20-\x7e]{200,}", raw)]


def describe(d):
    pf = d["m_ParsedForm"]
    out = ["Shader \"%s\"" % pf["m_Name"], ""]
    kw = pf.get("m_KeywordNames") or []
    flags = pf.get("m_KeywordFlags") or []
    out.append("keywords: " + ", ".join("%d=%s%s" % (i, k, " (local)" if i < len(flags) and flags[i] else "")
                                        for i, k in enumerate(kw)))
    out.append("fallback: %r  custom editor: %r" % (pf.get("m_FallbackName"), pf.get("m_CustomEditorName")))
    out.append("Properties")
    for p in pf["m_PropInfo"]["m_Props"]:
        fl = [n for bit, n in PROPFLAGS if p.get("m_Flags", 0) & bit]
        dv = [p.get("m_DefValue[%d]" % k) for k in range(4)]
        tex = (p.get("m_DefTexture") or {}).get("m_DefaultName")
        out.append("  %-24s %-28s %-8s flags=%s attrs=%s default=%s%s" % (
            p["m_Name"], repr(p.get("m_Description")), PROPTYPE[p["m_Type"]], fl, p.get("m_Attributes"), dv,
            (" texture=" + tex) if tex else ""))
    for si, ss in enumerate(pf["m_SubShaders"]):
        out.append("SubShader %d  LOD %s  Tags %s" % (si, ss.get("m_LOD"), ss["m_Tags"]["tags"]))
        for pi, ps in enumerate(ss["m_Passes"]):
            st = ps["m_State"]
            b = st["rtBlend0"]
            out.append("  Pass %d name=%r type=%s tags=%s" % (pi, ps.get("m_Name"), ps.get("m_Type"), ps["m_Tags"]["tags"]))
            out.append("    Blend %s %s, %s %s   BlendOp %s, %s   ColorMask %s" % (
                state_value(b["srcBlend"], BLEND), state_value(b["destBlend"], BLEND),
                state_value(b["srcBlendAlpha"], BLEND), state_value(b["destBlendAlpha"], BLEND),
                state_value(b["blendOp"], BLENDOP), state_value(b["blendOpAlpha"], BLENDOP), state_value(b["colMask"])))
            out.append("    ZWrite %s  ZTest %s  Cull %s  ZClip %s  Offset %s, %s  AlphaToMask %s  Lighting %s" % (
                state_value(st["zWrite"]), state_value(st["zTest"], COMPARE), state_value(st["culling"], CULL),
                state_value(st["zClip"]), state_value(st["offsetFactor"]), state_value(st["offsetUnits"]),
                state_value(st["alphaToMask"]), st.get("lighting")))
            so = st["stencilOp"]
            out.append("    Stencil Ref %s ReadMask %s WriteMask %s  Comp %s Pass %s Fail %s ZFail %s" % (
                state_value(st["stencilRef"]), state_value(st["stencilReadMask"]), state_value(st["stencilWriteMask"]),
                state_value(so["comp"], COMPARE), state_value(so["pass"], STENCILOP), state_value(so["fail"], STENCILOP),
                state_value(so["zFail"], STENCILOP)))
            for prog in ("progVertex", "progFragment"):
                subs = [s for grp in ps[prog]["m_PlayerSubPrograms"] for s in grp]
                variants = sorted({tuple(kw[k] for k in s["m_KeywordIndices"]) for s in subs})
                out.append("    %s variants: %s  requirements: %s" % (
                    prog, variants, sorted({s["m_ShaderRequirements"] for s in subs})))
    return out


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--match", default="Naninovel Extender/")
    ap.add_argument("--out", default=os.path.join(REPO, "work", "shaders"))
    args = ap.parse_args()
    os.makedirs(args.out, exist_ok=True)
    seen = {}
    with io.open(os.path.join(RES, "_index.csv"), encoding="utf-8-sig") as fh:
        for row in csv.DictReader(fh):
            if row["type"] != "Shader":
                continue
            path = dump_path(row)
            if not path:
                continue
            d = json.load(io.open(path, encoding="utf-8"))
            name = (d.get("m_ParsedForm") or {}).get("m_Name") or ""
            if args.match not in name or name in seen:
                continue
            seen[name] = path
            text = ["// Evidence for the reconstruction of this shipped shader.",
                    "// source dump: " + os.path.relpath(path, REPO).replace(BACKSLASH, "/"), ""] + describe(d)
            for k, program in enumerate(glsl_programs(d)):
                text += ["", "// ---- GLCore program %d ----" % k, program]
            target = os.path.join(args.out, name.replace("/", "_").replace(" ", "_") + ".txt")
            with io.open(target, "w", encoding="utf-8", newline="\n") as fh:
                fh.write("\n".join(text) + "\n")
            print("wrote %s  (%s)" % (os.path.relpath(target, REPO), os.path.relpath(path, REPO)))
    if not seen:
        print("no shipped shader matched %r" % args.match)


if __name__ == "__main__":
    main()
