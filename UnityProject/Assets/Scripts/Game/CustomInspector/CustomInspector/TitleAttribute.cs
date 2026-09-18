using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000044")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class TitleAttribute : PropertyAttribute
    {
    	[Token(Token = "0x400012F")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string content;

    	[Token(Token = "0x4000130")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly bool underlined;

    	[Token(Token = "0x4000131")]
    	[FieldOffset(Offset = "0x24")]
    	public int upperSpacing;

    	[Token(Token = "0x4000132")]
    	[FieldOffset(Offset = "0x28")]
    	public string tooltip;

    	[Token(Token = "0x4000133")]
    	[FieldOffset(Offset = "0x30")]
    	public byte fontSize;

    	[Token(Token = "0x4000134")]
    	[FieldOffset(Offset = "0x34")]
    	public TextAlignment alignment;

    	[Token(Token = "0x6000071")]
    	[Address(RVA = "0x43E570", Offset = "0x43CF70", VA = "0x18043E570")]
    	public TitleAttribute(string content, bool underlined = false)
    	{
    	}
    }
}
