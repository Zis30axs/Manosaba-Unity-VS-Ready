using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
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
		[Token(Token = "0x6000072")]
		[Address(RVA = "0x311C30", Offset = "0x310630", VA = "0x180311C30")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000007")]
	private IDictionary<LocaleKind, Sprite> Map
	{
		[Token(Token = "0x6000073")]
		[Address(RVA = "0x311CF0", Offset = "0x3106F0", VA = "0x180311CF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000074")]
	[Address(RVA = "0x312110", Offset = "0x310B10", VA = "0x180312110")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6000075")]
	[Address(RVA = "0x312290", Offset = "0x310C90", VA = "0x180312290")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6000076")]
	[Address(RVA = "0x312300", Offset = "0x310D00", VA = "0x180312300")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000077")]
	[Address(RVA = "0x3123A0", Offset = "0x310DA0", VA = "0x1803123A0")]
	private void ModifyImageByLocale(LocaleKind locale)
	{
	}

	[Token(Token = "0x6000078")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LocalizeImage()
	{
	}
}
