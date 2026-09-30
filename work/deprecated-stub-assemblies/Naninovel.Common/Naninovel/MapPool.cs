using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200000C")]
    public sealed class MapPool<TKey, TValue> : CollectionPool<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
    {
    	[Token(Token = "0x6000020")]
    	public MapPool()
    	{
    	}
    }
}
