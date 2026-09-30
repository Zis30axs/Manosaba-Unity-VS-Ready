using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20004DC")]
    public readonly struct UnlockableItemUpdatedArgs : IEquatable<UnlockableItemUpdatedArgs>
    {
    	[Token(Token = "0x400128E")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string Id;

    	[Token(Token = "0x400128F")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly bool Unlocked;

    	[Token(Token = "0x4001290")]
    	[FieldOffset(Offset = "0x9")]
    	public readonly bool Added;

    	[Token(Token = "0x6001853")]
    	[Address(RVA = "0x605550", Offset = "0x603F50", VA = "0x180605550")]
    	public UnlockableItemUpdatedArgs(string id, bool unlocked, bool added)
    	{ this = default; }

    	[Token(Token = "0x6001854")]
    	[Address(RVA = "0x6055C0", Offset = "0x603FC0", VA = "0x1806055C0", Slot = "4")]
    	public bool Equals(UnlockableItemUpdatedArgs other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001855")]
    	[Address(RVA = "0x605640", Offset = "0x604040", VA = "0x180605640", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001856")]
    	[Address(RVA = "0x605710", Offset = "0x604110", VA = "0x180605710", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
