using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003E8")]
public class BooleanListParameter : ParameterList<NullableBoolean>
{
	[Token(Token = "0x60012C9")]
	[Address(RVA = "0x5A4DB0", Offset = "0x5A37B0", VA = "0x1805A4DB0")]
	public static implicit operator BooleanListParameter(List<bool?> value)
	{
		return null;
	}

	[Token(Token = "0x60012CA")]
	[Address(RVA = "0x5A5030", Offset = "0x5A3A30", VA = "0x1805A5030")]
	public static implicit operator List<bool?>(BooleanListParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012CB")]
	[Address(RVA = "0x5A5230", Offset = "0x5A3C30", VA = "0x1805A5230")]
	public static implicit operator bool?[](BooleanListParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012CC")]
	[Address(RVA = "0x5A5420", Offset = "0x5A3E20", VA = "0x1805A5420", Slot = "26")]
	protected override NullableBoolean ParseItemValueText(string valueText, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x60012CD")]
	[Address(RVA = "0x5A5450", Offset = "0x5A3E50", VA = "0x1805A5450")]
	public BooleanListParameter()
	{
	}
}
