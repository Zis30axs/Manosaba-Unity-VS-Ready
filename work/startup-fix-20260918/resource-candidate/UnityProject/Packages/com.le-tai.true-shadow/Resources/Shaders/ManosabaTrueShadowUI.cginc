// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
#include "UnityCG.cginc"
#include "UnityUI.cginc"
sampler2D _MainTex;
float4 _MainTex_ST, _ClipRect;
fixed4 _Color, _TextureSampleAdd;
struct input_t { float4 vertex:POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; UNITY_VERTEX_INPUT_INSTANCE_ID };
struct output_t { float4 vertex:SV_POSITION; fixed4 color:COLOR; float2 uv:TEXCOORD0; float4 local:TEXCOORD1; UNITY_VERTEX_OUTPUT_STEREO };
output_t vert(input_t v) {
 output_t o; UNITY_SETUP_INSTANCE_ID(v); UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
 o.local=v.vertex; o.vertex=UnityObjectToClipPos(v.vertex); o.uv=TRANSFORM_TEX(v.uv,_MainTex); o.color=v.color*_Color; return o;
}
fixed4 frag(output_t i):SV_Target {
 fixed4 c=(tex2D(_MainTex,i.uv)+_TextureSampleAdd)*i.color;
 #ifdef UNITY_UI_CLIP_RECT
 c.a*=UnityGet2DClipping(i.local.xy,_ClipRect);
 #endif
 #ifdef UNITY_UI_ALPHACLIP
 clip(c.a-0.001);
 #endif
 #ifdef SHADOW_SCREEN
 c.rgb*=c.a;
 #endif
 #ifdef SHADOW_MULTIPLY
 c.rgb=lerp(fixed3(1,1,1),c.rgb,c.a);
 #endif
 return c;
}
