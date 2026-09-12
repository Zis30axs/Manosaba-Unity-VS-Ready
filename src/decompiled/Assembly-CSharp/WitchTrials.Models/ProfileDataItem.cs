using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x200015B")]
public class ProfileDataItem
{
	[Token(Token = "0x40006A1")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private LocalizedText[] _description;

	[Token(Token = "0x170000C8")]
	public IReadOnlyList<LocalizedText> Description
	{
		[Token(Token = "0x60005B0")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005B1")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public ProfileDataItem(LocalizedText[] description)
	{
	}
}
