using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000025")]
    public class MultipliableActorManagerUtility<TActor> where TActor : IActor
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000026")]
    	[CompilerGenerated]
    	private struct _003CWaitForCompletionOfPendingAddActorAsync_003Ed__2
    {
    		[Token(Token = "0x4000067")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000068")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

    		[Token(Token = "0x4000069")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public MultipliableActorManagerUtility<TActor> _003C_003E4__this;

    		[Token(Token = "0x400006A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string actorId;

    		[Token(Token = "0x400006B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string subId;

    		[Token(Token = "0x400006C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<TActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000062")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000063")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000066")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly IDictionary<string, UniTaskCompletionSource<TActor>> _pendingAddActorTasks;

    	[Token(Token = "0x600005D")]
    	public bool PendingAddActorTaskExists(string actorId, string subId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600005E")]
    	[AsyncStateMachine(typeof(MultipliableActorManagerUtility<>._003CWaitForCompletionOfPendingAddActorAsync_003Ed__2))]
    	public UniTask<TActor> WaitForCompletionOfPendingAddActorAsync(string actorId, string subId)
    	{
    		return default(UniTask<TActor>);
    	}

    	[Token(Token = "0x600005F")]
    	public void CreatePendingAddActorTask(string actorId, string subId)
    	{
    	}

    	[Token(Token = "0x6000060")]
    	public void SetResultAndRemovePendingAddActorTask(string actorId, string subId, TActor actor)
    	{
    	}

    	[Token(Token = "0x6000061")]
    	public MultipliableActorManagerUtility()
    	{
    	}
    }
}
