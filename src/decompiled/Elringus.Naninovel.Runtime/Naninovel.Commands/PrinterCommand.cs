using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006E8")]
public abstract class PrinterCommand : Command
{
	[StructLayout(3)]
	[Token(Token = "0x20006E9")]
	[CompilerGenerated]
	private struct _003CGetOrAddPrinter_003Ed__12
{
		[Token(Token = "0x4001A61")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A62")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<ITextPrinterActor> _003C_003Et__builder;

		[Token(Token = "0x4001A63")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PrinterCommand _003C_003E4__this;

		[Token(Token = "0x4001A64")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4001A65")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600211B")]
		[Address(RVA = "0x4FC820", Offset = "0x4FB220", VA = "0x1804FC820", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600211C")]
		[Address(RVA = "0x4FCC50", Offset = "0x4FB650", VA = "0x1804FCC50", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x170004D7")]
	protected abstract string AssignedPrinterId
	{
		[Token(Token = "0x6002112")]
		get;
	}

	[Token(Token = "0x170004D8")]
	protected virtual string AssignedAuthorId
	{
		[Token(Token = "0x6002113")]
		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "17")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004D9")]
	protected ITextPrinterManager PrinterManager
	{
		[Token(Token = "0x6002114")]
		[Address(RVA = "0x4FC460", Offset = "0x4FAE60", VA = "0x1804FC460")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004DA")]
	protected ICharacterManager CharacterManager
	{
		[Token(Token = "0x6002115")]
		[Address(RVA = "0x4FC4C0", Offset = "0x4FAEC0", VA = "0x1804FC4C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004DB")]
	protected TextPrintersConfiguration Configuration
	{
		[Token(Token = "0x6002116")]
		[Address(RVA = "0x4FC520", Offset = "0x4FAF20", VA = "0x1804FC520")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6002117")]
	[Address(RVA = "0x4FC5B0", Offset = "0x4FAFB0", VA = "0x1804FC5B0", Slot = "18")]
	public virtual UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002118")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "19")]
	public virtual void ReleaseResources()
	{
	}

	[Token(Token = "0x6002119")]
	[Address(RVA = "0x4FC630", Offset = "0x4FB030", VA = "0x1804FC630", Slot = "20")]
	[AsyncStateMachine(typeof(_003CGetOrAddPrinter_003Ed__12))]
	protected virtual UniTask<ITextPrinterActor> GetOrAddPrinter([Optional] AsyncToken token)
	{
		return default(UniTask<ITextPrinterActor>);
	}

	[Token(Token = "0x600211A")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	protected PrinterCommand()
	{
	}
}
