using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200013D")]
public class StageScreenController : MonoBehaviour, IPerspectiveStageScreenModifier
{
	[StructLayout(3)]
	[Token(Token = "0x200013E")]
	[CompilerGenerated]
	private struct _003CChangeScreenVisibilityAsync_003Ed__10
{
		[Token(Token = "0x400061F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000620")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000621")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int screenIndex;

		[Token(Token = "0x4000622")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public StageScreenController _003C_003E4__this;

		[Token(Token = "0x4000623")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public bool visible;

		[Token(Token = "0x4000624")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public CancellationToken ct;

		[Token(Token = "0x4000625")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float duration;

		[Token(Token = "0x4000626")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000627")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000553")]
		[Address(RVA = "0x388240", Offset = "0x386C40", VA = "0x180388240", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000554")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400061A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private GeneralScreen _generalScreen;

	[Token(Token = "0x400061B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private StageScreenContainer[] _screenContainers;

	[Token(Token = "0x400061C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private StageCameraContainer[] _cameraContainers;

	[Token(Token = "0x400061D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private ITrialStateManager _trialStateManager;

	[Token(Token = "0x400061E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private CancellationTokenSource[] _tokenSources;

	[Token(Token = "0x170000AA")]
	private ITrialStateManager TrialStateManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x387A50
		// Lazily-cached engine service lookup: `_trialStateManager ??=
		// Naninovel.Engine.GetServiceOrErr<ITrialStateManager>();`. Confirmed by
		// disassembly: null check on the field, then a call whose MethodInfo slot
		// resolves to `Method$Naninovel.Engine.GetServiceOrErr<ITrialStateManager>()`,
		// then the result is stored back into the field before returning it.
		[Token(Token = "0x600054A")]
		[Address(RVA = "0x387A50", Offset = "0x386450", VA = "0x180387A50")]
		get
		{
			return _trialStateManager ??= Naninovel.Engine.GetServiceOrErr<ITrialStateManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x387B10, 156 B
	// Much simpler than `StageCameraController::Awake` (same shared
	// `sub_297400(TYPE, length)` array-allocation helper, confirmed same
	// call shape): this controller has no per-screen task-slot tables to
	// pre-allocate (unlike the camera controller's `_modifyCameraTasks`/
	// `_shakeCameraTasks`), just the token-source array, sized off
	// `_screenContainers.Length` (a `[SerializeField]`, unconditionally
	// NRE-throwing dereference).
	[Token(Token = "0x600054B")]
	[Address(RVA = "0x387B10", Offset = "0x386510", VA = "0x180387B10")]
	private void Awake()
	{
		_tokenSources = new CancellationTokenSource[_screenContainers.Length];
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x384A10, 169 B
	// FOLDED BODY: this exact RVA is shared verbatim with
	// `StageCameraController.OnDestroy` (disassembly lists both managed
	// names against the one native function) -- valid because both classes
	// hold `_tokenSources` (a `CancellationTokenSource[]`) at the same
	// `FieldOffset` (0x40), the only field this method touches. See
	// `StageCameraController.OnDestroy`'s comment for the full byte-level
	// trace; identical body reconstructed here for the same reason.
	[Token(Token = "0x600054C")]
	[Address(RVA = "0x384A10", Offset = "0x383410", VA = "0x180384A10")]
	private void OnDestroy()
	{
		for (int i = 0; i < _tokenSources.Length; i++)
		{
			if (_tokenSources[i] != null)
			{
				_tokenSources[i].Cancel();
				_tokenSources[i].Dispose();
				_tokenSources[i] = null;
			}
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x387BB0, 388 B
	// Editor-only wiring, in store order: `GetComponentInChildren<GeneralScreen>()`
	// -> +0x20, `GetComponentsInChildren<StageScreenContainer>()` -> +0x28,
	// `GetComponentsInChildren<StageCameraContainer>()` -> +0x30.
	[Token(Token = "0x600054D")]
	[Address(RVA = "0x387BB0", Offset = "0x3865B0", VA = "0x180387BB0")]
	private void Reset()
	{
		_generalScreen = GetComponentInChildren<GeneralScreen>();
		_screenContainers = GetComponentsInChildren<StageScreenContainer>();
		_cameraContainers = GetComponentsInChildren<StageCameraContainer>();
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (using vs try/finally sugar) | RVA: 0x387D40, 475 B (entry) + MoveNext 0x388240
	// Same family as `StageCameraController.ModifyCameraAsync`:
	// - Bounds check (`jl` on < 0, `jge` against `_screenContainers.Length`)
	//   -> `Debug.LogWarning` of the formatted literal "Screen index {0} is out
	//   of range." and return.
	// - `TrialStateManager` interface slot 3 (`mov r8d, 3`) =
	//   `ITrialStateManager.RegisterScreenVisibilityState(index, visible)`.
	// - `_tokenSources[index]` cancel + dispose when non-null, replaced by a new
	//   source (stored through the array-store helper `sub_2390`), linked with
	//   `ct`.
	// - Before the single await, the inlined
	//   `StageCameraContainer.SetCameraEnabled(true)` on
	//   `_cameraContainers[index]` (+0x28 `_camera`). The await is
	//   `_screenContainers[index].ModifyAlphaAsync(visible ? 1f : 0f, duration,
	//   linkedCts.Token)`, whose kickoff is inlined.
	// - After it: when `!visible`, the camera is disabled again, then
	//   `_tokenSources[index].Dispose(); _tokenSources[index] = null;`.
	//   `linkedCts` is disposed through IDisposable, gated on `state < 0`, on
	//   every exit (the using/finally shape; B, as for the camera controller).
	// So a screen's camera is switched on for the fade-in and off only after a
	// completed fade-out.
	[Token(Token = "0x600054E")]
	[Address(RVA = "0x387D40", Offset = "0x386740", VA = "0x180387D40", Slot = "4")]
	[AsyncStateMachine(typeof(_003CChangeScreenVisibilityAsync_003Ed__10))]
	public async UniTask ChangeScreenVisibilityAsync(int screenIndex, bool visible, float duration, [Optional] CancellationToken ct)
	{
		if (screenIndex < 0 || screenIndex >= _screenContainers.Length)
		{
			UnityEngine.Debug.LogWarning($"Screen index {screenIndex} is out of range.");
			return;
		}

		TrialStateManager.RegisterScreenVisibilityState(screenIndex, visible);

		if (_tokenSources[screenIndex] != null)
		{
			_tokenSources[screenIndex].Cancel();
			_tokenSources[screenIndex].Dispose();
		}
		_tokenSources[screenIndex] = new CancellationTokenSource();
		CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_tokenSources[screenIndex].Token, ct);

		using (linkedCts)
		{
			_cameraContainers[screenIndex].SetCameraEnabled(true);
			await _screenContainers[screenIndex].ModifyAlphaAsync(visible ? 1f : 0f, duration, linkedCts.Token);
			if (!visible)
			{
				_cameraContainers[screenIndex].SetCameraEnabled(false);
			}
			_tokenSources[screenIndex].Dispose();
			_tokenSources[screenIndex] = null;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x387F20, 217 B
	// `sub_3E00` with ecx = 4 on ITrialStateManager = slot 4
	// `RegisterScreenSplitState(index, normalizedPosX, roll, direction)`, then
	// `_screenContainers[index]` (+0x28, bounds-checked) and a tail jump to
	// `StageScreenContainer.ModifyQuadrilateral(normalizedPosX, roll, direction)`.
	// Unlike `ChangeScreenVisibilityAsync` there is no explicit range warning.
	[Token(Token = "0x600054F")]
	[Address(RVA = "0x387F20", Offset = "0x386920", VA = "0x180387F20", Slot = "5")]
	public void SplitScreen(int screenIndex, float normalizedPosX, float roll, ScreenSplitDirection direction)
	{
		TrialStateManager.RegisterScreenSplitState(screenIndex, normalizedPosX, roll, direction);
		_screenContainers[screenIndex].ModifyQuadrilateral(normalizedPosX, roll, direction);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x388000, 464 B
	// `_generalScreen` (+0x20) and `destroyCancellationToken` feed the inlined
	// `GeneralScreen.PresentAsync(instantly: false, ct)` kickoff (state machine
	// field `instantly` stored 0). The resulting task goes to Cysharp `Forget`.
	[Token(Token = "0x6000550")]
	[Address(RVA = "0x388000", Offset = "0x386A00", VA = "0x180388000")]
	public void Present()
	{
		_generalScreen.PresentAsync(false, destroyCancellationToken).Forget();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3881E0, 81 B
	// `GeneralScreen.DismissAsync(instantly: false (r8 = 0), destroyCancellationToken)`
	// (0xCE7510), then `Forget`.
	[Token(Token = "0x6000551")]
	[Address(RVA = "0x3881E0", Offset = "0x386BE0", VA = "0x1803881E0")]
	public void Dismiss()
	{
		_generalScreen.DismissAsync(false, destroyCancellationToken).Forget();
	}

	[Token(Token = "0x6000552")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public StageScreenController()
	{
	}
}
