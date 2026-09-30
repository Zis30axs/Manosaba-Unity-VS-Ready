using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000012")]
    public readonly struct PooledObject<T> : IDisposable where T : class
    {
    	[Token(Token = "0x4000019")]
    	[FieldOffset(Offset = "0x0")]
    	[CompilerGenerated]
    	private readonly T _003Cobj_003EP;

    	[Token(Token = "0x400001A")]
    	[FieldOffset(Offset = "0x0")]
    	[CompilerGenerated]
    	private readonly ObjectPool<T> _003Cpool_003EP;

    	[Token(Token = "0x6000042")]
    	public PooledObject(T obj, ObjectPool<T> pool)
    	{ this = default; }

    	[Token(Token = "0x6000043")]
    	void IDisposable.Dispose()
    	{
    	}
    }
}
