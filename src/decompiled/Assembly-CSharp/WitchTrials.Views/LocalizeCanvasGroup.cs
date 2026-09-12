using System;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

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

		[Token(Token = "0x6000070")]
		[Address(RVA = "0x311C00", Offset = "0x310600", VA = "0x180311C00")]
		public void SetActive(bool value)
		{
		}

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
		[Token(Token = "0x600006B")]
		[Address(RVA = "0x311860", Offset = "0x310260", VA = "0x180311860")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600006C")]
	[Address(RVA = "0x311920", Offset = "0x310320", VA = "0x180311920")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x600006D")]
	[Address(RVA = "0x311B10", Offset = "0x310510", VA = "0x180311B10")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x600006E")]
	[Address(RVA = "0x311B60", Offset = "0x310560", VA = "0x180311B60")]
	private void HandleLocaleChanged(LocaleKind locale)
	{
	}

	[Token(Token = "0x600006F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LocalizeCanvasGroup()
	{
	}
}
