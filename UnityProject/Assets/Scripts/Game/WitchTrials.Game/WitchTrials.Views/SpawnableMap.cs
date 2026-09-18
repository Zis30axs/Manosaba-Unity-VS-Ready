using GigaCreation.Tools.Private.PlayableDirectors;
using LitMotion.Extensions;
using LitMotion;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace WitchTrials.Views
{
    [Token(Token = "0x200007C")]
    public class SpawnableMap : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200007D")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__12
    {
    		[Token(Token = "0x40001DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnableMap _003C_003E4__this;

    		[Token(Token = "0x40001DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x40001DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40001DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000197")]
    		[Address(RVA = "0x332520", Offset = "0x330F20", VA = "0x180332520", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000198")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200007E")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__11
    {
    		[Token(Token = "0x40001E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnableMap _003C_003E4__this;

    		[Token(Token = "0x40001E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x40001E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40001E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000199")]
    		[Address(RVA = "0x332C60", Offset = "0x331660", VA = "0x180332C60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600019A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40001D2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x40001D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private PlayableDirector[] _playableDirectors;

    	[Token(Token = "0x40001D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private CanvasGroup _rootCanvasGroup;

    	[Token(Token = "0x40001D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _hideDuration;

    	[Token(Token = "0x40001D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private ICameraManager _naniCameraManager;

    	[Token(Token = "0x40001D7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private PlayableDirector _current;

    	[Token(Token = "0x40001D8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private int _currenIndex;

    	[Token(Token = "0x40001D9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private CancellationTokenSource _cts;

    	[Token(Token = "0x17000026")]
    	private ICameraManager NaniCameraManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x330D80
    		// Lazy cache in +0x40 via `Engine.GetServiceOrErr<ICameraManager>()`.
    		[Token(Token = "0x6000192")]
    		[Address(RVA = "0x330D80", Offset = "0x32F780", VA = "0x180330D80")]
    		get
    		{
    			return _naniCameraManager ??= Engine.GetServiceOrErr<ICameraManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (GetValueOrDefault vs `?? 0` spelling: B) | RVA: 0x330E40
    	// Same body shape and spelling as ObjectionCutIn.SetSpawnParameters (0x32CCC0).
    	// 
    	// - `_canvas` (+0x20) is loaded first, then the inlined NaniCameraManager getter, ICameraManager
    	//   slot 0 `Camera`, and Canvas.set_worldCamera.
    	// - Null `parameters`, or a null `ElementAtOrDefault<string>(0)`, skips `ParseUtils.AsInvariantInt`
    	//   and yields a null `int?`.
    	// - `Nullable<int>.GetValueOrDefault()` stores the result into `_currenIndex` (+0x50).
    	// - `asap` is not read.
    	[Token(Token = "0x6000193")]
    	[Address(RVA = "0x330E40", Offset = "0x32F840", VA = "0x180330E40", Slot = "4")]
    	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    		_canvas.worldCamera = NaniCameraManager.Camera;
    		_currenIndex = (parameters?.ElementAtOrDefault(0)?.AsInvariantInt()).GetValueOrDefault();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x331080 (builder stub); MoveNext 0x332C60
    	// 
    	// - A live `_cts` (+0x58) is cancelled and disposed.
    	// - `AsyncToken.Completed` -> `_cts = null` and SetResult; this jumps past the using scope.
    	// - `_current` (+0x48) = `_playableDirectors[_currenIndex]`; then a new `_cts`.
    	// - The hoisted `<linkedCts>5__2` (+0x30) = CreateLinkedTokenSource(_cts.Token,
    	//   asyncToken.CancellationToken).
    	// - Branch on `PlayableGraph.IsValid()` (icall 0x260C8C8):
    	//   - invalid -> `PlayAsync(linkedCts.Token)`;
    	//   - valid -> `set_time(0.0)` then `ResumeAsync(linkedCts.Token)`.
    	// - `Evaluate()` runs before the task is awaited.
    	// - After the await: `_cts.Dispose()` (Dispose(true) + GC.SuppressFinalize inlined), then `_cts = null`,
    	//   then the using disposal (state < 0 guard, IDisposable thunk).
    	[Token(Token = "0x6000194")]
    	[Address(RVA = "0x331080", Offset = "0x32FA80", VA = "0x180331080", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__11))]
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
    		_current = _playableDirectors[_currenIndex];
    		_cts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, asyncToken.CancellationToken);
    		Cysharp.Threading.Tasks.UniTask task;
    		if (!_current.playableGraph.IsValid())
    		{
    			task = _current.PlayAsync(linkedCts.Token);
    		}
    		else
    		{
    			_current.time = 0.0;
    			task = _current.ResumeAsync(linkedCts.Token);
    		}
    		_current.Evaluate();
    		await task;
    		_cts.Dispose();
    		_cts = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x331210 (builder stub); MoveNext 0x332520
    	// 
    	// - The old `_cts` is cancelled and disposed exactly as in AwaitSpawn.
    	// - xmm6 starts at 0 and takes `_hideDuration` (+0x38) only when the token is not Completed.
    	// - `Mathf.Approximately(duration, 0f)` (sub_331670) -> SetResult. `_cts` is left disposed, not nulled.
    	// - Otherwise: a new `_cts` and the linked CTS.
    	// - `LMotion.Create<float, NoOptions, FloatMotionAdapter>(_rootCanvasGroup.alpha, 0f, duration)`
    	//   (the `to` slot is the zeroed rsp+0x108), `BindToAlpha(_rootCanvasGroup)`,
    	//   `ToUniTask(handle, CancellationToken)` (0xD83990), awaited.
    	// - `_cts.Dispose()` follows. Unlike AwaitSpawn there is no `_cts = null`.
    	[Token(Token = "0x6000195")]
    	[Address(RVA = "0x331210", Offset = "0x32FC10", VA = "0x180331210", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__12))]
    	public async Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
    	{
    		if (_cts != null)
    		{
    			_cts.Cancel();
    			_cts.Dispose();
    		}
    		float duration = asyncToken.Completed ? 0f : _hideDuration;
    		if (Mathf.Approximately(duration, 0f))
    		{
    			return;
    		}
    		_cts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, asyncToken.CancellationToken);
    		await LMotion.Create(_rootCanvasGroup.alpha, 0f, duration)
    			.BindToAlpha(_rootCanvasGroup)
    			.ToUniTask(linkedCts.Token);
    		_cts.Dispose();
    	}

    	[Token(Token = "0x6000196")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SpawnableMap()
    	{
    	}
    }
}
