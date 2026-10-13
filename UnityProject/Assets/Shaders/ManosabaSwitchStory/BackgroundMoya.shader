// Procedural fog compatibility approximation driven by the original material values.
Shader "Manosaba/SwitchStory/BackgroundMoya"
{
    Properties
    {
        [HideInInspector] _RendererColor ("Renderer tint", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Sprite flip", Vector) = (1,1,1,1)
        _FogAmount ("Fog amount", Range(0,1)) = 0.3
        _FogColorA ("Fog A", Color) = (1,0,0.69,0)
        _FogColorB ("Fog B", Color) = (0,0,0,0)
        _SwirlSpeed ("Swirl speed", Float) = 0.02
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off ZWrite Off Lighting Off
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        Pass
        {
            Name "MOYA"
            CGPROGRAM
            #pragma vertex StoryVert
            #pragma multi_compile_instancing
            #pragma fragment frag
            #include "StorySprite.cginc"
            float _FogAmount, _SwirlSpeed;
            float4 _FogColorA, _FogColorB;
            float4 frag(StoryVarying i) : SV_Target
            {
                float t = _UnscaledTime*_SwirlSpeed;
                float2 p = (i.uv-0.5)*float2(10,6);
                float warp = StoryCloud(p*0.45 + float2(t,-t));
                float cloud = StoryCloud(p + warp*2 + float2(t*1.7,t*0.7));
                float alpha = smoothstep(_FogAmount-0.12,_FogAmount+0.12,cloud);
                return float4(lerp(_FogColorB.rgb,_FogColorA.rgb,cloud),alpha)*i.color;
            }
            ENDCG
        }
    }
}
