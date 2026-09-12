using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Async;

namespace Naninovel;

[Token(Token = "0x20001D6")]
public class UniTaskCompletionSource
{
	[Token(Token = "0x40007A2")]
	private const int Pending = 0;

	[Token(Token = "0x40007A3")]
	private const int Succeeded = 1;

	[Token(Token = "0x40007A4")]
	private const int Faulted = 2;

	[Token(Token = "0x40007A5")]
	private const int Canceled = 3;

	[Token(Token = "0x40007A6")]
	[FieldOffset(Offset = "0x10")]
	private int state;

	[Token(Token = "0x40007A7")]
	[FieldOffset(Offset = "0x18")]
	private ExceptionHolder exception;

	[Token(Token = "0x40007A8")]
	[FieldOffset(Offset = "0x20")]
	private object continuation;

	[Token(Token = "0x1700012B")]
	private AwaiterStatus Naninovel_002EAsync_002EIAwaiter_002EStatus
	{
		[Token(Token = "0x6000823")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "4")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x1700012C")]
	private bool Naninovel_002EAsync_002EIAwaiter_002EIsCompleted
	{
		[Token(Token = "0x6000824")]
		[Address(RVA = "0x519A80", Offset = "0x518480", VA = "0x180519A80", Slot = "5")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700012D")]
	public UniTask Task
	{
		[Token(Token = "0x6000825")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		get
		{
			return default(UniTask);
		}
	}

	[Token(Token = "0x6000826")]
	[Address(RVA = "0x519A90", Offset = "0x518490", VA = "0x180519A90", Slot = "6")]
	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
	{
	}

	[Token(Token = "0x6000827")]
	[Address(RVA = "0x519BA0", Offset = "0x5185A0", VA = "0x180519BA0", Slot = "7")]
	private void System_002ERuntime_002ECompilerServices_002EICriticalNotifyCompletion_002EUnsafeOnCompleted(Action action)
	{
	}

	[Token(Token = "0x6000828")]
	[Address(RVA = "0x519F20", Offset = "0x518920", VA = "0x180519F20")]
	private void TryInvokeContinuation()
	{
	}

	[Token(Token = "0x6000829")]
	[Address(RVA = "0x51A0D0", Offset = "0x518AD0", VA = "0x18051A0D0", Slot = "9")]
	public bool TrySetResult()
	{
		return default(bool);
	}

	[Token(Token = "0x600082A")]
	[Address(RVA = "0x51A100", Offset = "0x518B00", VA = "0x18051A100", Slot = "10")]
	public bool TrySetException(Exception exception)
	{
		return default(bool);
	}

	[Token(Token = "0x600082B")]
	[Address(RVA = "0x51A2D0", Offset = "0x518CD0", VA = "0x18051A2D0", Slot = "11")]
	public bool TrySetCanceled()
	{
		return default(bool);
	}

	[Token(Token = "0x600082C")]
	[Address(RVA = "0x51A300", Offset = "0x518D00", VA = "0x18051A300")]
	public bool TrySetCanceled(OperationCanceledException exception)
	{
		return default(bool);
	}

	[Token(Token = "0x600082D")]
	[Address(RVA = "0x51A4D0", Offset = "0x518ED0", VA = "0x18051A4D0", Slot = "8")]
	private void System_002ERuntime_002ECompilerServices_002EINotifyCompletion_002EOnCompleted(Action continuation)
	{
	}

	[Token(Token = "0x600082E")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public UniTaskCompletionSource()
	{
	}
}
[Token(Token = "0x20001D7")]
public class UniTaskCompletionSource<T> : IPromise<T>, IResolvePromise<T>, IRejectPromise, ICancelPromise
{
	[Token(Token = "0x40007A9")]
	private const int Pending = 0;

	[Token(Token = "0x40007AA")]
	private const int Succeeded = 1;

	[Token(Token = "0x40007AB")]
	private const int Faulted = 2;

	[Token(Token = "0x40007AC")]
	private const int Canceled = 3;

	[Token(Token = "0x40007AD")]
	[FieldOffset(Offset = "0x0")]
	private int state;

	[Token(Token = "0x40007AE")]
	[FieldOffset(Offset = "0x0")]
	private T value;

	[Token(Token = "0x40007AF")]
	[FieldOffset(Offset = "0x0")]
	private ExceptionHolder exception;

	[Token(Token = "0x40007B0")]
	[FieldOffset(Offset = "0x0")]
	private object continuation;

	[Token(Token = "0x1700012E")]
	private bool Naninovel_002EAsync_002EIAwaiter_002EIsCompleted
	{
		[Token(Token = "0x600082F")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700012F")]
	public UniTask<T> Task
	{
		[Token(Token = "0x6000830")]
		get
		{
			return default(UniTask<T>);
		}
	}

	[Token(Token = "0x17000130")]
	public UniTask UnitTask
	{
		[Token(Token = "0x6000831")]
		get
		{
			return default(UniTask);
		}
	}

	[Token(Token = "0x17000131")]
	private AwaiterStatus Naninovel_002EAsync_002EIAwaiter_002EStatus
	{
		[Token(Token = "0x6000832")]
		get
		{
			return default(AwaiterStatus);
		}
	}

	[Token(Token = "0x6000833")]
	private T Naninovel_002EAsync_002EIAwaiter_003CT_003E_002EGetResult()
	{
		return default(T);
	}

	[Token(Token = "0x6000834")]
	private void System_002ERuntime_002ECompilerServices_002EICriticalNotifyCompletion_002EUnsafeOnCompleted(Action action)
	{
	}

	[Token(Token = "0x6000835")]
	private void TryInvokeContinuation()
	{
	}

	[Token(Token = "0x6000836")]
	public bool TrySetResult(T value)
	{
		return default(bool);
	}

	[Token(Token = "0x6000837")]
	public bool TrySetException(Exception exception)
	{
		return default(bool);
	}

	[Token(Token = "0x6000838")]
	public bool TrySetCanceled()
	{
		return default(bool);
	}

	[Token(Token = "0x6000839")]
	public bool TrySetCanceled(OperationCanceledException exception)
	{
		return default(bool);
	}

	[Token(Token = "0x600083A")]
	private void Naninovel_002EAsync_002EIAwaiter_002EGetResult()
	{
	}

	[Token(Token = "0x600083B")]
	private void System_002ERuntime_002ECompilerServices_002EINotifyCompletion_002EOnCompleted(Action continuation)
	{
	}

	[Token(Token = "0x600083C")]
	public UniTaskCompletionSource()
	{
	}
}
