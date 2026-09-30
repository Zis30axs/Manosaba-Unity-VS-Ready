from pathlib import Path
import hashlib, struct, zlib, random
BASE=Path(__file__).resolve().parent/'resource-candidate'
RES=BASE/'UnityProject/Packages/com.le-tai.true-shadow/Resources'
S=RES/'Shaders'
def write(path,text):
 path.parent.mkdir(parents=True,exist_ok=True);path.write_text(text,encoding='utf-8',newline='\n')
header='// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.\n'
write(S/'ManosabaTrueShadowUI.cginc',header+'''#include "UnityCG.cginc"
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
''')
for name,blend,define in [('Normal','SrcAlpha OneMinusSrcAlpha',''),('Additive','SrcAlpha One',''),('Screen','One OneMinusSrcColor','SHADOW_SCREEN'),('Multiply','DstColor Zero','SHADOW_MULTIPLY')]:
 write(S/f'TrueShadow-{name}.shader',header+'''Shader "UI/TrueShadow-'''+name+'''" {
 Properties {
  [PerRendererData] _MainTex ("Shadow", 2D) = "white" {}
  _Color ("Tint", Color) = (1,1,1,1)
  _StencilComp ("Stencil Comparison", Float) = 8
  _Stencil ("Stencil ID", Float) = 0
  _StencilOp ("Stencil Operation", Float) = 0
  _StencilWriteMask ("Stencil Write Mask", Float) = 255
  _StencilReadMask ("Stencil Read Mask", Float) = 255
  _ColorMask ("Color Mask", Float) = 15
  [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Alpha Clip", Float) = 0
 }
 SubShader {
  Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "CanUseSpriteAtlas"="True" }
  Stencil { Ref [_Stencil] Comp [_StencilComp] Pass [_StencilOp] ReadMask [_StencilReadMask] WriteMask [_StencilWriteMask] }
  Cull Off Lighting Off ZWrite Off ZTest [unity_GUIZTestMode]
  Blend '''+blend+'''
  ColorMask [_ColorMask]
  Pass {
   CGPROGRAM
   #pragma vertex vert
   #pragma fragment frag
   #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
   #pragma multi_compile_local _ UNITY_UI_ALPHACLIP
'''+('   #define '+define+'\n' if define else '')+'''   #include "ManosabaTrueShadowUI.cginc"
   ENDCG
  }
 }
}
''')
write(S/'ManosabaTrueShadowBlit.cginc',header+'''#include "UnityCG.cginc"
sampler2D _MainTex, _BlueNoise, _ShadowTex;
float4 _MainTex_TexelSize, _CropRegion, _TargetSize;
float _Radius, _OverflowAlpha, _AlphaMultiplier;
float2 _Offset;
float _Weights[64], _Offsets[64];
int _Extent;
fixed4 Blur(float2 uv) {
 float2 d=_MainTex_TexelSize.xy*max(_Radius,0.5);
 return (tex2D(_MainTex,uv+d)+tex2D(_MainTex,uv-d)+tex2D(_MainTex,uv+float2(d.x,-d.y))+tex2D(_MainTex,uv+float2(-d.x,d.y)))*0.25;
}
fixed4 fast(v2f_img i):SV_Target { return Blur(i.uv); }
fixed4 crop(v2f_img i):SV_Target { return Blur(lerp(_CropRegion.xy,_CropRegion.zw,i.uv)); }
fixed4 dither(v2f_img i):SV_Target {
 fixed4 c=Blur(i.uv); float noise=tex2D(_BlueNoise,i.uv*_TargetSize.xy/32).r-0.5;
 c.a=saturate(c.a+noise/255); return c;
}
fixed4 HQ(float2 uv,float2 axis) {
 float4 c=tex2D(_MainTex,uv)*_Weights[0];
 int count=min(64,(_Extent+1)/2+1);
 [loop] for(int k=1;k<count;k++) {
  // Zero weights can have NaN offsets in the decompiled BlurHQConfig. Do not sample them.
  if(_Weights[k]>0) { float2 d=axis*_Offsets[k]; c+=(tex2D(_MainTex,uv+d)+tex2D(_MainTex,uv-d))*_Weights[k]; }
 }
 return c;
}
fixed4 horizontal(v2f_img i):SV_Target { return HQ(i.uv,float2(_MainTex_TexelSize.x,0)); }
fixed4 vertical(v2f_img i):SV_Target { return HQ(i.uv,float2(0,_MainTex_TexelSize.y)); }
fixed4 imprint(v2f_img i):SV_Target {
 fixed4 c=tex2D(_MainTex,i.uv);
 #ifdef BLEACH
 c.rgb=1;
 #endif
 #ifdef INSET
 c.a=1-c.a;
 #endif
 return c;
}
float maskAt(float2 uv) {
 if(any(uv<0)||any(uv>1)) return _OverflowAlpha;
 return tex2D(_MainTex,uv).a;
}
fixed4 cutout(v2f_img i):SV_Target { return fixed4(0,0,0,maskAt(i.uv+_Offset)); }
fixed4 postprocess(v2f_img i):SV_Target {
 fixed4 c=tex2D(_ShadowTex,i.uv); c.a=saturate(c.a*_AlphaMultiplier); return c;
}
''')
for file,name,passes in [('ShadowGenerate','Generate',['fast','crop','dither']),('ShadowGenerateHQ','GenerateHQ',['horizontal','vertical']),('ImprintPostProcess','ImprintPostProcess',['imprint']),('ShadowCutout','Cutout',['cutout']),('ShadowPostProcess','PostProcess',['postprocess'])]:
 text=header+'Shader "Hidden/TrueShadow/'+name+'" {\n Properties { _MainTex ("Input", 2D) = "white" {} }\n SubShader {\n Cull Off ZWrite Off ZTest Always\n'
 if name=='Cutout':text+=' Blend Zero OneMinusSrcAlpha\n'
 for frag in passes:
  text+=' Pass {\n CGPROGRAM\n #pragma target 3.0\n #pragma vertex vert_img\n #pragma fragment '+frag+'\n'
  if name=='ImprintPostProcess':text+=' #pragma multi_compile_local _ BLEACH\n #pragma multi_compile_local _ INSET\n'
  text+=' #include "ManosabaTrueShadowBlit.cginc"\n ENDCG\n }\n'
 text+=' }\n}\n';write(S/(file+'.shader'),text)
