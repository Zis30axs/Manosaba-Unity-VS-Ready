using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000022")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class IndentAttribute : PropertyAttribute
{
	[Token(Token = "0x40000DF")]
	[FieldOffset(Offset = "0x18")]
	public readonly int additionalIndentLevel;

	[Token(Token = "0x6000022")]
	[Address(RVA = "0x43C830", Offset = "0x43B230", VA = "0x18043C830")]
	public IndentAttribute(int additionalIndentLevel = 1)
	{
	}
}
