"""Port the refutation cut-in Shader Graphs to built-in-pipeline shaders.

The shipped build only carries compiled programs for these URP Shader Graphs, so this tool reads the DX11 pixel
program out of the cached spawn bundle, disassembles it with the Windows d3dcompiler, and translates the straight-line
ps_4_0 code into HLSL, instruction by instruction.  The generated shaders are tracked sources
(Assets/Shaders/ManosabaCutIn/*.shader); rerun this tool only to regenerate them.

    python tools/unity-rebuild/port_cutin_shaders.py [--out UnityProject/Assets/Shaders/ManosabaCutIn]

Requires Windows (d3dcompiler_47.dll), UnityPy and lz4, and res/.imported-source from the content import.
"""
import argparse
import ctypes
import functools
import re
import struct
import sys
from pathlib import Path

REPO = Path(__file__).resolve().parents[2]
BUNDLE = REPO / "res/.imported-source/manosaba_Data/StreamingAssets/aa/StandaloneWindows64/naninovel-spawn_assets_all.bundle"

# Per shader: the material cbuffer (cb1) and global cbuffer (cb0) registers the pixel program reads, the textures in
# t-register order, and the ShaderLab properties.  Layout follows the HLSL packing of the Shader Graph UnityPerMaterial
# cbuffer (property declaration order, textures first as `_ST` registers), checked against how each register is used.
GLOBALS = {
    (0, 4, "x"): "0.0",                 # _GlobalMipBias.x: only used as the sample_b bias
    (0, 4, "z"): "0.0",                 # _AlphaToMaskAvailable
}
SHADERS = {
    "Background_0Fix": {
        "name": "Manosaba/CutIn/Background",
        "time": (0, 130, "x"),
        "cb1": {(1, 0, "x"): "_Float", (1, 1, "x"): "_BackgroundA.x", (1, 1, "y"): "_BackgroundA.y", (1, 1, "z"): "_BackgroundA.z",
                (1, 2, "x"): "_BackgroundB.x", (1, 2, "y"): "_BackgroundB.y", (1, 2, "z"): "_BackgroundB.z"},
        "textures": [],
        "properties": ["_Float (\"Strength\", Float) = 0.3", "_BackgroundA (\"Background A\", Color) = (1,0,0.688,0)",
                       "_BackgroundB (\"Background B\", Color) = (0,0,0,0)"],
    },
    "Glasses_0Fix": {
        "name": "Manosaba/CutIn/Glasses",
        "time": (0, 19, "x"),
        "cb1": {(1, 4, "x"): "_Fader", (1, 4, "y"): "_Fader2", (1, 4, "z"): "_Speed", (1, 4, "w"): "_Tick",
                (1, 5, "x"): "_EdgeSize", (1, 6, "x"): "_EclipseFlame.x", (1, 6, "y"): "_EclipseFlame.y", (1, 6, "z"): "_EclipseFlame.z"},
        "textures": ["_MainTex", "_kirakira", "_kirakira2"],
        "properties": ["[PerRendererData] _MainTex (\"Glass\", 2D) = \"white\" {}", "_kirakira (\"Sparkle 1\", 2D) = \"white\" {}",
                       "_kirakira2 (\"Sparkle 2\", 2D) = \"white\" {}", "_Fader (\"Fader\", Float) = 1", "_Fader2 (\"Fader 2\", Float) = 1",
                       "_Speed (\"Speed\", Float) = 3", "_Tick (\"Tick\", Float) = 1", "_EdgeSize (\"Edge size\", Float) = 0.02",
                       "_EclipseFlame (\"Eclipse flame\", Color) = (1,0,0.94,0)"],
    },
    "Iuminescence_dezolve_0Fix": {
        "name": "Manosaba/CutIn/LuminescenceDissolve",
        "time": None,
        "cb1": {(1, 1, "x"): "_Color.x", (1, 1, "y"): "_Color.y", (1, 1, "z"): "_Color.z", (1, 2, "x"): "_Tick"},
        "textures": ["_MainTex"],
        "properties": ["[PerRendererData] _MainTex (\"Luminance\", 2D) = \"white\" {}", "_Color (\"Color\", Color) = (1,0.871,1,1)",
                       "_Tick (\"Tick\", Float) = 0"],
    },
    "Iuminescence_Silhouette_0Fix": {
        "name": "Manosaba/CutIn/LuminescenceSilhouette",
        "time": None,
        "cb1": {(1, 1, "x"): "_Color.x", (1, 1, "y"): "_Color.y", (1, 1, "z"): "_Color.z"},
        "textures": ["_MainTex"],
        "properties": ["[PerRendererData] _MainTex (\"Luminance\", 2D) = \"white\" {}", "_Color (\"Color\", Color) = (1,0.871,1,1)"],
    },
    "Shadow_Fix": {
        "name": "Manosaba/CutIn/Shadow",
        "time": None,
        "cb1": {(1, 0, "x"): "_Color.x", (1, 0, "y"): "_Color.y", (1, 0, "z"): "_Color.z"},
        "textures": ["_MainTex"],
        "properties": ["[PerRendererData] _MainTex (\"Silhouette\", 2D) = \"white\" {}", "_Color (\"Color\", Color) = (0.451,0.49,0.6,1)"],
    },
}


