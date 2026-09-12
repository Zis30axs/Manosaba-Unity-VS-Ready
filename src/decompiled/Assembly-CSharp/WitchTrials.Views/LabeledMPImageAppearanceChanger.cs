using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views;

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

	[Token(Token = "0x6000028")]
	[Address(RVA = "0x30D3C0", Offset = "0x30BDC0", VA = "0x18030D3C0", Slot = "4")]
	protected override void Reset()
	{
	}

	[Token(Token = "0x6000029")]
	[Address(RVA = "0x30D500", Offset = "0x30BF00", VA = "0x18030D500", Slot = "5")]
	public override void ChangeAppearance(int appearanceIndex)
	{
	}

	[Token(Token = "0x600002A")]
	[Address(RVA = "0x30D7A0", Offset = "0x30C1A0", VA = "0x18030D7A0", Slot = "6")]
	public override void ChangeAppearance(bool zeroOrOne)
	{
	}

	[Token(Token = "0x600002B")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LabeledMPImageAppearanceChanger()
	{
	}
}
