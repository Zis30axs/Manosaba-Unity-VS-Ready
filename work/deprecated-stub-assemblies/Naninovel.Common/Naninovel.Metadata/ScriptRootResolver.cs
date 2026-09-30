using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Metadata
{
    [Token(Token = "0x2000082")]
    public static class ScriptRootResolver
    {
    	[Token(Token = "0x60002EC")]
    	[Address(RVA = "0x10F0170", Offset = "0x10EEB70", VA = "0x1810F0170")]
    	public static string Resolve(IEnumerable<string> fileUris)
    	{
    		return null;
    	}

    	[Token(Token = "0x60002ED")]
    	[Address(RVA = "0x10F05B0", Offset = "0x10EEFB0", VA = "0x1810F05B0")]
    	private static ReadOnlySpan<char> GetCommonPath(ReadOnlySpan<char> common, ReadOnlySpan<char> path)
    	{
    		return default(ReadOnlySpan<char>);
    	}
    }
}