@functools.lru_cache(None)
def shipped_shaders():
    import UnityPy
    trees = {}
    for obj in UnityPy.load(str(BUNDLE)).objects:
        if obj.type.name == "Shader":
            tree = obj.read_typetree()
            trees[(tree.get("m_ParsedForm") or {}).get("m_Name") or ""] = tree
    return trees


# UnityEngine.Rendering.BlendMode
BLEND_FACTORS = ["Zero", "One", "DstColor", "SrcColor", "OneMinusDstColor", "SrcAlpha", "OneMinusSrcColor", "DstAlpha",
                 "OneMinusDstAlpha", "SrcAlphaSaturate", "OneMinusSrcAlpha"]


def forward_blend(shader_name):
    """ShaderLab `Blend` line of the shipped `Universal Forward` pass (e.g. Shadow_Fix multiplies: DstColor Zero)."""
    tree = shipped_shaders()["Shader Graphs/" + shader_name]
    for subshader in tree["m_ParsedForm"]["m_SubShaders"]:
        for shader_pass in subshader["m_Passes"]:
            state = shader_pass["m_State"]
            if state.get("m_Name") != "Universal Forward":
                continue
            blend = state["rtBlend0"]
            if int(blend["blendOp"]["val"]) != 0 or int(blend["blendOpAlpha"]["val"]) != 0:
                raise ValueError(shader_name + ": only additive blend ops are supported")
            factors = [BLEND_FACTORS[int(blend[key]["val"])] for key in ("srcBlend", "destBlend", "srcBlendAlpha", "destBlendAlpha")]
            return "Blend %s %s, %s %s" % tuple(factors)
    raise RuntimeError("no Universal Forward pass in " + shader_name)


def disassemble_pixel_program(shader_name):
    import lz4.block
    d3d = ctypes.WinDLL("d3dcompiler_47.dll")
    d3d.D3DDisassemble.argtypes = [ctypes.c_void_p, ctypes.c_size_t, ctypes.c_uint, ctypes.c_char_p, ctypes.POINTER(ctypes.c_void_p)]
    d3d.D3DDisassemble.restype = ctypes.c_long
    for name, tree in shipped_shaders().items():
        if name != "Shader Graphs/" + shader_name:
            continue
        platform = tree["platforms"].index(4)  # d3d11
        blob = bytes(tree["compressedBlob"])
        offset, compressed, size = tree["offsets"][platform][0], tree["compressedLengths"][platform][0], tree["decompressedLengths"][platform][0]
        data = lz4.block.decompress(blob[offset:offset + compressed], uncompressed_size=size)
        position = 0
        while True:
            at = data.find(b"DXBC", position)
            if at < 0:
                break
            length = struct.unpack_from("<I", data, at + 24)[0]
            code = data[at:at + length]
            buffer = ctypes.create_string_buffer(code, len(code))
            result = ctypes.c_void_p()
            if d3d.D3DDisassemble(buffer, len(code), 0x80, None, ctypes.byref(result)) != 0:
                raise RuntimeError("D3DDisassemble failed")
            vtable = ctypes.cast(ctypes.cast(result, ctypes.POINTER(ctypes.c_void_p))[0], ctypes.POINTER(ctypes.c_void_p))
            pointer = ctypes.WINFUNCTYPE(ctypes.c_void_p, ctypes.c_void_p)(vtable[3])
            length_of = ctypes.WINFUNCTYPE(ctypes.c_size_t, ctypes.c_void_p)(vtable[4])
            text = ctypes.string_at(pointer(result), length_of(result)).decode("latin1")
            if re.search(r"^ps_4_0", text, re.M) and "sample" in text or (re.search(r"^ps_4_0", text, re.M) and "o0" in text and "discard" in text):
                return text
            position = at + max(length, 4)
    raise RuntimeError("no pixel program for " + shader_name)


