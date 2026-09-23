// RECONSTRUCTED | Confidence: A | shipped shader "Naninovel Extender/Maskable Transparent" (resources.assets
// path_id 22, res/Uncategorized/Shader/pathid_22.json).
// Evidence: work/shaders/Naninovel_Extender_Maskable_Transparent.txt (tools/unity-rebuild/dump_shipped_shaders.py).
//
// The shader of the shipped material "MaskableTransparent", which BackgroundsConfiguration (MainBackground, Stills,
// Tricks) and 22 CharactersConfiguration entries assign as ActorMetadata.CustomSpriteMaterial - i.e. what Naninovel's
// TransitionalSpriteRenderer draws those actors with instead of its own "Hidden/Naninovel/Transparent".
// It is that Naninovel shader plus a stencil test:
//   * properties and tags from the parsed form; pass state ZWrite Off, Blend One OneMinusSrcAlpha, Cull Back
//     (default), Stencil { Ref 1  Comp [_StencilComp]  Pass/Fail/ZFail Keep } - the switch
//     IMaskabilityChangeableActor.ChangeMaskability flips through _StencilComp;
//   * vertex: ObjectToClip, uv = TRANSFORM_TEX(uv, _MainTex); fragment: rgb = tex.rgb * _Opacity, a = tex.a
//     (the render texture already holds premultiplied content).  One variant, no keywords but stereo.
Shader "Naninovel Extender/Maskable Transparent"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Opacity ("Opacity", Float) = 0
        _StencilComp ("Stencil Comparison", Float) = 8
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
                color.rgb *= _Opacity;
                return color;
            }
            ENDCG
        }
    }
}
