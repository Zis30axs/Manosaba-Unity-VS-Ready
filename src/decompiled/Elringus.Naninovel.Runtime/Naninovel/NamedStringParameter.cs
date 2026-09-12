using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003FE")]
public class NamedStringParameter : NamedParameter<NamedString, NullableString>
{
	[Token(Token = "0x600133B")]
	[Address(RVA = "0x5A9FE0", Offset = "0x5A89E0", VA = "0x1805A9FE0")]
	public static implicit operator NamedStringParameter(NamedString value)
	{
		return null;
	}

	[Token(Token = "0x600133C")]
	[Address(RVA = "0x5AA080", Offset = "0x5A8A80", VA = "0x1805AA080")]
	public static implicit operator NamedString(NamedStringParameter param)
	{
		return null;
	}

	[Token(Token = "0x600133D")]
	[Address(RVA = "0x5AA110", Offset = "0x5A8B10", VA = "0x1805AA110", Slot = "23")]
	protected override NamedString ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return null;
	}

	[Token(Token = "0x600133E")]
	[Address(RVA = "0x5AA240", Offset = "0x5A8C40", VA = "0x1805AA240")]
	public NamedStringParameter()
	{
	}
}
