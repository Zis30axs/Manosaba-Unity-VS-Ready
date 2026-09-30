using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003F6")]
    public class NamedBooleanListParameter : ParameterList<NullableNamedBoolean>
    {
    	[Token(Token = "0x600131B")]
    	[Address(RVA = "0x5A8E10", Offset = "0x5A7810", VA = "0x1805A8E10")]
    	public static implicit operator NamedBooleanListParameter(List<NullableNamedBoolean> value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600131C")]
    	[Address(RVA = "0x5A8EB0", Offset = "0x5A78B0", VA = "0x1805A8EB0")]
    	public static implicit operator List<NullableNamedBoolean>(NamedBooleanListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x600131D")]
    	[Address(RVA = "0x5A8F40", Offset = "0x5A7940", VA = "0x1805A8F40", Slot = "26")]
    	protected override NullableNamedBoolean ParseItemValueText(string valueText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x600131E")]
    	[Address(RVA = "0x5A9050", Offset = "0x5A7A50", VA = "0x1805A9050")]
    	public NamedBooleanListParameter()
    	{
    	}
    }
}
