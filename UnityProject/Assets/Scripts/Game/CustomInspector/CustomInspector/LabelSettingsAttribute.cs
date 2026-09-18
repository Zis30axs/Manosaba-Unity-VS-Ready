using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000024")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class LabelSettingsAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000E2")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly LabelStyle style;

    	[Token(Token = "0x40000E3")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly string newName;

    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0x43CCE0", Offset = "0x43B6E0", VA = "0x18043CCE0")]
    	public LabelSettingsAttribute(LabelStyle style)
    	{
    	}

    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0x43CCF0", Offset = "0x43B6F0", VA = "0x18043CCF0")]
    	public LabelSettingsAttribute(string newName, LabelStyle style = LabelStyle.FullSpacing)
    	{
    	}
    }
}
