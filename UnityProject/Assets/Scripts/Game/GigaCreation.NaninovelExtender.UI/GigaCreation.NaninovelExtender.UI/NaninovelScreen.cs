using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000022")]
    [RequireComponent(typeof(CustomUI))]
    public class NaninovelScreen : MonoBehaviour, ICancelEventBlockable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000023")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__30
    {
    		[Token(Token = "0x4000085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public NaninovelScreen _003C_003E4__this;

    		[Token(Token = "0x4000088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float? duration;

    		[Token(Token = "0x400008A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private NaninovelInteractionBlocker _003C_003E7__wrap1;

    		[Token(Token = "0x400008B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__3;

    		[Token(Token = "0x400008C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400008D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000093")]
    		[Address(RVA = "0xD3DC00", Offset = "0xD3C600", VA = "0x180D3DC00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000094")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000024")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__27
    {
    		[Token(Token = "0x400008E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400008F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000090")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public NaninovelScreen _003C_003E4__this;

    		[Token(Token = "0x4000091")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000092")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float? duration;

    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000095")]
    		[Address(RVA = "0xD3E7C0", Offset = "0xD3D1C0", VA = "0x180D3E7C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000096")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000078")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CustomUI _customUI;

    	[Token(Token = "0x4000079")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _closeButton;

    	[Token(Token = "0x400007A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _enableCancelAction;

    	[Token(Token = "0x400007B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private UnityEvent _onBeginToPresent;

    	[Token(Token = "0x400007C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private UnityEvent _onEndToPresent;

    	[Token(Token = "0x400007D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private UnityEvent _onBeginToDismiss;

    	[Token(Token = "0x400007E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private UnityEvent _onEndToDismiss;

    	[Token(Token = "0x400007F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000080")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private Naninovel.IInputManager _naniInputManager;

    	[Token(Token = "0x4000081")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private IDisposable _closeButtonDisposable;

    	[Token(Token = "0x4000082")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private CancellationTokenSource _presentCts;

    	[Token(Token = "0x4000083")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private CancellationTokenSource _dismissCts;

    	[Token(Token = "0x17000012")]
    	public bool IsCancelEventBlocked
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x492EF0 / 0x492F00 -- [CompilerGenerated] auto-property, backing field +0x80
    		[Token(Token = "0x6000078")]
    		[Address(RVA = "0x492EF0", Offset = "0x4918F0", VA = "0x180492EF0")]
    		[CompilerGenerated]
    		private get;
    		[Token(Token = "0x6000079")]
    		[Address(RVA = "0x492F00", Offset = "0x491900", VA = "0x180492F00", Slot = "5")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x17000013")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3C6E0 -- lazy: [this+0x58], else ServiceLocator.Get<IInputManager>(cl = 1)
    		[Token(Token = "0x600007A")]
    		[Address(RVA = "0xD3C6E0", Offset = "0xD3B0E0", VA = "0x180D3C6E0")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	[Token(Token = "0x17000014")]
    	private Naninovel.IInputManager NaniInputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD3C7A0 -- lazy: [this+0x60], else Engine.GetServiceOrErr<Naninovel.IInputManager>()
    		[Token(Token = "0x600007B")]
    		[Address(RVA = "0xD3C7A0", Offset = "0xD3B1A0", VA = "0x180D3C7A0")]
    		get
    		{
    			if (_naniInputManager == null)
    			{
    				_naniInputManager = Engine.GetServiceOrErr<Naninovel.IInputManager>();
    			}
    			return _naniInputManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C860 -- _customUI (+0x20) vtable +0x2A8 = ScriptableUIBehaviour slot 23
    	//   VisibleOnAwake, read once per call
    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0xD3C860", Offset = "0xD3B260", VA = "0x180D3C860", Slot = "6")]
    	protected virtual void Awake()
    	{
    		SetCanvasesEnabled(_customUI.VisibleOnAwake);
    		SetGraphicRaycastersEnabled(_customUI.VisibleOnAwake);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3C8D0 -- field-like event add inlined (Delegate.Combine + CompareExchange loop
    	//   on the CustomUI's OnVisibilityChanged backing field)
    	[Token(Token = "0x600007D")]
    	[Address(RVA = "0xD3C8D0", Offset = "0xD3B2D0", VA = "0x180D3C8D0")]
    	private void OnEnable()
    	{
    		_customUI.OnVisibilityChanged += OnVisibilityChangedByCustomUi;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CA40 -- field-like event remove inlined (Delegate.Remove loop)
    	[Token(Token = "0x600007E")]
    	[Address(RVA = "0xD3CA40", Offset = "0xD3B440", VA = "0x180D3CA40")]
    	private void OnDisable()
    	{
    		_customUI.OnVisibilityChanged -= OnVisibilityChangedByCustomUi;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CBB0 -- GetComponent<CustomUI>() -> +0x20
    	[Token(Token = "0x600007F")]
    	[Address(RVA = "0xD3CBB0", Offset = "0xD3B5B0", VA = "0x180D3CBB0", Slot = "7")]
    	protected virtual void Reset()
    	{
    		_customUI = GetComponent<CustomUI>();
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (calls vs. the inlined copies) | RVA: 0xD3CC50 -- both branches are
    	//   Present / Dismiss inlined (Present/DismissAsync(null, destroyCancellationToken)) with a shared Forget()
    	[Token(Token = "0x6000080")]
    	[Address(RVA = "0xD3CC50", Offset = "0xD3B650", VA = "0x180D3CC50")]
    	private void OnVisibilityChangedByCustomUi(bool visible)
    	{
    		if (visible)
    		{
    			Present();
    		}
    		else
    		{
    			Dismiss();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CCC0 -- duration null (r8 = 0), ct = destroyCancellationToken, Cysharp Forget()
    	[Token(Token = "0x6000081")]
    	[Address(RVA = "0xD3CCC0", Offset = "0xD3B6C0", VA = "0x180D3CCC0")]
    	private void Present()
    	{
    		PresentAsync(null, base.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CD10 -- duration null (r8 = 0), ct = destroyCancellationToken, Cysharp Forget()
    	[Token(Token = "0x6000082")]
    	[Address(RVA = "0xD3CD10", Offset = "0xD3B710", VA = "0x180D3CD10")]
    	private void Dismiss()
    	{
    		DismissAsync(null, base.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CD60 (builder stub); MoveNext 0xD3E7C0, two await states.
    	// The body lives in PresentAsyncCore (XxxCore convention; <PresentAsync>d__27 metadata stays):
    	// - Returns at once while _presentCts (+0x70) is set; a running dismiss (_dismissCts +0x78) is cancelled and cleared.
    	// - BeginToPresent (vtable +0x1B8, slot 8); a fresh _presentCts linked with ct (<linkedCts>5__2, using).
    	// - Hidden (ScriptableUIBehaviour slot 28 Visible false): slot 44 ChangeVisibility(true, duration, linked token)
    	//   awaited (Naninovel UniTask, state 1).  Already visible: UniTask.WaitWhile(<PresentAsync>b__27_0 = slot 30
    	//   Opacity < 1.0, PlayerLoopTiming 8, linked token), state 0.
    	// - EndToPresent (slot 9), then _presentCts disposed and cleared.  A cancelled await skips that (DismissAsync
    	//   clears it).
    	// Annotated disassembly: work/title-path/disas/ns_15_PresentAsync_MoveNext.txt
    	[Token(Token = "0x6000083")]
    	[Address(RVA = "0xD3CD60", Offset = "0xD3B760", VA = "0x180D3CD60")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__27))]
    	public Cysharp.Threading.Tasks.UniTask PresentAsync([Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return PresentAsyncCore(duration, ct);
    	}

    	private async Cysharp.Threading.Tasks.UniTask PresentAsyncCore(float? duration, CancellationToken ct)
    	{
    		// Re-entrancy guard of the binary (0x180D3E8B3 cmp [rsi+0x70], 0 / jne to the end): a present already running.
    		if (_presentCts != null)
    		{
    			return;
    		}
    		if (_dismissCts != null)
    		{
    			_dismissCts.Cancel();
    			_dismissCts.Dispose();
    			_dismissCts = null;
    		}
    		BeginToPresent();
    		_presentCts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_presentCts.Token, ct);
    		if (!_customUI.Visible)
    		{
    			await _customUI.ChangeVisibility(true, duration, linkedCts.Token);
    		}
    		else
    		{
    			await Cysharp.Threading.Tasks.UniTask.WaitWhile(() => _customUI.Opacity < 1f, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, linkedCts.Token);
    		}
    		EndToPresent();
    		_presentCts.Dispose();
    		_presentCts = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CF30 -- raycasters off, canvases on, _onBeginToPresent (+0x38) Invoke
    	[Token(Token = "0x6000084")]
    	[Address(RVA = "0xD3CF30", Offset = "0xD3B930", VA = "0x180D3CF30", Slot = "8")]
    	protected virtual void BeginToPresent()
    	{
    		SetGraphicRaycastersEnabled(false);
    		SetCanvasesEnabled(true);
    		// Shipped null test: 0x180D3CF5x test rcx, rcx / je past the Invoke.
    		if (_onBeginToPresent != null)
    		{
    			_onBeginToPresent.Invoke();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CF70 -- raycasters on, EnableCancelEvent, _onEndToPresent (+0x40) Invoke
    	[Token(Token = "0x6000085")]
    	[Address(RVA = "0xD3CF70", Offset = "0xD3B970", VA = "0x180D3CF70", Slot = "9")]
    	protected virtual void EndToPresent()
    	{
    		SetGraphicRaycastersEnabled(true);
    		EnableCancelEvent();
    		// Shipped null test: test rcx, rcx / je past the Invoke.
    		if (_onEndToPresent != null)
    		{
    			_onEndToPresent.Invoke();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3CFB0 (builder stub); MoveNext 0xD3DC00, two await states.
    	// The body lives in DismissAsyncCore (XxxCore convention; <DismissAsync>d__30 metadata stays):
    	// - Returns at once while _dismissCts (+0x78) is set; otherwise a NaninovelInteractionBlocker() (0xCFBE30) is held
    	//   for the whole dismissal (<>7__wrap1, using) and a running present (_presentCts +0x70) is cancelled and cleared.
    	// - BeginToDismiss (slot 10); a fresh _dismissCts linked with ct (<linkedCts>5__3, using).
    	// - Visible: slot 44 ChangeVisibility(false, duration, linked token) awaited (state 1).  Already hiding:
    	//   UniTask.WaitWhile(<DismissAsync>b__30_0 = slot 30 Opacity > 0, PlayerLoopTiming 8, linked token), state 0.
    	// - EndToDismiss (slot 11), then _dismissCts disposed and cleared.
    	// Annotated disassembly: work/title-path/disas/ns_14_DismissAsync_MoveNext.txt
    	[Token(Token = "0x6000086")]
    	[Address(RVA = "0xD3CFB0", Offset = "0xD3B9B0", VA = "0x180D3CFB0")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__30))]
    	public Cysharp.Threading.Tasks.UniTask DismissAsync([Optional] float? duration, [Optional] CancellationToken ct)
    	{
    		return DismissAsyncCore(duration, ct);
    	}

    	private async Cysharp.Threading.Tasks.UniTask DismissAsyncCore(float? duration, CancellationToken ct)
    	{
    		// Re-entrancy guard of the binary (0x180D3DCFF cmp [rsi+0x78], 0 / jne to the end): a dismiss already running.
    		if (_dismissCts != null)
    		{
    			return;
    		}
    		using (new NaninovelInteractionBlocker())
    		{
    			if (_presentCts != null)
    			{
    				_presentCts.Cancel();
    				_presentCts.Dispose();
    				_presentCts = null;
    			}
    			BeginToDismiss();
    			_dismissCts = new CancellationTokenSource();
    			using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_dismissCts.Token, ct);
    			if (_customUI.Visible)
    			{
    				await _customUI.ChangeVisibility(false, duration, linkedCts.Token);
    			}
    			else
    			{
    				await Cysharp.Threading.Tasks.UniTask.WaitWhile(() => _customUI.Opacity > 0f, Cysharp.Threading.Tasks.PlayerLoopTiming.Update, linkedCts.Token);
    			}
    			EndToDismiss();
    			_dismissCts.Dispose();
    			_dismissCts = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (call vs. the inlined copy) | RVA: 0xD3D180 -- raycasters off,
    	//   DisableCancelEvent inlined, _onBeginToDismiss (+0x48) Invoke
    	[Token(Token = "0x6000087")]
    	[Address(RVA = "0xD3D180", Offset = "0xD3BB80", VA = "0x180D3D180", Slot = "10")]
    	protected virtual void BeginToDismiss()
    	{
    		SetGraphicRaycastersEnabled(false);
    		DisableCancelEvent();
    		// Shipped null test: test rcx, rcx / je past the Invoke.
    		if (_onBeginToDismiss != null)
    		{
    			_onBeginToDismiss.Invoke();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3D330 -- canvases off, _onEndToDismiss (+0x50) Invoke
    	[Token(Token = "0x6000088")]
    	[Address(RVA = "0xD3D330", Offset = "0xD3BD30", VA = "0x180D3D330", Slot = "11")]
    	protected virtual void EndToDismiss()
    	{
    		SetCanvasesEnabled(false);
    		// Shipped null test: test rcx, rcx / je past the Invoke.
    		if (_onEndToDismiss != null)
    		{
    			_onEndToDismiss.Invoke();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3D360 -- GetComponentsInChildren<Canvas>(), Behaviour.set_enabled per element
    	[Token(Token = "0x6000089")]
    	[Address(RVA = "0xD3D360", Offset = "0xD3BD60", VA = "0x180D3D360")]
    	private void SetCanvasesEnabled(bool enable)
    	{
    		Canvas[] canvases = GetComponentsInChildren<Canvas>();
    		foreach (Canvas canvas in canvases)
    		{
    			canvas.enabled = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3D490 -- GetComponentsInChildren<GraphicRaycaster>(), set_enabled per element
    	[Token(Token = "0x600008A")]
    	[Address(RVA = "0xD3D490", Offset = "0xD3BE90", VA = "0x180D3D490")]
    	private void SetGraphicRaycastersEnabled(bool enable)
    	{
    		GraphicRaycaster[] raycasters = GetComponentsInChildren<GraphicRaycaster>();
    		foreach (GraphicRaycaster raycaster in raycasters)
    		{
    			raycaster.enabled = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3D5C0
    	// - _closeButton (+0x28) alive: its onClick (+0x100) AsObservable().Subscribe(<EnableCancelEvent>b__35_0, which is
    	//   identical-code folded with Cancel) .AddTo(this) (0x67F4A0) -> _closeButtonDisposable (+0x68).
    	// - _enableCancelAction (+0x30): Naninovel IInputManager slot 6 GetSampler("Cancel"), IInputSampler slot 0
    	//   add_OnStart(Cancel).
    	[Token(Token = "0x600008B")]
    	[Address(RVA = "0xD3D5C0", Offset = "0xD3BFC0", VA = "0x180D3D5C0")]
    	private void EnableCancelEvent()
    	{
    		// Shipped null test: 0x180D3D6D9 test rbx / cmp [rbx+0x10], 0 (UnityEngine.Object alive check on _closeButton).
    		if (_closeButton != null)
    		{
    			_closeButtonDisposable = _closeButton.onClick.AsObservable().Subscribe(delegate
    			{
    				Cancel();
    			}).AddTo(this);
    		}
    		if (_enableCancelAction)
    		{
    			NaniInputManager.GetSampler("Cancel").OnStart += Cancel;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3D880 -- the close-button subscription disposed and cleared; with
    	//   _enableCancelAction, IInputSampler slot 1 remove_OnStart(Cancel) on GetSampler("Cancel")
    	[Token(Token = "0x600008C")]
    	[Address(RVA = "0xD3D880", Offset = "0xD3C280", VA = "0x180D3D880")]
    	private void DisableCancelEvent()
    	{
    		// Shipped null test: cmp [rbx+0x68], 0 / je past the Dispose.
    		if (_closeButtonDisposable != null)
    		{
    			_closeButtonDisposable.Dispose();
    			_closeButtonDisposable = null;
    		}
    		if (_enableCancelAction)
    		{
    			NaniInputManager.GetSampler("Cancel").OnStart -= Cancel;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3DA10 -- byte [this+0x80] (IsCancelEventBlocked) set -> nothing; otherwise
    	//   vtable +0x1F8 = slot 12 PlayCancelSfx, then Dismiss inlined
    	[Token(Token = "0x600008D")]
    	[Address(RVA = "0xD3DA10", Offset = "0xD3C410", VA = "0x180D3DA10")]
    	private void Cancel()
    	{
    		if (!IsCancelEventBlocked)
    		{
    			PlayCancelSfx();
    			Dismiss();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD3DA80 -- InputManager getter inlined, IInputManager slot 0
    	//   PlayResponseSfx(0 = Cancel)
    	[Token(Token = "0x600008E")]
    	[Address(RVA = "0xD3DA80", Offset = "0xD3C480", VA = "0x180D3DA80", Slot = "12")]
    	protected virtual void PlayCancelSfx()
    	{
    		InputManager.PlayResponseSfx(ResponseKind.Cancel);
    	}

    	[Token(Token = "0x600008F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public NaninovelScreen()
    	{
    	}
    }
}
