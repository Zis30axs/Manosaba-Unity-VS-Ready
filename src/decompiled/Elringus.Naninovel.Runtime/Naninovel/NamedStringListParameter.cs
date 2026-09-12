using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003FD")]
public class NamedStringListParameter : ParameterList<NullableNamedString>
{
	[Token(Token = "0x6001337")]
	[Address(RVA = "0x5A9D90", Offset = "0x5A8790", VA = "0x1805A9D90")]
	public static implicit operator NamedStringListParameter(List<NullableNamedString> value)
	{
		return null;
	}

	[Token(Token = "0x6001338")]
	[Address(RVA = "0x5A9E30", Offset = "0x5A8830", VA = "0x1805A9E30")]
	public static implicit operator List<NullableNamedString>(NamedStringListParameter param)
	{
		return null;
	}

	[Token(Token = "0x6001339")]
	[Address(RVA = "0x5A9EC0", Offset = "0x5A88C0", VA = "0x1805A9EC0", Slot = "26")]
	protected override NullableNamedString ParseItemValueText(string valueText, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x600133A")]
	[Address(RVA = "0x5A9FB0", Offset = "0x5A89B0", VA = "0x1805A9FB0")]
	public NamedStringListParameter()
	{
	}
}
