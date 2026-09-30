#nullable enable annotations
﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000049")]
    public class GenericPrefix : ILineComponent
    {
    	[Token(Token = "0x17000046")]
    	public PlainText Author
    	{
    		[Token(Token = "0x60001A4")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000047")]
    	public PlainText? Appearance
    	{
    		[Token(Token = "0x60001A5")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60001A3")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public GenericPrefix(PlainText author, [Optional] PlainText? appearance)
    	{
    	}

    	[Token(Token = "0x60001A6")]
    	[Address(RVA = "0x10E2B20", Offset = "0x10E1520", VA = "0x1810E2B20", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}
    }
}
