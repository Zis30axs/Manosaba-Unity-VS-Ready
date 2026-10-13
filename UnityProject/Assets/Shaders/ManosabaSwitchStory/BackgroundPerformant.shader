// Compatibility implementation using the Switch material's baked noise inputs.
// Coverage follows the recovered PC Background_0Fix sibling: noise supplies
// alpha and the threshold clips pixels. The Switch noise formula remains an
// approximation; the threshold must not turn the entire overlay opaque.
Shader "Manosaba/SwitchStory/BackgroundPerformant"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        _Float ("Threshold", Range(0,1)) = 0.3
        _BackgroundA ("Background A", Color) = (1,0,0.69,0)
        _BackgroundB ("Background B", Color) = (0,0,0,0)
        _SampleTexture2D_dc174d58386b461198496220d778838d_Texture_1_Texture2D ("Noise A", 2D) = "gray" {}
        _SampleTexture2D_9c9da0999f3640699de91a9910aeb546_Texture_1_Texture2D ("Noise B", 2D) = "gray" {}
        [HideInInspector] _SpriteLocalRect ("Local bounds", Vector) = (0,0,0,0)
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
            sampler2D _SampleTexture2D_dc174d58386b461198496220d778838d_Texture_1_Texture2D;
            sampler2D _SampleTexture2D_9c9da0999f3640699de91a9910aeb546_Texture_1_Texture2D;
            float _Float;
            float4 _BackgroundA, _BackgroundB;
            float4 frag(StoryVarying i) : SV_Target
            {
                float t = _UnscaledTime*0.06;
                float2 uv = (_SpriteLocalRect.z > 0 && _SpriteLocalRect.w > 0) ? i.local : i.uv;
                float a = tex2D(_SampleTexture2D_dc174d58386b461198496220d778838d_Texture_1_Texture2D,
                    frac(uv*2+float2(t,-t))).r;
                float b = tex2D(_SampleTexture2D_9c9da0999f3640699de91a9910aeb546_Texture_1_Texture2D,
                    frac(uv*3+float2(-t*0.7,t*0.3)+a*0.2)).r;
                float value = a*0.6+b*0.4;
                clip(value - _Float);
                return float4(lerp(_BackgroundB.rgb,_BackgroundA.rgb,value),saturate(value))*i.color;
            }
            ENDCG
        }
    }
}
