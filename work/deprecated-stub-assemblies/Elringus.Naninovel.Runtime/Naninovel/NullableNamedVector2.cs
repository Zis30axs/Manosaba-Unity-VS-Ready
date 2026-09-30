using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000227")]
    public class NullableNamedVector2 : NullableNamed<NamedVector2, NullableVector2>
    {
    	[Token(Token = "0x6000987")]
    	[Address(RVA = "0x527B70", Offset = "0x526570", VA = "0x180527B70")]
    	public static implicit operator NullableNamedVector2(NamedVector2 value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000988")]
    	[Address(RVA = "0x527C10", Offset = "0x526610", VA = "0x180527C10")]
    	public static implicit operator NamedVector2(NullableNamedVector2 nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000989")]
    	[Address(RVA = "0x527CA0", Offset = "0x5266A0", VA = "0x180527CA0")]
    	public NullableNamedVector2()
    	{
    	}
    }
}
