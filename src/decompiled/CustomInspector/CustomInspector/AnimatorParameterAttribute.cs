using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000009")]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class AnimatorParameterAttribute : PropertyAttribute
{
	[Token(Token = "0x40000A8")]
	[FieldOffset(Offset = "0x18")]
	public readonly string animatorPath;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
	public AnimatorParameterAttribute(string animatorPath)
	{
	}
}
