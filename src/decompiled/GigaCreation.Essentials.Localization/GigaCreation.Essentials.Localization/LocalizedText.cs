using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization;

[Serializable]
[Token(Token = "0x2000015")]
public class LocalizedText
{
	[Token(Token = "0x4000038")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private LocaleKind _locale;

	[Token(Token = "0x4000039")]
	[FieldOffset(Offset = "0x18")]
	[TextArea(1, 10)]
	[SerializeField]
	private string _text;

	[Token(Token = "0x17000010")]
	public LocaleKind Locale
	{
		[Token(Token = "0x600003A")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(LocaleKind);
		}
	}

	[Token(Token = "0x17000011")]
	public string Text
	{
		[Token(Token = "0x600003B")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600003C")]
	[Address(RVA = "0x38F5B0", Offset = "0x38DFB0", VA = "0x18038F5B0")]
	public LocalizedText(LocaleKind locale, string text)
	{
	}
}
