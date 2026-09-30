using System;
using System.Collections.Generic;
using LeTai.Effects;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.TrueShadow
{

public class ShadowFactory
{
	private static ShadowFactory instance;

	private readonly Dictionary<int, ShadowContainer> shadowCache = new Dictionary<int, ShadowContainer>();

	private readonly CommandBuffer cmd;

	private readonly MaterialPropertyBlock materialProps;

	private readonly ScalableBlur blurProcessorFast;

	private readonly BlurHQ blurProcessorAccurate;

	private readonly BlurConfig blurConfigFast;

	private readonly BlurConfig blurConfigAccurate;

	private Material cutoutMaterial;

	private Material imprintPostProcessMaterial;

	private Material shadowPostProcessMaterial;

	private static readonly Rect UNIT_RECT = new Rect(0f, 0f, 1f, 1f);

	private static readonly Vector4 ALPHA8_TEXTURE_BIAS = new Vector4(1f, 1f, 1f, 0f);

	public static ShadowFactory Instance => instance ?? (instance = new ShadowFactory());

	public int CachedCount => shadowCache.Count;

	private Material CutoutMaterial
	{
		get
		{
			if (!cutoutMaterial)
			{
				return cutoutMaterial = new Material(Shader.Find("Hidden/TrueShadow/Cutout"));
			}
			return cutoutMaterial;
		}
	}

	private Material ImprintPostProcessMaterial
	{
		get
		{
			if (!imprintPostProcessMaterial)
			{
				return imprintPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/ImprintPostProcess"));
			}
			return imprintPostProcessMaterial;
		}
	}

	private Material ShadowPostProcessMaterial
	{
		get
		{
			if (!shadowPostProcessMaterial)
			{
				return shadowPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/PostProcess"));
			}
			return shadowPostProcessMaterial;
		}
	}

	private ShadowFactory()
	{
		cmd = new CommandBuffer
		{
			name = "Shadow Commands"
		};
		materialProps = new MaterialPropertyBlock();
		ShaderProperties.Init(8);
		blurConfigFast = ScriptableObject.CreateInstance<ScalableBlurConfig>();
		blurConfigFast.hideFlags = HideFlags.HideAndDontSave;
		blurProcessorFast = new ScalableBlur();
		blurProcessorFast.Configure(blurConfigFast);
		blurConfigAccurate = ScriptableObject.CreateInstance<BlurHQConfig>();
		blurConfigAccurate.hideFlags = HideFlags.HideAndDontSave;
		blurProcessorAccurate = new BlurHQ();
		blurProcessorAccurate.Configure(blurConfigAccurate);
	}

	internal void Get(ShadowSettingSnapshot snapshot, ref ShadowContainer container)
	{
		if (float.IsNaN(snapshot.dimensions.x) || snapshot.dimensions.x < 1f || float.IsNaN(snapshot.dimensions.y) || snapshot.dimensions.y < 1f || !snapshot.shadow.Graphic.materialForRendering || (snapshot.size == 0f && snapshot.canvasRelativeOffset.sqrMagnitude == 0f))
		{
			ReleaseContainer(ref container);
			return;
		}
		float num = snapshot.size * 2f;
		if (snapshot.dimensions.x + num > 4097f || snapshot.dimensions.y + num > 4097f)
		{
			Debug.LogWarning("Requested Shadow is too large");
			return;
		}
		int hashCode = snapshot.GetHashCode();
		ShadowContainer obj = container;
		if (obj == null || obj.requestHash != hashCode)
		{
			ReleaseContainer(ref container);
			if (shadowCache.TryGetValue(hashCode, out var value))
			{
				value.RefCount++;
				container = value;
			}
			else
			{
				ShadowContainer shadowContainer = (shadowCache[hashCode] = GenerateShadow(snapshot));
				container = shadowContainer;
			}
		}
	}

	internal void ReleaseContainer(ref ShadowContainer container)
	{
		if (container != null && --container.RefCount <= 0)
		{
			RenderTexture.ReleaseTemporary(container.Texture);
			shadowCache.Remove(container.requestHash);
			container = null;
		}
	}

	private ShadowContainer GenerateShadow(ShadowSettingSnapshot snapshot)
	{
		cmd.Clear();
		cmd.BeginSample("TrueShadow:Capture");
		Mesh obj = snapshot.shadow.SpriteMeshHandle.obj;
		Bounds bounds = obj.bounds;
		BlurConfig blurConfig = ((snapshot.algorithm == BlurAlgorithmSelection.Fast) ? blurConfigFast : blurConfigAccurate);
		IBlurAlgorithm blurAlgorithm2;
		if (snapshot.algorithm != BlurAlgorithmSelection.Fast)
		{
			IBlurAlgorithm blurAlgorithm = blurProcessorAccurate;
			blurAlgorithm2 = blurAlgorithm;
		}
		else
		{
			IBlurAlgorithm blurAlgorithm = blurProcessorFast;
			blurAlgorithm2 = blurAlgorithm;
		}
		IBlurAlgorithm blurAlgorithm3 = blurAlgorithm2;
		int num = (snapshot.shadow.Inset ? System.Math.Max(Mathf.CeilToInt(Mathf.Max(Mathf.Abs(snapshot.canvasRelativeOffset.x), Mathf.Abs(snapshot.canvasRelativeOffset.y))), blurConfig.MinExtent) : Mathf.CeilToInt(snapshot.size));
		if (snapshot.algorithm == BlurAlgorithmSelection.Fast)
		{
			num += ((snapshot.size < 50f) ? 4 : 0);
		}
		Vector2Int vector2Int = Vector2Int.CeilToInt(snapshot.dimensions);
		int x = vector2Int.x;
		int y = vector2Int.y;
		int num2 = num * 2;
		x += num2;
		y += num2;
		RenderTexture temporary = RenderTexture.GetTemporary(x, y, 0, RenderTextureFormat.ARGB32);
		RenderTextureDescriptor descriptor = temporary.descriptor;
		RenderTexture temporary2 = RenderTexture.GetTemporary(descriptor);
		RenderTexture renderTexture = null;
		bool flag = snapshot.shadow.IgnoreCasterColor || snapshot.shadow.Inset;
		if (flag)
		{
			renderTexture = RenderTexture.GetTemporary(descriptor);
		}
		Texture content = snapshot.shadow.Content;
		materialProps.Clear();
		materialProps.SetVector(ShaderId.CLIP_RECT, new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.PositiveInfinity, float.PositiveInfinity));
		materialProps.SetInt(ShaderId.COLOR_MASK, 15);
		if (obj.subMeshCount <= 1)
		{
			if ((bool)content)
			{
				materialProps.SetTexture(ShaderId.MAIN_TEX, content);
				if (content is Texture2D { format: TextureFormat.Alpha8 })
				{
					materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, ALPHA8_TEXTURE_BIAS);
				}
				else
				{
					materialProps.SetVector(ShaderId.TEXTURE_SAMPLE_ADD, Vector4.zero);
				}
			}
			else
			{
				materialProps.SetTexture(ShaderId.MAIN_TEX, Texture2D.whiteTexture);
			}
		}
		cmd.SetRenderTarget(temporary2);
		cmd.ClearRenderTarget(clearDepth: true, clearColor: true, snapshot.shadow.ClearColor);
		cmd.SetViewport(new Rect(new Vector2(num, num), (Vector2)vector2Int));
		cmd.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.Ortho(bounds.min.x, bounds.max.x, bounds.min.y, bounds.max.y, -1f, 1f));
		float timeSinceLevelLoad = Time.timeSinceLevelLoad;
		materialProps.SetVector(ShaderId.TIME, new Vector4(timeSinceLevelLoad / 20f, timeSinceLevelLoad, timeSinceLevelLoad * 2f, timeSinceLevelLoad * 3f));
		materialProps.SetVector(ShaderId.SIN_TIME, new Vector4(Mathf.Sin(timeSinceLevelLoad / 8f), Mathf.Sin(timeSinceLevelLoad / 4f), Mathf.Sin(timeSinceLevelLoad / 2f), Mathf.Sin(timeSinceLevelLoad)));
		materialProps.SetVector(ShaderId.COS_TIME, new Vector4(Mathf.Cos(timeSinceLevelLoad / 8f), Mathf.Cos(timeSinceLevelLoad / 4f), Mathf.Cos(timeSinceLevelLoad / 2f), Mathf.Cos(timeSinceLevelLoad)));
		materialProps.SetVector(ShaderId.UNITY_DELTA_TIME, new Vector4(Time.deltaTime, 1f / Time.deltaTime, Time.smoothDeltaTime, 1f / Time.smoothDeltaTime));
		materialProps.SetInt(ShaderId.UI_VERTEX_COLOR_ALWAYS_GAMMA_SPACE, 1);
		if (snapshot.shadow.Graphic is TextMeshProUGUI || snapshot.shadow.Graphic is TMP_SubMeshUI)
		{
			Vector3 lossyScale = snapshot.canvas.transform.lossyScale;
			materialProps.SetFloat(ShaderId.SCALE_X, 1f / lossyScale.x);
			materialProps.SetFloat(ShaderId.SCALE_Y, 1f / lossyScale.y);
			materialProps.SetVector(ShaderId.SCREEN_PARAMS, new Vector4(x, y, 1f + 1f / (float)x, 1f + 1f / (float)y));
		}
		snapshot.shadow.ModifyShadowCastingMaterialProperties(materialProps);
		if (obj.subMeshCount == 1)
		{
			Material shadowCastingMaterial = snapshot.shadow.GetShadowCastingMaterial();
			for (int i = 0; i < shadowCastingMaterial.passCount; i++)
			{
				cmd.DrawMesh(obj, Matrix4x4.identity, shadowCastingMaterial, 0, i, materialProps);
			}
		}
		else
		{
			for (int j = 0; j < obj.subMeshCount; j++)
			{
				Material shadowCastingMaterialForSubMesh = snapshot.shadow.GetShadowCastingMaterialForSubMesh(j);
				for (int k = 0; k < shadowCastingMaterialForSubMesh.passCount; k++)
				{
					cmd.DrawMesh(obj, Matrix4x4.identity, shadowCastingMaterialForSubMesh, j, k, materialProps);
				}
			}
		}
		if (flag)
		{
			ImprintPostProcessMaterial.SetKeyword("BLEACH", snapshot.shadow.IgnoreCasterColor);
			ImprintPostProcessMaterial.SetKeyword("INSET", snapshot.shadow.Inset);
			cmd.Blit(temporary2, renderTexture, ImprintPostProcessMaterial);
		}
		cmd.EndSample("TrueShadow:Capture");
		bool num3 = (double)snapshot.shadow.Spread > 0.001;
		cmd.BeginSample("TrueShadow:Cast");
		RenderTexture renderTexture2 = (flag ? renderTexture : temporary2);
		RenderTexture renderTexture3 = ((!num3) ? temporary : RenderTexture.GetTemporary(temporary.descriptor));
		if ((double)snapshot.size < 0.01)
		{
			cmd.Blit(renderTexture2, renderTexture3);
		}
		else
		{
			blurConfig.Strength = snapshot.size;
			blurAlgorithm3.Blur(cmd, renderTexture2, UNIT_RECT, renderTexture3);
		}
		cmd.EndSample("TrueShadow:Cast");
		Vector2 vector = new Vector2(snapshot.canvasRelativeOffset.x / (float)x, snapshot.canvasRelativeOffset.y / (float)y);
		int num4 = (snapshot.shadow.Inset ? 1 : 0);
		if (num3)
		{
			cmd.BeginSample("TrueShadow:PostProcess");
			ShadowPostProcessMaterial.SetTexture(ShaderId.SHADOW_TEX, renderTexture3);
			ShadowPostProcessMaterial.SetVector(ShaderId.OFFSET, vector);
			ShadowPostProcessMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, num4);
			ShadowPostProcessMaterial.SetFloat(ShaderId.ALPHA_MULTIPLIER, 1f / Mathf.Max(1.5259022E-05f, 1f - snapshot.shadow.Spread));
			cmd.SetViewport(UNIT_RECT);
			cmd.Blit(renderTexture2, temporary, ShadowPostProcessMaterial);
			cmd.EndSample("TrueShadow:PostProcess");
		}
		else if (snapshot.shadow.Cutout)
		{
			cmd.BeginSample("TrueShadow:Cutout");
			CutoutMaterial.SetVector(ShaderId.OFFSET, vector);
			CutoutMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, num4);
			cmd.SetViewport(UNIT_RECT);
			cmd.Blit(renderTexture2, temporary, CutoutMaterial);
			cmd.EndSample("TrueShadow:Cutout");
		}
		Graphics.ExecuteCommandBuffer(cmd);
		RenderTexture.ReleaseTemporary(temporary2);
		RenderTexture.ReleaseTemporary(renderTexture2);
		if (num3)
		{
			RenderTexture.ReleaseTemporary(renderTexture3);
		}
		return new ShadowContainer(temporary, snapshot, num, vector2Int);
	}

	private RenderTexture GenColoredTexture(int hash)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixels32(new Color32[1]
		{
			new Color32((byte)(hash >> 8), (byte)(hash >> 16), (byte)(hash >> 24), byte.MaxValue)
		});
		texture2D.Apply();
		RenderTexture temporary = RenderTexture.GetTemporary(1, 1);
		Graphics.Blit(texture2D, temporary);
		return temporary;
	}
}
}
