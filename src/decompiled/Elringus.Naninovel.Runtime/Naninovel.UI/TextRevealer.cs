using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.UI;

[Token(Token = "0x20005B7")]
public class TextRevealer
{
	[StructLayout(3)]
	[Token(Token = "0x20005B8")]
	[CompilerGenerated]
	private struct _003CReveal_003Ed__3
{
		[Token(Token = "0x40015EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40015EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x40015ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TextRevealer _003C_003E4__this;

		[Token(Token = "0x40015EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float delay;

		[Token(Token = "0x40015EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x40015F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private float _003ClastRevealTime_003E5__2;

		[Token(Token = "0x40015F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CA3")]
		[Address(RVA = "0x4A31A0", Offset = "0x4A1BA0", VA = "0x1804A31A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CA4")]
		[Address(RVA = "0x4A3780", Offset = "0x4A2180", VA = "0x1804A3780", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40015E9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly IRevealableText text;

	[Token(Token = "0x40015EA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly Action<char, AsyncToken> handleCharRevealed;

	[Token(Token = "0x6001CA1")]
	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
	public TextRevealer(IRevealableText text, [Optional] Action<char, AsyncToken> handleCharRevealed)
	{
	}

	[Token(Token = "0x6001CA2")]
	[Address(RVA = "0x4A2F80", Offset = "0x4A1980", VA = "0x1804A2F80", Slot = "4")]
	[AsyncStateMachine(typeof(_003CReveal_003Ed__3))]
	public virtual UniTask<bool> Reveal(float delay, AsyncToken token)
	{
		return default(UniTask<bool>);
	}
}
