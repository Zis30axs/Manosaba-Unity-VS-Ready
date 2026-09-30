using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x2000639")]
    internal sealed class LazyPromise
    {
    	[Token(Token = "0x40017C1")]
    	[FieldOffset(Offset = "0x10")]
    	private Func<UniTask> factory;

    	[Token(Token = "0x40017C2")]
    	[FieldOffset(Offset = "0x18")]
    	private UniTask value;

    	[Token(Token = "0x17000490")]
    	public bool IsCompleted
    	{
    		[Token(Token = "0x6001EA8")]
    		[Address(RVA = "0x4CCB20", Offset = "0x4CB520", VA = "0x1804CCB20", Slot = "5")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000491")]
    	public AwaiterStatus Status
    	{
    		[Token(Token = "0x6001EA9")]
    		[Address(RVA = "0x4CCBC0", Offset = "0x4CB5C0", VA = "0x1804CCBC0", Slot = "4")]
    		get
    		{
    			return default(AwaiterStatus);
    		}
    	}

    	[Token(Token = "0x6001EA6")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public LazyPromise(Func<UniTask> factory)
    	{
    	}

    	[Token(Token = "0x6001EA7")]
    	[Address(RVA = "0x4CCA90", Offset = "0x4CB490", VA = "0x1804CCA90")]
    	private void Create()
    	{
    	}

    	[Token(Token = "0x6001EAA")]
    	[Address(RVA = "0x4CCC60", Offset = "0x4CB660", VA = "0x1804CCC60")]
    	public void GetResult()
    	{
    	}

    	[Token(Token = "0x6001EAB")]
    	[Address(RVA = "0x4CCC60", Offset = "0x4CB660", VA = "0x1804CCC60", Slot = "6")]
    	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
    	{
    	}

    	[Token(Token = "0x6001EAC")]
    	[Address(RVA = "0x4CCCF0", Offset = "0x4CB6F0", VA = "0x1804CCCF0", Slot = "7")]
    	public void UnsafeOnCompleted(Action continuation)
    	{
    	}

    	[Token(Token = "0x6001EAD")]
    	[Address(RVA = "0x4CCCF0", Offset = "0x4CB6F0", VA = "0x1804CCCF0", Slot = "8")]
    	public void OnCompleted(Action continuation)
    	{
    	}
    }
    [Token(Token = "0x200063A")]
    internal sealed class LazyPromise<T> : ICriticalNotifyCompletion, INotifyCompletion
    {
    	[Token(Token = "0x40017C3")]
    	[FieldOffset(Offset = "0x0")]
    	private Func<UniTask<T>> factory;

    	[Token(Token = "0x40017C4")]
    	[FieldOffset(Offset = "0x0")]
    	private UniTask<T> value;

    	[Token(Token = "0x17000492")]
    	public bool IsCompleted
    	{
    		[Token(Token = "0x6001EB0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000493")]
    	public AwaiterStatus Status
    	{
    		[Token(Token = "0x6001EB1")]
    		get
    		{
    			return default(AwaiterStatus);
    		}
    	}

    	[Token(Token = "0x6001EAE")]
    	public LazyPromise(Func<UniTask<T>> factory)
    	{
    	}

    	[Token(Token = "0x6001EAF")]
    	private void Create()
    	{
    	}

    	[Token(Token = "0x6001EB2")]
    	public T GetResult()
    	{
    		return default(T);
    	}

    	[Token(Token = "0x6001EB3")]
    	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
    	{
    	}

    	[Token(Token = "0x6001EB4")]
    	public void UnsafeOnCompleted(Action continuation)
    	{
    	}

    	[Token(Token = "0x6001EB5")]
    	public void OnCompleted(Action continuation)
    	{
    	}
    }
}