# ---- translation -----------------------------------------------------------------------------------------------

def split_operands(text):
    parts, depth, current = [], 0, ""
    for ch in text:
        if ch in "([":
            depth += 1
        elif ch in ")]":
            depth -= 1
        if ch == "," and depth == 0:
            parts.append(current.strip())
            current = ""
        else:
            current += ch
    if current.strip():
        parts.append(current.strip())
    return parts


def bits_to_float(bits):
    return struct.unpack("<f", struct.pack("<I", bits & 0xFFFFFFFF))[0]


def float_literal(bits):
    value = bits_to_float(bits)
    if value != value or value in (float("inf"), float("-inf")):
        return "asfloat(0x%08xu)" % (bits & 0xFFFFFFFF)
    text = "%.9g" % value
    if "." not in text and "e" not in text and "n" not in text:
        text += ".0"
    return "(%s)" % text


class Operand:
    def __init__(self, text):
        self.text = text
        self.neg = False
        self.abs = False
        body = text
        if body.startswith("-"):
            self.neg, body = True, body[1:]
        if body.startswith("|") and body.endswith("|"):
            self.abs, body = True, body[1:-1]
        self.body = body
        match = re.match(r"^(r|v|o|t|s)(\d+)(?:\.([xyzw]+))?$", body)
        if body == "null":
            self.kind = "null"
        elif body.startswith("l("):
            self.kind = "lit"
            self.values = [int(v.strip(), 16) for v in body[2:-1].split(",")]
        elif body.startswith("cb"):
            m = re.match(r"^cb(\d+)\[(\d+)\]\.([xyzw]+)$", body)
            self.kind, self.cb, self.reg, self.swz = "cb", int(m.group(1)), int(m.group(2)), m.group(3)
        elif match:
            self.kind, self.index, self.swz = match.group(1), int(match.group(2)), match.group(3) or ""
        else:
            raise ValueError("unsupported operand " + text)

    def letter(self, k):
        s = self.swz
        return s[k] if len(s) > 1 else s[0]