# Generated noise is explicitly not the original blue-noise distribution.
random.seed(20260918)
def chunk(t,b):return struct.pack('>I',len(b))+t+b+struct.pack('>I',zlib.crc32(t+b)&0xffffffff)
raw=b''.join(b'\0'+bytes(random.randrange(256) for _ in range(32)) for _ in range(32))
(RES/'True Shadow Blue Noise.png').write_bytes(b'\x89PNG\r\n\x1a\n'+chunk(b'IHDR',struct.pack('>IIBBBBB',32,32,8,0,0,0,0))+chunk(b'IDAT',zlib.compress(raw))+chunk(b'IEND',b''))
write(RES/'True Shadow Project Settings.asset','''%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: f730cb46b81ce0e4f879c0354e3e4f04, type: 3}
  m_Name: True Shadow Project Settings
  m_EditorClassIdentifier:
  useGlobalAngleByDefault: 0
  globalAngle: 90
  showQuickPresetsButtons: 1
  quickPresets: []
''')
for p in list(RES.rglob('*'))+[RES]:
 if p.suffix=='.meta':continue
 guid=hashlib.sha256(('Manosaba/runtime-resources/'+p.relative_to(BASE).as_posix()).encode()).hexdigest()[:32]
 write(Path(str(p)+'.meta'),'fileFormatVersion: 2\nguid: '+guid+'\n'+('folderAsset: yes\nDefaultImporter:\n  userData:\n' if p.is_dir() else ''))
print('candidate resources:',len(list(RES.rglob('*'))))
