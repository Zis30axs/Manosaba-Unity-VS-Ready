using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000156")]
public sealed class MapData : WitchBookDataBase<MapDataItem>
{
	[Token(Token = "0x400069B")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<VersionedItem<MapDataItem>> _items;

	[Token(Token = "0x170000C2")]
	public override IList<VersionedItem<MapDataItem>> Items
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
		// 601 project-wide methods fold to this same native body; the offset determines
		// which field each one reads. Here that is `_items` (+0x18, List<VersionedItem<MapDataItem>>).
		[Token(Token = "0x60005A5")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
		get
		{
			return _items;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38F2B0, 57 B
	// This is only the implicit `base()`: `WitchBookDataBase<MapDataItem>..ctor` is inlined down to
	// `ScriptableObject..ctor`. There is no field initializer, so `_items` (+0x18) stays null until Unity
	// deserializes it.
	[Token(Token = "0x60005A6")]
	[Address(RVA = "0x38F2B0", Offset = "0x38DCB0", VA = "0x18038F2B0")]
	public MapData()
	{
	}
}
