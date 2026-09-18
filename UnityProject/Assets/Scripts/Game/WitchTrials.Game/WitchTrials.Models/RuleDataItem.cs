using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Serializable]
    [Token(Token = "0x200015D")]
    public class RuleDataItem
    {
    	[Token(Token = "0x40006A3")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string _numbering;

    	[Token(Token = "0x40006A4")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocalizedText[] _subtitle;

    	[Token(Token = "0x40006A5")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private LocalizedText[] _description;

    	[Token(Token = "0x170000CA")]
    	public string Numbering
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (generic reference-field getter)
    		// 466 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_numbering` (+0x10, string).
    		[Token(Token = "0x60005B4")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return _numbering;
    		}
    	}

    	[Token(Token = "0x170000CB")]
    	public IReadOnlyList<LocalizedText> Subtitle
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (generic reference-field getter)
    		// 601 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_subtitle` (+0x18, LocalizedText[]).
    		[Token(Token = "0x60005B5")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _subtitle;
    		}
    	}

    	[Token(Token = "0x170000CC")]
    	public IReadOnlyList<LocalizedText> Description
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
    		// 292 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_description` (+0x20, LocalizedText[]).
    		[Token(Token = "0x60005B6")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _description;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38F3B0, 255 B (shared three-ref-store body)
    	// Stores `numbering`, `subtitle` and `description` to +0x10/+0x18/+0x20.
    	[Token(Token = "0x60005B7")]
    	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
    	public RuleDataItem(string numbering, LocalizedText[] subtitle, LocalizedText[] description)
    	{
    		_numbering = numbering;
    		_subtitle = subtitle;
    		_description = description;
    	}
    }
}
