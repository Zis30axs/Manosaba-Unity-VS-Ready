using System;
using System.Collections;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x200007D")]
public class SerializableSortedSet<T> : SerializableSet<T> where T : IComparable
{
	[Token(Token = "0x17000042")]
	public override T this[int index]
	{
		[Token(Token = "0x60001EC")]
		get
		{
			return default(T);
		}
		[Token(Token = "0x60001ED")]
		set
		{
		}
	}

	[Token(Token = "0x60001E1")]
	public override bool TryAdd(T item)
	{
		return default(bool);
	}

	[Token(Token = "0x60001E2")]
	public int AddAndGetIndex(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60001E3")]
	public override bool Remove(T item)
	{
		return default(bool);
	}

	[Token(Token = "0x60001E4")]
	public int RemoveAndGetIndex(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60001E5")]
	public override int GetIndexOf(T item)
	{
		return default(int);
	}

	[Token(Token = "0x60001E6")]
	public T[] GetItemsBetween(T t1, T t2)
	{
		return null;
	}

	[Token(Token = "0x60001E7")]
	public override bool Contains(T item)
	{
		return default(bool);
	}

	[Token(Token = "0x60001E8")]
	public IEnumerable<T> GetIEnumerable()
	{
		return null;
	}

	[Token(Token = "0x60001E9")]
	private IEnumerator<T> System_002ECollections_002EGeneric_002EIEnumerable_003CT_003E_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x60001EA")]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x60001EB")]
	private (int, bool) GetTheoreticalIndex(T item)
	{
		return default((int, bool));
	}

	[Token(Token = "0x60001EE")]
	protected override void Internal_OnDeserialization(object sender)
	{
	}

	[Token(Token = "0x60001EF")]
	private void System_002ECollections_002EICollection_002ECopyTo(Array array, int index)
	{
	}

	[Token(Token = "0x60001F0")]
	private void System_002ECollections_002EGeneric_002EICollection_003CT_003E_002ECopyTo(T[] array, int arrayIndex)
	{
	}

	[Token(Token = "0x60001F1")]
	public SerializableSortedSet()
	{
	}
}
