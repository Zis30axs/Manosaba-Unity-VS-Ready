using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000049")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class UnwrapAttribute : PropertyAttribute
    {
    	[Token(Token = "0x400013A")]
    	[FieldOffset(Offset = "0x18")]
    	public bool applyName;

    	[Token(Token = "0x6000076")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public UnwrapAttribute()
    	{
    	}
    }
}
