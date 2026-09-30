using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006CD")]
    public class Return : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006CE")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__3
    {
    		[Token(Token = "0x40019F5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40019F6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40019F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Return _003C_003E4__this;

    		[Token(Token = "0x40019F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020C3")]
    		[Address(RVA = "0x4F4250", Offset = "0x4F2C50", VA = "0x1804F4250", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020C4")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20006CF")]
    	[CompilerGenerated]
    	private struct _003CReset_003Ed__4
    {
    		[Token(Token = "0x40019F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40019FA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40019FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Return _003C_003E4__this;

    		[Token(Token = "0x40019FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020C5")]
    		[Address(RVA = "0x4F45E0", Offset = "0x4F2FE0", VA = "0x1804F45E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020C6")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40019F4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("reset")]
    	public StringListParameter ResetState;

    	[Token(Token = "0x170004CC")]
    	protected IScriptPlayer Player
    	{
    		[Token(Token = "0x60020BE")]
    		[Address(RVA = "0x4F3C20", Offset = "0x4F2620", VA = "0x1804F3C20")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60020BF")]
    	[Address(RVA = "0x4F3C80", Offset = "0x4F2680", VA = "0x1804F3C80", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020C0")]
    	[Address(RVA = "0x4F3DA0", Offset = "0x4F27A0", VA = "0x1804F3DA0", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CReset_003Ed__4))]
    	protected virtual UniTask Reset()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020C1")]
    	[Address(RVA = "0x4F3EC0", Offset = "0x4F28C0", VA = "0x1804F3EC0", Slot = "17")]
    	protected virtual void Navigate()
    	{
    	}

    	[Token(Token = "0x60020C2")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Return()
    	{
    	}
    }
}
