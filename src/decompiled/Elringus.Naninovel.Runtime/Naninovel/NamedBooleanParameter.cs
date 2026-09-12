using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003F7")]
public class NamedBooleanParameter : NamedParameter<NamedBoolean, NullableBoolean>
{
	[Token(Token = "0x600131F")]
	[Address(RVA = "0x5A9080", Offset = "0x5A7A80", VA = "0x1805A9080")]
	public static implicit operator NamedBooleanParameter(NamedBoolean value)
	{
		return null;
	}

	[Token(Token = "0x6001320")]
	[Address(RVA = "0x5A9120", Offset = "0x5A7B20", VA = "0x1805A9120")]
	public static implicit operator NamedBoolean(NamedBooleanParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001321")]
	[Address(RVA = "0x5A91B0", Offset = "0x5A7BB0", VA = "0x1805A91B0", Slot = "23")]
	protected override NamedBoolean ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x6001322")]
	[Address(RVA = "0x5A9310", Offset = "0x5A7D10", VA = "0x1805A9310")]
	public NamedBooleanParameter()
	{
	}
}
