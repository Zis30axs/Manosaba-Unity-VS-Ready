using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Serializable]
    [Token(Token = "0x2000015")]
    public class LocalizedText
    {
    	[Token(Token = "0x4000038")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private LocaleKind _locale;

    	[Token(Token = "0x4000039")]
    	[FieldOffset(Offset = "0x18")]
    	[TextArea(1, 10)]
    	[SerializeField]
    	private string _text;

    	[Token(Token = "0x17000010")]
    	public LocaleKind Locale
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x331650, 4 B
    		//
    		// `mov eax,[rcx+0x10]; ret` -- a plain 4-byte backing-field read.  This RVA is an
    		// identical-code-folded body shared with every `get` of a 4-byte field at +0x10
    		// (LocaleData.Kind and SpecialThanksDataItem.Course are the two already reconstructed in this
    		// project, 149 methods in total), so the offset is checked against dump.cs rather than inferred
    		// from the disassembly alone: `LocalizedText._locale // 0x10`, `[SerializeField]`.
    		//
    		// ON THE BOOT PATH, and the reason this was reconstructed: `AuthorTextBuilder.LoadDataAsync`
    		// (AuthorTextBuilder.cs:495) builds `item.Name.ToDictionary(t => t.Locale, t => t.Text)`.  While
    		// this returned `default(LocaleKind)` every entry of every name list keyed to LocaleKind 0 (`Ja`),
    		// so the load threw `ArgumentException: An item with the same key has already been added. Key: Ja`
    		// -- the same failure shape LocaleData.Kind had for LocalizationPresenter.Awake.
    		[Token(Token = "0x600003A")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		get
    		{
    			return _locale;
    		}
    	}

    	[Token(Token = "0x17000011")]
    	public string Text
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0, 5 B
    		//
    		// `mov rax,[rcx+0x18]; ret` -- the reference backing-field read, an identical-code-folded body
    		// shared with every `get` of a reference field at +0x18 (601 methods project-wide; CharacterData
    		// .Items, CharacterDataItem.Name and LocaleData.Item are the siblings already reconstructed here).
    		// dump.cs: `LocalizedText._text // 0x18`, `[TextArea(1, 10)] [SerializeField]`.
    		[Token(Token = "0x600003B")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _text;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38F5B0, 82 B (shared int+ref store body)
    	// The same folded body SpecialThanksDataItem's constructor uses: stores the 4-byte first argument
    	// (edx) to +0x10 and the reference second argument (r8) to +0x18, matching `_locale` (+0x10,
    	// LocaleKind) and `_text` (+0x18, string) in dump.cs.
    	[Token(Token = "0x600003C")]
    	[Address(RVA = "0x38F5B0", Offset = "0x38DFB0", VA = "0x18038F5B0")]
    	public LocalizedText(LocaleKind locale, string text)
    	{
    		_locale = locale;
    		_text = text;
    	}
    }
}
