using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200000B")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class AsButtonAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000AD")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly bool staysPressed;

    	[Token(Token = "0x40000AE")]
    	[FieldOffset(Offset = "0x1C")]
    	public Size size;

    	[Token(Token = "0x40000AF")]
    	[FieldOffset(Offset = "0x20")]
    	public FixedColor selectedColor;

    	[Token(Token = "0x40000B0")]
    	[FieldOffset(Offset = "0x28")]
    	public string label;

    	[Token(Token = "0x40000B1")]
    	[FieldOffset(Offset = "0x30")]
    	public string tooltip;

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0x43C590", Offset = "0x43AF90", VA = "0x18043C590")]
    	public AsButtonAttribute(bool staysPressed = true)
    	{
    	}
    }
}
