using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200022B")]
    public abstract class SerializableMap
    {
    	[Token(Token = "0x200022C")]
    	protected class Dictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>
    	{
    		[Token(Token = "0x6000994")]
    		public Dictionary()
    		{
    		}

    		[Token(Token = "0x6000995")]
    		public Dictionary(IEqualityComparer<TKey> comparer)
    		{
    		}

    		[Token(Token = "0x6000996")]
    		public Dictionary(IDictionary<TKey, TValue> dict)
    		{
    		}

    		[Token(Token = "0x6000997")]
    		public Dictionary(IDictionary<TKey, TValue> dict, IEqualityComparer<TKey> comparer)
    		{
    		}

    		[Token(Token = "0x6000998")]
    		public Dictionary(SerializationInfo info, StreamingContext context)
    		{
    		}
    	}

    	[Token(Token = "0x6000993")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	protected SerializableMap()
    	{
    	}
    }
    [Serializable]
    [Token(Token = "0x200022D")]
    public abstract class SerializableMap<TKey, TValue> : SerializableMap, ISerializationCallbackReceiver, IDeserializationCallback, ISerializable
    {
    	[Token(Token = "0x40008AE")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TKey[] keys;

    	[Token(Token = "0x40008AF")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TValue[] values;

    	[Token(Token = "0x40008B0")]
    	[FieldOffset(Offset = "0x0")]
    	private Dictionary<TKey, TValue> dictionary;

    	[Token(Token = "0x1700015A")]
    	public System.Collections.Generic.Dictionary<TKey, TValue>.KeyCollection KeyCollection
    	{
    		[Token(Token = "0x6000999")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700015B")]
    	public System.Collections.Generic.Dictionary<TKey, TValue>.ValueCollection ValueCollection
    	{
    		[Token(Token = "0x600099A")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700015C")]
    	public ICollection<TKey> Keys
    	{
    		[Token(Token = "0x60009A1")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700015D")]
    	private IEnumerable<TValue> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CTKey_002CTValue_003E_002EValues
    	{
    		[Token(Token = "0x60009A2")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700015E")]
    	private IEnumerable<TKey> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CTKey_002CTValue_003E_002EKeys
    	{
    		[Token(Token = "0x60009A3")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700015F")]
    	public ICollection<TValue> Values
    	{
    		[Token(Token = "0x60009A4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000160")]
    	public int Count
    	{
    		[Token(Token = "0x60009A5")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000161")]
    	public bool IsReadOnly
    	{
    		[Token(Token = "0x60009A6")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000162")]
    	public TValue this[TKey key]
    	{
    		[Token(Token = "0x60009A7")]
    		get
    		{
    			return default(TValue);
    		}
    		[Token(Token = "0x60009A8")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000163")]
    	public bool IsFixedSize
    	{
    		[Token(Token = "0x60009B4")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000164")]
    	private ICollection System_002ECollections_002EIDictionary_002EKeys
    	{
    		[Token(Token = "0x60009B5")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000165")]
    	private ICollection System_002ECollections_002EIDictionary_002EValues
    	{
    		[Token(Token = "0x60009B6")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000166")]
    	public bool IsSynchronized
    	{
    		[Token(Token = "0x60009B7")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000167")]
    	public object SyncRoot
    	{
    		[Token(Token = "0x60009B8")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000168")]
    	public object this[object key]
    	{
    		[Token(Token = "0x60009B9")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60009BA")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600099B")]
    	protected SerializableMap()
    	{
    	}

    	[Token(Token = "0x600099C")]
    	protected SerializableMap(IEqualityComparer<TKey> comparer)
    	{
    	}

    	[Token(Token = "0x600099D")]
    	protected SerializableMap(IDictionary<TKey, TValue> dict)
    	{
    	}

    	[Token(Token = "0x600099E")]
    	protected SerializableMap(IDictionary<TKey, TValue> dict, IEqualityComparer<TKey> comparer)
    	{
    	}

    	[Token(Token = "0x600099F")]
    	public void OnAfterDeserialize()
    	{
    	}

    	[Token(Token = "0x60009A0")]
    	public void OnBeforeSerialize()
    	{
    	}

    	[Token(Token = "0x60009A9")]
    	public void Add(TKey key, TValue value)
    	{
    	}

    	[Token(Token = "0x60009AA")]
    	public bool ContainsKey(TKey key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009AB")]
    	public bool Remove(TKey key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009AC")]
    	public bool TryGetValue(TKey key, out TValue value)
    	{
    		value = default;
    		value = default;
    		return default(bool);
    	}

    	[Token(Token = "0x60009AD")]
    	public void Add(KeyValuePair<TKey, TValue> item)
    	{
    	}

    	[Token(Token = "0x60009AE")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x60009AF")]
    	public bool Contains(KeyValuePair<TKey, TValue> item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009B0")]
    	public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    	{
    	}

    	[Token(Token = "0x60009B1")]
    	public bool Remove(KeyValuePair<TKey, TValue> item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009B2")]
    	public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60009B3")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60009BB")]
    	public void Add(object key, object value)
    	{
    	}

    	[Token(Token = "0x60009BC")]
    	public bool Contains(object key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60009BD")]
    	private IDictionaryEnumerator System_002ECollections_002EIDictionary_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60009BE")]
    	public void Remove(object key)
    	{
    	}

    	[Token(Token = "0x60009BF")]
    	public void CopyTo(Array array, int index)
    	{
    	}

    	[Token(Token = "0x60009C0")]
    	public void OnDeserialization(object sender)
    	{
    	}

    	[Token(Token = "0x60009C1")]
    	protected SerializableMap(SerializationInfo info, StreamingContext context)
    	{
    	}

    	[Token(Token = "0x60009C2")]
    	public void GetObjectData(SerializationInfo info, StreamingContext context)
    	{
    	}

    	[Token(Token = "0x60009C3")]
    	protected virtual TValue GetValue(TValue[] storage, int i)
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x60009C4")]
    	protected virtual void SetValue(TValue[] storage, int i, TValue value)
    	{
    	}
    }
}
