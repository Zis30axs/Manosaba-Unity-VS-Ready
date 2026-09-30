using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200021B")]
    public class NullableString : Nullable<string>
    {
    	[Token(Token = "0x6000963")]
    	[Address(RVA = "0x5268B0", Offset = "0x5252B0", VA = "0x1805268B0")]
    	public static implicit operator NullableString(string value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000964")]
    	[Address(RVA = "0x526950", Offset = "0x525350", VA = "0x180526950")]
    	public static implicit operator string(NullableString nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000965")]
    	[Address(RVA = "0x5269E0", Offset = "0x5253E0", VA = "0x1805269E0")]
    	public NullableString()
    	{
    	}
    }
}
