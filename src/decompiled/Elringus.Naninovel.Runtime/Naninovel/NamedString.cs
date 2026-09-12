using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200020F")]
public class NamedString : Named<NullableString>
{
	[Token(Token = "0x6000940")]
	[Address(RVA = "0x5258F0", Offset = "0x5242F0", VA = "0x1805258F0")]
	public NamedString()
	{
	}

	[Token(Token = "0x6000941")]
	[Address(RVA = "0x525920", Offset = "0x524320", VA = "0x180525920")]
	public NamedString(string name, string value)
	{
	}
}
