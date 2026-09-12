using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000223")]
public class NullableNamedString : NullableNamed<NamedString, NullableString>
{
	[Token(Token = "0x600097B")]
	[Address(RVA = "0x5275F0", Offset = "0x525FF0", VA = "0x1805275F0")]
	public static implicit operator NullableNamedString(NamedString value)
	{
		return null;
	}

	[Token(Token = "0x600097C")]
	[Address(RVA = "0x527690", Offset = "0x526090", VA = "0x180527690")]
	public static implicit operator NamedString(NullableNamedString nullable)
	{
		return null;
	}

	[Token(Token = "0x600097D")]
	[Address(RVA = "0x527720", Offset = "0x526120", VA = "0x180527720")]
	public NullableNamedString()
	{
	}
}
