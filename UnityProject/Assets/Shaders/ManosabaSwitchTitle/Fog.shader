// A built-in-pipeline fog approximation using the shipped material parameters.
// Original Switch Shader Graph binaries do not reveal the original graph topology.
Shader "Manosaba/SwitchTitle/Fog"
{
    Properties
    {
        _Color ("Source Fog Color", Color) = (1,1,1,1)
        [HideInInspector] _EpilogueOpacity ("Epilogue Reveal Opacity", Range(0,1)) = 1
        _Scale ("Source UV Scale", Vector) = (1,1,0,0)
        _Float ("Source Fog Amount", Float) = 0.05
        _Float_1 ("Source Detail Amount", Float) = 0.2
        _fineness ("Source Fineness", Float) = 60
        _Time00 ("Source Horizontal Speed", Float) = 0.2
        _SourceTime ("Source Vertical Speed", Float) = 0.2
        [HideInInspector] _RendererColor ("Renderer Color", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off Lighting Off ZWrite Off
        // Source Universal Forward pass: src=5,dst=10,srcAlpha=1,dstAlpha=10.
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"
            #include "TitleNoise.cginc"
            float4 _Scale;
            float _Float, _Float_1, _fineness, _Time00, _SourceTime;
            float _EpilogueOpacity;
            fixed4 frag(v2f input) : SV_Target
            {
                float2 uv = input.texcoord * max(abs(_Scale.xy), 0.001);
                float2 drift = _Time.y * float2(_Time00, _SourceTime) * 0.08;
                float cloud = TitleCloud(uv * max(_fineness / 12.0, 1.0) + drift);
                float detail = TitleNoise(uv * max(_fineness, 1.0) - drift);
                // Source shader has no texture/sampler bindings: the Sprite supplies geometry
                // and UV only. Sampling it would overlay CG1 on the other title backgrounds.
                fixed4 color = input.color;
                color.a *= saturate(_Float * (cloud + detail * _Float_1));
                color.a *= _EpilogueOpacity;
                return color;
            }
            ENDCG
        }
    }
}
