using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{

[AddComponentMenu("")]
[ExecuteAlways]
public class ShadowRenderer : MonoBehaviour, ILayoutIgnorer, IMaterialModifier, IMeshModifier
{
	private static readonly Dictionary<int, Material> MASK_MATERIALS_CACHE = new Dictionary<int, Material>();

	private static int reDrawAllFrameIndex = -1;

	private TrueShadow shadow;

	private RectTransform rt;

	private RawImage graphic;

	private Texture shadowTexture;

	private bool willBeDestroyed;

	public bool ignoreLayout => true;

	internal CanvasRenderer CanvasRenderer { get; private set; }

	internal static void ClearMaskMaterialCache()
	{
		foreach (KeyValuePair<int, Material> item in MASK_MATERIALS_CACHE)
		{
			if (Application.isPlaying)
			{
				Object.Destroy(item.Value);
			}
			else
			{
				Object.DestroyImmediate(item.Value);
			}
		}
		MASK_MATERIALS_CACHE.Clear();
	}

	public Material GetModifiedMaterial(Material baseMaterial)
	{
		if (!shadow)
		{
			return baseMaterial;
		}
		shadow.ModifyShadowRendererMaterial(baseMaterial);
		if (!baseMaterial.HasProperty(ShaderId.STENCIL_ID))
		{
			return baseMaterial;
		}
		Mask component = shadow.GetComponent<Mask>();
		bool flag = component != null && component.isActiveAndEnabled;
		int h = (flag ? baseMaterial.ComputeCRC() : baseMaterial.GetHashCode());
		int key = HashUtils.CombineHashCodes(flag.GetHashCode(), h);
		MASK_MATERIALS_CACHE.TryGetValue(key, out var value);
		if (!value)
		{
			value = new Material(baseMaterial);
			if (shadow.ShadowAsSibling)
			{
				value.SetInt(ShaderId.COLOR_MASK, 15);
				value.SetInt(ShaderId.STENCIL_OP, 0);
			}
			else if (flag)
			{
				int num = value.GetInt(ShaderId.STENCIL_ID) + 1;
				int i;
				for (i = 0; i < 8 && ((num >> i) & 1) != 1; i++)
				{
				}
				i = Mathf.Max(0, i - 1);
				int value2 = (1 << i) - 1;
				value.SetInt(ShaderId.STENCIL_ID, value2);
				value.SetInt(ShaderId.STENCIL_READ_MASK, value2);
			}
			MASK_MATERIALS_CACHE[key] = value;
		}
		else
		{
			int value3 = value.GetInt(ShaderId.STENCIL_ID);
			int value4 = value.GetInt(ShaderId.STENCIL_OP);
			int value5 = value.GetInt(ShaderId.COLOR_MASK);
			int value6 = value.GetInt(ShaderId.STENCIL_READ_MASK);
			value.CopyPropertiesFromMaterial(baseMaterial);
			value.SetInt(ShaderId.STENCIL_ID, value3);
			value.SetInt(ShaderId.STENCIL_OP, value4);
			value.SetInt(ShaderId.COLOR_MASK, value5);
			value.SetInt(ShaderId.STENCIL_READ_MASK, value6);
		}
		return value;
	}

	[Conditional("UNITY_EDITOR")]
	internal static void QueueReDrawAll()
	{
		reDrawAllFrameIndex = Time.frameCount + 1;
	}

	public static void Initialize(TrueShadow shadow, ref ShadowRenderer renderer)
	{
		if ((bool)renderer && renderer.shadow == shadow)
		{
			renderer.gameObject.SetActive(value: true);
			return;
		}
		string text = shadow.gameObject.name + "'s Shadow";
		HideFlags hideFlags = HideFlags.HideAndDontSave;
		GameObject gameObject = new GameObject(text)
		{
			hideFlags = hideFlags
		};
		RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
		gameObject.AddComponent<CanvasRenderer>();
		RawImage rawImage = gameObject.AddComponent<RawImage>();
		renderer = gameObject.AddComponent<ShadowRenderer>();
		shadow.SetHierachyDirty();
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.zero;
		rawImage.raycastTarget = false;
		rawImage.color = shadow.Color;
		renderer.shadow = shadow;
		renderer.rt = rectTransform;
		renderer.graphic = rawImage;
		renderer.UpdateMaterial();
		renderer.CanvasRenderer = gameObject.GetComponent<CanvasRenderer>();
		renderer.CanvasRenderer.SetColor(shadow.IgnoreCasterColor ? Color.white : shadow.CanvasRenderer.GetColor());
		renderer.CanvasRenderer.SetAlpha(shadow.CanvasRenderer.GetAlpha());
		renderer.ReLayout();
	}

	public void UpdateMaterial()
	{
		if (!graphic)
		{
			return;
		}
		if (shadow.Graphic is MaskableGraphic maskableGraphic)
		{
			graphic.maskable = maskableGraphic.maskable;
		}
		if (CanvasUpdateRegistry.IsRebuildingGraphics())
		{
			this.NextFrames(delegate
			{
				graphic.material = shadow.GetShadowRenderingMaterial();
			});
		}
		else
		{
			graphic.material = shadow.GetShadowRenderingMaterial();
		}
	}

	internal void ReLayout()
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		RectTransform rectTransform = shadow.RectTransform;
		Mesh obj = shadow.SpriteMeshHandle.obj;
		if (!rectTransform || !shadowTexture || !obj)
		{
			CanvasRenderer.SetAlpha(0f);
			return;
		}
		bool flag = !shadow.DisableFitCompensation && !(shadow.Graphic is Text) && !(shadow.Graphic is TextMeshProUGUI) && !(shadow.Graphic is TMP_SubMeshUI);
		ShadowContainer shadowContainer = shadow.ShadowContainer;
		float num = shadowContainer?.Snapshot?.canvasScale ?? graphic.canvas.scaleFactor;
		float num2 = 1f / num;
		Bounds bounds = obj.bounds;
		Vector2 vector = ((shadowContainer == null) ? Vector2.one : ((Vector2)bounds.size * num / shadowContainer.ImprintSize));
		Vector2 vector2 = new Vector2(shadowTexture.width, shadowTexture.height);
		vector2 *= vector;
		vector2 *= num2;
		if (flag)
		{
			if (shadow.Inset)
			{
				vector2 += Vector2.one * num2;
			}
			else
			{
				vector2 -= Vector2.one * num2;
			}
		}
		if (vector2.x < 0.001f || vector2.y < 0.001f)
		{
			CanvasRenderer.SetAlpha(0f);
			return;
		}
		rt.sizeDelta = vector2;
		float num3 = shadowContainer?.Padding ?? Mathf.CeilToInt(shadow.Size * num);
		num3 *= num2;
		if (flag)
		{
			num3 = ((!shadow.Inset) ? (num3 - 0.5f * num2) : (num3 + 0.5f * num2));
		}
		Vector2 vector3 = -(Vector2)bounds.min;
		rt.pivot = (vector3 + num3 * vector) / vector2;
		Vector2 self = ((shadowContainer == null) ? ((Vector2?)null) : (shadowContainer.Snapshot?.canvasRelativeOffset * num2)) ?? shadow.Offset;
		Vector3 vector4 = (shadow.ShadowAsSibling ? shadow.Offset.WithZ(0f) : self.WithZ(0f));
		rt.localPosition = (shadow.ShadowAsSibling ? (rectTransform.localPosition + vector4) : vector4);
		rt.localRotation = (shadow.ShadowAsSibling ? rectTransform.localRotation : Quaternion.identity);
		rt.localScale = (shadow.ShadowAsSibling ? rectTransform.localScale : Vector3.one);
		Color color = shadow.Color;
		if (shadow.UseCasterAlpha)
		{
			color.a *= shadow.Graphic.color.a;
		}
		if (shadow.Size < 1f && shadow.OffsetDistance < 1f)
		{
			float num4 = 1f - Mathf.Min(1f, shadow.Size + shadow.OffsetDistance);
			num4 *= num4;
			color.a *= 1f - num4;
		}
		graphic.color = color;
		CanvasRenderer.SetColor(shadow.IgnoreCasterColor ? Color.white : shadow.CanvasRenderer.GetColor());
		CanvasRenderer.SetAlpha(shadow.CanvasRenderer.GetAlpha());
		graphic.Rebuild(CanvasUpdate.PreRender);
	}

	public void SetTexture(Texture texture)
	{
		shadowTexture = texture;
		CanvasRenderer.SetTexture(texture);
		graphic.texture = texture;
	}

	public void SetMaterialDirty()
	{
		graphic.SetMaterialDirty();
	}

	public void ModifyMesh(VertexHelper vertexHelper)
	{
		if ((bool)shadow)
		{
			shadow.ModifyShadowRendererMesh(vertexHelper);
		}
	}

	public void ModifyMesh(Mesh mesh)
	{
	}

	protected virtual void LateUpdate()
	{
		if (!shadow)
		{
			Dispose();
		}
		if (!willBeDestroyed)
		{
			_ = (bool)base.gameObject;
		}
	}

	protected virtual void OnDestroy()
	{
		willBeDestroyed = true;
	}

	public void Dispose()
	{
		if (!willBeDestroyed)
		{
			if ((bool)shadow && shadow.ShadowAsSibling)
			{
				base.gameObject.SetActive(value: false);
				base.transform.SetParent(null);
			}
			Object.Destroy(base.gameObject);
		}
	}
}
}
