using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000382")]
public readonly struct LocaleChangedArgs : IEquatable<LocaleChangedArgs>
{
	[Token(Token = "0x4000D0F")]
	[FieldOffset(Offset = "0x0")]
	public readonly string CurrentLocale;

	[Token(Token = "0x4000D10")]
	[FieldOffset(Offset = "0x8")]
	public readonly string PreviousLocale;

	[Token(Token = "0x60010FE")]
	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
	public LocaleChangedArgs(string currentLocale, string previousLocale)
	{
	}

	[Token(Token = "0x60010FF")]
	[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
	public bool Equals(LocaleChangedArgs other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001100")]
	[Address(RVA = "0x5821A0", Offset = "0x580BA0", VA = "0x1805821A0", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001101")]
	[Address(RVA = "0x582240", Offset = "0x580C40", VA = "0x180582240", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
