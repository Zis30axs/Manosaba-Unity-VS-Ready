using GigaCreation.Tools.Service;
using System.Linq;
using System;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000023")]
    public class LocalizeRebuildables : MonoBehaviour
    {
    	[Token(Token = "0x4000055")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private RebuildableButton[] _buttons;

    	[Token(Token = "0x4000056")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private RebuildableToggle[] _toggles;

    	[Token(Token = "0x4000057")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private RebuildableLabel[] _labels;

    	[Token(Token = "0x4000058")]
    	[FieldOffset(Offset = "0x38")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x4000059")]
    	[FieldOffset(Offset = "0x40")]
    	private IDisposable _disposable;

    	[Token(Token = "0x17000008")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x312660, 186 B
    		// Lazy cache in +0x38 via `ServiceLocator.Get<ILocalizationManager>(true)` (cl=1); GigaCreation's ILocalizationManager
    		// derives from ILocalizationProvider.
    		[Token(Token = "0x600007E")]
    		[Address(RVA = "0x312660", Offset = "0x311060", VA = "0x180312660")]
    		get
    		{
    			return _localizationProvider ??= ServiceLocator.Get<ILocalizationManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the upcast lambdas' spelling) | RVA: 0x312720, 1932 B;
    	// <>c lambdas b__7_0/b__7_1 0x3133E0 (identity, shared body), b__7_2 0x3133F0
    	// 
    	// Buttons and toggles come from GetComponentsInChildren<T>(includeInactive: true) (inlined through gameObject, dl=1).
    	// Labels are every RebuildableLabel under this object minus the ones owned by a button or toggle (each owner's
    	// `GetComponentsInChildren<RebuildableLabel>()`, the no-argument overload). ToArray/Except are inlined.
    	[Token(Token = "0x600007F")]
    	[Address(RVA = "0x312720", Offset = "0x311120", VA = "0x180312720")]
    	private void Reset()
    	{
    		_buttons = GetComponentsInChildren<RebuildableButton>(includeInactive: true);
    		_toggles = GetComponentsInChildren<RebuildableToggle>(includeInactive: true);
    		RebuildableLabel[] ownedLabels = _buttons.Select((RebuildableButton button) => (Component)button).Concat(_toggles.Select((RebuildableToggle toggle) => (Component)toggle)).SelectMany((Component comp) => comp.GetComponentsInChildren<RebuildableLabel>())
    			.ToArray();
    		_labels = GetComponentsInChildren<RebuildableLabel>(includeInactive: true).Except(ownedLabels).ToArray();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x312EB0, 387 B
    	// Rebuilds for the current locale first, then subscribes to later changes (`Subscribe` is UniRx ObservableExtensions.Subscribe<LocaleKind>(Action<LocaleKind>) over ILocalizationProvider slot 3 `OnLocaleChangedAsObservable`; `AddTo(this)` is UniRx DisposableExtensions.AddTo<IDisposable>(Component).)
    	[Token(Token = "0x6000080")]
    	[Address(RVA = "0x312EB0", Offset = "0x3118B0", VA = "0x180312EB0")]
    	public void Activate()
    	{
    		RebuildAll(LocalizationProvider.CurrentLocale);
    		_disposable = LocalizationProvider.OnLocaleChangedAsObservable().Subscribe(RebuildAll).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x313040, 151 B
    	[Token(Token = "0x6000081")]
    	[Address(RVA = "0x313040", Offset = "0x311A40", VA = "0x180313040")]
    	public void Deactivate()
    	{
    		_disposable?.Dispose();
    		_disposable = null;
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (whether `controlsItsOwnSize: false` was spelled out) | RVA: 0x3130E0, 606 B
    	// 
    	// - RebuildableButton.Rebuild(null, true, bool?(false), locale): the Nullable<bool> ctor runs with value 0, so the
    	//   argument has a value (false), not `default`.
    	// - RebuildableToggle.Rebuild(true, locale) (0xCECC40) is inlined into its private RebuildElements (0xCECCB0).
    	// - RebuildableLabel.Rebuild(null, true, locale).
    	[Token(Token = "0x6000082")]
    	[Address(RVA = "0x3130E0", Offset = "0x311AE0", VA = "0x1803130E0")]
    	private void RebuildAll(LocaleKind locale)
    	{
    		RebuildableButton[] buttons = _buttons;
    		foreach (RebuildableButton rebuildableButton in buttons)
    		{
    			rebuildableButton.Rebuild(null, atRuntime: true, controlsItsOwnSize: false, locale);
    		}
    		RebuildableToggle[] toggles = _toggles;
    		foreach (RebuildableToggle rebuildableToggle in toggles)
    		{
    			rebuildableToggle.Rebuild(atRuntime: true, locale);
    		}
    		RebuildableLabel[] labels = _labels;
    		foreach (RebuildableLabel rebuildableLabel in labels)
    		{
    			rebuildableLabel.Rebuild(null, atRuntime: true, locale);
    		}
    	}

    	[Token(Token = "0x6000083")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public LocalizeRebuildables()
    	{
    	}
    }
}
