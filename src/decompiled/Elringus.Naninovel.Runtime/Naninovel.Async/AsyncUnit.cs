using System;
using Il2CppDummyDll;

namespace Naninovel.Async;

[Token(Token = "0x2000605")]
public struct AsyncUnit : IEquatable<AsyncUnit>
{
	[Token(Token = "0x4001796")]
	[FieldOffset(Offset = "0x0")]
	public static readonly AsyncUnit Default;

	[Token(Token = "0x6001E49")]
	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x6001E4A")]
	[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0", Slot = "4")]
	public bool Equals(AsyncUnit other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001E4B")]
	[Address(RVA = "0x4C7650", Offset = "0x4C6050", VA = "0x1804C7650", Slot = "3")]
	public override string ToString()
	{
		return null;
	}
}
