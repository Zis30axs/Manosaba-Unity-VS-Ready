using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200002E")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class ProgressBarAttribute : PropertyAttribute
{
	[Token(Token = "0x40000F5")]
	[FieldOffset(Offset = "0x18")]
	public Size size;

	[Token(Token = "0x40000F6")]
	[FieldOffset(Offset = "0x20")]
	public readonly string minGetter;

	[Token(Token = "0x40000F7")]
	[FieldOffset(Offset = "0x28")]
	public readonly float min;

	[Token(Token = "0x40000F8")]
	[FieldOffset(Offset = "0x30")]
	public readonly string maxGetter;

	[Token(Token = "0x40000F9")]
	[FieldOffset(Offset = "0x38")]
	public readonly float max;

	[Token(Token = "0x40000FA")]
	[FieldOffset(Offset = "0x3C")]
	public bool isReadOnly;

	[Token(Token = "0x6000043")]
	[Address(RVA = "0x43D480", Offset = "0x43BE80", VA = "0x18043D480")]
	public ProgressBarAttribute(float max)
	{
	}

	[Token(Token = "0x6000044")]
	[Address(RVA = "0x43D490", Offset = "0x43BE90", VA = "0x18043D490")]
	[Obsolete("Use syntax ProgressBar(float max, size = Size.medium) instead of ProgressBar(float max, Size.medium)")]
	public ProgressBarAttribute(float max, Size size = Size.medium)
	{
	}

	[Token(Token = "0x6000045")]
	[Address(RVA = "0x43D4A0", Offset = "0x43BEA0", VA = "0x18043D4A0")]
	public ProgressBarAttribute(float min, float max)
	{
	}

	[Token(Token = "0x6000046")]
	[Address(RVA = "0x43D4B0", Offset = "0x43BEB0", VA = "0x18043D4B0")]
	public ProgressBarAttribute(string minGetter, float max)
	{
	}

	[Token(Token = "0x6000047")]
	[Address(RVA = "0x43D510", Offset = "0x43BF10", VA = "0x18043D510")]
	public ProgressBarAttribute(float min, string maxGetter)
	{
	}

	[Token(Token = "0x6000048")]
	[Address(RVA = "0x43D570", Offset = "0x43BF70", VA = "0x18043D570")]
	public ProgressBarAttribute(string minGetter, string maxGetter)
	{
	}
}
