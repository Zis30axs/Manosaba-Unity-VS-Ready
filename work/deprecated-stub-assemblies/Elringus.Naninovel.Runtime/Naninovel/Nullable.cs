using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000219")]
    public class Nullable<TValue> : INullable<TValue>, INullableValue
    {
    	[Token(Token = "0x40008AC")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TValue value;

    	[Token(Token = "0x40008AD")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool hasValue;

    	[Token(Token = "0x17000155")]
    	public TValue Value
    	{
    		[Token(Token = "0x6000953")]
    		get
    		{
    			return default(TValue);
    		}
    		[Token(Token = "0x6000954")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000156")]
    	public bool HasValue
    	{
    		[Token(Token = "0x6000955")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000956")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000157")]
    	protected bool IsValueType
    	{
    		[Token(Token = "0x6000957")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6000958")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000959")]
    	public static implicit operator TValue(Nullable<TValue> nullable)
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x600095A")]
    	public static implicit operator Nullable<TValue>(TValue value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600095B")]
    	protected virtual TValue GetValue()
    	{
    		return default(TValue);
    	}

    	[Token(Token = "0x600095C")]
    	protected virtual void SetValue(TValue value)
    	{
    	}

    	[Token(Token = "0x600095D")]
    	protected virtual bool GetHasValue()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600095E")]
    	protected virtual void SetHasValue(bool hasValue)
    	{
    	}

    	[Token(Token = "0x600095F")]
    	public Nullable()
    	{
    	}
    }
}
