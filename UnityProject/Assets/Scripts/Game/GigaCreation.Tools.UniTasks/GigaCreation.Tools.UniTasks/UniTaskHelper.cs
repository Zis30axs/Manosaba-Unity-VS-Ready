using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;

namespace GigaCreation.Tools.UniTasks
{
    [Token(Token = "0x2000008")]
    public static class UniTaskHelper
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CSkippableDelay_003Ed__0
    {
    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public int millisecondsDelay;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Func<bool> condToSkip;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool ignoreTimeScale;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public PlayerLoopTiming delayTiming;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xD5DFF0", Offset = "0xD5C9F0", VA = "0x180D5DFF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000A")]
    	[CompilerGenerated]
    	private struct _003CSkippableDelay_003Ed__1
    {
    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public TimeSpan delayTimeSpan;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool ignoreTimeScale;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public PlayerLoopTiming delayTiming;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public Func<bool> condToSkip;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003Ccts_003E5__2;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private CancellationTokenSource _003ClinkedCts_003E5__3;

    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<int>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000010")]
    		[Address(RVA = "0xD5E4E0", Offset = "0xD5CEE0", VA = "0x180D5E4E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000011")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD5DC20", Offset = "0xD5C620", VA = "0x180D5DC20")]
    	[AsyncStateMachine(typeof(_003CSkippableDelay_003Ed__0))]
    	public static UniTask SkippableDelay(int millisecondsDelay, Func<bool> condToSkip, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xD5DE00", Offset = "0xD5C800", VA = "0x180D5DE00")]
    	[AsyncStateMachine(typeof(_003CSkippableDelay_003Ed__1))]
    	public static UniTask SkippableDelay(TimeSpan delayTimeSpan, Func<bool> condToSkip, bool ignoreTimeScale = false, PlayerLoopTiming delayTiming = PlayerLoopTiming.Update, CancellationToken ct = default)
    	{
    		return default(UniTask);
    	}
    }
}
