using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006DD")]
[CommandAlias("append")]
public class AppendText : PrinterCommand, Command.IPreloadable, Command.ILocalizable
{
	[StructLayout(3)]
	[Token(Token = "0x20006DE")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__11
{
		[Token(Token = "0x4001A34")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A35")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A36")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AppendText _003C_003E4__this;

		[Token(Token = "0x4001A37")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001A38")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IDisposable _003C__003E5__2;

		[Token(Token = "0x4001A39")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IDisposable>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001A3A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60020F2")]
		[Address(RVA = "0x4F8EB0", Offset = "0x4F78B0", VA = "0x1804F8EB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020F3")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006DF")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__9
{
		[Token(Token = "0x4001A3B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A3C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A3D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AppendText _003C_003E4__this;

		[Token(Token = "0x4001A3E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60020F4")]
		[Address(RVA = "0x4F9700", Offset = "0x4F8100", VA = "0x1804F9700", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020F5")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001A31")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	public LocalizableTextParameter Text;

	[Token(Token = "0x4001A32")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("printer")]
	[ActorContext("TextPrinters", -1, null)]
	public StringParameter PrinterId;

	[Token(Token = "0x4001A33")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterAlias("author")]
	[ActorContext("Characters", -1, null)]
	public StringParameter AuthorId;

	[Token(Token = "0x170004CF")]
	protected override string AssignedPrinterId
	{
		[Token(Token = "0x60020EA")]
		[Address(RVA = "0x4F89A0", Offset = "0x4F73A0", VA = "0x1804F89A0", Slot = "16")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004D0")]
	protected override string AssignedAuthorId
	{
		[Token(Token = "0x60020EB")]
		[Address(RVA = "0x4F8A30", Offset = "0x4F7430", VA = "0x1804F8A30", Slot = "17")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004D1")]
	protected IUIManager UIManager
	{
		[Token(Token = "0x60020EC")]
		[Address(RVA = "0x4F8AC0", Offset = "0x4F74C0", VA = "0x1804F8AC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60020ED")]
	[Address(RVA = "0x4F8B20", Offset = "0x4F7520", VA = "0x1804F8B20", Slot = "18")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__9))]
	public override UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020EE")]
	[Address(RVA = "0x4F8C40", Offset = "0x4F7640", VA = "0x1804F8C40", Slot = "19")]
	public override void ReleaseResources()
	{
	}

	[Token(Token = "0x60020EF")]
	[Address(RVA = "0x4F8D00", Offset = "0x4F7700", VA = "0x1804F8D00", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__11))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020F0")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public AppendText()
	{
	}
}
