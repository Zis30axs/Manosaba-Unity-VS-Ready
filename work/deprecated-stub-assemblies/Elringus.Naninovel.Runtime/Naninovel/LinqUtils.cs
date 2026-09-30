using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20002ED")]
    public static class LinqUtils
    {
    	[Token(Token = "0x6000DEB")]
    	public static void RemoveLastItem<T>(this List<T> list, [Optional] Predicate<T> predicate)
    	{
    	}

    	[Token(Token = "0x6000DEC")]
    	public static IEnumerable<T> TakeLast<T>(this IReadOnlyCollection<T> source, int count)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000DED")]
    	public static void ReplaceWith<T>(this ICollection<T> source, IEnumerable<T> from)
    	{
    	}

    	[Token(Token = "0x6000DEE")]
    	public static int GetArrayHashCode<T>(this T[] array)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DEF")]
    	public static bool IsIndexValid<T>(this T[] array, int index)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DF0")]
    	public static bool IsIndexValid<T>(this List<T> list, int index)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DF1")]
    	public static bool IsIndexValid<T>(this IReadOnlyCollection<T> list, int index)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DF2")]
    	public static int IndexOf<T>(this IReadOnlyList<T> list, T itemToFind)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DF3")]
    	public static int IndexOf<T>(this IList<T> list, Predicate<T> predicate)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DF4")]
    	public static int IndexOf<T>(this IReadOnlyList<T> list, Predicate<T> predicate)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DF5")]
    	public static T Random<T>(this IList<T> list)
    	{
    		return default(T);
    	}

    	[Token(Token = "0x6000DF6")]
    	public static T Random<T>(this T[] array)
    	{
    		return default(T);
    	}

    	[Token(Token = "0x6000DF7")]
    	public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property, [Optional] IEqualityComparer<TKey> propertyComparer)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000DF8")]
    	public static float ProgressOf<T>(this IList<T> list, T currentItem)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6000DF9")]
    	public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000DFA")]
    	public static int RemoveAll<T>(this LinkedList<T> list, Predicate<T> match)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DFB")]
    	public static IList<T> TopologicalOrder<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, bool warnCyclic = true)
    	{
    		return null;
    	}
    }
}
