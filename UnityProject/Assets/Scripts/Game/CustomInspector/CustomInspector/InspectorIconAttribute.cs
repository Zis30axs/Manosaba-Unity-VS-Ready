using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000023")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class InspectorIconAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000E0")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly InspectorIcon icon;

    	[Token(Token = "0x40000E1")]
    	[FieldOffset(Offset = "0x1C")]
    	public readonly bool appendAtEnd;

    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0x43CCC0", Offset = "0x43B6C0", VA = "0x18043CCC0")]
    	public InspectorIconAttribute(InspectorIcon icon, bool appendAtEnd = false)
    	{
    	}
    }
}
