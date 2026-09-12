using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000536")]
public class ControlPanelQuickSaveButton : ScriptableButton
{
	[StructLayout(3)]
	[Token(Token = "0x2000537")]
	[CompilerGenerated]
	private struct _003CQuickSave_003Ed__3
{
		[Token(Token = "0x400141A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400141B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400141C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ControlPanelQuickSaveButton _003C_003E4__this;

		[Token(Token = "0x400141D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60019E0")]
		[Address(RVA = "0x620730", Offset = "0x61F130", VA = "0x180620730", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60019E1")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001419")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private IStateManager gameState;

	[Token(Token = "0x60019DC")]
	[Address(RVA = "0x620540", Offset = "0x61EF40", VA = "0x180620540", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60019DD")]
	[Address(RVA = "0x620600", Offset = "0x61F000", VA = "0x180620600", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x60019DE")]
	[Address(RVA = "0x620600", Offset = "0x61F000", VA = "0x180620600")]
	[AsyncStateMachine(typeof(_003CQuickSave_003Ed__3))]
	private void QuickSave()
	{
	}

	[Token(Token = "0x60019DF")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public ControlPanelQuickSaveButton()
	{
	}
}
