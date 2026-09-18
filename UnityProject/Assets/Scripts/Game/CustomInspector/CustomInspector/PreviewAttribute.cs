using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200002D")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class PreviewAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000F4")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly float thumbnailSize;

    	[Token(Token = "0x6000042")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public PreviewAttribute(Size size = Size.medium)
    	{
    	}
    }
}
