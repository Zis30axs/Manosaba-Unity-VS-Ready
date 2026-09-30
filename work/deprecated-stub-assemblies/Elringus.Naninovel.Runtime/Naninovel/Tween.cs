using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002CC")]
    public readonly struct Tween : IEquatable<Tween>
    {
    	[Token(Token = "0x4000B0D")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly float Duration;

    	[Token(Token = "0x4000B0E")]
    	[FieldOffset(Offset = "0x4")]
    	public readonly EasingType Easing;

    	[Token(Token = "0x4000B0F")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly bool Scale;

    	[Token(Token = "0x4000B10")]
    	[FieldOffset(Offset = "0x9")]
    	public readonly bool Complete;

    	[Token(Token = "0x6000D6E")]
    	[Address(RVA = "0x5459A0", Offset = "0x5443A0", VA = "0x1805459A0")]
    	public Tween(float duration, EasingType easing = EasingType.Linear, bool scale = true, bool complete = true)
    	{ this = default; }

    	[Token(Token = "0x6000D6F")]
    	[Address(RVA = "0x5459C0", Offset = "0x5443C0", VA = "0x1805459C0", Slot = "4")]
    	public bool Equals(Tween other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000D70")]
    	[Address(RVA = "0x545A10", Offset = "0x544410", VA = "0x180545A10", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000D71")]
    	[Address(RVA = "0x545AE0", Offset = "0x5444E0", VA = "0x180545AE0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
