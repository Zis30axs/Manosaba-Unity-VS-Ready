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
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000089")]
public class ControlPanel : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x200008A")]
	[CompilerGenerated]
	private struct _003CReturnToTitleAsync_003Ed__50
{
		[Token(Token = "0x4000235")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000236")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000237")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ControlPanel _003C_003E4__this;

		[Token(Token = "0x4000238")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x4000239")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private INaninovelInteractionBlocker _003Cblocker_003E5__2;

		[Token(Token = "0x400023A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private ProvidableDialog _003Cdialog_003E5__3;

		[Token(Token = "0x400023B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask<ResponseKind>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400023C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60001DE")]
		[Address(RVA = "0x339250", Offset = "0x337C50", VA = "0x180339250", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001DF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000218")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	// RECONSTRUCTED | Confidence: A | .cctor RVA: 0x338830, 89 B
	// Static initializer only: `Animator.StringToHash("Blink")` stored into the static field +0x0.
	private static readonly int s_blink = Animator.StringToHash("Blink");

	[Token(Token = "0x4000219")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private NaninovelScriptPlayer _returnToTitleScript;

	[Token(Token = "0x400021A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private CanvasGroup _menuCanvasGroup;

	[Token(Token = "0x400021B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _logButton;

	[Token(Token = "0x400021C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Button _saveButton;

	[Token(Token = "0x400021D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private Button _loadButton;

	[Token(Token = "0x400021E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private Button _optionsButton;

	[Token(Token = "0x400021F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private Button _titleButton;

	[Token(Token = "0x4000220")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Space]
	[SerializeField]
	private CanvasGroup _homeCanvasGroup;

	[Token(Token = "0x4000221")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private HintDisplay _hintDisplay;

	[Token(Token = "0x4000222")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[SerializeField]
	private Animator _naviAnimator;

	[Token(Token = "0x4000223")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[Space]
	[SerializeField]
	private CanvasGroup _openButtonCanvasGroup;

	[Token(Token = "0x4000224")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private Button _openButton;

	[Token(Token = "0x4000225")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	[SerializeField]
	private CanvasGroup _closeButtonCanvasGroup;

	[Token(Token = "0x4000226")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	[SerializeField]
	private Button _closeButton;

	[Token(Token = "0x4000227")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	[SerializeField]
	private Button _rayBlocker;

	[Token(Token = "0x4000228")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	[Header("Parameters")]
	[SerializeField]
	private DialogData _returnToTitleDialogData;

	[Token(Token = "0x4000229")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _openSfxPath;

	[Token(Token = "0x400022A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _closeSfxPath;

	[Token(Token = "0x400022B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x400022C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private IDialogProvider _dialogProvider;

	[Token(Token = "0x400022D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC0")]
	private Naninovel.IInputManager _naniInputManager;

	[Token(Token = "0x400022E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400022F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x4000230")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000231")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private IBacklogUI _logUi;

	[Token(Token = "0x4000232")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private ISaveLoadUI _saveLoadUi;

	[Token(Token = "0x4000233")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private ISettingsUI _optionsUi;

	[Token(Token = "0x4000234")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	private bool _isOpened;

	[Token(Token = "0x1700002C")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x336FF0
		// Lazy cache in +0xB0 via `ServiceLocator.Get<IInputManager>(true)` (`cl = 1`).
		[Token(Token = "0x60001C2")]
		[Address(RVA = "0x336FF0", Offset = "0x3359F0", VA = "0x180336FF0")]
		get
		{
			return _inputManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(true);
		}
	}

	[Token(Token = "0x1700002D")]
	private IDialogProvider DialogProvider
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3370C0
		// Lazy cache in +0xB8 via `ServiceLocator.Get<IDialogManager>(true)`, stored as IDialogProvider.
		[Token(Token = "0x60001C3")]
		[Address(RVA = "0x3370C0", Offset = "0x335AC0", VA = "0x1803370C0")]
		get
		{
			return _dialogProvider ??= GigaCreation.Tools.Service.ServiceLocator.Get<IDialogManager>(true);
		}
	}

	[Token(Token = "0x1700002E")]
	private Naninovel.IInputManager NaniInputManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337190
		// Lazy cache in +0xC0 via `Engine.GetServiceOrErr<Naninovel.IInputManager>()`.
		[Token(Token = "0x60001C4")]
		[Address(RVA = "0x337190", Offset = "0x335B90", VA = "0x180337190")]
		get
		{
			return _naniInputManager ??= Engine.GetServiceOrErr<Naninovel.IInputManager>();
		}
	}

	[Token(Token = "0x1700002F")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337260
		// Lazy cache in +0xC8 via `Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x60001C5")]
		[Address(RVA = "0x337260", Offset = "0x335C60", VA = "0x180337260")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x17000030")]
	private IUIManager NaniUiManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337330
		// Lazily-cached engine service lookup: `_naniUiManager ??=
		// Naninovel.Engine.GetServiceOrErr<IUIManager>();`
		[Token(Token = "0x60001C6")]
		[Address(RVA = "0x337330", Offset = "0x335D30", VA = "0x180337330")]
		get
		{
			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
		}
	}

	[Token(Token = "0x17000031")]
	private IScriptPlayer NaniScriptPlayer
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337400 (same lazily-cached
		// GetServiceOrErr pattern as NaniUiManager above)
		[Token(Token = "0x60001C7")]
		[Address(RVA = "0x337400", Offset = "0x335E00", VA = "0x180337400")]
		get
		{
			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
		}
	}

	[Token(Token = "0x17000032")]
	private IBacklogUI LogUi
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3374D0
		// Lazily-cached lookup through the local UI manager, not the engine
		// directly: `_logUi ??= NaniUiManager.GetUI<IBacklogUI>();`
		[Token(Token = "0x60001C8")]
		[Address(RVA = "0x3374D0", Offset = "0x335ED0", VA = "0x1803374D0")]
		get
		{
			return _logUi ??= NaniUiManager.GetUI<IBacklogUI>();
		}
	}

	[Token(Token = "0x17000033")]
	private ISaveLoadUI SaveLoadUi
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337590 (same pattern as LogUi above)
		[Token(Token = "0x60001C9")]
		[Address(RVA = "0x337590", Offset = "0x335F90", VA = "0x180337590")]
		get
		{
			return _saveLoadUi ??= NaniUiManager.GetUI<ISaveLoadUI>();
		}
	}

	[Token(Token = "0x17000034")]
	private ISettingsUI OptionsUi
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x337650
		// Lazy cache in +0xF0 via `NaniUiManager.GetUI<ISettingsUI>()`.
		[Token(Token = "0x60001CA")]
		[Address(RVA = "0x337650", Offset = "0x336050", VA = "0x180337650")]
		get
		{
			return _optionsUi ??= NaniUiManager.GetUI<ISettingsUI>();
		}
	}

	// RECONSTRUCTED | Confidence: A (OnClickAsObservable spelling: B) | RVA: 0x337710, 1570 B; lambdas 0x338890 0x3389E0 0x338AE0 0x338BE0 0x338D30 0x338F50 0x338FF0 0x339090 0x3391B0
	// Lambdas <Start>b__47_0..8 at 0x338890 / 0x3389E0 / 0x338AE0 / 0x338BE0 / 0x338D30 / 0x338F50 / 0x338FF0 /
	// 0x339090 / 0x3391B0.
	// - Close() first; then each button's onClick (+0x100) AsObservable -> Subscribe(instance lambda) -> AddTo(this):
	//   _logButton +0x30, _saveButton +0x38, _loadButton +0x40, _optionsButton +0x48, _titleButton +0x50, _openButton
	//   +0x78, _closeButton +0x88.
	// - Behind liveness tests: `_hintDisplay` (+0x60) OnClickAsObservable (inlined, _hintApp.onClick) and `_rayBlocker`
	//   (+0x90) onClick.
	// - Menu lambdas: IInputManager slot 0 PlayResponseSfx(Submit), Close(), then ManagedUiExtensions.PresentAsync(null,
	//   destroyCancellationToken).Forget() on LogUi / SaveLoadUi (slot 1 PresentationMode = Save 2 / Load 1 first) /
	//   OptionsUi; the title lambda runs ReturnToTitleAsync(destroyCancellationToken).Forget().
	// - Open/close/ray-blocker lambdas: NaniAudioManager slot PlaySfx(_openSfxPath +0xA0 / _closeSfxPath +0xA8)
	//   (result discarded) then Open()/Close().
	// - Hint lambda: _hintDisplay.Present(); `_homeCanvasGroup` (+0x58) hidden; `_hintDisplay.OnClose = Open`.
	[Token(Token = "0x60001CB")]
	[Address(RVA = "0x337710", Offset = "0x336110", VA = "0x180337710")]
	private void Start()
	{
		Close();
		_logButton.OnClickAsObservable().Subscribe(_ =>
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			Close();
			LogUi.PresentAsync(null, destroyCancellationToken).Forget();
		}).AddTo(this);
		_saveButton.OnClickAsObservable().Subscribe(_ =>
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			Close();
			SaveLoadUi.PresentationMode = SaveLoadUIPresentationMode.Save;
			SaveLoadUi.PresentAsync(null, destroyCancellationToken).Forget();
		}).AddTo(this);
		_loadButton.OnClickAsObservable().Subscribe(_ =>
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			Close();
			SaveLoadUi.PresentationMode = SaveLoadUIPresentationMode.Load;
			SaveLoadUi.PresentAsync(null, destroyCancellationToken).Forget();
		}).AddTo(this);
		_optionsButton.OnClickAsObservable().Subscribe(_ =>
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			Close();
			OptionsUi.PresentAsync(null, destroyCancellationToken).Forget();
		}).AddTo(this);
		_titleButton.OnClickAsObservable().Subscribe(_ =>
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			Close();
			ReturnToTitleAsync(destroyCancellationToken).Forget();
		}).AddTo(this);
		_openButton.OnClickAsObservable().Subscribe(_ =>
		{
			NaniAudioManager.PlaySfx(_openSfxPath);
			Open();
		}).AddTo(this);
		_closeButton.OnClickAsObservable().Subscribe(_ =>
		{
			NaniAudioManager.PlaySfx(_closeSfxPath);
			Close();
		}).AddTo(this);
		if (_hintDisplay != null)
		{
			_hintDisplay.OnClickAsObservable().Subscribe(_ =>
			{
				_hintDisplay.Present();
				_homeCanvasGroup.alpha = 0f;
				_homeCanvasGroup.interactable = false;
				_homeCanvasGroup.blocksRaycasts = false;
				_hintDisplay.OnClose = Open;
			}).AddTo(this);
		}
		if (_rayBlocker != null)
		{
			_rayBlocker.OnClickAsObservable().Subscribe(_ =>
			{
				NaniAudioManager.PlaySfx(_closeSfxPath);
				Close();
			}).AddTo(this);
		}
	}

	// RECONSTRUCTED | Confidence: A (null-conditional spelling: B) | RVA: 0x337D40
	// NaniInputManager (real call) -> Naninovel IInputManager slot 6 GetSampler("Pause"), skipped (null) when the manager is
	// null; a non-null sampler gets IInputSampler slot 0 add_OnStart(ToggleVisibility).
	[Token(Token = "0x60001CC")]
	[Address(RVA = "0x337D40", Offset = "0x336740", VA = "0x180337D40")]
	private void OnEnable()
	{
		IInputSampler pauseSampler = NaniInputManager?.GetSampler("Pause");
		if (pauseSampler != null)
		{
			pauseSampler.OnStart += ToggleVisibility;
		}
	}

	// RECONSTRUCTED | Confidence: A (null-conditional spelling: B) | RVA: 0x337EB0
	// Mirror of OnEnable with IInputSampler slot 1 remove_OnStart(ToggleVisibility).
	[Token(Token = "0x60001CD")]
	[Address(RVA = "0x337EB0", Offset = "0x3368B0", VA = "0x180337EB0")]
	private void OnDisable()
	{
		IInputSampler pauseSampler = NaniInputManager?.GetSampler("Pause");
		if (pauseSampler != null)
		{
			pauseSampler.OnStart -= ToggleVisibility;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x338030 (stub), MoveNext 0x339250
	// - `<blocker>5__2` (+0x28) = new NaninovelInteractionBlocker() (parameterless ctor 0xCFBE30).
	// - `<dialog>5__3` (+0x30) = DialogProvider (getter inlined) GetOrAddDialog(); blocker.SetSortingOrder(dialog.SortingOrder - 1);
	//   await dialog.ConfirmAsync(_returnToTitleDialogData +0x98, null, ct).
	// - Cancel (0): dialog then blocker disposed, return. Otherwise the dialog's `using` ends (dispose, field cleared) and
	//   `_returnToTitleScript` (+0x20) PlayAsync(AsyncToken from ct) is awaited (Naninovel awaiter) inside the blocker's `using`.
	[Token(Token = "0x60001CE")]
	[Address(RVA = "0x338030", Offset = "0x336A30", VA = "0x180338030")]
	[AsyncStateMachine(typeof(_003CReturnToTitleAsync_003Ed__50))]
	private async Cysharp.Threading.Tasks.UniTask ReturnToTitleAsync([Optional] CancellationToken ct)
	{
		using INaninovelInteractionBlocker blocker = new NaninovelInteractionBlocker();
		using (ProvidableDialog dialog = DialogProvider.GetOrAddDialog())
		{
			blocker.SetSortingOrder(dialog.SortingOrder - 1);
			if (await dialog.ConfirmAsync(_returnToTitleDialogData, null, ct) == ResponseKind.Cancel)
			{
				return;
			}
		}
		await _returnToTitleScript.PlayAsync(ct);
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x3381E0
	// `_isOpened` (+0xF8) = true; IScriptPlayer slot 35 SetAutoPlayEnabled(false), slot 34 SetSkipEnabled(false); menu (+0x28),
	// home (+0x58), close-button (+0x80) groups on and open-button (+0x70) group off; live `_naviAnimator` (+0x68)
	// SetBool(s_blink, true).
	[Token(Token = "0x60001CF")]
	[Address(RVA = "0x3381E0", Offset = "0x336BE0", VA = "0x1803381E0")]
	private void Open()
	{
		_isOpened = true;
		NaniScriptPlayer.SetAutoPlayEnabled(false);
		NaniScriptPlayer.SetSkipEnabled(false);
		_menuCanvasGroup.alpha = 1f;
		_menuCanvasGroup.interactable = true;
		_menuCanvasGroup.blocksRaycasts = true;
		_homeCanvasGroup.alpha = 1f;
		_homeCanvasGroup.interactable = true;
		_homeCanvasGroup.blocksRaycasts = true;
		_openButtonCanvasGroup.alpha = 0f;
		_openButtonCanvasGroup.interactable = false;
		_openButtonCanvasGroup.blocksRaycasts = false;
		_closeButtonCanvasGroup.alpha = 1f;
		_closeButtonCanvasGroup.interactable = true;
		_closeButtonCanvasGroup.blocksRaycasts = true;
		if (_naviAnimator != null)
		{
			_naviAnimator.SetBool(s_blink, true);
		}
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x338460
	// `_isOpened` = false; groups inverted from Open; live `_naviAnimator` SetBool(s_blink, false); live `_hintDisplay`
	// (+0x60) Close().
	[Token(Token = "0x60001D0")]
	[Address(RVA = "0x338460", Offset = "0x336E60", VA = "0x180338460")]
	private void Close()
	{
		_isOpened = false;
		_menuCanvasGroup.alpha = 0f;
		_menuCanvasGroup.interactable = false;
		_menuCanvasGroup.blocksRaycasts = false;
		_homeCanvasGroup.alpha = 0f;
		_homeCanvasGroup.interactable = false;
		_homeCanvasGroup.blocksRaycasts = false;
		_openButtonCanvasGroup.alpha = 1f;
		_openButtonCanvasGroup.interactable = true;
		_openButtonCanvasGroup.blocksRaycasts = true;
		_closeButtonCanvasGroup.alpha = 0f;
		_closeButtonCanvasGroup.interactable = false;
		_closeButtonCanvasGroup.blocksRaycasts = false;
		if (_naviAnimator != null)
		{
			_naviAnimator.SetBool(s_blink, false);
		}
		if (_hintDisplay != null)
		{
			_hintDisplay.Close();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x338710
	// `_isOpened` (+0xF8) false -> tail jump to Open, otherwise to Close.
	[Token(Token = "0x60001D1")]
	[Address(RVA = "0x338710", Offset = "0x337110", VA = "0x180338710")]
	private void ToggleVisibility()
	{
		if (_isOpened)
		{
			Close();
		}
		else
		{
			Open();
		}
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x338730
	// Live `_hintDisplay` -> HintDisplay.OnShow inlined (notice SetActive(_hintMessages.Length > 0)).
	[Token(Token = "0x60001D2")]
	[Address(RVA = "0x338730", Offset = "0x337130", VA = "0x180338730")]
	public void OnShow()
	{
		if (_hintDisplay != null)
		{
			_hintDisplay.OnShow();
		}
	}

	[Token(Token = "0x60001D3")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ControlPanel()
	{
	}
}
