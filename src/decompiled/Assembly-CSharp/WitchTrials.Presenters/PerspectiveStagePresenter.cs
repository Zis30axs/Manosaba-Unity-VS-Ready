using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Models;
using WitchTrials.Views;

namespace WitchTrials.Presenters;

[Token(Token = "0x200014B")]
public class PerspectiveStagePresenter : MonoBehaviour, IPerspectiveStageLoader, ICourtModifier
{
	[StructLayout(3)]
	[Token(Token = "0x200014D")]
	[CompilerGenerated]
	private struct _003CLoadStageAsync_003Ed__13
{
		[Token(Token = "0x4000667")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000668")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000669")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStagePresenter _003C_003E4__this;

		[Token(Token = "0x400066A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PerspectiveStageKind kind;

		[Token(Token = "0x400066B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x400066C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600058C")]
		[Address(RVA = "0x38E6A0", Offset = "0x38D0A0", VA = "0x18038E6A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600058D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400065C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private PerspectiveStageData[] _stageData;

	[Token(Token = "0x400065D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private Transform _stageParent;

	[Token(Token = "0x400065E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private StageScreenController _screenController;

	[Token(Token = "0x400065F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private StageCameraController _cameraController;

	[Token(Token = "0x4000660")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IPerspectiveStageManager _perspectiveStageManager;

	[Token(Token = "0x4000661")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ITrialStateManager _trialStateManager;

	[Token(Token = "0x4000662")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IReadOnlyDictionary<PerspectiveStageKind, AssetReferenceGameObject> _stageReferences;

	[Token(Token = "0x4000663")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private GameObject _currentStage;

	[Token(Token = "0x170000B5")]
	private ITrialStateManager TrialStateManager
	{
		[Token(Token = "0x6000580")]
		[Address(RVA = "0x38D810", Offset = "0x38C210", VA = "0x18038D810")]
		get
		{
			return null;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38D8D0, 1558 B
	//
	// This is the "resident state" side of the PerspectiveStage subsystem
	// (as opposed to the `GosubTo*` "command call" side reconstructed
	// earlier this round): it resets both controllers to a known-hidden
	// state, builds the stage-kind lookup table, and constructs/registers
	// the single `PerspectiveStageManager` service this whole subsystem
	// runs on.
	//
	//   - `_screenController.Dismiss();` / `_cameraController.Dismiss();`
	//     -- NOT direct field/method access on those classes (their
	//     `_generalScreen` field and `SetCameraEnabled` method are both
	//     `private` per dump.cs, so a cross-class call could never compile
	//     to what's inlined here). Both are calls to `StageScreenController
	//     .Dismiss()`/`StageCameraController.Dismiss()` (both public,
	//     0-arg) with their own trivial bodies fully inlined at this call
	//     site -- confirmed independently: `StageScreenController.Dismiss`
	//     (RVA 0x3881E0) disassembles byte-for-byte identical to what's
	//     inlined here (`_generalScreen.DismissAsync(false,
	//     destroyCancellationToken).Forget()`); `StageCameraController.
	//     Dismiss` (RVA 0x385010) is a 10-byte tail-call trampoline into
	//     the private `SetCameraEnabled(false)` (confirmed by
	//     disassembling it directly and, for contrast, its sibling
	//     `Present()` at RVA 0x385000, which is the same trampoline shape
	//     tail-calling `SetCameraEnabled(true)` -- `Present`/`Dismiss` are
	//     simply named boolean toggles over the same private setter).
	//   - `_stageReferences = _stageData.ToDictionary(data => data.Kind,
	//     data => data.Stage);`: `_stageData` itself has NO inlined
	//     null-guard here (unlike `_stageParent`/`_screenController`/
	//     `_cameraController`, each of which gets its own explicit
	//     `test+je NullReferenceException` in this method) -- it flows
	//     straight into `Enumerable.ToDictionary`, a substantial generic
	//     BCL method called via a real `call` rather than inlined, which
	//     does its own `ArgumentNullException` guard internally. This
	//     asymmetry is expected, not a gap: the other three fields are
	//     hand-dereferenced in this method's own source and each gets its
	//     own compiler-inserted guard at the point of use; `_stageData` is
	//     just handed off as an opaque argument. The two selector lambdas
	//     are `PerspectiveStageData.Kind`/`.Stage`, confirmed via this
	//     project's METHODINFO alias trick -- both are folded bodies
	//     shared across many structurally-identical single-property-read
	//     lambdas elsewhere in the binary (same "trivial lambda folds to
	//     shared native code" pattern as `PrintDebateText.<Print>b__21_0/1`
	//     this round), and `PerspectiveStageData.Kind`/`.Stage` are
	//     themselves already-reconstructed Confidence-A properties
	//     reading fields at +0x10/+0x18 respectively.
	//   - `new PerspectiveStageManager(this, _screenController,
	//     _cameraController, this, _stageParent.position)`: the
	//     `il2cpp_codegen_object_new` allocation is followed by DIRECT
	//     field stores (`[+0x10]=this`, `[+0x18]=_screenController`,
	//     `[+0x20]=_cameraController`, `[+0x28]=this`,
	//     `[+0x30]/[+0x38]=Vector3`), not a call to the constructor --
	//     confirmed as a genuine inlining (not assumed) by independently
	//     disassembling `PerspectiveStageManager`'s own ctor (RVA
	//     0x3A0B50) and matching its field-store sequence and offsets
	//     exactly. The two `this` arguments fill different parameters
	//     (`IPerspectiveStageLoader loader` and `ICourtModifier
	//     courtModifier`) since `PerspectiveStagePresenter` implements
	//     both interfaces; `_screenController`/`_cameraController` supply
	//     `IPerspectiveStageScreenModifier`/`IPerspectiveStageCameraModifier`
	//     respectively (both classes declare those interfaces directly).
	//     `_stageParent.position` is confirmed via the cached-icall name
	//     string `"UnityEngine.Transform::get_position_Injected(...)"`
	//     read directly from the data section.
	//   - `ServiceLocator.Register<IPerspectiveStageManager>(...)` is a
	//     genuine tail call (`jmp`, not `call`) -- consistent with it
	//     being the last statement of this `void`-returning method calling
	//     another `void`-returning method (dump.cs confirms
	//     `Register<TService>(TService service)` returns `void`), so no
	//     return-value handling is needed.
	[Token(Token = "0x6000581")]
	[Address(RVA = "0x38D8D0", Offset = "0x38C2D0", VA = "0x18038D8D0")]
	private void Awake()
	{
		_screenController.Dismiss();
		_cameraController.Dismiss();
		_stageReferences = _stageData.ToDictionary((PerspectiveStageData data) => data.Kind, (PerspectiveStageData data) => data.Stage);
		_perspectiveStageManager = new PerspectiveStageManager(this, _screenController, _cameraController, this, _stageParent.position);
		ServiceLocator.Register(_perspectiveStageManager);
	}

	[Token(Token = "0x6000582")]
	[Address(RVA = "0x38DEF0", Offset = "0x38C8F0", VA = "0x18038DEF0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000583")]
	[Address(RVA = "0x38DF60", Offset = "0x38C960", VA = "0x18038DF60")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000584")]
	[Address(RVA = "0x38E0A0", Offset = "0x38CAA0", VA = "0x18038E0A0", Slot = "4")]
	[AsyncStateMachine(typeof(_003CLoadStageAsync_003Ed__13))]
	public UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000585")]
	[Address(RVA = "0x38E260", Offset = "0x38CC60", VA = "0x18038E260", Slot = "5")]
	public void UnloadStage()
	{
	}

	[Token(Token = "0x6000586")]
	[Address(RVA = "0x38E470", Offset = "0x38CE70", VA = "0x18038E470", Slot = "6")]
	public void ChangeFireFxActivity(bool active)
	{
	}

	[Token(Token = "0x6000587")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public PerspectiveStagePresenter()
	{
	}
}
