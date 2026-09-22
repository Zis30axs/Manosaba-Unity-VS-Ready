using UniRx;
using LitMotion.Extensions;
using LitMotion;
using GigaCreation.Tools.Service;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000038")]
    [RequireComponent(typeof(Canvas))]
    public class GeneralScreen : MonoBehaviour, ICancelEventBlockable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000039")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__31
    {
    		[Token(Token = "0x40000E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool instantly;

    		[Token(Token = "0x40000E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40000E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000116")]
    		[Address(RVA = "0xCE8800", Offset = "0xCE7200", VA = "0x180CE8800", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000117")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003A")]
    	[CompilerGenerated]
    	private struct _003CFadeAsync_003Ed__43
    {
    		[Token(Token = "0x40000E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float duration;

    		[Token(Token = "0x40000EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float to;

    		[Token(Token = "0x40000ED")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000EE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000EF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000118")]
    		[Address(RVA = "0xCE8EC0", Offset = "0xCE78C0", VA = "0x180CE8EC0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000119")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003B")]
    	[CompilerGenerated]
    	private struct _003CFadeInAsync_003Ed__41
    {
    		[Token(Token = "0x40000F0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000F1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000F2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000F3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool instantly;

    		[Token(Token = "0x40000F4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x21")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000F5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000F6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011A")]
    		[Address(RVA = "0xCE93F0", Offset = "0xCE7DF0", VA = "0x180CE93F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003C")]
    	[CompilerGenerated]
    	private struct _003CFadeOutAsync_003Ed__42
    {
    		[Token(Token = "0x40000F7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000F8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000F9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x40000FA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool instantly;

    		[Token(Token = "0x40000FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x21")]
    		public bool updateMotionManually;

    		[Token(Token = "0x40000FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011C")]
    		[Address(RVA = "0xCE9700", Offset = "0xCE8100", VA = "0x180CE9700", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003D")]
    	[CompilerGenerated]
    	private struct _003CPresentAsync_003Ed__28
    {
    		[Token(Token = "0x40000FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000100")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GeneralScreen _003C_003E4__this;

    		[Token(Token = "0x4000101")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000102")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public bool instantly;

    		[Token(Token = "0x4000103")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000104")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600011E")]
    		[Address(RVA = "0xCE9A10", Offset = "0xCE8410", VA = "0x180CE9A10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600011F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000D0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x40000D1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x40000D2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Underlay _underlay;

    	[Token(Token = "0x40000D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Button _closeButton;

    	[Token(Token = "0x40000D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _visibleOnAwake;

    	[Token(Token = "0x40000D5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private float _contentPresentDuration;

    	[Token(Token = "0x40000D6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private float _contentDismissDuration;

    	[Token(Token = "0x40000D7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
    	[Space]
    	[SerializeField]
    	private bool _enableCancelAction;

    	[Token(Token = "0x40000D8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private UnityEvent _onBeginToPresent;

    	[Token(Token = "0x40000D9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private UnityEvent _onEndToPresent;

    	[Token(Token = "0x40000DA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private UnityEvent _onBeginToDismiss;

    	[Token(Token = "0x40000DB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[SerializeField]
    	private UnityEvent _onEndToDismiss;

    	[Token(Token = "0x40000DC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x40000DD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private IDisposable _closeButtonDisposable;

    	[Token(Token = "0x40000DE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private CancellationTokenSource _presentCts;

    	[Token(Token = "0x40000DF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private CancellationTokenSource _dismissCts;

    	[Token(Token = "0x17000031")]
    	public bool IsCancelEventBlocked
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x619430 (get, folded: movzx eax, [rcx+0x90]) / 0x87B020 (set, slot 5:
    		//   mov [rcx+0x90], dl) -- `<IsCancelEventBlocked>k__BackingField`; ICancelEventBlockable.set, read by Cancel
    		// Annotated disassembly: work/title-path/disas/gs_619430, gs_87B020.txt
    		[Token(Token = "0x60000FC")]
    		[Address(RVA = "0x619430", Offset = "0x617E30", VA = "0x180619430")]
    		[CompilerGenerated]
    		private get;
    		[Token(Token = "0x60000FD")]
    		[Address(RVA = "0x87B020", Offset = "0x879A20", VA = "0x18087B020", Slot = "5")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x17000032")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE6C00 -- lazy: `_inputManager` (+0x70) tested, else
    		//   ServiceLocator.Get<IInputManager>(cl = 1) stored back (Pagination.InputManager's twin)
    		// Annotated disassembly: work/title-path/disas/gs_CE6C00.txt
    		[Token(Token = "0x60000FE")]
    		[Address(RVA = "0xCE6C00", Offset = "0xCE5600", VA = "0x180CE6C00")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	[Token(Token = "0x17000033")]
    	public int SortingOrder
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE6CC0 -- `_canvas` (+0x20).sortingOrder (icall, tail jump)
    		// Annotated disassembly: work/title-path/disas/gs_CE6CC0.txt
    		[Token(Token = "0x60000FF")]
    		[Address(RVA = "0xCE6CC0", Offset = "0xCE56C0", VA = "0x180CE6CC0")]
    		get
    		{
    			return _canvas.sortingOrder;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE6D60 -- tail jump to Canvas.set_sortingOrder on `_canvas` (+0x20)
    		// Annotated disassembly: work/title-path/disas/gs_CE6D60.txt
    		[Token(Token = "0x6000100")]
    		[Address(RVA = "0xCE6D60", Offset = "0xCE5760", VA = "0x180CE6D60")]
    		set
    		{
    			_canvas.sortingOrder = value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE6D80 -- SetCanvasesEnabled(`_visibleOnAwake` (+0x40), false),
    	//   SetGraphicRaycastersEnabled(`_visibleOnAwake`), then `_canvasGroup` (+0x28) alive -> alpha = 1 or 0.
    	//   The stub left every screen that starts hidden (`_visibleOnAwake` = 0) drawing: PerspectiveStage/ScreenCanvas,
    	//   an overlay canvas with two full-screen RawImages whose RenderTextures are only bound in a trial, covered
    	//   the whole title screen in white.
    	// Annotated disassembly: work/title-path/disas/gs_CE6D80.txt
    	[Token(Token = "0x6000101")]
    	[Address(RVA = "0xCE6D80", Offset = "0xCE5780", VA = "0x180CE6D80")]
    	private void Awake()
    	{
    		SetCanvasesEnabled(_visibleOnAwake);
    		SetGraphicRaycastersEnabled(_visibleOnAwake);
    		if (_canvasGroup)
    		{
    			_canvasGroup.alpha = (_visibleOnAwake ? 1f : 0f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE6EA0, 405 B -- editor Reset: GetComponent<Canvas> -> +0x20,
    	//   GetComponent<CanvasGroup> -> +0x28, GetComponentInChildren<Underlay> -> +0x30
    	// Annotated disassembly: work/title-path/disas/gs_CE6EA0.txt
    	[Token(Token = "0x6000102")]
    	[Address(RVA = "0xCE6EA0", Offset = "0xCE58A0", VA = "0x180CE6EA0")]
    	private void Reset()
    	{
    		_canvas = GetComponent<Canvas>();
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_underlay = GetComponentInChildren<Underlay>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7040 -- DismissAsync(instantly = 0, destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/gs_CE7040.txt
    	[Token(Token = "0x6000103")]
    	[Address(RVA = "0xCE7040", Offset = "0xCE5A40", VA = "0x180CE7040")]
    	private void Dismiss()
    	{
    		DismissAsync(false, destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7090 (kickoff) / 0xCE9A10 (<PresentAsync>d__28.MoveNext, 1724 B)
    	// The body lives in PresentAsyncCore (XxxCore convention; the <PresentAsync>d__28 metadata stays).
    	//   `_presentCts` (+0x80) already set -> done (0x180CE9AD0, a present in flight); a pending dismiss
    	//   (`_dismissCts`, +0x88) is cancelled, disposed and cleared; +0x80 = new CancellationTokenSource; `<linkedCts>5__2`
    	//   = CreateLinkedTokenSource(_presentCts.Token, ct) in a using (finally 0x180CEA00C); BeginToPresent; when
    	//   `_canvasGroup` is alive (op_Implicit) await FadeInAsync(instantly, false, linkedCts.Token); EndToPresent;
    	//   `_presentCts`.Dispose() and null.  No catch: a cancelled fade faults the task through the builder.
    	// Annotated disassembly: work/title-path/disas/gs_CE7090, gs_CE9A10.txt
    	[Token(Token = "0x6000104")]
    	[Address(RVA = "0xCE7090", Offset = "0xCE5A90", VA = "0x180CE7090")]
    	[AsyncStateMachine(typeof(_003CPresentAsync_003Ed__28))]
    	public UniTask PresentAsync(bool instantly = false, CancellationToken ct = default)
    	{
    		return PresentAsyncCore(instantly, ct);
    	}

    	private async UniTask PresentAsyncCore(bool instantly, CancellationToken ct)
    	{
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
    		_presentCts = new CancellationTokenSource();
    		using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_presentCts.Token, ct))
    		{
    			BeginToPresent();
    			if (_canvasGroup)
    			{
    				await FadeInAsync(instantly, false, linkedCts.Token);
    			}
    			EndToPresent();
    			_presentCts.Dispose();
    			_presentCts = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7260 -- raycasters off; SetCanvasesEnabled(true, keepHidden =
    	//   `_canvasGroup` alive (m_CachedPtr != 0, setne r8b)) so the canvases come on at alpha 0 before the fade;
    	//   `_underlay` (+0x30) alive -> SetRaycastTargetEnabled(true); `_onBeginToPresent` (+0x50).Invoke()
    	// Annotated disassembly: work/title-path/disas/gs_CE7260.txt
    	[Token(Token = "0x6000105")]
    	[Address(RVA = "0xCE7260", Offset = "0xCE5C60", VA = "0x180CE7260")]
    	public void BeginToPresent()
    	{
    		SetGraphicRaycastersEnabled(false);
    		SetCanvasesEnabled(true, _canvasGroup);
    		if (_underlay)
    		{
    			_underlay.SetRaycastTargetEnabled(true);
    		}
    		_onBeginToPresent.Invoke();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7400 -- raycasters on, EnableCancelEvent, `_underlay` alive ->
    	//   SetInteractable(true), `_onEndToPresent` (+0x58).Invoke()
    	// Annotated disassembly: work/title-path/disas/gs_CE7400.txt
    	[Token(Token = "0x6000106")]
    	[Address(RVA = "0xCE7400", Offset = "0xCE5E00", VA = "0x180CE7400")]
    	public void EndToPresent()
    	{
    		SetGraphicRaycastersEnabled(true);
    		EnableCancelEvent();
    		if (_underlay)
    		{
    			_underlay.SetInteractable(true);
    		}
    		_onEndToPresent.Invoke();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7510 (kickoff) / 0xCE8800 (<DismissAsync>d__31.MoveNext, 1724 B)
    	// The body lives in DismissAsyncCore.  PresentAsync's mirror: `_dismissCts` (+0x88) set -> done; a pending
    	//   present (+0x80) is cancelled/disposed/cleared; +0x88 = new CTS; linked CTS in a using; BeginToDismiss;
    	//   `_canvasGroup` alive -> await FadeOutAsync(instantly, false, linkedCts.Token); EndToDismiss; +0x88 disposed
    	//   and null.
    	// Annotated disassembly: work/title-path/disas/gs_CE7510, gs_CE8800.txt
    	[Token(Token = "0x6000107")]
    	[Address(RVA = "0xCE7510", Offset = "0xCE5F10", VA = "0x180CE7510")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__31))]
    	public UniTask DismissAsync(bool instantly = false, CancellationToken ct = default)
    	{
    		return DismissAsyncCore(instantly, ct);
    	}

    	private async UniTask DismissAsyncCore(bool instantly, CancellationToken ct)
    	{
    		if (_dismissCts != null)
    		{
    			return;
    		}
    		if (_presentCts != null)
    		{
    			_presentCts.Cancel();
    			_presentCts.Dispose();
    			_presentCts = null;
    		}
    		_dismissCts = new CancellationTokenSource();
    		using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_dismissCts.Token, ct))
    		{
    			BeginToDismiss();
    			if (_canvasGroup)
    			{
    				await FadeOutAsync(instantly, false, linkedCts.Token);
    			}
    			EndToDismiss();
    			_dismissCts.Dispose();
    			_dismissCts = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE76E0 -- raycasters off, DisableCancelEvent (inlined: same
    	//   metadata flag data_30DD1C6), `_underlay` alive -> SetInteractable(false), `_onBeginToDismiss` (+0x60).Invoke()
    	// Annotated disassembly: work/title-path/disas/gs_CE76E0.txt
    	[Token(Token = "0x6000108")]
    	[Address(RVA = "0xCE76E0", Offset = "0xCE60E0", VA = "0x180CE76E0")]
    	public void BeginToDismiss()
    	{
    		SetGraphicRaycastersEnabled(false);
    		DisableCancelEvent();
    		if (_underlay)
    		{
    			_underlay.SetInteractable(false);
    		}
    		_onBeginToDismiss.Invoke();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7950 -- SetCanvasesEnabled(false, false), `_underlay` alive ->
    	//   SetRaycastTargetEnabled(false), `_onEndToDismiss` (+0x68).Invoke()
    	// Annotated disassembly: work/title-path/disas/gs_CE7950.txt
    	[Token(Token = "0x6000109")]
    	[Address(RVA = "0xCE7950", Offset = "0xCE6350", VA = "0x180CE7950")]
    	public void EndToDismiss()
    	{
    		SetCanvasesEnabled(false);
    		if (_underlay)
    		{
    			_underlay.SetRaycastTargetEnabled(false);
    		}
    		_onEndToDismiss.Invoke();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7A60 -- keepHidden && enable && `_canvasGroup` alive -> alpha = 0;
    	//   then GetComponentsInChildren<Canvas>() (inactive excluded), Behaviour.set_enabled(enable) on each
    	// Annotated disassembly: work/title-path/disas/gs_CE7A60.txt
    	[Token(Token = "0x600010A")]
    	[Address(RVA = "0xCE7A60", Offset = "0xCE6460", VA = "0x180CE7A60")]
    	private void SetCanvasesEnabled(bool enable, bool keepHidden = false)
    	{
    		if (keepHidden && enable && _canvasGroup)
    		{
    			_canvasGroup.alpha = 0f;
    		}
    		Canvas[] componentsInChildren = GetComponentsInChildren<Canvas>();
    		for (int i = 0; i < componentsInChildren.Length; i++)
    		{
    			componentsInChildren[i].enabled = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7C60 -- GetComponentsInChildren<GraphicRaycaster>(), Behaviour.set_enabled
    	//   (icall data_30E84D0) with `enable` on each
    	// Annotated disassembly: work/title-path/disas/gs_CE7C60.txt
    	[Token(Token = "0x600010B")]
    	[Address(RVA = "0xCE7C60", Offset = "0xCE6660", VA = "0x180CE7C60")]
    	private void SetGraphicRaycastersEnabled(bool enable)
    	{
    		GraphicRaycaster[] componentsInChildren = GetComponentsInChildren<GraphicRaycaster>();
    		for (int i = 0; i < componentsInChildren.Length; i++)
    		{
    			componentsInChildren[i].enabled = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE7D90 -- `_closeButton` (+0x38) alive -> its onClick (+0x100)
    	//   .AsObservable().Subscribe(<EnableCancelEvent>b__36_0).AddTo(this) into `_closeButtonDisposable` (+0x78);
    	//   b__36_0 is identical-code folded with OnCancelActionPerformed at 0xCE81F0 (tail jump to Cancel).
    	//   `_enableCancelAction` (+0x4C) -> IInputManager.GetAction("Cancel", null) (slot 1) and its `performed`
    	//   CallbackArray (+0x170).AddCallback(OnCancelActionPerformed)
    	// Annotated disassembly: work/title-path/disas/gs_CE7D90.txt
    	[Token(Token = "0x600010C")]
    	[Address(RVA = "0xCE7D90", Offset = "0xCE6790", VA = "0x180CE7D90")]
    	private void EnableCancelEvent()
    	{
    		if (_closeButton)
    		{
    			_closeButtonDisposable = _closeButton.onClick.AsObservable().Subscribe(delegate
    			{
    				Cancel();
    			}).AddTo(this);
    		}
    		if (_enableCancelAction)
    		{
    			InputManager.GetAction("Cancel").performed += OnCancelActionPerformed;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE8050 -- `_closeButtonDisposable` (+0x78) non-null -> IDisposable.Dispose,
    	//   cleared; `_enableCancelAction` -> GetAction("Cancel").performed RemoveCallback(OnCancelActionPerformed)
    	// Annotated disassembly: work/title-path/disas/gs_CE8050.txt
    	[Token(Token = "0x600010D")]
    	[Address(RVA = "0xCE8050", Offset = "0xCE6A50", VA = "0x180CE8050")]
    	private void DisableCancelEvent()
    	{
    		// 0x180CE80C4: the shipped binary tests the subscription before disposing it (EnableCancelEvent only creates
    		// one when a close button is assigned)
    		if (_closeButtonDisposable != null)
    		{
    			_closeButtonDisposable.Dispose();
    			_closeButtonDisposable = null;
    		}
    		if (_enableCancelAction)
    		{
    			InputManager.GetAction("Cancel").performed -= OnCancelActionPerformed;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE81D0 -- tail jump to Underlay.TryGetClickTask on `_underlay` (+0x30)
    	// Annotated disassembly: work/title-path/disas/gs_CE81D0.txt
    	[Token(Token = "0x600010E")]
    	[Address(RVA = "0xCE81D0", Offset = "0xCE6BD0", VA = "0x180CE81D0")]
    	public bool TryGetClickUnderlayTask(out UniTask task, CancellationToken ct = default)
    	{
    		return _underlay.TryGetClickTask(out task, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE81F0 -- tail jump to Cancel
    	// Annotated disassembly: work/title-path/disas/gs_CE81F0.txt
    	[Token(Token = "0x600010F")]
    	[Address(RVA = "0xCE81F0", Offset = "0xCE6BF0", VA = "0x180CE81F0")]
    	private void OnCancelActionPerformed(InputAction.CallbackContext _)
    	{
    		Cancel();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE8200 -- `<IsCancelEventBlocked>k__BackingField` (+0x90) set -> nothing;
    	//   else IInputManager.PlayResponseSfx(0 = Cancel) (slot 0), DismissAsync(false, destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/gs_CE8200.txt
    	[Token(Token = "0x6000110")]
    	[Address(RVA = "0xCE8200", Offset = "0xCE6C00", VA = "0x180CE8200")]
    	private void Cancel()
    	{
    		if (!IsCancelEventBlocked)
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Cancel);
    			DismissAsync(false, destroyCancellationToken).Forget();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE82A0 (kickoff) / 0xCE93F0 (<FadeInAsync>d__41.MoveNext, 769 B)
    	// The body lives in FadeInAsyncCore.  await FadeAsync(to = 1.0 (data_2AE2820), duration = instantly ? 0 :
    	//   `_contentPresentDuration` (+0x44), updateMotionManually, ct)
    	// Annotated disassembly: work/title-path/disas/gs_CE82A0, gs_CE93F0.txt
    	[Token(Token = "0x6000111")]
    	[Address(RVA = "0xCE82A0", Offset = "0xCE6CA0", VA = "0x180CE82A0")]
    	[AsyncStateMachine(typeof(_003CFadeInAsync_003Ed__41))]
    	public UniTask FadeInAsync(bool instantly = false, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return FadeInAsyncCore(instantly, updateMotionManually, ct);
    	}

    	private async UniTask FadeInAsyncCore(bool instantly, bool updateMotionManually, CancellationToken ct)
    	{
    		await FadeAsync(1f, instantly ? 0f : _contentPresentDuration, updateMotionManually, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE8460 (kickoff) / 0xCE9700 (<FadeOutAsync>d__42.MoveNext, 769 B)
    	// The body lives in FadeOutAsyncCore.  await FadeAsync(to = 0 (xorps xmm2), duration = instantly ? 0 :
    	//   `_contentDismissDuration` (+0x48), updateMotionManually, ct)
    	// Annotated disassembly: work/title-path/disas/gs_CE8460, gs_CE9700.txt
    	[Token(Token = "0x6000112")]
    	[Address(RVA = "0xCE8460", Offset = "0xCE6E60", VA = "0x180CE8460")]
    	[AsyncStateMachine(typeof(_003CFadeOutAsync_003Ed__42))]
    	public UniTask FadeOutAsync(bool instantly = false, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return FadeOutAsyncCore(instantly, updateMotionManually, ct);
    	}

    	private async UniTask FadeOutAsyncCore(bool instantly, bool updateMotionManually, CancellationToken ct)
    	{
    		await FadeAsync(0f, instantly ? 0f : _contentDismissDuration, updateMotionManually, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE8620 (kickoff) / 0xCE8EC0 (<FadeAsync>d__43.MoveNext, 1314 B)
    	// The body lives in FadeAsyncCore.  `_canvasGroup` dead -> LogWarning(literal 0x2E68340 + name, this);
    	//   duration < 0 (comiss 0, [+0x20]) -> LogWarning(string.Format(literal 0x2E671A0, (object)duration));
    	//   else LMotion.Create<float, NoOptions, FloatMotionAdapter>(alpha, to, duration)
    	//   .WithScheduler(updateMotionManually ? MotionScheduler.Manual : static +0x60 = UpdateIgnoreTimeScale)
    	//   .BindToAlpha(_canvasGroup).AddTo(this (MonoBehaviour overload 0xDA53D0)).ToUniTask(ct) awaited.
    	// Annotated disassembly: work/title-path/disas/gs_CE8620, gs_CE8EC0.txt
    	[Token(Token = "0x6000113")]
    	[Address(RVA = "0xCE8620", Offset = "0xCE7020", VA = "0x180CE8620")]
    	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__43))]
    	private UniTask FadeAsync(float to, float duration, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return FadeAsyncCore(to, duration, updateMotionManually, ct);
    	}

    	private async UniTask FadeAsyncCore(float to, float duration, bool updateMotionManually, CancellationToken ct)
    	{
    		if (!_canvasGroup)
    		{
    			UnityEngine.Debug.LogWarning("Canvas Group がアタッチされていないため、フェードさせることはできません：" + base.name, this);
    		}
    		else if (duration < 0f)
    		{
    			UnityEngine.Debug.LogWarning(string.Format("時間は 0 以上でなければいけません：{0}", duration));
    		}
    		else
    		{
    			await LMotion.Create(_canvasGroup.alpha, to, duration)
    				.WithScheduler(updateMotionManually ? MotionScheduler.Manual : MotionScheduler.UpdateIgnoreTimeScale)
    				.BindToAlpha(_canvasGroup)
    				.AddTo(this)
    				.ToUniTask(ct);
    		}
    	}

    	[Token(Token = "0x6000114")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public GeneralScreen()
    	{
    	}
    }
}
