using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003FA")]
    public class NamedIntegerListParameter : ParameterList<NullableNamedInteger>
    {
    	[Token(Token = "0x600132B")]
    	[Address(RVA = "0x5A9870", Offset = "0x5A8270", VA = "0x1805A9870")]
    	public static implicit operator NamedIntegerListParameter(List<NullableNamedInteger> value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600132C")]
    	[Address(RVA = "0x5A9910", Offset = "0x5A8310", VA = "0x1805A9910")]
    	public static implicit operator List<NullableNamedInteger>(NamedIntegerListParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x600132D")]
    	[Address(RVA = "0x5A99A0", Offset = "0x5A83A0", VA = "0x1805A99A0", Slot = "26")]
    	protected override NullableNamedInteger ParseItemValueText(string valueText, out string errors)
    	{
    		errors = default;
    		errors = default;
    		return null;
    	}

    	[Token(Token = "0x600132E")]
    	[Address(RVA = "0x5A9AB0", Offset = "0x5A84B0", VA = "0x1805A9AB0")]
    	public NamedIntegerListParameter()
    	{
    	}
    }
}
