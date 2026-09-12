using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x200000D")]
public static class GenericPool<T> where T : class, new()
{
	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x0")]
	private static readonly ObjectPool<T> pool;

	[Token(Token = "0x6000021")]
	public static T Rent()
	{
		return null;
	}

	[Token(Token = "0x6000022")]
	public static PooledObject<T> Rent(out T obj)
	{
		obj = default;
		obj = default;
		return default(PooledObject<T>);
	}

	[Token(Token = "0x6000023")]
	public static void Return(T obj)
	{
	}

	[Token(Token = "0x6000024")]
	public static void Drop()
	{
	}
}
