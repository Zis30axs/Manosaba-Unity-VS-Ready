using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000233")]
    public readonly struct RawDataRepresentation : IEquatable<RawDataRepresentation>
    {
    	[Token(Token = "0x40008B8")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string Extension;

    	[Token(Token = "0x40008B9")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly string MimeType;

    	[Token(Token = "0x60009D3")]
    	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
    	public RawDataRepresentation(string extension, string mimeType)
    	{ this = default; }

    	[Token(Token = "0x60009D4")]
    	[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
    	public bool Equals(RawDataRepresentation other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009D5")]
    	[Address(RVA = "0x528B20", Offset = "0x527520", VA = "0x180528B20", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009D6")]
    	[Address(RVA = "0x528BC0", Offset = "0x5275C0", VA = "0x180528BC0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
