using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Private.PlayableDirectors;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000073")]
    public class ObjectionCutIn : SpawnableWithAudio, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
    {
    	[Serializable]
    	[Token(Token = "0x2000074")]
    	public class ActivationGameObjects
    	{
    		[Token(Token = "0x4000196")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public GameObject[] Objects;

    		// RECONSTRUCTED | Confidence: A | RVA: 0x32D0C0, 242 B (nested type, outside the buckets)
    		// `Objects` (+0x10) is loaded once and walked by index against its
    		// cached length (foreach over an array). Each element gets the inlined
    		// `GameObject.SetActive`: a null managed reference throws NRE, a
    		// destroyed native object throws via Unity's ThrowHelper, then the
    		// resolved icall `GameObject::SetActive_Injected(IntPtr, bool)`.
    		[Token(Token = "0x6000176")]
    		[Address(RVA = "0x32D0C0", Offset = "0x32BAC0", VA = "0x18032D0C0")]
    		public void SetActive(bool value)
    		{
    			foreach (GameObject obj in Objects)
    			{
    				obj.SetActive(value);
    			}
    		}

    		[Token(Token = "0x6000177")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public ActivationGameObjects()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000075")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__6
    {
    		[Token(Token = "0x4000197")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000198")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000199")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ObjectionCutIn _003C_003E4__this;

    		[Token(Token = "0x400019A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000178")]
    		[Address(RVA = "0x32D1C0", Offset = "0x32BBC0", VA = "0x18032D1C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000179")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000076")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__5
    {
    		[Token(Token = "0x400019B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400019C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400019D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ObjectionCutIn _003C_003E4__this;

    		[Token(Token = "0x400019E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x400019F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40001A0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600017A")]
    		[Address(RVA = "0x32D490", Offset = "0x32BE90", VA = "0x18032D490", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600017B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000192")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Space]
    	[SerializeField]
    	private PlayableDirector _playableDirector;

    	[Token(Token = "0x4000193")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private ActivationGameObjects[] _objectLibrary;

    	[Token(Token = "0x4000194")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private int _currentIndex;

    	[Token(Token = "0x4000195")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private CancellationTokenSource _cts;

    	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x32CCC0, 332 B
    	// Null `parameters` -> an all-zero `int?`; otherwise
    	// `Enumerable.ElementAtOrDefault<string>(parameters, 0)`, and a null
    	// string -> all-zero `int?`, else `Naninovel.ParseUtils.AsInvariantInt`
    	// (dump.cs: `Nullable<int> AsInvariantInt(string)`). The `int?` then goes
    	// through the fully shared `Nullable<int>.GetValueOrDefault()` path (the
    	// dynamic stack copy) and lands in +0x40 `_currentIndex`. So a missing or
    	// unparsable first parameter selects index 0. `asap` is never read.
    	// Roslyn compiles `?? 0` to the same `GetValueOrDefault()`, so the exact
    	// spelling is Confidence B.
    	[Token(Token = "0x6000172")]
    	[Address(RVA = "0x32CCC0", Offset = "0x32B6C0", VA = "0x18032CCC0", Slot = "4")]
    	// EMA ONLY (the other cut-ins keep the line above exactly): her cut-in holds three portraits,
    	// `RefuteCutIn_Ema_001` .. `_003`, and the shipped game shows the third.  The default therefore pins the third;
    	// with CutInPortraitSetting.UseAll any of the three is picked at random.
    	private const string EmaPortraitPrefix = "RefuteCutIn_Ema_";
    	private const int EmaShippedPortraitIndex = 2;

    	private bool IsEmaCutIn
    	{
    		get
    		{
    			GameObject[] first = _objectLibrary != null && _objectLibrary.Length > 0 ? _objectLibrary[0]?.Objects : null;
    			return first != null && first.Length > 0 && first[0] != null && first[0].name.StartsWith(EmaPortraitPrefix);
    		}
    	}

    	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    		_currentIndex = (parameters?.ElementAtOrDefault(0)?.AsInvariantInt()).GetValueOrDefault();
    		if (IsEmaCutIn)
    		{
    			_currentIndex = CutInPortraitSetting.UseAll
    				? UnityEngine.Random.Range(0, _objectLibrary.Length)
    				: Mathf.Min(EmaShippedPortraitIndex, _objectLibrary.Length - 1);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x32CE10, 386 B (entry) + MoveNext 0x32D490
    	// Naninovel builder, but the single await is on a Cysharp UniTask.
    	// 1. `_cts` cancel + dispose when non-null (the second load is
    	//    NRE-checked, not skipped, so this is not `?.`).
    	// 2. `AsyncToken.get_Completed` true -> store null to +0x48 and jump
    	//    straight to SetResult: an early return before anything is shown.
    	// 3. `for` over `_objectLibrary` (the field and `_currentIndex` are
    	//    reloaded every pass) with the inlined `ActivationGameObjects.SetActive(i
    	//    == _currentIndex)`: exactly one library entry is shown.
    	// 4. New `_cts`; `<linkedCts>5__2` = CreateLinkedTokenSource(_cts.Token,
    	//    asyncToken's first 8 bytes = `CancellationToken`), disposed through
    	//    IDisposable with a null check on both exits (a `using`).
    	// 5. Resolved icall `PlayableGraph::IsValid()` (icall-name string at
    	//    0x260C8C8) on `_playableDirector.playableGraph`. Valid -> `time = 0`
    	//    then `PlayableDirectorExtensions.ResumeAsync(director, token)`
    	//    (0xD51960); not valid -> `PlayAsync(director, token)` (0xD517C0). Both
    	//    signatures from dump.cs.
    	// 6. `_playableDirector.Evaluate()` runs after the task is created and
    	//    before it is awaited, so the task is held in a local.
    	// 7. After the await, the inlined `CancellationTokenSource.Dispose()`
    	//    (virtual `Dispose(true)` + `GC.SuppressFinalize`) on `_cts`, then
    	//    `_cts = null`, still inside the using.
    	[Token(Token = "0x6000173")]
    	[Address(RVA = "0x32CE10", Offset = "0x32B810", VA = "0x18032CE10", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__5))]
    	public async Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
    	{
    		if (_cts != null)
    		{
    			_cts.Cancel();
    			_cts.Dispose();
    		}
    		if (asyncToken.Completed)
    		{
    			_cts = null;
    			return;
    		}
    		for (int i = 0; i < _objectLibrary.Length; i++)
    		{
    			_objectLibrary[i].SetActive(i == _currentIndex);
    		}
    		_cts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, asyncToken.CancellationToken);
    		Cysharp.Threading.Tasks.UniTask task;
    		if (_playableDirector.playableGraph.IsValid())
    		{
    			_playableDirector.time = 0.0;
    			task = _playableDirector.ResumeAsync(linkedCts.Token);
    		}
    		else
    		{
    			task = _playableDirector.PlayAsync(linkedCts.Token);
    		}
    		_playableDirector.Evaluate();
    		await task;
    		_cts.Dispose();
    		_cts = null;
    	}

    	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x32CFA0, 281 B (entry) + MoveNext 0x32D1C0
    	// Cancels and disposes `_cts` when non-null (without nulling it), then
    	// awaits an all-zero `Naninovel.UniTask` taken after a class-init check on
    	// `Naninovel.UniTask`: the awaiter source is null, so it completes
    	// synchronously. Nothing is hidden or rewound here. Writing the await
    	// target as `UniTask.CompletedTask` is Confidence B (class-init check
    	// implies a static member).
    	[Token(Token = "0x6000174")]
    	[Address(RVA = "0x32CFA0", Offset = "0x32B9A0", VA = "0x18032CFA0", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__6))]
    	public async Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
    	{
    		if (_cts != null)
    		{
    			_cts.Cancel();
    			_cts.Dispose();
    		}
    		await Naninovel.UniTask.CompletedTask;
    	}

    	[Token(Token = "0x6000175")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ObjectionCutIn()
    	{
    	}
    }
}
