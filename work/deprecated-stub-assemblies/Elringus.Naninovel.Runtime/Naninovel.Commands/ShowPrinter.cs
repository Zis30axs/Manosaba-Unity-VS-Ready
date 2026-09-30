using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006F8")]
    public class ShowPrinter : PrinterCommand
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006F9")]
    	[CompilerGenerated]
    	private struct _003CHide_003Ed__6
    {
    		[Token(Token = "0x4001AB8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AB9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001ABA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShowPrinter _003C_003E4__this;

    		[Token(Token = "0x4001ABB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001ABC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001ABD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6002158")]
    		[Address(RVA = "0x503DE0", Offset = "0x5027E0", VA = "0x180503DE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002159")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001AB5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x4001AB6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("time")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001AB7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004E5")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x6002154")]
    		[Address(RVA = "0x4FA6E0", Offset = "0x4F90E0", VA = "0x1804FA6E0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6002155")]
    	[Address(RVA = "0x503BA0", Offset = "0x5025A0", VA = "0x180503BA0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002156")]
    	[Address(RVA = "0x503C50", Offset = "0x502650", VA = "0x180503C50", Slot = "21")]
    	[AsyncStateMachine(typeof(_003CHide_003Ed__6))]
    	protected virtual UniTask Hide(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002157")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ShowPrinter()
    	{
    	}
    }
}
