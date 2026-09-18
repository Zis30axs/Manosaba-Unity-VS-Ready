using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200000F")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ButtonAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000B6")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string methodPath;

    	[Token(Token = "0x40000B7")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly bool usePropertyAsParameter;

    	[Token(Token = "0x40000B8")]
    	[FieldOffset(Offset = "0x28")]
    	public string label;

    	[Token(Token = "0x40000B9")]
    	[FieldOffset(Offset = "0x30")]
    	public string tooltip;

    	[Token(Token = "0x40000BA")]
    	[FieldOffset(Offset = "0x38")]
    	public Size size;

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0x43C680", Offset = "0x43B080", VA = "0x18043C680")]
    	public ButtonAttribute(string methodPath, bool usePropertyAsParameter = false)
    	{
    	}
    }
}
