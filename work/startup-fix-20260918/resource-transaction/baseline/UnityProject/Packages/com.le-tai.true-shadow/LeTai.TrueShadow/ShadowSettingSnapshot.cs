using LeTai.Effects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{

internal class ShadowSettingSnapshot
{
	public readonly TrueShadow shadow;

	public readonly Canvas canvas;

	public readonly RectTransform canvasRt;

	public readonly float canvasScale;

	public readonly BlurAlgorithmSelection algorithm;

	public readonly float size;

	public readonly Vector2 canvasRelativeOffset;

	public readonly Vector2 dimensions;

	private const int DIMENSIONS_HASH_STEP = 1;

	private int hash;

	internal ShadowSettingSnapshot(TrueShadow shadow)
	{
		this.shadow = shadow;
		canvas = shadow.Graphic.canvas;
		canvasRt = (RectTransform)canvas.transform;
		Mesh obj = shadow.SpriteMeshHandle.obj;
		Bounds bounds;
		if ((bool)obj)
		{
			bounds = obj.bounds;
			if (bounds.extents == Vector3.zero)
			{
				obj.RecalculateBounds();
				bounds = obj.bounds;
			}
		}
		else
		{
			bounds = new Bounds(Vector3.zero, Vector3.zero);
		}
		canvasScale = canvas.scaleFactor;
		Quaternion quaternion = Quaternion.Inverse(canvasRt.rotation) * shadow.RectTransform.rotation;
		canvasRelativeOffset = shadow.Offset.Rotate(0f - quaternion.eulerAngles.z) * canvasScale;
		dimensions = (Vector2)bounds.size * canvasScale;
		size = shadow.Size * canvasScale;
		algorithm = shadow.Algorithm;
		CalcHash();
	}

	private void CalcHash()
	{
		Graphic graphic = shadow.Graphic;
		int h = (int)((double)canvasScale * 10000.0);
		int h2 = (shadow.Inset ? 1 : 0);
		Color clearColor = shadow.ClearColor;
		Color color = graphic.color;
		if (shadow.IgnoreCasterColor)
		{
			color = Color.clear;
		}
		int h3 = HashUtils.CombineHashCodes(shadow.IgnoreCasterColor ? 1 : 0, (int)(color.r * 255f), (int)(color.g * 255f), (int)(color.b * 255f), (int)(color.a * 255f), (int)(clearColor.r * 255f), (int)(clearColor.g * 255f), (int)(clearColor.b * 255f), (int)(clearColor.a * 255f));
		int h4 = HashUtils.CombineHashCodes(shadow.Cutout ? 1 : 0, (int)(canvasRelativeOffset.x * 100f), (int)(canvasRelativeOffset.y * 100f));
		int h5 = ((graphic is Image { type: Image.Type.Tiled }) ? dimensions.GetHashCode() : HashUtils.CombineHashCodes(Mathf.CeilToInt(dimensions.x / 1f), Mathf.CeilToInt(dimensions.y / 1f)));
		int h6 = (int)algorithm;
		int h7 = Mathf.CeilToInt(size * 100f);
		int h8 = Mathf.CeilToInt(shadow.Spread * 100f);
		Material materialForRendering = graphic.materialForRendering;
		int h9 = HashUtils.CombineHashCodes(materialForRendering ? materialForRendering.ComputeCRC() : 0, h, h2, h3, h4, h5, h6, h7, h8, shadow.CustomHash);
		if (!(graphic is Image image2))
		{
			if (!(graphic is RawImage rawImage))
			{
				if (!(graphic is Text text))
				{
					if (!(graphic is TextMeshProUGUI tmp))
					{
						if (graphic is TMP_SubMeshUI tMP_SubMeshUI)
						{
							hash = HashUtils.CombineHashCodes(h9, CalcTMPHash(tMP_SubMeshUI.textComponent));
						}
						else
						{
							hash = h9;
						}
					}
					else
					{
						hash = HashUtils.CombineHashCodes(h9, CalcTMPHash(tmp));
					}
				}
				else
				{
					hash = HashUtils.CombineHashCodes(h9, text.text.GetHashCode(), text.font.GetHashCode(), (int)text.alignment);
				}
			}
			else
			{
				int h10 = 0;
				if ((bool)rawImage.texture)
				{
					h10 = rawImage.texture.GetInstanceID();
				}
				hash = HashUtils.CombineHashCodes(h9, h10);
			}
		}
		else
		{
			int h11 = 0;
			if ((bool)image2.sprite)
			{
				h11 = image2.sprite.GetHashCode();
			}
			int h12 = HashUtils.CombineHashCodes((int)image2.type, (int)(image2.pixelsPerUnitMultiplier * 1000f), (int)(image2.fillAmount * 360f * 20f), (int)image2.fillMethod, image2.fillOrigin, image2.fillClockwise ? 1 : 0);
			hash = HashUtils.CombineHashCodes(h9, h11, h12);
		}
	}

	private int CalcTMPHash(TMP_Text tmp)
	{
		int h = 0;
		if (!shadow.IgnoreCasterColor)
		{
			h = HashUtils.CombineHashCodes(tmp.enableVertexGradient.GetHashCode(), tmp.colorGradient.GetHashCode(), tmp.overrideColorTags.GetHashCode());
		}
		return HashUtils.CombineHashCodes(HashUtils.CombineHashCodes(shadow.SkipTextHashing.GetHashCode(), shadow.SkipTextHashing ? tmp.textInfo.characterCount : (tmp.text?.GetHashCode() ?? 0)), tmp.maxVisibleCharacters, tmp.maxVisibleWords, tmp.maxVisibleLines, Mathf.CeilToInt(tmp.transform.lossyScale.y * 100f), tmp.font.GetHashCode(), tmp.fontSize.GetHashCode(), h, tmp.characterSpacing.GetHashCode(), tmp.wordSpacing.GetHashCode(), tmp.lineSpacing.GetHashCode(), tmp.paragraphSpacing.GetHashCode(), (int)tmp.alignment);
	}

	public override int GetHashCode()
	{
		return hash;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		return GetHashCode() == obj.GetHashCode();
	}
}
}
