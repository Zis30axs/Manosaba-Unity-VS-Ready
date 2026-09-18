using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200002B")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class MultipleOfAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000F2")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string stepPath;

    	[Token(Token = "0x40000F3")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly double step;

    	[Token(Token = "0x6000038")]
    	[Address(RVA = "0x43D140", Offset = "0x43BB40", VA = "0x18043D140")]
    	private MultipleOfAttribute()
    	{
    	}

    	[Token(Token = "0x6000039")]
    	[Address(RVA = "0x43D160", Offset = "0x43BB60", VA = "0x18043D160")]
    	public MultipleOfAttribute(string stepPath)
    	{
    	}

    	[Token(Token = "0x600003A")]
    	[Address(RVA = "0x43D1D0", Offset = "0x43BBD0", VA = "0x18043D1D0")]
    	public MultipleOfAttribute(double value)
    	{
    	}

    	[Token(Token = "0x600003B")]
    	[Address(RVA = "0x43D1F0", Offset = "0x43BBF0", VA = "0x18043D1F0")]
    	public MultipleOfAttribute(float value)
    	{
    	}

    	[Token(Token = "0x600003C")]
    	[Address(RVA = "0x43D210", Offset = "0x43BC10", VA = "0x18043D210")]
    	public MultipleOfAttribute(int value)
    	{
    	}
    }
}
