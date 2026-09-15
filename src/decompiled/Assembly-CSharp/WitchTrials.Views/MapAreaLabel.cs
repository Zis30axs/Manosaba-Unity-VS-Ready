using Il2CppDummyDll;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000F8")]
public class MapAreaLabel : MonoBehaviour
{
	[Token(Token = "0x40004A3")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x40004A4")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x40004A5")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Image[] _labelPlateImages;

	// RECONSTRUCTED | Confidence: A | RVA: 0x36E6B0
	[Token(Token = "0x60003EA")]
	[Address(RVA = "0x36E6B0", Offset = "0x36D0B0", VA = "0x18036E6B0")]
	private void Reset()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x36E750 (folded with SplashScreen.Awake)
	[Token(Token = "0x60003EB")]
	[Address(RVA = "0x36E750", Offset = "0x36D150", VA = "0x18036E750")]
	public void Show()
	{
		_canvasGroup.alpha = 1f;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3491C0 (folded with EndingStill.Awake)
	[Token(Token = "0x60003EC")]
	[Address(RVA = "0x3491C0", Offset = "0x347BC0", VA = "0x1803491C0")]
	public void Hide()
	{
		_canvasGroup.alpha = 0f;
	}

	// RECONSTRUCTED | Confidence: A (Math vs Mathf.Min: B) | RVA: 0x36E780
	// 
	// `_label` (+0x28) vtable +0x678 = TMP_Text slot 84 `preferredWidth`, passed to
	// `Array.BinarySearch<float>`. A negative result is complemented and clamped
	// with `cmovge` to `Length - 1` (an inlined int Min). The loop then sets
	// `Behaviour.enabled` (icall 0x2604C50) on each plate image to `i == index`.
	[Token(Token = "0x60003ED")]
	[Address(RVA = "0x36E780", Offset = "0x36D180", VA = "0x18036E780")]
	public void ModifyInEditor(float[] labelMaxWidths)
	{
		int index = Array.BinarySearch(labelMaxWidths, _label.preferredWidth);
		if (index < 0)
		{
			index = Math.Min(~index, labelMaxWidths.Length - 1);
		}
		for (int i = 0; i < _labelPlateImages.Length; i++)
		{
			_labelPlateImages[i].enabled = i == index;
		}
	}

	[Token(Token = "0x60003EE")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MapAreaLabel()
	{
	}
}
