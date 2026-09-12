using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000031")]
[AttributeUsage(AttributeTargets.Field)]
[Conditional("UNITY_EDITOR")]
public class RequireTypeAttribute : PropertyAttribute
{
	[Token(Token = "0x17000007")]
	public Type requiredType
	{
		[Token(Token = "0x600004A")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600004B")]
		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600004C")]
	[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
	public RequireTypeAttribute(Type type)
	{
	}
}
