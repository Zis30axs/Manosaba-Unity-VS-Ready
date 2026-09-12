using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.UI;

[Token(Token = "0x2000004")]
public class FontChangerModified : FontChanger
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private FontMaterialChanger _fontMaterialChanger;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x48")]
	[SerializeField]
	private TMP_Text[] _targetLabels;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0xD340C0", Offset = "0xD32AC0", VA = "0x180D340C0")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD34160", Offset = "0xD32B60", VA = "0x180D34160", Slot = "8")]
	protected override void HandleFontNameChanged(string fontName)
	{
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x63C4B0", Offset = "0x63AEB0", VA = "0x18063C4B0")]
	public FontChangerModified()
	{
	}
}
