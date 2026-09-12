using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006AB")]
[CommandAlias("input")]
public class InputCustomVariable : Command, Command.IPreloadable, Command.ILocalizable
{
	[StructLayout(3)]
	[Token(Token = "0x20006AC")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__7
{
		[Token(Token = "0x4001990")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001991")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001992")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public InputCustomVariable _003C_003E4__this;

		[Token(Token = "0x4001993")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<System.IDisposable>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600205B")]
		[Address(RVA = "0x4E9AB0", Offset = "0x4E84B0", VA = "0x1804E9AB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600205C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400198B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter VariableName;

	[Token(Token = "0x400198C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("type")]
	[ConstantContext(typeof(CustomVariableValueType), -1, null)]
	public StringParameter VariableType;

	[Token(Token = "0x400198D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public LocalizableTextParameter Summary;

	[Token(Token = "0x400198E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[ParameterAlias("value")]
	public LocalizableTextParameter PredefinedValue;

	[Token(Token = "0x400198F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("play")]
	[ParameterDefaultValue("true")]
	public BooleanParameter PlayOnSubmit;

	[Token(Token = "0x6002056")]
	[Address(RVA = "0x4E9480", Offset = "0x4E7E80", VA = "0x1804E9480", Slot = "16")]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002057")]
	[Address(RVA = "0x4E9580", Offset = "0x4E7F80", VA = "0x1804E9580", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x6002058")]
	[Address(RVA = "0x4E9680", Offset = "0x4E8080", VA = "0x1804E9680", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__7))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002059")]
	[Address(RVA = "0x4E97A0", Offset = "0x4E81A0", VA = "0x1804E97A0", Slot = "18")]
	protected virtual CustomVariableValueType ResolveType()
	{
		return default(CustomVariableValueType);
	}

	[Token(Token = "0x600205A")]
	[Address(RVA = "0x4E9A40", Offset = "0x4E8440", VA = "0x1804E9A40")]
	public InputCustomVariable()
	{
	}
}
