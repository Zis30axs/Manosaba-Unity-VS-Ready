using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200000B")]
public sealed class SetPool<T> : CollectionPool<HashSet<T>, T>
{
	[Token(Token = "0x600001F")]
	public SetPool()
	{
	}
}
