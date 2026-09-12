using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x20001F4")]
[AsyncMethodBuilder(typeof(AsyncUniTaskVoidMethodBuilder))]
public struct UniTaskVoid
{
	[Token(Token = "0x20001F5")]
	public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		[Token(Token = "0x17000134")]
		[DebuggerHidden]
		public bool IsCompleted
		{
			[Token(Token = "0x60008A4")]
			[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60008A5")]
		[Address(RVA = "0x51DC40", Offset = "0x51C640", VA = "0x18051DC40")]
		[DebuggerHidden]
		public void GetResult()
		{
		}

		[Token(Token = "0x60008A6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x60008A7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "4")]
		[DebuggerHidden]
		public void UnsafeOnCompleted(Action continuation)
		{
		}
	}

	[Token(Token = "0x60008A2")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public void Forget()
	{
	}

	[Token(Token = "0x60008A3")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	[DebuggerHidden]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}
}
