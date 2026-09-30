using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006D2")]
    public class Wait : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006D3")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__2
    {
    		[Token(Token = "0x4001A00")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A01")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A02")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A03")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Wait _003C_003E4__this;

    		[Token(Token = "0x4001A04")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A05")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60020D0")]
    		[Address(RVA = "0x4F52E0", Offset = "0x4F3CE0", VA = "0x1804F52E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020D1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006D4")]
    	[CompilerGenerated]
    	private struct _003CWaitForInput_003Ed__3
    {
    		[Token(Token = "0x4001A06")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A07")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A08")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A09")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IScriptPlayer _003Cplayer_003E5__2;

    		[Token(Token = "0x4001A0A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020D2")]
    		[Address(RVA = "0x4F5BB0", Offset = "0x4F45B0", VA = "0x1804F5BB0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020D3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006D5")]
    	[CompilerGenerated]
    	private struct _003CWaitForInputOrTimer_003Ed__4
    {
    		[Token(Token = "0x4001A0B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A0C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A0D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Wait _003C_003E4__this;

    		[Token(Token = "0x4001A0E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A0F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float waitTime;

    		[Token(Token = "0x4001A10")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x4001A11")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020D4")]
    		[Address(RVA = "0x4F6030", Offset = "0x4F4A30", VA = "0x1804F6030", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020D5")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006D6")]
    	[CompilerGenerated]
    	private struct _003CWaitForTimer_003Ed__5
    {
    		[Token(Token = "0x4001A12")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A13")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A14")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A15")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float waitTime;

    		[Token(Token = "0x4001A16")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IScriptPlayer _003Cplayer_003E5__2;

    		[Token(Token = "0x4001A17")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CstartTime_003E5__3;

    		[Token(Token = "0x4001A18")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020D6")]
    		[Address(RVA = "0x4F6570", Offset = "0x4F4F70", VA = "0x1804F6570", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020D7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40019FE")]
    	public const string InputLiteral = "i";

    	[Token(Token = "0x40019FF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public StringParameter WaitMode;

    	[Token(Token = "0x60020CB")]
    	[Address(RVA = "0x4F4D10", Offset = "0x4F3710", VA = "0x1804F4D10", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020CC")]
    	[Address(RVA = "0x4F4EA0", Offset = "0x4F38A0", VA = "0x1804F4EA0", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CWaitForInput_003Ed__3))]
    	protected virtual UniTask WaitForInput(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020CD")]
    	[Address(RVA = "0x4F4FD0", Offset = "0x4F39D0", VA = "0x1804F4FD0", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CWaitForInputOrTimer_003Ed__4))]
    	protected virtual UniTask WaitForInputOrTimer(float waitTime, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020CE")]
    	[Address(RVA = "0x4F5180", Offset = "0x4F3B80", VA = "0x1804F5180", Slot = "18")]
    	[AsyncStateMachine(typeof(_003CWaitForTimer_003Ed__5))]
    	protected virtual UniTask WaitForTimer(float waitTime, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020CF")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Wait()
    	{
    	}
    }
}
