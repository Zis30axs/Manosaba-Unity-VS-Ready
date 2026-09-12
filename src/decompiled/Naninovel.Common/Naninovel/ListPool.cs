using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200000A")]
public sealed class ListPool<T> : CollectionPool<List<T>, T>
{
	[Token(Token = "0x600001E")]
	public ListPool()
	{
	}
}
