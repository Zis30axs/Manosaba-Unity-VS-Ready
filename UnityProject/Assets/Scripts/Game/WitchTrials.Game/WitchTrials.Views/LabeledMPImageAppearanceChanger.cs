using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x200000E")]
    public class LabeledMPImageAppearanceChanger : MPImageAppearanceChanger
    {
    	[Token(Token = "0x4000018")]
    	[FieldOffset(Offset = "0x38")]
    	[Header("References for Label")]
    	[SerializeField]
    	private TextMeshProUGUI _label;

    	[Token(Token = "0x4000019")]
    	[FieldOffset(Offset = "0x40")]
    	[Header("Parameters for Label")]
    	[SerializeField]
    	private Color[] _labelColors;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30D3C0
    	// MPImageAppearanceChanger.Reset inlined (GetComponent<MPImage> -> +0x20), then
    	// GetComponentInChildren<TextMeshProUGUI>() -> +0x38.
    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0x30D3C0", Offset = "0x30BDC0", VA = "0x18030D3C0", Slot = "4")]
    	protected override void Reset()
    	{
    		base.Reset();
    		_label = GetComponentInChildren<TextMeshProUGUI>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30D500
    	// MPImageAppearanceChanger.ChangeAppearance(int) inlined byte-for-byte (0x30D9E0), then the same range test against
    	// `_labelColors` (+0x40): `_label` (+0x38) Graphic slot 23 color, or the same LogError message with context this.
    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0x30D500", Offset = "0x30BF00", VA = "0x18030D500", Slot = "5")]
    	public override void ChangeAppearance(int appearanceIndex)
    	{
    		base.ChangeAppearance(appearanceIndex);
    		if (appearanceIndex >= 0 && appearanceIndex < _labelColors.Length)
    		{
    			_label.color = _labelColors[appearanceIndex];
    		}
    		else
    		{
    			Debug.LogError(string.Format("The specified index is out of range: {0}", appearanceIndex), this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (index spelling: B) | RVA: 0x30D7A0
    	// Real call to MPImageAppearanceChanger.ChangeAppearance(bool) (0x30DB70); `_labelColors.Length >= 2` -> `_label.color`
    	// from index `movzx zeroOrOne`; otherwise LogError("Data length is not long enough: {0}", _labelColors.Length, this).
    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0x30D7A0", Offset = "0x30C1A0", VA = "0x18030D7A0", Slot = "6")]
    	public override void ChangeAppearance(bool zeroOrOne)
    	{
    		base.ChangeAppearance(zeroOrOne);
    		if (_labelColors.Length >= 2)
    		{
    			_label.color = _labelColors[zeroOrOne ? 1 : 0];
    		}
    		else
    		{
    			Debug.LogError(string.Format("Data length is not long enough: {0}", _labelColors.Length), this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x30D8F0 (shared empty MonoBehaviour ctor)
    	// No field initializers.
    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public LabeledMPImageAppearanceChanger()
    	{
    	}
    }
}
