using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200031A")]
    public static class InputPreprocessor
    {
    	[Token(Token = "0x4000BB8")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly HashSet<Func<string, string>> preprocessors;

    	[Token(Token = "0x6000EE0")]
    	[Address(RVA = "0x55D4F0", Offset = "0x55BEF0", VA = "0x18055D4F0")]
    	public static string PreprocessInput(string input)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000EE1")]
    	[Address(RVA = "0x55D680", Offset = "0x55C080", VA = "0x18055D680")]
    	public static bool AddPreprocessor(Func<string, string> preprocessor)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000EE2")]
    	[Address(RVA = "0x55D710", Offset = "0x55C110", VA = "0x18055D710")]
    	public static bool RemovePreprocessor(Func<string, string> preprocessor)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000EE3")]
    	[Address(RVA = "0x55D7A0", Offset = "0x55C1A0", VA = "0x18055D7A0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	public static void ResetPreprocessor()
    	{
    	}
    }
}
