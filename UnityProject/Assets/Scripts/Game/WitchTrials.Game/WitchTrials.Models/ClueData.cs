using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_items` (+0x18, List<VersionedItem<ClueDataItem>>).
    		[Token(Token = "0x60005A0")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
    		get
    		{
    			return _items;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38F270, 57 B
    	// This is only the implicit `base()`: `WitchBookDataBase<ClueDataItem>..ctor` is inlined down to
    	// `ScriptableObject..ctor`. There is no field initializer, so `_items` (+0x18) stays null until Unity
    	// deserializes it.
    	[Token(Token = "0x60005A1")]
    	[Address(RVA = "0x38F270", Offset = "0x38DC70", VA = "0x18038F270")]
    	public ClueData()
    	{
    	}
    }
}
