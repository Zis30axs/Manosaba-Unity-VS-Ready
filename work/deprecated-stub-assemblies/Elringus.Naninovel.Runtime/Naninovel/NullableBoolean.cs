using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200021E")]
    public class NullableBoolean : Nullable<bool>
    {
    	[Token(Token = "0x600096C")]
    	[Address(RVA = "0x526D40", Offset = "0x525740", VA = "0x180526D40")]
    	public static implicit operator NullableBoolean(bool value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600096D")]
    	[Address(RVA = "0x526DF0", Offset = "0x5257F0", VA = "0x180526DF0")]
    	public static implicit operator bool?(NullableBoolean nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x600096E")]
    	[Address(RVA = "0x526EB0", Offset = "0x5258B0", VA = "0x180526EB0")]
    	public NullableBoolean()
    	{
    	}
    }
}
