using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000032")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class RichTextAttribute : PropertyAttribute
{
	[Token(Token = "0x600004D")]
	[Address(RVA = "0x43D650", Offset = "0x43C050", VA = "0x18043D650")]
	public RichTextAttribute()
	{
	}

	[Token(Token = "0x600004E")]
	[Address(RVA = "0x43D650", Offset = "0x43C050", VA = "0x18043D650")]
	[Obsolete("Please use the [Multiline]-attribute or [TextArea]-attribute to edit the input-box height and it's wrapping.")]
	public RichTextAttribute(bool allowMultipleLines, int maxLines = 15)
	{
	}
}
