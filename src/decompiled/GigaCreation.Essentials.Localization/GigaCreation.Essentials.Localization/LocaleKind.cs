using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization;

[Token(Token = "0x200000A")]
public enum LocaleKind
{
	[Token(Token = "0x400000B")]
	[InspectorName("ja")]
	Ja,
	[Token(Token = "0x400000C")]
	[InspectorName("en-US")]
	EnUs,
	[Token(Token = "0x400000D")]
	[InspectorName("zh-Hans")]
	ZhHans,
	[Token(Token = "0x400000E")]
	[InspectorName("zh-Hant")]
	ZhHant,
	[Token(Token = "0x400000F")]
	[InspectorName("ko")]
	Ko
}
