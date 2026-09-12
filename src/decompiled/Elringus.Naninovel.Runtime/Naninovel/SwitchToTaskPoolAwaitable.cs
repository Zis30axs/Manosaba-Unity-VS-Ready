using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20001CB")]
public struct SwitchToTaskPoolAwaitable
{
	[Token(Token = "0x20001CC")]
	public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		[Token(Token = "0x400079C")]
		[FieldOffset(Offset = "0x0")]
		private static readonly Action<object> switchToCallback;

		[Token(Token = "0x17000129")]
		public bool IsCompleted
		{
			[Token(Token = "0x600080D")]
			[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x600080E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public void GetResult()
		{
		}

		[Token(Token = "0x600080F")]
		[Address(RVA = "0x519290", Offset = "0x517C90", VA = "0x180519290", Slot = "5")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000810")]
		[Address(RVA = "0x519430", Offset = "0x517E30", VA = "0x180519430", Slot = "4")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000811")]
		[Address(RVA = "0x5195D0", Offset = "0x517FD0", VA = "0x1805195D0")]
		private static void Callback(object state)
		{
		}
	}

	[Token(Token = "0x600080C")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}
}
