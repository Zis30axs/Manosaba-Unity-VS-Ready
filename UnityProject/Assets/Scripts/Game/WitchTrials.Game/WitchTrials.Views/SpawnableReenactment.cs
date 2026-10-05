using GigaCreation.Tools.Private.PlayableDirectors;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace WitchTrials.Views
{
    [Token(Token = "0x200007F")]
    public class SpawnableReenactment : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000080")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__8
    {
    		[Token(Token = "0x40001EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnableReenactment _003C_003E4__this;

    		[Token(Token = "0x40001EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x40001F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private TaskAwaiter _003C_003Eu__1;

    		[Token(Token = "0x60001A0")]
    		[Address(RVA = "0x333AF0", Offset = "0x3324F0", VA = "0x180333AF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001A1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000081")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__7
    {
    		[Token(Token = "0x40001F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnableReenactment _003C_003E4__this;

    		[Token(Token = "0x40001F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x40001F5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private TaskAwaiter _003C_003Eu__1;

    		[Token(Token = "0x60001A2")]
    		[Address(RVA = "0x333D60", Offset = "0x332760", VA = "0x180333D60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001A3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000082")]
    	[CompilerGenerated]
    	private struct _003CRunTimelineAsync_003Ed__9
    {
    		[Token(Token = "0x40001F6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public SpawnableReenactment _003C_003E4__this;

    		[Token(Token = "0x40001F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public PlayableDirector director;

    		[Token(Token = "0x40001FA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x40001FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40001FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60001A4")]
    		[Address(RVA = "0x334150", Offset = "0x332B50", VA = "0x180334150", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60001A5")]
    		[Address(RVA = "0x3349D0", Offset = "0x3333D0", VA = "0x1803349D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40001E6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private PlayableDirector _openingDirector;

    	[Token(Token = "0x40001E7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private PlayableDirector _closingDirector;

    	[Token(Token = "0x40001E8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private PlayableDirector[] _storiesDirector;

    	[Token(Token = "0x40001E9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private PlayableDirector _current;

    	[Token(Token = "0x40001EA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private int _currenIndex;

    	[Token(Token = "0x40001EB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private CancellationTokenSource _cts;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x333420, 332 B
    	// Parameter 0 is the story index (Naninovel ParseUtils.AsInvariantInt, int?); a missing or invalid value means 0. `asap` is unused.
    	[Token(Token = "0x600019B")]
    	[Address(RVA = "0x333420", Offset = "0x331E20", VA = "0x180333420", Slot = "4")]
    	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    		_currenIndex = (parameters?.ElementAtOrDefault(0)?.AsInvariantInt()).GetValueOrDefault();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x333570 (builder stub); MoveNext 0x333D60, 1003 B, two Task await states
    	// 
    	// - The previous run's `_cts` (+0x48) is cancelled and disposed; an already completed token clears it and stops.
	// - Story 0 is preceded by the opening timeline (+0x20); then the indexed story director plays.
    	[Token(Token = "0x600019C")]
    	[Address(RVA = "0x333570", Offset = "0x331F70", VA = "0x180333570", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__7))]
    	public async Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
    	{
    		if (_cts != null)
    		{
			CancellationTokenSource previousCts = _cts;
			_cts = null;
			CancelAndDispose(previousCts);
    		}
    		if (asyncToken.Completed)
    		{
    			_cts = null;
    			return;
    		}
    		if (_currenIndex == 0)
    		{
			await RunTimelineAsync(asyncToken, _openingDirector);
    		}
		// A malformed or stale spawn parameter must not abort the entire script player.
		// Reenactment prefabs normally contain one story director, but keep the lookup
		// defensive because SpawnExtended can receive an empty/invalid parameter list.
		if (_storiesDirector == null || _storiesDirector.Length == 0)
			return;
		int storyIndex = Mathf.Clamp(_currenIndex, 0, _storiesDirector.Length - 1);
		await RunTimelineAsync(asyncToken, _storiesDirector[storyIndex]);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3336F0 (builder stub); MoveNext 0x333AF0, 613 B
    	// Cancels the running timeline and plays the closing one (+0x28); no completed-token check here.
    	[Token(Token = "0x600019D")]
    	[Address(RVA = "0x3336F0", Offset = "0x3320F0", VA = "0x1803336F0", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__8))]
    	public async Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
    	{
    		if (_cts != null)
    		{
			CancellationTokenSource previousCts = _cts;
			_cts = null;
			CancelAndDispose(previousCts);
    		}
		await RunTimelineAsync(asyncToken, _closingDirector);
	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the task kept in a local across Evaluate) | RVA: 0x333870 (builder stub);
    	// MoveNext 0x334150, 2168 B (System AsyncTaskMethodBuilder: this is a real `async Task`)
    	// 
    	// - `_current` (+0x38) takes the director; a fresh `_cts` is linked with the command token.
    	// - Skipped when the director is destroyed or this GameObject is gone (inlined Unity `!=` / Object.op_Implicit).
    	// - icall `PlayableGraph::IsValid()`: an existing graph is rewound (`time = 0`) and resumed, otherwise PlayAsync
    	//   (GigaCreation PlayableDirectorExtensions). Evaluate() runs before the returned Cysharp UniTask is awaited.
	// - Each run disposes its own CTS and clears the shared field only if it still owns it.
    	[Token(Token = "0x600019E")]
    	[Address(RVA = "0x333870", Offset = "0x332270", VA = "0x180333870")]
    	[AsyncStateMachine(typeof(_003CRunTimelineAsync_003Ed__9))]
    	private async Task RunTimelineAsync(AsyncToken asyncToken, PlayableDirector director)
    	{
    		_current = director;
		// Keep ownership local. A cancelled run can finish after AwaitDestroy has
		// installed a new CTS for the closing timeline; disposing the shared field
		// from the old run would otherwise kill that new timeline.
		CancellationTokenSource timelineCts = new CancellationTokenSource();
		_cts = timelineCts;
		try
    		{
			using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(timelineCts.Token, asyncToken.CancellationToken);
			if (director != null && (bool)base.gameObject)
    			{
				Cysharp.Threading.Tasks.UniTask task;
				if (director.playableGraph.IsValid())
				{
					director.time = 0.0;
					task = director.ResumeAsync(linkedCts.Token);
				}
				else
				{
					task = director.PlayAsync(linkedCts.Token);
				}
				director.Evaluate();
				await task;
    			}
    		}
		finally
		{
			if (ReferenceEquals(_cts, timelineCts))
				_cts = null;
			timelineCts.Dispose();
		}
    	}

		private static void CancelAndDispose(CancellationTokenSource source)
		{
			try
			{
				source.Cancel();
			}
			catch (System.ObjectDisposedException)
			{
				// The previous timeline may have completed and disposed its CTS between
				// the shared-field read and this cancellation request.
			}
			try
			{
				source.Dispose();
			}
			catch (System.ObjectDisposedException)
			{
				// Dispose is intentionally idempotent across the same race.
			}
		}

    	[Token(Token = "0x600019F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SpawnableReenactment()
    	{
    	}
    }
}
