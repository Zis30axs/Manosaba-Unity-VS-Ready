using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200000E")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class BackgroundColorAttribute : PropertyAttribute
{
	[Token(Token = "0x40000B4")]
	[FieldOffset(Offset = "0x18")]
	public readonly FixedColor color;

	[Token(Token = "0x40000B5")]
	[FieldOffset(Offset = "0x1C")]
	public readonly float borderSize;

	[Token(Token = "0x6000007")]
	[Address(RVA = "0x43C660", Offset = "0x43B060", VA = "0x18043C660")]
	public BackgroundColorAttribute(FixedColor color = FixedColor.DarkGray, float borderSize = 5f)
	{
	}
}
