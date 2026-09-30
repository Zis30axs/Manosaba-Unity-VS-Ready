using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000404")]
    public class StringListParameter : ParameterList<NullableString>
    {
    	[Token(Token = "0x6001368")]
    	[Address(RVA = "0x5AB260", Offset = "0x5A9C60", VA = "0x1805AB260")]
    	public static implicit operator StringListParameter(List<string> value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001369")]
    	[Address(RVA = "0x5AB4E0", Offset = "0x5A9EE0", VA = "0x1805AB4E0")]
    	public static implicit operator List<string>(StringListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x600136A")]
    	[Address(RVA = "0x5AB6E0", Offset = "0x5AA0E0", VA = "0x1805AB6E0")]
    	public static implicit operator string[](StringListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x600136B")]
    	[Address(RVA = "0x5AB8D0", Offset = "0x5AA2D0", VA = "0x1805AB8D0")]
    	public IReadOnlyList<string> ToReadOnlyList()
    	{
    		return null;
    	}

    	[Token(Token = "0x600136C")]
    	[Address(RVA = "0x5ABB00", Offset = "0x5AA500", VA = "0x1805ABB00", Slot = "26")]
    	protected override NullableString ParseItemValueText(string valueText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x600136D")]
    	[Address(RVA = "0x5ABB60", Offset = "0x5AA560", VA = "0x1805ABB60")]
    	public StringListParameter()
    	{
    	}
    }
}
