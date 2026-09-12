using System;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x2000160")]
public class SpecialThanksDataItem
{
	[Token(Token = "0x40006AC")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private SpecialThanksCourse _course;

	[Token(Token = "0x40006AD")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private LocalizedText[] _name;

	[Token(Token = "0x170000D3")]
	public SpecialThanksCourse Course
	{
		[Token(Token = "0x60005C0")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(SpecialThanksCourse);
		}
	}

	[Token(Token = "0x170000D4")]
	public LocalizedText[] Name
	{
		[Token(Token = "0x60005C1")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005C2")]
	[Address(RVA = "0x38F5B0", Offset = "0x38DFB0", VA = "0x18038F5B0")]
	public SpecialThanksDataItem(SpecialThanksCourse course, LocalizedText[] name)
	{
	}
}
