using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000017")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class ForceFillAttribute : PropertyAttribute
{
	[Token(Token = "0x40000C4")]
	public const float errorSize = 35f;

	[Token(Token = "0x40000C5")]
	[FieldOffset(Offset = "0x18")]
	public readonly string[] notAllowed;

	[Token(Token = "0x40000C6")]
	[FieldOffset(Offset = "0x20")]
	public string errorMessage;

	[Token(Token = "0x40000C7")]
	[FieldOffset(Offset = "0x28")]
	public bool onlyTestInPlayMode;

	[Token(Token = "0x6000010")]
	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
	public ForceFillAttribute()
	{
	}

	[Token(Token = "0x6000011")]
	[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
	public ForceFillAttribute(params string[] notAllowed)
	{
	}
}
