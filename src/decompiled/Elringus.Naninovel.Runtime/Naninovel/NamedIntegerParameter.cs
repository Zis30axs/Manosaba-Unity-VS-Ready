using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003FB")]
public class NamedIntegerParameter : NamedParameter<NamedInteger, NullableInteger>
{
	[Token(Token = "0x600132F")]
	[Address(RVA = "0x5A9AE0", Offset = "0x5A84E0", VA = "0x1805A9AE0")]
	public static implicit operator NamedIntegerParameter(NamedInteger value)
	{
		return null;
	}

	[Token(Token = "0x6001330")]
	[Address(RVA = "0x5A9B80", Offset = "0x5A8580", VA = "0x1805A9B80")]
	public static implicit operator NamedInteger(NamedIntegerParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001331")]
	[Address(RVA = "0x5A9C10", Offset = "0x5A8610", VA = "0x1805A9C10", Slot = "23")]
	protected override NamedInteger ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x6001332")]
	[Address(RVA = "0x5A9D60", Offset = "0x5A8760", VA = "0x1805A9D60")]
	public NamedIntegerParameter()
	{
	}
}
