#nullable enable annotations
﻿using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000052")]
    public class Parameter : ILineComponent
    {
    	[Token(Token = "0x17000051")]
    	public PlainText? Identifier
    	{
    		[Token(Token = "0x60001BE")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000052")]
    	public MixedValue Value
    	{
    		[Token(Token = "0x60001BF")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000053")]
    	public bool Nameless
    	{
    		[Token(Token = "0x60001C0")]
    		[Address(RVA = "0x3A2AA0", Offset = "0x3A14A0", VA = "0x1803A2AA0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60001BD")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public Parameter(PlainText? identifier, MixedValue value)
    	{
    	}

    	[Token(Token = "0x60001C1")]
    	[Address(RVA = "0x10E3530", Offset = "0x10E1F30", VA = "0x1810E3530")]
    	public Parameter(MixedValue value)
    	{
    	}

    	[Token(Token = "0x60001C2")]
    	[Address(RVA = "0x10E3540", Offset = "0x10E1F40", VA = "0x1810E3540", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}
    }
}
