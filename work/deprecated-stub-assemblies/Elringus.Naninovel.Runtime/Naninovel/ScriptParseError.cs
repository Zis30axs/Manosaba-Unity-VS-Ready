using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200041A")]
    public readonly struct ScriptParseError : IEquatable<ScriptParseError>
    {
    	[Token(Token = "0x4000F02")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly int LineIndex;

    	[Token(Token = "0x4000F03")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly string Description;

    	[Token(Token = "0x170002C3")]
    	public int LineNumber
    	{
    		[Token(Token = "0x60013EB")]
    		[Address(RVA = "0x5B4280", Offset = "0x5B2C80", VA = "0x1805B4280")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x60013EC")]
    	[Address(RVA = "0x5B4290", Offset = "0x5B2C90", VA = "0x1805B4290")]
    	public ScriptParseError(int lineIndex, string description)
    	{ this = default; }

    	[Token(Token = "0x60013ED")]
    	[Address(RVA = "0x5B4300", Offset = "0x5B2D00", VA = "0x1805B4300")]
    	public string ToString(string scriptPathOrName)
    	{
    		return null;
    	}

    	[Token(Token = "0x60013EE")]
    	[Address(RVA = "0x5B44D0", Offset = "0x5B2ED0", VA = "0x1805B44D0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60013EF")]
    	[Address(RVA = "0x5B4510", Offset = "0x5B2F10", VA = "0x1805B4510", Slot = "4")]
    	public bool Equals(ScriptParseError other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013F0")]
    	[Address(RVA = "0x5B4560", Offset = "0x5B2F60", VA = "0x1805B4560", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013F1")]
    	[Address(RVA = "0x5B4510", Offset = "0x5B2F10", VA = "0x1805B4510")]
    	public static bool operator ==(ScriptParseError left, ScriptParseError right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013F2")]
    	[Address(RVA = "0x5B4630", Offset = "0x5B3030", VA = "0x1805B4630")]
    	public static bool operator !=(ScriptParseError left, ScriptParseError right)
    	{
    		return default(bool);
    	}
    }
}
