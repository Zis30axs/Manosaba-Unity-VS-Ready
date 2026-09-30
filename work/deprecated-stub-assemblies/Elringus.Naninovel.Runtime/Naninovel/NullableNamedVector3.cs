using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000228")]
    public class NullableNamedVector3 : NullableNamed<NamedVector3, NullableVector3>
    {
    	[Token(Token = "0x600098A")]
    	[Address(RVA = "0x527CD0", Offset = "0x5266D0", VA = "0x180527CD0")]
    	public static implicit operator NullableNamedVector3(NamedVector3 value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600098B")]
    	[Address(RVA = "0x527D70", Offset = "0x526770", VA = "0x180527D70")]
    	public static implicit operator NamedVector3(NullableNamedVector3 nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x600098C")]
    	[Address(RVA = "0x527E00", Offset = "0x526800", VA = "0x180527E00")]
    	public NullableNamedVector3()
    	{
    	}
    }
}
