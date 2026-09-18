using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Serializable]
    [Token(Token = "0x2000008")]
    public class LocaleData
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private LocaleKind _kind;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private LocaleDataItem _item;

    	[Token(Token = "0x17000006")]
    	public LocaleKind Kind
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x331650, 4 B
    		//
    		// The body is 4 bytes: `mov eax,[rcx+0x10]; ret` -- a plain backing-field read.  This RVA is an
    		// identical-code-folded body shared with every `get` of a 4-byte field at +0x10 (System.String$$get_Length,
    		// System.Version$$get_Major, ...), which is exactly why the offset is checked against dump.cs rather than
    		// inferred from the disassembly alone: `LocaleData._kind // 0x10` and `[SerializeField]`.
    		// This getter is on the boot path: LocalizationPresenter.Awake (RVA 0xCD34B0) builds its locale
    		// dictionary with `data => data.Kind`, so while it returned `default` every entry keyed to LocaleKind 0.
    		[Token(Token = "0x600000C")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		get
    		{
    			return _kind;
    		}
    	}

    	[Token(Token = "0x17000007")]
    	public LocaleDataItem Item
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0, 5 B
    		//
    		// 5 bytes: `mov rax,[rcx+0x18]; ret` -- the reference backing-field read, again a folded body shared
    		// with every `get` of a reference field at +0x18.  dump.cs: `LocaleData._item // 0x18`,
    		// `[SerializeField]`.  Used by the same LocalizationPresenter.Awake lambda (`data => data.Item`).
    		[Token(Token = "0x600000D")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _item;
    		}
    	}

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public LocaleData()
    	{
    	}
    }
}
