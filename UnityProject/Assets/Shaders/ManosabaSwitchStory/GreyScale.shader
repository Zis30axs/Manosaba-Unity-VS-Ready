// Compatibility curve-texture color mapping; source textures/animated properties are retained.
Shader "Manosaba/SwitchStory/GreyScale"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        [PerRendererData] _MainTex ("Sprite", 2D) = "white" {}
        _CurveTexture ("Color curve", 2D) = "white" {}
        _CurveIntensity ("Curve intensity", Float) = 0
        _Opacity ("Opacity", Float) = 1
        _Color ("Overlay", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off ZWrite Off Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex StoryVert
            #pragma multi_compile_instancing
            #pragma fragment frag
            #include "StorySprite.cginc"
            sampler2D _MainTex, _CurveTexture;
            float _CurveIntensity, _Opacity;
            float4 _Color;
            float4 frag(StoryVarying i) : SV_Target
            {
                float4 src = tex2D(_MainTex, i.uv);
                float luminance = dot(src.rgb, float3(0.2126,0.7152,0.0722));
                float3 mapped = tex2D(_CurveTexture, float2(luminance,0.5)).rgb;
                src.rgb = lerp(src.rgb, mapped, saturate(_CurveIntensity));
                src.rgb = lerp(src.rgb, _Color.rgb, saturate(_Color.a));
                src.a *= saturate(_Opacity);
                return src * i.color;
            }
            ENDCG
        }
    }
}
