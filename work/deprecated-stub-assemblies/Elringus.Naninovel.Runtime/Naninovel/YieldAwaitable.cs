using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20001C4")]
    public readonly struct YieldAwaitable
    {
    	[Token(Token = "0x20001C5")]
    	public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
    	{
    		[Token(Token = "0x4000794")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private readonly PlayerLoopTiming timing;

    		[Token(Token = "0x4000795")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		private readonly AsyncToken token;

    		[Token(Token = "0x17000126")]
    		public bool IsCompleted
    		{
    			[Token(Token = "0x60007FA")]
    			[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x60007FB")]
    		[Address(RVA = "0x5189C0", Offset = "0x5173C0", VA = "0x1805189C0")]
    		public Awaiter(PlayerLoopTiming timing, [Optional] AsyncToken token)
    		{ this = default; }

    		[Token(Token = "0x60007FC")]
    		[Address(RVA = "0x518A90", Offset = "0x517490", VA = "0x180518A90")]
    		public void GetResult()
    		{
    		}

    		[Token(Token = "0x60007FD")]
    		[Address(RVA = "0x518AA0", Offset = "0x5174A0", VA = "0x180518AA0", Slot = "5")]
    		public void OnCompleted(Action continuation)
    		{
    		}

    		[Token(Token = "0x60007FE")]
    		[Address(RVA = "0x518B10", Offset = "0x517510", VA = "0x180518B10", Slot = "4")]
    		public void UnsafeOnCompleted(Action continuation)
    		{
    		}
    	}

    	[Token(Token = "0x4000792")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly PlayerLoopTiming timing;

    	[Token(Token = "0x4000793")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private readonly AsyncToken token;

    	[Token(Token = "0x60007F8")]
    	[Address(RVA = "0x5189C0", Offset = "0x5173C0", VA = "0x1805189C0")]
    	public YieldAwaitable(PlayerLoopTiming timing, [Optional] AsyncToken token)
    	{ this = default; }

    	[Token(Token = "0x60007F9")]
    	[Address(RVA = "0x518A20", Offset = "0x517420", VA = "0x180518A20")]
    	public Awaiter GetAwaiter()
    	{
    		return default(Awaiter);
    	}
    }
}
