using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20001C7")]
public struct SwitchToMainThreadAwaitable
{
	[Token(Token = "0x20001C8")]
	public struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		[Token(Token = "0x17000127")]
		public bool IsCompleted
		{
			[Token(Token = "0x6000801")]
			[Address(RVA = "0x518ED0", Offset = "0x5178D0", VA = "0x180518ED0")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000802")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public void GetResult()
		{
		}

		[Token(Token = "0x6000803")]
		[Address(RVA = "0x518FC0", Offset = "0x5179C0", VA = "0x180518FC0", Slot = "5")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000804")]
		[Address(RVA = "0x519020", Offset = "0x517A20", VA = "0x180519020", Slot = "4")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}
	}

	[Token(Token = "0x6000800")]
	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}
}
