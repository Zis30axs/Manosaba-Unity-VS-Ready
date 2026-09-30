using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006E2")]
    public class HidePrinter : PrinterCommand
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006E3")]
    	[CompilerGenerated]
    	private struct _003CHide_003Ed__6
    {
    		[Token(Token = "0x4001A49")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A4A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A4B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public HidePrinter _003C_003E4__this;

    		[Token(Token = "0x4001A4C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A4D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001A4E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6002102")]
    		[Address(RVA = "0x4FA9B0", Offset = "0x4F93B0", VA = "0x1804FA9B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6002103")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001A46")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x4001A47")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("time")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4001A48")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004D3")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x60020FE")]
    		[Address(RVA = "0x4FA6E0", Offset = "0x4F90E0", VA = "0x1804FA6E0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60020FF")]
    	[Address(RVA = "0x4FA770", Offset = "0x4F9170", VA = "0x1804FA770", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002100")]
    	[Address(RVA = "0x4FA820", Offset = "0x4F9220", VA = "0x1804FA820", Slot = "21")]
    	[AsyncStateMachine(typeof(_003CHide_003Ed__6))]
    	protected virtual UniTask Hide(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002101")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public HidePrinter()
    	{
    	}
    }
}
