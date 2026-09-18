using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Serializable]
    [Token(Token = "0x2000154")]
    public class ClueDataItem
    {
    	[Token(Token = "0x400067A")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private LocalizedText[] _name;

    	[Token(Token = "0x400067B")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocalizedText[] _description;

    	[Token(Token = "0x170000C0")]
    	public IReadOnlyList<LocalizedText> Name
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (generic reference-field getter)
    		// 466 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_name` (+0x10, LocalizedText[]).
    		[Token(Token = "0x60005A2")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _name;
    		}
    	}

    	[Token(Token = "0x170000C1")]
    	public IReadOnlyList<LocalizedText> Description
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_description` (+0x18, LocalizedText[]).
    		[Token(Token = "0x60005A3")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _description;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36C0D0
    	// Generic "store two reference-typed constructor params into fields at
    	// +0x10/+0x18" thunk (63+ unrelated methods project-wide share this exact
    	// body -- see work/reversing/README.md on body folding): `_name = name;
    	// _description = description;`, GC write barrier on each store, no other logic.
    	[Token(Token = "0x60005A4")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public ClueDataItem(LocalizedText[] name, LocalizedText[] description)
    	{
    		_name = name;
    		_description = description;
    	}
    }
}
