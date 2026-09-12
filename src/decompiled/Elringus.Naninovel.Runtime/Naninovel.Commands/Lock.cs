using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x20006B1")]
public class Lock : Command
{
	[StructLayout(3)]
	[Token(Token = "0x20006B2")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__1
{
		[Token(Token = "0x400199C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400199D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400199E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Lock _003C_003E4__this;

		[Token(Token = "0x400199F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002066")]
		[Address(RVA = "0x4EA850", Offset = "0x4E9250", VA = "0x1804EA850", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002067")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400199B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ResourceContext("Unlockables", -1, null)]
	public StringParameter Id;

	[Token(Token = "0x6002064")]
	[Address(RVA = "0x4EA730", Offset = "0x4E9130", VA = "0x1804EA730", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002065")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Lock()
	{
	}
}
