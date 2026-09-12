using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000014")]
[Obsolete("Use [field: <attribute>] instead to directly work with the field")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class DisplayAutoPropertyAttribute : PropertyAttribute
{
	[Token(Token = "0x40000BF")]
	[FieldOffset(Offset = "0x18")]
	public readonly string propertyPath;

	[Token(Token = "0x40000C0")]
	[FieldOffset(Offset = "0x20")]
	public readonly bool allowChange;

	[Token(Token = "0x40000C1")]
	[FieldOffset(Offset = "0x28")]
	public string label;

	[Token(Token = "0x40000C2")]
	[FieldOffset(Offset = "0x30")]
	public string tooltip;

	[Token(Token = "0x600000D")]
	[Address(RVA = "0x43C840", Offset = "0x43B240", VA = "0x18043C840")]
	public DisplayAutoPropertyAttribute(string propertyPath, bool allowChange = true)
	{
	}
}
