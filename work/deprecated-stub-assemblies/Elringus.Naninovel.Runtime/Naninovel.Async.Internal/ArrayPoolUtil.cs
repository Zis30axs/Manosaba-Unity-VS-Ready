using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x2000633")]
    internal static class ArrayPoolUtil
    {
    	[Token(Token = "0x2000634")]
    	public struct RentArray<T> : IDisposable
    	{
    		[Token(Token = "0x40017B5")]
    		[FieldOffset(Offset = "0x0")]
    		public readonly T[] Array;

    		[Token(Token = "0x40017B6")]
    		[FieldOffset(Offset = "0x0")]
    		public readonly int Length;

    		[Token(Token = "0x40017B7")]
    		[FieldOffset(Offset = "0x0")]
    		private ArrayPool<T> pool;

    		[Token(Token = "0x6001E81")]
    		public RentArray(T[] array, int length, ArrayPool<T> pool)
    		{ this = default; }

    		[Token(Token = "0x6001E82")]
    		public void Dispose()
    		{
    		}

    		[Token(Token = "0x6001E83")]
    		public void DisposeManually(bool clearArray)
    		{
    		}
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6001E7E")]
    	internal static void EnsureCapacity<T>(ref T[] array, int index, ArrayPool<T> pool)
    	{
    	}

    	[MethodImpl(8)]
    	[Token(Token = "0x6001E7F")]
    	private static void EnsureCapacityCore<T>(ref T[] array, int index, ArrayPool<T> pool)
    	{
    	}

    	[Token(Token = "0x6001E80")]
    	public static RentArray<T> Materialize<T>(IEnumerable<T> source)
    	{
    		return default(RentArray<T>);
    	}
    }
}
