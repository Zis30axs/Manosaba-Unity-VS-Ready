using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000226")]
    public class NullableNamedBoolean : NullableNamed<NamedBoolean, NullableBoolean>
    {
    	[Token(Token = "0x6000984")]
    	[Address(RVA = "0x527A10", Offset = "0x526410", VA = "0x180527A10")]
    	public static implicit operator NullableNamedBoolean(NamedBoolean value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000985")]
    	[Address(RVA = "0x527AB0", Offset = "0x5264B0", VA = "0x180527AB0")]
    	public static implicit operator NamedBoolean(NullableNamedBoolean nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000986")]
    	[Address(RVA = "0x527B40", Offset = "0x526540", VA = "0x180527B40")]
    	public NullableNamedBoolean()
    	{
    	}
    }
}
