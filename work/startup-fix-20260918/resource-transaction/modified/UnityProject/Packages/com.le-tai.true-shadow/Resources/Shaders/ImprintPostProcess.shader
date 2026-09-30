// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
Shader "Hidden/TrueShadow/ImprintPostProcess" {
 Properties { _MainTex ("Input", 2D) = "white" {} }
 SubShader {
 Cull Off ZWrite Off ZTest Always
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment imprint
 #pragma multi_compile_local _ BLEACH
 #pragma multi_compile_local _ INSET
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 }
}
