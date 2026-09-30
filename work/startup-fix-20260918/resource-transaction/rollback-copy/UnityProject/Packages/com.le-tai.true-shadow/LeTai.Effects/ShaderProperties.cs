using UnityEngine;

namespace LeTai.Effects
{

public static class ShaderProperties
{
	private static bool isInitialized;

	public static int[] intermediateRT;

	public static int blurRadius;

	public static int blurTextureCropRegion;

	public static void Init()
	{
		if (!isInitialized)
		{
			blurRadius = Shader.PropertyToID("_Radius");
			blurTextureCropRegion = Shader.PropertyToID("_CropRegion");
			isInitialized = true;
		}
	}

	public static void Init(int stackDepth)
	{
		intermediateRT = new int[stackDepth * 2 - 1];
		for (int i = 0; i < intermediateRT.Length; i++)
		{
			intermediateRT[i] = Shader.PropertyToID($"TI_intermediate_rt_{i}");
		}
		Init();
	}
}
}
