using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000724")]
    public class Unlock : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000725")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__1
    {
    		[Token(Token = "0x4001B45")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B46")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B47")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Unlock _003C_003E4__this;

    		[Token(Token = "0x4001B48")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60021F0")]
    		[Address(RVA = "0x50FAE0", Offset = "0x50E4E0", VA = "0x18050FAE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021F1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001B44")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ResourceContext("Unlockables", -1, null)]
    	public StringParameter Id;

    	[Token(Token = "0x60021EE")]
    	[Address(RVA = "0x50F9C0", Offset = "0x50E3C0", VA = "0x18050F9C0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021EF")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Unlock()
    	{
    	}
    }
}
