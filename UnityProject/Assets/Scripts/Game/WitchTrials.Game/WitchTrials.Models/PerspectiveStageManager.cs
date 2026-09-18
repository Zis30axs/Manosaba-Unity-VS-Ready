using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000191")]
    public class PerspectiveStageManager : IPerspectiveStageManager, IPerspectiveStageLoader, IPerspectiveStageScreenModifier, IPerspectiveStageCameraModifier, ICourtModifier, IService
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000192")]
    	[CompilerGenerated]
    	private struct _003CChangeScreenVisibilityAsync_003Ed__10
    {
    		[Token(Token = "0x4000758")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000759")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400075A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PerspectiveStageManager _003C_003E4__this;

    		[Token(Token = "0x400075B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int screenIndex;

    		[Token(Token = "0x400075C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public bool visible;

    		[Token(Token = "0x400075D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float duration;

    		[Token(Token = "0x400075E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x400075F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60006FA")]
    		[Address(RVA = "0x3A1640", Offset = "0x3A0040", VA = "0x1803A1640", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60006FB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000193")]
    	[CompilerGenerated]
    	private struct _003CLoadStageAsync_003Ed__8
    {
    		[Token(Token = "0x4000760")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000761")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000762")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PerspectiveStageManager _003C_003E4__this;

    		[Token(Token = "0x4000763")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public PerspectiveStageKind kind;

    		[Token(Token = "0x4000764")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000765")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60006FC")]
    		[Address(RVA = "0x3A1950", Offset = "0x3A0350", VA = "0x1803A1950", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60006FD")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000194")]
    	[CompilerGenerated]
    	private struct _003CModifyCameraAsync_003Ed__12
    {
    		[Token(Token = "0x4000766")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000767")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000768")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PerspectiveStageManager _003C_003E4__this;

    		[Token(Token = "0x4000769")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int cameraIndex;

    		[Token(Token = "0x400076A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ModifyCameraHeightTween heightTween;

    		[Token(Token = "0x400076B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public ModifyCameraRotationTween rotationTween;

    		[Token(Token = "0x400076C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public ModifyCameraDistanceTween distanceTween;

    		[Token(Token = "0x400076D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public CancellationToken ct;

    		[Token(Token = "0x400076E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60006FE")]
    		[Address(RVA = "0x3A1C60", Offset = "0x3A0660", VA = "0x1803A1C60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60006FF")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000195")]
    	[CompilerGenerated]
    	private struct _003CShakeCameraAsync_003Ed__13
    {
    		[Token(Token = "0x400076F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000770")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000771")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PerspectiveStageManager _003C_003E4__this;

    		[Token(Token = "0x4000772")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public int cameraIndex;

    		[Token(Token = "0x4000773")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public Vector3 power;

    		[Token(Token = "0x4000774")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float duration;

    		[Token(Token = "0x4000775")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    		public int frequency;

    		[Token(Token = "0x4000776")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public float dampingRatio;

    		[Token(Token = "0x4000777")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    		public bool randomly;

    		[Token(Token = "0x4000778")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000779")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000700")]
    		[Address(RVA = "0x3A1F80", Offset = "0x3A0980", VA = "0x1803A1F80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000701")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000753")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly IPerspectiveStageLoader _loader;

    	[Token(Token = "0x4000754")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly IPerspectiveStageScreenModifier _screenModifier;

    	[Token(Token = "0x4000755")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly IPerspectiveStageCameraModifier _cameraModifier;

    	[Token(Token = "0x4000756")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly ICourtModifier _courtModifier;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0B30, 19 B (shared trampoline, +3 aliases)
    	// Get-only auto-property; backing storage read directly (`[this+0x30]`/
    	// `[this+0x38]`, the 12-byte `Vector3` written by the constructor below).
    	// Same shared "return this Vector3-shaped field" trampoline body already
    	// documented for two unrelated getters elsewhere in the binary
    	// (`UnityEngine.LightAnchor::get_anchorPositionOffset`,
    	// `UnityEngine.XR.MeshGenerationResult::get_Position`) -- confirmed by
    	// the disassembler resolving all three names to the identical RVA.
    	[Token(Token = "0x17000128")]
    	public Vector3 StageOffset
    	{
    		[Token(Token = "0x60006F1")]
    		[Address(RVA = "0x3A0B30", Offset = "0x39F530", VA = "0x1803A0B30", Slot = "4")]
    		[CompilerGenerated]
    		get;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0B50, 358 B
    	// Plain field-by-field store of the four injected interfaces plus the
    	// `Vector3 stageOffset` argument into the `StageOffset` auto-property's
    	// backing field (a get-only auto-property can be assigned directly from
    	// its own constructor at the source level -- this is exactly that, not
    	// a separate hidden field). No other logic.
    	[Token(Token = "0x60006F2")]
    	[Address(RVA = "0x3A0B50", Offset = "0x39F550", VA = "0x1803A0B50")]
    	public PerspectiveStageManager(IPerspectiveStageLoader loader, IPerspectiveStageScreenModifier screenModifier, IPerspectiveStageCameraModifier cameraModifier, ICourtModifier courtModifier, Vector3 stageOffset)
    	{
    		_loader = loader;
    		_screenModifier = screenModifier;
    		_cameraModifier = cameraModifier;
    		_courtModifier = courtModifier;
    		StageOffset = stageOffset;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0CC0 (entry) + 0x3A1950 (MoveNext, 781 B)
    	//
    	// Confirms the wiring diagram from `PerspectiveStagePresenter::Awake`:
    	// every method on this class is a thin single-await forward to the
    	// matching sub-interface field, resolved through the same shared
    	// "cache interface-slot lookup, then call/tail-call the resolved
    	// VirtualInvokeData" trampolines used project-wide (the universal
    	// vtable-base-0x138 pattern) -- NOT reimplemented logic. Verified here
    	// by tracing the exact register/stack provenance of every argument
    	// through the trampoline (`sub_3D50`) into the resolved call, and by
    	// confirming the interface member index the trampoline resolves
    	// (`r8d = 0`) against `IPerspectiveStageLoader`'s declaration order
    	// (`LoadStageAsync` is declared first).
    	[Token(Token = "0x60006F3")]
    	[Address(RVA = "0x3A0CC0", Offset = "0x39F6C0", VA = "0x1803A0CC0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CLoadStageAsync_003Ed__8))]
    	public async UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct)
    	{
    		await _loader.LoadStageAsync(kind, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0E70, 78 B
    	// Zero-arg tail-call forward: `mov ecx, 1` selects the interface-slot
    	// cache lookup for `IPerspectiveStageLoader`'s 2nd declared member
    	// (0-indexed) -- `UnloadStage`, matching declaration order in
    	// `IPerspectiveStageLoader.cs`.
    	[Token(Token = "0x60006F4")]
    	[Address(RVA = "0x3A0E70", Offset = "0x39F870", VA = "0x1803A0E70", Slot = "6")]
    	public void UnloadStage()
    	{
    		_loader.UnloadStage();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0EC0 (entry) + 0x3A1640 (MoveNext, 769 B)
    	// Same thin-forward shape as `LoadStageAsync`, targeting `_screenModifier`
    	// via a differently-shaped shared trampoline (`sub_3EB0`, hardcoded to
    	// `IPerspectiveStageScreenModifier` + method index 0 = this method's
    	// declared position). All four arguments' register/stack provenance
    	// traced through to the resolved call in matching positional order.
    	[Token(Token = "0x60006F5")]
    	[Address(RVA = "0x3A0EC0", Offset = "0x39F8C0", VA = "0x1803A0EC0", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CChangeScreenVisibilityAsync_003Ed__10))]
    	public async UniTask ChangeScreenVisibilityAsync(int screenIndex, bool visible, float duration, [Optional] CancellationToken ct)
    	{
    		await _screenModifier.ChangeScreenVisibilityAsync(screenIndex, visible, duration, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A10A0, 142 B
    	// `mov ecx, 1` selects `IPerspectiveStageScreenModifier`'s 2nd declared
    	// member (0-indexed) -- `SplitScreen`. All four arguments (int, float,
    	// float, enum) traced through the generic tail-call trampoline
    	// (`sub_3E00`) in declared order.
    	[Token(Token = "0x60006F6")]
    	[Address(RVA = "0x3A10A0", Offset = "0x39FAA0", VA = "0x1803A10A0", Slot = "8")]
    	public void SplitScreen(int screenIndex, float normalizedPosX, float roll, ScreenSplitDirection direction)
    	{
    		_screenModifier.SplitScreen(screenIndex, normalizedPosX, roll, direction);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A1130 (entry) + 0x3A1C60 (MoveNext, 786 B)
    	// Same thin-forward shape, targeting `_cameraModifier` via a hardcoded
    	// trampoline (`sub_3C80`, `IPerspectiveStageCameraModifier` + method
    	// index 0 = this method's declared position). All five arguments'
    	// register/stack provenance traced through the trampoline's own local
    	// frame math into the resolved call in matching positional order. This
    	// is not merely a plausible target: `PerspectiveStagePresenter::Awake`
    	// (already reconstructed) constructs this manager as `new
    	// PerspectiveStageManager(this, _screenController, _cameraController,
    	// this, _stageParent.position)` -- so `_cameraModifier` at runtime IS
    	// `_cameraController`, a `StageCameraController` (its `ModifyCameraAsync`
    	// reconstructed earlier this session), and `_loader`/`_courtModifier`
    	// are both the presenter itself. This closes a complete, non-hypothetical
    	// call chain end to end: `PerspectiveStageManager.ModifyCameraAsync` →
    	// `StageCameraController.ModifyCameraAsync`.
    	[Token(Token = "0x60006F7")]
    	[Address(RVA = "0x3A1130", Offset = "0x39FB30", VA = "0x1803A1130", Slot = "9")]
    	[AsyncStateMachine(typeof(_003CModifyCameraAsync_003Ed__12))]
    	public async UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct)
    	{
    		await _cameraModifier.ModifyCameraAsync(cameraIndex, heightTween, rotationTween, distanceTween, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A13E0 (entry) + 0x3A1F80 (MoveNext, 864 B)
    	// Same thin-forward shape via a differently-shaped hardcoded trampoline
    	// (`sub_3B50`, `IPerspectiveStageCameraModifier` + method index 1 =
    	// `ShakeCameraAsync`'s declared position). `Vector3 power` is passed by
    	// pointer to a local copy (matches the ctor's own by-pointer `Vector3`
    	// handling). Fully traced this session (not inferred by family
    	// resemblance to `sub_3C80`): every one of the seven forwarded values'
    	// source offset inside `sub_3B50`'s own stack frame was mapped back to
    	// the caller's frame via the frame-size arithmetic (`push rdi; sub rsp,
    	// 0x70` => callee `[rsp+X]` = caller `[rsp+(X-0x80)]`), and each landed
    	// on exactly the field the caller loaded for it (`power` reconstructed
    	// from two reads through the passed-by-pointer local copy, the other
    	// five read straight through) -- same Confidence-A grade as
    	// `ModifyCameraAsync` above, via the same underlying `_cameraModifier`
    	// identity (`StageCameraController`, see that method's comment).
    	[Token(Token = "0x60006F8")]
    	[Address(RVA = "0x3A13E0", Offset = "0x39FDE0", VA = "0x1803A13E0", Slot = "10")]
    	[AsyncStateMachine(typeof(_003CShakeCameraAsync_003Ed__13))]
    	public async UniTask ShakeCameraAsync(int cameraIndex, Vector3 power, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
    	{
    		await _cameraModifier.ShakeCameraAsync(cameraIndex, power, duration, frequency, dampingRatio, randomly, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A15E0, 91 B
    	// `xor ecx, ecx` selects `ICourtModifier`'s only declared member (index
    	// 0) -- `ChangeFireFxActivity`. Tail-call forward, `active` traced
    	// through unchanged.
    	[Token(Token = "0x60006F9")]
    	[Address(RVA = "0x3A15E0", Offset = "0x39FFE0", VA = "0x1803A15E0", Slot = "11")]
    	public void ChangeFireFxActivity(bool active)
    	{
    		_courtModifier.ChangeFireFxActivity(active);
    	}
    }
}
