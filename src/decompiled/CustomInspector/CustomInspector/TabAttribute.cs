using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000042")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class TabAttribute : PropertyAttribute
{
	[Token(Token = "0x400012D")]
	[FieldOffset(Offset = "0x18")]
	public readonly string groupName;

	[Token(Token = "0x400012E")]
	[FieldOffset(Offset = "0x20")]
	[Obsolete("Please use the [BackgroundColor]-attribute instead")]
	public FixedColor backgroundColor;

	[Token(Token = "0x600006E")]
	[Address(RVA = "0x43E450", Offset = "0x43CE50", VA = "0x18043E450")]
	public TabAttribute(string groupName)
	{
	}

	[Token(Token = "0x600006F")]
	[Address(RVA = "0x43E4B0", Offset = "0x43CEB0", VA = "0x18043E4B0")]
	public TabAttribute(InspectorIcon icon)
	{
	}
}
