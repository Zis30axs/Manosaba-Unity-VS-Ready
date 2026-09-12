using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x200004E")]
public class SlideToggle : Toggle
{
	[Token(Token = "0x4000144")]
	[FieldOffset(Offset = "0x128")]
	[Header("References")]
	[SerializeField]
	private RectTransform _rectTransform;

	[Token(Token = "0x4000145")]
	[FieldOffset(Offset = "0x130")]
	[Header("Parameters")]
	[SerializeField]
	private SlideUtility _slideUtil;

	[Token(Token = "0x4000146")]
	[FieldOffset(Offset = "0x138")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x4000147")]
	[FieldOffset(Offset = "0x140")]
	private SelectableUtility _selectableUtil;

	[Token(Token = "0x17000040")]
	private SelectableUtility SelectableUtil
	{
		[Token(Token = "0x600018D")]
		[Address(RVA = "0xCEFFE0", Offset = "0xCEE9E0", VA = "0x180CEFFE0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000041")]
	protected bool IsActiveSelectableUtil
	{
		[Token(Token = "0x600018E")]
		[Address(RVA = "0xCF00B0", Offset = "0xCEEAB0", VA = "0x180CF00B0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x600018F")]
	[Address(RVA = "0xCF00E0", Offset = "0xCEEAE0", VA = "0x180CF00E0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000190")]
	[Address(RVA = "0xCF0140", Offset = "0xCEEB40", VA = "0x180CF0140", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6000191")]
	[Address(RVA = "0xCF0350", Offset = "0xCEED50", VA = "0x180CF0350", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6000192")]
	[Address(RVA = "0xCF0390", Offset = "0xCEED90", VA = "0x180CF0390")]
	private void Initialize()
	{
	}

	[Token(Token = "0x6000193")]
	[Address(RVA = "0xCF0550", Offset = "0xCEEF50", VA = "0x180CF0550")]
	private void OnHighlightingChanged(bool isHighlighted)
	{
	}

	[Token(Token = "0x6000194")]
	[Address(RVA = "0xCF0860", Offset = "0xCEF260", VA = "0x180CF0860")]
	private void OnValueChanged(bool value)
	{
	}

	[Token(Token = "0x6000195")]
	[Address(RVA = "0xCF08A0", Offset = "0xCEF2A0", VA = "0x180CF08A0", Slot = "26")]
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
	}

	[Token(Token = "0x6000196")]
	[Address(RVA = "0xCF0A30", Offset = "0xCEF430", VA = "0x180CF0A30")]
	protected IObservable<bool> OnValueChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x6000197")]
	[Address(RVA = "0xCF0A80", Offset = "0xCEF480", VA = "0x180CF0A80")]
	protected IObservable<bool> OnHighlightingChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x6000198")]
	[Address(RVA = "0xCF0AA0", Offset = "0xCEF4A0", VA = "0x180CF0AA0")]
	public SlideToggle()
	{
	}
}
