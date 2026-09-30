// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
Shader "Hidden/TrueShadow/PostProcess" {
 Properties { _MainTex ("Input", 2D) = "white" {} }
 SubShader {
 Cull Off ZWrite Off ZTest Always
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment postprocess
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 }
}
