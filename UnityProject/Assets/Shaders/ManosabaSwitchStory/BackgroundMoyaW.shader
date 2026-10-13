// The W variant retains its own source colors/speed and shares the compatibility fog program.
Shader "Manosaba/SwitchStory/BackgroundMoyaW"
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
        UsePass "Manosaba/SwitchStory/BackgroundMoya/MOYA"
    }
}
