using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x200019F")]
    public class AuthorData : ScriptableObject
    {
    	[Token(Token = "0x4000782")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private List<AuthorDataItem> _items;

    	[Token(Token = "0x1700012A")]
    	public IList<AuthorDataItem> Items
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_items` (+0x18, List<AuthorDataItem>).
    		[Token(Token = "0x6000718")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _items;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A3070, 197 B
    	// This is the field initializer `_items (+0x18) = new List<AuthorDataItem>()` (List..ctor 0x857BA0),
    	// then a tail jump to `ScriptableObject..ctor`.
    	[Token(Token = "0x6000719")]
    	[Address(RVA = "0x3A3070", Offset = "0x3A1A70", VA = "0x1803A3070")]
    	public AuthorData()
    	{
    		_items = new List<AuthorDataItem>();
    	}
    }
}
