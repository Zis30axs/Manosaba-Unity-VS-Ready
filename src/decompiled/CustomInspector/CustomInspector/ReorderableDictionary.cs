using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x200006D")]
public class ReorderableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
	[Serializable]
	[Token(Token = "0x200006E")]
	public class SerializableKeyValuePair
	{
		[Token(Token = "0x4000184")]
		[FieldOffset(Offset = "0x0")]
		public TKey key;

		[Token(Token = "0x4000185")]
		[FieldOffset(Offset = "0x0")]
		public TValue value;

		[Token(Token = "0x4000186")]
		[FieldOffset(Offset = "0x0")]
		public bool isValid;

		[Token(Token = "0x6000167")]
		public SerializableKeyValuePair(TKey key, TValue value, bool isValid)
		{
		}
	}

	[Token(Token = "0x4000182")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private bool _;

	[Token(Token = "0x4000183")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private List<SerializableKeyValuePair> keyValuePairs;

	[Token(Token = "0x17000023")]
	public new TValue this[TKey key]
	{
		[Token(Token = "0x6000165")]
		get
		{
			return default(TValue);
		}
		[Token(Token = "0x6000166")]
		set
		{
		}
	}

	[Token(Token = "0x6000153")]
	public ReorderableDictionary()
	{
	}

	[Token(Token = "0x6000154")]
	public ReorderableDictionary(ReorderableDictionary<TKey, TValue> dict)
	{
	}

	[Token(Token = "0x6000155")]
	public ReorderableDictionary(IDictionary<TKey, TValue> dictionary)
	{
	}

	[Token(Token = "0x6000156")]
	public ReorderableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
	{
	}

	[Token(Token = "0x6000157")]
	public ReorderableDictionary(IEqualityComparer<TKey> comparer)
	{
	}

	[Token(Token = "0x6000158")]
	public ReorderableDictionary(int capacity)
	{
	}

	[Token(Token = "0x6000159")]
	public ReorderableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
	{
	}

	[Token(Token = "0x600015A")]
	public ReorderableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
	{
	}

	[Token(Token = "0x600015B")]
	public ReorderableDictionary(int capacity, IEqualityComparer<TKey> comparer)
	{
	}

	[Token(Token = "0x600015C")]
	protected ReorderableDictionary(SerializationInfo info, StreamingContext context)
	{
	}

	[Token(Token = "0x600015D")]
	[Conditional("UNITY_EDITOR")]
	private void ApplyValuesToList()
	{
	}

	[Token(Token = "0x600015E")]
	private void UnityEngine_002EISerializationCallbackReceiver_002EOnBeforeSerialize()
	{
	}

	[Token(Token = "0x600015F")]
	private void UnityEngine_002EISerializationCallbackReceiver_002EOnAfterDeserialize()
	{
	}

	[Token(Token = "0x6000160")]
	public new void Clear()
	{
	}

	[Token(Token = "0x6000161")]
	public new bool TryAdd(TKey key, TValue value)
	{
		return default(bool);
	}

	[Token(Token = "0x6000162")]
	public new void Add(TKey key, TValue value)
	{
	}

	[Token(Token = "0x6000163")]
	public new bool Remove(TKey key)
	{
		return default(bool);
	}

	[Token(Token = "0x6000164")]
	public new bool Remove(TKey key, out TValue value)
	{
		value = default;
		value = default;
		return default(bool);
	}
}
