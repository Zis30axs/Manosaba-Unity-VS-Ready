using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003EE")]
    public class DecimalListParameter : ParameterList<NullableFloat>
    {
    	[Token(Token = "0x60012F0")]
    	[Address(RVA = "0x5A6370", Offset = "0x5A4D70", VA = "0x1805A6370")]
    	public static implicit operator DecimalListParameter(List<float?> value)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012F1")]
    	[Address(RVA = "0x5A65F0", Offset = "0x5A4FF0", VA = "0x1805A65F0")]
    	public static implicit operator List<float?>(DecimalListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012F2")]
    	[Address(RVA = "0x5A67F0", Offset = "0x5A51F0", VA = "0x1805A67F0")]
    	public static implicit operator float?[](DecimalListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012F3")]
    	[Address(RVA = "0x5A69E0", Offset = "0x5A53E0", VA = "0x1805A69E0", Slot = "26")]
    	protected override NullableFloat ParseItemValueText(string valueText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x60012F4")]
    	[Address(RVA = "0x5A6A10", Offset = "0x5A5410", VA = "0x1805A6A10")]
    	public DecimalListParameter()
    	{
    	}
    }
}
