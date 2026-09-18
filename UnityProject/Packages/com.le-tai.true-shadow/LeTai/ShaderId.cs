using UnityEngine;

namespace LeTai
{

public static class ShaderId
{
	public static readonly int MAIN_TEX = Shader.PropertyToID("_MainTex");

	public static readonly int BLUE_NOISE = Shader.PropertyToID("_BlueNoise");

	public static readonly int TARGET_SIZE = Shader.PropertyToID("_TargetSize");

	public static readonly int TMP_TEX = Shader.PropertyToID("_TrueShadow_TmpTex");

	public static readonly int WEIGHTS = Shader.PropertyToID("_Weights");

	public static readonly int OFFSETS = Shader.PropertyToID("_Offsets");

	public static readonly int EXTENT = Shader.PropertyToID("_Extent");

	public static readonly int SHADOW_TEX = Shader.PropertyToID("_ShadowTex");

	public static readonly int CLIP_RECT = Shader.PropertyToID("_ClipRect");

	public static readonly int TEXTURE_SAMPLE_ADD = Shader.PropertyToID("_TextureSampleAdd");

	public static readonly int COLOR_MASK = Shader.PropertyToID("_ColorMask");

	public static readonly int STENCIL_OP = Shader.PropertyToID("_StencilOp");

	public static readonly int STENCIL_ID = Shader.PropertyToID("_Stencil");

	public static readonly int STENCIL_READ_MASK = Shader.PropertyToID("_StencilReadMask");

	public static readonly int OFFSET = Shader.PropertyToID("_Offset");

	public static readonly int OVERFLOW_ALPHA = Shader.PropertyToID("_OverflowAlpha");

	public static readonly int ALPHA_MULTIPLIER = Shader.PropertyToID("_AlphaMultiplier");

	public static readonly int TIME = Shader.PropertyToID("_Time");

	public static readonly int SIN_TIME = Shader.PropertyToID("_SinTime");

	public static readonly int COS_TIME = Shader.PropertyToID("_CosTime");

	public static readonly int UNITY_DELTA_TIME = Shader.PropertyToID("unity_DeltaTime");

	public static readonly int SCREEN_PARAMS = Shader.PropertyToID("_ScreenParams");

	public static readonly int UI_VERTEX_COLOR_ALWAYS_GAMMA_SPACE = Shader.PropertyToID("_UIVertexColorAlwaysGammaSpace");

	public static readonly int SCALE_X = Shader.PropertyToID("_ScaleX");

	public static readonly int SCALE_Y = Shader.PropertyToID("_ScaleY");
}
}
