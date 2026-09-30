using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20003FF")]
    public readonly struct ParameterInfo : IEquatable<ParameterInfo>
    {
    	[Token(Token = "0x1700029E")]
    	public ICommandParameter Instance
    	{
    		[Token(Token = "0x600133F")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700029F")]
    	public string Id
    	{
    		[Token(Token = "0x6001340")]
    		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002A0")]
    	public string Alias
    	{
    		[Token(Token = "0x6001341")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002A1")]
    	public string DefaultValue
    	{
    		[Token(Token = "0x6001342")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002A2")]
    	public bool Required
    	{
    		[Token(Token = "0x6001343")]
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002A3")]
    	public bool Nameless
    	{
    		[Token(Token = "0x6001344")]
    		[Address(RVA = "0x5AA270", Offset = "0x5A8C70", VA = "0x1805AA270")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001345")]
    	[Address(RVA = "0x5AA2F0", Offset = "0x5A8CF0", VA = "0x1805AA2F0")]
    	public ParameterInfo(ICommandParameter instance, string id, [Optional] string alias, [Optional] string defaultValue, bool required = false)
    	{ this = default; }

    	[Token(Token = "0x6001346")]
    	[Address(RVA = "0x5AA450", Offset = "0x5A8E50", VA = "0x1805AA450", Slot = "4")]
    	public bool Equals(ParameterInfo other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001347")]
    	[Address(RVA = "0x5AA490", Offset = "0x5A8E90", VA = "0x1805AA490", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001348")]
    	[Address(RVA = "0x5AA550", Offset = "0x5A8F50", VA = "0x1805AA550", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
