using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200000C")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class AsRangeAttribute : PropertyAttribute
{
	[Token(Token = "0x40000B2")]
	[FieldOffset(Offset = "0x18")]
	public readonly float minLimit;

	[Token(Token = "0x40000B3")]
	[FieldOffset(Offset = "0x1C")]
	public readonly float maxLimit;

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x43C640", Offset = "0x43B040", VA = "0x18043C640")]
	public AsRangeAttribute(float minLimit, float maxLimit)
	{
	}
}
