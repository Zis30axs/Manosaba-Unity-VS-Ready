using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x200015C")]
public class RuleData : WitchBookDataBase<RuleDataItem>
{
	[Token(Token = "0x40006A2")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<VersionedItem<RuleDataItem>> _items;

	[Token(Token = "0x170000C9")]
	public override IList<VersionedItem<RuleDataItem>> Items
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
		// 601 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_items` (+0x18, List<VersionedItem<RuleDataItem>>).
		[Token(Token = "0x60005B2")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
		get
		{
			return _items;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F370, 57 B
	// This is only the implicit `base()`: `WitchBookDataBase<RuleDataItem>..ctor` is inlined down to
	// `ScriptableObject..ctor`. There is no field initializer, so `_items` (+0x18) stays null until Unity
	// deserializes it.
	[Token(Token = "0x60005B3")]
	[Address(RVA = "0x38F370", Offset = "0x38DD70", VA = "0x18038F370")]
	public RuleData()
	{
	}
}
