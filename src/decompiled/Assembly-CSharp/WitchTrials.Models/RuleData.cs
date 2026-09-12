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
		[Token(Token = "0x60005B2")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005B3")]
	[Address(RVA = "0x38F370", Offset = "0x38DD70", VA = "0x18038F370")]
	public RuleData()
	{
	}
}
