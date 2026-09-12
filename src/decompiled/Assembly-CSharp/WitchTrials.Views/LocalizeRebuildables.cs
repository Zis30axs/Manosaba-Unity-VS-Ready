using System;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

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
		[Token(Token = "0x600007E")]
		[Address(RVA = "0x312660", Offset = "0x311060", VA = "0x180312660")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600007F")]
	[Address(RVA = "0x312720", Offset = "0x311120", VA = "0x180312720")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000080")]
	[Address(RVA = "0x312EB0", Offset = "0x3118B0", VA = "0x180312EB0")]
	public void Activate()
	{
	}

	[Token(Token = "0x6000081")]
	[Address(RVA = "0x313040", Offset = "0x311A40", VA = "0x180313040")]
	public void Deactivate()
	{
	}

	[Token(Token = "0x6000082")]
	[Address(RVA = "0x3130E0", Offset = "0x311AE0", VA = "0x1803130E0")]
	private void RebuildAll(LocaleKind locale)
	{
	}

	[Token(Token = "0x6000083")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public LocalizeRebuildables()
	{
	}
}
