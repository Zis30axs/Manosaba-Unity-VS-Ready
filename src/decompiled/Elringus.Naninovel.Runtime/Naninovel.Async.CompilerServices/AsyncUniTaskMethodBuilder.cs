using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.CompilerServices;

[Token(Token = "0x2000655")]
public struct AsyncUniTaskMethodBuilder
{
	[Token(Token = "0x400180A")]
	[FieldOffset(Offset = "0x0")]
	private UniTaskCompletionSource promise;

	[Token(Token = "0x400180B")]
	[FieldOffset(Offset = "0x8")]
	private Action moveNext;

	[Token(Token = "0x1700049E")]
	[DebuggerHidden]
	public UniTask Task
	{
		[Token(Token = "0x6001F34")]
		[Address(RVA = "0x4D1540", Offset = "0x4CFF40", VA = "0x1804D1540")]
		get
		{
			return default(UniTask);
		}
	}

	[Token(Token = "0x6001F33")]
	[Address(RVA = "0x4D1530", Offset = "0x4CFF30", VA = "0x1804D1530")]
	[DebuggerHidden]
	public static AsyncUniTaskMethodBuilder Create()
	{
		return default(AsyncUniTaskMethodBuilder);
	}

	[Token(Token = "0x6001F35")]
	[Address(RVA = "0x4D16A0", Offset = "0x4D00A0", VA = "0x1804D16A0")]
	[DebuggerHidden]
	public void SetException(Exception exception)
	{
	}

	[Token(Token = "0x6001F36")]
	[Address(RVA = "0x4D17D0", Offset = "0x4D01D0", VA = "0x1804D17D0")]
	[DebuggerHidden]
	public void SetResult()
	{
	}

	[Token(Token = "0x6001F37")]
	[DebuggerHidden]
	public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F38")]
	[DebuggerHidden]
	public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F39")]
	[DebuggerHidden]
	public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F3A")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[DebuggerHidden]
	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
	}
}
[Token(Token = "0x2000656")]
public struct AsyncUniTaskMethodBuilder<T>
{
	[Token(Token = "0x400180C")]
	[FieldOffset(Offset = "0x0")]
	private T result;

	[Token(Token = "0x400180D")]
	[FieldOffset(Offset = "0x0")]
	private UniTaskCompletionSource<T> promise;

	[Token(Token = "0x400180E")]
	[FieldOffset(Offset = "0x0")]
	private Action moveNext;

	[Token(Token = "0x1700049F")]
	[DebuggerHidden]
	public UniTask<T> Task
	{
		[Token(Token = "0x6001F3C")]
		get
		{
			return default(UniTask<T>);
		}
	}

	[Token(Token = "0x6001F3B")]
	[DebuggerHidden]
	public static AsyncUniTaskMethodBuilder<T> Create()
	{
		return default(AsyncUniTaskMethodBuilder<T>);
	}

	[Token(Token = "0x6001F3D")]
	[DebuggerHidden]
	public void SetException(Exception exception)
	{
	}

	[Token(Token = "0x6001F3E")]
	[DebuggerHidden]
	public void SetResult(T result)
	{
	}

	[Token(Token = "0x6001F3F")]
	[DebuggerHidden]
	public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F40")]
	[DebuggerHidden]
	public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F41")]
	[DebuggerHidden]
	public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F42")]
	[DebuggerHidden]
	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
	}
}
