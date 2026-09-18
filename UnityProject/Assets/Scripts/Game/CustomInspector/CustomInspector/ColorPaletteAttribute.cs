using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000010")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class ColorPaletteAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000BB")]
    	[FieldOffset(Offset = "0x18")]
    	public bool hideFoldout;

    	[Token(Token = "0x40000BC")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly string paletteName;

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0x43C780", Offset = "0x43B180", VA = "0x18043C780")]
    	public ColorPaletteAttribute(string paletteName = "default")
    	{
    	}
    }
}
