using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.UI;

[Token(Token = "0x20005C7")]
public class TitleExitButton : ScriptableButton
{
	[StructLayout(3)]
	[Token(Token = "0x20005C8")]
	[CompilerGenerated]
	private struct _003COnButtonClick_003Ed__4
{
		[Token(Token = "0x4001625")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001626")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001627")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public TitleExitButton _003C_003E4__this;

		[Token(Token = "0x4001628")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private InteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x4001629")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CF6")]
		[Address(RVA = "0x4A9D10", Offset = "0x4A8710", VA = "0x1804A9D10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CF7")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005C9")]
	[CompilerGenerated]
	private struct _003CPlayTitleExit_003Ed__5
{
		[Token(Token = "0x400162A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400162B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400162C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TitleExitButton _003C_003E4__this;

		[Token(Token = "0x400162D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private string _003CscriptPath_003E5__2;

		[Token(Token = "0x400162E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<Resource<Script>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400162F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001CF8")]
		[Address(RVA = "0x4AA5C0", Offset = "0x4A8FC0", VA = "0x1804AA5C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CF9")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001622")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private IScriptPlayer player;

	[Token(Token = "0x4001623")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private IScriptManager scripts;

	[Token(Token = "0x4001624")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private IStateManager state;

	[Token(Token = "0x6001CF1")]
	[Address(RVA = "0x4A9890", Offset = "0x4A8290", VA = "0x1804A9890", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001CF2")]
	[Address(RVA = "0x4A9A50", Offset = "0x4A8450", VA = "0x1804A9A50", Slot = "62")]
	[AsyncStateMachine(typeof(_003COnButtonClick_003Ed__4))]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001CF3")]
	[Address(RVA = "0x4A9B80", Offset = "0x4A8580", VA = "0x1804A9B80", Slot = "63")]
	[AsyncStateMachine(typeof(_003CPlayTitleExit_003Ed__5))]
	protected virtual UniTask PlayTitleExit()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CF4")]
	[Address(RVA = "0x3368F0", Offset = "0x3352F0", VA = "0x1803368F0")]
	public TitleExitButton()
	{
	}
}
