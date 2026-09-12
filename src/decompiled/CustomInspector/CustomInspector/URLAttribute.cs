using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200004A")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class URLAttribute : PropertyAttribute
{
	[Token(Token = "0x400013B")]
	[FieldOffset(Offset = "0x18")]
	public readonly string link;

	[Token(Token = "0x400013C")]
	[FieldOffset(Offset = "0x20")]
	public string label;

	[Token(Token = "0x400013D")]
	[FieldOffset(Offset = "0x28")]
	public string tooltip;

	[Token(Token = "0x6000077")]
	[Address(RVA = "0x43E790", Offset = "0x43D190", VA = "0x18043E790")]
	public URLAttribute(string link)
	{
	}
}
