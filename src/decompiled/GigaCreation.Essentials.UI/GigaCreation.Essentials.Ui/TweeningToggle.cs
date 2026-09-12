using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000050")]
public class TweeningToggle : Toggle
{
	[Token(Token = "0x400014B")]
	[FieldOffset(Offset = "0x128")]
	[Header("References")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x400014C")]
	[FieldOffset(Offset = "0x130")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x400014D")]
	[FieldOffset(Offset = "0x138")]
	[Header("Parameters")]
	[SerializeField]
	private bool _scaleUpWhenHighlighted;

	[Token(Token = "0x400014E")]
	[FieldOffset(Offset = "0x140")]
	[SerializeField]
	private ScaleUtility _scaleUtil;

	[Token(Token = "0x400014F")]
	[FieldOffset(Offset = "0x148")]
	[SerializeField]
	private bool _fadeOutWhenOff;

	[Token(Token = "0x4000150")]
	[FieldOffset(Offset = "0x149")]
	[SerializeField]
	private bool _fadeOutWhenDisabled;

	[Token(Token = "0x4000151")]
	[FieldOffset(Offset = "0x150")]
	[SerializeField]
	private FadeUtility _fadeUtil;

	[Token(Token = "0x4000152")]
	[FieldOffset(Offset = "0x158")]
	[Space]
	[SerializeField]
	private bool _disableRaycastBlockingWhenOff;

	[Token(Token = "0x4000153")]
	[FieldOffset(Offset = "0x160")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x4000154")]
	[FieldOffset(Offset = "0x168")]
	private SelectableUtility _selectableUtil;

	[Token(Token = "0x17000045")]
	private SelectableUtility SelectableUtil
	{
		[Token(Token = "0x600019D")]
		[Address(RVA = "0xCF0BF0", Offset = "0xCEF5F0", VA = "0x180CF0BF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600019E")]
	[Address(RVA = "0xCF0CC0", Offset = "0xCEF6C0", VA = "0x180CF0CC0", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x600019F")]
	[Address(RVA = "0xCF0D30", Offset = "0xCEF730", VA = "0x180CF0D30", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60001A0")]
	[Address(RVA = "0xCF0D70", Offset = "0xCEF770", VA = "0x180CF0D70")]
	private void Initialize()
	{
	}

	[Token(Token = "0x60001A1")]
	[Address(RVA = "0xCF0F90", Offset = "0xCEF990", VA = "0x180CF0F90")]
	private void OnHighlightingChanged(bool isHighlighted)
	{
	}

	[Token(Token = "0x60001A2")]
	[Address(RVA = "0xCF1010", Offset = "0xCEFA10", VA = "0x180CF1010")]
	private void OnEnablingChanged(bool isEnabled)
	{
	}

	[Token(Token = "0x60001A3")]
	[Address(RVA = "0xCF1070", Offset = "0xCEFA70", VA = "0x180CF1070")]
	private void OnValueChanged(bool turnedOn)
	{
	}

	[Token(Token = "0x60001A4")]
	[Address(RVA = "0xCF10E0", Offset = "0xCEFAE0", VA = "0x180CF10E0", Slot = "26")]
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
	}

	[Token(Token = "0x60001A5")]
	[Address(RVA = "0xCF1270", Offset = "0xCEFC70", VA = "0x180CF1270")]
	private bool IsFadeEnabled()
	{
		return default(bool);
	}

	[Token(Token = "0x60001A6")]
	[Address(RVA = "0xCF1290", Offset = "0xCEFC90", VA = "0x180CF1290")]
	public TweeningToggle()
	{
	}
}
