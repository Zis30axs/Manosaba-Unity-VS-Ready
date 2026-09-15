using Il2CppDummyDll;
using MPUIKIT;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x200000F")]
public class MPImageAppearanceChanger : MonoBehaviour
{
	[Token(Token = "0x400001A")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private MPImage _mpImage;

	[Token(Token = "0x400001B")]
	[FieldOffset(Offset = "0x28")]
	[Header("Parameters")]
	[SerializeField]
	private float[] _strokes;

	[Token(Token = "0x400001C")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Color[] _colors;

	// RECONSTRUCTED | Confidence: A | RVA: 0x30D940
	// GetComponent<MPImage>() into +0x20.
	[Token(Token = "0x600002C")]
	[Address(RVA = "0x30D940", Offset = "0x30C340", VA = "0x18030D940", Slot = "4")]
	protected virtual void Reset()
	{
		_mpImage = GetComponent<MPImage>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30D9E0
	// Index >= 0 and below both `_strokes` (+0x28) and `_colors` (+0x30) lengths -> MPImage.StrokeWidth = _strokes[i],
	// Graphic slot 23 `color` = _colors[i]; otherwise Debug.LogError(string.Format("The specified index is out of range: {0}",
	// boxed index), this).
	[Token(Token = "0x600002D")]
	[Address(RVA = "0x30D9E0", Offset = "0x30C3E0", VA = "0x18030D9E0", Slot = "5")]
	public virtual void ChangeAppearance(int appearanceIndex)
	{
		if (appearanceIndex >= 0 && appearanceIndex < _strokes.Length && appearanceIndex < _colors.Length)
		{
			_mpImage.StrokeWidth = _strokes[appearanceIndex];
			_mpImage.color = _colors[appearanceIndex];
		}
		else
		{
			Debug.LogError(string.Format("The specified index is out of range: {0}", appearanceIndex), this);
		}
	}

	// RECONSTRUCTED | Confidence: A (index and Min spelling: B) | RVA: 0x30DB70
	// Both arrays at least 2 long -> StrokeWidth and color from index `movzx zeroOrOne`; otherwise LogError with
	// "Data length is not long enough: {0}" and the smaller of the two lengths (`cmp` + `cmovge`), context this.
	[Token(Token = "0x600002E")]
	[Address(RVA = "0x30DB70", Offset = "0x30C570", VA = "0x18030DB70", Slot = "6")]
	public virtual void ChangeAppearance(bool zeroOrOne)
	{
		if (_strokes.Length >= 2 && _colors.Length >= 2)
		{
			_mpImage.StrokeWidth = _strokes[zeroOrOne ? 1 : 0];
			_mpImage.color = _colors[zeroOrOne ? 1 : 0];
		}
		else
		{
			Debug.LogError(string.Format("Data length is not long enough: {0}", Mathf.Min(_strokes.Length, _colors.Length)), this);
		}
	}

	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x30D8F0 (shared empty MonoBehaviour ctor)
	// No field initializers.
	[Token(Token = "0x600002F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MPImageAppearanceChanger()
	{
	}
}
