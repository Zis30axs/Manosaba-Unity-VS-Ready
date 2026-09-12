using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000036")]
public static class SelfFill
{
	[Token(Token = "0x6000051")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[Conditional("UNITY_EDITOR")]
	public static void CheckSelfFilled(this MonoBehaviour @object)
	{
	}

	[Token(Token = "0x6000052")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[Conditional("UNITY_EDITOR")]
	public static void CheckSelfFilled(this object @object, Transform owner)
	{
	}
}
