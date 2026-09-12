using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000224")]
public class NullableNamedInteger : NullableNamed<NamedInteger, NullableInteger>
{
	[Token(Token = "0x600097E")]
	[Address(RVA = "0x527750", Offset = "0x526150", VA = "0x180527750")]
	public static implicit operator NullableNamedInteger(NamedInteger value)
	{
		return null;
	}

	[Token(Token = "0x600097F")]
	[Address(RVA = "0x5277F0", Offset = "0x5261F0", VA = "0x1805277F0")]
	public static implicit operator NamedInteger(NullableNamedInteger nullable)
	{
		return null;
	}

	[Token(Token = "0x6000980")]
	[Address(RVA = "0x527880", Offset = "0x526280", VA = "0x180527880")]
	public NullableNamedInteger()
	{
	}
}
