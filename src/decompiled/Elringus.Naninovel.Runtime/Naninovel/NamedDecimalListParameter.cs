using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003F8")]
public class NamedDecimalListParameter : ParameterList<NullableNamedFloat>
{
	[Token(Token = "0x6001323")]
	[Address(RVA = "0x5A9340", Offset = "0x5A7D40", VA = "0x1805A9340")]
	public static implicit operator NamedDecimalListParameter(List<NullableNamedFloat> value)
	{
		return null;
	}

	[Token(Token = "0x6001324")]
	[Address(RVA = "0x5A93E0", Offset = "0x5A7DE0", VA = "0x1805A93E0")]
	public static implicit operator List<NullableNamedFloat>(NamedDecimalListParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001325")]
	[Address(RVA = "0x5A9470", Offset = "0x5A7E70", VA = "0x1805A9470", Slot = "26")]
	protected override NullableNamedFloat ParseItemValueText(string valueText, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x6001326")]
	[Address(RVA = "0x5A9580", Offset = "0x5A7F80", VA = "0x1805A9580")]
	public NamedDecimalListParameter()
	{
	}
}
