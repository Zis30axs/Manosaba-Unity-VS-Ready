using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Adapters;
using LitMotion.Extensions;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000131")]
    public class StageCameraContainer : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000132")]
    	[CompilerGenerated]
    	private struct _003CModifyDistanceAsync_003Ed__7
    {
    		[Token(Token = "0x40005BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float distance;

    		[Token(Token = "0x40005C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float duration;

    		[Token(Token = "0x40005C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Ease ease;

    		[Token(Token = "0x40005C5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600052A")]
    		[Address(RVA = "0x3822D0", Offset = "0x380CD0", VA = "0x1803822D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600052B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000133")]
    	[CompilerGenerated]
    	private struct _003CModifyHeightAsync_003Ed__5
    {
    		[Token(Token = "0x40005C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005C8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005C9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005CA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float height;

    		[Token(Token = "0x40005CB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float duration;

    		[Token(Token = "0x40005CC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public Ease ease;

    		[Token(Token = "0x40005CD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005CE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600052C")]
    		[Address(RVA = "0x382770", Offset = "0x381170", VA = "0x180382770", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600052D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000134")]
    	[CompilerGenerated]
    	private struct _003CModifyRotationAsync_003Ed__6
    {
    		[Token(Token = "0x40005CF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005D0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005D1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public float?[] rotation;

    		[Token(Token = "0x40005D2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005D3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float duration;

    		[Token(Token = "0x40005D4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public Ease ease;

    		[Token(Token = "0x40005D5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005D6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600052E")]
    		[Address(RVA = "0x382C10", Offset = "0x381610", VA = "0x180382C10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600052F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000135")]
    	[CompilerGenerated]
    	private struct _003CShakeCameraXAsync_003Ed__8
    {
    		[Token(Token = "0x40005D7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005D8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005D9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool randomly;

    		[Token(Token = "0x40005DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float x;

    		[Token(Token = "0x40005DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float duration;

    		[Token(Token = "0x40005DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public int frequency;

    		[Token(Token = "0x40005DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float dampingRatio;

    		[Token(Token = "0x40005DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000530")]
    		[Address(RVA = "0x3838E0", Offset = "0x3822E0", VA = "0x1803838E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000531")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000136")]
    	[CompilerGenerated]
    	private struct _003CShakeCameraYAsync_003Ed__9
    {
    		[Token(Token = "0x40005E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool randomly;

    		[Token(Token = "0x40005E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float y;

    		[Token(Token = "0x40005E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float duration;

    		[Token(Token = "0x40005E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public int frequency;

    		[Token(Token = "0x40005E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float dampingRatio;

    		[Token(Token = "0x40005E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000532")]
    		[Address(RVA = "0x383D40", Offset = "0x382740", VA = "0x180383D40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000533")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000137")]
    	[CompilerGenerated]
    	private struct _003CShakeCameraZAsync_003Ed__10
    {
    		[Token(Token = "0x40005EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40005EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40005ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public StageCameraContainer _003C_003E4__this;

    		[Token(Token = "0x40005EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool randomly;

    		[Token(Token = "0x40005EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float z;

    		[Token(Token = "0x40005F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float duration;

    		[Token(Token = "0x40005F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public int frequency;

    		[Token(Token = "0x40005F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float dampingRatio;

    		[Token(Token = "0x40005F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x40005F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000534")]
    		[Address(RVA = "0x3841A0", Offset = "0x382BA0", VA = "0x1803841A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000535")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40005BC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x40005BD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Camera _camera;

    	[Token(Token = "0x40005BE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Transform _cameraTransform;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x380FA0, 361 B
    	// Editor-only wiring, in store order: `transform` -> +0x20 `_transform`,
    	// `GetComponentInChildren<Camera>()` -> +0x28 `_camera`, then the camera's
    	// `transform` -> +0x30 `_cameraTransform`. The last read reloads +0x28 with
    	// an explicit NRE throw, so Reset fails if no child Camera exists.
    	[Token(Token = "0x600051F")]
    	[Address(RVA = "0x380FA0", Offset = "0x37F9A0", VA = "0x180380FA0")]
    	private void Reset()
    	{
    		_transform = transform;
    		_camera = GetComponentInChildren<Camera>();
    		_cameraTransform = _camera.transform;
    	}

    	// Body (RVA 0x3407C0, 30 B, folded with `CreditRoll.SetCanvasEnabled`) is
    	// `[rcx+0x28]` (`_camera`) null-checked, then a tail jump to
    	// `Behaviour.set_enabled(enable)`. A shared tiny body in ranked.json's
    	// `aliased` list, so not counted as progress.
    	[Token(Token = "0x6000520")]
    	[Address(RVA = "0x3407C0", Offset = "0x33F1C0", VA = "0x1803407C0")]
    	public void SetCameraEnabled(bool enable)
    	{
    		_camera.enabled = enable;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x381110, 475 B (entry) + MoveNext 0x382770
    	// Single await.
    	// - `from` is `_transform.localPosition` (+0x20): the resolved icall
    	//   `Transform::get_localPosition_Injected` (name string at 0x26098D0).
    	// - `to` is `(0f, height, 0f)`: x and z are hard zero, so x/z drift on the
    	//   container transform is not preserved.
    	// - The tween is `LMotion.Create<Vector3,...>(from, to, duration)` ->
    	//   `WithEase(ease)` -> `BindToLocalPosition(_transform)` ->
    	//   `MotionHandleExtensions.AddTo(handle, this)` -> `ToUniTask(ct)`.
    	[Token(Token = "0x6000521")]
    	[Address(RVA = "0x381110", Offset = "0x37FB10", VA = "0x180381110")]
    	[AsyncStateMachine(typeof(_003CModifyHeightAsync_003Ed__5))]
    	public async UniTask ModifyHeightAsync(float height, Ease ease, float duration, [Optional] CancellationToken ct)
    	{
    		await LMotion.Create(_transform.localPosition, new Vector3(0f, height, 0f), duration)
    			.WithEase(ease)
    			.BindToLocalPosition(_transform)
    			.AddTo(this)
    			.ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3812F0, 555 B (entry) + MoveNext 0x382C10
    	// The rotation is split across the two transforms.
    	// - Yaw goes to the container: component 1 of `rotation` (via
    	//   `ElementAtOrDefault`), or when unset `_transform.localRotation` (icall
    	//   `Transform::get_localRotation_Injected`) `.eulerAngles.y`. It becomes
    	//   `Quaternion.Euler(0f, yaw, 0f)`: the inlined body is a multiply by rdata
    	//   0x2AE25B4 = 0.0174533 (Deg2Rad) feeding icall
    	//   `Quaternion::Internal_FromEulerRad_Injected`.
    	// - Pitch (component 0) and roll (component 2) go to the camera, each
    	//   falling back to the matching `_cameraTransform.localRotation.eulerAngles`
    	//   component, as `Quaternion.Euler(pitch, 0f, roll)`.
    	// - Two tweens are built in that order, each `LMotion.Create<Quaternion>(
    	//   current localRotation, target, duration).WithEase(ease)
    	//   .BindToLocalRotation(transform).AddTo(this).ToUniTask(ct)`, on
    	//   `_transform` then `_cameraTransform`.
    	// - They are packed into a `ValueTuple<UniTask, UniTask>` and awaited once
    	//   through Cysharp `UniTaskExtensions.GetAwaiter((UniTask, UniTask))`.
    	[Token(Token = "0x6000522")]
    	[Address(RVA = "0x3812F0", Offset = "0x37FCF0", VA = "0x1803812F0")]
    	[AsyncStateMachine(typeof(_003CModifyRotationAsync_003Ed__6))]
    	public async UniTask ModifyRotationAsync(float?[] rotation, Ease ease, float duration, [Optional] CancellationToken ct)
    	{
    		Quaternion containerRotation = Quaternion.Euler(0f, rotation.ElementAtOrDefault(1) ?? _transform.localRotation.eulerAngles.y, 0f);
    		Quaternion cameraRotation = Quaternion.Euler(
    			rotation.ElementAtOrDefault(0) ?? _cameraTransform.localRotation.eulerAngles.x,
    			0f,
    			rotation.ElementAtOrDefault(2) ?? _cameraTransform.localRotation.eulerAngles.z);
    		UniTask containerTask = LMotion.Create(_transform.localRotation, containerRotation, duration)
    			.WithEase(ease)
    			.BindToLocalRotation(_transform)
    			.AddTo(this)
    			.ToUniTask(ct);
    		UniTask cameraTask = LMotion.Create(_cameraTransform.localRotation, cameraRotation, duration)
    			.WithEase(ease)
    			.BindToLocalRotation(_cameraTransform)
    			.AddTo(this)
    			.ToUniTask(ct);
    		await (containerTask, cameraTask);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x381520, 475 B (entry) + MoveNext 0x3822D0
    	// Byte-for-byte the same shape as `ModifyHeightAsync`, but on
    	// `_cameraTransform` (+0x30) with `to = (0f, 0f, distance)`: the dolly is the
    	// camera's local z, again with hard-zero x/y.
    	[Token(Token = "0x6000523")]
    	[Address(RVA = "0x381520", Offset = "0x37FF20", VA = "0x180381520")]
    	[AsyncStateMachine(typeof(_003CModifyDistanceAsync_003Ed__7))]
    	public async UniTask ModifyDistanceAsync(float distance, Ease ease, float duration, [Optional] CancellationToken ct)
    	{
    		await LMotion.Create(_cameraTransform.localPosition, new Vector3(0f, 0f, distance), duration)
    			.WithEase(ease)
    			.BindToLocalPosition(_cameraTransform)
    			.AddTo(this)
    			.ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x381700, 508 B (entry) + MoveNext 0x3838E0
    	// Single await.
    	// - `_cameraTransform.localPosition.x` is the motion's start value
    	//   (localPosition via the same `get_localPosition_Injected` icall).
    	// - `randomly` (+0x20 in the state machine) selects
    	//   `CreateShakeMotion(start, x, duration, frequency, dampingRatio)` bound
    	//   with `BindToLocalPositionX<ShakeOptions, FloatShakeMotionAdapter>`, else
    	//   `CreatePunchMotion(...)` with the same arguments and the Punch binding.
    	//   Both bind to `_cameraTransform`.
    	// - The resulting handle then goes through `AddTo(this)` and `ToUniTask(ct)`.
    	// The two builder types differ, so each branch binds on its own.
    	[Token(Token = "0x6000524")]
    	[Address(RVA = "0x381700", Offset = "0x380100", VA = "0x180381700")]
    	[AsyncStateMachine(typeof(_003CShakeCameraXAsync_003Ed__8))]
    	public async UniTask ShakeCameraXAsync(float x, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
    	{
    		float start = _cameraTransform.localPosition.x;
    		MotionHandle handle = randomly
    			? CreateShakeMotion(start, x, duration, frequency, dampingRatio).BindToLocalPositionX(_cameraTransform)
    			: CreatePunchMotion(start, x, duration, frequency, dampingRatio).BindToLocalPositionX(_cameraTransform);
    		await handle.AddTo(this).ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x381900, 508 B (entry) + MoveNext 0x383D40
    	// Same as `ShakeCameraXAsync` on the y axis: start from localPosition +4
    	// (`movss xmm1, [rsp+0x54]`), bound with `BindToLocalPositionY`.
    	[Token(Token = "0x6000525")]
    	[Address(RVA = "0x381900", Offset = "0x380300", VA = "0x180381900")]
    	[AsyncStateMachine(typeof(_003CShakeCameraYAsync_003Ed__9))]
    	public async UniTask ShakeCameraYAsync(float y, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
    	{
    		float start = _cameraTransform.localPosition.y;
    		MotionHandle handle = randomly
    			? CreateShakeMotion(start, y, duration, frequency, dampingRatio).BindToLocalPositionY(_cameraTransform)
    			: CreatePunchMotion(start, y, duration, frequency, dampingRatio).BindToLocalPositionY(_cameraTransform);
    		await handle.AddTo(this).ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x381B00, 508 B (entry) + MoveNext 0x3841A0
    	// Same as `ShakeCameraXAsync` on the z axis: start from localPosition +8
    	// (`movss xmm1, [rsp+0x58]`), bound with `BindToLocalPositionZ`.
    	[Token(Token = "0x6000526")]
    	[Address(RVA = "0x381B00", Offset = "0x380500", VA = "0x180381B00")]
    	[AsyncStateMachine(typeof(_003CShakeCameraZAsync_003Ed__10))]
    	public async UniTask ShakeCameraZAsync(float z, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
    	{
    		float start = _cameraTransform.localPosition.z;
    		MotionHandle handle = randomly
    			? CreateShakeMotion(start, z, duration, frequency, dampingRatio).BindToLocalPositionZ(_cameraTransform)
    			: CreatePunchMotion(start, z, duration, frequency, dampingRatio).BindToLocalPositionZ(_cameraTransform);
    		await handle.AddTo(this).ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x381D00, 664 B
    	// A direct call to `LMotion.Shake.Create(start, strength, duration)`, then
    	// the inlined `MotionBuilderExtensions.WithFrequency<float,
    	// FloatShakeMotionAdapter>` and `WithDampingRatio<...>`. Those inlined bodies
    	// write `frequency` and then `dampingRatio` into the builder's pooled
    	// options buffer.
    	[Token(Token = "0x6000527")]
    	[Address(RVA = "0x381D00", Offset = "0x380700", VA = "0x180381D00")]
    	private static MotionBuilder<float, ShakeOptions, FloatShakeMotionAdapter> CreateShakeMotion(float start, float strength, float duration, int frequency, float dampingRatio)
    	{
    		return LMotion.Shake.Create(start, strength, duration)
    			.WithFrequency(frequency)
    			.WithDampingRatio(dampingRatio);
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (Punch.Create spelling) | RVA: 0x381FA0, 814 B
    	// `LMotion.Create<float, PunchOptions, FloatPunchMotionAdapter>(from, to,
    	// duration)`, then `WithOptions` with the literal options {Frequency = 10,
    	// DampingRatio = 1f} (`mov [rsp+0xa0], 0xA` / `mov [rsp+0xa4], 0x3f800000`).
    	// That pair is the inlined body of `LMotion.Punch.Create`, which applies
    	// the default punch options, so the source spelling is B. After it, the
    	// inlined `WithFrequency(frequency)` and `WithDampingRatio(dampingRatio)`
    	// overwrite both defaults, exactly as in `CreateShakeMotion`.
    	[Token(Token = "0x6000528")]
    	[Address(RVA = "0x381FA0", Offset = "0x3809A0", VA = "0x180381FA0")]
    	private static MotionBuilder<float, PunchOptions, FloatPunchMotionAdapter> CreatePunchMotion(float from, float to, float duration, int frequency, float dampingRatio)
    	{
    		return LMotion.Punch.Create(from, to, duration)
    			.WithFrequency(frequency)
    			.WithDampingRatio(dampingRatio);
    	}

    	[Token(Token = "0x6000529")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public StageCameraContainer()
    	{
    	}
    }
}
