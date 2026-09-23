// RECONSTRUCTED | Confidence: A | shipped shader "Naninovel Extender/Maskable Transparent Toned" (resources.assets
// path_id 32, res/Uncategorized/Shader/pathid_32.json).
// Evidence: work/shaders/Naninovel_Extender_Maskable_Transparent_Toned.txt (tools/unity-rebuild/dump_shipped_shaders.py).
//
// The shader of the shipped material "MaskableTransparentToned_1" (_ToneTex = resources.assets "ToneCurve" 256x4 R8,
// _ToneIntensity 0.5), which CharactersConfiguration assigns to Ema, Hiro, Meruru, Sherry, AnAn, Leia, Coco, Margo
// and Nanoka as ActorMetadata.CustomSpriteMaterial.  "Naninovel Extender/Maskable Transparent" plus a tone curve:
//   lin = GammaToLinearSpace(tex.rgb)   (the GLSL's x * (x * (x * 0.305306 + 0.682171) + 0.0125229), UnityCG)
//   tone = (_ToneTex(lin.r, 0).r, _ToneTex(lin.g, 0).r, _ToneTex(lin.b, 0).r)
//   rgb = lerp(tex.rgb, tone * tex.a, _ToneIntensity);  return (rgb * _Opacity, tex.a)
// Same pass state as the untoned shader (ZWrite Off, Blend One OneMinusSrcAlpha, Stencil { Ref 1 Comp [_StencilComp] }).
Shader "Naninovel Extender/Maskable Transparent Toned"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Opacity ("Opacity", Float) = 0
        _StencilComp ("Stencil Comparison", Float) = 8
        [NoScaleOffset] _ToneTex ("Tone Texture", 2D) = "white" {}
        _ToneIntensity ("Tone Intensity", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        Stencil
        {
            Ref 1
            Comp [_StencilComp]
        }

        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"

            #pragma target 2.0
            #pragma vertex ComputeVertex
            #pragma fragment ComputeFragment

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Opacity;
            sampler2D _ToneTex;
            float _ToneIntensity;

            struct VertexInput
            {
                float4 Vertex : POSITION;
                float2 TexCoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VertexOutput
            {
                float4 Vertex : SV_POSITION;
                float2 TexCoord : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            VertexOutput ComputeVertex (VertexInput vertexInput)
            {
                VertexOutput vertexOutput;
                UNITY_SETUP_INSTANCE_ID(vertexInput);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(vertexOutput);
                vertexOutput.Vertex = UnityObjectToClipPos(vertexInput.Vertex);
                vertexOutput.TexCoord = TRANSFORM_TEX(vertexInput.TexCoord, _MainTex);
                return vertexOutput;
            }

            fixed4 ComputeFragment (VertexOutput vertexOutput) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, vertexOutput.TexCoord);
                float3 linearColor = GammaToLinearSpace(color.rgb);
                float3 tone = float3(
                    tex2D(_ToneTex, float2(linearColor.r, 0)).r,
                    tex2D(_ToneTex, float2(linearColor.g, 0)).r,
                    tex2D(_ToneTex, float2(linearColor.b, 0)).r);
                color.rgb = lerp(color.rgb, tone * color.a, _ToneIntensity);
                color.rgb *= _Opacity;
                return color;
            }
            ENDCG
        }
    }
}
