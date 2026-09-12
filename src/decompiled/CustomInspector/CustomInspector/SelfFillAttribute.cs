using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000034")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class SelfFillAttribute : PropertyAttribute
{
	[Token(Token = "0x4000102")]
	[FieldOffset(Offset = "0x18")]
	public OwnerMode mode;

	[Token(Token = "0x4000103")]
	[FieldOffset(Offset = "0x1C")]
	public readonly bool hideIfFilled;

	[Token(Token = "0x6000050")]
	[Address(RVA = "0x43D670", Offset = "0x43C070", VA = "0x18043D670")]
	public SelfFillAttribute(bool hideIfFilled = false)
	{
	}
}
