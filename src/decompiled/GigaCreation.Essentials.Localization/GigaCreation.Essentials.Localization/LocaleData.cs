using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization;

[Serializable]
[Token(Token = "0x2000008")]
public class LocaleData
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private LocaleKind _kind;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private LocaleDataItem _item;

	[Token(Token = "0x17000006")]
	public LocaleKind Kind
	{
		[Token(Token = "0x600000C")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(LocaleKind);
		}
	}

	[Token(Token = "0x17000007")]
	public LocaleDataItem Item
	{
		[Token(Token = "0x600000D")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public LocaleData()
	{
	}
}
