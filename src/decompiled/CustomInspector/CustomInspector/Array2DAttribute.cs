using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200004C")]
[Conditional("UNITY_EDITOR")]
public class Array2DAttribute : PropertyAttribute
{
	[Token(Token = "0x6000079")]
	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
	public Array2DAttribute()
	{
	}
}
