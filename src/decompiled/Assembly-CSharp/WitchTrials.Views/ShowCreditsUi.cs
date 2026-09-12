using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x2000042")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("credit")]
public class ShowCreditsUi : Command, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x2000043")]
	[CompilerGenerated]
	private struct _003CPlayAsync_003Ed__11
{
		[Token(Token = "0x40000C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowCreditsUi _003C_003E4__this;

		[Token(Token = "0x40000C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000E1")]
		[Address(RVA = "0x31CF50", Offset = "0x31B950", VA = "0x18031CF50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000E2")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000044")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__8
{
		[Token(Token = "0x40000C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowCreditsUi _003C_003E4__this;

		[Token(Token = "0x40000C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000E3")]
		[Address(RVA = "0x31D290", Offset = "0x31BC90", VA = "0x18031D290", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000E4")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000BC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[RequiredParameter]
	[ParameterAlias("")]
	public IntegerParameter ActNumber;

	[Token(Token = "0x40000BD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Wait;

	[Token(Token = "0x40000BE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x40000BF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ICreditsUI _creditsUi;

	[Token(Token = "0x1700000E")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x60000DA")]
		[Address(RVA = "0x31C970", Offset = "0x31B370", VA = "0x18031C970")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700000F")]
	private ICreditsUI CreditsUi
	{
		[Token(Token = "0x60000DB")]
		[Address(RVA = "0x31CA30", Offset = "0x31B430", VA = "0x18031CA30")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000DC")]
	[Address(RVA = "0x31CBA0", Offset = "0x31B5A0", VA = "0x18031CBA0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__8))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000DD")]
	[Address(RVA = "0x31CCC0", Offset = "0x31B6C0", VA = "0x18031CCC0", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x60000DE")]
	[Address(RVA = "0x31CD20", Offset = "0x31B720", VA = "0x18031CD20", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000DF")]
	[Address(RVA = "0x31CDD0", Offset = "0x31B7D0", VA = "0x18031CDD0")]
	[AsyncStateMachine(typeof(_003CPlayAsync_003Ed__11))]
	private UniTask PlayAsync(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000E0")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ShowCreditsUi()
	{
	}
}
