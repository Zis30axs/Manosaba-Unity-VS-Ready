using GigaCreation.Tools.Service;
using System.Linq;
using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000020")]
[RequireComponent(typeof(Image))]
public class LocalizeImage : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x2000021")]
	private class LocaleContent
	{
		[Token(Token = "0x4000050")]
		[FieldOffset(Offset = "0x10")]
		public LocaleKind Locale;

		[Token(Token = "0x4000051")]
		[FieldOffset(Offset = "0x18")]
		public Sprite Sprite;

		// RECONSTRUCTED | Confidence: A | RVA: 0x30C000 (shared empty body; no field initializers)
		[Token(Token = "0x6000079")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public LocaleContent()
		{
		}
	}

	[Token(Token = "0x400004B")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Image _image;

	[Token(Token = "0x400004C")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private LocaleContent[] _settings;

	[Token(Token = "0x400004D")]
	[FieldOffset(Offset = "0x30")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x400004E")]
	[FieldOffset(Offset = "0x38")]
	private IDictionary<LocaleKind, Sprite> _map;

	[Token(Token = "0x400004F")]
	[FieldOffset(Offset = "0x40")]
	private IDisposable _disposable;

	[Token(Token = "0x17000006")]
	private ILocalizationProvider LocalizationProvider
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x311C30, 186 B
		// Lazy cache in +0x30 via `ServiceLocator.Get<ILocalizationManager>(true)` (cl=1); GigaCreation's ILocalizationManager
		// derives from ILocalizationProvider.
		[Token(Token = "0x6000072")]
		[Address(RVA = "0x311C30", Offset = "0x310630", VA = "0x180311C30")]
		get
		{
			return _localizationProvider ??= ServiceLocator.Get<ILocalizationManager>(true);
		}
	}

	[Token(Token = "0x17000007")]
	private IDictionary<LocaleKind, Sprite> Map
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x311CF0, 1044 B; <>c lambdas b__8_0 0x312620 (+0x10 Locale), b__8_1 0x312640 (+0x18 Sprite)
		// 
		// Built once: `_settings.ToDictionary(Locale, Sprite)`, then the image's own sprite (Image.sprite inlined, +0xE0) is
		// added under ILocalizationProvider slot 0 `SourceLocale` via IDictionary slot 5 `Add`. A setting for the source
		// locale therefore throws on the duplicate key.
		[Token(Token = "0x6000073")]
		[Address(RVA = "0x311CF0", Offset = "0x3106F0", VA = "0x180311CF0")]
		get
		{
			if (_map == null)
			{
				_map = _settings.ToDictionary((LocaleContent content) => content.Locale, (LocaleContent content) => content.Sprite);
				_map.Add(LocalizationProvider.SourceLocale, _image.sprite);
			}
			return _map;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x312110, 382 B (ends in a tail call to ModifyImageByLocale(CurrentLocale))
	// `Subscribe` is UniRx ObservableExtensions.Subscribe<LocaleKind>(Action<LocaleKind>) over ILocalizationProvider slot 3 `OnLocaleChangedAsObservable`; `AddTo(this)` is UniRx DisposableExtensions.AddTo<IDisposable>(Component).
	[Token(Token = "0x6000074")]
	[Address(RVA = "0x312110", Offset = "0x310B10", VA = "0x180312110")]
	private void OnEnable()
	{
		_disposable = LocalizationProvider.OnLocaleChangedAsObservable().Subscribe(ModifyImageByLocale).AddTo(this);
		ModifyImageByLocale(LocalizationProvider.CurrentLocale);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x312290, 100 B (override sprite cleared; subscription disposed, field kept)
	[Token(Token = "0x6000075")]
	[Address(RVA = "0x312290", Offset = "0x310C90", VA = "0x180312290")]
	private void OnDisable()
	{
		_image.overrideSprite = null;
		_disposable?.Dispose();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x312300, 151 B
	[Token(Token = "0x6000076")]
	[Address(RVA = "0x312300", Offset = "0x310D00", VA = "0x180312300")]
	private void Reset()
	{
		_image = GetComponent<Image>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3123A0, 479 B
	// 
	// `_image` is read before `Map` (assignment target first). IDictionary slot 7 `TryGetValue`; the fallback reads the
	// `_map` field directly (+0x38, not the getter) with slot 0 `this[SourceLocale]`. Then Image vtable +0x408 (slot 45)
	// `SetNativeSize()`.
	[Token(Token = "0x6000077")]
	[Address(RVA = "0x3123A0", Offset = "0x310DA0", VA = "0x1803123A0")]
	private void ModifyImageByLocale(LocaleKind locale)
	{
		_image.overrideSprite = (Map.TryGetValue(locale, out Sprite value) ? value : _map[LocalizationProvider.SourceLocale]);
		_image.SetNativeSize();
	}

	[Token(Token = "0x6000078")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LocalizeImage()
	{
	}
}
