using GigaCreation.NaninovelExtender.Common;
using GigaCreation.Tools.Debugging.General;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Cameras;
using GigaCreation.NaninovelExtender.Core;
using GigaCreation.NaninovelExtender.Localization;
using GigaCreation.Tools.Service;
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
    [Token(Token = "0x20000C8")]
    public class OptionsUi : CustomUI, ISettingsUI, IManagedUI
    {
    	[Serializable]
    	[Token(Token = "0x20000C9")]
    	public class PageCanvas
    	{
    		[Token(Token = "0x400039C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		[SerializeField]
    		private Toggle _toggle;

    		[Token(Token = "0x400039D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		[SerializeField]
    		private Canvas _canvas;

    		[Token(Token = "0x1700005E")]
    		public Toggle Toggle
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared generic reference-field getter, +0x10 `_toggle`)
    			[Token(Token = "0x60002F9")]
    			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    			get
    			{
    				return _toggle;
    			}
    		}

    		[Token(Token = "0x60002FA")]
    		[Address(RVA = "0x356FD0", Offset = "0x3559D0", VA = "0x180356FD0")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x356FD0, 71 B -- Behaviour.set_enabled on _canvas (+0x18), then a tail jump
    		//   to Toggle.Set(active, sendCallback: true) on _toggle (+0x10), which is the isOn setter.  While this was a stub
    		//   the 文字/图形/声音 tabs only changed their highlight: every page canvas kept its prefab state.
    		public void SetActive(bool active)
    		{
    			_canvas.enabled = active;
    			_toggle.isOn = active;
    		}

    		[Token(Token = "0x60002FB")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public PageCanvas()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000CE")]
    	[CompilerGenerated]
    	private struct _003CChangeLocaleAsync_003Ed__54
    {
    		[Token(Token = "0x40003A7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40003A8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40003A9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public OptionsUi _003C_003E4__this;

    		[Token(Token = "0x40003AA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string locale;

    		[Token(Token = "0x40003AB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x40003AC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000306")]
    		[Address(RVA = "0x357330", Offset = "0x355D30", VA = "0x180357330", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000307")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000CF")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__49
    {
    		[Token(Token = "0x40003AD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40003AE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Naninovel.Async.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40003AF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public OptionsUi _003C_003E4__this;

    		[Token(Token = "0x40003B0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Naninovel.UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x40003B1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private List<string> _003Cnames_003E5__3;

    		[Token(Token = "0x40003B2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask<TMPro.TMP_FontAsset[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40003B3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000308")]
    		[Address(RVA = "0x357920", Offset = "0x356320", VA = "0x180357920", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000309")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000D0")]
    	[CompilerGenerated]
    	private struct _003CSaveAsync_003Ed__56
    {
    		[Token(Token = "0x40003B4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40003B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40003B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public OptionsUi _003C_003E4__this;

    		[Token(Token = "0x40003B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x40003B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x40003B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600030A")]
    		[Address(RVA = "0x358990", Offset = "0x357390", VA = "0x180358990", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600030B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400037D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	[Header("References")]
    	[SerializeField]
    	private LabeledSlider _messageSpeedSlider;

    	[Token(Token = "0x400037E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private LabeledSlider _autoDelaySlider;

    	[Token(Token = "0x400037F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	[SerializeField]
    	private OptionItemToggleGroup _skipModeToggleGroup;

    	[Token(Token = "0x4000380")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	[SerializeField]
    	private OptionItemToggleGroup _toggleSkipModeToggleGroup;

    	[Token(Token = "0x4000381")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	[SerializeField]
    	private OptionItemToggleGroup _criticalChoiceHintToggleGroup;

    	[Token(Token = "0x4000382")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
    	[SerializeField]
    	private LocalizationDropdown _languageDropdown;

    	[Token(Token = "0x4000383")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
    	[SerializeField]
    	private RebuildableButton _resetMessageOptionsButton;

    	[Token(Token = "0x4000384")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
    	[Space]
    	[SerializeField]
    	private OptionItemToggleGroup _screenModeToggleGroup;

    	[Token(Token = "0x4000385")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
    	[SerializeField]
    	private OptionItemDropdown _resolutionDropdown;

    	[Token(Token = "0x4000386")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
    	[SerializeField]
    	private OptionItemDropdown _frameRateDropdown;

    	[Token(Token = "0x4000387")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
    	[SerializeField]
    	private OptionItemToggleGroup _postProcessingToggleGroup;

    	[Token(Token = "0x4000388")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
    	[SerializeField]
    	private RebuildableButton _resetGraphicOptionsButton;

    	[Token(Token = "0x4000389")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
    	[Space]
    	[SerializeField]
    	private LabeledSlider _masterVolumeSlider;

    	[Token(Token = "0x400038A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
    	[SerializeField]
    	private LabeledSlider _bgmVolumeSlider;

    	[Token(Token = "0x400038B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
    	[SerializeField]
    	private LabeledSlider _sfxVolumeSlider;

    	[Token(Token = "0x400038C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
    	[SerializeField]
    	private LabeledSlider _voiceVolumeSlider;

    	[Token(Token = "0x400038D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
    	[SerializeField]
    	private RebuildableButton _resetAudioOptionsButton;

    	[Token(Token = "0x400038E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
    	[Header("Pages")]
    	[SerializeField]
    	private PageCanvas[] _pages;

    	[Token(Token = "0x400038F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
    	private GigaCreation.Essentials.Input.IInputManager _inputManager;

    	[Token(Token = "0x4000390")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
    	private GigaCreation.Essentials.Localization.ILocalizationManager _localizationManager;

    	[Token(Token = "0x4000391")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
    	private INaninovelManager _naninovelManager;

    	[Token(Token = "0x4000392")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x188")]
    	private ICameraManagerExtended _naniCameraManager;

    	[Token(Token = "0x4000393")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x190")]
    	private IAudioManager _naniAudioManager;

    	[Token(Token = "0x4000394")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x198")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x4000395")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1A0")]
    	private ITextPrinterManager _naniPrinterManager;

    	[Token(Token = "0x4000396")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1A8")]
    	private IWitchTrialsChoiceHandlerManager _witchTrialsChoiceHandlerManager;

    	[Token(Token = "0x4000397")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1B0")]
    	private ILocalizationManagerExtended _naniLocalizationManager;

    	[Token(Token = "0x4000398")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1B8")]
    	private string[] _availableLocales;

    	[Token(Token = "0x4000399")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1C0")]
    	private int _unavailableResolutionOptionCount;

    	[Token(Token = "0x400039A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1C8")]
    	private CompositeDisposable _disposables;

    	[Token(Token = "0x400039B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x1D0")]
    	private Cysharp.Threading.Tasks.UniTaskCompletionSource _saveUcs;

    	[Token(Token = "0x17000055")]
    	private GigaCreation.Essentials.Input.IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352860
    		// Lazily-cached ServiceLocator lookup: `_inputManager ??=
    		// ServiceLocator.Get<IInputManager>(includeDerivedType: true);`
    		[Token(Token = "0x60002D3")]
    		[Address(RVA = "0x352860", Offset = "0x351260", VA = "0x180352860")]
    		get
    		{
    			return _inputManager ??= ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(includeDerivedType: true);
    		}
    	}

    	[Token(Token = "0x17000056")]
    	private GigaCreation.Essentials.Localization.ILocalizationManager LocalizationManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352930, 202 B
    		// Lazy cache in +0x178 via `ServiceLocator.Get<ILocalizationManager>(true)`.
    		[Token(Token = "0x60002D4")]
    		[Address(RVA = "0x352930", Offset = "0x351330", VA = "0x180352930")]
    		get
    		{
    			return _localizationManager ??= ServiceLocator.Get<GigaCreation.Essentials.Localization.ILocalizationManager>(includeDerivedType: true);
    		}
    	}

    	[Token(Token = "0x17000057")]
    	private INaninovelManager NaninovelManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352A00, 202 B
    		// Lazy cache in +0x180 via `ServiceLocator.Get<INaninovelManager>(true)`.
    		[Token(Token = "0x60002D5")]
    		[Address(RVA = "0x352A00", Offset = "0x351400", VA = "0x180352A00")]
    		get
    		{
    			return _naninovelManager ??= ServiceLocator.Get<INaninovelManager>(includeDerivedType: true);
    		}
    	}

    	[Token(Token = "0x17000058")]
    	private ICameraManagerExtended NaniCameraManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352AD0, 202 B
    		// Lazy cache in +0x188 via `Engine.GetServiceOrErr<ICameraManagerExtended>()`.
    		[Token(Token = "0x60002D6")]
    		[Address(RVA = "0x352AD0", Offset = "0x3514D0", VA = "0x180352AD0")]
    		get
    		{
    			return _naniCameraManager ??= Engine.GetServiceOrErr<ICameraManagerExtended>();
    		}
    	}

    	[Token(Token = "0x17000059")]
    	private IAudioManager NaniAudioManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352BA0, 202 B
    		// Lazy cache in +0x190 via `Engine.GetServiceOrErr<IAudioManager>()`.
    		[Token(Token = "0x60002D7")]
    		[Address(RVA = "0x352BA0", Offset = "0x3515A0", VA = "0x180352BA0")]
    		get
    		{
    			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
    		}
    	}

    	[Token(Token = "0x1700005A")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352C70, 202 B
    		// Lazy cache in +0x198 via `Engine.GetServiceOrErr<IScriptPlayer>()`.
    		[Token(Token = "0x60002D8")]
    		[Address(RVA = "0x352C70", Offset = "0x351670", VA = "0x180352C70")]
    		get
    		{
    			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
    		}
    	}

    	[Token(Token = "0x1700005B")]
    	private ITextPrinterManager NaniPrinterManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352D40, 202 B
    		// Lazy cache in +0x1A0 via `Engine.GetServiceOrErr<ITextPrinterManager>()`.
    		[Token(Token = "0x60002D9")]
    		[Address(RVA = "0x352D40", Offset = "0x351740", VA = "0x180352D40")]
    		get
    		{
    			return _naniPrinterManager ??= Engine.GetServiceOrErr<ITextPrinterManager>();
    		}
    	}

    	[Token(Token = "0x1700005C")]
    	private IWitchTrialsChoiceHandlerManager WitchTrialsChoiceHandlerManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352E10, 202 B
    		// Lazy cache in +0x1A8 via `Naninovel.Engine.GetServiceOrErr<IWitchTrialsChoiceHandlerManager>()`
    		// (an engine service, not a ServiceLocator lookup).
    		[Token(Token = "0x60002DA")]
    		[Address(RVA = "0x352E10", Offset = "0x351810", VA = "0x180352E10")]
    		get
    		{
    			return _witchTrialsChoiceHandlerManager ??= Naninovel.Engine.GetServiceOrErr<IWitchTrialsChoiceHandlerManager>();
    		}
    	}

    	[Token(Token = "0x1700005D")]
    	private ILocalizationManagerExtended NaniLocalizationManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x352EE0, 202 B
    		// Lazy cache in +0x1B0 via `Engine.GetServiceOrErr<ILocalizationManagerExtended>()`.
    		[Token(Token = "0x60002DB")]
    		[Address(RVA = "0x352EE0", Offset = "0x3518E0", VA = "0x180352EE0")]
    		get
    		{
    			return _naniLocalizationManager ??= Engine.GetServiceOrErr<ILocalizationManagerExtended>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (base.Initialize inlining and wrapper names: B) | RVA: 0x352FB0 (stub), MoveNext 0x357920, 4199 B; lambdas 0x354A90 0x3570C0 0x3570D0 0x3571A0 0x357230
    	// - `<task>5__2` (+0x20) is stored as default before anything runs: CustomUI.Initialize (0x3483B0) is inlined, so this
    	//   is `base.Initialize()`; it is awaited last (u__2).
    	// - DebugOptionsBase.IsDebugMode: DisplayClass49_0.configuredLanguages = Languages.GetAll() (inlined to get_configured)
    	//   .Select(<>c.b__49_2, the shared `mov rax,[rdx]` = Language.Tag).ToArray(); _availableLocales = ILocalizationManager
    	//   slot 3 AvailableLocales.OrderBy(b__3 = configuredLanguages.IndexOf(locale)).ToArray().
    	//   Otherwise ILocalizationManagerExtended slot 0 ValidLocales.ToArray().
    	// - `<names>5__3` = _availableLocales.Select(<>O Languages.GetNameByTag).ToList().
    	// - await EnumerableAsyncExtensions.Select(_availableLocales, b__49_4) via UniTaskExtensions.GetAwaiter<TMP_FontAsset>;
    	//   b__49_4 = LocalizationManager (inlined; ILocalizationManager : IFontProvider) slot 0
    	//   GetOrLoadFontAssetAsync(locale.ToLocaleKind(), destroyCancellationToken).
    	// - _languageDropdown: ClearOptions / AddOptions(names) / `is TweeningDropdownLocalized` -> ItemFontAssets (+0x1F0) =
    	//   fonts, i.e. LocalizationDropdown.SetOptions(options, fontAssets) inlined.
    	// - _unavailableResolutionOptionCount = Configuration(+0x18 ResolutionsForPc).Length - slot 8 AvailableResolutionsForPc.Length.
    	// - _resolutionDropdown.SetOptions(AvailableResolutionsForPc.Select("{0} x {1}").ToList());
    	//   _frameRateDropdown.SetOptions(Configuration.TargetFpsValues (+0x28).Select("{0} fps").ToList()).
    	[Token(Token = "0x60002DC")]
    	[Address(RVA = "0x352FB0", Offset = "0x3519B0", VA = "0x180352FB0", Slot = "83")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__49))]
    	public override async Naninovel.UniTask Initialize()
    	{
    		Naninovel.UniTask task = base.Initialize();
    		if (DebugOptionsBase.IsDebugMode)
    		{
    			string[] configuredLanguages = Languages.GetAll().Select(language => language.Tag).ToArray();
    			_availableLocales = NaniLocalizationManager.AvailableLocales.OrderBy(locale => configuredLanguages.IndexOf(locale)).ToArray();
    		}
    		else
    		{
    			_availableLocales = NaniLocalizationManager.ValidLocales.ToArray();
    		}
    		List<string> names = _availableLocales.Select(Languages.GetNameByTag).ToList();
    		TMPro.TMP_FontAsset[] fontAssets = await EnumerableAsyncExtensions.Select(_availableLocales, locale => LocalizationManager.GetOrLoadFontAssetAsync(locale.ToLocaleKind(), destroyCancellationToken));
    		_languageDropdown.SetOptions(names, fontAssets);
    		_unavailableResolutionOptionCount = NaniCameraManager.Configuration.ResolutionsForPc.Length - NaniCameraManager.AvailableResolutionsForPc.Length;
    		_resolutionDropdown.SetOptions(NaniCameraManager.AvailableResolutionsForPc.Select(size => string.Format("{0} x {1}", size.x, size.y)).ToList());
    		_frameRateDropdown.SetOptions(NaniCameraManager.Configuration.TargetFpsValues.Select(fps => string.Format("{0} fps", fps)).ToList());
    		await task;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3530E0, 163 B
    	// visible -> ActivateUi(); otherwise Dispose() (inlined); then tail-jumps to CustomUI.HandleVisibilityChanged.
    	[Token(Token = "0x60002DD")]
    	[Address(RVA = "0x3530E0", Offset = "0x351AE0", VA = "0x1803530E0", Slot = "55")]
    	protected override void HandleVisibilityChanged(bool visible)
    	{
    		if (visible)
    		{
    			ActivateUi();
    		}
    		else
    		{
    			Dispose();
    		}
    		base.HandleVisibilityChanged(visible);
    	}

    	// RECONSTRUCTED | Confidence: A (wrapper names: B) | RVA: 0x353190, 3636 B; lambdas 0x354C70 0x354CE0 0x354D40 0x354DB0 0x354E10 0x30C000 0x354EC0 0x354F70 0x355180 0x355410 0x3554C0 0x355570 0x355610 0x3556C0 0x355890 0x355900 0x355970 0x3559E0 0x355A50 0x357280
    	// - Early return while _disposables (+0x1C8) exists; else new CompositeDisposable, ApplyOptionValuesToUi(), and every
    	//   subscription is `.Subscribe(lambda).AddTo(_disposables, this)`.
    	// - Sliders: value / 10f into ITextPrinterManager slot 7 BaseRevealSpeed, slot 9 BaseAutoDelay, IAudioManager slots
    	//   2/4/6/8 Master/Bgm/Sfx/Voice volume; the two OnConfirm lambdas only play the Submit sfx (IInputManager slot 0, arg 1).
    	// - Toggle groups/dropdowns play Submit first: IScriptPlayer slot 20 SkipMode = idx == 0 ? Everything : ReadOnly;
    	//   _toggleSkipModeToggleGroup gets the cached static <>c.b__51_5, folded into the empty body 0x30C000;
    	//   IWitchTrialsChoiceHandlerManager slot 1 = idx == 1; ChangeLocaleAsync(_availableLocales[idx]).Forget();
    	//   ICameraManagerExtended slot 1 FullScreen = idx == 1, slot 3 ResolutionLevel = idx + _unavailableResolutionOptionCount,
    	//   slot 5 TargetFpsLevel = level, slot 7 IsPostProcessingEnabled = idx == 1.
    	// - Reset buttons (RebuildableButton.OnClickAsObservable, ResponseKind): copy the IEngineService<TConfig>.Configuration
    	//   defaults back (DefaultBaseRevealSpeed +0x40 / DefaultBaseAutoDelay +0x44, DefaultSkipMode +0x18,
    	//   DefaultCriticalChoiceHintEnabled +0x18; DefaultGraphicsOptions +0x30 .FullScreen/.TargetFpsLevel/.PostProcessingActivity
    	//   with ResolutionLevel = _unavailableResolutionOptionCount; Default*Volume +0x30..+0x3C), then ApplyOptionValuesToUi().
    	// - Pages: `for` over _pages (array reloaded each pass) with a per-iteration DisplayClass51_0 {page, this}; Toggle (+0x10,
    	//   getter inlined).OnValueChangedAsObservable -> b__19: Submit sfx only when enabled, then page.SetActive(enable);
    	//   after subscribing, page.SetActive(i == 0). SetActive (0x356FD0) is inlined in both places.
    	[Token(Token = "0x60002DE")]
    	[Address(RVA = "0x353190", Offset = "0x351B90", VA = "0x180353190")]
    	private void ActivateUi()
    	{
    		if (_disposables != null)
    		{
    			return;
    		}
    		_disposables = new CompositeDisposable();
    		ApplyOptionValuesToUi();
    		_messageSpeedSlider.OnSlideAsObservable().Subscribe(x => NaniPrinterManager.BaseRevealSpeed = x / 10f).AddTo(_disposables, this);
    		_messageSpeedSlider.OnConfirmAsObservable().Subscribe(_ => InputManager.PlayResponseSfx(ResponseKind.Submit)).AddTo(_disposables, this);
    		_autoDelaySlider.OnSlideAsObservable().Subscribe(x => NaniPrinterManager.BaseAutoDelay = x / 10f).AddTo(_disposables, this);
    		_autoDelaySlider.OnConfirmAsObservable().Subscribe(_ => InputManager.PlayResponseSfx(ResponseKind.Submit)).AddTo(_disposables, this);
    		_skipModeToggleGroup.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniScriptPlayer.SkipMode = idx == 0 ? PlayerSkipMode.Everything : PlayerSkipMode.ReadOnly;
    		}).AddTo(_disposables, this);
    		_toggleSkipModeToggleGroup.OnValueChangedAsObservable().Subscribe(idx => { }).AddTo(_disposables, this);
    		_criticalChoiceHintToggleGroup.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			WitchTrialsChoiceHandlerManager.CriticalChoiceHintEnabled = idx == 1;
    		}).AddTo(_disposables, this);
    		_languageDropdown.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			ChangeLocaleAsync(_availableLocales[idx]).Forget();
    		}).AddTo(_disposables, this);
    		_resetMessageOptionsButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniPrinterManager.BaseRevealSpeed = NaniPrinterManager.Configuration.DefaultBaseRevealSpeed;
    			NaniPrinterManager.BaseAutoDelay = NaniPrinterManager.Configuration.DefaultBaseAutoDelay;
    			NaniScriptPlayer.SkipMode = NaniScriptPlayer.Configuration.DefaultSkipMode;
    			WitchTrialsChoiceHandlerManager.CriticalChoiceHintEnabled = WitchTrialsChoiceHandlerManager.Configuration.DefaultCriticalChoiceHintEnabled;
    			ApplyOptionValuesToUi();
    		}).AddTo(_disposables, this);
    		_screenModeToggleGroup.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniCameraManager.FullScreen = idx == 1;
    		}).AddTo(_disposables, this);
    		_resolutionDropdown.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniCameraManager.ResolutionLevel = idx + _unavailableResolutionOptionCount;
    		}).AddTo(_disposables, this);
    		_frameRateDropdown.OnValueChangedAsObservable().Subscribe(level =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniCameraManager.TargetFpsLevel = level;
    		}).AddTo(_disposables, this);
    		_postProcessingToggleGroup.OnValueChangedAsObservable().Subscribe(idx =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniCameraManager.IsPostProcessingEnabled = idx == 1;
    		}).AddTo(_disposables, this);
    		_resetGraphicOptionsButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniCameraManager.FullScreen = NaniCameraManager.Configuration.DefaultGraphicsOptions.FullScreen;
    			NaniCameraManager.ResolutionLevel = _unavailableResolutionOptionCount;
    			NaniCameraManager.TargetFpsLevel = NaniCameraManager.Configuration.DefaultGraphicsOptions.TargetFpsLevel;
    			NaniCameraManager.IsPostProcessingEnabled = NaniCameraManager.Configuration.DefaultGraphicsOptions.PostProcessingActivity;
    			ApplyOptionValuesToUi();
    		}).AddTo(_disposables, this);
    		_masterVolumeSlider.OnSlideAsObservable().Subscribe(x => NaniAudioManager.MasterVolume = x / 10f).AddTo(_disposables, this);
    		_bgmVolumeSlider.OnSlideAsObservable().Subscribe(x => NaniAudioManager.BgmVolume = x / 10f).AddTo(_disposables, this);
    		_sfxVolumeSlider.OnSlideAsObservable().Subscribe(x => NaniAudioManager.SfxVolume = x / 10f).AddTo(_disposables, this);
    		_voiceVolumeSlider.OnSlideAsObservable().Subscribe(x => NaniAudioManager.VoiceVolume = x / 10f).AddTo(_disposables, this);
    		_resetAudioOptionsButton.OnClickAsObservable().Subscribe(_ =>
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    			NaniAudioManager.MasterVolume = NaniAudioManager.Configuration.DefaultMasterVolume;
    			NaniAudioManager.BgmVolume = NaniAudioManager.Configuration.DefaultBgmVolume;
    			NaniAudioManager.SfxVolume = NaniAudioManager.Configuration.DefaultSfxVolume;
    			NaniAudioManager.VoiceVolume = NaniAudioManager.Configuration.DefaultVoiceVolume;
    			ApplyOptionValuesToUi();
    		}).AddTo(_disposables, this);
    		for (int i = 0; i < _pages.Length; i++)
    		{
    			PageCanvas page = _pages[i];
    			page.Toggle.OnValueChangedAsObservable().Subscribe(enable =>
    			{
    				if (enable)
    				{
    					InputManager.PlayResponseSfx(ResponseKind.Submit);
    				}
    				page.SetActive(enable);
    			}).AddTo(_disposables, this);
    			page.SetActive(i == 0);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x353FD0, 119 B
    	// Only when _disposables is non-null: Dispose() it and clear the field.
    	[Token(Token = "0x60002DF")]
    	[Address(RVA = "0x353FD0", Offset = "0x3529D0", VA = "0x180353FD0")]
    	private void Dispose()
    	{
    		if (_disposables != null)
    		{
    			_disposables.Dispose();
    			_disposables = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (wrapper names: B) | RVA: 0x354050, 1342 B
    	// - LabeledSlider.SetValue(int) inlined as `_slider (+0x20).value = (float)int` (Slider vtable slot 47); each value is
    	//   RoundToInt(x * 10f) of ITextPrinterManager slot 6/8 and IAudioManager slot 1/3/5/7.
    	// - OptionItemToggleGroup.SetValue(bool ? 1 : 0): SkipMode (IScriptPlayer slot 19) != Everything; CriticalChoiceHintEnabled
    	//   (slot 0); FullScreen (ICameraManagerExtended slot 0); IsPostProcessingEnabled (slot 6).
    	// - (LinqUtils.IndexOf is written in extension form: Naninovel.Common also defines a LinqUtils, without IndexOf.)
    	// - OptionItemDropdown.SetValue inlined as TMP_Dropdown.SetValue(v, sendCallback: false): language index =
    	//   LinqUtils.IndexOf(_availableLocales, SelectedLocale (ILocalizationManager slot 2)); ResolutionLevel (slot 2) -
    	//   _unavailableResolutionOptionCount; TargetFpsLevel (slot 4).
    	[Token(Token = "0x60002E0")]
    	[Address(RVA = "0x354050", Offset = "0x352A50", VA = "0x180354050")]
    	private void ApplyOptionValuesToUi()
    	{
    		_messageSpeedSlider.SetValue(Mathf.RoundToInt(NaniPrinterManager.BaseRevealSpeed * 10f));
    		_autoDelaySlider.SetValue(Mathf.RoundToInt(NaniPrinterManager.BaseAutoDelay * 10f));
    		_skipModeToggleGroup.SetValue(NaniScriptPlayer.SkipMode != PlayerSkipMode.Everything ? 1 : 0);
    		_criticalChoiceHintToggleGroup.SetValue(WitchTrialsChoiceHandlerManager.CriticalChoiceHintEnabled ? 1 : 0);
    		_languageDropdown.SetValue(_availableLocales.IndexOf(NaniLocalizationManager.SelectedLocale));
    		_screenModeToggleGroup.SetValue(NaniCameraManager.FullScreen ? 1 : 0);
    		_resolutionDropdown.SetValue(NaniCameraManager.ResolutionLevel - _unavailableResolutionOptionCount);
    		_frameRateDropdown.SetValue(NaniCameraManager.TargetFpsLevel);
    		_masterVolumeSlider.SetValue(Mathf.RoundToInt(NaniAudioManager.MasterVolume * 10f));
    		_bgmVolumeSlider.SetValue(Mathf.RoundToInt(NaniAudioManager.BgmVolume * 10f));
    		_sfxVolumeSlider.SetValue(Mathf.RoundToInt(NaniAudioManager.SfxVolume * 10f));
    		_voiceVolumeSlider.SetValue(Mathf.RoundToInt(NaniAudioManager.VoiceVolume * 10f));
    		_postProcessingToggleGroup.SetValue(NaniCameraManager.IsPostProcessingEnabled ? 1 : 0);
    	}

    	// RECONSTRUCTED | Confidence: A (SelectLocale overload: B) | RVA: 0x354590 (stub), MoveNext 0x357330, 1517 B
    	// `<>s__2` (+0x28) = NaninovelLoadingScreen.Show(1 = Short, true, Array.Empty<ICancelEventBlockable>()); inside it:
    	// ChangeCaptionVisibility(false), await ILocalizationManager slot 6 SelectLocale(locale) (thunk 0x71F0, string arg),
    	// ChangeCaptionVisibility(true). Only the loading screen is disposed on the exception path.
    	[Token(Token = "0x60002E1")]
    	[Address(RVA = "0x354590", Offset = "0x352F90", VA = "0x180354590")]
    	[AsyncStateMachine(typeof(_003CChangeLocaleAsync_003Ed__54))]
    	private async Cysharp.Threading.Tasks.UniTask ChangeLocaleAsync(string locale)
    	{
    		using (NaninovelLoadingScreen.Show(LoadingMode.Short, haltInput: true, Array.Empty<ICancelEventBlockable>()))
    		{
    			_languageDropdown.ChangeCaptionVisibility(false);
    			await NaniLocalizationManager.SelectLocale(locale);
    			_languageDropdown.ChangeCaptionVisibility(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x354730, 424 B
    	// Start<SaveAsync d__56> inlined with destroyCancellationToken, then Forget().
    	[Token(Token = "0x60002E2")]
    	[Address(RVA = "0x354730", Offset = "0x353130", VA = "0x180354730")]
    	public void Save()
    	{
    		SaveAsync(destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A (TrySetResult spelling: B) | RVA: 0x3548E0 (stub), MoveNext 0x358990, 2485 B
    	// - If _saveUcs (+0x1D0) exists and its Task status (IUniTaskSource slot 0, token 0) is Pending: under
    	//   NaninovelLoadingScreen.Show(Short, true, empty) await that task.
    	// - _saveUcs = new UniTaskCompletionSource(); await NaninovelManager (getter inlined) INaninovelStateSaver slot 0
    	//   SaveSettingsAsync(ct); then TrySignalCompletion(1 = Succeeded) = TrySetResult().
    	[Token(Token = "0x60002E3")]
    	[Address(RVA = "0x3548E0", Offset = "0x3532E0", VA = "0x1803548E0")]
    	[AsyncStateMachine(typeof(_003CSaveAsync_003Ed__56))]
    	private async Cysharp.Threading.Tasks.UniTask SaveAsync([Optional] CancellationToken ct)
    	{
    		if (_saveUcs != null && _saveUcs.Task.Status == Cysharp.Threading.Tasks.UniTaskStatus.Pending)
    		{
    			using (NaninovelLoadingScreen.Show(LoadingMode.Short, haltInput: true, Array.Empty<ICancelEventBlockable>()))
    			{
    				await _saveUcs.Task;
    			}
    		}
    		_saveUcs = new Cysharp.Threading.Tasks.UniTaskCompletionSource();
    		await NaninovelManager.SaveSettingsAsync(ct);
    		_saveUcs.TrySetResult();
    	}

    	[Token(Token = "0x60002E4")]
    	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
    	public OptionsUi()
    	{
    	}
    }
}
