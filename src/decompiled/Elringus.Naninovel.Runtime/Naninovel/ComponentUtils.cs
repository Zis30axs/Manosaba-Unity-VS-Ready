using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20002D7")]
public static class ComponentUtils
{
	[Token(Token = "0x6000DB4")]
	public static List<T> GetComponentsOnChildren<T>(this GameObject gameObject) where T : Component
	{
		return null;
	}

	[Token(Token = "0x6000DB5")]
	public static List<T> GetComponentsOnChildren<T>(this Component component) where T : Component
	{
		return null;
	}

	[Token(Token = "0x6000DB6")]
	public static T FindTopmostComponent<T>(this GameObject gameObject) where T : Component
	{
		return null;
	}

	[Token(Token = "0x6000DB7")]
	public static T FindTopmostComponent<T>(this Component component) where T : Component
	{
		return null;
	}
}
