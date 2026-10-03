Shader "Manosaba/Prototype Lit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.15
        _Metallic ("Metallic", Range(0,1)) = 0.05
        _Cutoff ("Alpha cutoff", Range(0,1)) = 0.35
        [Toggle] _AlphaClip ("Alpha clip", Float) = 0
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
        [HideInInspector] _SrcBlend ("Source blend", Float) = 1
        [HideInInspector] _DstBlend ("Destination blend", Float) = 0
        [HideInInspector] _ZWrite ("Depth write", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }
        Cull [_Cull]
        Blend [_SrcBlend] [_DstBlend]
        ZWrite [_ZWrite]
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows addshadow keepalpha
        #pragma target 3.0
        sampler2D _MainTex;
        fixed4 _Color;
        half _Glossiness;
        half _Metallic;
        float _Cutoff;
        float _AlphaClip;
        struct Input { float2 uv_MainTex; };
        void surf(Input input, inout SurfaceOutputStandard output)
        {
            fixed4 color = tex2D(_MainTex, input.uv_MainTex) * _Color;
            if (_AlphaClip > 0.5) clip(color.a - _Cutoff);
            output.Albedo = color.rgb;
            output.Alpha = color.a;
            output.Metallic = _Metallic;
            output.Smoothness = _Glossiness;
        }
        ENDCG
    }
    Fallback "Standard"
}
