using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Toggleables
{
    [Serializable]
    [Token(Token = "0x2000004")]
    public class ToggleableClass<TClass> where TClass : class
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool _enabled;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TClass _value;

    	[Token(Token = "0x17000001")]
    	private TClass Value
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x95C060 (shared) -- cmp byte [rcx+0x10] (_enabled): _value (+0x18) or null
    		[Token(Token = "0x6000003")]
    		get
    		{
    			return _enabled ? _value : null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x95C070 (shared) -- new ToggleableClass<TClass> (RGCTX class), value stored to
    	//   +0x18 only; _enabled stays false
    	[Token(Token = "0x6000004")]
    	public static implicit operator ToggleableClass<TClass>(TClass value)
    	{
    		return new ToggleableClass<TClass>
    		{
    			_value = value
    		};
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x95C140 (shared) -- the shipped null test on `value` (test rcx, rcx / je -> null),
    	//   then Value inlined (_enabled at +0x10 ? _value at +0x18 : null)
    	[Token(Token = "0x6000005")]
    	public static implicit operator TClass(ToggleableClass<TClass> value)
    	{
    		if (value != null)
    		{
    			return value.Value;
    		}
    		return null;
    	}

    	[Token(Token = "0x6000006")]
    	public ToggleableClass()
    	{
    	}
    }
}
