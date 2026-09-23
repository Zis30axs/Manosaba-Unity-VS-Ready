// RECONSTRUCTED | Confidence: A | shipped shader "Naninovel Extender/Default" (in every layered-character bundle,
// e.g. alisa.bundle path_id of res/Assets/#WitchTrials/Prefabs/Naninovel/Characters/LayeredCharacters/Alisa~114.json).
// Evidence: work/shaders/Naninovel_Extender_Default.txt (tools/unity-rebuild/dump_shipped_shaders.py).
//
// The player build keeps no source, but it keeps the parsed ShaderLab form and the GLCore programs as GLSL text:
//   * properties, flags ([PerRendererData] / [HideInInspector] / [MaterialToggle]), defaults and tags below are
//     the parsed form's, one for one;
//   * pass state: Blend One OneMinusSrcAlpha, Cull Off, ZWrite Off, Lighting Off, and a stencil block whose every
//     value is bound to a material property (Ref/ReadMask/WriteMask/Comp/Pass);
//   * variants: (), ETC1_EXTERNAL_ALPHA, PIXELSNAP_ON (local), both - the vertex programs are Unity's SpriteVert
//     (UnityFlipSprite, ObjectToClip, color = vertex * _Color * _RendererColor, UnityPixelSnap) and the sampler is
//     SampleSpriteTexture (_AlphaTex / _EnableExternalAlpha), i.e. UnitySprites.cginc;
//   * fragment (GLSL program 0): c = tex * color; discard when c.a - _Cutoff < 0; rgb *= a.
// The mask / masked layer materials (Naninovel_Default#Mask_Ref1_CutoffLow, #Masked_Ref2, ...) are this shader with
// different stencil and cutoff values; without it every character layer drew unmasked with the default sprite material.
Shader "Naninovel Extender/Default"
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
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                clip(c.a - _Cutoff);
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}
