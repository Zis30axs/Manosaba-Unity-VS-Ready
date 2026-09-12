using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006D7")]
[CommandAlias("i")]
public class WaitForInput : Command
{
	[StructLayout(3)]
	[Token(Token = "0x20006D8")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__0
{
		[Token(Token = "0x4001A19")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001A1A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001A1B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public WaitForInput _003C_003E4__this;

		[Token(Token = "0x4001A1C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001A1D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60020DA")]
		[Address(RVA = "0x4F6C00", Offset = "0x4F5600", VA = "0x1804F6C00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60020DB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x60020D8")]
	[Address(RVA = "0x4F6A80", Offset = "0x4F5480", VA = "0x1804F6A80", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__0))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020D9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public WaitForInput()
	{
	}
}
