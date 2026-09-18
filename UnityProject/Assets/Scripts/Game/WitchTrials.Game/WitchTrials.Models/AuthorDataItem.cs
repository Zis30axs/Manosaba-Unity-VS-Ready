using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Serializable]
    [Token(Token = "0x20001A0")]
    public class AuthorDataItem
    {
    	[Token(Token = "0x4000783")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string _id;

    	[Token(Token = "0x4000784")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocalizedText[] _taggedText;

    	[Token(Token = "0x1700012B")]
    	public string Id
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (generic reference-field getter)
    		// 466 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_id` (+0x10, string).
    		[Token(Token = "0x600071A")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _id;
    		}
    	}

    	[Token(Token = "0x1700012C")]
    	public IReadOnlyList<LocalizedText> TaggedText
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_taggedText` (+0x18, LocalizedText[]).
    		[Token(Token = "0x600071B")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _taggedText;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36C0D0 (shared body with
    	// ClueDataItem's constructor -- see ClueDataItem.cs for the evidence)
    	[Token(Token = "0x600071C")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public AuthorDataItem(string id, LocalizedText[] taggedText)
    	{
    		_id = id;
    		_taggedText = taggedText;
    	}
    }
}
