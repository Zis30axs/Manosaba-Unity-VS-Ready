using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006BE")]
[RequireNested]
[Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
public class Delay : Command, Command.INestedHost
{
	[StructLayout(3)]
	[Token(Token = "0x20006C0")]
	[CompilerGenerated]
	private struct _003CExecuteDelayed_003Ed__5
{
		[Token(Token = "0x40019C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40019CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40019CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AsyncToken token;

		[Token(Token = "0x40019CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ScriptPlaylist delayedList;

		[Token(Token = "0x40019CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600208E")]
		[Address(RVA = "0x4EF340", Offset = "0x4EDD40", VA = "0x1804EF340", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600208F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20006C1")]
	[CompilerGenerated]
	private struct _003CWaitDelay_003Ed__4
{
		[Token(Token = "0x40019CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40019CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40019D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AsyncToken token;

		[Token(Token = "0x40019D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float waitTime;

		[Token(Token = "0x40019D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IScriptPlayer _003Cplayer_003E5__2;

		[Token(Token = "0x40019D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private float _003CstartTime_003E5__3;

		[Token(Token = "0x40019D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002090")]
		[Address(RVA = "0x4EF600", Offset = "0x4EE000", VA = "0x1804EF600", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002091")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40019C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	public DecimalParameter Seconds;

	[Token(Token = "0x6002086")]
	[Address(RVA = "0x4EE970", Offset = "0x4ED370", VA = "0x1804EE970", Slot = "17")]
	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
	{
		return default(int);
	}

	[Token(Token = "0x6002087")]
	[Address(RVA = "0x4EEA50", Offset = "0x4ED450", VA = "0x1804EEA50", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002088")]
	[Address(RVA = "0x4EEE00", Offset = "0x4ED800", VA = "0x1804EEE00", Slot = "18")]
	protected virtual ScriptPlaylist BuildDelayedList()
	{
		return null;
	}

	[Token(Token = "0x6002089")]
	[Address(RVA = "0x4EF020", Offset = "0x4EDA20", VA = "0x1804EF020", Slot = "19")]
	[AsyncStateMachine(typeof(_003CWaitDelay_003Ed__4))]
	protected virtual UniTask WaitDelay(float waitTime, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600208A")]
	[Address(RVA = "0x4EF180", Offset = "0x4EDB80", VA = "0x1804EF180", Slot = "20")]
	[AsyncStateMachine(typeof(_003CExecuteDelayed_003Ed__5))]
	protected virtual UniTask ExecuteDelayed(ScriptPlaylist delayedList, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600208B")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Delay()
	{
	}
}
