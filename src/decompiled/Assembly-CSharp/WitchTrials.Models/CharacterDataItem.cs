using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x2000152")]
public class CharacterDataItem
{
	[Token(Token = "0x4000673")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string _id;

	[Token(Token = "0x4000674")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private LocalizedText[] _name;

	[Token(Token = "0x4000675")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private LocalizedText[] _familyName;

	[Token(Token = "0x4000676")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private string _age;

	[Token(Token = "0x4000677")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private string _height;

	[Token(Token = "0x4000678")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private string _weight;

	[Token(Token = "0x170000B9")]
	public string Id
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (generic reference-field getter)
		// 466 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_id` (+0x10, string).
		[Token(Token = "0x6000599")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return _id;
		}
	}

	[Token(Token = "0x170000BA")]
	public IReadOnlyList<LocalizedText> Name
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18 `_name`)
		[Token(Token = "0x600059A")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return _name;
		}
	}

	[Token(Token = "0x170000BB")]
	public IReadOnlyList<LocalizedText> FamilyName
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
		// 292 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_familyName` (+0x20, LocalizedText[]).
		[Token(Token = "0x600059B")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return _familyName;
		}
	}

	[Token(Token = "0x170000BC")]
	public string Age
	{
		[Token(Token = "0x600059C")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000BD")]
	public string Height
	{
		[Token(Token = "0x600059D")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000BE")]
	public string Weight
	{
		[Token(Token = "0x600059E")]
		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410")]
		get
		{
			return null;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F070, 511 B
	// Six stores in parameter order to +0x10..+0x38, each with a write barrier; the last three args come from the stack.
	[Token(Token = "0x600059F")]
	[Address(RVA = "0x38F070", Offset = "0x38DA70", VA = "0x18038F070")]
	public CharacterDataItem(string id, LocalizedText[] name, LocalizedText[] familyName, string age, string height, string weight)
	{
		_id = id;
		_name = name;
		_familyName = familyName;
		_age = age;
		_height = height;
		_weight = weight;
	}
}
