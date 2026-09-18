using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector.Extensions
{
    [Token(Token = "0x2000004")]
    public static class DebugTest
    {
    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0x444B60", Offset = "0x443560", VA = "0x180444B60")]
    	[Conditional("UNITY_EDITOR")]
    	public static void CheckFilled(this MonoBehaviour @object, Type attributeType)
    	{
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0x444DC0", Offset = "0x4437C0", VA = "0x180444DC0")]
    	[Conditional("UNITY_EDITOR")]
    	public static void CheckFilled(this object _object, Transform owner, Type attributeType)
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x445130", Offset = "0x443B30", VA = "0x180445130")]
    	private static string GetPathString(this Transform child)
    	{
    		return null;
    	}
    }
}
