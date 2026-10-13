// Compatibility dissolve retaining the dumped radial texture and Timeline _Tick.
Shader "Manosaba/SwitchStory/GlassBreakRadial"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        [PerRendererData] _MainTex ("Glass", 2D) = "white" {}
        [HDR] _Color ("Light", Color) = (1,0.87,1,1)
        _Tick ("Dissolve", Range(0,1)) = 0
        _EclipseFlame ("Edge", Color) = (1,1,1,0.3)
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
            float4 _Color, _EclipseFlame;
            float _Tick;
            float4 frag(StoryVarying i) : SV_Target
            {
                float4 src = tex2D(_MainTex, i.uv);
                float luminance = dot(src.rgb, float3(0.2126,0.7152,0.0722));
                // Dumped Timeline values are a short 0 -> 1.2 -> 0 pulse.
                // Zero is the inactive state, and positive values reveal a radial
                // fracture. This is a compatibility model, not the NVN formula.
                float progress = max(_Tick, 0);
                float radius = length(i.uv - 0.5) * 2;
                float visible = (1 - smoothstep(progress-0.025, progress+0.025, radius))
                              * step(0.0001, progress);
                float edge = (1 - smoothstep(0, 0.035, abs(radius-progress))) * visible;
                // Source map has an opaque alpha channel; its black RGB is empty
                // coverage, not a black panel. Keep the verified straight-alpha blend.
                return float4(src.rgb*_Color.rgb + edge*_EclipseFlame.rgb,
                              saturate(luminance)*src.a*_Color.a*visible) * i.color;
            }
            ENDCG
        }
    }
}
