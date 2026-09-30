using System;
using System.Collections;
using System.Collections.Generic;

namespace LeTai.TrueShadow
{

internal class IndexedSet<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	private readonly List<T> list = new List<T>();

	private readonly Dictionary<T, int> dict = new Dictionary<T, int>();

	public int Count => list.Count;

	public bool IsReadOnly => false;

	public T this[int index]
	{
		get
		{
			return list[index];
		}
		set
		{
			T key = list[index];
			dict.Remove(key);
			list[index] = value;
			dict.Add(key, index);
		}
	}

	public void Add(T item)
	{
		dict.Add(item, list.Count);
		list.Add(item);
	}

	public bool AddUnique(T item)
	{
		if (dict.ContainsKey(item))
		{
			return false;
		}
		dict.Add(item, list.Count);
		list.Add(item);
		return true;
	}

	public bool Remove(T item)
	{
		if (!dict.TryGetValue(item, out var value))
		{
			return false;
		}
		RemoveAt(value);
		return true;
	}

	public void Remove(Predicate<T> match)
	{
		int num = 0;
		while (num < list.Count)
		{
			T val = list[num];
			if (match(val))
			{
				Remove(val);
			}
			else
			{
				num++;
			}
		}
	}

	public void Clear()
	{
		list.Clear();
		dict.Clear();
	}

	public bool Contains(T item)
	{
		return dict.ContainsKey(item);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		list.CopyTo(array, arrayIndex);
	}

	public int IndexOf(T item)
	{
		if (dict.TryGetValue(item, out var value))
		{
			return value;
		}
		return -1;
	}

	public void Insert(int index, T item)
	{
		throw new NotSupportedException("Random Insertion is semantically invalid, since this structure does not guarantee ordering.");
	}

	public void RemoveAt(int index)
	{
		T key = list[index];
		dict.Remove(key);
		if (index == list.Count - 1)
		{
			list.RemoveAt(index);
			return;
		}
		int index2 = list.Count - 1;
		T val = list[index2];
		list[index] = val;
		dict[val] = index;
		list.RemoveAt(index2);
	}

	public void Sort(Comparison<T> sortLayoutFunction)
	{
		list.Sort(sortLayoutFunction);
		for (int i = 0; i < list.Count; i++)
		{
			T key = list[i];
			dict[key] = i;
		}
	}

	public IEnumerator<T> GetEnumerator()
	{
		return list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
}
