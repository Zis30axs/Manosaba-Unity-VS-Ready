using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003EA")]
public class BooleanParameter : CommandParameter<bool>
{
	[Token(Token = "0x60012D3")]
	[Address(RVA = "0x5A5810", Offset = "0x5A4210", VA = "0x1805A5810")]
	public static implicit operator BooleanParameter(bool value)
	{
		return null;
	}

	[Token(Token = "0x60012D4")]
	[Address(RVA = "0x5A58C0", Offset = "0x5A42C0", VA = "0x1805A58C0")]
	public static implicit operator bool?(BooleanParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012D5")]
	[Address(RVA = "0x5A5980", Offset = "0x5A4380", VA = "0x1805A5980")]
	public static implicit operator BooleanParameter(NullableBoolean value)
	{
		return null;
	}

	[Token(Token = "0x60012D6")]
	[Address(RVA = "0x5A5A50", Offset = "0x5A4450", VA = "0x1805A5A50")]
	public static implicit operator NullableBoolean(BooleanParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012D7")]
	[Address(RVA = "0x5A5C20", Offset = "0x5A4620", VA = "0x1805A5C20", Slot = "23")]
	protected override bool ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return default(bool);
	}

	[Token(Token = "0x60012D8")]
	[Address(RVA = "0x5A5CC0", Offset = "0x5A46C0", VA = "0x1805A5CC0")]
	public BooleanParameter()
	{
	}
}
