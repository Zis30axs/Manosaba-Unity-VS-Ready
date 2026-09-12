using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000048")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class UnitAttribute : PropertyAttribute
{
	[Token(Token = "0x4000139")]
	[FieldOffset(Offset = "0x18")]
	public readonly string unitName;

	[Token(Token = "0x6000075")]
	[Address(RVA = "0x43E730", Offset = "0x43D130", VA = "0x18043E730")]
	public UnitAttribute(string unitName)
	{
	}
}
