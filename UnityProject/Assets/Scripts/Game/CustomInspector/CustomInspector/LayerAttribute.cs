using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000025")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class LayerAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000E4")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string requiredName;

    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public LayerAttribute()
    	{
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
    	public LayerAttribute(string requiredName)
    	{
    	}
    }
}
