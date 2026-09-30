using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000470")]
    public readonly struct GameSaveLoadArgs : IEquatable<GameSaveLoadArgs>
    {
    	[Token(Token = "0x4001091")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly string SlotId;

    	[Token(Token = "0x4001092")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly bool Quick;

    	[Token(Token = "0x60015D3")]
    	[Address(RVA = "0x5B2910", Offset = "0x5B1310", VA = "0x1805B2910")]
    	public GameSaveLoadArgs(string slotId, bool quick)
    	{ this = default; }

    	[Token(Token = "0x60015D4")]
    	[Address(RVA = "0x5D9EB0", Offset = "0x5D88B0", VA = "0x1805D9EB0", Slot = "4")]
    	public bool Equals(GameSaveLoadArgs other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60015D5")]
    	[Address(RVA = "0x5D9F20", Offset = "0x5D8920", VA = "0x1805D9F20", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60015D6")]
    	[Address(RVA = "0x5D9FE0", Offset = "0x5D89E0", VA = "0x1805D9FE0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
