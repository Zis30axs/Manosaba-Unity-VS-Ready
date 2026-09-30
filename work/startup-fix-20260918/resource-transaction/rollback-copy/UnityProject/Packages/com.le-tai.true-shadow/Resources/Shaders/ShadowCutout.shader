// COMPATIBILITY RECONSTRUCTION: authored from the local C# interface; not an original TrueShadow asset.
Shader "Hidden/TrueShadow/Cutout" {
 Properties { _MainTex ("Input", 2D) = "white" {} }
 SubShader {
 Cull Off ZWrite Off ZTest Always
 Blend Zero OneMinusSrcAlpha
 Pass {
 CGPROGRAM
 #pragma target 3.0
 #pragma vertex vert_img
 #pragma fragment cutout
 #include "ManosabaTrueShadowBlit.cginc"
 ENDCG
 }
 }
}
