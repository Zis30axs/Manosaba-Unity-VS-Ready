using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using Naninovel;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000B4")]
public class ExecutionButtonScreen : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20000B5")]
	[CompilerGenerated]
	private struct _003COnFullFillAsync_003Ed__26
{
		[Token(Token = "0x400033A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400033B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400033C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x400033D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400033E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600029C")]
		[Address(RVA = "0x34C310", Offset = "0x34AD10", VA = "0x18034C310", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600029D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000B6")]
	[CompilerGenerated]
	private struct _003CPressAsync_003Ed__24
{
		[Token(Token = "0x400033F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000340")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000341")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x4000342")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000343")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000344")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600029E")]
		[Address(RVA = "0x34C9B0", Offset = "0x34B3B0", VA = "0x18034C9B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600029F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000B7")]
	[CompilerGenerated]
	private struct _003CReleaseAsync_003Ed__25
{
		[Token(Token = "0x4000345")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000346")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000347")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ExecutionButtonScreen _003C_003E4__this;

		[Token(Token = "0x4000348")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000349")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400034A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002A0")]
		[Address(RVA = "0x34D2A0", Offset = "0x34BCA0", VA = "0x18034D2A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002A1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000329")]
	private const string ExecutionConfirmedLabelName = "ExecutionConfirmed";

	[Token(Token = "0x400032A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private RectTransform _buttonContainer;

	[Token(Token = "0x400032B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x400032C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Image _blood;

	[Token(Token = "0x400032D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _checkIcon;

	[Token(Token = "0x400032E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Header("Parameters")]
	[SerializeField]
	private float _scaleOnPressed;

	[Token(Token = "0x400032F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
	[SerializeField]
	private float _scaleDuration;

	[Token(Token = "0x4000330")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private float _increaseSpeed;

	[Token(Token = "0x4000331")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4C")]
	[SerializeField]
	private float _decreaseSpeed;

	[Token(Token = "0x4000332")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private float _delayAfterFullFill;

	[Token(Token = "0x4000333")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _chargingSfx;

	[Token(Token = "0x4000334")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _completeSfx;

	// Field initializer: the constructor (RVA 0x34BE00) allocates a
	// CompositeDisposable and stores it to +0x68 before the inlined, empty
	// MonoBehaviour base constructor.
	[Token(Token = "0x4000335")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private readonly CompositeDisposable _disposables = new CompositeDisposable();

	[Token(Token = "0x4000336")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x4000337")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000338")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private bool _pressed;

	[Token(Token = "0x4000339")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000049")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x34B4B0, 186 B
		// Lazy cache in +0x70 via `Naninovel.Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x6000290")]
		[Address(RVA = "0x34B4B0", Offset = "0x349EB0", VA = "0x18034B4B0")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x1700004A")]
	private IScriptPlayer NaniScriptPlayer
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x34B570, 186 B
		// Lazy cache in +0x78 via `Naninovel.Engine.GetServiceOrErr<IScriptPlayer>()`.
		[Token(Token = "0x6000291")]
		[Address(RVA = "0x34B570", Offset = "0x349F70", VA = "0x18034B570")]
		get
		{
			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34B630, 59 B
	[Token(Token = "0x6000292")]
	[Address(RVA = "0x34B630", Offset = "0x34A030", VA = "0x18034B630")]
	private void Awake()
	{
		_blood.fillAmount = 0f;
		_checkIcon.alpha = 0f;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34B670, 550 B
	// Resets the gauge (no `_disposables.Clear()`, unlike `Dispose`) and makes
	// three subscriptions. Each goes through `.AddTo(_disposables, this)`: the
	// MethodInfo resolves to the generic instance at 0x67FCB0, which dump.cs
	// lists under `AddTo<T>(T, ICollection<IDisposable>, Component)`. The
	// `_pressed = false` store lands between loading `_button` and the first
	// trigger call; no call separates them, so placing it before the
	// subscriptions is faithful.
	[Token(Token = "0x6000293")]
	[Address(RVA = "0x34B670", Offset = "0x34A070", VA = "0x18034B670")]
	public void Ready()
	{
		_blood.fillAmount = 0f;
		_checkIcon.alpha = 0f;
		_pressed = false;
		// RECONSTRUCTED | Confidence: A | lambda <Ready>b__22_0, RVA: 0x34BEE0, 424 B
		// The lambda inlines `PressAsync`'s kickoff (Start<<PressAsync>d__24>) and
		// passes `destroyCancellationToken`; the result goes to Cysharp `Forget`.
		_button.OnPointerDownAsObservable()
			.Subscribe((PointerEventData _) => PressAsync(destroyCancellationToken).Forget())
			.AddTo(_disposables, this);
		// RECONSTRUCTED | Confidence: A | lambda <Ready>b__22_1, RVA: 0x34C090, 61 B
		_button.OnPointerUpAsObservable()
			.Subscribe((PointerEventData _) => ReleaseAsync(destroyCancellationToken).Forget())
			.AddTo(_disposables, this);
		// RECONSTRUCTED | Confidence: A | lambda <Ready>b__22_2, RVA: 0x34C0D0, 567 B
		// Per-frame gauge. Image+0xFC is `m_FillAmount` (inlined getter). Pressed:
		// `v = fill + _increaseSpeed; v > 0.1f ? v : 0.1f` (constant 0x2AE2628 =
		// `cdcccc3d` = 0.1f). Released: `v = fill - _decreaseSpeed; v > 0 ? v : 0`.
		// No `Time.deltaTime` factor: the speeds are per frame, so the fill rate
		// depends on frame rate. Writing the clamps as `Mathf.Max` is Confidence B
		// (inlined, indistinguishable from a hand ternary); values and branch
		// directions are A. `sub_331670` is the `Mathf.Approximately` body
		// (`|b-a| < max(1e-6 * max(|a|,|b|), Epsilon * 8)`, constants 0x2AE255C =
		// 1e-6 and 0x2AE2AA4 = 8.0), called with (fillAmount, 1f).
		this.UpdateAsObservable()
			.Subscribe((Unit _) =>
			{
				_blood.fillAmount = _pressed
					? Mathf.Max(_blood.fillAmount + _increaseSpeed, 0.1f)
					: Mathf.Max(_blood.fillAmount - _decreaseSpeed, 0f);
				if (Mathf.Approximately(_blood.fillAmount, 1f))
				{
					OnFullFillAsync(destroyCancellationToken).Forget();
				}
			})
			.AddTo(_disposables, this);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34B8A0, 83 B
	[Token(Token = "0x6000294")]
	[Address(RVA = "0x34B8A0", Offset = "0x34A2A0", VA = "0x18034B8A0")]
	public void Dispose()
	{
		_disposables.Clear();
		_blood.fillAmount = 0f;
		_checkIcon.alpha = 0f;
		_pressed = false;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34B900, 417 B (entry) + MoveNext 0x34C9B0
	// `sub_46C0` is an IAudioManager interface thunk that resolves slot 0x13 =
	// 19 = `PlaySfx(path, volume, fadeTime, loop, group, token)`, called with
	// (1f, 0f, false, null, default) -- written out in full because dump.cs
	// prints no default value for `group` or `token`. The task goes to Naninovel's
	// `Forget` (inlined to `ForgetCore`). The CTS is allocated inline (the
	// stores to +0x20 = 1 / +0x24 = -1 are the inlined parameterless
	// constructor). `<linkedCts>5__2` is disposed through IDisposable with a
	// null check on both the normal and the exceptional path, which is a
	// `using`; `_cts.Dispose(); _cts = null;` run after the await, inside it.
	// `_cts` is cancelled and disposed at the start but not nulled before
	// being replaced. Motion: `Vector2.one` (Vector2 static +0x8 =
	// `oneVector`) to `Vector2.one * _scaleOnPressed` over `_scaleDuration`,
	// bound to `_buttonContainer`, `AddTo(MonoBehaviour)` (0xDA53D0), then
	// `ToUniTask(handle, CancellationToken)` (0xD83990).
	[Token(Token = "0x6000295")]
	[Address(RVA = "0x34B900", Offset = "0x34A300", VA = "0x18034B900")]
	[AsyncStateMachine(typeof(_003CPressAsync_003Ed__24))]
	private async Cysharp.Threading.Tasks.UniTask PressAsync([Optional] CancellationToken ct)
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
		}
		NaniAudioManager.PlaySfx(_chargingSfx, 1f, 0f, false, null, default(AsyncToken)).Forget();
		_pressed = true;
		_cts = new CancellationTokenSource();
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
		await LMotion.Create(Vector2.one, Vector2.one * _scaleOnPressed, _scaleDuration)
			.BindToLocalScaleXY(_buttonContainer)
			.AddTo(this)
			.ToUniTask(linkedCts.Token);
		_cts.Dispose();
		_cts = null;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34BAB0, 417 B (entry) + MoveNext 0x34D2A0
	// Mirror of `PressAsync`. The audio call is `sub_59F0` with ecx = 0x14 =
	// slot 20 = `StopSfx(path, fadeTime, token)` on `_chargingSfx` (+0x58)
	// with fadeTime 0.1f (0x2AE2628); `_pressed` is set false; the motion runs
	// from `Vector2.one * _scaleOnPressed` back to `Vector2.one`.
	[Token(Token = "0x6000296")]
	[Address(RVA = "0x34BAB0", Offset = "0x34A4B0", VA = "0x18034BAB0")]
	[AsyncStateMachine(typeof(_003CReleaseAsync_003Ed__25))]
	private async Cysharp.Threading.Tasks.UniTask ReleaseAsync([Optional] CancellationToken ct)
	{
		if (_cts != null)
		{
			_cts.Cancel();
			_cts.Dispose();
		}
		NaniAudioManager.StopSfx(_chargingSfx, 0.1f, default(AsyncToken)).Forget();
		_pressed = false;
		_cts = new CancellationTokenSource();
		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, ct);
		await LMotion.Create(Vector2.one * _scaleOnPressed, Vector2.one, _scaleDuration)
			.BindToLocalScaleXY(_buttonContainer)
			.AddTo(this)
			.ToUniTask(linkedCts.Token);
		_cts.Dispose();
		_cts = null;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34BC60, 401 B (entry) + MoveNext 0x34C310
	// Two await points (states 0 and 1). `ReleaseAsync(ct)` is called before
	// the completion SFX is started, but its awaiter is taken only afterwards,
	// so the task is held in a local (A for ordering). `PlaySfx` slot 19 on
	// `_completeSfx` (+0x60) with default arguments. `TimeSpan.Interval(v,
	// 1000)` is the inlined `TimeSpan.FromSeconds`. The Delay overload
	// (0x180D710) is `Delay(TimeSpan, DelayType, PlayerLoopTiming,
	// CancellationToken, bool)`, called with DelayType 0 (`DeltaTime`),
	// timing 8 (the default), `ct`, and false. The label string literal is
	// "ExecutionConfirmed"; using the const name is Confidence B.
	[Token(Token = "0x6000297")]
	[Address(RVA = "0x34BC60", Offset = "0x34A660", VA = "0x18034BC60")]
	[AsyncStateMachine(typeof(_003COnFullFillAsync_003Ed__26))]
	private async Cysharp.Threading.Tasks.UniTask OnFullFillAsync([Optional] CancellationToken ct)
	{
		_disposables.Clear();
		Cysharp.Threading.Tasks.UniTask releaseTask = ReleaseAsync(ct);
		NaniAudioManager.PlaySfx(_completeSfx, 1f, 0f, false, null, default(AsyncToken)).Forget();
		await releaseTask;
		_checkIcon.alpha = 1f;
		await Cysharp.Threading.Tasks.UniTask.Delay(TimeSpan.FromSeconds(_delayAfterFullFill), Cysharp.Threading.Tasks.DelayType.DeltaTime, cancellationToken: ct);
		NaniScriptPlayer.ResumeAtLabel(ExecutionConfirmedLabelName);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34BE00, 210 B
	// Only the `_disposables` field initializer (see the field).
	[Token(Token = "0x6000298")]
	[Address(RVA = "0x34BE00", Offset = "0x34A800", VA = "0x18034BE00")]
	public ExecutionButtonScreen()
	{
	}
}
