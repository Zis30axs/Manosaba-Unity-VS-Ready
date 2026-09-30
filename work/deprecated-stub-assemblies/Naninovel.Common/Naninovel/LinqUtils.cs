#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000016")]
    public static class LinqUtils
    {
    	[Token(Token = "0x6000050")]
    	public static bool IsIndexValid<T>(this IEnumerable<T> e, int index) where T : notnull
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000051")]
    	public static T? FirstOrNull<T>(this IEnumerable<T> e) where T : struct
    	{
    		return null;
    	}

    	[Token(Token = "0x6000052")]
    	public static T? FirstOrNull<T>(this IEnumerable<T> e, Func<T, bool> predicate) where T : struct
    	{
    		return null;
    	}

    	[Token(Token = "0x6000053")]
    	public static T? LastOrNull<T>(this IEnumerable<T> e) where T : struct
    	{
    		return null;
    	}

    	[Token(Token = "0x6000054")]
    	public static T? LastOrNull<T>(this IEnumerable<T> e, Func<T, bool> predicate) where T : struct
    	{
    		return null;
    	}

    	[Token(Token = "0x6000055")]
    	public static T? AtOrNull<T>(this IEnumerable<T> e, int index) where T : struct
    	{
    		return null;
    	}
    }
}
