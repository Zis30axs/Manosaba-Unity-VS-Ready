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
	private readonly CompositeDisposable _disposables = new CompositeDisposable();

	// RECONSTRUCTED | Confidence: A | RVA: 0x30CE00
	// Real call to SlideToggle.OnEnable (0xCF0140); then `IsActiveSelectableUtil` inlined (0xCF00B0: +0x140 ->
	// SelectableUtility.IsActive) gates a tail jump to Initialize.
	[Token(Token = "0x600001D")]
	[Address(RVA = "0x30CE00", Offset = "0x30B800", VA = "0x18030CE00", Slot = "5")]
	protected override void OnEnable()
	{
		base.OnEnable();
		if (IsActiveSelectableUtil)
		{
			Initialize();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30CE50
	// SlideToggle.OnDisable inlined byte-for-byte (0xCF0350: Toggle.SetToggleGroup(null, false), Selectable.OnDisable,
	// base +0x138 Clear), then this class's +0x180 `_disposables.Clear()`.
	[Token(Token = "0x600001E")]
	[Address(RVA = "0x30CE50", Offset = "0x30B850", VA = "0x18030CE50", Slot = "7")]
	protected override void OnDisable()
	{
		base.OnDisable();
		_disposables.Clear();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30CEB0
	// - `OnHighlightingChangedAsObservable()` inlined (0xCF0A80: SelectableUtil +0x10 `_isHighlighted`) ->
	//   Subscribe(OnHighlightingChanged) -> AddTo(_disposables +0x180, this).
	// - `OnValueChangedAsObservable()` inlined (0xCF0A30: onValueChanged +0x118 AsObservable) -> Subscribe(OnValueChanged)
	//   -> AddTo(_disposables, this).
	// - OnHighlightingChanged(false), then OnValueChanged(isOn) inlined (sprite select + tail jump).
	[Token(Token = "0x600001F")]
	[Address(RVA = "0x30CEB0", Offset = "0x30B8B0", VA = "0x18030CEB0")]
	private void Initialize()
	{
		OnHighlightingChangedAsObservable().Subscribe(OnHighlightingChanged).AddTo(_disposables, this);
		OnValueChangedAsObservable().Subscribe(OnValueChanged).AddTo(_disposables, this);
		OnHighlightingChanged(false);
		OnValueChanged(isOn);
	}

	// RECONSTRUCTED | Confidence: A (`|` spelling: B) | RVA: 0x30D090
	// `or dl, [this+0x120]` (m_IsOn) -> `_defaultLabel` (+0x160) alpha = 0f when highlighted else 1f; `_highlightedLabel`
	// (+0x168) alpha = 1f when highlighted else 0f (constant 0x2AE2820 = 1f).
	[Token(Token = "0x6000020")]
	[Address(RVA = "0x30D090", Offset = "0x30BA90", VA = "0x18030D090")]
	private void OnHighlightingChanged(bool value)
	{
		bool isHighlighted = value | isOn;
		_defaultLabel.alpha = isHighlighted ? 0f : 1f;
		_highlightedLabel.alpha = isHighlighted ? 1f : 0f;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30D110
	// `_base` (+0x158).sprite = [this + value*8 + 0x170] (0x170 `_inactiveBase`, 0x178 `_activeBase`); then
	// tail jump to OnHighlightingChanged(value).
	[Token(Token = "0x6000021")]
	[Address(RVA = "0x30D110", Offset = "0x30BB10", VA = "0x18030D110")]
	private void OnValueChanged(bool value)
	{
		_base.sprite = value ? _activeBase : _inactiveBase;
		OnHighlightingChanged(value);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30D160
	// `_canvas` (+0x148) and `_graphicRaycaster` (+0x150) enabled = true.
	[Token(Token = "0x6000022")]
	[Address(RVA = "0x30D160", Offset = "0x30BB60", VA = "0x18030D160")]
	public void Show()
	{
		_canvas.enabled = true;
		_graphicRaycaster.enabled = true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30D1A0
	// `_canvas` (+0x148) and `_graphicRaycaster` (+0x150) enabled = false.
	[Token(Token = "0x6000023")]
	[Address(RVA = "0x30D1A0", Offset = "0x30BBA0", VA = "0x18030D1A0")]
	public void Hide()
	{
		_canvas.enabled = false;
		_graphicRaycaster.enabled = false;
	}

	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x30D1E0, 468 B
	// Only the +0x180 `_disposables = new CompositeDisposable()` store belongs to this class (written as the field
	// initializer). The +0x130 `new SlideUtility` (float +0x14 = 0.1f) and +0x138 `new CompositeDisposable()` stores are
	// SlideToggle's own field initializers, inlined, followed by the tail jump to Toggle..ctor.
	[Token(Token = "0x6000024")]
	[Address(RVA = "0x30D1E0", Offset = "0x30BBE0", VA = "0x18030D1E0")]
	public WitchTrialsTabToggle()
	{
	}
}
