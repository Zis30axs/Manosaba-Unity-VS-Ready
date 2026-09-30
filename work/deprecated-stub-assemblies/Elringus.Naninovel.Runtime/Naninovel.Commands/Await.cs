using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006BA")]
    [RequireNested]
    [Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
    public class Await : Command, Command.INestedHost
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20006BB")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__2
    {
    		[Token(Token = "0x40019BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40019BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40019BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Await _003C_003E4__this;

    		[Token(Token = "0x40019BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40019C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IScriptPlayer _003Cplayer_003E5__2;

    		[Token(Token = "0x40019C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600207A")]
    		[Address(RVA = "0x4ED710", Offset = "0x4EC110", VA = "0x1804ED710", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600207B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40019BB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private bool initial;

    	[Token(Token = "0x6002077")]
    	[Address(RVA = "0x4ED480", Offset = "0x4EBE80", VA = "0x1804ED480", Slot = "17")]
    	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6002078")]
    	[Address(RVA = "0x4ED580", Offset = "0x4EBF80", VA = "0x1804ED580", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002079")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Await()
    	{
    	}
    }
}
