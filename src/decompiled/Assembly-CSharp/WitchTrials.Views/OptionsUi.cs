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

namespace WitchTrials.Views;

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
			[Token(Token = "0x60002F9")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x60002FA")]
		[Address(RVA = "0x356FD0", Offset = "0x3559D0", VA = "0x180356FD0")]
		public void SetActive(bool active)
		{
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
		[Token(Token = "0x60002D4")]
		[Address(RVA = "0x352930", Offset = "0x351330", VA = "0x180352930")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000057")]
	private INaninovelManager NaninovelManager
	{
		[Token(Token = "0x60002D5")]
		[Address(RVA = "0x352A00", Offset = "0x351400", VA = "0x180352A00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000058")]
	private ICameraManagerExtended NaniCameraManager
	{
		[Token(Token = "0x60002D6")]
		[Address(RVA = "0x352AD0", Offset = "0x3514D0", VA = "0x180352AD0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000059")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x60002D7")]
		[Address(RVA = "0x352BA0", Offset = "0x3515A0", VA = "0x180352BA0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700005A")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60002D8")]
		[Address(RVA = "0x352C70", Offset = "0x351670", VA = "0x180352C70")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700005B")]
	private ITextPrinterManager NaniPrinterManager
	{
		[Token(Token = "0x60002D9")]
		[Address(RVA = "0x352D40", Offset = "0x351740", VA = "0x180352D40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700005C")]
	private IWitchTrialsChoiceHandlerManager WitchTrialsChoiceHandlerManager
	{
		[Token(Token = "0x60002DA")]
		[Address(RVA = "0x352E10", Offset = "0x351810", VA = "0x180352E10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700005D")]
	private ILocalizationManagerExtended NaniLocalizationManager
	{
		[Token(Token = "0x60002DB")]
		[Address(RVA = "0x352EE0", Offset = "0x3518E0", VA = "0x180352EE0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60002DC")]
	[Address(RVA = "0x352FB0", Offset = "0x3519B0", VA = "0x180352FB0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__49))]
	public override Naninovel.UniTask Initialize()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60002DD")]
	[Address(RVA = "0x3530E0", Offset = "0x351AE0", VA = "0x1803530E0", Slot = "55")]
	protected override void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x60002DE")]
	[Address(RVA = "0x353190", Offset = "0x351B90", VA = "0x180353190")]
	private void ActivateUi()
	{
	}

	[Token(Token = "0x60002DF")]
	[Address(RVA = "0x353FD0", Offset = "0x3529D0", VA = "0x180353FD0")]
	private void Dispose()
	{
	}

	[Token(Token = "0x60002E0")]
	[Address(RVA = "0x354050", Offset = "0x352A50", VA = "0x180354050")]
	private void ApplyOptionValuesToUi()
	{
	}

	[Token(Token = "0x60002E1")]
	[Address(RVA = "0x354590", Offset = "0x352F90", VA = "0x180354590")]
	[AsyncStateMachine(typeof(_003CChangeLocaleAsync_003Ed__54))]
	private Cysharp.Threading.Tasks.UniTask ChangeLocaleAsync(string locale)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x60002E2")]
	[Address(RVA = "0x354730", Offset = "0x353130", VA = "0x180354730")]
	public void Save()
	{
	}

	[Token(Token = "0x60002E3")]
	[Address(RVA = "0x3548E0", Offset = "0x3532E0", VA = "0x1803548E0")]
	[AsyncStateMachine(typeof(_003CSaveAsync_003Ed__56))]
	private Cysharp.Threading.Tasks.UniTask SaveAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x60002E4")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public OptionsUi()
	{
	}
}
