using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200003F")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowMethodAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000126")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string getmethodPath;

    	[Token(Token = "0x4000127")]
    	[FieldOffset(Offset = "0x20")]
    	public string label;

    	[Token(Token = "0x4000128")]
    	[FieldOffset(Offset = "0x28")]
    	public string tooltip;

    	[Token(Token = "0x600006A")]
    	[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
    	public ShowMethodAttribute(string getmethodPath)
    	{
    	}
    }
}
