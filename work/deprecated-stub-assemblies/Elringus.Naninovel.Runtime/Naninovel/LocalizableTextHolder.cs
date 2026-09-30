using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000386")]
    public class LocalizableTextHolder : IEquatable<LocalizableTextHolder>
    {
    	[Token(Token = "0x17000250")]
    	public LocalizableText Text
    	{
    		[Token(Token = "0x600111B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(LocalizableText);
    		}
    	}

    	[Token(Token = "0x17000251")]
    	public object Holder
    	{
    		[Token(Token = "0x600111C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600111D")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public LocalizableTextHolder(LocalizableText text, object holder)
    	{
    	}

    	[Token(Token = "0x600111E")]
    	[Address(RVA = "0x584880", Offset = "0x583280", VA = "0x180584880", Slot = "4")]
    	public bool Equals(LocalizableTextHolder other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600111F")]
    	[Address(RVA = "0x584950", Offset = "0x583350", VA = "0x180584950", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001120")]
    	[Address(RVA = "0x584AD0", Offset = "0x5834D0", VA = "0x180584AD0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001121")]
    	[Address(RVA = "0x584B50", Offset = "0x583550", VA = "0x180584B50", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}
    }
}
