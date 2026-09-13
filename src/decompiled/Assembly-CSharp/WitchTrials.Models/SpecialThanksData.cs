using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x200015E")]
public class SpecialThanksData : ScriptableObject
{
	[Token(Token = "0x40006A6")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<TextSizeByCourse> _textSizeByCourses;

	[Token(Token = "0x40006A7")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private List<SpecialThanksDataItem> _items;

	[Token(Token = "0x170000CD")]
	public IList<TextSizeByCourse> TextSizeByCourses
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
		// 601 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_textSizeByCourses` (+0x18, List<TextSizeByCourse>).
		[Token(Token = "0x60005B8")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return _textSizeByCourses;
		}
	}

	[Token(Token = "0x170000CE")]
	public IList<SpecialThanksDataItem> Items
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
		// 292 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_items` (+0x20, List<SpecialThanksDataItem>).
		[Token(Token = "0x60005B9")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return _items;
		}
	}

	[Token(Token = "0x60005BA")]
	[Address(RVA = "0x38F4B0", Offset = "0x38DEB0", VA = "0x18038F4B0")]
	public SpecialThanksData()
	{
	}
}
