// Particle compatibility pass; the importer restores the source blend factors.
Shader "Manosaba/SwitchStory/Particles"
{
    Properties
    {
        _MainTex ("Particle", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Source blend", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Destination blend", Float) = 10
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendAlpha ("Source alpha blend", Float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlendAlpha ("Destination alpha blend", Float) = 10
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        Cull Off ZWrite Off Lighting Off
        Blend [_SrcBlend] [_DstBlend], [_SrcBlendAlpha] [_DstBlendAlpha]
        Pass
        {
            CGPROGRAM
            #pragma vertex StoryParticleVert
            #pragma fragment frag
            #include "StorySprite.cginc"
            sampler2D _MainTex;
            float4 _Color, _MainTex_ST;
            float4 frag(StoryVarying i) : SV_Target
            {
                return tex2D(_MainTex, i.uv*_MainTex_ST.xy+_MainTex_ST.zw)*_Color*i.color;
            }
            ENDCG
        }
    }
}
