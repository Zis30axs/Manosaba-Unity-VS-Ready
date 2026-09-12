using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000011")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class CopyPasteAttribute : PropertyAttribute
{
	[Token(Token = "0x40000BD")]
	[FieldOffset(Offset = "0x18")]
	public readonly bool previewClipboard;

	[Token(Token = "0x600000A")]
	[Address(RVA = "0x43C820", Offset = "0x43B220", VA = "0x18043C820")]
	public CopyPasteAttribute(bool previewClipboard = true)
	{
	}
}
