// Conservative grain overlay, not a claim of exact Shader Graph equivalence.
Shader "Manosaba/SwitchTitle/Noise"
{
    Properties
    {
        _Color ("Source Optional Tint", Color) = (0,0,0,0)
        [HideInInspector] _EpilogueOpacity ("Epilogue Reveal Opacity", Range(0,1)) = 1
        _Scale ("Source UV Scale", Vector) = (1,1,0,0)
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
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnitySprites.cginc"
            #include "TitleNoise.cginc"
            float4 _Scale;
            float _EpilogueOpacity;
            v2f vert(appdata_t input)
            {
                v2f output = SpriteVert(input);
                // ED3's saved _Color is transparent black. Treat it as optional tint,
                // leaving renderer alpha available for the source overlay.
                output.color = input.color * _RendererColor;
                return output;
            }
            fixed4 frag(v2f input) : SV_Target
            {
                float2 uv = input.texcoord * max(abs(_Scale.xy), 0.001);
                float grain = TitleHash(floor(uv * 720.0) + floor(_Time.y * 12.0));
                // The source program declares no texture/sampler bindings. Its Sprite is a quad,
                // not an input image; RGB and alpha must not sample the unrelated CG1 texture.
                fixed4 color = input.color;
                color.rgb = lerp(color.rgb, _Color.rgb, saturate(_Color.a));
                color.a *= grain * 0.06;
                color.a *= _EpilogueOpacity;
                return color;
            }
            ENDCG
        }
    }
}
