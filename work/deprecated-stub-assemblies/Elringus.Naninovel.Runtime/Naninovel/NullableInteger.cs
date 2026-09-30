using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200021C")]
    public class NullableInteger : Nullable<int>
    {
    	[Token(Token = "0x6000966")]
    	[Address(RVA = "0x526A10", Offset = "0x525410", VA = "0x180526A10")]
    	public static implicit operator NullableInteger(int value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000967")]
    	[Address(RVA = "0x526AB0", Offset = "0x5254B0", VA = "0x180526AB0")]
    	public static implicit operator int?(NullableInteger nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000968")]
    	[Address(RVA = "0x526B70", Offset = "0x525570", VA = "0x180526B70")]
    	public NullableInteger()
    	{
    	}
    }
}
