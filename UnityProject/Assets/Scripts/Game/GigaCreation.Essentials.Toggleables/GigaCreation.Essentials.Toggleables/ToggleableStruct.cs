#nullable enable annotations
﻿using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Toggleables
{
    [Serializable]
    [Token(Token = "0x2000005")]
    public struct ToggleableStruct<TStruct> where TStruct : struct
    {
    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool _enabled;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private TStruct _value;

    	[Token(Token = "0x17000002")]
    	private readonly TStruct? ValueOrNull
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x95C180 (shared generic) -- tests the _enabled byte (field 0 of the RGCTX
    		//   offset table); set -> a Nullable built from _value, clear -> the zeroed Nullable (null)
    		[Token(Token = "0x6000007")]
    		get
    		{
    			return _enabled ? new TStruct?(_value) : null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x95C3A0 (shared generic) -- a zeroed struct (sub_306BB0) with only _value
    	//   copied in (RGCTX field-offset table +0x20); _enabled stays false
    	[Token(Token = "0x6000008")]
    	public static implicit operator ToggleableStruct<TStruct>(TStruct value)
    	{
    		return new ToggleableStruct<TStruct>
    		{
    			_value = value
    		};
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x95C510 (shared generic) -- calls get_ValueOrNull (0x95C180) and copies the result
    	[Token(Token = "0x6000009")]
    	public static implicit operator TStruct?(ToggleableStruct<TStruct> value)
    	{
    		return value.ValueOrNull;
    	}
    }
}
