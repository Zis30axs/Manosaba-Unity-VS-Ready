using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000635")]
internal static class ArrayUtil
{
	[MethodImpl(256)]
	[Token(Token = "0x6001E84")]
	public static void EnsureCapacity<T>(ref T[] array, int index)
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001E85")]
	private static void EnsureCore<T>(ref T[] array, int index)
	{
	}

	[Token(Token = "0x6001E86")]
	public static (T[], int) Materialize<T>(IEnumerable<T> source)
	{
		return default((T[], int));
	}
}
