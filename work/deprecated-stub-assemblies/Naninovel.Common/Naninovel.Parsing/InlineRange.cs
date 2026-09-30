#nullable enable annotations
﻿using System;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x200002A")]
    public readonly struct InlineRange : IEquatable<InlineRange>
    {
    	[Token(Token = "0x4000048")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly int Start;

    	[Token(Token = "0x4000049")]
    	[FieldOffset(Offset = "0x4")]
    	public readonly int Length;

    	[Token(Token = "0x1700001C")]
    	public int End
    	{
    		[Token(Token = "0x60000AC")]
    		[Address(RVA = "0x10D3A80", Offset = "0x10D2480", VA = "0x1810D3A80")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0x10D3A90", Offset = "0x10D2490", VA = "0x1810D3A90")]
    	public InlineRange(int start, int length)
    	{ this = default; }

    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0x10D3B70", Offset = "0x10D2570", VA = "0x1810D3B70", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0x525680", Offset = "0x524080", VA = "0x180525680", Slot = "4")]
    	public bool Equals(InlineRange other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000B0")]
    	[Address(RVA = "0x10D3C30", Offset = "0x10D2630", VA = "0x1810D3C30", Slot = "0")]
    	public override bool Equals(object? obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000B1")]
    	[Address(RVA = "0x525730", Offset = "0x524130", VA = "0x180525730", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
