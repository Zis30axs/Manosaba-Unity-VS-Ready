using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Token(Token = "0x2000018")]
public static class ForceFill
{
	[Token(Token = "0x6000012")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[Conditional("UNITY_EDITOR")]
	public static void CheckForceFilled(this MonoBehaviour @object)
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[Conditional("UNITY_EDITOR")]
	public static void CheckForceFilled(this object @object, Transform owner)
	{
	}
}
