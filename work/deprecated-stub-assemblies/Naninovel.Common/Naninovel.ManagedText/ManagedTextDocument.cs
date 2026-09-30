#nullable enable annotations
﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.ManagedText
{
    [Token(Token = "0x200008B")]
    public class ManagedTextDocument
    {
    	[Token(Token = "0x40001D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly Dictionary<string, ManagedTextRecord> map;

    	[Token(Token = "0x170000D2")]
    	public string Header
    	{
    		[Token(Token = "0x6000328")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000D3")]
    	public IReadOnlyCollection<ManagedTextRecord> Records
    	{
    		[Token(Token = "0x6000329")]
    		[Address(RVA = "0x10F3ED0", Offset = "0x10F28D0", VA = "0x1810F3ED0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600032A")]
    	[Address(RVA = "0x10F3F20", Offset = "0x10F2920", VA = "0x1810F3F20")]
    	public ManagedTextDocument(IEnumerable<ManagedTextRecord> records, [Optional] string? header)
    	{
    	}

    	[Token(Token = "0x600032B")]
    	[Address(RVA = "0x10F4260", Offset = "0x10F2C60", VA = "0x1810F4260")]
    	public bool Contains(string key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600032C")]
    	[Address(RVA = "0x10F42C0", Offset = "0x10F2CC0", VA = "0x1810F42C0")]
    	public ManagedTextRecord Get(string key)
    	{
    		return default(ManagedTextRecord);
    	}

    	[Token(Token = "0x600032D")]
    	[Address(RVA = "0x10F4350", Offset = "0x10F2D50", VA = "0x1810F4350")]
    	public bool TryGet(string key, out ManagedTextRecord record)
    	{
    		record = default;
    		record = default;
    		return default(bool);
    	}
    }
}
