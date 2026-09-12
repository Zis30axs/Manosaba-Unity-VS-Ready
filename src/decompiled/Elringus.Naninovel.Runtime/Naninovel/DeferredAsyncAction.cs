using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002DB")]
public readonly struct DeferredAsyncAction : IAsyncDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x20002DC")]
	[CompilerGenerated]
	private struct _003CDisposeAsync_003Ed__2
{
		[Token(Token = "0x4000B30")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000B31")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncValueTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000B32")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public DeferredAsyncAction _003C_003E4__this;

		[Token(Token = "0x4000B33")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000DC3")]
		[Address(RVA = "0x5480B0", Offset = "0x546AB0", VA = "0x1805480B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000DC4")]
		[Address(RVA = "0x5483F0", Offset = "0x546DF0", VA = "0x1805483F0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000B2F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Func<UniTask> action;

	[Token(Token = "0x6000DC1")]
	[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
	public DeferredAsyncAction(Func<UniTask> action)
	{
	}

	[Token(Token = "0x6000DC2")]
	[Address(RVA = "0x547F20", Offset = "0x546920", VA = "0x180547F20", Slot = "4")]
	[AsyncStateMachine(typeof(_003CDisposeAsync_003Ed__2))]
	public ValueTask DisposeAsync()
	{
		return default(ValueTask);
	}
}
