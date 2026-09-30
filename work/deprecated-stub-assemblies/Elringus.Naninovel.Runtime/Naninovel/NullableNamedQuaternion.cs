using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200022A")]
    public class NullableNamedQuaternion : NullableNamed<NamedQuaternion, NullableQuaternion>
    {
    	[Token(Token = "0x6000990")]
    	[Address(RVA = "0x527F90", Offset = "0x526990", VA = "0x180527F90")]
    	public static implicit operator NullableNamedQuaternion(NamedQuaternion value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000991")]
    	[Address(RVA = "0x528030", Offset = "0x526A30", VA = "0x180528030")]
    	public static implicit operator NamedQuaternion(NullableNamedQuaternion nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000992")]
    	[Address(RVA = "0x5280C0", Offset = "0x526AC0", VA = "0x1805280C0")]
    	public NullableNamedQuaternion()
    	{
    	}
    }
}
