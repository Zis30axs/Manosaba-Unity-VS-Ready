using GigaCreation.NaninovelExtender.Ui;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Dialogs.Views;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000DC")]
    [RequireComponent(typeof(CanvasGroup))]
    public class TitleUi : CustomUI, ITitleUI, IManagedUI
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20000DD")]
    	[CompilerGenerated]
    	private struct _003CChangeVisibility_003Ed__53
    {
    		[Token(Token = "0x4000433")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000434")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000435")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TitleUi _003C_003E4__this;

    		[Token(Token = "0x4000436")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool visible;

    		[Token(Token = "0x4000437")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float? duration;

    		[Token(Token = "0x4000438")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000439")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000361")]
    		[Address(RVA = "0x363C40", Offset = "0x362640", VA = "0x180363C40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000362")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000DE")]
    	[CompilerGenerated]
    	private struct _003CLoadGameAsync_003Ed__58
    {
    		[Token(Token = "0x400043A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400043B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400043C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TitleUi _003C_003E4__this;

    		[Token(Token = "0x400043D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400043E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000363")]
    		[Address(RVA = "0x3643B0", Offset = "0x362DB0", VA = "0x1803643B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000364")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000DF")]
    	[CompilerGenerated]
    	private struct _003CQuitGameAsync_003Ed__60
    {
    		[Token(Token = "0x400043F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000440")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000441")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public TitleUi _003C_003E4__this;

    		[Token(Token = "0x4000442")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000443")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

    		[Token(Token = "0x4000444")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private ProvidableDialog _003Cdialog_003E5__3;

    		[Token(Token = "0x4000445")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000365")]
    		[Address(RVA = "0x3646E0", Offset = "0x3630E0", VA = "0x1803646E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000366")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000418")]
    	private const string StartGameLabelName = "StartGame";

    	[Token(Token = "0x4000419")]
    	private const string QuitGameLabelName = "QuitGame";

    	[Token(Token = "0x400041A")]
    	private const string GameProgressVarName = "g_gameProgress";

    	[Token(Token = "0x400041B")]
    	private const string NextScenarioVarName = "nextScenario";

    	[Token(Token = "0x400041C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	[Header("References for Title")]
    	[SerializeField]
    	private Button _loadGameButton;

    	[Token(Token = "0x400041D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private Button _newGameButton;

    	[Token(Token = "0x400041E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	[SerializeField]
    	private Button _galleryButton;

    	[Token(Token = "0x400041F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	[SerializeField]
    	private Button _optionsButton;

    	[Token(Token = "0x4000420")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	[SerializeField]
    	private Button _exitButton;

    	[Token(Token = "0x4000421")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
    	[SerializeField]
    	private GoogleForm _googleForm;

    	[Token(Token = "0x4000422")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
    	[Header("Parameters for Title")]
    	[SerializeField]
    	private DialogData _confirmToQuitGameDialogData;

    	[Token(Token = "0x4000423")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
    	[SerializeField]
    	private EasingType _easingTypeOnShow;

    	[Token(Token = "0x4000424")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x11C")]
    	[SerializeField]
    	private EasingType _easingTypeOnHide;

    	[Token(Token = "0x4000425")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
    	[ResourcePopup("Audio")]
    	[SerializeField]
    	private string _startGameSfxPath;

    	[Token(Token = "0x4000426")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
    	private readonly Tweener<FloatTween> _fadeTweener;

    	[Token(Token = "0x4000427")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
    	private readonly CompositeDisposable _disposables;

    	[Token(Token = "0x4000428")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000429")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
    	private IDialogProvider _dialogProvider;

    	[Token(Token = "0x400042A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
    	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

    	[Token(Token = "0x400042B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
    	private ICustomVariableManager _naniVarManager;

    	[Token(Token = "0x400042C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
    	private IAudioManager _naniAudioManager;

    	[Token(Token = "0x400042D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x400042E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
    	private IUIManager _naniUiManager;

    	[Token(Token = "0x400042F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
    	private ISaveLoadUI _saveLoadUi;

    	[Token(Token = "0x4000430")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
    	private ICGGalleryUI _galleryUi;

    	[Token(Token = "0x4000431")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
    	private ISettingsUI _optionsUi;

    	[Token(Token = "0x4000432")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x188")]
    	private bool _visibleSelf;

    	[Token(Token = "0x1700006B")]
    	public override bool Visible
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3619A0
    		// Returns `_visibleSelf` (+0x188).
    		[Token(Token = "0x6000344")]
    		[Address(RVA = "0x3619A0", Offset = "0x3603A0", VA = "0x1803619A0", Slot = "28")]
    		get
    		{
    			return _visibleSelf;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3619B0, 20 B
    		// Tail-jumps through vtable +0x408 (slot 45 SetVisibility).
    		[Token(Token = "0x6000345")]
    		[Address(RVA = "0x3619B0", Offset = "0x3603B0", VA = "0x1803619B0", Slot = "29")]
    		set
    		{
    			SetVisibility(value);
    		}
    	}

    	[Token(Token = "0x1700006C")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3619D0, 202 B
    		// Lazy cache in +0x138 via `ServiceLocator.Get<IInputManager>(true)` (`cl = 1`).
    		[Token(Token = "0x6000346")]
    		[Address(RVA = "0x3619D0", Offset = "0x3603D0", VA = "0x1803619D0")]
    		get
    		{
    			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
    		}
    	}

    	[Token(Token = "0x1700006D")]
    	private IDialogProvider DialogProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361AA0
    		// Lazy cache in +0x140 via `ServiceLocator.Get<IDialogManager>(true)`, stored as IDialogProvider.
    		[Token(Token = "0x6000347")]
    		[Address(RVA = "0x361AA0", Offset = "0x3604A0", VA = "0x180361AA0")]
    		get
    		{
    			return _dialogProvider ??= GigaCreation.Tools.Service.ServiceLocator.Get<IDialogManager>(true);
    		}
    	}

    	[Token(Token = "0x1700006E")]
    	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361B70, 202 B
    		// Lazy cache in +0x148 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`.
    		[Token(Token = "0x6000348")]
    		[Address(RVA = "0x361B70", Offset = "0x360570", VA = "0x180361B70")]
    		get
    		{
    			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
    		}
    	}

    	[Token(Token = "0x1700006F")]
    	private ICustomVariableManager NaniVarManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361C40
    		// Lazy cache in +0x150 via `Engine.GetServiceOrErr<ICustomVariableManager>()`.
    		[Token(Token = "0x6000349")]
    		[Address(RVA = "0x361C40", Offset = "0x360640", VA = "0x180361C40")]
    		get
    		{
    			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
    		}
    	}

    	[Token(Token = "0x17000070")]
    	private IAudioManager NaniAudioManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361D10, 202 B
    		// Lazy cache in +0x158 via `Engine.GetServiceOrErr<IAudioManager>()`.
    		[Token(Token = "0x600034A")]
    		[Address(RVA = "0x361D10", Offset = "0x360710", VA = "0x180361D10")]
    		get
    		{
    			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
    		}
    	}

    	[Token(Token = "0x17000071")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361DE0
    		// Lazy cache in +0x160 via `Engine.GetServiceOrErr<IScriptPlayer>()`.
    		[Token(Token = "0x600034B")]
    		[Address(RVA = "0x361DE0", Offset = "0x3607E0", VA = "0x180361DE0")]
    		get
    		{
    			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
    		}
    	}

    	[Token(Token = "0x17000072")]
    	private IUIManager NaniUiManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361EB0
    		// Lazy cache in +0x168 via `Engine.GetServiceOrErr<IUIManager>()`.
    		[Token(Token = "0x600034C")]
    		[Address(RVA = "0x361EB0", Offset = "0x3608B0", VA = "0x180361EB0")]
    		get
    		{
    			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
    		}
    	}

    	[Token(Token = "0x17000073")]
    	private ISaveLoadUI SaveLoadUi
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x361F80, 191 B
    		// A lazy cache in +0x170 `_saveLoadUi`, filled by calling `get_NaniUiManager`
    		// (0x361EB0, not inlined) and then `IUIManager.GetUI<ISaveLoadUI>()`. A
    		// null manager throws NullReferenceException (no `?.`).
    		[Token(Token = "0x600034D")]
    		[Address(RVA = "0x361F80", Offset = "0x360980", VA = "0x180361F80")]
    		get
    		{
    			return _saveLoadUi ??= NaniUiManager.GetUI<ISaveLoadUI>();
    		}
    	}

    	[Token(Token = "0x17000074")]
    	private IManagedUI GalleryUi
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x362040, 191 B
    		// Lazy cache in +0x178 via `NaniUiManager.GetUI<ICGGalleryUI>()` (getter not inlined; null manager throws).
    		[Token(Token = "0x600034E")]
    		[Address(RVA = "0x362040", Offset = "0x360A40", VA = "0x180362040")]
    		get
    		{
    			return _galleryUi ??= NaniUiManager.GetUI<ICGGalleryUI>();
    		}
    	}

    	[Token(Token = "0x17000075")]
    	private IManagedUI OptionsUi
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x362100
    		// Lazy cache in +0x180 via `NaniUiManager.GetUI<ISettingsUI>()`.
    		[Token(Token = "0x600034F")]
    		[Address(RVA = "0x362100", Offset = "0x360B00", VA = "0x180362100")]
    		get
    		{
    			return _optionsUi ??= NaniUiManager.GetUI<ISettingsUI>();
    		}
    	}

    	[Token(Token = "0x17000076")]
    	private bool CanLoadGame
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3621C0
    		// `TryGetVariableValue<int>("g_gameProgress")` succeeded and value > 0.
    		[Token(Token = "0x6000350")]
    		[Address(RVA = "0x3621C0", Offset = "0x360BC0", VA = "0x1803621C0")]
    		get
    		{
    			return NaniVarManager.TryGetVariableValue<int>(GameProgressVarName, out int progress) && progress > 0;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362240, 103 B
    	// CustomUI.Awake (0x619530), then an unconditional `mov qword ptr [rbx+0x108], 0` (raw bytes 48 C7 83 08 01 00 00
    	// 00 00 00 00) plus write barrier: the serialized `_googleForm` reference is cleared, so the Activate branch that
    	// subscribes to it never runs in this build.
    	[Token(Token = "0x6000351")]
    	[Address(RVA = "0x362240", Offset = "0x360C40", VA = "0x180362240", Slot = "4")]
    	protected override void Awake()
    	{
    		base.Awake();
    		_googleForm = null;
    	}

    	// RECONSTRUCTED | Confidence: A (Tween argument naming: B) | RVA: 0x3622B0 (stub), MoveNext 0x363C40, 1891 B
    	// - Naninovel builder; `_fadeTweener.Running` -> Stop(); `_visibleSelf` = visible; vtable slot 55
    	//   HandleVisibilityChanged(visible).
    	// - Slot 42 CanvasGroup; Unity-null -> return. Slot 33 DisableInteraction false -> blocksRaycasts = visible.
    	// - Slot 43 ControlOpacity false -> return. time = duration ?? slot 19 FadeTime; target = visible ? 1f : 0f.
    	// - time == 0 (ucomiss/jp/jne) -> CanvasGroup.alpha = target. Otherwise easing = _visibleSelf ? +0x118 : +0x11C,
    	//   from = CanvasGroup.alpha, Tween{Duration=time, Easing, Scale=slot 21 IgnoreTimeScale, Complete=true},
    	//   FloatTween(from, target, tween, new Action<float>(slot 49 SetOpacity)); await RunAwaitable(tween, asyncToken, this).
    	[Token(Token = "0x6000352")]
    	[Address(RVA = "0x3622B0", Offset = "0x360CB0", VA = "0x1803622B0", Slot = "44")]
    	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__53))]
    	public override async Naninovel.UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken asyncToken)
    	{
    		if (_fadeTweener.Running)
    		{
    			_fadeTweener.Stop();
    		}
    		_visibleSelf = visible;
    		HandleVisibilityChanged(visible);
    		if (CanvasGroup == null)
    		{
    			return;
    		}
    		if (!DisableInteraction)
    		{
    			CanvasGroup.blocksRaycasts = visible;
    		}
    		if (!ControlOpacity)
    		{
    			return;
    		}
    		float time = duration ?? FadeTime;
    		float targetOpacity = visible ? 1f : 0f;
    		if (time == 0f)
    		{
    			CanvasGroup.alpha = targetOpacity;
    			return;
    		}
    		EasingType easing = _visibleSelf ? _easingTypeOnShow : _easingTypeOnHide;
    		FloatTween tween = new FloatTween(CanvasGroup.alpha, targetOpacity, new Tween(time, easing, IgnoreTimeScale, true), SetOpacity);
    		await _fadeTweener.RunAwaitable(tween, asyncToken, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362460
    	// Synchronous twin of ChangeVisibility: stop tween, store `_visibleSelf`, slot 55 HandleVisibilityChanged, then
    	// slot 42/33 blocksRaycasts and slot 43 ControlOpacity -> alpha = visible ? 1 : 0.
    	[Token(Token = "0x6000353")]
    	[Address(RVA = "0x362460", Offset = "0x360E60", VA = "0x180362460", Slot = "45")]
    	public override void SetVisibility(bool visible)
    	{
    		if (_fadeTweener.Running)
    		{
    			_fadeTweener.Stop();
    		}
    		_visibleSelf = visible;
    		HandleVisibilityChanged(visible);
    		if (CanvasGroup == null)
    		{
    			return;
    		}
    		if (!DisableInteraction)
    		{
    			CanvasGroup.blocksRaycasts = visible;
    		}
    		if (ControlOpacity)
    		{
    			CanvasGroup.alpha = visible ? 1f : 0f;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362690
    	// `_loadGameButton.interactable = CanLoadGame`.
    	[Token(Token = "0x6000354")]
    	[Address(RVA = "0x362690", Offset = "0x361090", VA = "0x180362690")]
    	public void OnBeginToPresentEvent()
    	{
    		_loadGameButton.interactable = CanLoadGame;
    	}

    	// RECONSTRUCTED | Confidence: A (OnClickAsObservable spelling: B) | RVA: 0x3626D0
    	// Lambdas <Activate>b__56_0..5 at 0x363130 / 0x363340 / 0x363570 / 0x3636C0 / 0x363810 / 0x363A20.
    	// - Each `button.onClick (+0x100).AsObservable().Subscribe(instance lambda).AddTo(_disposables, this)`.
    	// - _loadGameButton only when CanLoadGame; then _newGameButton, _galleryButton, _optionsButton, _exitButton.
    	// - _googleForm (Unity-live check) -> GoogleForm.OnClickAsObservable (0x33A4B0, inlined `_button` +0x20).
    	// - b__56_0: IInputManager slot 0 PlayResponseSfx(Submit); LoadGameAsync(destroyCancellationToken).Forget().
    	// - b__56_1: NaniAudioManager.PlaySfx(_startGameSfxPath +0x120), then StartGame() inlined.
    	// - b__56_2/3: Submit sfx; GalleryUi / OptionsUi PresentAsync(null, destroyCancellationToken).Forget().
    	// - b__56_4: Submit sfx; QuitGameAsync(destroyCancellationToken).Forget().
    	// - b__56_5: Submit sfx; _googleForm.OpenGoogleFormAsync(destroyCancellationToken).Forget() (Start<GoogleForm d__> inlined).
    	[Token(Token = "0x6000355")]
    	[Address(RVA = "0x3626D0", Offset = "0x3610D0", VA = "0x1803626D0")]
    	public void Activate()
    	{
    		if (CanLoadGame)
    		{
    			_loadGameButton.OnClickAsObservable().Subscribe(_ =>
    			{
    				InputManager.PlayResponseSfx(ResponseKind.Submit);
    				LoadGameAsync(destroyCancellationToken).Forget();
    			}).AddTo(_disposables, this);
    		}
    		_newGameButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			NaniAudioManager.PlaySfx(_startGameSfxPath);
    			StartGame();
    		}).AddTo(_disposables, this);
    		_galleryButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			GalleryUi.PresentAsync(null, destroyCancellationToken).Forget();
    		}).AddTo(_disposables, this);
    		_optionsButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			OptionsUi.PresentAsync(null, destroyCancellationToken).Forget();
    		}).AddTo(_disposables, this);
    		_exitButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			QuitGameAsync(destroyCancellationToken).Forget();
    		}).AddTo(_disposables, this);
    		if (_googleForm != null)
    		{
    			_googleForm.OnClickAsObservable().Subscribe(_ =>
    			{
    				InputManager.PlayResponseSfx(ResponseKind.Submit);
    				_googleForm.OpenGoogleFormAsync(destroyCancellationToken).Forget();
    			}).AddTo(_disposables, this);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362B50
    	// `_disposables.Clear()`.
    	[Token(Token = "0x6000356")]
    	[Address(RVA = "0x362B50", Offset = "0x361550", VA = "0x180362B50")]
    	public void Deactivate()
    	{
    		_disposables.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362B80 (stub), MoveNext 0x3643B0, 800 B
    	// SaveLoadUi (not inlined) ISaveLoadUI slot 1 set_PresentationMode(1 = Load); then
    	// ManagedUiExtensions.PresentAsync(SaveLoadUi, null, ct) awaited (Cysharp builder).
    	[Token(Token = "0x6000357")]
    	[Address(RVA = "0x362B80", Offset = "0x361580", VA = "0x180362B80")]
    	[AsyncStateMachine(typeof(_003CLoadGameAsync_003Ed__58))]
    	private async Cysharp.Threading.Tasks.UniTask LoadGameAsync([Optional] CancellationToken ct)
    	{
    		SaveLoadUi.PresentationMode = SaveLoadUIPresentationMode.Load;
    		await SaveLoadUi.PresentAsync(null, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362D20
    	// IWitchTrialsNaninovelManager slot 3 SetTitleScreenShown(false); TrySetVariableValue<string>("nextScenario",
    	// INaninovelManager slot 0 StartGameScenarioName); NaniScriptPlayer.ResumeAtLabel("StartGame").
    	[Token(Token = "0x6000358")]
    	[Address(RVA = "0x362D20", Offset = "0x361720", VA = "0x180362D20")]
    	private void StartGame()
    	{
    		WitchTrialsNaninovelManager.SetTitleScreenShown(false);
    		NaniVarManager.TrySetVariableValue(NextScenarioVarName, WitchTrialsNaninovelManager.StartGameScenarioName);
    		NaniScriptPlayer.ResumeAtLabel(StartGameLabelName);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362E30 (stub), MoveNext 0x3646E0, 2264 B
    	// - `<blocker>5__2` (+0x28) = new NaninovelInteractionBlocker(); `<dialog>5__3` (+0x30) = DialogProvider (getter
    	//   inlined) GetOrAddDialog(); blocker.SetSortingOrder(dialog.SortingOrder - 1).
    	// - ConfirmAsync(_confirmToQuitGameDialogData +0x110, null, ct). Result 0 (Cancel): dispose dialog, dispose
    	//   blocker, finish. Otherwise dispose dialog, ResumeAtLabel("QuitGame"), then dispose blocker.
    	[Token(Token = "0x6000359")]
    	[Address(RVA = "0x362E30", Offset = "0x361830", VA = "0x180362E30")]
    	[AsyncStateMachine(typeof(_003CQuitGameAsync_003Ed__60))]
    	private async Cysharp.Threading.Tasks.UniTask QuitGameAsync([Optional] CancellationToken ct)
    	{
    		using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker();
    		using (ProvidableDialog dialog = DialogProvider.GetOrAddDialog())
    		{
    			blocker.SetSortingOrder(dialog.SortingOrder - 1);
    			if (await dialog.ConfirmAsync(_confirmToQuitGameDialogData, null, ct) == ResponseKind.Cancel)
    			{
    				return;
    			}
    		}
    		NaniScriptPlayer.ResumeAtLabel(QuitGameLabelName);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x362FE0
    	// `_fadeTweener = new Tweener<FloatTween>()`, `_disposables = new CompositeDisposable()`, then CustomUI ctor.
    	[Token(Token = "0x600035A")]
    	[Address(RVA = "0x362FE0", Offset = "0x3619E0", VA = "0x180362FE0")]
    	public TitleUi()
    	{
    		_fadeTweener = new Tweener<FloatTween>();
    		_disposables = new CompositeDisposable();
    	}
    }
}
