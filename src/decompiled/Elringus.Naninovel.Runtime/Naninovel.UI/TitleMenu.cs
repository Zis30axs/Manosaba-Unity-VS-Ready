using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x20005CB")]
[RequireComponent(typeof(CanvasGroup))]
public class TitleMenu : CustomUI, ITitleUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20005CC")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__4
{
		[Token(Token = "0x4001634")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001635")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001636")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool visible;

		[Token(Token = "0x4001637")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TitleMenu _003C_003E4__this;

		[Token(Token = "0x4001638")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x4001639")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float? duration;

		[Token(Token = "0x400163A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private InteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x400163B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001D03")]
		[Address(RVA = "0x4AB620", Offset = "0x4AA020", VA = "0x1804AB620", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D04")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005CD")]
	[CompilerGenerated]
	private struct _003CPlayTitleScript_003Ed__5
{
		[Token(Token = "0x400163C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400163D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400163E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TitleMenu _003C_003E4__this;

		[Token(Token = "0x400163F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001640")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001641")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001D05")]
		[Address(RVA = "0x4ABE10", Offset = "0x4AA810", VA = "0x1804ABE10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D06")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001631")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private IScriptPlayer player;

	[Token(Token = "0x4001632")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private IScriptManager scripts;

	[Token(Token = "0x4001633")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private string titleScriptPath;

	[Token(Token = "0x6001CFE")]
	[Address(RVA = "0x4AB100", Offset = "0x4A9B00", VA = "0x1804AB100", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001CFF")]
	[Address(RVA = "0x4AB2D0", Offset = "0x4A9CD0", VA = "0x1804AB2D0", Slot = "44")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__4))]
	public override UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001D00")]
	[Address(RVA = "0x4AB490", Offset = "0x4A9E90", VA = "0x1804AB490", Slot = "95")]
	[AsyncStateMachine(typeof(_003CPlayTitleScript_003Ed__5))]
	protected virtual UniTask PlayTitleScript(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001D01")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public TitleMenu()
	{
	}
}
