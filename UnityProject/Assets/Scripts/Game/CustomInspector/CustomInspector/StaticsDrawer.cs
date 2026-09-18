using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x2000080")]
    public class StaticsDrawer
    {
    	[Token(Token = "0x40001AF")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private bool b;

    	[Token(Token = "0x60001F4")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public StaticsDrawer()
    	{
    	}
    }
}
