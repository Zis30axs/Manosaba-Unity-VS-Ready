using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x200005B")]
public class ListContainer<T> : List<T>
{
	[Token(Token = "0x400015A")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private bool _;

	[Token(Token = "0x400015B")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private List<T> values;

	[Token(Token = "0x17000010")]
	public T this[int index]
	{
		[Token(Token = "0x60000CD")]
		get
		{
			return default(T);
		}
		[Token(Token = "0x60000CE")]
		set
		{
		}
	}

	[Token(Token = "0x17000011")]
	public int Count
	{
		[Token(Token = "0x60000CF")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000012")]
	public int Capacity
	{
		[Token(Token = "0x60000D0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000013")]
	private bool System_002ECollections_002EGeneric_002EICollection_003CT_003E_002EIsReadOnly
	{
		[Token(Token = "0x60000FF")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000014")]
	public bool IsSynchronized
	{
		[Token(Token = "0x6000100")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000015")]
	public object SyncRoot
	{
		[Token(Token = "0x6000101")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000016")]
	public bool IsFixedSize
	{
		[Token(Token = "0x6000102")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000017")]
	public bool IsReadOnly
	{
		[Token(Token = "0x6000103")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000018")]
	private object System_002ECollections_002EIList_002EItem
	{
		[Token(Token = "0x6000104")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000105")]
		set
		{
		}
	}

	[Token(Token = "0x60000C7")]
	public ListContainer()
	{
	}

	[Token(Token = "0x60000C8")]
	public ListContainer(int capacity)
	{
	}

	[Token(Token = "0x60000C9")]
	public ListContainer(List<T> collection)
	{
	}

	[Token(Token = "0x60000CA")]
	public ListContainer(IEnumerable<T> collection)
	{
	}

	[Token(Token = "0x60000D1")]
	public void Add(T item)
	{
	}

	[Token(Token = "0x60000D2")]
	public void AddRange(IEnumerable<T> collection)
	{
	}

	[Token(Token = "0x60000D3")]
	public ReadOnlyCollection<T> AsReadOnly()
	{
		return null;
	}

	[Token(Token = "0x60000D4")]
	public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
	{
		return default(int);
	}

	[Token(Token = "0x60000D5")]
	public int BinarySearch(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60000D6")]
	public int BinarySearch(T item, IComparer<T> comparer)
	{
		return default(int);
	}

	[Token(Token = "0x60000D7")]
	public void Clear()
	{
	}

	[Token(Token = "0x60000D8")]
	public bool Contains(T item)
	{
		return default(bool);
	}

	[Token(Token = "0x60000D9")]
	public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
	{
		return null;
	}

	[Token(Token = "0x60000DA")]
	public void CopyTo(T[] array, int arrayIndex)
	{
	}

	[Token(Token = "0x60000DB")]
	public void CopyTo(T[] array)
	{
	}

	[Token(Token = "0x60000DC")]
	public void CopyTo(int index, T[] array, int arrayIndex, int count)
	{
	}

	[Token(Token = "0x60000DD")]
	public bool Exists(Predicate<T> match)
	{
		return default(bool);
	}

	[Token(Token = "0x60000DE")]
	public T Find(Predicate<T> match)
	{
		return default(T);
	}

	[Token(Token = "0x60000DF")]
	public List<T> FindAll(Predicate<T> match)
	{
		return null;
	}

	[Token(Token = "0x60000E0")]
	public int FindIndex(int startIndex, int count, Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E1")]
	public int FindIndex(int startIndex, Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E2")]
	public int FindIndex(Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E3")]
	public T FindLast(Predicate<T> match)
	{
		return default(T);
	}

	[Token(Token = "0x60000E4")]
	public int FindLastIndex(int startIndex, int count, Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E5")]
	public int FindLastIndex(int startIndex, Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E6")]
	public int FindLastIndex(Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000E7")]
	public void ForEach(Action<T> action)
	{
	}

	[Token(Token = "0x60000E8")]
	public List<T>.Enumerator GetEnumerator()
	{
		return default(List<T>.Enumerator);
	}

	[Token(Token = "0x60000E9")]
	public List<T> GetRange(int index, int count)
	{
		return null;
	}

	[Token(Token = "0x60000EA")]
	public int IndexOf(T item, int index, int count)
	{
		return default(int);
	}

	[Token(Token = "0x60000EB")]
	public int IndexOf(T item, int index)
	{
		return default(int);
	}

	[Token(Token = "0x60000EC")]
	public int IndexOf(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60000ED")]
	public void Insert(int index, T item)
	{
	}

	[Token(Token = "0x60000EE")]
	public void InsertRange(int index, IEnumerable<T> collection)
	{
	}

	[Token(Token = "0x60000EF")]
	public int LastIndexOf(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60000F0")]
	public int LastIndexOf(T item, int index)
	{
		return default(int);
	}

	[Token(Token = "0x60000F1")]
	public int LastIndexOf(T item, int index, int count)
	{
		return default(int);
	}

	[Token(Token = "0x60000F2")]
	public bool Remove(T item)
	{
		return default(bool);
	}

	[Token(Token = "0x60000F3")]
	public int RemoveAll(Predicate<T> match)
	{
		return default(int);
	}

	[Token(Token = "0x60000F4")]
	public void RemoveAt(int index)
	{
	}

	[Token(Token = "0x60000F5")]
	public void RemoveRange(int index, int count)
	{
	}

	[Token(Token = "0x60000F6")]
	public void Reverse(int index, int count)
	{
	}

	[Token(Token = "0x60000F7")]
	public void Reverse()
	{
	}

	[Token(Token = "0x60000F8")]
	public void Sort(Comparison<T> comparison)
	{
	}

	[Token(Token = "0x60000F9")]
	public void Sort(int index, int count, IComparer<T> comparer)
	{
	}

	[Token(Token = "0x60000FA")]
	public void Sort()
	{
	}

	[Token(Token = "0x60000FB")]
	public void Sort(IComparer<T> comparer)
	{
	}

	[Token(Token = "0x60000FC")]
	public T[] ToArray()
	{
		return null;
	}

	[Token(Token = "0x60000FD")]
	public void TrimExcess()
	{
	}

	[Token(Token = "0x60000FE")]
	public bool TrueForAll(Predicate<T> match)
	{
		return default(bool);
	}

	[Token(Token = "0x6000106")]
	private IEnumerator<T> System_002ECollections_002EGeneric_002EIEnumerable_003CT_003E_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x6000107")]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x6000108")]
	public void CopyTo(Array array, int index)
	{
	}

	[Token(Token = "0x6000109")]
	public int Add(object value)
	{
		return default(int);
	}

	[Token(Token = "0x600010A")]
	public bool Contains(object value)
	{
		return default(bool);
	}

	[Token(Token = "0x600010B")]
	public int IndexOf(object value)
	{
		return default(int);
	}

	[Token(Token = "0x600010C")]
	public void Insert(int index, object value)
	{
	}

	[Token(Token = "0x600010D")]
	public void Remove(object value)
	{
	}
}
