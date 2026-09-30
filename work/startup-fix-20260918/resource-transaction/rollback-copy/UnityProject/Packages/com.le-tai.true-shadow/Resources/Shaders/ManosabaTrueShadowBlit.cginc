// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
#include "UnityCG.cginc"
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
