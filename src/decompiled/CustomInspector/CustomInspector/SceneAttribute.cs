using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000033")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class SceneAttribute : PropertyAttribute
{
	[Token(Token = "0x4000101")]
	[FieldOffset(Offset = "0x18")]
	public readonly bool useFullPath;

	[Token(Token = "0x600004F")]
	[Address(RVA = "0x43D660", Offset = "0x43C060", VA = "0x18043D660")]
	public SceneAttribute(bool useFullPath = false)
	{
	}
}
