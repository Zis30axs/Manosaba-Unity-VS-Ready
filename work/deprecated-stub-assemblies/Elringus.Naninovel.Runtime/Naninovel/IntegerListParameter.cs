using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003F2")]
    public class IntegerListParameter : ParameterList<NullableInteger>
    {
    	[Token(Token = "0x6001305")]
    	[Address(RVA = "0x5A72C0", Offset = "0x5A5CC0", VA = "0x1805A72C0")]
    	public static implicit operator IntegerListParameter(List<int?> value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001306")]
    	[Address(RVA = "0x5A7540", Offset = "0x5A5F40", VA = "0x1805A7540")]
    	public static implicit operator List<int?>(IntegerListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001307")]
    	[Address(RVA = "0x5A7740", Offset = "0x5A6140", VA = "0x1805A7740")]
    	public static implicit operator int?[](IntegerListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001308")]
    	[Address(RVA = "0x5A7930", Offset = "0x5A6330", VA = "0x1805A7930", Slot = "26")]
    	protected override NullableInteger ParseItemValueText(string valueText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x6001309")]
    	[Address(RVA = "0x5A7960", Offset = "0x5A6360", VA = "0x1805A7960")]
    	public IntegerListParameter()
    	{
    	}
    }
}
