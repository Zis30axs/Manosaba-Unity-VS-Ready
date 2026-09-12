using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000019")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class FromChildrenAttribute : PropertyAttribute
{
	[Token(Token = "0x40000C8")]
	[FieldOffset(Offset = "0x18")]
	public bool allowNull;

	[Token(Token = "0x6000014")]
	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
	public FromChildrenAttribute()
	{
	}
}
