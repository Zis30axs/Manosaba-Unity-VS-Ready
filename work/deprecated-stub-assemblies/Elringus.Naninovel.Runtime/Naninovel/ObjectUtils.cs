using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002F3")]
    public static class ObjectUtils
    {
    	[Token(Token = "0x6000E1A")]
    	[Address(RVA = "0x54BE20", Offset = "0x54A820", VA = "0x18054BE20")]
    	public static void DestroyOrImmediate(UnityEngine.Object obj)
    	{
    	}

    	[Token(Token = "0x6000E1B")]
    	[Address(RVA = "0x54BF90", Offset = "0x54A990", VA = "0x18054BF90")]
    	public static void DestroyAllChildren(Transform trs)
    	{
    	}

    	[Token(Token = "0x6000E1C")]
    	[Address(RVA = "0x54C1C0", Offset = "0x54ABC0", VA = "0x18054C1C0")]
    	public static void AssertRequiredObjects(this Component component, params UnityEngine.Object[] requiredObjects)
    	{
    	}

    	[Token(Token = "0x6000E1D")]
    	[Address(RVA = "0x54C3C0", Offset = "0x54ADC0", VA = "0x18054C3C0")]
    	public static void ForEachDescendant(this GameObject gameObject, Action<GameObject> action, bool invokeOnSelf = true)
    	{
    	}

    	[Token(Token = "0x6000E1E")]
    	[Address(RVA = "0x54C6C0", Offset = "0x54B0C0", VA = "0x18054C6C0")]
    	public static bool IsValid(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000E1F")]
    	[Address(RVA = "0x54C7D0", Offset = "0x54B1D0", VA = "0x18054C7D0")]
    	public static bool IsEditedInPrefabMode(GameObject obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000E20")]
    	[Address(RVA = "0x54C7D0", Offset = "0x54B1D0", VA = "0x18054C7D0")]
    	public static bool IsPartOfPrefab(GameObject obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000E21")]
    	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0")]
    	public static string BuildAssetLink(UnityEngine.Object asset, [Optional] int? line)
    	{
    		return null;
    	}
    }
}
