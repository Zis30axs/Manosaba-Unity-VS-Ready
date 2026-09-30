using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000252")]
    public readonly struct CachedResourceLocation : IEquatable<CachedResourceLocation>
    {
    	[Token(Token = "0x400091D")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string Path;

    	[Token(Token = "0x400091E")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly Type Type;

    	[Token(Token = "0x6000A48")]
    	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
    	public CachedResourceLocation(string path, Type type)
    	{ this = default; }

    	[Token(Token = "0x6000A49")]
    	[Address(RVA = "0x530710", Offset = "0x52F110", VA = "0x180530710")]
    	public CachedResourceLocation(string path, string typeName)
    	{ this = default; }

    	[Token(Token = "0x6000A4A")]
    	[Address(RVA = "0x5307A0", Offset = "0x52F1A0", VA = "0x1805307A0")]
    	public static bool operator ==(CachedResourceLocation left, CachedResourceLocation right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000A4B")]
    	[Address(RVA = "0x530840", Offset = "0x52F240", VA = "0x180530840")]
    	public static bool operator !=(CachedResourceLocation left, CachedResourceLocation right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000A4C")]
    	[Address(RVA = "0x5308E0", Offset = "0x52F2E0", VA = "0x1805308E0", Slot = "4")]
    	public bool Equals(CachedResourceLocation other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000A4D")]
    	[Address(RVA = "0x530960", Offset = "0x52F360", VA = "0x180530960", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000A4E")]
    	[Address(RVA = "0x530A60", Offset = "0x52F460", VA = "0x180530A60", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
