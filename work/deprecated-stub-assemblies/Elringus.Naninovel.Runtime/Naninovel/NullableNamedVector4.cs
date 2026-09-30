using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000229")]
    public class NullableNamedVector4 : NullableNamed<NamedVector4, NullableVector4>
    {
    	[Token(Token = "0x600098D")]
    	[Address(RVA = "0x527E30", Offset = "0x526830", VA = "0x180527E30")]
    	public static implicit operator NullableNamedVector4(NamedVector4 value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600098E")]
    	[Address(RVA = "0x527ED0", Offset = "0x5268D0", VA = "0x180527ED0")]
    	public static implicit operator NamedVector4(NullableNamedVector4 nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x600098F")]
    	[Address(RVA = "0x527F60", Offset = "0x526960", VA = "0x180527F60")]
    	public NullableNamedVector4()
    	{
    	}
    }
}
