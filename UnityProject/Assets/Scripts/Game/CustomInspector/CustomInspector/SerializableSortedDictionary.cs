using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x200007C")]
    public class SerializableSortedDictionary<TKey, TValue> : SerializableDictionary<TKey, TValue> where TKey : IComparable
    {
    	[Token(Token = "0x40001AC")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly SerializableSortedSet<TKey> keys_casted;

    	[Token(Token = "0x60001DD")]
    	public SerializableSortedDictionary()
    	{
    	}

    	[Token(Token = "0x60001DE")]
    	public SerializableSortedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    	{
    	}

    	[Token(Token = "0x60001DF")]
    	public override bool TryAdd(TKey key, TValue value)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001E0")]
    	protected override void Internal_OnDeserialization(object sender)
    	{
    	}
    }
}
