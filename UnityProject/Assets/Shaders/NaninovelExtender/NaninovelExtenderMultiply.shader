// RECONSTRUCTED | Confidence: A | shipped shader "Naninovel Extender/Multiply" (in every layered-character bundle,
// e.g. res/Assets/#WitchTrials/Prefabs/Naninovel/Characters/LayeredCharacters/Alisa~57.json).
// Evidence: work/shaders/Naninovel_Extender_Multiply.txt (tools/unity-rebuild/dump_shipped_shaders.py).
//
// Identical to "Naninovel Extender/Default" (same properties, tags, stencil block, variants, vertex program and the
// same fragment program 0: c = tex * color; clip(c.a - _Cutoff); rgb *= a) except the blend:
//   Blend DstColor OneMinusSrcAlpha, DstColor OneMinusSrcAlpha   BlendOp Add, Max
// With the premultiplied output that is dst * (1 - a + a * rgb): a true multiply at the layer's opacity.  The
// character shading layers (Naninovel_Multiply, #Masked_Ref1/2, #Mask_Ref2_CutoffMin) use it; drawn with the
// default sprite material they were painted over the skin as flat grey instead of darkening it.
Shader "Naninovel Extender/Multiply"
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
        Blend DstColor OneMinusSrcAlpha, DstColor OneMinusSrcAlpha
        BlendOp Add, Max

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
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                clip(c.a - _Cutoff);
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}
