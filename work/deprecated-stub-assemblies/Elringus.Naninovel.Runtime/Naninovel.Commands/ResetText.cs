using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006F6")]
    public class ResetText : PrinterCommand
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006F7")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__3
    {
    		[Token(Token = "0x4001AB0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AB1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AB2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ResetText _003C_003E4__this;

    		[Token(Token = "0x4001AB3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AB4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6002152")]
    		[Address(RVA = "0x5038E0", Offset = "0x5022E0", VA = "0x1805038E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002153")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001AAF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x170004E4")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x600214F")]
    		[Address(RVA = "0x4FA6E0", Offset = "0x4F90E0", VA = "0x1804FA6E0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6002150")]
    	[Address(RVA = "0x503760", Offset = "0x502160", VA = "0x180503760", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002151")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ResetText()
    	{
    	}
    }
}
