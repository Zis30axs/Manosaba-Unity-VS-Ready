using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200001A")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class GetSetAttribute : PropertyAttribute
{
	[Token(Token = "0x40000C9")]
	[FieldOffset(Offset = "0x18")]
	public readonly string getmethodPath;

	[Token(Token = "0x40000CA")]
	[FieldOffset(Offset = "0x20")]
	public readonly string setmethodPath;

	[Token(Token = "0x40000CB")]
	[FieldOffset(Offset = "0x28")]
	public string label;

	[Token(Token = "0x40000CC")]
	[FieldOffset(Offset = "0x30")]
	public string tooltip;

	[Token(Token = "0x6000015")]
	[Address(RVA = "0x43C930", Offset = "0x43B330", VA = "0x18043C930")]
	public GetSetAttribute(string getmethodPath, string setmethodPath)
	{
	}
}
