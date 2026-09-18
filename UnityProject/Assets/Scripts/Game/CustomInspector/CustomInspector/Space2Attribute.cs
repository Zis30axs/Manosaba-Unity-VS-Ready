using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000041")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class Space2Attribute : PropertyAttribute
    {
    	[Token(Token = "0x400012C")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly float pixels;

    	[Token(Token = "0x600006C")]
    	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
    	private Space2Attribute()
    	{
    	}

    	[Token(Token = "0x600006D")]
    	[Address(RVA = "0x43E430", Offset = "0x43CE30", VA = "0x18043E430")]
    	public Space2Attribute(float pixel)
    	{
    	}
    }
}
