using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.ManagedText;

[Token(Token = "0x200008C")]
public readonly struct ManagedTextRecord : IEquatable<ManagedTextRecord>
{
	[Token(Token = "0x40001D7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public readonly string Key;

	[Token(Token = "0x40001D8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	public readonly string Value;

	[Token(Token = "0x40001D9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public readonly string Comment;

	[Token(Token = "0x600032E")]
	[Address(RVA = "0x10F43C0", Offset = "0x10F2DC0", VA = "0x1810F43C0")]
	public ManagedTextRecord(string key, [Optional] string? value, [Optional] string? comment)
	{
	}

	[Token(Token = "0x600032F")]
	[Address(RVA = "0x10F44F0", Offset = "0x10F2EF0", VA = "0x1810F44F0", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6000330")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(ManagedTextRecord other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000331")]
	[Address(RVA = "0x10F45D0", Offset = "0x10F2FD0", VA = "0x1810F45D0", Slot = "0")]
	public override bool Equals(object? obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6000332")]
	[Address(RVA = "0x477700", Offset = "0x476100", VA = "0x180477700", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
