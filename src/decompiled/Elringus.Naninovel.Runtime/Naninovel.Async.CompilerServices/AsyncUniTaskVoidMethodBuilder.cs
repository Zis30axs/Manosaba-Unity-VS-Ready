using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.CompilerServices;

[Token(Token = "0x2000657")]
public struct AsyncUniTaskVoidMethodBuilder
{
	[Token(Token = "0x400180F")]
	[FieldOffset(Offset = "0x0")]
	private Action moveNext;

	[Token(Token = "0x170004A0")]
	public UniTaskVoid Task
	{
		[Token(Token = "0x6001F44")]
		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
		get
		{
			return default(UniTaskVoid);
		}
	}

	[Token(Token = "0x6001F43")]
	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0")]
	[DebuggerHidden]
	public static AsyncUniTaskVoidMethodBuilder Create()
	{
		return default(AsyncUniTaskVoidMethodBuilder);
	}

	[Token(Token = "0x6001F45")]
	[Address(RVA = "0x4D1890", Offset = "0x4D0290", VA = "0x1804D1890")]
	[DebuggerHidden]
	public void SetException(Exception exception)
	{
	}

	[Token(Token = "0x6001F46")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[DebuggerHidden]
	public void SetResult()
	{
	}

	[Token(Token = "0x6001F47")]
	[DebuggerHidden]
	public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F48")]
	[DebuggerHidden]
	public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F49")]
	[DebuggerHidden]
	public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
	{
	}

	[Token(Token = "0x6001F4A")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	[DebuggerHidden]
	public void SetStateMachine(IAsyncStateMachine stateMachine)
	{
	}
}
