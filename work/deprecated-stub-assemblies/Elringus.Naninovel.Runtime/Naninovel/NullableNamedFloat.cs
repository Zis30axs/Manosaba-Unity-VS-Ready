using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000225")]
    public class NullableNamedFloat : NullableNamed<NamedFloat, NullableFloat>
    {
    	[Token(Token = "0x6000981")]
    	[Address(RVA = "0x5278B0", Offset = "0x5262B0", VA = "0x1805278B0")]
    	public static implicit operator NullableNamedFloat(NamedFloat value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000982")]
    	[Address(RVA = "0x527950", Offset = "0x526350", VA = "0x180527950")]
    	public static implicit operator NamedFloat(NullableNamedFloat nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000983")]
    	[Address(RVA = "0x5279E0", Offset = "0x5263E0", VA = "0x1805279E0")]
    	public NullableNamedFloat()
    	{
    	}
    }
}
