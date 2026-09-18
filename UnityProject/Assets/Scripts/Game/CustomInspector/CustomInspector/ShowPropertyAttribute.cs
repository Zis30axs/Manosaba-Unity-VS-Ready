using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000040")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowPropertyAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000129")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string getPropertyPath;

    	[Token(Token = "0x400012A")]
    	[FieldOffset(Offset = "0x20")]
    	public string label;

    	[Token(Token = "0x400012B")]
    	[FieldOffset(Offset = "0x28")]
    	public string tooltip;

    	[Token(Token = "0x600006B")]
    	[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
    	public ShowPropertyAttribute(string getPropertyPath)
    	{
    	}
    }
}
