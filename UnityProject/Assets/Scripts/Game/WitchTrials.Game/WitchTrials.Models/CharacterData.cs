using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000151")]
    public class CharacterData : ScriptableObject
    {
    	[Token(Token = "0x4000672")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private List<CharacterDataItem> _items;

    	[Token(Token = "0x170000B8")]
    	public IList<CharacterDataItem> Items
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_items` (+0x18, List<CharacterDataItem>).
    		[Token(Token = "0x6000597")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _items;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38EF90, 197 B
    	// This is the field initializer `_items (+0x18) = new List<CharacterDataItem>()` (List..ctor 0x857BA0),
    	// then a tail jump to `ScriptableObject..ctor`.
    	[Token(Token = "0x6000598")]
    	[Address(RVA = "0x38EF90", Offset = "0x38D990", VA = "0x18038EF90")]
    	public CharacterData()
    	{
    		_items = new List<CharacterDataItem>();
    	}
    }
}
