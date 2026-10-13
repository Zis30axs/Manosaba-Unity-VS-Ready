// Source multiply blend and tint, adapted to the built-in sprite pipeline.
Shader "Manosaba/SwitchStory/CharaShadow"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        [PerRendererData] _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Shadow", Color) = (0.415686,0.439216,0.533333,1)
        _Float ("Shadow strength", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off ZWrite Off Lighting Off
        Blend DstColor Zero, Zero One
        Pass
        {
            CGPROGRAM
            #pragma vertex StoryVert
            #pragma multi_compile_instancing
            #pragma fragment frag
            #include "StorySprite.cginc"
            sampler2D _MainTex;
            float4 _Color;
            float _Float;
            float4 frag(StoryVarying i) : SV_Target
            {
                float4 src = tex2D(_MainTex, i.uv);
                float a = src.a * i.color.a * _Color.a * saturate(_Float);
                return float4(lerp(1, _Color.rgb * i.color.rgb, a), a);
            }
            ENDCG
        }
    }
}
