using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x200015A")]
public sealed class ProfileData : WitchBookDataBase<ProfileDataItem>
{
	[Token(Token = "0x40006A0")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<VersionedItem<ProfileDataItem>> _items;

	[Token(Token = "0x170000C7")]
	public override IList<VersionedItem<ProfileDataItem>> Items
	{
		[Token(Token = "0x60005AE")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005AF")]
	[Address(RVA = "0x38F330", Offset = "0x38DD30", VA = "0x18038F330")]
	public ProfileData()
	{
	}
}
