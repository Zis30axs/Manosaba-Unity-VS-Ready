using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using LitMotion;
using System;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000C6")]
public class LoadingIndicatorRolling : LoadingIndicatorBase
{
	[StructLayout(3)]
	[Token(Token = "0x20000C7")]
	[CompilerGenerated]
	private struct _003CShowAsync_003Ed__7
{
		[Token(Token = "0x4000377")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000378")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000379")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LoadingIndicatorRolling _003C_003E4__this;

		[Token(Token = "0x400037A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400037B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400037C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002D1")]
		[Address(RVA = "0x351C00", Offset = "0x350600", VA = "0x180351C00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002D2")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000372")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x4000373")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private float _duration = 1f;

	[Token(Token = "0x4000374")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
	[SerializeField]
	private Ease _ease;

	[Token(Token = "0x4000375")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private float _delay;

	[Token(Token = "0x4000376")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private CancellationTokenSource _cts;

	// RECONSTRUCTED | Confidence: A | RVA: 0x3515C0, 260 B
	// LoadingIndicatorBase.Reset (0xD3BB40, `_canvasGroup = GetComponent<CanvasGroup>()`) is inlined first.
	[Token(Token = "0x60002CB")]
	[Address(RVA = "0x3515C0", Offset = "0x34FFC0", VA = "0x1803515C0", Slot = "4")]
	protected override void Reset()
	{
		base.Reset();
		_transform = base.transform;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3516D0, 424 B (ShowAsync builder stub inlined; Cysharp `Forget()`)
	[Token(Token = "0x60002CC")]
	[Address(RVA = "0x3516D0", Offset = "0x3500D0", VA = "0x1803516D0", Slot = "5")]
	public override void OnShow()
	{
		ShowAsync(base.destroyCancellationToken).Forget();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x351880 (builder stub); MoveNext 0x351C00, 3157 B, two await states
	// 
	// - A previous run is cancelled and disposed; a fresh `_cts` (+0x40) is linked with `ct` (`<linkedCts>5__2`, using).
	// - Loop while the linked source is not cancelled (inlined `_state >= 2` test): one LitMotion turn 0 -> 360 over
	//   `_duration` with `_ease`, scheduler MotionScheduler static +0x60 (`UpdateIgnoreTimeScale`), bound to SetRoll,
	//   AddTo(this), awaited via ToUniTask(linked token); then `UniTask.Delay(FromSeconds(_delay))` with the DelayType
	//   overload (0x180D710: DelayType 0, PlayerLoopTiming 8, cancelImmediately false).
	// - After a normal exit `_cts` is disposed and cleared. Cancellation during an await throws out of the loop, so that
	//   cleanup is skipped (OnHide already disposes and clears `_cts`); only the linked source is disposed.
	[Token(Token = "0x60002CD")]
	[Address(RVA = "0x351880", Offset = "0x350280", VA = "0x180351880")]
	[AsyncStateMachine(typeof(_003CShowAsync_003Ed__7))]
	private async UniTask ShowAsync([Optional] CancellationToken ct)
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
		}
		_cts = new CancellationTokenSource();
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
		while (!linkedCts.IsCancellationRequested)
		{
			await LMotion.Create(0f, 360f, _duration).WithEase(_ease).WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
				.Bind(SetRoll)
				.AddTo(this)
				.ToUniTask(linkedCts.Token);
			await UniTask.Delay(TimeSpan.FromSeconds(_delay), Cysharp.Threading.Tasks.DelayType.DeltaTime, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, linkedCts.Token);
		}
		if (_cts != null)
		{
			_cts.Dispose();
			_cts = null;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x351A30, 213 B (CancellationTokenSource.Cancel inlined: disposed check + NotifyCancellation(false))
	[Token(Token = "0x60002CE")]
	[Address(RVA = "0x351A30", Offset = "0x350430", VA = "0x180351A30", Slot = "6")]
	public override void OnHide()
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
			_cts = null;
		}
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (the angle computed into a local first) | RVA: 0x351B10, 147 B
	// Constants 180.0 (data RVA 0x2AE2C08) and 360.0 (0x2AE2C40); negation via the sign mask at 0x2AE3A40. Only the z
	// Euler angle is replaced, so the indicator turns clockwise (0, -1 ... -179.x, then 180 ... 0).
	[Token(Token = "0x60002CF")]
	[Address(RVA = "0x351B10", Offset = "0x350510", VA = "0x180351B10")]
	private void SetRoll(float value)
	{
		float z = ((value < 180f) ? (0f - value) : (360f - value));
		Vector3 localEulerAngles = _transform.localEulerAngles;
		_transform.localEulerAngles = new Vector3(localEulerAngles.x, localEulerAngles.y, z);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x351BB0, 74 B (only the `_duration = 1f` field initializer, +0x30)
	[Token(Token = "0x60002D0")]
	[Address(RVA = "0x351BB0", Offset = "0x3505B0", VA = "0x180351BB0")]
	public LoadingIndicatorRolling()
	{
	}
}
