using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200021F")]
    public class NullableVector2 : Nullable<Vector2>
    {
    	[Token(Token = "0x600096F")]
    	[Address(RVA = "0x526EE0", Offset = "0x5258E0", VA = "0x180526EE0")]
    	public static implicit operator NullableVector2(Vector2 value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000970")]
    	[Address(RVA = "0x526F90", Offset = "0x525990", VA = "0x180526F90")]
    	public static implicit operator Vector2?(NullableVector2 nullable)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000971")]
    	[Address(RVA = "0x527070", Offset = "0x525A70", VA = "0x180527070")]
    	public NullableVector2()
    	{
    	}
    }
}
