using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003F4")]
public class IntegerParameter : CommandParameter<int>
{
	[Token(Token = "0x600130F")]
	[Address(RVA = "0x5A7D20", Offset = "0x5A6720", VA = "0x1805A7D20")]
	public static implicit operator IntegerParameter(int value)
	{
		return null;
	}

	[Token(Token = "0x6001310")]
	[Address(RVA = "0x5A7DC0", Offset = "0x5A67C0", VA = "0x1805A7DC0")]
	public static implicit operator int?(IntegerParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001311")]
	[Address(RVA = "0x5A7E80", Offset = "0x5A6880", VA = "0x1805A7E80")]
	public static implicit operator IntegerParameter(NullableInteger value)
	{
		return null;
	}

	[Token(Token = "0x6001312")]
	[Address(RVA = "0x5A7F50", Offset = "0x5A6950", VA = "0x1805A7F50")]
	public static implicit operator NullableInteger(IntegerParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001313")]
	[Address(RVA = "0x5A8120", Offset = "0x5A6B20", VA = "0x1805A8120", Slot = "23")]
	protected override int ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return default(int);
	}

	[Token(Token = "0x6001314")]
	[Address(RVA = "0x5A81C0", Offset = "0x5A6BC0", VA = "0x1805A81C0")]
	public IntegerParameter()
	{
	}
}
