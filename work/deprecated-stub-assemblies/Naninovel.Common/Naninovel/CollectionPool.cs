using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000008")]
    public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new() where TItem : notnull
    {
    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x0")]
    	internal static readonly ObjectPool<TCollection> pool;

    	[Token(Token = "0x6000014")]
    	public static TCollection Rent()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000015")]
    	public static PooledObject<TCollection> Rent(out TCollection obj)
    	{
    		obj = default;
    		obj = default;
    		return default(PooledObject<TCollection>);
    	}

    	[Token(Token = "0x6000016")]
    	public static void Return(TCollection obj)
    	{
    	}

    	[Token(Token = "0x6000017")]
    	public static void Drop()
    	{
    	}

    	[Token(Token = "0x6000018")]
    	public CollectionPool()
    	{
    	}
    }
}
