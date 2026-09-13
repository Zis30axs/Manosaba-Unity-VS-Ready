using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000138")]
public class StageCameraController : MonoBehaviour, IPerspectiveStageCameraModifier
{
	[StructLayout(3)]
	[Token(Token = "0x2000139")]
	[CompilerGenerated]
	private struct _003CModifyCameraAsync_003Ed__10
{
		[Token(Token = "0x40005FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40005FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40005FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int cameraIndex;

		[Token(Token = "0x40005FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageCameraController _003C_003E4__this;

		[Token(Token = "0x40005FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ModifyCameraHeightTween heightTween;

		[Token(Token = "0x40005FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public ModifyCameraRotationTween rotationTween;

		[Token(Token = "0x4000600")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public ModifyCameraDistanceTween distanceTween;

		[Token(Token = "0x4000601")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public CancellationToken ct;

		[Token(Token = "0x4000602")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000603")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000540")]
		[Address(RVA = "0x3850A0", Offset = "0x383AA0", VA = "0x1803850A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000541")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200013A")]
	[CompilerGenerated]
	private struct _003CShakeCameraAsync_003Ed__11
{
		[Token(Token = "0x4000604")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000605")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000606")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int cameraIndex;

		[Token(Token = "0x4000607")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageCameraController _003C_003E4__this;

		[Token(Token = "0x4000608")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x4000609")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public Vector3 force;

		[Token(Token = "0x400060A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public float duration;

		[Token(Token = "0x400060B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public int frequency;

		[Token(Token = "0x400060C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
		public float dampingRatio;

		[Token(Token = "0x400060D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public bool randomly;

		[Token(Token = "0x400060E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400060F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000542")]
		[Address(RVA = "0x386190", Offset = "0x384B90", VA = "0x180386190", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000543")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40005F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private StageCameraContainer[] _cameraContainers;

	[Token(Token = "0x40005F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private ITrialStateManager _trialStateManager;

	[Token(Token = "0x40005F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private UniTask[][] _modifyCameraTasks;

	[Token(Token = "0x40005F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private UniTask[][] _shakeCameraTasks;

	[Token(Token = "0x40005F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private CancellationTokenSource[] _tokenSources;

	[Token(Token = "0x170000A9")]
	private ITrialStateManager TrialStateManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x384600, 186 B
		// Lazily-cached engine service lookup: `_trialStateManager ??=
		// Naninovel.Engine.GetServiceOrErr<ITrialStateManager>();`. Byte-for-byte
		// identical shape to `StageScreenController.TrialStateManager` (RVA
		// 0x387A50): null check on `[this+0x28]`, then a call whose MethodInfo
		// slot resolves to `Method$Naninovel.Engine.GetServiceOrErr<ITrialStateManager>()`,
		// result stored back into the field before returning it.
		[Token(Token = "0x6000536")]
		[Address(RVA = "0x384600", Offset = "0x383000", VA = "0x180384600")]
		get
		{
			return _trialStateManager ??= Naninovel.Engine.GetServiceOrErr<ITrialStateManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3846C0, 840 B
	//
	// Pre-allocates the two per-camera task-slot tables this controller
	// uses to track in-flight `ModifyCameraAsync`/`ShakeCameraAsync`
	// operations, one row per camera container, plus a matching
	// `CancellationTokenSource[]` (left as an array of `null`s here --
	// individual sources are created lazily elsewhere, not in `Awake`).
	//
	// Every `new T[n]`-shaped allocation here (5 of them: the two outer
	// jagged arrays, one inner row per iteration of each of the two
	// loops, and the token-source array) resolves to the SAME native
	// helper, `sub_297400(TYPE array, int length)` -- independently
	// disassembled and confirmed to be a generic array-allocation routine
	// (computes `elementSize * length + header`, calls the GC allocator,
	// stores the array TypeInfo and length) called with the exact
	// `TYPE=..., length=...` shape at every site, not a specialized
	// per-type constructor -- i.e. this is IL2CPP's shared codegen for
	// the `newarr`/`new T[n]` IL instruction, not five different named
	// APIs. The row lengths (4 for `_modifyCameraTasks`, 3 for
	// `_shakeCameraTasks`) are literal immediates (`mov edx, 4`/
	// `mov edx, 3`) at the respective call sites, not derived from
	// anything.
	//
	// RESOLVED (see `ModifyCameraAsync` below for the full reconstruction):
	// `_modifyCameraTasks[cameraIndex]`'s row is indexed by fixed
	// per-tween-kind constants, confirmed by three independent bounds-check
	// literals against the row's `.Length` (`cmp [row+0x18], 0/1/2; jbe
	// <out-of-range>`) paired with 16-byte (`UniTask` struct size) element
	// writes at matching offsets: slot 0 (`row+0x20`) = height-tween task,
	// slot 1 (`row+0x30`) = rotation-tween, slot 2 (`row+0x40`) =
	// distance-tween (each populated only if its tween parameter is
	// non-null, else `default(UniTask)`). Slot 3 (`row+0x50`) is never
	// written anywhere in `ModifyCameraAsync` -- it stays `default(UniTask)`
	// permanently, inert in the `await tasks;` combinator (confirmed
	// `UniTaskExtensions.GetAwaiter(UniTask[])`, RVA 0x18203F0, awaits the
	// whole array). Why the row is allocated at length 4 instead of exactly
	// 3 is still UNKNOWN -- `_shakeCameraTasks`'s 3 slots have NOT been
	// examined yet (would require disassembling
	// `<ShakeCameraAsync>d__11.MoveNext()`, RVA 0x386190) and may or may not
	// shed further light on it.
	//
	// `_cameraContainers` (a `[SerializeField]`) is read fresh, with its
	// own explicit NRE-throwing null check, THREE separate times (once
	// per outer-array allocation) rather than cached into a local once --
	// confirmed deliberately, not a simplification opportunity: the
	// disassembly performs three independent `test+je` sequences on three
	// independent reloads of `[this+0x20]`, not one check reused three
	// times.
	[Token(Token = "0x6000537")]
	[Address(RVA = "0x3846C0", Offset = "0x3830C0", VA = "0x1803846C0")]
	private void Awake()
	{
		_modifyCameraTasks = new UniTask[_cameraContainers.Length][];
		for (int i = 0; i < _modifyCameraTasks.Length; i++)
		{
			_modifyCameraTasks[i] = new UniTask[4];
		}
		_shakeCameraTasks = new UniTask[_cameraContainers.Length][];
		for (int i = 0; i < _shakeCameraTasks.Length; i++)
		{
			_shakeCameraTasks[i] = new UniTask[3];
		}
		_tokenSources = new CancellationTokenSource[_cameraContainers.Length];
	}

	[Token(Token = "0x6000538")]
	[Address(RVA = "0x384A10", Offset = "0x383410", VA = "0x180384A10")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000539")]
	[Address(RVA = "0x384AC0", Offset = "0x3834C0", VA = "0x180384AC0")]
	private void Reset()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x384B50 (680 B, entry) + 0x3850A0 (4319 B, MoveNext)
	//
	// Public entry (0x384B50) is the standard compiler-generated
	// `AsyncUniTaskMethodBuilder.Start<...>()` kickoff -- no logic of its own.
	// All behavior below comes from `<ModifyCameraAsync>d__10.MoveNext()`
	// (0x3850A0), which has exactly ONE await point (confirmed: the init
	// block declares a single `AwaitUnsafeOnCompleted<UniTask.Awaiter,
	// ...d__10>()`), making the 4319-byte size mostly straight-line setup
	// rather than branching complexity.
	//
	// Evidence chain, in execution order:
	// - Bounds check: `cameraIndex < 0` (`jl`) OR `>= _cameraContainers.Length`
	//   (`jge`) both branch to the same LogWarning+return block, which builds
	//   the exact literal `'Camera index {0} is out of range.'` via
	//   `String.FormatHelper`+`ParamsArray..ctor` with one boxed int arg --
	//   i.e. `$"Camera index {cameraIndex} is out of range."`.
	// - `TrialStateManager` access at this call site is byte-identical to the
	//   just-reconstructed getter (0x384600): same null-check-then-
	//   `GetServiceOrErr<ITrialStateManager>()`-then-cache shape, confirming
	//   it's a plain property read, not a separate inlined lookup.
	// - `RegisterCameraState(int, float?, float?[], float?)` identified by
	//   exact signature match against `ITrialStateManager` (only member with
	//   this parameter shape) AND vtable-slot arithmetic (`mov r8d, 2` selects
	//   the 3rd declared interface member, 0-indexed -- `RegisterCameraState`
	//   is declared 3rd in `ITrialStateManager.cs`). Each nullable argument's
	//   construction matches `x?.To` exactly: height/distance tweens store a
	//   plain `float To` (confirmed against `ModifyCameraHeightTween`/
	//   `ModifyCameraDistanceTween`'s reconstructed `_to` field at +0x10, so
	//   `heightTween?.To`/`distanceTween?.To` compile to a null-check +
	//   `new Nullable<float>(...)`, seen verbatim); rotation's `To` is already
	//   a reference-typed `float?[]` (confirmed against
	//   `ModifyCameraRotationTween`'s `_to` field, also +0x10, an object
	//   reference not a struct), so `rotationTween?.To` compiles to a plain
	//   null-check + 8-byte reference load, also seen verbatim -- this
	//   asymmetry in codegen shape is what confirmed the field's real type
	//   before this method was ever disassembled.
	// - `_tokenSources[cameraIndex]`: cancel+dispose the previous source (if
	//   any, re-checked null independently for each of the two calls -- same
	//   "no caching across statements" convention already documented in
	//   `Awake()`), replace with a fresh `CancellationTokenSource`, then
	//   `CancellationTokenSource.CreateLinkedTokenSource(...Token, ct)`.
	// - Three tween-task launches into `_modifyCameraTasks[cameraIndex]`,
	//   confirmed against the row's own bounds-check literals (`cmp
	//   [row+0x18], 0/1/2; jbe <range-check>`) paired with 16-byte (`UniTask`
	//   struct size) writes at `row+0x20/+0x30/+0x40`: slot 0 = height, slot 1
	//   = rotation, slot 2 = distance. Each container call's arguments
	//   (`To`/`Ease`/`Duration`) were verified against the already-
	//   reconstructed record property getters' own field offsets, and each
	//   call's parameter ORDER against `StageCameraContainer`'s own
	//   `ModifyHeightAsync`/`ModifyRotationAsync`/`ModifyDistanceAsync`
	//   signatures (height/distance calls are inlined `Start<>()` kickoffs at
	//   this call site rather than plain calls, unlike rotation's; this is
	//   the same "trivial-wrapper inlining is inconsistent per-instantiation"
	//   pattern already seen for `Gosub`/`Dismiss` trampolines elsewhere in
	//   this session -- confirmed via each callee's own struct field offsets
	//   in `StageCameraContainer.cs`, not assumed from the inlining shape).
	// - The single await targets `UniTaskExtensions.GetAwaiter(UniTask[]
	//   tasks)` -- independently disassembled at RVA 0x18203F0 and confirmed
	//   via its own `dump.cs` signature to take ONLY the array (no index),
	//   which is what makes `await _modifyCameraTasks[cameraIndex];` valid
	//   C# at all (the array becomes directly awaitable through this
	//   extension). Slot 3 of the row is never written anywhere in this
	//   method -- it stays `default(UniTask)` (already-completed, inert in
	//   the combined await) for the lifetime of this controller. Why the row
	//   is sized 4 rather than exactly 3 remains UNKNOWN; `_shakeCameraTasks`
	//   (3 slots, consumed by `ShakeCameraAsync`, RVA 0x384E00) has not yet
	//   been examined and may or may not shed further light on it.
	// - Post-await, `_tokenSources[cameraIndex].Dispose(); ... = null;` run
	//   BEFORE `linkedCts` is disposed -- both directly observed in
	//   instruction order at 0x180385da5-e32. `linkedCts`'s dispose there
	//   (and at two further gated copies of the same call at 0x180385f9f and
	//   0x180385fd0, one per MoveNext re-entry path) goes through an explicit
	//   `IDisposable`-interface call thunk rather than a direct
	//   `CancellationTokenSource.Dispose()` dispatch, and each copy is gated
	//   behind a `state < 0` check -- the signature of a compiler-emitted
	//   `finally` reached from multiple entry points (normal fallthrough
	//   plus exception unwind). This establishes (a) the statement order
	//   and (b) that the dispose is finally-dispatched through `IDisposable`
	//   -- CONFIDENCE B: it does NOT by itself distinguish a `using
	//   (linkedCts) { ...; ...Dispose(); ... = null; }` block from a
	//   hand-written `try { ...; ...Dispose(); ... = null; } finally {
	//   ((IDisposable)linkedCts).Dispose(); }`, since `CancellationTokenSource`
	//   is a non-sealed type with a virtual `Dispose()`, and Roslyn emits the
	//   same `IDisposable`-cast dispatch for `using` on such a type. The
	//   `using` form is written below because it reproduces the observed
	//   order and dispatch exactly and is the idiomatic choice; the
	//   alternative would compile identically at this level of detail.
	[Token(Token = "0x600053A")]
	[Address(RVA = "0x384B50", Offset = "0x383550", VA = "0x180384B50", Slot = "4")]
	[AsyncStateMachine(typeof(_003CModifyCameraAsync_003Ed__10))]
	public async UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct)
	{
		if (cameraIndex < 0 || cameraIndex >= _cameraContainers.Length)
		{
			UnityEngine.Debug.LogWarning($"Camera index {cameraIndex} is out of range.");
			return;
		}

		TrialStateManager.RegisterCameraState(cameraIndex, heightTween?.To, rotationTween?.To, distanceTween?.To);

		if (_tokenSources[cameraIndex] != null)
		{
			_tokenSources[cameraIndex].Cancel();
			_tokenSources[cameraIndex].Dispose();
		}
		_tokenSources[cameraIndex] = new CancellationTokenSource();
		CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_tokenSources[cameraIndex].Token, ct);

		_modifyCameraTasks[cameraIndex][0] = (heightTween != null) ? _cameraContainers[cameraIndex].ModifyHeightAsync(heightTween.To, heightTween.Ease, heightTween.Duration, linkedCts.Token) : default(UniTask);
		_modifyCameraTasks[cameraIndex][1] = (rotationTween != null) ? _cameraContainers[cameraIndex].ModifyRotationAsync(rotationTween.To, rotationTween.Ease, rotationTween.Duration, linkedCts.Token) : default(UniTask);
		_modifyCameraTasks[cameraIndex][2] = (distanceTween != null) ? _cameraContainers[cameraIndex].ModifyDistanceAsync(distanceTween.To, distanceTween.Ease, distanceTween.Duration, linkedCts.Token) : default(UniTask);

		using (linkedCts)
		{
			await _modifyCameraTasks[cameraIndex];
			_tokenSources[cameraIndex].Dispose();
			_tokenSources[cameraIndex] = null;
		}
	}

	[Token(Token = "0x600053B")]
	[Address(RVA = "0x384E00", Offset = "0x383800", VA = "0x180384E00", Slot = "5")]
	[AsyncStateMachine(typeof(_003CShakeCameraAsync_003Ed__11))]
	public UniTask ShakeCameraAsync(int cameraIndex, Vector3 force, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600053C")]
	[Address(RVA = "0x385000", Offset = "0x383A00", VA = "0x180385000")]
	public void Present()
	{
	}

	[Token(Token = "0x600053D")]
	[Address(RVA = "0x385010", Offset = "0x383A10", VA = "0x180385010")]
	public void Dismiss()
	{
	}

	[Token(Token = "0x600053E")]
	[Address(RVA = "0x385020", Offset = "0x383A20", VA = "0x180385020")]
	private void SetCameraEnabled(bool active)
	{
	}

	[Token(Token = "0x600053F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageCameraController()
	{
	}
}
