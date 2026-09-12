using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006FC")]
public class ResetState : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000701")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__2
{
		[Token(Token = "0x4001AC9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001ACA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001ACB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ResetState _003C_003E4__this;

		[Token(Token = "0x4001ACC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600216B")]
		[Address(RVA = "0x5050C0", Offset = "0x503AC0", VA = "0x1805050C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600216C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001AC0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	public StringListParameter Exclude;

	[Token(Token = "0x4001AC1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	public StringListParameter Only;

	[Token(Token = "0x600215E")]
	[Address(RVA = "0x504B30", Offset = "0x503530", VA = "0x180504B30", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600215F")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ResetState()
	{
	}
}