class Translator:
    def __init__(self, config):
        self.config = config
        self.lines = []
        self.temps = 0

    # operand component read as float ('f') or unsigned ('u') / signed ('i') integer
    def read(self, op, k, kind):
        if op.kind == "lit":
            bits = op.values[k] if len(op.values) > 1 else op.values[0]
            if kind == "f":
                expr = float_literal(bits)
            elif kind == "u":
                expr = "0x%08xu" % (bits & 0xFFFFFFFF)
            else:
                signed = bits if bits < 0x80000000 else bits - 0x100000000
                expr = "(%d)" % signed
        else:
            if op.kind == "r":
                # temps hold raw 32-bit patterns (uint4): integer values never pass through a float register,
                # where a GPU may flush denormals (every small integer is one).
                raw = "r%d.%s" % (op.index, op.letter(k))
                expr = {"f": "asfloat(%s)", "u": "%s", "i": "asint(%s)"}[kind] % raw
                return self.modify(op, expr, kind)
            if op.kind == "v":
                raw = "v%d.%s" % (op.index, op.letter(k))
            elif op.kind == "cb":
                key = (op.cb, op.reg, op.letter(k))
                raw = None
                if op.cb == 0 and key in GLOBALS:
                    raw = GLOBALS[key]
                elif self.config["time"] and key == tuple(self.config["time"]):
                    raw = "_Time.y"
                elif key in self.config["cb1"]:
                    raw = self.config["cb1"][key]
                if raw is None:
                    raise KeyError("unmapped constant cb%d[%d].%s" % key)
                if kind == "f":
                    expr = "(%s)" % raw
                elif kind == "u":
                    expr = "asuint(%s)" % raw
                else:
                    expr = "asint(%s)" % raw
                return self.modify(op, expr, kind)
            else:
                raise ValueError("cannot read " + op.text)
            if kind == "f":
                expr = raw
            elif kind == "u":
                expr = "asuint(%s)" % raw
            else:
                expr = "asint(%s)" % raw
        return self.modify(op, expr, kind)

    @staticmethod
    def modify(op, expr, kind):
        if op.abs:
            expr = "abs(%s)" % expr
        if op.neg:
            expr = "(-%s)" % expr if kind != "u" else "(0u - %s)" % expr
        return expr

    def emit(self, dst, mask_positions, producer, kind, sat=False):
        """producer(k) -> expression string for destination position k, in `kind` ('f' float, 'u' uint, 'i' int)."""
        for k in mask_positions:
            expr = producer(k)
            if sat and kind == "f":
                expr = "saturate(%s)" % expr
            target = ("r%d" if dst.kind == "r" else "o%d") % dst.index
            if dst.kind == "r":
                value = {"f": "asuint(%s)", "u": "%s", "i": "(uint)(%s)"}[kind] % expr
            else:
                value = {"f": "%s", "u": "asfloat(%s)", "i": "asfloat((uint)(%s))"}[kind] % expr
            self.lines.append("%s.%s = %s;" % (target, "xyzw"[k], value))

    def instruction(self, opcode, operands):
        sat = opcode.endswith("_sat")
        base = opcode[:-4] if sat else opcode
        ops = [Operand(o) for o in operands]
        dst = ops[0] if ops else None

        def positions(operand):
            return [i for i, ch in enumerate("xyzw") if ch in operand.swz]

        def f(i):
            return lambda k: self.read(ops[i], k, "f")

        unary = {"mov": "{0}", "frc": "frac({0})", "round_ni": "floor({0})", "log": "log2({0})", "exp": "exp2({0})",
                 "deriv_rtx": "ddx({0})", "deriv_rty": "ddy({0})", "sqrt": "sqrt({0})", "rsq": "rsqrt({0})"}
        binary = {"add": "({0} + {1})", "mul": "({0} * {1})", "div": "({0} / {1})", "max": "max({0}, {1})", "min": "min({0}, {1})"}
        if base in unary:
            self.emit(dst, positions(dst), lambda k: unary[base].format(self.read(ops[1], k, "f")), "f", sat)
        elif base in binary:
            self.emit(dst, positions(dst), lambda k: binary[base].format(self.read(ops[1], k, "f"), self.read(ops[2], k, "f")), "f", sat)
        elif base == "mad":
            self.emit(dst, positions(dst), lambda k: "(%s * %s + %s)" % (self.read(ops[1], k, "f"), self.read(ops[2], k, "f"), self.read(ops[3], k, "f")), "f", sat)
        elif base == "sincos":
            if ops[0].kind != "null":
                self.emit(ops[0], positions(ops[0]), lambda k: "sin(%s)" % self.read(ops[2], k, "f"), "f")
            if ops[1].kind != "null":
                self.emit(ops[1], positions(ops[1]), lambda k: "cos(%s)" % self.read(ops[2], k, "f"), "f")
        elif base == "iadd":
            self.emit(dst, positions(dst), lambda k: "(%s + %s)" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "imul":
            # imul hi, lo, a, b : only the low half is used here
            if ops[0].kind != "null":
                raise ValueError("imul high half is used")
            self.emit(ops[1], positions(ops[1]), lambda k: "(%s * %s)" % (self.read(ops[2], k, "u"), self.read(ops[3], k, "u")), "u")
        elif base == "xor":
            self.emit(dst, positions(dst), lambda k: "(%s ^ %s)" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "and":
            self.emit(dst, positions(dst), lambda k: "(%s & %s)" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "or":
            self.emit(dst, positions(dst), lambda k: "(%s | %s)" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "not":
            self.emit(dst, positions(dst), lambda k: "(~%s)" % self.read(ops[1], k, "u"), "u")
        elif base == "ushr":
            self.emit(dst, positions(dst), lambda k: "(%s >> (%s & 31u))" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "ishl":
            self.emit(dst, positions(dst), lambda k: "(%s << (%s & 31u))" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "u")), "u")
        elif base == "ftoi":
            self.emit(dst, positions(dst), lambda k: "(uint)(int)(%s)" % self.read(ops[1], k, "f"), "u")
        elif base == "ftou":
            self.emit(dst, positions(dst), lambda k: "(uint)(%s)" % self.read(ops[1], k, "f"), "u")
        elif base == "utof":
            self.emit(dst, positions(dst), lambda k: "((float)%s)" % self.read(ops[1], k, "u"), "f")
        elif base == "itof":
            self.emit(dst, positions(dst), lambda k: "((float)%s)" % self.read(ops[1], k, "i"), "f")
        elif base in ("lt", "ge", "ne", "eq"):
            symbol = {"lt": "<", "ge": ">=", "ne": "!=", "eq": "=="}[base]
            self.emit(dst, positions(dst), lambda k: "((%s %s %s) ? 0xFFFFFFFFu : 0u)" % (self.read(ops[1], k, "f"), symbol, self.read(ops[2], k, "f")), "u")
        elif base == "movc":
            self.emit(dst, positions(dst), lambda k: "((%s != 0u) ? %s : %s)" % (self.read(ops[1], k, "u"), self.read(ops[2], k, "f"), self.read(ops[3], k, "f")), "f")
        elif base == "discard_nz":
            self.lines.append("if (%s != 0u) discard;" % self.read(ops[0], 0, "u"))
        elif base == "sample_b" or base == "sample":
            texture = ops[2]
            sampler_index = texture.index
            names = self.config["textures"]
            if sampler_index >= len(names):
                raise KeyError("texture t%d is not mapped" % sampler_index)
            coord = "float2(%s, %s)" % (self.read(ops[1], 0, "f"), self.read(ops[1], 1, "f"))
            self.lines.append("{ float4 sampled = tex2D(%s, %s);" % (names[sampler_index], coord))
            for k in positions(dst):
                self.lines.append("  r%d.%s = asuint(sampled.%s);" % (dst.index, "xyzw"[k], texture.letter(k)) if dst.kind == "r"
                                  else "  o%d.%s = sampled.%s;" % (dst.index, "xyzw"[k], texture.letter(k)))
            self.lines.append("}")
        elif base == "ret":
            pass
        else:
            raise ValueError("unsupported opcode " + opcode)

    def translate(self, text):
        for raw in text.splitlines():
            line = raw.replace(chr(0), "").strip()
            if not line or line.startswith("//"):
                continue
            if re.match(r"^(ps_4_0|dcl_|ret)", line):
                if line.startswith("dcl_temps"):
                    self.temps = int(line.split()[1])
                continue
            opcode, _, rest = line.partition(" ")
            if not opcode.strip():
                continue
            self.instruction(opcode, split_operands(rest))
        return self.lines


TEMPLATE = """// GENERATED by tools/unity-rebuild/port_cutin_shaders.py from the shipped Shader Graph `{source}` (URP, Unlit, transparent).
// Only the compiled DX11 program exists in the build, so this is a literal translation of its pixel program.  Do not
// edit by hand; regenerate with the tool.  Property names match the shipped ones so the original Timeline curves
// (material._Tick, _Fader, _Fader2, _Float) keep driving it.
Shader "{name}"
{{
    Properties
    {{
{properties}
    }}

    SubShader
    {{
        Tags {{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True" }}
        Cull Off
        Lighting Off
        ZWrite Off
        {blend}

        Pass
        {{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            }};

            struct v2f
            {{
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            }};

{uniforms}

            v2f vert(appdata v)
            {{
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }}

            fixed4 frag(v2f input) : SV_Target
            {{
                float4 v1 = float4(input.uv, 0, 0);
                float4 o0 = float4(0, 0, 0, 0);
{temps}
{body}
                return (fixed4)o0;
            }}
            ENDCG
        }}
    }}
}}
"""


def generate(shader_name, config):
    text = disassemble_pixel_program(shader_name)
    translator = Translator(config)
    body = translator.translate(text)
    uniforms = []
    for prop in config["properties"]:
        match = re.match(r"^(?:\[[^\]]+\]\s*)?(\w+)\s*\(\"[^\"]*\",\s*(\w+)", prop)
        pname, ptype = match.group(1), match.group(2)
        uniforms.append({"2D": "            sampler2D %s;", "Float": "            float %s;", "Color": "            float4 %s;"}[ptype] % pname)
    temps = "\n".join("                uint4 r%d = uint4(0, 0, 0, 0);" % i for i in range(translator.temps))
    return TEMPLATE.format(source="Shader Graphs/" + shader_name, name=config["name"], blend=forward_blend(shader_name),
                           properties="\n".join("        " + p for p in config["properties"]),
                           uniforms="\n".join(uniforms), temps=temps, body="\n".join("                " + line for line in body)), text


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--out", default=str(REPO / "UnityProject/Assets/Shaders/ManosabaCutIn"))
    parser.add_argument("--only", action="append", default=[])
    args = parser.parse_args()
    out = Path(args.out)
    out.mkdir(parents=True, exist_ok=True)
    for source, config in SHADERS.items():
        if args.only and source not in args.only:
            continue
        shader, disassembly = generate(source, config)
        target = out / (config["name"].split("/")[-1] + ".shader")
        target.write_text(shader, encoding="utf-8", newline="\r\n")
        print("wrote", target, len(shader.splitlines()), "lines from", len(disassembly.splitlines()), "disassembly lines")


if __name__ == "__main__":
    sys.exit(main())
