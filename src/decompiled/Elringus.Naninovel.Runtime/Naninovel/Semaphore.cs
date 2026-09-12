using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x2000129")]
public class Semaphore : IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x200012B")]
	[CompilerGenerated]
	private struct _003CWait_003Ed__5
{
		[Token(Token = "0x40004A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40004A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40004A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Semaphore _003C_003E4__this;

		[Token(Token = "0x40004A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40004A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600059B")]
		[Address(RVA = "0x5119B0", Offset = "0x5103B0", VA = "0x1805119B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600059C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400049C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly ConcurrentQueue<UniTaskCompletionSource> waiters;

	[Token(Token = "0x400049D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly int maxCount;

	[Token(Token = "0x400049E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
	private int count;

	[Token(Token = "0x6000593")]
	[Address(RVA = "0x5113C0", Offset = "0x50FDC0", VA = "0x1805113C0")]
	public Semaphore(int initialCount, int maxCount = 2147483647)
	{
	}

	[Token(Token = "0x6000594")]
	[Address(RVA = "0x511490", Offset = "0x50FE90", VA = "0x180511490")]
	public UniTask Wait()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000595")]
	[Address(RVA = "0x511550", Offset = "0x50FF50", VA = "0x180511550")]
	[AsyncStateMachine(typeof(_003CWait_003Ed__5))]
	public UniTask Wait(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000596")]
	[Address(RVA = "0x5116D0", Offset = "0x5100D0", VA = "0x1805116D0")]
	public void Release()
	{
	}

	[Token(Token = "0x6000597")]
	[Address(RVA = "0x511780", Offset = "0x510180", VA = "0x180511780")]
	public void Release(int releaseCount)
	{
	}

	[Token(Token = "0x6000598")]
	[Address(RVA = "0x511830", Offset = "0x510230", VA = "0x180511830", Slot = "4")]
	public void Dispose()
	{
	}
}
