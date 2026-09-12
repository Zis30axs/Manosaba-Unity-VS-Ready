using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200020A")]
public class LiteralMap<TValue> : Dictionary<string, TValue>
{
	[Token(Token = "0x6000931")]
	public LiteralMap()
	{
	}

	[Token(Token = "0x6000932")]
	public LiteralMap(IDictionary<string, TValue> dictionary)
	{
	}
}
