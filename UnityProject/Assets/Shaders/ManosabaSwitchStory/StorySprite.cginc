// Built-in compatibility helpers. The Switch build contains NVN shader binaries,
// not Shader Graph source; the new shader algorithms are explicit approximations.
#include "UnityCG.cginc"

// Match Unity's SpriteRenderer per-draw tint/flip protocol without treating
// shader-specific _Color (sometimes an overlay) as the renderer color.
#ifdef UNITY_INSTANCING_ENABLED
UNITY_INSTANCING_BUFFER_START(PerDrawSprite)
    UNITY_DEFINE_INSTANCED_PROP(fixed4, unity_SpriteRendererColorArray)
    UNITY_DEFINE_INSTANCED_PROP(fixed2, unity_SpriteFlipArray)
UNITY_INSTANCING_BUFFER_END(PerDrawSprite)
#define _RendererColor UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteRendererColorArray)
#define _Flip UNITY_ACCESS_INSTANCED_PROP(PerDrawSprite, unity_SpriteFlipArray)
#endif
CBUFFER_START(UnityPerDrawSprite)
#ifndef UNITY_INSTANCING_ENABLED
fixed4 _RendererColor;
fixed2 _Flip;
#endif
float _EnableExternalAlpha;
CBUFFER_END
struct StoryInput { float4 vertex : POSITION; float2 uv : TEXCOORD0; float4 color : COLOR; UNITY_VERTEX_INPUT_INSTANCE_ID };
struct StoryVarying { float4 pos : SV_POSITION; float2 uv : TEXCOORD0; float4 color : COLOR; float2 local : TEXCOORD1; UNITY_VERTEX_OUTPUT_STEREO };
float4 _SpriteLocalRect;
float _UnscaledTime;
StoryVarying StoryVert(StoryInput v)
{
    UNITY_SETUP_INSTANCE_ID(v);
    StoryVarying o;
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    o.pos = UnityObjectToClipPos(float4(v.vertex.xy * _Flip, v.vertex.zw));
    o.uv = v.uv;
    o.color = v.color * _RendererColor;
    o.local = (v.vertex.xy - _SpriteLocalRect.xy) * _SpriteLocalRect.zw;
    return o;
}
StoryVarying StoryParticleVert(StoryInput v)
{
    UNITY_SETUP_INSTANCE_ID(v);
    StoryVarying o;
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    o.color = v.color;
    o.local = v.uv;
    return o;
}
float StoryHash(float2 p)
{
    p = frac(p * float2(123.34, 456.21));
    p += dot(p, p + 45.32);
    return frac(p.x * p.y);
}
float StoryNoise(float2 p)
{
    float2 i = floor(p), f = frac(p), u = f*f*(3-2*f);
    return lerp(lerp(StoryHash(i), StoryHash(i+float2(1,0)), u.x),
                lerp(StoryHash(i+float2(0,1)), StoryHash(i+1), u.x), u.y);
}
float StoryCloud(float2 p)
{
    return StoryNoise(p)*0.57 + StoryNoise(p*2.03)*0.28 + StoryNoise(p*4.11)*0.15;
}
