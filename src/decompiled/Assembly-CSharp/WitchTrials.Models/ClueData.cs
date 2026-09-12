using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000153")]
public sealed class ClueData : WitchBookDataBase<ClueDataItem>
{
	[Token(Token = "0x4000679")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<VersionedItem<ClueDataItem>> _items;

	[Token(Token = "0x170000BF")]
	public override IList<VersionedItem<ClueDataItem>> Items
	{
		[Token(Token = "0x60005A0")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005A1")]
	[Address(RVA = "0x38F270", Offset = "0x38DC70", VA = "0x18038F270")]
	public ClueData()
	{
	}
}
