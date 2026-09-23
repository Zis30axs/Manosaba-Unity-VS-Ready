// RECONSTRUCTED | Confidence: A | shipped shader "Naninovel Extender/Softlight" (in the layered-character bundles,
// e.g. res/Assets/#WitchTrials/Prefabs/Naninovel/Characters/LayeredCharacters/Alisa~107.json).
// Evidence: work/shaders/Naninovel_Extender_Softlight.txt (tools/unity-rebuild/dump_shipped_shaders.py).
//
// Properties, tags, stencil block, blend (One OneMinusSrcAlpha), variants and vertex program as
// "Naninovel Extender/Default".  Soft light computed in the fragment with the tint (vertex color) as the base layer
// c and the texture as the blend layer s (GLSL program 0):
//   clip(tex.a * color.a - _Cutoff)
//   lo = 2 s c + s^2 (1 - 2c);  hi = 2 s (1 - c) + sqrt(s) (2c - 1);  rgb = c < 0.5 ? lo : hi (per channel)
//   a = tex.a * color.a;  return (rgb * a, a)
Shader "Naninovel Extender/Softlight"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _StencilRef ("Stencil Reference", Float) = 0
        _ReadMask ("Read Mask", Float) = 255
        _WriteMask ("Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _Cutoff ("Mask Alpha Cutoff", Range(0, 1)) = 0
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Stencil
        {
            Ref [_StencilRef]
            ReadMask [_ReadMask]
            WriteMask [_WriteMask]
            Comp [_StencilComp]
            Pass [_StencilOp]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"

            float _Cutoff;

            fixed4 frag (v2f IN) : SV_Target
            {
                fixed4 tex = SampleSpriteTexture(IN.texcoord);
                fixed a = tex.a * IN.color.a;
                clip(a - _Cutoff);
                fixed3 s = tex.rgb;
                fixed3 c = IN.color.rgb;
                fixed3 lo = 2 * s * c + s * s * (1 - 2 * c);
                fixed3 hi = 2 * s * (1 - c) + sqrt(s) * (2 * c - 1);
                fixed3 rgb = c < 0.5 ? lo : hi;
                return fixed4(rgb * a, a);
            }
        ENDCG
        }
    }
}
