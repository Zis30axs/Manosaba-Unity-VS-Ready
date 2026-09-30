using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x200053C")]
    public class ControlPanelTitleButton : ScriptableButton
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200053D")]
    	[CompilerGenerated]
    	private struct _003CExitToTitle_003Ed__5
    {
    		[Token(Token = "0x4001426")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001427")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001428")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ControlPanelTitleButton _003C_003E4__this;

    		[Token(Token = "0x4001429")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400142A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private IDisposable _003C_003E7__wrap1;

    		[Token(Token = "0x400142B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<IDisposable>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x400142C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x60019F8")]
    		[Address(RVA = "0x621A40", Offset = "0x620440", VA = "0x180621A40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60019F9")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001423")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[ManagedText("DefaultUI")]
    	protected static string ConfirmationMessage;

    	[Token(Token = "0x4001424")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	private IStateManager state;

    	[Token(Token = "0x4001425")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	private IUIManager ui;

    	[Token(Token = "0x60019F3")]
    	[Address(RVA = "0x621510", Offset = "0x61FF10", VA = "0x180621510", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60019F4")]
    	[Address(RVA = "0x621660", Offset = "0x620060", VA = "0x180621660", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60019F5")]
    	[Address(RVA = "0x621850", Offset = "0x620250", VA = "0x180621850")]
    	[AsyncStateMachine(typeof(_003CExitToTitle_003Ed__5))]
    	private void ExitToTitle()
    	{
    	}

    	[Token(Token = "0x60019F6")]
    	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
    	public ControlPanelTitleButton()
    	{
    	}
    }
}
