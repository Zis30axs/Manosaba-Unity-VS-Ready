using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.Effects
{

public class BlurHQ : IBlurAlgorithm
{
	public const float MAX_RADIUS = 126f;

	private BlurHQConfig config;

	private Material _material;

	private Material Material
	{
		get
		{
			if (_material == null)
			{
				_material = new Material(Shader.Find("Hidden/TrueShadow/GenerateHQ"));
			}
			return _material;
		}
	}

	public void Configure(BlurConfig config)
	{
		this.config = (BlurHQConfig)config;
	}

	public void Blur(CommandBuffer cmd, RenderTargetIdentifier src, Rect srcCropRegion, RenderTexture target)
	{
		cmd.GetTemporaryRT(ShaderId.TMP_TEX, target.width, target.height, 0, FilterMode.Bilinear, target.format);
		Material.DisableKeyword("MAX_TAPS_64");
		Material.DisableKeyword("MAX_TAPS_16");
		Material.DisableKeyword("MAX_TAPS_4");
		if (config.NumWeight <= 8)
		{
			Material.EnableKeyword("MAX_TAPS_8");
		}
		else if (config.NumWeight <= 32)
		{
			Material.EnableKeyword("MAX_TAPS_32");
		}
		else
		{
			Material.EnableKeyword("MAX_TAPS_64");
		}
		Material.SetInt(ShaderId.EXTENT, config.Extent);
		Material.SetFloatArray(ShaderId.WEIGHTS, config.weights);
		Material.SetFloatArray(ShaderId.OFFSETS, config.offsets);
		Material.SetTexture(ShaderId.BLUE_NOISE, Resources.BLUE_NOISE);
		Material.SetVector(ShaderId.TARGET_SIZE, new Vector4(target.width, target.height));
		cmd.Blit(src, ShaderId.TMP_TEX, Material, 0);
		cmd.Blit(ShaderId.TMP_TEX, target, Material, 1);
		cmd.ReleaseTemporaryRT(ShaderId.TMP_TEX);
	}
}
}
