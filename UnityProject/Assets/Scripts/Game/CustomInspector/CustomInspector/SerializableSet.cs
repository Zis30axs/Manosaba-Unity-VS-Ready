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
    [Token(Token = "0x200007A")]
    public class SerializableSet<T> : HashSet<T>
    {
    	[Token(Token = "0x200007B")]
    	[CompilerGenerated]
    	private sealed class _003CGetEnumerator_003Ed__21
    {
    		[Token(Token = "0x40001A8")]
    		[FieldOffset(Offset = "0x0")]
    		private int _003C_003E1__state;

    		[Token(Token = "0x40001A9")]
    		[FieldOffset(Offset = "0x0")]
    		private T _003C_003E2__current;

    		[Token(Token = "0x40001AA")]
    		[FieldOffset(Offset = "0x0")]
    		public SerializableSet<T> _003C_003E4__this;

    		[Token(Token = "0x40001AB")]
    		[FieldOffset(Offset = "0x0")]
    		private List<T>.Enumerator _003C_003E7__wrap1;

    		[Token(Token = "0x17000040")]
    		private T System_002ECollections_002EGeneric_002EIEnumerator_003CT_003E_002ECurrent
    		{
    			[Token(Token = "0x60001DA")]
    			[DebuggerHidden]
    			get
    			{
    				return default(T);
    			}
    		}

    		[Token(Token = "0x17000041")]
    		private object System_002ECollections_002EIEnumerator_002ECurrent
    		{
    			[Token(Token = "0x60001DC")]
    			[DebuggerHidden]
    			get
    			{
    				return null;
    			}
    		}

    		[Token(Token = "0x60001D6")]
    		[DebuggerHidden]
    		public _003CGetEnumerator_003Ed__21(int _003C_003E1__state)
    		{
    		}

    		[Token(Token = "0x60001D7")]
    		[DebuggerHidden]
    		private void System_002EIDisposable_002EDispose()
    		{
    		}

    		[Token(Token = "0x60001D8")]
    		private bool MoveNext()
    		{
    			return default(bool);
    		}

    		[Token(Token = "0x60001D9")]
    		private void _003C_003Em__Finally1()
    		{
    		}

    		[Token(Token = "0x60001DB")]
    		[DebuggerHidden]
    		private void System_002ECollections_002EIEnumerator_002EReset()
    		{
    		}
    	}

    	[Token(Token = "0x40001A5")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool info;

    	[Token(Token = "0x40001A6")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	protected ListContainer<T> values;

    	[Token(Token = "0x40001A7")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private T editor_input;

    	[Token(Token = "0x1700003B")]
    	public int Count
    	{
    		[Token(Token = "0x60001BE")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700003C")]
    	public virtual T this[int index]
    	{
    		[Token(Token = "0x60001C7")]
    		get
    		{
    			return default(T);
    		}
    		[Token(Token = "0x60001C8")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700003D")]
    	private bool System_002ECollections_002EICollection_002EIsSynchronized
    	{
    		[Token(Token = "0x60001D3")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700003E")]
    	private object System_002ECollections_002EICollection_002ESyncRoot
    	{
    		[Token(Token = "0x60001D4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700003F")]
    	private bool System_002ECollections_002EGeneric_002EICollection_003CT_003E_002EIsReadOnly
    	{
    		[Token(Token = "0x60001D5")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60001BF")]
    	public SerializableSet()
    	{
    	}

    	[Token(Token = "0x60001C0")]
    	public SerializableSet(IEnumerable<T> collection)
    	{
    	}

    	[Token(Token = "0x60001C1")]
    	public void Add(T item)
    	{
    	}

    	[Token(Token = "0x60001C2")]
    	public virtual bool TryAdd(T item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001C3")]
    	public virtual bool Remove(T item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001C4")]
    	public void RemoveAt(int index)
    	{
    	}

    	[Token(Token = "0x60001C5")]
    	public virtual int GetIndexOf(T item)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60001C6")]
    	public T GetByIndex(int index)
    	{
    		return default(T);
    	}

    	[Token(Token = "0x60001C9")]
    	private void System_002ERuntime_002ESerialization_002EIDeserializationCallback_002EOnDeserialization(object sender)
    	{
    	}

    	[Token(Token = "0x60001CA")]
    	protected virtual void Internal_OnDeserialization(object sender)
    	{
    	}

    	[Token(Token = "0x60001CB")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x60001CC")]
    	public virtual bool Contains(T item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001CD")]
    	public ReadOnlyCollection<T> AsReadOnly()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001CE")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001CF")]
    	[IteratorStateMachine(typeof(SerializableSet<>._003CGetEnumerator_003Ed__21))]
    	public IEnumerator<T> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60001D0")]
    	public static explicit operator List<T>(SerializableSet<T> s)
    	{
    		return null;
    	}

    	[Token(Token = "0x60001D1")]
    	private void System_002ECollections_002EICollection_002ECopyTo(Array array, int index)
    	{
    	}

    	[Token(Token = "0x60001D2")]
    	private void System_002ECollections_002EGeneric_002EICollection_003CT_003E_002ECopyTo(T[] array, int arrayIndex)
    	{
    	}
    }
}
