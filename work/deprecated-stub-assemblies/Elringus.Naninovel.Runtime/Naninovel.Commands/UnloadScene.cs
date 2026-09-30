using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000722")]
    public class UnloadScene : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000723")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__1
    {
    		[Token(Token = "0x4001B40")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001B41")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B42")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public UnloadScene _003C_003E4__this;

    		[Token(Token = "0x4001B43")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UnityAsyncExtensions.AsyncOperationAwaiter _003C_003Eu__1;

    		[Token(Token = "0x60021EC")]
    		[Address(RVA = "0x50F630", Offset = "0x50E030", VA = "0x18050F630", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60021ED")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001B3F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public StringParameter SceneName;

    	[Token(Token = "0x60021EA")]
    	[Address(RVA = "0x50F510", Offset = "0x50DF10", VA = "0x18050F510", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60021EB")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public UnloadScene()
    	{
    	}
    }
}
