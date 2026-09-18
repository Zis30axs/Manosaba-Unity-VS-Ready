#nullable enable annotations
using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200001B")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class GUIColorAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000CD")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string colorString;

    	[Token(Token = "0x40000CE")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly FixedColor? fixedColor;

    	[Token(Token = "0x40000CF")]
    	[FieldOffset(Offset = "0x28")]
    	public readonly bool colorWholeUI;

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x43C9F0", Offset = "0x43B3F0", VA = "0x18043C9F0")]
    	public GUIColorAttribute(string color = "(0.9, 0.0, 0, 1)", bool colorWholeUI = false)
    	{
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0x43CA60", Offset = "0x43B460", VA = "0x18043CA60")]
    	public GUIColorAttribute(FixedColor fixedColor, bool colorWholeUI = false)
    	{
    	}
    }
}
