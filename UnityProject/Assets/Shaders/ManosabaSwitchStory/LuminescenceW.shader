// Compatibility implementation for the Switch W silhouette. The recovered PC
// sibling uses red-channel coverage; the W Forward program also reads opacity.
// This is not a recovered NVN pixel program.
Shader "Manosaba/SwitchStory/LuminescenceW"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        [PerRendererData] _MainTex ("Light", 2D) = "white" {}
        _Color ("Tint", Color) = (1,0.87,1,1)
        // Retained for serialized material compatibility; the source W Forward
        // program does not read this property.
        _Tick ("Unused source property", Range(0,1)) = 0
        _transparent ("Opacity", Range(0,1)) = 1
        [HideInInspector] _SpriteLocalRect ("Local bounds", Vector) = (0,0,1,1)
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
            sampler2D _MainTex;
            float4 _Color;
            float _transparent;
            float4 frag(StoryVarying i) : SV_Target
            {
                float4 src = tex2D(_MainTex, i.uv);
                // These SpriteRenderer textures contain opaque black around
                // the glow. Texture alpha would draw that black over the scene.
                return float4(src.rgb*_Color.rgb,src.r*saturate(_transparent))*i.color;
            }
            ENDCG
        }
    }
}
