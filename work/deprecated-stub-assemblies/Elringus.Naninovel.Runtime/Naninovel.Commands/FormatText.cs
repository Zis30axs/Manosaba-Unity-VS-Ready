using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006E0")]
    [CommandAlias("format")]
    public class FormatText : PrinterCommand
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006E1")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__4
    {
    		[Token(Token = "0x4001A41")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001A42")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001A43")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public FormatText _003C_003E4__this;

    		[Token(Token = "0x4001A44")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001A45")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60020FC")]
    		[Address(RVA = "0x4FA3D0", Offset = "0x4F8DD0", VA = "0x1804FA3D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60020FD")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001A3F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ActorContext("Characters", 1, null)]
    	public NamedStringListParameter Templates;

    	[Token(Token = "0x4001A40")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterAlias("printer")]
    	[ActorContext("TextPrinters", -1, null)]
    	public StringParameter PrinterId;

    	[Token(Token = "0x170004D2")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x60020F6")]
    		[Address(RVA = "0x4F89A0", Offset = "0x4F73A0", VA = "0x1804F89A0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60020F7")]
    	[Address(RVA = "0x4F9C00", Offset = "0x4F8600", VA = "0x1804F9C00", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__4))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020F8")]
    	[Address(RVA = "0x4F9D80", Offset = "0x4F8780", VA = "0x1804F9D80", Slot = "21")]
    	protected virtual bool ShouldResetTemplates()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60020F9")]
    	[Address(RVA = "0x4F9F10", Offset = "0x4F8910", VA = "0x1804F9F10")]
    	protected void ResetTemplates(ITextPrinterActor printer)
    	{
    	}

    	[Token(Token = "0x60020FA")]
    	[Address(RVA = "0x4F9F80", Offset = "0x4F8980", VA = "0x1804F9F80")]
    	protected void AssignTemplates(ITextPrinterActor printer)
    	{
    	}

    	[Token(Token = "0x60020FB")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public FormatText()
    	{
    	}
    }
}
