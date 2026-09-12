using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000029")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
[Conditional("UNITY_EDITOR")]
public class MessageBoxAttribute : PropertyAttribute
{
	[Token(Token = "0x40000EE")]
	[FieldOffset(Offset = "0x18")]
	public readonly string content;

	[Token(Token = "0x40000EF")]
	[FieldOffset(Offset = "0x20")]
	public readonly MessageBoxType type;

	[Token(Token = "0x6000031")]
	[Address(RVA = "0x43D000", Offset = "0x43BA00", VA = "0x18043D000")]
	public MessageBoxAttribute(string content, MessageBoxType type = MessageBoxType.Error)
	{
	}
}
