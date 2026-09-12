using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000534")]
public class ControlPanelQuickLoadButton : ScriptableButton
{
	[StructLayout(3)]
	[Token(Token = "0x2000535")]
	[CompilerGenerated]
	private struct _003CQuickLoad_003Ed__6
{
		[Token(Token = "0x4001413")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001414")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001415")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ControlPanelQuickLoadButton _003C_003E4__this;

		[Token(Token = "0x4001416")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IDisposable _003C_003E7__wrap1;

		[Token(Token = "0x4001417")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IDisposable>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001418")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<GameStateMap>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60019DA")]
		[Address(RVA = "0x61FEE0", Offset = "0x61E8E0", VA = "0x18061FEE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60019DB")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001412")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private IStateManager gameState;

	[Token(Token = "0x60019D2")]
	[Address(RVA = "0x61F5A0", Offset = "0x61DFA0", VA = "0x18061F5A0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60019D3")]
	[Address(RVA = "0x61F660", Offset = "0x61E060", VA = "0x18061F660", Slot = "6")]
	protected override void Start()
	{
	}

	[Token(Token = "0x60019D4")]
	[Address(RVA = "0x61F780", Offset = "0x61E180", VA = "0x18061F780", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x60019D5")]
	[Address(RVA = "0x61F9D0", Offset = "0x61E3D0", VA = "0x18061F9D0", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60019D6")]
	[Address(RVA = "0x61FC20", Offset = "0x61E620", VA = "0x18061FC20", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x60019D7")]
	[Address(RVA = "0x61FDA0", Offset = "0x61E7A0", VA = "0x18061FDA0")]
	[AsyncStateMachine(typeof(_003CQuickLoad_003Ed__6))]
	private void QuickLoad()
	{
	}

	[Token(Token = "0x60019D8")]
	[Address(RVA = "0x61F660", Offset = "0x61E060", VA = "0x18061F660")]
	private void ControlInteractability(string _)
	{
	}

	[Token(Token = "0x60019D9")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public ControlPanelQuickLoadButton()
	{
	}
}
