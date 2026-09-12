using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200000C")]
public sealed class WitchTrialsTabToggle : SlideToggle
{
	[Token(Token = "0x4000010")]
	[FieldOffset(Offset = "0x148")]
	[Header("References for WitchTrials")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x4000011")]
	[FieldOffset(Offset = "0x150")]
	[SerializeField]
	private GraphicRaycaster _graphicRaycaster;

	[Token(Token = "0x4000012")]
	[FieldOffset(Offset = "0x158")]
	[SerializeField]
	private Image _base;

	[Token(Token = "0x4000013")]
	[FieldOffset(Offset = "0x160")]
	[SerializeField]
	private CanvasGroup _defaultLabel;

	[Token(Token = "0x4000014")]
	[FieldOffset(Offset = "0x168")]
	[SerializeField]
	private CanvasGroup _highlightedLabel;

	[Token(Token = "0x4000015")]
	[FieldOffset(Offset = "0x170")]
	[Header("Parameters for WitchTrials")]
	[SerializeField]
	private Sprite _inactiveBase;

	[Token(Token = "0x4000016")]
	[FieldOffset(Offset = "0x178")]
	[SerializeField]
	private Sprite _activeBase;

	[Token(Token = "0x4000017")]
	[FieldOffset(Offset = "0x180")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x600001D")]
	[Address(RVA = "0x30CE00", Offset = "0x30B800", VA = "0x18030CE00", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x600001E")]
	[Address(RVA = "0x30CE50", Offset = "0x30B850", VA = "0x18030CE50", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x600001F")]
	[Address(RVA = "0x30CEB0", Offset = "0x30B8B0", VA = "0x18030CEB0")]
	private void Initialize()
	{
	}

	[Token(Token = "0x6000020")]
	[Address(RVA = "0x30D090", Offset = "0x30BA90", VA = "0x18030D090")]
	private void OnHighlightingChanged(bool value)
	{
	}

	[Token(Token = "0x6000021")]
	[Address(RVA = "0x30D110", Offset = "0x30BB10", VA = "0x18030D110")]
	private void OnValueChanged(bool value)
	{
	}

	[Token(Token = "0x6000022")]
	[Address(RVA = "0x30D160", Offset = "0x30BB60", VA = "0x18030D160")]
	public void Show()
	{
	}

	[Token(Token = "0x6000023")]
	[Address(RVA = "0x30D1A0", Offset = "0x30BBA0", VA = "0x18030D1A0")]
	public void Hide()
	{
	}

	[Token(Token = "0x6000024")]
	[Address(RVA = "0x30D1E0", Offset = "0x30BBE0", VA = "0x18030D1E0")]
	public WitchTrialsTabToggle()
	{
	}
}
