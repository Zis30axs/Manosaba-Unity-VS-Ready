using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000008")]
    [RequireComponent(typeof(CanvasGroup))]
    public class MovingArrow : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CMoveAsync_003Ed__13
    {
    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public MovingArrow _003C_003E4__this;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0xCD72A0", Offset = "0xCD5CA0", VA = "0x180CD72A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400000F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000010")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x4000011")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private RectTransform _imageRectTransform;

    	[Token(Token = "0x4000012")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _move = 24f;

    	[Token(Token = "0x4000013")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    	[SerializeField]
    	private float _duration = 0.4f;

    	[Token(Token = "0x4000014")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private float _interval = 1.2f;

    	[Token(Token = "0x4000015")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	private Vector2 _imageInitialPosition;

    	[Token(Token = "0x4000016")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private CancellationTokenSource _cts;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6BE0 -- [this+0x30].anchoredPosition stored to +0x44/+0x48
    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCD6BE0", Offset = "0xCD55E0", VA = "0x180CD6BE0")]
    	private void Awake()
    	{
    		_imageInitialPosition = _imageRectTransform.anchoredPosition;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6C20 -- GetComponent<CanvasGroup>() -> +0x20, GetComponent<Button>() -> +0x28
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCD6C20", Offset = "0xCD5620", VA = "0x180CD6C20")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_button = GetComponent<Button>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B6C0 (shared) -- CanvasGroupExtensions.Present (0xD53430) inlined on +0x20
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0x36B6C0", Offset = "0x36A0C0", VA = "0x18036B6C0")]
    	public void Present()
    	{
    		_canvasGroup.Present();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36B710 (shared) -- CanvasGroupExtensions.Dismiss (0xD53480) inlined on +0x20
    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x36B710", Offset = "0x36A110", VA = "0x18036B710")]
    	public void Dismiss()
    	{
    		_canvasGroup.Dismiss();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6D50 -- Present inlined (alpha 1 / interactable / blocksRaycasts on +0x20),
    	//   then the MoveAsync builder stub inlined with ct = destroyCancellationToken and Cysharp `Forget()`
    	// Annotated disassembly: work/title-path/disas/ma_05_0xCD6D50.txt
    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCD6D50", Offset = "0xCD5750", VA = "0x180CD6D50")]
    	public void Activate()
    	{
    		Present();
    		MoveAsync(base.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD6F30 (builder stub); MoveNext 0xCD72A0, three await states.
    	// The body lives in MoveAsyncCore (the repo's XxxCore convention; the <MoveAsync>d__13 metadata stays).
    	// - A previous `_cts` (+0x50) is cancelled and disposed; a fresh one is linked with `ct` (`<linkedCts>5__2`, using).
    	// - Loop while the linked source is not cancelled (inlined `_state >= 2` test at 0x180CD7C7A):
    	//   state 0 `UniTask.Delay(TimeSpan.Interval(_interval, 1000))` (DelayType 0, PlayerLoopTiming 8, no
    	//   cancelImmediately); state 1 LMotion x: initial.x -> initial.x - _move over _duration, Ease 5 (OutQuad);
    	//   state 2 back again with Ease 4 (InQuad); both BindToAnchoredPositionX(+0x30), AddTo(this), ToUniTask(linked).
    	// - The loop exit only disposes the linked source; `_cts` is left for Deactivate.
    	// Annotated disassembly: work/title-path/disas/ma_10_0xCD72A0.txt
    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCD6F30", Offset = "0xCD5930", VA = "0x180CD6F30")]
    	[AsyncStateMachine(typeof(_003CMoveAsync_003Ed__13))]
    	private UniTask MoveAsync([Optional] CancellationToken ct)
    	{
    		return MoveAsyncCore(ct);
    	}

    	private async UniTask MoveAsyncCore(CancellationToken ct)
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
    			await UniTask.Delay(TimeSpan.FromSeconds(_interval), Cysharp.Threading.Tasks.DelayType.DeltaTime, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, linkedCts.Token);
    			await LMotion.Create(_imageInitialPosition.x, _imageInitialPosition.x - _move, _duration).WithEase(Ease.OutQuad)
    				.BindToAnchoredPositionX(_imageRectTransform)
    				.AddTo(this)
    				.ToUniTask(linkedCts.Token);
    			await LMotion.Create(_imageInitialPosition.x - _move, _imageInitialPosition.x, _duration).WithEase(Ease.InQuad)
    				.BindToAnchoredPositionX(_imageRectTransform)
    				.AddTo(this)
    				.ToUniTask(linkedCts.Token);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD70E0 -- `_cts` (+0x50) cancelled (Cancel inlined: disposed check +
    	//   NotifyCancellation(false)), disposed and cleared; Dismiss inlined; anchoredPosition reset to +0x44/+0x48
    	// Annotated disassembly: work/title-path/disas/ma_07_0xCD70E0.txt
    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0xCD70E0", Offset = "0xCD5AE0", VA = "0x180CD70E0")]
    	public void Deactivate()
    	{
    		if (_cts != null)
    		{
    			_cts.Cancel();
    			_cts.Dispose();
    			_cts = null;
    		}
    		Dismiss();
    		_imageRectTransform.anchoredPosition = _imageInitialPosition;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD7210 -- [this+0x28]._button, [+0x100] m_OnClick, tail jump
    	//   UniRx UnityEventExtensions.AsObservable
    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0xCD7210", Offset = "0xCD5C10", VA = "0x180CD7210")]
    	public IObservable<Unit> OnClickAsObservable()
    	{
    		return _button.onClick.AsObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD7240 -- only the field initializers: _move (+0x38) = 24.0 (0x41C00000),
    	// _duration (+0x3C) = 0.4 (0x3ECCCCCD), _interval (+0x40) = 1.2 (0x3F99999A); then MonoBehaviour..ctor
    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xCD7240", Offset = "0xCD5C40", VA = "0x180CD7240")]
    	public MovingArrow()
    	{
    	}
    }
}
