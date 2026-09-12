using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200001C")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class HideFieldAttribute : PropertyAttribute
{
	[Token(Token = "0x6000018")]
	[Address(RVA = "0x43CAF0", Offset = "0x43B4F0", VA = "0x18043CAF0")]
	public HideFieldAttribute()
	{
	}
}
