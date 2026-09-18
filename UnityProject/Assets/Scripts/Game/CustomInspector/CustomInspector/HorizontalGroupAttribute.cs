using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200001F")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class HorizontalGroupAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000D7")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly bool beginNewGroup;

    	[Token(Token = "0x40000D8")]
    	[FieldOffset(Offset = "0x1C")]
    	public float size;

    	[Token(Token = "0x40000D9")]
    	public const int defaultOrder = -10;

    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0x43CB70", Offset = "0x43B570", VA = "0x18043CB70")]
    	public HorizontalGroupAttribute(bool beginNewGroup = false)
    	{
    	}
    }
}
