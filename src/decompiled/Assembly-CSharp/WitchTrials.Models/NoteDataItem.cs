using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x2000159")]
public class NoteDataItem
{
	[Token(Token = "0x400069E")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private LocalizedText[] _title;

	[Token(Token = "0x400069F")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private LocalizedText[] _description;

	[Token(Token = "0x170000C5")]
	public IReadOnlyList<LocalizedText> Title
	{
		[Token(Token = "0x60005AB")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000C6")]
	public IReadOnlyList<LocalizedText> Description
	{
		[Token(Token = "0x60005AC")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005AD")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public NoteDataItem(LocalizedText[] title, LocalizedText[] description)
	{
	}
}
