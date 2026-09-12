using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x200063E")]
public abstract class ReusablePromise
{
	[Token(Token = "0x40017D5")]
	[FieldOffset(Offset = "0x10")]
	private ExceptionDispatchInfo exception;

	[Token(Token = "0x40017D6")]
	[FieldOffset(Offset = "0x18")]
	private object continuation;

	[Token(Token = "0x40017D7")]
	[FieldOffset(Offset = "0x20")]
	private AwaiterStatus status;

	[Token(Token = "0x17000495")]
	public UniTask Task
	{
		[Token(Token = "0x6001ED4")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		get
		{
			return default(UniTask);
		}
	}

	[Token(Token = "0x17000496")]
	public virtual bool IsCompleted
	{
		[Token(Token = "0x6001ED5")]
		[Address(RVA = "0x4CDE30", Offset = "0x4CC830", VA = "0x1804CDE30", Slot = "9")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000497")]
	public AwaiterStatus Status
	{
		[Token(Token = "0x6001ED7")]
		[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360", Slot = "4")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x6001ED6")]
	[Address(RVA = "0x4CDE40", Offset = "0x4CC840", VA = "0x1804CDE40", Slot = "10")]
	public virtual void GetResult()
	{
	}

	[Token(Token = "0x6001ED8")]
	[Address(RVA = "0x4CDF50", Offset = "0x4CC950", VA = "0x1804CDF50", Slot = "6")]
	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
	{
	}

	[Token(Token = "0x6001ED9")]
	[Address(RVA = "0x4CDF70", Offset = "0x4CC970", VA = "0x1804CDF70")]
	public void ResetStatus(bool forceReset)
	{
	}

	[Token(Token = "0x6001EDA")]
	[Address(RVA = "0x4CDF90", Offset = "0x4CC990", VA = "0x1804CDF90", Slot = "11")]
	public virtual bool TrySetCanceled()
	{
		return default(bool);
	}

	[Token(Token = "0x6001EDB")]
	[Address(RVA = "0x4CDFC0", Offset = "0x4CC9C0", VA = "0x1804CDFC0", Slot = "12")]
	public virtual bool TrySetException(Exception ex)
	{
		return default(bool);
	}

	[Token(Token = "0x6001EDC")]
	[Address(RVA = "0x4CE100", Offset = "0x4CCB00", VA = "0x1804CE100", Slot = "13")]
	public virtual bool TrySetResult()
	{
		return default(bool);
	}

	[Token(Token = "0x6001EDD")]
	[Address(RVA = "0x4CE130", Offset = "0x4CCB30", VA = "0x1804CE130")]
	private void TryInvokeContinuation()
	{
	}

	[Token(Token = "0x6001EDE")]
	[Address(RVA = "0x4CE2C0", Offset = "0x4CCCC0", VA = "0x1804CE2C0", Slot = "8")]
	public void OnCompleted(Action action)
	{
	}

	[Token(Token = "0x6001EDF")]
	[Address(RVA = "0x4CE2D0", Offset = "0x4CCCD0", VA = "0x1804CE2D0", Slot = "7")]
	public void UnsafeOnCompleted(Action action)
	{
	}

	[Token(Token = "0x6001EE0")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	protected ReusablePromise()
	{
	}
}
[Token(Token = "0x200063F")]
public abstract class ReusablePromise<T> : ICriticalNotifyCompletion, INotifyCompletion
{
	[Token(Token = "0x40017D8")]
	[FieldOffset(Offset = "0x0")]
	private T result;

	[Token(Token = "0x40017D9")]
	[FieldOffset(Offset = "0x0")]
	private ExceptionDispatchInfo exception;

	[Token(Token = "0x40017DA")]
	[FieldOffset(Offset = "0x0")]
	private object continuation;

	[Token(Token = "0x40017DB")]
	[FieldOffset(Offset = "0x0")]
	private AwaiterStatus status;

	[Token(Token = "0x17000498")]
	public UniTask<T> Task
	{
		[Token(Token = "0x6001EE1")]
		get
		{
			return default(UniTask<T>);
		}
	}

	[Token(Token = "0x17000499")]
	public virtual bool IsCompleted
	{
		[Token(Token = "0x6001EE2")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700049A")]
	protected T RawResult
	{
		[Token(Token = "0x6001EE3")]
		get
		{
			return default(T);
		}
	}

	[Token(Token = "0x1700049B")]
	public AwaiterStatus Status
	{
		[Token(Token = "0x6001EE6")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x6001EE4")]
	protected void ForceSetResult(T result)
	{
	}

	[Token(Token = "0x6001EE5")]
	public virtual T GetResult()
	{
		return default(T);
	}

	[Token(Token = "0x6001EE7")]
	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
	{
	}

	[Token(Token = "0x6001EE8")]
	public void ResetStatus(bool forceReset)
	{
	}

	[Token(Token = "0x6001EE9")]
	public virtual bool TrySetCanceled()
	{
		return default(bool);
	}

	[Token(Token = "0x6001EEA")]
	public virtual bool TrySetException(Exception ex)
	{
		return default(bool);
	}

	[Token(Token = "0x6001EEB")]
	public virtual bool TrySetResult(T result)
	{
		return default(bool);
	}

	[Token(Token = "0x6001EEC")]
	protected void TryInvokeContinuation()
	{
	}

	[Token(Token = "0x6001EED")]
	public void OnCompleted(Action action)
	{
	}

	[Token(Token = "0x6001EEE")]
	public void UnsafeOnCompleted(Action action)
	{
	}

	[Token(Token = "0x6001EEF")]
	protected ReusablePromise()
	{
	}
}
