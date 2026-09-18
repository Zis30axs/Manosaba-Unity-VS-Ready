using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x2000073")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
    	[Token(Token = "0x2000074")]
    	protected class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
    	{
    		[Token(Token = "0x4000194")]
    		[FieldOffset(Offset = "0x0")]
    		private readonly DictionaryEntry[] items;

    		[Token(Token = "0x4000195")]
    		[FieldOffset(Offset = "0x0")]
    		private int index;

    		[Token(Token = "0x17000033")]
    		public object Current
    		{
    			[Token(Token = "0x60001A1")]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x17000034")]
    		public DictionaryEntry Entry
    		{
    			[Token(Token = "0x60001A2")]
    			get
    			{
    				return default(DictionaryEntry);
    			}
    		}

    		[Token(Token = "0x17000035")]
    		public object Key
    		{
    			[Token(Token = "0x60001A3")]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x17000036")]
    		public object Value
    		{
    			[Token(Token = "0x60001A4")]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x60001A0")]
    		public DictionaryEnumerator(SerializableDictionary<TKey, TValue> sd)
    		{
    		}

    		[Token(Token = "0x60001A5")]
    		public bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x60001A6")]
    		private void ValidateIndex()
    		{
    		}

    		[Token(Token = "0x60001A7")]
    		public void Reset()
    		{
    		}
    	}

    	[Token(Token = "0x2000077")]
    	[CompilerGenerated]
    	private sealed class _003CGetEnumerator_003Ed__26
    {
    		[Token(Token = "0x400019D")]
    		[FieldOffset(Offset = "0x0")]
    		private int _003C_003E1__state;

    		[Token(Token = "0x400019E")]
    		[FieldOffset(Offset = "0x0")]
    		private (TKey key, TValue value) _003C_003E2__current;

    		[Token(Token = "0x400019F")]
    		[FieldOffset(Offset = "0x0")]
    		public SerializableDictionary<TKey, TValue> _003C_003E4__this;

    		[Token(Token = "0x40001A0")]
    		[FieldOffset(Offset = "0x0")]
    		private int _003Ci_003E5__2;

    		[Token(Token = "0x17000037")]
    		private (TKey, TValue) System_002ECollections_002EGeneric_002EIEnumerator_003C_0028TKeykey_002CTValuevalue_0029_003E_002ECurrent
    		{
    			[Token(Token = "0x60001B4")]
    			[DebuggerHidden]
    			get
    			{
    				return default((TKey, TValue));
    			}
    		}

    		[Token(Token = "0x17000038")]
    		private object System_002ECollections_002EIEnumerator_002ECurrent
    		{
    			[Token(Token = "0x60001B6")]
    			[DebuggerHidden]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x60001B1")]
    		[DebuggerHidden]
    		public _003CGetEnumerator_003Ed__26(int _003C_003E1__state)
    		{
    		}

    		[Token(Token = "0x60001B2")]
    		[DebuggerHidden]
    		private void System_002EIDisposable_002EDispose()
    		{
    		}

    		[Token(Token = "0x60001B3")]
    		private bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x60001B5")]
    		[DebuggerHidden]
    		private void System_002ECollections_002EIEnumerator_002EReset()
    		{
    		}
    	}

    	[Token(Token = "0x400018F")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool info;

    	[Token(Token = "0x4000190")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	protected SerializableSet<TKey> keys;

    	[Token(Token = "0x4000191")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	protected ListContainer<TValue> values;

    	[Token(Token = "0x4000192")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TKey editor_keyInput;

    	[Token(Token = "0x4000193")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TValue editor_valueInput;

    	[Token(Token = "0x17000024")]
    	public ReadOnlyCollection<TKey> Keys
    	{
    		[Token(Token = "0x6000170")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000025")]
    	public ReadOnlyCollection<TValue> Values
    	{
    		[Token(Token = "0x6000171")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000026")]
    	public int Count
    	{
    		[Token(Token = "0x6000172")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000027")]
    	public TValue this[TKey key]
    	{
    		[Token(Token = "0x600017E")]
    		get
    		{
    			return default(TValue);
    		}
    		[Token(Token = "0x600017F")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000028")]
    	public bool IsReadOnly
    	{
    		[Token(Token = "0x6000189")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000029")]
    	public bool IsFixedSize
    	{
    		[Token(Token = "0x600018A")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700002A")]
    	public bool IsSynchronized
    	{
    		[Token(Token = "0x600018B")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700002B")]
    	private object System_002ECollections_002EICollection_002ESyncRoot
    	{
    		[Token(Token = "0x600018C")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700002C")]
    	private ICollection System_002ECollections_002EIDictionary_002EKeys
    	{
    		[Token(Token = "0x600018D")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700002D")]
    	private ICollection System_002ECollections_002EIDictionary_002EValues
    	{
    		[Token(Token = "0x600018E")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700002E")]
    	private ICollection<TKey> System_002ECollections_002EGeneric_002EIDictionary_003CTKey_002CTValue_003E_002EKeys
    	{
    		[Token(Token = "0x600018F")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700002F")]
    	private ICollection<TValue> System_002ECollections_002EGeneric_002EIDictionary_003CTKey_002CTValue_003E_002EValues
    	{
    		[Token(Token = "0x6000190")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000030")]
    	private IEnumerable<TKey> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CTKey_002CTValue_003E_002EKeys
    	{
    		[Token(Token = "0x6000191")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000031")]
    	private IEnumerable<TValue> System_002ECollections_002EGeneric_002EIReadOnlyDictionary_003CTKey_002CTValue_003E_002EValues
    	{
    		[Token(Token = "0x6000192")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000032")]
    	private object System_002ECollections_002EIDictionary_002EItem
    	{
    		[Token(Token = "0x6000193")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000194")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000173")]
    	public SerializableDictionary()
    	{
    	}

    	[Token(Token = "0x6000174")]
    	public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
    	{
    	}

    	[Token(Token = "0x6000175")]
    	public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
    	{
    	}

    	[Token(Token = "0x6000176")]
    	protected SerializableDictionary(SerializableSet<TKey> keys, List<TValue> values)
    	{
    	}

    	[Token(Token = "0x6000177")]
    	public void Add(TKey key, TValue value)
    	{
    	}

    	[Token(Token = "0x6000178")]
    	public virtual bool TryAdd(TKey key, TValue value)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000179")]
    	public bool Remove(TKey key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600017A")]
    	public bool TryRemove(TKey key, out TValue value)
    	{
    		value = default;
    		value = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600017B")]
    	public void RemoveAt(int index)
    	{
    	}

    	[Token(Token = "0x600017C")]
    	public bool TryGetValue(TKey key, out TValue value)
    	{
    		value = default;
    		value = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600017D")]
    	public TValue GetValue(TKey key)
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x6000180")]
    	public bool ContainsKey(TKey key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000181")]
    	public bool Contains(TKey key, TValue value)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000182")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x6000183")]
    	[IteratorStateMachine(typeof(SerializableDictionary<, >._003CGetEnumerator_003Ed__26))]
    	public IEnumerator<(TKey, TValue)> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000184")]
    	public static explicit operator (List<TKey>, List<TValue>)(SerializableDictionary<TKey, TValue> d)
    	{
    		return default((List<TKey>, List<TValue>));
    	}

    	[Token(Token = "0x6000185")]
    	public static explicit operator List<(TKey, TValue)>(SerializableDictionary<TKey, TValue> d)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000187")]

    	private IEnumerator<KeyValuePair<TKey, TValue>> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CTKey_002CTValue_003E_003E_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000188")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000195")]
    	private void System_002ECollections_002EGeneric_002EICollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CTKey_002CTValue_003E_003E_002EAdd(KeyValuePair<TKey, TValue> item)
    	{
    	}

    	[Token(Token = "0x6000196")]
    	private bool System_002ECollections_002EGeneric_002EICollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CTKey_002CTValue_003E_003E_002ERemove(KeyValuePair<TKey, TValue> item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000197")]
    	private bool System_002ECollections_002EGeneric_002EICollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CTKey_002CTValue_003E_003E_002EContains(KeyValuePair<TKey, TValue> item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000198")]
    	private void System_002ECollections_002EIDictionary_002EAdd(object key, object value)
    	{
    	}

    	[Token(Token = "0x6000199")]
    	private void System_002ECollections_002EIDictionary_002ERemove(object key)
    	{
    	}

    	[Token(Token = "0x600019A")]
    	private bool System_002ECollections_002EIDictionary_002EContains(object key)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600019B")]
    	private IDictionaryEnumerator System_002ECollections_002EIDictionary_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x600019C")]
    	private void System_002ECollections_002EICollection_002ECopyTo(Array array, int index)
    	{
    	}

    	[Token(Token = "0x600019D")]
    	private void System_002ECollections_002EGeneric_002EICollection_003CSystem_002ECollections_002EGeneric_002EKeyValuePair_003CTKey_002CTValue_003E_003E_002ECopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    	{
    	}

    	[Token(Token = "0x600019E")]
    	private void System_002ERuntime_002ESerialization_002EIDeserializationCallback_002EOnDeserialization(object sender)
    	{
    	}

    	[Token(Token = "0x600019F")]
    	protected virtual void Internal_OnDeserialization(object sender)
    	{
    	}
    }
}
