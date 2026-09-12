using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006E4")]
[CommandAlias("printer")]
[ActorContext("TextPrinters", -1, "Id")]
public class ModifyTextPrinter : ModifyOrthoActor<ITextPrinterActor, TextPrinterState, TextPrinterMetadata, TextPrintersConfiguration, ITextPrinterManager>
{
	[StructLayout(3)]
	[Token(Token = "0x20006E5")]
	[CompilerGenerated]
	private struct _003CModify_003Ed__9
{
		[Token(Token = "0x4001A52")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A53")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A54")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyTextPrinter _003C_003E4__this;

		[Token(Token = "0x4001A55")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001A56")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600210A")]
		[Address(RVA = "0x4FB5D0", Offset = "0x4F9FD0", VA = "0x1804FB5D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600210B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001A4F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	[ParameterAlias("")]
	[ActorContext("TextPrinters", 0, null)]
	[AppearanceContext(1, null, null)]
	public NamedStringParameter IdAndAppearance;

	[Token(Token = "0x4001A50")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	[ParameterAlias("default")]
	[ParameterDefaultValue("true")]
	public BooleanParameter MakeDefault;

	[Token(Token = "0x4001A51")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	[ParameterDefaultValue("true")]
	public BooleanParameter HideOther;

	[Token(Token = "0x170004D4")]
	protected override bool AllowPreload
	{
		[Token(Token = "0x6002104")]
		[Address(RVA = "0x4FB090", Offset = "0x4F9A90", VA = "0x1804FB090", Slot = "30")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170004D5")]
	protected override string AssignedId
	{
		[Token(Token = "0x6002105")]
		[Address(RVA = "0x4FB140", Offset = "0x4F9B40", VA = "0x1804FB140", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004D6")]
	protected override string AlternativeAppearance
	{
		[Token(Token = "0x6002106")]
		[Address(RVA = "0x4FB200", Offset = "0x4F9C00", VA = "0x1804FB200", Slot = "29")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6002107")]
	[Address(RVA = "0x4FB2D0", Offset = "0x4F9CD0", VA = "0x1804FB2D0", Slot = "34")]
	[AsyncStateMachine(typeof(_003CModify_003Ed__9))]
	protected override UniTask Modify(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002108")]
	[Address(RVA = "0x4FB450", Offset = "0x4F9E50", VA = "0x1804FB450")]
	public ModifyTextPrinter()
	{
	}
}
