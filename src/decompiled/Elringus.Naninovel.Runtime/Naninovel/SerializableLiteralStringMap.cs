using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200020B")]
public class SerializableLiteralStringMap : SerializableMap<string, string>
{
	[Token(Token = "0x6000933")]
	[Address(RVA = "0x525780", Offset = "0x524180", VA = "0x180525780")]
	public SerializableLiteralStringMap()
	{
	}

	[Token(Token = "0x6000934")]
	[Address(RVA = "0x525830", Offset = "0x524230", VA = "0x180525830")]
	public SerializableLiteralStringMap(IDictionary<string, string> dictionary)
	{
	}
}
