using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20001CD")]
public readonly struct SwitchToSynchronizationContextAwaitable
{
	[Token(Token = "0x20001CE")]
	public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		[Token(Token = "0x400079E")]
		[FieldOffset(Offset = "0x0")]
		private static readonly SendOrPostCallback switchToCallback;

		[Token(Token = "0x400079F")]
		[FieldOffset(Offset = "0x0")]
		private readonly SynchronizationContext synchronizationContext;

		[Token(Token = "0x1700012A")]
		public bool IsCompleted
		{
			[Token(Token = "0x6000816")]
			[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x6000815")]
		[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
		public Awaiter(SynchronizationContext synchronizationContext)
		{
		}

		[Token(Token = "0x6000817")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public void GetResult()
		{
		}

		[Token(Token = "0x6000818")]
		[Address(RVA = "0x519720", Offset = "0x518120", VA = "0x180519720", Slot = "5")]
		public void OnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x6000819")]
		[Address(RVA = "0x5197B0", Offset = "0x5181B0", VA = "0x1805197B0", Slot = "4")]
		public void UnsafeOnCompleted(Action continuation)
		{
		}

		[Token(Token = "0x600081A")]
		[Address(RVA = "0x519840", Offset = "0x518240", VA = "0x180519840")]
		private static void Callback(object state)
		{
		}
	}

	[Token(Token = "0x400079D")]
	[FieldOffset(Offset = "0x0")]
	private readonly SynchronizationContext synchronizationContext;

	[Token(Token = "0x6000813")]
	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
	public SwitchToSynchronizationContextAwaitable(SynchronizationContext synchronizationContext)
	{
	}

	[Token(Token = "0x6000814")]
	[Address(RVA = "0x513AA0", Offset = "0x5124A0", VA = "0x180513AA0")]
	public Awaiter GetAwaiter()
	{
		return default(Awaiter);
	}
}
