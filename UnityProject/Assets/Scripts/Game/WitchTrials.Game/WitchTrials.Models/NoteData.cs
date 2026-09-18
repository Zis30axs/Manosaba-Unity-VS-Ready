using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000158")]
    public class NoteData : WitchBookDataBase<NoteDataItem>
    {
    	[Token(Token = "0x400069D")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private List<VersionedItem<NoteDataItem>> _items;

    	[Token(Token = "0x170000C4")]
    	public override IList<VersionedItem<NoteDataItem>> Items
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_items` (+0x18, List<VersionedItem<NoteDataItem>>).
    		[Token(Token = "0x60005A9")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
    		get
    		{
    			return _items;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38F2F0, 57 B
    	// This is only the implicit `base()`: `WitchBookDataBase<NoteDataItem>..ctor` is inlined down to
    	// `ScriptableObject..ctor`. There is no field initializer, so `_items` (+0x18) stays null until Unity
    	// deserializes it.
    	[Token(Token = "0x60005AA")]
    	[Address(RVA = "0x38F2F0", Offset = "0x38DCF0", VA = "0x18038F2F0")]
    	public NoteData()
    	{
    	}
    }
}
