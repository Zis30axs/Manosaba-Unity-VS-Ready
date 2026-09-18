using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000012")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class DecimalsAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000BE")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly int amount;

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0x43C830", Offset = "0x43B230", VA = "0x18043C830")]
    	public DecimalsAttribute(int amount)
    	{
    	}
    }
}
