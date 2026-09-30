#nullable enable annotations
﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x20002D4")]
    public static class AsyncUtils
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20002D5")]
    	[CompilerGenerated]
    	private struct _003CDelayFrame_003Ed__3
    {
    		[Token(Token = "0x4000B27")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B28")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000B29")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public int frameCount;

    		[Token(Token = "0x4000B2A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000B2B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<int>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000DB0")]
    		[Address(RVA = "0x547510", Offset = "0x545F10", VA = "0x180547510", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000DB1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6000DAC")]
    	[Address(RVA = "0x547140", Offset = "0x545B40", VA = "0x180547140")]
    	public static UniTask.Awaiter GetAwaiter(this UniTask? task)
    	{
    		return default(UniTask.Awaiter);
    	}

    	[Token(Token = "0x6000DAD")]
    	public static UniTask<T>.Awaiter GetAwaiter<T>(this UniTask<T>? task)
    	{
    		return default(UniTask<T>.Awaiter);
    	}

    	[Token(Token = "0x6000DAE")]
    	[Address(RVA = "0x547300", Offset = "0x545D00", VA = "0x180547300")]
    	public static YieldAwaitable WaitEndOfFrame([Optional] AsyncToken token)
    	{
    		return default(YieldAwaitable);
    	}

    	[Token(Token = "0x6000DAF")]
    	[Address(RVA = "0x5473D0", Offset = "0x545DD0", VA = "0x1805473D0")]
    	[AsyncStateMachine(typeof(_003CDelayFrame_003Ed__3))]
    	public static UniTask DelayFrame(int frameCount, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}
    }
}
