using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x200005A")]
[Conditional("UNITY_EDITOR")]
public class LineGraphAttribute : PropertyAttribute
{
	[Token(Token = "0x4000158")]
	public const float defaultGraphHeight = 150f;

	[Token(Token = "0x4000159")]
	[FieldOffset(Offset = "0x18")]
	public float graphHeight;

	[Token(Token = "0x60000C6")]
	[Address(RVA = "0x440150", Offset = "0x43EB50", VA = "0x180440150")]
	public LineGraphAttribute()
	{
	}
}
