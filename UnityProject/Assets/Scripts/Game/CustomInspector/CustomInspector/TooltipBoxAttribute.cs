using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000046")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class TooltipBoxAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000138")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string content;

    	[Token(Token = "0x6000073")]
    	[Address(RVA = "0x43E730", Offset = "0x43D130", VA = "0x18043E730")]
    	public TooltipBoxAttribute(string content)
    	{
    	}
    }
}
