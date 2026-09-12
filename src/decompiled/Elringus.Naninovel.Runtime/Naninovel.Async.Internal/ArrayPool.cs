using System.Threading;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000632")]
internal sealed class ArrayPool<T>
{
	[Token(Token = "0x40017B0")]
	private const int DefaultMaxNumberOfArraysPerBucket = 50;

	[Token(Token = "0x40017B1")]
	[FieldOffset(Offset = "0x0")]
	private static readonly T[] EmptyArray;

	[Token(Token = "0x40017B2")]
	[FieldOffset(Offset = "0x0")]
	public static readonly ArrayPool<T> Shared;

	[Token(Token = "0x40017B3")]
	[FieldOffset(Offset = "0x0")]
	private readonly MinimumQueue<T[]>[] buckets;

	[Token(Token = "0x40017B4")]
	[FieldOffset(Offset = "0x0")]
	private readonly SpinLock[] locks;

	[Token(Token = "0x6001E78")]
	private ArrayPool()
	{
	}

	[Token(Token = "0x6001E79")]
	public T[] Rent(int minimumLength)
	{
		return null;
	}

	[Token(Token = "0x6001E7A")]
	public void Return(T[] array, bool clearArray = false)
	{
	}

	[Token(Token = "0x6001E7B")]
	private static int CalculateSize(int size)
	{
		return default(int);
	}

	[Token(Token = "0x6001E7C")]
	private static int GetQueueIndex(int size)
	{
		return default(int);
	}
}
