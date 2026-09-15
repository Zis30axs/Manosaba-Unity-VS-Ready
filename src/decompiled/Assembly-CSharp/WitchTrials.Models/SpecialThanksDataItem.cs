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
		// RECONSTRUCTED | Confidence: A | RVA: 0x331650 (generic 4-byte-field getter)
		// 149 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_course` (+0x10, SpecialThanksCourse).
		[Token(Token = "0x60005C0")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return _course;
		}
	}

	[Token(Token = "0x170000D4")]
	public LocalizedText[] Name
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
		// 601 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_name` (+0x18, LocalizedText[]).
		[Token(Token = "0x60005C1")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return _name;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F5B0, 82 B (shared int+ref store body)
	// Stores `course` (edx) to +0x10 and `name` (r8) to +0x18.
	[Token(Token = "0x60005C2")]
	[Address(RVA = "0x38F5B0", Offset = "0x38DFB0", VA = "0x18038F5B0")]
	public SpecialThanksDataItem(SpecialThanksCourse course, LocalizedText[] name)
	{
		_course = course;
		_name = name;
	}
}
