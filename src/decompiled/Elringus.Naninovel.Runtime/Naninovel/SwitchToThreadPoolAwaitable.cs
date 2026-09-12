using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20001C9")]
public struct SwitchToThreadPoolAwaitable
{
	[Token(Token = "0x20001CA")]
	public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		[Token(Token = "0x400079B")]
		[FieldOffset(Offset = "0x0")]
		private static readonly WaitCallback switchToCallback;

		[Token(Token = "0x17000128")]
		public bool IsCompleted
		{
			[Token(Token = "0x6000806")]
			[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000807")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public void GetResult()
		{
		}

		[Token(Token = "0x6000808")]
		[Address(RVA = "0x519080", Offset = "0x517A80", VA = "0x180519080", Slot = "5")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000809")]
		[Address(RVA = "0x5190E0", Offset = "0x517AE0", VA = "0x1805190E0", Slot = "4")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x600080A")]
		[Address(RVA = "0x519140", Offset = "0x517B40", VA = "0x180519140")]
		private static void Callback(object state)
		{
		}
	}

	[Token(Token = "0x6000805")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}
}
