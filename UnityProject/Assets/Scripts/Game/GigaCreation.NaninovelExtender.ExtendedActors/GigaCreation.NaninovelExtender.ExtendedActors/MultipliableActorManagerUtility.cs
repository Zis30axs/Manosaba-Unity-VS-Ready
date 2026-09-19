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

    	// Generic class: the shared instantiation carries the code (RVAs below are the
    	// MultipliableActorManagerUtility<__Il2CppFullySharedGenericType> methods in script.json).
    	// The four methods key `_pendingAddActorTasks` by IMultipliableActor.BuildId(actorId, subId): each one has
    	// the inlined `subId != null && subId.Length > 0 ? actorId + "-" + subId : actorId` (STR '-', String.Concat).
    	// Annotated disassembly: work/title-path/disas/mamu_*.txt

    	// RECONSTRUCTED | Confidence: A | RVA: 0x87C670, 182 B
    	// `_pendingAddActorTasks` (+0x10) is dereferenced without a null test (THROW NRE at 0x18087C721); the interface
    	// call is IDictionary.ContainsKey (thunk 0x180034590, ecx = interface slot 4).
    	[Token(Token = "0x600005D")]
    	public bool PendingAddActorTaskExists(string actorId, string subId)
    	{
    		return _pendingAddActorTasks.ContainsKey(IMultipliableActor.BuildId(actorId, subId));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x87C730 (kickoff, 0x87C730 + 17 KB annotated) / 0xBA6740 (MoveNext)
    	// MoveNext: BuildId, `_pendingAddActorTasks[id]` (get_Item thunk 0x1800528D0), `.Task`
    	// (UniTaskCompletionSource<T>.get_Task 0x1809702E0), GetAwaiter, await, SetResult(result).
    	[Token(Token = "0x600005E")]
    	[AsyncStateMachine(typeof(MultipliableActorManagerUtility<>._003CWaitForCompletionOfPendingAddActorAsync_003Ed__2))]
    	public UniTask<TActor> WaitForCompletionOfPendingAddActorAsync(string actorId, string subId)
    	{
    		return WaitForCompletionOfPendingAddActorAsyncCore(actorId, subId);
    	}

    	private async UniTask<TActor> WaitForCompletionOfPendingAddActorAsyncCore(string actorId, string subId)
    	{
    		return await _pendingAddActorTasks[IMultipliableActor.BuildId(actorId, subId)].Task;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x87CB00, 352 B
    	// il2cpp_codegen_object_new(UniTaskCompletionSource<TActor>) then IDictionary set_Item (interface slot 1):
    	// `_pendingAddActorTasks[id] = new UniTaskCompletionSource<TActor>()`.
    	[Token(Token = "0x600005F")]
    	public void CreatePendingAddActorTask(string actorId, string subId)
    	{
    		_pendingAddActorTasks[IMultipliableActor.BuildId(actorId, subId)] = new UniTaskCompletionSource<TActor>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x87CC70, 423 B
    	//   0x18087CD58  source = _pendingAddActorTasks[id]     (get_Item thunk 0x1800528D0; a missing key throws, a null
    	//                dictionary is the THROW NRE at 0x18087CE12 -- there is no TryGetValue and no null test on `source`
    	//                other than the one that leads to the same NRE)
    	//   0x18087CDB8  source.TrySetResult(actor)            (UniTaskCompletionSource<T>.TrySetResult)
    	//   0x18087CDF4  _pendingAddActorTasks.Remove(id)      (thunk 0x180034590, ecx = interface slot 6)
    	[Token(Token = "0x6000060")]
    	public void SetResultAndRemovePendingAddActorTask(string actorId, string subId, TActor actor)
    	{
    		string id = IMultipliableActor.BuildId(actorId, subId);
    		_pendingAddActorTasks[id].TrySetResult(actor);
    		_pendingAddActorTasks.Remove(id);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x87CE20, 193 B
    	// il2cpp_codegen_object_new(Dictionary<string, UniTaskCompletionSource<TActor>>) + Dictionary..ctor(),
    	// stored to +0x10 (the field is declared as IDictionary).
    	[Token(Token = "0x6000061")]
    	public MultipliableActorManagerUtility()
    	{
    		_pendingAddActorTasks = new Dictionary<string, UniTaskCompletionSource<TActor>>();
    	}
    }
}
