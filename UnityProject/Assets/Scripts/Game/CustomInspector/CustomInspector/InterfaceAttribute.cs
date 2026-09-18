using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000063")]
    [Conditional("UNITY_EDITOR")]
    public class InterfaceAttribute : PropertyAttribute
    {
    	[Token(Token = "0x6000130")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public InterfaceAttribute()
    	{
    	}
    }
}
