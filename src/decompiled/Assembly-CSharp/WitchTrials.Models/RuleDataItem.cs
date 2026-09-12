using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x200015D")]
public class RuleDataItem
{
	[Token(Token = "0x40006A3")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string _numbering;

	[Token(Token = "0x40006A4")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private LocalizedText[] _subtitle;

	[Token(Token = "0x40006A5")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private LocalizedText[] _description;

	[Token(Token = "0x170000CA")]
	public string Numbering
	{
		[Token(Token = "0x60005B4")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000CB")]
	public IReadOnlyList<LocalizedText> Subtitle
	{
		[Token(Token = "0x60005B5")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000CC")]
	public IReadOnlyList<LocalizedText> Description
	{
		[Token(Token = "0x60005B6")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005B7")]
	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
	public RuleDataItem(string numbering, LocalizedText[] subtitle, LocalizedText[] description)
	{
	}
}
