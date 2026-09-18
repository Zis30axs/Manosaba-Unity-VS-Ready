using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000045")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class ToolbarAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000135")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly float height;

    	[Token(Token = "0x4000136")]
    	[FieldOffset(Offset = "0x1C")]
    	public readonly float space;

    	[Token(Token = "0x4000137")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly float maxWidth;

    	[Token(Token = "0x6000072")]
    	[Address(RVA = "0x43E5E0", Offset = "0x43CFE0", VA = "0x18043E5E0")]
    	public ToolbarAttribute(float height = 30f, float space = 10f, float maxWidth = 150f)
    	{
    	}
    }
}
