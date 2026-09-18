using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x200004D")]
    public class Array2D<T>
    {
    	[Serializable]
    	[Token(Token = "0x200004E")]
    	private class Row
    	{
    		[Token(Token = "0x4000144")]
    		[FieldOffset(Offset = "0x0")]
    		public T[] elements;

    		[Token(Token = "0x6000083")]
    		public Row(int elementAmount)
    		{
    		}
    	}

    	[Token(Token = "0x4000141")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	[Min(1f)]
    	private int rowAmount;

    	[Token(Token = "0x4000142")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	[Min(1f)]
    	private int columnAmount;

    	[Token(Token = "0x4000143")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private Row[] rows;

    	[Token(Token = "0x1700000A")]
    	public int RowAmount
    	{
    		[Token(Token = "0x600007A")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	public int ColumnAmount
    	{
    		[Token(Token = "0x600007B")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700000C")]
    	public T this[int rowIndex, int columnIndex]
    	{
    		[Token(Token = "0x600007C")]
    		get
    		{
    			return default(T);
    		}
    		[Token(Token = "0x600007D")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600007E")]
    	public T GetElement(int rowIndex, int columnIndex)
    	{
    		return default(T);
    	}

    	[Token(Token = "0x600007F")]
    	public void SetElement(T value, int rowIndex, int columnIndex)
    	{
    	}

    	[Token(Token = "0x6000080")]
    	private void ThrowExceptionsIfInvalid(int rowIndex, int columnIndex)
    	{
    	}

    	[Token(Token = "0x6000081")]
    	public Array2D()
    	{
    	}

    	[Token(Token = "0x6000082")]
    	public Array2D(int rowAmount, int columnAmount)
    	{
    	}
    }
}
