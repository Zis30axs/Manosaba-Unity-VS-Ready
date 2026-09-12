using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000007")]
public class GlowingAndTweeningButton : TweeningButton
{
	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x148")]
	[Header("References for Glowing")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x400000D")]
	[FieldOffset(Offset = "0x150")]
	[Header("Parameters for Glowing")]
	[SerializeField]
	private bool _glowWhenEnabled;

	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x158")]
	[SerializeField]
	private LabelGlowUtility _labelGlowUtil;

	[Token(Token = "0x6000011")]
	[Address(RVA = "0xCD6B60", Offset = "0xCD5560", VA = "0x180CD6B60", Slot = "45")]
	protected override void OnEnablingChanged(bool isEnabled)
	{
	}

	[Token(Token = "0x6000012")]
	[Address(RVA = "0x30CA10", Offset = "0x30B410", VA = "0x18030CA10")]
	public GlowingAndTweeningButton()
	{
	}
}
