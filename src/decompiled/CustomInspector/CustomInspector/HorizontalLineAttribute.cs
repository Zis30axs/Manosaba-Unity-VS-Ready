using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000020")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class HorizontalLineAttribute : PropertyAttribute
{
	[Token(Token = "0x40000DA")]
	[FieldOffset(Offset = "0x18")]
	public string message;

	[Token(Token = "0x40000DB")]
	[FieldOffset(Offset = "0x20")]
	public float thickness;

	[Token(Token = "0x40000DC")]
	[FieldOffset(Offset = "0x24")]
	public float spacing;

	[Token(Token = "0x40000DD")]
	[FieldOffset(Offset = "0x28")]
	public float gapSize;

	[Token(Token = "0x40000DE")]
	[FieldOffset(Offset = "0x2C")]
	public readonly FixedColor color;

	[Token(Token = "0x600001B")]
	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
	private HorizontalLineAttribute()
	{
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0x43CB90", Offset = "0x43B590", VA = "0x18043CB90")]
	public HorizontalLineAttribute(float thickness = 1f, FixedColor color = FixedColor.Black, float spacing = 7f, float gapSize = 0f)
	{
	}

	[Token(Token = "0x600001D")]
	[Address(RVA = "0x43CBC0", Offset = "0x43B5C0", VA = "0x18043CBC0")]
	public HorizontalLineAttribute(string message, float thickness = 1f, FixedColor color = FixedColor.Cyan, float spacing = 15f, float gapSize = 0f)
	{
	}

	[Token(Token = "0x600001E")]
	[Address(RVA = "0x43CC40", Offset = "0x43B640", VA = "0x18043CC40")]
	public HorizontalLineAttribute(string message, float thickness, float spacing, float gapSize = 0f)
	{
	}
}
