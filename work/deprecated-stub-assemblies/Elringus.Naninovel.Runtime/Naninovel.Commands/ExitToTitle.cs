using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006A7")]
    [CommandAlias("title")]
    [Branch(BranchTraits.Endpoint, null, "{$TitleScript}")]
    public class ExitToTitle : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006A8")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__0
    {
    		[Token(Token = "0x400197B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400197C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400197D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		private IStateManager _003CgameState_003E5__2;

    		[Token(Token = "0x400197E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private IUIManager _003CuiManager_003E5__3;

    		[Token(Token = "0x400197F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IDisposable _003C_003E7__wrap3;

    		[Token(Token = "0x4001980")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<IDisposable>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001981")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600204F")]
    		[Address(RVA = "0x4E7F10", Offset = "0x4E6910", VA = "0x1804E7F10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002050")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x600204D")]
    	[Address(RVA = "0x4E7E50", Offset = "0x4E6850", VA = "0x1804E7E50", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__0))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600204E")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ExitToTitle()
    	{
    	}
    }
}
