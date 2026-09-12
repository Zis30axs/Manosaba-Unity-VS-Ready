using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000013")]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
[Conditional("UNITY_EDITOR")]
public class Delayed2Attribute : PropertyAttribute
{
	[Token(Token = "0x600000C")]
	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
	public Delayed2Attribute()
	{
	}
}
