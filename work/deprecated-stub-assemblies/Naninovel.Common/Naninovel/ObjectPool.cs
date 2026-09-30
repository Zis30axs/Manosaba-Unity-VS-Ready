#nullable enable annotations
﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200000F")]
    public abstract class ObjectPool
    {
    	[Token(Token = "0x17000007")]
    	public static bool PoolingEnabled
    	{
    		[Token(Token = "0x6000029")]
    		[Address(RVA = "0x10CFB70", Offset = "0x10CE570", VA = "0x1810CFB70")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600002A")]
    		[Address(RVA = "0x10CFBD0", Offset = "0x10CE5D0", VA = "0x1810CFBD0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	protected ObjectPool()
    	{
    	}
    }
    [Token(Token = "0x2000010")]
    public sealed class ObjectPool<T> : ObjectPool, IDisposable where T : class
    {
    	[Token(Token = "0x2000011")]
    	public sealed class Options
    	{
    		[Token(Token = "0x1700000B")]
    		public int MinSize
    		{
    			[Token(Token = "0x6000037")]
    			[CompilerGenerated]
    			get
    			{
    				return default(int);
    			}
    			[Token(Token = "0x6000038")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700000C")]
    		public int MaxSize
    		{
    			[Token(Token = "0x6000039")]
    			[CompilerGenerated]
    			get
    			{
    				return default(int);
    			}
    			[Token(Token = "0x600003A")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700000D")]
    		public Action<T>? OnRent
    		{
    			[Token(Token = "0x600003B")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x600003C")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700000E")]
    		public Action<T>? OnReturn
    		{
    			[Token(Token = "0x600003D")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x600003E")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700000F")]
    		public Action<T>? OnDrop
    		{
    			[Token(Token = "0x600003F")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6000040")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6000041")]
    		public Options()
    		{
    		}
    	}

    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly List<T> pool;

    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly Func<T> factory;

    	[Token(Token = "0x4000012")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly Options options;

    	[Token(Token = "0x4000013")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private T? lastReturned;

    	[Token(Token = "0x17000008")]
    	public int Total
    	{
    		[Token(Token = "0x600002D")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x600002E")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000009")]
    	public int Rented
    	{
    		[Token(Token = "0x600002F")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	public int Available
    	{
    		[Token(Token = "0x6000030")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x6000031")]
    	public ObjectPool(Func<T> factory, [Optional] Options? options)
    	{
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6000032")]
    	public T Rent()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000033")]
    	public PooledObject<T> Rent(out T obj)
    	{
    		obj = default;
    		obj = default;
    		return default(PooledObject<T>);
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6000034")]
    	public void Return(T obj)
    	{
    	}

    	[Token(Token = "0x6000035")]
    	public void Drop()
    	{
    	}

    	[Token(Token = "0x6000036")]
    	public void Dispose()
    	{
    	}
    }
}
