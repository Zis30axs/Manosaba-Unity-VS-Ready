using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.Effects
{

public class ScalableBlur : IBlurAlgorithm
{
	private Material material;

	private ScalableBlurConfig config;

	private const int BLUR_PASS = 0;

	private const int CROP_BLUR_PASS = 1;

	private const int DITHER_BLUR_PASS = 2;

	private Material Material
	{
		get
		{
			if (material == null)
			{
				material = new Material(Shader.Find("Hidden/TrueShadow/Generate"));
			}
			return material;
		}
		set
		{
			material = value;
		}
	}

	public void Configure(BlurConfig config)
	{
		this.config = (ScalableBlurConfig)config;
	}

	public void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target)
	{
		float radius = config.Radius;
		Material.SetFloat(ShaderProperties.blurRadius, radius);
		Material.SetVector(ShaderProperties.blurTextureCropRegion, srcCropRegion.ToMinMaxVector());
		int downsampleFactor = ((config.Iteration > 0) ? 1 : 0);
		int num = Mathf.Max(config.Iteration * 2 - 1, 1);
		int num2 = ShaderProperties.intermediateRT[0];
		CreateTempRenderTextureFrom(cmd, num2, target, downsampleFactor);
		cmd.Blit(src, num2, Material, 1);
		for (int i = 1; i < num; i++)
		{
			BlurAtDepth(cmd, i, target);
		}
		Material.SetTexture(ShaderId.BLUE_NOISE, Resources.BLUE_NOISE);
		Material.SetVector(ShaderId.TARGET_SIZE, new Vector4(target.width, target.height));
		cmd.Blit(ShaderProperties.intermediateRT[num - 1], target, Material, 2);
		CleanupIntermediateRT(cmd, num);
	}

	protected virtual void BlurAtDepth(CommandBuffer cmd, int depth, RenderTexture baseTexture)
	{
		int a = Utility.SimplePingPong(depth, config.Iteration - 1) + 1;
		a = Mathf.Min(a, config.MaxDepth);
		CreateTempRenderTextureFrom(cmd, ShaderProperties.intermediateRT[depth], baseTexture, a);
		cmd.Blit(ShaderProperties.intermediateRT[depth - 1], ShaderProperties.intermediateRT[depth], Material, 0);
	}

	private static void CreateTempRenderTextureFrom(CommandBuffer cmd, int nameId, RenderTexture src, int downsampleFactor)
	{
		int width = src.width >> downsampleFactor;
		int height = src.height >> downsampleFactor;
		cmd.GetTemporaryRT(nameId, width, height, 0, FilterMode.Bilinear, src.format);
	}

	private static void CleanupIntermediateRT(CommandBuffer cmd, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			cmd.ReleaseTemporaryRT(ShaderProperties.intermediateRT[i]);
		}
	}
}
}
