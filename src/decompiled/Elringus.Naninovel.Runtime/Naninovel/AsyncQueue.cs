using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x2000207")]
public class AsyncQueue<T>
{
	[StructLayout(3)]
	[Token(Token = "0x2000208")]
	[CompilerGenerated]
	private struct _003CWait_003Ed__5
{
		[Token(Token = "0x40008A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<T> _003C_003Et__builder;

		[Token(Token = "0x40008A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncQueue<T> _003C_003E4__this;

		[Token(Token = "0x40008A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40008A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000926")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000927")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40008A1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly ConcurrentQueue<T> queue;

	[Token(Token = "0x40008A2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Semaphore semaphore;

	[Token(Token = "0x1700014C")]
	public int Count
	{
		[Token(Token = "0x600091F")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x6000920")]
	public void Enqueue(T item)
	{
	}

	[Token(Token = "0x6000921")]
	[AsyncStateMachine(typeof(AsyncQueue<>._003CWait_003Ed__5))]
	public UniTask<T> Wait(AsyncToken token)
	{
		return default(UniTask<T>);
	}

	[Token(Token = "0x6000922")]
	public void Dispose()
	{
	}

	[Token(Token = "0x6000923")]
	public IEnumerator<T> GetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x6000924")]
	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
	{
		return null;
	}

	[Token(Token = "0x6000925")]
	public AsyncQueue()
	{
	}
}
