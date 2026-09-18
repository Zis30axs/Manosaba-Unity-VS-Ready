using GigaCreation.Tools.Service;
using System;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x200001E")]
    public class LocalizeCanvasGroup : MonoBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x200001F")]
    	private class LocaleReference
    	{
    		[Token(Token = "0x4000049")]
    		[FieldOffset(Offset = "0x10")]
    		public LocaleKind Locale;

    		[Token(Token = "0x400004A")]
    		[FieldOffset(Offset = "0x18")]
    		public CanvasGroup CanvasGroup;

    		// RECONSTRUCTED | Confidence: A | RVA: 0x311C00, 47 B (1.0 is the float at data RVA 0x2AE2820)
    		[Token(Token = "0x6000070")]
    		[Address(RVA = "0x311C00", Offset = "0x310600", VA = "0x180311C00")]
    		public void SetActive(bool value)
    		{
    			CanvasGroup.alpha = (value ? 1f : 0f);
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000 (shared empty body; no field initializers)
    		[Token(Token = "0x6000071")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public LocaleReference()
    		{
    		}
    	}

    	[Token(Token = "0x4000046")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private LocaleReference[] _settings;

    	[Token(Token = "0x4000047")]
    	[FieldOffset(Offset = "0x28")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x4000048")]
    	[FieldOffset(Offset = "0x30")]
    	private IDisposable _disposable;

    	[Token(Token = "0x17000005")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x311860, 186 B
    		// Lazy cache in +0x28 via `ServiceLocator.Get<ILocalizationManager>(true)` (cl=1); GigaCreation's ILocalizationManager
    		// derives from ILocalizationProvider.
    		[Token(Token = "0x600006B")]
    		[Address(RVA = "0x311860", Offset = "0x310260", VA = "0x180311860")]
    		get
    		{
    			return _localizationProvider ??= ServiceLocator.Get<ILocalizationManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (HandleLocaleChanged written as a call; it is inlined) | RVA: 0x311920, 485 B
    	// `Subscribe` is UniRx ObservableExtensions.Subscribe<LocaleKind>(Action<LocaleKind>) over ILocalizationProvider slot 3 `OnLocaleChangedAsObservable`; `AddTo(this)` is UniRx DisposableExtensions.AddTo<IDisposable>(Component).
    	// The current locale (slot 1 `CurrentLocale`) is applied immediately after subscribing.
    	[Token(Token = "0x600006C")]
    	[Address(RVA = "0x311920", Offset = "0x310320", VA = "0x180311920")]
    	private void OnEnable()
    	{
    		_disposable = LocalizationProvider.OnLocaleChangedAsObservable().Subscribe(HandleLocaleChanged).AddTo(this);
    		HandleLocaleChanged(LocalizationProvider.CurrentLocale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x311B10, 76 B (the subscription is disposed but the field is not cleared)
    	[Token(Token = "0x600006D")]
    	[Address(RVA = "0x311B10", Offset = "0x310510", VA = "0x180311B10")]
    	private void OnDisable()
    	{
    		_disposable?.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x311B60, 147 B
    	// LocaleReference.SetActive is inlined: the canvas group of the matching locale gets alpha 1, all others 0.
    	[Token(Token = "0x600006E")]
    	[Address(RVA = "0x311B60", Offset = "0x310560", VA = "0x180311B60")]
    	private void HandleLocaleChanged(LocaleKind locale)
    	{
    		LocaleReference[] settings = _settings;
    		foreach (LocaleReference localeReference in settings)
    		{
    			localeReference.SetActive(localeReference.Locale == locale);
    		}
    	}

    	[Token(Token = "0x600006F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public LocalizeCanvasGroup()
    	{
    	}
    }
}
