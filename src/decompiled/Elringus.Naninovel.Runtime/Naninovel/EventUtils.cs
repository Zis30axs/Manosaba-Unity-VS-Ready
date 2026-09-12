using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel;

[Token(Token = "0x20002DE")]
public static class EventUtils
{
	[Token(Token = "0x4000B34")]
	[FieldOffset(Offset = "0x0")]
	private static readonly List<RaycastResult> raycastResults;

	[Token(Token = "0x170001ED")]
	public static GameObject Selected
	{
		[Token(Token = "0x6000DC6")]
		[Address(RVA = "0x548450", Offset = "0x546E50", VA = "0x180548450")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000DC7")]
	[Address(RVA = "0x548570", Offset = "0x546F70", VA = "0x180548570")]
	public static GameObject GetHoveredGameObject()
	{
		return null;
	}

	[Token(Token = "0x6000DC8")]
	[Address(RVA = "0x548CE0", Offset = "0x5476E0", VA = "0x180548CE0")]
	public static void Select(GameObject go)
	{
	}

	[Token(Token = "0x6000DC9")]
	[Address(RVA = "0x548DF0", Offset = "0x5477F0", VA = "0x180548DF0")]
	public static void SafeInvoke(this Action action)
	{
	}

	[Token(Token = "0x6000DCA")]
	public static void SafeInvoke<T0>(this Action<T0> action, T0 arg0)
	{
	}

	[Token(Token = "0x6000DCB")]
	public static void SafeInvoke<T0, T1>(this Action<T0, T1> action, T0 arg0, T1 arg1)
	{
	}

	[Token(Token = "0x6000DCC")]
	public static void SafeInvoke<T0, T1, T2>(this Action<T0, T1, T2> action, T0 arg0, T1 arg1, T2 arg2)
	{
	}
}
