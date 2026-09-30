using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20004B0")]
    public static class TransitionUtils
    {
    	[Token(Token = "0x40011D2")]
    	[FieldOffset(Offset = "0x0")]
    	public static readonly string DefaultTransition;

    	[Token(Token = "0x40011D3")]
    	private const string keywordPrefix = "NANINOVEL_TRANSITION_";

    	[Token(Token = "0x40011D4")]
    	private const string crossfade = "Crossfade";

    	[Token(Token = "0x40011D5")]
    	[FieldOffset(Offset = "0x8")]
    	private static readonly Dictionary<string, Vector4> nameToDefaultParamsMap;

    	[Token(Token = "0x6001766")]
    	[Address(RVA = "0x5F3350", Offset = "0x5F1D50", VA = "0x1805F3350")]
    	public static string ResolveParameterValue(StringParameter param)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001767")]
    	[Address(RVA = "0x5F35D0", Offset = "0x5F1FD0", VA = "0x1805F35D0")]
    	public static string ToShaderKeyword(string transition)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001768")]
    	[Address(RVA = "0x5F3630", Offset = "0x5F2030", VA = "0x1805F3630")]
    	public static Vector4 GetDefaultParams(string transition)
    	{
    		return default(Vector4);
    	}

    	[Token(Token = "0x6001769")]
    	[Address(RVA = "0x5F3720", Offset = "0x5F2120", VA = "0x1805F3720")]
    	public static string GetEnabled(Material material)
    	{
    		return null;
    	}

    	[Token(Token = "0x600176A")]
    	[Address(RVA = "0x5F38A0", Offset = "0x5F22A0", VA = "0x1805F38A0")]
    	public static void EnableKeyword(Material material, string transition)
    	{
    	}
    }
}
