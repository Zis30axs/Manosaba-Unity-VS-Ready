// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
Shader "Hidden/TrueShadow/Generate" {
 Properties { _MainTex ("Input", 2D) = "white" {} }
 SubShader {
 Cull Off ZWrite Off ZTest Always
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment fast
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment crop
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment dither
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 }
}
